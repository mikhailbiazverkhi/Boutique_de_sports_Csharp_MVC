namespace TP_test.Models
{
    public class Image
    {
        public int ImageID { get; set; }
        public string NomImage { get; set; }
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }
    }
}
