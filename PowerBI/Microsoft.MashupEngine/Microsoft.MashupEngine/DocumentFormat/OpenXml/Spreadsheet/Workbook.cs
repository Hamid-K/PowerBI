using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B2A RID: 11050
	[ChildElementInfo(typeof(SmartTagTypes))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(WorkbookExtensionList))]
	[ChildElementInfo(typeof(ExternalReferences))]
	[ChildElementInfo(typeof(FileVersion))]
	[ChildElementInfo(typeof(FileSharing))]
	[ChildElementInfo(typeof(WorkbookProperties))]
	[ChildElementInfo(typeof(WorkbookProtection))]
	[ChildElementInfo(typeof(BookViews))]
	[ChildElementInfo(typeof(Sheets))]
	[ChildElementInfo(typeof(FunctionGroups))]
	[ChildElementInfo(typeof(CustomWorkbookViews))]
	[ChildElementInfo(typeof(DefinedNames))]
	[ChildElementInfo(typeof(CalculationProperties))]
	[ChildElementInfo(typeof(OleSize))]
	[ChildElementInfo(typeof(SmartTagProperties))]
	[ChildElementInfo(typeof(PivotCaches))]
	[ChildElementInfo(typeof(WebPublishing))]
	[ChildElementInfo(typeof(FileRecoveryProperties))]
	[ChildElementInfo(typeof(WebPublishObjects))]
	internal class Workbook : OpenXmlPartRootElement
	{
		// Token: 0x17007723 RID: 30499
		// (get) Token: 0x06016969 RID: 92521 RVA: 0x002A7490 File Offset: 0x002A5690
		public override string LocalName
		{
			get
			{
				return "workbook";
			}
		}

		// Token: 0x17007724 RID: 30500
		// (get) Token: 0x0601696A RID: 92522 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007725 RID: 30501
		// (get) Token: 0x0601696B RID: 92523 RVA: 0x0032CC4C File Offset: 0x0032AE4C
		internal override int ElementTypeId
		{
			get
			{
				return 11048;
			}
		}

		// Token: 0x0601696C RID: 92524 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601696D RID: 92525 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Workbook(WorkbookPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x0601696E RID: 92526 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(WorkbookPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17007726 RID: 30502
		// (get) Token: 0x0601696F RID: 92527 RVA: 0x0032CC53 File Offset: 0x0032AE53
		// (set) Token: 0x06016970 RID: 92528 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public WorkbookPart WorkbookPart
		{
			get
			{
				return base.OpenXmlPart as WorkbookPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06016971 RID: 92529 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Workbook(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016972 RID: 92530 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Workbook(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016973 RID: 92531 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Workbook(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016974 RID: 92532 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Workbook()
		{
		}

		// Token: 0x06016975 RID: 92533 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(WorkbookPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06016976 RID: 92534 RVA: 0x0032CC60 File Offset: 0x0032AE60
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "fileVersion" == name)
			{
				return new FileVersion();
			}
			if (22 == namespaceId && "fileSharing" == name)
			{
				return new FileSharing();
			}
			if (22 == namespaceId && "workbookPr" == name)
			{
				return new WorkbookProperties();
			}
			if (22 == namespaceId && "workbookProtection" == name)
			{
				return new WorkbookProtection();
			}
			if (22 == namespaceId && "bookViews" == name)
			{
				return new BookViews();
			}
			if (22 == namespaceId && "sheets" == name)
			{
				return new Sheets();
			}
			if (22 == namespaceId && "functionGroups" == name)
			{
				return new FunctionGroups();
			}
			if (22 == namespaceId && "externalReferences" == name)
			{
				return new ExternalReferences();
			}
			if (22 == namespaceId && "definedNames" == name)
			{
				return new DefinedNames();
			}
			if (22 == namespaceId && "calcPr" == name)
			{
				return new CalculationProperties();
			}
			if (22 == namespaceId && "oleSize" == name)
			{
				return new OleSize();
			}
			if (22 == namespaceId && "customWorkbookViews" == name)
			{
				return new CustomWorkbookViews();
			}
			if (22 == namespaceId && "pivotCaches" == name)
			{
				return new PivotCaches();
			}
			if (22 == namespaceId && "smartTagPr" == name)
			{
				return new SmartTagProperties();
			}
			if (22 == namespaceId && "smartTagTypes" == name)
			{
				return new SmartTagTypes();
			}
			if (22 == namespaceId && "webPublishing" == name)
			{
				return new WebPublishing();
			}
			if (22 == namespaceId && "fileRecoveryPr" == name)
			{
				return new FileRecoveryProperties();
			}
			if (22 == namespaceId && "webPublishObjects" == name)
			{
				return new WebPublishObjects();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new WorkbookExtensionList();
			}
			return null;
		}

		// Token: 0x17007727 RID: 30503
		// (get) Token: 0x06016977 RID: 92535 RVA: 0x0032CE36 File Offset: 0x0032B036
		internal override string[] ElementTagNames
		{
			get
			{
				return Workbook.eleTagNames;
			}
		}

		// Token: 0x17007728 RID: 30504
		// (get) Token: 0x06016978 RID: 92536 RVA: 0x0032CE3D File Offset: 0x0032B03D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Workbook.eleNamespaceIds;
			}
		}

		// Token: 0x17007729 RID: 30505
		// (get) Token: 0x06016979 RID: 92537 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700772A RID: 30506
		// (get) Token: 0x0601697A RID: 92538 RVA: 0x0032CE44 File Offset: 0x0032B044
		// (set) Token: 0x0601697B RID: 92539 RVA: 0x0032CE4D File Offset: 0x0032B04D
		public FileVersion FileVersion
		{
			get
			{
				return base.GetElement<FileVersion>(0);
			}
			set
			{
				base.SetElement<FileVersion>(0, value);
			}
		}

		// Token: 0x1700772B RID: 30507
		// (get) Token: 0x0601697C RID: 92540 RVA: 0x0032CE57 File Offset: 0x0032B057
		// (set) Token: 0x0601697D RID: 92541 RVA: 0x0032CE60 File Offset: 0x0032B060
		public FileSharing FileSharing
		{
			get
			{
				return base.GetElement<FileSharing>(1);
			}
			set
			{
				base.SetElement<FileSharing>(1, value);
			}
		}

		// Token: 0x1700772C RID: 30508
		// (get) Token: 0x0601697E RID: 92542 RVA: 0x0032CE6A File Offset: 0x0032B06A
		// (set) Token: 0x0601697F RID: 92543 RVA: 0x0032CE73 File Offset: 0x0032B073
		public WorkbookProperties WorkbookProperties
		{
			get
			{
				return base.GetElement<WorkbookProperties>(2);
			}
			set
			{
				base.SetElement<WorkbookProperties>(2, value);
			}
		}

		// Token: 0x1700772D RID: 30509
		// (get) Token: 0x06016980 RID: 92544 RVA: 0x0032CE7D File Offset: 0x0032B07D
		// (set) Token: 0x06016981 RID: 92545 RVA: 0x0032CE86 File Offset: 0x0032B086
		public WorkbookProtection WorkbookProtection
		{
			get
			{
				return base.GetElement<WorkbookProtection>(3);
			}
			set
			{
				base.SetElement<WorkbookProtection>(3, value);
			}
		}

		// Token: 0x1700772E RID: 30510
		// (get) Token: 0x06016982 RID: 92546 RVA: 0x0032CE90 File Offset: 0x0032B090
		// (set) Token: 0x06016983 RID: 92547 RVA: 0x0032CE99 File Offset: 0x0032B099
		public BookViews BookViews
		{
			get
			{
				return base.GetElement<BookViews>(4);
			}
			set
			{
				base.SetElement<BookViews>(4, value);
			}
		}

		// Token: 0x1700772F RID: 30511
		// (get) Token: 0x06016984 RID: 92548 RVA: 0x0032CEA3 File Offset: 0x0032B0A3
		// (set) Token: 0x06016985 RID: 92549 RVA: 0x0032CEAC File Offset: 0x0032B0AC
		public Sheets Sheets
		{
			get
			{
				return base.GetElement<Sheets>(5);
			}
			set
			{
				base.SetElement<Sheets>(5, value);
			}
		}

		// Token: 0x17007730 RID: 30512
		// (get) Token: 0x06016986 RID: 92550 RVA: 0x0032CEB6 File Offset: 0x0032B0B6
		// (set) Token: 0x06016987 RID: 92551 RVA: 0x0032CEBF File Offset: 0x0032B0BF
		public FunctionGroups FunctionGroups
		{
			get
			{
				return base.GetElement<FunctionGroups>(6);
			}
			set
			{
				base.SetElement<FunctionGroups>(6, value);
			}
		}

		// Token: 0x17007731 RID: 30513
		// (get) Token: 0x06016988 RID: 92552 RVA: 0x0032CEC9 File Offset: 0x0032B0C9
		// (set) Token: 0x06016989 RID: 92553 RVA: 0x0032CED2 File Offset: 0x0032B0D2
		public ExternalReferences ExternalReferences
		{
			get
			{
				return base.GetElement<ExternalReferences>(7);
			}
			set
			{
				base.SetElement<ExternalReferences>(7, value);
			}
		}

		// Token: 0x17007732 RID: 30514
		// (get) Token: 0x0601698A RID: 92554 RVA: 0x0032CEDC File Offset: 0x0032B0DC
		// (set) Token: 0x0601698B RID: 92555 RVA: 0x0032CEE5 File Offset: 0x0032B0E5
		public DefinedNames DefinedNames
		{
			get
			{
				return base.GetElement<DefinedNames>(8);
			}
			set
			{
				base.SetElement<DefinedNames>(8, value);
			}
		}

		// Token: 0x17007733 RID: 30515
		// (get) Token: 0x0601698C RID: 92556 RVA: 0x0032CEEF File Offset: 0x0032B0EF
		// (set) Token: 0x0601698D RID: 92557 RVA: 0x0032CEF9 File Offset: 0x0032B0F9
		public CalculationProperties CalculationProperties
		{
			get
			{
				return base.GetElement<CalculationProperties>(9);
			}
			set
			{
				base.SetElement<CalculationProperties>(9, value);
			}
		}

		// Token: 0x17007734 RID: 30516
		// (get) Token: 0x0601698E RID: 92558 RVA: 0x0032CF04 File Offset: 0x0032B104
		// (set) Token: 0x0601698F RID: 92559 RVA: 0x0032CF0E File Offset: 0x0032B10E
		public OleSize OleSize
		{
			get
			{
				return base.GetElement<OleSize>(10);
			}
			set
			{
				base.SetElement<OleSize>(10, value);
			}
		}

		// Token: 0x17007735 RID: 30517
		// (get) Token: 0x06016990 RID: 92560 RVA: 0x0032CF19 File Offset: 0x0032B119
		// (set) Token: 0x06016991 RID: 92561 RVA: 0x0032CF23 File Offset: 0x0032B123
		public CustomWorkbookViews CustomWorkbookViews
		{
			get
			{
				return base.GetElement<CustomWorkbookViews>(11);
			}
			set
			{
				base.SetElement<CustomWorkbookViews>(11, value);
			}
		}

		// Token: 0x17007736 RID: 30518
		// (get) Token: 0x06016992 RID: 92562 RVA: 0x0032CF2E File Offset: 0x0032B12E
		// (set) Token: 0x06016993 RID: 92563 RVA: 0x0032CF38 File Offset: 0x0032B138
		public PivotCaches PivotCaches
		{
			get
			{
				return base.GetElement<PivotCaches>(12);
			}
			set
			{
				base.SetElement<PivotCaches>(12, value);
			}
		}

		// Token: 0x17007737 RID: 30519
		// (get) Token: 0x06016994 RID: 92564 RVA: 0x0032CF43 File Offset: 0x0032B143
		// (set) Token: 0x06016995 RID: 92565 RVA: 0x0032CF4D File Offset: 0x0032B14D
		public SmartTagProperties SmartTagProperties
		{
			get
			{
				return base.GetElement<SmartTagProperties>(13);
			}
			set
			{
				base.SetElement<SmartTagProperties>(13, value);
			}
		}

		// Token: 0x17007738 RID: 30520
		// (get) Token: 0x06016996 RID: 92566 RVA: 0x0032CF58 File Offset: 0x0032B158
		// (set) Token: 0x06016997 RID: 92567 RVA: 0x0032CF62 File Offset: 0x0032B162
		public SmartTagTypes SmartTagTypes
		{
			get
			{
				return base.GetElement<SmartTagTypes>(14);
			}
			set
			{
				base.SetElement<SmartTagTypes>(14, value);
			}
		}

		// Token: 0x17007739 RID: 30521
		// (get) Token: 0x06016998 RID: 92568 RVA: 0x0032CF6D File Offset: 0x0032B16D
		// (set) Token: 0x06016999 RID: 92569 RVA: 0x0032CF77 File Offset: 0x0032B177
		public WebPublishing WebPublishing
		{
			get
			{
				return base.GetElement<WebPublishing>(15);
			}
			set
			{
				base.SetElement<WebPublishing>(15, value);
			}
		}

		// Token: 0x0601699A RID: 92570 RVA: 0x0032CF82 File Offset: 0x0032B182
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Workbook>(deep);
		}

		// Token: 0x0400992F RID: 39215
		private const string tagName = "workbook";

		// Token: 0x04009930 RID: 39216
		private const byte tagNsId = 22;

		// Token: 0x04009931 RID: 39217
		internal const int ElementTypeIdConst = 11048;

		// Token: 0x04009932 RID: 39218
		private static readonly string[] eleTagNames = new string[]
		{
			"fileVersion", "fileSharing", "workbookPr", "workbookProtection", "bookViews", "sheets", "functionGroups", "externalReferences", "definedNames", "calcPr",
			"oleSize", "customWorkbookViews", "pivotCaches", "smartTagPr", "smartTagTypes", "webPublishing", "fileRecoveryPr", "webPublishObjects", "extLst"
		};

		// Token: 0x04009933 RID: 39219
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 22, 22, 22, 22, 22, 22, 22
		};
	}
}
