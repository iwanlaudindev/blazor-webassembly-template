using System.ComponentModel.DataAnnotations;
using Water.Management.Shared.ViewModels;

namespace Water.Management.Shared.Models;

public class Geophysics
{
    public Geophysics() { }

    public Geophysics(GeophysicsVm model) 
    {
        Code = model.Code;
        Aquifer = model.Aquifer;
        Elevation = model.Elevation;
        StartSurveyDt = model.StartSurveyDt;
        EndSurveyDt = model.EndSurveyDt;
        Latitude = model.Latitude;
        Longitude = model.Longitude;
        ProvinceId = model.ProvinceId;
        CityId = model.CityId;
        DistrictId = model.DistrictId;
        SubDistrictId = model.SubDistrictId;
        DetailLocation = model.DetailLocation;
        SocialMapping = model.SocialMapping;
        Vegetation = model.Vegetation;
        Demography = model.Demography;
        GeneralGeology = model.GeneralGeology;
        Rainfall = model.Rainfall;
    }

    [Required]
    [RegularExpression(@"^\S*$", ErrorMessage = "ID cannot contain spaces.")]
    [Display(Name = "ID")]
    public string? Code { get; set; }
    public double? Aquifer { get; set; }
    public double? Elevation { get; set; }
    [Required]
    [Display(Name = "Social Mapping")]
    public string? SocialMapping { get; set; }
    [Required]
    [Display(Name = "Rainfall")]
    public string? Rainfall { get; set; }
    [Required]
    [Display(Name = "Vegetation")]
    public string? Vegetation { get; set; }
    [Required]
    [Display(Name = "Demography")]
    public string? Demography { get; set; }
    [Required]
    [Display(Name = "General Geology")]
    public string? GeneralGeology { get; set; }
    [Required]
    [Display(Name = "Start Date")]
    public DateOnly? StartSurveyDt { get; set; }
    [Required]
    [Display(Name = "End Date")]
    public DateOnly? EndSurveyDt { get; set; }
    [Required]
    [Display(Name = "Province")]
    public string? ProvinceId { get; set; }
    [Required]
    [Display(Name = "City")]
    public string? CityId { get; set; }
    [Required]
    [Display(Name = "District")]
    public string? DistrictId { get; set; }
    [Required]
    [Display(Name = "Sub District")]
    public string? SubDistrictId { get; set; }
    public string? DetailLocation { get; set; }
    [Required]
    [Display(Name = "Longitude")]
    public double? Longitude { get; set; }
    [Required]
    [Display(Name = "Latitude")]
    public double? Latitude { get; set; }
}
