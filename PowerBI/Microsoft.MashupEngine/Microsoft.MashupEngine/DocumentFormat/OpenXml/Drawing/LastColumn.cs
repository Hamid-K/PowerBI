using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200280A RID: 10250
	[GeneratedCode("DomGen", "2.0")]
	internal class LastColumn : TablePartStyleType
	{
		// Token: 0x1700654D RID: 25933
		// (get) Token: 0x060140B1 RID: 82097 RVA: 0x0030EAEB File Offset: 0x0030CCEB
		public override string LocalName
		{
			get
			{
				return "lastCol";
			}
		}

		// Token: 0x1700654E RID: 25934
		// (get) Token: 0x060140B2 RID: 82098 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700654F RID: 25935
		// (get) Token: 0x060140B3 RID: 82099 RVA: 0x0030EAF2 File Offset: 0x0030CCF2
		internal override int ElementTypeId
		{
			get
			{
				return 10285;
			}
		}

		// Token: 0x060140B4 RID: 82100 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060140B5 RID: 82101 RVA: 0x0030EA63 File Offset: 0x0030CC63
		public LastColumn()
		{
		}

		// Token: 0x060140B6 RID: 82102 RVA: 0x0030EA6B File Offset: 0x0030CC6B
		public LastColumn(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140B7 RID: 82103 RVA: 0x0030EA74 File Offset: 0x0030CC74
		public LastColumn(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140B8 RID: 82104 RVA: 0x0030EA7D File Offset: 0x0030CC7D
		public LastColumn(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060140B9 RID: 82105 RVA: 0x0030EAF9 File Offset: 0x0030CCF9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LastColumn>(deep);
		}

		// Token: 0x040088C7 RID: 35015
		private const string tagName = "lastCol";

		// Token: 0x040088C8 RID: 35016
		private const byte tagNsId = 10;

		// Token: 0x040088C9 RID: 35017
		internal const int ElementTypeIdConst = 10285;
	}
}
