## Contents

### [StudioLE.Conversion](StudioLE.Conversion/src)

A library providing standardized methods to convert between different types.

### [StudioLE.Core](StudioLE.Core/src)

This package is now OBSOLETE. The previous logic has been moved to separate packages.

### [StudioLE.Extensions.Logging](StudioLE.Extensions.Logging/src)

A library of methods to help with System.Console and Microsoft.Extensions.Logging. Including handling of ANSI colors and escape codes.

### [StudioLE.Extensions.Logging.Cache](StudioLE.Extensions.Logging.Cache/src)

An ILogger implementation that stores logs in a collection to be retrieved later. Its primary use case is to validate logging in tests.

### [StudioLE.Extensions.Logging.Console](StudioLE.Extensions.Logging.Console/src)

An ILogger implementation that formats logs in color but without the multiline scope of the default ConsoleLogger.

### [StudioLE.Extensions.System](StudioLE.Extensions.System/src)

A library of helper methods and extensions for primitives and the `System` namespace.

### [StudioLE.Patterns.Abstractions](StudioLE.Patterns.Abstractions/src)

Abstract interfaces to declare a conventional design pattern is implemented.

### [StudioLE.Results](StudioLE.Results/src)

A basic implementation of a railway oriented programming pattern with Success and Failure paths for a return value. As intriguing as this approach is it goes against the conventions of .NET. A better approach is to return null on failure and pass any warnings or errors via an ILogger.

### [StudioLE.Serialization](StudioLE.Serialization/src)

A library providing a standardized approach to parsing and serialization via ISerializer, IDeserializer, and IParser abstractions. Also includes logic to deeply interrogate the composition and properties of any object as a composite ObjectTree.

### [StudioLE.Serialization.Abstractions](StudioLE.Serialization.Abstractions/src)

Abstractions of a standardized approach to parsing and serialization via ISerializer, IDeserializer, and IParser abstractions.

### [StudioLE.Serialization.Yaml](StudioLE.Serialization.Yaml/src)

A concrete implementation of `StudioLE.Serialization` for YAML serialization using `YamlDotNet`.

### [StudioLE.Storage](StudioLE.Storage/src)

A library providing a standardized approach for writing files to storage. Includes concrete implementations for storing files to the local file system.

### [StudioLE.Storage.Abstractions](StudioLE.Storage.Abstractions/src)

Abstractions of a standardized approach for writing files to storage.

### [StudioLE.Storage.Blob](StudioLE.Storage.Blob/src)

A concrete implementation of `StudioLE.Storage` for writing to Azure Blob Storage.

## License

This repository and its libraries are provided open source with the [AGPL-3.0](https://www.gnu.org/licenses/agpl-3.0.en.html) license that requires you must disclose your source code when you distribute, publish, or provide access to modified or derivative software.

Developers who wish to keep modified or derivative software proprietary or closed source can [get in touch for a commercial license agreements](https://studiole.uk/contact/)

> Copyright © Laurence Elsdon 2023-2026
>
> This program is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
>
> This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more details.
>
> You should have received a copy of the GNU Affero General Public License along with this program. If not, see <https://www.gnu.org/licenses/>.

→ [GNU Affero General Public License](LICENSE.md)
