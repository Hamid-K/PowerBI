using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002279 RID: 8825
	[GeneratedCode("DomGen", "2.0")]
	internal class ControlClone : OpenXmlLeafElement
	{
		// Token: 0x17003E35 RID: 15925
		// (get) Token: 0x0600EA70 RID: 60016 RVA: 0x002AD773 File Offset: 0x002AB973
		public override string LocalName
		{
			get
			{
				return "control";
			}
		}

		// Token: 0x17003E36 RID: 15926
		// (get) Token: 0x0600EA71 RID: 60017 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003E37 RID: 15927
		// (get) Token: 0x0600EA72 RID: 60018 RVA: 0x002CB2E2 File Offset: 0x002C94E2
		internal override int ElementTypeId
		{
			get
			{
				return 12584;
			}
		}

		// Token: 0x0600EA73 RID: 60019 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003E38 RID: 15928
		// (get) Token: 0x0600EA74 RID: 60020 RVA: 0x002CB2E9 File Offset: 0x002C94E9
		internal override string[] AttributeTagNames
		{
			get
			{
				return ControlClone.attributeTagNames;
			}
		}

		// Token: 0x17003E39 RID: 15929
		// (get) Token: 0x0600EA75 RID: 60021 RVA: 0x002CB2F0 File Offset: 0x002C94F0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ControlClone.attributeNamespaceIds;
			}
		}

		// Token: 0x17003E3A RID: 15930
		// (get) Token: 0x0600EA76 RID: 60022 RVA: 0x002CB2F7 File Offset: 0x002C94F7
		// (set) Token: 0x0600EA77 RID: 60023 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003E3B RID: 15931
		// (get) Token: 0x0600EA78 RID: 60024 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EA79 RID: 60025 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003E3C RID: 15932
		// (get) Token: 0x0600EA7A RID: 60026 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0600EA7B RID: 60027 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003E3D RID: 15933
		// (get) Token: 0x0600EA7C RID: 60028 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EA7D RID: 60029 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003E3E RID: 15934
		// (get) Token: 0x0600EA7E RID: 60030 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EA7F RID: 60031 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17003E3F RID: 15935
		// (get) Token: 0x0600EA80 RID: 60032 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EA81 RID: 60033 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x17003E40 RID: 15936
		// (get) Token: 0x0600EA82 RID: 60034 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EA83 RID: 60035 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17003E41 RID: 15937
		// (get) Token: 0x0600EA84 RID: 60036 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EA85 RID: 60037 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17003E42 RID: 15938
		// (get) Token: 0x0600EA86 RID: 60038 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600EA87 RID: 60039 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17003E43 RID: 15939
		// (get) Token: 0x0600EA88 RID: 60040 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600EA89 RID: 60041 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003E44 RID: 15940
		// (get) Token: 0x0600EA8A RID: 60042 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600EA8B RID: 60043 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17003E45 RID: 15941
		// (get) Token: 0x0600EA8C RID: 60044 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600EA8D RID: 60045 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003E46 RID: 15942
		// (get) Token: 0x0600EA8E RID: 60046 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600EA8F RID: 60047 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17003E47 RID: 15943
		// (get) Token: 0x0600EA90 RID: 60048 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600EA91 RID: 60049 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17003E48 RID: 15944
		// (get) Token: 0x0600EA92 RID: 60050 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600EA93 RID: 60051 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17003E49 RID: 15945
		// (get) Token: 0x0600EA94 RID: 60052 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600EA95 RID: 60053 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17003E4A RID: 15946
		// (get) Token: 0x0600EA96 RID: 60054 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600EA97 RID: 60055 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17003E4B RID: 15947
		// (get) Token: 0x0600EA98 RID: 60056 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600EA99 RID: 60057 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17003E4C RID: 15948
		// (get) Token: 0x0600EA9A RID: 60058 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600EA9B RID: 60059 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
		{
			get
			{
				return (StringValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17003E4D RID: 15949
		// (get) Token: 0x0600EA9C RID: 60060 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600EA9D RID: 60061 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17003E4E RID: 15950
		// (get) Token: 0x0600EA9E RID: 60062 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600EA9F RID: 60063 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
		{
			get
			{
				return (StringValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x17003E4F RID: 15951
		// (get) Token: 0x0600EAA0 RID: 60064 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600EAA1 RID: 60065 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
		{
			get
			{
				return (StringValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x17003E50 RID: 15952
		// (get) Token: 0x0600EAA2 RID: 60066 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x0600EAA3 RID: 60067 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x17003E51 RID: 15953
		// (get) Token: 0x0600EAA4 RID: 60068 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600EAA5 RID: 60069 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x17003E52 RID: 15954
		// (get) Token: 0x0600EAA6 RID: 60070 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600EAA7 RID: 60071 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
		{
			get
			{
				return (StringValue)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x17003E53 RID: 15955
		// (get) Token: 0x0600EAA8 RID: 60072 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600EAA9 RID: 60073 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x17003E54 RID: 15956
		// (get) Token: 0x0600EAAA RID: 60074 RVA: 0x002C8B45 File Offset: 0x002C6D45
		// (set) Token: 0x0600EAAB RID: 60075 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x17003E55 RID: 15957
		// (get) Token: 0x0600EAAC RID: 60076 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600EAAD RID: 60077 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[27];
			}
			set
			{
				base.Attributes[27] = value;
			}
		}

		// Token: 0x17003E56 RID: 15958
		// (get) Token: 0x0600EAAE RID: 60078 RVA: 0x002C8B55 File Offset: 0x002C6D55
		// (set) Token: 0x0600EAAF RID: 60079 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
		{
			get
			{
				return (BooleanValue)base.Attributes[28];
			}
			set
			{
				base.Attributes[28] = value;
			}
		}

		// Token: 0x17003E57 RID: 15959
		// (get) Token: 0x0600EAB0 RID: 60080 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600EAB1 RID: 60081 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
		{
			get
			{
				return (StringValue)base.Attributes[29];
			}
			set
			{
				base.Attributes[29] = value;
			}
		}

		// Token: 0x0600EAB3 RID: 60083 RVA: 0x002CB308 File Offset: 0x002C9508
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
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getDescription" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "image" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "imageMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getImage" == name)
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
			if (namespaceId == 0 && "showImage" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getShowImage" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600EAB4 RID: 60084 RVA: 0x002CB5B1 File Offset: 0x002C97B1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ControlClone>(deep);
		}

		// Token: 0x0600EAB5 RID: 60085 RVA: 0x002CB5BC File Offset: 0x002C97BC
		// Note: this type is marked as 'beforefieldinit'.
		static ControlClone()
		{
			byte[] array = new byte[30];
			ControlClone.attributeNamespaceIds = array;
		}

		// Token: 0x04006FBC RID: 28604
		private const string tagName = "control";

		// Token: 0x04006FBD RID: 28605
		private const byte tagNsId = 34;

		// Token: 0x04006FBE RID: 28606
		internal const int ElementTypeIdConst = 12584;

		// Token: 0x04006FBF RID: 28607
		private static string[] attributeTagNames = new string[]
		{
			"size", "getSize", "enabled", "getEnabled", "description", "getDescription", "image", "imageMso", "getImage", "idQ",
			"idMso", "tag", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso", "insertBeforeMso",
			"insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x04006FC0 RID: 28608
		private static byte[] attributeNamespaceIds;
	}
}
