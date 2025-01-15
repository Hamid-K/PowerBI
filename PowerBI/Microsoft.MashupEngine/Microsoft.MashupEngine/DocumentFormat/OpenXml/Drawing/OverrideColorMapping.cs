using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027E5 RID: 10213
	[GeneratedCode("DomGen", "2.0")]
	internal class OverrideColorMapping : ColorMappingType
	{
		// Token: 0x17006464 RID: 25700
		// (get) Token: 0x06013E78 RID: 81528 RVA: 0x0030D0B7 File Offset: 0x0030B2B7
		public override string LocalName
		{
			get
			{
				return "overrideClrMapping";
			}
		}

		// Token: 0x17006465 RID: 25701
		// (get) Token: 0x06013E79 RID: 81529 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006466 RID: 25702
		// (get) Token: 0x06013E7A RID: 81530 RVA: 0x0030D0BE File Offset: 0x0030B2BE
		internal override int ElementTypeId
		{
			get
			{
				return 10245;
			}
		}

		// Token: 0x06013E7B RID: 81531 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013E7C RID: 81532 RVA: 0x0030D0C5 File Offset: 0x0030B2C5
		public OverrideColorMapping()
		{
		}

		// Token: 0x06013E7D RID: 81533 RVA: 0x0030D0CD File Offset: 0x0030B2CD
		public OverrideColorMapping(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E7E RID: 81534 RVA: 0x0030D0D6 File Offset: 0x0030B2D6
		public OverrideColorMapping(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E7F RID: 81535 RVA: 0x0030D0DF File Offset: 0x0030B2DF
		public OverrideColorMapping(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013E80 RID: 81536 RVA: 0x0030D0E8 File Offset: 0x0030B2E8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OverrideColorMapping>(deep);
		}

		// Token: 0x04008839 RID: 34873
		private const string tagName = "overrideClrMapping";

		// Token: 0x0400883A RID: 34874
		private const byte tagNsId = 10;

		// Token: 0x0400883B RID: 34875
		internal const int ElementTypeIdConst = 10245;
	}
}
