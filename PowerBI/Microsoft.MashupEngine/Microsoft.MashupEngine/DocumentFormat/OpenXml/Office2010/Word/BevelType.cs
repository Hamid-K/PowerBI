using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024C9 RID: 9417
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class BevelType : OpenXmlLeafElement
	{
		// Token: 0x170052ED RID: 21229
		// (get) Token: 0x06011784 RID: 71556 RVA: 0x002EEC80 File Offset: 0x002ECE80
		internal override string[] AttributeTagNames
		{
			get
			{
				return BevelType.attributeTagNames;
			}
		}

		// Token: 0x170052EE RID: 21230
		// (get) Token: 0x06011785 RID: 71557 RVA: 0x002EEC87 File Offset: 0x002ECE87
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BevelType.attributeNamespaceIds;
			}
		}

		// Token: 0x170052EF RID: 21231
		// (get) Token: 0x06011786 RID: 71558 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06011787 RID: 71559 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "w")]
		public Int64Value Width
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

		// Token: 0x170052F0 RID: 21232
		// (get) Token: 0x06011788 RID: 71560 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x06011789 RID: 71561 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(52, "h")]
		public Int64Value Height
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

		// Token: 0x170052F1 RID: 21233
		// (get) Token: 0x0601178A RID: 71562 RVA: 0x002EEC8E File Offset: 0x002ECE8E
		// (set) Token: 0x0601178B RID: 71563 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(52, "prst")]
		public EnumValue<BevelPresetTypeValues> PresetProfileType
		{
			get
			{
				return (EnumValue<BevelPresetTypeValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601178C RID: 71564 RVA: 0x002EECA0 File Offset: 0x002ECEA0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "w" == name)
			{
				return new Int64Value();
			}
			if (52 == namespaceId && "h" == name)
			{
				return new Int64Value();
			}
			if (52 == namespaceId && "prst" == name)
			{
				return new EnumValue<BevelPresetTypeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x040079FB RID: 31227
		private static string[] attributeTagNames = new string[] { "w", "h", "prst" };

		// Token: 0x040079FC RID: 31228
		private static byte[] attributeNamespaceIds = new byte[] { 52, 52, 52 };
	}
}
