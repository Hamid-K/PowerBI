using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F2A RID: 12074
	[GeneratedCode("DomGen", "2.0")]
	internal class StartNumberingValue : NonNegativeDecimalNumberType
	{
		// Token: 0x17008F40 RID: 36672
		// (get) Token: 0x06019E46 RID: 106054 RVA: 0x00313F27 File Offset: 0x00312127
		public override string LocalName
		{
			get
			{
				return "start";
			}
		}

		// Token: 0x17008F41 RID: 36673
		// (get) Token: 0x06019E47 RID: 106055 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F42 RID: 36674
		// (get) Token: 0x06019E48 RID: 106056 RVA: 0x00359013 File Offset: 0x00357213
		internal override int ElementTypeId
		{
			get
			{
				return 11863;
			}
		}

		// Token: 0x06019E49 RID: 106057 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019E4B RID: 106059 RVA: 0x0035901A File Offset: 0x0035721A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StartNumberingValue>(deep);
		}

		// Token: 0x0400AAC0 RID: 43712
		private const string tagName = "start";

		// Token: 0x0400AAC1 RID: 43713
		private const byte tagNsId = 23;

		// Token: 0x0400AAC2 RID: 43714
		internal const int ElementTypeIdConst = 11863;
	}
}
