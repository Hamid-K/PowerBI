using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025EC RID: 9708
	[ChildElementInfo(typeof(SeriesLines))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(OfPieType))]
	[ChildElementInfo(typeof(VaryColors))]
	[ChildElementInfo(typeof(PieChartSeries))]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(GapWidth))]
	[ChildElementInfo(typeof(SplitType))]
	[ChildElementInfo(typeof(SplitPosition))]
	[ChildElementInfo(typeof(CustomSplit))]
	[ChildElementInfo(typeof(SecondPieSize))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class OfPieChart : OpenXmlCompositeElement
	{
		// Token: 0x170058DD RID: 22749
		// (get) Token: 0x060124B8 RID: 74936 RVA: 0x002F8DBF File Offset: 0x002F6FBF
		public override string LocalName
		{
			get
			{
				return "ofPieChart";
			}
		}

		// Token: 0x170058DE RID: 22750
		// (get) Token: 0x060124B9 RID: 74937 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170058DF RID: 22751
		// (get) Token: 0x060124BA RID: 74938 RVA: 0x002F8DC6 File Offset: 0x002F6FC6
		internal override int ElementTypeId
		{
			get
			{
				return 10553;
			}
		}

		// Token: 0x060124BB RID: 74939 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060124BC RID: 74940 RVA: 0x00293ECF File Offset: 0x002920CF
		public OfPieChart()
		{
		}

		// Token: 0x060124BD RID: 74941 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OfPieChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060124BE RID: 74942 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OfPieChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060124BF RID: 74943 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OfPieChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060124C0 RID: 74944 RVA: 0x002F8DD0 File Offset: 0x002F6FD0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "ofPieType" == name)
			{
				return new OfPieType();
			}
			if (11 == namespaceId && "varyColors" == name)
			{
				return new VaryColors();
			}
			if (11 == namespaceId && "ser" == name)
			{
				return new PieChartSeries();
			}
			if (11 == namespaceId && "dLbls" == name)
			{
				return new DataLabels();
			}
			if (11 == namespaceId && "gapWidth" == name)
			{
				return new GapWidth();
			}
			if (11 == namespaceId && "splitType" == name)
			{
				return new SplitType();
			}
			if (11 == namespaceId && "splitPos" == name)
			{
				return new SplitPosition();
			}
			if (11 == namespaceId && "custSplit" == name)
			{
				return new CustomSplit();
			}
			if (11 == namespaceId && "secondPieSize" == name)
			{
				return new SecondPieSize();
			}
			if (11 == namespaceId && "serLines" == name)
			{
				return new SeriesLines();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170058E0 RID: 22752
		// (get) Token: 0x060124C1 RID: 74945 RVA: 0x002F8EE6 File Offset: 0x002F70E6
		internal override string[] ElementTagNames
		{
			get
			{
				return OfPieChart.eleTagNames;
			}
		}

		// Token: 0x170058E1 RID: 22753
		// (get) Token: 0x060124C2 RID: 74946 RVA: 0x002F8EED File Offset: 0x002F70ED
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return OfPieChart.eleNamespaceIds;
			}
		}

		// Token: 0x170058E2 RID: 22754
		// (get) Token: 0x060124C3 RID: 74947 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170058E3 RID: 22755
		// (get) Token: 0x060124C4 RID: 74948 RVA: 0x002F8EF4 File Offset: 0x002F70F4
		// (set) Token: 0x060124C5 RID: 74949 RVA: 0x002F8EFD File Offset: 0x002F70FD
		public OfPieType OfPieType
		{
			get
			{
				return base.GetElement<OfPieType>(0);
			}
			set
			{
				base.SetElement<OfPieType>(0, value);
			}
		}

		// Token: 0x170058E4 RID: 22756
		// (get) Token: 0x060124C6 RID: 74950 RVA: 0x002F7E8F File Offset: 0x002F608F
		// (set) Token: 0x060124C7 RID: 74951 RVA: 0x002F7E98 File Offset: 0x002F6098
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

		// Token: 0x060124C8 RID: 74952 RVA: 0x002F8F07 File Offset: 0x002F7107
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OfPieChart>(deep);
		}

		// Token: 0x04007F09 RID: 32521
		private const string tagName = "ofPieChart";

		// Token: 0x04007F0A RID: 32522
		private const byte tagNsId = 11;

		// Token: 0x04007F0B RID: 32523
		internal const int ElementTypeIdConst = 10553;

		// Token: 0x04007F0C RID: 32524
		private static readonly string[] eleTagNames = new string[]
		{
			"ofPieType", "varyColors", "ser", "dLbls", "gapWidth", "splitType", "splitPos", "custSplit", "secondPieSize", "serLines",
			"extLst"
		};

		// Token: 0x04007F0D RID: 32525
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11
		};
	}
}
