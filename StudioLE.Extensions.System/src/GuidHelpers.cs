// ReSharper disable once CommentTypo
//
// The majority of the logic in GuidHelpers is from Faithlife.Utility
// https://github.com/Faithlife/FaithlifeUtility
//
// This specific GuidHelpers class is therefore under their license:
// https://github.com/Faithlife/FaithlifeUtility/blob/3400ce614c2dbd8ad29e16134ac1282545f89c89/LICENSE


using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace StudioLE.Extensions.System;

/// <summary>
/// Helper methods for working with <see cref="Guid"/>.
/// </summary>
/// <see href="https://github.com/Faithlife/FaithlifeUtility/blob/3400ce614c2dbd8ad29e16134ac1282545f89c89/src/Faithlife.Utility/GuidUtility.cs">Source</see>
/// <see href="https://stackoverflow.com/a/5657517/247218"/>
public static class GuidHelpers
{
    /// <summary>
    /// Tries to parse the specified string as a <see cref="Guid"/>.  A return value indicates whether the operation succeeded.
    /// </summary>
    /// <param name="value">The GUID string to attempt to parse.</param>
    /// <param name="guid">When this method returns, contains the <see cref="Guid"/> equivalent to the GUID
    /// contained in <paramref name="value"/>, if the conversion succeeded, or Guid.Empty if the conversion failed.</param>
    /// <returns><c>true</c> if a GUID was successfully parsed; <c>false</c> otherwise.</returns>
    public static bool TryParse(string? value, out Guid guid)
    {
        return Guid.TryParse(value, out guid);
    }

    /// <summary>
    /// Converts a GUID to a lowercase string with no dashes.
    /// </summary>
    /// <param name="guid">The GUID.</param>
    /// <returns>The GUID as a lowercase string with no dashes.</returns>
    public static string ToLowerNoDashString(this Guid guid)
    {
        return guid.ToString("N");
    }

    /// <summary>
    /// Converts a lowercase, no dashes string to a GUID.
    /// </summary>
    /// <param name="value">The string.</param>
    /// <returns>The GUID.</returns>
    /// <exception cref="FormatException">The argument is not a valid GUID short string.</exception>
    public static Guid FromLowerNoDashString(string value)
    {
        return TryFromLowerNoDashString(value) ?? throw new FormatException("The string '{0}' is not a no-dash lowercase GUID.".FormatInvariant(value));
    }

    /// <summary>
    /// Attempts to convert a lowercase, no dashes string to a GUID.
    /// </summary>
    /// <param name="value">The string.</param>
    /// <returns>The GUID, if the string could be converted; otherwise, null.</returns>
    public static Guid? TryFromLowerNoDashString(string value)
    {
        return !TryParse(value, out Guid guid) || value != guid.ToLowerNoDashString() ? default(Guid?) : guid;
    }

    /// <summary>
    /// Creates a name-based UUID using the algorithm from RFC 4122 §4.3.
    /// </summary>
    /// <param name="namespaceId">The ID of the namespace.</param>
    /// <param name="name">The name (within that namespace).</param>
    /// <returns>A UUID derived from the namespace and name.</returns>
    public static Guid Create(Guid namespaceId, string name)
    {
        return Create(namespaceId, name, 5);
    }

    /// <summary>
    /// Creates a name-based UUID using the algorithm from RFC 4122 §4.3.
    /// </summary>
    /// <param name="namespaceId">The ID of the namespace.</param>
    /// <param name="name">The name (within that namespace).</param>
    /// <param name="version">The version number of the UUID to create; this value must be either
    /// 3 (for MD5 hashing) or 5 (for SHA-1 hashing).</param>
    /// <returns>A UUID derived from the namespace and name.</returns>
    public static Guid Create(Guid namespaceId, string name, int version)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        // convert the name to a sequence of octets (as defined by the standard or conventions of its namespace) (step 3)
        // ASSUME: UTF-8 encoding is always appropriate
        return Create(namespaceId, Encoding.UTF8.GetBytes(name), version);
    }

    /// <summary>
    /// Creates a name-based UUID using the algorithm from RFC 4122 §4.3.
    /// </summary>
    /// <param name="namespaceId">The ID of the namespace.</param>
    /// <param name="nameBytes">The name (within that namespace).</param>
    /// <returns>A UUID derived from the namespace and name.</returns>
    public static Guid Create(Guid namespaceId, byte[] nameBytes)
    {
        return Create(namespaceId, nameBytes, 5);
    }

    /// <summary>
    /// Creates a name-based UUID using the algorithm from RFC 4122 §4.3.
    /// </summary>
    /// <param name="namespaceId">The ID of the namespace.</param>
    /// <param name="nameBytes">The name (within that namespace).</param>
    /// <param name="version">The version number of the UUID to create; this value must be either
    /// 3 (for MD5 hashing) or 5 (for SHA-1 hashing).</param>
    /// <returns>A UUID derived from the namespace and name.</returns>
    [SuppressMessage("Security", "CA5350:Do Not Use Weak Cryptographic Algorithms", Justification = "Per spec.")]
    [SuppressMessage("Security", "CA5351:Do Not Use Broken Cryptographic Algorithms", Justification = "Per spec.")]
    public static Guid Create(Guid namespaceId, byte[] nameBytes, int version)
    {
        if (version != 3 && version != 5)
            throw new ArgumentOutOfRangeException(nameof(version), "version must be either 3 or 5.");

        // convert the namespace UUID to network order (step 3)
        byte[] namespaceBytes = namespaceId.ToByteArray();
        SwapByteOrder(namespaceBytes);

        // compute the hash of the namespace ID concatenated with the name (step 4)
        byte[] data = namespaceBytes.Concat(nameBytes).ToArray();
        byte[] hash;
        using (HashAlgorithm algorithm = version == 3 ? MD5.Create() : SHA1.Create())
            hash = algorithm.ComputeHash(data);

        // most bytes from the hash are copied straight to the bytes of the new GUID (steps 5-7, 9, 11-12)
        byte[] newGuid = new byte[16];
        Array.Copy(hash, 0, newGuid, 0, 16);

        // set the four most significant bits (bits 12 through 15) of the time_hi_and_version field to the appropriate 4-bit version number from Section 4.1.3 (step 8)
        newGuid[6] = (byte)((newGuid[6] & 0x0F) | (version << 4));

        // set the two most significant bits (bits 6 and 7) of the clock_seq_hi_and_reserved to zero and one, respectively (step 10)
        newGuid[8] = (byte)((newGuid[8] & 0x3F) | 0x80);

        // convert the resulting UUID to local byte order (step 13)
        SwapByteOrder(newGuid);
        return new(newGuid);
    }

    /// <summary>
    /// The namespace for fully-qualified domain names (from RFC 4122, Appendix C).
    /// </summary>
    public static readonly Guid DnsNamespace = new("6ba7b810-9dad-11d1-80b4-00c04fd430c8");

    /// <summary>
    /// The namespace for URLs (from RFC 4122, Appendix C).
    /// </summary>
    public static readonly Guid UrlNamespace = new("6ba7b811-9dad-11d1-80b4-00c04fd430c8");

    /// <summary>
    /// The namespace for ISO OIDs (from RFC 4122, Appendix C).
    /// </summary>
    public static readonly Guid IsoOidNamespace = new("6ba7b812-9dad-11d1-80b4-00c04fd430c8");

    // Converts a GUID (expressed as a byte array) to/from network order (MSB-first).
    internal static void SwapByteOrder(byte[] guid)
    {
        SwapBytes(guid, 0, 3);
        SwapBytes(guid, 1, 2);
        SwapBytes(guid, 4, 5);
        SwapBytes(guid, 6, 7);
    }

    private static void SwapBytes(byte[] guid, int left, int right)
    {
        (guid[right], guid[left]) = (guid[left], guid[right]);
    }

    /// <summary>
    /// Formats the string using the invariant culture.
    /// </summary>
    /// <param name="format">The format string.</param>
    /// <param name="args">The format arguments.</param>
    /// <returns>The formatted string.</returns>
    private static string FormatInvariant(this string format, params object?[] args)
    {
        return string.Format(CultureInfo.InvariantCulture, format, args);
    }
}
