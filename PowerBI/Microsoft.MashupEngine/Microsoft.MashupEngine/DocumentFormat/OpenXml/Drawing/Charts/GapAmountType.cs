using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025A4 RID: 9636
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class GapAmountType : OpenXmlLeafElement
	{
		// Token: 0x170056F0 RID: 22256
		// (get) Token: 0x06012079 RID: 73849 RVA: 0x002F4E40 File Offset: 0x002F3040
		internal override string[] AttributeTagNames
		{
			get
			{
				return GapAmountType.attributeTagNames;
			}
		}

		// Token: 0x170056F1 RID: 22257
		// (get) Token: 0x0601207A RID: 73850 RVA: 0x002F4E47 File Offset: 0x002F3047
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GapAmountType.attributeNamespaceIds;
			}
		}

		// Token: 0x170056F2 RID: 22258
		// (get) Token: 0x0601207B RID: 73851 RVA: 0x002F0704 File Offset: 0x002EE904
		// (set) Token: 0x0601207C RID: 73852 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public UInt16Value Val
		{
			get
			{
				return (UInt16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601207D RID: 73853 RVA: 0x002F41C3 File Offset: 0x002F23C3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new UInt16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601207F RID: 73855 RVA: 0x002F4E50 File Offset: 0x002F3050
		// Note: this type is marked as 'beforefieldinit'.
		static GapAmountType()
		{
			byte[] array = new byte[1];
			GapAmountType.attributeNamespaceIds = array;
		}

		// Token: 0x04007DD1 RID: 32209
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007DD2 RID: 32210
		private static byte[] attributeNamespaceIds;
	}
}
