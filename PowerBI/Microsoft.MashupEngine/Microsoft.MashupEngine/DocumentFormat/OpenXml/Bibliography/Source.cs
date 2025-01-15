using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x02002903 RID: 10499
	[ChildElementInfo(typeof(AlbumTitle))]
	[ChildElementInfo(typeof(ThesisType))]
	[ChildElementInfo(typeof(Theater))]
	[ChildElementInfo(typeof(MonthAccessed))]
	[ChildElementInfo(typeof(Month))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AuthorList))]
	[ChildElementInfo(typeof(BookTitle))]
	[ChildElementInfo(typeof(Broadcaster))]
	[ChildElementInfo(typeof(BroadcastTitle))]
	[ChildElementInfo(typeof(CaseNumber))]
	[ChildElementInfo(typeof(ChapterNumber))]
	[ChildElementInfo(typeof(City))]
	[ChildElementInfo(typeof(Comments))]
	[ChildElementInfo(typeof(ConferenceName))]
	[ChildElementInfo(typeof(CountryRegion))]
	[ChildElementInfo(typeof(Court))]
	[ChildElementInfo(typeof(Day))]
	[ChildElementInfo(typeof(DayAccessed))]
	[ChildElementInfo(typeof(Department))]
	[ChildElementInfo(typeof(Distributor))]
	[ChildElementInfo(typeof(Edition))]
	[ChildElementInfo(typeof(GuidString))]
	[ChildElementInfo(typeof(Institution))]
	[ChildElementInfo(typeof(InternetSiteTitle))]
	[ChildElementInfo(typeof(Issue))]
	[ChildElementInfo(typeof(JournalName))]
	[ChildElementInfo(typeof(LcId))]
	[ChildElementInfo(typeof(Medium))]
	[ChildElementInfo(typeof(AbbreviatedCaseNumber))]
	[ChildElementInfo(typeof(NumberVolumes))]
	[ChildElementInfo(typeof(Pages))]
	[ChildElementInfo(typeof(PatentNumber))]
	[ChildElementInfo(typeof(PeriodicalTitle))]
	[ChildElementInfo(typeof(ProductionCompany))]
	[ChildElementInfo(typeof(PublicationTitle))]
	[ChildElementInfo(typeof(Publisher))]
	[ChildElementInfo(typeof(RecordingNumber))]
	[ChildElementInfo(typeof(ReferenceOrder))]
	[ChildElementInfo(typeof(YearAccessed))]
	[ChildElementInfo(typeof(SourceType))]
	[ChildElementInfo(typeof(ShortTitle))]
	[ChildElementInfo(typeof(StandardNumber))]
	[ChildElementInfo(typeof(StateProvince))]
	[ChildElementInfo(typeof(Station))]
	[ChildElementInfo(typeof(Tag))]
	[ChildElementInfo(typeof(Reporter))]
	[ChildElementInfo(typeof(Title))]
	[ChildElementInfo(typeof(PatentType))]
	[ChildElementInfo(typeof(UrlString))]
	[ChildElementInfo(typeof(Version))]
	[ChildElementInfo(typeof(Volume))]
	[ChildElementInfo(typeof(Year))]
	internal class Source : OpenXmlCompositeElement
	{
		// Token: 0x170069C7 RID: 27079
		// (get) Token: 0x06014B4C RID: 84812 RVA: 0x003159F0 File Offset: 0x00313BF0
		public override string LocalName
		{
			get
			{
				return "Source";
			}
		}

		// Token: 0x170069C8 RID: 27080
		// (get) Token: 0x06014B4D RID: 84813 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170069C9 RID: 27081
		// (get) Token: 0x06014B4E RID: 84814 RVA: 0x003159F7 File Offset: 0x00313BF7
		internal override int ElementTypeId
		{
			get
			{
				return 10833;
			}
		}

		// Token: 0x06014B4F RID: 84815 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014B50 RID: 84816 RVA: 0x00293ECF File Offset: 0x002920CF
		public Source()
		{
		}

		// Token: 0x06014B51 RID: 84817 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Source(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014B52 RID: 84818 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Source(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014B53 RID: 84819 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Source(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014B54 RID: 84820 RVA: 0x00315A00 File Offset: 0x00313C00
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (9 == namespaceId && "AbbreviatedCaseNumber" == name)
			{
				return new AbbreviatedCaseNumber();
			}
			if (9 == namespaceId && "AlbumTitle" == name)
			{
				return new AlbumTitle();
			}
			if (9 == namespaceId && "Author" == name)
			{
				return new AuthorList();
			}
			if (9 == namespaceId && "BookTitle" == name)
			{
				return new BookTitle();
			}
			if (9 == namespaceId && "Broadcaster" == name)
			{
				return new Broadcaster();
			}
			if (9 == namespaceId && "BroadcastTitle" == name)
			{
				return new BroadcastTitle();
			}
			if (9 == namespaceId && "CaseNumber" == name)
			{
				return new CaseNumber();
			}
			if (9 == namespaceId && "ChapterNumber" == name)
			{
				return new ChapterNumber();
			}
			if (9 == namespaceId && "City" == name)
			{
				return new City();
			}
			if (9 == namespaceId && "Comments" == name)
			{
				return new Comments();
			}
			if (9 == namespaceId && "ConferenceName" == name)
			{
				return new ConferenceName();
			}
			if (9 == namespaceId && "CountryRegion" == name)
			{
				return new CountryRegion();
			}
			if (9 == namespaceId && "Court" == name)
			{
				return new Court();
			}
			if (9 == namespaceId && "Day" == name)
			{
				return new Day();
			}
			if (9 == namespaceId && "DayAccessed" == name)
			{
				return new DayAccessed();
			}
			if (9 == namespaceId && "Department" == name)
			{
				return new Department();
			}
			if (9 == namespaceId && "Distributor" == name)
			{
				return new Distributor();
			}
			if (9 == namespaceId && "Edition" == name)
			{
				return new Edition();
			}
			if (9 == namespaceId && "Guid" == name)
			{
				return new GuidString();
			}
			if (9 == namespaceId && "Institution" == name)
			{
				return new Institution();
			}
			if (9 == namespaceId && "InternetSiteTitle" == name)
			{
				return new InternetSiteTitle();
			}
			if (9 == namespaceId && "Issue" == name)
			{
				return new Issue();
			}
			if (9 == namespaceId && "JournalName" == name)
			{
				return new JournalName();
			}
			if (9 == namespaceId && "LCID" == name)
			{
				return new LcId();
			}
			if (9 == namespaceId && "Medium" == name)
			{
				return new Medium();
			}
			if (9 == namespaceId && "Month" == name)
			{
				return new Month();
			}
			if (9 == namespaceId && "MonthAccessed" == name)
			{
				return new MonthAccessed();
			}
			if (9 == namespaceId && "NumberVolumes" == name)
			{
				return new NumberVolumes();
			}
			if (9 == namespaceId && "Pages" == name)
			{
				return new Pages();
			}
			if (9 == namespaceId && "PatentNumber" == name)
			{
				return new PatentNumber();
			}
			if (9 == namespaceId && "PeriodicalTitle" == name)
			{
				return new PeriodicalTitle();
			}
			if (9 == namespaceId && "ProductionCompany" == name)
			{
				return new ProductionCompany();
			}
			if (9 == namespaceId && "PublicationTitle" == name)
			{
				return new PublicationTitle();
			}
			if (9 == namespaceId && "Publisher" == name)
			{
				return new Publisher();
			}
			if (9 == namespaceId && "RecordingNumber" == name)
			{
				return new RecordingNumber();
			}
			if (9 == namespaceId && "RefOrder" == name)
			{
				return new ReferenceOrder();
			}
			if (9 == namespaceId && "Reporter" == name)
			{
				return new Reporter();
			}
			if (9 == namespaceId && "SourceType" == name)
			{
				return new SourceType();
			}
			if (9 == namespaceId && "ShortTitle" == name)
			{
				return new ShortTitle();
			}
			if (9 == namespaceId && "StandardNumber" == name)
			{
				return new StandardNumber();
			}
			if (9 == namespaceId && "StateProvince" == name)
			{
				return new StateProvince();
			}
			if (9 == namespaceId && "Station" == name)
			{
				return new Station();
			}
			if (9 == namespaceId && "Tag" == name)
			{
				return new Tag();
			}
			if (9 == namespaceId && "Theater" == name)
			{
				return new Theater();
			}
			if (9 == namespaceId && "ThesisType" == name)
			{
				return new ThesisType();
			}
			if (9 == namespaceId && "Title" == name)
			{
				return new Title();
			}
			if (9 == namespaceId && "Type" == name)
			{
				return new PatentType();
			}
			if (9 == namespaceId && "URL" == name)
			{
				return new UrlString();
			}
			if (9 == namespaceId && "Version" == name)
			{
				return new Version();
			}
			if (9 == namespaceId && "Volume" == name)
			{
				return new Volume();
			}
			if (9 == namespaceId && "Year" == name)
			{
				return new Year();
			}
			if (9 == namespaceId && "YearAccessed" == name)
			{
				return new YearAccessed();
			}
			return null;
		}

		// Token: 0x170069CA RID: 27082
		// (get) Token: 0x06014B55 RID: 84821 RVA: 0x00315EEE File Offset: 0x003140EE
		internal override string[] ElementTagNames
		{
			get
			{
				return Source.eleTagNames;
			}
		}

		// Token: 0x170069CB RID: 27083
		// (get) Token: 0x06014B56 RID: 84822 RVA: 0x00315EF5 File Offset: 0x003140F5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Source.eleNamespaceIds;
			}
		}

		// Token: 0x170069CC RID: 27084
		// (get) Token: 0x06014B57 RID: 84823 RVA: 0x0000240C File Offset: 0x0000060C
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneAll;
			}
		}

		// Token: 0x170069CD RID: 27085
		// (get) Token: 0x06014B58 RID: 84824 RVA: 0x00315EFC File Offset: 0x003140FC
		// (set) Token: 0x06014B59 RID: 84825 RVA: 0x00315F05 File Offset: 0x00314105
		public AbbreviatedCaseNumber AbbreviatedCaseNumber
		{
			get
			{
				return base.GetElement<AbbreviatedCaseNumber>(0);
			}
			set
			{
				base.SetElement<AbbreviatedCaseNumber>(0, value);
			}
		}

		// Token: 0x170069CE RID: 27086
		// (get) Token: 0x06014B5A RID: 84826 RVA: 0x00315F0F File Offset: 0x0031410F
		// (set) Token: 0x06014B5B RID: 84827 RVA: 0x00315F18 File Offset: 0x00314118
		public AlbumTitle AlbumTitle
		{
			get
			{
				return base.GetElement<AlbumTitle>(1);
			}
			set
			{
				base.SetElement<AlbumTitle>(1, value);
			}
		}

		// Token: 0x170069CF RID: 27087
		// (get) Token: 0x06014B5C RID: 84828 RVA: 0x00315F22 File Offset: 0x00314122
		// (set) Token: 0x06014B5D RID: 84829 RVA: 0x00315F2B File Offset: 0x0031412B
		public AuthorList AuthorList
		{
			get
			{
				return base.GetElement<AuthorList>(2);
			}
			set
			{
				base.SetElement<AuthorList>(2, value);
			}
		}

		// Token: 0x170069D0 RID: 27088
		// (get) Token: 0x06014B5E RID: 84830 RVA: 0x00315F35 File Offset: 0x00314135
		// (set) Token: 0x06014B5F RID: 84831 RVA: 0x00315F3E File Offset: 0x0031413E
		public BookTitle BookTitle
		{
			get
			{
				return base.GetElement<BookTitle>(3);
			}
			set
			{
				base.SetElement<BookTitle>(3, value);
			}
		}

		// Token: 0x170069D1 RID: 27089
		// (get) Token: 0x06014B60 RID: 84832 RVA: 0x00315F48 File Offset: 0x00314148
		// (set) Token: 0x06014B61 RID: 84833 RVA: 0x00315F51 File Offset: 0x00314151
		public Broadcaster Broadcaster
		{
			get
			{
				return base.GetElement<Broadcaster>(4);
			}
			set
			{
				base.SetElement<Broadcaster>(4, value);
			}
		}

		// Token: 0x170069D2 RID: 27090
		// (get) Token: 0x06014B62 RID: 84834 RVA: 0x00315F5B File Offset: 0x0031415B
		// (set) Token: 0x06014B63 RID: 84835 RVA: 0x00315F64 File Offset: 0x00314164
		public BroadcastTitle BroadcastTitle
		{
			get
			{
				return base.GetElement<BroadcastTitle>(5);
			}
			set
			{
				base.SetElement<BroadcastTitle>(5, value);
			}
		}

		// Token: 0x170069D3 RID: 27091
		// (get) Token: 0x06014B64 RID: 84836 RVA: 0x00315F6E File Offset: 0x0031416E
		// (set) Token: 0x06014B65 RID: 84837 RVA: 0x00315F77 File Offset: 0x00314177
		public CaseNumber CaseNumber
		{
			get
			{
				return base.GetElement<CaseNumber>(6);
			}
			set
			{
				base.SetElement<CaseNumber>(6, value);
			}
		}

		// Token: 0x170069D4 RID: 27092
		// (get) Token: 0x06014B66 RID: 84838 RVA: 0x00315F81 File Offset: 0x00314181
		// (set) Token: 0x06014B67 RID: 84839 RVA: 0x00315F8A File Offset: 0x0031418A
		public ChapterNumber ChapterNumber
		{
			get
			{
				return base.GetElement<ChapterNumber>(7);
			}
			set
			{
				base.SetElement<ChapterNumber>(7, value);
			}
		}

		// Token: 0x170069D5 RID: 27093
		// (get) Token: 0x06014B68 RID: 84840 RVA: 0x00315F94 File Offset: 0x00314194
		// (set) Token: 0x06014B69 RID: 84841 RVA: 0x00315F9D File Offset: 0x0031419D
		public City City
		{
			get
			{
				return base.GetElement<City>(8);
			}
			set
			{
				base.SetElement<City>(8, value);
			}
		}

		// Token: 0x170069D6 RID: 27094
		// (get) Token: 0x06014B6A RID: 84842 RVA: 0x00315FA7 File Offset: 0x003141A7
		// (set) Token: 0x06014B6B RID: 84843 RVA: 0x00315FB1 File Offset: 0x003141B1
		public Comments Comments
		{
			get
			{
				return base.GetElement<Comments>(9);
			}
			set
			{
				base.SetElement<Comments>(9, value);
			}
		}

		// Token: 0x170069D7 RID: 27095
		// (get) Token: 0x06014B6C RID: 84844 RVA: 0x00315FBC File Offset: 0x003141BC
		// (set) Token: 0x06014B6D RID: 84845 RVA: 0x00315FC6 File Offset: 0x003141C6
		public ConferenceName ConferenceName
		{
			get
			{
				return base.GetElement<ConferenceName>(10);
			}
			set
			{
				base.SetElement<ConferenceName>(10, value);
			}
		}

		// Token: 0x170069D8 RID: 27096
		// (get) Token: 0x06014B6E RID: 84846 RVA: 0x00315FD1 File Offset: 0x003141D1
		// (set) Token: 0x06014B6F RID: 84847 RVA: 0x00315FDB File Offset: 0x003141DB
		public CountryRegion CountryRegion
		{
			get
			{
				return base.GetElement<CountryRegion>(11);
			}
			set
			{
				base.SetElement<CountryRegion>(11, value);
			}
		}

		// Token: 0x170069D9 RID: 27097
		// (get) Token: 0x06014B70 RID: 84848 RVA: 0x00315FE6 File Offset: 0x003141E6
		// (set) Token: 0x06014B71 RID: 84849 RVA: 0x00315FF0 File Offset: 0x003141F0
		public Court Court
		{
			get
			{
				return base.GetElement<Court>(12);
			}
			set
			{
				base.SetElement<Court>(12, value);
			}
		}

		// Token: 0x170069DA RID: 27098
		// (get) Token: 0x06014B72 RID: 84850 RVA: 0x00315FFB File Offset: 0x003141FB
		// (set) Token: 0x06014B73 RID: 84851 RVA: 0x00316005 File Offset: 0x00314205
		public Day Day
		{
			get
			{
				return base.GetElement<Day>(13);
			}
			set
			{
				base.SetElement<Day>(13, value);
			}
		}

		// Token: 0x170069DB RID: 27099
		// (get) Token: 0x06014B74 RID: 84852 RVA: 0x00316010 File Offset: 0x00314210
		// (set) Token: 0x06014B75 RID: 84853 RVA: 0x0031601A File Offset: 0x0031421A
		public DayAccessed DayAccessed
		{
			get
			{
				return base.GetElement<DayAccessed>(14);
			}
			set
			{
				base.SetElement<DayAccessed>(14, value);
			}
		}

		// Token: 0x170069DC RID: 27100
		// (get) Token: 0x06014B76 RID: 84854 RVA: 0x00316025 File Offset: 0x00314225
		// (set) Token: 0x06014B77 RID: 84855 RVA: 0x0031602F File Offset: 0x0031422F
		public Department Department
		{
			get
			{
				return base.GetElement<Department>(15);
			}
			set
			{
				base.SetElement<Department>(15, value);
			}
		}

		// Token: 0x170069DD RID: 27101
		// (get) Token: 0x06014B78 RID: 84856 RVA: 0x0031603A File Offset: 0x0031423A
		// (set) Token: 0x06014B79 RID: 84857 RVA: 0x00316044 File Offset: 0x00314244
		public Distributor Distributor
		{
			get
			{
				return base.GetElement<Distributor>(16);
			}
			set
			{
				base.SetElement<Distributor>(16, value);
			}
		}

		// Token: 0x170069DE RID: 27102
		// (get) Token: 0x06014B7A RID: 84858 RVA: 0x0031604F File Offset: 0x0031424F
		// (set) Token: 0x06014B7B RID: 84859 RVA: 0x00316059 File Offset: 0x00314259
		public Edition Edition
		{
			get
			{
				return base.GetElement<Edition>(17);
			}
			set
			{
				base.SetElement<Edition>(17, value);
			}
		}

		// Token: 0x170069DF RID: 27103
		// (get) Token: 0x06014B7C RID: 84860 RVA: 0x00316064 File Offset: 0x00314264
		// (set) Token: 0x06014B7D RID: 84861 RVA: 0x0031606E File Offset: 0x0031426E
		public GuidString GuidString
		{
			get
			{
				return base.GetElement<GuidString>(18);
			}
			set
			{
				base.SetElement<GuidString>(18, value);
			}
		}

		// Token: 0x170069E0 RID: 27104
		// (get) Token: 0x06014B7E RID: 84862 RVA: 0x00316079 File Offset: 0x00314279
		// (set) Token: 0x06014B7F RID: 84863 RVA: 0x00316083 File Offset: 0x00314283
		public Institution Institution
		{
			get
			{
				return base.GetElement<Institution>(19);
			}
			set
			{
				base.SetElement<Institution>(19, value);
			}
		}

		// Token: 0x170069E1 RID: 27105
		// (get) Token: 0x06014B80 RID: 84864 RVA: 0x0031608E File Offset: 0x0031428E
		// (set) Token: 0x06014B81 RID: 84865 RVA: 0x00316098 File Offset: 0x00314298
		public InternetSiteTitle InternetSiteTitle
		{
			get
			{
				return base.GetElement<InternetSiteTitle>(20);
			}
			set
			{
				base.SetElement<InternetSiteTitle>(20, value);
			}
		}

		// Token: 0x170069E2 RID: 27106
		// (get) Token: 0x06014B82 RID: 84866 RVA: 0x003160A3 File Offset: 0x003142A3
		// (set) Token: 0x06014B83 RID: 84867 RVA: 0x003160AD File Offset: 0x003142AD
		public Issue Issue
		{
			get
			{
				return base.GetElement<Issue>(21);
			}
			set
			{
				base.SetElement<Issue>(21, value);
			}
		}

		// Token: 0x170069E3 RID: 27107
		// (get) Token: 0x06014B84 RID: 84868 RVA: 0x003160B8 File Offset: 0x003142B8
		// (set) Token: 0x06014B85 RID: 84869 RVA: 0x003160C2 File Offset: 0x003142C2
		public JournalName JournalName
		{
			get
			{
				return base.GetElement<JournalName>(22);
			}
			set
			{
				base.SetElement<JournalName>(22, value);
			}
		}

		// Token: 0x170069E4 RID: 27108
		// (get) Token: 0x06014B86 RID: 84870 RVA: 0x003160CD File Offset: 0x003142CD
		// (set) Token: 0x06014B87 RID: 84871 RVA: 0x003160D7 File Offset: 0x003142D7
		public LcId LcId
		{
			get
			{
				return base.GetElement<LcId>(23);
			}
			set
			{
				base.SetElement<LcId>(23, value);
			}
		}

		// Token: 0x170069E5 RID: 27109
		// (get) Token: 0x06014B88 RID: 84872 RVA: 0x003160E2 File Offset: 0x003142E2
		// (set) Token: 0x06014B89 RID: 84873 RVA: 0x003160EC File Offset: 0x003142EC
		public Medium Medium
		{
			get
			{
				return base.GetElement<Medium>(24);
			}
			set
			{
				base.SetElement<Medium>(24, value);
			}
		}

		// Token: 0x170069E6 RID: 27110
		// (get) Token: 0x06014B8A RID: 84874 RVA: 0x003160F7 File Offset: 0x003142F7
		// (set) Token: 0x06014B8B RID: 84875 RVA: 0x00316101 File Offset: 0x00314301
		public Month Month
		{
			get
			{
				return base.GetElement<Month>(25);
			}
			set
			{
				base.SetElement<Month>(25, value);
			}
		}

		// Token: 0x170069E7 RID: 27111
		// (get) Token: 0x06014B8C RID: 84876 RVA: 0x0031610C File Offset: 0x0031430C
		// (set) Token: 0x06014B8D RID: 84877 RVA: 0x00316116 File Offset: 0x00314316
		public MonthAccessed MonthAccessed
		{
			get
			{
				return base.GetElement<MonthAccessed>(26);
			}
			set
			{
				base.SetElement<MonthAccessed>(26, value);
			}
		}

		// Token: 0x170069E8 RID: 27112
		// (get) Token: 0x06014B8E RID: 84878 RVA: 0x00316121 File Offset: 0x00314321
		// (set) Token: 0x06014B8F RID: 84879 RVA: 0x0031612B File Offset: 0x0031432B
		public NumberVolumes NumberVolumes
		{
			get
			{
				return base.GetElement<NumberVolumes>(27);
			}
			set
			{
				base.SetElement<NumberVolumes>(27, value);
			}
		}

		// Token: 0x170069E9 RID: 27113
		// (get) Token: 0x06014B90 RID: 84880 RVA: 0x00316136 File Offset: 0x00314336
		// (set) Token: 0x06014B91 RID: 84881 RVA: 0x00316140 File Offset: 0x00314340
		public Pages Pages
		{
			get
			{
				return base.GetElement<Pages>(28);
			}
			set
			{
				base.SetElement<Pages>(28, value);
			}
		}

		// Token: 0x170069EA RID: 27114
		// (get) Token: 0x06014B92 RID: 84882 RVA: 0x0031614B File Offset: 0x0031434B
		// (set) Token: 0x06014B93 RID: 84883 RVA: 0x00316155 File Offset: 0x00314355
		public PatentNumber PatentNumber
		{
			get
			{
				return base.GetElement<PatentNumber>(29);
			}
			set
			{
				base.SetElement<PatentNumber>(29, value);
			}
		}

		// Token: 0x170069EB RID: 27115
		// (get) Token: 0x06014B94 RID: 84884 RVA: 0x00316160 File Offset: 0x00314360
		// (set) Token: 0x06014B95 RID: 84885 RVA: 0x0031616A File Offset: 0x0031436A
		public PeriodicalTitle PeriodicalTitle
		{
			get
			{
				return base.GetElement<PeriodicalTitle>(30);
			}
			set
			{
				base.SetElement<PeriodicalTitle>(30, value);
			}
		}

		// Token: 0x170069EC RID: 27116
		// (get) Token: 0x06014B96 RID: 84886 RVA: 0x00316175 File Offset: 0x00314375
		// (set) Token: 0x06014B97 RID: 84887 RVA: 0x0031617F File Offset: 0x0031437F
		public ProductionCompany ProductionCompany
		{
			get
			{
				return base.GetElement<ProductionCompany>(31);
			}
			set
			{
				base.SetElement<ProductionCompany>(31, value);
			}
		}

		// Token: 0x170069ED RID: 27117
		// (get) Token: 0x06014B98 RID: 84888 RVA: 0x0031618A File Offset: 0x0031438A
		// (set) Token: 0x06014B99 RID: 84889 RVA: 0x00316194 File Offset: 0x00314394
		public PublicationTitle PublicationTitle
		{
			get
			{
				return base.GetElement<PublicationTitle>(32);
			}
			set
			{
				base.SetElement<PublicationTitle>(32, value);
			}
		}

		// Token: 0x170069EE RID: 27118
		// (get) Token: 0x06014B9A RID: 84890 RVA: 0x0031619F File Offset: 0x0031439F
		// (set) Token: 0x06014B9B RID: 84891 RVA: 0x003161A9 File Offset: 0x003143A9
		public Publisher Publisher
		{
			get
			{
				return base.GetElement<Publisher>(33);
			}
			set
			{
				base.SetElement<Publisher>(33, value);
			}
		}

		// Token: 0x170069EF RID: 27119
		// (get) Token: 0x06014B9C RID: 84892 RVA: 0x003161B4 File Offset: 0x003143B4
		// (set) Token: 0x06014B9D RID: 84893 RVA: 0x003161BE File Offset: 0x003143BE
		public RecordingNumber RecordingNumber
		{
			get
			{
				return base.GetElement<RecordingNumber>(34);
			}
			set
			{
				base.SetElement<RecordingNumber>(34, value);
			}
		}

		// Token: 0x170069F0 RID: 27120
		// (get) Token: 0x06014B9E RID: 84894 RVA: 0x003161C9 File Offset: 0x003143C9
		// (set) Token: 0x06014B9F RID: 84895 RVA: 0x003161D3 File Offset: 0x003143D3
		public ReferenceOrder ReferenceOrder
		{
			get
			{
				return base.GetElement<ReferenceOrder>(35);
			}
			set
			{
				base.SetElement<ReferenceOrder>(35, value);
			}
		}

		// Token: 0x170069F1 RID: 27121
		// (get) Token: 0x06014BA0 RID: 84896 RVA: 0x003161DE File Offset: 0x003143DE
		// (set) Token: 0x06014BA1 RID: 84897 RVA: 0x003161E8 File Offset: 0x003143E8
		public Reporter Reporter
		{
			get
			{
				return base.GetElement<Reporter>(36);
			}
			set
			{
				base.SetElement<Reporter>(36, value);
			}
		}

		// Token: 0x170069F2 RID: 27122
		// (get) Token: 0x06014BA2 RID: 84898 RVA: 0x003161F3 File Offset: 0x003143F3
		// (set) Token: 0x06014BA3 RID: 84899 RVA: 0x003161FD File Offset: 0x003143FD
		public SourceType SourceType
		{
			get
			{
				return base.GetElement<SourceType>(37);
			}
			set
			{
				base.SetElement<SourceType>(37, value);
			}
		}

		// Token: 0x170069F3 RID: 27123
		// (get) Token: 0x06014BA4 RID: 84900 RVA: 0x00316208 File Offset: 0x00314408
		// (set) Token: 0x06014BA5 RID: 84901 RVA: 0x00316212 File Offset: 0x00314412
		public ShortTitle ShortTitle
		{
			get
			{
				return base.GetElement<ShortTitle>(38);
			}
			set
			{
				base.SetElement<ShortTitle>(38, value);
			}
		}

		// Token: 0x170069F4 RID: 27124
		// (get) Token: 0x06014BA6 RID: 84902 RVA: 0x0031621D File Offset: 0x0031441D
		// (set) Token: 0x06014BA7 RID: 84903 RVA: 0x00316227 File Offset: 0x00314427
		public StandardNumber StandardNumber
		{
			get
			{
				return base.GetElement<StandardNumber>(39);
			}
			set
			{
				base.SetElement<StandardNumber>(39, value);
			}
		}

		// Token: 0x170069F5 RID: 27125
		// (get) Token: 0x06014BA8 RID: 84904 RVA: 0x00316232 File Offset: 0x00314432
		// (set) Token: 0x06014BA9 RID: 84905 RVA: 0x0031623C File Offset: 0x0031443C
		public StateProvince StateProvince
		{
			get
			{
				return base.GetElement<StateProvince>(40);
			}
			set
			{
				base.SetElement<StateProvince>(40, value);
			}
		}

		// Token: 0x170069F6 RID: 27126
		// (get) Token: 0x06014BAA RID: 84906 RVA: 0x00316247 File Offset: 0x00314447
		// (set) Token: 0x06014BAB RID: 84907 RVA: 0x00316251 File Offset: 0x00314451
		public Station Station
		{
			get
			{
				return base.GetElement<Station>(41);
			}
			set
			{
				base.SetElement<Station>(41, value);
			}
		}

		// Token: 0x170069F7 RID: 27127
		// (get) Token: 0x06014BAC RID: 84908 RVA: 0x0031625C File Offset: 0x0031445C
		// (set) Token: 0x06014BAD RID: 84909 RVA: 0x00316266 File Offset: 0x00314466
		public Tag Tag
		{
			get
			{
				return base.GetElement<Tag>(42);
			}
			set
			{
				base.SetElement<Tag>(42, value);
			}
		}

		// Token: 0x170069F8 RID: 27128
		// (get) Token: 0x06014BAE RID: 84910 RVA: 0x00316271 File Offset: 0x00314471
		// (set) Token: 0x06014BAF RID: 84911 RVA: 0x0031627B File Offset: 0x0031447B
		public Theater Theater
		{
			get
			{
				return base.GetElement<Theater>(43);
			}
			set
			{
				base.SetElement<Theater>(43, value);
			}
		}

		// Token: 0x170069F9 RID: 27129
		// (get) Token: 0x06014BB0 RID: 84912 RVA: 0x00316286 File Offset: 0x00314486
		// (set) Token: 0x06014BB1 RID: 84913 RVA: 0x00316290 File Offset: 0x00314490
		public ThesisType ThesisType
		{
			get
			{
				return base.GetElement<ThesisType>(44);
			}
			set
			{
				base.SetElement<ThesisType>(44, value);
			}
		}

		// Token: 0x170069FA RID: 27130
		// (get) Token: 0x06014BB2 RID: 84914 RVA: 0x0031629B File Offset: 0x0031449B
		// (set) Token: 0x06014BB3 RID: 84915 RVA: 0x003162A5 File Offset: 0x003144A5
		public Title Title
		{
			get
			{
				return base.GetElement<Title>(45);
			}
			set
			{
				base.SetElement<Title>(45, value);
			}
		}

		// Token: 0x170069FB RID: 27131
		// (get) Token: 0x06014BB4 RID: 84916 RVA: 0x003162B0 File Offset: 0x003144B0
		// (set) Token: 0x06014BB5 RID: 84917 RVA: 0x003162BA File Offset: 0x003144BA
		public PatentType PatentType
		{
			get
			{
				return base.GetElement<PatentType>(46);
			}
			set
			{
				base.SetElement<PatentType>(46, value);
			}
		}

		// Token: 0x170069FC RID: 27132
		// (get) Token: 0x06014BB6 RID: 84918 RVA: 0x003162C5 File Offset: 0x003144C5
		// (set) Token: 0x06014BB7 RID: 84919 RVA: 0x003162CF File Offset: 0x003144CF
		public UrlString UrlString
		{
			get
			{
				return base.GetElement<UrlString>(47);
			}
			set
			{
				base.SetElement<UrlString>(47, value);
			}
		}

		// Token: 0x170069FD RID: 27133
		// (get) Token: 0x06014BB8 RID: 84920 RVA: 0x003162DA File Offset: 0x003144DA
		// (set) Token: 0x06014BB9 RID: 84921 RVA: 0x003162E4 File Offset: 0x003144E4
		public Version Version
		{
			get
			{
				return base.GetElement<Version>(48);
			}
			set
			{
				base.SetElement<Version>(48, value);
			}
		}

		// Token: 0x170069FE RID: 27134
		// (get) Token: 0x06014BBA RID: 84922 RVA: 0x003162EF File Offset: 0x003144EF
		// (set) Token: 0x06014BBB RID: 84923 RVA: 0x003162F9 File Offset: 0x003144F9
		public Volume Volume
		{
			get
			{
				return base.GetElement<Volume>(49);
			}
			set
			{
				base.SetElement<Volume>(49, value);
			}
		}

		// Token: 0x170069FF RID: 27135
		// (get) Token: 0x06014BBC RID: 84924 RVA: 0x00316304 File Offset: 0x00314504
		// (set) Token: 0x06014BBD RID: 84925 RVA: 0x0031630E File Offset: 0x0031450E
		public Year Year
		{
			get
			{
				return base.GetElement<Year>(50);
			}
			set
			{
				base.SetElement<Year>(50, value);
			}
		}

		// Token: 0x17006A00 RID: 27136
		// (get) Token: 0x06014BBE RID: 84926 RVA: 0x00316319 File Offset: 0x00314519
		// (set) Token: 0x06014BBF RID: 84927 RVA: 0x00316323 File Offset: 0x00314523
		public YearAccessed YearAccessed
		{
			get
			{
				return base.GetElement<YearAccessed>(51);
			}
			set
			{
				base.SetElement<YearAccessed>(51, value);
			}
		}

		// Token: 0x06014BC0 RID: 84928 RVA: 0x0031632E File Offset: 0x0031452E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Source>(deep);
		}

		// Token: 0x04008F95 RID: 36757
		private const string tagName = "Source";

		// Token: 0x04008F96 RID: 36758
		private const byte tagNsId = 9;

		// Token: 0x04008F97 RID: 36759
		internal const int ElementTypeIdConst = 10833;

		// Token: 0x04008F98 RID: 36760
		private static readonly string[] eleTagNames = new string[]
		{
			"AbbreviatedCaseNumber", "AlbumTitle", "Author", "BookTitle", "Broadcaster", "BroadcastTitle", "CaseNumber", "ChapterNumber", "City", "Comments",
			"ConferenceName", "CountryRegion", "Court", "Day", "DayAccessed", "Department", "Distributor", "Edition", "Guid", "Institution",
			"InternetSiteTitle", "Issue", "JournalName", "LCID", "Medium", "Month", "MonthAccessed", "NumberVolumes", "Pages", "PatentNumber",
			"PeriodicalTitle", "ProductionCompany", "PublicationTitle", "Publisher", "RecordingNumber", "RefOrder", "Reporter", "SourceType", "ShortTitle", "StandardNumber",
			"StateProvince", "Station", "Tag", "Theater", "ThesisType", "Title", "Type", "URL", "Version", "Volume",
			"Year", "YearAccessed"
		};

		// Token: 0x04008F99 RID: 36761
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9
		};
	}
}
