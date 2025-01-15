using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing.Charts;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002568 RID: 9576
	[ChildElementInfo(typeof(PivotSource))]
	[ChildElementInfo(typeof(TextProperties))]
	[ChildElementInfo(typeof(ExternalData))]
	[ChildElementInfo(typeof(PrintSettings))]
	[ChildElementInfo(typeof(UserShapesReference))]
	[ChildElementInfo(typeof(ChartSpaceExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Date1904))]
	[ChildElementInfo(typeof(EditingLanguage))]
	[ChildElementInfo(typeof(RoundedCorners))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(Style))]
	[ChildElementInfo(typeof(ColorMapOverride))]
	[ChildElementInfo(typeof(Style), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Protection))]
	[ChildElementInfo(typeof(Chart))]
	internal class ChartSpace : OpenXmlPartRootElement
	{
		// Token: 0x170055B4 RID: 21940
		// (get) Token: 0x06011D9D RID: 73117 RVA: 0x002F2FED File Offset: 0x002F11ED
		public override string LocalName
		{
			get
			{
				return "chartSpace";
			}
		}

		// Token: 0x170055B5 RID: 21941
		// (get) Token: 0x06011D9E RID: 73118 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055B6 RID: 21942
		// (get) Token: 0x06011D9F RID: 73119 RVA: 0x002F2FF4 File Offset: 0x002F11F4
		internal override int ElementTypeId
		{
			get
			{
				return 10386;
			}
		}

		// Token: 0x06011DA0 RID: 73120 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011DA1 RID: 73121 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal ChartSpace(ChartPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06011DA2 RID: 73122 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(ChartPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170055B7 RID: 21943
		// (get) Token: 0x06011DA3 RID: 73123 RVA: 0x002F2FFB File Offset: 0x002F11FB
		// (set) Token: 0x06011DA4 RID: 73124 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public ChartPart ChartPart
		{
			get
			{
				return base.OpenXmlPart as ChartPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06011DA5 RID: 73125 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public ChartSpace(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011DA6 RID: 73126 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public ChartSpace(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011DA7 RID: 73127 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public ChartSpace(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011DA8 RID: 73128 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public ChartSpace()
		{
		}

		// Token: 0x06011DA9 RID: 73129 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(ChartPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06011DAA RID: 73130 RVA: 0x002F3008 File Offset: 0x002F1208
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "date1904" == name)
			{
				return new Date1904();
			}
			if (11 == namespaceId && "lang" == name)
			{
				return new EditingLanguage();
			}
			if (11 == namespaceId && "roundedCorners" == name)
			{
				return new RoundedCorners();
			}
			if (46 == namespaceId && "style" == name)
			{
				return new Style();
			}
			if (11 == namespaceId && "style" == name)
			{
				return new Style();
			}
			if (11 == namespaceId && "clrMapOvr" == name)
			{
				return new ColorMapOverride();
			}
			if (11 == namespaceId && "pivotSource" == name)
			{
				return new PivotSource();
			}
			if (11 == namespaceId && "protection" == name)
			{
				return new Protection();
			}
			if (11 == namespaceId && "chart" == name)
			{
				return new Chart();
			}
			if (11 == namespaceId && "spPr" == name)
			{
				return new ChartShapeProperties();
			}
			if (11 == namespaceId && "txPr" == name)
			{
				return new TextProperties();
			}
			if (11 == namespaceId && "externalData" == name)
			{
				return new ExternalData();
			}
			if (11 == namespaceId && "printSettings" == name)
			{
				return new PrintSettings();
			}
			if (11 == namespaceId && "userShapes" == name)
			{
				return new UserShapesReference();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ChartSpaceExtensionList();
			}
			return null;
		}

		// Token: 0x170055B8 RID: 21944
		// (get) Token: 0x06011DAB RID: 73131 RVA: 0x002F317E File Offset: 0x002F137E
		internal override string[] ElementTagNames
		{
			get
			{
				return ChartSpace.eleTagNames;
			}
		}

		// Token: 0x170055B9 RID: 21945
		// (get) Token: 0x06011DAC RID: 73132 RVA: 0x002F3185 File Offset: 0x002F1385
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ChartSpace.eleNamespaceIds;
			}
		}

		// Token: 0x170055BA RID: 21946
		// (get) Token: 0x06011DAD RID: 73133 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170055BB RID: 21947
		// (get) Token: 0x06011DAE RID: 73134 RVA: 0x002F318C File Offset: 0x002F138C
		// (set) Token: 0x06011DAF RID: 73135 RVA: 0x002F3195 File Offset: 0x002F1395
		public Date1904 Date1904
		{
			get
			{
				return base.GetElement<Date1904>(0);
			}
			set
			{
				base.SetElement<Date1904>(0, value);
			}
		}

		// Token: 0x170055BC RID: 21948
		// (get) Token: 0x06011DB0 RID: 73136 RVA: 0x002F319F File Offset: 0x002F139F
		// (set) Token: 0x06011DB1 RID: 73137 RVA: 0x002F31A8 File Offset: 0x002F13A8
		public EditingLanguage EditingLanguage
		{
			get
			{
				return base.GetElement<EditingLanguage>(1);
			}
			set
			{
				base.SetElement<EditingLanguage>(1, value);
			}
		}

		// Token: 0x170055BD RID: 21949
		// (get) Token: 0x06011DB2 RID: 73138 RVA: 0x002F31B2 File Offset: 0x002F13B2
		// (set) Token: 0x06011DB3 RID: 73139 RVA: 0x002F31BB File Offset: 0x002F13BB
		public RoundedCorners RoundedCorners
		{
			get
			{
				return base.GetElement<RoundedCorners>(2);
			}
			set
			{
				base.SetElement<RoundedCorners>(2, value);
			}
		}

		// Token: 0x06011DB4 RID: 73140 RVA: 0x002F31C5 File Offset: 0x002F13C5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChartSpace>(deep);
		}

		// Token: 0x04007CE8 RID: 31976
		private const string tagName = "chartSpace";

		// Token: 0x04007CE9 RID: 31977
		private const byte tagNsId = 11;

		// Token: 0x04007CEA RID: 31978
		internal const int ElementTypeIdConst = 10386;

		// Token: 0x04007CEB RID: 31979
		private static readonly string[] eleTagNames = new string[]
		{
			"date1904", "lang", "roundedCorners", "style", "style", "clrMapOvr", "pivotSource", "protection", "chart", "spPr",
			"txPr", "externalData", "printSettings", "userShapes", "extLst"
		};

		// Token: 0x04007CEC RID: 31980
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 46, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 11
		};
	}
}
