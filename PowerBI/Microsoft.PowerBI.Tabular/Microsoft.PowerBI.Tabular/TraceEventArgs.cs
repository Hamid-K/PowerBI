using System;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000DF RID: 223
	[Guid("9324A406-7388-49a1-BA4A-8A6D4D9DD98C")]
	public sealed class TraceEventArgs : EventArgs
	{
		// Token: 0x170003A2 RID: 930
		public string this[TraceColumn column]
		{
			get
			{
				return this.data[(int)column];
			}
			set
			{
				this.data[(int)column] = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x000708AF File Offset: 0x0006EAAF
		public XmlaMessageCollection XmlaMessages
		{
			get
			{
				return this.xmlaMessages;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x000708B8 File Offset: 0x0006EAB8
		public TraceEventClass EventClass
		{
			get
			{
				string text = this.data[0];
				if (text == null)
				{
					return TraceEventClass.NotAvailable;
				}
				return (TraceEventClass)Enum.Parse(typeof(TraceEventClass), text);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x000708E8 File Offset: 0x0006EAE8
		public TraceEventSubclass EventSubclass
		{
			get
			{
				string text = this.data[0];
				string text2 = this.data[1];
				if (text == null || text2 == null)
				{
					return TraceEventSubclass.NotAvailable;
				}
				return this.GetEventSubclass(XmlConvert.ToInt32(text), XmlConvert.ToInt32(text2));
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00070924 File Offset: 0x0006EB24
		public DateTime CurrentTime
		{
			get
			{
				return XmlConvert.ToDateTime(this[TraceColumn.CurrentTime], XmlDateTimeSerializationMode.Utc).ToLocalTime();
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x00070948 File Offset: 0x0006EB48
		public DateTime StartTime
		{
			get
			{
				return XmlConvert.ToDateTime(this[TraceColumn.StartTime], XmlDateTimeSerializationMode.Utc).ToLocalTime();
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0007096C File Offset: 0x0006EB6C
		public DateTime EndTime
		{
			get
			{
				return XmlConvert.ToDateTime(this[TraceColumn.EndTime], XmlDateTimeSerializationMode.Utc).ToLocalTime();
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x0007098E File Offset: 0x0006EB8E
		public long Duration
		{
			get
			{
				return XmlConvert.ToInt64(this[TraceColumn.Duration]);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0007099C File Offset: 0x0006EB9C
		public long CpuTime
		{
			get
			{
				return XmlConvert.ToInt64(this[TraceColumn.CpuTime]);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x000709AA File Offset: 0x0006EBAA
		public string JobID
		{
			get
			{
				return this.data[7];
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x000709B4 File Offset: 0x0006EBB4
		public string SessionType
		{
			get
			{
				return this.data[8];
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x000709BE File Offset: 0x0006EBBE
		public long ProgressTotal
		{
			get
			{
				return XmlConvert.ToInt64(this[TraceColumn.ProgressTotal]);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x000709CD File Offset: 0x0006EBCD
		public long IntegerData
		{
			get
			{
				return XmlConvert.ToInt64(this[TraceColumn.IntegerData]);
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x000709DC File Offset: 0x0006EBDC
		public string ObjectID
		{
			get
			{
				return this.data[11];
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x000709E8 File Offset: 0x0006EBE8
		public Type ObjectType
		{
			get
			{
				string text = this.data[12];
				if (text == null)
				{
					return null;
				}
				int num = XmlConvert.ToInt32(text);
				if (num <= 100051)
				{
					if (num <= 100002)
					{
						if (num == 100000)
						{
							return typeof(Server);
						}
						if (num == 100002)
						{
							return typeof(Database);
						}
					}
					else
					{
						if (num == 100005)
						{
							return typeof(Role);
						}
						switch (num)
						{
						case 100044:
							return typeof(Assembly);
						case 100045:
							return typeof(Assembly);
						case 100046:
							return typeof(Role);
						default:
							if (num == 100051)
							{
								return typeof(Trace);
							}
							break;
						}
					}
				}
				else if (num <= 100145)
				{
					if (num == 100101)
					{
						return typeof(DatabaseCollection);
					}
					if (num == 100104)
					{
						return typeof(RoleCollection);
					}
					switch (num)
					{
					case 100143:
						return typeof(AssemblyCollection);
					case 100144:
						return typeof(AssemblyCollection);
					case 100145:
						return typeof(RoleCollection);
					}
				}
				else
				{
					if (num == 100150)
					{
						return typeof(TraceCollection);
					}
					if (num == 801002)
					{
						return typeof(Database);
					}
					switch (num)
					{
					case 802010:
						return typeof(Model);
					case 802011:
						return typeof(DataSource);
					case 802012:
						return typeof(Table);
					case 802013:
						return typeof(Column);
					case 802014:
						return typeof(AttributeHierarchy);
					case 802015:
						return typeof(Partition);
					case 802016:
						return typeof(Relationship);
					case 802017:
						return typeof(Measure);
					case 802018:
						return typeof(Hierarchy);
					case 802019:
						return typeof(Level);
					case 802020:
						return typeof(Annotation);
					case 802021:
						return typeof(KPI);
					case 802022:
						return typeof(Culture);
					case 802023:
						return typeof(ObjectTranslation);
					case 802024:
						return typeof(LinguisticMetadata);
					case 802038:
						return typeof(Perspective);
					case 802039:
						return typeof(PerspectiveTable);
					case 802040:
						return typeof(PerspectiveColumn);
					case 802041:
						return typeof(PerspectiveHierarchy);
					case 802042:
						return typeof(PerspectiveMeasure);
					case 802043:
						return typeof(ModelRole);
					case 802044:
						return typeof(ModelRoleMember);
					case 802045:
						return typeof(TablePermission);
					case 802046:
						return typeof(Variation);
					case 802051:
						return typeof(ColumnPermission);
					case 802052:
						return typeof(DetailRowsDefinition);
					}
				}
				return null;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00070D30 File Offset: 0x0006EF30
		public string ObjectName
		{
			get
			{
				return this.data[13];
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x00070D3B File Offset: 0x0006EF3B
		public string ObjectPath
		{
			get
			{
				return this.data[14];
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00070D46 File Offset: 0x0006EF46
		public string ObjectReference
		{
			get
			{
				return this[TraceColumn.ObjectReference];
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00070D50 File Offset: 0x0006EF50
		public string SessionID
		{
			get
			{
				return this.data[39];
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00070D5B File Offset: 0x0006EF5B
		public int Severity
		{
			get
			{
				return XmlConvert.ToInt32(this[TraceColumn.Severity]);
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00070D6C File Offset: 0x0006EF6C
		public TraceEventSuccess Success
		{
			get
			{
				string text = Utils.Trim(this.data[23]);
				if (text == null)
				{
					return TraceEventSuccess.NotAvailable;
				}
				if (text == "0")
				{
					return TraceEventSuccess.Failure;
				}
				if (!(text == "1"))
				{
					return TraceEventSuccess.Unknown;
				}
				return TraceEventSuccess.Success;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00070DAE File Offset: 0x0006EFAE
		public string Error
		{
			get
			{
				return this.data[24];
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00070DB9 File Offset: 0x0006EFB9
		public string ConnectionID
		{
			get
			{
				return this.data[25];
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00070DC4 File Offset: 0x0006EFC4
		public string DatabaseName
		{
			get
			{
				return this.data[28];
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x00070DCF File Offset: 0x0006EFCF
		public string NTUserName
		{
			get
			{
				return this.data[32];
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00070DDA File Offset: 0x0006EFDA
		public string NTDomainName
		{
			get
			{
				return this.data[33];
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00070DE5 File Offset: 0x0006EFE5
		public string ClientHostName
		{
			get
			{
				return this.data[35];
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00070DF0 File Offset: 0x0006EFF0
		public string ClientProcessID
		{
			get
			{
				return this.data[36];
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00070DFB File Offset: 0x0006EFFB
		public string ApplicationName
		{
			get
			{
				return this.data[37];
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00070E06 File Offset: 0x0006F006
		public string NTCanonicalUserName
		{
			get
			{
				return this.data[40];
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00070E11 File Offset: 0x0006F011
		public string Spid
		{
			get
			{
				return this.data[41];
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00070E1C File Offset: 0x0006F01C
		public string TextData
		{
			get
			{
				return this.data[42];
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00070E27 File Offset: 0x0006F027
		public string ServerName
		{
			get
			{
				return this.data[43];
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00070E32 File Offset: 0x0006F032
		public string RequestParameters
		{
			get
			{
				return this.data[44];
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00070E3D File Offset: 0x0006F03D
		public string RequestProperties
		{
			get
			{
				return this.data[45];
			}
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00070E48 File Offset: 0x0006F048
		private TraceEventSubclass GetEventSubclass(int eventClass, int eventSubclass)
		{
			if (eventClass <= 86)
			{
				if (eventClass <= 39)
				{
					switch (eventClass)
					{
					case 4:
						switch (eventSubclass)
						{
						case 1:
							return TraceEventSubclass.InstanceShutdown;
						case 2:
							return TraceEventSubclass.InstanceStarted;
						case 3:
							return TraceEventSubclass.InstancePaused;
						case 4:
							return TraceEventSubclass.InstanceContinued;
						}
						break;
					case 5:
						switch (eventSubclass)
						{
						case 1:
							return TraceEventSubclass.Process;
						case 2:
							return TraceEventSubclass.Merge;
						case 3:
							return TraceEventSubclass.Delete;
						case 4:
							return TraceEventSubclass.DeleteOldAggregations;
						case 5:
							return TraceEventSubclass.Rebuild;
						case 6:
							return TraceEventSubclass.Commit;
						case 7:
							return TraceEventSubclass.Rollback;
						case 8:
							return TraceEventSubclass.CreateIndexes;
						case 9:
							return TraceEventSubclass.CreateTable;
						case 10:
							return TraceEventSubclass.InsertInto;
						case 11:
							return TraceEventSubclass.Transaction;
						case 12:
							return TraceEventSubclass.Initialize;
						case 13:
							return TraceEventSubclass.Discretize;
						case 14:
							return TraceEventSubclass.Query;
						case 15:
							return TraceEventSubclass.CreateView;
						case 16:
							return TraceEventSubclass.WriteData;
						case 17:
							return TraceEventSubclass.ReadData;
						case 18:
							return TraceEventSubclass.GroupData;
						case 19:
							return TraceEventSubclass.GroupDataRecord;
						case 20:
							return TraceEventSubclass.BuildIndex;
						case 21:
							return TraceEventSubclass.Aggregate;
						case 22:
							return TraceEventSubclass.BuildDecode;
						case 23:
							return TraceEventSubclass.WriteDecode;
						case 24:
							return TraceEventSubclass.BuildDataMiningDecode;
						case 25:
							return TraceEventSubclass.ExecuteSql;
						case 26:
							return TraceEventSubclass.ExecuteModifiedSql;
						case 27:
							return TraceEventSubclass.Connecting;
						case 28:
							return TraceEventSubclass.BuildAggregationsAndIndexes;
						case 29:
							return TraceEventSubclass.MergeAggregationsOnDisk;
						case 30:
							return TraceEventSubclass.BuildIndexForRigidAggregations;
						case 31:
							return TraceEventSubclass.BuildIndexForFlexibleAggregations;
						case 32:
							return TraceEventSubclass.WriteAggregationsAndIndexes;
						case 33:
							return TraceEventSubclass.WriteSegment;
						case 34:
							return TraceEventSubclass.DataMiningProgress;
						case 35:
							return TraceEventSubclass.ReadBufferFullReport;
						case 36:
							return TraceEventSubclass.ProactiveCacheConversion;
						case 37:
							return TraceEventSubclass.Backup;
						case 38:
							return TraceEventSubclass.Restore;
						case 39:
							return TraceEventSubclass.Synchronize;
						case 40:
							return TraceEventSubclass.BuildProcessingSchedule;
						case 41:
							return TraceEventSubclass.Detach;
						case 42:
							return TraceEventSubclass.Attach;
						case 43:
							return TraceEventSubclass.AnalyzeEncodeData;
						case 44:
							return TraceEventSubclass.CompressSegment;
						case 45:
							return TraceEventSubclass.WriteTableColumn;
						case 46:
							return TraceEventSubclass.RelationshipBuildPrepare;
						case 47:
							return TraceEventSubclass.BuildRelationshipSegment;
						case 48:
							return TraceEventSubclass.Load;
						case 49:
							return TraceEventSubclass.MetadataLoad;
						case 50:
							return TraceEventSubclass.DataLoad;
						case 51:
							return TraceEventSubclass.PostLoad;
						case 52:
							return TraceEventSubclass.MetadataTraversalDuringBackup;
						case 53:
							return TraceEventSubclass.VertiPaq;
						case 54:
							return TraceEventSubclass.HierarchyProcessing;
						case 55:
							return TraceEventSubclass.SwitchingDictionary;
						case 57:
							return TraceEventSubclass.TabularCommit;
						case 58:
							return TraceEventSubclass.TabularSequencePoint;
						case 59:
							return TraceEventSubclass.TabularRefresh;
						case 60:
							return TraceEventSubclass.TabularSave;
						case 61:
							return TraceEventSubclass.TokenizationStoreProcessing;
						case 63:
							return TraceEventSubclass.Dbcc;
						case 64:
							return TraceEventSubclass.CheckTabularDataStructure;
						case 65:
							return TraceEventSubclass.CheckColumnDataForDuplicatesOrNullValues;
						case 66:
							return TraceEventSubclass.AnalyzeRefreshPolicyImpactForTabularPartition;
						case 67:
							return TraceEventSubclass.ParallelSession;
						case 68:
							return TraceEventSubclass.AutoAggsTraining;
						case 69:
							return TraceEventSubclass.AutoAggsCardinalityAnalysis;
						case 70:
							return TraceEventSubclass.Export;
						}
						break;
					case 6:
						switch (eventSubclass)
						{
						case 1:
							return TraceEventSubclass.Process;
						case 2:
							return TraceEventSubclass.Merge;
						case 3:
							return TraceEventSubclass.Delete;
						case 4:
							return TraceEventSubclass.DeleteOldAggregations;
						case 5:
							return TraceEventSubclass.Rebuild;
						case 6:
							return TraceEventSubclass.Commit;
						case 7:
							return TraceEventSubclass.Rollback;
						case 8:
							return TraceEventSubclass.CreateIndexes;
						case 9:
							return TraceEventSubclass.CreateTable;
						case 10:
							return TraceEventSubclass.InsertInto;
						case 11:
							return TraceEventSubclass.Transaction;
						case 12:
							return TraceEventSubclass.Initialize;
						case 13:
							return TraceEventSubclass.Discretize;
						case 14:
							return TraceEventSubclass.Query;
						case 15:
							return TraceEventSubclass.CreateView;
						case 16:
							return TraceEventSubclass.WriteData;
						case 17:
							return TraceEventSubclass.ReadData;
						case 18:
							return TraceEventSubclass.GroupData;
						case 19:
							return TraceEventSubclass.GroupDataRecord;
						case 20:
							return TraceEventSubclass.BuildIndex;
						case 21:
							return TraceEventSubclass.Aggregate;
						case 22:
							return TraceEventSubclass.BuildDecode;
						case 23:
							return TraceEventSubclass.WriteDecode;
						case 24:
							return TraceEventSubclass.BuildDataMiningDecode;
						case 25:
							return TraceEventSubclass.ExecuteSql;
						case 26:
							return TraceEventSubclass.ExecuteModifiedSql;
						case 27:
							return TraceEventSubclass.Connecting;
						case 28:
							return TraceEventSubclass.BuildAggregationsAndIndexes;
						case 29:
							return TraceEventSubclass.MergeAggregationsOnDisk;
						case 30:
							return TraceEventSubclass.BuildIndexForRigidAggregations;
						case 31:
							return TraceEventSubclass.BuildIndexForFlexibleAggregations;
						case 32:
							return TraceEventSubclass.WriteAggregationsAndIndexes;
						case 33:
							return TraceEventSubclass.WriteSegment;
						case 34:
							return TraceEventSubclass.DataMiningProgress;
						case 35:
							return TraceEventSubclass.ReadBufferFullReport;
						case 36:
							return TraceEventSubclass.ProactiveCacheConversion;
						case 37:
							return TraceEventSubclass.Backup;
						case 38:
							return TraceEventSubclass.Restore;
						case 39:
							return TraceEventSubclass.Synchronize;
						case 40:
							return TraceEventSubclass.BuildProcessingSchedule;
						case 41:
							return TraceEventSubclass.Detach;
						case 42:
							return TraceEventSubclass.Attach;
						case 43:
							return TraceEventSubclass.AnalyzeEncodeData;
						case 44:
							return TraceEventSubclass.CompressSegment;
						case 45:
							return TraceEventSubclass.WriteTableColumn;
						case 46:
							return TraceEventSubclass.RelationshipBuildPrepare;
						case 47:
							return TraceEventSubclass.BuildRelationshipSegment;
						case 48:
							return TraceEventSubclass.Load;
						case 49:
							return TraceEventSubclass.MetadataLoad;
						case 50:
							return TraceEventSubclass.DataLoad;
						case 51:
							return TraceEventSubclass.PostLoad;
						case 52:
							return TraceEventSubclass.MetadataTraversalDuringBackup;
						case 53:
							return TraceEventSubclass.VertiPaq;
						case 54:
							return TraceEventSubclass.HierarchyProcessing;
						case 55:
							return TraceEventSubclass.SwitchingDictionary;
						case 57:
							return TraceEventSubclass.TabularCommit;
						case 58:
							return TraceEventSubclass.TabularSequencePoint;
						case 59:
							return TraceEventSubclass.TabularRefresh;
						case 60:
							return TraceEventSubclass.TabularSave;
						case 61:
							return TraceEventSubclass.TokenizationStoreProcessing;
						case 63:
							return TraceEventSubclass.Dbcc;
						case 64:
							return TraceEventSubclass.CheckTabularDataStructure;
						case 65:
							return TraceEventSubclass.CheckColumnDataForDuplicatesOrNullValues;
						case 66:
							return TraceEventSubclass.AnalyzeRefreshPolicyImpactForTabularPartition;
						case 67:
							return TraceEventSubclass.ParallelSession;
						case 68:
							return TraceEventSubclass.AutoAggsTraining;
						case 69:
							return TraceEventSubclass.AutoAggsCardinalityAnalysis;
						case 70:
							return TraceEventSubclass.Export;
						}
						break;
					case 7:
						switch (eventSubclass)
						{
						case 1:
							return TraceEventSubclass.Process;
						case 2:
							return TraceEventSubclass.Merge;
						case 3:
							return TraceEventSubclass.Delete;
						case 4:
							return TraceEventSubclass.DeleteOldAggregations;
						case 5:
							return TraceEventSubclass.Rebuild;
						case 6:
							return TraceEventSubclass.Commit;
						case 7:
							return TraceEventSubclass.Rollback;
						case 8:
							return TraceEventSubclass.CreateIndexes;
						case 9:
							return TraceEventSubclass.CreateTable;
						case 10:
							return TraceEventSubclass.InsertInto;
						case 11:
							return TraceEventSubclass.Transaction;
						case 12:
							return TraceEventSubclass.Initialize;
						case 13:
							return TraceEventSubclass.Discretize;
						case 14:
							return TraceEventSubclass.Query;
						case 15:
							return TraceEventSubclass.CreateView;
						case 16:
							return TraceEventSubclass.WriteData;
						case 17:
							return TraceEventSubclass.ReadData;
						case 18:
							return TraceEventSubclass.GroupData;
						case 19:
							return TraceEventSubclass.GroupDataRecord;
						case 20:
							return TraceEventSubclass.BuildIndex;
						case 21:
							return TraceEventSubclass.Aggregate;
						case 22:
							return TraceEventSubclass.BuildDecode;
						case 23:
							return TraceEventSubclass.WriteDecode;
						case 24:
							return TraceEventSubclass.BuildDataMiningDecode;
						case 25:
							return TraceEventSubclass.ExecuteSql;
						case 26:
							return TraceEventSubclass.ExecuteModifiedSql;
						case 27:
							return TraceEventSubclass.Connecting;
						case 28:
							return TraceEventSubclass.BuildAggregationsAndIndexes;
						case 29:
							return TraceEventSubclass.MergeAggregationsOnDisk;
						case 30:
							return TraceEventSubclass.BuildIndexForRigidAggregations;
						case 31:
							return TraceEventSubclass.BuildIndexForFlexibleAggregations;
						case 32:
							return TraceEventSubclass.WriteAggregationsAndIndexes;
						case 33:
							return TraceEventSubclass.WriteSegment;
						case 34:
							return TraceEventSubclass.DataMiningProgress;
						case 35:
							return TraceEventSubclass.ReadBufferFullReport;
						case 36:
							return TraceEventSubclass.ProactiveCacheConversion;
						case 37:
							return TraceEventSubclass.Backup;
						case 38:
							return TraceEventSubclass.Restore;
						case 39:
							return TraceEventSubclass.Synchronize;
						case 40:
							return TraceEventSubclass.BuildProcessingSchedule;
						case 41:
							return TraceEventSubclass.Detach;
						case 42:
							return TraceEventSubclass.Attach;
						case 43:
							return TraceEventSubclass.AnalyzeEncodeData;
						case 44:
							return TraceEventSubclass.CompressSegment;
						case 45:
							return TraceEventSubclass.WriteTableColumn;
						case 46:
							return TraceEventSubclass.RelationshipBuildPrepare;
						case 47:
							return TraceEventSubclass.BuildRelationshipSegment;
						case 48:
							return TraceEventSubclass.Load;
						case 49:
							return TraceEventSubclass.MetadataLoad;
						case 50:
							return TraceEventSubclass.DataLoad;
						case 51:
							return TraceEventSubclass.PostLoad;
						case 52:
							return TraceEventSubclass.MetadataTraversalDuringBackup;
						case 53:
							return TraceEventSubclass.VertiPaq;
						case 54:
							return TraceEventSubclass.HierarchyProcessing;
						case 55:
							return TraceEventSubclass.SwitchingDictionary;
						case 57:
							return TraceEventSubclass.TabularCommit;
						case 58:
							return TraceEventSubclass.TabularSequencePoint;
						case 59:
							return TraceEventSubclass.TabularRefresh;
						case 60:
							return TraceEventSubclass.TabularSave;
						case 61:
							return TraceEventSubclass.TokenizationStoreProcessing;
						case 63:
							return TraceEventSubclass.Dbcc;
						case 64:
							return TraceEventSubclass.CheckTabularDataStructure;
						case 65:
							return TraceEventSubclass.CheckColumnDataForDuplicatesOrNullValues;
						case 66:
							return TraceEventSubclass.AnalyzeRefreshPolicyImpactForTabularPartition;
						case 69:
							return TraceEventSubclass.AutoAggsCardinalityAnalysis;
						case 70:
							return TraceEventSubclass.Export;
						}
						break;
					case 8:
						switch (eventSubclass)
						{
						case 1:
							return TraceEventSubclass.Process;
						case 2:
							return TraceEventSubclass.Merge;
						case 3:
							return TraceEventSubclass.Delete;
						case 4:
							return TraceEventSubclass.DeleteOldAggregations;
						case 5:
							return TraceEventSubclass.Rebuild;
						case 6:
							return TraceEventSubclass.Commit;
						case 7:
							return TraceEventSubclass.Rollback;
						case 8:
							return TraceEventSubclass.CreateIndexes;
						case 9:
							return TraceEventSubclass.CreateTable;
						case 10:
							return TraceEventSubclass.InsertInto;
						case 11:
							return TraceEventSubclass.Transaction;
						case 12:
							return TraceEventSubclass.Initialize;
						case 13:
							return TraceEventSubclass.Discretize;
						case 14:
							return TraceEventSubclass.Query;
						case 15:
							return TraceEventSubclass.CreateView;
						case 16:
							return TraceEventSubclass.WriteData;
						case 17:
							return TraceEventSubclass.ReadData;
						case 18:
							return TraceEventSubclass.GroupData;
						case 19:
							return TraceEventSubclass.GroupDataRecord;
						case 20:
							return TraceEventSubclass.BuildIndex;
						case 21:
							return TraceEventSubclass.Aggregate;
						case 22:
							return TraceEventSubclass.BuildDecode;
						case 23:
							return TraceEventSubclass.WriteDecode;
						case 24:
							return TraceEventSubclass.BuildDataMiningDecode;
						case 25:
							return TraceEventSubclass.ExecuteSql;
						case 26:
							return TraceEventSubclass.ExecuteModifiedSql;
						case 27:
							return TraceEventSubclass.Connecting;
						case 28:
							return TraceEventSubclass.BuildAggregationsAndIndexes;
						case 29:
							return TraceEventSubclass.MergeAggregationsOnDisk;
						case 30:
							return TraceEventSubclass.BuildIndexForRigidAggregations;
						case 31:
							return TraceEventSubclass.BuildIndexForFlexibleAggregations;
						case 32:
							return TraceEventSubclass.WriteAggregationsAndIndexes;
						case 33:
							return TraceEventSubclass.WriteSegment;
						case 34:
							return TraceEventSubclass.DataMiningProgress;
						case 35:
							return TraceEventSubclass.ReadBufferFullReport;
						case 36:
							return TraceEventSubclass.ProactiveCacheConversion;
						case 37:
							return TraceEventSubclass.Backup;
						case 38:
							return TraceEventSubclass.Restore;
						case 39:
							return TraceEventSubclass.Synchronize;
						case 40:
							return TraceEventSubclass.BuildProcessingSchedule;
						case 41:
							return TraceEventSubclass.Detach;
						case 42:
							return TraceEventSubclass.Attach;
						case 43:
							return TraceEventSubclass.AnalyzeEncodeData;
						case 44:
							return TraceEventSubclass.CompressSegment;
						case 45:
							return TraceEventSubclass.WriteTableColumn;
						case 46:
							return TraceEventSubclass.RelationshipBuildPrepare;
						case 47:
							return TraceEventSubclass.BuildRelationshipSegment;
						case 48:
							return TraceEventSubclass.Load;
						case 49:
							return TraceEventSubclass.MetadataLoad;
						case 50:
							return TraceEventSubclass.DataLoad;
						case 51:
							return TraceEventSubclass.PostLoad;
						case 52:
							return TraceEventSubclass.MetadataTraversalDuringBackup;
						case 53:
							return TraceEventSubclass.VertiPaq;
						case 54:
							return TraceEventSubclass.HierarchyProcessing;
						case 55:
							return TraceEventSubclass.SwitchingDictionary;
						case 57:
							return TraceEventSubclass.TabularCommit;
						case 58:
							return TraceEventSubclass.TabularSequencePoint;
						case 59:
							return TraceEventSubclass.TabularRefresh;
						case 60:
							return TraceEventSubclass.TabularSave;
						case 61:
							return TraceEventSubclass.TokenizationStoreProcessing;
						case 63:
							return TraceEventSubclass.Dbcc;
						case 64:
							return TraceEventSubclass.CheckTabularDataStructure;
						case 65:
							return TraceEventSubclass.CheckColumnDataForDuplicatesOrNullValues;
						case 66:
							return TraceEventSubclass.AnalyzeRefreshPolicyImpactForTabularPartition;
						}
						break;
					case 9:
						switch (eventSubclass)
						{
						case 0:
							return TraceEventSubclass.MdxQuery;
						case 1:
							return TraceEventSubclass.DmxQuery;
						case 2:
							return TraceEventSubclass.SqlQuery;
						case 3:
							return TraceEventSubclass.DAXQuery;
						case 4:
							return TraceEventSubclass.JSON;
						}
						break;
					case 10:
						switch (eventSubclass)
						{
						case 0:
							return TraceEventSubclass.MdxQuery;
						case 1:
							return TraceEventSubclass.DmxQuery;
						case 2:
							return TraceEventSubclass.SqlQuery;
						case 3:
							return TraceEventSubclass.DAXQuery;
						case 4:
							return TraceEventSubclass.JSON;
						}
						break;
					case 11:
						switch (eventSubclass)
						{
						case 1:
							return TraceEventSubclass.CacheData;
						case 2:
							return TraceEventSubclass.NonCacheData;
						case 3:
							return TraceEventSubclass.InternalData;
						case 4:
							return TraceEventSubclass.SqlData;
						default:
							if (eventSubclass == 11)
							{
								return TraceEventSubclass.MeasureGroupStructuralChange;
							}
							if (eventSubclass == 12)
							{
								return TraceEventSubclass.MeasureGroupDeletion;
							}
							break;
						}
						break;
					case 12:
						switch (eventSubclass)
						{
						case 21:
							return TraceEventSubclass.CacheData;
						case 22:
							return TraceEventSubclass.NonCacheData;
						case 23:
							return TraceEventSubclass.InternalData;
						case 24:
							return TraceEventSubclass.SqlData;
						}
						break;
					case 13:
					case 14:
					case 17:
					case 18:
						break;
					case 15:
						switch (eventSubclass)
						{
						case 0:
							return TraceEventSubclass.Create;
						case 1:
							return TraceEventSubclass.Alter;
						case 2:
							return TraceEventSubclass.Delete;
						case 3:
							return TraceEventSubclass.Process;
						case 4:
							return TraceEventSubclass.DesignAggregations;
						case 5:
							return TraceEventSubclass.WBInsert;
						case 6:
							return TraceEventSubclass.WBUpdate;
						case 7:
							return TraceEventSubclass.WBDelete;
						case 8:
							return TraceEventSubclass.Backup;
						case 9:
							return TraceEventSubclass.Restore;
						case 10:
							return TraceEventSubclass.MergePartitions;
						case 11:
							return TraceEventSubclass.Subscribe;
						case 12:
							return TraceEventSubclass.Batch;
						case 13:
							return TraceEventSubclass.BeginTransaction;
						case 14:
							return TraceEventSubclass.CommitTransaction;
						case 15:
							return TraceEventSubclass.RollbackTransaction;
						case 16:
							return TraceEventSubclass.GetTransactionState;
						case 17:
							return TraceEventSubclass.Cancel;
						case 18:
							return TraceEventSubclass.Synchronize;
						case 19:
							return TraceEventSubclass.Import80MiningModels;
						case 20:
							return TraceEventSubclass.Attach;
						case 21:
							return TraceEventSubclass.Detach;
						case 22:
							return TraceEventSubclass.SetAuthContext;
						case 23:
							return TraceEventSubclass.ImageLoad;
						case 24:
							return TraceEventSubclass.ImageSave;
						case 25:
							return TraceEventSubclass.CloneDatabase;
						case 26:
							return TraceEventSubclass.TabularCreate;
						case 27:
							return TraceEventSubclass.TabularAlter;
						case 28:
							return TraceEventSubclass.TabularDelete;
						case 29:
							return TraceEventSubclass.TabularRefresh;
						case 30:
							return TraceEventSubclass.Interpret;
						case 31:
							return TraceEventSubclass.ExtAuth;
						case 32:
							return TraceEventSubclass.Dbcc;
						case 33:
							return TraceEventSubclass.TabularRename;
						case 34:
							return TraceEventSubclass.TabularSequencePoint;
						case 35:
							return TraceEventSubclass.TabularUpgrade;
						case 36:
							return TraceEventSubclass.TabularMergePartitions;
						case 37:
							return TraceEventSubclass.DisableDatabase;
						case 38:
							return TraceEventSubclass.JsonCommand;
						case 39:
							return TraceEventSubclass.Evict;
						case 40:
							return TraceEventSubclass.CommitImport;
						case 41:
							return TraceEventSubclass.RemoveDiscontinuedFeatures;
						case 42:
							return TraceEventSubclass.Export;
						default:
							if (eventSubclass == 10000)
							{
								return TraceEventSubclass.Other;
							}
							break;
						}
						break;
					case 16:
						switch (eventSubclass)
						{
						case 0:
							return TraceEventSubclass.Create;
						case 1:
							return TraceEventSubclass.Alter;
						case 2:
							return TraceEventSubclass.Delete;
						case 3:
							return TraceEventSubclass.Process;
						case 4:
							return TraceEventSubclass.DesignAggregations;
						case 5:
							return TraceEventSubclass.WBInsert;
						case 6:
							return TraceEventSubclass.WBUpdate;
						case 7:
							return TraceEventSubclass.WBDelete;
						case 8:
							return TraceEventSubclass.Backup;
						case 9:
							return TraceEventSubclass.Restore;
						case 10:
							return TraceEventSubclass.MergePartitions;
						case 11:
							return TraceEventSubclass.Subscribe;
						case 12:
							return TraceEventSubclass.Batch;
						case 13:
							return TraceEventSubclass.BeginTransaction;
						case 14:
							return TraceEventSubclass.CommitTransaction;
						case 15:
							return TraceEventSubclass.RollbackTransaction;
						case 16:
							return TraceEventSubclass.GetTransactionState;
						case 17:
							return TraceEventSubclass.Cancel;
						case 18:
							return TraceEventSubclass.Synchronize;
						case 19:
							return TraceEventSubclass.Import80MiningModels;
						case 20:
							return TraceEventSubclass.Attach;
						case 21:
							return TraceEventSubclass.Detach;
						case 22:
							return TraceEventSubclass.SetAuthContext;
						case 23:
							return TraceEventSubclass.ImageLoad;
						case 24:
							return TraceEventSubclass.ImageSave;
						case 25:
							return TraceEventSubclass.CloneDatabase;
						case 26:
							return TraceEventSubclass.TabularCreate;
						case 27:
							return TraceEventSubclass.TabularAlter;
						case 28:
							return TraceEventSubclass.TabularDelete;
						case 29:
							return TraceEventSubclass.TabularRefresh;
						case 30:
							return TraceEventSubclass.Interpret;
						case 31:
							return TraceEventSubclass.ExtAuth;
						case 32:
							return TraceEventSubclass.Dbcc;
						case 33:
							return TraceEventSubclass.TabularRename;
						case 34:
							return TraceEventSubclass.TabularSequencePoint;
						case 35:
							return TraceEventSubclass.TabularUpgrade;
						case 36:
							return TraceEventSubclass.TabularMergePartitions;
						case 37:
							return TraceEventSubclass.DisableDatabase;
						case 38:
							return TraceEventSubclass.JsonCommand;
						case 39:
							return TraceEventSubclass.Evict;
						case 40:
							return TraceEventSubclass.CommitImport;
						case 41:
							return TraceEventSubclass.RemoveDiscontinuedFeatures;
						case 42:
							return TraceEventSubclass.Export;
						default:
							if (eventSubclass == 10000)
							{
								return TraceEventSubclass.Other;
							}
							break;
						}
						break;
					case 19:
						switch (eventSubclass)
						{
						case 1:
							return TraceEventSubclass.Backup;
						case 2:
							return TraceEventSubclass.Restore;
						case 3:
							return TraceEventSubclass.Synchronize;
						case 4:
							return TraceEventSubclass.Detach;
						case 5:
							return TraceEventSubclass.Attach;
						case 6:
							return TraceEventSubclass.ImageLoad;
						case 7:
							return TraceEventSubclass.ImageSave;
						}
						break;
					default:
						switch (eventClass)
						{
						case 33:
							switch (eventSubclass)
							{
							case 1:
								return TraceEventSubclass.DiscoverConnections;
							case 2:
								return TraceEventSubclass.DiscoverSessions;
							case 3:
								return TraceEventSubclass.DiscoverTransactions;
							case 6:
								return TraceEventSubclass.DiscoverDatabaseConnections;
							case 7:
								return TraceEventSubclass.DiscoverJobs;
							case 8:
								return TraceEventSubclass.DiscoverLocks;
							case 12:
								return TraceEventSubclass.DiscoverPerformanceCounters;
							case 13:
								return TraceEventSubclass.DiscoverMemoryUsage;
							case 14:
								return TraceEventSubclass.DiscoverJobProgress;
							case 15:
								return TraceEventSubclass.DiscoverMemoryGrant;
							}
							break;
						case 34:
							switch (eventSubclass)
							{
							case 1:
								return TraceEventSubclass.DiscoverConnections;
							case 2:
								return TraceEventSubclass.DiscoverSessions;
							case 3:
								return TraceEventSubclass.DiscoverTransactions;
							case 6:
								return TraceEventSubclass.DiscoverDatabaseConnections;
							case 7:
								return TraceEventSubclass.DiscoverJobs;
							case 8:
								return TraceEventSubclass.DiscoverLocks;
							case 12:
								return TraceEventSubclass.DiscoverPerformanceCounters;
							case 13:
								return TraceEventSubclass.DiscoverMemoryUsage;
							case 14:
								return TraceEventSubclass.DiscoverJobProgress;
							case 15:
								return TraceEventSubclass.DiscoverMemoryGrant;
							case 16:
								return TraceEventSubclass.DISCOVER_COMMANDS;
							case 17:
								return TraceEventSubclass.DISCOVER_COMMAND_OBJECTS;
							case 18:
								return TraceEventSubclass.DISCOVER_OBJECT_ACTIVITY;
							case 19:
								return TraceEventSubclass.DISCOVER_OBJECT_MEMORY_USAGE;
							}
							break;
						case 35:
							switch (eventSubclass)
							{
							case 1:
								return TraceEventSubclass.DiscoverConnections;
							case 2:
								return TraceEventSubclass.DiscoverSessions;
							case 3:
								return TraceEventSubclass.DiscoverTransactions;
							case 6:
								return TraceEventSubclass.DiscoverDatabaseConnections;
							case 7:
								return TraceEventSubclass.DiscoverJobs;
							case 8:
								return TraceEventSubclass.DiscoverLocks;
							case 12:
								return TraceEventSubclass.DiscoverPerformanceCounters;
							case 13:
								return TraceEventSubclass.DiscoverMemoryUsage;
							case 14:
								return TraceEventSubclass.DiscoverJobProgress;
							case 15:
								return TraceEventSubclass.DiscoverMemoryGrant;
							case 16:
								return TraceEventSubclass.DISCOVER_COMMANDS;
							case 17:
								return TraceEventSubclass.DISCOVER_COMMAND_OBJECTS;
							case 18:
								return TraceEventSubclass.DISCOVER_OBJECT_ACTIVITY;
							case 19:
								return TraceEventSubclass.DISCOVER_OBJECT_MEMORY_USAGE;
							}
							break;
						case 36:
							switch (eventSubclass)
							{
							case 0:
								return TraceEventSubclass.SchemaCatalogs;
							case 1:
								return TraceEventSubclass.SchemaTables;
							case 2:
								return TraceEventSubclass.SchemaColumns;
							case 3:
								return TraceEventSubclass.SchemaProviderTypes;
							case 4:
								return TraceEventSubclass.SchemaCubes;
							case 5:
								return TraceEventSubclass.SchemaDimensions;
							case 6:
								return TraceEventSubclass.SchemaHierarchies;
							case 7:
								return TraceEventSubclass.SchemaLevels;
							case 8:
								return TraceEventSubclass.SchemaMeasures;
							case 9:
								return TraceEventSubclass.SchemaProperties;
							case 10:
								return TraceEventSubclass.SchemaMembers;
							case 11:
								return TraceEventSubclass.SchemaFunctions;
							case 12:
								return TraceEventSubclass.SchemaActions;
							case 13:
								return TraceEventSubclass.SchemaSets;
							case 14:
								return TraceEventSubclass.DiscoverInstances;
							case 15:
								return TraceEventSubclass.SchemaKpis;
							case 16:
								return TraceEventSubclass.SchemaMeasureGroups;
							case 17:
								return TraceEventSubclass.SchemaCommands;
							case 18:
								return TraceEventSubclass.SchemaMiningServices;
							case 19:
								return TraceEventSubclass.SchemaMiningServiceParameters;
							case 20:
								return TraceEventSubclass.SchemaMiningFunctions;
							case 21:
								return TraceEventSubclass.SchemaMiningModelContent;
							case 22:
								return TraceEventSubclass.SchemaMiningModelXml;
							case 23:
								return TraceEventSubclass.SchemaMiningModels;
							case 24:
								return TraceEventSubclass.SchemaMiningColumns;
							case 25:
								return TraceEventSubclass.DiscoverDataSources;
							case 26:
								return TraceEventSubclass.DiscoverProperties;
							case 27:
								return TraceEventSubclass.DiscoverSchemaRowsets;
							case 28:
								return TraceEventSubclass.DiscoverEnumerators;
							case 29:
								return TraceEventSubclass.DiscoverKeywords;
							case 30:
								return TraceEventSubclass.DiscoverLiterals;
							case 31:
								return TraceEventSubclass.DiscoverXmlMetadata;
							case 32:
								return TraceEventSubclass.DiscoverTraces;
							case 33:
								return TraceEventSubclass.DiscoverTraceDefinitionProviderInfo;
							case 34:
								return TraceEventSubclass.DiscoverTraceColumns;
							case 35:
								return TraceEventSubclass.DiscoverTraceEventCategories;
							case 36:
								return TraceEventSubclass.SchemaMiningStructures;
							case 37:
								return TraceEventSubclass.SchemaMiningStructureColumns;
							case 38:
								return TraceEventSubclass.DiscoverMasterKey;
							case 39:
								return TraceEventSubclass.SchemaInputDataSources;
							case 40:
								return TraceEventSubclass.DiscoverLocations;
							case 41:
								return TraceEventSubclass.DiscoverPartitionDimensionStat;
							case 42:
								return TraceEventSubclass.DiscoverPartitionStat;
							case 43:
								return TraceEventSubclass.DiscoverDimensionStat;
							case 44:
								return TraceEventSubclass.SchemaMeasureGroupDimensions;
							case 45:
								return TraceEventSubclass.DISCOVER_XEVENT_PACKAGES;
							case 46:
								return TraceEventSubclass.DISCOVER_XEVENT_OBJECTS;
							case 47:
								return TraceEventSubclass.DISCOVER_XEVENT_OBJECT_COLUMNS;
							case 48:
								return TraceEventSubclass.DISCOVER_XEVENT_SESSION_TARGETS;
							case 49:
								return TraceEventSubclass.DISCOVER_XEVENT_TRACE_DEFINITION;
							case 50:
								return TraceEventSubclass.DISCOVER_STORAGE_TABLES;
							case 51:
								return TraceEventSubclass.DISCOVER_STORAGE_TABLE_COLUMNS;
							case 52:
								return TraceEventSubclass.DISCOVER_STORAGE_TABLE_COLUMN_SEGMENTS;
							case 53:
								return TraceEventSubclass.DISCOVER_CALC_DEPENDENCY;
							case 54:
								return TraceEventSubclass.DISCOVER_CSDL_METADATA;
							case 55:
								return TraceEventSubclass.DISCOVER_RESOURCE_POOLS;
							case 56:
								return TraceEventSubclass.TabularSchemaModel;
							case 57:
								return TraceEventSubclass.TabularSchemaDataSources;
							case 58:
								return TraceEventSubclass.TabularSchemaTables;
							case 59:
								return TraceEventSubclass.TabularSchemaColumns;
							case 60:
								return TraceEventSubclass.TabularSchemaAttributeHierarchies;
							case 61:
								return TraceEventSubclass.TabularSchemaPartitions;
							case 62:
								return TraceEventSubclass.TabularSchemaRelationships;
							case 63:
								return TraceEventSubclass.TabularSchemaMeasures;
							case 64:
								return TraceEventSubclass.TabularSchemaHierarchies;
							case 65:
								return TraceEventSubclass.TabularSchemaLevels;
							case 67:
								return TraceEventSubclass.TabularSchemaTableStorages;
							case 68:
								return TraceEventSubclass.TabularSchemaColumnStorages;
							case 69:
								return TraceEventSubclass.TabularSchemaPartitionStorages;
							case 70:
								return TraceEventSubclass.TabularSchemaSegmentMapStorages;
							case 71:
								return TraceEventSubclass.TabularSchemaDictionaryStorages;
							case 72:
								return TraceEventSubclass.TabularSchemaColumnPartitionStorages;
							case 73:
								return TraceEventSubclass.TabularSchemaRelationshipStorages;
							case 74:
								return TraceEventSubclass.TabularSchemaRelationshipIndexStorages;
							case 75:
								return TraceEventSubclass.TabularSchemaAttributeHierarchyStorages;
							case 76:
								return TraceEventSubclass.TabularSchemaHierarchyStorages;
							case 77:
								return TraceEventSubclass.DISCOVER_RING_BUFFERS;
							case 78:
								return TraceEventSubclass.TabularSchemaKpis;
							case 79:
								return TraceEventSubclass.TabularSchemaStorageFolders;
							case 80:
								return TraceEventSubclass.TabularSchemaStorageFiles;
							case 81:
								return TraceEventSubclass.TabularSchemaSegmentStorages;
							case 82:
								return TraceEventSubclass.TabularSchemaCultures;
							case 83:
								return TraceEventSubclass.TabularSchemaObjectTranslations;
							case 84:
								return TraceEventSubclass.TabularSchemaLinguisticMetadata;
							case 85:
								return TraceEventSubclass.TabularSchemaAnnotations;
							case 86:
								return TraceEventSubclass.TabularSchemaPerspectives;
							case 87:
								return TraceEventSubclass.TabularSchemaPerspectiveTables;
							case 88:
								return TraceEventSubclass.TabularSchemaPerspectiveColumns;
							case 89:
								return TraceEventSubclass.TabularSchemaPerspectiveHierarchies;
							case 90:
								return TraceEventSubclass.TabularSchemaPerspectiveMeasures;
							case 91:
								return TraceEventSubclass.TabularSchemaRoles;
							case 92:
								return TraceEventSubclass.TabularSchemaRoleMemberships;
							case 93:
								return TraceEventSubclass.TabularSchemaTablePermissions;
							case 94:
								return TraceEventSubclass.TabularSchemaVariations;
							case 95:
								return TraceEventSubclass.TabularSchemaSets;
							case 96:
								return TraceEventSubclass.TabularSchemaPerspectiveSets;
							case 97:
								return TraceEventSubclass.TabularSchemaExtendedProperties;
							case 98:
								return TraceEventSubclass.TabularSchemaExpressions;
							case 99:
								return TraceEventSubclass.TabularSchemaColumnPermissions;
							case 100:
								return TraceEventSubclass.TabularSchemaDetailRowsDefinitions;
							case 101:
								return TraceEventSubclass.TabularSchemaRelatedColumnDetails;
							case 102:
								return TraceEventSubclass.TabularSchemaGroupByColumns;
							case 103:
								return TraceEventSubclass.TabularSchemaCalculationGroups;
							case 104:
								return TraceEventSubclass.TabularSchemaCalculationItems;
							case 105:
								return TraceEventSubclass.TabularSchemaAlternateOfDefinitions;
							case 106:
								return TraceEventSubclass.TabularSchemaRefreshPolicies;
							case 107:
								return TraceEventSubclass.DiscoverPowerBIDatasources;
							case 108:
								return TraceEventSubclass.TabularSchemaFormatStringDefinitions;
							case 109:
								return TraceEventSubclass.DiscoverMExpressions;
							case 110:
								return TraceEventSubclass.TabularSchemaPowerbiRoles;
							case 111:
								return TraceEventSubclass.TabularSchemaQueryGroups;
							case 112:
								return TraceEventSubclass.DiscoverDBMemoryStats;
							case 113:
								return TraceEventSubclass.DiscoverMemoryStats;
							case 114:
								return TraceEventSubclass.TabularSchemaAnalyticsAimetadata;
							case 115:
								return TraceEventSubclass.DiscoverObjectCounters;
							case 116:
								return TraceEventSubclass.DiscoverModelSecurity;
							case 117:
								return TraceEventSubclass.TabularSchemaDataCoverageDefinitions;
							case 118:
								return TraceEventSubclass.TabularSchemaCalculationExpressions;
							}
							break;
						case 38:
							switch (eventSubclass)
							{
							case 0:
								return TraceEventSubclass.SchemaCatalogs;
							case 1:
								return TraceEventSubclass.SchemaTables;
							case 2:
								return TraceEventSubclass.SchemaColumns;
							case 3:
								return TraceEventSubclass.SchemaProviderTypes;
							case 4:
								return TraceEventSubclass.SchemaCubes;
							case 5:
								return TraceEventSubclass.SchemaDimensions;
							case 6:
								return TraceEventSubclass.SchemaHierarchies;
							case 7:
								return TraceEventSubclass.SchemaLevels;
							case 8:
								return TraceEventSubclass.SchemaMeasures;
							case 9:
								return TraceEventSubclass.SchemaProperties;
							case 10:
								return TraceEventSubclass.SchemaMembers;
							case 11:
								return TraceEventSubclass.SchemaFunctions;
							case 12:
								return TraceEventSubclass.SchemaActions;
							case 13:
								return TraceEventSubclass.SchemaSets;
							case 14:
								return TraceEventSubclass.DiscoverInstances;
							case 15:
								return TraceEventSubclass.SchemaKpis;
							case 16:
								return TraceEventSubclass.SchemaMeasureGroups;
							case 17:
								return TraceEventSubclass.SchemaCommands;
							case 18:
								return TraceEventSubclass.SchemaMiningServices;
							case 19:
								return TraceEventSubclass.SchemaMiningServiceParameters;
							case 20:
								return TraceEventSubclass.SchemaMiningFunctions;
							case 21:
								return TraceEventSubclass.SchemaMiningModelContent;
							case 22:
								return TraceEventSubclass.SchemaMiningModelXml;
							case 23:
								return TraceEventSubclass.SchemaMiningModels;
							case 24:
								return TraceEventSubclass.SchemaMiningColumns;
							case 25:
								return TraceEventSubclass.DiscoverDataSources;
							case 26:
								return TraceEventSubclass.DiscoverProperties;
							case 27:
								return TraceEventSubclass.DiscoverSchemaRowsets;
							case 28:
								return TraceEventSubclass.DiscoverEnumerators;
							case 29:
								return TraceEventSubclass.DiscoverKeywords;
							case 30:
								return TraceEventSubclass.DiscoverLiterals;
							case 31:
								return TraceEventSubclass.DiscoverXmlMetadata;
							case 32:
								return TraceEventSubclass.DiscoverTraces;
							case 33:
								return TraceEventSubclass.DiscoverTraceDefinitionProviderInfo;
							case 34:
								return TraceEventSubclass.DiscoverTraceColumns;
							case 35:
								return TraceEventSubclass.DiscoverTraceEventCategories;
							case 36:
								return TraceEventSubclass.SchemaMiningStructures;
							case 37:
								return TraceEventSubclass.SchemaMiningStructureColumns;
							case 38:
								return TraceEventSubclass.DiscoverMasterKey;
							case 39:
								return TraceEventSubclass.SchemaInputDataSources;
							case 40:
								return TraceEventSubclass.DiscoverLocations;
							case 41:
								return TraceEventSubclass.DiscoverPartitionDimensionStat;
							case 42:
								return TraceEventSubclass.DiscoverPartitionStat;
							case 43:
								return TraceEventSubclass.DiscoverDimensionStat;
							case 44:
								return TraceEventSubclass.SchemaMeasureGroupDimensions;
							case 45:
								return TraceEventSubclass.DISCOVER_XEVENT_PACKAGES;
							case 46:
								return TraceEventSubclass.DISCOVER_XEVENT_OBJECTS;
							case 47:
								return TraceEventSubclass.DISCOVER_XEVENT_OBJECT_COLUMNS;
							case 48:
								return TraceEventSubclass.DISCOVER_XEVENT_SESSION_TARGETS;
							case 49:
								return TraceEventSubclass.DISCOVER_XEVENT_TRACE_DEFINITION;
							case 50:
								return TraceEventSubclass.DISCOVER_STORAGE_TABLES;
							case 51:
								return TraceEventSubclass.DISCOVER_STORAGE_TABLE_COLUMNS;
							case 52:
								return TraceEventSubclass.DISCOVER_STORAGE_TABLE_COLUMN_SEGMENTS;
							case 53:
								return TraceEventSubclass.DISCOVER_CALC_DEPENDENCY;
							case 54:
								return TraceEventSubclass.DISCOVER_CSDL_METADATA;
							case 55:
								return TraceEventSubclass.DISCOVER_RESOURCE_POOLS;
							case 56:
								return TraceEventSubclass.TabularSchemaModel;
							case 57:
								return TraceEventSubclass.TabularSchemaDataSources;
							case 58:
								return TraceEventSubclass.TabularSchemaTables;
							case 59:
								return TraceEventSubclass.TabularSchemaColumns;
							case 60:
								return TraceEventSubclass.TabularSchemaAttributeHierarchies;
							case 61:
								return TraceEventSubclass.TabularSchemaPartitions;
							case 62:
								return TraceEventSubclass.TabularSchemaRelationships;
							case 63:
								return TraceEventSubclass.TabularSchemaMeasures;
							case 64:
								return TraceEventSubclass.TabularSchemaHierarchies;
							case 65:
								return TraceEventSubclass.TabularSchemaLevels;
							case 67:
								return TraceEventSubclass.TabularSchemaTableStorages;
							case 68:
								return TraceEventSubclass.TabularSchemaColumnStorages;
							case 69:
								return TraceEventSubclass.TabularSchemaPartitionStorages;
							case 70:
								return TraceEventSubclass.TabularSchemaSegmentMapStorages;
							case 71:
								return TraceEventSubclass.TabularSchemaDictionaryStorages;
							case 72:
								return TraceEventSubclass.TabularSchemaColumnPartitionStorages;
							case 73:
								return TraceEventSubclass.TabularSchemaRelationshipStorages;
							case 74:
								return TraceEventSubclass.TabularSchemaRelationshipIndexStorages;
							case 75:
								return TraceEventSubclass.TabularSchemaAttributeHierarchyStorages;
							case 76:
								return TraceEventSubclass.TabularSchemaHierarchyStorages;
							case 77:
								return TraceEventSubclass.DISCOVER_RING_BUFFERS;
							case 78:
								return TraceEventSubclass.TabularSchemaKpis;
							case 79:
								return TraceEventSubclass.TabularSchemaStorageFolders;
							case 80:
								return TraceEventSubclass.TabularSchemaStorageFiles;
							case 81:
								return TraceEventSubclass.TabularSchemaSegmentStorages;
							case 82:
								return TraceEventSubclass.TabularSchemaCultures;
							case 83:
								return TraceEventSubclass.TabularSchemaObjectTranslations;
							case 84:
								return TraceEventSubclass.TabularSchemaLinguisticMetadata;
							case 85:
								return TraceEventSubclass.TabularSchemaAnnotations;
							case 86:
								return TraceEventSubclass.TabularSchemaPerspectives;
							case 87:
								return TraceEventSubclass.TabularSchemaPerspectiveTables;
							case 88:
								return TraceEventSubclass.TabularSchemaPerspectiveColumns;
							case 89:
								return TraceEventSubclass.TabularSchemaPerspectiveHierarchies;
							case 90:
								return TraceEventSubclass.TabularSchemaPerspectiveMeasures;
							case 91:
								return TraceEventSubclass.TabularSchemaRoles;
							case 92:
								return TraceEventSubclass.TabularSchemaRoleMemberships;
							case 93:
								return TraceEventSubclass.TabularSchemaTablePermissions;
							case 94:
								return TraceEventSubclass.TabularSchemaVariations;
							case 95:
								return TraceEventSubclass.TabularSchemaSets;
							case 96:
								return TraceEventSubclass.TabularSchemaPerspectiveSets;
							case 97:
								return TraceEventSubclass.TabularSchemaExtendedProperties;
							case 98:
								return TraceEventSubclass.TabularSchemaExpressions;
							case 99:
								return TraceEventSubclass.TabularSchemaColumnPermissions;
							case 100:
								return TraceEventSubclass.TabularSchemaDetailRowsDefinitions;
							case 101:
								return TraceEventSubclass.TabularSchemaRelatedColumnDetails;
							case 102:
								return TraceEventSubclass.TabularSchemaGroupByColumns;
							case 103:
								return TraceEventSubclass.TabularSchemaCalculationGroups;
							case 104:
								return TraceEventSubclass.TabularSchemaCalculationItems;
							case 105:
								return TraceEventSubclass.TabularSchemaAlternateOfDefinitions;
							case 106:
								return TraceEventSubclass.TabularSchemaRefreshPolicies;
							case 107:
								return TraceEventSubclass.DiscoverPowerBIDatasources;
							case 108:
								return TraceEventSubclass.TabularSchemaFormatStringDefinitions;
							case 109:
								return TraceEventSubclass.DiscoverMExpressions;
							case 110:
								return TraceEventSubclass.TabularSchemaPowerbiRoles;
							case 111:
								return TraceEventSubclass.TabularSchemaQueryGroups;
							case 112:
								return TraceEventSubclass.DiscoverDBMemoryStats;
							case 113:
								return TraceEventSubclass.DiscoverMemoryStats;
							case 114:
								return TraceEventSubclass.TabularSchemaAnalyticsAimetadata;
							case 115:
								return TraceEventSubclass.DiscoverObjectCounters;
							case 116:
								return TraceEventSubclass.DiscoverModelSecurity;
							case 117:
								return TraceEventSubclass.TabularSchemaDataCoverageDefinitions;
							case 118:
								return TraceEventSubclass.TabularSchemaCalculationExpressions;
							}
							break;
						case 39:
							switch (eventSubclass)
							{
							case 0:
								return TraceEventSubclass.ProactiveCachingBegin;
							case 1:
								return TraceEventSubclass.ProactiveCachingEnd;
							case 2:
								return TraceEventSubclass.FlightRecorderStarted;
							case 3:
								return TraceEventSubclass.FlightRecorderStopped;
							case 4:
								return TraceEventSubclass.ConfigurationPropertiesUpdated;
							case 5:
								return TraceEventSubclass.SqlTrace;
							case 6:
								return TraceEventSubclass.ObjectCreated;
							case 7:
								return TraceEventSubclass.ObjectDeleted;
							case 8:
								return TraceEventSubclass.ObjectAltered;
							case 9:
								return TraceEventSubclass.ProactiveCachingPollingBegin;
							case 10:
								return TraceEventSubclass.ProactiveCachingPollingEnd;
							case 11:
								return TraceEventSubclass.FlightRecorderSnapshotBegin;
							case 12:
								return TraceEventSubclass.FlightRecorderSnapshotEnd;
							case 13:
								return TraceEventSubclass.ProactiveCachingNotifiableObjectUpdated;
							case 14:
								return TraceEventSubclass.LazyProcessingStartProcessing;
							case 15:
								return TraceEventSubclass.LazyProcessingProcessingComplete;
							case 16:
								return TraceEventSubclass.SessionOpenedEventBegin;
							case 17:
								return TraceEventSubclass.SessionOpenedEventEnd;
							case 18:
								return TraceEventSubclass.SessionClosingEventBegin;
							case 19:
								return TraceEventSubclass.SessionClosingEventEnd;
							case 20:
								return TraceEventSubclass.CubeOpenedEventBegin;
							case 21:
								return TraceEventSubclass.CubeOpenedEventEnd;
							case 22:
								return TraceEventSubclass.CubeClosingEventBegin;
							case 23:
								return TraceEventSubclass.CubeClosingEventEnd;
							case 24:
								return TraceEventSubclass.TransactionAbortRequested;
							case 25:
								return TraceEventSubclass.OpenedConnection;
							}
							break;
						}
						break;
					}
				}
				else if (eventClass != 61)
				{
					switch (eventClass)
					{
					case 73:
						switch (eventSubclass)
						{
						case 1:
							return TraceEventSubclass.GetData;
						case 2:
							return TraceEventSubclass.ProcessCalculatedMembers;
						case 3:
							return TraceEventSubclass.PostOrder;
						}
						break;
					case 76:
						switch (eventSubclass)
						{
						case 1:
							return TraceEventSubclass.SerializeAxes;
						case 2:
							return TraceEventSubclass.SerializeCells;
						case 3:
							return TraceEventSubclass.SerializeSqlRowset;
						case 4:
							return TraceEventSubclass.SerializeFlattenedRowset;
						}
						break;
					case 78:
						if (eventSubclass == 1)
						{
							return TraceEventSubclass.MdxScript;
						}
						if (eventSubclass == 2)
						{
							return TraceEventSubclass.MdxScriptCommand;
						}
						break;
					case 80:
						if (eventSubclass == 1)
						{
							return TraceEventSubclass.MdxScript;
						}
						if (eventSubclass == 2)
						{
							return TraceEventSubclass.MdxScriptCommand;
						}
						break;
					case 81:
						if (eventSubclass == 1)
						{
							return TraceEventSubclass.CacheData;
						}
						if (eventSubclass == 2)
						{
							return TraceEventSubclass.NonCacheData;
						}
						break;
					case 82:
						if (eventSubclass <= 20)
						{
							switch (eventSubclass)
							{
							case 0:
								return TraceEventSubclass.VertiPaqScan;
							case 1:
								return TraceEventSubclass.TabularQuery;
							case 2:
								return TraceEventSubclass.UserHierarchyProcessingQuery;
							case 3:
							case 4:
							case 6:
							case 7:
							case 8:
							case 9:
								break;
							case 5:
								return TraceEventSubclass.BatchVertiPaqScan;
							case 10:
								return TraceEventSubclass.VertiPaqScanInternal;
							case 11:
								return TraceEventSubclass.TabularQueryInternal;
							case 12:
								return TraceEventSubclass.UserHierarchyProcessingQueryInternal;
							default:
								if (eventSubclass == 20)
								{
									return TraceEventSubclass.VertiPaqScanQueryPlan;
								}
								break;
							}
						}
						else
						{
							if (eventSubclass == 30)
							{
								return TraceEventSubclass.VertiPaqScanLocal;
							}
							if (eventSubclass == 40)
							{
								return TraceEventSubclass.VertiPaqScanRemote;
							}
							if (eventSubclass == 50)
							{
								return TraceEventSubclass.VertiPaqCacheProbe;
							}
						}
						break;
					case 83:
						if (eventSubclass <= 20)
						{
							switch (eventSubclass)
							{
							case 0:
								return TraceEventSubclass.VertiPaqScan;
							case 1:
								return TraceEventSubclass.TabularQuery;
							case 2:
								return TraceEventSubclass.UserHierarchyProcessingQuery;
							case 3:
							case 4:
							case 6:
							case 7:
							case 8:
							case 9:
								break;
							case 5:
								return TraceEventSubclass.BatchVertiPaqScan;
							case 10:
								return TraceEventSubclass.VertiPaqScanInternal;
							case 11:
								return TraceEventSubclass.TabularQueryInternal;
							case 12:
								return TraceEventSubclass.UserHierarchyProcessingQueryInternal;
							default:
								if (eventSubclass == 20)
								{
									return TraceEventSubclass.VertiPaqScanQueryPlan;
								}
								break;
							}
						}
						else
						{
							if (eventSubclass == 30)
							{
								return TraceEventSubclass.VertiPaqScanLocal;
							}
							if (eventSubclass == 40)
							{
								return TraceEventSubclass.VertiPaqScanRemote;
							}
							if (eventSubclass == 50)
							{
								return TraceEventSubclass.VertiPaqCacheProbe;
							}
						}
						break;
					case 85:
						if (eventSubclass == 0)
						{
							return TraceEventSubclass.VertiPaqCacheExactMatch;
						}
						break;
					case 86:
						if (eventSubclass == 0)
						{
							return TraceEventSubclass.VertiPaqCacheNotFound;
						}
						break;
					}
				}
				else
				{
					switch (eventSubclass)
					{
					case 1:
						return TraceEventSubclass.GetDataFromMeasureGroupCache;
					case 2:
						return TraceEventSubclass.GetDataFromFlatCache;
					case 3:
						return TraceEventSubclass.GetDataFromCalculationCache;
					case 4:
						return TraceEventSubclass.GetDataFromPersistedCache;
					}
				}
			}
			else if (eventClass <= 126)
			{
				switch (eventClass)
				{
				case 110:
					switch (eventSubclass)
					{
					case 1:
						return TraceEventSubclass.InitEvalNodeStart;
					case 2:
						return TraceEventSubclass.InitEvalNodeEnd;
					case 3:
						return TraceEventSubclass.BuildEvalNodeStart;
					case 4:
						return TraceEventSubclass.BuildEvalNodeEnd;
					case 5:
						return TraceEventSubclass.PrepareEvalNodeStart;
					case 6:
						return TraceEventSubclass.PrepareEvalNodeEnd;
					case 7:
						return TraceEventSubclass.RunEvalNodeStart;
					case 8:
						return TraceEventSubclass.RunEvalNodeEnd;
					}
					break;
				case 111:
					switch (eventSubclass)
					{
					case 100:
						return TraceEventSubclass.BuildEvalNodeEliminatedEmptyCalculations;
					case 101:
						return TraceEventSubclass.BuildEvalNodeSubtractedCalculationSpaces;
					case 102:
						return TraceEventSubclass.BuildEvalNodeAppliedVisualTotals;
					case 103:
						return TraceEventSubclass.BuildEvalNodeDetectedCachedEvaluationNode;
					case 104:
						return TraceEventSubclass.BuildEvalNodeDetectedCachedEvaluationResults;
					case 105:
						return TraceEventSubclass.PrepareEvalNodeBeginPrepareEvaluationItem;
					case 106:
						return TraceEventSubclass.PrepareEvalNodeFinishedPrepareEvaluationItem;
					case 107:
						return TraceEventSubclass.RunEvalNodeFinishedCalculatingItem;
					}
					break;
				case 112:
					switch (eventSubclass)
					{
					case 1:
						return TraceEventSubclass.DAXVertiPaqLogicalPlan;
					case 2:
						return TraceEventSubclass.DAXVertiPaqPhysicalPlan;
					case 3:
						return TraceEventSubclass.DAXDirectQueryAlgebrizerTree;
					case 4:
						return TraceEventSubclass.DAXDirectQueryLogicalPlan;
					}
					break;
				case 113:
					break;
				case 114:
					switch (eventSubclass)
					{
					case 1:
						return TraceEventSubclass.RGWLGroupExceedHighMemoryLimit;
					case 2:
						return TraceEventSubclass.RGWLGroupExceedHardMemoryLimit;
					case 3:
						return TraceEventSubclass.RGWLGroupBelowHighMemoryLimit;
					case 4:
						return TraceEventSubclass.RGWLGroupBelowHardMemoryLimit;
					}
					break;
				default:
					if (eventClass == 126)
					{
						if (eventSubclass == 1)
						{
							return TraceEventSubclass.MdxScript;
						}
						if (eventSubclass == 2)
						{
							return TraceEventSubclass.MdxScriptCommand;
						}
					}
					break;
				}
			}
			else if (eventClass != 131)
			{
				if (eventClass == 134)
				{
					if (eventSubclass == 1)
					{
						return TraceEventSubclass.GraphCreated;
					}
					if (eventSubclass == 2)
					{
						return TraceEventSubclass.GraphFinished;
					}
				}
			}
			else if (eventSubclass == 1)
			{
				return TraceEventSubclass.RewriteAttempted;
			}
			return TraceEventSubclass.NotAvailable;
		}

		// Token: 0x040001B2 RID: 434
		private string[] data = new string[Trace.TraceColumnCount];

		// Token: 0x040001B3 RID: 435
		internal XmlaMessageCollection xmlaMessages;
	}
}
