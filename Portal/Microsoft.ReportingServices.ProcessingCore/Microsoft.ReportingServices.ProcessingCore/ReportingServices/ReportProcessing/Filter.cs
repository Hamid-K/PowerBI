using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006D5 RID: 1749
	[Serializable]
	internal sealed class Filter
	{
		// Token: 0x17002131 RID: 8497
		// (get) Token: 0x06005E89 RID: 24201 RVA: 0x001805A6 File Offset: 0x0017E7A6
		// (set) Token: 0x06005E8A RID: 24202 RVA: 0x001805AE File Offset: 0x0017E7AE
		internal ExpressionInfo Expression
		{
			get
			{
				return this.m_expression;
			}
			set
			{
				this.m_expression = value;
			}
		}

		// Token: 0x17002132 RID: 8498
		// (get) Token: 0x06005E8B RID: 24203 RVA: 0x001805B7 File Offset: 0x0017E7B7
		// (set) Token: 0x06005E8C RID: 24204 RVA: 0x001805BF File Offset: 0x0017E7BF
		internal Filter.Operators Operator
		{
			get
			{
				return this.m_operator;
			}
			set
			{
				this.m_operator = value;
			}
		}

		// Token: 0x17002133 RID: 8499
		// (get) Token: 0x06005E8D RID: 24205 RVA: 0x001805C8 File Offset: 0x0017E7C8
		// (set) Token: 0x06005E8E RID: 24206 RVA: 0x001805D0 File Offset: 0x0017E7D0
		internal ExpressionInfoList Values
		{
			get
			{
				return this.m_values;
			}
			set
			{
				this.m_values = value;
			}
		}

		// Token: 0x17002134 RID: 8500
		// (get) Token: 0x06005E8F RID: 24207 RVA: 0x001805D9 File Offset: 0x0017E7D9
		// (set) Token: 0x06005E90 RID: 24208 RVA: 0x001805E1 File Offset: 0x0017E7E1
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

		// Token: 0x17002135 RID: 8501
		// (get) Token: 0x06005E91 RID: 24209 RVA: 0x001805EA File Offset: 0x0017E7EA
		internal FilterExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06005E92 RID: 24210 RVA: 0x001805F4 File Offset: 0x0017E7F4
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.FilterStart();
			if (this.m_expression != null)
			{
				this.m_expression.Initialize("FilterExpression", context);
				context.ExprHostBuilder.FilterExpression(this.m_expression);
			}
			if (this.m_values != null)
			{
				for (int i = 0; i < this.m_values.Count; i++)
				{
					ExpressionInfo expressionInfo = this.m_values[i];
					Global.Tracer.Assert(expressionInfo != null);
					expressionInfo.Initialize("FilterValue", context);
					context.ExprHostBuilder.FilterValue(expressionInfo);
				}
			}
			this.m_exprHostID = context.ExprHostBuilder.FilterEnd();
		}

		// Token: 0x06005E93 RID: 24211 RVA: 0x0018069C File Offset: 0x0017E89C
		internal void SetExprHost(IList<FilterExprHost> filterHosts, ObjectModelImpl reportObjectModel)
		{
			if (this.ExprHostID >= 0)
			{
				Global.Tracer.Assert(filterHosts != null && reportObjectModel != null);
				this.m_exprHost = filterHosts[this.ExprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x06005E94 RID: 24212 RVA: 0x001806DC File Offset: 0x0017E8DC
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Expression, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Operator, Token.Enum),
				new MemberInfo(MemberName.Values, ObjectType.ExpressionInfoList),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x0400303B RID: 12347
		private ExpressionInfo m_expression;

		// Token: 0x0400303C RID: 12348
		private Filter.Operators m_operator;

		// Token: 0x0400303D RID: 12349
		private ExpressionInfoList m_values;

		// Token: 0x0400303E RID: 12350
		private int m_exprHostID = -1;

		// Token: 0x0400303F RID: 12351
		[NonSerialized]
		private FilterExprHost m_exprHost;

		// Token: 0x02000CBE RID: 3262
		internal enum Operators
		{
			// Token: 0x04004E6A RID: 20074
			Equal,
			// Token: 0x04004E6B RID: 20075
			Like,
			// Token: 0x04004E6C RID: 20076
			GreaterThan,
			// Token: 0x04004E6D RID: 20077
			GreaterThanOrEqual,
			// Token: 0x04004E6E RID: 20078
			LessThan,
			// Token: 0x04004E6F RID: 20079
			LessThanOrEqual,
			// Token: 0x04004E70 RID: 20080
			TopN,
			// Token: 0x04004E71 RID: 20081
			BottomN,
			// Token: 0x04004E72 RID: 20082
			TopPercent,
			// Token: 0x04004E73 RID: 20083
			BottomPercent,
			// Token: 0x04004E74 RID: 20084
			In,
			// Token: 0x04004E75 RID: 20085
			Between,
			// Token: 0x04004E76 RID: 20086
			NotEqual
		}
	}
}
