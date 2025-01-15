using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CE2 RID: 11490
	[GeneratedCode("DomGen", "2.0")]
	internal class IconFilter : OpenXmlLeafElement
	{
		// Token: 0x1700861A RID: 34330
		// (get) Token: 0x06018AEE RID: 101102 RVA: 0x002E6A70 File Offset: 0x002E4C70
		public override string LocalName
		{
			get
			{
				return "iconFilter";
			}
		}

		// Token: 0x1700861B RID: 34331
		// (get) Token: 0x06018AEF RID: 101103 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700861C RID: 34332
		// (get) Token: 0x06018AF0 RID: 101104 RVA: 0x00343F43 File Offset: 0x00342143
		internal override int ElementTypeId
		{
			get
			{
				return 11472;
			}
		}

		// Token: 0x06018AF1 RID: 101105 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700861D RID: 34333
		// (get) Token: 0x06018AF2 RID: 101106 RVA: 0x00343F4A File Offset: 0x0034214A
		internal override string[] AttributeTagNames
		{
			get
			{
				return IconFilter.attributeTagNames;
			}
		}

		// Token: 0x1700861E RID: 34334
		// (get) Token: 0x06018AF3 RID: 101107 RVA: 0x00343F51 File Offset: 0x00342151
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return IconFilter.attributeNamespaceIds;
			}
		}

		// Token: 0x1700861F RID: 34335
		// (get) Token: 0x06018AF4 RID: 101108 RVA: 0x0034055C File Offset: 0x0033E75C
		// (set) Token: 0x06018AF5 RID: 101109 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "iconSet")]
		public EnumValue<IconSetValues> IconSet
		{
			get
			{
				return (EnumValue<IconSetValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008620 RID: 34336
		// (get) Token: 0x06018AF6 RID: 101110 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06018AF7 RID: 101111 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "iconId")]
		public UInt32Value IconId
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

		// Token: 0x06018AF9 RID: 101113 RVA: 0x00343F58 File Offset: 0x00342158
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "iconSet" == name)
			{
				return new EnumValue<IconSetValues>();
			}
			if (namespaceId == 0 && "iconId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018AFA RID: 101114 RVA: 0x00343F8E File Offset: 0x0034218E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IconFilter>(deep);
		}

		// Token: 0x06018AFB RID: 101115 RVA: 0x00343F98 File Offset: 0x00342198
		// Note: this type is marked as 'beforefieldinit'.
		static IconFilter()
		{
			byte[] array = new byte[2];
			IconFilter.attributeNamespaceIds = array;
		}

		// Token: 0x0400A14B RID: 41291
		private const string tagName = "iconFilter";

		// Token: 0x0400A14C RID: 41292
		private const byte tagNsId = 22;

		// Token: 0x0400A14D RID: 41293
		internal const int ElementTypeIdConst = 11472;

		// Token: 0x0400A14E RID: 41294
		private static string[] attributeTagNames = new string[] { "iconSet", "iconId" };

		// Token: 0x0400A14F RID: 41295
		private static byte[] attributeNamespaceIds;
	}
}
