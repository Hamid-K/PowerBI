using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A7B RID: 10875
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class NormalViewPortionType : OpenXmlLeafElement
	{
		// Token: 0x1700733B RID: 29499
		// (get) Token: 0x0601606F RID: 90223 RVA: 0x00325D3F File Offset: 0x00323F3F
		internal override string[] AttributeTagNames
		{
			get
			{
				return NormalViewPortionType.attributeTagNames;
			}
		}

		// Token: 0x1700733C RID: 29500
		// (get) Token: 0x06016070 RID: 90224 RVA: 0x00325D46 File Offset: 0x00323F46
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NormalViewPortionType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700733D RID: 29501
		// (get) Token: 0x06016071 RID: 90225 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06016072 RID: 90226 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "sz")]
		public Int32Value Size
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700733E RID: 29502
		// (get) Token: 0x06016073 RID: 90227 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06016074 RID: 90228 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "autoAdjust")]
		public BooleanValue AutoAdjust
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06016075 RID: 90229 RVA: 0x00325D4D File Offset: 0x00323F4D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "sz" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "autoAdjust" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016077 RID: 90231 RVA: 0x00325D84 File Offset: 0x00323F84
		// Note: this type is marked as 'beforefieldinit'.
		static NormalViewPortionType()
		{
			byte[] array = new byte[2];
			NormalViewPortionType.attributeNamespaceIds = array;
		}

		// Token: 0x040095DC RID: 38364
		private static string[] attributeTagNames = new string[] { "sz", "autoAdjust" };

		// Token: 0x040095DD RID: 38365
		private static byte[] attributeNamespaceIds;
	}
}
