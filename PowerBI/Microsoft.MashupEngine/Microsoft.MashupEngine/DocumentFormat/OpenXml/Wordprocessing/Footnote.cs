using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FC5 RID: 12229
	[GeneratedCode("DomGen", "2.0")]
	internal class Footnote : FootnoteEndnoteType
	{
		// Token: 0x17009401 RID: 37889
		// (get) Token: 0x0601A875 RID: 108661 RVA: 0x0035D8BC File Offset: 0x0035BABC
		public override string LocalName
		{
			get
			{
				return "footnote";
			}
		}

		// Token: 0x17009402 RID: 37890
		// (get) Token: 0x0601A876 RID: 108662 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009403 RID: 37891
		// (get) Token: 0x0601A877 RID: 108663 RVA: 0x00363BF5 File Offset: 0x00361DF5
		internal override int ElementTypeId
		{
			get
			{
				return 11937;
			}
		}

		// Token: 0x0601A878 RID: 108664 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A879 RID: 108665 RVA: 0x00363BFC File Offset: 0x00361DFC
		public Footnote()
		{
		}

		// Token: 0x0601A87A RID: 108666 RVA: 0x00363C04 File Offset: 0x00361E04
		public Footnote(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A87B RID: 108667 RVA: 0x00363C0D File Offset: 0x00361E0D
		public Footnote(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A87C RID: 108668 RVA: 0x00363C16 File Offset: 0x00361E16
		public Footnote(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A87D RID: 108669 RVA: 0x00363C1F File Offset: 0x00361E1F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Footnote>(deep);
		}

		// Token: 0x0400AD59 RID: 44377
		private const string tagName = "footnote";

		// Token: 0x0400AD5A RID: 44378
		private const byte tagNsId = 23;

		// Token: 0x0400AD5B RID: 44379
		internal const int ElementTypeIdConst = 11937;
	}
}
