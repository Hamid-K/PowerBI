using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BA2 RID: 11170
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class ColorType : OpenXmlLeafElement
	{
		// Token: 0x17007B4B RID: 31563
		// (get) Token: 0x06017294 RID: 94868 RVA: 0x00333583 File Offset: 0x00331783
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorType.attributeTagNames;
			}
		}

		// Token: 0x17007B4C RID: 31564
		// (get) Token: 0x06017295 RID: 94869 RVA: 0x0033358A File Offset: 0x0033178A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorType.attributeNamespaceIds;
			}
		}

		// Token: 0x17007B4D RID: 31565
		// (get) Token: 0x06017296 RID: 94870 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06017297 RID: 94871 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007B4E RID: 31566
		// (get) Token: 0x06017298 RID: 94872 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017299 RID: 94873 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17007B4F RID: 31567
		// (get) Token: 0x0601729A RID: 94874 RVA: 0x002E82CD File Offset: 0x002E64CD
		// (set) Token: 0x0601729B RID: 94875 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17007B50 RID: 31568
		// (get) Token: 0x0601729C RID: 94876 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x0601729D RID: 94877 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17007B51 RID: 31569
		// (get) Token: 0x0601729E RID: 94878 RVA: 0x002E82DC File Offset: 0x002E64DC
		// (set) Token: 0x0601729F RID: 94879 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x060172A0 RID: 94880 RVA: 0x00333594 File Offset: 0x00331794
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

		// Token: 0x060172A2 RID: 94882 RVA: 0x00333618 File Offset: 0x00331818
		// Note: this type is marked as 'beforefieldinit'.
		static ColorType()
		{
			byte[] array = new byte[5];
			ColorType.attributeNamespaceIds = array;
		}

		// Token: 0x04009B5B RID: 39771
		private static string[] attributeTagNames = new string[] { "auto", "indexed", "rgb", "theme", "tint" };

		// Token: 0x04009B5C RID: 39772
		private static byte[] attributeNamespaceIds;
	}
}
