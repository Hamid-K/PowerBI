using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x02002308 RID: 8968
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BackstageGroups), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SimpleGroups), FileFormatVersions.Office2010)]
	internal class BackstageTab : OpenXmlCompositeElement
	{
		// Token: 0x170047D2 RID: 18386
		// (get) Token: 0x0600FEDE RID: 65246 RVA: 0x002D001B File Offset: 0x002CE21B
		public override string LocalName
		{
			get
			{
				return "tab";
			}
		}

		// Token: 0x170047D3 RID: 18387
		// (get) Token: 0x0600FEDF RID: 65247 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170047D4 RID: 18388
		// (get) Token: 0x0600FEE0 RID: 65248 RVA: 0x002DD7B9 File Offset: 0x002DB9B9
		internal override int ElementTypeId
		{
			get
			{
				return 13110;
			}
		}

		// Token: 0x0600FEE1 RID: 65249 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170047D5 RID: 18389
		// (get) Token: 0x0600FEE2 RID: 65250 RVA: 0x002DD7C0 File Offset: 0x002DB9C0
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageTab.attributeTagNames;
			}
		}

		// Token: 0x170047D6 RID: 18390
		// (get) Token: 0x0600FEE3 RID: 65251 RVA: 0x002DD7C7 File Offset: 0x002DB9C7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageTab.attributeNamespaceIds;
			}
		}

		// Token: 0x170047D7 RID: 18391
		// (get) Token: 0x0600FEE4 RID: 65252 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FEE5 RID: 65253 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170047D8 RID: 18392
		// (get) Token: 0x0600FEE6 RID: 65254 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FEE7 RID: 65255 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170047D9 RID: 18393
		// (get) Token: 0x0600FEE8 RID: 65256 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FEE9 RID: 65257 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170047DA RID: 18394
		// (get) Token: 0x0600FEEA RID: 65258 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FEEB RID: 65259 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170047DB RID: 18395
		// (get) Token: 0x0600FEEC RID: 65260 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FEED RID: 65261 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x170047DC RID: 18396
		// (get) Token: 0x0600FEEE RID: 65262 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FEEF RID: 65263 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x170047DD RID: 18397
		// (get) Token: 0x0600FEF0 RID: 65264 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FEF1 RID: 65265 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x170047DE RID: 18398
		// (get) Token: 0x0600FEF2 RID: 65266 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FEF3 RID: 65267 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x170047DF RID: 18399
		// (get) Token: 0x0600FEF4 RID: 65268 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x0600FEF5 RID: 65269 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x170047E0 RID: 18400
		// (get) Token: 0x0600FEF6 RID: 65270 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600FEF7 RID: 65271 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x170047E1 RID: 18401
		// (get) Token: 0x0600FEF8 RID: 65272 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600FEF9 RID: 65273 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x170047E2 RID: 18402
		// (get) Token: 0x0600FEFA RID: 65274 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FEFB RID: 65275 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x170047E3 RID: 18403
		// (get) Token: 0x0600FEFC RID: 65276 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x0600FEFD RID: 65277 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x170047E4 RID: 18404
		// (get) Token: 0x0600FEFE RID: 65278 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600FEFF RID: 65279 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x170047E5 RID: 18405
		// (get) Token: 0x0600FF00 RID: 65280 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600FF01 RID: 65281 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x170047E6 RID: 18406
		// (get) Token: 0x0600FF02 RID: 65282 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600FF03 RID: 65283 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x170047E7 RID: 18407
		// (get) Token: 0x0600FF04 RID: 65284 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600FF05 RID: 65285 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "title")]
		public StringValue Title
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

		// Token: 0x170047E8 RID: 18408
		// (get) Token: 0x0600FF06 RID: 65286 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600FF07 RID: 65287 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getTitle")]
		public StringValue GetTitle
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

		// Token: 0x170047E9 RID: 18409
		// (get) Token: 0x0600FF08 RID: 65288 RVA: 0x002DD7CE File Offset: 0x002DB9CE
		// (set) Token: 0x0600FF09 RID: 65289 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "columnWidthPercent")]
		public IntegerValue ColumnWidthPercent
		{
			get
			{
				return (IntegerValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x170047EA RID: 18410
		// (get) Token: 0x0600FF0A RID: 65290 RVA: 0x002C32C2 File Offset: 0x002C14C2
		// (set) Token: 0x0600FF0B RID: 65291 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "firstColumnMinWidth")]
		public IntegerValue FirstColumnMinWidth
		{
			get
			{
				return (IntegerValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x170047EB RID: 18411
		// (get) Token: 0x0600FF0C RID: 65292 RVA: 0x002C32D2 File Offset: 0x002C14D2
		// (set) Token: 0x0600FF0D RID: 65293 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "firstColumnMaxWidth")]
		public IntegerValue FirstColumnMaxWidth
		{
			get
			{
				return (IntegerValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x170047EC RID: 18412
		// (get) Token: 0x0600FF0E RID: 65294 RVA: 0x002C32E2 File Offset: 0x002C14E2
		// (set) Token: 0x0600FF0F RID: 65295 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "secondColumnMinWidth")]
		public IntegerValue SecondColumnMinWidth
		{
			get
			{
				return (IntegerValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x170047ED RID: 18413
		// (get) Token: 0x0600FF10 RID: 65296 RVA: 0x002DD7DE File Offset: 0x002DB9DE
		// (set) Token: 0x0600FF11 RID: 65297 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "secondColumnMaxWidth")]
		public IntegerValue SecondColumnMaxWidth
		{
			get
			{
				return (IntegerValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x0600FF12 RID: 65298 RVA: 0x00293ECF File Offset: 0x002920CF
		public BackstageTab()
		{
		}

		// Token: 0x0600FF13 RID: 65299 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BackstageTab(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FF14 RID: 65300 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BackstageTab(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FF15 RID: 65301 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BackstageTab(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FF16 RID: 65302 RVA: 0x002DD7EE File Offset: 0x002DB9EE
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "firstColumn" == name)
			{
				return new BackstageGroups();
			}
			if (57 == namespaceId && "secondColumn" == name)
			{
				return new SimpleGroups();
			}
			return null;
		}

		// Token: 0x170047EE RID: 18414
		// (get) Token: 0x0600FF17 RID: 65303 RVA: 0x002DD821 File Offset: 0x002DBA21
		internal override string[] ElementTagNames
		{
			get
			{
				return BackstageTab.eleTagNames;
			}
		}

		// Token: 0x170047EF RID: 18415
		// (get) Token: 0x0600FF18 RID: 65304 RVA: 0x002DD828 File Offset: 0x002DBA28
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BackstageTab.eleNamespaceIds;
			}
		}

		// Token: 0x170047F0 RID: 18416
		// (get) Token: 0x0600FF19 RID: 65305 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170047F1 RID: 18417
		// (get) Token: 0x0600FF1A RID: 65306 RVA: 0x002DD82F File Offset: 0x002DBA2F
		// (set) Token: 0x0600FF1B RID: 65307 RVA: 0x002DD838 File Offset: 0x002DBA38
		public BackstageGroups BackstageGroups
		{
			get
			{
				return base.GetElement<BackstageGroups>(0);
			}
			set
			{
				base.SetElement<BackstageGroups>(0, value);
			}
		}

		// Token: 0x170047F2 RID: 18418
		// (get) Token: 0x0600FF1C RID: 65308 RVA: 0x002DD842 File Offset: 0x002DBA42
		// (set) Token: 0x0600FF1D RID: 65309 RVA: 0x002DD84B File Offset: 0x002DBA4B
		public SimpleGroups SimpleGroups
		{
			get
			{
				return base.GetElement<SimpleGroups>(1);
			}
			set
			{
				base.SetElement<SimpleGroups>(1, value);
			}
		}

		// Token: 0x0600FF1E RID: 65310 RVA: 0x002DD858 File Offset: 0x002DBA58
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
			if (namespaceId == 0 && "keytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getKeytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "title" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getTitle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "columnWidthPercent" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "firstColumnMinWidth" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "firstColumnMaxWidth" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "secondColumnMinWidth" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "secondColumnMaxWidth" == name)
			{
				return new IntegerValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FF1F RID: 65311 RVA: 0x002DDA67 File Offset: 0x002DBC67
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageTab>(deep);
		}

		// Token: 0x0600FF20 RID: 65312 RVA: 0x002DDA70 File Offset: 0x002DBC70
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageTab()
		{
			byte[] array = new byte[23];
			BackstageTab.attributeNamespaceIds = array;
			BackstageTab.eleTagNames = new string[] { "firstColumn", "secondColumn" };
			BackstageTab.eleNamespaceIds = new byte[] { 57, 57 };
		}

		// Token: 0x04007236 RID: 29238
		private const string tagName = "tab";

		// Token: 0x04007237 RID: 29239
		private const byte tagNsId = 57;

		// Token: 0x04007238 RID: 29240
		internal const int ElementTypeIdConst = 13110;

		// Token: 0x04007239 RID: 29241
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "idMso", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "enabled", "getEnabled",
			"label", "getLabel", "visible", "getVisible", "keytip", "getKeytip", "title", "getTitle", "columnWidthPercent", "firstColumnMinWidth",
			"firstColumnMaxWidth", "secondColumnMinWidth", "secondColumnMaxWidth"
		};

		// Token: 0x0400723A RID: 29242
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400723B RID: 29243
		private static readonly string[] eleTagNames;

		// Token: 0x0400723C RID: 29244
		private static readonly byte[] eleNamespaceIds;
	}
}
