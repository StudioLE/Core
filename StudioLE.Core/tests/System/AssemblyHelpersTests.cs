using System.Reflection;
using NUnit.Framework;
using StudioLE.Core.System;

namespace StudioLE.Core.Tests.System;

internal sealed class AssemblyHelpersTests
{
    [Test]
    public void AssemblyHelpers_GetCallingAssemblies()
    {
        // Arrange
        // Act
        Assembly[] assemblies = AssemblyHelpers.GetCallingAssemblies().ToArray();
        string[] names = assemblies
            .Select(assembly => assembly.GetName().Name ?? string.Empty)
            .ToArray();

        // Preview
        Console.WriteLine(names.Join());

        // Assert
        Assert.That(names.ElementAt(0), Is.EqualTo("StudioLE.Core.Tests"));
    }
}
