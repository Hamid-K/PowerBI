using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022DB RID: 8923
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RadioButtonBackstageItem), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class RadioGroup : OpenXmlCompositeElement
	{
		// Token: 0x17004567 RID: 17767
		// (get) Token: 0x0600F993 RID: 63891 RVA: 0x002D8DFF File Offset: 0x002D6FFF
		public override string LocalName
		{
			get
			{
				return "radioGroup";
			}
		}

		// Token: 0x17004568 RID: 17768
		// (get) Token: 0x0600F994 RID: 63892 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004569 RID: 17769
		// (get) Token: 0x0600F995 RID: 63893 RVA: 0x002D8E06 File Offset: 0x002D7006
		internal override int ElementTypeId
		{
			get
			{
				return 13068;
			}
		}

		// Token: 0x0600F996 RID: 63894 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700456A RID: 17770
		// (get) Token: 0x0600F997 RID: 63895 RVA: 0x002D8E0D File Offset: 0x002D700D
		internal override string[] AttributeTagNames
		{
			get
			{
				return RadioGroup.attributeTagNames;
			}
		}

		// Token: 0x1700456B RID: 17771
		// (get) Token: 0x0600F998 RID: 63896 RVA: 0x002D8E14 File Offset: 0x002D7014
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RadioGroup.attributeNamespaceIds;
			}
		}

		// Token: 0x1700456C RID: 17772
		// (get) Token: 0x0600F999 RID: 63897 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F99A RID: 63898 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700456D RID: 17773
		// (get) Token: 0x0600F99B RID: 63899 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F99C RID: 63900 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x1700456E RID: 17774
		// (get) Token: 0x0600F99D RID: 63901 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F99E RID: 63902 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x1700456F RID: 17775
		// (get) Token: 0x0600F99F RID: 63903 RVA: 0x002D8849 File Offset: 0x002D6A49
		// (set) Token: 0x0600F9A0 RID: 63904 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "alignLabel")]
		public EnumValue<ExpandValues> AlignLabel
		{
			get
			{
				return (EnumValue<ExpandValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17004570 RID: 17776
		// (get) Token: 0x0600F9A1 RID: 63905 RVA: 0x002D8858 File Offset: 0x002D6A58
		// (set) Token: 0x0600F9A2 RID: 63906 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "expand")]
		public EnumValue<ExpandValues> Expand
		{
			get
			{
				return (EnumValue<ExpandValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17004571 RID: 17777
		// (get) Token: 0x0600F9A3 RID: 63907 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0600F9A4 RID: 63908 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17004572 RID: 17778
		// (get) Token: 0x0600F9A5 RID: 63909 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F9A6 RID: 63910 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17004573 RID: 17779
		// (get) Token: 0x0600F9A7 RID: 63911 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F9A8 RID: 63912 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17004574 RID: 17780
		// (get) Token: 0x0600F9A9 RID: 63913 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F9AA RID: 63914 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17004575 RID: 17781
		// (get) Token: 0x0600F9AB RID: 63915 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0600F9AC RID: 63916 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17004576 RID: 17782
		// (get) Token: 0x0600F9AD RID: 63917 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F9AE RID: 63918 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17004577 RID: 17783
		// (get) Token: 0x0600F9AF RID: 63919 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F9B0 RID: 63920 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x17004578 RID: 17784
		// (get) Token: 0x0600F9B1 RID: 63921 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F9B2 RID: 63922 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17004579 RID: 17785
		// (get) Token: 0x0600F9B3 RID: 63923 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F9B4 RID: 63924 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x1700457A RID: 17786
		// (get) Token: 0x0600F9B5 RID: 63925 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F9B6 RID: 63926 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "getSelectedItemIndex")]
		public StringValue GetSelectedItemIndex
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

		// Token: 0x1700457B RID: 17787
		// (get) Token: 0x0600F9B7 RID: 63927 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F9B8 RID: 63928 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getItemCount")]
		public StringValue GetItemCount
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

		// Token: 0x1700457C RID: 17788
		// (get) Token: 0x0600F9B9 RID: 63929 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F9BA RID: 63930 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getItemLabel")]
		public StringValue GetItemLabel
		{
			get
			{
				return (StringValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x1700457D RID: 17789
		// (get) Token: 0x0600F9BB RID: 63931 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F9BC RID: 63932 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getItemID")]
		public StringValue GetItemID
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

		// Token: 0x0600F9BD RID: 63933 RVA: 0x00293ECF File Offset: 0x002920CF
		public RadioGroup()
		{
		}

		// Token: 0x0600F9BE RID: 63934 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RadioGroup(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F9BF RID: 63935 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RadioGroup(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F9C0 RID: 63936 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RadioGroup(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F9C1 RID: 63937 RVA: 0x002D8E1B File Offset: 0x002D701B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "radioButton" == name)
			{
				return new RadioButtonBackstageItem();
			}
			return null;
		}

		// Token: 0x0600F9C2 RID: 63938 RVA: 0x002D8E38 File Offset: 0x002D7038
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
			if (namespaceId == 0 && "alignLabel" == name)
			{
				return new EnumValue<ExpandValues>();
			}
			if (namespaceId == 0 && "expand" == name)
			{
				return new EnumValue<ExpandValues>();
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
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "onAction" == name)
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
			if (namespaceId == 0 && "getSelectedItemIndex" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemCount" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemID" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F9C3 RID: 63939 RVA: 0x002D8FD9 File Offset: 0x002D71D9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RadioGroup>(deep);
		}

		// Token: 0x0600F9C4 RID: 63940 RVA: 0x002D8FE4 File Offset: 0x002D71E4
		// Note: this type is marked as 'beforefieldinit'.
		static RadioGroup()
		{
			byte[] array = new byte[18];
			RadioGroup.attributeNamespaceIds = array;
		}

		// Token: 0x04007174 RID: 29044
		private const string tagName = "radioGroup";

		// Token: 0x04007175 RID: 29045
		private const byte tagNsId = 57;

		// Token: 0x04007176 RID: 29046
		internal const int ElementTypeIdConst = 13068;

		// Token: 0x04007177 RID: 29047
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "alignLabel", "expand", "enabled", "getEnabled", "label", "getLabel", "visible",
			"getVisible", "onAction", "keytip", "getKeytip", "getSelectedItemIndex", "getItemCount", "getItemLabel", "getItemID"
		};

		// Token: 0x04007178 RID: 29048
		private static byte[] attributeNamespaceIds;
	}
}
