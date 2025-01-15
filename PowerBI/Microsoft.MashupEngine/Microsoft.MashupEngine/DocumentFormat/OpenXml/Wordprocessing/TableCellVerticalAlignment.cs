using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EFF RID: 12031
	[GeneratedCode("DomGen", "2.0")]
	internal class TableCellVerticalAlignment : OpenXmlLeafElement
	{
		// Token: 0x17008DBA RID: 36282
		// (get) Token: 0x06019AC6 RID: 105158 RVA: 0x003475A8 File Offset: 0x003457A8
		public override string LocalName
		{
			get
			{
				return "vAlign";
			}
		}

		// Token: 0x17008DBB RID: 36283
		// (get) Token: 0x06019AC7 RID: 105159 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DBC RID: 36284
		// (get) Token: 0x06019AC8 RID: 105160 RVA: 0x00353BC4 File Offset: 0x00351DC4
		internal override int ElementTypeId
		{
			get
			{
				return 11658;
			}
		}

		// Token: 0x06019AC9 RID: 105161 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008DBD RID: 36285
		// (get) Token: 0x06019ACA RID: 105162 RVA: 0x00353BCB File Offset: 0x00351DCB
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableCellVerticalAlignment.attributeTagNames;
			}
		}

		// Token: 0x17008DBE RID: 36286
		// (get) Token: 0x06019ACB RID: 105163 RVA: 0x00353BD2 File Offset: 0x00351DD2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableCellVerticalAlignment.attributeNamespaceIds;
			}
		}

		// Token: 0x17008DBF RID: 36287
		// (get) Token: 0x06019ACC RID: 105164 RVA: 0x00353BD9 File Offset: 0x00351DD9
		// (set) Token: 0x06019ACD RID: 105165 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<TableVerticalAlignmentValues> Val
		{
			get
			{
				return (EnumValue<TableVerticalAlignmentValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019ACF RID: 105167 RVA: 0x00353BE8 File Offset: 0x00351DE8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<TableVerticalAlignmentValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019AD0 RID: 105168 RVA: 0x00353C0A File Offset: 0x00351E0A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCellVerticalAlignment>(deep);
		}

		// Token: 0x0400AA0C RID: 43532
		private const string tagName = "vAlign";

		// Token: 0x0400AA0D RID: 43533
		private const byte tagNsId = 23;

		// Token: 0x0400AA0E RID: 43534
		internal const int ElementTypeIdConst = 11658;

		// Token: 0x0400AA0F RID: 43535
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AA10 RID: 43536
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
