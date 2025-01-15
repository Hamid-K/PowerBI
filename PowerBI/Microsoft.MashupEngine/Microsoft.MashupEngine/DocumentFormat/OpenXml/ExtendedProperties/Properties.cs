using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002932 RID: 10546
	[ChildElementInfo(typeof(TitlesOfParts))]
	[ChildElementInfo(typeof(HyperlinksChanged))]
	[ChildElementInfo(typeof(ScaleCrop))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Template))]
	[ChildElementInfo(typeof(Manager))]
	[ChildElementInfo(typeof(Company))]
	[ChildElementInfo(typeof(Pages))]
	[ChildElementInfo(typeof(Words))]
	[ChildElementInfo(typeof(Characters))]
	[ChildElementInfo(typeof(PresentationFormat))]
	[ChildElementInfo(typeof(Lines))]
	[ChildElementInfo(typeof(Paragraphs))]
	[ChildElementInfo(typeof(Slides))]
	[ChildElementInfo(typeof(Notes))]
	[ChildElementInfo(typeof(TotalTime))]
	[ChildElementInfo(typeof(HiddenSlides))]
	[ChildElementInfo(typeof(MultimediaClips))]
	[ChildElementInfo(typeof(HeadingPairs))]
	[ChildElementInfo(typeof(LinksUpToDate))]
	[ChildElementInfo(typeof(CharactersWithSpaces))]
	[ChildElementInfo(typeof(SharedDocument))]
	[ChildElementInfo(typeof(HyperlinkBase))]
	[ChildElementInfo(typeof(HyperlinkList))]
	[ChildElementInfo(typeof(DigitalSignature))]
	[ChildElementInfo(typeof(Application))]
	[ChildElementInfo(typeof(ApplicationVersion))]
	[ChildElementInfo(typeof(DocumentSecurity))]
	internal class Properties : OpenXmlPartRootElement
	{
		// Token: 0x17006AF2 RID: 27378
		// (get) Token: 0x06014E01 RID: 85505 RVA: 0x00316653 File Offset: 0x00314853
		public override string LocalName
		{
			get
			{
				return "Properties";
			}
		}

		// Token: 0x17006AF3 RID: 27379
		// (get) Token: 0x06014E02 RID: 85506 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006AF4 RID: 27380
		// (get) Token: 0x06014E03 RID: 85507 RVA: 0x003183C7 File Offset: 0x003165C7
		internal override int ElementTypeId
		{
			get
			{
				return 10998;
			}
		}

		// Token: 0x06014E04 RID: 85508 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014E05 RID: 85509 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Properties(ExtendedFilePropertiesPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06014E06 RID: 85510 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(ExtendedFilePropertiesPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006AF5 RID: 27381
		// (get) Token: 0x06014E07 RID: 85511 RVA: 0x003183CE File Offset: 0x003165CE
		// (set) Token: 0x06014E08 RID: 85512 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public ExtendedFilePropertiesPart ExtendedFilePropertiesPart
		{
			get
			{
				return base.OpenXmlPart as ExtendedFilePropertiesPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06014E09 RID: 85513 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Properties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014E0A RID: 85514 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Properties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014E0B RID: 85515 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Properties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014E0C RID: 85516 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Properties()
		{
		}

		// Token: 0x06014E0D RID: 85517 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(ExtendedFilePropertiesPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06014E0E RID: 85518 RVA: 0x003183DC File Offset: 0x003165DC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (3 == namespaceId && "Template" == name)
			{
				return new Template();
			}
			if (3 == namespaceId && "Manager" == name)
			{
				return new Manager();
			}
			if (3 == namespaceId && "Company" == name)
			{
				return new Company();
			}
			if (3 == namespaceId && "Pages" == name)
			{
				return new Pages();
			}
			if (3 == namespaceId && "Words" == name)
			{
				return new Words();
			}
			if (3 == namespaceId && "Characters" == name)
			{
				return new Characters();
			}
			if (3 == namespaceId && "PresentationFormat" == name)
			{
				return new PresentationFormat();
			}
			if (3 == namespaceId && "Lines" == name)
			{
				return new Lines();
			}
			if (3 == namespaceId && "Paragraphs" == name)
			{
				return new Paragraphs();
			}
			if (3 == namespaceId && "Slides" == name)
			{
				return new Slides();
			}
			if (3 == namespaceId && "Notes" == name)
			{
				return new Notes();
			}
			if (3 == namespaceId && "TotalTime" == name)
			{
				return new TotalTime();
			}
			if (3 == namespaceId && "HiddenSlides" == name)
			{
				return new HiddenSlides();
			}
			if (3 == namespaceId && "MMClips" == name)
			{
				return new MultimediaClips();
			}
			if (3 == namespaceId && "ScaleCrop" == name)
			{
				return new ScaleCrop();
			}
			if (3 == namespaceId && "HeadingPairs" == name)
			{
				return new HeadingPairs();
			}
			if (3 == namespaceId && "TitlesOfParts" == name)
			{
				return new TitlesOfParts();
			}
			if (3 == namespaceId && "LinksUpToDate" == name)
			{
				return new LinksUpToDate();
			}
			if (3 == namespaceId && "CharactersWithSpaces" == name)
			{
				return new CharactersWithSpaces();
			}
			if (3 == namespaceId && "SharedDoc" == name)
			{
				return new SharedDocument();
			}
			if (3 == namespaceId && "HyperlinkBase" == name)
			{
				return new HyperlinkBase();
			}
			if (3 == namespaceId && "HLinks" == name)
			{
				return new HyperlinkList();
			}
			if (3 == namespaceId && "HyperlinksChanged" == name)
			{
				return new HyperlinksChanged();
			}
			if (3 == namespaceId && "DigSig" == name)
			{
				return new DigitalSignature();
			}
			if (3 == namespaceId && "Application" == name)
			{
				return new Application();
			}
			if (3 == namespaceId && "AppVersion" == name)
			{
				return new ApplicationVersion();
			}
			if (3 == namespaceId && "DocSecurity" == name)
			{
				return new DocumentSecurity();
			}
			return null;
		}

		// Token: 0x17006AF6 RID: 27382
		// (get) Token: 0x06014E0F RID: 85519 RVA: 0x00318657 File Offset: 0x00316857
		internal override string[] ElementTagNames
		{
			get
			{
				return Properties.eleTagNames;
			}
		}

		// Token: 0x17006AF7 RID: 27383
		// (get) Token: 0x06014E10 RID: 85520 RVA: 0x0031865E File Offset: 0x0031685E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Properties.eleNamespaceIds;
			}
		}

		// Token: 0x17006AF8 RID: 27384
		// (get) Token: 0x06014E11 RID: 85521 RVA: 0x0000240C File Offset: 0x0000060C
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneAll;
			}
		}

		// Token: 0x17006AF9 RID: 27385
		// (get) Token: 0x06014E12 RID: 85522 RVA: 0x00318665 File Offset: 0x00316865
		// (set) Token: 0x06014E13 RID: 85523 RVA: 0x0031866E File Offset: 0x0031686E
		public Template Template
		{
			get
			{
				return base.GetElement<Template>(0);
			}
			set
			{
				base.SetElement<Template>(0, value);
			}
		}

		// Token: 0x17006AFA RID: 27386
		// (get) Token: 0x06014E14 RID: 85524 RVA: 0x00318678 File Offset: 0x00316878
		// (set) Token: 0x06014E15 RID: 85525 RVA: 0x00318681 File Offset: 0x00316881
		public Manager Manager
		{
			get
			{
				return base.GetElement<Manager>(1);
			}
			set
			{
				base.SetElement<Manager>(1, value);
			}
		}

		// Token: 0x17006AFB RID: 27387
		// (get) Token: 0x06014E16 RID: 85526 RVA: 0x0031868B File Offset: 0x0031688B
		// (set) Token: 0x06014E17 RID: 85527 RVA: 0x00318694 File Offset: 0x00316894
		public Company Company
		{
			get
			{
				return base.GetElement<Company>(2);
			}
			set
			{
				base.SetElement<Company>(2, value);
			}
		}

		// Token: 0x17006AFC RID: 27388
		// (get) Token: 0x06014E18 RID: 85528 RVA: 0x0031869E File Offset: 0x0031689E
		// (set) Token: 0x06014E19 RID: 85529 RVA: 0x003186A7 File Offset: 0x003168A7
		public Pages Pages
		{
			get
			{
				return base.GetElement<Pages>(3);
			}
			set
			{
				base.SetElement<Pages>(3, value);
			}
		}

		// Token: 0x17006AFD RID: 27389
		// (get) Token: 0x06014E1A RID: 85530 RVA: 0x003186B1 File Offset: 0x003168B1
		// (set) Token: 0x06014E1B RID: 85531 RVA: 0x003186BA File Offset: 0x003168BA
		public Words Words
		{
			get
			{
				return base.GetElement<Words>(4);
			}
			set
			{
				base.SetElement<Words>(4, value);
			}
		}

		// Token: 0x17006AFE RID: 27390
		// (get) Token: 0x06014E1C RID: 85532 RVA: 0x003186C4 File Offset: 0x003168C4
		// (set) Token: 0x06014E1D RID: 85533 RVA: 0x003186CD File Offset: 0x003168CD
		public Characters Characters
		{
			get
			{
				return base.GetElement<Characters>(5);
			}
			set
			{
				base.SetElement<Characters>(5, value);
			}
		}

		// Token: 0x17006AFF RID: 27391
		// (get) Token: 0x06014E1E RID: 85534 RVA: 0x003186D7 File Offset: 0x003168D7
		// (set) Token: 0x06014E1F RID: 85535 RVA: 0x003186E0 File Offset: 0x003168E0
		public PresentationFormat PresentationFormat
		{
			get
			{
				return base.GetElement<PresentationFormat>(6);
			}
			set
			{
				base.SetElement<PresentationFormat>(6, value);
			}
		}

		// Token: 0x17006B00 RID: 27392
		// (get) Token: 0x06014E20 RID: 85536 RVA: 0x003186EA File Offset: 0x003168EA
		// (set) Token: 0x06014E21 RID: 85537 RVA: 0x003186F3 File Offset: 0x003168F3
		public Lines Lines
		{
			get
			{
				return base.GetElement<Lines>(7);
			}
			set
			{
				base.SetElement<Lines>(7, value);
			}
		}

		// Token: 0x17006B01 RID: 27393
		// (get) Token: 0x06014E22 RID: 85538 RVA: 0x003186FD File Offset: 0x003168FD
		// (set) Token: 0x06014E23 RID: 85539 RVA: 0x00318706 File Offset: 0x00316906
		public Paragraphs Paragraphs
		{
			get
			{
				return base.GetElement<Paragraphs>(8);
			}
			set
			{
				base.SetElement<Paragraphs>(8, value);
			}
		}

		// Token: 0x17006B02 RID: 27394
		// (get) Token: 0x06014E24 RID: 85540 RVA: 0x00318710 File Offset: 0x00316910
		// (set) Token: 0x06014E25 RID: 85541 RVA: 0x0031871A File Offset: 0x0031691A
		public Slides Slides
		{
			get
			{
				return base.GetElement<Slides>(9);
			}
			set
			{
				base.SetElement<Slides>(9, value);
			}
		}

		// Token: 0x17006B03 RID: 27395
		// (get) Token: 0x06014E26 RID: 85542 RVA: 0x00318725 File Offset: 0x00316925
		// (set) Token: 0x06014E27 RID: 85543 RVA: 0x0031872F File Offset: 0x0031692F
		public Notes Notes
		{
			get
			{
				return base.GetElement<Notes>(10);
			}
			set
			{
				base.SetElement<Notes>(10, value);
			}
		}

		// Token: 0x17006B04 RID: 27396
		// (get) Token: 0x06014E28 RID: 85544 RVA: 0x0031873A File Offset: 0x0031693A
		// (set) Token: 0x06014E29 RID: 85545 RVA: 0x00318744 File Offset: 0x00316944
		public TotalTime TotalTime
		{
			get
			{
				return base.GetElement<TotalTime>(11);
			}
			set
			{
				base.SetElement<TotalTime>(11, value);
			}
		}

		// Token: 0x17006B05 RID: 27397
		// (get) Token: 0x06014E2A RID: 85546 RVA: 0x0031874F File Offset: 0x0031694F
		// (set) Token: 0x06014E2B RID: 85547 RVA: 0x00318759 File Offset: 0x00316959
		public HiddenSlides HiddenSlides
		{
			get
			{
				return base.GetElement<HiddenSlides>(12);
			}
			set
			{
				base.SetElement<HiddenSlides>(12, value);
			}
		}

		// Token: 0x17006B06 RID: 27398
		// (get) Token: 0x06014E2C RID: 85548 RVA: 0x00318764 File Offset: 0x00316964
		// (set) Token: 0x06014E2D RID: 85549 RVA: 0x0031876E File Offset: 0x0031696E
		public MultimediaClips MultimediaClips
		{
			get
			{
				return base.GetElement<MultimediaClips>(13);
			}
			set
			{
				base.SetElement<MultimediaClips>(13, value);
			}
		}

		// Token: 0x17006B07 RID: 27399
		// (get) Token: 0x06014E2E RID: 85550 RVA: 0x00318779 File Offset: 0x00316979
		// (set) Token: 0x06014E2F RID: 85551 RVA: 0x00318783 File Offset: 0x00316983
		public ScaleCrop ScaleCrop
		{
			get
			{
				return base.GetElement<ScaleCrop>(14);
			}
			set
			{
				base.SetElement<ScaleCrop>(14, value);
			}
		}

		// Token: 0x17006B08 RID: 27400
		// (get) Token: 0x06014E30 RID: 85552 RVA: 0x0031878E File Offset: 0x0031698E
		// (set) Token: 0x06014E31 RID: 85553 RVA: 0x00318798 File Offset: 0x00316998
		public HeadingPairs HeadingPairs
		{
			get
			{
				return base.GetElement<HeadingPairs>(15);
			}
			set
			{
				base.SetElement<HeadingPairs>(15, value);
			}
		}

		// Token: 0x17006B09 RID: 27401
		// (get) Token: 0x06014E32 RID: 85554 RVA: 0x003187A3 File Offset: 0x003169A3
		// (set) Token: 0x06014E33 RID: 85555 RVA: 0x003187AD File Offset: 0x003169AD
		public TitlesOfParts TitlesOfParts
		{
			get
			{
				return base.GetElement<TitlesOfParts>(16);
			}
			set
			{
				base.SetElement<TitlesOfParts>(16, value);
			}
		}

		// Token: 0x17006B0A RID: 27402
		// (get) Token: 0x06014E34 RID: 85556 RVA: 0x003187B8 File Offset: 0x003169B8
		// (set) Token: 0x06014E35 RID: 85557 RVA: 0x003187C2 File Offset: 0x003169C2
		public LinksUpToDate LinksUpToDate
		{
			get
			{
				return base.GetElement<LinksUpToDate>(17);
			}
			set
			{
				base.SetElement<LinksUpToDate>(17, value);
			}
		}

		// Token: 0x17006B0B RID: 27403
		// (get) Token: 0x06014E36 RID: 85558 RVA: 0x003187CD File Offset: 0x003169CD
		// (set) Token: 0x06014E37 RID: 85559 RVA: 0x003187D7 File Offset: 0x003169D7
		public CharactersWithSpaces CharactersWithSpaces
		{
			get
			{
				return base.GetElement<CharactersWithSpaces>(18);
			}
			set
			{
				base.SetElement<CharactersWithSpaces>(18, value);
			}
		}

		// Token: 0x17006B0C RID: 27404
		// (get) Token: 0x06014E38 RID: 85560 RVA: 0x003187E2 File Offset: 0x003169E2
		// (set) Token: 0x06014E39 RID: 85561 RVA: 0x003187EC File Offset: 0x003169EC
		public SharedDocument SharedDocument
		{
			get
			{
				return base.GetElement<SharedDocument>(19);
			}
			set
			{
				base.SetElement<SharedDocument>(19, value);
			}
		}

		// Token: 0x17006B0D RID: 27405
		// (get) Token: 0x06014E3A RID: 85562 RVA: 0x003187F7 File Offset: 0x003169F7
		// (set) Token: 0x06014E3B RID: 85563 RVA: 0x00318801 File Offset: 0x00316A01
		public HyperlinkBase HyperlinkBase
		{
			get
			{
				return base.GetElement<HyperlinkBase>(20);
			}
			set
			{
				base.SetElement<HyperlinkBase>(20, value);
			}
		}

		// Token: 0x17006B0E RID: 27406
		// (get) Token: 0x06014E3C RID: 85564 RVA: 0x0031880C File Offset: 0x00316A0C
		// (set) Token: 0x06014E3D RID: 85565 RVA: 0x00318816 File Offset: 0x00316A16
		public HyperlinkList HyperlinkList
		{
			get
			{
				return base.GetElement<HyperlinkList>(21);
			}
			set
			{
				base.SetElement<HyperlinkList>(21, value);
			}
		}

		// Token: 0x17006B0F RID: 27407
		// (get) Token: 0x06014E3E RID: 85566 RVA: 0x00318821 File Offset: 0x00316A21
		// (set) Token: 0x06014E3F RID: 85567 RVA: 0x0031882B File Offset: 0x00316A2B
		public HyperlinksChanged HyperlinksChanged
		{
			get
			{
				return base.GetElement<HyperlinksChanged>(22);
			}
			set
			{
				base.SetElement<HyperlinksChanged>(22, value);
			}
		}

		// Token: 0x17006B10 RID: 27408
		// (get) Token: 0x06014E40 RID: 85568 RVA: 0x00318836 File Offset: 0x00316A36
		// (set) Token: 0x06014E41 RID: 85569 RVA: 0x00318840 File Offset: 0x00316A40
		public DigitalSignature DigitalSignature
		{
			get
			{
				return base.GetElement<DigitalSignature>(23);
			}
			set
			{
				base.SetElement<DigitalSignature>(23, value);
			}
		}

		// Token: 0x17006B11 RID: 27409
		// (get) Token: 0x06014E42 RID: 85570 RVA: 0x0031884B File Offset: 0x00316A4B
		// (set) Token: 0x06014E43 RID: 85571 RVA: 0x00318855 File Offset: 0x00316A55
		public Application Application
		{
			get
			{
				return base.GetElement<Application>(24);
			}
			set
			{
				base.SetElement<Application>(24, value);
			}
		}

		// Token: 0x17006B12 RID: 27410
		// (get) Token: 0x06014E44 RID: 85572 RVA: 0x00318860 File Offset: 0x00316A60
		// (set) Token: 0x06014E45 RID: 85573 RVA: 0x0031886A File Offset: 0x00316A6A
		public ApplicationVersion ApplicationVersion
		{
			get
			{
				return base.GetElement<ApplicationVersion>(25);
			}
			set
			{
				base.SetElement<ApplicationVersion>(25, value);
			}
		}

		// Token: 0x17006B13 RID: 27411
		// (get) Token: 0x06014E46 RID: 85574 RVA: 0x00318875 File Offset: 0x00316A75
		// (set) Token: 0x06014E47 RID: 85575 RVA: 0x0031887F File Offset: 0x00316A7F
		public DocumentSecurity DocumentSecurity
		{
			get
			{
				return base.GetElement<DocumentSecurity>(26);
			}
			set
			{
				base.SetElement<DocumentSecurity>(26, value);
			}
		}

		// Token: 0x06014E48 RID: 85576 RVA: 0x0031888A File Offset: 0x00316A8A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Properties>(deep);
		}

		// Token: 0x0400906E RID: 36974
		private const string tagName = "Properties";

		// Token: 0x0400906F RID: 36975
		private const byte tagNsId = 3;

		// Token: 0x04009070 RID: 36976
		internal const int ElementTypeIdConst = 10998;

		// Token: 0x04009071 RID: 36977
		private static readonly string[] eleTagNames = new string[]
		{
			"Template", "Manager", "Company", "Pages", "Words", "Characters", "PresentationFormat", "Lines", "Paragraphs", "Slides",
			"Notes", "TotalTime", "HiddenSlides", "MMClips", "ScaleCrop", "HeadingPairs", "TitlesOfParts", "LinksUpToDate", "CharactersWithSpaces", "SharedDoc",
			"HyperlinkBase", "HLinks", "HyperlinksChanged", "DigSig", "Application", "AppVersion", "DocSecurity"
		};

		// Token: 0x04009072 RID: 36978
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3
		};
	}
}
