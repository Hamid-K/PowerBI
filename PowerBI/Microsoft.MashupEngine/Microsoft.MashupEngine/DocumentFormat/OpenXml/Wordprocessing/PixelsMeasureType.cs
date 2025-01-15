using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F87 RID: 12167
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class PixelsMeasureType : OpenXmlLeafElement
	{
		// Token: 0x170091AA RID: 37290
		// (get) Token: 0x0601A38D RID: 107405 RVA: 0x0035F3F8 File Offset: 0x0035D5F8
		internal override string[] AttributeTagNames
		{
			get
			{
				return PixelsMeasureType.attributeTagNames;
			}
		}

		// Token: 0x170091AB RID: 37291
		// (get) Token: 0x0601A38E RID: 107406 RVA: 0x0035F3FF File Offset: 0x0035D5FF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PixelsMeasureType.attributeNamespaceIds;
			}
		}

		// Token: 0x170091AC RID: 37292
		// (get) Token: 0x0601A38F RID: 107407 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601A390 RID: 107408 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
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

		// Token: 0x0601A391 RID: 107409 RVA: 0x00348AE4 File Offset: 0x00346CE4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400AC44 RID: 44100
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AC45 RID: 44101
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
