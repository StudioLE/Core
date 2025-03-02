using System.ComponentModel.DataAnnotations;

namespace StudioLE.Serialization.Tests.Resources;

public record struct ExampleRecordStruct()
{
    [Required]
    public string RecordStructStringValue { get; set; } = string.Empty;

    [Required]
    public string RecordStructArgValue { get; set; } = string.Empty;

    [ValidateComplexType]
    public ExampleNestedRecordStruct NestedRecordStruct { get; set; } = new();
}
