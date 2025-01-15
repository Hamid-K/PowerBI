using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025E9 RID: 9705
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(PieChartSeries))]
	[ChildElementInfo(typeof(FirstSliceAngle))]
	[ChildElementInfo(typeof(VaryColors))]
	[ChildElementInfo(typeof(HoleSize))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DoughnutChart : OpenXmlCompositeElement
	{
		// Token: 0x170058C4 RID: 22724
		// (get) Token: 0x06012480 RID: 74880 RVA: 0x002F8908 File Offset: 0x002F6B08
		public override string LocalName
		{
			get
			{
				return "doughnutChart";
			}
		}

		// Token: 0x170058C5 RID: 22725
		// (get) Token: 0x06012481 RID: 74881 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170058C6 RID: 22726
		// (get) Token: 0x06012482 RID: 74882 RVA: 0x002F890F File Offset: 0x002F6B0F
		internal override int ElementTypeId
		{
			get
			{
				return 10550;
			}
		}

		// Token: 0x06012483 RID: 74883 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012484 RID: 74884 RVA: 0x00293ECF File Offset: 0x002920CF
		public DoughnutChart()
		{
		}

		// Token: 0x06012485 RID: 74885 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DoughnutChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012486 RID: 74886 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DoughnutChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012487 RID: 74887 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DoughnutChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012488 RID: 74888 RVA: 0x002F8918 File Offset: 0x002F6B18
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
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
			if (11 == namespaceId && "firstSliceAng" == name)
			{
				return new FirstSliceAngle();
			}
			if (11 == namespaceId && "holeSize" == name)
			{
				return new HoleSize();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170058C7 RID: 22727
		// (get) Token: 0x06012489 RID: 74889 RVA: 0x002F89B6 File Offset: 0x002F6BB6
		internal override string[] ElementTagNames
		{
			get
			{
				return DoughnutChart.eleTagNames;
			}
		}

		// Token: 0x170058C8 RID: 22728
		// (get) Token: 0x0601248A RID: 74890 RVA: 0x002F89BD File Offset: 0x002F6BBD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DoughnutChart.eleNamespaceIds;
			}
		}

		// Token: 0x170058C9 RID: 22729
		// (get) Token: 0x0601248B RID: 74891 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170058CA RID: 22730
		// (get) Token: 0x0601248C RID: 74892 RVA: 0x002F87AC File Offset: 0x002F69AC
		// (set) Token: 0x0601248D RID: 74893 RVA: 0x002F87B5 File Offset: 0x002F69B5
		public VaryColors VaryColors
		{
			get
			{
				return base.GetElement<VaryColors>(0);
			}
			set
			{
				base.SetElement<VaryColors>(0, value);
			}
		}

		// Token: 0x0601248E RID: 74894 RVA: 0x002F89C4 File Offset: 0x002F6BC4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoughnutChart>(deep);
		}

		// Token: 0x04007EFA RID: 32506
		private const string tagName = "doughnutChart";

		// Token: 0x04007EFB RID: 32507
		private const byte tagNsId = 11;

		// Token: 0x04007EFC RID: 32508
		internal const int ElementTypeIdConst = 10550;

		// Token: 0x04007EFD RID: 32509
		private static readonly string[] eleTagNames = new string[] { "varyColors", "ser", "dLbls", "firstSliceAng", "holeSize", "extLst" };

		// Token: 0x04007EFE RID: 32510
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11 };
	}
}
