using System.Text.Json.Serialization;

namespace Water.Management.Shared.ViewModels;

public class WaterSourceVm : BaseVm
{
    [JsonPropertyName("waterSupplyId")]
    public Guid WaterSupplyId { get; set; }
    [JsonPropertyName("code")]
    public string? Code { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("ph")]
    public double? Ph { get; set; }
    [JsonPropertyName("tdsppm")]
    public double? TDSPPM { get; set; }
    [JsonPropertyName("depth")]
    public double? Depth { get; set; }
    [JsonPropertyName("waterTable")]
    public double? WaterTable { get; set; }
    [JsonPropertyName("elevation")]
    public double? Elevation { get; set; }
    [JsonPropertyName("waterSupplyTypeId")]
    public string? WaterSupplyTypeId { get; set; }
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    [JsonPropertyName("typeOther")]
    public string? TypeOther { get; set; }
    [JsonPropertyName("startSurveyDt")]
    public DateOnly? StartSurveyDt { get; set; }
    [JsonPropertyName("endSurveyDt")]
    public DateOnly? EndSurveyDt { get; set; }
    [JsonPropertyName("conditionOther")]
    public string? ConditionOther { get; set; }
    [JsonPropertyName("lastStatus")]
    public string? LastStatus { get; set; }
    [JsonPropertyName("socialMapping")]
    public string? SocialMapping { get; set; }
    [JsonPropertyName("rainfall")]
    public string? Rainfall { get; set; }
    [JsonPropertyName("vegetation")]
    public string? Vegetation { get; set; }
    [JsonPropertyName("demography")]
    public string? Demography { get; set; }
    [JsonPropertyName("generalGeology")]
    public string? GeneralGeology { get; set; }
    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }
    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }
    [JsonPropertyName("provinceId")]
    public string? ProvinceId { get; set; }
    [JsonPropertyName("province")]
    public string? Province { get; set; }
    [JsonPropertyName("cityId")]
    public string? CityId { get; set; }
    [JsonPropertyName("city")]
    public string? City { get; set; }
    [JsonPropertyName("districtId")]
    public string? DistrictId { get; set; }
    [JsonPropertyName("district")]
    public string? District { get; set; }
    [JsonPropertyName("subDistrictId")]
    public string? SubDistrictId { get; set; }
    [JsonPropertyName("SubDistrict")]
    public string? SubDistrict { get; set; }
    [JsonPropertyName("detailLocation")]
    public string? DetailLocation { get; set; }
    [JsonPropertyName("conditions")]
    public List<WaterConditionVm>? Conditions { get; set; }
    [JsonPropertyName("Images")]
    public List<WaterImagesVm>? Images { get; set; }
}

