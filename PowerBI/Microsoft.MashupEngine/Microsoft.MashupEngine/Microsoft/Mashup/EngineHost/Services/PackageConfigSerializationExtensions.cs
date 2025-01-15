using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001ABC RID: 6844
	internal static class PackageConfigSerializationExtensions
	{
		// Token: 0x0600AC29 RID: 44073 RVA: 0x00236AD4 File Offset: 0x00234CD4
		public static void WritePackageConfig(this BinaryWriter writer, PackageConfig packageConfig)
		{
			writer.WriteNullableString(packageConfig.Version);
			writer.WriteNullableString(packageConfig.MinVersion);
			writer.WriteNullableString(packageConfig.Culture);
		}

		// Token: 0x0600AC2A RID: 44074 RVA: 0x00236AFA File Offset: 0x00234CFA
		public static PackageConfig ReadPackageConfig(this BinaryReader reader)
		{
			return new PackageConfig
			{
				Version = reader.ReadNullableString(),
				MinVersion = reader.ReadNullableString(),
				Culture = reader.ReadNullableString()
			};
		}
	}
}
