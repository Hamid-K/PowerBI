using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200241C RID: 9244
	[ChildElementInfo(typeof(ConditionalFormattingValueObject), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ConditionalFormattingIcon), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class IconSet : OpenXmlCompositeElement
	{
		// Token: 0x17004F36 RID: 20278
		// (get) Token: 0x06010F1B RID: 69403 RVA: 0x002E8E89 File Offset: 0x002E7089
		public override string LocalName
		{
			get
			{
				return "iconSet";
			}
		}

		// Token: 0x17004F37 RID: 20279
		// (get) Token: 0x06010F1C RID: 69404 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F38 RID: 20280
		// (get) Token: 0x06010F1D RID: 69405 RVA: 0x002E8E90 File Offset: 0x002E7090
		internal override int ElementTypeId
		{
			get
			{
				return 12962;
			}
		}

		// Token: 0x06010F1E RID: 69406 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004F39 RID: 20281
		// (get) Token: 0x06010F1F RID: 69407 RVA: 0x002E8E97 File Offset: 0x002E7097
		internal override string[] AttributeTagNames
		{
			get
			{
				return IconSet.attributeTagNames;
			}
		}

		// Token: 0x17004F3A RID: 20282
		// (get) Token: 0x06010F20 RID: 69408 RVA: 0x002E8E9E File Offset: 0x002E709E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return IconSet.attributeNamespaceIds;
			}
		}

		// Token: 0x17004F3B RID: 20283
		// (get) Token: 0x06010F21 RID: 69409 RVA: 0x002E6A8C File Offset: 0x002E4C8C
		// (set) Token: 0x06010F22 RID: 69410 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "iconSet")]
		public EnumValue<IconSetTypeValues> IconSetTypes
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

		// Token: 0x17004F3C RID: 20284
		// (get) Token: 0x06010F23 RID: 69411 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06010F24 RID: 69412 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "showValue")]
		public BooleanValue ShowValue
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004F3D RID: 20285
		// (get) Token: 0x06010F25 RID: 69413 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06010F26 RID: 69414 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "percent")]
		public BooleanValue Percent
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17004F3E RID: 20286
		// (get) Token: 0x06010F27 RID: 69415 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06010F28 RID: 69416 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "reverse")]
		public BooleanValue Reverse
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17004F3F RID: 20287
		// (get) Token: 0x06010F29 RID: 69417 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06010F2A RID: 69418 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "custom")]
		public BooleanValue Custom
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06010F2B RID: 69419 RVA: 0x00293ECF File Offset: 0x002920CF
		public IconSet()
		{
		}

		// Token: 0x06010F2C RID: 69420 RVA: 0x00293ED7 File Offset: 0x002920D7
		public IconSet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010F2D RID: 69421 RVA: 0x00293EE0 File Offset: 0x002920E0
		public IconSet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010F2E RID: 69422 RVA: 0x00293EE9 File Offset: 0x002920E9
		public IconSet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010F2F RID: 69423 RVA: 0x002E8EA5 File Offset: 0x002E70A5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "cfvo" == name)
			{
				return new ConditionalFormattingValueObject();
			}
			if (53 == namespaceId && "cfIcon" == name)
			{
				return new ConditionalFormattingIcon();
			}
			return null;
		}

		// Token: 0x06010F30 RID: 69424 RVA: 0x002E8ED8 File Offset: 0x002E70D8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "iconSet" == name)
			{
				return new EnumValue<IconSetTypeValues>();
			}
			if (namespaceId == 0 && "showValue" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "percent" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "reverse" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "custom" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010F31 RID: 69425 RVA: 0x002E8F5B File Offset: 0x002E715B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IconSet>(deep);
		}

		// Token: 0x06010F32 RID: 69426 RVA: 0x002E8F64 File Offset: 0x002E7164
		// Note: this type is marked as 'beforefieldinit'.
		static IconSet()
		{
			byte[] array = new byte[5];
			IconSet.attributeNamespaceIds = array;
		}

		// Token: 0x040076FF RID: 30463
		private const string tagName = "iconSet";

		// Token: 0x04007700 RID: 30464
		private const byte tagNsId = 53;

		// Token: 0x04007701 RID: 30465
		internal const int ElementTypeIdConst = 12962;

		// Token: 0x04007702 RID: 30466
		private static string[] attributeTagNames = new string[] { "iconSet", "showValue", "percent", "reverse", "custom" };

		// Token: 0x04007703 RID: 30467
		private static byte[] attributeNamespaceIds;
	}
}
