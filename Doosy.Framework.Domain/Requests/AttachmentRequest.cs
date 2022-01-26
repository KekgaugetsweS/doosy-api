using System.IO;

namespace Doosy.Framework.Domain
{
    public class AttachmentRequest
    {
        public Stream StreamData { get; set; }
        public string AttachmentName { get; set; }
        public string MimeType { get; set; }
    }
}
