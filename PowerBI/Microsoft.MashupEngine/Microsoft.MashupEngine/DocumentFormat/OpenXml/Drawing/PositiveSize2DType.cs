using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200278E RID: 10126
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class PositiveSize2DType : OpenXmlLeafElement
	{
		// Token: 0x170061E2 RID: 25058
		// (get) Token: 0x060138FC RID: 80124 RVA: 0x003084A9 File Offset: 0x003066A9
		internal override string[] AttributeTagNames
		{
			get
			{
				return PositiveSize2DType.attributeTagNames;
			}
		}

		// Token: 0x170061E3 RID: 25059
		// (get) Token: 0x060138FD RID: 80125 RVA: 0x003084B0 File Offset: 0x003066B0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PositiveSize2DType.attributeNamespaceIds;
			}
		}

		// Token: 0x170061E4 RID: 25060
		// (get) Token: 0x060138FE RID: 80126 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x060138FF RID: 80127 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "cx")]
		public Int64Value Cx
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170061E5 RID: 25061
		// (get) Token: 0x06013900 RID: 80128 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x06013901 RID: 80129 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "cy")]
		public Int64Value Cy
		{
			get
			{
				return (Int64Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06013902 RID: 80130 RVA: 0x002FCAAF File Offset: 0x002FACAF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "cx" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "cy" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013904 RID: 80132 RVA: 0x003084B8 File Offset: 0x003066B8
		// Note: this type is marked as 'beforefieldinit'.
		static PositiveSize2DType()
		{
			byte[] array = new byte[2];
			PositiveSize2DType.attributeNamespaceIds = array;
		}

		// Token: 0x040086C5 RID: 34501
		private static string[] attributeTagNames = new string[] { "cx", "cy" };

		// Token: 0x040086C6 RID: 34502
		private static byte[] attributeNamespaceIds;
	}
}
