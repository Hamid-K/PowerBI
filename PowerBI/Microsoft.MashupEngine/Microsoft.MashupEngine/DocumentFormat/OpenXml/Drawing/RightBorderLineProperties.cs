using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002759 RID: 10073
	[GeneratedCode("DomGen", "2.0")]
	internal class RightBorderLineProperties : LinePropertiesType
	{
		// Token: 0x170060C0 RID: 24768
		// (get) Token: 0x06013641 RID: 79425 RVA: 0x003069BB File Offset: 0x00304BBB
		public override string LocalName
		{
			get
			{
				return "lnR";
			}
		}

		// Token: 0x170060C1 RID: 24769
		// (get) Token: 0x06013642 RID: 79426 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060C2 RID: 24770
		// (get) Token: 0x06013643 RID: 79427 RVA: 0x003069C2 File Offset: 0x00304BC2
		internal override int ElementTypeId
		{
			get
			{
				return 10253;
			}
		}

		// Token: 0x06013644 RID: 79428 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013645 RID: 79429 RVA: 0x00306961 File Offset: 0x00304B61
		public RightBorderLineProperties()
		{
		}

		// Token: 0x06013646 RID: 79430 RVA: 0x00306969 File Offset: 0x00304B69
		public RightBorderLineProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013647 RID: 79431 RVA: 0x00306972 File Offset: 0x00304B72
		public RightBorderLineProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013648 RID: 79432 RVA: 0x0030697B File Offset: 0x00304B7B
		public RightBorderLineProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013649 RID: 79433 RVA: 0x003069C9 File Offset: 0x00304BC9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RightBorderLineProperties>(deep);
		}

		// Token: 0x04008601 RID: 34305
		private const string tagName = "lnR";

		// Token: 0x04008602 RID: 34306
		private const byte tagNsId = 10;

		// Token: 0x04008603 RID: 34307
		internal const int ElementTypeIdConst = 10253;
	}
}
