using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027EA RID: 10218
	[ChildElementInfo(typeof(ExtraColorScheme))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ExtraColorSchemeList : OpenXmlCompositeElement
	{
		// Token: 0x17006486 RID: 25734
		// (get) Token: 0x06013EC8 RID: 81608 RVA: 0x0030D3BC File Offset: 0x0030B5BC
		public override string LocalName
		{
			get
			{
				return "extraClrSchemeLst";
			}
		}

		// Token: 0x17006487 RID: 25735
		// (get) Token: 0x06013EC9 RID: 81609 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006488 RID: 25736
		// (get) Token: 0x06013ECA RID: 81610 RVA: 0x0030D3C3 File Offset: 0x0030B5C3
		internal override int ElementTypeId
		{
			get
			{
				return 10250;
			}
		}

		// Token: 0x06013ECB RID: 81611 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013ECC RID: 81612 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExtraColorSchemeList()
		{
		}

		// Token: 0x06013ECD RID: 81613 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExtraColorSchemeList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013ECE RID: 81614 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExtraColorSchemeList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013ECF RID: 81615 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExtraColorSchemeList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013ED0 RID: 81616 RVA: 0x0030D3CA File Offset: 0x0030B5CA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "extraClrScheme" == name)
			{
				return new ExtraColorScheme();
			}
			return null;
		}

		// Token: 0x06013ED1 RID: 81617 RVA: 0x0030D3E5 File Offset: 0x0030B5E5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExtraColorSchemeList>(deep);
		}

		// Token: 0x0400884E RID: 34894
		private const string tagName = "extraClrSchemeLst";

		// Token: 0x0400884F RID: 34895
		private const byte tagNsId = 10;

		// Token: 0x04008850 RID: 34896
		internal const int ElementTypeIdConst = 10250;
	}
}
