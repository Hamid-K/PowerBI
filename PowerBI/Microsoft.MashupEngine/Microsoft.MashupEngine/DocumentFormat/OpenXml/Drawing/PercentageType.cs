using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026E0 RID: 9952
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class PercentageType : OpenXmlLeafElement
	{
		// Token: 0x17005DC8 RID: 24008
		// (get) Token: 0x06012FBB RID: 77755 RVA: 0x0030180F File Offset: 0x002FFA0F
		internal override string[] AttributeTagNames
		{
			get
			{
				return PercentageType.attributeTagNames;
			}
		}

		// Token: 0x17005DC9 RID: 24009
		// (get) Token: 0x06012FBC RID: 77756 RVA: 0x00301816 File Offset: 0x002FFA16
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PercentageType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005DCA RID: 24010
		// (get) Token: 0x06012FBD RID: 77757 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06012FBE RID: 77758 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012FBF RID: 77759 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012FC1 RID: 77761 RVA: 0x00301820 File Offset: 0x002FFA20
		// Note: this type is marked as 'beforefieldinit'.
		static PercentageType()
		{
			byte[] array = new byte[1];
			PercentageType.attributeNamespaceIds = array;
		}

		// Token: 0x0400840B RID: 33803
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400840C RID: 33804
		private static byte[] attributeNamespaceIds;
	}
}
