using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022D0 RID: 8912
	[ChildElementInfo(typeof(VisibleButton), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MenuRegular), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(VisibleToggleButton), FileFormatVersions.Office2010)]
	internal class SplitButton : OpenXmlCompositeElement
	{
		// Token: 0x17004476 RID: 17526
		// (get) Token: 0x0600F79D RID: 63389 RVA: 0x002C9F5F File Offset: 0x002C815F
		public override string LocalName
		{
			get
			{
				return "splitButton";
			}
		}

		// Token: 0x17004477 RID: 17527
		// (get) Token: 0x0600F79E RID: 63390 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004478 RID: 17528
		// (get) Token: 0x0600F79F RID: 63391 RVA: 0x002D709D File Offset: 0x002D529D
		internal override int ElementTypeId
		{
			get
			{
				return 13057;
			}
		}

		// Token: 0x0600F7A0 RID: 63392 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004479 RID: 17529
		// (get) Token: 0x0600F7A1 RID: 63393 RVA: 0x002D70A4 File Offset: 0x002D52A4
		internal override string[] AttributeTagNames
		{
			get
			{
				return SplitButton.attributeTagNames;
			}
		}

		// Token: 0x1700447A RID: 17530
		// (get) Token: 0x0600F7A2 RID: 63394 RVA: 0x002D70AB File Offset: 0x002D52AB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SplitButton.attributeNamespaceIds;
			}
		}

		// Token: 0x1700447B RID: 17531
		// (get) Token: 0x0600F7A3 RID: 63395 RVA: 0x002D42D0 File Offset: 0x002D24D0
		// (set) Token: 0x0600F7A4 RID: 63396 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "size")]
		public EnumValue<SizeValues> Size
		{
			get
			{
				return (EnumValue<SizeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700447C RID: 17532
		// (get) Token: 0x0600F7A5 RID: 63397 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F7A6 RID: 63398 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getSize")]
		public StringValue GetSize
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

		// Token: 0x1700447D RID: 17533
		// (get) Token: 0x0600F7A7 RID: 63399 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0600F7A8 RID: 63400 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x1700447E RID: 17534
		// (get) Token: 0x0600F7A9 RID: 63401 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F7AA RID: 63402 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700447F RID: 17535
		// (get) Token: 0x0600F7AB RID: 63403 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F7AC RID: 63404 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17004480 RID: 17536
		// (get) Token: 0x0600F7AD RID: 63405 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F7AE RID: 63406 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17004481 RID: 17537
		// (get) Token: 0x0600F7AF RID: 63407 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F7B0 RID: 63408 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "tag")]
		public StringValue Tag
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17004482 RID: 17538
		// (get) Token: 0x0600F7B1 RID: 63409 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F7B2 RID: 63410 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17004483 RID: 17539
		// (get) Token: 0x0600F7B3 RID: 63411 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F7B4 RID: 63412 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17004484 RID: 17540
		// (get) Token: 0x0600F7B5 RID: 63413 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F7B6 RID: 63414 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17004485 RID: 17541
		// (get) Token: 0x0600F7B7 RID: 63415 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F7B8 RID: 63416 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17004486 RID: 17542
		// (get) Token: 0x0600F7B9 RID: 63417 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F7BA RID: 63418 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17004487 RID: 17543
		// (get) Token: 0x0600F7BB RID: 63419 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x0600F7BC RID: 63420 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17004488 RID: 17544
		// (get) Token: 0x0600F7BD RID: 63421 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F7BE RID: 63422 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17004489 RID: 17545
		// (get) Token: 0x0600F7BF RID: 63423 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F7C0 RID: 63424 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
		{
			get
			{
				return (StringValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x1700448A RID: 17546
		// (get) Token: 0x0600F7C1 RID: 63425 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F7C2 RID: 63426 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x1700448B RID: 17547
		// (get) Token: 0x0600F7C3 RID: 63427 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x0600F7C4 RID: 63428 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x1700448C RID: 17548
		// (get) Token: 0x0600F7C5 RID: 63429 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F7C6 RID: 63430 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x0600F7C7 RID: 63431 RVA: 0x00293ECF File Offset: 0x002920CF
		public SplitButton()
		{
		}

		// Token: 0x0600F7C8 RID: 63432 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SplitButton(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F7C9 RID: 63433 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SplitButton(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F7CA RID: 63434 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SplitButton(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F7CB RID: 63435 RVA: 0x002D70B4 File Offset: 0x002D52B4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "button" == name)
			{
				return new VisibleButton();
			}
			if (57 == namespaceId && "toggleButton" == name)
			{
				return new VisibleToggleButton();
			}
			if (57 == namespaceId && "menu" == name)
			{
				return new MenuRegular();
			}
			return null;
		}

		// Token: 0x0600F7CC RID: 63436 RVA: 0x002D710C File Offset: 0x002D530C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "size" == name)
			{
				return new EnumValue<SizeValues>();
			}
			if (namespaceId == 0 && "getSize" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "enabled" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getEnabled" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "keytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getKeytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showLabel" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getShowLabel" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F7CD RID: 63437 RVA: 0x002D72AD File Offset: 0x002D54AD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SplitButton>(deep);
		}

		// Token: 0x0600F7CE RID: 63438 RVA: 0x002D72B8 File Offset: 0x002D54B8
		// Note: this type is marked as 'beforefieldinit'.
		static SplitButton()
		{
			byte[] array = new byte[18];
			SplitButton.attributeNamespaceIds = array;
		}

		// Token: 0x0400713D RID: 28989
		private const string tagName = "splitButton";

		// Token: 0x0400713E RID: 28990
		private const byte tagNsId = 57;

		// Token: 0x0400713F RID: 28991
		internal const int ElementTypeIdConst = 13057;

		// Token: 0x04007140 RID: 28992
		private static string[] attributeTagNames = new string[]
		{
			"size", "getSize", "enabled", "getEnabled", "id", "idQ", "tag", "idMso", "insertAfterMso", "insertBeforeMso",
			"insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel"
		};

		// Token: 0x04007141 RID: 28993
		private static byte[] attributeNamespaceIds;
	}
}
