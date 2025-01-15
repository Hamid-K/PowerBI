using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002555 RID: 9557
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TickMarkType : OpenXmlLeafElement
	{
		// Token: 0x17005575 RID: 21877
		// (get) Token: 0x06011D1D RID: 72989 RVA: 0x002F2CC0 File Offset: 0x002F0EC0
		internal override string[] AttributeTagNames
		{
			get
			{
				return TickMarkType.attributeTagNames;
			}
		}

		// Token: 0x17005576 RID: 21878
		// (get) Token: 0x06011D1E RID: 72990 RVA: 0x002F2CC7 File Offset: 0x002F0EC7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TickMarkType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005577 RID: 21879
		// (get) Token: 0x06011D1F RID: 72991 RVA: 0x002F2CCE File Offset: 0x002F0ECE
		// (set) Token: 0x06011D20 RID: 72992 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<TickMarkValues> Val
		{
			get
			{
				return (EnumValue<TickMarkValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011D21 RID: 72993 RVA: 0x002F2CDD File Offset: 0x002F0EDD
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<TickMarkValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011D23 RID: 72995 RVA: 0x002F2D00 File Offset: 0x002F0F00
		// Note: this type is marked as 'beforefieldinit'.
		static TickMarkType()
		{
			byte[] array = new byte[1];
			TickMarkType.attributeNamespaceIds = array;
		}

		// Token: 0x04007CAD RID: 31917
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007CAE RID: 31918
		private static byte[] attributeNamespaceIds;
	}
}
