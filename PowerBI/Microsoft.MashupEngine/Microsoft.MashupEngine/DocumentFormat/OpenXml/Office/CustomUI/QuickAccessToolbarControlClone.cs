using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x0200228E RID: 8846
	[GeneratedCode("DomGen", "2.0")]
	internal class QuickAccessToolbarControlClone : OpenXmlLeafElement
	{
		// Token: 0x1700407D RID: 16509
		// (get) Token: 0x0600EF2A RID: 61226 RVA: 0x002AD773 File Offset: 0x002AB973
		public override string LocalName
		{
			get
			{
				return "control";
			}
		}

		// Token: 0x1700407E RID: 16510
		// (get) Token: 0x0600EF2B RID: 61227 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x1700407F RID: 16511
		// (get) Token: 0x0600EF2C RID: 61228 RVA: 0x002CFB69 File Offset: 0x002CDD69
		internal override int ElementTypeId
		{
			get
			{
				return 12605;
			}
		}

		// Token: 0x0600EF2D RID: 61229 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17004080 RID: 16512
		// (get) Token: 0x0600EF2E RID: 61230 RVA: 0x002CFB70 File Offset: 0x002CDD70
		internal override string[] AttributeTagNames
		{
			get
			{
				return QuickAccessToolbarControlClone.attributeTagNames;
			}
		}

		// Token: 0x17004081 RID: 16513
		// (get) Token: 0x0600EF2F RID: 61231 RVA: 0x002CFB77 File Offset: 0x002CDD77
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return QuickAccessToolbarControlClone.attributeNamespaceIds;
			}
		}

		// Token: 0x17004082 RID: 16514
		// (get) Token: 0x0600EF30 RID: 61232 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EF31 RID: 61233 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004083 RID: 16515
		// (get) Token: 0x0600EF32 RID: 61234 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EF33 RID: 61235 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004084 RID: 16516
		// (get) Token: 0x0600EF34 RID: 61236 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EF35 RID: 61237 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004085 RID: 16517
		// (get) Token: 0x0600EF36 RID: 61238 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EF37 RID: 61239 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17004086 RID: 16518
		// (get) Token: 0x0600EF38 RID: 61240 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EF39 RID: 61241 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x17004087 RID: 16519
		// (get) Token: 0x0600EF3A RID: 61242 RVA: 0x002CFB7E File Offset: 0x002CDD7E
		// (set) Token: 0x0600EF3B RID: 61243 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "size")]
		public EnumValue<SizeValues> Size
		{
			get
			{
				return (EnumValue<SizeValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17004088 RID: 16520
		// (get) Token: 0x0600EF3C RID: 61244 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EF3D RID: 61245 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getSize")]
		public StringValue GetSize
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

		// Token: 0x17004089 RID: 16521
		// (get) Token: 0x0600EF3E RID: 61246 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EF3F RID: 61247 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x1700408A RID: 16522
		// (get) Token: 0x0600EF40 RID: 61248 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600EF41 RID: 61249 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x1700408B RID: 16523
		// (get) Token: 0x0600EF42 RID: 61250 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600EF43 RID: 61251 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x1700408C RID: 16524
		// (get) Token: 0x0600EF44 RID: 61252 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600EF45 RID: 61253 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x1700408D RID: 16525
		// (get) Token: 0x0600EF46 RID: 61254 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600EF47 RID: 61255 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x1700408E RID: 16526
		// (get) Token: 0x0600EF48 RID: 61256 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600EF49 RID: 61257 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x1700408F RID: 16527
		// (get) Token: 0x0600EF4A RID: 61258 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600EF4B RID: 61259 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17004090 RID: 16528
		// (get) Token: 0x0600EF4C RID: 61260 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x0600EF4D RID: 61261 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17004091 RID: 16529
		// (get) Token: 0x0600EF4E RID: 61262 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600EF4F RID: 61263 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17004092 RID: 16530
		// (get) Token: 0x0600EF50 RID: 61264 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600EF51 RID: 61265 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17004093 RID: 16531
		// (get) Token: 0x0600EF52 RID: 61266 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600EF53 RID: 61267 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17004094 RID: 16532
		// (get) Token: 0x0600EF54 RID: 61268 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600EF55 RID: 61269 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17004095 RID: 16533
		// (get) Token: 0x0600EF56 RID: 61270 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600EF57 RID: 61271 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17004096 RID: 16534
		// (get) Token: 0x0600EF58 RID: 61272 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600EF59 RID: 61273 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17004097 RID: 16535
		// (get) Token: 0x0600EF5A RID: 61274 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600EF5B RID: 61275 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17004098 RID: 16536
		// (get) Token: 0x0600EF5C RID: 61276 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x0600EF5D RID: 61277 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17004099 RID: 16537
		// (get) Token: 0x0600EF5E RID: 61278 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600EF5F RID: 61279 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x1700409A RID: 16538
		// (get) Token: 0x0600EF60 RID: 61280 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600EF61 RID: 61281 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x1700409B RID: 16539
		// (get) Token: 0x0600EF62 RID: 61282 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600EF63 RID: 61283 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x1700409C RID: 16540
		// (get) Token: 0x0600EF64 RID: 61284 RVA: 0x002C8B45 File Offset: 0x002C6D45
		// (set) Token: 0x0600EF65 RID: 61285 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x1700409D RID: 16541
		// (get) Token: 0x0600EF66 RID: 61286 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600EF67 RID: 61287 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x1700409E RID: 16542
		// (get) Token: 0x0600EF68 RID: 61288 RVA: 0x002C8B55 File Offset: 0x002C6D55
		// (set) Token: 0x0600EF69 RID: 61289 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x1700409F RID: 16543
		// (get) Token: 0x0600EF6A RID: 61290 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600EF6B RID: 61291 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x0600EF6D RID: 61293 RVA: 0x002CFB90 File Offset: 0x002CDD90
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
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getDescription" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "size" == name)
			{
				return new EnumValue<SizeValues>();
			}
			if (namespaceId == 0 && "getSize" == name)
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

		// Token: 0x0600EF6E RID: 61294 RVA: 0x002CFE39 File Offset: 0x002CE039
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<QuickAccessToolbarControlClone>(deep);
		}

		// Token: 0x0600EF6F RID: 61295 RVA: 0x002CFE44 File Offset: 0x002CE044
		// Note: this type is marked as 'beforefieldinit'.
		static QuickAccessToolbarControlClone()
		{
			byte[] array = new byte[30];
			QuickAccessToolbarControlClone.attributeNamespaceIds = array;
		}

		// Token: 0x04007027 RID: 28711
		private const string tagName = "control";

		// Token: 0x04007028 RID: 28712
		private const byte tagNsId = 34;

		// Token: 0x04007029 RID: 28713
		internal const int ElementTypeIdConst = 12605;

		// Token: 0x0400702A RID: 28714
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "idMso", "description", "getDescription", "size", "getSize", "image", "imageMso", "getImage",
			"screentip", "getScreentip", "supertip", "getSupertip", "enabled", "getEnabled", "label", "getLabel", "insertAfterMso", "insertBeforeMso",
			"insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x0400702B RID: 28715
		private static byte[] attributeNamespaceIds;
	}
}
