using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000460 RID: 1120
	public sealed class DataAggregateObj : IErrorContext, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600337D RID: 13181 RVA: 0x000E4722 File Offset: 0x000E2922
		internal DataAggregateObj()
		{
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000E472C File Offset: 0x000E292C
		internal DataAggregateObj(DataAggregateInfo aggInfo, OnDemandProcessingContext odpContext)
		{
			this.m_nonAggregateMode = false;
			this.m_aggregator = AggregatorFactory.Instance.CreateAggregator(odpContext, aggInfo);
			this.m_aggregateDef = aggInfo;
			this.m_reportRT = odpContext.ReportRuntime;
			if (this.m_reportRT.ReportExprHost != null)
			{
				this.m_aggregateDef.SetExprHosts(this.m_reportRT.ReportExprHost, odpContext.ReportObjectModel);
			}
			this.Init();
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x000E479A File Offset: 0x000E299A
		internal DataAggregateObj(DataAggregateInfo aggrDef, DataAggregateObjResult aggrResult)
		{
			this.m_nonAggregateMode = true;
			this.m_aggregateDef = aggrDef;
			this.m_aggregateResult = aggrResult;
		}

		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x06003380 RID: 13184 RVA: 0x000E47B7 File Offset: 0x000E29B7
		internal string Name
		{
			get
			{
				return this.m_aggregateDef.Name;
			}
		}

		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x06003381 RID: 13185 RVA: 0x000E47C4 File Offset: 0x000E29C4
		internal List<string> DuplicateNames
		{
			get
			{
				return this.m_aggregateDef.DuplicateNames;
			}
		}

		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x06003382 RID: 13186 RVA: 0x000E47D1 File Offset: 0x000E29D1
		internal bool NonAggregateMode
		{
			get
			{
				return this.m_nonAggregateMode;
			}
		}

		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x06003383 RID: 13187 RVA: 0x000E47D9 File Offset: 0x000E29D9
		internal DataAggregateInfo AggregateDef
		{
			get
			{
				return this.m_aggregateDef;
			}
		}

		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x06003384 RID: 13188 RVA: 0x000E47E1 File Offset: 0x000E29E1
		// (set) Token: 0x06003385 RID: 13189 RVA: 0x000E47E9 File Offset: 0x000E29E9
		internal bool UsedInExpression
		{
			get
			{
				return this.m_usedInExpression;
			}
			set
			{
				this.m_usedInExpression = value;
			}
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x000E47F2 File Offset: 0x000E29F2
		internal void Init()
		{
			if (this.m_nonAggregateMode)
			{
				return;
			}
			this.m_aggregator.Init();
			this.m_aggregateResult = new DataAggregateObjResult();
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x000E4813 File Offset: 0x000E2A13
		internal void ResetForNoRows()
		{
			if (this.m_nonAggregateMode)
			{
				this.m_aggregateResult = new DataAggregateObjResult();
				this.m_aggregateResult.Value = AggregatorFactory.Instance.GetNoRowsResult(this.m_aggregateDef);
				return;
			}
			this.Init();
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x000E484C File Offset: 0x000E2A4C
		internal void Update()
		{
			if (this.m_aggregateResult.ErrorOccurred || this.m_nonAggregateMode)
			{
				return;
			}
			if (this.m_aggregateDef.ShouldRecordFieldReferences())
			{
				this.m_reportRT.ReportObjectModel.FieldsImpl.ResetFieldsUsedInExpression();
			}
			object[] array;
			DataFieldStatus dataFieldStatus;
			this.m_aggregateResult.ErrorOccurred = this.EvaluateParameters(out array, out dataFieldStatus);
			if (dataFieldStatus != DataFieldStatus.None)
			{
				this.m_aggregateResult.HasCode = true;
				this.m_aggregateResult.FieldStatus = dataFieldStatus;
			}
			if (this.m_aggregateDef.ShouldRecordFieldReferences())
			{
				List<string> list = new List<string>();
				this.m_reportRT.ReportObjectModel.FieldsImpl.AddFieldsUsedInExpression(list);
				this.m_aggregateDef.StoreFieldReferences(this.m_reportRT.ReportObjectModel.OdpContext, list);
			}
			if (this.m_aggregateResult.ErrorOccurred)
			{
				return;
			}
			try
			{
				this.m_aggregator.Update(array, this);
			}
			catch (ReportProcessingException)
			{
				this.m_aggregateResult.ErrorOccurred = true;
			}
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x000E4944 File Offset: 0x000E2B44
		internal DataAggregateObjResult AggregateResult()
		{
			if (!this.m_nonAggregateMode && !this.m_aggregateResult.ErrorOccurred)
			{
				try
				{
					this.m_aggregateResult.Value = this.m_aggregator.Result();
				}
				catch (ReportProcessingException)
				{
					this.m_aggregateResult.ErrorOccurred = true;
					this.m_aggregateResult.Value = null;
				}
			}
			if (this.m_aggregateDef.MustCopyAggregateResult)
			{
				return new DataAggregateObjResult(this.m_aggregateResult);
			}
			return this.m_aggregateResult;
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x000E49C8 File Offset: 0x000E2BC8
		internal bool EvaluateParameters(out object[] values, out DataFieldStatus fieldStatus)
		{
			bool flag = false;
			fieldStatus = DataFieldStatus.None;
			values = new object[this.m_aggregateDef.Expressions.Length];
			for (int i = 0; i < this.m_aggregateDef.Expressions.Length; i++)
			{
				try
				{
					Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = this.m_reportRT.EvaluateAggregateVariantOrBinaryParamExpr(this.m_aggregateDef, i, this);
					values[i] = variantResult.Value;
					flag |= variantResult.ErrorOccurred;
					if (variantResult.FieldStatus != DataFieldStatus.None)
					{
						fieldStatus = variantResult.FieldStatus;
					}
				}
				catch (ReportProcessingException_MissingAggregateDependency)
				{
					if (this.m_aggregateDef.AggregateType == DataAggregateInfo.AggregateTypes.Previous)
					{
						values[i] = null;
						fieldStatus = DataFieldStatus.None;
						return false;
					}
					Global.Tracer.Assert(false, "Unfulfilled aggregate dependency outside of a previous");
					throw;
				}
			}
			return flag;
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x000E4A84 File Offset: 0x000E2C84
		internal void Set(DataAggregateObjResult aggregateResult)
		{
			this.m_nonAggregateMode = true;
			this.m_aggregateResult = aggregateResult;
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x000E4A94 File Offset: 0x000E2C94
		void IErrorContext.Register(ProcessingErrorCode code, Severity severity, params string[] arguments)
		{
			if (!this.m_aggregateResult.HasCode)
			{
				this.m_aggregateResult.HasCode = true;
				this.m_aggregateResult.Code = code;
				this.m_aggregateResult.Severity = severity;
				this.m_aggregateResult.Arguments = arguments;
			}
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x000E4AD3 File Offset: 0x000E2CD3
		void IErrorContext.Register(ProcessingErrorCode code, Severity severity, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, params string[] arguments)
		{
			if (!this.m_aggregateResult.HasCode)
			{
				this.m_aggregateResult.HasCode = true;
				this.m_aggregateResult.Code = code;
				this.m_aggregateResult.Severity = severity;
				this.m_aggregateResult.Arguments = arguments;
			}
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x000E4B14 File Offset: 0x000E2D14
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(DataAggregateObj.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.UsedInExpression)
				{
					switch (memberName)
					{
					case MemberName.NonAggregateMode:
						writer.Write(this.m_nonAggregateMode);
						break;
					case MemberName.Aggregator:
						writer.Write(this.m_aggregator);
						break;
					case MemberName.AggregateDef:
						writer.Write(scalabilityCache.StoreStaticReference(this.m_aggregateDef));
						break;
					case MemberName.ReportRuntime:
					{
						int num = scalabilityCache.StoreStaticReference(this.m_reportRT);
						writer.Write(num);
						break;
					}
					case MemberName.AggregateResult:
						writer.Write(this.m_aggregateResult);
						break;
					default:
						Global.Tracer.Assert(false);
						break;
					}
				}
				else
				{
					writer.Write(this.m_usedInExpression);
				}
			}
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000E4BF0 File Offset: 0x000E2DF0
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(DataAggregateObj.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.UsedInExpression)
				{
					switch (memberName)
					{
					case MemberName.NonAggregateMode:
						this.m_nonAggregateMode = reader.ReadBoolean();
						break;
					case MemberName.Aggregator:
						this.m_aggregator = (DataAggregate)reader.ReadRIFObject();
						break;
					case MemberName.AggregateDef:
					{
						int num = reader.ReadInt32();
						this.m_aggregateDef = (DataAggregateInfo)scalabilityCache.FetchStaticReference(num);
						break;
					}
					case MemberName.ReportRuntime:
					{
						int num2 = reader.ReadInt32();
						this.m_reportRT = (Microsoft.ReportingServices.RdlExpressions.ReportRuntime)scalabilityCache.FetchStaticReference(num2);
						break;
					}
					case MemberName.AggregateResult:
						this.m_aggregateResult = (DataAggregateObjResult)reader.ReadRIFObject();
						break;
					default:
						Global.Tracer.Assert(false);
						break;
					}
				}
				else
				{
					this.m_usedInExpression = reader.ReadBoolean();
				}
			}
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000E4CE1 File Offset: 0x000E2EE1
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x000E4CE3 File Offset: 0x000E2EE3
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj;
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x000E4CE8 File Offset: 0x000E2EE8
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (DataAggregateObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.NonAggregateMode, Token.Boolean),
					new MemberInfo(MemberName.Aggregator, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregate),
					new MemberInfo(MemberName.AggregateDef, Token.Int32),
					new MemberInfo(MemberName.ReportRuntime, Token.Int32),
					new MemberInfo(MemberName.UsedInExpression, Token.Boolean),
					new MemberInfo(MemberName.AggregateResult, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjResult)
				});
			}
			return DataAggregateObj.m_declaration;
		}

		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x06003393 RID: 13203 RVA: 0x000E4D7D File Offset: 0x000E2F7D
		public int Size
		{
			get
			{
				return 1 + ItemSizes.SizeOf(this.m_aggregator) + ItemSizes.ReferenceSize + ItemSizes.ReferenceSize + 1 + ItemSizes.SizeOf(this.m_aggregateResult);
			}
		}

		// Token: 0x040019C6 RID: 6598
		private bool m_nonAggregateMode;

		// Token: 0x040019C7 RID: 6599
		private DataAggregate m_aggregator;

		// Token: 0x040019C8 RID: 6600
		[StaticReference]
		private DataAggregateInfo m_aggregateDef;

		// Token: 0x040019C9 RID: 6601
		[StaticReference]
		private Microsoft.ReportingServices.RdlExpressions.ReportRuntime m_reportRT;

		// Token: 0x040019CA RID: 6602
		private bool m_usedInExpression;

		// Token: 0x040019CB RID: 6603
		private DataAggregateObjResult m_aggregateResult;

		// Token: 0x040019CC RID: 6604
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = DataAggregateObj.GetDeclaration();
	}
}
