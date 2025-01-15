using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FFD RID: 12285
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TableWidthDxaNilType : OpenXmlLeafElement
	{
		// Token: 0x170095D7 RID: 38359
		// (get) Token: 0x0601AC68 RID: 109672 RVA: 0x003676CC File Offset: 0x003658CC
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableWidthDxaNilType.attributeTagNames;
			}
		}

		// Token: 0x170095D8 RID: 38360
		// (get) Token: 0x0601AC69 RID: 109673 RVA: 0x003676D3 File Offset: 0x003658D3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableWidthDxaNilType.attributeNamespaceIds;
			}
		}

		// Token: 0x170095D9 RID: 38361
		// (get) Token: 0x0601AC6A RID: 109674 RVA: 0x0034726F File Offset: 0x0034546F
		// (set) Token: 0x0601AC6B RID: 109675 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "w")]
		public Int16Value Width
		{
			get
			{
				return (Int16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170095DA RID: 38362
		// (get) Token: 0x0601AC6C RID: 109676 RVA: 0x003676DA File Offset: 0x003658DA
		// (set) Token: 0x0601AC6D RID: 109677 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "type")]
		public EnumValue<TableWidthValues> Type
		{
			get
			{
				return (EnumValue<TableWidthValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601AC6E RID: 109678 RVA: 0x003676E9 File Offset: 0x003658E9
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "w" == name)
			{
				return new Int16Value();
			}
			if (23 == namespaceId && "type" == name)
			{
				return new EnumValue<TableWidthValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400AE45 RID: 44613
		private static string[] attributeTagNames = new string[] { "w", "type" };

		// Token: 0x0400AE46 RID: 44614
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
