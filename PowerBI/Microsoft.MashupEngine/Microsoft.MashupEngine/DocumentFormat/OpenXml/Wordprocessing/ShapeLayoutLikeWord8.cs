using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E00 RID: 11776
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeLayoutLikeWord8 : OnOffType
	{
		// Token: 0x1700889C RID: 34972
		// (get) Token: 0x06018FF8 RID: 102392 RVA: 0x0034570D File Offset: 0x0034390D
		public override string LocalName
		{
			get
			{
				return "shapeLayoutLikeWW8";
			}
		}

		// Token: 0x1700889D RID: 34973
		// (get) Token: 0x06018FF9 RID: 102393 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700889E RID: 34974
		// (get) Token: 0x06018FFA RID: 102394 RVA: 0x00345714 File Offset: 0x00343914
		internal override int ElementTypeId
		{
			get
			{
				return 12086;
			}
		}

		// Token: 0x06018FFB RID: 102395 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FFD RID: 102397 RVA: 0x0034571B File Offset: 0x0034391B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeLayoutLikeWord8>(deep);
		}

		// Token: 0x0400A66B RID: 42603
		private const string tagName = "shapeLayoutLikeWW8";

		// Token: 0x0400A66C RID: 42604
		private const byte tagNsId = 23;

		// Token: 0x0400A66D RID: 42605
		internal const int ElementTypeIdConst = 12086;
	}
}
