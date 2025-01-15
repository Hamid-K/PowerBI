using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022CA RID: 8906
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class EditBox : OpenXmlLeafElement
	{
		// Token: 0x17004377 RID: 17271
		// (get) Token: 0x0600F58F RID: 62863 RVA: 0x002CC299 File Offset: 0x002CA499
		public override string LocalName
		{
			get
			{
				return "editBox";
			}
		}

		// Token: 0x17004378 RID: 17272
		// (get) Token: 0x0600F590 RID: 62864 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004379 RID: 17273
		// (get) Token: 0x0600F591 RID: 62865 RVA: 0x002D5211 File Offset: 0x002D3411
		internal override int ElementTypeId
		{
			get
			{
				return 13051;
			}
		}

		// Token: 0x0600F592 RID: 62866 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700437A RID: 17274
		// (get) Token: 0x0600F593 RID: 62867 RVA: 0x002D5218 File Offset: 0x002D3418
		internal override string[] AttributeTagNames
		{
			get
			{
				return EditBox.attributeTagNames;
			}
		}

		// Token: 0x1700437B RID: 17275
		// (get) Token: 0x0600F594 RID: 62868 RVA: 0x002D521F File Offset: 0x002D341F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return EditBox.attributeNamespaceIds;
			}
		}

		// Token: 0x1700437C RID: 17276
		// (get) Token: 0x0600F595 RID: 62869 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0600F596 RID: 62870 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700437D RID: 17277
		// (get) Token: 0x0600F597 RID: 62871 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F598 RID: 62872 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x1700437E RID: 17278
		// (get) Token: 0x0600F599 RID: 62873 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F59A RID: 62874 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x1700437F RID: 17279
		// (get) Token: 0x0600F59B RID: 62875 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F59C RID: 62876 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17004380 RID: 17280
		// (get) Token: 0x0600F59D RID: 62877 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F59E RID: 62878 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17004381 RID: 17281
		// (get) Token: 0x0600F59F RID: 62879 RVA: 0x002BDE3A File Offset: 0x002BC03A
		// (set) Token: 0x0600F5A0 RID: 62880 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17004382 RID: 17282
		// (get) Token: 0x0600F5A1 RID: 62881 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F5A2 RID: 62882 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17004383 RID: 17283
		// (get) Token: 0x0600F5A3 RID: 62883 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F5A4 RID: 62884 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17004384 RID: 17284
		// (get) Token: 0x0600F5A5 RID: 62885 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F5A6 RID: 62886 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17004385 RID: 17285
		// (get) Token: 0x0600F5A7 RID: 62887 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F5A8 RID: 62888 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17004386 RID: 17286
		// (get) Token: 0x0600F5A9 RID: 62889 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F5AA RID: 62890 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x17004387 RID: 17287
		// (get) Token: 0x0600F5AB RID: 62891 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F5AC RID: 62892 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17004388 RID: 17288
		// (get) Token: 0x0600F5AD RID: 62893 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F5AE RID: 62894 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17004389 RID: 17289
		// (get) Token: 0x0600F5AF RID: 62895 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F5B0 RID: 62896 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x1700438A RID: 17290
		// (get) Token: 0x0600F5B1 RID: 62897 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F5B2 RID: 62898 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x1700438B RID: 17291
		// (get) Token: 0x0600F5B3 RID: 62899 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F5B4 RID: 62900 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x1700438C RID: 17292
		// (get) Token: 0x0600F5B5 RID: 62901 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F5B6 RID: 62902 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x1700438D RID: 17293
		// (get) Token: 0x0600F5B7 RID: 62903 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F5B8 RID: 62904 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x1700438E RID: 17294
		// (get) Token: 0x0600F5B9 RID: 62905 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F5BA RID: 62906 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x1700438F RID: 17295
		// (get) Token: 0x0600F5BB RID: 62907 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F5BC RID: 62908 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17004390 RID: 17296
		// (get) Token: 0x0600F5BD RID: 62909 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F5BE RID: 62910 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17004391 RID: 17297
		// (get) Token: 0x0600F5BF RID: 62911 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F5C0 RID: 62912 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x17004392 RID: 17298
		// (get) Token: 0x0600F5C1 RID: 62913 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600F5C2 RID: 62914 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x17004393 RID: 17299
		// (get) Token: 0x0600F5C3 RID: 62915 RVA: 0x002C99DC File Offset: 0x002C7BDC
		// (set) Token: 0x0600F5C4 RID: 62916 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x17004394 RID: 17300
		// (get) Token: 0x0600F5C5 RID: 62917 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600F5C6 RID: 62918 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x17004395 RID: 17301
		// (get) Token: 0x0600F5C7 RID: 62919 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600F5C8 RID: 62920 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x17004396 RID: 17302
		// (get) Token: 0x0600F5C9 RID: 62921 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600F5CA RID: 62922 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x17004397 RID: 17303
		// (get) Token: 0x0600F5CB RID: 62923 RVA: 0x002C99EC File Offset: 0x002C7BEC
		// (set) Token: 0x0600F5CC RID: 62924 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x17004398 RID: 17304
		// (get) Token: 0x0600F5CD RID: 62925 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600F5CE RID: 62926 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x17004399 RID: 17305
		// (get) Token: 0x0600F5CF RID: 62927 RVA: 0x002C99FC File Offset: 0x002C7BFC
		// (set) Token: 0x0600F5D0 RID: 62928 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x1700439A RID: 17306
		// (get) Token: 0x0600F5D1 RID: 62929 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600F5D2 RID: 62930 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
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

		// Token: 0x0600F5D4 RID: 62932 RVA: 0x002D5228 File Offset: 0x002D3428
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

		// Token: 0x0600F5D5 RID: 62933 RVA: 0x002D54E7 File Offset: 0x002D36E7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EditBox>(deep);
		}

		// Token: 0x0600F5D6 RID: 62934 RVA: 0x002D54F0 File Offset: 0x002D36F0
		// Note: this type is marked as 'beforefieldinit'.
		static EditBox()
		{
			byte[] array = new byte[31];
			EditBox.attributeNamespaceIds = array;
		}

		// Token: 0x0400711F RID: 28959
		private const string tagName = "editBox";

		// Token: 0x04007120 RID: 28960
		private const byte tagNsId = 57;

		// Token: 0x04007121 RID: 28961
		internal const int ElementTypeIdConst = 13051;

		// Token: 0x04007122 RID: 28962
		private static string[] attributeTagNames = new string[]
		{
			"enabled", "getEnabled", "image", "imageMso", "getImage", "maxLength", "getText", "onChange", "sizeString", "id",
			"idQ", "tag", "idMso", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso",
			"insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage",
			"getShowImage"
		};

		// Token: 0x04007123 RID: 28963
		private static byte[] attributeNamespaceIds;
	}
}
