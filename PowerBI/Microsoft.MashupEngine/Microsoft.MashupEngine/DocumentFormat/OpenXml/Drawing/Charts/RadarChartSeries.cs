using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025DF RID: 9695
	[ChildElementInfo(typeof(SeriesText))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Order))]
	[ChildElementInfo(typeof(Index))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(PictureOptions))]
	[ChildElementInfo(typeof(Marker))]
	[ChildElementInfo(typeof(DataPoint))]
	[ChildElementInfo(typeof(DataLabels))]
	[ChildElementInfo(typeof(CategoryAxisData))]
	[ChildElementInfo(typeof(Values))]
	[ChildElementInfo(typeof(Bubble3D))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(Smooth))]
	internal class RadarChartSeries : OpenXmlCompositeElement
	{
		// Token: 0x17005877 RID: 22647
		// (get) Token: 0x060123D0 RID: 74704 RVA: 0x002F1B23 File Offset: 0x002EFD23
		public override string LocalName
		{
			get
			{
				return "ser";
			}
		}

		// Token: 0x17005878 RID: 22648
		// (get) Token: 0x060123D1 RID: 74705 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005879 RID: 22649
		// (get) Token: 0x060123D2 RID: 74706 RVA: 0x002F7B93 File Offset: 0x002F5D93
		internal override int ElementTypeId
		{
			get
			{
				return 10540;
			}
		}

		// Token: 0x060123D3 RID: 74707 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060123D4 RID: 74708 RVA: 0x00293ECF File Offset: 0x002920CF
		public RadarChartSeries()
		{
		}

		// Token: 0x060123D5 RID: 74709 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RadarChartSeries(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060123D6 RID: 74710 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RadarChartSeries(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060123D7 RID: 74711 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RadarChartSeries(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060123D8 RID: 74712 RVA: 0x002F7B9C File Offset: 0x002F5D9C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "idx" == name)
			{
				return new Index();
			}
			if (11 == namespaceId && "order" == name)
			{
				return new Order();
			}
			if (11 == namespaceId && "tx" == name)
			{
				return new SeriesText();
			}
			if (11 == namespaceId && "spPr" == name)
			{
				return new ChartShapeProperties();
			}
			if (11 == namespaceId && "pictureOptions" == name)
			{
				return new PictureOptions();
			}
			if (11 == namespaceId && "marker" == name)
			{
				return new Marker();
			}
			if (11 == namespaceId && "dPt" == name)
			{
				return new DataPoint();
			}
			if (11 == namespaceId && "dLbls" == name)
			{
				return new DataLabels();
			}
			if (11 == namespaceId && "cat" == name)
			{
				return new CategoryAxisData();
			}
			if (11 == namespaceId && "val" == name)
			{
				return new Values();
			}
			if (11 == namespaceId && "bubble3D" == name)
			{
				return new Bubble3D();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			if (11 == namespaceId && "smooth" == name)
			{
				return new Smooth();
			}
			return null;
		}

		// Token: 0x1700587A RID: 22650
		// (get) Token: 0x060123D9 RID: 74713 RVA: 0x002F7CE2 File Offset: 0x002F5EE2
		internal override string[] ElementTagNames
		{
			get
			{
				return RadarChartSeries.eleTagNames;
			}
		}

		// Token: 0x1700587B RID: 22651
		// (get) Token: 0x060123DA RID: 74714 RVA: 0x002F7CE9 File Offset: 0x002F5EE9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RadarChartSeries.eleNamespaceIds;
			}
		}

		// Token: 0x1700587C RID: 22652
		// (get) Token: 0x060123DB RID: 74715 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700587D RID: 22653
		// (get) Token: 0x060123DC RID: 74716 RVA: 0x002F1CB8 File Offset: 0x002EFEB8
		// (set) Token: 0x060123DD RID: 74717 RVA: 0x002F1CC1 File Offset: 0x002EFEC1
		public Index Index
		{
			get
			{
				return base.GetElement<Index>(0);
			}
			set
			{
				base.SetElement<Index>(0, value);
			}
		}

		// Token: 0x1700587E RID: 22654
		// (get) Token: 0x060123DE RID: 74718 RVA: 0x002F1CCB File Offset: 0x002EFECB
		// (set) Token: 0x060123DF RID: 74719 RVA: 0x002F1CD4 File Offset: 0x002EFED4
		public Order Order
		{
			get
			{
				return base.GetElement<Order>(1);
			}
			set
			{
				base.SetElement<Order>(1, value);
			}
		}

		// Token: 0x1700587F RID: 22655
		// (get) Token: 0x060123E0 RID: 74720 RVA: 0x002F1CDE File Offset: 0x002EFEDE
		// (set) Token: 0x060123E1 RID: 74721 RVA: 0x002F1CE7 File Offset: 0x002EFEE7
		public SeriesText SeriesText
		{
			get
			{
				return base.GetElement<SeriesText>(2);
			}
			set
			{
				base.SetElement<SeriesText>(2, value);
			}
		}

		// Token: 0x17005880 RID: 22656
		// (get) Token: 0x060123E2 RID: 74722 RVA: 0x002F1CF1 File Offset: 0x002EFEF1
		// (set) Token: 0x060123E3 RID: 74723 RVA: 0x002F1CFA File Offset: 0x002EFEFA
		public ChartShapeProperties ChartShapeProperties
		{
			get
			{
				return base.GetElement<ChartShapeProperties>(3);
			}
			set
			{
				base.SetElement<ChartShapeProperties>(3, value);
			}
		}

		// Token: 0x17005881 RID: 22657
		// (get) Token: 0x060123E4 RID: 74724 RVA: 0x002F2478 File Offset: 0x002F0678
		// (set) Token: 0x060123E5 RID: 74725 RVA: 0x002F2481 File Offset: 0x002F0681
		public PictureOptions PictureOptions
		{
			get
			{
				return base.GetElement<PictureOptions>(4);
			}
			set
			{
				base.SetElement<PictureOptions>(4, value);
			}
		}

		// Token: 0x17005882 RID: 22658
		// (get) Token: 0x060123E6 RID: 74726 RVA: 0x002F7CF0 File Offset: 0x002F5EF0
		// (set) Token: 0x060123E7 RID: 74727 RVA: 0x002F7CF9 File Offset: 0x002F5EF9
		public Marker Marker
		{
			get
			{
				return base.GetElement<Marker>(5);
			}
			set
			{
				base.SetElement<Marker>(5, value);
			}
		}

		// Token: 0x060123E8 RID: 74728 RVA: 0x002F7D03 File Offset: 0x002F5F03
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RadarChartSeries>(deep);
		}

		// Token: 0x04007ECA RID: 32458
		private const string tagName = "ser";

		// Token: 0x04007ECB RID: 32459
		private const byte tagNsId = 11;

		// Token: 0x04007ECC RID: 32460
		internal const int ElementTypeIdConst = 10540;

		// Token: 0x04007ECD RID: 32461
		private static readonly string[] eleTagNames = new string[]
		{
			"idx", "order", "tx", "spPr", "pictureOptions", "marker", "dPt", "dLbls", "cat", "val",
			"bubble3D", "extLst", "smooth"
		};

		// Token: 0x04007ECE RID: 32462
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11
		};
	}
}
