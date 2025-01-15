using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DAC RID: 11692
	[GeneratedCode("DomGen", "2.0")]
	internal class BodyDiv : OnOffType
	{
		// Token: 0x170087A0 RID: 34720
		// (get) Token: 0x06018E00 RID: 101888 RVA: 0x00344F81 File Offset: 0x00343181
		public override string LocalName
		{
			get
			{
				return "bodyDiv";
			}
		}

		// Token: 0x170087A1 RID: 34721
		// (get) Token: 0x06018E01 RID: 101889 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087A2 RID: 34722
		// (get) Token: 0x06018E02 RID: 101890 RVA: 0x00344F88 File Offset: 0x00343188
		internal override int ElementTypeId
		{
			get
			{
				return 11928;
			}
		}

		// Token: 0x06018E03 RID: 101891 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E05 RID: 101893 RVA: 0x00344F8F File Offset: 0x0034318F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BodyDiv>(deep);
		}

		// Token: 0x0400A56F RID: 42351
		private const string tagName = "bodyDiv";

		// Token: 0x0400A570 RID: 42352
		private const byte tagNsId = 23;

		// Token: 0x0400A571 RID: 42353
		internal const int ElementTypeIdConst = 11928;
	}
}
