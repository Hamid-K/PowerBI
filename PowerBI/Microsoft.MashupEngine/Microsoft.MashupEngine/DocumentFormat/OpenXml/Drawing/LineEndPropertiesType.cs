using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027DA RID: 10202
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class LineEndPropertiesType : OpenXmlLeafElement
	{
		// Token: 0x170063FA RID: 25594
		// (get) Token: 0x06013D91 RID: 81297 RVA: 0x0030C403 File Offset: 0x0030A603
		internal override string[] AttributeTagNames
		{
			get
			{
				return LineEndPropertiesType.attributeTagNames;
			}
		}

		// Token: 0x170063FB RID: 25595
		// (get) Token: 0x06013D92 RID: 81298 RVA: 0x0030C40A File Offset: 0x0030A60A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LineEndPropertiesType.attributeNamespaceIds;
			}
		}

		// Token: 0x170063FC RID: 25596
		// (get) Token: 0x06013D93 RID: 81299 RVA: 0x0030C411 File Offset: 0x0030A611
		// (set) Token: 0x06013D94 RID: 81300 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<LineEndValues> Type
		{
			get
			{
				return (EnumValue<LineEndValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170063FD RID: 25597
		// (get) Token: 0x06013D95 RID: 81301 RVA: 0x0030C420 File Offset: 0x0030A620
		// (set) Token: 0x06013D96 RID: 81302 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "w")]
		public EnumValue<LineEndWidthValues> Width
		{
			get
			{
				return (EnumValue<LineEndWidthValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170063FE RID: 25598
		// (get) Token: 0x06013D97 RID: 81303 RVA: 0x0030C42F File Offset: 0x0030A62F
		// (set) Token: 0x06013D98 RID: 81304 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "len")]
		public EnumValue<LineEndLengthValues> Length
		{
			get
			{
				return (EnumValue<LineEndLengthValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06013D99 RID: 81305 RVA: 0x0030C440 File Offset: 0x0030A640
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<LineEndValues>();
			}
			if (namespaceId == 0 && "w" == name)
			{
				return new EnumValue<LineEndWidthValues>();
			}
			if (namespaceId == 0 && "len" == name)
			{
				return new EnumValue<LineEndLengthValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013D9B RID: 81307 RVA: 0x0030C498 File Offset: 0x0030A698
		// Note: this type is marked as 'beforefieldinit'.
		static LineEndPropertiesType()
		{
			byte[] array = new byte[3];
			LineEndPropertiesType.attributeNamespaceIds = array;
		}

		// Token: 0x0400880F RID: 34831
		private static string[] attributeTagNames = new string[] { "type", "w", "len" };

		// Token: 0x04008810 RID: 34832
		private static byte[] attributeNamespaceIds;
	}
}
