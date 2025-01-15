using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027E3 RID: 10211
	[GeneratedCode("DomGen", "2.0")]
	internal class TextDefault : DefaultShapeDefinitionType
	{
		// Token: 0x1700644F RID: 25679
		// (get) Token: 0x06013E49 RID: 81481 RVA: 0x0030CEB1 File Offset: 0x0030B0B1
		public override string LocalName
		{
			get
			{
				return "txDef";
			}
		}

		// Token: 0x17006450 RID: 25680
		// (get) Token: 0x06013E4A RID: 81482 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006451 RID: 25681
		// (get) Token: 0x06013E4B RID: 81483 RVA: 0x0030CEB8 File Offset: 0x0030B0B8
		internal override int ElementTypeId
		{
			get
			{
				return 10243;
			}
		}

		// Token: 0x06013E4C RID: 81484 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013E4D RID: 81485 RVA: 0x0030CE6E File Offset: 0x0030B06E
		public TextDefault()
		{
		}

		// Token: 0x06013E4E RID: 81486 RVA: 0x0030CE76 File Offset: 0x0030B076
		public TextDefault(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E4F RID: 81487 RVA: 0x0030CE7F File Offset: 0x0030B07F
		public TextDefault(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E50 RID: 81488 RVA: 0x0030CE88 File Offset: 0x0030B088
		public TextDefault(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013E51 RID: 81489 RVA: 0x0030CEBF File Offset: 0x0030B0BF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextDefault>(deep);
		}

		// Token: 0x04008832 RID: 34866
		private const string tagName = "txDef";

		// Token: 0x04008833 RID: 34867
		private const byte tagNsId = 10;

		// Token: 0x04008834 RID: 34868
		internal const int ElementTypeIdConst = 10243;
	}
}
