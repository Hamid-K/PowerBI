using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EAD RID: 11949
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class LanguageType : OpenXmlLeafElement
	{
		// Token: 0x17008BAC RID: 35756
		// (get) Token: 0x0601964A RID: 104010 RVA: 0x003492A8 File Offset: 0x003474A8
		internal override string[] AttributeTagNames
		{
			get
			{
				return LanguageType.attributeTagNames;
			}
		}

		// Token: 0x17008BAD RID: 35757
		// (get) Token: 0x0601964B RID: 104011 RVA: 0x003492AF File Offset: 0x003474AF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LanguageType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008BAE RID: 35758
		// (get) Token: 0x0601964C RID: 104012 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601964D RID: 104013 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
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

		// Token: 0x17008BAF RID: 35759
		// (get) Token: 0x0601964E RID: 104014 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601964F RID: 104015 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "eastAsia")]
		public StringValue EastAsia
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

		// Token: 0x17008BB0 RID: 35760
		// (get) Token: 0x06019650 RID: 104016 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06019651 RID: 104017 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "bidi")]
		public StringValue Bidi
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

		// Token: 0x06019652 RID: 104018 RVA: 0x003492B8 File Offset: 0x003474B8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "eastAsia" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "bidi" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A8C3 RID: 43203
		private static string[] attributeTagNames = new string[] { "val", "eastAsia", "bidi" };

		// Token: 0x0400A8C4 RID: 43204
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
