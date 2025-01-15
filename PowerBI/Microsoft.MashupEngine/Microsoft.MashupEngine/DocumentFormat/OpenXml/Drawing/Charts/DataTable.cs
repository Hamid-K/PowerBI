using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025F4 RID: 9716
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShowHorizontalBorder))]
	[ChildElementInfo(typeof(ShowVerticalBorder))]
	[ChildElementInfo(typeof(ShowOutlineBorder))]
	[ChildElementInfo(typeof(ShowKeys))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(TextProperties))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class DataTable : OpenXmlCompositeElement
	{
		// Token: 0x1700594B RID: 22859
		// (get) Token: 0x060125A4 RID: 75172 RVA: 0x002FA150 File Offset: 0x002F8350
		public override string LocalName
		{
			get
			{
				return "dTable";
			}
		}

		// Token: 0x1700594C RID: 22860
		// (get) Token: 0x060125A5 RID: 75173 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700594D RID: 22861
		// (get) Token: 0x060125A6 RID: 75174 RVA: 0x002FA157 File Offset: 0x002F8357
		internal override int ElementTypeId
		{
			get
			{
				return 10561;
			}
		}

		// Token: 0x060125A7 RID: 75175 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060125A8 RID: 75176 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataTable()
		{
		}

		// Token: 0x060125A9 RID: 75177 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataTable(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060125AA RID: 75178 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataTable(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060125AB RID: 75179 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataTable(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060125AC RID: 75180 RVA: 0x002FA160 File Offset: 0x002F8360
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "showHorzBorder" == name)
			{
				return new ShowHorizontalBorder();
			}
			if (11 == namespaceId && "showVertBorder" == name)
			{
				return new ShowVerticalBorder();
			}
			if (11 == namespaceId && "showOutline" == name)
			{
				return new ShowOutlineBorder();
			}
			if (11 == namespaceId && "showKeys" == name)
			{
				return new ShowKeys();
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

		// Token: 0x1700594E RID: 22862
		// (get) Token: 0x060125AD RID: 75181 RVA: 0x002FA216 File Offset: 0x002F8416
		internal override string[] ElementTagNames
		{
			get
			{
				return DataTable.eleTagNames;
			}
		}

		// Token: 0x1700594F RID: 22863
		// (get) Token: 0x060125AE RID: 75182 RVA: 0x002FA21D File Offset: 0x002F841D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DataTable.eleNamespaceIds;
			}
		}

		// Token: 0x17005950 RID: 22864
		// (get) Token: 0x060125AF RID: 75183 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005951 RID: 22865
		// (get) Token: 0x060125B0 RID: 75184 RVA: 0x002FA224 File Offset: 0x002F8424
		// (set) Token: 0x060125B1 RID: 75185 RVA: 0x002FA22D File Offset: 0x002F842D
		public ShowHorizontalBorder ShowHorizontalBorder
		{
			get
			{
				return base.GetElement<ShowHorizontalBorder>(0);
			}
			set
			{
				base.SetElement<ShowHorizontalBorder>(0, value);
			}
		}

		// Token: 0x17005952 RID: 22866
		// (get) Token: 0x060125B2 RID: 75186 RVA: 0x002FA237 File Offset: 0x002F8437
		// (set) Token: 0x060125B3 RID: 75187 RVA: 0x002FA240 File Offset: 0x002F8440
		public ShowVerticalBorder ShowVerticalBorder
		{
			get
			{
				return base.GetElement<ShowVerticalBorder>(1);
			}
			set
			{
				base.SetElement<ShowVerticalBorder>(1, value);
			}
		}

		// Token: 0x17005953 RID: 22867
		// (get) Token: 0x060125B4 RID: 75188 RVA: 0x002FA24A File Offset: 0x002F844A
		// (set) Token: 0x060125B5 RID: 75189 RVA: 0x002FA253 File Offset: 0x002F8453
		public ShowOutlineBorder ShowOutlineBorder
		{
			get
			{
				return base.GetElement<ShowOutlineBorder>(2);
			}
			set
			{
				base.SetElement<ShowOutlineBorder>(2, value);
			}
		}

		// Token: 0x17005954 RID: 22868
		// (get) Token: 0x060125B6 RID: 75190 RVA: 0x002FA25D File Offset: 0x002F845D
		// (set) Token: 0x060125B7 RID: 75191 RVA: 0x002FA266 File Offset: 0x002F8466
		public ShowKeys ShowKeys
		{
			get
			{
				return base.GetElement<ShowKeys>(3);
			}
			set
			{
				base.SetElement<ShowKeys>(3, value);
			}
		}

		// Token: 0x17005955 RID: 22869
		// (get) Token: 0x060125B8 RID: 75192 RVA: 0x002FA270 File Offset: 0x002F8470
		// (set) Token: 0x060125B9 RID: 75193 RVA: 0x002FA279 File Offset: 0x002F8479
		public ChartShapeProperties ChartShapeProperties
		{
			get
			{
				return base.GetElement<ChartShapeProperties>(4);
			}
			set
			{
				base.SetElement<ChartShapeProperties>(4, value);
			}
		}

		// Token: 0x17005956 RID: 22870
		// (get) Token: 0x060125BA RID: 75194 RVA: 0x002FA283 File Offset: 0x002F8483
		// (set) Token: 0x060125BB RID: 75195 RVA: 0x002FA28C File Offset: 0x002F848C
		public TextProperties TextProperties
		{
			get
			{
				return base.GetElement<TextProperties>(5);
			}
			set
			{
				base.SetElement<TextProperties>(5, value);
			}
		}

		// Token: 0x17005957 RID: 22871
		// (get) Token: 0x060125BC RID: 75196 RVA: 0x002F5D42 File Offset: 0x002F3F42
		// (set) Token: 0x060125BD RID: 75197 RVA: 0x002F5D4B File Offset: 0x002F3F4B
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(6);
			}
			set
			{
				base.SetElement<ExtensionList>(6, value);
			}
		}

		// Token: 0x060125BE RID: 75198 RVA: 0x002FA296 File Offset: 0x002F8496
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataTable>(deep);
		}

		// Token: 0x04007F31 RID: 32561
		private const string tagName = "dTable";

		// Token: 0x04007F32 RID: 32562
		private const byte tagNsId = 11;

		// Token: 0x04007F33 RID: 32563
		internal const int ElementTypeIdConst = 10561;

		// Token: 0x04007F34 RID: 32564
		private static readonly string[] eleTagNames = new string[] { "showHorzBorder", "showVertBorder", "showOutline", "showKeys", "spPr", "txPr", "extLst" };

		// Token: 0x04007F35 RID: 32565
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11, 11 };
	}
}
