using DohrniiFoundation.Helpers;
using Newtonsoft.Json;

namespace DohrniiFoundation.Models.Lessons
{
    /// <summary>
    /// This model class is to bind properties of all the chapters categories details
    /// </summary>
    public class ChaptersCategoryModel : ObservableObject
    {
        #region Properties
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("category_name")]
        public string CategoryName { get; set; }
        //[JsonProperty("created_at")]
        //public DateTime CreatedAt { get; set; }
        //[JsonProperty("updated_at")]
        //public DateTime UpdatedAt { get; set; }
        //[JsonProperty("deleted_at")]
        //public object DeletedAt { get; set; }
        private bool isNotGrdientVisible;
        public bool IsNotGrdientVisible
        {
            get { return isNotGrdientVisible; }
            set
            {
                if (isNotGrdientVisible != value)
                {
                    isNotGrdientVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private bool isGradientVisible;
        public bool IsGradientVisible
        {
            get { return isGradientVisible; }
            set
            {
                if (isGradientVisible != value)
                {
                    isGradientVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private bool isCategorySelected;
        public bool IsCategorySelected
        {
            get { return isCategorySelected; }
            set
            {
                if (isCategorySelected != value)
                {
                    isCategorySelected = value;
                    this.OnPropertyChanged();
                }
            }
        }
        #endregion
    }
}
