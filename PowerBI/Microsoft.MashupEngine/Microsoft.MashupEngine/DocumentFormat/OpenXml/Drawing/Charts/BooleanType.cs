using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002509 RID: 9481
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class BooleanType : OpenXmlLeafElement
	{
		// Token: 0x17005434 RID: 21556
		// (get) Token: 0x06011A62 RID: 72290 RVA: 0x002F11C7 File Offset: 0x002EF3C7
		internal override string[] AttributeTagNames
		{
			get
			{
				return BooleanType.attributeTagNames;
			}
		}

		// Token: 0x17005435 RID: 21557
		// (get) Token: 0x06011A63 RID: 72291 RVA: 0x002F11CE File Offset: 0x002EF3CE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BooleanType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005436 RID: 21558
		// (get) Token: 0x06011A64 RID: 72292 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06011A65 RID: 72293 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public BooleanValue Val
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

		// Token: 0x06011A66 RID: 72294 RVA: 0x002DE6BC File Offset: 0x002DC8BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011A68 RID: 72296 RVA: 0x002F11D8 File Offset: 0x002EF3D8
		// Note: this type is marked as 'beforefieldinit'.
		static BooleanType()
		{
			byte[] array = new byte[1];
			BooleanType.attributeNamespaceIds = array;
		}

		// Token: 0x04007BB0 RID: 31664
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007BB1 RID: 31665
		private static byte[] attributeNamespaceIds;
	}
}
