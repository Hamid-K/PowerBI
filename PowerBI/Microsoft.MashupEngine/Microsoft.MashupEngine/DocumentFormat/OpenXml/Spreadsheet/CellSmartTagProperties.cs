using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BD6 RID: 11222
	[GeneratedCode("DomGen", "2.0")]
	internal class CellSmartTagProperties : OpenXmlLeafElement
	{
		// Token: 0x17007D56 RID: 32086
		// (get) Token: 0x060176E6 RID: 95974 RVA: 0x00336B62 File Offset: 0x00334D62
		public override string LocalName
		{
			get
			{
				return "cellSmartTagPr";
			}
		}

		// Token: 0x17007D57 RID: 32087
		// (get) Token: 0x060176E7 RID: 95975 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007D58 RID: 32088
		// (get) Token: 0x060176E8 RID: 95976 RVA: 0x00336B69 File Offset: 0x00334D69
		internal override int ElementTypeId
		{
			get
			{
				return 11195;
			}
		}

		// Token: 0x060176E9 RID: 95977 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007D59 RID: 32089
		// (get) Token: 0x060176EA RID: 95978 RVA: 0x00336B70 File Offset: 0x00334D70
		internal override string[] AttributeTagNames
		{
			get
			{
				return CellSmartTagProperties.attributeTagNames;
			}
		}

		// Token: 0x17007D5A RID: 32090
		// (get) Token: 0x060176EB RID: 95979 RVA: 0x00336B77 File Offset: 0x00334D77
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CellSmartTagProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17007D5B RID: 32091
		// (get) Token: 0x060176EC RID: 95980 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060176ED RID: 95981 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "key")]
		public StringValue Key
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007D5C RID: 32092
		// (get) Token: 0x060176EE RID: 95982 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060176EF RID: 95983 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "val")]
		public StringValue Val
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060176F1 RID: 95985 RVA: 0x00336B7E File Offset: 0x00334D7E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "key" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060176F2 RID: 95986 RVA: 0x00336BB4 File Offset: 0x00334DB4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellSmartTagProperties>(deep);
		}

		// Token: 0x060176F3 RID: 95987 RVA: 0x00336BC0 File Offset: 0x00334DC0
		// Note: this type is marked as 'beforefieldinit'.
		static CellSmartTagProperties()
		{
			byte[] array = new byte[2];
			CellSmartTagProperties.attributeNamespaceIds = array;
		}

		// Token: 0x04009C51 RID: 40017
		private const string tagName = "cellSmartTagPr";

		// Token: 0x04009C52 RID: 40018
		private const byte tagNsId = 22;

		// Token: 0x04009C53 RID: 40019
		internal const int ElementTypeIdConst = 11195;

		// Token: 0x04009C54 RID: 40020
		private static string[] attributeTagNames = new string[] { "key", "val" };

		// Token: 0x04009C55 RID: 40021
		private static byte[] attributeNamespaceIds;
	}
}
