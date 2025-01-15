using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200257D RID: 9597
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Formula))]
	[ChildElementInfo(typeof(NumberingCache))]
	internal class NumberReference : OpenXmlCompositeElement
	{
		// Token: 0x17005608 RID: 22024
		// (get) Token: 0x06011E76 RID: 73334 RVA: 0x002F3805 File Offset: 0x002F1A05
		public override string LocalName
		{
			get
			{
				return "numRef";
			}
		}

		// Token: 0x17005609 RID: 22025
		// (get) Token: 0x06011E77 RID: 73335 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700560A RID: 22026
		// (get) Token: 0x06011E78 RID: 73336 RVA: 0x002F380C File Offset: 0x002F1A0C
		internal override int ElementTypeId
		{
			get
			{
				return 10397;
			}
		}

		// Token: 0x06011E79 RID: 73337 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011E7A RID: 73338 RVA: 0x00293ECF File Offset: 0x002920CF
		public NumberReference()
		{
		}

		// Token: 0x06011E7B RID: 73339 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NumberReference(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011E7C RID: 73340 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NumberReference(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011E7D RID: 73341 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NumberReference(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011E7E RID: 73342 RVA: 0x002F3814 File Offset: 0x002F1A14
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "f" == name)
			{
				return new Formula();
			}
			if (11 == namespaceId && "numCache" == name)
			{
				return new NumberingCache();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700560B RID: 22027
		// (get) Token: 0x06011E7F RID: 73343 RVA: 0x002F386A File Offset: 0x002F1A6A
		internal override string[] ElementTagNames
		{
			get
			{
				return NumberReference.eleTagNames;
			}
		}

		// Token: 0x1700560C RID: 22028
		// (get) Token: 0x06011E80 RID: 73344 RVA: 0x002F3871 File Offset: 0x002F1A71
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NumberReference.eleNamespaceIds;
			}
		}

		// Token: 0x1700560D RID: 22029
		// (get) Token: 0x06011E81 RID: 73345 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700560E RID: 22030
		// (get) Token: 0x06011E82 RID: 73346 RVA: 0x002F3878 File Offset: 0x002F1A78
		// (set) Token: 0x06011E83 RID: 73347 RVA: 0x002F3881 File Offset: 0x002F1A81
		public Formula Formula
		{
			get
			{
				return base.GetElement<Formula>(0);
			}
			set
			{
				base.SetElement<Formula>(0, value);
			}
		}

		// Token: 0x1700560F RID: 22031
		// (get) Token: 0x06011E84 RID: 73348 RVA: 0x002F388B File Offset: 0x002F1A8B
		// (set) Token: 0x06011E85 RID: 73349 RVA: 0x002F3894 File Offset: 0x002F1A94
		public NumberingCache NumberingCache
		{
			get
			{
				return base.GetElement<NumberingCache>(1);
			}
			set
			{
				base.SetElement<NumberingCache>(1, value);
			}
		}

		// Token: 0x17005610 RID: 22032
		// (get) Token: 0x06011E86 RID: 73350 RVA: 0x002F389E File Offset: 0x002F1A9E
		// (set) Token: 0x06011E87 RID: 73351 RVA: 0x002F38A7 File Offset: 0x002F1AA7
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(2);
			}
			set
			{
				base.SetElement<ExtensionList>(2, value);
			}
		}

		// Token: 0x06011E88 RID: 73352 RVA: 0x002F38B1 File Offset: 0x002F1AB1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberReference>(deep);
		}

		// Token: 0x04007D2D RID: 32045
		private const string tagName = "numRef";

		// Token: 0x04007D2E RID: 32046
		private const byte tagNsId = 11;

		// Token: 0x04007D2F RID: 32047
		internal const int ElementTypeIdConst = 10397;

		// Token: 0x04007D30 RID: 32048
		private static readonly string[] eleTagNames = new string[] { "f", "numCache", "extLst" };

		// Token: 0x04007D31 RID: 32049
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11 };
	}
}
