using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002806 RID: 10246
	[GeneratedCode("DomGen", "2.0")]
	internal class Band1Horizontal : TablePartStyleType
	{
		// Token: 0x17006541 RID: 25921
		// (get) Token: 0x0601408D RID: 82061 RVA: 0x0030EA8F File Offset: 0x0030CC8F
		public override string LocalName
		{
			get
			{
				return "band1H";
			}
		}

		// Token: 0x17006542 RID: 25922
		// (get) Token: 0x0601408E RID: 82062 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006543 RID: 25923
		// (get) Token: 0x0601408F RID: 82063 RVA: 0x0030EA96 File Offset: 0x0030CC96
		internal override int ElementTypeId
		{
			get
			{
				return 10281;
			}
		}

		// Token: 0x06014090 RID: 82064 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014091 RID: 82065 RVA: 0x0030EA63 File Offset: 0x0030CC63
		public Band1Horizontal()
		{
		}

		// Token: 0x06014092 RID: 82066 RVA: 0x0030EA6B File Offset: 0x0030CC6B
		public Band1Horizontal(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014093 RID: 82067 RVA: 0x0030EA74 File Offset: 0x0030CC74
		public Band1Horizontal(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014094 RID: 82068 RVA: 0x0030EA7D File Offset: 0x0030CC7D
		public Band1Horizontal(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014095 RID: 82069 RVA: 0x0030EA9D File Offset: 0x0030CC9D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Band1Horizontal>(deep);
		}

		// Token: 0x040088BB RID: 35003
		private const string tagName = "band1H";

		// Token: 0x040088BC RID: 35004
		private const byte tagNsId = 10;

		// Token: 0x040088BD RID: 35005
		internal const int ElementTypeIdConst = 10281;
	}
}
