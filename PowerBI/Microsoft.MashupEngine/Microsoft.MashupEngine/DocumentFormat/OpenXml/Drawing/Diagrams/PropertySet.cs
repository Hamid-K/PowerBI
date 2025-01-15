using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002692 RID: 9874
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PresentationLayoutVariables))]
	[ChildElementInfo(typeof(Style))]
	internal class PropertySet : OpenXmlCompositeElement
	{
		// Token: 0x17005D23 RID: 23843
		// (get) Token: 0x06012E5F RID: 77407 RVA: 0x003007E5 File Offset: 0x002FE9E5
		public override string LocalName
		{
			get
			{
				return "prSet";
			}
		}

		// Token: 0x17005D24 RID: 23844
		// (get) Token: 0x06012E60 RID: 77408 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005D25 RID: 23845
		// (get) Token: 0x06012E61 RID: 77409 RVA: 0x003007EC File Offset: 0x002FE9EC
		internal override int ElementTypeId
		{
			get
			{
				return 10689;
			}
		}

		// Token: 0x06012E62 RID: 77410 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005D26 RID: 23846
		// (get) Token: 0x06012E63 RID: 77411 RVA: 0x003007F3 File Offset: 0x002FE9F3
		internal override string[] AttributeTagNames
		{
			get
			{
				return PropertySet.attributeTagNames;
			}
		}

		// Token: 0x17005D27 RID: 23847
		// (get) Token: 0x06012E64 RID: 77412 RVA: 0x003007FA File Offset: 0x002FE9FA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PropertySet.attributeNamespaceIds;
			}
		}

		// Token: 0x17005D28 RID: 23848
		// (get) Token: 0x06012E65 RID: 77413 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012E66 RID: 77414 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "presAssocID")]
		public StringValue PresentationElementId
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005D29 RID: 23849
		// (get) Token: 0x06012E67 RID: 77415 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06012E68 RID: 77416 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "presName")]
		public StringValue PresentationName
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005D2A RID: 23850
		// (get) Token: 0x06012E69 RID: 77417 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06012E6A RID: 77418 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "presStyleLbl")]
		public StringValue PresentationStyleLabel
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17005D2B RID: 23851
		// (get) Token: 0x06012E6B RID: 77419 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x06012E6C RID: 77420 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "presStyleIdx")]
		public Int32Value PresentationStyleIndex
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17005D2C RID: 23852
		// (get) Token: 0x06012E6D RID: 77421 RVA: 0x002C8292 File Offset: 0x002C6492
		// (set) Token: 0x06012E6E RID: 77422 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "presStyleCnt")]
		public Int32Value PresentationStyleCount
		{
			get
			{
				return (Int32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17005D2D RID: 23853
		// (get) Token: 0x06012E6F RID: 77423 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x06012E70 RID: 77424 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "loTypeId")]
		public StringValue LayoutTypeId
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17005D2E RID: 23854
		// (get) Token: 0x06012E71 RID: 77425 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06012E72 RID: 77426 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "loCatId")]
		public StringValue LayoutCategoryId
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17005D2F RID: 23855
		// (get) Token: 0x06012E73 RID: 77427 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x06012E74 RID: 77428 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "qsTypeId")]
		public StringValue QuickStyleTypeId
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17005D30 RID: 23856
		// (get) Token: 0x06012E75 RID: 77429 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x06012E76 RID: 77430 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "qsCatId")]
		public StringValue QuickStyleCategoryId
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17005D31 RID: 23857
		// (get) Token: 0x06012E77 RID: 77431 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x06012E78 RID: 77432 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "csTypeId")]
		public StringValue ColorType
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17005D32 RID: 23858
		// (get) Token: 0x06012E79 RID: 77433 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x06012E7A RID: 77434 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "csCatId")]
		public StringValue ColorCategoryId
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17005D33 RID: 23859
		// (get) Token: 0x06012E7B RID: 77435 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x06012E7C RID: 77436 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "coherent3DOff")]
		public BooleanValue Coherent3D
		{
			get
			{
				return (BooleanValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17005D34 RID: 23860
		// (get) Token: 0x06012E7D RID: 77437 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x06012E7E RID: 77438 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "phldrT")]
		public StringValue PlaceholderText
		{
			get
			{
				return (StringValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17005D35 RID: 23861
		// (get) Token: 0x06012E7F RID: 77439 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x06012E80 RID: 77440 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "phldr")]
		public BooleanValue Placeholder
		{
			get
			{
				return (BooleanValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17005D36 RID: 23862
		// (get) Token: 0x06012E81 RID: 77441 RVA: 0x00300801 File Offset: 0x002FEA01
		// (set) Token: 0x06012E82 RID: 77442 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "custAng")]
		public Int32Value Rotation
		{
			get
			{
				return (Int32Value)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17005D37 RID: 23863
		// (get) Token: 0x06012E83 RID: 77443 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x06012E84 RID: 77444 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "custFlipVert")]
		public BooleanValue VerticalFlip
		{
			get
			{
				return (BooleanValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17005D38 RID: 23864
		// (get) Token: 0x06012E85 RID: 77445 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x06012E86 RID: 77446 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "custFlipHor")]
		public BooleanValue HorizontalFlip
		{
			get
			{
				return (BooleanValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17005D39 RID: 23865
		// (get) Token: 0x06012E87 RID: 77447 RVA: 0x00300811 File Offset: 0x002FEA11
		// (set) Token: 0x06012E88 RID: 77448 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "custSzX")]
		public Int32Value FixedWidthOverride
		{
			get
			{
				return (Int32Value)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17005D3A RID: 23866
		// (get) Token: 0x06012E89 RID: 77449 RVA: 0x00300821 File Offset: 0x002FEA21
		// (set) Token: 0x06012E8A RID: 77450 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "custSzY")]
		public Int32Value FixedHeightOverride
		{
			get
			{
				return (Int32Value)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17005D3B RID: 23867
		// (get) Token: 0x06012E8B RID: 77451 RVA: 0x00300831 File Offset: 0x002FEA31
		// (set) Token: 0x06012E8C RID: 77452 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "custScaleX")]
		public Int32Value WidthScale
		{
			get
			{
				return (Int32Value)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x17005D3C RID: 23868
		// (get) Token: 0x06012E8D RID: 77453 RVA: 0x00300841 File Offset: 0x002FEA41
		// (set) Token: 0x06012E8E RID: 77454 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "custScaleY")]
		public Int32Value HightScale
		{
			get
			{
				return (Int32Value)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x17005D3D RID: 23869
		// (get) Token: 0x06012E8F RID: 77455 RVA: 0x002DB1B1 File Offset: 0x002D93B1
		// (set) Token: 0x06012E90 RID: 77456 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "custT")]
		public BooleanValue TextChanged
		{
			get
			{
				return (BooleanValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x17005D3E RID: 23870
		// (get) Token: 0x06012E91 RID: 77457 RVA: 0x00300851 File Offset: 0x002FEA51
		// (set) Token: 0x06012E92 RID: 77458 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "custLinFactX")]
		public Int32Value FactorWidth
		{
			get
			{
				return (Int32Value)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x17005D3F RID: 23871
		// (get) Token: 0x06012E93 RID: 77459 RVA: 0x00300861 File Offset: 0x002FEA61
		// (set) Token: 0x06012E94 RID: 77460 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "custLinFactY")]
		public Int32Value FactorHeight
		{
			get
			{
				return (Int32Value)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x17005D40 RID: 23872
		// (get) Token: 0x06012E95 RID: 77461 RVA: 0x00300871 File Offset: 0x002FEA71
		// (set) Token: 0x06012E96 RID: 77462 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "custLinFactNeighborX")]
		public Int32Value NeighborOffsetWidth
		{
			get
			{
				return (Int32Value)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x17005D41 RID: 23873
		// (get) Token: 0x06012E97 RID: 77463 RVA: 0x00300881 File Offset: 0x002FEA81
		// (set) Token: 0x06012E98 RID: 77464 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "custLinFactNeighborY")]
		public Int32Value NeighborOffsetHeight
		{
			get
			{
				return (Int32Value)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x17005D42 RID: 23874
		// (get) Token: 0x06012E99 RID: 77465 RVA: 0x00300891 File Offset: 0x002FEA91
		// (set) Token: 0x06012E9A RID: 77466 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "custRadScaleRad")]
		public Int32Value RadiusScale
		{
			get
			{
				return (Int32Value)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x17005D43 RID: 23875
		// (get) Token: 0x06012E9B RID: 77467 RVA: 0x003008A1 File Offset: 0x002FEAA1
		// (set) Token: 0x06012E9C RID: 77468 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "custRadScaleInc")]
		public Int32Value IncludeAngleScale
		{
			get
			{
				return (Int32Value)base.Attributes[27];
			}
			set
			{
				base.Attributes[27] = value;
			}
		}

		// Token: 0x06012E9D RID: 77469 RVA: 0x00293ECF File Offset: 0x002920CF
		public PropertySet()
		{
		}

		// Token: 0x06012E9E RID: 77470 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PropertySet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012E9F RID: 77471 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PropertySet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012EA0 RID: 77472 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PropertySet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012EA1 RID: 77473 RVA: 0x003008B1 File Offset: 0x002FEAB1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "presLayoutVars" == name)
			{
				return new PresentationLayoutVariables();
			}
			if (14 == namespaceId && "style" == name)
			{
				return new Style();
			}
			return null;
		}

		// Token: 0x17005D44 RID: 23876
		// (get) Token: 0x06012EA2 RID: 77474 RVA: 0x003008E4 File Offset: 0x002FEAE4
		internal override string[] ElementTagNames
		{
			get
			{
				return PropertySet.eleTagNames;
			}
		}

		// Token: 0x17005D45 RID: 23877
		// (get) Token: 0x06012EA3 RID: 77475 RVA: 0x003008EB File Offset: 0x002FEAEB
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PropertySet.eleNamespaceIds;
			}
		}

		// Token: 0x17005D46 RID: 23878
		// (get) Token: 0x06012EA4 RID: 77476 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005D47 RID: 23879
		// (get) Token: 0x06012EA5 RID: 77477 RVA: 0x003008F2 File Offset: 0x002FEAF2
		// (set) Token: 0x06012EA6 RID: 77478 RVA: 0x003008FB File Offset: 0x002FEAFB
		public PresentationLayoutVariables PresentationLayoutVariables
		{
			get
			{
				return base.GetElement<PresentationLayoutVariables>(0);
			}
			set
			{
				base.SetElement<PresentationLayoutVariables>(0, value);
			}
		}

		// Token: 0x17005D48 RID: 23880
		// (get) Token: 0x06012EA7 RID: 77479 RVA: 0x00300905 File Offset: 0x002FEB05
		// (set) Token: 0x06012EA8 RID: 77480 RVA: 0x0030090E File Offset: 0x002FEB0E
		public Style Style
		{
			get
			{
				return base.GetElement<Style>(1);
			}
			set
			{
				base.SetElement<Style>(1, value);
			}
		}

		// Token: 0x06012EA9 RID: 77481 RVA: 0x00300918 File Offset: 0x002FEB18
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "presAssocID" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "presName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "presStyleLbl" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "presStyleIdx" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "presStyleCnt" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "loTypeId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "loCatId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "qsTypeId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "qsCatId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "csTypeId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "csCatId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "coherent3DOff" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "phldrT" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "phldr" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "custAng" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "custFlipVert" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "custFlipHor" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "custSzX" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "custSzY" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "custScaleX" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "custScaleY" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "custT" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "custLinFactX" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "custLinFactY" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "custLinFactNeighborX" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "custLinFactNeighborY" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "custRadScaleRad" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "custRadScaleInc" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012EAA RID: 77482 RVA: 0x00300B95 File Offset: 0x002FED95
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PropertySet>(deep);
		}

		// Token: 0x06012EAB RID: 77483 RVA: 0x00300BA0 File Offset: 0x002FEDA0
		// Note: this type is marked as 'beforefieldinit'.
		static PropertySet()
		{
			byte[] array = new byte[28];
			PropertySet.attributeNamespaceIds = array;
			PropertySet.eleTagNames = new string[] { "presLayoutVars", "style" };
			PropertySet.eleNamespaceIds = new byte[] { 14, 14 };
		}

		// Token: 0x04008220 RID: 33312
		private const string tagName = "prSet";

		// Token: 0x04008221 RID: 33313
		private const byte tagNsId = 14;

		// Token: 0x04008222 RID: 33314
		internal const int ElementTypeIdConst = 10689;

		// Token: 0x04008223 RID: 33315
		private static string[] attributeTagNames = new string[]
		{
			"presAssocID", "presName", "presStyleLbl", "presStyleIdx", "presStyleCnt", "loTypeId", "loCatId", "qsTypeId", "qsCatId", "csTypeId",
			"csCatId", "coherent3DOff", "phldrT", "phldr", "custAng", "custFlipVert", "custFlipHor", "custSzX", "custSzY", "custScaleX",
			"custScaleY", "custT", "custLinFactX", "custLinFactY", "custLinFactNeighborX", "custLinFactNeighborY", "custRadScaleRad", "custRadScaleInc"
		};

		// Token: 0x04008224 RID: 33316
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008225 RID: 33317
		private static readonly string[] eleTagNames;

		// Token: 0x04008226 RID: 33318
		private static readonly byte[] eleNamespaceIds;
	}
}
