using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025E0 RID: 9696
	[ChildElementInfo(typeof(VaryColors))]
	[ChildElementInfo(typeof(Grouping))]
	[ChildElementInfo(typeof(DropLines))]
	[ChildElementInfo(typeof(AxisId))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(AreaChartSeries))]
	internal class AreaChart : OpenXmlCompositeElement
	{
		// Token: 0x17005883 RID: 22659
		// (get) Token: 0x060123EA RID: 74730 RVA: 0x002F7DAA File Offset: 0x002F5FAA
		public override string LocalName
		{
			get
			{
				return "areaChart";
			}
		}

		// Token: 0x17005884 RID: 22660
		// (get) Token: 0x060123EB RID: 74731 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005885 RID: 22661
		// (get) Token: 0x060123EC RID: 74732 RVA: 0x002F7DB1 File Offset: 0x002F5FB1
		internal override int ElementTypeId
		{
			get
			{
				return 10541;
			}
		}

		// Token: 0x060123ED RID: 74733 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060123EE RID: 74734 RVA: 0x00293ECF File Offset: 0x002920CF
		public AreaChart()
		{
		}

		// Token: 0x060123EF RID: 74735 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AreaChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060123F0 RID: 74736 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AreaChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060123F1 RID: 74737 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AreaChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060123F2 RID: 74738 RVA: 0x002F7DB8 File Offset: 0x002F5FB8
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

		// Token: 0x17005886 RID: 22662
		// (get) Token: 0x060123F3 RID: 74739 RVA: 0x002F7E6E File Offset: 0x002F606E
		internal override string[] ElementTagNames
		{
			get
			{
				return AreaChart.eleTagNames;
			}
		}

		// Token: 0x17005887 RID: 22663
		// (get) Token: 0x060123F4 RID: 74740 RVA: 0x002F7E75 File Offset: 0x002F6075
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AreaChart.eleNamespaceIds;
			}
		}

		// Token: 0x17005888 RID: 22664
		// (get) Token: 0x060123F5 RID: 74741 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005889 RID: 22665
		// (get) Token: 0x060123F6 RID: 74742 RVA: 0x002F7E7C File Offset: 0x002F607C
		// (set) Token: 0x060123F7 RID: 74743 RVA: 0x002F7E85 File Offset: 0x002F6085
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

		// Token: 0x1700588A RID: 22666
		// (get) Token: 0x060123F8 RID: 74744 RVA: 0x002F7E8F File Offset: 0x002F608F
		// (set) Token: 0x060123F9 RID: 74745 RVA: 0x002F7E98 File Offset: 0x002F6098
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

		// Token: 0x060123FA RID: 74746 RVA: 0x002F7EA2 File Offset: 0x002F60A2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AreaChart>(deep);
		}

		// Token: 0x04007ECF RID: 32463
		private const string tagName = "areaChart";

		// Token: 0x04007ED0 RID: 32464
		private const byte tagNsId = 11;

		// Token: 0x04007ED1 RID: 32465
		internal const int ElementTypeIdConst = 10541;

		// Token: 0x04007ED2 RID: 32466
		private static readonly string[] eleTagNames = new string[] { "grouping", "varyColors", "ser", "dLbls", "dropLines", "axId", "extLst" };

		// Token: 0x04007ED3 RID: 32467
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11, 11 };
	}
}
