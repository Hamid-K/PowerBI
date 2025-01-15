using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025E3 RID: 9699
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GapDepth))]
	[ChildElementInfo(typeof(AxisId))]
	[ChildElementInfo(typeof(Grouping))]
	[ChildElementInfo(typeof(VaryColors))]
	[ChildElementInfo(typeof(LineChartSeries))]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(DropLines))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class Line3DChart : OpenXmlCompositeElement
	{
		// Token: 0x1700589B RID: 22683
		// (get) Token: 0x06012420 RID: 74784 RVA: 0x002F8248 File Offset: 0x002F6448
		public override string LocalName
		{
			get
			{
				return "line3DChart";
			}
		}

		// Token: 0x1700589C RID: 22684
		// (get) Token: 0x06012421 RID: 74785 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700589D RID: 22685
		// (get) Token: 0x06012422 RID: 74786 RVA: 0x002F824F File Offset: 0x002F644F
		internal override int ElementTypeId
		{
			get
			{
				return 10544;
			}
		}

		// Token: 0x06012423 RID: 74787 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012424 RID: 74788 RVA: 0x00293ECF File Offset: 0x002920CF
		public Line3DChart()
		{
		}

		// Token: 0x06012425 RID: 74789 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Line3DChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012426 RID: 74790 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Line3DChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012427 RID: 74791 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Line3DChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012428 RID: 74792 RVA: 0x002F8258 File Offset: 0x002F6458
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

		// Token: 0x1700589E RID: 22686
		// (get) Token: 0x06012429 RID: 74793 RVA: 0x002F8326 File Offset: 0x002F6526
		internal override string[] ElementTagNames
		{
			get
			{
				return Line3DChart.eleTagNames;
			}
		}

		// Token: 0x1700589F RID: 22687
		// (get) Token: 0x0601242A RID: 74794 RVA: 0x002F832D File Offset: 0x002F652D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Line3DChart.eleNamespaceIds;
			}
		}

		// Token: 0x170058A0 RID: 22688
		// (get) Token: 0x0601242B RID: 74795 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170058A1 RID: 22689
		// (get) Token: 0x0601242C RID: 74796 RVA: 0x002F7E7C File Offset: 0x002F607C
		// (set) Token: 0x0601242D RID: 74797 RVA: 0x002F7E85 File Offset: 0x002F6085
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

		// Token: 0x170058A2 RID: 22690
		// (get) Token: 0x0601242E RID: 74798 RVA: 0x002F7E8F File Offset: 0x002F608F
		// (set) Token: 0x0601242F RID: 74799 RVA: 0x002F7E98 File Offset: 0x002F6098
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

		// Token: 0x06012430 RID: 74800 RVA: 0x002F8334 File Offset: 0x002F6534
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Line3DChart>(deep);
		}

		// Token: 0x04007EDE RID: 32478
		private const string tagName = "line3DChart";

		// Token: 0x04007EDF RID: 32479
		private const byte tagNsId = 11;

		// Token: 0x04007EE0 RID: 32480
		internal const int ElementTypeIdConst = 10544;

		// Token: 0x04007EE1 RID: 32481
		private static readonly string[] eleTagNames = new string[] { "grouping", "varyColors", "ser", "dLbls", "dropLines", "gapDepth", "axId", "extLst" };

		// Token: 0x04007EE2 RID: 32482
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11, 11, 11 };
	}
}
