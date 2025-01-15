using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200040F RID: 1039
	public sealed class DataSetInstance : ScopeInstance
	{
		// Token: 0x06002CAB RID: 11435 RVA: 0x000CDA9E File Offset: 0x000CBC9E
		internal DataSetInstance(DataSet dataSetDef)
		{
			this.m_dataSetDef = dataSetDef;
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x000CDABF File Offset: 0x000CBCBF
		internal DataSetInstance()
		{
		}

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x000CDAD9 File Offset: 0x000CBCD9
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSetInstance;
			}
		}

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x000CDAE0 File Offset: 0x000CBCE0
		// (set) Token: 0x06002CAF RID: 11439 RVA: 0x000CDAE8 File Offset: 0x000CBCE8
		internal DataSet DataSetDef
		{
			get
			{
				return this.m_dataSetDef;
			}
			set
			{
				this.m_dataSetDef = value;
			}
		}

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x06002CB0 RID: 11440 RVA: 0x000CDAF1 File Offset: 0x000CBCF1
		// (set) Token: 0x06002CB1 RID: 11441 RVA: 0x000CDAF9 File Offset: 0x000CBCF9
		internal int RecordSetSize
		{
			get
			{
				return this.m_recordSetSize;
			}
			set
			{
				this.m_recordSetSize = value;
			}
		}

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x06002CB2 RID: 11442 RVA: 0x000CDB02 File Offset: 0x000CBD02
		internal bool NoRows
		{
			get
			{
				return this.m_recordSetSize <= 0;
			}
		}

		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x000CDB10 File Offset: 0x000CBD10
		// (set) Token: 0x06002CB4 RID: 11444 RVA: 0x000CDB18 File Offset: 0x000CBD18
		internal FieldInfo[] FieldInfos
		{
			get
			{
				return this.m_fieldInfos;
			}
			set
			{
				this.m_fieldInfos = value;
			}
		}

		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x000CDB21 File Offset: 0x000CBD21
		// (set) Token: 0x06002CB6 RID: 11446 RVA: 0x000CDB29 File Offset: 0x000CBD29
		internal string RewrittenCommandText
		{
			get
			{
				return this.m_rewrittenCommandText;
			}
			set
			{
				this.m_rewrittenCommandText = value;
			}
		}

		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x000CDB32 File Offset: 0x000CBD32
		// (set) Token: 0x06002CB8 RID: 11448 RVA: 0x000CDB3A File Offset: 0x000CBD3A
		internal string CommandText
		{
			get
			{
				return this.m_commandText;
			}
			set
			{
				this.m_commandText = value;
			}
		}

		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x000CDB43 File Offset: 0x000CBD43
		// (set) Token: 0x06002CBA RID: 11450 RVA: 0x000CDB4B File Offset: 0x000CBD4B
		internal DataSet.TriState CaseSensitivity
		{
			get
			{
				return this.m_caseSensitivity;
			}
			set
			{
				this.m_caseSensitivity = value;
			}
		}

		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x06002CBB RID: 11451 RVA: 0x000CDB54 File Offset: 0x000CBD54
		// (set) Token: 0x06002CBC RID: 11452 RVA: 0x000CDB5C File Offset: 0x000CBD5C
		internal DataSet.TriState AccentSensitivity
		{
			get
			{
				return this.m_accentSensitivity;
			}
			set
			{
				this.m_accentSensitivity = value;
			}
		}

		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x000CDB65 File Offset: 0x000CBD65
		// (set) Token: 0x06002CBE RID: 11454 RVA: 0x000CDB6D File Offset: 0x000CBD6D
		internal DataSet.TriState KanatypeSensitivity
		{
			get
			{
				return this.m_kanatypeSensitivity;
			}
			set
			{
				this.m_kanatypeSensitivity = value;
			}
		}

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x000CDB76 File Offset: 0x000CBD76
		// (set) Token: 0x06002CC0 RID: 11456 RVA: 0x000CDB7E File Offset: 0x000CBD7E
		internal DataSet.TriState WidthSensitivity
		{
			get
			{
				return this.m_widthSensitivity;
			}
			set
			{
				this.m_widthSensitivity = value;
			}
		}

		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x000CDB87 File Offset: 0x000CBD87
		// (set) Token: 0x06002CC2 RID: 11458 RVA: 0x000CDB8F File Offset: 0x000CBD8F
		internal uint LCID
		{
			get
			{
				return this.m_lcid;
			}
			set
			{
				this.m_lcid = value;
			}
		}

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x06002CC3 RID: 11459 RVA: 0x000CDB98 File Offset: 0x000CBD98
		// (set) Token: 0x06002CC4 RID: 11460 RVA: 0x000CDBA0 File Offset: 0x000CBDA0
		internal List<LookupObjResult> LookupResults
		{
			get
			{
				return this.m_lookupResults;
			}
			set
			{
				this.m_lookupResults = value;
			}
		}

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x000CDBA9 File Offset: 0x000CBDA9
		// (set) Token: 0x06002CC6 RID: 11462 RVA: 0x000CDBB1 File Offset: 0x000CBDB1
		internal bool OldSnapshotTablixProcessingComplete
		{
			get
			{
				return this.m_oldSnapshotTablixProcessingComplete;
			}
			set
			{
				this.m_oldSnapshotTablixProcessingComplete = value;
			}
		}

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x000CDBBA File Offset: 0x000CBDBA
		// (set) Token: 0x06002CC8 RID: 11464 RVA: 0x000CDBC2 File Offset: 0x000CBDC2
		internal string DataChunkName
		{
			get
			{
				return this.m_dataChunkName;
			}
			set
			{
				this.m_dataChunkName = value;
			}
		}

		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x000CDBCB File Offset: 0x000CBDCB
		internal CompareInfo CompareInfo
		{
			get
			{
				if (this.m_cachedCompareInfo == null)
				{
					this.CreateCompareInfo();
				}
				return this.m_cachedCompareInfo;
			}
		}

		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x000CDBE1 File Offset: 0x000CBDE1
		internal CompareOptions ClrCompareOptions
		{
			get
			{
				if (this.m_cachedCompareInfo == null)
				{
					this.CreateCompareInfo();
				}
				return this.m_cachedCompareOptions;
			}
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x000CDBF7 File Offset: 0x000CBDF7
		internal void InitializeForReprocessing()
		{
			this.m_oldSnapshotTablixProcessingComplete = false;
			this.m_aggregateValues = null;
			this.m_lookupResults = null;
			this.m_firstRowOffset = -1L;
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000CDC16 File Offset: 0x000CBE16
		internal override void AddChildScope(IReference<ScopeInstance> child, int indexInCollection)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000CDC24 File Offset: 0x000CBE24
		internal void SetupEnvironment(OnDemandProcessingContext odpContext, bool newDataSetDefinition)
		{
			if (newDataSetDefinition)
			{
				odpContext.SetupFieldsForNewDataSet(this.m_dataSetDef, this, false, this.NoRows);
			}
			if (!this.NoRows)
			{
				if (this.m_firstRowOffset == DataFieldRow.UnInitializedStreamOffset)
				{
					odpContext.ReportObjectModel.CreateNoRows();
					return;
				}
				Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.DataChunkReader dataChunkReader = odpContext.GetDataChunkReader(this.m_dataSetDef.IndexInCollection);
				dataChunkReader.ReadOneRowAtPosition(this.m_firstRowOffset);
				odpContext.ReportObjectModel.FieldsImpl.NewRow(this.m_firstRowOffset);
				odpContext.ReportObjectModel.UpdateFieldValues(!newDataSetDefinition, dataChunkReader.RecordRow, this, dataChunkReader.ReaderExtensionsSupported);
			}
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x000CDCBC File Offset: 0x000CBEBC
		internal void SetupDataSetLevelAggregates(OnDemandProcessingContext odpContext)
		{
			int num = 0;
			base.SetupAggregates<DataAggregateInfo>(odpContext, this.m_dataSetDef.Aggregates, ref num);
			base.SetupAggregates<DataAggregateInfo>(odpContext, this.m_dataSetDef.PostSortAggregates, ref num);
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x000CDCF3 File Offset: 0x000CBEF3
		internal void SetupCollationSettings(OnDemandProcessingContext odpContext)
		{
			odpContext.SetComparisonInformation(this.CompareInfo, this.ClrCompareOptions, this.m_dataSetDef.NullsAsBlanks, this.m_dataSetDef.UseOrdinalStringKeyGeneration);
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x000CDD1D File Offset: 0x000CBF1D
		internal void SaveCollationSettings(DataSet dataSet)
		{
			this.LCID = dataSet.LCID;
			this.CaseSensitivity = dataSet.CaseSensitivity;
			this.WidthSensitivity = dataSet.WidthSensitivity;
			this.AccentSensitivity = dataSet.AccentSensitivity;
			this.KanatypeSensitivity = dataSet.KanatypeSensitivity;
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000CDD5C File Offset: 0x000CBF5C
		private void CreateCompareInfo()
		{
			if (this.m_dataSetDef.NeedAutoDetectCollation())
			{
				this.m_dataSetDef.LCID = this.m_lcid;
				this.m_dataSetDef.CaseSensitivity = this.m_caseSensitivity;
				this.m_dataSetDef.AccentSensitivity = this.m_accentSensitivity;
				this.m_dataSetDef.KanatypeSensitivity = this.m_kanatypeSensitivity;
				this.m_dataSetDef.WidthSensitivity = this.m_widthSensitivity;
			}
			this.m_cachedCompareInfo = this.m_dataSetDef.CreateCultureInfoFromLcid().CompareInfo;
			this.m_cachedCompareOptions = this.m_dataSetDef.GetCLRCompareOptions();
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000CDDF4 File Offset: 0x000CBFF4
		internal IDataComparer CreateProcessingComparer(OnDemandProcessingContext odpContext)
		{
			if (odpContext.ContextMode == OnDemandProcessingContext.Mode.Streaming)
			{
				return new CommonDataComparer(this.CompareInfo, this.ClrCompareOptions, this.m_dataSetDef.NullsAsBlanks);
			}
			return new ReportProcessing.ProcessingComparer(this.CompareInfo, this.ClrCompareOptions, this.m_dataSetDef.NullsAsBlanks);
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000CDE43 File Offset: 0x000CC043
		internal DateTime GetQueryExecutionTime(DateTime reportExecutionTime)
		{
			if (!(this.m_executionTime == DateTime.MinValue))
			{
				return this.m_executionTime;
			}
			return reportExecutionTime;
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000CDE5F File Offset: 0x000CC05F
		internal void SetQueryExecutionTime(DateTime queryExecutionTime)
		{
			this.m_executionTime = queryExecutionTime;
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000CDE68 File Offset: 0x000CC068
		internal FieldInfo GetOrCreateFieldInfo(int aIndex)
		{
			if (this.m_fieldInfos == null)
			{
				this.m_fieldInfos = new FieldInfo[this.m_dataSetDef.NonCalculatedFieldCount];
			}
			if (this.m_fieldInfos[aIndex] == null)
			{
				this.m_fieldInfos[aIndex] = new FieldInfo();
			}
			return this.m_fieldInfos[aIndex];
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000CDEA7 File Offset: 0x000CC0A7
		internal bool IsFieldMissing(int index)
		{
			return this.m_fieldInfos != null && this.m_fieldInfos[index] != null && this.m_fieldInfos[index].Missing;
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000CDECA File Offset: 0x000CC0CA
		internal int GetFieldPropertyCount(int index)
		{
			if (this.m_fieldInfos == null || this.m_fieldInfos[index] == null)
			{
				return 0;
			}
			return this.m_fieldInfos[index].PropertyCount;
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000CDEF0 File Offset: 0x000CC0F0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSetInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeInstance, new List<MemberInfo>
			{
				new MemberInfo(MemberName.RecordSetSize, Token.Int32),
				new MemberInfo(MemberName.CommandText, Token.String),
				new MemberInfo(MemberName.RewrittenCommandText, Token.String),
				new MemberInfo(MemberName.Fields, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FieldInfo),
				new MemberInfo(MemberName.CaseSensitivity, Token.Enum),
				new MemberInfo(MemberName.AccentSensitivity, Token.Enum),
				new MemberInfo(MemberName.KanatypeSensitivity, Token.Enum),
				new MemberInfo(MemberName.WidthSensitivity, Token.Enum),
				new MemberInfo(MemberName.LCID, Token.UInt32),
				new ReadOnlyMemberInfo(MemberName.TablixProcessingComplete, Token.Boolean),
				new MemberInfo(MemberName.DataChunkName, Token.String),
				new MemberInfo(MemberName.LookupResults, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupObjResult),
				new MemberInfo(MemberName.ExecutionTime, Token.DateTime)
			});
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000CDFFC File Offset: 0x000CC1FC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(DataSetInstance.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.CommandText)
				{
					switch (memberName)
					{
					case MemberName.RecordSetSize:
						writer.Write(this.m_recordSetSize);
						continue;
					case MemberName.RewrittenCommandText:
						writer.Write(this.m_rewrittenCommandText);
						continue;
					case MemberName.Fields:
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] fieldInfos = this.m_fieldInfos;
						writer.Write(fieldInfos);
						continue;
					}
					case MemberName.CaseSensitivity:
						writer.WriteEnum((int)this.m_caseSensitivity);
						continue;
					case MemberName.Collation:
						break;
					case MemberName.AccentSensitivity:
						writer.WriteEnum((int)this.m_accentSensitivity);
						continue;
					case MemberName.KanatypeSensitivity:
						writer.WriteEnum((int)this.m_kanatypeSensitivity);
						continue;
					case MemberName.WidthSensitivity:
						writer.WriteEnum((int)this.m_widthSensitivity);
						continue;
					case MemberName.LCID:
						writer.Write(this.m_lcid);
						continue;
					default:
						if (memberName == MemberName.CommandText)
						{
							writer.Write(this.m_commandText);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.ExecutionTime)
					{
						writer.Write(this.m_executionTime);
						continue;
					}
					if (memberName == MemberName.DataChunkName)
					{
						writer.Write(this.m_dataChunkName);
						continue;
					}
					if (memberName == MemberName.LookupResults)
					{
						writer.Write<LookupObjResult>(this.m_lookupResults);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000CE170 File Offset: 0x000CC370
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(DataSetInstance.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.CommandText)
				{
					switch (memberName)
					{
					case MemberName.RecordSetSize:
						this.m_recordSetSize = reader.ReadInt32();
						continue;
					case MemberName.RewrittenCommandText:
						this.m_rewrittenCommandText = reader.ReadString();
						continue;
					case MemberName.Fields:
						this.m_fieldInfos = reader.ReadArrayOfRIFObjects<FieldInfo>();
						continue;
					case MemberName.CaseSensitivity:
						this.m_caseSensitivity = (DataSet.TriState)reader.ReadEnum();
						continue;
					case MemberName.Collation:
						break;
					case MemberName.AccentSensitivity:
						this.m_accentSensitivity = (DataSet.TriState)reader.ReadEnum();
						continue;
					case MemberName.KanatypeSensitivity:
						this.m_kanatypeSensitivity = (DataSet.TriState)reader.ReadEnum();
						continue;
					case MemberName.WidthSensitivity:
						this.m_widthSensitivity = (DataSet.TriState)reader.ReadEnum();
						continue;
					case MemberName.LCID:
						this.m_lcid = reader.ReadUInt32();
						continue;
					case MemberName.TablixProcessingComplete:
						this.m_oldSnapshotTablixProcessingComplete = reader.ReadBoolean();
						continue;
					default:
						if (memberName == MemberName.CommandText)
						{
							this.m_commandText = reader.ReadString();
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.ExecutionTime)
					{
						this.m_executionTime = reader.ReadDateTime();
						continue;
					}
					if (memberName == MemberName.DataChunkName)
					{
						this.m_dataChunkName = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.LookupResults)
					{
						this.m_lookupResults = reader.ReadListOfRIFObjects<List<LookupObjResult>>();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000CE2F7 File Offset: 0x000CC4F7
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000CE304 File Offset: 0x000CC504
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSetInstance;
		}

		// Token: 0x040017F9 RID: 6137
		private int m_recordSetSize = -1;

		// Token: 0x040017FA RID: 6138
		private string m_rewrittenCommandText;

		// Token: 0x040017FB RID: 6139
		private string m_commandText;

		// Token: 0x040017FC RID: 6140
		private DateTime m_executionTime = DateTime.MinValue;

		// Token: 0x040017FD RID: 6141
		private FieldInfo[] m_fieldInfos;

		// Token: 0x040017FE RID: 6142
		private uint m_lcid;

		// Token: 0x040017FF RID: 6143
		private DataSet.TriState m_caseSensitivity;

		// Token: 0x04001800 RID: 6144
		private DataSet.TriState m_accentSensitivity;

		// Token: 0x04001801 RID: 6145
		private DataSet.TriState m_kanatypeSensitivity;

		// Token: 0x04001802 RID: 6146
		private DataSet.TriState m_widthSensitivity;

		// Token: 0x04001803 RID: 6147
		[NonSerialized]
		private bool m_oldSnapshotTablixProcessingComplete;

		// Token: 0x04001804 RID: 6148
		private string m_dataChunkName;

		// Token: 0x04001805 RID: 6149
		private List<LookupObjResult> m_lookupResults;

		// Token: 0x04001806 RID: 6150
		[NonSerialized]
		private CompareInfo m_cachedCompareInfo;

		// Token: 0x04001807 RID: 6151
		[NonSerialized]
		private CompareOptions m_cachedCompareOptions;

		// Token: 0x04001808 RID: 6152
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DataSetInstance.GetDeclaration();

		// Token: 0x04001809 RID: 6153
		[NonSerialized]
		private DataSet m_dataSetDef;
	}
}
