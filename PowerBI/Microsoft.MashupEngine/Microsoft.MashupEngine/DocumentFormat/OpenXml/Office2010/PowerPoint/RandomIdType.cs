using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023B5 RID: 9141
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class RandomIdType : OpenXmlLeafElement
	{
		// Token: 0x17004C76 RID: 19574
		// (get) Token: 0x060108FA RID: 67834 RVA: 0x002E4BC1 File Offset: 0x002E2DC1
		internal override string[] AttributeTagNames
		{
			get
			{
				return RandomIdType.attributeTagNames;
			}
		}

		// Token: 0x17004C77 RID: 19575
		// (get) Token: 0x060108FB RID: 67835 RVA: 0x002E4BC8 File Offset: 0x002E2DC8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RandomIdType.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C78 RID: 19576
		// (get) Token: 0x060108FC RID: 67836 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060108FD RID: 67837 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public UInt32Value Val
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060108FE RID: 67838 RVA: 0x002E4A8C File Offset: 0x002E2C8C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010900 RID: 67840 RVA: 0x002E4BD0 File Offset: 0x002E2DD0
		// Note: this type is marked as 'beforefieldinit'.
		static RandomIdType()
		{
			byte[] array = new byte[1];
			RandomIdType.attributeNamespaceIds = array;
		}

		// Token: 0x04007543 RID: 30019
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007544 RID: 30020
		private static byte[] attributeNamespaceIds;
	}
}
