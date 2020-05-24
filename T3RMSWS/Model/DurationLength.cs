using System.ComponentModel.DataAnnotations;

namespace T3RMSWS.Data
{
    public enum DurationLength
    {
        [Display(Name = "0.5 hour")]
        HalfHour = 1,
        [Display(Name = "1.0 hour")]
        OneHour = 2,
        [Display(Name = "1.5 hours")]
        OneAndHalfHours = 3,
        [Display(Name = "2.0 hours")]
        TwoHour = 4,
        [Display(Name = "2.5 hours")]
        TwoAndHalfHours = 5,
        [Display(Name = "3.0 hours")]
        ThreeHour = 6,
        [Display(Name = "3.5 hours")]
        ThreeAndHalfHours = 7,
        [Display(Name = "4.0 hours")]
        FourHour = 8,
    }
}
