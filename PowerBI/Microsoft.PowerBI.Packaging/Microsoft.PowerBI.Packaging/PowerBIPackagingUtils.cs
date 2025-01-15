using System;
using System.IO;
using System.IO.Packaging;
using System.Text;
using Microsoft.PowerBI.Packaging.Extensions;
using Microsoft.PowerBI.Packaging.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000025 RID: 37
	public static class PowerBIPackagingUtils
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00005018 File Offset: 0x00003218
		public static byte[] GetContentAsBytes(IStreamablePowerBIPackagePartContent streamablePowerBIPackagePartContent, bool isOptional)
		{
			if (streamablePowerBIPackagePartContent != null)
			{
				byte[] array;
				using (Stream stream = streamablePowerBIPackagePartContent.GetStream())
				{
					if (stream == null)
					{
						if (!isOptional)
						{
							throw new NullReferenceException("stream to the required part of the package is null.");
						}
						array = null;
					}
					else
					{
						array = stream.ReadAllBytes();
					}
				}
				return array;
			}
			if (!isOptional)
			{
				throw new IOException("A required package part is missing from the package.");
			}
			return null;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005078 File Offset: 0x00003278
		public static string GetContentAsString(IStreamablePowerBIPackagePartContent streamablePowerBIPackagePartContent, bool isOptional, Encoding encoding = null)
		{
			byte[] contentAsBytes = PowerBIPackagingUtils.GetContentAsBytes(streamablePowerBIPackagePartContent, isOptional);
			if (contentAsBytes != null)
			{
				return (encoding ?? Encoding.Unicode).GetString(contentAsBytes);
			}
			return null;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000050A4 File Offset: 0x000032A4
		internal static void SetContent(Package package, Uri storagePath, Stream contentStream, string contentType, bool isOptional, CompressionOption compressionOption)
		{
			if (contentStream != null)
			{
				using (Stream writeStream = PowerBIPackagingUtils.GetWriteStream(package, storagePath, contentType, compressionOption))
				{
					contentStream.CopyTo(writeStream, 4096);
				}
				return;
			}
			if (isOptional)
			{
				if (package.PartExists(storagePath))
				{
					package.DeletePart(storagePath);
				}
				return;
			}
			throw new IOException("Non-optional storage part " + ((storagePath != null) ? storagePath.ToString() : null) + " is missing.");
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005120 File Offset: 0x00003320
		public static T DeserializeJsonPackagePartContent<T>(IStreamablePowerBIPackagePartContent streamablePowerBIPackagePartContent, int expectedVersion, bool isOptional)
		{
			return PowerBIPackagingUtils.DeserializeSettingsJson<T>(PowerBIPackagingUtils.ReadJsonPackagePartContent(streamablePowerBIPackagePartContent, isOptional), expectedVersion);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000512F File Offset: 0x0000332F
		public static T DeserializeJsonPackagePartContent<T>(IStreamablePowerBIPackagePartContent streamablePowerBIPackagePartContent, Version expectedVersion, bool isOptional)
		{
			JObject jobject = PowerBIPackagingUtils.ReadJsonPackagePartContent(streamablePowerBIPackagePartContent, isOptional);
			PowerBIPackagingUtils.GetVersion(jobject, expectedVersion);
			return PowerBIPackagingUtils.DeserializeSettingsJson<T>(jobject);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005145 File Offset: 0x00003345
		public static JObject ReadJsonPackagePartContent(IStreamablePowerBIPackagePartContent streamablePowerBIPackagePartContent, bool isOptional)
		{
			return JObject.Parse(PowerBIPackagingUtils.GetContentAsString(streamablePowerBIPackagePartContent, isOptional, null));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005154 File Offset: 0x00003354
		public static T DeserializeSettingsJson<T>(JObject jsonContent, int expectedVersion)
		{
			PowerBIPackagingUtils.GetVersion(jsonContent, expectedVersion);
			return PowerBIPackagingUtils.DeserializeSettingsJson<T>(jsonContent);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005164 File Offset: 0x00003364
		private static T DeserializeSettingsJson<T>(JObject jsonContent)
		{
			T t;
			try
			{
				using (JsonReader jsonReader = jsonContent.CreateReader())
				{
					t = PowerBIPackagingUtils.jsonSerializer.Deserialize<T>(jsonReader);
				}
			}
			catch (JsonReaderException ex)
			{
				throw new FileFormatException("Invalid json content", ex);
			}
			return t;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000051BC File Offset: 0x000033BC
		internal static int GetVersion(JObject jsonContent, int targetVersion)
		{
			return PowerBIPackagingUtils.GetVersion<int>(jsonContent, "Version", (int jsonVersion) => jsonVersion > targetVersion);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000051F0 File Offset: 0x000033F0
		private static Version GetVersion(JObject jsonContent, Version targetVersion)
		{
			Version parsedJsonVersion = null;
			PowerBIPackagingUtils.GetVersion<string>(jsonContent, "version", (string jsonVersion) => !Version.TryParse(jsonVersion, out parsedJsonVersion) || parsedJsonVersion > targetVersion);
			return parsedJsonVersion;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005230 File Offset: 0x00003430
		private static T GetVersion<T>(JObject jsonContent, string versionKey, Func<T, bool> versionComparer)
		{
			JToken jtoken;
			if (!jsonContent.TryGetValue(versionKey, out jtoken))
			{
				throw new NewerFileVersionException(null);
			}
			T t = jtoken.Value<T>();
			if (versionComparer(t))
			{
				throw new NewerFileVersionException(t.ToString());
			}
			return jtoken.Value<T>();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005278 File Offset: 0x00003478
		private static Stream GetWriteStream(Package package, Uri storagePath, string contentType, CompressionOption compressionOption)
		{
			return PowerBIPackagingUtils.EnsurePart(package, storagePath, contentType, compressionOption).GetStream(FileMode.Create, FileAccess.ReadWrite);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000528C File Offset: 0x0000348C
		private static PackagePart EnsurePart(Package package, Uri storagePath, string contentType, CompressionOption compressionOption)
		{
			if (package == null)
			{
				throw new InvalidOperationException("Cannot write to an empty package");
			}
			if (package.PartExists(storagePath))
			{
				PackagePart part = package.GetPart(storagePath);
				if (part.ContentType == contentType)
				{
					return part;
				}
				package.DeletePart(storagePath);
			}
			return package.CreatePart(storagePath, contentType, compressionOption);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000052D8 File Offset: 0x000034D8
		public static IStreamablePowerBIPackagePartContent AsStreamableContent(byte[] bytes)
		{
			return new StreamablePowerBIPackagePartContent(bytes, "");
		}

		// Token: 0x04000093 RID: 147
		private static readonly JsonSerializer jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
		{
			DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
		});
	}
}
