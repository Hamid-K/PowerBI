using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022C7 RID: 8903
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class LabelControl : OpenXmlLeafElement
	{
		// Token: 0x17004313 RID: 17171
		// (get) Token: 0x0600F4C7 RID: 62663 RVA: 0x002CB6EA File Offset: 0x002C98EA
		public override string LocalName
		{
			get
			{
				return "labelControl";
			}
		}

		// Token: 0x17004314 RID: 17172
		// (get) Token: 0x0600F4C8 RID: 62664 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004315 RID: 17173
		// (get) Token: 0x0600F4C9 RID: 62665 RVA: 0x002D46C2 File Offset: 0x002D28C2
		internal override int ElementTypeId
		{
			get
			{
				return 13048;
			}
		}

		// Token: 0x0600F4CA RID: 62666 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004316 RID: 17174
		// (get) Token: 0x0600F4CB RID: 62667 RVA: 0x002D46C9 File Offset: 0x002D28C9
		internal override string[] AttributeTagNames
		{
			get
			{
				return LabelControl.attributeTagNames;
			}
		}

		// Token: 0x17004317 RID: 17175
		// (get) Token: 0x0600F4CC RID: 62668 RVA: 0x002D46D0 File Offset: 0x002D28D0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LabelControl.attributeNamespaceIds;
			}
		}

		// Token: 0x17004318 RID: 17176
		// (get) Token: 0x0600F4CD RID: 62669 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F4CE RID: 62670 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004319 RID: 17177
		// (get) Token: 0x0600F4CF RID: 62671 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F4D0 RID: 62672 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x1700431A RID: 17178
		// (get) Token: 0x0600F4D1 RID: 62673 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F4D2 RID: 62674 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "tag")]
		public StringValue Tag
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700431B RID: 17179
		// (get) Token: 0x0600F4D3 RID: 62675 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F4D4 RID: 62676 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x1700431C RID: 17180
		// (get) Token: 0x0600F4D5 RID: 62677 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F4D6 RID: 62678 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x1700431D RID: 17181
		// (get) Token: 0x0600F4D7 RID: 62679 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F4D8 RID: 62680 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x1700431E RID: 17182
		// (get) Token: 0x0600F4D9 RID: 62681 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F4DA RID: 62682 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x1700431F RID: 17183
		// (get) Token: 0x0600F4DB RID: 62683 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F4DC RID: 62684 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17004320 RID: 17184
		// (get) Token: 0x0600F4DD RID: 62685 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x0600F4DE RID: 62686 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17004321 RID: 17185
		// (get) Token: 0x0600F4DF RID: 62687 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F4E0 RID: 62688 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17004322 RID: 17186
		// (get) Token: 0x0600F4E1 RID: 62689 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F4E2 RID: 62690 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17004323 RID: 17187
		// (get) Token: 0x0600F4E3 RID: 62691 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F4E4 RID: 62692 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17004324 RID: 17188
		// (get) Token: 0x0600F4E5 RID: 62693 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F4E6 RID: 62694 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
		{
			get
			{
				return (StringValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17004325 RID: 17189
		// (get) Token: 0x0600F4E7 RID: 62695 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F4E8 RID: 62696 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17004326 RID: 17190
		// (get) Token: 0x0600F4E9 RID: 62697 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F4EA RID: 62698 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x17004327 RID: 17191
		// (get) Token: 0x0600F4EB RID: 62699 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F4EC RID: 62700 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x17004328 RID: 17192
		// (get) Token: 0x0600F4ED RID: 62701 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x0600F4EE RID: 62702 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x17004329 RID: 17193
		// (get) Token: 0x0600F4EF RID: 62703 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F4F0 RID: 62704 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x1700432A RID: 17194
		// (get) Token: 0x0600F4F1 RID: 62705 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x0600F4F2 RID: 62706 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x1700432B RID: 17195
		// (get) Token: 0x0600F4F3 RID: 62707 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F4F4 RID: 62708 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x0600F4F6 RID: 62710 RVA: 0x002D46D8 File Offset: 0x002D28D8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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
			if (namespaceId == 0 && "screentip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getScreentip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "supertip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getSupertip" == name)
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
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getLabel" == name)
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

		// Token: 0x0600F4F7 RID: 62711 RVA: 0x002D48A5 File Offset: 0x002D2AA5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LabelControl>(deep);
		}

		// Token: 0x0600F4F8 RID: 62712 RVA: 0x002D48B0 File Offset: 0x002D2AB0
		// Note: this type is marked as 'beforefieldinit'.
		static LabelControl()
		{
			byte[] array = new byte[20];
			LabelControl.attributeNamespaceIds = array;
		}

		// Token: 0x04007110 RID: 28944
		private const string tagName = "labelControl";

		// Token: 0x04007111 RID: 28945
		private const byte tagNsId = 57;

		// Token: 0x04007112 RID: 28946
		internal const int ElementTypeIdConst = 13048;

		// Token: 0x04007113 RID: 28947
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "idMso", "screentip", "getScreentip", "supertip", "getSupertip", "enabled", "getEnabled",
			"label", "getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "showLabel", "getShowLabel"
		};

		// Token: 0x04007114 RID: 28948
		private static byte[] attributeNamespaceIds;
	}
}
