using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C9E RID: 11422
	[ChildElementInfo(typeof(CustomProperty))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomProperties : OpenXmlCompositeElement
	{
		// Token: 0x17008433 RID: 33843
		// (get) Token: 0x0601864F RID: 99919 RVA: 0x003413DD File Offset: 0x0033F5DD
		public override string LocalName
		{
			get
			{
				return "customProperties";
			}
		}

		// Token: 0x17008434 RID: 33844
		// (get) Token: 0x06018650 RID: 99920 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008435 RID: 33845
		// (get) Token: 0x06018651 RID: 99921 RVA: 0x003413E4 File Offset: 0x0033F5E4
		internal override int ElementTypeId
		{
			get
			{
				return 11402;
			}
		}

		// Token: 0x06018652 RID: 99922 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018653 RID: 99923 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomProperties()
		{
		}

		// Token: 0x06018654 RID: 99924 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018655 RID: 99925 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018656 RID: 99926 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018657 RID: 99927 RVA: 0x003413EB File Offset: 0x0033F5EB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "customPr" == name)
			{
				return new CustomProperty();
			}
			return null;
		}

		// Token: 0x06018658 RID: 99928 RVA: 0x00341406 File Offset: 0x0033F606
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomProperties>(deep);
		}

		// Token: 0x0400A00F RID: 40975
		private const string tagName = "customProperties";

		// Token: 0x0400A010 RID: 40976
		private const byte tagNsId = 22;

		// Token: 0x0400A011 RID: 40977
		internal const int ElementTypeIdConst = 11402;
	}
}
