using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B87 RID: 11143
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FieldsUsage))]
	[ChildElementInfo(typeof(GroupLevels))]
	[ChildElementInfo(typeof(CacheHierarchyExtensionList))]
	internal class CacheHierarchy : OpenXmlCompositeElement
	{
		// Token: 0x17007A7B RID: 31355
		// (get) Token: 0x060170CB RID: 94411 RVA: 0x002E5F7E File Offset: 0x002E417E
		public override string LocalName
		{
			get
			{
				return "cacheHierarchy";
			}
		}

		// Token: 0x17007A7C RID: 31356
		// (get) Token: 0x060170CC RID: 94412 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007A7D RID: 31357
		// (get) Token: 0x060170CD RID: 94413 RVA: 0x003321E3 File Offset: 0x003303E3
		internal override int ElementTypeId
		{
			get
			{
				return 11121;
			}
		}

		// Token: 0x060170CE RID: 94414 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007A7E RID: 31358
		// (get) Token: 0x060170CF RID: 94415 RVA: 0x003321EA File Offset: 0x003303EA
		internal override string[] AttributeTagNames
		{
			get
			{
				return CacheHierarchy.attributeTagNames;
			}
		}

		// Token: 0x17007A7F RID: 31359
		// (get) Token: 0x060170D0 RID: 94416 RVA: 0x003321F1 File Offset: 0x003303F1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CacheHierarchy.attributeNamespaceIds;
			}
		}

		// Token: 0x17007A80 RID: 31360
		// (get) Token: 0x060170D1 RID: 94417 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060170D2 RID: 94418 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uniqueName")]
		public StringValue UniqueName
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

		// Token: 0x17007A81 RID: 31361
		// (get) Token: 0x060170D3 RID: 94419 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060170D4 RID: 94420 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "caption")]
		public StringValue Caption
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

		// Token: 0x17007A82 RID: 31362
		// (get) Token: 0x060170D5 RID: 94421 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060170D6 RID: 94422 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "measure")]
		public BooleanValue Measure
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

		// Token: 0x17007A83 RID: 31363
		// (get) Token: 0x060170D7 RID: 94423 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060170D8 RID: 94424 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "set")]
		public BooleanValue Set
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007A84 RID: 31364
		// (get) Token: 0x060170D9 RID: 94425 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x060170DA RID: 94426 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "parentSet")]
		public UInt32Value ParentSet
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007A85 RID: 31365
		// (get) Token: 0x060170DB RID: 94427 RVA: 0x002ED371 File Offset: 0x002EB571
		// (set) Token: 0x060170DC RID: 94428 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "iconSet")]
		public Int32Value IconSet
		{
			get
			{
				return (Int32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007A86 RID: 31366
		// (get) Token: 0x060170DD RID: 94429 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060170DE RID: 94430 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "attribute")]
		public BooleanValue Attribute
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007A87 RID: 31367
		// (get) Token: 0x060170DF RID: 94431 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x060170E0 RID: 94432 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "time")]
		public BooleanValue Time
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

		// Token: 0x17007A88 RID: 31368
		// (get) Token: 0x060170E1 RID: 94433 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x060170E2 RID: 94434 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "keyAttribute")]
		public BooleanValue KeyAttribute
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

		// Token: 0x17007A89 RID: 31369
		// (get) Token: 0x060170E3 RID: 94435 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x060170E4 RID: 94436 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "defaultMemberUniqueName")]
		public StringValue DefaultMemberUniqueName
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

		// Token: 0x17007A8A RID: 31370
		// (get) Token: 0x060170E5 RID: 94437 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x060170E6 RID: 94438 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "allUniqueName")]
		public StringValue AllUniqueName
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

		// Token: 0x17007A8B RID: 31371
		// (get) Token: 0x060170E7 RID: 94439 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x060170E8 RID: 94440 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "allCaption")]
		public StringValue AllCaption
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

		// Token: 0x17007A8C RID: 31372
		// (get) Token: 0x060170E9 RID: 94441 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x060170EA RID: 94442 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "dimensionUniqueName")]
		public StringValue DimensionUniqueName
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

		// Token: 0x17007A8D RID: 31373
		// (get) Token: 0x060170EB RID: 94443 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x060170EC RID: 94444 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "displayFolder")]
		public StringValue DisplayFolder
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

		// Token: 0x17007A8E RID: 31374
		// (get) Token: 0x060170ED RID: 94445 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x060170EE RID: 94446 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "measureGroup")]
		public StringValue MeasureGroup
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

		// Token: 0x17007A8F RID: 31375
		// (get) Token: 0x060170EF RID: 94447 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x060170F0 RID: 94448 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "measures")]
		public BooleanValue Measures
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

		// Token: 0x17007A90 RID: 31376
		// (get) Token: 0x060170F1 RID: 94449 RVA: 0x002E6F1A File Offset: 0x002E511A
		// (set) Token: 0x060170F2 RID: 94450 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17007A91 RID: 31377
		// (get) Token: 0x060170F3 RID: 94451 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x060170F4 RID: 94452 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "oneField")]
		public BooleanValue OneField
		{
			get
			{
				return (BooleanValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17007A92 RID: 31378
		// (get) Token: 0x060170F5 RID: 94453 RVA: 0x003321F8 File Offset: 0x003303F8
		// (set) Token: 0x060170F6 RID: 94454 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "memberValueDatatype")]
		public UInt16Value MemberValueDatatype
		{
			get
			{
				return (UInt16Value)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17007A93 RID: 31379
		// (get) Token: 0x060170F7 RID: 94455 RVA: 0x002D6080 File Offset: 0x002D4280
		// (set) Token: 0x060170F8 RID: 94456 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "unbalanced")]
		public BooleanValue Unbalanced
		{
			get
			{
				return (BooleanValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x17007A94 RID: 31380
		// (get) Token: 0x060170F9 RID: 94457 RVA: 0x002C8F75 File Offset: 0x002C7175
		// (set) Token: 0x060170FA RID: 94458 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "unbalancedGroup")]
		public BooleanValue UnbalancedGroup
		{
			get
			{
				return (BooleanValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x17007A95 RID: 31381
		// (get) Token: 0x060170FB RID: 94459 RVA: 0x002DB1B1 File Offset: 0x002D93B1
		// (set) Token: 0x060170FC RID: 94460 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "hidden")]
		public BooleanValue Hidden
		{
			get
			{
				return (BooleanValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x060170FD RID: 94461 RVA: 0x00293ECF File Offset: 0x002920CF
		public CacheHierarchy()
		{
		}

		// Token: 0x060170FE RID: 94462 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CacheHierarchy(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060170FF RID: 94463 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CacheHierarchy(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017100 RID: 94464 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CacheHierarchy(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017101 RID: 94465 RVA: 0x00332208 File Offset: 0x00330408
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "fieldsUsage" == name)
			{
				return new FieldsUsage();
			}
			if (22 == namespaceId && "groupLevels" == name)
			{
				return new GroupLevels();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new CacheHierarchyExtensionList();
			}
			return null;
		}

		// Token: 0x17007A96 RID: 31382
		// (get) Token: 0x06017102 RID: 94466 RVA: 0x0033225E File Offset: 0x0033045E
		internal override string[] ElementTagNames
		{
			get
			{
				return CacheHierarchy.eleTagNames;
			}
		}

		// Token: 0x17007A97 RID: 31383
		// (get) Token: 0x06017103 RID: 94467 RVA: 0x00332265 File Offset: 0x00330465
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CacheHierarchy.eleNamespaceIds;
			}
		}

		// Token: 0x17007A98 RID: 31384
		// (get) Token: 0x06017104 RID: 94468 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007A99 RID: 31385
		// (get) Token: 0x06017105 RID: 94469 RVA: 0x0033226C File Offset: 0x0033046C
		// (set) Token: 0x06017106 RID: 94470 RVA: 0x00332275 File Offset: 0x00330475
		public FieldsUsage FieldsUsage
		{
			get
			{
				return base.GetElement<FieldsUsage>(0);
			}
			set
			{
				base.SetElement<FieldsUsage>(0, value);
			}
		}

		// Token: 0x17007A9A RID: 31386
		// (get) Token: 0x06017107 RID: 94471 RVA: 0x0033227F File Offset: 0x0033047F
		// (set) Token: 0x06017108 RID: 94472 RVA: 0x00332288 File Offset: 0x00330488
		public GroupLevels GroupLevels
		{
			get
			{
				return base.GetElement<GroupLevels>(1);
			}
			set
			{
				base.SetElement<GroupLevels>(1, value);
			}
		}

		// Token: 0x17007A9B RID: 31387
		// (get) Token: 0x06017109 RID: 94473 RVA: 0x00332292 File Offset: 0x00330492
		// (set) Token: 0x0601710A RID: 94474 RVA: 0x0033229B File Offset: 0x0033049B
		public CacheHierarchyExtensionList CacheHierarchyExtensionList
		{
			get
			{
				return base.GetElement<CacheHierarchyExtensionList>(2);
			}
			set
			{
				base.SetElement<CacheHierarchyExtensionList>(2, value);
			}
		}

		// Token: 0x0601710B RID: 94475 RVA: 0x003322A8 File Offset: 0x003304A8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uniqueName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "caption" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "measure" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "set" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "parentSet" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "iconSet" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "attribute" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "time" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "keyAttribute" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "defaultMemberUniqueName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "allUniqueName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "allCaption" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "dimensionUniqueName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "displayFolder" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "measureGroup" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "measures" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "oneField" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "memberValueDatatype" == name)
			{
				return new UInt16Value();
			}
			if (namespaceId == 0 && "unbalanced" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "unbalancedGroup" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "hidden" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601710C RID: 94476 RVA: 0x003324A1 File Offset: 0x003306A1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CacheHierarchy>(deep);
		}

		// Token: 0x0601710D RID: 94477 RVA: 0x003324AC File Offset: 0x003306AC
		// Note: this type is marked as 'beforefieldinit'.
		static CacheHierarchy()
		{
			byte[] array = new byte[22];
			CacheHierarchy.attributeNamespaceIds = array;
			CacheHierarchy.eleTagNames = new string[] { "fieldsUsage", "groupLevels", "extLst" };
			CacheHierarchy.eleNamespaceIds = new byte[] { 22, 22, 22 };
		}

		// Token: 0x04009AE6 RID: 39654
		private const string tagName = "cacheHierarchy";

		// Token: 0x04009AE7 RID: 39655
		private const byte tagNsId = 22;

		// Token: 0x04009AE8 RID: 39656
		internal const int ElementTypeIdConst = 11121;

		// Token: 0x04009AE9 RID: 39657
		private static string[] attributeTagNames = new string[]
		{
			"uniqueName", "caption", "measure", "set", "parentSet", "iconSet", "attribute", "time", "keyAttribute", "defaultMemberUniqueName",
			"allUniqueName", "allCaption", "dimensionUniqueName", "displayFolder", "measureGroup", "measures", "count", "oneField", "memberValueDatatype", "unbalanced",
			"unbalancedGroup", "hidden"
		};

		// Token: 0x04009AEA RID: 39658
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009AEB RID: 39659
		private static readonly string[] eleTagNames;

		// Token: 0x04009AEC RID: 39660
		private static readonly byte[] eleNamespaceIds;
	}
}
