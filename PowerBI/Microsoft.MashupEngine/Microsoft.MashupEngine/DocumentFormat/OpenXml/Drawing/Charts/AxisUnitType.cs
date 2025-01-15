using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025BD RID: 9661
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class AxisUnitType : OpenXmlLeafElement
	{
		// Token: 0x17005773 RID: 22387
		// (get) Token: 0x06012194 RID: 74132 RVA: 0x002F5823 File Offset: 0x002F3A23
		internal override string[] AttributeTagNames
		{
			get
			{
				return AxisUnitType.attributeTagNames;
			}
		}

		// Token: 0x17005774 RID: 22388
		// (get) Token: 0x06012195 RID: 74133 RVA: 0x002F582A File Offset: 0x002F3A2A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AxisUnitType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005775 RID: 22389
		// (get) Token: 0x06012196 RID: 74134 RVA: 0x002E7DC5 File Offset: 0x002E5FC5
		// (set) Token: 0x06012197 RID: 74135 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public DoubleValue Val
		{
			get
			{
				return (DoubleValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012198 RID: 74136 RVA: 0x002F2E7D File Offset: 0x002F107D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601219A RID: 74138 RVA: 0x002F5834 File Offset: 0x002F3A34
		// Note: this type is marked as 'beforefieldinit'.
		static AxisUnitType()
		{
			byte[] array = new byte[1];
			AxisUnitType.attributeNamespaceIds = array;
		}

		// Token: 0x04007E37 RID: 32311
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007E38 RID: 32312
		private static byte[] attributeNamespaceIds;
	}
}
