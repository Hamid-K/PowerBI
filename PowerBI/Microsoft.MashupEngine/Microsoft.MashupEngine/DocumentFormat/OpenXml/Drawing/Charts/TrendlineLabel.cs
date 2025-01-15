using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200259A RID: 9626
	[ChildElementInfo(typeof(ChartText))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NumberingFormat))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(TextProperties))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(Layout))]
	internal class TrendlineLabel : OpenXmlCompositeElement
	{
		// Token: 0x170056BE RID: 22206
		// (get) Token: 0x06012001 RID: 73729 RVA: 0x002F4A1B File Offset: 0x002F2C1B
		public override string LocalName
		{
			get
			{
				return "trendlineLbl";
			}
		}

		// Token: 0x170056BF RID: 22207
		// (get) Token: 0x06012002 RID: 73730 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056C0 RID: 22208
		// (get) Token: 0x06012003 RID: 73731 RVA: 0x002F4A22 File Offset: 0x002F2C22
		internal override int ElementTypeId
		{
			get
			{
				return 10445;
			}
		}

		// Token: 0x06012004 RID: 73732 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012005 RID: 73733 RVA: 0x00293ECF File Offset: 0x002920CF
		public TrendlineLabel()
		{
		}

		// Token: 0x06012006 RID: 73734 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TrendlineLabel(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012007 RID: 73735 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TrendlineLabel(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012008 RID: 73736 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TrendlineLabel(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012009 RID: 73737 RVA: 0x002F4A2C File Offset: 0x002F2C2C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "layout" == name)
			{
				return new Layout();
			}
			if (11 == namespaceId && "tx" == name)
			{
				return new ChartText();
			}
			if (11 == namespaceId && "numFmt" == name)
			{
				return new NumberingFormat();
			}
			if (11 == namespaceId && "spPr" == name)
			{
				return new ChartShapeProperties();
			}
			if (11 == namespaceId && "txPr" == name)
			{
				return new TextProperties();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170056C1 RID: 22209
		// (get) Token: 0x0601200A RID: 73738 RVA: 0x002F4ACA File Offset: 0x002F2CCA
		internal override string[] ElementTagNames
		{
			get
			{
				return TrendlineLabel.eleTagNames;
			}
		}

		// Token: 0x170056C2 RID: 22210
		// (get) Token: 0x0601200B RID: 73739 RVA: 0x002F4AD1 File Offset: 0x002F2CD1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TrendlineLabel.eleNamespaceIds;
			}
		}

		// Token: 0x170056C3 RID: 22211
		// (get) Token: 0x0601200C RID: 73740 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170056C4 RID: 22212
		// (get) Token: 0x0601200D RID: 73741 RVA: 0x002F4AD8 File Offset: 0x002F2CD8
		// (set) Token: 0x0601200E RID: 73742 RVA: 0x002F4AE1 File Offset: 0x002F2CE1
		public Layout Layout
		{
			get
			{
				return base.GetElement<Layout>(0);
			}
			set
			{
				base.SetElement<Layout>(0, value);
			}
		}

		// Token: 0x170056C5 RID: 22213
		// (get) Token: 0x0601200F RID: 73743 RVA: 0x002F4AEB File Offset: 0x002F2CEB
		// (set) Token: 0x06012010 RID: 73744 RVA: 0x002F4AF4 File Offset: 0x002F2CF4
		public ChartText ChartText
		{
			get
			{
				return base.GetElement<ChartText>(1);
			}
			set
			{
				base.SetElement<ChartText>(1, value);
			}
		}

		// Token: 0x170056C6 RID: 22214
		// (get) Token: 0x06012011 RID: 73745 RVA: 0x002F4AFE File Offset: 0x002F2CFE
		// (set) Token: 0x06012012 RID: 73746 RVA: 0x002F4B07 File Offset: 0x002F2D07
		public NumberingFormat NumberingFormat
		{
			get
			{
				return base.GetElement<NumberingFormat>(2);
			}
			set
			{
				base.SetElement<NumberingFormat>(2, value);
			}
		}

		// Token: 0x170056C7 RID: 22215
		// (get) Token: 0x06012013 RID: 73747 RVA: 0x002F1CF1 File Offset: 0x002EFEF1
		// (set) Token: 0x06012014 RID: 73748 RVA: 0x002F1CFA File Offset: 0x002EFEFA
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

		// Token: 0x170056C8 RID: 22216
		// (get) Token: 0x06012015 RID: 73749 RVA: 0x002F2C31 File Offset: 0x002F0E31
		// (set) Token: 0x06012016 RID: 73750 RVA: 0x002F2C3A File Offset: 0x002F0E3A
		public TextProperties TextProperties
		{
			get
			{
				return base.GetElement<TextProperties>(4);
			}
			set
			{
				base.SetElement<TextProperties>(4, value);
			}
		}

		// Token: 0x170056C9 RID: 22217
		// (get) Token: 0x06012017 RID: 73751 RVA: 0x002F2C44 File Offset: 0x002F0E44
		// (set) Token: 0x06012018 RID: 73752 RVA: 0x002F2C4D File Offset: 0x002F0E4D
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(5);
			}
			set
			{
				base.SetElement<ExtensionList>(5, value);
			}
		}

		// Token: 0x06012019 RID: 73753 RVA: 0x002F4B11 File Offset: 0x002F2D11
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TrendlineLabel>(deep);
		}

		// Token: 0x04007DAC RID: 32172
		private const string tagName = "trendlineLbl";

		// Token: 0x04007DAD RID: 32173
		private const byte tagNsId = 11;

		// Token: 0x04007DAE RID: 32174
		internal const int ElementTypeIdConst = 10445;

		// Token: 0x04007DAF RID: 32175
		private static readonly string[] eleTagNames = new string[] { "layout", "tx", "numFmt", "spPr", "txPr", "extLst" };

		// Token: 0x04007DB0 RID: 32176
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11 };
	}
}
