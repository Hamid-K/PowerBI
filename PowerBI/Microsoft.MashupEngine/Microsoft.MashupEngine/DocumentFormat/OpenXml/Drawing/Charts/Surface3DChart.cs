using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025EE RID: 9710
	[ChildElementInfo(typeof(BandFormats))]
	[ChildElementInfo(typeof(Wireframe))]
	[ChildElementInfo(typeof(VaryColors))]
	[ChildElementInfo(typeof(SurfaceChartSeries))]
	[ChildElementInfo(typeof(AxisId))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Surface3DChart : OpenXmlCompositeElement
	{
		// Token: 0x170058EC RID: 22764
		// (get) Token: 0x060124DA RID: 74970 RVA: 0x002F90D4 File Offset: 0x002F72D4
		public override string LocalName
		{
			get
			{
				return "surface3DChart";
			}
		}

		// Token: 0x170058ED RID: 22765
		// (get) Token: 0x060124DB RID: 74971 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170058EE RID: 22766
		// (get) Token: 0x060124DC RID: 74972 RVA: 0x002F90DB File Offset: 0x002F72DB
		internal override int ElementTypeId
		{
			get
			{
				return 10555;
			}
		}

		// Token: 0x060124DD RID: 74973 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060124DE RID: 74974 RVA: 0x00293ECF File Offset: 0x002920CF
		public Surface3DChart()
		{
		}

		// Token: 0x060124DF RID: 74975 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Surface3DChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060124E0 RID: 74976 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Surface3DChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060124E1 RID: 74977 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Surface3DChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060124E2 RID: 74978 RVA: 0x002F90E4 File Offset: 0x002F72E4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "wireframe" == name)
			{
				return new Wireframe();
			}
			if (11 == namespaceId && "varyColors" == name)
			{
				return new VaryColors();
			}
			if (11 == namespaceId && "ser" == name)
			{
				return new SurfaceChartSeries();
			}
			if (11 == namespaceId && "bandFmts" == name)
			{
				return new BandFormats();
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

		// Token: 0x170058EF RID: 22767
		// (get) Token: 0x060124E3 RID: 74979 RVA: 0x002F9182 File Offset: 0x002F7382
		internal override string[] ElementTagNames
		{
			get
			{
				return Surface3DChart.eleTagNames;
			}
		}

		// Token: 0x170058F0 RID: 22768
		// (get) Token: 0x060124E4 RID: 74980 RVA: 0x002F9189 File Offset: 0x002F7389
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Surface3DChart.eleNamespaceIds;
			}
		}

		// Token: 0x170058F1 RID: 22769
		// (get) Token: 0x060124E5 RID: 74981 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170058F2 RID: 22770
		// (get) Token: 0x060124E6 RID: 74982 RVA: 0x002F9058 File Offset: 0x002F7258
		// (set) Token: 0x060124E7 RID: 74983 RVA: 0x002F9061 File Offset: 0x002F7261
		public Wireframe Wireframe
		{
			get
			{
				return base.GetElement<Wireframe>(0);
			}
			set
			{
				base.SetElement<Wireframe>(0, value);
			}
		}

		// Token: 0x170058F3 RID: 22771
		// (get) Token: 0x060124E8 RID: 74984 RVA: 0x002F7E8F File Offset: 0x002F608F
		// (set) Token: 0x060124E9 RID: 74985 RVA: 0x002F7E98 File Offset: 0x002F6098
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

		// Token: 0x060124EA RID: 74986 RVA: 0x002F9190 File Offset: 0x002F7390
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Surface3DChart>(deep);
		}

		// Token: 0x04007F13 RID: 32531
		private const string tagName = "surface3DChart";

		// Token: 0x04007F14 RID: 32532
		private const byte tagNsId = 11;

		// Token: 0x04007F15 RID: 32533
		internal const int ElementTypeIdConst = 10555;

		// Token: 0x04007F16 RID: 32534
		private static readonly string[] eleTagNames = new string[] { "wireframe", "varyColors", "ser", "bandFmts", "axId", "extLst" };

		// Token: 0x04007F17 RID: 32535
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11 };
	}
}
