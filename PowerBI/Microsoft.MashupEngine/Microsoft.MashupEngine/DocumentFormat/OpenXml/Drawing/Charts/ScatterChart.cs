using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025E6 RID: 9702
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(AxisId))]
	[ChildElementInfo(typeof(ScatterStyle))]
	[ChildElementInfo(typeof(VaryColors))]
	[ChildElementInfo(typeof(ScatterChartSeries))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class ScatterChart : OpenXmlCompositeElement
	{
		// Token: 0x170058AE RID: 22702
		// (get) Token: 0x0601244E RID: 74830 RVA: 0x002F85D0 File Offset: 0x002F67D0
		public override string LocalName
		{
			get
			{
				return "scatterChart";
			}
		}

		// Token: 0x170058AF RID: 22703
		// (get) Token: 0x0601244F RID: 74831 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170058B0 RID: 22704
		// (get) Token: 0x06012450 RID: 74832 RVA: 0x002F85D7 File Offset: 0x002F67D7
		internal override int ElementTypeId
		{
			get
			{
				return 10547;
			}
		}

		// Token: 0x06012451 RID: 74833 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012452 RID: 74834 RVA: 0x00293ECF File Offset: 0x002920CF
		public ScatterChart()
		{
		}

		// Token: 0x06012453 RID: 74835 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ScatterChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012454 RID: 74836 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ScatterChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012455 RID: 74837 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ScatterChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012456 RID: 74838 RVA: 0x002F85E0 File Offset: 0x002F67E0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "scatterStyle" == name)
			{
				return new ScatterStyle();
			}
			if (11 == namespaceId && "varyColors" == name)
			{
				return new VaryColors();
			}
			if (11 == namespaceId && "ser" == name)
			{
				return new ScatterChartSeries();
			}
			if (11 == namespaceId && "dLbls" == name)
			{
				return new DataLabels();
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

		// Token: 0x170058B1 RID: 22705
		// (get) Token: 0x06012457 RID: 74839 RVA: 0x002F867E File Offset: 0x002F687E
		internal override string[] ElementTagNames
		{
			get
			{
				return ScatterChart.eleTagNames;
			}
		}

		// Token: 0x170058B2 RID: 22706
		// (get) Token: 0x06012458 RID: 74840 RVA: 0x002F8685 File Offset: 0x002F6885
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ScatterChart.eleNamespaceIds;
			}
		}

		// Token: 0x170058B3 RID: 22707
		// (get) Token: 0x06012459 RID: 74841 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170058B4 RID: 22708
		// (get) Token: 0x0601245A RID: 74842 RVA: 0x002F868C File Offset: 0x002F688C
		// (set) Token: 0x0601245B RID: 74843 RVA: 0x002F8695 File Offset: 0x002F6895
		public ScatterStyle ScatterStyle
		{
			get
			{
				return base.GetElement<ScatterStyle>(0);
			}
			set
			{
				base.SetElement<ScatterStyle>(0, value);
			}
		}

		// Token: 0x170058B5 RID: 22709
		// (get) Token: 0x0601245C RID: 74844 RVA: 0x002F7E8F File Offset: 0x002F608F
		// (set) Token: 0x0601245D RID: 74845 RVA: 0x002F7E98 File Offset: 0x002F6098
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

		// Token: 0x0601245E RID: 74846 RVA: 0x002F869F File Offset: 0x002F689F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScatterChart>(deep);
		}

		// Token: 0x04007EEB RID: 32491
		private const string tagName = "scatterChart";

		// Token: 0x04007EEC RID: 32492
		private const byte tagNsId = 11;

		// Token: 0x04007EED RID: 32493
		internal const int ElementTypeIdConst = 10547;

		// Token: 0x04007EEE RID: 32494
		private static readonly string[] eleTagNames = new string[] { "scatterStyle", "varyColors", "ser", "dLbls", "axId", "extLst" };

		// Token: 0x04007EEF RID: 32495
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11 };
	}
}
