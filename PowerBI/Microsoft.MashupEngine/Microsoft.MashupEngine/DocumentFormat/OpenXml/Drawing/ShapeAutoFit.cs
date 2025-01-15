using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200273B RID: 10043
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeAutoFit : OpenXmlLeafElement
	{
		// Token: 0x17006049 RID: 24649
		// (get) Token: 0x06013527 RID: 79143 RVA: 0x0030615F File Offset: 0x0030435F
		public override string LocalName
		{
			get
			{
				return "spAutoFit";
			}
		}

		// Token: 0x1700604A RID: 24650
		// (get) Token: 0x06013528 RID: 79144 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700604B RID: 24651
		// (get) Token: 0x06013529 RID: 79145 RVA: 0x00306166 File Offset: 0x00304366
		internal override int ElementTypeId
		{
			get
			{
				return 10101;
			}
		}

		// Token: 0x0601352A RID: 79146 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601352C RID: 79148 RVA: 0x0030616D File Offset: 0x0030436D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeAutoFit>(deep);
		}

		// Token: 0x040085A0 RID: 34208
		private const string tagName = "spAutoFit";

		// Token: 0x040085A1 RID: 34209
		private const byte tagNsId = 10;

		// Token: 0x040085A2 RID: 34210
		internal const int ElementTypeIdConst = 10101;
	}
}
