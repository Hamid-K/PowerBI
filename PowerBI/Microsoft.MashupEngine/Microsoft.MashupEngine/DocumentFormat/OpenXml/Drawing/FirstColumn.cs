using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200280B RID: 10251
	[GeneratedCode("DomGen", "2.0")]
	internal class FirstColumn : TablePartStyleType
	{
		// Token: 0x17006550 RID: 25936
		// (get) Token: 0x060140BA RID: 82106 RVA: 0x0030EB02 File Offset: 0x0030CD02
		public override string LocalName
		{
			get
			{
				return "firstCol";
			}
		}

		// Token: 0x17006551 RID: 25937
		// (get) Token: 0x060140BB RID: 82107 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006552 RID: 25938
		// (get) Token: 0x060140BC RID: 82108 RVA: 0x0030EB09 File Offset: 0x0030CD09
		internal override int ElementTypeId
		{
			get
			{
				return 10286;
			}
		}

		// Token: 0x060140BD RID: 82109 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060140BE RID: 82110 RVA: 0x0030EA63 File Offset: 0x0030CC63
		public FirstColumn()
		{
		}

		// Token: 0x060140BF RID: 82111 RVA: 0x0030EA6B File Offset: 0x0030CC6B
		public FirstColumn(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140C0 RID: 82112 RVA: 0x0030EA74 File Offset: 0x0030CC74
		public FirstColumn(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140C1 RID: 82113 RVA: 0x0030EA7D File Offset: 0x0030CC7D
		public FirstColumn(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060140C2 RID: 82114 RVA: 0x0030EB10 File Offset: 0x0030CD10
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FirstColumn>(deep);
		}

		// Token: 0x040088CA RID: 35018
		private const string tagName = "firstCol";

		// Token: 0x040088CB RID: 35019
		private const byte tagNsId = 10;

		// Token: 0x040088CC RID: 35020
		internal const int ElementTypeIdConst = 10286;
	}
}
