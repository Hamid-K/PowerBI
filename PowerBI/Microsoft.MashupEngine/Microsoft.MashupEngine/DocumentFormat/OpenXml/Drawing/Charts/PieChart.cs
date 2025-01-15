using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025E7 RID: 9703
	[ChildElementInfo(typeof(VaryColors))]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(FirstSliceAngle))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PieChartSeries))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class PieChart : OpenXmlCompositeElement
	{
		// Token: 0x170058B6 RID: 22710
		// (get) Token: 0x06012460 RID: 74848 RVA: 0x002F8708 File Offset: 0x002F6908
		public override string LocalName
		{
			get
			{
				return "pieChart";
			}
		}

		// Token: 0x170058B7 RID: 22711
		// (get) Token: 0x06012461 RID: 74849 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170058B8 RID: 22712
		// (get) Token: 0x06012462 RID: 74850 RVA: 0x002F870F File Offset: 0x002F690F
		internal override int ElementTypeId
		{
			get
			{
				return 10548;
			}
		}

		// Token: 0x06012463 RID: 74851 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012464 RID: 74852 RVA: 0x00293ECF File Offset: 0x002920CF
		public PieChart()
		{
		}

		// Token: 0x06012465 RID: 74853 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PieChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012466 RID: 74854 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PieChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012467 RID: 74855 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PieChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012468 RID: 74856 RVA: 0x002F8718 File Offset: 0x002F6918
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
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170058B9 RID: 22713
		// (get) Token: 0x06012469 RID: 74857 RVA: 0x002F879E File Offset: 0x002F699E
		internal override string[] ElementTagNames
		{
			get
			{
				return PieChart.eleTagNames;
			}
		}

		// Token: 0x170058BA RID: 22714
		// (get) Token: 0x0601246A RID: 74858 RVA: 0x002F87A5 File Offset: 0x002F69A5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PieChart.eleNamespaceIds;
			}
		}

		// Token: 0x170058BB RID: 22715
		// (get) Token: 0x0601246B RID: 74859 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170058BC RID: 22716
		// (get) Token: 0x0601246C RID: 74860 RVA: 0x002F87AC File Offset: 0x002F69AC
		// (set) Token: 0x0601246D RID: 74861 RVA: 0x002F87B5 File Offset: 0x002F69B5
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

		// Token: 0x0601246E RID: 74862 RVA: 0x002F87BF File Offset: 0x002F69BF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PieChart>(deep);
		}

		// Token: 0x04007EF0 RID: 32496
		private const string tagName = "pieChart";

		// Token: 0x04007EF1 RID: 32497
		private const byte tagNsId = 11;

		// Token: 0x04007EF2 RID: 32498
		internal const int ElementTypeIdConst = 10548;

		// Token: 0x04007EF3 RID: 32499
		private static readonly string[] eleTagNames = new string[] { "varyColors", "ser", "dLbls", "firstSliceAng", "extLst" };

		// Token: 0x04007EF4 RID: 32500
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11 };
	}
}
