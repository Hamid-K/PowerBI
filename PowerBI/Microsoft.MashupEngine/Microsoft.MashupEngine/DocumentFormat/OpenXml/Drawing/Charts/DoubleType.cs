using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200255A RID: 9562
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class DoubleType : OpenXmlLeafElement
	{
		// Token: 0x1700558A RID: 21898
		// (get) Token: 0x06011D48 RID: 73032 RVA: 0x002F2E6F File Offset: 0x002F106F
		internal override string[] AttributeTagNames
		{
			get
			{
				return DoubleType.attributeTagNames;
			}
		}

		// Token: 0x1700558B RID: 21899
		// (get) Token: 0x06011D49 RID: 73033 RVA: 0x002F2E76 File Offset: 0x002F1076
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DoubleType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700558C RID: 21900
		// (get) Token: 0x06011D4A RID: 73034 RVA: 0x002E7DC5 File Offset: 0x002E5FC5
		// (set) Token: 0x06011D4B RID: 73035 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06011D4C RID: 73036 RVA: 0x002F2E7D File Offset: 0x002F107D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011D4E RID: 73038 RVA: 0x002F2EA0 File Offset: 0x002F10A0
		// Note: this type is marked as 'beforefieldinit'.
		static DoubleType()
		{
			byte[] array = new byte[1];
			DoubleType.attributeNamespaceIds = array;
		}

		// Token: 0x04007CBF RID: 31935
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007CC0 RID: 31936
		private static byte[] attributeNamespaceIds;
	}
}
