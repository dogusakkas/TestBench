using ScrewTest.DataType;
using ScrewTest.Screw.Communication.Interface;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ScrewTest.Screw.Communication.ScrewCommunication
{
    public class EthernetCommunicaton : ICommunicationStrategy
    {
        public TcpListener _listener;
        public CancellationTokenSource _cts;
        private string _ip { get; set; }
        private int _port { get; set; }
        private TcpClient _client { get; set; }
        public NetworkStream _stream { get; set; }

        public PictureBox ConnectionStatusImage { get; set; }
        public Label ConnectionTypeLabel { get; set; }

        CancellationTokenSource ICommunicationStrategy._cts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public EthernetCommunicaton(string ip = "127.0.0.1", int port = 4545)
        {
            _ip = ip;
            _port = port;
            _cts = new CancellationTokenSource();
        }

        public async Task<StandardData> Connect()
        {
            try
            {
                if (_client?.Connected == true && _stream != null)
                {
                    return new StandardData(true, "Bağlantı zaten açık.");
                }

                _client = new TcpClient();
                var connectTask = _client.ConnectAsync(_ip, _port);
                var timeoutTask = Task.Delay(5000);

                var completedTask = await Task.WhenAny(connectTask, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    _client.Close();
                    _client = null;
                    //UpdateConnectionStatus();
                    return new StandardData(false, "Bağlantı zaman aşımına uğradı.");
                }

                _stream = _client.GetStream();
                _cts = new CancellationTokenSource();
                //UpdateConnectionStatus();
                //UpdateConnectionType();

                return new StandardData(true, "Bağlantı başarılı.");
            }
            catch (Exception e)
            {
                //UpdateConnectionStatus();
                return new StandardData(false, $"Ethernet bağlantısı açılamadı. Hata: {e.Message}");
            }
        }

        public async Task<StandardData> Disconnect()
        {
            try
            {
                if (_client?.Connected != true && _stream == null && _listener == null)
                {
                    return new StandardData(true, "Bağlantı zaten kapalı.");
                }

                _cts?.Cancel();

                _stream?.Close();
                _stream = null;

                _client?.Close();
                _client = null;

                _listener?.Stop();
                _listener = null;

                Console.WriteLine($"Bağlantı kapatıldı. {DateTime.Now}");
                return new StandardData(true, "Ethernet bağlantısı kapatıldı.");
            }
            catch (Exception ex)
            {
                return new StandardData(false, $"Ethernet bağlantısı kapatılamadı. Hata: {ex.Message}");
            }
        }

        public async Task<StandardData> ReadData()
        {
            string data = "";

            try
            {
                while (!_cts.IsCancellationRequested)
                {
                    byte[] buffer = new byte[_client.ReceiveBufferSize];
                    var bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length, _cts.Token);

                    if (bytesRead == 0)
                    {
                        Console.WriteLine("Sunucu bağlantıyı kapattı. Thread sonlandırılıyor.");
                        break;
                    }

                    data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"{DateTime.Now} Alınan veri : " + data);

                    return new StandardData(data, true, "data başarıyla alındı.");

                }
            }
            catch (Exception ex)
            {
                if (_cts.IsCancellationRequested)
                {
                    return new StandardData(false, $"Ethernet port dinleme işlemi iptal edildi.", StandardData.OperationStatus.CancelledByUser);
                }

                Console.WriteLine("ReadData CATCH", ex.Message);

                await Disconnect();
                return new StandardData(false, $"Ethernet port dinleme işleminde beklenmeyen bir hata oluştu. Hata: {ex.Message}");
            }

            return new StandardData(true, data);
        }

        public async Task<StandardData> OneReadData()
        {
            string data = "";

            try
            {
                byte[] buffer = new byte[_client.ReceiveBufferSize];

                if (_stream == null)
                {
                    return new StandardData(false, "Bağlantı oluşturulamadı. Tekrar deneniyor");
                }

                var readTask = _stream.ReadAsync(buffer, 0, buffer.Length);
                var timeoutTask = Task.Delay(3000);

                var completedTask = await Task.WhenAny(readTask, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    return new StandardData(false, "Veri okuma zaman aşımına uğradı (3 saniye içinde veri gelmedi).");
                }

                int bytesRead = await readTask;

                if (bytesRead == 0)
                {
                    Console.WriteLine("Sunucu bağlantıyı kapattı. Thread sonlandırılıyor.");
                    return new StandardData(false, "Sunucu bağlantıyı kapattı.");
                }

                data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"{DateTime.Now} Alınan veri : " + data);
            }
            catch (Exception ex)
            {
                if (_cts.IsCancellationRequested)
                {
                    return new StandardData(false, $"Ethernet port dinleme işlemi iptal edildi. - ReadData", StandardData.OperationStatus.CancelledByUser);
                }

                Console.WriteLine("ReadData CATCH", ex.Message);
                await Disconnect();
                return new StandardData(false, $"Ethernet port dinleme işleminde beklenmeyen bir hata oluştu. Hata: {ex.Message}");
            }

            return new StandardData(true, data);
        }

        public async Task<StandardData> WriteData(bool result, string data = "")
        {
            try
            {
                //if (IsSocketConnected(_client.Client))
                if (_stream != null && IsSocketConnected(_client.Client))
                {
                    Debug.WriteLine("Bağlantı hazır, veri gönderiliyor.");
                    byte[] bytes = Encoding.Default.GetBytes(data);
                    await _stream.WriteAsync(bytes, 0, bytes.Length);
                    await _stream.FlushAsync();

                    await Task.Delay(50);

                    return new StandardData(true, "Veri gönderildi.");
                }
                else
                {
                    Debug.WriteLine("Bağlantı yok, veri gönderilemiyor.");
                    return new StandardData(false, "Bağlantı oluşturulamadı. Tekrar deneniyor");
                }
            }
            catch (Exception ex)
            {
                return new StandardData(false, $"Veri gönderilemedi: {ex.Message}");
            }
        }

        private bool IsSocketConnected(Socket s)
        {
            try
            {
                return !(s.Poll(1, SelectMode.SelectRead) && s.Available == 0);
            }
            catch (SocketException) { return false; }
        }

        public void UpdateConnectionStatus()
        {
            throw new NotImplementedException();
        }

        public void UpdateConnectionType()
        {
            throw new NotImplementedException();
        }


        public async Task StartListening()
        {
            int port = 4545;
            _cts = new CancellationTokenSource();

            if (_listener == null)
            {
                _listener = new TcpListener(IPAddress.Any, port);
                _listener.Start();
                Console.WriteLine($"[TestBench] Dinlemeye başlandı. Port: {port}");

                try
                {
                    //using (_cts.Token.Register(() => _listener.Stop()))
                    //{
                    //    _client = await _listener.AcceptTcpClientAsync();
                    //}


                    while (!_cts.IsCancellationRequested)
                    {
                        _client = await _listener.AcceptTcpClientAsync();
                        Console.WriteLine("[TestBench] Client bağlandı!");
                        _stream = _client.GetStream();
                        _ = Task.Run(() => ReadLoop());
                    }

                }
                catch (ObjectDisposedException)
                {
                    Console.WriteLine("[TestBench] Listener durduruldu.");
                    StopListening();
                    return;
                }
                catch (SocketException ex) when (ex.SocketErrorCode == SocketError.OperationAborted)
                {
                    Console.WriteLine("[TestBench] Accept iptal edildi.");
                    StopListening();
                    return;
                }

                Console.WriteLine($"[TestBench] Client bağlandı!");

                _stream = _client.GetStream();
                _ = Task.Run(() => ReadLoop());
            }
        }

        public void StopListening()
        {
            try
            {
                _cts?.Cancel();

                _stream?.Close();
                _client?.Close();

                _listener?.Stop();

                Console.WriteLine("[TestBench] Listener kapatıldı.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TestBench] Stop sırasında hata: {ex.Message}");
            }
        }



        public static class PsetData
        {
            public static List<string> PsetList { get; set; } = new List<string>();
        }


        private async Task ReadLoop()
        {
            byte[] buffer = new byte[1024];

            try
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length, _cts.Token);

                    if (bytesRead == 0)
                    {
                        Console.WriteLine("[TestBench] Client bağlantıyı kapattı.");
                        break;
                    }

                    string data = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim('\0');
                    Console.WriteLine($"[TestBench] Gelen Veri: {data}");

                    // Gelen MID0001'e cevap ver -- CONNECT MACHINE
                    if (data.Substring(4, 4) == "0001") // MID0001 geldi
                    {
                        string mid0002Response = "00200002            \0"; // MID0002
                        byte[] responseBytes = Encoding.ASCII.GetBytes(mid0002Response);
                        await _stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                        await _stream.FlushAsync();

                        Console.WriteLine("[TestBench] MID0002 cevabı gönderildi.");
                    }

                    // Gelen MID0060'a cevap ver -- PF8000 SUBSCRIBE
                    if (data.Substring(4, 4) == "0060") // MID0060 geldi
                    {
                        string MID5 = "00240005001 0000    0060\0"; // MID0005

                        byte[] responseBytes = Encoding.ASCII.GetBytes(MID5);
                        await _stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                        await _stream.FlushAsync();

                        Console.WriteLine("[TestBench] MID0005 cevabı gönderildi.");
                    }

                    // Gelen MID008'a cevap ver -- MT6000 SUBSCRIBE
                    if (data.Substring(4, 4) == "0008") // MID0060 geldi
                    {
                        string MID5 = "00240005001 0000    0060\0"; // MID0005

                        byte[] responseBytes = Encoding.ASCII.GetBytes(MID5);
                        await _stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                        await _stream.FlushAsync();

                        Console.WriteLine("[TestBench] MID0005 cevabı gönderildi.");
                    }

                    // Gelen MID9999'a cevap ver -- KEEPALIVE
                    if (data.Substring(4, 4) == "9999") // MID9999 geldi
                    {
                        string MID9999 = "00209999    0000    \0"; // MID9999

                        byte[] responseBytes = Encoding.ASCII.GetBytes(MID9999);
                        await _stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                        await _stream.FlushAsync();

                        Console.WriteLine("[TestBench] MID9999 cevabı gönderildi.");
                    }

                    // Gelen MID0018'a cevap ver -- PSET
                    if (data.Substring(4, 4) == "0018")
                    {
                        string parameterSetIDStr = data.Substring(21, 2).Trim();
                        int intConvert = Convert.ToInt32(parameterSetIDStr);
                        string parameterSet = intConvert.ToString();
                        bool psetExists = PsetData.PsetList.Any(x => x == parameterSet);


                        string response = psetExists
                        ? $"00240005 001 0000    0018{parameterSetIDStr}\0"
                        : $"00240004 001 0000    0018{parameterSetIDStr}\0";


                        byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                        await _stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                        await _stream.FlushAsync();

                        Console.WriteLine($"[TestBench] MID0018 için {(psetExists ? "başarılı" : "başarısız")} cevap gönderildi.");
                    }


                    // Gelen MID0003'a cevap ver -- DISCONNECT
                    if (data.Substring(4, 4) == "0003") // MID0003 geldi
                    {
                        ////_cts.Cancel();
                        //_stream?.Close();
                        //_client?.Close();
                        //await _stream.FlushAsync();

                        break;

                        Console.WriteLine("[TestBench] MID0003 cevap gönderilmedi. MID0003 cevaplanacak bir MID kodu değil.");
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TestBench] ReadLoop hatası: {ex.Message}");
            }
        }

    }
}
