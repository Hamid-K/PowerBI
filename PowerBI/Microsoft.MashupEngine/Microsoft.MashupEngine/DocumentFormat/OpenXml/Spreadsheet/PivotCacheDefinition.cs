using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B19 RID: 11033
	[ChildElementInfo(typeof(Kpis))]
	[ChildElementInfo(typeof(CalculatedMembers))]
	[ChildElementInfo(typeof(CacheSource))]
	[ChildElementInfo(typeof(CacheFields))]
	[ChildElementInfo(typeof(CacheHierarchies))]
	[ChildElementInfo(typeof(MeasureGroups))]
	[ChildElementInfo(typeof(TupleCache))]
	[ChildElementInfo(typeof(CalculatedItems))]
	[ChildElementInfo(typeof(PivotCacheDefinitionExtensionList))]
	[ChildElementInfo(typeof(Dimensions))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Maps))]
	internal class PivotCacheDefinition : OpenXmlPartRootElement
	{
		// Token: 0x170075CF RID: 30159
		// (get) Token: 0x06016660 RID: 91744 RVA: 0x002A82F8 File Offset: 0x002A64F8
		public override string LocalName
		{
			get
			{
				return "pivotCacheDefinition";
			}
		}

		// Token: 0x170075D0 RID: 30160
		// (get) Token: 0x06016661 RID: 91745 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170075D1 RID: 30161
		// (get) Token: 0x06016662 RID: 91746 RVA: 0x00329977 File Offset: 0x00327B77
		internal override int ElementTypeId
		{
			get
			{
				return 11031;
			}
		}

		// Token: 0x06016663 RID: 91747 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170075D2 RID: 30162
		// (get) Token: 0x06016664 RID: 91748 RVA: 0x0032997E File Offset: 0x00327B7E
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotCacheDefinition.attributeTagNames;
			}
		}

		// Token: 0x170075D3 RID: 30163
		// (get) Token: 0x06016665 RID: 91749 RVA: 0x00329985 File Offset: 0x00327B85
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotCacheDefinition.attributeNamespaceIds;
			}
		}

		// Token: 0x170075D4 RID: 30164
		// (get) Token: 0x06016666 RID: 91750 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016667 RID: 91751 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
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

		// Token: 0x170075D5 RID: 30165
		// (get) Token: 0x06016668 RID: 91752 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06016669 RID: 91753 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "invalid")]
		public BooleanValue Invalid
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170075D6 RID: 30166
		// (get) Token: 0x0601666A RID: 91754 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601666B RID: 91755 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "saveData")]
		public BooleanValue SaveData
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

		// Token: 0x170075D7 RID: 30167
		// (get) Token: 0x0601666C RID: 91756 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0601666D RID: 91757 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "refreshOnLoad")]
		public BooleanValue RefreshOnLoad
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

		// Token: 0x170075D8 RID: 30168
		// (get) Token: 0x0601666E RID: 91758 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0601666F RID: 91759 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "optimizeMemory")]
		public BooleanValue OptimizeMemory
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170075D9 RID: 30169
		// (get) Token: 0x06016670 RID: 91760 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06016671 RID: 91761 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "enableRefresh")]
		public BooleanValue EnableRefresh
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

		// Token: 0x170075DA RID: 30170
		// (get) Token: 0x06016672 RID: 91762 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06016673 RID: 91763 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "refreshedBy")]
		public StringValue RefreshedBy
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

		// Token: 0x170075DB RID: 30171
		// (get) Token: 0x06016674 RID: 91764 RVA: 0x0032998C File Offset: 0x00327B8C
		// (set) Token: 0x06016675 RID: 91765 RVA: 0x002BD516 File Offset: 0x002BB716
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(0, "refreshedDateIso")]
		public DateTimeValue LastRefreshedDateIso
		{
			get
			{
				return (DateTimeValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170075DC RID: 30172
		// (get) Token: 0x06016676 RID: 91766 RVA: 0x0032999B File Offset: 0x00327B9B
		// (set) Token: 0x06016677 RID: 91767 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "refreshedDate")]
		public DoubleValue RefreshedDate
		{
			get
			{
				return (DoubleValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170075DD RID: 30173
		// (get) Token: 0x06016678 RID: 91768 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06016679 RID: 91769 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "backgroundQuery")]
		public BooleanValue BackgroundQuery
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

		// Token: 0x170075DE RID: 30174
		// (get) Token: 0x0601667A RID: 91770 RVA: 0x0031EC49 File Offset: 0x0031CE49
		// (set) Token: 0x0601667B RID: 91771 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "missingItemsLimit")]
		public UInt32Value MissingItemsLimit
		{
			get
			{
				return (UInt32Value)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x170075DF RID: 30175
		// (get) Token: 0x0601667C RID: 91772 RVA: 0x003299AA File Offset: 0x00327BAA
		// (set) Token: 0x0601667D RID: 91773 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "createdVersion")]
		public ByteValue CreatedVersion
		{
			get
			{
				return (ByteValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x170075E0 RID: 30176
		// (get) Token: 0x0601667E RID: 91774 RVA: 0x003299BA File Offset: 0x00327BBA
		// (set) Token: 0x0601667F RID: 91775 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "refreshedVersion")]
		public ByteValue RefreshedVersion
		{
			get
			{
				return (ByteValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x170075E1 RID: 30177
		// (get) Token: 0x06016680 RID: 91776 RVA: 0x003299CA File Offset: 0x00327BCA
		// (set) Token: 0x06016681 RID: 91777 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "minRefreshableVersion")]
		public ByteValue MinRefreshableVersion
		{
			get
			{
				return (ByteValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x170075E2 RID: 30178
		// (get) Token: 0x06016682 RID: 91778 RVA: 0x003299DA File Offset: 0x00327BDA
		// (set) Token: 0x06016683 RID: 91779 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "recordCount")]
		public UInt32Value RecordCount
		{
			get
			{
				return (UInt32Value)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x170075E3 RID: 30179
		// (get) Token: 0x06016684 RID: 91780 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x06016685 RID: 91781 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "upgradeOnRefresh")]
		public BooleanValue UpgradeOnRefresh
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

		// Token: 0x170075E4 RID: 30180
		// (get) Token: 0x06016686 RID: 91782 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x06016687 RID: 91783 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "tupleCache")]
		public BooleanValue IsTupleCache
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

		// Token: 0x170075E5 RID: 30181
		// (get) Token: 0x06016688 RID: 91784 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x06016689 RID: 91785 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "supportSubquery")]
		public BooleanValue SupportSubquery
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

		// Token: 0x170075E6 RID: 30182
		// (get) Token: 0x0601668A RID: 91786 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x0601668B RID: 91787 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "supportAdvancedDrill")]
		public BooleanValue SupportAdvancedDrill
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

		// Token: 0x0601668C RID: 91788 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal PivotCacheDefinition(PivotTableCacheDefinitionPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x0601668D RID: 91789 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(PivotTableCacheDefinitionPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170075E7 RID: 30183
		// (get) Token: 0x0601668E RID: 91790 RVA: 0x003299EA File Offset: 0x00327BEA
		// (set) Token: 0x0601668F RID: 91791 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public PivotTableCacheDefinitionPart PivotTableCacheDefinitionPart
		{
			get
			{
				return base.OpenXmlPart as PivotTableCacheDefinitionPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06016690 RID: 91792 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public PivotCacheDefinition(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016691 RID: 91793 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public PivotCacheDefinition(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016692 RID: 91794 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public PivotCacheDefinition(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016693 RID: 91795 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public PivotCacheDefinition()
		{
		}

		// Token: 0x06016694 RID: 91796 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(PivotTableCacheDefinitionPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06016695 RID: 91797 RVA: 0x003299F8 File Offset: 0x00327BF8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "cacheSource" == name)
			{
				return new CacheSource();
			}
			if (22 == namespaceId && "cacheFields" == name)
			{
				return new CacheFields();
			}
			if (22 == namespaceId && "cacheHierarchies" == name)
			{
				return new CacheHierarchies();
			}
			if (22 == namespaceId && "kpis" == name)
			{
				return new Kpis();
			}
			if (22 == namespaceId && "tupleCache" == name)
			{
				return new TupleCache();
			}
			if (22 == namespaceId && "calculatedItems" == name)
			{
				return new CalculatedItems();
			}
			if (22 == namespaceId && "calculatedMembers" == name)
			{
				return new CalculatedMembers();
			}
			if (22 == namespaceId && "dimensions" == name)
			{
				return new Dimensions();
			}
			if (22 == namespaceId && "measureGroups" == name)
			{
				return new MeasureGroups();
			}
			if (22 == namespaceId && "maps" == name)
			{
				return new Maps();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new PivotCacheDefinitionExtensionList();
			}
			return null;
		}

		// Token: 0x170075E8 RID: 30184
		// (get) Token: 0x06016696 RID: 91798 RVA: 0x00329B0E File Offset: 0x00327D0E
		internal override string[] ElementTagNames
		{
			get
			{
				return PivotCacheDefinition.eleTagNames;
			}
		}

		// Token: 0x170075E9 RID: 30185
		// (get) Token: 0x06016697 RID: 91799 RVA: 0x00329B15 File Offset: 0x00327D15
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PivotCacheDefinition.eleNamespaceIds;
			}
		}

		// Token: 0x170075EA RID: 30186
		// (get) Token: 0x06016698 RID: 91800 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170075EB RID: 30187
		// (get) Token: 0x06016699 RID: 91801 RVA: 0x00329B1C File Offset: 0x00327D1C
		// (set) Token: 0x0601669A RID: 91802 RVA: 0x00329B25 File Offset: 0x00327D25
		public CacheSource CacheSource
		{
			get
			{
				return base.GetElement<CacheSource>(0);
			}
			set
			{
				base.SetElement<CacheSource>(0, value);
			}
		}

		// Token: 0x170075EC RID: 30188
		// (get) Token: 0x0601669B RID: 91803 RVA: 0x00329B2F File Offset: 0x00327D2F
		// (set) Token: 0x0601669C RID: 91804 RVA: 0x00329B38 File Offset: 0x00327D38
		public CacheFields CacheFields
		{
			get
			{
				return base.GetElement<CacheFields>(1);
			}
			set
			{
				base.SetElement<CacheFields>(1, value);
			}
		}

		// Token: 0x170075ED RID: 30189
		// (get) Token: 0x0601669D RID: 91805 RVA: 0x00329B42 File Offset: 0x00327D42
		// (set) Token: 0x0601669E RID: 91806 RVA: 0x00329B4B File Offset: 0x00327D4B
		public CacheHierarchies CacheHierarchies
		{
			get
			{
				return base.GetElement<CacheHierarchies>(2);
			}
			set
			{
				base.SetElement<CacheHierarchies>(2, value);
			}
		}

		// Token: 0x170075EE RID: 30190
		// (get) Token: 0x0601669F RID: 91807 RVA: 0x00329B55 File Offset: 0x00327D55
		// (set) Token: 0x060166A0 RID: 91808 RVA: 0x00329B5E File Offset: 0x00327D5E
		public Kpis Kpis
		{
			get
			{
				return base.GetElement<Kpis>(3);
			}
			set
			{
				base.SetElement<Kpis>(3, value);
			}
		}

		// Token: 0x170075EF RID: 30191
		// (get) Token: 0x060166A1 RID: 91809 RVA: 0x00329B68 File Offset: 0x00327D68
		// (set) Token: 0x060166A2 RID: 91810 RVA: 0x00329B71 File Offset: 0x00327D71
		public TupleCache TupleCache
		{
			get
			{
				return base.GetElement<TupleCache>(4);
			}
			set
			{
				base.SetElement<TupleCache>(4, value);
			}
		}

		// Token: 0x170075F0 RID: 30192
		// (get) Token: 0x060166A3 RID: 91811 RVA: 0x00329B7B File Offset: 0x00327D7B
		// (set) Token: 0x060166A4 RID: 91812 RVA: 0x00329B84 File Offset: 0x00327D84
		public CalculatedItems CalculatedItems
		{
			get
			{
				return base.GetElement<CalculatedItems>(5);
			}
			set
			{
				base.SetElement<CalculatedItems>(5, value);
			}
		}

		// Token: 0x170075F1 RID: 30193
		// (get) Token: 0x060166A5 RID: 91813 RVA: 0x00329B8E File Offset: 0x00327D8E
		// (set) Token: 0x060166A6 RID: 91814 RVA: 0x00329B97 File Offset: 0x00327D97
		public CalculatedMembers CalculatedMembers
		{
			get
			{
				return base.GetElement<CalculatedMembers>(6);
			}
			set
			{
				base.SetElement<CalculatedMembers>(6, value);
			}
		}

		// Token: 0x170075F2 RID: 30194
		// (get) Token: 0x060166A7 RID: 91815 RVA: 0x00329BA1 File Offset: 0x00327DA1
		// (set) Token: 0x060166A8 RID: 91816 RVA: 0x00329BAA File Offset: 0x00327DAA
		public Dimensions Dimensions
		{
			get
			{
				return base.GetElement<Dimensions>(7);
			}
			set
			{
				base.SetElement<Dimensions>(7, value);
			}
		}

		// Token: 0x170075F3 RID: 30195
		// (get) Token: 0x060166A9 RID: 91817 RVA: 0x00329BB4 File Offset: 0x00327DB4
		// (set) Token: 0x060166AA RID: 91818 RVA: 0x00329BBD File Offset: 0x00327DBD
		public MeasureGroups MeasureGroups
		{
			get
			{
				return base.GetElement<MeasureGroups>(8);
			}
			set
			{
				base.SetElement<MeasureGroups>(8, value);
			}
		}

		// Token: 0x170075F4 RID: 30196
		// (get) Token: 0x060166AB RID: 91819 RVA: 0x00329BC7 File Offset: 0x00327DC7
		// (set) Token: 0x060166AC RID: 91820 RVA: 0x00329BD1 File Offset: 0x00327DD1
		public Maps Maps
		{
			get
			{
				return base.GetElement<Maps>(9);
			}
			set
			{
				base.SetElement<Maps>(9, value);
			}
		}

		// Token: 0x170075F5 RID: 30197
		// (get) Token: 0x060166AD RID: 91821 RVA: 0x00329BDC File Offset: 0x00327DDC
		// (set) Token: 0x060166AE RID: 91822 RVA: 0x00329BE6 File Offset: 0x00327DE6
		public PivotCacheDefinitionExtensionList PivotCacheDefinitionExtensionList
		{
			get
			{
				return base.GetElement<PivotCacheDefinitionExtensionList>(10);
			}
			set
			{
				base.SetElement<PivotCacheDefinitionExtensionList>(10, value);
			}
		}

		// Token: 0x060166AF RID: 91823 RVA: 0x00329BF4 File Offset: 0x00327DF4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "invalid" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "saveData" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "refreshOnLoad" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "optimizeMemory" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "enableRefresh" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "refreshedBy" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "refreshedDateIso" == name)
			{
				return new DateTimeValue();
			}
			if (namespaceId == 0 && "refreshedDate" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "backgroundQuery" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "missingItemsLimit" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "createdVersion" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "refreshedVersion" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "minRefreshableVersion" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "recordCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "upgradeOnRefresh" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "tupleCache" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "supportSubquery" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "supportAdvancedDrill" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060166B0 RID: 91824 RVA: 0x00329DAD File Offset: 0x00327FAD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotCacheDefinition>(deep);
		}

		// Token: 0x060166B1 RID: 91825 RVA: 0x00329DB8 File Offset: 0x00327FB8
		// Note: this type is marked as 'beforefieldinit'.
		static PivotCacheDefinition()
		{
			byte[] array = new byte[19];
			array[0] = 19;
			PivotCacheDefinition.attributeNamespaceIds = array;
			PivotCacheDefinition.eleTagNames = new string[]
			{
				"cacheSource", "cacheFields", "cacheHierarchies", "kpis", "tupleCache", "calculatedItems", "calculatedMembers", "dimensions", "measureGroups", "maps",
				"extLst"
			};
			PivotCacheDefinition.eleNamespaceIds = new byte[]
			{
				22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
				22
			};
		}

		// Token: 0x040098DA RID: 39130
		private const string tagName = "pivotCacheDefinition";

		// Token: 0x040098DB RID: 39131
		private const byte tagNsId = 22;

		// Token: 0x040098DC RID: 39132
		internal const int ElementTypeIdConst = 11031;

		// Token: 0x040098DD RID: 39133
		private static string[] attributeTagNames = new string[]
		{
			"id", "invalid", "saveData", "refreshOnLoad", "optimizeMemory", "enableRefresh", "refreshedBy", "refreshedDateIso", "refreshedDate", "backgroundQuery",
			"missingItemsLimit", "createdVersion", "refreshedVersion", "minRefreshableVersion", "recordCount", "upgradeOnRefresh", "tupleCache", "supportSubquery", "supportAdvancedDrill"
		};

		// Token: 0x040098DE RID: 39134
		private static byte[] attributeNamespaceIds;

		// Token: 0x040098DF RID: 39135
		private static readonly string[] eleTagNames;

		// Token: 0x040098E0 RID: 39136
		private static readonly byte[] eleNamespaceIds;
	}
}
