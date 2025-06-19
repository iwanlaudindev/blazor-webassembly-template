using System.ComponentModel.DataAnnotations;
using Water.Management.Shared.Validators;
using Water.Management.Shared.ViewModels;

namespace Water.Management.Shared.Models;

public class WaterSource
{
    public WaterSource()
    {
        
    }

    public WaterSource(WaterSourceVm model)
    {
        Code = model.Code;
        Name = model.Name;
        Ph = model.Ph;
        TDSPPM = model.TDSPPM;
        Depth = model.Depth;
        WaterTable = model.WaterTable;
        Elevation = model.Elevation;
        WaterSupplyTypeId = model.WaterSupplyTypeId;
        TypeOther = model.TypeOther;
        StartSurveyDt = model.StartSurveyDt;
        EndSurveyDt = model.EndSurveyDt;
        Latitude = model.Latitude;
        Longitude = model.Longitude;
        Conditions = model.Conditions?.Select(e => e.WaterConditionId).ToArray()!;
        ConditionOther = model.ConditionOther;
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
    [Display(Name = "Name")]
    public string? Name { get; set; }
    [Required]
    [RegularExpression(@"^\S*$", ErrorMessage = "ID cannot contain spaces.")]
    [Display(Name = "ID")]
    public string? Code { get; set; }
    [RequiredIfNotEmpty("TypeOther", ErrorMessage = "The Type field is required.")]
    [Display(Name ="Type")]
    public string? WaterSupplyTypeId { get; set; }
    public string? TypeOther { get; set; }
    public double? Ph { get; set; }
    public double? TDSPPM { get; set; }
    public double? Depth { get; set; }
    public double? WaterTable { get; set; }
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
    [RequiredIfNotEmpty("ConditionOther", ErrorMessage = "The Condition field is required.")]
    [Display(Name ="Condition")]
    public string[] Conditions { get; set; } = Array.Empty<string>();
    public string? ConditionOther { get; set; }
}

