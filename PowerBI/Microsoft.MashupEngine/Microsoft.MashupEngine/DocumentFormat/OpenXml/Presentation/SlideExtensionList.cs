using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A9B RID: 10907
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SlideExtension))]
	internal class SlideExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x1700741D RID: 29725
		// (get) Token: 0x06016270 RID: 90736 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x1700741E RID: 29726
		// (get) Token: 0x06016271 RID: 90737 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700741F RID: 29727
		// (get) Token: 0x06016272 RID: 90738 RVA: 0x0032704B File Offset: 0x0032524B
		internal override int ElementTypeId
		{
			get
			{
				return 12322;
			}
		}

		// Token: 0x06016273 RID: 90739 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016274 RID: 90740 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlideExtensionList()
		{
		}

		// Token: 0x06016275 RID: 90741 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlideExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016276 RID: 90742 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlideExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016277 RID: 90743 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlideExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016278 RID: 90744 RVA: 0x00327052 File Offset: 0x00325252
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "ext" == name)
			{
				return new SlideExtension();
			}
			return null;
		}

		// Token: 0x06016279 RID: 90745 RVA: 0x0032706D File Offset: 0x0032526D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideExtensionList>(deep);
		}

		// Token: 0x04009674 RID: 38516
		private const string tagName = "extLst";

		// Token: 0x04009675 RID: 38517
		private const byte tagNsId = 24;

		// Token: 0x04009676 RID: 38518
		internal const int ElementTypeIdConst = 12322;
	}
}
