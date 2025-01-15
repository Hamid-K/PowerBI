using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200254A RID: 9546
	[ChildElementInfo(typeof(Delete))]
	[ChildElementInfo(typeof(DataLabel))]
	[ChildElementInfo(typeof(NumberingFormat))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(TextProperties))]
	[ChildElementInfo(typeof(DataLabelPosition))]
	[ChildElementInfo(typeof(ShowLegendKey))]
	[ChildElementInfo(typeof(ShowValue))]
	[ChildElementInfo(typeof(ShowCategoryName))]
	[ChildElementInfo(typeof(ShowSeriesName))]
	[ChildElementInfo(typeof(ShowPercent))]
	[ChildElementInfo(typeof(ShowBubbleSize))]
	[ChildElementInfo(typeof(Separator))]
	[ChildElementInfo(typeof(ShowLeaderLines))]
	[ChildElementInfo(typeof(LeaderLines))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DataLabels : OpenXmlCompositeElement
	{
		// Token: 0x17005514 RID: 21780
		// (get) Token: 0x06011C47 RID: 72775 RVA: 0x002F1DE4 File Offset: 0x002EFFE4
		public override string LocalName
		{
			get
			{
				return "dLbls";
			}
		}

		// Token: 0x17005515 RID: 21781
		// (get) Token: 0x06011C48 RID: 72776 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005516 RID: 21782
		// (get) Token: 0x06011C49 RID: 72777 RVA: 0x002F1DEB File Offset: 0x002EFFEB
		internal override int ElementTypeId
		{
			get
			{
				return 10363;
			}
		}

		// Token: 0x06011C4A RID: 72778 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011C4B RID: 72779 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataLabels()
		{
		}

		// Token: 0x06011C4C RID: 72780 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataLabels(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011C4D RID: 72781 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataLabels(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011C4E RID: 72782 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataLabels(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011C4F RID: 72783 RVA: 0x002F1DF4 File Offset: 0x002EFFF4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "dLbl" == name)
			{
				return new DataLabel();
			}
			if (11 == namespaceId && "delete" == name)
			{
				return new Delete();
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
			if (11 == namespaceId && "showLeaderLines" == name)
			{
				return new ShowLeaderLines();
			}
			if (11 == namespaceId && "leaderLines" == name)
			{
				return new LeaderLines();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06011C50 RID: 72784 RVA: 0x002F1F82 File Offset: 0x002F0182
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataLabels>(deep);
		}

		// Token: 0x04007C7A RID: 31866
		private const string tagName = "dLbls";

		// Token: 0x04007C7B RID: 31867
		private const byte tagNsId = 11;

		// Token: 0x04007C7C RID: 31868
		internal const int ElementTypeIdConst = 10363;
	}
}
