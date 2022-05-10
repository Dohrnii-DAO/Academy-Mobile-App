
namespace DohrniiFoundation.IServices
{
    /// <summary>
    /// This interface is to set the orientation
    /// </summary>
    public interface IOrientationService
    {
        /// <summary>
        /// This method is to set the application in the landscape mode
        /// </summary>
        void Landscape();
        /// <summary>
        /// This method is to se the application in the portrait mode
        /// </summary>
        void Portrait();
    }
}
