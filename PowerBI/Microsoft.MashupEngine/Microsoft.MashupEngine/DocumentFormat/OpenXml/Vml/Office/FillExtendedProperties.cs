using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002213 RID: 8723
	[GeneratedCode("DomGen", "2.0")]
	internal class FillExtendedProperties : OpenXmlLeafElement
	{
		// Token: 0x170038F9 RID: 14585
		// (get) Token: 0x0600DF8B RID: 57227 RVA: 0x002BF458 File Offset: 0x002BD658
		public override string LocalName
		{
			get
			{
				return "fill";
			}
		}

		// Token: 0x170038FA RID: 14586
		// (get) Token: 0x0600DF8C RID: 57228 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x170038FB RID: 14587
		// (get) Token: 0x0600DF8D RID: 57229 RVA: 0x002BF45F File Offset: 0x002BD65F
		internal override int ElementTypeId
		{
			get
			{
				return 12416;
			}
		}

		// Token: 0x0600DF8E RID: 57230 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170038FC RID: 14588
		// (get) Token: 0x0600DF8F RID: 57231 RVA: 0x002BF466 File Offset: 0x002BD666
		internal override string[] AttributeTagNames
		{
			get
			{
				return FillExtendedProperties.attributeTagNames;
			}
		}

		// Token: 0x170038FD RID: 14589
		// (get) Token: 0x0600DF90 RID: 57232 RVA: 0x002BF46D File Offset: 0x002BD66D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FillExtendedProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170038FE RID: 14590
		// (get) Token: 0x0600DF91 RID: 57233 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600DF92 RID: 57234 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170038FF RID: 14591
		// (get) Token: 0x0600DF93 RID: 57235 RVA: 0x002BF474 File Offset: 0x002BD674
		// (set) Token: 0x0600DF94 RID: 57236 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "type")]
		public EnumValue<FillValues> Type
		{
			get
			{
				return (EnumValue<FillValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0600DF96 RID: 57238 RVA: 0x002BF483 File Offset: 0x002BD683
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<FillValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DF97 RID: 57239 RVA: 0x002BF4BB File Offset: 0x002BD6BB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FillExtendedProperties>(deep);
		}

		// Token: 0x0600DF98 RID: 57240 RVA: 0x002BF4C4 File Offset: 0x002BD6C4
		// Note: this type is marked as 'beforefieldinit'.
		static FillExtendedProperties()
		{
			byte[] array = new byte[2];
			array[0] = 26;
			FillExtendedProperties.attributeNamespaceIds = array;
		}

		// Token: 0x04006DA3 RID: 28067
		private const string tagName = "fill";

		// Token: 0x04006DA4 RID: 28068
		private const byte tagNsId = 27;

		// Token: 0x04006DA5 RID: 28069
		internal const int ElementTypeIdConst = 12416;

		// Token: 0x04006DA6 RID: 28070
		private static string[] attributeTagNames = new string[] { "ext", "type" };

		// Token: 0x04006DA7 RID: 28071
		private static byte[] attributeNamespaceIds;
	}
}
