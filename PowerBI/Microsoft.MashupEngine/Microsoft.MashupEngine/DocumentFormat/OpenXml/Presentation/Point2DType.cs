using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A7F RID: 10879
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class Point2DType : OpenXmlLeafElement
	{
		// Token: 0x1700734D RID: 29517
		// (get) Token: 0x06016096 RID: 90262 RVA: 0x00325EB1 File Offset: 0x003240B1
		internal override string[] AttributeTagNames
		{
			get
			{
				return Point2DType.attributeTagNames;
			}
		}

		// Token: 0x1700734E RID: 29518
		// (get) Token: 0x06016097 RID: 90263 RVA: 0x00325EB8 File Offset: 0x003240B8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Point2DType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700734F RID: 29519
		// (get) Token: 0x06016098 RID: 90264 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06016099 RID: 90265 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007350 RID: 29520
		// (get) Token: 0x0601609A RID: 90266 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x0601609B RID: 90267 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x0601609C RID: 90268 RVA: 0x00308403 File Offset: 0x00306603
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

		// Token: 0x0601609E RID: 90270 RVA: 0x00325EC0 File Offset: 0x003240C0
		// Note: this type is marked as 'beforefieldinit'.
		static Point2DType()
		{
			byte[] array = new byte[2];
			Point2DType.attributeNamespaceIds = array;
		}

		// Token: 0x040095E9 RID: 38377
		private static string[] attributeTagNames = new string[] { "x", "y" };

		// Token: 0x040095EA RID: 38378
		private static byte[] attributeNamespaceIds;
	}
}
