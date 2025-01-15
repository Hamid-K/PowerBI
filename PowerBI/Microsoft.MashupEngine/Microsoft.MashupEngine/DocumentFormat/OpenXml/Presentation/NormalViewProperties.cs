using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A88 RID: 10888
	[ChildElementInfo(typeof(RestoredLeft))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RestoredTop))]
	internal class NormalViewProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007383 RID: 29571
		// (get) Token: 0x0601610D RID: 90381 RVA: 0x0032630B File Offset: 0x0032450B
		public override string LocalName
		{
			get
			{
				return "normalViewPr";
			}
		}

		// Token: 0x17007384 RID: 29572
		// (get) Token: 0x0601610E RID: 90382 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007385 RID: 29573
		// (get) Token: 0x0601610F RID: 90383 RVA: 0x00326312 File Offset: 0x00324512
		internal override int ElementTypeId
		{
			get
			{
				return 12301;
			}
		}

		// Token: 0x06016110 RID: 90384 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007386 RID: 29574
		// (get) Token: 0x06016111 RID: 90385 RVA: 0x00326319 File Offset: 0x00324519
		internal override string[] AttributeTagNames
		{
			get
			{
				return NormalViewProperties.attributeTagNames;
			}
		}

		// Token: 0x17007387 RID: 29575
		// (get) Token: 0x06016112 RID: 90386 RVA: 0x00326320 File Offset: 0x00324520
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NormalViewProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17007388 RID: 29576
		// (get) Token: 0x06016113 RID: 90387 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06016114 RID: 90388 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "showOutlineIcons")]
		public BooleanValue ShowOutlineIcons
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

		// Token: 0x17007389 RID: 29577
		// (get) Token: 0x06016115 RID: 90389 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06016116 RID: 90390 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "snapVertSplitter")]
		public BooleanValue SnapVerticalSplitter
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

		// Token: 0x1700738A RID: 29578
		// (get) Token: 0x06016117 RID: 90391 RVA: 0x00326327 File Offset: 0x00324527
		// (set) Token: 0x06016118 RID: 90392 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "vertBarState")]
		public EnumValue<SplitterBarStateValues> VerticalBarState
		{
			get
			{
				return (EnumValue<SplitterBarStateValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700738B RID: 29579
		// (get) Token: 0x06016119 RID: 90393 RVA: 0x00326336 File Offset: 0x00324536
		// (set) Token: 0x0601611A RID: 90394 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "horzBarState")]
		public EnumValue<SplitterBarStateValues> HorizontalBarState
		{
			get
			{
				return (EnumValue<SplitterBarStateValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700738C RID: 29580
		// (get) Token: 0x0601611B RID: 90395 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0601611C RID: 90396 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "preferSingleView")]
		public BooleanValue PreferSingleView
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x0601611D RID: 90397 RVA: 0x00293ECF File Offset: 0x002920CF
		public NormalViewProperties()
		{
		}

		// Token: 0x0601611E RID: 90398 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NormalViewProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601611F RID: 90399 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NormalViewProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016120 RID: 90400 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NormalViewProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016121 RID: 90401 RVA: 0x00326348 File Offset: 0x00324548
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "restoredLeft" == name)
			{
				return new RestoredLeft();
			}
			if (24 == namespaceId && "restoredTop" == name)
			{
				return new RestoredTop();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700738D RID: 29581
		// (get) Token: 0x06016122 RID: 90402 RVA: 0x0032639E File Offset: 0x0032459E
		internal override string[] ElementTagNames
		{
			get
			{
				return NormalViewProperties.eleTagNames;
			}
		}

		// Token: 0x1700738E RID: 29582
		// (get) Token: 0x06016123 RID: 90403 RVA: 0x003263A5 File Offset: 0x003245A5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NormalViewProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700738F RID: 29583
		// (get) Token: 0x06016124 RID: 90404 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007390 RID: 29584
		// (get) Token: 0x06016125 RID: 90405 RVA: 0x003263AC File Offset: 0x003245AC
		// (set) Token: 0x06016126 RID: 90406 RVA: 0x003263B5 File Offset: 0x003245B5
		public RestoredLeft RestoredLeft
		{
			get
			{
				return base.GetElement<RestoredLeft>(0);
			}
			set
			{
				base.SetElement<RestoredLeft>(0, value);
			}
		}

		// Token: 0x17007391 RID: 29585
		// (get) Token: 0x06016127 RID: 90407 RVA: 0x003263BF File Offset: 0x003245BF
		// (set) Token: 0x06016128 RID: 90408 RVA: 0x003263C8 File Offset: 0x003245C8
		public RestoredTop RestoredTop
		{
			get
			{
				return base.GetElement<RestoredTop>(1);
			}
			set
			{
				base.SetElement<RestoredTop>(1, value);
			}
		}

		// Token: 0x17007392 RID: 29586
		// (get) Token: 0x06016129 RID: 90409 RVA: 0x003263D2 File Offset: 0x003245D2
		// (set) Token: 0x0601612A RID: 90410 RVA: 0x003263DB File Offset: 0x003245DB
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

		// Token: 0x0601612B RID: 90411 RVA: 0x003263E8 File Offset: 0x003245E8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "showOutlineIcons" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "snapVertSplitter" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "vertBarState" == name)
			{
				return new EnumValue<SplitterBarStateValues>();
			}
			if (namespaceId == 0 && "horzBarState" == name)
			{
				return new EnumValue<SplitterBarStateValues>();
			}
			if (namespaceId == 0 && "preferSingleView" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601612C RID: 90412 RVA: 0x0032646B File Offset: 0x0032466B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NormalViewProperties>(deep);
		}

		// Token: 0x0601612D RID: 90413 RVA: 0x00326474 File Offset: 0x00324674
		// Note: this type is marked as 'beforefieldinit'.
		static NormalViewProperties()
		{
			byte[] array = new byte[5];
			NormalViewProperties.attributeNamespaceIds = array;
			NormalViewProperties.eleTagNames = new string[] { "restoredLeft", "restoredTop", "extLst" };
			NormalViewProperties.eleNamespaceIds = new byte[] { 24, 24, 24 };
		}

		// Token: 0x0400960F RID: 38415
		private const string tagName = "normalViewPr";

		// Token: 0x04009610 RID: 38416
		private const byte tagNsId = 24;

		// Token: 0x04009611 RID: 38417
		internal const int ElementTypeIdConst = 12301;

		// Token: 0x04009612 RID: 38418
		private static string[] attributeTagNames = new string[] { "showOutlineIcons", "snapVertSplitter", "vertBarState", "horzBarState", "preferSingleView" };

		// Token: 0x04009613 RID: 38419
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009614 RID: 38420
		private static readonly string[] eleTagNames;

		// Token: 0x04009615 RID: 38421
		private static readonly byte[] eleNamespaceIds;
	}
}
