using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200278A RID: 10122
	[GeneratedCode("DomGen", "2.0")]
	internal class ScaleY : RatioType
	{
		// Token: 0x170061D5 RID: 25045
		// (get) Token: 0x060138E1 RID: 80097 RVA: 0x003083DE File Offset: 0x003065DE
		public override string LocalName
		{
			get
			{
				return "sy";
			}
		}

		// Token: 0x170061D6 RID: 25046
		// (get) Token: 0x060138E2 RID: 80098 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061D7 RID: 25047
		// (get) Token: 0x060138E3 RID: 80099 RVA: 0x003083E5 File Offset: 0x003065E5
		internal override int ElementTypeId
		{
			get
			{
				return 10160;
			}
		}

		// Token: 0x060138E4 RID: 80100 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060138E6 RID: 80102 RVA: 0x003083EC File Offset: 0x003065EC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScaleY>(deep);
		}

		// Token: 0x040086BA RID: 34490
		private const string tagName = "sy";

		// Token: 0x040086BB RID: 34491
		private const byte tagNsId = 10;

		// Token: 0x040086BC RID: 34492
		internal const int ElementTypeIdConst = 10160;
	}
}
