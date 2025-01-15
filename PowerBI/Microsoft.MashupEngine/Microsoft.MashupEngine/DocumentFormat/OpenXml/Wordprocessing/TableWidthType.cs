using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EDA RID: 11994
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TableWidthType : OpenXmlLeafElement
	{
		// Token: 0x17008D2E RID: 36142
		// (get) Token: 0x060199A8 RID: 104872 RVA: 0x0035334A File Offset: 0x0035154A
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableWidthType.attributeTagNames;
			}
		}

		// Token: 0x17008D2F RID: 36143
		// (get) Token: 0x060199A9 RID: 104873 RVA: 0x00353351 File Offset: 0x00351551
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableWidthType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008D30 RID: 36144
		// (get) Token: 0x060199AA RID: 104874 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060199AB RID: 104875 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "w")]
		public StringValue Width
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008D31 RID: 36145
		// (get) Token: 0x060199AC RID: 104876 RVA: 0x00353358 File Offset: 0x00351558
		// (set) Token: 0x060199AD RID: 104877 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "type")]
		public EnumValue<TableWidthUnitValues> Type
		{
			get
			{
				return (EnumValue<TableWidthUnitValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060199AE RID: 104878 RVA: 0x00353367 File Offset: 0x00351567
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "w" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "type" == name)
			{
				return new EnumValue<TableWidthUnitValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A997 RID: 43415
		private static string[] attributeTagNames = new string[] { "w", "type" };

		// Token: 0x0400A998 RID: 43416
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
