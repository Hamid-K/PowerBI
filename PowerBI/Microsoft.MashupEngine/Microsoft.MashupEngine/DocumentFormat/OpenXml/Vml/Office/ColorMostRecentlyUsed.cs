using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x0200221F RID: 8735
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorMostRecentlyUsed : OpenXmlLeafElement
	{
		// Token: 0x17003944 RID: 14660
		// (get) Token: 0x0600E037 RID: 57399 RVA: 0x002BFB47 File Offset: 0x002BDD47
		public override string LocalName
		{
			get
			{
				return "colormru";
			}
		}

		// Token: 0x17003945 RID: 14661
		// (get) Token: 0x0600E038 RID: 57400 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x17003946 RID: 14662
		// (get) Token: 0x0600E039 RID: 57401 RVA: 0x002BFB4E File Offset: 0x002BDD4E
		internal override int ElementTypeId
		{
			get
			{
				return 12428;
			}
		}

		// Token: 0x0600E03A RID: 57402 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003947 RID: 14663
		// (get) Token: 0x0600E03B RID: 57403 RVA: 0x002BFB55 File Offset: 0x002BDD55
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorMostRecentlyUsed.attributeTagNames;
			}
		}

		// Token: 0x17003948 RID: 14664
		// (get) Token: 0x0600E03C RID: 57404 RVA: 0x002BFB5C File Offset: 0x002BDD5C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorMostRecentlyUsed.attributeNamespaceIds;
			}
		}

		// Token: 0x17003949 RID: 14665
		// (get) Token: 0x0600E03D RID: 57405 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600E03E RID: 57406 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(26, "ext")]
		public EnumValue<ExtensionHandlingBehaviorValues> Extension
		{
			get
			{
				return (EnumValue<ExtensionHandlingBehaviorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700394A RID: 14666
		// (get) Token: 0x0600E03F RID: 57407 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E040 RID: 57408 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "colors")]
		public StringValue Colors
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0600E042 RID: 57410 RVA: 0x002BFB63 File Offset: 0x002BDD63
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			if (namespaceId == 0 && "colors" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E043 RID: 57411 RVA: 0x002BFB9B File Offset: 0x002BDD9B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorMostRecentlyUsed>(deep);
		}

		// Token: 0x0600E044 RID: 57412 RVA: 0x002BFBA4 File Offset: 0x002BDDA4
		// Note: this type is marked as 'beforefieldinit'.
		static ColorMostRecentlyUsed()
		{
			byte[] array = new byte[2];
			array[0] = 26;
			ColorMostRecentlyUsed.attributeNamespaceIds = array;
		}

		// Token: 0x04006DD9 RID: 28121
		private const string tagName = "colormru";

		// Token: 0x04006DDA RID: 28122
		private const byte tagNsId = 27;

		// Token: 0x04006DDB RID: 28123
		internal const int ElementTypeIdConst = 12428;

		// Token: 0x04006DDC RID: 28124
		private static string[] attributeTagNames = new string[] { "ext", "colors" };

		// Token: 0x04006DDD RID: 28125
		private static byte[] attributeNamespaceIds;
	}
}
