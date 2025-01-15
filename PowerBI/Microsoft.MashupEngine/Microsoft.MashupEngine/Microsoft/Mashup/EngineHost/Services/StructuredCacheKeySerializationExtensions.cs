using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B40 RID: 6976
	internal static class StructuredCacheKeySerializationExtensions
	{
		// Token: 0x0600AE9C RID: 44700 RVA: 0x0023C231 File Offset: 0x0023A431
		public static void WriteStructuredCacheKey(this BinaryWriter writer, StructuredCacheKey key)
		{
			writer.WriteNullable(key.Credentials, new Action<BinaryWriter, ResourceCredentialCollection>(IResourceCredentialSerializationExtensions.WriteResourceCredentialCollection));
			writer.WriteArray(key.Parts, new Action<BinaryWriter, string>(BinaryReaderWriterExtensions.WriteString));
		}

		// Token: 0x0600AE9D RID: 44701 RVA: 0x0023C264 File Offset: 0x0023A464
		public static StructuredCacheKey ReadStructuredCacheKey(this BinaryReader reader)
		{
			ResourceCredentialCollection resourceCredentialCollection = reader.ReadNullable(new Func<BinaryReader, ResourceCredentialCollection>(IResourceCredentialSerializationExtensions.ReadResourceCredentialCollection));
			string[] array = reader.ReadArray(new Func<BinaryReader, string>(BinaryReaderWriterExtensions.ReadString));
			return new StructuredCacheKey(resourceCredentialCollection, array);
		}
	}
}
