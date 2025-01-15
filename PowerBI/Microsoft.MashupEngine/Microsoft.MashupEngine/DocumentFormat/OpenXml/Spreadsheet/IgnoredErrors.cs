using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CA0 RID: 11424
	[ChildElementInfo(typeof(IgnoredError))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class IgnoredErrors : OpenXmlCompositeElement
	{
		// Token: 0x17008439 RID: 33849
		// (get) Token: 0x06018663 RID: 99939 RVA: 0x002E5C8D File Offset: 0x002E3E8D
		public override string LocalName
		{
			get
			{
				return "ignoredErrors";
			}
		}

		// Token: 0x1700843A RID: 33850
		// (get) Token: 0x06018664 RID: 99940 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700843B RID: 33851
		// (get) Token: 0x06018665 RID: 99941 RVA: 0x00341441 File Offset: 0x0033F641
		internal override int ElementTypeId
		{
			get
			{
				return 11404;
			}
		}

		// Token: 0x06018666 RID: 99942 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018667 RID: 99943 RVA: 0x00293ECF File Offset: 0x002920CF
		public IgnoredErrors()
		{
		}

		// Token: 0x06018668 RID: 99944 RVA: 0x00293ED7 File Offset: 0x002920D7
		public IgnoredErrors(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018669 RID: 99945 RVA: 0x00293EE0 File Offset: 0x002920E0
		public IgnoredErrors(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601866A RID: 99946 RVA: 0x00293EE9 File Offset: 0x002920E9
		public IgnoredErrors(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601866B RID: 99947 RVA: 0x00341448 File Offset: 0x0033F648
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ignoredError" == name)
			{
				return new IgnoredError();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x0601866C RID: 99948 RVA: 0x0034147B File Offset: 0x0033F67B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IgnoredErrors>(deep);
		}

		// Token: 0x0400A015 RID: 40981
		private const string tagName = "ignoredErrors";

		// Token: 0x0400A016 RID: 40982
		private const byte tagNsId = 22;

		// Token: 0x0400A017 RID: 40983
		internal const int ElementTypeIdConst = 11404;
	}
}
