using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002603 RID: 9731
	[ChildElementInfo(typeof(Floor))]
	[ChildElementInfo(typeof(Title))]
	[ChildElementInfo(typeof(AutoTitleDeleted))]
	[ChildElementInfo(typeof(PivotFormats))]
	[ChildElementInfo(typeof(View3D))]
	[ChildElementInfo(typeof(PlotVisibleOnly))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BackWall))]
	[ChildElementInfo(typeof(PlotArea))]
	[ChildElementInfo(typeof(Legend))]
	[ChildElementInfo(typeof(SideWall))]
	[ChildElementInfo(typeof(DisplayBlanksAs))]
	[ChildElementInfo(typeof(ShowDataLabelsOverMaximum))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class Chart : OpenXmlCompositeElement
	{
		// Token: 0x170059B7 RID: 22967
		// (get) Token: 0x06012690 RID: 75408 RVA: 0x002AC9FE File Offset: 0x002AABFE
		public override string LocalName
		{
			get
			{
				return "chart";
			}
		}

		// Token: 0x170059B8 RID: 22968
		// (get) Token: 0x06012691 RID: 75409 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170059B9 RID: 22969
		// (get) Token: 0x06012692 RID: 75410 RVA: 0x002FAB88 File Offset: 0x002F8D88
		internal override int ElementTypeId
		{
			get
			{
				return 10578;
			}
		}

		// Token: 0x06012693 RID: 75411 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012694 RID: 75412 RVA: 0x00293ECF File Offset: 0x002920CF
		public Chart()
		{
		}

		// Token: 0x06012695 RID: 75413 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Chart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012696 RID: 75414 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Chart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012697 RID: 75415 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Chart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012698 RID: 75416 RVA: 0x002FAB90 File Offset: 0x002F8D90
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "title" == name)
			{
				return new Title();
			}
			if (11 == namespaceId && "autoTitleDeleted" == name)
			{
				return new AutoTitleDeleted();
			}
			if (11 == namespaceId && "pivotFmts" == name)
			{
				return new PivotFormats();
			}
			if (11 == namespaceId && "view3D" == name)
			{
				return new View3D();
			}
			if (11 == namespaceId && "floor" == name)
			{
				return new Floor();
			}
			if (11 == namespaceId && "sideWall" == name)
			{
				return new SideWall();
			}
			if (11 == namespaceId && "backWall" == name)
			{
				return new BackWall();
			}
			if (11 == namespaceId && "plotArea" == name)
			{
				return new PlotArea();
			}
			if (11 == namespaceId && "legend" == name)
			{
				return new Legend();
			}
			if (11 == namespaceId && "plotVisOnly" == name)
			{
				return new PlotVisibleOnly();
			}
			if (11 == namespaceId && "dispBlanksAs" == name)
			{
				return new DisplayBlanksAs();
			}
			if (11 == namespaceId && "showDLblsOverMax" == name)
			{
				return new ShowDataLabelsOverMaximum();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170059BA RID: 22970
		// (get) Token: 0x06012699 RID: 75417 RVA: 0x002FACD6 File Offset: 0x002F8ED6
		internal override string[] ElementTagNames
		{
			get
			{
				return Chart.eleTagNames;
			}
		}

		// Token: 0x170059BB RID: 22971
		// (get) Token: 0x0601269A RID: 75418 RVA: 0x002FACDD File Offset: 0x002F8EDD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Chart.eleNamespaceIds;
			}
		}

		// Token: 0x170059BC RID: 22972
		// (get) Token: 0x0601269B RID: 75419 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170059BD RID: 22973
		// (get) Token: 0x0601269C RID: 75420 RVA: 0x002FACE4 File Offset: 0x002F8EE4
		// (set) Token: 0x0601269D RID: 75421 RVA: 0x002FACED File Offset: 0x002F8EED
		public Title Title
		{
			get
			{
				return base.GetElement<Title>(0);
			}
			set
			{
				base.SetElement<Title>(0, value);
			}
		}

		// Token: 0x170059BE RID: 22974
		// (get) Token: 0x0601269E RID: 75422 RVA: 0x002FACF7 File Offset: 0x002F8EF7
		// (set) Token: 0x0601269F RID: 75423 RVA: 0x002FAD00 File Offset: 0x002F8F00
		public AutoTitleDeleted AutoTitleDeleted
		{
			get
			{
				return base.GetElement<AutoTitleDeleted>(1);
			}
			set
			{
				base.SetElement<AutoTitleDeleted>(1, value);
			}
		}

		// Token: 0x170059BF RID: 22975
		// (get) Token: 0x060126A0 RID: 75424 RVA: 0x002FAD0A File Offset: 0x002F8F0A
		// (set) Token: 0x060126A1 RID: 75425 RVA: 0x002FAD13 File Offset: 0x002F8F13
		public PivotFormats PivotFormats
		{
			get
			{
				return base.GetElement<PivotFormats>(2);
			}
			set
			{
				base.SetElement<PivotFormats>(2, value);
			}
		}

		// Token: 0x170059C0 RID: 22976
		// (get) Token: 0x060126A2 RID: 75426 RVA: 0x002FAD1D File Offset: 0x002F8F1D
		// (set) Token: 0x060126A3 RID: 75427 RVA: 0x002FAD26 File Offset: 0x002F8F26
		public View3D View3D
		{
			get
			{
				return base.GetElement<View3D>(3);
			}
			set
			{
				base.SetElement<View3D>(3, value);
			}
		}

		// Token: 0x170059C1 RID: 22977
		// (get) Token: 0x060126A4 RID: 75428 RVA: 0x002FAD30 File Offset: 0x002F8F30
		// (set) Token: 0x060126A5 RID: 75429 RVA: 0x002FAD39 File Offset: 0x002F8F39
		public Floor Floor
		{
			get
			{
				return base.GetElement<Floor>(4);
			}
			set
			{
				base.SetElement<Floor>(4, value);
			}
		}

		// Token: 0x170059C2 RID: 22978
		// (get) Token: 0x060126A6 RID: 75430 RVA: 0x002FAD43 File Offset: 0x002F8F43
		// (set) Token: 0x060126A7 RID: 75431 RVA: 0x002FAD4C File Offset: 0x002F8F4C
		public SideWall SideWall
		{
			get
			{
				return base.GetElement<SideWall>(5);
			}
			set
			{
				base.SetElement<SideWall>(5, value);
			}
		}

		// Token: 0x170059C3 RID: 22979
		// (get) Token: 0x060126A8 RID: 75432 RVA: 0x002FAD56 File Offset: 0x002F8F56
		// (set) Token: 0x060126A9 RID: 75433 RVA: 0x002FAD5F File Offset: 0x002F8F5F
		public BackWall BackWall
		{
			get
			{
				return base.GetElement<BackWall>(6);
			}
			set
			{
				base.SetElement<BackWall>(6, value);
			}
		}

		// Token: 0x170059C4 RID: 22980
		// (get) Token: 0x060126AA RID: 75434 RVA: 0x002FAD69 File Offset: 0x002F8F69
		// (set) Token: 0x060126AB RID: 75435 RVA: 0x002FAD72 File Offset: 0x002F8F72
		public PlotArea PlotArea
		{
			get
			{
				return base.GetElement<PlotArea>(7);
			}
			set
			{
				base.SetElement<PlotArea>(7, value);
			}
		}

		// Token: 0x170059C5 RID: 22981
		// (get) Token: 0x060126AC RID: 75436 RVA: 0x002FAD7C File Offset: 0x002F8F7C
		// (set) Token: 0x060126AD RID: 75437 RVA: 0x002FAD85 File Offset: 0x002F8F85
		public Legend Legend
		{
			get
			{
				return base.GetElement<Legend>(8);
			}
			set
			{
				base.SetElement<Legend>(8, value);
			}
		}

		// Token: 0x170059C6 RID: 22982
		// (get) Token: 0x060126AE RID: 75438 RVA: 0x002FAD8F File Offset: 0x002F8F8F
		// (set) Token: 0x060126AF RID: 75439 RVA: 0x002FAD99 File Offset: 0x002F8F99
		public PlotVisibleOnly PlotVisibleOnly
		{
			get
			{
				return base.GetElement<PlotVisibleOnly>(9);
			}
			set
			{
				base.SetElement<PlotVisibleOnly>(9, value);
			}
		}

		// Token: 0x170059C7 RID: 22983
		// (get) Token: 0x060126B0 RID: 75440 RVA: 0x002FADA4 File Offset: 0x002F8FA4
		// (set) Token: 0x060126B1 RID: 75441 RVA: 0x002FADAE File Offset: 0x002F8FAE
		public DisplayBlanksAs DisplayBlanksAs
		{
			get
			{
				return base.GetElement<DisplayBlanksAs>(10);
			}
			set
			{
				base.SetElement<DisplayBlanksAs>(10, value);
			}
		}

		// Token: 0x170059C8 RID: 22984
		// (get) Token: 0x060126B2 RID: 75442 RVA: 0x002FADB9 File Offset: 0x002F8FB9
		// (set) Token: 0x060126B3 RID: 75443 RVA: 0x002FADC3 File Offset: 0x002F8FC3
		public ShowDataLabelsOverMaximum ShowDataLabelsOverMaximum
		{
			get
			{
				return base.GetElement<ShowDataLabelsOverMaximum>(11);
			}
			set
			{
				base.SetElement<ShowDataLabelsOverMaximum>(11, value);
			}
		}

		// Token: 0x170059C9 RID: 22985
		// (get) Token: 0x060126B4 RID: 75444 RVA: 0x002FADCE File Offset: 0x002F8FCE
		// (set) Token: 0x060126B5 RID: 75445 RVA: 0x002FADD8 File Offset: 0x002F8FD8
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(12);
			}
			set
			{
				base.SetElement<ExtensionList>(12, value);
			}
		}

		// Token: 0x060126B6 RID: 75446 RVA: 0x002FADE3 File Offset: 0x002F8FE3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Chart>(deep);
		}

		// Token: 0x04007F75 RID: 32629
		private const string tagName = "chart";

		// Token: 0x04007F76 RID: 32630
		private const byte tagNsId = 11;

		// Token: 0x04007F77 RID: 32631
		internal const int ElementTypeIdConst = 10578;

		// Token: 0x04007F78 RID: 32632
		private static readonly string[] eleTagNames = new string[]
		{
			"title", "autoTitleDeleted", "pivotFmts", "view3D", "floor", "sideWall", "backWall", "plotArea", "legend", "plotVisOnly",
			"dispBlanksAs", "showDLblsOverMax", "extLst"
		};

		// Token: 0x04007F79 RID: 32633
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11
		};
	}
}
