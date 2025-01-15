using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CB3 RID: 11443
	[ChildElementInfo(typeof(PageField))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PageFields : OpenXmlCompositeElement
	{
		// Token: 0x1700849A RID: 33946
		// (get) Token: 0x0601876B RID: 100203 RVA: 0x00341B1B File Offset: 0x0033FD1B
		public override string LocalName
		{
			get
			{
				return "pageFields";
			}
		}

		// Token: 0x1700849B RID: 33947
		// (get) Token: 0x0601876C RID: 100204 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700849C RID: 33948
		// (get) Token: 0x0601876D RID: 100205 RVA: 0x00341B22 File Offset: 0x0033FD22
		internal override int ElementTypeId
		{
			get
			{
				return 11423;
			}
		}

		// Token: 0x0601876E RID: 100206 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700849D RID: 33949
		// (get) Token: 0x0601876F RID: 100207 RVA: 0x00341B29 File Offset: 0x0033FD29
		internal override string[] AttributeTagNames
		{
			get
			{
				return PageFields.attributeTagNames;
			}
		}

		// Token: 0x1700849E RID: 33950
		// (get) Token: 0x06018770 RID: 100208 RVA: 0x00341B30 File Offset: 0x0033FD30
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PageFields.attributeNamespaceIds;
			}
		}

		// Token: 0x1700849F RID: 33951
		// (get) Token: 0x06018771 RID: 100209 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018772 RID: 100210 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06018773 RID: 100211 RVA: 0x00293ECF File Offset: 0x002920CF
		public PageFields()
		{
		}

		// Token: 0x06018774 RID: 100212 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PageFields(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018775 RID: 100213 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PageFields(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018776 RID: 100214 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PageFields(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018777 RID: 100215 RVA: 0x00341B37 File Offset: 0x0033FD37
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pageField" == name)
			{
				return new PageField();
			}
			return null;
		}

		// Token: 0x06018778 RID: 100216 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018779 RID: 100217 RVA: 0x00341B52 File Offset: 0x0033FD52
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PageFields>(deep);
		}

		// Token: 0x0601877A RID: 100218 RVA: 0x00341B5C File Offset: 0x0033FD5C
		// Note: this type is marked as 'beforefieldinit'.
		static PageFields()
		{
			byte[] array = new byte[1];
			PageFields.attributeNamespaceIds = array;
		}

		// Token: 0x0400A064 RID: 41060
		private const string tagName = "pageFields";

		// Token: 0x0400A065 RID: 41061
		private const byte tagNsId = 22;

		// Token: 0x0400A066 RID: 41062
		internal const int ElementTypeIdConst = 11423;

		// Token: 0x0400A067 RID: 41063
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A068 RID: 41064
		private static byte[] attributeNamespaceIds;
	}
}
