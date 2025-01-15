using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025E2 RID: 9698
	[ChildElementInfo(typeof(ShowMarker))]
	[ChildElementInfo(typeof(Grouping))]
	[ChildElementInfo(typeof(Smooth))]
	[ChildElementInfo(typeof(AxisId))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(VaryColors))]
	[ChildElementInfo(typeof(LineChartSeries))]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(DropLines))]
	[ChildElementInfo(typeof(HighLowLines))]
	[ChildElementInfo(typeof(UpDownBars))]
	internal class LineChart : OpenXmlCompositeElement
	{
		// Token: 0x17005893 RID: 22675
		// (get) Token: 0x0601240E RID: 74766 RVA: 0x002F807C File Offset: 0x002F627C
		public override string LocalName
		{
			get
			{
				return "lineChart";
			}
		}

		// Token: 0x17005894 RID: 22676
		// (get) Token: 0x0601240F RID: 74767 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005895 RID: 22677
		// (get) Token: 0x06012410 RID: 74768 RVA: 0x002F8083 File Offset: 0x002F6283
		internal override int ElementTypeId
		{
			get
			{
				return 10543;
			}
		}

		// Token: 0x06012411 RID: 74769 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012412 RID: 74770 RVA: 0x00293ECF File Offset: 0x002920CF
		public LineChart()
		{
		}

		// Token: 0x06012413 RID: 74771 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LineChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012414 RID: 74772 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LineChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012415 RID: 74773 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LineChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012416 RID: 74774 RVA: 0x002F808C File Offset: 0x002F628C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "grouping" == name)
			{
				return new Grouping();
			}
			if (11 == namespaceId && "varyColors" == name)
			{
				return new VaryColors();
			}
			if (11 == namespaceId && "ser" == name)
			{
				return new LineChartSeries();
			}
			if (11 == namespaceId && "dLbls" == name)
			{
				return new DataLabels();
			}
			if (11 == namespaceId && "dropLines" == name)
			{
				return new DropLines();
			}
			if (11 == namespaceId && "hiLowLines" == name)
			{
				return new HighLowLines();
			}
			if (11 == namespaceId && "upDownBars" == name)
			{
				return new UpDownBars();
			}
			if (11 == namespaceId && "marker" == name)
			{
				return new ShowMarker();
			}
			if (11 == namespaceId && "smooth" == name)
			{
				return new Smooth();
			}
			if (11 == namespaceId && "axId" == name)
			{
				return new AxisId();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005896 RID: 22678
		// (get) Token: 0x06012417 RID: 74775 RVA: 0x002F81A2 File Offset: 0x002F63A2
		internal override string[] ElementTagNames
		{
			get
			{
				return LineChart.eleTagNames;
			}
		}

		// Token: 0x17005897 RID: 22679
		// (get) Token: 0x06012418 RID: 74776 RVA: 0x002F81A9 File Offset: 0x002F63A9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return LineChart.eleNamespaceIds;
			}
		}

		// Token: 0x17005898 RID: 22680
		// (get) Token: 0x06012419 RID: 74777 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005899 RID: 22681
		// (get) Token: 0x0601241A RID: 74778 RVA: 0x002F7E7C File Offset: 0x002F607C
		// (set) Token: 0x0601241B RID: 74779 RVA: 0x002F7E85 File Offset: 0x002F6085
		public Grouping Grouping
		{
			get
			{
				return base.GetElement<Grouping>(0);
			}
			set
			{
				base.SetElement<Grouping>(0, value);
			}
		}

		// Token: 0x1700589A RID: 22682
		// (get) Token: 0x0601241C RID: 74780 RVA: 0x002F7E8F File Offset: 0x002F608F
		// (set) Token: 0x0601241D RID: 74781 RVA: 0x002F7E98 File Offset: 0x002F6098
		public VaryColors VaryColors
		{
			get
			{
				return base.GetElement<VaryColors>(1);
			}
			set
			{
				base.SetElement<VaryColors>(1, value);
			}
		}

		// Token: 0x0601241E RID: 74782 RVA: 0x002F81B0 File Offset: 0x002F63B0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LineChart>(deep);
		}

		// Token: 0x04007ED9 RID: 32473
		private const string tagName = "lineChart";

		// Token: 0x04007EDA RID: 32474
		private const byte tagNsId = 11;

		// Token: 0x04007EDB RID: 32475
		internal const int ElementTypeIdConst = 10543;

		// Token: 0x04007EDC RID: 32476
		private static readonly string[] eleTagNames = new string[]
		{
			"grouping", "varyColors", "ser", "dLbls", "dropLines", "hiLowLines", "upDownBars", "marker", "smooth", "axId",
			"extLst"
		};

		// Token: 0x04007EDD RID: 32477
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11
		};
	}
}
