using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BA8 RID: 11176
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class InternationalPropertyType : OpenXmlLeafElement
	{
		// Token: 0x17007B64 RID: 31588
		// (get) Token: 0x060172C7 RID: 94919 RVA: 0x0033371B File Offset: 0x0033191B
		internal override string[] AttributeTagNames
		{
			get
			{
				return InternationalPropertyType.attributeTagNames;
			}
		}

		// Token: 0x17007B65 RID: 31589
		// (get) Token: 0x060172C8 RID: 94920 RVA: 0x00333722 File Offset: 0x00331922
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return InternationalPropertyType.attributeNamespaceIds;
			}
		}

		// Token: 0x17007B66 RID: 31590
		// (get) Token: 0x060172C9 RID: 94921 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060172CA RID: 94922 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060172CB RID: 94923 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060172CD RID: 94925 RVA: 0x0033372C File Offset: 0x0033192C
		// Note: this type is marked as 'beforefieldinit'.
		static InternationalPropertyType()
		{
			byte[] array = new byte[1];
			InternationalPropertyType.attributeNamespaceIds = array;
		}

		// Token: 0x04009B6E RID: 39790
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009B6F RID: 39791
		private static byte[] attributeNamespaceIds;
	}
}
