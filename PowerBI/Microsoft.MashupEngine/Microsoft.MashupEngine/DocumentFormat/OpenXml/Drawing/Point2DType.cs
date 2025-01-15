using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200278B RID: 10123
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class Point2DType : OpenXmlLeafElement
	{
		// Token: 0x170061D8 RID: 25048
		// (get) Token: 0x060138E7 RID: 80103 RVA: 0x003083F5 File Offset: 0x003065F5
		internal override string[] AttributeTagNames
		{
			get
			{
				return Point2DType.attributeTagNames;
			}
		}

		// Token: 0x170061D9 RID: 25049
		// (get) Token: 0x060138E8 RID: 80104 RVA: 0x003083FC File Offset: 0x003065FC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Point2DType.attributeNamespaceIds;
			}
		}

		// Token: 0x170061DA RID: 25050
		// (get) Token: 0x060138E9 RID: 80105 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x060138EA RID: 80106 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "x")]
		public Int64Value X
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

		// Token: 0x170061DB RID: 25051
		// (get) Token: 0x060138EB RID: 80107 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x060138EC RID: 80108 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "y")]
		public Int64Value Y
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

		// Token: 0x060138ED RID: 80109 RVA: 0x00308403 File Offset: 0x00306603
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "x" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "y" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060138EF RID: 80111 RVA: 0x0030843C File Offset: 0x0030663C
		// Note: this type is marked as 'beforefieldinit'.
		static Point2DType()
		{
			byte[] array = new byte[2];
			Point2DType.attributeNamespaceIds = array;
		}

		// Token: 0x040086BD RID: 34493
		private static string[] attributeTagNames = new string[] { "x", "y" };

		// Token: 0x040086BE RID: 34494
		private static byte[] attributeNamespaceIds;
	}
}
