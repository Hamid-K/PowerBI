using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A87 RID: 10887
	[ChildElementInfo(typeof(CommonViewProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GuideList))]
	internal class CommonSlideViewProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007376 RID: 29558
		// (get) Token: 0x060160F2 RID: 90354 RVA: 0x003261B5 File Offset: 0x003243B5
		public override string LocalName
		{
			get
			{
				return "cSldViewPr";
			}
		}

		// Token: 0x17007377 RID: 29559
		// (get) Token: 0x060160F3 RID: 90355 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007378 RID: 29560
		// (get) Token: 0x060160F4 RID: 90356 RVA: 0x003261BC File Offset: 0x003243BC
		internal override int ElementTypeId
		{
			get
			{
				return 12300;
			}
		}

		// Token: 0x060160F5 RID: 90357 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007379 RID: 29561
		// (get) Token: 0x060160F6 RID: 90358 RVA: 0x003261C3 File Offset: 0x003243C3
		internal override string[] AttributeTagNames
		{
			get
			{
				return CommonSlideViewProperties.attributeTagNames;
			}
		}

		// Token: 0x1700737A RID: 29562
		// (get) Token: 0x060160F7 RID: 90359 RVA: 0x003261CA File Offset: 0x003243CA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CommonSlideViewProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700737B RID: 29563
		// (get) Token: 0x060160F8 RID: 90360 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060160F9 RID: 90361 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "snapToGrid")]
		public BooleanValue SnapToGrid
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700737C RID: 29564
		// (get) Token: 0x060160FA RID: 90362 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060160FB RID: 90363 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "snapToObjects")]
		public BooleanValue SnapToObjects
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700737D RID: 29565
		// (get) Token: 0x060160FC RID: 90364 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060160FD RID: 90365 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "showGuides")]
		public BooleanValue ShowGuides
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x060160FE RID: 90366 RVA: 0x00293ECF File Offset: 0x002920CF
		public CommonSlideViewProperties()
		{
		}

		// Token: 0x060160FF RID: 90367 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CommonSlideViewProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016100 RID: 90368 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CommonSlideViewProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016101 RID: 90369 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CommonSlideViewProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016102 RID: 90370 RVA: 0x003261D1 File Offset: 0x003243D1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cViewPr" == name)
			{
				return new CommonViewProperties();
			}
			if (24 == namespaceId && "guideLst" == name)
			{
				return new GuideList();
			}
			return null;
		}

		// Token: 0x1700737E RID: 29566
		// (get) Token: 0x06016103 RID: 90371 RVA: 0x00326204 File Offset: 0x00324404
		internal override string[] ElementTagNames
		{
			get
			{
				return CommonSlideViewProperties.eleTagNames;
			}
		}

		// Token: 0x1700737F RID: 29567
		// (get) Token: 0x06016104 RID: 90372 RVA: 0x0032620B File Offset: 0x0032440B
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CommonSlideViewProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17007380 RID: 29568
		// (get) Token: 0x06016105 RID: 90373 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007381 RID: 29569
		// (get) Token: 0x06016106 RID: 90374 RVA: 0x00326212 File Offset: 0x00324412
		// (set) Token: 0x06016107 RID: 90375 RVA: 0x0032621B File Offset: 0x0032441B
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

		// Token: 0x17007382 RID: 29570
		// (get) Token: 0x06016108 RID: 90376 RVA: 0x00326225 File Offset: 0x00324425
		// (set) Token: 0x06016109 RID: 90377 RVA: 0x0032622E File Offset: 0x0032442E
		public GuideList GuideList
		{
			get
			{
				return base.GetElement<GuideList>(1);
			}
			set
			{
				base.SetElement<GuideList>(1, value);
			}
		}

		// Token: 0x0601610A RID: 90378 RVA: 0x00326238 File Offset: 0x00324438
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "snapToGrid" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "snapToObjects" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showGuides" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601610B RID: 90379 RVA: 0x0032628F File Offset: 0x0032448F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommonSlideViewProperties>(deep);
		}

		// Token: 0x0601610C RID: 90380 RVA: 0x00326298 File Offset: 0x00324498
		// Note: this type is marked as 'beforefieldinit'.
		static CommonSlideViewProperties()
		{
			byte[] array = new byte[3];
			CommonSlideViewProperties.attributeNamespaceIds = array;
			CommonSlideViewProperties.eleTagNames = new string[] { "cViewPr", "guideLst" };
			CommonSlideViewProperties.eleNamespaceIds = new byte[] { 24, 24 };
		}

		// Token: 0x04009608 RID: 38408
		private const string tagName = "cSldViewPr";

		// Token: 0x04009609 RID: 38409
		private const byte tagNsId = 24;

		// Token: 0x0400960A RID: 38410
		internal const int ElementTypeIdConst = 12300;

		// Token: 0x0400960B RID: 38411
		private static string[] attributeTagNames = new string[] { "snapToGrid", "snapToObjects", "showGuides" };

		// Token: 0x0400960C RID: 38412
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400960D RID: 38413
		private static readonly string[] eleTagNames;

		// Token: 0x0400960E RID: 38414
		private static readonly byte[] eleNamespaceIds;
	}
}
