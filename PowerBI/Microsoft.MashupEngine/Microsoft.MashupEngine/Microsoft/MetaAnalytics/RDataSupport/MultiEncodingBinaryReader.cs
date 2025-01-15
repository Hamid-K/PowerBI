using System;
using System.IO;

namespace Microsoft.MetaAnalytics.RDataSupport
{
	// Token: 0x02000169 RID: 361
	internal class MultiEncodingBinaryReader : BinaryReader
	{
		// Token: 0x060006E5 RID: 1765 RVA: 0x0000B4D6 File Offset: 0x000096D6
		public MultiEncodingBinaryReader(Stream input)
			: base(input)
		{
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0000B4DF File Offset: 0x000096DF
		public virtual string ReadString(MultiEncodingBinaryReader.EncodingOptions encoding)
		{
			return base.ReadString();
		}

		// Token: 0x0200016A RID: 362
		public enum EncodingOptions
		{
			// Token: 0x04000406 RID: 1030
			Latin1,
			// Token: 0x04000407 RID: 1031
			UTF8
		}
	}
}
