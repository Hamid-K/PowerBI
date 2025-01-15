using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F70 RID: 12144
	[GeneratedCode("DomGen", "2.0")]
	internal class MailMergeFieldType : OpenXmlLeafElement
	{
		// Token: 0x170090E9 RID: 37097
		// (get) Token: 0x0601A1F1 RID: 106993 RVA: 0x0031CE60 File Offset: 0x0031B060
		public override string LocalName
		{
			get
			{
				return "type";
			}
		}

		// Token: 0x170090EA RID: 37098
		// (get) Token: 0x0601A1F2 RID: 106994 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170090EB RID: 37099
		// (get) Token: 0x0601A1F3 RID: 106995 RVA: 0x0035DAD8 File Offset: 0x0035BCD8
		internal override int ElementTypeId
		{
			get
			{
				return 11800;
			}
		}

		// Token: 0x0601A1F4 RID: 106996 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170090EC RID: 37100
		// (get) Token: 0x0601A1F5 RID: 106997 RVA: 0x0035DADF File Offset: 0x0035BCDF
		internal override string[] AttributeTagNames
		{
			get
			{
				return MailMergeFieldType.attributeTagNames;
			}
		}

		// Token: 0x170090ED RID: 37101
		// (get) Token: 0x0601A1F6 RID: 106998 RVA: 0x0035DAE6 File Offset: 0x0035BCE6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MailMergeFieldType.attributeNamespaceIds;
			}
		}

		// Token: 0x170090EE RID: 37102
		// (get) Token: 0x0601A1F7 RID: 106999 RVA: 0x0035DAED File Offset: 0x0035BCED
		// (set) Token: 0x0601A1F8 RID: 107000 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<MailMergeOdsoFieldValues> Val
		{
			get
			{
				return (EnumValue<MailMergeOdsoFieldValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A1FA RID: 107002 RVA: 0x0035DAFC File Offset: 0x0035BCFC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<MailMergeOdsoFieldValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A1FB RID: 107003 RVA: 0x0035DB1E File Offset: 0x0035BD1E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MailMergeFieldType>(deep);
		}

		// Token: 0x0400ABE5 RID: 44005
		private const string tagName = "type";

		// Token: 0x0400ABE6 RID: 44006
		private const byte tagNsId = 23;

		// Token: 0x0400ABE7 RID: 44007
		internal const int ElementTypeIdConst = 11800;

		// Token: 0x0400ABE8 RID: 44008
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ABE9 RID: 44009
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
