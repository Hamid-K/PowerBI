using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027E8 RID: 10216
	[ChildElementInfo(typeof(FontScheme))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ColorScheme))]
	[ChildElementInfo(typeof(FormatScheme))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class ThemeElements : OpenXmlCompositeElement
	{
		// Token: 0x17006472 RID: 25714
		// (get) Token: 0x06013E9C RID: 81564 RVA: 0x0030D1B5 File Offset: 0x0030B3B5
		public override string LocalName
		{
			get
			{
				return "themeElements";
			}
		}

		// Token: 0x17006473 RID: 25715
		// (get) Token: 0x06013E9D RID: 81565 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006474 RID: 25716
		// (get) Token: 0x06013E9E RID: 81566 RVA: 0x0030D1BC File Offset: 0x0030B3BC
		internal override int ElementTypeId
		{
			get
			{
				return 10248;
			}
		}

		// Token: 0x06013E9F RID: 81567 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013EA0 RID: 81568 RVA: 0x00293ECF File Offset: 0x002920CF
		public ThemeElements()
		{
		}

		// Token: 0x06013EA1 RID: 81569 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ThemeElements(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013EA2 RID: 81570 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ThemeElements(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013EA3 RID: 81571 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ThemeElements(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013EA4 RID: 81572 RVA: 0x0030D1C4 File Offset: 0x0030B3C4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "clrScheme" == name)
			{
				return new ColorScheme();
			}
			if (10 == namespaceId && "fontScheme" == name)
			{
				return new FontScheme();
			}
			if (10 == namespaceId && "fmtScheme" == name)
			{
				return new FormatScheme();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006475 RID: 25717
		// (get) Token: 0x06013EA5 RID: 81573 RVA: 0x0030D232 File Offset: 0x0030B432
		internal override string[] ElementTagNames
		{
			get
			{
				return ThemeElements.eleTagNames;
			}
		}

		// Token: 0x17006476 RID: 25718
		// (get) Token: 0x06013EA6 RID: 81574 RVA: 0x0030D239 File Offset: 0x0030B439
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ThemeElements.eleNamespaceIds;
			}
		}

		// Token: 0x17006477 RID: 25719
		// (get) Token: 0x06013EA7 RID: 81575 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006478 RID: 25720
		// (get) Token: 0x06013EA8 RID: 81576 RVA: 0x00307324 File Offset: 0x00305524
		// (set) Token: 0x06013EA9 RID: 81577 RVA: 0x0030732D File Offset: 0x0030552D
		public ColorScheme ColorScheme
		{
			get
			{
				return base.GetElement<ColorScheme>(0);
			}
			set
			{
				base.SetElement<ColorScheme>(0, value);
			}
		}

		// Token: 0x17006479 RID: 25721
		// (get) Token: 0x06013EAA RID: 81578 RVA: 0x00307337 File Offset: 0x00305537
		// (set) Token: 0x06013EAB RID: 81579 RVA: 0x00307340 File Offset: 0x00305540
		public FontScheme FontScheme
		{
			get
			{
				return base.GetElement<FontScheme>(1);
			}
			set
			{
				base.SetElement<FontScheme>(1, value);
			}
		}

		// Token: 0x1700647A RID: 25722
		// (get) Token: 0x06013EAC RID: 81580 RVA: 0x0030734A File Offset: 0x0030554A
		// (set) Token: 0x06013EAD RID: 81581 RVA: 0x00307353 File Offset: 0x00305553
		public FormatScheme FormatScheme
		{
			get
			{
				return base.GetElement<FormatScheme>(2);
			}
			set
			{
				base.SetElement<FormatScheme>(2, value);
			}
		}

		// Token: 0x1700647B RID: 25723
		// (get) Token: 0x06013EAE RID: 81582 RVA: 0x002E0C29 File Offset: 0x002DEE29
		// (set) Token: 0x06013EAF RID: 81583 RVA: 0x002E0C32 File Offset: 0x002DEE32
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(3);
			}
			set
			{
				base.SetElement<ExtensionList>(3, value);
			}
		}

		// Token: 0x06013EB0 RID: 81584 RVA: 0x0030D240 File Offset: 0x0030B440
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ThemeElements>(deep);
		}

		// Token: 0x04008844 RID: 34884
		private const string tagName = "themeElements";

		// Token: 0x04008845 RID: 34885
		private const byte tagNsId = 10;

		// Token: 0x04008846 RID: 34886
		internal const int ElementTypeIdConst = 10248;

		// Token: 0x04008847 RID: 34887
		private static readonly string[] eleTagNames = new string[] { "clrScheme", "fontScheme", "fmtScheme", "extLst" };

		// Token: 0x04008848 RID: 34888
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
