using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002592 RID: 9618
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(ChartText))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DataLabelPosition))]
	[ChildElementInfo(typeof(Separator))]
	[ChildElementInfo(typeof(Delete))]
	[ChildElementInfo(typeof(Layout))]
	[ChildElementInfo(typeof(NumberingFormat))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(TextProperties))]
	[ChildElementInfo(typeof(Index))]
	[ChildElementInfo(typeof(ShowLegendKey))]
	[ChildElementInfo(typeof(ShowValue))]
	[ChildElementInfo(typeof(ShowCategoryName))]
	[ChildElementInfo(typeof(ShowSeriesName))]
	[ChildElementInfo(typeof(ShowPercent))]
	[ChildElementInfo(typeof(ShowBubbleSize))]
	internal class DataLabel : OpenXmlCompositeElement
	{
		// Token: 0x17005684 RID: 22148
		// (get) Token: 0x06011F87 RID: 73607 RVA: 0x002F4317 File Offset: 0x002F2517
		public override string LocalName
		{
			get
			{
				return "dLbl";
			}
		}

		// Token: 0x17005685 RID: 22149
		// (get) Token: 0x06011F88 RID: 73608 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005686 RID: 22150
		// (get) Token: 0x06011F89 RID: 73609 RVA: 0x002F431E File Offset: 0x002F251E
		internal override int ElementTypeId
		{
			get
			{
				return 10428;
			}
		}

		// Token: 0x06011F8A RID: 73610 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011F8B RID: 73611 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataLabel()
		{
		}

		// Token: 0x06011F8C RID: 73612 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataLabel(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011F8D RID: 73613 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataLabel(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011F8E RID: 73614 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataLabel(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011F8F RID: 73615 RVA: 0x002F4328 File Offset: 0x002F2528
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "idx" == name)
			{
				return new Index();
			}
			if (11 == namespaceId && "delete" == name)
			{
				return new Delete();
			}
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
			if (11 == namespaceId && "dLblPos" == name)
			{
				return new DataLabelPosition();
			}
			if (11 == namespaceId && "showLegendKey" == name)
			{
				return new ShowLegendKey();
			}
			if (11 == namespaceId && "showVal" == name)
			{
				return new ShowValue();
			}
			if (11 == namespaceId && "showCatName" == name)
			{
				return new ShowCategoryName();
			}
			if (11 == namespaceId && "showSerName" == name)
			{
				return new ShowSeriesName();
			}
			if (11 == namespaceId && "showPercent" == name)
			{
				return new ShowPercent();
			}
			if (11 == namespaceId && "showBubbleSize" == name)
			{
				return new ShowBubbleSize();
			}
			if (11 == namespaceId && "separator" == name)
			{
				return new Separator();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005687 RID: 22151
		// (get) Token: 0x06011F90 RID: 73616 RVA: 0x002F44B6 File Offset: 0x002F26B6
		internal override string[] ElementTagNames
		{
			get
			{
				return DataLabel.eleTagNames;
			}
		}

		// Token: 0x17005688 RID: 22152
		// (get) Token: 0x06011F91 RID: 73617 RVA: 0x002F44BD File Offset: 0x002F26BD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DataLabel.eleNamespaceIds;
			}
		}

		// Token: 0x17005689 RID: 22153
		// (get) Token: 0x06011F92 RID: 73618 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700568A RID: 22154
		// (get) Token: 0x06011F93 RID: 73619 RVA: 0x002F1CB8 File Offset: 0x002EFEB8
		// (set) Token: 0x06011F94 RID: 73620 RVA: 0x002F1CC1 File Offset: 0x002EFEC1
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

		// Token: 0x06011F95 RID: 73621 RVA: 0x002F44C4 File Offset: 0x002F26C4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataLabel>(deep);
		}

		// Token: 0x04007D84 RID: 32132
		private const string tagName = "dLbl";

		// Token: 0x04007D85 RID: 32133
		private const byte tagNsId = 11;

		// Token: 0x04007D86 RID: 32134
		internal const int ElementTypeIdConst = 10428;

		// Token: 0x04007D87 RID: 32135
		private static readonly string[] eleTagNames = new string[]
		{
			"idx", "delete", "layout", "tx", "numFmt", "spPr", "txPr", "dLblPos", "showLegendKey", "showVal",
			"showCatName", "showSerName", "showPercent", "showBubbleSize", "separator", "extLst"
		};

		// Token: 0x04007D88 RID: 32136
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 11, 11
		};
	}
}
