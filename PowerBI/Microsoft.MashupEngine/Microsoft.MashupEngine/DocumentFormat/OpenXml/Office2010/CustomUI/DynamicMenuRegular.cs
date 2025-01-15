using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022C2 RID: 8898
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DynamicMenuRegular : OpenXmlLeafElement
	{
		// Token: 0x17004288 RID: 17032
		// (get) Token: 0x0600F3A9 RID: 62377 RVA: 0x002CA71A File Offset: 0x002C891A
		public override string LocalName
		{
			get
			{
				return "dynamicMenu";
			}
		}

		// Token: 0x17004289 RID: 17033
		// (get) Token: 0x0600F3AA RID: 62378 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700428A RID: 17034
		// (get) Token: 0x0600F3AB RID: 62379 RVA: 0x002D35EE File Offset: 0x002D17EE
		internal override int ElementTypeId
		{
			get
			{
				return 13043;
			}
		}

		// Token: 0x0600F3AC RID: 62380 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700428B RID: 17035
		// (get) Token: 0x0600F3AD RID: 62381 RVA: 0x002D35F5 File Offset: 0x002D17F5
		internal override string[] AttributeTagNames
		{
			get
			{
				return DynamicMenuRegular.attributeTagNames;
			}
		}

		// Token: 0x1700428C RID: 17036
		// (get) Token: 0x0600F3AE RID: 62382 RVA: 0x002D35FC File Offset: 0x002D17FC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DynamicMenuRegular.attributeNamespaceIds;
			}
		}

		// Token: 0x1700428D RID: 17037
		// (get) Token: 0x0600F3AF RID: 62383 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F3B0 RID: 62384 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x1700428E RID: 17038
		// (get) Token: 0x0600F3B1 RID: 62385 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F3B2 RID: 62386 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x1700428F RID: 17039
		// (get) Token: 0x0600F3B3 RID: 62387 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F3B4 RID: 62388 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17004290 RID: 17040
		// (get) Token: 0x0600F3B5 RID: 62389 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F3B6 RID: 62390 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x17004291 RID: 17041
		// (get) Token: 0x0600F3B7 RID: 62391 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F3B8 RID: 62392 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17004292 RID: 17042
		// (get) Token: 0x0600F3B9 RID: 62393 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F3BA RID: 62394 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17004293 RID: 17043
		// (get) Token: 0x0600F3BB RID: 62395 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F3BC RID: 62396 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getContent")]
		public StringValue GetContent
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

		// Token: 0x17004294 RID: 17044
		// (get) Token: 0x0600F3BD RID: 62397 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x0600F3BE RID: 62398 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "invalidateContentOnDrop")]
		public BooleanValue InvalidateContentOnDrop
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17004295 RID: 17045
		// (get) Token: 0x0600F3BF RID: 62399 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F3C0 RID: 62400 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17004296 RID: 17046
		// (get) Token: 0x0600F3C1 RID: 62401 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F3C2 RID: 62402 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17004297 RID: 17047
		// (get) Token: 0x0600F3C3 RID: 62403 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F3C4 RID: 62404 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17004298 RID: 17048
		// (get) Token: 0x0600F3C5 RID: 62405 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F3C6 RID: 62406 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17004299 RID: 17049
		// (get) Token: 0x0600F3C7 RID: 62407 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F3C8 RID: 62408 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x1700429A RID: 17050
		// (get) Token: 0x0600F3C9 RID: 62409 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F3CA RID: 62410 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x1700429B RID: 17051
		// (get) Token: 0x0600F3CB RID: 62411 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F3CC RID: 62412 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x1700429C RID: 17052
		// (get) Token: 0x0600F3CD RID: 62413 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x0600F3CE RID: 62414 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x1700429D RID: 17053
		// (get) Token: 0x0600F3CF RID: 62415 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F3D0 RID: 62416 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x1700429E RID: 17054
		// (get) Token: 0x0600F3D1 RID: 62417 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F3D2 RID: 62418 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x1700429F RID: 17055
		// (get) Token: 0x0600F3D3 RID: 62419 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F3D4 RID: 62420 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x170042A0 RID: 17056
		// (get) Token: 0x0600F3D5 RID: 62421 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F3D6 RID: 62422 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x170042A1 RID: 17057
		// (get) Token: 0x0600F3D7 RID: 62423 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F3D8 RID: 62424 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x170042A2 RID: 17058
		// (get) Token: 0x0600F3D9 RID: 62425 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F3DA RID: 62426 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x170042A3 RID: 17059
		// (get) Token: 0x0600F3DB RID: 62427 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600F3DC RID: 62428 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x170042A4 RID: 17060
		// (get) Token: 0x0600F3DD RID: 62429 RVA: 0x002C99DC File Offset: 0x002C7BDC
		// (set) Token: 0x0600F3DE RID: 62430 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x170042A5 RID: 17061
		// (get) Token: 0x0600F3DF RID: 62431 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600F3E0 RID: 62432 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x170042A6 RID: 17062
		// (get) Token: 0x0600F3E1 RID: 62433 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600F3E2 RID: 62434 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x170042A7 RID: 17063
		// (get) Token: 0x0600F3E3 RID: 62435 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600F3E4 RID: 62436 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x170042A8 RID: 17064
		// (get) Token: 0x0600F3E5 RID: 62437 RVA: 0x002C99EC File Offset: 0x002C7BEC
		// (set) Token: 0x0600F3E6 RID: 62438 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x170042A9 RID: 17065
		// (get) Token: 0x0600F3E7 RID: 62439 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600F3E8 RID: 62440 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x170042AA RID: 17066
		// (get) Token: 0x0600F3E9 RID: 62441 RVA: 0x002C99FC File Offset: 0x002C7BFC
		// (set) Token: 0x0600F3EA RID: 62442 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x170042AB RID: 17067
		// (get) Token: 0x0600F3EB RID: 62443 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600F3EC RID: 62444 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
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

		// Token: 0x0600F3EE RID: 62446 RVA: 0x002D3604 File Offset: 0x002D1804
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getDescription" == name)
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
			if (namespaceId == 0 && "getContent" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "invalidateContentOnDrop" == name)
			{
				return new BooleanValue();
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

		// Token: 0x0600F3EF RID: 62447 RVA: 0x002D38C3 File Offset: 0x002D1AC3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DynamicMenuRegular>(deep);
		}

		// Token: 0x0600F3F0 RID: 62448 RVA: 0x002D38CC File Offset: 0x002D1ACC
		// Note: this type is marked as 'beforefieldinit'.
		static DynamicMenuRegular()
		{
			byte[] array = new byte[31];
			DynamicMenuRegular.attributeNamespaceIds = array;
		}

		// Token: 0x040070F7 RID: 28919
		private const string tagName = "dynamicMenu";

		// Token: 0x040070F8 RID: 28920
		private const byte tagNsId = 57;

		// Token: 0x040070F9 RID: 28921
		internal const int ElementTypeIdConst = 13043;

		// Token: 0x040070FA RID: 28922
		private static string[] attributeTagNames = new string[]
		{
			"description", "getDescription", "id", "idQ", "tag", "idMso", "getContent", "invalidateContentOnDrop", "image", "imageMso",
			"getImage", "screentip", "getScreentip", "supertip", "getSupertip", "enabled", "getEnabled", "label", "getLabel", "insertAfterMso",
			"insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage",
			"getShowImage"
		};

		// Token: 0x040070FB RID: 28923
		private static byte[] attributeNamespaceIds;
	}
}
