using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B8F RID: 11151
	[ChildElementInfo(typeof(QueryTableField))]
	[GeneratedCode("DomGen", "2.0")]
	internal class QueryTableFields : OpenXmlCompositeElement
	{
		// Token: 0x17007AF2 RID: 31474
		// (get) Token: 0x060171CC RID: 94668 RVA: 0x00332F72 File Offset: 0x00331172
		public override string LocalName
		{
			get
			{
				return "queryTableFields";
			}
		}

		// Token: 0x17007AF3 RID: 31475
		// (get) Token: 0x060171CD RID: 94669 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007AF4 RID: 31476
		// (get) Token: 0x060171CE RID: 94670 RVA: 0x00332F79 File Offset: 0x00331179
		internal override int ElementTypeId
		{
			get
			{
				return 11130;
			}
		}

		// Token: 0x060171CF RID: 94671 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007AF5 RID: 31477
		// (get) Token: 0x060171D0 RID: 94672 RVA: 0x00332F80 File Offset: 0x00331180
		internal override string[] AttributeTagNames
		{
			get
			{
				return QueryTableFields.attributeTagNames;
			}
		}

		// Token: 0x17007AF6 RID: 31478
		// (get) Token: 0x060171D1 RID: 94673 RVA: 0x00332F87 File Offset: 0x00331187
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return QueryTableFields.attributeNamespaceIds;
			}
		}

		// Token: 0x17007AF7 RID: 31479
		// (get) Token: 0x060171D2 RID: 94674 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060171D3 RID: 94675 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060171D4 RID: 94676 RVA: 0x00293ECF File Offset: 0x002920CF
		public QueryTableFields()
		{
		}

		// Token: 0x060171D5 RID: 94677 RVA: 0x00293ED7 File Offset: 0x002920D7
		public QueryTableFields(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060171D6 RID: 94678 RVA: 0x00293EE0 File Offset: 0x002920E0
		public QueryTableFields(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060171D7 RID: 94679 RVA: 0x00293EE9 File Offset: 0x002920E9
		public QueryTableFields(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060171D8 RID: 94680 RVA: 0x00332F8E File Offset: 0x0033118E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "queryTableField" == name)
			{
				return new QueryTableField();
			}
			return null;
		}

		// Token: 0x060171D9 RID: 94681 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060171DA RID: 94682 RVA: 0x00332FA9 File Offset: 0x003311A9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<QueryTableFields>(deep);
		}

		// Token: 0x060171DB RID: 94683 RVA: 0x00332FB4 File Offset: 0x003311B4
		// Note: this type is marked as 'beforefieldinit'.
		static QueryTableFields()
		{
			byte[] array = new byte[1];
			QueryTableFields.attributeNamespaceIds = array;
		}

		// Token: 0x04009B14 RID: 39700
		private const string tagName = "queryTableFields";

		// Token: 0x04009B15 RID: 39701
		private const byte tagNsId = 22;

		// Token: 0x04009B16 RID: 39702
		internal const int ElementTypeIdConst = 11130;

		// Token: 0x04009B17 RID: 39703
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009B18 RID: 39704
		private static byte[] attributeNamespaceIds;
	}
}
