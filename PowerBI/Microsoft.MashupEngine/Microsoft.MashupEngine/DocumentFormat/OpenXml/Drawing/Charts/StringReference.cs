using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002585 RID: 9605
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Formula))]
	[ChildElementInfo(typeof(StringCache))]
	internal class StringReference : OpenXmlCompositeElement
	{
		// Token: 0x17005638 RID: 22072
		// (get) Token: 0x06011EEA RID: 73450 RVA: 0x002F3C8C File Offset: 0x002F1E8C
		public override string LocalName
		{
			get
			{
				return "strRef";
			}
		}

		// Token: 0x17005639 RID: 22073
		// (get) Token: 0x06011EEB RID: 73451 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700563A RID: 22074
		// (get) Token: 0x06011EEC RID: 73452 RVA: 0x002F3C93 File Offset: 0x002F1E93
		internal override int ElementTypeId
		{
			get
			{
				return 10404;
			}
		}

		// Token: 0x06011EED RID: 73453 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011EEE RID: 73454 RVA: 0x00293ECF File Offset: 0x002920CF
		public StringReference()
		{
		}

		// Token: 0x06011EEF RID: 73455 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StringReference(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011EF0 RID: 73456 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StringReference(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011EF1 RID: 73457 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StringReference(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011EF2 RID: 73458 RVA: 0x002F3C9C File Offset: 0x002F1E9C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "f" == name)
			{
				return new Formula();
			}
			if (11 == namespaceId && "strCache" == name)
			{
				return new StringCache();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700563B RID: 22075
		// (get) Token: 0x06011EF3 RID: 73459 RVA: 0x002F3CF2 File Offset: 0x002F1EF2
		internal override string[] ElementTagNames
		{
			get
			{
				return StringReference.eleTagNames;
			}
		}

		// Token: 0x1700563C RID: 22076
		// (get) Token: 0x06011EF4 RID: 73460 RVA: 0x002F3CF9 File Offset: 0x002F1EF9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return StringReference.eleNamespaceIds;
			}
		}

		// Token: 0x1700563D RID: 22077
		// (get) Token: 0x06011EF5 RID: 73461 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700563E RID: 22078
		// (get) Token: 0x06011EF6 RID: 73462 RVA: 0x002F3878 File Offset: 0x002F1A78
		// (set) Token: 0x06011EF7 RID: 73463 RVA: 0x002F3881 File Offset: 0x002F1A81
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

		// Token: 0x1700563F RID: 22079
		// (get) Token: 0x06011EF8 RID: 73464 RVA: 0x002F3D00 File Offset: 0x002F1F00
		// (set) Token: 0x06011EF9 RID: 73465 RVA: 0x002F3D09 File Offset: 0x002F1F09
		public StringCache StringCache
		{
			get
			{
				return base.GetElement<StringCache>(1);
			}
			set
			{
				base.SetElement<StringCache>(1, value);
			}
		}

		// Token: 0x17005640 RID: 22080
		// (get) Token: 0x06011EFA RID: 73466 RVA: 0x002F389E File Offset: 0x002F1A9E
		// (set) Token: 0x06011EFB RID: 73467 RVA: 0x002F38A7 File Offset: 0x002F1AA7
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

		// Token: 0x06011EFC RID: 73468 RVA: 0x002F3D13 File Offset: 0x002F1F13
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StringReference>(deep);
		}

		// Token: 0x04007D4E RID: 32078
		private const string tagName = "strRef";

		// Token: 0x04007D4F RID: 32079
		private const byte tagNsId = 11;

		// Token: 0x04007D50 RID: 32080
		internal const int ElementTypeIdConst = 10404;

		// Token: 0x04007D51 RID: 32081
		private static readonly string[] eleTagNames = new string[] { "f", "strCache", "extLst" };

		// Token: 0x04007D52 RID: 32082
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11 };
	}
}
