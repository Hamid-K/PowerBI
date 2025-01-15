using System;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.Edm
{
	// Token: 0x020000E1 RID: 225
	internal class ModelCompressor
	{
		// Token: 0x06001104 RID: 4356 RVA: 0x00027D20 File Offset: 0x00025F20
		public virtual byte[] Compress(XDocument model)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
				{
					model.Save(gzipStream);
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00027D80 File Offset: 0x00025F80
		public virtual XDocument Decompress(byte[] bytes)
		{
			XDocument xdocument;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
				{
					xdocument = XDocument.Load(gzipStream);
				}
			}
			return xdocument;
		}
	}
}
