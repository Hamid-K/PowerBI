using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x0200228A RID: 8842
	[GeneratedCode("DomGen", "2.0")]
	internal class VisibleToggleButton : OpenXmlLeafElement
	{
		// Token: 0x1700402D RID: 16429
		// (get) Token: 0x0600EE84 RID: 61060 RVA: 0x002C99C0 File Offset: 0x002C7BC0
		public override string LocalName
		{
			get
			{
				return "toggleButton";
			}
		}

		// Token: 0x1700402E RID: 16430
		// (get) Token: 0x0600EE85 RID: 61061 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x1700402F RID: 16431
		// (get) Token: 0x0600EE86 RID: 61062 RVA: 0x002CF140 File Offset: 0x002CD340
		internal override int ElementTypeId
		{
			get
			{
				return 12601;
			}
		}

		// Token: 0x0600EE87 RID: 61063 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17004030 RID: 16432
		// (get) Token: 0x0600EE88 RID: 61064 RVA: 0x002CF147 File Offset: 0x002CD347
		internal override string[] AttributeTagNames
		{
			get
			{
				return VisibleToggleButton.attributeTagNames;
			}
		}

		// Token: 0x17004031 RID: 16433
		// (get) Token: 0x0600EE89 RID: 61065 RVA: 0x002CF14E File Offset: 0x002CD34E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VisibleToggleButton.attributeNamespaceIds;
			}
		}

		// Token: 0x17004032 RID: 16434
		// (get) Token: 0x0600EE8A RID: 61066 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EE8B RID: 61067 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "getPressed")]
		public StringValue GetPressed
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

		// Token: 0x17004033 RID: 16435
		// (get) Token: 0x0600EE8C RID: 61068 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EE8D RID: 61069 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x17004034 RID: 16436
		// (get) Token: 0x0600EE8E RID: 61070 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0600EE8F RID: 61071 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004035 RID: 16437
		// (get) Token: 0x0600EE90 RID: 61072 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EE91 RID: 61073 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17004036 RID: 16438
		// (get) Token: 0x0600EE92 RID: 61074 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EE93 RID: 61075 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17004037 RID: 16439
		// (get) Token: 0x0600EE94 RID: 61076 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EE95 RID: 61077 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17004038 RID: 16440
		// (get) Token: 0x0600EE96 RID: 61078 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EE97 RID: 61079 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17004039 RID: 16441
		// (get) Token: 0x0600EE98 RID: 61080 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EE99 RID: 61081 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x1700403A RID: 16442
		// (get) Token: 0x0600EE9A RID: 61082 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600EE9B RID: 61083 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x1700403B RID: 16443
		// (get) Token: 0x0600EE9C RID: 61084 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600EE9D RID: 61085 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x1700403C RID: 16444
		// (get) Token: 0x0600EE9E RID: 61086 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600EE9F RID: 61087 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x1700403D RID: 16445
		// (get) Token: 0x0600EEA0 RID: 61088 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600EEA1 RID: 61089 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x1700403E RID: 16446
		// (get) Token: 0x0600EEA2 RID: 61090 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600EEA3 RID: 61091 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x1700403F RID: 16447
		// (get) Token: 0x0600EEA4 RID: 61092 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600EEA5 RID: 61093 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17004040 RID: 16448
		// (get) Token: 0x0600EEA6 RID: 61094 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600EEA7 RID: 61095 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17004041 RID: 16449
		// (get) Token: 0x0600EEA8 RID: 61096 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600EEA9 RID: 61097 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17004042 RID: 16450
		// (get) Token: 0x0600EEAA RID: 61098 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600EEAB RID: 61099 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17004043 RID: 16451
		// (get) Token: 0x0600EEAC RID: 61100 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600EEAD RID: 61101 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17004044 RID: 16452
		// (get) Token: 0x0600EEAE RID: 61102 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600EEAF RID: 61103 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17004045 RID: 16453
		// (get) Token: 0x0600EEB0 RID: 61104 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600EEB1 RID: 61105 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17004046 RID: 16454
		// (get) Token: 0x0600EEB2 RID: 61106 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600EEB3 RID: 61107 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17004047 RID: 16455
		// (get) Token: 0x0600EEB4 RID: 61108 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600EEB5 RID: 61109 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17004048 RID: 16456
		// (get) Token: 0x0600EEB6 RID: 61110 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600EEB7 RID: 61111 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
		{
			get
			{
				return (StringValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x17004049 RID: 16457
		// (get) Token: 0x0600EEB8 RID: 61112 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600EEB9 RID: 61113 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x1700404A RID: 16458
		// (get) Token: 0x0600EEBA RID: 61114 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600EEBB RID: 61115 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x1700404B RID: 16459
		// (get) Token: 0x0600EEBC RID: 61116 RVA: 0x002CBE3C File Offset: 0x002CA03C
		// (set) Token: 0x0600EEBD RID: 61117 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x1700404C RID: 16460
		// (get) Token: 0x0600EEBE RID: 61118 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600EEBF RID: 61119 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x1700404D RID: 16461
		// (get) Token: 0x0600EEC0 RID: 61120 RVA: 0x002C99EC File Offset: 0x002C7BEC
		// (set) Token: 0x0600EEC1 RID: 61121 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
		{
			get
			{
				return (BooleanValue)base.Attributes[27];
			}
			set
			{
				base.Attributes[27] = value;
			}
		}

		// Token: 0x1700404E RID: 16462
		// (get) Token: 0x0600EEC2 RID: 61122 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600EEC3 RID: 61123 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
		{
			get
			{
				return (StringValue)base.Attributes[28];
			}
			set
			{
				base.Attributes[28] = value;
			}
		}

		// Token: 0x0600EEC5 RID: 61125 RVA: 0x002CF158 File Offset: 0x002CD358
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "getPressed" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "onAction" == name)
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

		// Token: 0x0600EEC6 RID: 61126 RVA: 0x002CF3EB File Offset: 0x002CD5EB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VisibleToggleButton>(deep);
		}

		// Token: 0x0600EEC7 RID: 61127 RVA: 0x002CF3F4 File Offset: 0x002CD5F4
		// Note: this type is marked as 'beforefieldinit'.
		static VisibleToggleButton()
		{
			byte[] array = new byte[29];
			VisibleToggleButton.attributeNamespaceIds = array;
		}

		// Token: 0x04007013 RID: 28691
		private const string tagName = "toggleButton";

		// Token: 0x04007014 RID: 28692
		private const byte tagNsId = 34;

		// Token: 0x04007015 RID: 28693
		internal const int ElementTypeIdConst = 12601;

		// Token: 0x04007016 RID: 28694
		private static string[] attributeTagNames = new string[]
		{
			"getPressed", "onAction", "enabled", "getEnabled", "description", "getDescription", "image", "imageMso", "getImage", "id",
			"idQ", "idMso", "tag", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso",
			"insertBeforeMso", "insertAfterQ", "insertBeforeQ", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x04007017 RID: 28695
		private static byte[] attributeNamespaceIds;
	}
}
