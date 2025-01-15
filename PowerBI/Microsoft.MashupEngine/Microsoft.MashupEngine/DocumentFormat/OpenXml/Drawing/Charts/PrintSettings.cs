using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002605 RID: 9733
	[ChildElementInfo(typeof(HeaderFooter))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PageMargins))]
	[ChildElementInfo(typeof(PageSetup))]
	[ChildElementInfo(typeof(LegacyDrawingHeaderFooter))]
	internal class PrintSettings : OpenXmlCompositeElement
	{
		// Token: 0x170059D4 RID: 22996
		// (get) Token: 0x060126CD RID: 75469 RVA: 0x002FAF47 File Offset: 0x002F9147
		public override string LocalName
		{
			get
			{
				return "printSettings";
			}
		}

		// Token: 0x170059D5 RID: 22997
		// (get) Token: 0x060126CE RID: 75470 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170059D6 RID: 22998
		// (get) Token: 0x060126CF RID: 75471 RVA: 0x002FAF4E File Offset: 0x002F914E
		internal override int ElementTypeId
		{
			get
			{
				return 10580;
			}
		}

		// Token: 0x060126D0 RID: 75472 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060126D1 RID: 75473 RVA: 0x00293ECF File Offset: 0x002920CF
		public PrintSettings()
		{
		}

		// Token: 0x060126D2 RID: 75474 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PrintSettings(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060126D3 RID: 75475 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PrintSettings(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060126D4 RID: 75476 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PrintSettings(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060126D5 RID: 75477 RVA: 0x002FAF58 File Offset: 0x002F9158
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "headerFooter" == name)
			{
				return new HeaderFooter();
			}
			if (11 == namespaceId && "pageMargins" == name)
			{
				return new PageMargins();
			}
			if (11 == namespaceId && "pageSetup" == name)
			{
				return new PageSetup();
			}
			if (11 == namespaceId && "legacyDrawingHF" == name)
			{
				return new LegacyDrawingHeaderFooter();
			}
			return null;
		}

		// Token: 0x170059D7 RID: 22999
		// (get) Token: 0x060126D6 RID: 75478 RVA: 0x002FAFC6 File Offset: 0x002F91C6
		internal override string[] ElementTagNames
		{
			get
			{
				return PrintSettings.eleTagNames;
			}
		}

		// Token: 0x170059D8 RID: 23000
		// (get) Token: 0x060126D7 RID: 75479 RVA: 0x002FAFCD File Offset: 0x002F91CD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PrintSettings.eleNamespaceIds;
			}
		}

		// Token: 0x170059D9 RID: 23001
		// (get) Token: 0x060126D8 RID: 75480 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170059DA RID: 23002
		// (get) Token: 0x060126D9 RID: 75481 RVA: 0x002FAFD4 File Offset: 0x002F91D4
		// (set) Token: 0x060126DA RID: 75482 RVA: 0x002FAFDD File Offset: 0x002F91DD
		public HeaderFooter HeaderFooter
		{
			get
			{
				return base.GetElement<HeaderFooter>(0);
			}
			set
			{
				base.SetElement<HeaderFooter>(0, value);
			}
		}

		// Token: 0x170059DB RID: 23003
		// (get) Token: 0x060126DB RID: 75483 RVA: 0x002FAFE7 File Offset: 0x002F91E7
		// (set) Token: 0x060126DC RID: 75484 RVA: 0x002FAFF0 File Offset: 0x002F91F0
		public PageMargins PageMargins
		{
			get
			{
				return base.GetElement<PageMargins>(1);
			}
			set
			{
				base.SetElement<PageMargins>(1, value);
			}
		}

		// Token: 0x170059DC RID: 23004
		// (get) Token: 0x060126DD RID: 75485 RVA: 0x002FAFFA File Offset: 0x002F91FA
		// (set) Token: 0x060126DE RID: 75486 RVA: 0x002FB003 File Offset: 0x002F9203
		public PageSetup PageSetup
		{
			get
			{
				return base.GetElement<PageSetup>(2);
			}
			set
			{
				base.SetElement<PageSetup>(2, value);
			}
		}

		// Token: 0x170059DD RID: 23005
		// (get) Token: 0x060126DF RID: 75487 RVA: 0x002FB00D File Offset: 0x002F920D
		// (set) Token: 0x060126E0 RID: 75488 RVA: 0x002FB016 File Offset: 0x002F9216
		public LegacyDrawingHeaderFooter LegacyDrawingHeaderFooter
		{
			get
			{
				return base.GetElement<LegacyDrawingHeaderFooter>(3);
			}
			set
			{
				base.SetElement<LegacyDrawingHeaderFooter>(3, value);
			}
		}

		// Token: 0x060126E1 RID: 75489 RVA: 0x002FB020 File Offset: 0x002F9220
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PrintSettings>(deep);
		}

		// Token: 0x04007F81 RID: 32641
		private const string tagName = "printSettings";

		// Token: 0x04007F82 RID: 32642
		private const byte tagNsId = 11;

		// Token: 0x04007F83 RID: 32643
		internal const int ElementTypeIdConst = 10580;

		// Token: 0x04007F84 RID: 32644
		private static readonly string[] eleTagNames = new string[] { "headerFooter", "pageMargins", "pageSetup", "legacyDrawingHF" };

		// Token: 0x04007F85 RID: 32645
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11 };
	}
}
