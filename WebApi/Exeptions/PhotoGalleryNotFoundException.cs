namespace WebApi.Exeptions
{
    public class PhotoGalleryNotFoundException : PhotoGalleryException
    {
        const string defaultMessage = "Item was not found";

        public PhotoGalleryNotFoundException() : base(defaultMessage)
        {
        }

        public PhotoGalleryNotFoundException(Type entityType) : base($"{entityType.Name} was not found")
        {
        }

        public PhotoGalleryNotFoundException(string message) : base(message)
        {
        }
    }
}
