using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025EB RID: 9707
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(AxisId))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BarGrouping))]
	[ChildElementInfo(typeof(VaryColors))]
	[ChildElementInfo(typeof(BarChartSeries))]
	[ChildElementInfo(typeof(BarDirection))]
	[ChildElementInfo(typeof(GapWidth))]
	[ChildElementInfo(typeof(GapDepth))]
	[ChildElementInfo(typeof(Shape))]
	internal class Bar3DChart : OpenXmlCompositeElement
	{
		// Token: 0x170058D4 RID: 22740
		// (get) Token: 0x060124A4 RID: 74916 RVA: 0x002F8C13 File Offset: 0x002F6E13
		public override string LocalName
		{
			get
			{
				return "bar3DChart";
			}
		}

		// Token: 0x170058D5 RID: 22741
		// (get) Token: 0x060124A5 RID: 74917 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170058D6 RID: 22742
		// (get) Token: 0x060124A6 RID: 74918 RVA: 0x002F8C1A File Offset: 0x002F6E1A
		internal override int ElementTypeId
		{
			get
			{
				return 10552;
			}
		}

		// Token: 0x060124A7 RID: 74919 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060124A8 RID: 74920 RVA: 0x00293ECF File Offset: 0x002920CF
		public Bar3DChart()
		{
		}

		// Token: 0x060124A9 RID: 74921 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Bar3DChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060124AA RID: 74922 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Bar3DChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060124AB RID: 74923 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Bar3DChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060124AC RID: 74924 RVA: 0x002F8C24 File Offset: 0x002F6E24
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
			if (11 == namespaceId && "gapDepth" == name)
			{
				return new GapDepth();
			}
			if (11 == namespaceId && "shape" == name)
			{
				return new Shape();
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

		// Token: 0x170058D7 RID: 22743
		// (get) Token: 0x060124AD RID: 74925 RVA: 0x002F8D22 File Offset: 0x002F6F22
		internal override string[] ElementTagNames
		{
			get
			{
				return Bar3DChart.eleTagNames;
			}
		}

		// Token: 0x170058D8 RID: 22744
		// (get) Token: 0x060124AE RID: 74926 RVA: 0x002F8D29 File Offset: 0x002F6F29
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Bar3DChart.eleNamespaceIds;
			}
		}

		// Token: 0x170058D9 RID: 22745
		// (get) Token: 0x060124AF RID: 74927 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170058DA RID: 22746
		// (get) Token: 0x060124B0 RID: 74928 RVA: 0x002F8B4C File Offset: 0x002F6D4C
		// (set) Token: 0x060124B1 RID: 74929 RVA: 0x002F8B55 File Offset: 0x002F6D55
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

		// Token: 0x170058DB RID: 22747
		// (get) Token: 0x060124B2 RID: 74930 RVA: 0x002F8B5F File Offset: 0x002F6D5F
		// (set) Token: 0x060124B3 RID: 74931 RVA: 0x002F8B68 File Offset: 0x002F6D68
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

		// Token: 0x170058DC RID: 22748
		// (get) Token: 0x060124B4 RID: 74932 RVA: 0x002F8B72 File Offset: 0x002F6D72
		// (set) Token: 0x060124B5 RID: 74933 RVA: 0x002F8B7B File Offset: 0x002F6D7B
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

		// Token: 0x060124B6 RID: 74934 RVA: 0x002F8D30 File Offset: 0x002F6F30
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Bar3DChart>(deep);
		}

		// Token: 0x04007F04 RID: 32516
		private const string tagName = "bar3DChart";

		// Token: 0x04007F05 RID: 32517
		private const byte tagNsId = 11;

		// Token: 0x04007F06 RID: 32518
		internal const int ElementTypeIdConst = 10552;

		// Token: 0x04007F07 RID: 32519
		private static readonly string[] eleTagNames = new string[] { "barDir", "grouping", "varyColors", "ser", "dLbls", "gapWidth", "gapDepth", "shape", "axId", "extLst" };

		// Token: 0x04007F08 RID: 32520
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11, 11, 11, 11, 11 };
	}
}
