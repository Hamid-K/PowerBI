using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002554 RID: 9556
	[ChildElementInfo(typeof(Layout))]
	[ChildElementInfo(typeof(TextProperties))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ChartText))]
	[ChildElementInfo(typeof(Overlay))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	internal class Title : OpenXmlCompositeElement
	{
		// Token: 0x17005569 RID: 21865
		// (get) Token: 0x06011D03 RID: 72963 RVA: 0x002F2B3B File Offset: 0x002F0D3B
		public override string LocalName
		{
			get
			{
				return "title";
			}
		}

		// Token: 0x1700556A RID: 21866
		// (get) Token: 0x06011D04 RID: 72964 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700556B RID: 21867
		// (get) Token: 0x06011D05 RID: 72965 RVA: 0x002F2B42 File Offset: 0x002F0D42
		internal override int ElementTypeId
		{
			get
			{
				return 10379;
			}
		}

		// Token: 0x06011D06 RID: 72966 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D07 RID: 72967 RVA: 0x00293ECF File Offset: 0x002920CF
		public Title()
		{
		}

		// Token: 0x06011D08 RID: 72968 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Title(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011D09 RID: 72969 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Title(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011D0A RID: 72970 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Title(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011D0B RID: 72971 RVA: 0x002F2B4C File Offset: 0x002F0D4C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "tx" == name)
			{
				return new ChartText();
			}
			if (11 == namespaceId && "layout" == name)
			{
				return new Layout();
			}
			if (11 == namespaceId && "overlay" == name)
			{
				return new Overlay();
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

		// Token: 0x1700556C RID: 21868
		// (get) Token: 0x06011D0C RID: 72972 RVA: 0x002F2BEA File Offset: 0x002F0DEA
		internal override string[] ElementTagNames
		{
			get
			{
				return Title.eleTagNames;
			}
		}

		// Token: 0x1700556D RID: 21869
		// (get) Token: 0x06011D0D RID: 72973 RVA: 0x002F2BF1 File Offset: 0x002F0DF1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Title.eleNamespaceIds;
			}
		}

		// Token: 0x1700556E RID: 21870
		// (get) Token: 0x06011D0E RID: 72974 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700556F RID: 21871
		// (get) Token: 0x06011D0F RID: 72975 RVA: 0x002F2BF8 File Offset: 0x002F0DF8
		// (set) Token: 0x06011D10 RID: 72976 RVA: 0x002F2C01 File Offset: 0x002F0E01
		public ChartText ChartText
		{
			get
			{
				return base.GetElement<ChartText>(0);
			}
			set
			{
				base.SetElement<ChartText>(0, value);
			}
		}

		// Token: 0x17005570 RID: 21872
		// (get) Token: 0x06011D11 RID: 72977 RVA: 0x002F2C0B File Offset: 0x002F0E0B
		// (set) Token: 0x06011D12 RID: 72978 RVA: 0x002F2C14 File Offset: 0x002F0E14
		public Layout Layout
		{
			get
			{
				return base.GetElement<Layout>(1);
			}
			set
			{
				base.SetElement<Layout>(1, value);
			}
		}

		// Token: 0x17005571 RID: 21873
		// (get) Token: 0x06011D13 RID: 72979 RVA: 0x002F2C1E File Offset: 0x002F0E1E
		// (set) Token: 0x06011D14 RID: 72980 RVA: 0x002F2C27 File Offset: 0x002F0E27
		public Overlay Overlay
		{
			get
			{
				return base.GetElement<Overlay>(2);
			}
			set
			{
				base.SetElement<Overlay>(2, value);
			}
		}

		// Token: 0x17005572 RID: 21874
		// (get) Token: 0x06011D15 RID: 72981 RVA: 0x002F1CF1 File Offset: 0x002EFEF1
		// (set) Token: 0x06011D16 RID: 72982 RVA: 0x002F1CFA File Offset: 0x002EFEFA
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

		// Token: 0x17005573 RID: 21875
		// (get) Token: 0x06011D17 RID: 72983 RVA: 0x002F2C31 File Offset: 0x002F0E31
		// (set) Token: 0x06011D18 RID: 72984 RVA: 0x002F2C3A File Offset: 0x002F0E3A
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

		// Token: 0x17005574 RID: 21876
		// (get) Token: 0x06011D19 RID: 72985 RVA: 0x002F2C44 File Offset: 0x002F0E44
		// (set) Token: 0x06011D1A RID: 72986 RVA: 0x002F2C4D File Offset: 0x002F0E4D
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

		// Token: 0x06011D1B RID: 72987 RVA: 0x002F2C57 File Offset: 0x002F0E57
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Title>(deep);
		}

		// Token: 0x04007CA8 RID: 31912
		private const string tagName = "title";

		// Token: 0x04007CA9 RID: 31913
		private const byte tagNsId = 11;

		// Token: 0x04007CAA RID: 31914
		internal const int ElementTypeIdConst = 10379;

		// Token: 0x04007CAB RID: 31915
		private static readonly string[] eleTagNames = new string[] { "tx", "layout", "overlay", "spPr", "txPr", "extLst" };

		// Token: 0x04007CAC RID: 31916
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11 };
	}
}
