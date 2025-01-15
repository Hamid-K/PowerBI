using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200275C RID: 10076
	[GeneratedCode("DomGen", "2.0")]
	internal class TopLeftToBottomRightBorderLineProperties : LinePropertiesType
	{
		// Token: 0x170060C9 RID: 24777
		// (get) Token: 0x0601365C RID: 79452 RVA: 0x00306A00 File Offset: 0x00304C00
		public override string LocalName
		{
			get
			{
				return "lnTlToBr";
			}
		}

		// Token: 0x170060CA RID: 24778
		// (get) Token: 0x0601365D RID: 79453 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060CB RID: 24779
		// (get) Token: 0x0601365E RID: 79454 RVA: 0x00306A07 File Offset: 0x00304C07
		internal override int ElementTypeId
		{
			get
			{
				return 10256;
			}
		}

		// Token: 0x0601365F RID: 79455 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013660 RID: 79456 RVA: 0x00306961 File Offset: 0x00304B61
		public TopLeftToBottomRightBorderLineProperties()
		{
		}

		// Token: 0x06013661 RID: 79457 RVA: 0x00306969 File Offset: 0x00304B69
		public TopLeftToBottomRightBorderLineProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013662 RID: 79458 RVA: 0x00306972 File Offset: 0x00304B72
		public TopLeftToBottomRightBorderLineProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013663 RID: 79459 RVA: 0x0030697B File Offset: 0x00304B7B
		public TopLeftToBottomRightBorderLineProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013664 RID: 79460 RVA: 0x00306A0E File Offset: 0x00304C0E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopLeftToBottomRightBorderLineProperties>(deep);
		}

		// Token: 0x0400860A RID: 34314
		private const string tagName = "lnTlToBr";

		// Token: 0x0400860B RID: 34315
		private const byte tagNsId = 10;

		// Token: 0x0400860C RID: 34316
		internal const int ElementTypeIdConst = 10256;
	}
}
