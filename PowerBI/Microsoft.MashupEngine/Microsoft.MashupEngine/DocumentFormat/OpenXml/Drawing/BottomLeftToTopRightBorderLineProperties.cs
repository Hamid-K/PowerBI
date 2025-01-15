using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200275D RID: 10077
	[GeneratedCode("DomGen", "2.0")]
	internal class BottomLeftToTopRightBorderLineProperties : LinePropertiesType
	{
		// Token: 0x170060CC RID: 24780
		// (get) Token: 0x06013665 RID: 79461 RVA: 0x00306A17 File Offset: 0x00304C17
		public override string LocalName
		{
			get
			{
				return "lnBlToTr";
			}
		}

		// Token: 0x170060CD RID: 24781
		// (get) Token: 0x06013666 RID: 79462 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060CE RID: 24782
		// (get) Token: 0x06013667 RID: 79463 RVA: 0x00306A1E File Offset: 0x00304C1E
		internal override int ElementTypeId
		{
			get
			{
				return 10257;
			}
		}

		// Token: 0x06013668 RID: 79464 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013669 RID: 79465 RVA: 0x00306961 File Offset: 0x00304B61
		public BottomLeftToTopRightBorderLineProperties()
		{
		}

		// Token: 0x0601366A RID: 79466 RVA: 0x00306969 File Offset: 0x00304B69
		public BottomLeftToTopRightBorderLineProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601366B RID: 79467 RVA: 0x00306972 File Offset: 0x00304B72
		public BottomLeftToTopRightBorderLineProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601366C RID: 79468 RVA: 0x0030697B File Offset: 0x00304B7B
		public BottomLeftToTopRightBorderLineProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601366D RID: 79469 RVA: 0x00306A25 File Offset: 0x00304C25
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BottomLeftToTopRightBorderLineProperties>(deep);
		}

		// Token: 0x0400860D RID: 34317
		private const string tagName = "lnBlToTr";

		// Token: 0x0400860E RID: 34318
		private const byte tagNsId = 10;

		// Token: 0x0400860F RID: 34319
		internal const int ElementTypeIdConst = 10257;
	}
}
