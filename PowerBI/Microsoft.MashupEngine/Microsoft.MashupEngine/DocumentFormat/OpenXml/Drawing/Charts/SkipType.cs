using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025B9 RID: 9657
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class SkipType : OpenXmlLeafElement
	{
		// Token: 0x17005764 RID: 22372
		// (get) Token: 0x06012175 RID: 74101 RVA: 0x002F5707 File Offset: 0x002F3907
		internal override string[] AttributeTagNames
		{
			get
			{
				return SkipType.attributeTagNames;
			}
		}

		// Token: 0x17005765 RID: 22373
		// (get) Token: 0x06012176 RID: 74102 RVA: 0x002F570E File Offset: 0x002F390E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SkipType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005766 RID: 22374
		// (get) Token: 0x06012177 RID: 74103 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06012178 RID: 74104 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012179 RID: 74105 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601217B RID: 74107 RVA: 0x002F5738 File Offset: 0x002F3938
		// Note: this type is marked as 'beforefieldinit'.
		static SkipType()
		{
			byte[] array = new byte[1];
			SkipType.attributeNamespaceIds = array;
		}

		// Token: 0x04007E2A RID: 32298
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007E2B RID: 32299
		private static byte[] attributeNamespaceIds;
	}
}
