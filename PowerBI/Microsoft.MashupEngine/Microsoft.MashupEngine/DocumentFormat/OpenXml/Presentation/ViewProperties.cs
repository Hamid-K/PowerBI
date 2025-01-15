using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A07 RID: 10759
	[ChildElementInfo(typeof(SorterViewProperties))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GridSpacing))]
	[ChildElementInfo(typeof(NormalViewProperties))]
	[ChildElementInfo(typeof(SlideViewProperties))]
	[ChildElementInfo(typeof(OutlineViewProperties))]
	[ChildElementInfo(typeof(NotesTextViewProperties))]
	[ChildElementInfo(typeof(NotesViewProperties))]
	internal class ViewProperties : OpenXmlPartRootElement
	{
		// Token: 0x17006F6D RID: 28525
		// (get) Token: 0x06015816 RID: 88086 RVA: 0x0031FEE5 File Offset: 0x0031E0E5
		public override string LocalName
		{
			get
			{
				return "viewPr";
			}
		}

		// Token: 0x17006F6E RID: 28526
		// (get) Token: 0x06015817 RID: 88087 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006F6F RID: 28527
		// (get) Token: 0x06015818 RID: 88088 RVA: 0x0031FEEC File Offset: 0x0031E0EC
		internal override int ElementTypeId
		{
			get
			{
				return 12186;
			}
		}

		// Token: 0x06015819 RID: 88089 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006F70 RID: 28528
		// (get) Token: 0x0601581A RID: 88090 RVA: 0x0031FEF3 File Offset: 0x0031E0F3
		internal override string[] AttributeTagNames
		{
			get
			{
				return ViewProperties.attributeTagNames;
			}
		}

		// Token: 0x17006F71 RID: 28529
		// (get) Token: 0x0601581B RID: 88091 RVA: 0x0031FEFA File Offset: 0x0031E0FA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ViewProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17006F72 RID: 28530
		// (get) Token: 0x0601581C RID: 88092 RVA: 0x0031FF01 File Offset: 0x0031E101
		// (set) Token: 0x0601581D RID: 88093 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "lastView")]
		public EnumValue<ViewValues> LastView
		{
			get
			{
				return (EnumValue<ViewValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006F73 RID: 28531
		// (get) Token: 0x0601581E RID: 88094 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601581F RID: 88095 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "showComments")]
		public BooleanValue ShowComments
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

		// Token: 0x06015820 RID: 88096 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal ViewProperties(ViewPropertiesPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06015821 RID: 88097 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(ViewPropertiesPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006F74 RID: 28532
		// (get) Token: 0x06015822 RID: 88098 RVA: 0x0031FF10 File Offset: 0x0031E110
		// (set) Token: 0x06015823 RID: 88099 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public ViewPropertiesPart ViewPropertiesPart
		{
			get
			{
				return base.OpenXmlPart as ViewPropertiesPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06015824 RID: 88100 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public ViewProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015825 RID: 88101 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public ViewProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015826 RID: 88102 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public ViewProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015827 RID: 88103 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public ViewProperties()
		{
		}

		// Token: 0x06015828 RID: 88104 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(ViewPropertiesPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06015829 RID: 88105 RVA: 0x0031FF20 File Offset: 0x0031E120
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "normalViewPr" == name)
			{
				return new NormalViewProperties();
			}
			if (24 == namespaceId && "slideViewPr" == name)
			{
				return new SlideViewProperties();
			}
			if (24 == namespaceId && "outlineViewPr" == name)
			{
				return new OutlineViewProperties();
			}
			if (24 == namespaceId && "notesTextViewPr" == name)
			{
				return new NotesTextViewProperties();
			}
			if (24 == namespaceId && "sorterViewPr" == name)
			{
				return new SorterViewProperties();
			}
			if (24 == namespaceId && "notesViewPr" == name)
			{
				return new NotesViewProperties();
			}
			if (24 == namespaceId && "gridSpacing" == name)
			{
				return new GridSpacing();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006F75 RID: 28533
		// (get) Token: 0x0601582A RID: 88106 RVA: 0x0031FFEE File Offset: 0x0031E1EE
		internal override string[] ElementTagNames
		{
			get
			{
				return ViewProperties.eleTagNames;
			}
		}

		// Token: 0x17006F76 RID: 28534
		// (get) Token: 0x0601582B RID: 88107 RVA: 0x0031FFF5 File Offset: 0x0031E1F5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ViewProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006F77 RID: 28535
		// (get) Token: 0x0601582C RID: 88108 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006F78 RID: 28536
		// (get) Token: 0x0601582D RID: 88109 RVA: 0x0031FFFC File Offset: 0x0031E1FC
		// (set) Token: 0x0601582E RID: 88110 RVA: 0x00320005 File Offset: 0x0031E205
		public NormalViewProperties NormalViewProperties
		{
			get
			{
				return base.GetElement<NormalViewProperties>(0);
			}
			set
			{
				base.SetElement<NormalViewProperties>(0, value);
			}
		}

		// Token: 0x17006F79 RID: 28537
		// (get) Token: 0x0601582F RID: 88111 RVA: 0x0032000F File Offset: 0x0031E20F
		// (set) Token: 0x06015830 RID: 88112 RVA: 0x00320018 File Offset: 0x0031E218
		public SlideViewProperties SlideViewProperties
		{
			get
			{
				return base.GetElement<SlideViewProperties>(1);
			}
			set
			{
				base.SetElement<SlideViewProperties>(1, value);
			}
		}

		// Token: 0x17006F7A RID: 28538
		// (get) Token: 0x06015831 RID: 88113 RVA: 0x00320022 File Offset: 0x0031E222
		// (set) Token: 0x06015832 RID: 88114 RVA: 0x0032002B File Offset: 0x0031E22B
		public OutlineViewProperties OutlineViewProperties
		{
			get
			{
				return base.GetElement<OutlineViewProperties>(2);
			}
			set
			{
				base.SetElement<OutlineViewProperties>(2, value);
			}
		}

		// Token: 0x17006F7B RID: 28539
		// (get) Token: 0x06015833 RID: 88115 RVA: 0x00320035 File Offset: 0x0031E235
		// (set) Token: 0x06015834 RID: 88116 RVA: 0x0032003E File Offset: 0x0031E23E
		public NotesTextViewProperties NotesTextViewProperties
		{
			get
			{
				return base.GetElement<NotesTextViewProperties>(3);
			}
			set
			{
				base.SetElement<NotesTextViewProperties>(3, value);
			}
		}

		// Token: 0x17006F7C RID: 28540
		// (get) Token: 0x06015835 RID: 88117 RVA: 0x00320048 File Offset: 0x0031E248
		// (set) Token: 0x06015836 RID: 88118 RVA: 0x00320051 File Offset: 0x0031E251
		public SorterViewProperties SorterViewProperties
		{
			get
			{
				return base.GetElement<SorterViewProperties>(4);
			}
			set
			{
				base.SetElement<SorterViewProperties>(4, value);
			}
		}

		// Token: 0x17006F7D RID: 28541
		// (get) Token: 0x06015837 RID: 88119 RVA: 0x0032005B File Offset: 0x0031E25B
		// (set) Token: 0x06015838 RID: 88120 RVA: 0x00320064 File Offset: 0x0031E264
		public NotesViewProperties NotesViewProperties
		{
			get
			{
				return base.GetElement<NotesViewProperties>(5);
			}
			set
			{
				base.SetElement<NotesViewProperties>(5, value);
			}
		}

		// Token: 0x17006F7E RID: 28542
		// (get) Token: 0x06015839 RID: 88121 RVA: 0x0032006E File Offset: 0x0031E26E
		// (set) Token: 0x0601583A RID: 88122 RVA: 0x00320077 File Offset: 0x0031E277
		public GridSpacing GridSpacing
		{
			get
			{
				return base.GetElement<GridSpacing>(6);
			}
			set
			{
				base.SetElement<GridSpacing>(6, value);
			}
		}

		// Token: 0x17006F7F RID: 28543
		// (get) Token: 0x0601583B RID: 88123 RVA: 0x00320081 File Offset: 0x0031E281
		// (set) Token: 0x0601583C RID: 88124 RVA: 0x0032008A File Offset: 0x0031E28A
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(7);
			}
			set
			{
				base.SetElement<ExtensionList>(7, value);
			}
		}

		// Token: 0x0601583D RID: 88125 RVA: 0x00320094 File Offset: 0x0031E294
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "lastView" == name)
			{
				return new EnumValue<ViewValues>();
			}
			if (namespaceId == 0 && "showComments" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601583E RID: 88126 RVA: 0x003200CA File Offset: 0x0031E2CA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ViewProperties>(deep);
		}

		// Token: 0x0601583F RID: 88127 RVA: 0x003200D4 File Offset: 0x0031E2D4
		// Note: this type is marked as 'beforefieldinit'.
		static ViewProperties()
		{
			byte[] array = new byte[2];
			ViewProperties.attributeNamespaceIds = array;
			ViewProperties.eleTagNames = new string[] { "normalViewPr", "slideViewPr", "outlineViewPr", "notesTextViewPr", "sorterViewPr", "notesViewPr", "gridSpacing", "extLst" };
			ViewProperties.eleNamespaceIds = new byte[] { 24, 24, 24, 24, 24, 24, 24, 24 };
		}

		// Token: 0x040093A2 RID: 37794
		private const string tagName = "viewPr";

		// Token: 0x040093A3 RID: 37795
		private const byte tagNsId = 24;

		// Token: 0x040093A4 RID: 37796
		internal const int ElementTypeIdConst = 12186;

		// Token: 0x040093A5 RID: 37797
		private static string[] attributeTagNames = new string[] { "lastView", "showComments" };

		// Token: 0x040093A6 RID: 37798
		private static byte[] attributeNamespaceIds;

		// Token: 0x040093A7 RID: 37799
		private static readonly string[] eleTagNames;

		// Token: 0x040093A8 RID: 37800
		private static readonly byte[] eleNamespaceIds;
	}
}
