using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B91 RID: 11153
	[GeneratedCode("DomGen", "2.0")]
	internal class DeletedField : OpenXmlLeafElement
	{
		// Token: 0x17007AFE RID: 31486
		// (get) Token: 0x060171EC RID: 94700 RVA: 0x00333053 File Offset: 0x00331253
		public override string LocalName
		{
			get
			{
				return "deletedField";
			}
		}

		// Token: 0x17007AFF RID: 31487
		// (get) Token: 0x060171ED RID: 94701 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B00 RID: 31488
		// (get) Token: 0x060171EE RID: 94702 RVA: 0x0033305A File Offset: 0x0033125A
		internal override int ElementTypeId
		{
			get
			{
				return 11132;
			}
		}

		// Token: 0x060171EF RID: 94703 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007B01 RID: 31489
		// (get) Token: 0x060171F0 RID: 94704 RVA: 0x00333061 File Offset: 0x00331261
		internal override string[] AttributeTagNames
		{
			get
			{
				return DeletedField.attributeTagNames;
			}
		}

		// Token: 0x17007B02 RID: 31490
		// (get) Token: 0x060171F1 RID: 94705 RVA: 0x00333068 File Offset: 0x00331268
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DeletedField.attributeNamespaceIds;
			}
		}

		// Token: 0x17007B03 RID: 31491
		// (get) Token: 0x060171F2 RID: 94706 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060171F3 RID: 94707 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x060171F5 RID: 94709 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060171F6 RID: 94710 RVA: 0x0033306F File Offset: 0x0033126F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DeletedField>(deep);
		}

		// Token: 0x060171F7 RID: 94711 RVA: 0x00333078 File Offset: 0x00331278
		// Note: this type is marked as 'beforefieldinit'.
		static DeletedField()
		{
			byte[] array = new byte[1];
			DeletedField.attributeNamespaceIds = array;
		}

		// Token: 0x04009B1E RID: 39710
		private const string tagName = "deletedField";

		// Token: 0x04009B1F RID: 39711
		private const byte tagNsId = 22;

		// Token: 0x04009B20 RID: 39712
		internal const int ElementTypeIdConst = 11132;

		// Token: 0x04009B21 RID: 39713
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x04009B22 RID: 39714
		private static byte[] attributeNamespaceIds;
	}
}
