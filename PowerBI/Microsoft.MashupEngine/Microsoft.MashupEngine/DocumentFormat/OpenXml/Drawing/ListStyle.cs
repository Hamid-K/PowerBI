using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027DF RID: 10207
	[ChildElementInfo(typeof(Level7ParagraphProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DefaultParagraphProperties))]
	[ChildElementInfo(typeof(Level1ParagraphProperties))]
	[ChildElementInfo(typeof(Level2ParagraphProperties))]
	[ChildElementInfo(typeof(Level3ParagraphProperties))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(Level5ParagraphProperties))]
	[ChildElementInfo(typeof(Level6ParagraphProperties))]
	[ChildElementInfo(typeof(Level4ParagraphProperties))]
	[ChildElementInfo(typeof(Level8ParagraphProperties))]
	[ChildElementInfo(typeof(Level9ParagraphProperties))]
	internal class ListStyle : OpenXmlCompositeElement
	{
		// Token: 0x17006430 RID: 25648
		// (get) Token: 0x06013E00 RID: 81408 RVA: 0x0030CA8A File Offset: 0x0030AC8A
		public override string LocalName
		{
			get
			{
				return "lstStyle";
			}
		}

		// Token: 0x17006431 RID: 25649
		// (get) Token: 0x06013E01 RID: 81409 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006432 RID: 25650
		// (get) Token: 0x06013E02 RID: 81410 RVA: 0x0030CA91 File Offset: 0x0030AC91
		internal override int ElementTypeId
		{
			get
			{
				return 10240;
			}
		}

		// Token: 0x06013E03 RID: 81411 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013E04 RID: 81412 RVA: 0x00293ECF File Offset: 0x002920CF
		public ListStyle()
		{
		}

		// Token: 0x06013E05 RID: 81413 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ListStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E06 RID: 81414 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ListStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E07 RID: 81415 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ListStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013E08 RID: 81416 RVA: 0x0030CA98 File Offset: 0x0030AC98
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "defPPr" == name)
			{
				return new DefaultParagraphProperties();
			}
			if (10 == namespaceId && "lvl1pPr" == name)
			{
				return new Level1ParagraphProperties();
			}
			if (10 == namespaceId && "lvl2pPr" == name)
			{
				return new Level2ParagraphProperties();
			}
			if (10 == namespaceId && "lvl3pPr" == name)
			{
				return new Level3ParagraphProperties();
			}
			if (10 == namespaceId && "lvl4pPr" == name)
			{
				return new Level4ParagraphProperties();
			}
			if (10 == namespaceId && "lvl5pPr" == name)
			{
				return new Level5ParagraphProperties();
			}
			if (10 == namespaceId && "lvl6pPr" == name)
			{
				return new Level6ParagraphProperties();
			}
			if (10 == namespaceId && "lvl7pPr" == name)
			{
				return new Level7ParagraphProperties();
			}
			if (10 == namespaceId && "lvl8pPr" == name)
			{
				return new Level8ParagraphProperties();
			}
			if (10 == namespaceId && "lvl9pPr" == name)
			{
				return new Level9ParagraphProperties();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006433 RID: 25651
		// (get) Token: 0x06013E09 RID: 81417 RVA: 0x0030CBAE File Offset: 0x0030ADAE
		internal override string[] ElementTagNames
		{
			get
			{
				return ListStyle.eleTagNames;
			}
		}

		// Token: 0x17006434 RID: 25652
		// (get) Token: 0x06013E0A RID: 81418 RVA: 0x0030CBB5 File Offset: 0x0030ADB5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ListStyle.eleNamespaceIds;
			}
		}

		// Token: 0x17006435 RID: 25653
		// (get) Token: 0x06013E0B RID: 81419 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006436 RID: 25654
		// (get) Token: 0x06013E0C RID: 81420 RVA: 0x0030CBBC File Offset: 0x0030ADBC
		// (set) Token: 0x06013E0D RID: 81421 RVA: 0x0030CBC5 File Offset: 0x0030ADC5
		public DefaultParagraphProperties DefaultParagraphProperties
		{
			get
			{
				return base.GetElement<DefaultParagraphProperties>(0);
			}
			set
			{
				base.SetElement<DefaultParagraphProperties>(0, value);
			}
		}

		// Token: 0x17006437 RID: 25655
		// (get) Token: 0x06013E0E RID: 81422 RVA: 0x0030CBCF File Offset: 0x0030ADCF
		// (set) Token: 0x06013E0F RID: 81423 RVA: 0x0030CBD8 File Offset: 0x0030ADD8
		public Level1ParagraphProperties Level1ParagraphProperties
		{
			get
			{
				return base.GetElement<Level1ParagraphProperties>(1);
			}
			set
			{
				base.SetElement<Level1ParagraphProperties>(1, value);
			}
		}

		// Token: 0x17006438 RID: 25656
		// (get) Token: 0x06013E10 RID: 81424 RVA: 0x0030CBE2 File Offset: 0x0030ADE2
		// (set) Token: 0x06013E11 RID: 81425 RVA: 0x0030CBEB File Offset: 0x0030ADEB
		public Level2ParagraphProperties Level2ParagraphProperties
		{
			get
			{
				return base.GetElement<Level2ParagraphProperties>(2);
			}
			set
			{
				base.SetElement<Level2ParagraphProperties>(2, value);
			}
		}

		// Token: 0x17006439 RID: 25657
		// (get) Token: 0x06013E12 RID: 81426 RVA: 0x0030CBF5 File Offset: 0x0030ADF5
		// (set) Token: 0x06013E13 RID: 81427 RVA: 0x0030CBFE File Offset: 0x0030ADFE
		public Level3ParagraphProperties Level3ParagraphProperties
		{
			get
			{
				return base.GetElement<Level3ParagraphProperties>(3);
			}
			set
			{
				base.SetElement<Level3ParagraphProperties>(3, value);
			}
		}

		// Token: 0x1700643A RID: 25658
		// (get) Token: 0x06013E14 RID: 81428 RVA: 0x0030CC08 File Offset: 0x0030AE08
		// (set) Token: 0x06013E15 RID: 81429 RVA: 0x0030CC11 File Offset: 0x0030AE11
		public Level4ParagraphProperties Level4ParagraphProperties
		{
			get
			{
				return base.GetElement<Level4ParagraphProperties>(4);
			}
			set
			{
				base.SetElement<Level4ParagraphProperties>(4, value);
			}
		}

		// Token: 0x1700643B RID: 25659
		// (get) Token: 0x06013E16 RID: 81430 RVA: 0x0030CC1B File Offset: 0x0030AE1B
		// (set) Token: 0x06013E17 RID: 81431 RVA: 0x0030CC24 File Offset: 0x0030AE24
		public Level5ParagraphProperties Level5ParagraphProperties
		{
			get
			{
				return base.GetElement<Level5ParagraphProperties>(5);
			}
			set
			{
				base.SetElement<Level5ParagraphProperties>(5, value);
			}
		}

		// Token: 0x1700643C RID: 25660
		// (get) Token: 0x06013E18 RID: 81432 RVA: 0x0030CC2E File Offset: 0x0030AE2E
		// (set) Token: 0x06013E19 RID: 81433 RVA: 0x0030CC37 File Offset: 0x0030AE37
		public Level6ParagraphProperties Level6ParagraphProperties
		{
			get
			{
				return base.GetElement<Level6ParagraphProperties>(6);
			}
			set
			{
				base.SetElement<Level6ParagraphProperties>(6, value);
			}
		}

		// Token: 0x1700643D RID: 25661
		// (get) Token: 0x06013E1A RID: 81434 RVA: 0x0030CC41 File Offset: 0x0030AE41
		// (set) Token: 0x06013E1B RID: 81435 RVA: 0x0030CC4A File Offset: 0x0030AE4A
		public Level7ParagraphProperties Level7ParagraphProperties
		{
			get
			{
				return base.GetElement<Level7ParagraphProperties>(7);
			}
			set
			{
				base.SetElement<Level7ParagraphProperties>(7, value);
			}
		}

		// Token: 0x1700643E RID: 25662
		// (get) Token: 0x06013E1C RID: 81436 RVA: 0x0030CC54 File Offset: 0x0030AE54
		// (set) Token: 0x06013E1D RID: 81437 RVA: 0x0030CC5D File Offset: 0x0030AE5D
		public Level8ParagraphProperties Level8ParagraphProperties
		{
			get
			{
				return base.GetElement<Level8ParagraphProperties>(8);
			}
			set
			{
				base.SetElement<Level8ParagraphProperties>(8, value);
			}
		}

		// Token: 0x1700643F RID: 25663
		// (get) Token: 0x06013E1E RID: 81438 RVA: 0x0030CC67 File Offset: 0x0030AE67
		// (set) Token: 0x06013E1F RID: 81439 RVA: 0x0030CC71 File Offset: 0x0030AE71
		public Level9ParagraphProperties Level9ParagraphProperties
		{
			get
			{
				return base.GetElement<Level9ParagraphProperties>(9);
			}
			set
			{
				base.SetElement<Level9ParagraphProperties>(9, value);
			}
		}

		// Token: 0x17006440 RID: 25664
		// (get) Token: 0x06013E20 RID: 81440 RVA: 0x0030CC7C File Offset: 0x0030AE7C
		// (set) Token: 0x06013E21 RID: 81441 RVA: 0x0030CC86 File Offset: 0x0030AE86
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(10);
			}
			set
			{
				base.SetElement<ExtensionList>(10, value);
			}
		}

		// Token: 0x06013E22 RID: 81442 RVA: 0x0030CC91 File Offset: 0x0030AE91
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ListStyle>(deep);
		}

		// Token: 0x04008825 RID: 34853
		private const string tagName = "lstStyle";

		// Token: 0x04008826 RID: 34854
		private const byte tagNsId = 10;

		// Token: 0x04008827 RID: 34855
		internal const int ElementTypeIdConst = 10240;

		// Token: 0x04008828 RID: 34856
		private static readonly string[] eleTagNames = new string[]
		{
			"defPPr", "lvl1pPr", "lvl2pPr", "lvl3pPr", "lvl4pPr", "lvl5pPr", "lvl6pPr", "lvl7pPr", "lvl8pPr", "lvl9pPr",
			"extLst"
		};

		// Token: 0x04008829 RID: 34857
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
			10
		};
	}
}
