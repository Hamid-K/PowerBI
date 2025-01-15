using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A89 RID: 10889
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CommonSlideViewProperties))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class SlideViewProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007393 RID: 29587
		// (get) Token: 0x0601612E RID: 90414 RVA: 0x003264FE File Offset: 0x003246FE
		public override string LocalName
		{
			get
			{
				return "slideViewPr";
			}
		}

		// Token: 0x17007394 RID: 29588
		// (get) Token: 0x0601612F RID: 90415 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007395 RID: 29589
		// (get) Token: 0x06016130 RID: 90416 RVA: 0x00326505 File Offset: 0x00324705
		internal override int ElementTypeId
		{
			get
			{
				return 12302;
			}
		}

		// Token: 0x06016131 RID: 90417 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016132 RID: 90418 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlideViewProperties()
		{
		}

		// Token: 0x06016133 RID: 90419 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlideViewProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016134 RID: 90420 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlideViewProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016135 RID: 90421 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlideViewProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016136 RID: 90422 RVA: 0x0032650C File Offset: 0x0032470C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cSldViewPr" == name)
			{
				return new CommonSlideViewProperties();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007396 RID: 29590
		// (get) Token: 0x06016137 RID: 90423 RVA: 0x0032653F File Offset: 0x0032473F
		internal override string[] ElementTagNames
		{
			get
			{
				return SlideViewProperties.eleTagNames;
			}
		}

		// Token: 0x17007397 RID: 29591
		// (get) Token: 0x06016138 RID: 90424 RVA: 0x00326546 File Offset: 0x00324746
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SlideViewProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17007398 RID: 29592
		// (get) Token: 0x06016139 RID: 90425 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007399 RID: 29593
		// (get) Token: 0x0601613A RID: 90426 RVA: 0x0032654D File Offset: 0x0032474D
		// (set) Token: 0x0601613B RID: 90427 RVA: 0x00326556 File Offset: 0x00324756
		public CommonSlideViewProperties CommonSlideViewProperties
		{
			get
			{
				return base.GetElement<CommonSlideViewProperties>(0);
			}
			set
			{
				base.SetElement<CommonSlideViewProperties>(0, value);
			}
		}

		// Token: 0x1700739A RID: 29594
		// (get) Token: 0x0601613C RID: 90428 RVA: 0x00323F93 File Offset: 0x00322193
		// (set) Token: 0x0601613D RID: 90429 RVA: 0x00323F9C File Offset: 0x0032219C
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x0601613E RID: 90430 RVA: 0x00326560 File Offset: 0x00324760
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideViewProperties>(deep);
		}

		// Token: 0x04009616 RID: 38422
		private const string tagName = "slideViewPr";

		// Token: 0x04009617 RID: 38423
		private const byte tagNsId = 24;

		// Token: 0x04009618 RID: 38424
		internal const int ElementTypeIdConst = 12302;

		// Token: 0x04009619 RID: 38425
		private static readonly string[] eleTagNames = new string[] { "cSldViewPr", "extLst" };

		// Token: 0x0400961A RID: 38426
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24 };
	}
}
