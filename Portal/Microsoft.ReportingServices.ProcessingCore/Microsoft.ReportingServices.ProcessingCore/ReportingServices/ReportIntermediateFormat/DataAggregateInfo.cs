using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200045A RID: 1114
	[Serializable]
	public class DataAggregateInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IStaticReferenceable
	{
		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x06003309 RID: 13065 RVA: 0x000E31C2 File Offset: 0x000E13C2
		internal virtual bool MustCopyAggregateResult
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x0600330A RID: 13066 RVA: 0x000E31C5 File Offset: 0x000E13C5
		// (set) Token: 0x0600330B RID: 13067 RVA: 0x000E31CD File Offset: 0x000E13CD
		internal string Name
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

		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x0600330C RID: 13068 RVA: 0x000E31D6 File Offset: 0x000E13D6
		// (set) Token: 0x0600330D RID: 13069 RVA: 0x000E31E3 File Offset: 0x000E13E3
		internal string EvaluationScopeName
		{
			get
			{
				return this.PublishingInfo.Scope;
			}
			set
			{
				this.PublishingInfo.Scope = value;
			}
		}

		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x0600330E RID: 13070 RVA: 0x000E31F1 File Offset: 0x000E13F1
		// (set) Token: 0x0600330F RID: 13071 RVA: 0x000E31FE File Offset: 0x000E13FE
		internal IRIFDataScope EvaluationScope
		{
			get
			{
				return this.PublishingInfo.EvaluationScope;
			}
			set
			{
				this.PublishingInfo.EvaluationScope = value;
			}
		}

		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x000E320C File Offset: 0x000E140C
		// (set) Token: 0x06003311 RID: 13073 RVA: 0x000E3214 File Offset: 0x000E1414
		internal DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return this.m_aggregateType;
			}
			set
			{
				this.m_aggregateType = value;
			}
		}

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x06003312 RID: 13074 RVA: 0x000E321D File Offset: 0x000E141D
		// (set) Token: 0x06003313 RID: 13075 RVA: 0x000E3225 File Offset: 0x000E1425
		internal ExpressionInfo[] Expressions
		{
			get
			{
				return this.m_expressions;
			}
			set
			{
				this.m_expressions = value;
			}
		}

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x000E322E File Offset: 0x000E142E
		// (set) Token: 0x06003315 RID: 13077 RVA: 0x000E3236 File Offset: 0x000E1436
		internal List<string> DuplicateNames
		{
			get
			{
				return this.m_duplicateNames;
			}
			set
			{
				this.m_duplicateNames = value;
			}
		}

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x000E323F File Offset: 0x000E143F
		// (set) Token: 0x06003317 RID: 13079 RVA: 0x000E3247 File Offset: 0x000E1447
		internal int DataSetIndexInCollection
		{
			get
			{
				return this.m_dataSetIndexInCollection;
			}
			set
			{
				this.m_dataSetIndexInCollection = value;
			}
		}

		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x000E3250 File Offset: 0x000E1450
		internal string ExpressionText
		{
			get
			{
				if (this.m_expressions != null && 1 == this.m_expressions.Length)
				{
					return this.m_expressions[0].OriginalText;
				}
				return string.Empty;
			}
		}

		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x06003319 RID: 13081 RVA: 0x000E3278 File Offset: 0x000E1478
		internal string ExpressionTextForCompaction
		{
			get
			{
				if (this.PublishingInfo.Recursive)
				{
					return this.ExpressionText + "$Recursive";
				}
				return this.ExpressionText;
			}
		}

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x0600331A RID: 13082 RVA: 0x000E329E File Offset: 0x000E149E
		internal AggregateParamExprHost[] ExpressionHosts
		{
			get
			{
				return this.m_expressionHosts;
			}
		}

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x000E32A6 File Offset: 0x000E14A6
		// (set) Token: 0x0600331C RID: 13084 RVA: 0x000E32AE File Offset: 0x000E14AE
		internal bool ExprHostInitialized
		{
			get
			{
				return this.m_exprHostInitialized;
			}
			set
			{
				this.m_exprHostInitialized = value;
			}
		}

		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x0600331D RID: 13085 RVA: 0x000E32B7 File Offset: 0x000E14B7
		// (set) Token: 0x0600331E RID: 13086 RVA: 0x000E32C4 File Offset: 0x000E14C4
		internal bool Recursive
		{
			get
			{
				return this.PublishingInfo.Recursive;
			}
			set
			{
				this.PublishingInfo.Recursive = value;
			}
		}

		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x0600331F RID: 13087 RVA: 0x000E32D2 File Offset: 0x000E14D2
		internal bool IsAggregateOfAggregate
		{
			get
			{
				return this.PublishingInfo.NestedAggregates != null && this.PublishingInfo.NestedAggregates.Count > 0;
			}
		}

		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x000E32F6 File Offset: 0x000E14F6
		// (set) Token: 0x06003321 RID: 13089 RVA: 0x000E32FE File Offset: 0x000E14FE
		internal int UpdateScopeID
		{
			get
			{
				return this.m_updateScopeID;
			}
			set
			{
				this.m_updateScopeID = value;
			}
		}

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x06003322 RID: 13090 RVA: 0x000E3307 File Offset: 0x000E1507
		// (set) Token: 0x06003323 RID: 13091 RVA: 0x000E330F File Offset: 0x000E150F
		internal int UpdateScopeDepth
		{
			get
			{
				return this.m_updateScopeDepth;
			}
			set
			{
				this.m_updateScopeDepth = value;
			}
		}

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x06003324 RID: 13092 RVA: 0x000E3318 File Offset: 0x000E1518
		// (set) Token: 0x06003325 RID: 13093 RVA: 0x000E3320 File Offset: 0x000E1520
		internal bool UpdatesAtRowScope
		{
			get
			{
				return this.m_updatesAtRowScope;
			}
			set
			{
				this.m_updatesAtRowScope = value;
			}
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000E332C File Offset: 0x000E152C
		internal void AddNestedAggregate(DataAggregateInfo agg)
		{
			if (DataAggregateInfo.AggregateTypes.Previous == this.m_aggregateType)
			{
				return;
			}
			int num;
			if (agg.IsAggregateOfAggregate)
			{
				num = agg.PublishingInfo.AggregateOfAggregatesLevel + 1;
			}
			else
			{
				num = 0;
			}
			if (num > this.PublishingInfo.AggregateOfAggregatesLevel)
			{
				this.PublishingInfo.AggregateOfAggregatesLevel = num;
			}
			if (this.PublishingInfo.NestedAggregates == null)
			{
				this.PublishingInfo.NestedAggregates = new List<DataAggregateInfo>();
			}
			this.PublishingInfo.NestedAggregates.Add(agg);
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x000E33A6 File Offset: 0x000E15A6
		internal bool ShouldRecordFieldReferences()
		{
			return !this.m_hasCachedFieldReferences;
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000E33B1 File Offset: 0x000E15B1
		internal void StoreFieldReferences(OnDemandProcessingContext odpContext, List<string> dataFieldNames)
		{
			this.m_hasCachedFieldReferences = true;
			odpContext.OdpMetadata.ReportSnapshot.AggregateFieldReferences[this.m_name] = dataFieldNames;
		}

		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x000E33D6 File Offset: 0x000E15D6
		internal DataAggregateInfo.PublishingValidationInfo PublishingInfo
		{
			get
			{
				if (this.m_publishingInfo == null)
				{
					this.m_publishingInfo = new DataAggregateInfo.PublishingValidationInfo();
				}
				return this.m_publishingInfo;
			}
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000E33F4 File Offset: 0x000E15F4
		public virtual object PublishClone(AutomaticSubtotalContext context)
		{
			DataAggregateInfo dataAggregateInfo = (DataAggregateInfo)base.MemberwiseClone();
			if (dataAggregateInfo.m_publishingInfo != null)
			{
				dataAggregateInfo.m_publishingInfo = this.m_publishingInfo.PublishClone();
				dataAggregateInfo.m_publishingInfo.NestedAggregates = null;
			}
			dataAggregateInfo.m_name = context.CreateAggregateID(this.m_name);
			bool flag = false;
			if (context.OuterAggregate != null)
			{
				flag = true;
				context.OuterAggregate.AddNestedAggregate(dataAggregateInfo);
			}
			if (this.IsAggregateOfAggregate)
			{
				context.OuterAggregate = dataAggregateInfo;
			}
			if (this.PublishingInfo.HasScope)
			{
				if (flag)
				{
					dataAggregateInfo.SetScope(context.GetNewScopeNameForInnerOrOuterAggregate(this));
				}
				else
				{
					dataAggregateInfo.SetScope(context.GetNewScopeName(this.PublishingInfo.Scope));
				}
			}
			if (this.m_expressions != null)
			{
				dataAggregateInfo.m_expressions = new ExpressionInfo[this.m_expressions.Length];
				for (int i = 0; i < this.m_expressions.Length; i++)
				{
					dataAggregateInfo.m_expressions[i] = (ExpressionInfo)this.m_expressions[i].PublishClone(context);
				}
			}
			return dataAggregateInfo;
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000E34F4 File Offset: 0x000E16F4
		internal virtual string GetAsString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.m_aggregateType.ToString());
			stringBuilder.Append("(");
			if (this.m_expressions != null)
			{
				for (int i = 0; i < this.m_expressions.Length; i++)
				{
					stringBuilder.Append(this.m_expressions[i].OriginalText);
				}
			}
			if (this.PublishingInfo.HasScope)
			{
				if (this.m_expressions != null)
				{
					stringBuilder.Append(", \"");
				}
				stringBuilder.Append(this.PublishingInfo.Scope);
				stringBuilder.Append("\"");
			}
			if (this.PublishingInfo.Recursive)
			{
				if (this.m_expressions != null || this.PublishingInfo.HasScope)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("Recursive");
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000E35E5 File Offset: 0x000E17E5
		internal void SetScope(string scope)
		{
			this.PublishingInfo.HasScope = true;
			this.PublishingInfo.Scope = scope;
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x000E35FF File Offset: 0x000E17FF
		internal bool GetScope(out string scope)
		{
			scope = this.PublishingInfo.Scope;
			return this.PublishingInfo.HasScope;
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x000E361C File Offset: 0x000E181C
		internal void SetExprHosts(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
			if (!this.m_exprHostInitialized)
			{
				for (int i = 0; i < this.m_expressions.Length; i++)
				{
					ExpressionInfo expressionInfo = this.m_expressions[i];
					if (expressionInfo.ExprHostID >= 0)
					{
						if (this.m_expressionHosts == null)
						{
							this.m_expressionHosts = new AggregateParamExprHost[this.m_expressions.Length];
						}
						AggregateParamExprHost aggregateParamExprHost = reportExprHost.AggregateParamHostsRemotable[expressionInfo.ExprHostID];
						aggregateParamExprHost.SetReportObjectModel(reportObjectModel);
						this.m_expressionHosts[i] = aggregateParamExprHost;
					}
				}
				this.m_exprHostInitialized = true;
				this.m_exprHostReportObjectModel = reportObjectModel;
				return;
			}
			if (this.m_exprHostReportObjectModel != reportObjectModel && this.m_expressionHosts != null)
			{
				for (int j = 0; j < this.m_expressionHosts.Length; j++)
				{
					if (this.m_expressionHosts[j] != null)
					{
						this.m_expressionHosts[j].SetReportObjectModel(reportObjectModel);
					}
				}
				this.m_exprHostReportObjectModel = reportObjectModel;
			}
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x000E36FE File Offset: 0x000E18FE
		internal bool IsPostSortAggregate()
		{
			return this.m_aggregateType == DataAggregateInfo.AggregateTypes.First || DataAggregateInfo.AggregateTypes.Last == this.m_aggregateType || DataAggregateInfo.AggregateTypes.Previous == this.m_aggregateType;
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x000E371E File Offset: 0x000E191E
		internal virtual bool IsRunningValue()
		{
			return false;
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x000E3724 File Offset: 0x000E1924
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.AggregateType, Token.Enum),
				new MemberInfo(MemberName.Expressions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DuplicateNames, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.String),
				new MemberInfo(MemberName.DataSetIndexInCollection, Token.Int32),
				new MemberInfo(MemberName.UpdateScopeID, Token.Int32),
				new MemberInfo(MemberName.UpdateScopeDepth, Token.Int32),
				new MemberInfo(MemberName.UpdatesAtRowScope, Token.Boolean)
			});
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x000E37DC File Offset: 0x000E19DC
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(DataAggregateInfo.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.DataSetIndexInCollection)
				{
					switch (memberName)
					{
					case MemberName.Name:
						writer.Write(this.m_name);
						continue;
					case MemberName.Value:
					case MemberName.DataType:
						break;
					case MemberName.AggregateType:
						writer.WriteEnum((int)this.m_aggregateType);
						continue;
					case MemberName.Expressions:
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] expressions = this.m_expressions;
						writer.Write(expressions);
						continue;
					}
					case MemberName.DuplicateNames:
						writer.WriteListOfPrimitives<string>(this.m_duplicateNames);
						continue;
					default:
						switch (memberName)
						{
						case MemberName.UpdateScopeID:
							writer.Write(this.m_updateScopeID);
							continue;
						case MemberName.UpdateScopeDepth:
							writer.Write(this.m_updateScopeDepth);
							continue;
						case MemberName.UpdatesAtRowScope:
							writer.Write(this.m_updatesAtRowScope);
							continue;
						}
						break;
					}
					Global.Tracer.Assert(false);
				}
				else
				{
					writer.Write(this.m_dataSetIndexInCollection);
				}
			}
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x000E38D8 File Offset: 0x000E1AD8
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(DataAggregateInfo.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.DataSetIndexInCollection)
				{
					switch (memberName)
					{
					case MemberName.Name:
						this.m_name = reader.ReadString();
						continue;
					case MemberName.Value:
					case MemberName.DataType:
						break;
					case MemberName.AggregateType:
						this.m_aggregateType = (DataAggregateInfo.AggregateTypes)reader.ReadEnum();
						continue;
					case MemberName.Expressions:
						this.m_expressions = reader.ReadArrayOfRIFObjects<ExpressionInfo>();
						continue;
					case MemberName.DuplicateNames:
						this.m_duplicateNames = reader.ReadListOfPrimitives<string>();
						continue;
					default:
						switch (memberName)
						{
						case MemberName.UpdateScopeID:
							this.m_updateScopeID = reader.ReadInt32();
							continue;
						case MemberName.UpdateScopeDepth:
							this.m_updateScopeDepth = reader.ReadInt32();
							continue;
						case MemberName.UpdatesAtRowScope:
							this.m_updatesAtRowScope = reader.ReadBoolean();
							continue;
						}
						break;
					}
					Global.Tracer.Assert(false);
				}
				else
				{
					this.m_dataSetIndexInCollection = reader.ReadInt32();
				}
			}
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x000E39D2 File Offset: 0x000E1BD2
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000E39DF File Offset: 0x000E1BDF
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfo;
		}

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x000E39E6 File Offset: 0x000E1BE6
		public int ID
		{
			get
			{
				return this.m_staticId;
			}
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000E39EE File Offset: 0x000E1BEE
		public void SetID(int id)
		{
			this.m_staticId = id;
		}

		// Token: 0x040019A7 RID: 6567
		private string m_name;

		// Token: 0x040019A8 RID: 6568
		private DataAggregateInfo.AggregateTypes m_aggregateType;

		// Token: 0x040019A9 RID: 6569
		private ExpressionInfo[] m_expressions;

		// Token: 0x040019AA RID: 6570
		private List<string> m_duplicateNames;

		// Token: 0x040019AB RID: 6571
		private int m_dataSetIndexInCollection = -1;

		// Token: 0x040019AC RID: 6572
		private int m_updateScopeID = -1;

		// Token: 0x040019AD RID: 6573
		private int m_updateScopeDepth = -1;

		// Token: 0x040019AE RID: 6574
		private bool m_updatesAtRowScope;

		// Token: 0x040019AF RID: 6575
		[NonSerialized]
		private DataAggregateInfo.PublishingValidationInfo m_publishingInfo;

		// Token: 0x040019B0 RID: 6576
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DataAggregateInfo.GetDeclaration();

		// Token: 0x040019B1 RID: 6577
		[NonSerialized]
		private AggregateParamExprHost[] m_expressionHosts;

		// Token: 0x040019B2 RID: 6578
		[NonSerialized]
		private bool m_exprHostInitialized;

		// Token: 0x040019B3 RID: 6579
		[NonSerialized]
		private ObjectModelImpl m_exprHostReportObjectModel;

		// Token: 0x040019B4 RID: 6580
		[NonSerialized]
		private bool m_hasCachedFieldReferences;

		// Token: 0x040019B5 RID: 6581
		[NonSerialized]
		private int m_staticId = int.MinValue;

		// Token: 0x0200096A RID: 2410
		internal enum AggregateTypes
		{
			// Token: 0x040040A3 RID: 16547
			First,
			// Token: 0x040040A4 RID: 16548
			Last,
			// Token: 0x040040A5 RID: 16549
			Sum,
			// Token: 0x040040A6 RID: 16550
			Avg,
			// Token: 0x040040A7 RID: 16551
			Max,
			// Token: 0x040040A8 RID: 16552
			Min,
			// Token: 0x040040A9 RID: 16553
			CountDistinct,
			// Token: 0x040040AA RID: 16554
			CountRows,
			// Token: 0x040040AB RID: 16555
			Count,
			// Token: 0x040040AC RID: 16556
			StDev,
			// Token: 0x040040AD RID: 16557
			Var,
			// Token: 0x040040AE RID: 16558
			StDevP,
			// Token: 0x040040AF RID: 16559
			VarP,
			// Token: 0x040040B0 RID: 16560
			Aggregate,
			// Token: 0x040040B1 RID: 16561
			Previous,
			// Token: 0x040040B2 RID: 16562
			Union
		}

		// Token: 0x0200096B RID: 2411
		internal class PublishingValidationInfo
		{
			// Token: 0x17002985 RID: 10629
			// (get) Token: 0x06008043 RID: 32835 RVA: 0x00210D2C File Offset: 0x0020EF2C
			// (set) Token: 0x06008044 RID: 32836 RVA: 0x00210D34 File Offset: 0x0020EF34
			internal Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
			{
				get
				{
					return this.m_objectType;
				}
				set
				{
					this.m_objectType = value;
				}
			}

			// Token: 0x17002986 RID: 10630
			// (get) Token: 0x06008045 RID: 32837 RVA: 0x00210D3D File Offset: 0x0020EF3D
			// (set) Token: 0x06008046 RID: 32838 RVA: 0x00210D45 File Offset: 0x0020EF45
			internal string ObjectName
			{
				get
				{
					return this.m_objectName;
				}
				set
				{
					this.m_objectName = value;
				}
			}

			// Token: 0x17002987 RID: 10631
			// (get) Token: 0x06008047 RID: 32839 RVA: 0x00210D4E File Offset: 0x0020EF4E
			// (set) Token: 0x06008048 RID: 32840 RVA: 0x00210D56 File Offset: 0x0020EF56
			internal string PropertyName
			{
				get
				{
					return this.m_propertyName;
				}
				set
				{
					this.m_propertyName = value;
				}
			}

			// Token: 0x06008049 RID: 32841 RVA: 0x00210D5F File Offset: 0x0020EF5F
			internal DataAggregateInfo.PublishingValidationInfo PublishClone()
			{
				return (DataAggregateInfo.PublishingValidationInfo)base.MemberwiseClone();
			}

			// Token: 0x17002988 RID: 10632
			// (get) Token: 0x0600804A RID: 32842 RVA: 0x00210D6C File Offset: 0x0020EF6C
			// (set) Token: 0x0600804B RID: 32843 RVA: 0x00210D74 File Offset: 0x0020EF74
			internal IRIFDataScope EvaluationScope
			{
				get
				{
					return this.m_evaluationScope;
				}
				set
				{
					this.m_evaluationScope = value;
				}
			}

			// Token: 0x17002989 RID: 10633
			// (get) Token: 0x0600804C RID: 32844 RVA: 0x00210D7D File Offset: 0x0020EF7D
			// (set) Token: 0x0600804D RID: 32845 RVA: 0x00210D85 File Offset: 0x0020EF85
			internal List<DataAggregateInfo> NestedAggregates
			{
				get
				{
					return this.m_nestedAggregates;
				}
				set
				{
					this.m_nestedAggregates = value;
				}
			}

			// Token: 0x1700298A RID: 10634
			// (get) Token: 0x0600804E RID: 32846 RVA: 0x00210D8E File Offset: 0x0020EF8E
			// (set) Token: 0x0600804F RID: 32847 RVA: 0x00210D96 File Offset: 0x0020EF96
			internal string Scope
			{
				get
				{
					return this.m_scope;
				}
				set
				{
					this.m_scope = value;
				}
			}

			// Token: 0x1700298B RID: 10635
			// (get) Token: 0x06008050 RID: 32848 RVA: 0x00210D9F File Offset: 0x0020EF9F
			// (set) Token: 0x06008051 RID: 32849 RVA: 0x00210DA7 File Offset: 0x0020EFA7
			internal bool HasScope
			{
				get
				{
					return this.m_hasScope;
				}
				set
				{
					this.m_hasScope = value;
				}
			}

			// Token: 0x1700298C RID: 10636
			// (get) Token: 0x06008052 RID: 32850 RVA: 0x00210DB0 File Offset: 0x0020EFB0
			// (set) Token: 0x06008053 RID: 32851 RVA: 0x00210DB8 File Offset: 0x0020EFB8
			internal bool Recursive
			{
				get
				{
					return this.m_recursive;
				}
				set
				{
					this.m_recursive = value;
				}
			}

			// Token: 0x1700298D RID: 10637
			// (get) Token: 0x06008054 RID: 32852 RVA: 0x00210DC1 File Offset: 0x0020EFC1
			// (set) Token: 0x06008055 RID: 32853 RVA: 0x00210DC9 File Offset: 0x0020EFC9
			internal int AggregateOfAggregatesLevel
			{
				get
				{
					return this.m_aggregateOfAggregatesLevel;
				}
				set
				{
					this.m_aggregateOfAggregatesLevel = value;
				}
			}

			// Token: 0x1700298E RID: 10638
			// (get) Token: 0x06008056 RID: 32854 RVA: 0x00210DD2 File Offset: 0x0020EFD2
			// (set) Token: 0x06008057 RID: 32855 RVA: 0x00210DDA File Offset: 0x0020EFDA
			internal bool HasAnyFieldReferences
			{
				get
				{
					return this.m_hasAnyFieldReferences;
				}
				set
				{
					this.m_hasAnyFieldReferences = value;
				}
			}

			// Token: 0x040040B3 RID: 16563
			private Microsoft.ReportingServices.ReportProcessing.ObjectType m_objectType;

			// Token: 0x040040B4 RID: 16564
			private string m_objectName;

			// Token: 0x040040B5 RID: 16565
			private string m_propertyName;

			// Token: 0x040040B6 RID: 16566
			private IRIFDataScope m_evaluationScope;

			// Token: 0x040040B7 RID: 16567
			private List<DataAggregateInfo> m_nestedAggregates;

			// Token: 0x040040B8 RID: 16568
			private string m_scope;

			// Token: 0x040040B9 RID: 16569
			private bool m_hasScope;

			// Token: 0x040040BA RID: 16570
			private bool m_recursive;

			// Token: 0x040040BB RID: 16571
			private int m_aggregateOfAggregatesLevel = -1;

			// Token: 0x040040BC RID: 16572
			private bool m_hasAnyFieldReferences;
		}
	}
}
