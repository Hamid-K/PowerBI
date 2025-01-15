using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027EB RID: 10219
	[ChildElementInfo(typeof(CustomColor))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomColorList : OpenXmlCompositeElement
	{
		// Token: 0x17006489 RID: 25737
		// (get) Token: 0x06013ED2 RID: 81618 RVA: 0x0030D3EE File Offset: 0x0030B5EE
		public override string LocalName
		{
			get
			{
				return "custClrLst";
			}
		}

		// Token: 0x1700648A RID: 25738
		// (get) Token: 0x06013ED3 RID: 81619 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700648B RID: 25739
		// (get) Token: 0x06013ED4 RID: 81620 RVA: 0x0030D3F5 File Offset: 0x0030B5F5
		internal override int ElementTypeId
		{
			get
			{
				return 10251;
			}
		}

		// Token: 0x06013ED5 RID: 81621 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013ED6 RID: 81622 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomColorList()
		{
		}

		// Token: 0x06013ED7 RID: 81623 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomColorList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013ED8 RID: 81624 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomColorList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013ED9 RID: 81625 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomColorList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013EDA RID: 81626 RVA: 0x0030D3FC File Offset: 0x0030B5FC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "custClr" == name)
			{
				return new CustomColor();
			}
			return null;
		}

		// Token: 0x06013EDB RID: 81627 RVA: 0x0030D417 File Offset: 0x0030B617
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomColorList>(deep);
		}

		// Token: 0x04008851 RID: 34897
		private const string tagName = "custClrLst";

		// Token: 0x04008852 RID: 34898
		private const byte tagNsId = 10;

		// Token: 0x04008853 RID: 34899
		internal const int ElementTypeIdConst = 10251;
	}
}
