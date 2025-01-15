using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C63 RID: 11363
	[ChildElementInfo(typeof(TableColumn))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TableColumns : OpenXmlCompositeElement
	{
		// Token: 0x17008290 RID: 33424
		// (get) Token: 0x0601826C RID: 98924 RVA: 0x0033EFCA File Offset: 0x0033D1CA
		public override string LocalName
		{
			get
			{
				return "tableColumns";
			}
		}

		// Token: 0x17008291 RID: 33425
		// (get) Token: 0x0601826D RID: 98925 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008292 RID: 33426
		// (get) Token: 0x0601826E RID: 98926 RVA: 0x0033EFD1 File Offset: 0x0033D1D1
		internal override int ElementTypeId
		{
			get
			{
				return 11344;
			}
		}

		// Token: 0x0601826F RID: 98927 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008293 RID: 33427
		// (get) Token: 0x06018270 RID: 98928 RVA: 0x0033EFD8 File Offset: 0x0033D1D8
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableColumns.attributeTagNames;
			}
		}

		// Token: 0x17008294 RID: 33428
		// (get) Token: 0x06018271 RID: 98929 RVA: 0x0033EFDF File Offset: 0x0033D1DF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableColumns.attributeNamespaceIds;
			}
		}

		// Token: 0x17008295 RID: 33429
		// (get) Token: 0x06018272 RID: 98930 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018273 RID: 98931 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018274 RID: 98932 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableColumns()
		{
		}

		// Token: 0x06018275 RID: 98933 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableColumns(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018276 RID: 98934 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableColumns(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018277 RID: 98935 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableColumns(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018278 RID: 98936 RVA: 0x0033EFE6 File Offset: 0x0033D1E6
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "tableColumn" == name)
			{
				return new TableColumn();
			}
			return null;
		}

		// Token: 0x06018279 RID: 98937 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601827A RID: 98938 RVA: 0x0033F001 File Offset: 0x0033D201
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableColumns>(deep);
		}

		// Token: 0x0601827B RID: 98939 RVA: 0x0033F00C File Offset: 0x0033D20C
		// Note: this type is marked as 'beforefieldinit'.
		static TableColumns()
		{
			byte[] array = new byte[1];
			TableColumns.attributeNamespaceIds = array;
		}

		// Token: 0x04009F10 RID: 40720
		private const string tagName = "tableColumns";

		// Token: 0x04009F11 RID: 40721
		private const byte tagNsId = 22;

		// Token: 0x04009F12 RID: 40722
		internal const int ElementTypeIdConst = 11344;

		// Token: 0x04009F13 RID: 40723
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009F14 RID: 40724
		private static byte[] attributeNamespaceIds;
	}
}
