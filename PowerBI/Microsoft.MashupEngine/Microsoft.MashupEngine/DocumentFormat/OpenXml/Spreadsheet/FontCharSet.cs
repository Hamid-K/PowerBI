using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C18 RID: 11288
	[GeneratedCode("DomGen", "2.0")]
	internal class FontCharSet : OpenXmlLeafElement
	{
		// Token: 0x17008036 RID: 32822
		// (get) Token: 0x06017D0C RID: 97548 RVA: 0x0033377A File Offset: 0x0033197A
		public override string LocalName
		{
			get
			{
				return "charset";
			}
		}

		// Token: 0x17008037 RID: 32823
		// (get) Token: 0x06017D0D RID: 97549 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008038 RID: 32824
		// (get) Token: 0x06017D0E RID: 97550 RVA: 0x0033B83B File Offset: 0x00339A3B
		internal override int ElementTypeId
		{
			get
			{
				return 11269;
			}
		}

		// Token: 0x06017D0F RID: 97551 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008039 RID: 32825
		// (get) Token: 0x06017D10 RID: 97552 RVA: 0x0033B842 File Offset: 0x00339A42
		internal override string[] AttributeTagNames
		{
			get
			{
				return FontCharSet.attributeTagNames;
			}
		}

		// Token: 0x1700803A RID: 32826
		// (get) Token: 0x06017D11 RID: 97553 RVA: 0x0033B849 File Offset: 0x00339A49
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FontCharSet.attributeNamespaceIds;
			}
		}

		// Token: 0x1700803B RID: 32827
		// (get) Token: 0x06017D12 RID: 97554 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06017D13 RID: 97555 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public Int32Value Val
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06017D15 RID: 97557 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017D16 RID: 97558 RVA: 0x0033B850 File Offset: 0x00339A50
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FontCharSet>(deep);
		}

		// Token: 0x06017D17 RID: 97559 RVA: 0x0033B85C File Offset: 0x00339A5C
		// Note: this type is marked as 'beforefieldinit'.
		static FontCharSet()
		{
			byte[] array = new byte[1];
			FontCharSet.attributeNamespaceIds = array;
		}

		// Token: 0x04009DAB RID: 40363
		private const string tagName = "charset";

		// Token: 0x04009DAC RID: 40364
		private const byte tagNsId = 22;

		// Token: 0x04009DAD RID: 40365
		internal const int ElementTypeIdConst = 11269;

		// Token: 0x04009DAE RID: 40366
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009DAF RID: 40367
		private static byte[] attributeNamespaceIds;
	}
}
