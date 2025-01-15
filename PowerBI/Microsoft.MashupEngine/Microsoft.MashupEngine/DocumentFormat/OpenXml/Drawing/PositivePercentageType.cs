using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026DB RID: 9947
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class PositivePercentageType : OpenXmlLeafElement
	{
		// Token: 0x17005DB3 RID: 23987
		// (get) Token: 0x06012F90 RID: 77712 RVA: 0x003016F7 File Offset: 0x002FF8F7
		internal override string[] AttributeTagNames
		{
			get
			{
				return PositivePercentageType.attributeTagNames;
			}
		}

		// Token: 0x17005DB4 RID: 23988
		// (get) Token: 0x06012F91 RID: 77713 RVA: 0x003016FE File Offset: 0x002FF8FE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PositivePercentageType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005DB5 RID: 23989
		// (get) Token: 0x06012F92 RID: 77714 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06012F93 RID: 77715 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012F94 RID: 77716 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012F96 RID: 77718 RVA: 0x00301708 File Offset: 0x002FF908
		// Note: this type is marked as 'beforefieldinit'.
		static PositivePercentageType()
		{
			byte[] array = new byte[1];
			PositivePercentageType.attributeNamespaceIds = array;
		}

		// Token: 0x040083F9 RID: 33785
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040083FA RID: 33786
		private static byte[] attributeNamespaceIds;
	}
}
