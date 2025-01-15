using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002587 RID: 9607
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class LayoutModeType : OpenXmlLeafElement
	{
		// Token: 0x17005647 RID: 22087
		// (get) Token: 0x06011F0A RID: 73482 RVA: 0x002F3DE7 File Offset: 0x002F1FE7
		internal override string[] AttributeTagNames
		{
			get
			{
				return LayoutModeType.attributeTagNames;
			}
		}

		// Token: 0x17005648 RID: 22088
		// (get) Token: 0x06011F0B RID: 73483 RVA: 0x002F3DEE File Offset: 0x002F1FEE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LayoutModeType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005649 RID: 22089
		// (get) Token: 0x06011F0C RID: 73484 RVA: 0x002F3DF5 File Offset: 0x002F1FF5
		// (set) Token: 0x06011F0D RID: 73485 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<LayoutModeValues> Val
		{
			get
			{
				return (EnumValue<LayoutModeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011F0E RID: 73486 RVA: 0x002F3E04 File Offset: 0x002F2004
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<LayoutModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011F10 RID: 73488 RVA: 0x002F3E24 File Offset: 0x002F2024
		// Note: this type is marked as 'beforefieldinit'.
		static LayoutModeType()
		{
			byte[] array = new byte[1];
			LayoutModeType.attributeNamespaceIds = array;
		}

		// Token: 0x04007D58 RID: 32088
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007D59 RID: 32089
		private static byte[] attributeNamespaceIds;
	}
}
