using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025E5 RID: 9701
	[ChildElementInfo(typeof(RadarChartSeries))]
	[ChildElementInfo(typeof(VaryColors))]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(AxisId))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RadarStyle))]
	internal class RadarChart : OpenXmlCompositeElement
	{
		// Token: 0x170058A6 RID: 22694
		// (get) Token: 0x0601243C RID: 74812 RVA: 0x002F8497 File Offset: 0x002F6697
		public override string LocalName
		{
			get
			{
				return "radarChart";
			}
		}

		// Token: 0x170058A7 RID: 22695
		// (get) Token: 0x0601243D RID: 74813 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170058A8 RID: 22696
		// (get) Token: 0x0601243E RID: 74814 RVA: 0x002F849E File Offset: 0x002F669E
		internal override int ElementTypeId
		{
			get
			{
				return 10546;
			}
		}

		// Token: 0x0601243F RID: 74815 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012440 RID: 74816 RVA: 0x00293ECF File Offset: 0x002920CF
		public RadarChart()
		{
		}

		// Token: 0x06012441 RID: 74817 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RadarChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012442 RID: 74818 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RadarChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012443 RID: 74819 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RadarChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012444 RID: 74820 RVA: 0x002F84A8 File Offset: 0x002F66A8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "radarStyle" == name)
			{
				return new RadarStyle();
			}
			if (11 == namespaceId && "varyColors" == name)
			{
				return new VaryColors();
			}
			if (11 == namespaceId && "ser" == name)
			{
				return new RadarChartSeries();
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

		// Token: 0x170058A9 RID: 22697
		// (get) Token: 0x06012445 RID: 74821 RVA: 0x002F8546 File Offset: 0x002F6746
		internal override string[] ElementTagNames
		{
			get
			{
				return RadarChart.eleTagNames;
			}
		}

		// Token: 0x170058AA RID: 22698
		// (get) Token: 0x06012446 RID: 74822 RVA: 0x002F854D File Offset: 0x002F674D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RadarChart.eleNamespaceIds;
			}
		}

		// Token: 0x170058AB RID: 22699
		// (get) Token: 0x06012447 RID: 74823 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170058AC RID: 22700
		// (get) Token: 0x06012448 RID: 74824 RVA: 0x002F8554 File Offset: 0x002F6754
		// (set) Token: 0x06012449 RID: 74825 RVA: 0x002F855D File Offset: 0x002F675D
		public RadarStyle RadarStyle
		{
			get
			{
				return base.GetElement<RadarStyle>(0);
			}
			set
			{
				base.SetElement<RadarStyle>(0, value);
			}
		}

		// Token: 0x170058AD RID: 22701
		// (get) Token: 0x0601244A RID: 74826 RVA: 0x002F7E8F File Offset: 0x002F608F
		// (set) Token: 0x0601244B RID: 74827 RVA: 0x002F7E98 File Offset: 0x002F6098
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

		// Token: 0x0601244C RID: 74828 RVA: 0x002F8567 File Offset: 0x002F6767
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RadarChart>(deep);
		}

		// Token: 0x04007EE6 RID: 32486
		private const string tagName = "radarChart";

		// Token: 0x04007EE7 RID: 32487
		private const byte tagNsId = 11;

		// Token: 0x04007EE8 RID: 32488
		internal const int ElementTypeIdConst = 10546;

		// Token: 0x04007EE9 RID: 32489
		private static readonly string[] eleTagNames = new string[] { "radarStyle", "varyColors", "ser", "dLbls", "axId", "extLst" };

		// Token: 0x04007EEA RID: 32490
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11 };
	}
}
