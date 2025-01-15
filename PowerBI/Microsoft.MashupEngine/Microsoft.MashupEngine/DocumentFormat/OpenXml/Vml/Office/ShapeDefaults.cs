using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002201 RID: 8705
	[ChildElementInfo(typeof(Shadow))]
	[ChildElementInfo(typeof(Stroke))]
	[ChildElementInfo(typeof(TextBox))]
	[ChildElementInfo(typeof(ImageData))]
	[ChildElementInfo(typeof(Skew))]
	[ChildElementInfo(typeof(Extrusion))]
	[ChildElementInfo(typeof(Callout))]
	[ChildElementInfo(typeof(Lock))]
	[ChildElementInfo(typeof(ColorMostRecentlyUsed))]
	[ChildElementInfo(typeof(ColorMenu))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Fill))]
	internal class ShapeDefaults : OpenXmlCompositeElement
	{
		// Token: 0x17003807 RID: 14343
		// (get) Token: 0x0600DDA2 RID: 56738 RVA: 0x002BD440 File Offset: 0x002BB640
		public override string LocalName
		{
			get
			{
				return "shapedefaults";
			}
		}

		// Token: 0x17003808 RID: 14344
		// (get) Token: 0x0600DDA3 RID: 56739 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x17003809 RID: 14345
		// (get) Token: 0x0600DDA4 RID: 56740 RVA: 0x002BD447 File Offset: 0x002BB647
		internal override int ElementTypeId
		{
			get
			{
				return 12399;
			}
		}

		// Token: 0x0600DDA5 RID: 56741 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700380A RID: 14346
		// (get) Token: 0x0600DDA6 RID: 56742 RVA: 0x002BD44E File Offset: 0x002BB64E
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeDefaults.attributeTagNames;
			}
		}

		// Token: 0x1700380B RID: 14347
		// (get) Token: 0x0600DDA7 RID: 56743 RVA: 0x002BD455 File Offset: 0x002BB655
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeDefaults.attributeNamespaceIds;
			}
		}

		// Token: 0x1700380C RID: 14348
		// (get) Token: 0x0600DDA8 RID: 56744 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600DDA9 RID: 56745 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700380D RID: 14349
		// (get) Token: 0x0600DDAA RID: 56746 RVA: 0x002BD46B File Offset: 0x002BB66B
		// (set) Token: 0x0600DDAB RID: 56747 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "spidmax")]
		public IntegerValue MaxShapeId
		{
			get
			{
				return (IntegerValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700380E RID: 14350
		// (get) Token: 0x0600DDAC RID: 56748 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600DDAD RID: 56749 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "style")]
		public StringValue Style
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

		// Token: 0x1700380F RID: 14351
		// (get) Token: 0x0600DDAE RID: 56750 RVA: 0x002BD49F File Offset: 0x002BB69F
		// (set) Token: 0x0600DDAF RID: 56751 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "fill")]
		public TrueFalseValue BeFilled
		{
			get
			{
				return (TrueFalseValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17003810 RID: 14352
		// (get) Token: 0x0600DDB0 RID: 56752 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600DDB1 RID: 56753 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "fillcolor")]
		public StringValue FillColor
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

		// Token: 0x17003811 RID: 14353
		// (get) Token: 0x0600DDB2 RID: 56754 RVA: 0x002BD4D3 File Offset: 0x002BB6D3
		// (set) Token: 0x0600DDB3 RID: 56755 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "stroke")]
		public TrueFalseValue IsStroke
		{
			get
			{
				return (TrueFalseValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17003812 RID: 14354
		// (get) Token: 0x0600DDB4 RID: 56756 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600DDB5 RID: 56757 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "strokecolor")]
		public StringValue StrokeColor
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

		// Token: 0x17003813 RID: 14355
		// (get) Token: 0x0600DDB6 RID: 56758 RVA: 0x002BD507 File Offset: 0x002BB707
		// (set) Token: 0x0600DDB7 RID: 56759 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(27, "allowincell")]
		public TrueFalseValue AllowInCell
		{
			get
			{
				return (TrueFalseValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17003814 RID: 14356
		// (get) Token: 0x0600DDB8 RID: 56760 RVA: 0x002BD521 File Offset: 0x002BB721
		// (set) Token: 0x0600DDB9 RID: 56761 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(27, "allowoverlap")]
		public TrueFalseValue AllowOverlap
		{
			get
			{
				return (TrueFalseValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17003815 RID: 14357
		// (get) Token: 0x0600DDBA RID: 56762 RVA: 0x002BD53B File Offset: 0x002BB73B
		// (set) Token: 0x0600DDBB RID: 56763 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(27, "insetmode")]
		public EnumValue<InsetMarginValues> InsetMode
		{
			get
			{
				return (EnumValue<InsetMarginValues>)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x0600DDBC RID: 56764 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeDefaults()
		{
		}

		// Token: 0x0600DDBD RID: 56765 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeDefaults(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DDBE RID: 56766 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeDefaults(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DDBF RID: 56767 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeDefaults(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600DDC0 RID: 56768 RVA: 0x002BD558 File Offset: 0x002BB758
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "fill" == name)
			{
				return new Fill();
			}
			if (26 == namespaceId && "imagedata" == name)
			{
				return new ImageData();
			}
			if (26 == namespaceId && "stroke" == name)
			{
				return new Stroke();
			}
			if (26 == namespaceId && "textbox" == name)
			{
				return new TextBox();
			}
			if (26 == namespaceId && "shadow" == name)
			{
				return new Shadow();
			}
			if (27 == namespaceId && "skew" == name)
			{
				return new Skew();
			}
			if (27 == namespaceId && "extrusion" == name)
			{
				return new Extrusion();
			}
			if (27 == namespaceId && "callout" == name)
			{
				return new Callout();
			}
			if (27 == namespaceId && "lock" == name)
			{
				return new Lock();
			}
			if (27 == namespaceId && "colormru" == name)
			{
				return new ColorMostRecentlyUsed();
			}
			if (27 == namespaceId && "colormenu" == name)
			{
				return new ColorMenu();
			}
			return null;
		}

		// Token: 0x17003816 RID: 14358
		// (get) Token: 0x0600DDC1 RID: 56769 RVA: 0x002BD66E File Offset: 0x002BB86E
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeDefaults.eleTagNames;
			}
		}

		// Token: 0x17003817 RID: 14359
		// (get) Token: 0x0600DDC2 RID: 56770 RVA: 0x002BD675 File Offset: 0x002BB875
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeDefaults.eleNamespaceIds;
			}
		}

		// Token: 0x17003818 RID: 14360
		// (get) Token: 0x0600DDC3 RID: 56771 RVA: 0x0000240C File Offset: 0x0000060C
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneAll;
			}
		}

		// Token: 0x17003819 RID: 14361
		// (get) Token: 0x0600DDC4 RID: 56772 RVA: 0x002BD67C File Offset: 0x002BB87C
		// (set) Token: 0x0600DDC5 RID: 56773 RVA: 0x002BD685 File Offset: 0x002BB885
		public Fill Fill
		{
			get
			{
				return base.GetElement<Fill>(0);
			}
			set
			{
				base.SetElement<Fill>(0, value);
			}
		}

		// Token: 0x1700381A RID: 14362
		// (get) Token: 0x0600DDC6 RID: 56774 RVA: 0x002BD68F File Offset: 0x002BB88F
		// (set) Token: 0x0600DDC7 RID: 56775 RVA: 0x002BD698 File Offset: 0x002BB898
		public ImageData ImageData
		{
			get
			{
				return base.GetElement<ImageData>(1);
			}
			set
			{
				base.SetElement<ImageData>(1, value);
			}
		}

		// Token: 0x1700381B RID: 14363
		// (get) Token: 0x0600DDC8 RID: 56776 RVA: 0x002BD6A2 File Offset: 0x002BB8A2
		// (set) Token: 0x0600DDC9 RID: 56777 RVA: 0x002BD6AB File Offset: 0x002BB8AB
		public Stroke Stroke
		{
			get
			{
				return base.GetElement<Stroke>(2);
			}
			set
			{
				base.SetElement<Stroke>(2, value);
			}
		}

		// Token: 0x1700381C RID: 14364
		// (get) Token: 0x0600DDCA RID: 56778 RVA: 0x002BD6B5 File Offset: 0x002BB8B5
		// (set) Token: 0x0600DDCB RID: 56779 RVA: 0x002BD6BE File Offset: 0x002BB8BE
		public TextBox TextBox
		{
			get
			{
				return base.GetElement<TextBox>(3);
			}
			set
			{
				base.SetElement<TextBox>(3, value);
			}
		}

		// Token: 0x1700381D RID: 14365
		// (get) Token: 0x0600DDCC RID: 56780 RVA: 0x002BD6C8 File Offset: 0x002BB8C8
		// (set) Token: 0x0600DDCD RID: 56781 RVA: 0x002BD6D1 File Offset: 0x002BB8D1
		public Shadow Shadow
		{
			get
			{
				return base.GetElement<Shadow>(4);
			}
			set
			{
				base.SetElement<Shadow>(4, value);
			}
		}

		// Token: 0x1700381E RID: 14366
		// (get) Token: 0x0600DDCE RID: 56782 RVA: 0x002BD6DB File Offset: 0x002BB8DB
		// (set) Token: 0x0600DDCF RID: 56783 RVA: 0x002BD6E4 File Offset: 0x002BB8E4
		public Skew Skew
		{
			get
			{
				return base.GetElement<Skew>(5);
			}
			set
			{
				base.SetElement<Skew>(5, value);
			}
		}

		// Token: 0x1700381F RID: 14367
		// (get) Token: 0x0600DDD0 RID: 56784 RVA: 0x002BD6EE File Offset: 0x002BB8EE
		// (set) Token: 0x0600DDD1 RID: 56785 RVA: 0x002BD6F7 File Offset: 0x002BB8F7
		public Extrusion Extrusion
		{
			get
			{
				return base.GetElement<Extrusion>(6);
			}
			set
			{
				base.SetElement<Extrusion>(6, value);
			}
		}

		// Token: 0x17003820 RID: 14368
		// (get) Token: 0x0600DDD2 RID: 56786 RVA: 0x002BD701 File Offset: 0x002BB901
		// (set) Token: 0x0600DDD3 RID: 56787 RVA: 0x002BD70A File Offset: 0x002BB90A
		public Callout Callout
		{
			get
			{
				return base.GetElement<Callout>(7);
			}
			set
			{
				base.SetElement<Callout>(7, value);
			}
		}

		// Token: 0x17003821 RID: 14369
		// (get) Token: 0x0600DDD4 RID: 56788 RVA: 0x002BD714 File Offset: 0x002BB914
		// (set) Token: 0x0600DDD5 RID: 56789 RVA: 0x002BD71D File Offset: 0x002BB91D
		public Lock Lock
		{
			get
			{
				return base.GetElement<Lock>(8);
			}
			set
			{
				base.SetElement<Lock>(8, value);
			}
		}

		// Token: 0x17003822 RID: 14370
		// (get) Token: 0x0600DDD6 RID: 56790 RVA: 0x002BD727 File Offset: 0x002BB927
		// (set) Token: 0x0600DDD7 RID: 56791 RVA: 0x002BD731 File Offset: 0x002BB931
		public ColorMostRecentlyUsed ColorMostRecentlyUsed
		{
			get
			{
				return base.GetElement<ColorMostRecentlyUsed>(9);
			}
			set
			{
				base.SetElement<ColorMostRecentlyUsed>(9, value);
			}
		}

		// Token: 0x17003823 RID: 14371
		// (get) Token: 0x0600DDD8 RID: 56792 RVA: 0x002BD73C File Offset: 0x002BB93C
		// (set) Token: 0x0600DDD9 RID: 56793 RVA: 0x002BD746 File Offset: 0x002BB946
		public ColorMenu ColorMenu
		{
			get
			{
				return base.GetElement<ColorMenu>(10);
			}
			set
			{
				base.SetElement<ColorMenu>(10, value);
			}
		}

		// Token: 0x0600DDDA RID: 56794 RVA: 0x002BD754 File Offset: 0x002BB954
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			if (namespaceId == 0 && "spidmax" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "style" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fill" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "fillcolor" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "stroke" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "strokecolor" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "allowincell" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "allowoverlap" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "insetmode" == name)
			{
				return new EnumValue<InsetMarginValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DDDB RID: 56795 RVA: 0x002BD84D File Offset: 0x002BBA4D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeDefaults>(deep);
		}

		// Token: 0x04006D4E RID: 27982
		private const string tagName = "shapedefaults";

		// Token: 0x04006D4F RID: 27983
		private const byte tagNsId = 27;

		// Token: 0x04006D50 RID: 27984
		internal const int ElementTypeIdConst = 12399;

		// Token: 0x04006D51 RID: 27985
		private static string[] attributeTagNames = new string[] { "ext", "spidmax", "style", "fill", "fillcolor", "stroke", "strokecolor", "allowincell", "allowoverlap", "insetmode" };

		// Token: 0x04006D52 RID: 27986
		private static byte[] attributeNamespaceIds = new byte[] { 26, 0, 0, 0, 0, 0, 0, 27, 27, 27 };

		// Token: 0x04006D53 RID: 27987
		private static readonly string[] eleTagNames = new string[]
		{
			"fill", "imagedata", "stroke", "textbox", "shadow", "skew", "extrusion", "callout", "lock", "colormru",
			"colormenu"
		};

		// Token: 0x04006D54 RID: 27988
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			26, 26, 26, 26, 26, 27, 27, 27, 27, 27,
			27
		};
	}
}
