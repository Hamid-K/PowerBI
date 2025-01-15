using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000708 RID: 1800
	[Serializable]
	internal class DataAggregateInfo
	{
		// Token: 0x1700239A RID: 9114
		// (get) Token: 0x06006488 RID: 25736 RVA: 0x0018DDF7 File Offset: 0x0018BFF7
		// (set) Token: 0x06006489 RID: 25737 RVA: 0x0018DDFF File Offset: 0x0018BFFF
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

		// Token: 0x1700239B RID: 9115
		// (get) Token: 0x0600648A RID: 25738 RVA: 0x0018DE08 File Offset: 0x0018C008
		// (set) Token: 0x0600648B RID: 25739 RVA: 0x0018DE10 File Offset: 0x0018C010
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

		// Token: 0x1700239C RID: 9116
		// (get) Token: 0x0600648C RID: 25740 RVA: 0x0018DE19 File Offset: 0x0018C019
		// (set) Token: 0x0600648D RID: 25741 RVA: 0x0018DE21 File Offset: 0x0018C021
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

		// Token: 0x1700239D RID: 9117
		// (get) Token: 0x0600648E RID: 25742 RVA: 0x0018DE2A File Offset: 0x0018C02A
		// (set) Token: 0x0600648F RID: 25743 RVA: 0x0018DE32 File Offset: 0x0018C032
		internal StringList DuplicateNames
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

		// Token: 0x1700239E RID: 9118
		// (get) Token: 0x06006490 RID: 25744 RVA: 0x0018DE3B File Offset: 0x0018C03B
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

		// Token: 0x1700239F RID: 9119
		// (get) Token: 0x06006491 RID: 25745 RVA: 0x0018DE63 File Offset: 0x0018C063
		internal AggregateParamExprHost[] ExpressionHosts
		{
			get
			{
				return this.m_expressionHosts;
			}
		}

		// Token: 0x170023A0 RID: 9120
		// (get) Token: 0x06006492 RID: 25746 RVA: 0x0018DE6B File Offset: 0x0018C06B
		// (set) Token: 0x06006493 RID: 25747 RVA: 0x0018DE73 File Offset: 0x0018C073
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

		// Token: 0x170023A1 RID: 9121
		// (get) Token: 0x06006494 RID: 25748 RVA: 0x0018DE7C File Offset: 0x0018C07C
		// (set) Token: 0x06006495 RID: 25749 RVA: 0x0018DE84 File Offset: 0x0018C084
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

		// Token: 0x170023A2 RID: 9122
		// (get) Token: 0x06006496 RID: 25750 RVA: 0x0018DE8D File Offset: 0x0018C08D
		// (set) Token: 0x06006497 RID: 25751 RVA: 0x0018DE95 File Offset: 0x0018C095
		internal bool IsCopied
		{
			get
			{
				return this.m_isCopied;
			}
			set
			{
				this.m_isCopied = value;
			}
		}

		// Token: 0x170023A3 RID: 9123
		// (get) Token: 0x06006498 RID: 25752 RVA: 0x0018DE9E File Offset: 0x0018C09E
		internal bool SuppressExceptions
		{
			get
			{
				return this.m_suppressExceptions;
			}
		}

		// Token: 0x170023A4 RID: 9124
		// (get) Token: 0x06006499 RID: 25753 RVA: 0x0018DEA6 File Offset: 0x0018C0A6
		// (set) Token: 0x0600649A RID: 25754 RVA: 0x0018DEAE File Offset: 0x0018C0AE
		internal List<string> FieldsUsedInValueExpression
		{
			get
			{
				return this.m_fieldsUsedInValueExpression;
			}
			set
			{
				this.m_fieldsUsedInValueExpression = value;
			}
		}

		// Token: 0x0600649B RID: 25755 RVA: 0x0018DEB8 File Offset: 0x0018C0B8
		internal DataAggregateInfo DeepClone(InitializationContext context)
		{
			DataAggregateInfo dataAggregateInfo = new DataAggregateInfo();
			this.DeepCloneInternal(dataAggregateInfo, context);
			return dataAggregateInfo;
		}

		// Token: 0x0600649C RID: 25756 RVA: 0x0018DED4 File Offset: 0x0018C0D4
		protected void DeepCloneInternal(DataAggregateInfo clone, InitializationContext context)
		{
			clone.m_name = context.GenerateAggregateID(this.m_name);
			clone.m_aggregateType = this.m_aggregateType;
			if (this.m_expressions != null)
			{
				int num = this.m_expressions.Length;
				clone.m_expressions = new ExpressionInfo[num];
				for (int i = 0; i < num; i++)
				{
					clone.m_expressions[i] = this.m_expressions[i].DeepClone(context);
				}
			}
			Global.Tracer.Assert(this.m_duplicateNames == null);
			clone.m_recursive = this.m_recursive;
			clone.m_isCopied = false;
			clone.m_suppressExceptions = true;
			if (this.m_hasScope)
			{
				clone.SetScope(context.EscalateScope(this.m_scope));
			}
		}

		// Token: 0x0600649D RID: 25757 RVA: 0x0018DF87 File Offset: 0x0018C187
		internal void SetScope(string scope)
		{
			this.m_hasScope = true;
			this.m_scope = scope;
		}

		// Token: 0x0600649E RID: 25758 RVA: 0x0018DF97 File Offset: 0x0018C197
		internal bool GetScope(out string scope)
		{
			scope = this.m_scope;
			return this.m_hasScope;
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x0018DFA8 File Offset: 0x0018C1A8
		internal void SetExprHosts(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
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

		// Token: 0x060064A0 RID: 25760 RVA: 0x0018E085 File Offset: 0x0018C285
		internal bool IsPostSortAggregate()
		{
			return this.m_aggregateType == DataAggregateInfo.AggregateTypes.First || DataAggregateInfo.AggregateTypes.Last == this.m_aggregateType || DataAggregateInfo.AggregateTypes.Previous == this.m_aggregateType;
		}

		// Token: 0x060064A1 RID: 25761 RVA: 0x0018E0A8 File Offset: 0x0018C2A8
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.AggregateType, Token.Enum),
				new MemberInfo(MemberName.Expressions, Token.Array, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DuplicateNames, ObjectType.StringList)
			});
		}

		// Token: 0x04003269 RID: 12905
		private string m_name;

		// Token: 0x0400326A RID: 12906
		private DataAggregateInfo.AggregateTypes m_aggregateType;

		// Token: 0x0400326B RID: 12907
		private ExpressionInfo[] m_expressions;

		// Token: 0x0400326C RID: 12908
		private StringList m_duplicateNames;

		// Token: 0x0400326D RID: 12909
		[NonSerialized]
		private string m_scope;

		// Token: 0x0400326E RID: 12910
		[NonSerialized]
		private bool m_hasScope;

		// Token: 0x0400326F RID: 12911
		[NonSerialized]
		private bool m_recursive;

		// Token: 0x04003270 RID: 12912
		[NonSerialized]
		private bool m_isCopied;

		// Token: 0x04003271 RID: 12913
		[NonSerialized]
		private AggregateParamExprHost[] m_expressionHosts;

		// Token: 0x04003272 RID: 12914
		[NonSerialized]
		private bool m_exprHostInitialized;

		// Token: 0x04003273 RID: 12915
		[NonSerialized]
		private ObjectModelImpl m_exprHostReportObjectModel;

		// Token: 0x04003274 RID: 12916
		[NonSerialized]
		private bool m_suppressExceptions;

		// Token: 0x04003275 RID: 12917
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x02000CD9 RID: 3289
		internal enum AggregateTypes
		{
			// Token: 0x04004F09 RID: 20233
			First,
			// Token: 0x04004F0A RID: 20234
			Last,
			// Token: 0x04004F0B RID: 20235
			Sum,
			// Token: 0x04004F0C RID: 20236
			Avg,
			// Token: 0x04004F0D RID: 20237
			Max,
			// Token: 0x04004F0E RID: 20238
			Min,
			// Token: 0x04004F0F RID: 20239
			CountDistinct,
			// Token: 0x04004F10 RID: 20240
			CountRows,
			// Token: 0x04004F11 RID: 20241
			Count,
			// Token: 0x04004F12 RID: 20242
			StDev,
			// Token: 0x04004F13 RID: 20243
			Var,
			// Token: 0x04004F14 RID: 20244
			StDevP,
			// Token: 0x04004F15 RID: 20245
			VarP,
			// Token: 0x04004F16 RID: 20246
			Aggregate,
			// Token: 0x04004F17 RID: 20247
			Previous
		}
	}
}
