using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B4A RID: 11082
	[GeneratedCode("DomGen", "2.0")]
	internal class CharacterValue : OpenXmlLeafElement
	{
		// Token: 0x1700780E RID: 30734
		// (get) Token: 0x06016B85 RID: 93061 RVA: 0x0032E52E File Offset: 0x0032C72E
		public override string LocalName
		{
			get
			{
				return "s";
			}
		}

		// Token: 0x1700780F RID: 30735
		// (get) Token: 0x06016B86 RID: 93062 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007810 RID: 30736
		// (get) Token: 0x06016B87 RID: 93063 RVA: 0x0032E535 File Offset: 0x0032C735
		internal override int ElementTypeId
		{
			get
			{
				return 11065;
			}
		}

		// Token: 0x06016B88 RID: 93064 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007811 RID: 30737
		// (get) Token: 0x06016B89 RID: 93065 RVA: 0x0032E53C File Offset: 0x0032C73C
		internal override string[] AttributeTagNames
		{
			get
			{
				return CharacterValue.attributeTagNames;
			}
		}

		// Token: 0x17007812 RID: 30738
		// (get) Token: 0x06016B8A RID: 93066 RVA: 0x0032E543 File Offset: 0x0032C743
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CharacterValue.attributeNamespaceIds;
			}
		}

		// Token: 0x17007813 RID: 30739
		// (get) Token: 0x06016B8B RID: 93067 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016B8C RID: 93068 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "v")]
		public StringValue Val
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

		// Token: 0x06016B8E RID: 93070 RVA: 0x0032E54A File Offset: 0x0032C74A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "v" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016B8F RID: 93071 RVA: 0x0032E56A File Offset: 0x0032C76A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CharacterValue>(deep);
		}

		// Token: 0x06016B90 RID: 93072 RVA: 0x0032E574 File Offset: 0x0032C774
		// Note: this type is marked as 'beforefieldinit'.
		static CharacterValue()
		{
			byte[] array = new byte[1];
			CharacterValue.attributeNamespaceIds = array;
		}

		// Token: 0x040099AE RID: 39342
		private const string tagName = "s";

		// Token: 0x040099AF RID: 39343
		private const byte tagNsId = 22;

		// Token: 0x040099B0 RID: 39344
		internal const int ElementTypeIdConst = 11065;

		// Token: 0x040099B1 RID: 39345
		private static string[] attributeTagNames = new string[] { "v" };

		// Token: 0x040099B2 RID: 39346
		private static byte[] attributeNamespaceIds;
	}
}
