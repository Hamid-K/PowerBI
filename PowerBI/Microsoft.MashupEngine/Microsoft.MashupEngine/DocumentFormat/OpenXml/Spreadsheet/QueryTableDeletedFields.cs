using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B90 RID: 11152
	[ChildElementInfo(typeof(DeletedField))]
	[GeneratedCode("DomGen", "2.0")]
	internal class QueryTableDeletedFields : OpenXmlCompositeElement
	{
		// Token: 0x17007AF8 RID: 31480
		// (get) Token: 0x060171DC RID: 94684 RVA: 0x00332FE3 File Offset: 0x003311E3
		public override string LocalName
		{
			get
			{
				return "queryTableDeletedFields";
			}
		}

		// Token: 0x17007AF9 RID: 31481
		// (get) Token: 0x060171DD RID: 94685 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007AFA RID: 31482
		// (get) Token: 0x060171DE RID: 94686 RVA: 0x00332FEA File Offset: 0x003311EA
		internal override int ElementTypeId
		{
			get
			{
				return 11131;
			}
		}

		// Token: 0x060171DF RID: 94687 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007AFB RID: 31483
		// (get) Token: 0x060171E0 RID: 94688 RVA: 0x00332FF1 File Offset: 0x003311F1
		internal override string[] AttributeTagNames
		{
			get
			{
				return QueryTableDeletedFields.attributeTagNames;
			}
		}

		// Token: 0x17007AFC RID: 31484
		// (get) Token: 0x060171E1 RID: 94689 RVA: 0x00332FF8 File Offset: 0x003311F8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return QueryTableDeletedFields.attributeNamespaceIds;
			}
		}

		// Token: 0x17007AFD RID: 31485
		// (get) Token: 0x060171E2 RID: 94690 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060171E3 RID: 94691 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060171E4 RID: 94692 RVA: 0x00293ECF File Offset: 0x002920CF
		public QueryTableDeletedFields()
		{
		}

		// Token: 0x060171E5 RID: 94693 RVA: 0x00293ED7 File Offset: 0x002920D7
		public QueryTableDeletedFields(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060171E6 RID: 94694 RVA: 0x00293EE0 File Offset: 0x002920E0
		public QueryTableDeletedFields(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060171E7 RID: 94695 RVA: 0x00293EE9 File Offset: 0x002920E9
		public QueryTableDeletedFields(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060171E8 RID: 94696 RVA: 0x00332FFF File Offset: 0x003311FF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "deletedField" == name)
			{
				return new DeletedField();
			}
			return null;
		}

		// Token: 0x060171E9 RID: 94697 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060171EA RID: 94698 RVA: 0x0033301A File Offset: 0x0033121A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<QueryTableDeletedFields>(deep);
		}

		// Token: 0x060171EB RID: 94699 RVA: 0x00333024 File Offset: 0x00331224
		// Note: this type is marked as 'beforefieldinit'.
		static QueryTableDeletedFields()
		{
			byte[] array = new byte[1];
			QueryTableDeletedFields.attributeNamespaceIds = array;
		}

		// Token: 0x04009B19 RID: 39705
		private const string tagName = "queryTableDeletedFields";

		// Token: 0x04009B1A RID: 39706
		private const byte tagNsId = 22;

		// Token: 0x04009B1B RID: 39707
		internal const int ElementTypeIdConst = 11131;

		// Token: 0x04009B1C RID: 39708
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009B1D RID: 39709
		private static byte[] attributeNamespaceIds;
	}
}
