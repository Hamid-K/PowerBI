using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001DF RID: 479
	internal class BinaryFormatCacheObjectSerializer : IDataCacheObjectSerializer
	{
		// Token: 0x06000F94 RID: 3988 RVA: 0x000352E8 File Offset: 0x000334E8
		public void Serialize(Stream stream, object value)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(stream, value);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00035320 File Offset: 0x00033520
		public object Deserialize(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			return binaryFormatter.Deserialize(stream);
		}
	}
}
