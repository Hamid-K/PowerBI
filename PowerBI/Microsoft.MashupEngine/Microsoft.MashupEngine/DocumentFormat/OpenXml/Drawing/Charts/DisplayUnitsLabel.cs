using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025B4 RID: 9652
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(ChartText))]
	[ChildElementInfo(typeof(TextProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Layout))]
	internal class DisplayUnitsLabel : OpenXmlCompositeElement
	{
		// Token: 0x17005742 RID: 22338
		// (get) Token: 0x0601212F RID: 74031 RVA: 0x002F545B File Offset: 0x002F365B
		public override string LocalName
		{
			get
			{
				return "dispUnitsLbl";
			}
		}

		// Token: 0x17005743 RID: 22339
		// (get) Token: 0x06012130 RID: 74032 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005744 RID: 22340
		// (get) Token: 0x06012131 RID: 74033 RVA: 0x002F5462 File Offset: 0x002F3662
		internal override int ElementTypeId
		{
			get
			{
				return 10476;
			}
		}

		// Token: 0x06012132 RID: 74034 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012133 RID: 74035 RVA: 0x00293ECF File Offset: 0x002920CF
		public DisplayUnitsLabel()
		{
		}

		// Token: 0x06012134 RID: 74036 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DisplayUnitsLabel(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012135 RID: 74037 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DisplayUnitsLabel(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012136 RID: 74038 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DisplayUnitsLabel(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012137 RID: 74039 RVA: 0x002F546C File Offset: 0x002F366C
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
			if (11 == namespaceId && "spPr" == name)
			{
				return new ChartShapeProperties();
			}
			if (11 == namespaceId && "txPr" == name)
			{
				return new TextProperties();
			}
			return null;
		}

		// Token: 0x17005745 RID: 22341
		// (get) Token: 0x06012138 RID: 74040 RVA: 0x002F54DA File Offset: 0x002F36DA
		internal override string[] ElementTagNames
		{
			get
			{
				return DisplayUnitsLabel.eleTagNames;
			}
		}

		// Token: 0x17005746 RID: 22342
		// (get) Token: 0x06012139 RID: 74041 RVA: 0x002F54E1 File Offset: 0x002F36E1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DisplayUnitsLabel.eleNamespaceIds;
			}
		}

		// Token: 0x17005747 RID: 22343
		// (get) Token: 0x0601213A RID: 74042 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005748 RID: 22344
		// (get) Token: 0x0601213B RID: 74043 RVA: 0x002F4AD8 File Offset: 0x002F2CD8
		// (set) Token: 0x0601213C RID: 74044 RVA: 0x002F4AE1 File Offset: 0x002F2CE1
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

		// Token: 0x17005749 RID: 22345
		// (get) Token: 0x0601213D RID: 74045 RVA: 0x002F4AEB File Offset: 0x002F2CEB
		// (set) Token: 0x0601213E RID: 74046 RVA: 0x002F4AF4 File Offset: 0x002F2CF4
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

		// Token: 0x1700574A RID: 22346
		// (get) Token: 0x0601213F RID: 74047 RVA: 0x002F470E File Offset: 0x002F290E
		// (set) Token: 0x06012140 RID: 74048 RVA: 0x002F4717 File Offset: 0x002F2917
		public ChartShapeProperties ChartShapeProperties
		{
			get
			{
				return base.GetElement<ChartShapeProperties>(2);
			}
			set
			{
				base.SetElement<ChartShapeProperties>(2, value);
			}
		}

		// Token: 0x1700574B RID: 22347
		// (get) Token: 0x06012141 RID: 74049 RVA: 0x002F54E8 File Offset: 0x002F36E8
		// (set) Token: 0x06012142 RID: 74050 RVA: 0x002F54F1 File Offset: 0x002F36F1
		public TextProperties TextProperties
		{
			get
			{
				return base.GetElement<TextProperties>(3);
			}
			set
			{
				base.SetElement<TextProperties>(3, value);
			}
		}

		// Token: 0x06012143 RID: 74051 RVA: 0x002F54FB File Offset: 0x002F36FB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DisplayUnitsLabel>(deep);
		}

		// Token: 0x04007E11 RID: 32273
		private const string tagName = "dispUnitsLbl";

		// Token: 0x04007E12 RID: 32274
		private const byte tagNsId = 11;

		// Token: 0x04007E13 RID: 32275
		internal const int ElementTypeIdConst = 10476;

		// Token: 0x04007E14 RID: 32276
		private static readonly string[] eleTagNames = new string[] { "layout", "tx", "spPr", "txPr" };

		// Token: 0x04007E15 RID: 32277
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11 };
	}
}
