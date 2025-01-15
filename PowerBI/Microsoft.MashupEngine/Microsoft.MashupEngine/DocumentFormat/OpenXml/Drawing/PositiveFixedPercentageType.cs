using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026D3 RID: 9939
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class PositiveFixedPercentageType : OpenXmlLeafElement
	{
		// Token: 0x17005D98 RID: 23960
		// (get) Token: 0x06012F59 RID: 77657 RVA: 0x003015E3 File Offset: 0x002FF7E3
		internal override string[] AttributeTagNames
		{
			get
			{
				return PositiveFixedPercentageType.attributeTagNames;
			}
		}

		// Token: 0x17005D99 RID: 23961
		// (get) Token: 0x06012F5A RID: 77658 RVA: 0x003015EA File Offset: 0x002FF7EA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PositiveFixedPercentageType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005D9A RID: 23962
		// (get) Token: 0x06012F5B RID: 77659 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06012F5C RID: 77660 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012F5D RID: 77661 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012F5F RID: 77663 RVA: 0x003015F4 File Offset: 0x002FF7F4
		// Note: this type is marked as 'beforefieldinit'.
		static PositiveFixedPercentageType()
		{
			byte[] array = new byte[1];
			PositiveFixedPercentageType.attributeNamespaceIds = array;
		}

		// Token: 0x040083E0 RID: 33760
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040083E1 RID: 33761
		private static byte[] attributeNamespaceIds;
	}
}
