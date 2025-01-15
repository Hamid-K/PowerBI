using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029FE RID: 10750
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HtmlPublishProperties), FileFormatVersions.Office2007)]
	[ChildElementInfo(typeof(WebProperties), FileFormatVersions.Office2007)]
	[ChildElementInfo(typeof(PrintingProperties))]
	[ChildElementInfo(typeof(ShowProperties))]
	[ChildElementInfo(typeof(ColorMostRecentlyUsed))]
	[ChildElementInfo(typeof(PresentationPropertiesExtensionList))]
	internal class PresentationProperties : OpenXmlPartRootElement
	{
		// Token: 0x17006EF2 RID: 28402
		// (get) Token: 0x060156F6 RID: 87798 RVA: 0x0031F17F File Offset: 0x0031D37F
		public override string LocalName
		{
			get
			{
				return "presentationPr";
			}
		}

		// Token: 0x17006EF3 RID: 28403
		// (get) Token: 0x060156F7 RID: 87799 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006EF4 RID: 28404
		// (get) Token: 0x060156F8 RID: 87800 RVA: 0x0031F186 File Offset: 0x0031D386
		internal override int ElementTypeId
		{
			get
			{
				return 12177;
			}
		}

		// Token: 0x060156F9 RID: 87801 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060156FA RID: 87802 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal PresentationProperties(PresentationPropertiesPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x060156FB RID: 87803 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(PresentationPropertiesPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006EF5 RID: 28405
		// (get) Token: 0x060156FC RID: 87804 RVA: 0x0031F18D File Offset: 0x0031D38D
		// (set) Token: 0x060156FD RID: 87805 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public PresentationPropertiesPart PresentationPropertiesPart
		{
			get
			{
				return base.OpenXmlPart as PresentationPropertiesPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x060156FE RID: 87806 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public PresentationProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060156FF RID: 87807 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public PresentationProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015700 RID: 87808 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public PresentationProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015701 RID: 87809 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public PresentationProperties()
		{
		}

		// Token: 0x06015702 RID: 87810 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(PresentationPropertiesPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06015703 RID: 87811 RVA: 0x0031F19C File Offset: 0x0031D39C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "htmlPubPr" == name)
			{
				return new HtmlPublishProperties();
			}
			if (24 == namespaceId && "webPr" == name)
			{
				return new WebProperties();
			}
			if (24 == namespaceId && "prnPr" == name)
			{
				return new PrintingProperties();
			}
			if (24 == namespaceId && "showPr" == name)
			{
				return new ShowProperties();
			}
			if (24 == namespaceId && "clrMru" == name)
			{
				return new ColorMostRecentlyUsed();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new PresentationPropertiesExtensionList();
			}
			return null;
		}

		// Token: 0x17006EF6 RID: 28406
		// (get) Token: 0x06015704 RID: 87812 RVA: 0x0031F23A File Offset: 0x0031D43A
		internal override string[] ElementTagNames
		{
			get
			{
				return PresentationProperties.eleTagNames;
			}
		}

		// Token: 0x17006EF7 RID: 28407
		// (get) Token: 0x06015705 RID: 87813 RVA: 0x0031F241 File Offset: 0x0031D441
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PresentationProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006EF8 RID: 28408
		// (get) Token: 0x06015706 RID: 87814 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006EF9 RID: 28409
		// (get) Token: 0x06015707 RID: 87815 RVA: 0x0031F248 File Offset: 0x0031D448
		// (set) Token: 0x06015708 RID: 87816 RVA: 0x0031F251 File Offset: 0x0031D451
		[OfficeAvailability(FileFormatVersions.Office2007)]
		public HtmlPublishProperties HtmlPublishProperties
		{
			get
			{
				return base.GetElement<HtmlPublishProperties>(0);
			}
			set
			{
				base.SetElement<HtmlPublishProperties>(0, value);
			}
		}

		// Token: 0x17006EFA RID: 28410
		// (get) Token: 0x06015709 RID: 87817 RVA: 0x0031F25B File Offset: 0x0031D45B
		// (set) Token: 0x0601570A RID: 87818 RVA: 0x0031F264 File Offset: 0x0031D464
		[OfficeAvailability(FileFormatVersions.Office2007)]
		public WebProperties WebProperties
		{
			get
			{
				return base.GetElement<WebProperties>(1);
			}
			set
			{
				base.SetElement<WebProperties>(1, value);
			}
		}

		// Token: 0x17006EFB RID: 28411
		// (get) Token: 0x0601570B RID: 87819 RVA: 0x0031F26E File Offset: 0x0031D46E
		// (set) Token: 0x0601570C RID: 87820 RVA: 0x0031F277 File Offset: 0x0031D477
		public PrintingProperties PrintingProperties
		{
			get
			{
				return base.GetElement<PrintingProperties>(2);
			}
			set
			{
				base.SetElement<PrintingProperties>(2, value);
			}
		}

		// Token: 0x17006EFC RID: 28412
		// (get) Token: 0x0601570D RID: 87821 RVA: 0x0031F281 File Offset: 0x0031D481
		// (set) Token: 0x0601570E RID: 87822 RVA: 0x0031F28A File Offset: 0x0031D48A
		public ShowProperties ShowProperties
		{
			get
			{
				return base.GetElement<ShowProperties>(3);
			}
			set
			{
				base.SetElement<ShowProperties>(3, value);
			}
		}

		// Token: 0x17006EFD RID: 28413
		// (get) Token: 0x0601570F RID: 87823 RVA: 0x0031F294 File Offset: 0x0031D494
		// (set) Token: 0x06015710 RID: 87824 RVA: 0x0031F29D File Offset: 0x0031D49D
		public ColorMostRecentlyUsed ColorMostRecentlyUsed
		{
			get
			{
				return base.GetElement<ColorMostRecentlyUsed>(4);
			}
			set
			{
				base.SetElement<ColorMostRecentlyUsed>(4, value);
			}
		}

		// Token: 0x17006EFE RID: 28414
		// (get) Token: 0x06015711 RID: 87825 RVA: 0x0031F2A7 File Offset: 0x0031D4A7
		// (set) Token: 0x06015712 RID: 87826 RVA: 0x0031F2B0 File Offset: 0x0031D4B0
		public PresentationPropertiesExtensionList PresentationPropertiesExtensionList
		{
			get
			{
				return base.GetElement<PresentationPropertiesExtensionList>(5);
			}
			set
			{
				base.SetElement<PresentationPropertiesExtensionList>(5, value);
			}
		}

		// Token: 0x06015713 RID: 87827 RVA: 0x0031F2BA File Offset: 0x0031D4BA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PresentationProperties>(deep);
		}

		// Token: 0x0400936D RID: 37741
		private const string tagName = "presentationPr";

		// Token: 0x0400936E RID: 37742
		private const byte tagNsId = 24;

		// Token: 0x0400936F RID: 37743
		internal const int ElementTypeIdConst = 12177;

		// Token: 0x04009370 RID: 37744
		private static readonly string[] eleTagNames = new string[] { "htmlPubPr", "webPr", "prnPr", "showPr", "clrMru", "extLst" };

		// Token: 0x04009371 RID: 37745
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24, 24, 24, 24 };
	}
}
