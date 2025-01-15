using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A78 RID: 10872
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SlideLayoutId))]
	internal class SlideLayoutIdList : OpenXmlCompositeElement
	{
		// Token: 0x17007327 RID: 29479
		// (get) Token: 0x06016041 RID: 90177 RVA: 0x00325B4F File Offset: 0x00323D4F
		public override string LocalName
		{
			get
			{
				return "sldLayoutIdLst";
			}
		}

		// Token: 0x17007328 RID: 29480
		// (get) Token: 0x06016042 RID: 90178 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007329 RID: 29481
		// (get) Token: 0x06016043 RID: 90179 RVA: 0x00325B56 File Offset: 0x00323D56
		internal override int ElementTypeId
		{
			get
			{
				return 12287;
			}
		}

		// Token: 0x06016044 RID: 90180 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016045 RID: 90181 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlideLayoutIdList()
		{
		}

		// Token: 0x06016046 RID: 90182 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlideLayoutIdList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016047 RID: 90183 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlideLayoutIdList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016048 RID: 90184 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlideLayoutIdList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016049 RID: 90185 RVA: 0x00325B5D File Offset: 0x00323D5D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "sldLayoutId" == name)
			{
				return new SlideLayoutId();
			}
			return null;
		}

		// Token: 0x0601604A RID: 90186 RVA: 0x00325B78 File Offset: 0x00323D78
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideLayoutIdList>(deep);
		}

		// Token: 0x040095CF RID: 38351
		private const string tagName = "sldLayoutIdLst";

		// Token: 0x040095D0 RID: 38352
		private const byte tagNsId = 24;

		// Token: 0x040095D1 RID: 38353
		internal const int ElementTypeIdConst = 12287;
	}
}
