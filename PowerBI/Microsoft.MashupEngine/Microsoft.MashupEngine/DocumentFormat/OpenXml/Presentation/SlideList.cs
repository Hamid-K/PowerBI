using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A5B RID: 10843
	[ChildElementInfo(typeof(SlideListEntry))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SlideList : OpenXmlCompositeElement
	{
		// Token: 0x1700721F RID: 29215
		// (get) Token: 0x06015DF8 RID: 89592 RVA: 0x00323EF8 File Offset: 0x003220F8
		public override string LocalName
		{
			get
			{
				return "sldLst";
			}
		}

		// Token: 0x17007220 RID: 29216
		// (get) Token: 0x06015DF9 RID: 89593 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007221 RID: 29217
		// (get) Token: 0x06015DFA RID: 89594 RVA: 0x00323EFF File Offset: 0x003220FF
		internal override int ElementTypeId
		{
			get
			{
				return 12261;
			}
		}

		// Token: 0x06015DFB RID: 89595 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015DFC RID: 89596 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlideList()
		{
		}

		// Token: 0x06015DFD RID: 89597 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlideList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015DFE RID: 89598 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlideList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015DFF RID: 89599 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlideList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015E00 RID: 89600 RVA: 0x00323F06 File Offset: 0x00322106
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "sld" == name)
			{
				return new SlideListEntry();
			}
			return null;
		}

		// Token: 0x06015E01 RID: 89601 RVA: 0x00323F21 File Offset: 0x00322121
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideList>(deep);
		}

		// Token: 0x04009537 RID: 38199
		private const string tagName = "sldLst";

		// Token: 0x04009538 RID: 38200
		private const byte tagNsId = 24;

		// Token: 0x04009539 RID: 38201
		internal const int ElementTypeIdConst = 12261;
	}
}
