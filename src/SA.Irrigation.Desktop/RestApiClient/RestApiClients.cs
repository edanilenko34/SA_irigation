// <auto-generated>
//     This code was generated by Refitter.
// </auto-generated>


using Refit;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SA.Irrigation.Desktop.RestApiClient
{
    [System.CodeDom.Compiler.GeneratedCode("Refitter", "1.0.0.0")]
    public partial interface ISAIrrigationAPI
    {
        /// <returns>Success</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Post("/api/DeviceModel")]
        Task<DeviceModelDto> CreateModelAsync([Body] CreateDeviceModelRequest body);

        /// <returns>Success</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Get("/api/DeviceModel")]
        Task<ICollection<DeviceModelDto>> GetAllModelsAsync();

        /// <returns>A <see cref="Task"/> that completes when the request is finished.</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>404</term>
        /// <description>Not Found</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Delete("/api/DeviceModel/{id}")]
        Task DeleteModelAsync(System.Guid id);

        /// <returns>Success</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>404</term>
        /// <description>Not Found</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Put("/api/DeviceModel/{id}")]
        Task<DeviceModelDto> ModelUpdateAsync(System.Guid id, [Body] CreateDeviceModelRequest body);

        /// <returns>Success</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// <item>
        /// <term>404</term>
        /// <description>Not Found</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Get("/api/DeviceModel/{id}")]
        Task<DeviceModelDto> DeviceModelGET(System.Guid id);

        /// <returns>Success</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Get("/api/Devices")]
        Task<ICollection<DeviceDto>> GetAllDevicesAsync();

        /// <returns>Success</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Post("/api/Devices")]
        Task<DeviceDto> CreateDeviceAsync([Body] CreateOrUpdateDeviceRequest body);

        /// <returns>Success</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// <item>
        /// <term>404</term>
        /// <description>Not Found</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Get("/api/Devices/{id}")]
        Task<DeviceDto> DevicesGET(System.Guid id);

        /// <returns>Success</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// <item>
        /// <term>404</term>
        /// <description>Not Found</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Put("/api/Devices/{id}")]
        Task<DeviceDto> UpdateDeviceAsync(System.Guid id, [Body] CreateOrUpdateDeviceRequest body);

        /// <returns>A <see cref="Task"/> that completes when the request is finished.</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// <item>
        /// <term>404</term>
        /// <description>Not Found</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Delete("/api/Devices/{id}")]
        Task DeleteDeviceAsync(System.Guid id);

        /// <returns>Success</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Get("/api/Devices/bytype/{deviceType}")]
        Task<ICollection<DeviceDto>> GetDevicesByTypeAsync(DeviceType deviceType);

        /// <returns>Success</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// <item>
        /// <term>404</term>
        /// <description>Not Found</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Post("/api/Schedules")]
        Task<ScheduleDto> CreateScheduleAsync([Query] System.Guid? deviceId, [Body] CreateOrUpdateScheduleRequest body);

        /// <returns>Success</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// <item>
        /// <term>404</term>
        /// <description>Not Found</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Put("/api/Schedules/{id}")]
        Task<ScheduleDto> SchedulesPUT(System.Guid id, [Body] CreateOrUpdateScheduleRequest body);

        /// <returns>A <see cref="Task"/> that completes when the request is finished.</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Delete("/api/Schedules/{id}")]
        Task DeleteScheduleAsync(System.Guid id);

        /// <returns>Success</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// <item>
        /// <term>404</term>
        /// <description>Not Found</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Get("/api/Schedules/{id}")]
        Task<ScheduleDto> SchedulesGET(System.Guid id);

        /// <returns>Success</returns>
        /// <exception cref="ApiException">
        /// Thrown when the request returns a non-success status code:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>400</term>
        /// <description>Bad Request</description>
        /// </item>
        /// <item>
        /// <term>500</term>
        /// <description>Server Error</description>
        /// </item>
        /// <item>
        /// <term>404</term>
        /// <description>Not Found</description>
        /// </item>
        /// </list>
        /// </exception>
        [Headers("Accept: text/plain, application/json, text/json")]
        [Get("/api/Schedules/parent/{parentid}")]
        Task<ICollection<ScheduleDto>> GetScheduleByParent(System.Guid parentId);


    }
}


//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 612 // Disable "CS0612 '...' is obsolete"
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."
#pragma warning disable 8073 // Disable "CS8073 The result of the expression is always 'false' since a value of type 'T' is never equal to 'null' of type 'T?'"
#pragma warning disable 3016 // Disable "CS3016 Arrays as attribute arguments is not CLS-compliant"
#pragma warning disable 8603 // Disable "CS8603 Possible null reference return"
#pragma warning disable 8604 // Disable "CS8604 Possible null reference argument for parameter"
#pragma warning disable 8625 // Disable "CS8625 Cannot convert null literal to non-nullable reference type"
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).

namespace SA.Irrigation.Desktop.RestApiClient
{
    using System = global::System;

    

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class CreateDeviceModelRequest
    {

        [JsonPropertyName("name")]
        [System.ComponentModel.DataAnnotations.StringLength(128)]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [System.ComponentModel.DataAnnotations.StringLength(1024)]
        public string Description { get; set; }

        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DeviceType Type { get; set; }

        [JsonPropertyName("openCommand")]
        [System.ComponentModel.DataAnnotations.StringLength(64)]
        public string OpenCommand { get; set; }

        [JsonPropertyName("closeCommand")]
        [System.ComponentModel.DataAnnotations.StringLength(64)]
        public string CloseCommand { get; set; }

        [JsonPropertyName("getDataCommand")]
        [System.ComponentModel.DataAnnotations.StringLength(64)]
        public string GetDataCommand { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class CreateOrUpdateDeviceRequest
    {

        [JsonPropertyName("address")]
        public int Address { get; set; }

        [JsonPropertyName("name")]
        [System.ComponentModel.DataAnnotations.StringLength(128)]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [System.ComponentModel.DataAnnotations.StringLength(1024)]
        public string Description { get; set; }

        [JsonPropertyName("modelId")]
        public System.Guid ModelId { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class CreateOrUpdateScheduleRequest
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("startCron")]
        public string StartCron { get; set; }

        [JsonPropertyName("finishBy")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FinishByType FinishBy { get; set; }

        [JsonPropertyName("finishCron")]
        public string? FinishCron { get; set; }

        [JsonPropertyName("finishDeviceId")]
        public System.Guid? FinishDeviceId { get; set; }

        [JsonPropertyName("parentId")]
        public System.Guid ParentId { get; set; }

        [JsonPropertyName("finishValue")]
        public double? FinishValue { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class DeviceDto
    {

        [JsonPropertyName("id")]
        public System.Guid Id { get; set; }

        [JsonPropertyName("address")]
        public int Address { get; set; }

        [JsonPropertyName("name")]
        [System.ComponentModel.DataAnnotations.StringLength(128)]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [System.ComponentModel.DataAnnotations.StringLength(1024)]
        public string Description { get; set; }

        [JsonPropertyName("model")]
        public DeviceModelDto Model { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class DeviceModelDto
    {

        [JsonPropertyName("id")]
        public System.Guid Id { get; set; }

        [JsonPropertyName("name")]
        [System.ComponentModel.DataAnnotations.StringLength(128)]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [System.ComponentModel.DataAnnotations.StringLength(1024)]
        public string Description { get; set; }

        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DeviceType Type { get; set; }

        [JsonPropertyName("openCommand")]
        [System.ComponentModel.DataAnnotations.StringLength(64)]
        public string OpenCommand { get; set; }

        [JsonPropertyName("closeCommand")]
        [System.ComponentModel.DataAnnotations.StringLength(64)]
        public string CloseCommand { get; set; }

        [JsonPropertyName("getDataCommand")]
        [System.ComponentModel.DataAnnotations.StringLength(64)]
        public string GetDataCommand { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public enum DeviceType
    {

        [System.Runtime.Serialization.EnumMember(Value = @"Valve")]
        Valve = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"Sensor")]
        Sensor = 1,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public enum FinishByType
    {

        [System.Runtime.Serialization.EnumMember(Value = @"ByTime")]
        ByTime = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"ByValue")]
        ByValue = 1,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ProblemDetails
    {

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        [JsonPropertyName("instance")]
        public string Instance { get; set; }

        private IDictionary<string, object> _additionalProperties;

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties ?? (_additionalProperties = new Dictionary<string, object>()); }
            set { _additionalProperties = value; }
        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ScheduleDto
    {

        [JsonPropertyName("id")]
        public System.Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("startCron")]
        public string StartCron { get; set; }

        [JsonPropertyName("finishBy")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FinishByType FinishBy { get; set; }

        [JsonPropertyName("finishCron")]
        public string? FinishCron { get; set; }

        [JsonPropertyName("finishDeviceId")]
        public System.Guid? FinishDeviceId { get; set; }

        [JsonPropertyName("finishValue")]
        public double? FinishValue { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class TimeSpan
    {

        [JsonPropertyName("ticks")]
        public long Ticks { get; set; }

        [JsonPropertyName("days")]
        public int Days { get; set; }

        [JsonPropertyName("hours")]
        public int Hours { get; set; }

        [JsonPropertyName("milliseconds")]
        public int Milliseconds { get; set; }

        [JsonPropertyName("microseconds")]
        public int Microseconds { get; set; }

        [JsonPropertyName("nanoseconds")]
        public int Nanoseconds { get; set; }

        [JsonPropertyName("minutes")]
        public int Minutes { get; set; }

        [JsonPropertyName("seconds")]
        public int Seconds { get; set; }

        [JsonPropertyName("totalDays")]
        public double TotalDays { get; set; }

        [JsonPropertyName("totalHours")]
        public double TotalHours { get; set; }

        [JsonPropertyName("totalMilliseconds")]
        public double TotalMilliseconds { get; set; }

        [JsonPropertyName("totalMicroseconds")]
        public double TotalMicroseconds { get; set; }

        [JsonPropertyName("totalNanoseconds")]
        public double TotalNanoseconds { get; set; }

        [JsonPropertyName("totalMinutes")]
        public double TotalMinutes { get; set; }

        [JsonPropertyName("totalSeconds")]
        public double TotalSeconds { get; set; }

    }


}

#pragma warning restore  108
#pragma warning restore  114
#pragma warning restore  472
#pragma warning restore  612
#pragma warning restore 1573
#pragma warning restore 1591
#pragma warning restore 8073
#pragma warning restore 3016
#pragma warning restore 8603
#pragma warning restore 8604
#pragma warning restore 8625