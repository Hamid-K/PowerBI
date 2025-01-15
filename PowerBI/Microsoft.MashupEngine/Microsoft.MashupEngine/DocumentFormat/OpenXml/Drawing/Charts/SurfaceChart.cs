using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025ED RID: 9709
	[ChildElementInfo(typeof(AxisId))]
	[ChildElementInfo(typeof(Wireframe))]
	[ChildElementInfo(typeof(SurfaceChartSeries))]
	[ChildElementInfo(typeof(BandFormats))]
	[ChildElementInfo(typeof(VaryColors))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SurfaceChart : OpenXmlCompositeElement
	{
		// Token: 0x170058E5 RID: 22757
		// (get) Token: 0x060124CA RID: 74954 RVA: 0x002F8F9C File Offset: 0x002F719C
		public override string LocalName
		{
			get
			{
				return "surfaceChart";
			}
		}

		// Token: 0x170058E6 RID: 22758
		// (get) Token: 0x060124CB RID: 74955 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170058E7 RID: 22759
		// (get) Token: 0x060124CC RID: 74956 RVA: 0x002F8FA3 File Offset: 0x002F71A3
		internal override int ElementTypeId
		{
			get
			{
				return 10554;
			}
		}

		// Token: 0x060124CD RID: 74957 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060124CE RID: 74958 RVA: 0x00293ECF File Offset: 0x002920CF
		public SurfaceChart()
		{
		}

		// Token: 0x060124CF RID: 74959 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SurfaceChart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060124D0 RID: 74960 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SurfaceChart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060124D1 RID: 74961 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SurfaceChart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060124D2 RID: 74962 RVA: 0x002F8FAC File Offset: 0x002F71AC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "wireframe" == name)
			{
				return new Wireframe();
			}
			if (11 == namespaceId && "ser" == name)
			{
				return new SurfaceChartSeries();
			}
			if (11 == namespaceId && "bandFmts" == name)
			{
				return new BandFormats();
			}
			if (11 == namespaceId && "varyColors" == name)
			{
				return new VaryColors();
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

		// Token: 0x170058E8 RID: 22760
		// (get) Token: 0x060124D3 RID: 74963 RVA: 0x002F904A File Offset: 0x002F724A
		internal override string[] ElementTagNames
		{
			get
			{
				return SurfaceChart.eleTagNames;
			}
		}

		// Token: 0x170058E9 RID: 22761
		// (get) Token: 0x060124D4 RID: 74964 RVA: 0x002F9051 File Offset: 0x002F7251
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SurfaceChart.eleNamespaceIds;
			}
		}

		// Token: 0x170058EA RID: 22762
		// (get) Token: 0x060124D5 RID: 74965 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170058EB RID: 22763
		// (get) Token: 0x060124D6 RID: 74966 RVA: 0x002F9058 File Offset: 0x002F7258
		// (set) Token: 0x060124D7 RID: 74967 RVA: 0x002F9061 File Offset: 0x002F7261
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

		// Token: 0x060124D8 RID: 74968 RVA: 0x002F906B File Offset: 0x002F726B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SurfaceChart>(deep);
		}

		// Token: 0x04007F0E RID: 32526
		private const string tagName = "surfaceChart";

		// Token: 0x04007F0F RID: 32527
		private const byte tagNsId = 11;

		// Token: 0x04007F10 RID: 32528
		internal const int ElementTypeIdConst = 10554;

		// Token: 0x04007F11 RID: 32529
		private static readonly string[] eleTagNames = new string[] { "wireframe", "ser", "bandFmts", "varyColors", "axId", "extLst" };

		// Token: 0x04007F12 RID: 32530
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11 };
	}
}
