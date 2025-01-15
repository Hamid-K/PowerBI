using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F19 RID: 12057
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Frameset))]
	[ChildElementInfo(typeof(Divs))]
	[ChildElementInfo(typeof(WebPageEncoding))]
	[ChildElementInfo(typeof(OptimizeForBrowser))]
	[ChildElementInfo(typeof(RelyOnVML))]
	[ChildElementInfo(typeof(AllowPNG))]
	[ChildElementInfo(typeof(DoNotRelyOnCSS))]
	[ChildElementInfo(typeof(DoNotSaveAsSingleFile))]
	[ChildElementInfo(typeof(DoNotOrganizeInFolder))]
	[ChildElementInfo(typeof(DoNotUseLongFileNames))]
	[ChildElementInfo(typeof(PixelsPerInch))]
	[ChildElementInfo(typeof(TargetScreenSize))]
	[ChildElementInfo(typeof(SaveSmartTagAsXml))]
	internal class WebSettings : OpenXmlPartRootElement
	{
		// Token: 0x17008E70 RID: 36464
		// (get) Token: 0x06019C70 RID: 105584 RVA: 0x002A6C0B File Offset: 0x002A4E0B
		public override string LocalName
		{
			get
			{
				return "webSettings";
			}
		}

		// Token: 0x17008E71 RID: 36465
		// (get) Token: 0x06019C71 RID: 105585 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E72 RID: 36466
		// (get) Token: 0x06019C72 RID: 105586 RVA: 0x003565B4 File Offset: 0x003547B4
		internal override int ElementTypeId
		{
			get
			{
				return 11698;
			}
		}

		// Token: 0x06019C73 RID: 105587 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019C74 RID: 105588 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal WebSettings(WebSettingsPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06019C75 RID: 105589 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(WebSettingsPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17008E73 RID: 36467
		// (get) Token: 0x06019C76 RID: 105590 RVA: 0x003565BB File Offset: 0x003547BB
		// (set) Token: 0x06019C77 RID: 105591 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public WebSettingsPart WebSettingsPart
		{
			get
			{
				return base.OpenXmlPart as WebSettingsPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06019C78 RID: 105592 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public WebSettings(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019C79 RID: 105593 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public WebSettings(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019C7A RID: 105594 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public WebSettings(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019C7B RID: 105595 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public WebSettings()
		{
		}

		// Token: 0x06019C7C RID: 105596 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(WebSettingsPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06019C7D RID: 105597 RVA: 0x003565C8 File Offset: 0x003547C8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "frameset" == name)
			{
				return new Frameset();
			}
			if (23 == namespaceId && "divs" == name)
			{
				return new Divs();
			}
			if (23 == namespaceId && "encoding" == name)
			{
				return new WebPageEncoding();
			}
			if (23 == namespaceId && "optimizeForBrowser" == name)
			{
				return new OptimizeForBrowser();
			}
			if (23 == namespaceId && "relyOnVML" == name)
			{
				return new RelyOnVML();
			}
			if (23 == namespaceId && "allowPNG" == name)
			{
				return new AllowPNG();
			}
			if (23 == namespaceId && "doNotRelyOnCSS" == name)
			{
				return new DoNotRelyOnCSS();
			}
			if (23 == namespaceId && "doNotSaveAsSingleFile" == name)
			{
				return new DoNotSaveAsSingleFile();
			}
			if (23 == namespaceId && "doNotOrganizeInFolder" == name)
			{
				return new DoNotOrganizeInFolder();
			}
			if (23 == namespaceId && "doNotUseLongFileNames" == name)
			{
				return new DoNotUseLongFileNames();
			}
			if (23 == namespaceId && "pixelsPerInch" == name)
			{
				return new PixelsPerInch();
			}
			if (23 == namespaceId && "targetScreenSz" == name)
			{
				return new TargetScreenSize();
			}
			if (23 == namespaceId && "saveSmartTagsAsXml" == name)
			{
				return new SaveSmartTagAsXml();
			}
			return null;
		}

		// Token: 0x17008E74 RID: 36468
		// (get) Token: 0x06019C7E RID: 105598 RVA: 0x0035670E File Offset: 0x0035490E
		internal override string[] ElementTagNames
		{
			get
			{
				return WebSettings.eleTagNames;
			}
		}

		// Token: 0x17008E75 RID: 36469
		// (get) Token: 0x06019C7F RID: 105599 RVA: 0x00356715 File Offset: 0x00354915
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return WebSettings.eleNamespaceIds;
			}
		}

		// Token: 0x17008E76 RID: 36470
		// (get) Token: 0x06019C80 RID: 105600 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008E77 RID: 36471
		// (get) Token: 0x06019C81 RID: 105601 RVA: 0x0035671C File Offset: 0x0035491C
		// (set) Token: 0x06019C82 RID: 105602 RVA: 0x00356725 File Offset: 0x00354925
		public Frameset Frameset
		{
			get
			{
				return base.GetElement<Frameset>(0);
			}
			set
			{
				base.SetElement<Frameset>(0, value);
			}
		}

		// Token: 0x17008E78 RID: 36472
		// (get) Token: 0x06019C83 RID: 105603 RVA: 0x0035672F File Offset: 0x0035492F
		// (set) Token: 0x06019C84 RID: 105604 RVA: 0x00356738 File Offset: 0x00354938
		public Divs Divs
		{
			get
			{
				return base.GetElement<Divs>(1);
			}
			set
			{
				base.SetElement<Divs>(1, value);
			}
		}

		// Token: 0x17008E79 RID: 36473
		// (get) Token: 0x06019C85 RID: 105605 RVA: 0x00356742 File Offset: 0x00354942
		// (set) Token: 0x06019C86 RID: 105606 RVA: 0x0035674B File Offset: 0x0035494B
		public WebPageEncoding WebPageEncoding
		{
			get
			{
				return base.GetElement<WebPageEncoding>(2);
			}
			set
			{
				base.SetElement<WebPageEncoding>(2, value);
			}
		}

		// Token: 0x17008E7A RID: 36474
		// (get) Token: 0x06019C87 RID: 105607 RVA: 0x00356755 File Offset: 0x00354955
		// (set) Token: 0x06019C88 RID: 105608 RVA: 0x0035675E File Offset: 0x0035495E
		public OptimizeForBrowser OptimizeForBrowser
		{
			get
			{
				return base.GetElement<OptimizeForBrowser>(3);
			}
			set
			{
				base.SetElement<OptimizeForBrowser>(3, value);
			}
		}

		// Token: 0x17008E7B RID: 36475
		// (get) Token: 0x06019C89 RID: 105609 RVA: 0x00356768 File Offset: 0x00354968
		// (set) Token: 0x06019C8A RID: 105610 RVA: 0x00356771 File Offset: 0x00354971
		public RelyOnVML RelyOnVML
		{
			get
			{
				return base.GetElement<RelyOnVML>(4);
			}
			set
			{
				base.SetElement<RelyOnVML>(4, value);
			}
		}

		// Token: 0x17008E7C RID: 36476
		// (get) Token: 0x06019C8B RID: 105611 RVA: 0x0035677B File Offset: 0x0035497B
		// (set) Token: 0x06019C8C RID: 105612 RVA: 0x00356784 File Offset: 0x00354984
		public AllowPNG AllowPNG
		{
			get
			{
				return base.GetElement<AllowPNG>(5);
			}
			set
			{
				base.SetElement<AllowPNG>(5, value);
			}
		}

		// Token: 0x17008E7D RID: 36477
		// (get) Token: 0x06019C8D RID: 105613 RVA: 0x0035678E File Offset: 0x0035498E
		// (set) Token: 0x06019C8E RID: 105614 RVA: 0x00356797 File Offset: 0x00354997
		public DoNotRelyOnCSS DoNotRelyOnCSS
		{
			get
			{
				return base.GetElement<DoNotRelyOnCSS>(6);
			}
			set
			{
				base.SetElement<DoNotRelyOnCSS>(6, value);
			}
		}

		// Token: 0x17008E7E RID: 36478
		// (get) Token: 0x06019C8F RID: 105615 RVA: 0x003567A1 File Offset: 0x003549A1
		// (set) Token: 0x06019C90 RID: 105616 RVA: 0x003567AA File Offset: 0x003549AA
		public DoNotSaveAsSingleFile DoNotSaveAsSingleFile
		{
			get
			{
				return base.GetElement<DoNotSaveAsSingleFile>(7);
			}
			set
			{
				base.SetElement<DoNotSaveAsSingleFile>(7, value);
			}
		}

		// Token: 0x17008E7F RID: 36479
		// (get) Token: 0x06019C91 RID: 105617 RVA: 0x003567B4 File Offset: 0x003549B4
		// (set) Token: 0x06019C92 RID: 105618 RVA: 0x003567BD File Offset: 0x003549BD
		public DoNotOrganizeInFolder DoNotOrganizeInFolder
		{
			get
			{
				return base.GetElement<DoNotOrganizeInFolder>(8);
			}
			set
			{
				base.SetElement<DoNotOrganizeInFolder>(8, value);
			}
		}

		// Token: 0x17008E80 RID: 36480
		// (get) Token: 0x06019C93 RID: 105619 RVA: 0x003567C7 File Offset: 0x003549C7
		// (set) Token: 0x06019C94 RID: 105620 RVA: 0x003567D1 File Offset: 0x003549D1
		public DoNotUseLongFileNames DoNotUseLongFileNames
		{
			get
			{
				return base.GetElement<DoNotUseLongFileNames>(9);
			}
			set
			{
				base.SetElement<DoNotUseLongFileNames>(9, value);
			}
		}

		// Token: 0x17008E81 RID: 36481
		// (get) Token: 0x06019C95 RID: 105621 RVA: 0x003567DC File Offset: 0x003549DC
		// (set) Token: 0x06019C96 RID: 105622 RVA: 0x003567E6 File Offset: 0x003549E6
		public PixelsPerInch PixelsPerInch
		{
			get
			{
				return base.GetElement<PixelsPerInch>(10);
			}
			set
			{
				base.SetElement<PixelsPerInch>(10, value);
			}
		}

		// Token: 0x17008E82 RID: 36482
		// (get) Token: 0x06019C97 RID: 105623 RVA: 0x003567F1 File Offset: 0x003549F1
		// (set) Token: 0x06019C98 RID: 105624 RVA: 0x003567FB File Offset: 0x003549FB
		public TargetScreenSize TargetScreenSize
		{
			get
			{
				return base.GetElement<TargetScreenSize>(11);
			}
			set
			{
				base.SetElement<TargetScreenSize>(11, value);
			}
		}

		// Token: 0x17008E83 RID: 36483
		// (get) Token: 0x06019C99 RID: 105625 RVA: 0x00356806 File Offset: 0x00354A06
		// (set) Token: 0x06019C9A RID: 105626 RVA: 0x00356810 File Offset: 0x00354A10
		public SaveSmartTagAsXml SaveSmartTagAsXml
		{
			get
			{
				return base.GetElement<SaveSmartTagAsXml>(12);
			}
			set
			{
				base.SetElement<SaveSmartTagAsXml>(12, value);
			}
		}

		// Token: 0x06019C9B RID: 105627 RVA: 0x0035681B File Offset: 0x00354A1B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WebSettings>(deep);
		}

		// Token: 0x0400AA76 RID: 43638
		private const string tagName = "webSettings";

		// Token: 0x0400AA77 RID: 43639
		private const byte tagNsId = 23;

		// Token: 0x0400AA78 RID: 43640
		internal const int ElementTypeIdConst = 11698;

		// Token: 0x0400AA79 RID: 43641
		private static readonly string[] eleTagNames = new string[]
		{
			"frameset", "divs", "encoding", "optimizeForBrowser", "relyOnVML", "allowPNG", "doNotRelyOnCSS", "doNotSaveAsSingleFile", "doNotOrganizeInFolder", "doNotUseLongFileNames",
			"pixelsPerInch", "targetScreenSz", "saveSmartTagsAsXml"
		};

		// Token: 0x0400AA7A RID: 43642
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23
		};
	}
}
