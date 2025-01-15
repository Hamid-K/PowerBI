using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BE7 RID: 11239
	[GeneratedCode("DomGen", "2.0")]
	internal class TablePart : OpenXmlLeafElement
	{
		// Token: 0x17007E0E RID: 32270
		// (get) Token: 0x0601786B RID: 96363 RVA: 0x00337F07 File Offset: 0x00336107
		public override string LocalName
		{
			get
			{
				return "tablePart";
			}
		}

		// Token: 0x17007E0F RID: 32271
		// (get) Token: 0x0601786C RID: 96364 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007E10 RID: 32272
		// (get) Token: 0x0601786D RID: 96365 RVA: 0x00337F0E File Offset: 0x0033610E
		internal override int ElementTypeId
		{
			get
			{
				return 11211;
			}
		}

		// Token: 0x0601786E RID: 96366 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007E11 RID: 32273
		// (get) Token: 0x0601786F RID: 96367 RVA: 0x00337F15 File Offset: 0x00336115
		internal override string[] AttributeTagNames
		{
			get
			{
				return TablePart.attributeTagNames;
			}
		}

		// Token: 0x17007E12 RID: 32274
		// (get) Token: 0x06017870 RID: 96368 RVA: 0x00337F1C File Offset: 0x0033611C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TablePart.attributeNamespaceIds;
			}
		}

		// Token: 0x17007E13 RID: 32275
		// (get) Token: 0x06017871 RID: 96369 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017872 RID: 96370 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x06017874 RID: 96372 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017875 RID: 96373 RVA: 0x00337F23 File Offset: 0x00336123
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TablePart>(deep);
		}

		// Token: 0x04009CA3 RID: 40099
		private const string tagName = "tablePart";

		// Token: 0x04009CA4 RID: 40100
		private const byte tagNsId = 22;

		// Token: 0x04009CA5 RID: 40101
		internal const int ElementTypeIdConst = 11211;

		// Token: 0x04009CA6 RID: 40102
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x04009CA7 RID: 40103
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
