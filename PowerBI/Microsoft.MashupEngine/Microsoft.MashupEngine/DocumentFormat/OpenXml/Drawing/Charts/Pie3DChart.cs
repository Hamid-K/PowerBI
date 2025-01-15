using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025E8 RID: 9704
	[ChildElementInfo(typeof(DataLabels))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(VaryColors))]
	[ChildElementInfo(typeof(PieChartSeries))]
	internal class Pie3DChart : OpenXmlCompositeElement
	{
		// Token: 0x170058BD RID: 22717
		// (get) Token: 0x06012470 RID: 74864 RVA: 0x002F8820 File Offset: 0x002F6A20
		public override string LocalName
		{
			get
			{
				return "pie3DChart";
			}
		}

		// Token: 0x170058BE RID: 22718
		// (get) Token: 0x06012471 RID: 74865 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170058BF RID: 22719
		// (get) Token: 0x06012472 RID: 74866 RVA: 0x002F8827 File Offset: 0x002F6A27
		internal override int ElementTypeId
		{
			get
			{
				return 10549;
			}
		}

		// Token: 0x06012473 RID: 74867 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012474 RID: 74868 RVA: 0x00293ECF File Offset: 0x002920CF
		public Pie3DChart()
		{
		}

		// Token: 0x06012475 RID: 74869 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Pie3DChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012476 RID: 74870 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Pie3DChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012477 RID: 74871 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Pie3DChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012478 RID: 74872 RVA: 0x002F8830 File Offset: 0x002F6A30
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
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170058C0 RID: 22720
		// (get) Token: 0x06012479 RID: 74873 RVA: 0x002F889E File Offset: 0x002F6A9E
		internal override string[] ElementTagNames
		{
			get
			{
				return Pie3DChart.eleTagNames;
			}
		}

		// Token: 0x170058C1 RID: 22721
		// (get) Token: 0x0601247A RID: 74874 RVA: 0x002F88A5 File Offset: 0x002F6AA5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Pie3DChart.eleNamespaceIds;
			}
		}

		// Token: 0x170058C2 RID: 22722
		// (get) Token: 0x0601247B RID: 74875 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170058C3 RID: 22723
		// (get) Token: 0x0601247C RID: 74876 RVA: 0x002F87AC File Offset: 0x002F69AC
		// (set) Token: 0x0601247D RID: 74877 RVA: 0x002F87B5 File Offset: 0x002F69B5
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

		// Token: 0x0601247E RID: 74878 RVA: 0x002F88AC File Offset: 0x002F6AAC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Pie3DChart>(deep);
		}

		// Token: 0x04007EF5 RID: 32501
		private const string tagName = "pie3DChart";

		// Token: 0x04007EF6 RID: 32502
		private const byte tagNsId = 11;

		// Token: 0x04007EF7 RID: 32503
		internal const int ElementTypeIdConst = 10549;

		// Token: 0x04007EF8 RID: 32504
		private static readonly string[] eleTagNames = new string[] { "varyColors", "ser", "dLbls", "extLst" };

		// Token: 0x04007EF9 RID: 32505
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11 };
	}
}
