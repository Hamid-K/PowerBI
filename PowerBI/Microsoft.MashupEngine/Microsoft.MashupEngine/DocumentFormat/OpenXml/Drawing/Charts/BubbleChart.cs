using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025EF RID: 9711
	[ChildElementInfo(typeof(VaryColors))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BubbleChartSeries))]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(BubbleScale))]
	[ChildElementInfo(typeof(ShowNegativeBubbles))]
	[ChildElementInfo(typeof(SizeRepresents))]
	[ChildElementInfo(typeof(AxisId))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class BubbleChart : OpenXmlCompositeElement
	{
		// Token: 0x170058F4 RID: 22772
		// (get) Token: 0x060124EC RID: 74988 RVA: 0x002F91FC File Offset: 0x002F73FC
		public override string LocalName
		{
			get
			{
				return "bubbleChart";
			}
		}

		// Token: 0x170058F5 RID: 22773
		// (get) Token: 0x060124ED RID: 74989 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170058F6 RID: 22774
		// (get) Token: 0x060124EE RID: 74990 RVA: 0x002F9203 File Offset: 0x002F7403
		internal override int ElementTypeId
		{
			get
			{
				return 10556;
			}
		}

		// Token: 0x060124EF RID: 74991 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060124F0 RID: 74992 RVA: 0x00293ECF File Offset: 0x002920CF
		public BubbleChart()
		{
		}

		// Token: 0x060124F1 RID: 74993 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BubbleChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060124F2 RID: 74994 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BubbleChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060124F3 RID: 74995 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BubbleChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060124F4 RID: 74996 RVA: 0x002F920C File Offset: 0x002F740C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "varyColors" == name)
			{
				return new VaryColors();
			}
			if (11 == namespaceId && "ser" == name)
			{
				return new BubbleChartSeries();
			}
			if (11 == namespaceId && "dLbls" == name)
			{
				return new DataLabels();
			}
			if (11 == namespaceId && "bubbleScale" == name)
			{
				return new BubbleScale();
			}
			if (11 == namespaceId && "showNegBubbles" == name)
			{
				return new ShowNegativeBubbles();
			}
			if (11 == namespaceId && "sizeRepresents" == name)
			{
				return new SizeRepresents();
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

		// Token: 0x170058F7 RID: 22775
		// (get) Token: 0x060124F5 RID: 74997 RVA: 0x002F92DA File Offset: 0x002F74DA
		internal override string[] ElementTagNames
		{
			get
			{
				return BubbleChart.eleTagNames;
			}
		}

		// Token: 0x170058F8 RID: 22776
		// (get) Token: 0x060124F6 RID: 74998 RVA: 0x002F92E1 File Offset: 0x002F74E1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BubbleChart.eleNamespaceIds;
			}
		}

		// Token: 0x170058F9 RID: 22777
		// (get) Token: 0x060124F7 RID: 74999 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170058FA RID: 22778
		// (get) Token: 0x060124F8 RID: 75000 RVA: 0x002F87AC File Offset: 0x002F69AC
		// (set) Token: 0x060124F9 RID: 75001 RVA: 0x002F87B5 File Offset: 0x002F69B5
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

		// Token: 0x060124FA RID: 75002 RVA: 0x002F92E8 File Offset: 0x002F74E8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BubbleChart>(deep);
		}

		// Token: 0x04007F18 RID: 32536
		private const string tagName = "bubbleChart";

		// Token: 0x04007F19 RID: 32537
		private const byte tagNsId = 11;

		// Token: 0x04007F1A RID: 32538
		internal const int ElementTypeIdConst = 10556;

		// Token: 0x04007F1B RID: 32539
		private static readonly string[] eleTagNames = new string[] { "varyColors", "ser", "dLbls", "bubbleScale", "showNegBubbles", "sizeRepresents", "axId", "extLst" };

		// Token: 0x04007F1C RID: 32540
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11, 11, 11 };
	}
}
