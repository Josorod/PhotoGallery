namespace WebApi.Exeptions
{
    public class PhotoGalleryFailedRegisterException : PhotoGalleryException
    {
        public PhotoGalleryFailedRegisterException() : base()
        {
        }

        public PhotoGalleryFailedRegisterException(string message) : base(message)
        {
        }
    }
}
