using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200241F RID: 9247
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ConditionalFormattingIcon : OpenXmlLeafElement
	{
		// Token: 0x17004F59 RID: 20313
		// (get) Token: 0x06010F68 RID: 69480 RVA: 0x002E9297 File Offset: 0x002E7497
		public override string LocalName
		{
			get
			{
				return "cfIcon";
			}
		}

		// Token: 0x17004F5A RID: 20314
		// (get) Token: 0x06010F69 RID: 69481 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F5B RID: 20315
		// (get) Token: 0x06010F6A RID: 69482 RVA: 0x002E929E File Offset: 0x002E749E
		internal override int ElementTypeId
		{
			get
			{
				return 12965;
			}
		}

		// Token: 0x06010F6B RID: 69483 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004F5C RID: 20316
		// (get) Token: 0x06010F6C RID: 69484 RVA: 0x002E92A5 File Offset: 0x002E74A5
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConditionalFormattingIcon.attributeTagNames;
			}
		}

		// Token: 0x17004F5D RID: 20317
		// (get) Token: 0x06010F6D RID: 69485 RVA: 0x002E92AC File Offset: 0x002E74AC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConditionalFormattingIcon.attributeNamespaceIds;
			}
		}

		// Token: 0x17004F5E RID: 20318
		// (get) Token: 0x06010F6E RID: 69486 RVA: 0x002E6A8C File Offset: 0x002E4C8C
		// (set) Token: 0x06010F6F RID: 69487 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "iconSet")]
		public EnumValue<IconSetTypeValues> IconSet
		{
			get
			{
				return (EnumValue<IconSetTypeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004F5F RID: 20319
		// (get) Token: 0x06010F70 RID: 69488 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06010F71 RID: 69489 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x06010F73 RID: 69491 RVA: 0x002E6A9B File Offset: 0x002E4C9B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "iconSet" == name)
			{
				return new EnumValue<IconSetTypeValues>();
			}
			if (namespaceId == 0 && "iconId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010F74 RID: 69492 RVA: 0x002E92B3 File Offset: 0x002E74B3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConditionalFormattingIcon>(deep);
		}

		// Token: 0x06010F75 RID: 69493 RVA: 0x002E92BC File Offset: 0x002E74BC
		// Note: this type is marked as 'beforefieldinit'.
		static ConditionalFormattingIcon()
		{
			byte[] array = new byte[2];
			ConditionalFormattingIcon.attributeNamespaceIds = array;
		}

		// Token: 0x04007710 RID: 30480
		private const string tagName = "cfIcon";

		// Token: 0x04007711 RID: 30481
		private const byte tagNsId = 53;

		// Token: 0x04007712 RID: 30482
		internal const int ElementTypeIdConst = 12965;

		// Token: 0x04007713 RID: 30483
		private static string[] attributeTagNames = new string[] { "iconSet", "iconId" };

		// Token: 0x04007714 RID: 30484
		private static byte[] attributeNamespaceIds;
	}
}
