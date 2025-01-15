using System;
using System.IO;
using System.IO.Compression;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000021 RID: 33
	internal sealed class MdxBasXmlCompressedDataReader : MdxBasXmlDataReader
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00007849 File Offset: 0x00005A49
		public MdxBasXmlCompressedDataReader(SapBwCommand command, MdxCommand mdxCommand, MdxColumnProvider columnProvider)
			: base(command, mdxCommand, columnProvider)
		{
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00007854 File Offset: 0x00005A54
		protected override string GetDataBapi
		{
			get
			{
				return "RSR_MDX_BXML_GET_GZIP_DATA";
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000785B File Offset: 0x00005A5B
		protected override Stream GetStream(byte[] xmlData)
		{
			return new BufferedStream(new DeflateStream(new MemoryStream(xmlData, false), CompressionMode.Decompress));
		}
	}
}
