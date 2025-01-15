using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002734 RID: 10036
	[ChildElementInfo(typeof(LatinFont))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ComplexScriptFont))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(EastAsianFont))]
	[ChildElementInfo(typeof(SupplementalFont))]
	internal abstract class FontCollectionType : OpenXmlCompositeElement
	{
		// Token: 0x060134CA RID: 79050 RVA: 0x00305DA8 File Offset: 0x00303FA8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "latin" == name)
			{
				return new LatinFont();
			}
			if (10 == namespaceId && "ea" == name)
			{
				return new EastAsianFont();
			}
			if (10 == namespaceId && "cs" == name)
			{
				return new ComplexScriptFont();
			}
			if (10 == namespaceId && "font" == name)
			{
				return new SupplementalFont();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006021 RID: 24609
		// (get) Token: 0x060134CB RID: 79051 RVA: 0x00305E2E File Offset: 0x0030402E
		internal override string[] ElementTagNames
		{
			get
			{
				return FontCollectionType.eleTagNames;
			}
		}

		// Token: 0x17006022 RID: 24610
		// (get) Token: 0x060134CC RID: 79052 RVA: 0x00305E35 File Offset: 0x00304035
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FontCollectionType.eleNamespaceIds;
			}
		}

		// Token: 0x17006023 RID: 24611
		// (get) Token: 0x060134CD RID: 79053 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006024 RID: 24612
		// (get) Token: 0x060134CE RID: 79054 RVA: 0x00305E3C File Offset: 0x0030403C
		// (set) Token: 0x060134CF RID: 79055 RVA: 0x00305E45 File Offset: 0x00304045
		public LatinFont LatinFont
		{
			get
			{
				return base.GetElement<LatinFont>(0);
			}
			set
			{
				base.SetElement<LatinFont>(0, value);
			}
		}

		// Token: 0x17006025 RID: 24613
		// (get) Token: 0x060134D0 RID: 79056 RVA: 0x00305E4F File Offset: 0x0030404F
		// (set) Token: 0x060134D1 RID: 79057 RVA: 0x00305E58 File Offset: 0x00304058
		public EastAsianFont EastAsianFont
		{
			get
			{
				return base.GetElement<EastAsianFont>(1);
			}
			set
			{
				base.SetElement<EastAsianFont>(1, value);
			}
		}

		// Token: 0x17006026 RID: 24614
		// (get) Token: 0x060134D2 RID: 79058 RVA: 0x00305E62 File Offset: 0x00304062
		// (set) Token: 0x060134D3 RID: 79059 RVA: 0x00305E6B File Offset: 0x0030406B
		public ComplexScriptFont ComplexScriptFont
		{
			get
			{
				return base.GetElement<ComplexScriptFont>(2);
			}
			set
			{
				base.SetElement<ComplexScriptFont>(2, value);
			}
		}

		// Token: 0x060134D4 RID: 79060 RVA: 0x00293ECF File Offset: 0x002920CF
		protected FontCollectionType()
		{
		}

		// Token: 0x060134D5 RID: 79061 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected FontCollectionType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134D6 RID: 79062 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected FontCollectionType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134D7 RID: 79063 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected FontCollectionType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04008586 RID: 34182
		private static readonly string[] eleTagNames = new string[] { "latin", "ea", "cs", "font", "extLst" };

		// Token: 0x04008587 RID: 34183
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10 };
	}
}
