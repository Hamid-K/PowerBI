using System;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200007E RID: 126
	public sealed class CubeDef : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x060007D1 RID: 2001 RVA: 0x00025C08 File Offset: 0x00023E08
		internal CubeDef(DataRow cubeRow, AdomdConnection connection, DateTime populationTime, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, cubeRow, null, null, catalog, sessionId);
			this.populationTime = populationTime;
			this.metadataCache = new CubeMetadataCache(connection, this);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00025C44 File Offset: 0x00023E44
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00025C4C File Offset: 0x00023E4C
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.CubeRow, CubeDef.cubeNameColumn).ToString();
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00025C63 File Offset: 0x00023E63
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.CubeRow, CubeDef.descriptionColumn).ToString();
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00025C7A File Offset: 0x00023E7A
		public DateTime LastUpdated
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.CubeRow, CubeDef.lastSchemaUpdateColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00025C96 File Offset: 0x00023E96
		public DateTime LastProcessed
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.CubeRow, CubeDef.lastDataUpdateColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00025CB2 File Offset: 0x00023EB2
		public string Caption
		{
			get
			{
				if (this.CubeRow.Table.Columns.Contains(CubeDef.cubeCaption))
				{
					return AdomdUtils.GetProperty(this.CubeRow, CubeDef.cubeCaption).ToString();
				}
				return this.Name;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00025CEC File Offset: 0x00023EEC
		public CubeType Type
		{
			get
			{
				if (!this.CubeRow.Table.Columns.Contains("CUBE_SOURCE"))
				{
					return CubeType.Cube;
				}
				int num = Convert.ToInt32(AdomdUtils.GetProperty(this.CubeRow, "CUBE_SOURCE"), CultureInfo.InvariantCulture);
				if (num == 1)
				{
					return CubeType.Cube;
				}
				if (num == 2)
				{
					return CubeType.Dimension;
				}
				return CubeType.Unknown;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00025D3F File Offset: 0x00023F3F
		public AdomdConnection ParentConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00025D47 File Offset: 0x00023F47
		public DimensionCollection Dimensions
		{
			get
			{
				if (this.dimensions == null)
				{
					this.dimensions = new DimensionCollection(this.Connection, this);
				}
				else
				{
					this.dimensions.CollectionInternal.CheckCache();
				}
				return this.dimensions;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00025D7C File Offset: 0x00023F7C
		public KpiCollection Kpis
		{
			get
			{
				if (!this.Connection.IsPostYukonProvider())
				{
					throw new NotSupportedException(SR.NotSupportedByProvider);
				}
				if (this.kpis == null)
				{
					this.kpis = new KpiCollection(this.Connection, this);
				}
				else
				{
					this.kpis.CollectionInternal.CheckCache();
				}
				return this.kpis;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00025DD3 File Offset: 0x00023FD3
		public NamedSetCollection NamedSets
		{
			get
			{
				if (this.namedSets == null)
				{
					this.namedSets = new NamedSetCollection(this.Connection, this);
				}
				else
				{
					this.namedSets.CollectionInternal.Refresh();
				}
				return this.namedSets;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00025E07 File Offset: 0x00024007
		public MeasureCollection Measures
		{
			get
			{
				if (this.measures == null)
				{
					this.measures = new MeasureCollection(this.Connection, this);
				}
				else
				{
					this.measures.CollectionInternal.Refresh();
				}
				return this.measures;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00025E3B File Offset: 0x0002403B
		public PropertyCollection Properties
		{
			get
			{
				if (this.propertyCollection == null)
				{
					this.propertyCollection = new PropertyCollection(this.CubeRow, this);
				}
				return this.propertyCollection;
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00025E5D File Offset: 0x0002405D
		public object GetSchemaObject(SchemaObjectType schemaObjectType, string uniqueName)
		{
			return this.GetSchemaObject(schemaObjectType, uniqueName, true);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00025E68 File Offset: 0x00024068
		public object GetSchemaObject(SchemaObjectType schemaObjectType, string uniqueName, bool retryUniqueNameOnServer)
		{
			if (uniqueName == null)
			{
				throw new ArgumentNullException("uniqueName");
			}
			if (uniqueName.Length == 0)
			{
				throw new ArgumentException(SR.ArgumentErrorUniqueNameEmpty, "uniqueName");
			}
			if ((schemaObjectType < SchemaObjectType.ObjectTypeDimension || schemaObjectType > SchemaObjectType.ObjectTypeMiningServiceParameter) && schemaObjectType != SchemaObjectType.ObjectTypeNamedSet)
			{
				throw new ArgumentException(SR.ArgumentErrorInvalidSchemaObjectType, "schemaObjectType");
			}
			return this.InternalGetSchemaObject(schemaObjectType, uniqueName, retryUniqueNameOnServer);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00025EC2 File Offset: 0x000240C2
		internal object InternalGetSchemaObject(SchemaObjectType schemaObjectType, string uniqueName)
		{
			return this.InternalGetSchemaObject(schemaObjectType, uniqueName, false);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00025ED0 File Offset: 0x000240D0
		internal object InternalGetSchemaObject(SchemaObjectType schemaObjectType, string uniqueName, bool retryUniqueName)
		{
			DataRow dataRow;
			if (SchemaObjectType.ObjectTypeMember == schemaObjectType)
			{
				ListDictionary listDictionary = new ListDictionary();
				listDictionary.Add(CubeCollectionInternal.cubeNameRest, this.Name);
				AdomdUtils.AddCubeSourceRestrictionIfApplicable(this.Connection, listDictionary);
				string text = "MDSCHEMA_MEMBERS";
				string text2 = "MEMBER_UNIQUE_NAME";
				listDictionary.Add(text2, uniqueName);
				AdomdUtils.AddMemberBinaryRestrictionIfApplicable(this.Connection, listDictionary);
				DataRowCollection rows = AdomdUtils.GetRows(this.Connection, text, listDictionary);
				if (rows.Count != 1)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(uniqueName), "uniqueName");
				}
				dataRow = rows[0];
			}
			else
			{
				dataRow = this.metadataCache.FindObjectByUniqueName(schemaObjectType, uniqueName);
				if (dataRow == null && retryUniqueName)
				{
					ListDictionary listDictionary2 = new ListDictionary();
					listDictionary2.Add(CubeCollectionInternal.cubeNameRest, this.Name);
					AdomdUtils.AddCubeSourceRestrictionIfApplicable(this.Connection, listDictionary2);
					string text3;
					string text4;
					switch (schemaObjectType)
					{
					case SchemaObjectType.ObjectTypeDimension:
						text3 = DimensionCollectionInternal.schemaName;
						text4 = DimensionCollectionInternal.dimUNameRest;
						goto IL_0166;
					case SchemaObjectType.ObjectTypeHierarchy:
						text3 = HierarchyCollectionInternal.schemaName;
						text4 = HierarchyCollectionInternal.hierUNameRest;
						goto IL_0166;
					case SchemaObjectType.ObjectTypeLevel:
						text3 = LevelCollectionInternal.schemaName;
						text4 = LevelCollectionInternal.levelUNameRest;
						goto IL_0166;
					case SchemaObjectType.ObjectTypeMember:
					case (SchemaObjectType)5:
						break;
					case SchemaObjectType.ObjectTypeMeasure:
						text3 = MeasureCollectionInternal.schemaName;
						text4 = Measure.uniqueNameColumn;
						goto IL_0166;
					case SchemaObjectType.ObjectTypeKpi:
						text3 = KpiCollectionInternal.schemaName;
						text4 = Kpi.kpiNameColumn;
						goto IL_0166;
					default:
						if (schemaObjectType == SchemaObjectType.ObjectTypeNamedSet)
						{
							text3 = NamedSetCollectionInternal.schemaName;
							text4 = "SET_NAME";
							goto IL_0166;
						}
						break;
					}
					throw new ArgumentOutOfRangeException("schemaObjectType");
					IL_0166:
					listDictionary2.Add(text4, uniqueName);
					AdomdUtils.AddObjectVisibilityRestrictionIfApplicable(this.Connection, text3, listDictionary2);
					DataRowCollection rows2 = AdomdUtils.GetRows(this.Connection, text3, listDictionary2);
					if (rows2.Count > 0)
					{
						uniqueName = rows2[0][text4] as string;
						if (uniqueName != null)
						{
							dataRow = this.metadataCache.FindObjectByUniqueName(schemaObjectType, uniqueName);
						}
					}
				}
			}
			if (dataRow == null)
			{
				throw new ArgumentException(SR.Indexer_ObjectNotFound(uniqueName), "uniqueName");
			}
			switch (schemaObjectType)
			{
			case SchemaObjectType.ObjectTypeDimension:
				return DimensionCollectionInternal.GetDimensionByRow(this.Connection, dataRow, this, this.baseData.Catalog, this.baseData.SessionID);
			case SchemaObjectType.ObjectTypeHierarchy:
			{
				Dimension dimension = (Dimension)this.InternalGetSchemaObject(SchemaObjectType.ObjectTypeDimension, dataRow[Dimension.uniqueNameColumn].ToString());
				return HierarchyCollectionInternal.GetHiearchyByRow(this.Connection, dataRow, dimension, this.baseData.Catalog, this.baseData.SessionID);
			}
			case SchemaObjectType.ObjectTypeLevel:
			{
				string text5 = dataRow[Hierarchy.uniqueNameColumn].ToString();
				Hierarchy hierarchy = (Hierarchy)this.InternalGetSchemaObject(SchemaObjectType.ObjectTypeHierarchy, text5);
				return LevelCollectionInternal.GetLevelByRow(this.Connection, dataRow, hierarchy, this.baseData.Catalog, this.baseData.SessionID);
			}
			case SchemaObjectType.ObjectTypeMember:
				return new Member(this.Connection, dataRow, null, null, MemberOrigin.Metadata, this.Name, null, -1, this.baseData.Catalog, this.baseData.SessionID);
			case (SchemaObjectType)5:
				break;
			case SchemaObjectType.ObjectTypeMeasure:
				return MeasureCollectionInternal.GetMeasureByRow(this.Connection, dataRow, this, this.baseData.Catalog, this.baseData.SessionID);
			case SchemaObjectType.ObjectTypeKpi:
				return KpiCollectionInternal.GetKpiByRow(this.Connection, dataRow, this, this.baseData.Catalog, this.baseData.SessionID);
			default:
				if (schemaObjectType == SchemaObjectType.ObjectTypeNamedSet)
				{
					return NamedSetCollectionInternal.GetNamedSetByRow(this.Connection, dataRow, this, this.baseData.Catalog, this.baseData.SessionID);
				}
				break;
			}
			throw new ArgumentOutOfRangeException("schemaObjectType");
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0002624E File Offset: 0x0002444E
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0002625B File Offset: 0x0002445B
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x00026268 File Offset: 0x00024468
		bool IAdomdBaseObject.IsMetadata
		{
			get
			{
				return this.baseData.IsMetadata;
			}
			set
			{
				this.baseData.IsMetadata = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00026276 File Offset: 0x00024476
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x00026283 File Offset: 0x00024483
		object IAdomdBaseObject.MetadataData
		{
			get
			{
				return this.baseData.MetadataData;
			}
			set
			{
				this.baseData.MetadataData = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00026291 File Offset: 0x00024491
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x00026294 File Offset: 0x00024494
		IAdomdBaseObject IAdomdBaseObject.ParentObject
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00026296 File Offset: 0x00024496
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0002629E File Offset: 0x0002449E
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeDimension;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x000262A1 File Offset: 0x000244A1
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x000262A9 File Offset: 0x000244A9
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x000262B1 File Offset: 0x000244B1
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x000262BE File Offset: 0x000244BE
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x000262CB File Offset: 0x000244CB
		string IMetadataObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x000262D3 File Offset: 0x000244D3
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x000262DB File Offset: 0x000244DB
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(CubeDef);
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000262E7 File Offset: 0x000244E7
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0002630A File Offset: 0x0002450A
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00026318 File Offset: 0x00024518
		public static bool operator ==(CubeDef o1, CubeDef o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00026321 File Offset: 0x00024521
		public static bool operator !=(CubeDef o1, CubeDef o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0002632D File Offset: 0x0002452D
		internal DataRow CubeRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0002633F File Offset: 0x0002453F
		internal DateTime PopulatedTime
		{
			get
			{
				return this.populationTime;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00026347 File Offset: 0x00024547
		private AdomdConnection Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00026354 File Offset: 0x00024554
		internal MemberCollection GetMembers(string memberSet, string[] properties, Level parentLevel, Member parentMember)
		{
			string dimensionPropertiesClause = MemberQueryGenerator.GetDimensionPropertiesClause(properties);
			string memberQuery = MemberQueryGenerator.GetMemberQuery(memberSet, dimensionPropertiesClause, this);
			return this.ExecuteMembersQuery(memberQuery, parentLevel, parentMember);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0002637B File Offset: 0x0002457B
		private MemberCollection ExecuteMembersQuery(string memberMdxQuery, Level parentLevel, Member parentMember)
		{
			return this.ExecuteMembersQuery(memberMdxQuery, parentLevel, parentMember, 0, 0);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00026388 File Offset: 0x00024588
		private MemberCollection ExecuteMembersQuery(string memberMdxQuery, Level parentLevel, Member parentMember, int memberAxisPosition, int memberHierarhcyPosition)
		{
			if (memberAxisPosition < 0)
			{
				throw new ArgumentOutOfRangeException("memberAxisPosition");
			}
			if (memberHierarhcyPosition < 0)
			{
				throw new ArgumentOutOfRangeException("memberHierarhcyPosition");
			}
			AdomdConnection connection = this.Connection;
			if (connection == null)
			{
				throw new NotSupportedException(SR.NotSupportedWhenConnectionMissing);
			}
			AdomdUtils.CheckConnectionOpened(connection);
			CellSet cellSet = new AdomdCommand(memberMdxQuery, connection).ExecuteCellSet();
			if (memberAxisPosition >= cellSet.Axes.Count)
			{
				throw new ArgumentOutOfRangeException("memberAxisPosition");
			}
			IDSFDataSet axisDataset = cellSet.Axes[memberAxisPosition].Set.AxisDataset;
			DataTable dataTable = null;
			if (memberHierarhcyPosition != 0 || axisDataset.Count != 0)
			{
				if (memberHierarhcyPosition >= axisDataset.Count)
				{
					throw new ArgumentOutOfRangeException("memberHierarhcyPosition");
				}
				dataTable = axisDataset[memberHierarhcyPosition];
			}
			return new MemberCollection(connection, dataTable, this.Name, parentLevel, parentMember);
		}

		// Token: 0x0400055A RID: 1370
		private BaseObjectData baseData;

		// Token: 0x0400055B RID: 1371
		private DimensionCollection dimensions;

		// Token: 0x0400055C RID: 1372
		private KpiCollection kpis;

		// Token: 0x0400055D RID: 1373
		private MeasureCollection measures;

		// Token: 0x0400055E RID: 1374
		private NamedSetCollection namedSets;

		// Token: 0x0400055F RID: 1375
		private PropertyCollection propertyCollection;

		// Token: 0x04000560 RID: 1376
		private DateTime populationTime;

		// Token: 0x04000561 RID: 1377
		internal IMetadataCache metadataCache;

		// Token: 0x04000562 RID: 1378
		private int hashCode;

		// Token: 0x04000563 RID: 1379
		private bool hashCodeCalculated;

		// Token: 0x04000564 RID: 1380
		internal static string cubeNameColumn = "CUBE_NAME";

		// Token: 0x04000565 RID: 1381
		internal static string cubeNameRest = CubeDef.cubeNameColumn;

		// Token: 0x04000566 RID: 1382
		internal static string cubeCaption = "CUBE_CAPTION";

		// Token: 0x04000567 RID: 1383
		internal static string descriptionColumn = "DESCRIPTION";

		// Token: 0x04000568 RID: 1384
		internal static string lastDataUpdateColumn = "LAST_DATA_UPDATE";

		// Token: 0x04000569 RID: 1385
		internal static string lastSchemaUpdateColumn = "LAST_SCHEMA_UPDATE";

		// Token: 0x0400056A RID: 1386
		private const string cubeSourceColumn = "CUBE_SOURCE";

		// Token: 0x0400056B RID: 1387
		private const int cubeSourceCube = 1;

		// Token: 0x0400056C RID: 1388
		private const int cubeSourceDimension = 2;
	}
}
