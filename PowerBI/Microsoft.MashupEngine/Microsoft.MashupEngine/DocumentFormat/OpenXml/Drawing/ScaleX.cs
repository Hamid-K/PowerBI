using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002789 RID: 10121
	[GeneratedCode("DomGen", "2.0")]
	internal class ScaleX : RatioType
	{
		// Token: 0x170061D2 RID: 25042
		// (get) Token: 0x060138DB RID: 80091 RVA: 0x003083BF File Offset: 0x003065BF
		public override string LocalName
		{
			get
			{
				return "sx";
			}
		}

		// Token: 0x170061D3 RID: 25043
		// (get) Token: 0x060138DC RID: 80092 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061D4 RID: 25044
		// (get) Token: 0x060138DD RID: 80093 RVA: 0x003083C6 File Offset: 0x003065C6
		internal override int ElementTypeId
		{
			get
			{
				return 10159;
			}
		}

		// Token: 0x060138DE RID: 80094 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060138E0 RID: 80096 RVA: 0x003083D5 File Offset: 0x003065D5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScaleX>(deep);
		}

		// Token: 0x040086B7 RID: 34487
		private const string tagName = "sx";

		// Token: 0x040086B8 RID: 34488
		private const byte tagNsId = 10;

		// Token: 0x040086B9 RID: 34489
		internal const int ElementTypeIdConst = 10159;
	}
}
