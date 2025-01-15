using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Wordprocessing
{
	// Token: 0x02002236 RID: 8758
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class BorderType : OpenXmlLeafElement
	{
		// Token: 0x1700395E RID: 14686
		// (get) Token: 0x0600E06B RID: 57451 RVA: 0x002BFD6B File Offset: 0x002BDF6B
		internal override string[] AttributeTagNames
		{
			get
			{
				return BorderType.attributeTagNames;
			}
		}

		// Token: 0x1700395F RID: 14687
		// (get) Token: 0x0600E06C RID: 57452 RVA: 0x002BFD72 File Offset: 0x002BDF72
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BorderType.attributeNamespaceIds;
			}
		}

		// Token: 0x17003960 RID: 14688
		// (get) Token: 0x0600E06D RID: 57453 RVA: 0x002BFD79 File Offset: 0x002BDF79
		// (set) Token: 0x0600E06E RID: 57454 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<BorderValues> Type
		{
			get
			{
				return (EnumValue<BorderValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003961 RID: 14689
		// (get) Token: 0x0600E06F RID: 57455 RVA: 0x002BD46B File Offset: 0x002BB66B
		// (set) Token: 0x0600E070 RID: 57456 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "width")]
		public IntegerValue Width
		{
			get
			{
				return (IntegerValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17003962 RID: 14690
		// (get) Token: 0x0600E071 RID: 57457 RVA: 0x002BDE2B File Offset: 0x002BC02B
		// (set) Token: 0x0600E072 RID: 57458 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "shadow")]
		public TrueFalseValue Shadow
		{
			get
			{
				return (TrueFalseValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0600E073 RID: 57459 RVA: 0x002BFD88 File Offset: 0x002BDF88
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<BorderValues>();
			}
			if (namespaceId == 0 && "width" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "shadow" == name)
			{
				return new TrueFalseValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E075 RID: 57461 RVA: 0x002BFDE0 File Offset: 0x002BDFE0
		// Note: this type is marked as 'beforefieldinit'.
		static BorderType()
		{
			byte[] array = new byte[3];
			BorderType.attributeNamespaceIds = array;
		}

		// Token: 0x04006E4B RID: 28235
		private static string[] attributeTagNames = new string[] { "type", "width", "shadow" };

		// Token: 0x04006E4C RID: 28236
		private static byte[] attributeNamespaceIds;
	}
}
