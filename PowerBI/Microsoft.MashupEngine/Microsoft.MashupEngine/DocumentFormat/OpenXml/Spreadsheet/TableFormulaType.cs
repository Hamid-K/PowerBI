using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C2C RID: 11308
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TableFormulaType : OpenXmlLeafTextElement
	{
		// Token: 0x170080D5 RID: 32981
		// (get) Token: 0x06017E73 RID: 97907 RVA: 0x0033C673 File Offset: 0x0033A873
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableFormulaType.attributeTagNames;
			}
		}

		// Token: 0x170080D6 RID: 32982
		// (get) Token: 0x06017E74 RID: 97908 RVA: 0x0033C67A File Offset: 0x0033A87A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableFormulaType.attributeNamespaceIds;
			}
		}

		// Token: 0x170080D7 RID: 32983
		// (get) Token: 0x06017E75 RID: 97909 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06017E76 RID: 97910 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "array")]
		public BooleanValue Array
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170080D8 RID: 32984
		// (get) Token: 0x06017E77 RID: 97911 RVA: 0x0033C681 File Offset: 0x0033A881
		// (set) Token: 0x06017E78 RID: 97912 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(1, "space")]
		public EnumValue<SpaceProcessingModeValues> Space
		{
			get
			{
				return (EnumValue<SpaceProcessingModeValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06017E79 RID: 97913 RVA: 0x0033C690 File Offset: 0x0033A890
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "array" == name)
			{
				return new BooleanValue();
			}
			if (1 == namespaceId && "space" == name)
			{
				return new EnumValue<SpaceProcessingModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017E7A RID: 97914 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		protected TableFormulaType()
		{
		}

		// Token: 0x06017E7B RID: 97915 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		protected TableFormulaType(string text)
			: base(text)
		{
		}

		// Token: 0x06017E7C RID: 97916 RVA: 0x0033C6C8 File Offset: 0x0033A8C8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x04009E11 RID: 40465
		private static string[] attributeTagNames = new string[] { "array", "space" };

		// Token: 0x04009E12 RID: 40466
		private static byte[] attributeNamespaceIds = new byte[] { 0, 1 };
	}
}
