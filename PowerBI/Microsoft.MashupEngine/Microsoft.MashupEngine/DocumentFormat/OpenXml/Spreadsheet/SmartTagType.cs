using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C39 RID: 11321
	[GeneratedCode("DomGen", "2.0")]
	internal class SmartTagType : OpenXmlLeafElement
	{
		// Token: 0x1700814B RID: 33099
		// (get) Token: 0x06017F71 RID: 98161 RVA: 0x0033D218 File Offset: 0x0033B418
		public override string LocalName
		{
			get
			{
				return "smartTagType";
			}
		}

		// Token: 0x1700814C RID: 33100
		// (get) Token: 0x06017F72 RID: 98162 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700814D RID: 33101
		// (get) Token: 0x06017F73 RID: 98163 RVA: 0x0033D21F File Offset: 0x0033B41F
		internal override int ElementTypeId
		{
			get
			{
				return 11303;
			}
		}

		// Token: 0x06017F74 RID: 98164 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700814E RID: 33102
		// (get) Token: 0x06017F75 RID: 98165 RVA: 0x0033D226 File Offset: 0x0033B426
		internal override string[] AttributeTagNames
		{
			get
			{
				return SmartTagType.attributeTagNames;
			}
		}

		// Token: 0x1700814F RID: 33103
		// (get) Token: 0x06017F76 RID: 98166 RVA: 0x0033D22D File Offset: 0x0033B42D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SmartTagType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008150 RID: 33104
		// (get) Token: 0x06017F77 RID: 98167 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017F78 RID: 98168 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "namespaceUri")]
		public StringValue SmartTagNamespace
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

		// Token: 0x17008151 RID: 33105
		// (get) Token: 0x06017F79 RID: 98169 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017F7A RID: 98170 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x17008152 RID: 33106
		// (get) Token: 0x06017F7B RID: 98171 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06017F7C RID: 98172 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "url")]
		public StringValue Url
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06017F7E RID: 98174 RVA: 0x0033D234 File Offset: 0x0033B434
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "namespaceUri" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "url" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017F7F RID: 98175 RVA: 0x0033D28B File Offset: 0x0033B48B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SmartTagType>(deep);
		}

		// Token: 0x06017F80 RID: 98176 RVA: 0x0033D294 File Offset: 0x0033B494
		// Note: this type is marked as 'beforefieldinit'.
		static SmartTagType()
		{
			byte[] array = new byte[3];
			SmartTagType.attributeNamespaceIds = array;
		}

		// Token: 0x04009E51 RID: 40529
		private const string tagName = "smartTagType";

		// Token: 0x04009E52 RID: 40530
		private const byte tagNsId = 22;

		// Token: 0x04009E53 RID: 40531
		internal const int ElementTypeIdConst = 11303;

		// Token: 0x04009E54 RID: 40532
		private static string[] attributeTagNames = new string[] { "namespaceUri", "name", "url" };

		// Token: 0x04009E55 RID: 40533
		private static byte[] attributeNamespaceIds;
	}
}
