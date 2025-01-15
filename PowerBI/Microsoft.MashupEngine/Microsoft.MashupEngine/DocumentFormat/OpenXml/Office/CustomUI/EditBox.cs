using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x0200227D RID: 8829
	[GeneratedCode("DomGen", "2.0")]
	internal class EditBox : OpenXmlLeafElement
	{
		// Token: 0x17003EBC RID: 16060
		// (get) Token: 0x0600EB7E RID: 60286 RVA: 0x002CC299 File Offset: 0x002CA499
		public override string LocalName
		{
			get
			{
				return "editBox";
			}
		}

		// Token: 0x17003EBD RID: 16061
		// (get) Token: 0x0600EB7F RID: 60287 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003EBE RID: 16062
		// (get) Token: 0x0600EB80 RID: 60288 RVA: 0x002CC2A0 File Offset: 0x002CA4A0
		internal override int ElementTypeId
		{
			get
			{
				return 12588;
			}
		}

		// Token: 0x0600EB81 RID: 60289 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003EBF RID: 16063
		// (get) Token: 0x0600EB82 RID: 60290 RVA: 0x002CC2A7 File Offset: 0x002CA4A7
		internal override string[] AttributeTagNames
		{
			get
			{
				return EditBox.attributeTagNames;
			}
		}

		// Token: 0x17003EC0 RID: 16064
		// (get) Token: 0x0600EB83 RID: 60291 RVA: 0x002CC2AE File Offset: 0x002CA4AE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return EditBox.attributeNamespaceIds;
			}
		}

		// Token: 0x17003EC1 RID: 16065
		// (get) Token: 0x0600EB84 RID: 60292 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0600EB85 RID: 60293 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003EC2 RID: 16066
		// (get) Token: 0x0600EB86 RID: 60294 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EB87 RID: 60295 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17003EC3 RID: 16067
		// (get) Token: 0x0600EB88 RID: 60296 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EB89 RID: 60297 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17003EC4 RID: 16068
		// (get) Token: 0x0600EB8A RID: 60298 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EB8B RID: 60299 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17003EC5 RID: 16069
		// (get) Token: 0x0600EB8C RID: 60300 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EB8D RID: 60301 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17003EC6 RID: 16070
		// (get) Token: 0x0600EB8E RID: 60302 RVA: 0x002BDE3A File Offset: 0x002BC03A
		// (set) Token: 0x0600EB8F RID: 60303 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "maxLength")]
		public IntegerValue MaxLength
		{
			get
			{
				return (IntegerValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17003EC7 RID: 16071
		// (get) Token: 0x0600EB90 RID: 60304 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EB91 RID: 60305 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getText")]
		public StringValue GetText
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

		// Token: 0x17003EC8 RID: 16072
		// (get) Token: 0x0600EB92 RID: 60306 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EB93 RID: 60307 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "onChange")]
		public StringValue OnChange
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

		// Token: 0x17003EC9 RID: 16073
		// (get) Token: 0x0600EB94 RID: 60308 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600EB95 RID: 60309 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "sizeString")]
		public StringValue SizeString
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

		// Token: 0x17003ECA RID: 16074
		// (get) Token: 0x0600EB96 RID: 60310 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600EB97 RID: 60311 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003ECB RID: 16075
		// (get) Token: 0x0600EB98 RID: 60312 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600EB99 RID: 60313 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003ECC RID: 16076
		// (get) Token: 0x0600EB9A RID: 60314 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600EB9B RID: 60315 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003ECD RID: 16077
		// (get) Token: 0x0600EB9C RID: 60316 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600EB9D RID: 60317 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17003ECE RID: 16078
		// (get) Token: 0x0600EB9E RID: 60318 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600EB9F RID: 60319 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003ECF RID: 16079
		// (get) Token: 0x0600EBA0 RID: 60320 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600EBA1 RID: 60321 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17003ED0 RID: 16080
		// (get) Token: 0x0600EBA2 RID: 60322 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600EBA3 RID: 60323 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x17003ED1 RID: 16081
		// (get) Token: 0x0600EBA4 RID: 60324 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600EBA5 RID: 60325 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17003ED2 RID: 16082
		// (get) Token: 0x0600EBA6 RID: 60326 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600EBA7 RID: 60327 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17003ED3 RID: 16083
		// (get) Token: 0x0600EBA8 RID: 60328 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600EBA9 RID: 60329 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17003ED4 RID: 16084
		// (get) Token: 0x0600EBAA RID: 60330 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600EBAB RID: 60331 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17003ED5 RID: 16085
		// (get) Token: 0x0600EBAC RID: 60332 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600EBAD RID: 60333 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17003ED6 RID: 16086
		// (get) Token: 0x0600EBAE RID: 60334 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600EBAF RID: 60335 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17003ED7 RID: 16087
		// (get) Token: 0x0600EBB0 RID: 60336 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600EBB1 RID: 60337 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17003ED8 RID: 16088
		// (get) Token: 0x0600EBB2 RID: 60338 RVA: 0x002C99DC File Offset: 0x002C7BDC
		// (set) Token: 0x0600EBB3 RID: 60339 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x17003ED9 RID: 16089
		// (get) Token: 0x0600EBB4 RID: 60340 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600EBB5 RID: 60341 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17003EDA RID: 16090
		// (get) Token: 0x0600EBB6 RID: 60342 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600EBB7 RID: 60343 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17003EDB RID: 16091
		// (get) Token: 0x0600EBB8 RID: 60344 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600EBB9 RID: 60345 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17003EDC RID: 16092
		// (get) Token: 0x0600EBBA RID: 60346 RVA: 0x002C99EC File Offset: 0x002C7BEC
		// (set) Token: 0x0600EBBB RID: 60347 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x17003EDD RID: 16093
		// (get) Token: 0x0600EBBC RID: 60348 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600EBBD RID: 60349 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x17003EDE RID: 16094
		// (get) Token: 0x0600EBBE RID: 60350 RVA: 0x002C99FC File Offset: 0x002C7BFC
		// (set) Token: 0x0600EBBF RID: 60351 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
		{
			get
			{
				return (BooleanValue)base.Attributes[29];
			}
			set
			{
				base.Attributes[29] = value;
			}
		}

		// Token: 0x17003EDF RID: 16095
		// (get) Token: 0x0600EBC0 RID: 60352 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600EBC1 RID: 60353 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
		{
			get
			{
				return (StringValue)base.Attributes[30];
			}
			set
			{
				base.Attributes[30] = value;
			}
		}

		// Token: 0x0600EBC3 RID: 60355 RVA: 0x002CC2B8 File Offset: 0x002CA4B8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "enabled" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getEnabled" == name)
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
			if (namespaceId == 0 && "maxLength" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "getText" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "onChange" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sizeString" == name)
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

		// Token: 0x0600EBC4 RID: 60356 RVA: 0x002CC577 File Offset: 0x002CA777
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EditBox>(deep);
		}

		// Token: 0x0600EBC5 RID: 60357 RVA: 0x002CC580 File Offset: 0x002CA780
		// Note: this type is marked as 'beforefieldinit'.
		static EditBox()
		{
			byte[] array = new byte[31];
			EditBox.attributeNamespaceIds = array;
		}

		// Token: 0x04006FD0 RID: 28624
		private const string tagName = "editBox";

		// Token: 0x04006FD1 RID: 28625
		private const byte tagNsId = 34;

		// Token: 0x04006FD2 RID: 28626
		internal const int ElementTypeIdConst = 12588;

		// Token: 0x04006FD3 RID: 28627
		private static string[] attributeTagNames = new string[]
		{
			"enabled", "getEnabled", "image", "imageMso", "getImage", "maxLength", "getText", "onChange", "sizeString", "id",
			"idQ", "idMso", "tag", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso",
			"insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage",
			"getShowImage"
		};

		// Token: 0x04006FD4 RID: 28628
		private static byte[] attributeNamespaceIds;
	}
}
