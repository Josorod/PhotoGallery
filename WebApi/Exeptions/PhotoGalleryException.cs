namespace WebApi.Exeptions
{
    public class PhotoGalleryException : Exception
    {
        public PhotoGalleryException() : base()
        {
        }

        public PhotoGalleryException(string message) : base(message)
        {
        }
    }
}
