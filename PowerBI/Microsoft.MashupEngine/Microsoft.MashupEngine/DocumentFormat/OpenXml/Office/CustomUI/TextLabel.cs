using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x0200227A RID: 8826
	[GeneratedCode("DomGen", "2.0")]
	internal class TextLabel : OpenXmlLeafElement
	{
		// Token: 0x17003E58 RID: 15960
		// (get) Token: 0x0600EAB6 RID: 60086 RVA: 0x002CB6EA File Offset: 0x002C98EA
		public override string LocalName
		{
			get
			{
				return "labelControl";
			}
		}

		// Token: 0x17003E59 RID: 15961
		// (get) Token: 0x0600EAB7 RID: 60087 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003E5A RID: 15962
		// (get) Token: 0x0600EAB8 RID: 60088 RVA: 0x002CB6F1 File Offset: 0x002C98F1
		internal override int ElementTypeId
		{
			get
			{
				return 12585;
			}
		}

		// Token: 0x0600EAB9 RID: 60089 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003E5B RID: 15963
		// (get) Token: 0x0600EABA RID: 60090 RVA: 0x002CB6F8 File Offset: 0x002C98F8
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextLabel.attributeTagNames;
			}
		}

		// Token: 0x17003E5C RID: 15964
		// (get) Token: 0x0600EABB RID: 60091 RVA: 0x002CB6FF File Offset: 0x002C98FF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextLabel.attributeNamespaceIds;
			}
		}

		// Token: 0x17003E5D RID: 15965
		// (get) Token: 0x0600EABC RID: 60092 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EABD RID: 60093 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003E5E RID: 15966
		// (get) Token: 0x0600EABE RID: 60094 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EABF RID: 60095 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003E5F RID: 15967
		// (get) Token: 0x0600EAC0 RID: 60096 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EAC1 RID: 60097 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17003E60 RID: 15968
		// (get) Token: 0x0600EAC2 RID: 60098 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EAC3 RID: 60099 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003E61 RID: 15969
		// (get) Token: 0x0600EAC4 RID: 60100 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EAC5 RID: 60101 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003E62 RID: 15970
		// (get) Token: 0x0600EAC6 RID: 60102 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EAC7 RID: 60103 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003E63 RID: 15971
		// (get) Token: 0x0600EAC8 RID: 60104 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EAC9 RID: 60105 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17003E64 RID: 15972
		// (get) Token: 0x0600EACA RID: 60106 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EACB RID: 60107 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17003E65 RID: 15973
		// (get) Token: 0x0600EACC RID: 60108 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x0600EACD RID: 60109 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17003E66 RID: 15974
		// (get) Token: 0x0600EACE RID: 60110 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600EACF RID: 60111 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003E67 RID: 15975
		// (get) Token: 0x0600EAD0 RID: 60112 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600EAD1 RID: 60113 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003E68 RID: 15976
		// (get) Token: 0x0600EAD2 RID: 60114 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600EAD3 RID: 60115 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003E69 RID: 15977
		// (get) Token: 0x0600EAD4 RID: 60116 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600EAD5 RID: 60117 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17003E6A RID: 15978
		// (get) Token: 0x0600EAD6 RID: 60118 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600EAD7 RID: 60119 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003E6B RID: 15979
		// (get) Token: 0x0600EAD8 RID: 60120 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600EAD9 RID: 60121 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003E6C RID: 15980
		// (get) Token: 0x0600EADA RID: 60122 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600EADB RID: 60123 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17003E6D RID: 15981
		// (get) Token: 0x0600EADC RID: 60124 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x0600EADD RID: 60125 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17003E6E RID: 15982
		// (get) Token: 0x0600EADE RID: 60126 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600EADF RID: 60127 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17003E6F RID: 15983
		// (get) Token: 0x0600EAE0 RID: 60128 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x0600EAE1 RID: 60129 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17003E70 RID: 15984
		// (get) Token: 0x0600EAE2 RID: 60130 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600EAE3 RID: 60131 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x0600EAE5 RID: 60133 RVA: 0x002CB718 File Offset: 0x002C9918
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
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
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

		// Token: 0x0600EAE6 RID: 60134 RVA: 0x002CB8E5 File Offset: 0x002C9AE5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextLabel>(deep);
		}

		// Token: 0x0600EAE7 RID: 60135 RVA: 0x002CB8F0 File Offset: 0x002C9AF0
		// Note: this type is marked as 'beforefieldinit'.
		static TextLabel()
		{
			byte[] array = new byte[20];
			TextLabel.attributeNamespaceIds = array;
		}

		// Token: 0x04006FC1 RID: 28609
		private const string tagName = "labelControl";

		// Token: 0x04006FC2 RID: 28610
		private const byte tagNsId = 34;

		// Token: 0x04006FC3 RID: 28611
		internal const int ElementTypeIdConst = 12585;

		// Token: 0x04006FC4 RID: 28612
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "idMso", "tag", "screentip", "getScreentip", "supertip", "getSupertip", "enabled", "getEnabled",
			"label", "getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "showLabel", "getShowLabel"
		};

		// Token: 0x04006FC5 RID: 28613
		private static byte[] attributeNamespaceIds;
	}
}
