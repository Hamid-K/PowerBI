using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004B8 RID: 1208
	[Serializable]
	public sealed class DataSource : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable, IProcessingDataSource
	{
		// Token: 0x06003C41 RID: 15425 RVA: 0x00103DDE File Offset: 0x00101FDE
		internal DataSource()
		{
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x00103DFF File Offset: 0x00101FFF
		internal DataSource(int id)
		{
			this.m_referenceID = id;
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x00103E28 File Offset: 0x00102028
		internal DataSource(int id, Guid sharedDataSourceReferenceId)
		{
			this.m_referenceID = id;
			this.m_ID = sharedDataSourceReferenceId;
			this.m_isArtificialDataSource = true;
			this.m_name = " Data source for shared dataset";
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x00103E74 File Offset: 0x00102074
		internal DataSource(int id, Guid sharedDataSourceReferenceId, DataSetCore dataSetCore)
			: this(id, sharedDataSourceReferenceId)
		{
			DataSet dataSet = new DataSet(dataSetCore);
			this.m_dataSets = new List<DataSet>(1);
			this.m_dataSets.Add(dataSet);
		}

		// Token: 0x170019C7 RID: 6599
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x00103EA8 File Offset: 0x001020A8
		internal bool IsArtificialForSharedDataSets
		{
			get
			{
				return this.m_isArtificialDataSource;
			}
		}

		// Token: 0x170019C8 RID: 6600
		// (get) Token: 0x06003C46 RID: 15430 RVA: 0x00103EB0 File Offset: 0x001020B0
		// (set) Token: 0x06003C47 RID: 15431 RVA: 0x00103EB8 File Offset: 0x001020B8
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170019C9 RID: 6601
		// (get) Token: 0x06003C48 RID: 15432 RVA: 0x00103EC1 File Offset: 0x001020C1
		// (set) Token: 0x06003C49 RID: 15433 RVA: 0x00103EC9 File Offset: 0x001020C9
		public bool Transaction
		{
			get
			{
				return this.m_transaction;
			}
			set
			{
				this.m_transaction = value;
			}
		}

		// Token: 0x170019CA RID: 6602
		// (get) Token: 0x06003C4A RID: 15434 RVA: 0x00103ED2 File Offset: 0x001020D2
		// (set) Token: 0x06003C4B RID: 15435 RVA: 0x00103EDA File Offset: 0x001020DA
		public string Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x170019CB RID: 6603
		// (get) Token: 0x06003C4C RID: 15436 RVA: 0x00103EE3 File Offset: 0x001020E3
		// (set) Token: 0x06003C4D RID: 15437 RVA: 0x00103EEB File Offset: 0x001020EB
		internal ExpressionInfo ConnectStringExpression
		{
			get
			{
				return this.m_connectString;
			}
			set
			{
				this.m_connectString = value;
			}
		}

		// Token: 0x170019CC RID: 6604
		// (get) Token: 0x06003C4E RID: 15438 RVA: 0x00103EF4 File Offset: 0x001020F4
		// (set) Token: 0x06003C4F RID: 15439 RVA: 0x00103EFC File Offset: 0x001020FC
		public bool IntegratedSecurity
		{
			get
			{
				return this.m_integratedSecurity;
			}
			set
			{
				this.m_integratedSecurity = value;
			}
		}

		// Token: 0x170019CD RID: 6605
		// (get) Token: 0x06003C50 RID: 15440 RVA: 0x00103F05 File Offset: 0x00102105
		// (set) Token: 0x06003C51 RID: 15441 RVA: 0x00103F0D File Offset: 0x0010210D
		public string Prompt
		{
			get
			{
				return this.m_prompt;
			}
			set
			{
				this.m_prompt = value;
			}
		}

		// Token: 0x170019CE RID: 6606
		// (get) Token: 0x06003C52 RID: 15442 RVA: 0x00103F16 File Offset: 0x00102116
		// (set) Token: 0x06003C53 RID: 15443 RVA: 0x00103F1E File Offset: 0x0010211E
		public string DataSourceReference
		{
			get
			{
				return this.m_dataSourceReference;
			}
			set
			{
				this.m_dataSourceReference = value;
			}
		}

		// Token: 0x170019CF RID: 6607
		// (get) Token: 0x06003C54 RID: 15444 RVA: 0x00103F27 File Offset: 0x00102127
		// (set) Token: 0x06003C55 RID: 15445 RVA: 0x00103F2F File Offset: 0x0010212F
		internal List<DataSet> DataSets
		{
			get
			{
				return this.m_dataSets;
			}
			set
			{
				this.m_dataSets = value;
			}
		}

		// Token: 0x170019D0 RID: 6608
		// (get) Token: 0x06003C56 RID: 15446 RVA: 0x00103F38 File Offset: 0x00102138
		// (set) Token: 0x06003C57 RID: 15447 RVA: 0x00103F40 File Offset: 0x00102140
		public Guid ID
		{
			get
			{
				return this.m_ID;
			}
			set
			{
				this.m_ID = value;
			}
		}

		// Token: 0x170019D1 RID: 6609
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x00103F49 File Offset: 0x00102149
		internal DataSourceExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170019D2 RID: 6610
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x00103F51 File Offset: 0x00102151
		// (set) Token: 0x06003C5A RID: 15450 RVA: 0x00103F59 File Offset: 0x00102159
		internal int ExprHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
			}
		}

		// Token: 0x170019D3 RID: 6611
		// (get) Token: 0x06003C5B RID: 15451 RVA: 0x00103F62 File Offset: 0x00102162
		// (set) Token: 0x06003C5C RID: 15452 RVA: 0x00103F6A File Offset: 0x0010216A
		internal bool IsComplex
		{
			get
			{
				return this.m_isComplex;
			}
			set
			{
				this.m_isComplex = value;
			}
		}

		// Token: 0x170019D4 RID: 6612
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x00103F73 File Offset: 0x00102173
		// (set) Token: 0x06003C5E RID: 15454 RVA: 0x00103F7B File Offset: 0x0010217B
		internal Dictionary<string, bool> ParameterNames
		{
			get
			{
				return this.m_parameterNames;
			}
			set
			{
				this.m_parameterNames = value;
			}
		}

		// Token: 0x170019D5 RID: 6613
		// (get) Token: 0x06003C5F RID: 15455 RVA: 0x00103F84 File Offset: 0x00102184
		// (set) Token: 0x06003C60 RID: 15456 RVA: 0x00103F8C File Offset: 0x0010218C
		public string SharedDataSourceReferencePath
		{
			get
			{
				return this.m_sharedDataSourceReferencePath;
			}
			set
			{
				this.m_sharedDataSourceReferencePath = value;
			}
		}

		// Token: 0x170019D6 RID: 6614
		// (get) Token: 0x06003C61 RID: 15457 RVA: 0x00103F95 File Offset: 0x00102195
		// (set) Token: 0x06003C62 RID: 15458 RVA: 0x00103F9D File Offset: 0x0010219D
		internal string ConnectionCategory
		{
			get
			{
				return this.m_connectionCategory;
			}
			set
			{
				this.m_connectionCategory = value;
			}
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x00103FA8 File Offset: 0x001021A8
		internal void Initialize(InitializationContext context)
		{
			context.ObjectType = Microsoft.ReportingServices.ReportProcessing.ObjectType.DataSource;
			context.ObjectName = this.m_name;
			this.InternalInitialize(context);
			if (this.m_dataSets != null)
			{
				for (int i = 0; i < this.m_dataSets.Count; i++)
				{
					Global.Tracer.Assert(this.m_dataSets[i] != null, "(null != m_dataSets[i])");
					this.m_dataSets[i].Initialize(context);
				}
				for (int j = 0; j < this.m_dataSets.Count; j++)
				{
					this.m_dataSets[j].CheckCircularDefaultRelationshipReference(context);
				}
			}
		}

		// Token: 0x06003C64 RID: 15460 RVA: 0x00104048 File Offset: 0x00102248
		internal void DetermineDecomposability(InitializationContext context)
		{
			if (this.m_dataSets != null)
			{
				foreach (DataSet dataSet in this.m_dataSets)
				{
					dataSet.DetermineDecomposability(context);
				}
			}
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x001040A4 File Offset: 0x001022A4
		internal string ResolveConnectionString(OnDemandProcessingContext pc, out DataSourceInfo dataSourceInfo)
		{
			dataSourceInfo = this.GetDataSourceInfo(pc);
			string text;
			if (dataSourceInfo != null)
			{
				text = dataSourceInfo.GetConnectionString(pc.DataProtection);
				if (!dataSourceInfo.IsReference && text == null)
				{
					text = this.EvaluateConnectStringExpression(pc);
				}
			}
			else
			{
				text = this.EvaluateConnectStringExpression(pc);
			}
			if (DataSourceInfo.HasUseridReference(text))
			{
				pc.ReportObjectModel.UserImpl.SetConnectionStringUserProfileDependencyOrThrow();
			}
			return text;
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x00104108 File Offset: 0x00102308
		internal DataSourceInfo GetDataSourceInfo(OnDemandProcessingContext pc)
		{
			DataSourceInfo dataSourceInfo = null;
			if (pc.DataSourceInfos != null)
			{
				if (pc.IsSharedDataSetExecutionOnly)
				{
					dataSourceInfo = pc.DataSourceInfos.GetForSharedDataSetExecution();
				}
				else
				{
					if (Guid.Empty != this.ID)
					{
						dataSourceInfo = pc.DataSourceInfos.GetByID(this.ID);
					}
					if (dataSourceInfo == null)
					{
						dataSourceInfo = pc.DataSourceInfos.GetByName(this.Name, pc.ReportContext);
					}
				}
				if (dataSourceInfo == null)
				{
					throw new DataSourceNotFoundException(this.Name);
				}
			}
			else if (this.DataSourceReference != null)
			{
				throw new DataSourceNotFoundException(this.Name);
			}
			return dataSourceInfo;
		}

		// Token: 0x06003C67 RID: 15463 RVA: 0x00104198 File Offset: 0x00102398
		private void InternalInitialize(InitializationContext context)
		{
			context.ExprHostBuilder.DataSourceStart(this.m_name);
			if (this.m_connectString != null)
			{
				this.m_connectString.Initialize("ConnectString", context);
				context.ExprHostBuilder.DataSourceConnectString(this.m_connectString);
			}
			this.m_exprHostID = context.ExprHostBuilder.DataSourceEnd();
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x001041F4 File Offset: 0x001023F4
		private void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (this.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
				this.m_exprHost = reportExprHost.DataSourceHostsRemotable[this.ExprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x06003C69 RID: 15465 RVA: 0x00104248 File Offset: 0x00102448
		private string EvaluateConnectStringExpression(OnDemandProcessingContext processingContext)
		{
			if (this.m_connectString == null)
			{
				return null;
			}
			if (ExpressionInfo.Types.Constant == this.m_connectString.Type)
			{
				return this.m_connectString.StringValue;
			}
			Global.Tracer.Assert(processingContext.ReportRuntime != null, "(null != processingContext.ReportRuntime)");
			if (processingContext.ReportRuntime.ReportExprHost != null)
			{
				this.SetExprHost(processingContext.ReportRuntime.ReportExprHost, processingContext.ReportObjectModel);
			}
			Microsoft.ReportingServices.RdlExpressions.StringResult stringResult = processingContext.ReportRuntime.EvaluateConnectString(this);
			if (stringResult.ErrorOccurred)
			{
				throw new ReportProcessingException(ErrorCode.rsDataSourceConnectStringProcessingError, new object[] { this.m_name });
			}
			return stringResult.Value;
		}

		// Token: 0x06003C6A RID: 15466 RVA: 0x001042E8 File Offset: 0x001024E8
		internal bool AnyActiveDataSetNeedsAutoDetectCollation()
		{
			foreach (DataSet dataSet in this.m_dataSets)
			{
				if (!dataSet.UsedOnlyInParameters && dataSet.NeedAutoDetectCollation())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x0010434C File Offset: 0x0010254C
		internal void MergeCollationSettingsForAllDataSets(ErrorContext errorContext, string cultureName, bool caseSensitive, bool accentSensitive, bool kanatypeSensitive, bool widthSensitive)
		{
			for (int i = 0; i < this.m_dataSets.Count; i++)
			{
				this.m_dataSets[i].MergeCollationSettings(errorContext, this.m_type, cultureName, caseSensitive, accentSensitive, kanatypeSensitive, widthSensitive);
			}
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x00104390 File Offset: 0x00102590
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSource, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Transaction, Token.Boolean),
				new MemberInfo(MemberName.Type, Token.String),
				new MemberInfo(MemberName.ConnectString, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntegratedSecurity, Token.Boolean),
				new MemberInfo(MemberName.Prompt, Token.String),
				new MemberInfo(MemberName.DataSourceReference, Token.String),
				new MemberInfo(MemberName.DataSets, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSet),
				new MemberInfo(MemberName.ID, Token.Guid),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.SharedDataSourceReferencePath, Token.String),
				new MemberInfo(MemberName.ReferenceID, Token.Int32),
				new MemberInfo(MemberName.IsArtificialDataSource, Token.Boolean)
			});
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x001044BC File Offset: 0x001026BC
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(DataSource.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.IntegratedSecurity)
				{
					if (memberName <= MemberName.Name)
					{
						if (memberName == MemberName.ID)
						{
							writer.Write(this.m_ID);
							continue;
						}
						if (memberName == MemberName.Name)
						{
							writer.Write(this.m_name);
							continue;
						}
					}
					else
					{
						switch (memberName)
						{
						case MemberName.Type:
							writer.Write(this.m_type);
							continue;
						case MemberName.Transaction:
							writer.Write(this.m_transaction);
							continue;
						case MemberName.ConnectString:
							writer.Write(this.m_connectString);
							continue;
						case MemberName.DataSets:
							writer.Write<DataSet>(this.m_dataSets);
							continue;
						default:
							if (memberName == MemberName.Prompt)
							{
								writer.Write(this.m_prompt);
								continue;
							}
							if (memberName == MemberName.IntegratedSecurity)
							{
								writer.Write(this.m_integratedSecurity);
								continue;
							}
							break;
						}
					}
				}
				else if (memberName <= MemberName.ExprHostID)
				{
					if (memberName == MemberName.DataSourceReference)
					{
						writer.Write(this.m_dataSourceReference);
						continue;
					}
					if (memberName == MemberName.ExprHostID)
					{
						writer.Write(this.m_exprHostID);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.SharedDataSourceReferencePath)
					{
						writer.Write(this.m_sharedDataSourceReferencePath);
						continue;
					}
					if (memberName == MemberName.ReferenceID)
					{
						writer.Write(this.m_referenceID);
						continue;
					}
					if (memberName == MemberName.IsArtificialDataSource)
					{
						writer.Write(this.m_isArtificialDataSource);
						continue;
					}
				}
				Global.Tracer.Assert(false, string.Empty);
			}
		}

		// Token: 0x06003C6E RID: 15470 RVA: 0x00104678 File Offset: 0x00102878
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(DataSource.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.IntegratedSecurity)
				{
					if (memberName <= MemberName.Name)
					{
						if (memberName == MemberName.ID)
						{
							this.m_ID = reader.ReadGuid();
							continue;
						}
						if (memberName == MemberName.Name)
						{
							this.m_name = reader.ReadString();
							continue;
						}
					}
					else
					{
						switch (memberName)
						{
						case MemberName.Type:
							this.m_type = reader.ReadString();
							continue;
						case MemberName.Transaction:
							this.m_transaction = reader.ReadBoolean();
							continue;
						case MemberName.ConnectString:
							this.m_connectString = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.DataSets:
							this.m_dataSets = reader.ReadGenericListOfRIFObjects<DataSet>();
							continue;
						default:
							if (memberName == MemberName.Prompt)
							{
								this.m_prompt = reader.ReadString();
								continue;
							}
							if (memberName == MemberName.IntegratedSecurity)
							{
								this.m_integratedSecurity = reader.ReadBoolean();
								continue;
							}
							break;
						}
					}
				}
				else if (memberName <= MemberName.ExprHostID)
				{
					if (memberName == MemberName.DataSourceReference)
					{
						this.m_dataSourceReference = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.ExprHostID)
					{
						this.m_exprHostID = reader.ReadInt32();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.SharedDataSourceReferencePath)
					{
						this.m_sharedDataSourceReferencePath = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.ReferenceID)
					{
						this.m_referenceID = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.IsArtificialDataSource)
					{
						this.m_isArtificialDataSource = reader.ReadBoolean();
						continue;
					}
				}
				Global.Tracer.Assert(false, string.Empty);
			}
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x00104836 File Offset: 0x00102A36
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, string.Empty);
		}

		// Token: 0x06003C70 RID: 15472 RVA: 0x00104848 File Offset: 0x00102A48
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSource;
		}

		// Token: 0x170019D7 RID: 6615
		// (get) Token: 0x06003C71 RID: 15473 RVA: 0x0010484F File Offset: 0x00102A4F
		int Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable.ID
		{
			get
			{
				return this.m_referenceID;
			}
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x00104857 File Offset: 0x00102A57
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSource;
		}

		// Token: 0x04001C57 RID: 7255
		private int m_referenceID = -1;

		// Token: 0x04001C58 RID: 7256
		private string m_name;

		// Token: 0x04001C59 RID: 7257
		private bool m_transaction;

		// Token: 0x04001C5A RID: 7258
		private string m_type;

		// Token: 0x04001C5B RID: 7259
		private ExpressionInfo m_connectString;

		// Token: 0x04001C5C RID: 7260
		private bool m_integratedSecurity;

		// Token: 0x04001C5D RID: 7261
		private string m_prompt;

		// Token: 0x04001C5E RID: 7262
		private string m_dataSourceReference;

		// Token: 0x04001C5F RID: 7263
		private List<DataSet> m_dataSets;

		// Token: 0x04001C60 RID: 7264
		private Guid m_ID = Guid.Empty;

		// Token: 0x04001C61 RID: 7265
		private int m_exprHostID = -1;

		// Token: 0x04001C62 RID: 7266
		private string m_sharedDataSourceReferencePath;

		// Token: 0x04001C63 RID: 7267
		private bool m_isArtificialDataSource;

		// Token: 0x04001C64 RID: 7268
		[NonSerialized]
		private DataSourceExprHost m_exprHost;

		// Token: 0x04001C65 RID: 7269
		[NonSerialized]
		private bool m_isComplex;

		// Token: 0x04001C66 RID: 7270
		[NonSerialized]
		private Dictionary<string, bool> m_parameterNames;

		// Token: 0x04001C67 RID: 7271
		[NonSerialized]
		private string m_connectionCategory;

		// Token: 0x04001C68 RID: 7272
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DataSource.GetDeclaration();
	}
}
