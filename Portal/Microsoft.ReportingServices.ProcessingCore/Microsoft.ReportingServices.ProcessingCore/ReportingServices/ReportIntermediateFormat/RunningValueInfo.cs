using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200045B RID: 1115
	[Serializable]
	public sealed class RunningValueInfo : DataAggregateInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x0600333A RID: 13114 RVA: 0x000E3A2B File Offset: 0x000E1C2B
		internal override bool MustCopyAggregateResult
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x000E3A2E File Offset: 0x000E1C2E
		internal override bool IsRunningValue()
		{
			return true;
		}

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x000E3A31 File Offset: 0x000E1C31
		// (set) Token: 0x0600333D RID: 13117 RVA: 0x000E3A39 File Offset: 0x000E1C39
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

		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x0600333E RID: 13118 RVA: 0x000E3A42 File Offset: 0x000E1C42
		// (set) Token: 0x0600333F RID: 13119 RVA: 0x000E3A4A File Offset: 0x000E1C4A
		internal int TotalGroupingExpressionCount
		{
			get
			{
				return this.m_totalGroupingExpressionCount;
			}
			set
			{
				this.m_totalGroupingExpressionCount = value;
			}
		}

		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x000E3A53 File Offset: 0x000E1C53
		// (set) Token: 0x06003341 RID: 13121 RVA: 0x000E3A5B File Offset: 0x000E1C5B
		internal bool IsScopedInEvaluationScope
		{
			get
			{
				return this.m_isScopedInEvaluationScope;
			}
			set
			{
				this.m_isScopedInEvaluationScope = value;
			}
		}

		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x000E3A64 File Offset: 0x000E1C64
		internal bool HasDirectFieldReferences
		{
			get
			{
				bool flag = false;
				if (base.Expressions != null && base.Expressions.Length != 0)
				{
					for (int i = 0; i < base.Expressions.Length; i++)
					{
						if (base.Expressions[i].HasDirectFieldReferences)
						{
							return true;
						}
					}
				}
				return flag;
			}
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x000E3AA9 File Offset: 0x000E1CA9
		public override object PublishClone(AutomaticSubtotalContext context)
		{
			RunningValueInfo runningValueInfo = (RunningValueInfo)base.PublishClone(context);
			runningValueInfo.m_scope = context.GetNewScopeName(this.m_scope);
			return runningValueInfo;
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x000E3ACC File Offset: 0x000E1CCC
		internal DataAggregateInfo GetAsAggregate()
		{
			DataAggregateInfo dataAggregateInfo = null;
			if (base.AggregateType != DataAggregateInfo.AggregateTypes.Previous)
			{
				dataAggregateInfo = new DataAggregateInfo();
				dataAggregateInfo.AggregateType = base.AggregateType;
				dataAggregateInfo.Expressions = base.Expressions;
				dataAggregateInfo.SetScope(this.m_scope);
			}
			return dataAggregateInfo;
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x000E3B10 File Offset: 0x000E1D10
		internal override string GetAsString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			DataAggregateInfo.AggregateTypes aggregateType = base.AggregateType;
			if (aggregateType != DataAggregateInfo.AggregateTypes.CountRows)
			{
				if (aggregateType != DataAggregateInfo.AggregateTypes.Previous)
				{
					stringBuilder.Append("RunningValue(");
					if (base.Expressions != null)
					{
						for (int i = 0; i < base.Expressions.Length; i++)
						{
							stringBuilder.Append(base.Expressions[i].OriginalText);
						}
					}
					stringBuilder.Append(", ");
					stringBuilder.Append(base.AggregateType.ToString());
					if (this.m_scope != null)
					{
						stringBuilder.Append(", \"");
						stringBuilder.Append(this.m_scope);
						stringBuilder.Append("\"");
					}
				}
				else
				{
					stringBuilder.Append("Previous(");
					if (base.Expressions != null)
					{
						for (int j = 0; j < base.Expressions.Length; j++)
						{
							stringBuilder.Append(base.Expressions[j].OriginalText);
						}
						if (this.m_scope != null)
						{
							stringBuilder.Append(", \"");
							stringBuilder.Append(this.m_scope);
							stringBuilder.Append("\"");
						}
					}
				}
			}
			else
			{
				stringBuilder.Append("RowNumber(");
				if (this.m_scope != null)
				{
					stringBuilder.Append("\"");
					stringBuilder.Append(this.m_scope);
					stringBuilder.Append("\"");
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x000E3C90 File Offset: 0x000E1E90
		internal void Initialize(InitializationContext context, string dataSetName, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			if (base.Expressions != null && base.Expressions.Length != 0)
			{
				for (int i = 0; i < base.Expressions.Length; i++)
				{
					ExpressionInfo expressionInfo = base.Expressions[i];
					if (base.AggregateType == DataAggregateInfo.AggregateTypes.Previous && this.m_scope != null && expressionInfo.Aggregates != null)
					{
						using (List<DataAggregateInfo>.Enumerator enumerator = expressionInfo.Aggregates.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string text;
								if (enumerator.Current.GetScope(out text) && !context.IsSameOrChildScope(this.m_scope, text))
								{
									context.ErrorContext.Register(ProcessingErrorCode.rsInvalidScopeInInnerAggregateOfPreviousAggregate, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
								}
							}
						}
					}
					expressionInfo.AggregateInitialize(dataSetName, objectType, objectName, propertyName, context);
				}
			}
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x000E3D74 File Offset: 0x000E1F74
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RunningValueInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfo, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Scope, Token.String),
				new MemberInfo(MemberName.TotalGroupingExpressionCount, Token.Int32),
				new MemberInfo(MemberName.IsScopedInEvaluationScope, Token.Boolean)
			});
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000E3DD0 File Offset: 0x000E1FD0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RunningValueInfo.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.Scope:
					writer.Write(this.m_scope);
					break;
				case MemberName.TotalGroupingExpressionCount:
					writer.Write(this.m_totalGroupingExpressionCount);
					break;
				case MemberName.IsScopedInEvaluationScope:
					writer.Write(this.m_isScopedInEvaluationScope);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000E3E58 File Offset: 0x000E2058
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RunningValueInfo.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.Scope:
					this.m_scope = reader.ReadString();
					break;
				case MemberName.TotalGroupingExpressionCount:
					this.m_totalGroupingExpressionCount = reader.ReadInt32();
					break;
				case MemberName.IsScopedInEvaluationScope:
					this.m_isScopedInEvaluationScope = reader.ReadBoolean();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000E3EDF File Offset: 0x000E20DF
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000E3EEC File Offset: 0x000E20EC
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RunningValueInfo;
		}

		// Token: 0x040019B6 RID: 6582
		private string m_scope;

		// Token: 0x040019B7 RID: 6583
		private int m_totalGroupingExpressionCount;

		// Token: 0x040019B8 RID: 6584
		private bool m_isScopedInEvaluationScope;

		// Token: 0x040019B9 RID: 6585
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = RunningValueInfo.GetDeclaration();
	}
}
