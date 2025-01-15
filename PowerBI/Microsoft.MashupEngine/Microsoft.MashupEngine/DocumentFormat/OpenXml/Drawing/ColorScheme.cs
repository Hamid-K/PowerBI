using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002778 RID: 10104
	[ChildElementInfo(typeof(Accent4Color))]
	[ChildElementInfo(typeof(FollowedHyperlinkColor))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Hyperlink))]
	[ChildElementInfo(typeof(Light2Color))]
	[ChildElementInfo(typeof(Dark1Color))]
	[ChildElementInfo(typeof(Light1Color))]
	[ChildElementInfo(typeof(Dark2Color))]
	[ChildElementInfo(typeof(Accent2Color))]
	[ChildElementInfo(typeof(Accent1Color))]
	[ChildElementInfo(typeof(Accent3Color))]
	[ChildElementInfo(typeof(Accent5Color))]
	[ChildElementInfo(typeof(Accent6Color))]
	internal class ColorScheme : OpenXmlCompositeElement
	{
		// Token: 0x17006173 RID: 24947
		// (get) Token: 0x060137F2 RID: 79858 RVA: 0x00307B53 File Offset: 0x00305D53
		public override string LocalName
		{
			get
			{
				return "clrScheme";
			}
		}

		// Token: 0x17006174 RID: 24948
		// (get) Token: 0x060137F3 RID: 79859 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006175 RID: 24949
		// (get) Token: 0x060137F4 RID: 79860 RVA: 0x00307B5A File Offset: 0x00305D5A
		internal override int ElementTypeId
		{
			get
			{
				return 10144;
			}
		}

		// Token: 0x060137F5 RID: 79861 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006176 RID: 24950
		// (get) Token: 0x060137F6 RID: 79862 RVA: 0x00307B61 File Offset: 0x00305D61
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorScheme.attributeTagNames;
			}
		}

		// Token: 0x17006177 RID: 24951
		// (get) Token: 0x060137F7 RID: 79863 RVA: 0x00307B68 File Offset: 0x00305D68
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorScheme.attributeNamespaceIds;
			}
		}

		// Token: 0x17006178 RID: 24952
		// (get) Token: 0x060137F8 RID: 79864 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060137F9 RID: 79865 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x060137FA RID: 79866 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColorScheme()
		{
		}

		// Token: 0x060137FB RID: 79867 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColorScheme(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060137FC RID: 79868 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColorScheme(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060137FD RID: 79869 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColorScheme(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060137FE RID: 79870 RVA: 0x00307B70 File Offset: 0x00305D70
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "dk1" == name)
			{
				return new Dark1Color();
			}
			if (10 == namespaceId && "lt1" == name)
			{
				return new Light1Color();
			}
			if (10 == namespaceId && "dk2" == name)
			{
				return new Dark2Color();
			}
			if (10 == namespaceId && "lt2" == name)
			{
				return new Light2Color();
			}
			if (10 == namespaceId && "accent1" == name)
			{
				return new Accent1Color();
			}
			if (10 == namespaceId && "accent2" == name)
			{
				return new Accent2Color();
			}
			if (10 == namespaceId && "accent3" == name)
			{
				return new Accent3Color();
			}
			if (10 == namespaceId && "accent4" == name)
			{
				return new Accent4Color();
			}
			if (10 == namespaceId && "accent5" == name)
			{
				return new Accent5Color();
			}
			if (10 == namespaceId && "accent6" == name)
			{
				return new Accent6Color();
			}
			if (10 == namespaceId && "hlink" == name)
			{
				return new Hyperlink();
			}
			if (10 == namespaceId && "folHlink" == name)
			{
				return new FollowedHyperlinkColor();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006179 RID: 24953
		// (get) Token: 0x060137FF RID: 79871 RVA: 0x00307CB6 File Offset: 0x00305EB6
		internal override string[] ElementTagNames
		{
			get
			{
				return ColorScheme.eleTagNames;
			}
		}

		// Token: 0x1700617A RID: 24954
		// (get) Token: 0x06013800 RID: 79872 RVA: 0x00307CBD File Offset: 0x00305EBD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ColorScheme.eleNamespaceIds;
			}
		}

		// Token: 0x1700617B RID: 24955
		// (get) Token: 0x06013801 RID: 79873 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700617C RID: 24956
		// (get) Token: 0x06013802 RID: 79874 RVA: 0x00307CC4 File Offset: 0x00305EC4
		// (set) Token: 0x06013803 RID: 79875 RVA: 0x00307CCD File Offset: 0x00305ECD
		public Dark1Color Dark1Color
		{
			get
			{
				return base.GetElement<Dark1Color>(0);
			}
			set
			{
				base.SetElement<Dark1Color>(0, value);
			}
		}

		// Token: 0x1700617D RID: 24957
		// (get) Token: 0x06013804 RID: 79876 RVA: 0x00307CD7 File Offset: 0x00305ED7
		// (set) Token: 0x06013805 RID: 79877 RVA: 0x00307CE0 File Offset: 0x00305EE0
		public Light1Color Light1Color
		{
			get
			{
				return base.GetElement<Light1Color>(1);
			}
			set
			{
				base.SetElement<Light1Color>(1, value);
			}
		}

		// Token: 0x1700617E RID: 24958
		// (get) Token: 0x06013806 RID: 79878 RVA: 0x00307CEA File Offset: 0x00305EEA
		// (set) Token: 0x06013807 RID: 79879 RVA: 0x00307CF3 File Offset: 0x00305EF3
		public Dark2Color Dark2Color
		{
			get
			{
				return base.GetElement<Dark2Color>(2);
			}
			set
			{
				base.SetElement<Dark2Color>(2, value);
			}
		}

		// Token: 0x1700617F RID: 24959
		// (get) Token: 0x06013808 RID: 79880 RVA: 0x00307CFD File Offset: 0x00305EFD
		// (set) Token: 0x06013809 RID: 79881 RVA: 0x00307D06 File Offset: 0x00305F06
		public Light2Color Light2Color
		{
			get
			{
				return base.GetElement<Light2Color>(3);
			}
			set
			{
				base.SetElement<Light2Color>(3, value);
			}
		}

		// Token: 0x17006180 RID: 24960
		// (get) Token: 0x0601380A RID: 79882 RVA: 0x00307D10 File Offset: 0x00305F10
		// (set) Token: 0x0601380B RID: 79883 RVA: 0x00307D19 File Offset: 0x00305F19
		public Accent1Color Accent1Color
		{
			get
			{
				return base.GetElement<Accent1Color>(4);
			}
			set
			{
				base.SetElement<Accent1Color>(4, value);
			}
		}

		// Token: 0x17006181 RID: 24961
		// (get) Token: 0x0601380C RID: 79884 RVA: 0x00307D23 File Offset: 0x00305F23
		// (set) Token: 0x0601380D RID: 79885 RVA: 0x00307D2C File Offset: 0x00305F2C
		public Accent2Color Accent2Color
		{
			get
			{
				return base.GetElement<Accent2Color>(5);
			}
			set
			{
				base.SetElement<Accent2Color>(5, value);
			}
		}

		// Token: 0x17006182 RID: 24962
		// (get) Token: 0x0601380E RID: 79886 RVA: 0x00307D36 File Offset: 0x00305F36
		// (set) Token: 0x0601380F RID: 79887 RVA: 0x00307D3F File Offset: 0x00305F3F
		public Accent3Color Accent3Color
		{
			get
			{
				return base.GetElement<Accent3Color>(6);
			}
			set
			{
				base.SetElement<Accent3Color>(6, value);
			}
		}

		// Token: 0x17006183 RID: 24963
		// (get) Token: 0x06013810 RID: 79888 RVA: 0x00307D49 File Offset: 0x00305F49
		// (set) Token: 0x06013811 RID: 79889 RVA: 0x00307D52 File Offset: 0x00305F52
		public Accent4Color Accent4Color
		{
			get
			{
				return base.GetElement<Accent4Color>(7);
			}
			set
			{
				base.SetElement<Accent4Color>(7, value);
			}
		}

		// Token: 0x17006184 RID: 24964
		// (get) Token: 0x06013812 RID: 79890 RVA: 0x00307D5C File Offset: 0x00305F5C
		// (set) Token: 0x06013813 RID: 79891 RVA: 0x00307D65 File Offset: 0x00305F65
		public Accent5Color Accent5Color
		{
			get
			{
				return base.GetElement<Accent5Color>(8);
			}
			set
			{
				base.SetElement<Accent5Color>(8, value);
			}
		}

		// Token: 0x17006185 RID: 24965
		// (get) Token: 0x06013814 RID: 79892 RVA: 0x00307D6F File Offset: 0x00305F6F
		// (set) Token: 0x06013815 RID: 79893 RVA: 0x00307D79 File Offset: 0x00305F79
		public Accent6Color Accent6Color
		{
			get
			{
				return base.GetElement<Accent6Color>(9);
			}
			set
			{
				base.SetElement<Accent6Color>(9, value);
			}
		}

		// Token: 0x17006186 RID: 24966
		// (get) Token: 0x06013816 RID: 79894 RVA: 0x00307D84 File Offset: 0x00305F84
		// (set) Token: 0x06013817 RID: 79895 RVA: 0x00307D8E File Offset: 0x00305F8E
		public Hyperlink Hyperlink
		{
			get
			{
				return base.GetElement<Hyperlink>(10);
			}
			set
			{
				base.SetElement<Hyperlink>(10, value);
			}
		}

		// Token: 0x17006187 RID: 24967
		// (get) Token: 0x06013818 RID: 79896 RVA: 0x00307D99 File Offset: 0x00305F99
		// (set) Token: 0x06013819 RID: 79897 RVA: 0x00307DA3 File Offset: 0x00305FA3
		public FollowedHyperlinkColor FollowedHyperlinkColor
		{
			get
			{
				return base.GetElement<FollowedHyperlinkColor>(11);
			}
			set
			{
				base.SetElement<FollowedHyperlinkColor>(11, value);
			}
		}

		// Token: 0x17006188 RID: 24968
		// (get) Token: 0x0601381A RID: 79898 RVA: 0x00307DAE File Offset: 0x00305FAE
		// (set) Token: 0x0601381B RID: 79899 RVA: 0x00307DB8 File Offset: 0x00305FB8
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(12);
			}
			set
			{
				base.SetElement<ExtensionList>(12, value);
			}
		}

		// Token: 0x0601381C RID: 79900 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601381D RID: 79901 RVA: 0x00307DC3 File Offset: 0x00305FC3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorScheme>(deep);
		}

		// Token: 0x0601381E RID: 79902 RVA: 0x00307DCC File Offset: 0x00305FCC
		// Note: this type is marked as 'beforefieldinit'.
		static ColorScheme()
		{
			byte[] array = new byte[1];
			ColorScheme.attributeNamespaceIds = array;
			ColorScheme.eleTagNames = new string[]
			{
				"dk1", "lt1", "dk2", "lt2", "accent1", "accent2", "accent3", "accent4", "accent5", "accent6",
				"hlink", "folHlink", "extLst"
			};
			ColorScheme.eleNamespaceIds = new byte[]
			{
				10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
				10, 10, 10
			};
		}

		// Token: 0x0400867A RID: 34426
		private const string tagName = "clrScheme";

		// Token: 0x0400867B RID: 34427
		private const byte tagNsId = 10;

		// Token: 0x0400867C RID: 34428
		internal const int ElementTypeIdConst = 10144;

		// Token: 0x0400867D RID: 34429
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x0400867E RID: 34430
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400867F RID: 34431
		private static readonly string[] eleTagNames;

		// Token: 0x04008680 RID: 34432
		private static readonly byte[] eleNamespaceIds;
	}
}
