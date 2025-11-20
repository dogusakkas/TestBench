using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewTest.DataType
{
    public class StandardData
    {

        /// <summary>
        /// Mevcut operasyon durumunun kontrol edildiği alandır.
        /// </summary>
        public enum OperationStatus
        {
            Success,
            CancelledByUser,
            CommunicationError,
            Timeout,
            UnexpectedError
        }

        /// <summary>
        /// İşlem sonucunda elde edilen içerik.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// İşlemin başarılı olup olmadığını gösterir.
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// İşlemle ilgili mesaj (başarı veya hata açıklaması).
        /// </summary>
        public string OperationMessage { get; set; }

        public OperationStatus Status { get; set; }

        /// <summary>
        /// Belirtilen içerik, durum ve mesaj bilgileriyle yeni bir StandardData örneği oluşturur.
        /// </summary>
        /// <param name="content">İçerik verisi.</param>
        /// <param name="isSuccessful">İşlemin başarılı olup olmadığı.</param>
        /// <param name="operationMessage">İşlem mesajı.</param>
        public StandardData(string content, bool isSuccessful, string operationMessage, OperationStatus status = OperationStatus.Success)
        {
            Content = content;
            IsSuccessful = isSuccessful;
            OperationMessage = operationMessage;
            Status = status;
        }


        /// <summary>
        /// Sadece içerik verildiğinde, işlemin başarılı olduğunu, varsayılan bir mesajı ve statüs durumunu kabul eder.
        /// </summary>
        /// <param name="content">İçerik verisi.</param>
        public StandardData(bool isSuccessful, string message = "", OperationStatus status = OperationStatus.CancelledByUser)
        : this("", isSuccessful, message, status)
        {
        }

        /// <summary>
        /// Sadece içerik verildiğinde, işlemin başarılı olduğunu ve varsayılan bir mesajı kabul eder.
        /// </summary>
        /// <param name="content">İçerik verisi.</param>
        public StandardData(string content)
            : this(content, true, "İşlem başarılı.")
        {
        }

        /// <summary>
        /// Durum ve İşlem sonucu verildiğinde, içerik boş olarak kabul edilir.
        /// </summary>
        /// <param name="content">İçerik verisi.</param>
        public StandardData(bool isSuccessful, string operationMessage)
            : this(string.Empty, isSuccessful, operationMessage)
        {
        }

        public override string ToString()
        {
            return $"Başarılı: {IsSuccessful}, Mesaj: {OperationMessage}, İçerik: {Content}";
        }
    }
}
