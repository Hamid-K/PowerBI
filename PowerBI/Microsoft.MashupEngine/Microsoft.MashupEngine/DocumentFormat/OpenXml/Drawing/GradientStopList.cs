using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027C3 RID: 10179
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GradientStop))]
	internal class GradientStopList : OpenXmlCompositeElement
	{
		// Token: 0x17006375 RID: 25461
		// (get) Token: 0x06013C58 RID: 80984 RVA: 0x002EE9D0 File Offset: 0x002ECBD0
		public override string LocalName
		{
			get
			{
				return "gsLst";
			}
		}

		// Token: 0x17006376 RID: 25462
		// (get) Token: 0x06013C59 RID: 80985 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006377 RID: 25463
		// (get) Token: 0x06013C5A RID: 80986 RVA: 0x0030B90E File Offset: 0x00309B0E
		internal override int ElementTypeId
		{
			get
			{
				return 10209;
			}
		}

		// Token: 0x06013C5B RID: 80987 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013C5C RID: 80988 RVA: 0x00293ECF File Offset: 0x002920CF
		public GradientStopList()
		{
		}

		// Token: 0x06013C5D RID: 80989 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GradientStopList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013C5E RID: 80990 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GradientStopList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013C5F RID: 80991 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GradientStopList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013C60 RID: 80992 RVA: 0x0030B915 File Offset: 0x00309B15
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "gs" == name)
			{
				return new GradientStop();
			}
			return null;
		}

		// Token: 0x06013C61 RID: 80993 RVA: 0x0030B930 File Offset: 0x00309B30
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GradientStopList>(deep);
		}

		// Token: 0x040087B4 RID: 34740
		private const string tagName = "gsLst";

		// Token: 0x040087B5 RID: 34741
		private const byte tagNsId = 10;

		// Token: 0x040087B6 RID: 34742
		internal const int ElementTypeIdConst = 10209;
	}
}
