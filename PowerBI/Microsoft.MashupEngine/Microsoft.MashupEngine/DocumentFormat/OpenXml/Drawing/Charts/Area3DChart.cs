using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025E1 RID: 9697
	[ChildElementInfo(typeof(AreaChartSeries))]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(DropLines))]
	[ChildElementInfo(typeof(GapDepth))]
	[ChildElementInfo(typeof(AxisId))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(VaryColors))]
	[ChildElementInfo(typeof(Grouping))]
	internal class Area3DChart : OpenXmlCompositeElement
	{
		// Token: 0x1700588B RID: 22667
		// (get) Token: 0x060123FC RID: 74748 RVA: 0x002F7F14 File Offset: 0x002F6114
		public override string LocalName
		{
			get
			{
				return "area3DChart";
			}
		}

		// Token: 0x1700588C RID: 22668
		// (get) Token: 0x060123FD RID: 74749 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700588D RID: 22669
		// (get) Token: 0x060123FE RID: 74750 RVA: 0x002F7F1B File Offset: 0x002F611B
		internal override int ElementTypeId
		{
			get
			{
				return 10542;
			}
		}

		// Token: 0x060123FF RID: 74751 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012400 RID: 74752 RVA: 0x00293ECF File Offset: 0x002920CF
		public Area3DChart()
		{
		}

		// Token: 0x06012401 RID: 74753 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Area3DChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012402 RID: 74754 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Area3DChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012403 RID: 74755 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Area3DChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012404 RID: 74756 RVA: 0x002F7F24 File Offset: 0x002F6124
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
				return new AreaChartSeries();
			}
			if (11 == namespaceId && "dLbls" == name)
			{
				return new DataLabels();
			}
			if (11 == namespaceId && "dropLines" == name)
			{
				return new DropLines();
			}
			if (11 == namespaceId && "gapDepth" == name)
			{
				return new GapDepth();
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

		// Token: 0x1700588E RID: 22670
		// (get) Token: 0x06012405 RID: 74757 RVA: 0x002F7FF2 File Offset: 0x002F61F2
		internal override string[] ElementTagNames
		{
			get
			{
				return Area3DChart.eleTagNames;
			}
		}

		// Token: 0x1700588F RID: 22671
		// (get) Token: 0x06012406 RID: 74758 RVA: 0x002F7FF9 File Offset: 0x002F61F9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Area3DChart.eleNamespaceIds;
			}
		}

		// Token: 0x17005890 RID: 22672
		// (get) Token: 0x06012407 RID: 74759 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005891 RID: 22673
		// (get) Token: 0x06012408 RID: 74760 RVA: 0x002F7E7C File Offset: 0x002F607C
		// (set) Token: 0x06012409 RID: 74761 RVA: 0x002F7E85 File Offset: 0x002F6085
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

		// Token: 0x17005892 RID: 22674
		// (get) Token: 0x0601240A RID: 74762 RVA: 0x002F7E8F File Offset: 0x002F608F
		// (set) Token: 0x0601240B RID: 74763 RVA: 0x002F7E98 File Offset: 0x002F6098
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

		// Token: 0x0601240C RID: 74764 RVA: 0x002F8000 File Offset: 0x002F6200
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Area3DChart>(deep);
		}

		// Token: 0x04007ED4 RID: 32468
		private const string tagName = "area3DChart";

		// Token: 0x04007ED5 RID: 32469
		private const byte tagNsId = 11;

		// Token: 0x04007ED6 RID: 32470
		internal const int ElementTypeIdConst = 10542;

		// Token: 0x04007ED7 RID: 32471
		private static readonly string[] eleTagNames = new string[] { "grouping", "varyColors", "ser", "dLbls", "dropLines", "gapDepth", "axId", "extLst" };

		// Token: 0x04007ED8 RID: 32472
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11, 11, 11 };
	}
}
