using System;
using System.IO;
using Microsoft.Mashup.Client.Packaging.SerializationObjectModel;

namespace Microsoft.Mashup.Client.Packaging.Serializers
{
	// Token: 0x02000016 RID: 22
	public static class PackageMetadataSerializer
	{
		// Token: 0x06000073 RID: 115 RVA: 0x000030C4 File Offset: 0x000012C4
		public static bool TryDeserialize(byte[] bytes, out SerializedPackageMetadata serializedPackageMetadata, out byte[] contentStorageBytes)
		{
			MemoryStream memoryStream = new MemoryStream(bytes);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			int num = binaryReader.ReadInt32();
			if (num != 0)
			{
				throw new FormatException();
			}
			int num2 = binaryReader.ReadInt32();
			byte[] array = binaryReader.ReadBytes(num2);
			int num3 = binaryReader.ReadInt32();
			contentStorageBytes = binaryReader.ReadBytes(num3);
			return Xml<SerializedPackageMetadata>.TryDeserializeBytes(array, out serializedPackageMetadata);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000311C File Offset: 0x0000131C
		public static byte[] Serialize(SerializedPackageMetadata serializedPackageMetadata, byte[] contentStorageBytes)
		{
			byte[] array = Xml<SerializedPackageMetadata>.SerializeBytes(serializedPackageMetadata);
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(0);
			binaryWriter.Write(array.Length);
			binaryWriter.Write(array);
			binaryWriter.Write(contentStorageBytes.Length);
			binaryWriter.Write(contentStorageBytes);
			binaryWriter.Flush();
			return memoryStream.ToArray();
		}

		// Token: 0x0400005E RID: 94
		private const int currentVersion = 0;
	}
}
