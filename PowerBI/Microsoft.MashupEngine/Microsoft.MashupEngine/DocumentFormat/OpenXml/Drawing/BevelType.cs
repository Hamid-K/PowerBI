using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027B9 RID: 10169
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class BevelType : OpenXmlLeafElement
	{
		// Token: 0x17006346 RID: 25414
		// (get) Token: 0x06013BF7 RID: 80887 RVA: 0x0030B589 File Offset: 0x00309789
		internal override string[] AttributeTagNames
		{
			get
			{
				return BevelType.attributeTagNames;
			}
		}

		// Token: 0x17006347 RID: 25415
		// (get) Token: 0x06013BF8 RID: 80888 RVA: 0x0030B590 File Offset: 0x00309790
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BevelType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006348 RID: 25416
		// (get) Token: 0x06013BF9 RID: 80889 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06013BFA RID: 80890 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "w")]
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

		// Token: 0x17006349 RID: 25417
		// (get) Token: 0x06013BFB RID: 80891 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x06013BFC RID: 80892 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "h")]
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

		// Token: 0x1700634A RID: 25418
		// (get) Token: 0x06013BFD RID: 80893 RVA: 0x0030B597 File Offset: 0x00309797
		// (set) Token: 0x06013BFE RID: 80894 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "prst")]
		public EnumValue<BevelPresetValues> Preset
		{
			get
			{
				return (EnumValue<BevelPresetValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06013BFF RID: 80895 RVA: 0x0030B5A8 File Offset: 0x003097A8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "w" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "h" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "prst" == name)
			{
				return new EnumValue<BevelPresetValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013C01 RID: 80897 RVA: 0x0030B600 File Offset: 0x00309800
		// Note: this type is marked as 'beforefieldinit'.
		static BevelType()
		{
			byte[] array = new byte[3];
			BevelType.attributeNamespaceIds = array;
		}

		// Token: 0x04008794 RID: 34708
		private static string[] attributeTagNames = new string[] { "w", "h", "prst" };

		// Token: 0x04008795 RID: 34709
		private static byte[] attributeNamespaceIds;
	}
}
