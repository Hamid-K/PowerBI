using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200275B RID: 10075
	[GeneratedCode("DomGen", "2.0")]
	internal class BottomBorderLineProperties : LinePropertiesType
	{
		// Token: 0x170060C6 RID: 24774
		// (get) Token: 0x06013653 RID: 79443 RVA: 0x003069E9 File Offset: 0x00304BE9
		public override string LocalName
		{
			get
			{
				return "lnB";
			}
		}

		// Token: 0x170060C7 RID: 24775
		// (get) Token: 0x06013654 RID: 79444 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060C8 RID: 24776
		// (get) Token: 0x06013655 RID: 79445 RVA: 0x003069F0 File Offset: 0x00304BF0
		internal override int ElementTypeId
		{
			get
			{
				return 10255;
			}
		}

		// Token: 0x06013656 RID: 79446 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013657 RID: 79447 RVA: 0x00306961 File Offset: 0x00304B61
		public BottomBorderLineProperties()
		{
		}

		// Token: 0x06013658 RID: 79448 RVA: 0x00306969 File Offset: 0x00304B69
		public BottomBorderLineProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013659 RID: 79449 RVA: 0x00306972 File Offset: 0x00304B72
		public BottomBorderLineProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601365A RID: 79450 RVA: 0x0030697B File Offset: 0x00304B7B
		public BottomBorderLineProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601365B RID: 79451 RVA: 0x003069F7 File Offset: 0x00304BF7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BottomBorderLineProperties>(deep);
		}

		// Token: 0x04008607 RID: 34311
		private const string tagName = "lnB";

		// Token: 0x04008608 RID: 34312
		private const byte tagNsId = 10;

		// Token: 0x04008609 RID: 34313
		internal const int ElementTypeIdConst = 10255;
	}
}
