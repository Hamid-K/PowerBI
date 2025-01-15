using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002550 RID: 9552
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PictureOptions))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(Order))]
	[ChildElementInfo(typeof(SeriesText))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(Index))]
	[ChildElementInfo(typeof(CategoryAxisData))]
	[ChildElementInfo(typeof(Values))]
	[ChildElementInfo(typeof(Bubble3D))]
	internal class SurfaceChartSeries : OpenXmlCompositeElement
	{
		// Token: 0x17005546 RID: 21830
		// (get) Token: 0x06011CB5 RID: 72885 RVA: 0x002F1B23 File Offset: 0x002EFD23
		public override string LocalName
		{
			get
			{
				return "ser";
			}
		}

		// Token: 0x17005547 RID: 21831
		// (get) Token: 0x06011CB6 RID: 72886 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005548 RID: 21832
		// (get) Token: 0x06011CB7 RID: 72887 RVA: 0x002F2752 File Offset: 0x002F0952
		internal override int ElementTypeId
		{
			get
			{
				return 10371;
			}
		}

		// Token: 0x06011CB8 RID: 72888 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011CB9 RID: 72889 RVA: 0x00293ECF File Offset: 0x002920CF
		public SurfaceChartSeries()
		{
		}

		// Token: 0x06011CBA RID: 72890 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SurfaceChartSeries(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011CBB RID: 72891 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SurfaceChartSeries(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011CBC RID: 72892 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SurfaceChartSeries(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011CBD RID: 72893 RVA: 0x002F275C File Offset: 0x002F095C
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
			return null;
		}

		// Token: 0x17005549 RID: 21833
		// (get) Token: 0x06011CBE RID: 72894 RVA: 0x002F2842 File Offset: 0x002F0A42
		internal override string[] ElementTagNames
		{
			get
			{
				return SurfaceChartSeries.eleTagNames;
			}
		}

		// Token: 0x1700554A RID: 21834
		// (get) Token: 0x06011CBF RID: 72895 RVA: 0x002F2849 File Offset: 0x002F0A49
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SurfaceChartSeries.eleNamespaceIds;
			}
		}

		// Token: 0x1700554B RID: 21835
		// (get) Token: 0x06011CC0 RID: 72896 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700554C RID: 21836
		// (get) Token: 0x06011CC1 RID: 72897 RVA: 0x002F1CB8 File Offset: 0x002EFEB8
		// (set) Token: 0x06011CC2 RID: 72898 RVA: 0x002F1CC1 File Offset: 0x002EFEC1
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

		// Token: 0x1700554D RID: 21837
		// (get) Token: 0x06011CC3 RID: 72899 RVA: 0x002F1CCB File Offset: 0x002EFECB
		// (set) Token: 0x06011CC4 RID: 72900 RVA: 0x002F1CD4 File Offset: 0x002EFED4
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

		// Token: 0x1700554E RID: 21838
		// (get) Token: 0x06011CC5 RID: 72901 RVA: 0x002F1CDE File Offset: 0x002EFEDE
		// (set) Token: 0x06011CC6 RID: 72902 RVA: 0x002F1CE7 File Offset: 0x002EFEE7
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

		// Token: 0x1700554F RID: 21839
		// (get) Token: 0x06011CC7 RID: 72903 RVA: 0x002F1CF1 File Offset: 0x002EFEF1
		// (set) Token: 0x06011CC8 RID: 72904 RVA: 0x002F1CFA File Offset: 0x002EFEFA
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

		// Token: 0x17005550 RID: 21840
		// (get) Token: 0x06011CC9 RID: 72905 RVA: 0x002F2478 File Offset: 0x002F0678
		// (set) Token: 0x06011CCA RID: 72906 RVA: 0x002F2481 File Offset: 0x002F0681
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

		// Token: 0x17005551 RID: 21841
		// (get) Token: 0x06011CCB RID: 72907 RVA: 0x002F2850 File Offset: 0x002F0A50
		// (set) Token: 0x06011CCC RID: 72908 RVA: 0x002F2859 File Offset: 0x002F0A59
		public CategoryAxisData CategoryAxisData
		{
			get
			{
				return base.GetElement<CategoryAxisData>(5);
			}
			set
			{
				base.SetElement<CategoryAxisData>(5, value);
			}
		}

		// Token: 0x17005552 RID: 21842
		// (get) Token: 0x06011CCD RID: 72909 RVA: 0x002F2863 File Offset: 0x002F0A63
		// (set) Token: 0x06011CCE RID: 72910 RVA: 0x002F286C File Offset: 0x002F0A6C
		public Values Values
		{
			get
			{
				return base.GetElement<Values>(6);
			}
			set
			{
				base.SetElement<Values>(6, value);
			}
		}

		// Token: 0x17005553 RID: 21843
		// (get) Token: 0x06011CCF RID: 72911 RVA: 0x002F2876 File Offset: 0x002F0A76
		// (set) Token: 0x06011CD0 RID: 72912 RVA: 0x002F287F File Offset: 0x002F0A7F
		public Bubble3D Bubble3D
		{
			get
			{
				return base.GetElement<Bubble3D>(7);
			}
			set
			{
				base.SetElement<Bubble3D>(7, value);
			}
		}

		// Token: 0x17005554 RID: 21844
		// (get) Token: 0x06011CD1 RID: 72913 RVA: 0x002F2889 File Offset: 0x002F0A89
		// (set) Token: 0x06011CD2 RID: 72914 RVA: 0x002F2892 File Offset: 0x002F0A92
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(8);
			}
			set
			{
				base.SetElement<ExtensionList>(8, value);
			}
		}

		// Token: 0x06011CD3 RID: 72915 RVA: 0x002F289C File Offset: 0x002F0A9C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SurfaceChartSeries>(deep);
		}

		// Token: 0x04007C96 RID: 31894
		private const string tagName = "ser";

		// Token: 0x04007C97 RID: 31895
		private const byte tagNsId = 11;

		// Token: 0x04007C98 RID: 31896
		internal const int ElementTypeIdConst = 10371;

		// Token: 0x04007C99 RID: 31897
		private static readonly string[] eleTagNames = new string[] { "idx", "order", "tx", "spPr", "pictureOptions", "cat", "val", "bubble3D", "extLst" };

		// Token: 0x04007C9A RID: 31898
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11, 11, 11, 11 };
	}
}
