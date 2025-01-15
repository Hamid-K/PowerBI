using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025E4 RID: 9700
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LineChartSeries))]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(DropLines))]
	[ChildElementInfo(typeof(HighLowLines))]
	[ChildElementInfo(typeof(UpDownBars))]
	[ChildElementInfo(typeof(AxisId))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(VaryColors))]
	internal class StockChart : OpenXmlCompositeElement
	{
		// Token: 0x170058A3 RID: 22691
		// (get) Token: 0x06012432 RID: 74802 RVA: 0x002F83B0 File Offset: 0x002F65B0
		public override string LocalName
		{
			get
			{
				return "stockChart";
			}
		}

		// Token: 0x170058A4 RID: 22692
		// (get) Token: 0x06012433 RID: 74803 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170058A5 RID: 22693
		// (get) Token: 0x06012434 RID: 74804 RVA: 0x002F83B7 File Offset: 0x002F65B7
		internal override int ElementTypeId
		{
			get
			{
				return 10545;
			}
		}

		// Token: 0x06012435 RID: 74805 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012436 RID: 74806 RVA: 0x00293ECF File Offset: 0x002920CF
		public StockChart()
		{
		}

		// Token: 0x06012437 RID: 74807 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StockChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012438 RID: 74808 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StockChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012439 RID: 74809 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StockChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601243A RID: 74810 RVA: 0x002F83C0 File Offset: 0x002F65C0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
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
			if (11 == namespaceId && "axId" == name)
			{
				return new AxisId();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			if (11 == namespaceId && "varyColors" == name)
			{
				return new VaryColors();
			}
			return null;
		}

		// Token: 0x0601243B RID: 74811 RVA: 0x002F848E File Offset: 0x002F668E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StockChart>(deep);
		}

		// Token: 0x04007EE3 RID: 32483
		private const string tagName = "stockChart";

		// Token: 0x04007EE4 RID: 32484
		private const byte tagNsId = 11;

		// Token: 0x04007EE5 RID: 32485
		internal const int ElementTypeIdConst = 10545;
	}
}
