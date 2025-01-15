using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Charts
{
	// Token: 0x0200231C RID: 8988
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class BooleanFalseType : OpenXmlLeafElement
	{
		// Token: 0x17004850 RID: 18512
		// (get) Token: 0x0600FFEF RID: 65519 RVA: 0x002DE6AE File Offset: 0x002DC8AE
		internal override string[] AttributeTagNames
		{
			get
			{
				return BooleanFalseType.attributeTagNames;
			}
		}

		// Token: 0x17004851 RID: 18513
		// (get) Token: 0x0600FFF0 RID: 65520 RVA: 0x002DE6B5 File Offset: 0x002DC8B5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BooleanFalseType.attributeNamespaceIds;
			}
		}

		// Token: 0x17004852 RID: 18514
		// (get) Token: 0x0600FFF1 RID: 65521 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0600FFF2 RID: 65522 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0600FFF3 RID: 65523 RVA: 0x002DE6BC File Offset: 0x002DC8BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FFF5 RID: 65525 RVA: 0x002DE6DC File Offset: 0x002DC8DC
		// Note: this type is marked as 'beforefieldinit'.
		static BooleanFalseType()
		{
			byte[] array = new byte[1];
			BooleanFalseType.attributeNamespaceIds = array;
		}

		// Token: 0x04007297 RID: 29335
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007298 RID: 29336
		private static byte[] attributeNamespaceIds;
	}
}
