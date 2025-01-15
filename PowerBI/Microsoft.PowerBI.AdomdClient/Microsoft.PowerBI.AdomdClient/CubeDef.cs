using System;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200007E RID: 126
	public sealed class CubeDef : IAdomdBaseObject, IMetadataObject
	{
		// Token: 0x060007C4 RID: 1988 RVA: 0x000258D8 File Offset: 0x00023AD8
		internal CubeDef(DataRow cubeRow, AdomdConnection connection, DateTime populationTime, string catalog, string sessionId)
		{
			this.baseData = new BaseObjectData(connection, true, null, cubeRow, null, null, catalog, sessionId);
			this.populationTime = populationTime;
			this.metadataCache = new CubeMetadataCache(connection, this);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00025914 File Offset: 0x00023B14
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0002591C File Offset: 0x00023B1C
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.CubeRow, CubeDef.cubeNameColumn).ToString();
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00025933 File Offset: 0x00023B33
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.CubeRow, CubeDef.descriptionColumn).ToString();
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0002594A File Offset: 0x00023B4A
		public DateTime LastUpdated
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.CubeRow, CubeDef.lastSchemaUpdateColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00025966 File Offset: 0x00023B66
		public DateTime LastProcessed
		{
			get
			{
				return Convert.ToDateTime(AdomdUtils.GetProperty(this.CubeRow, CubeDef.lastDataUpdateColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00025982 File Offset: 0x00023B82
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

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x000259BC File Offset: 0x00023BBC
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

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00025A0F File Offset: 0x00023C0F
		public AdomdConnection ParentConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00025A17 File Offset: 0x00023C17
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

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00025A4C File Offset: 0x00023C4C
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

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00025AA3 File Offset: 0x00023CA3
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

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00025AD7 File Offset: 0x00023CD7
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

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00025B0B File Offset: 0x00023D0B
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

		// Token: 0x060007D2 RID: 2002 RVA: 0x00025B2D File Offset: 0x00023D2D
		public object GetSchemaObject(SchemaObjectType schemaObjectType, string uniqueName)
		{
			return this.GetSchemaObject(schemaObjectType, uniqueName, true);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00025B38 File Offset: 0x00023D38
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

		// Token: 0x060007D4 RID: 2004 RVA: 0x00025B92 File Offset: 0x00023D92
		internal object InternalGetSchemaObject(SchemaObjectType schemaObjectType, string uniqueName)
		{
			return this.InternalGetSchemaObject(schemaObjectType, uniqueName, false);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00025BA0 File Offset: 0x00023DA0
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

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00025F1E File Offset: 0x0002411E
		AdomdConnection IAdomdBaseObject.Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00025F2B File Offset: 0x0002412B
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x00025F38 File Offset: 0x00024138
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

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00025F46 File Offset: 0x00024146
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x00025F53 File Offset: 0x00024153
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

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00025F61 File Offset: 0x00024161
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x00025F64 File Offset: 0x00024164
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

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00025F66 File Offset: 0x00024166
		string IAdomdBaseObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00025F6E File Offset: 0x0002416E
		SchemaObjectType IAdomdBaseObject.SchemaObjectType
		{
			get
			{
				return SchemaObjectType.ObjectTypeDimension;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00025F71 File Offset: 0x00024171
		string IAdomdBaseObject.InternalUniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00025F79 File Offset: 0x00024179
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00025F81 File Offset: 0x00024181
		string IMetadataObject.Catalog
		{
			get
			{
				return this.baseData.Catalog;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00025F8E File Offset: 0x0002418E
		string IMetadataObject.SessionId
		{
			get
			{
				return this.baseData.SessionID;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00025F9B File Offset: 0x0002419B
		string IMetadataObject.CubeName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00025FA3 File Offset: 0x000241A3
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00025FAB File Offset: 0x000241AB
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(CubeDef);
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00025FB7 File Offset: 0x000241B7
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00025FDA File Offset: 0x000241DA
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00025FE8 File Offset: 0x000241E8
		public static bool operator ==(CubeDef o1, CubeDef o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00025FF1 File Offset: 0x000241F1
		public static bool operator !=(CubeDef o1, CubeDef o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00025FFD File Offset: 0x000241FD
		internal DataRow CubeRow
		{
			get
			{
				return (DataRow)this.baseData.MetadataData;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0002600F File Offset: 0x0002420F
		internal DateTime PopulatedTime
		{
			get
			{
				return this.populationTime;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x00026017 File Offset: 0x00024217
		private AdomdConnection Connection
		{
			get
			{
				return this.baseData.Connection;
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00026024 File Offset: 0x00024224
		internal MemberCollection GetMembers(string memberSet, string[] properties, Level parentLevel, Member parentMember)
		{
			string dimensionPropertiesClause = MemberQueryGenerator.GetDimensionPropertiesClause(properties);
			string memberQuery = MemberQueryGenerator.GetMemberQuery(memberSet, dimensionPropertiesClause, this);
			return this.ExecuteMembersQuery(memberQuery, parentLevel, parentMember);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0002604B File Offset: 0x0002424B
		private MemberCollection ExecuteMembersQuery(string memberMdxQuery, Level parentLevel, Member parentMember)
		{
			return this.ExecuteMembersQuery(memberMdxQuery, parentLevel, parentMember, 0, 0);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00026058 File Offset: 0x00024258
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

		// Token: 0x0400054D RID: 1357
		private BaseObjectData baseData;

		// Token: 0x0400054E RID: 1358
		private DimensionCollection dimensions;

		// Token: 0x0400054F RID: 1359
		private KpiCollection kpis;

		// Token: 0x04000550 RID: 1360
		private MeasureCollection measures;

		// Token: 0x04000551 RID: 1361
		private NamedSetCollection namedSets;

		// Token: 0x04000552 RID: 1362
		private PropertyCollection propertyCollection;

		// Token: 0x04000553 RID: 1363
		private DateTime populationTime;

		// Token: 0x04000554 RID: 1364
		internal IMetadataCache metadataCache;

		// Token: 0x04000555 RID: 1365
		private int hashCode;

		// Token: 0x04000556 RID: 1366
		private bool hashCodeCalculated;

		// Token: 0x04000557 RID: 1367
		internal static string cubeNameColumn = "CUBE_NAME";

		// Token: 0x04000558 RID: 1368
		internal static string cubeNameRest = CubeDef.cubeNameColumn;

		// Token: 0x04000559 RID: 1369
		internal static string cubeCaption = "CUBE_CAPTION";

		// Token: 0x0400055A RID: 1370
		internal static string descriptionColumn = "DESCRIPTION";

		// Token: 0x0400055B RID: 1371
		internal static string lastDataUpdateColumn = "LAST_DATA_UPDATE";

		// Token: 0x0400055C RID: 1372
		internal static string lastSchemaUpdateColumn = "LAST_SCHEMA_UPDATE";

		// Token: 0x0400055D RID: 1373
		private const string cubeSourceColumn = "CUBE_SOURCE";

		// Token: 0x0400055E RID: 1374
		private const int cubeSourceCube = 1;

		// Token: 0x0400055F RID: 1375
		private const int cubeSourceDimension = 2;
	}
}
