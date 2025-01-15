using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023FC RID: 9212
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class ColorType : OpenXmlLeafElement
	{
		// Token: 0x17004E98 RID: 20120
		// (get) Token: 0x06010DB8 RID: 69048 RVA: 0x002E82BF File Offset: 0x002E64BF
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorType.attributeTagNames;
			}
		}

		// Token: 0x17004E99 RID: 20121
		// (get) Token: 0x06010DB9 RID: 69049 RVA: 0x002E82C6 File Offset: 0x002E64C6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorType.attributeNamespaceIds;
			}
		}

		// Token: 0x17004E9A RID: 20122
		// (get) Token: 0x06010DBA RID: 69050 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010DBB RID: 69051 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "auto")]
		public BooleanValue Auto
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004E9B RID: 20123
		// (get) Token: 0x06010DBC RID: 69052 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06010DBD RID: 69053 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "indexed")]
		public UInt32Value Indexed
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004E9C RID: 20124
		// (get) Token: 0x06010DBE RID: 69054 RVA: 0x002E82CD File Offset: 0x002E64CD
		// (set) Token: 0x06010DBF RID: 69055 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "rgb")]
		public HexBinaryValue Rgb
		{
			get
			{
				return (HexBinaryValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17004E9D RID: 20125
		// (get) Token: 0x06010DC0 RID: 69056 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06010DC1 RID: 69057 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "theme")]
		public UInt32Value Theme
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17004E9E RID: 20126
		// (get) Token: 0x06010DC2 RID: 69058 RVA: 0x002E82DC File Offset: 0x002E64DC
		// (set) Token: 0x06010DC3 RID: 69059 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "tint")]
		public DoubleValue Tint
		{
			get
			{
				return (DoubleValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06010DC4 RID: 69060 RVA: 0x002E82EC File Offset: 0x002E64EC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "auto" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "indexed" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "rgb" == name)
			{
				return new HexBinaryValue();
			}
			if (namespaceId == 0 && "theme" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "tint" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010DC6 RID: 69062 RVA: 0x002E8370 File Offset: 0x002E6570
		// Note: this type is marked as 'beforefieldinit'.
		static ColorType()
		{
			byte[] array = new byte[5];
			ColorType.attributeNamespaceIds = array;
		}

		// Token: 0x04007684 RID: 30340
		private static string[] attributeTagNames = new string[] { "auto", "indexed", "rgb", "theme", "tint" };

		// Token: 0x04007685 RID: 30341
		private static byte[] attributeNamespaceIds;
	}
}
