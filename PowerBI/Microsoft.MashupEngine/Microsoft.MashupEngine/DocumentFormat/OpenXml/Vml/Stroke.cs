using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml.Office;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002246 RID: 8774
	[ChildElementInfo(typeof(ColumnStroke))]
	[ChildElementInfo(typeof(TopStroke))]
	[ChildElementInfo(typeof(LeftStroke))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BottomStroke))]
	[ChildElementInfo(typeof(RightStroke))]
	internal class Stroke : OpenXmlCompositeElement
	{
		// Token: 0x170039B8 RID: 14776
		// (get) Token: 0x0600E129 RID: 57641 RVA: 0x002C06DA File Offset: 0x002BE8DA
		public override string LocalName
		{
			get
			{
				return "stroke";
			}
		}

		// Token: 0x170039B9 RID: 14777
		// (get) Token: 0x0600E12A RID: 57642 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x170039BA RID: 14778
		// (get) Token: 0x0600E12B RID: 57643 RVA: 0x002C06E1 File Offset: 0x002BE8E1
		internal override int ElementTypeId
		{
			get
			{
				return 12510;
			}
		}

		// Token: 0x0600E12C RID: 57644 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170039BB RID: 14779
		// (get) Token: 0x0600E12D RID: 57645 RVA: 0x002C06E8 File Offset: 0x002BE8E8
		internal override string[] AttributeTagNames
		{
			get
			{
				return Stroke.attributeTagNames;
			}
		}

		// Token: 0x170039BC RID: 14780
		// (get) Token: 0x0600E12E RID: 57646 RVA: 0x002C06EF File Offset: 0x002BE8EF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Stroke.attributeNamespaceIds;
			}
		}

		// Token: 0x170039BD RID: 14781
		// (get) Token: 0x0600E12F RID: 57647 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E130 RID: 57648 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x170039BE RID: 14782
		// (get) Token: 0x0600E131 RID: 57649 RVA: 0x002BDACB File Offset: 0x002BBCCB
		// (set) Token: 0x0600E132 RID: 57650 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "on")]
		public TrueFalseValue On
		{
			get
			{
				return (TrueFalseValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170039BF RID: 14783
		// (get) Token: 0x0600E133 RID: 57651 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E134 RID: 57652 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "weight")]
		public StringValue Weight
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

		// Token: 0x170039C0 RID: 14784
		// (get) Token: 0x0600E135 RID: 57653 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E136 RID: 57654 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "color")]
		public StringValue Color
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170039C1 RID: 14785
		// (get) Token: 0x0600E137 RID: 57655 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E138 RID: 57656 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "opacity")]
		public StringValue Opacity
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170039C2 RID: 14786
		// (get) Token: 0x0600E139 RID: 57657 RVA: 0x002C06F6 File Offset: 0x002BE8F6
		// (set) Token: 0x0600E13A RID: 57658 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "linestyle")]
		public EnumValue<StrokeLineStyleValues> LineStyle
		{
			get
			{
				return (EnumValue<StrokeLineStyleValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170039C3 RID: 14787
		// (get) Token: 0x0600E13B RID: 57659 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E13C RID: 57660 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "miterlimit")]
		public StringValue Miterlimit
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

		// Token: 0x170039C4 RID: 14788
		// (get) Token: 0x0600E13D RID: 57661 RVA: 0x002C0705 File Offset: 0x002BE905
		// (set) Token: 0x0600E13E RID: 57662 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "joinstyle")]
		public EnumValue<StrokeJoinStyleValues> JoinStyle
		{
			get
			{
				return (EnumValue<StrokeJoinStyleValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170039C5 RID: 14789
		// (get) Token: 0x0600E13F RID: 57663 RVA: 0x002C0714 File Offset: 0x002BE914
		// (set) Token: 0x0600E140 RID: 57664 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "endcap")]
		public EnumValue<StrokeEndCapValues> EndCap
		{
			get
			{
				return (EnumValue<StrokeEndCapValues>)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170039C6 RID: 14790
		// (get) Token: 0x0600E141 RID: 57665 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E142 RID: 57666 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "dashstyle")]
		public StringValue DashStyle
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

		// Token: 0x170039C7 RID: 14791
		// (get) Token: 0x0600E143 RID: 57667 RVA: 0x002C0723 File Offset: 0x002BE923
		// (set) Token: 0x0600E144 RID: 57668 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "filltype")]
		public EnumValue<StrokeFillTypeValues> FillType
		{
			get
			{
				return (EnumValue<StrokeFillTypeValues>)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x170039C8 RID: 14792
		// (get) Token: 0x0600E145 RID: 57669 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E146 RID: 57670 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "src")]
		public StringValue Source
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x170039C9 RID: 14793
		// (get) Token: 0x0600E147 RID: 57671 RVA: 0x002C029C File Offset: 0x002BE49C
		// (set) Token: 0x0600E148 RID: 57672 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "imageaspect")]
		public EnumValue<ImageAspectValues> ImageAspect
		{
			get
			{
				return (EnumValue<ImageAspectValues>)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x170039CA RID: 14794
		// (get) Token: 0x0600E149 RID: 57673 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600E14A RID: 57674 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "imagesize")]
		public StringValue ImageSize
		{
			get
			{
				return (StringValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x170039CB RID: 14795
		// (get) Token: 0x0600E14B RID: 57675 RVA: 0x002BFFE2 File Offset: 0x002BE1E2
		// (set) Token: 0x0600E14C RID: 57676 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "imagealignshape")]
		public TrueFalseValue ImageAlignShape
		{
			get
			{
				return (TrueFalseValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x170039CC RID: 14796
		// (get) Token: 0x0600E14D RID: 57677 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600E14E RID: 57678 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "color2")]
		public StringValue Color2
		{
			get
			{
				return (StringValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x170039CD RID: 14797
		// (get) Token: 0x0600E14F RID: 57679 RVA: 0x002C0733 File Offset: 0x002BE933
		// (set) Token: 0x0600E150 RID: 57680 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "startarrow")]
		public EnumValue<StrokeArrowValues> StartArrow
		{
			get
			{
				return (EnumValue<StrokeArrowValues>)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x170039CE RID: 14798
		// (get) Token: 0x0600E151 RID: 57681 RVA: 0x002C0743 File Offset: 0x002BE943
		// (set) Token: 0x0600E152 RID: 57682 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "startarrowwidth")]
		public EnumValue<StrokeArrowWidthValues> StartArrowWidth
		{
			get
			{
				return (EnumValue<StrokeArrowWidthValues>)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x170039CF RID: 14799
		// (get) Token: 0x0600E153 RID: 57683 RVA: 0x002C0753 File Offset: 0x002BE953
		// (set) Token: 0x0600E154 RID: 57684 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "startarrowlength")]
		public EnumValue<StrokeArrowLengthValues> StartArrowLength
		{
			get
			{
				return (EnumValue<StrokeArrowLengthValues>)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x170039D0 RID: 14800
		// (get) Token: 0x0600E155 RID: 57685 RVA: 0x002C0763 File Offset: 0x002BE963
		// (set) Token: 0x0600E156 RID: 57686 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "endarrow")]
		public EnumValue<StrokeArrowValues> EndArrow
		{
			get
			{
				return (EnumValue<StrokeArrowValues>)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x170039D1 RID: 14801
		// (get) Token: 0x0600E157 RID: 57687 RVA: 0x002C0773 File Offset: 0x002BE973
		// (set) Token: 0x0600E158 RID: 57688 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "endarrowwidth")]
		public EnumValue<StrokeArrowWidthValues> EndArrowWidth
		{
			get
			{
				return (EnumValue<StrokeArrowWidthValues>)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x170039D2 RID: 14802
		// (get) Token: 0x0600E159 RID: 57689 RVA: 0x002C0783 File Offset: 0x002BE983
		// (set) Token: 0x0600E15A RID: 57690 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "endarrowlength")]
		public EnumValue<StrokeArrowLengthValues> EndArrowLength
		{
			get
			{
				return (EnumValue<StrokeArrowLengthValues>)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x170039D3 RID: 14803
		// (get) Token: 0x0600E15B RID: 57691 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600E15C RID: 57692 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(27, "href")]
		public StringValue Href
		{
			get
			{
				return (StringValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x170039D4 RID: 14804
		// (get) Token: 0x0600E15D RID: 57693 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600E15E RID: 57694 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(27, "althref")]
		public StringValue AlternateImageReference
		{
			get
			{
				return (StringValue)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x170039D5 RID: 14805
		// (get) Token: 0x0600E15F RID: 57695 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600E160 RID: 57696 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(27, "title")]
		public StringValue Title
		{
			get
			{
				return (StringValue)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x170039D6 RID: 14806
		// (get) Token: 0x0600E161 RID: 57697 RVA: 0x002C0793 File Offset: 0x002BE993
		// (set) Token: 0x0600E162 RID: 57698 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(27, "forcedash")]
		public TrueFalseValue ForceDash
		{
			get
			{
				return (TrueFalseValue)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x170039D7 RID: 14807
		// (get) Token: 0x0600E163 RID: 57699 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600E164 RID: 57700 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(19, "id")]
		public StringValue RelationshipId
		{
			get
			{
				return (StringValue)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x170039D8 RID: 14808
		// (get) Token: 0x0600E165 RID: 57701 RVA: 0x002BE381 File Offset: 0x002BC581
		// (set) Token: 0x0600E166 RID: 57702 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "insetpen")]
		public TrueFalseValue Insetpen
		{
			get
			{
				return (TrueFalseValue)base.Attributes[27];
			}
			set
			{
				base.Attributes[27] = value;
			}
		}

		// Token: 0x0600E167 RID: 57703 RVA: 0x00293ECF File Offset: 0x002920CF
		public Stroke()
		{
		}

		// Token: 0x0600E168 RID: 57704 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Stroke(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E169 RID: 57705 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Stroke(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E16A RID: 57706 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Stroke(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E16B RID: 57707 RVA: 0x002C07A4 File Offset: 0x002BE9A4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (27 == namespaceId && "left" == name)
			{
				return new LeftStroke();
			}
			if (27 == namespaceId && "top" == name)
			{
				return new TopStroke();
			}
			if (27 == namespaceId && "right" == name)
			{
				return new RightStroke();
			}
			if (27 == namespaceId && "bottom" == name)
			{
				return new BottomStroke();
			}
			if (27 == namespaceId && "column" == name)
			{
				return new ColumnStroke();
			}
			return null;
		}

		// Token: 0x170039D9 RID: 14809
		// (get) Token: 0x0600E16C RID: 57708 RVA: 0x002C082A File Offset: 0x002BEA2A
		internal override string[] ElementTagNames
		{
			get
			{
				return Stroke.eleTagNames;
			}
		}

		// Token: 0x170039DA RID: 14810
		// (get) Token: 0x0600E16D RID: 57709 RVA: 0x002C0831 File Offset: 0x002BEA31
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Stroke.eleNamespaceIds;
			}
		}

		// Token: 0x170039DB RID: 14811
		// (get) Token: 0x0600E16E RID: 57710 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170039DC RID: 14812
		// (get) Token: 0x0600E16F RID: 57711 RVA: 0x002C0838 File Offset: 0x002BEA38
		// (set) Token: 0x0600E170 RID: 57712 RVA: 0x002C0841 File Offset: 0x002BEA41
		public LeftStroke LeftStroke
		{
			get
			{
				return base.GetElement<LeftStroke>(0);
			}
			set
			{
				base.SetElement<LeftStroke>(0, value);
			}
		}

		// Token: 0x170039DD RID: 14813
		// (get) Token: 0x0600E171 RID: 57713 RVA: 0x002C084B File Offset: 0x002BEA4B
		// (set) Token: 0x0600E172 RID: 57714 RVA: 0x002C0854 File Offset: 0x002BEA54
		public TopStroke TopStroke
		{
			get
			{
				return base.GetElement<TopStroke>(1);
			}
			set
			{
				base.SetElement<TopStroke>(1, value);
			}
		}

		// Token: 0x170039DE RID: 14814
		// (get) Token: 0x0600E173 RID: 57715 RVA: 0x002C085E File Offset: 0x002BEA5E
		// (set) Token: 0x0600E174 RID: 57716 RVA: 0x002C0867 File Offset: 0x002BEA67
		public RightStroke RightStroke
		{
			get
			{
				return base.GetElement<RightStroke>(2);
			}
			set
			{
				base.SetElement<RightStroke>(2, value);
			}
		}

		// Token: 0x170039DF RID: 14815
		// (get) Token: 0x0600E175 RID: 57717 RVA: 0x002C0871 File Offset: 0x002BEA71
		// (set) Token: 0x0600E176 RID: 57718 RVA: 0x002C087A File Offset: 0x002BEA7A
		public BottomStroke BottomStroke
		{
			get
			{
				return base.GetElement<BottomStroke>(3);
			}
			set
			{
				base.SetElement<BottomStroke>(3, value);
			}
		}

		// Token: 0x170039E0 RID: 14816
		// (get) Token: 0x0600E177 RID: 57719 RVA: 0x002C0884 File Offset: 0x002BEA84
		// (set) Token: 0x0600E178 RID: 57720 RVA: 0x002C088D File Offset: 0x002BEA8D
		public ColumnStroke ColumnStroke
		{
			get
			{
				return base.GetElement<ColumnStroke>(4);
			}
			set
			{
				base.SetElement<ColumnStroke>(4, value);
			}
		}

		// Token: 0x0600E179 RID: 57721 RVA: 0x002C0898 File Offset: 0x002BEA98
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "on" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "weight" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "color" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "opacity" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "linestyle" == name)
			{
				return new EnumValue<StrokeLineStyleValues>();
			}
			if (namespaceId == 0 && "miterlimit" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "joinstyle" == name)
			{
				return new EnumValue<StrokeJoinStyleValues>();
			}
			if (namespaceId == 0 && "endcap" == name)
			{
				return new EnumValue<StrokeEndCapValues>();
			}
			if (namespaceId == 0 && "dashstyle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "filltype" == name)
			{
				return new EnumValue<StrokeFillTypeValues>();
			}
			if (namespaceId == 0 && "src" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "imageaspect" == name)
			{
				return new EnumValue<ImageAspectValues>();
			}
			if (namespaceId == 0 && "imagesize" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "imagealignshape" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "color2" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "startarrow" == name)
			{
				return new EnumValue<StrokeArrowValues>();
			}
			if (namespaceId == 0 && "startarrowwidth" == name)
			{
				return new EnumValue<StrokeArrowWidthValues>();
			}
			if (namespaceId == 0 && "startarrowlength" == name)
			{
				return new EnumValue<StrokeArrowLengthValues>();
			}
			if (namespaceId == 0 && "endarrow" == name)
			{
				return new EnumValue<StrokeArrowValues>();
			}
			if (namespaceId == 0 && "endarrowwidth" == name)
			{
				return new EnumValue<StrokeArrowWidthValues>();
			}
			if (namespaceId == 0 && "endarrowlength" == name)
			{
				return new EnumValue<StrokeArrowLengthValues>();
			}
			if (27 == namespaceId && "href" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "althref" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "title" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "forcedash" == name)
			{
				return new TrueFalseValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insetpen" == name)
			{
				return new TrueFalseValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E17A RID: 57722 RVA: 0x002C0B1F File Offset: 0x002BED1F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Stroke>(deep);
		}

		// Token: 0x04006EA2 RID: 28322
		private const string tagName = "stroke";

		// Token: 0x04006EA3 RID: 28323
		private const byte tagNsId = 26;

		// Token: 0x04006EA4 RID: 28324
		internal const int ElementTypeIdConst = 12510;

		// Token: 0x04006EA5 RID: 28325
		private static string[] attributeTagNames = new string[]
		{
			"id", "on", "weight", "color", "opacity", "linestyle", "miterlimit", "joinstyle", "endcap", "dashstyle",
			"filltype", "src", "imageaspect", "imagesize", "imagealignshape", "color2", "startarrow", "startarrowwidth", "startarrowlength", "endarrow",
			"endarrowwidth", "endarrowlength", "href", "althref", "title", "forcedash", "id", "insetpen"
		};

		// Token: 0x04006EA6 RID: 28326
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 27, 27, 27, 27, 19, 0
		};

		// Token: 0x04006EA7 RID: 28327
		private static readonly string[] eleTagNames = new string[] { "left", "top", "right", "bottom", "column" };

		// Token: 0x04006EA8 RID: 28328
		private static readonly byte[] eleNamespaceIds = new byte[] { 27, 27, 27, 27, 27 };
	}
}
