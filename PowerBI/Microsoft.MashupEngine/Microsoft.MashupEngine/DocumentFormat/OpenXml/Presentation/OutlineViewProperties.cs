using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A8A RID: 10890
	[ChildElementInfo(typeof(OutlineViewSlideList))]
	[ChildElementInfo(typeof(CommonViewProperties))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class OutlineViewProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700739B RID: 29595
		// (get) Token: 0x06016140 RID: 90432 RVA: 0x003265AD File Offset: 0x003247AD
		public override string LocalName
		{
			get
			{
				return "outlineViewPr";
			}
		}

		// Token: 0x1700739C RID: 29596
		// (get) Token: 0x06016141 RID: 90433 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700739D RID: 29597
		// (get) Token: 0x06016142 RID: 90434 RVA: 0x003265B4 File Offset: 0x003247B4
		internal override int ElementTypeId
		{
			get
			{
				return 12303;
			}
		}

		// Token: 0x06016143 RID: 90435 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016144 RID: 90436 RVA: 0x00293ECF File Offset: 0x002920CF
		public OutlineViewProperties()
		{
		}

		// Token: 0x06016145 RID: 90437 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OutlineViewProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016146 RID: 90438 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OutlineViewProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016147 RID: 90439 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OutlineViewProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016148 RID: 90440 RVA: 0x003265BC File Offset: 0x003247BC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cViewPr" == name)
			{
				return new CommonViewProperties();
			}
			if (24 == namespaceId && "sldLst" == name)
			{
				return new OutlineViewSlideList();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700739E RID: 29598
		// (get) Token: 0x06016149 RID: 90441 RVA: 0x00326612 File Offset: 0x00324812
		internal override string[] ElementTagNames
		{
			get
			{
				return OutlineViewProperties.eleTagNames;
			}
		}

		// Token: 0x1700739F RID: 29599
		// (get) Token: 0x0601614A RID: 90442 RVA: 0x00326619 File Offset: 0x00324819
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return OutlineViewProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170073A0 RID: 29600
		// (get) Token: 0x0601614B RID: 90443 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170073A1 RID: 29601
		// (get) Token: 0x0601614C RID: 90444 RVA: 0x00326212 File Offset: 0x00324412
		// (set) Token: 0x0601614D RID: 90445 RVA: 0x0032621B File Offset: 0x0032441B
		public CommonViewProperties CommonViewProperties
		{
			get
			{
				return base.GetElement<CommonViewProperties>(0);
			}
			set
			{
				base.SetElement<CommonViewProperties>(0, value);
			}
		}

		// Token: 0x170073A2 RID: 29602
		// (get) Token: 0x0601614E RID: 90446 RVA: 0x00326620 File Offset: 0x00324820
		// (set) Token: 0x0601614F RID: 90447 RVA: 0x00326629 File Offset: 0x00324829
		public OutlineViewSlideList OutlineViewSlideList
		{
			get
			{
				return base.GetElement<OutlineViewSlideList>(1);
			}
			set
			{
				base.SetElement<OutlineViewSlideList>(1, value);
			}
		}

		// Token: 0x170073A3 RID: 29603
		// (get) Token: 0x06016150 RID: 90448 RVA: 0x003263D2 File Offset: 0x003245D2
		// (set) Token: 0x06016151 RID: 90449 RVA: 0x003263DB File Offset: 0x003245DB
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(2);
			}
			set
			{
				base.SetElement<ExtensionList>(2, value);
			}
		}

		// Token: 0x06016152 RID: 90450 RVA: 0x00326633 File Offset: 0x00324833
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OutlineViewProperties>(deep);
		}

		// Token: 0x0400961B RID: 38427
		private const string tagName = "outlineViewPr";

		// Token: 0x0400961C RID: 38428
		private const byte tagNsId = 24;

		// Token: 0x0400961D RID: 38429
		internal const int ElementTypeIdConst = 12303;

		// Token: 0x0400961E RID: 38430
		private static readonly string[] eleTagNames = new string[] { "cViewPr", "sldLst", "extLst" };

		// Token: 0x0400961F RID: 38431
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24 };
	}
}
