using System;
using System.IO;
using System.Text;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C44 RID: 7236
	internal static class BinarySerializer
	{
		// Token: 0x0600B48B RID: 46219 RVA: 0x00249BD5 File Offset: 0x00247DD5
		static BinarySerializer()
		{
			BinarySerializer.fallbackEncoding.EncoderFallback = new EncoderReplacementFallback("\ufffd");
		}

		// Token: 0x0600B48C RID: 46220 RVA: 0x00249C00 File Offset: 0x00247E00
		public static byte[] Serialize(Action<BinaryWriter> serializer)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream, BinarySerializer.fallbackEncoding))
				{
					serializer(binaryWriter);
					binaryWriter.Flush();
					array = memoryStream.ToArray();
				}
			}
			return array;
		}

		// Token: 0x0600B48D RID: 46221 RVA: 0x00249C68 File Offset: 0x00247E68
		public static T Deserialize<T>(byte[] bytes, Func<BinaryReader, T> deserializer)
		{
			T t;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					t = deserializer(binaryReader);
				}
			}
			return t;
		}

		// Token: 0x04005BC9 RID: 23497
		private static readonly Encoding fallbackEncoding = (Encoding)Encoding.UTF8.Clone();
	}
}
