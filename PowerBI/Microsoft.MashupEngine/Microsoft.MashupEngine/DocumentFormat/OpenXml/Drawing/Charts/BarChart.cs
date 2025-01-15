using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025EA RID: 9706
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AxisId))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(BarDirection))]
	[ChildElementInfo(typeof(BarGrouping))]
	[ChildElementInfo(typeof(VaryColors))]
	[ChildElementInfo(typeof(BarChartSeries))]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(GapWidth))]
	[ChildElementInfo(typeof(Overlap))]
	[ChildElementInfo(typeof(SeriesLines))]
	internal class BarChart : OpenXmlCompositeElement
	{
		// Token: 0x170058CB RID: 22731
		// (get) Token: 0x06012490 RID: 74896 RVA: 0x002F8A30 File Offset: 0x002F6C30
		public override string LocalName
		{
			get
			{
				return "barChart";
			}
		}

		// Token: 0x170058CC RID: 22732
		// (get) Token: 0x06012491 RID: 74897 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170058CD RID: 22733
		// (get) Token: 0x06012492 RID: 74898 RVA: 0x002F8A37 File Offset: 0x002F6C37
		internal override int ElementTypeId
		{
			get
			{
				return 10551;
			}
		}

		// Token: 0x06012493 RID: 74899 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012494 RID: 74900 RVA: 0x00293ECF File Offset: 0x002920CF
		public BarChart()
		{
		}

		// Token: 0x06012495 RID: 74901 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BarChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012496 RID: 74902 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BarChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012497 RID: 74903 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BarChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012498 RID: 74904 RVA: 0x002F8A40 File Offset: 0x002F6C40
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "barDir" == name)
			{
				return new BarDirection();
			}
			if (11 == namespaceId && "grouping" == name)
			{
				return new BarGrouping();
			}
			if (11 == namespaceId && "varyColors" == name)
			{
				return new VaryColors();
			}
			if (11 == namespaceId && "ser" == name)
			{
				return new BarChartSeries();
			}
			if (11 == namespaceId && "dLbls" == name)
			{
				return new DataLabels();
			}
			if (11 == namespaceId && "gapWidth" == name)
			{
				return new GapWidth();
			}
			if (11 == namespaceId && "overlap" == name)
			{
				return new Overlap();
			}
			if (11 == namespaceId && "serLines" == name)
			{
				return new SeriesLines();
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

		// Token: 0x170058CE RID: 22734
		// (get) Token: 0x06012499 RID: 74905 RVA: 0x002F8B3E File Offset: 0x002F6D3E
		internal override string[] ElementTagNames
		{
			get
			{
				return BarChart.eleTagNames;
			}
		}

		// Token: 0x170058CF RID: 22735
		// (get) Token: 0x0601249A RID: 74906 RVA: 0x002F8B45 File Offset: 0x002F6D45
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BarChart.eleNamespaceIds;
			}
		}

		// Token: 0x170058D0 RID: 22736
		// (get) Token: 0x0601249B RID: 74907 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170058D1 RID: 22737
		// (get) Token: 0x0601249C RID: 74908 RVA: 0x002F8B4C File Offset: 0x002F6D4C
		// (set) Token: 0x0601249D RID: 74909 RVA: 0x002F8B55 File Offset: 0x002F6D55
		public BarDirection BarDirection
		{
			get
			{
				return base.GetElement<BarDirection>(0);
			}
			set
			{
				base.SetElement<BarDirection>(0, value);
			}
		}

		// Token: 0x170058D2 RID: 22738
		// (get) Token: 0x0601249E RID: 74910 RVA: 0x002F8B5F File Offset: 0x002F6D5F
		// (set) Token: 0x0601249F RID: 74911 RVA: 0x002F8B68 File Offset: 0x002F6D68
		public BarGrouping BarGrouping
		{
			get
			{
				return base.GetElement<BarGrouping>(1);
			}
			set
			{
				base.SetElement<BarGrouping>(1, value);
			}
		}

		// Token: 0x170058D3 RID: 22739
		// (get) Token: 0x060124A0 RID: 74912 RVA: 0x002F8B72 File Offset: 0x002F6D72
		// (set) Token: 0x060124A1 RID: 74913 RVA: 0x002F8B7B File Offset: 0x002F6D7B
		public VaryColors VaryColors
		{
			get
			{
				return base.GetElement<VaryColors>(2);
			}
			set
			{
				base.SetElement<VaryColors>(2, value);
			}
		}

		// Token: 0x060124A2 RID: 74914 RVA: 0x002F8B85 File Offset: 0x002F6D85
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BarChart>(deep);
		}

		// Token: 0x04007EFF RID: 32511
		private const string tagName = "barChart";

		// Token: 0x04007F00 RID: 32512
		private const byte tagNsId = 11;

		// Token: 0x04007F01 RID: 32513
		internal const int ElementTypeIdConst = 10551;

		// Token: 0x04007F02 RID: 32514
		private static readonly string[] eleTagNames = new string[] { "barDir", "grouping", "varyColors", "ser", "dLbls", "gapWidth", "overlap", "serLines", "axId", "extLst" };

		// Token: 0x04007F03 RID: 32515
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11, 11, 11, 11, 11 };
	}
}
