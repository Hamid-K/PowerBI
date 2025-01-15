using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CDF RID: 11487
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CustomFilter))]
	internal class CustomFilters : OpenXmlCompositeElement
	{
		// Token: 0x17008603 RID: 34307
		// (get) Token: 0x06018ABC RID: 101052 RVA: 0x002E6B87 File Offset: 0x002E4D87
		public override string LocalName
		{
			get
			{
				return "customFilters";
			}
		}

		// Token: 0x17008604 RID: 34308
		// (get) Token: 0x06018ABD RID: 101053 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008605 RID: 34309
		// (get) Token: 0x06018ABE RID: 101054 RVA: 0x00343D2F File Offset: 0x00341F2F
		internal override int ElementTypeId
		{
			get
			{
				return 11469;
			}
		}

		// Token: 0x06018ABF RID: 101055 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008606 RID: 34310
		// (get) Token: 0x06018AC0 RID: 101056 RVA: 0x00343D36 File Offset: 0x00341F36
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomFilters.attributeTagNames;
			}
		}

		// Token: 0x17008607 RID: 34311
		// (get) Token: 0x06018AC1 RID: 101057 RVA: 0x00343D3D File Offset: 0x00341F3D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomFilters.attributeNamespaceIds;
			}
		}

		// Token: 0x17008608 RID: 34312
		// (get) Token: 0x06018AC2 RID: 101058 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06018AC3 RID: 101059 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "and")]
		public BooleanValue And
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06018AC4 RID: 101060 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomFilters()
		{
		}

		// Token: 0x06018AC5 RID: 101061 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomFilters(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018AC6 RID: 101062 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomFilters(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018AC7 RID: 101063 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomFilters(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018AC8 RID: 101064 RVA: 0x00343D44 File Offset: 0x00341F44
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "customFilter" == name)
			{
				return new CustomFilter();
			}
			return null;
		}

		// Token: 0x06018AC9 RID: 101065 RVA: 0x002E6BBE File Offset: 0x002E4DBE
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "and" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018ACA RID: 101066 RVA: 0x00343D5F File Offset: 0x00341F5F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomFilters>(deep);
		}

		// Token: 0x06018ACB RID: 101067 RVA: 0x00343D68 File Offset: 0x00341F68
		// Note: this type is marked as 'beforefieldinit'.
		static CustomFilters()
		{
			byte[] array = new byte[1];
			CustomFilters.attributeNamespaceIds = array;
		}

		// Token: 0x0400A13C RID: 41276
		private const string tagName = "customFilters";

		// Token: 0x0400A13D RID: 41277
		private const byte tagNsId = 22;

		// Token: 0x0400A13E RID: 41278
		internal const int ElementTypeIdConst = 11469;

		// Token: 0x0400A13F RID: 41279
		private static string[] attributeTagNames = new string[] { "and" };

		// Token: 0x0400A140 RID: 41280
		private static byte[] attributeNamespaceIds;
	}
}
