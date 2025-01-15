using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x0200220C RID: 8716
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class StrokeChildType : OpenXmlLeafElement
	{
		// Token: 0x170038C7 RID: 14535
		// (get) Token: 0x0600DF26 RID: 57126 RVA: 0x002BEEC4 File Offset: 0x002BD0C4
		internal override string[] AttributeTagNames
		{
			get
			{
				return StrokeChildType.attributeTagNames;
			}
		}

		// Token: 0x170038C8 RID: 14536
		// (get) Token: 0x0600DF27 RID: 57127 RVA: 0x002BEECB File Offset: 0x002BD0CB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StrokeChildType.attributeNamespaceIds;
			}
		}

		// Token: 0x170038C9 RID: 14537
		// (get) Token: 0x0600DF28 RID: 57128 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600DF29 RID: 57129 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(26, "ext")]
		public EnumValue<ExtensionHandlingBehaviorValues> Extension
		{
			get
			{
				return (EnumValue<ExtensionHandlingBehaviorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170038CA RID: 14538
		// (get) Token: 0x0600DF2A RID: 57130 RVA: 0x002BDACB File Offset: 0x002BBCCB
		// (set) Token: 0x0600DF2B RID: 57131 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170038CB RID: 14539
		// (get) Token: 0x0600DF2C RID: 57132 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600DF2D RID: 57133 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170038CC RID: 14540
		// (get) Token: 0x0600DF2E RID: 57134 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600DF2F RID: 57135 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170038CD RID: 14541
		// (get) Token: 0x0600DF30 RID: 57136 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600DF31 RID: 57137 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "color2")]
		public StringValue Color2
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

		// Token: 0x170038CE RID: 14542
		// (get) Token: 0x0600DF32 RID: 57138 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600DF33 RID: 57139 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "opacity")]
		public StringValue Opacity
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

		// Token: 0x170038CF RID: 14543
		// (get) Token: 0x0600DF34 RID: 57140 RVA: 0x002BEED2 File Offset: 0x002BD0D2
		// (set) Token: 0x0600DF35 RID: 57141 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "linestyle")]
		public EnumValue<StrokeLineStyleValues> LineStyle
		{
			get
			{
				return (EnumValue<StrokeLineStyleValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170038D0 RID: 14544
		// (get) Token: 0x0600DF36 RID: 57142 RVA: 0x002BEEE1 File Offset: 0x002BD0E1
		// (set) Token: 0x0600DF37 RID: 57143 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "miterlimit")]
		public DecimalValue MiterLimit
		{
			get
			{
				return (DecimalValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170038D1 RID: 14545
		// (get) Token: 0x0600DF38 RID: 57144 RVA: 0x002BEEF0 File Offset: 0x002BD0F0
		// (set) Token: 0x0600DF39 RID: 57145 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "joinstyle")]
		public EnumValue<StrokeJoinStyleValues> JoinStyle
		{
			get
			{
				return (EnumValue<StrokeJoinStyleValues>)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170038D2 RID: 14546
		// (get) Token: 0x0600DF3A RID: 57146 RVA: 0x002BEEFF File Offset: 0x002BD0FF
		// (set) Token: 0x0600DF3B RID: 57147 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "endcap")]
		public EnumValue<StrokeEndCapValues> EndCap
		{
			get
			{
				return (EnumValue<StrokeEndCapValues>)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170038D3 RID: 14547
		// (get) Token: 0x0600DF3C RID: 57148 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600DF3D RID: 57149 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "dashstyle")]
		public StringValue DashStyle
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

		// Token: 0x170038D4 RID: 14548
		// (get) Token: 0x0600DF3E RID: 57150 RVA: 0x002BE837 File Offset: 0x002BCA37
		// (set) Token: 0x0600DF3F RID: 57151 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "insetpen")]
		public TrueFalseValue InsetPen
		{
			get
			{
				return (TrueFalseValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x170038D5 RID: 14549
		// (get) Token: 0x0600DF40 RID: 57152 RVA: 0x002BEF0F File Offset: 0x002BD10F
		// (set) Token: 0x0600DF41 RID: 57153 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "filltype")]
		public EnumValue<FillTypeValues> FillType
		{
			get
			{
				return (EnumValue<FillTypeValues>)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x170038D6 RID: 14550
		// (get) Token: 0x0600DF42 RID: 57154 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600DF43 RID: 57155 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "src")]
		public StringValue Source
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

		// Token: 0x170038D7 RID: 14551
		// (get) Token: 0x0600DF44 RID: 57156 RVA: 0x002BEF2F File Offset: 0x002BD12F
		// (set) Token: 0x0600DF45 RID: 57157 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "imageaspect")]
		public EnumValue<ImageAspectValues> ImageAspect
		{
			get
			{
				return (EnumValue<ImageAspectValues>)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x170038D8 RID: 14552
		// (get) Token: 0x0600DF46 RID: 57158 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600DF47 RID: 57159 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "imagesize")]
		public StringValue ImageSize
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

		// Token: 0x170038D9 RID: 14553
		// (get) Token: 0x0600DF48 RID: 57160 RVA: 0x002BEF3F File Offset: 0x002BD13F
		// (set) Token: 0x0600DF49 RID: 57161 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "imagealignshape")]
		public TrueFalseValue ImageAlignShape
		{
			get
			{
				return (TrueFalseValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x170038DA RID: 14554
		// (get) Token: 0x0600DF4A RID: 57162 RVA: 0x002BEF4F File Offset: 0x002BD14F
		// (set) Token: 0x0600DF4B RID: 57163 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "startarrow")]
		public EnumValue<StrokeArrowValues> StartArrow
		{
			get
			{
				return (EnumValue<StrokeArrowValues>)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x170038DB RID: 14555
		// (get) Token: 0x0600DF4C RID: 57164 RVA: 0x002BEF5F File Offset: 0x002BD15F
		// (set) Token: 0x0600DF4D RID: 57165 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "startarrowwidth")]
		public EnumValue<StrokeArrowWidthValues> StartArrowWidth
		{
			get
			{
				return (EnumValue<StrokeArrowWidthValues>)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x170038DC RID: 14556
		// (get) Token: 0x0600DF4E RID: 57166 RVA: 0x002BEF6F File Offset: 0x002BD16F
		// (set) Token: 0x0600DF4F RID: 57167 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "startarrowlength")]
		public EnumValue<StrokeArrowLengthValues> StartArrowLength
		{
			get
			{
				return (EnumValue<StrokeArrowLengthValues>)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x170038DD RID: 14557
		// (get) Token: 0x0600DF50 RID: 57168 RVA: 0x002BEF7F File Offset: 0x002BD17F
		// (set) Token: 0x0600DF51 RID: 57169 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "endarrow")]
		public EnumValue<StrokeArrowValues> EndArrow
		{
			get
			{
				return (EnumValue<StrokeArrowValues>)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x170038DE RID: 14558
		// (get) Token: 0x0600DF52 RID: 57170 RVA: 0x002BEF8F File Offset: 0x002BD18F
		// (set) Token: 0x0600DF53 RID: 57171 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "endarrowwidth")]
		public EnumValue<StrokeArrowWidthValues> EndArrowWidth
		{
			get
			{
				return (EnumValue<StrokeArrowWidthValues>)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x170038DF RID: 14559
		// (get) Token: 0x0600DF54 RID: 57172 RVA: 0x002BEF9F File Offset: 0x002BD19F
		// (set) Token: 0x0600DF55 RID: 57173 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "endarrowlength")]
		public EnumValue<StrokeArrowLengthValues> EndArrowLength
		{
			get
			{
				return (EnumValue<StrokeArrowLengthValues>)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x170038E0 RID: 14560
		// (get) Token: 0x0600DF56 RID: 57174 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600DF57 RID: 57175 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(27, "href")]
		public StringValue Href
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

		// Token: 0x170038E1 RID: 14561
		// (get) Token: 0x0600DF58 RID: 57176 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600DF59 RID: 57177 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(27, "althref")]
		public StringValue AlternateImageReference
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

		// Token: 0x170038E2 RID: 14562
		// (get) Token: 0x0600DF5A RID: 57178 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600DF5B RID: 57179 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(27, "title")]
		public StringValue Title
		{
			get
			{
				return (StringValue)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x170038E3 RID: 14563
		// (get) Token: 0x0600DF5C RID: 57180 RVA: 0x002BEFBF File Offset: 0x002BD1BF
		// (set) Token: 0x0600DF5D RID: 57181 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(27, "forcedash")]
		public TrueFalseValue ForceDash
		{
			get
			{
				return (TrueFalseValue)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x0600DF5E RID: 57182 RVA: 0x002BEFD0 File Offset: 0x002BD1D0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
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
			if (namespaceId == 0 && "color2" == name)
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
				return new DecimalValue();
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
			if (namespaceId == 0 && "insetpen" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "filltype" == name)
			{
				return new EnumValue<FillTypeValues>();
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x04006D8D RID: 28045
		private static string[] attributeTagNames = new string[]
		{
			"ext", "on", "weight", "color", "color2", "opacity", "linestyle", "miterlimit", "joinstyle", "endcap",
			"dashstyle", "insetpen", "filltype", "src", "imageaspect", "imagesize", "imagealignshape", "startarrow", "startarrowwidth", "startarrowlength",
			"endarrow", "endarrowwidth", "endarrowlength", "href", "althref", "title", "forcedash"
		};

		// Token: 0x04006D8E RID: 28046
		private static byte[] attributeNamespaceIds = new byte[]
		{
			26, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 27, 27, 27, 27
		};
	}
}
