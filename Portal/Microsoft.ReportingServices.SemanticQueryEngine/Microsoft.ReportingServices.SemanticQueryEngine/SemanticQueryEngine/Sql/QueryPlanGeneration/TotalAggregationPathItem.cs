using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x0200006A RID: 106
	internal sealed class TotalAggregationPathItem : IPathItem
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x00015103 File Offset: 0x00013303
		internal TotalAggregationPathItem(IQueryEntity targetEntity)
		{
			if (targetEntity == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("targetEntity"));
			}
			this.m_targetEntity = targetEntity;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00004B5D File Offset: 0x00002D5D
		Cardinality IPathItem.Cardinality
		{
			[DebuggerStepThrough]
			get
			{
				return Cardinality.Many;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00004B5D File Offset: 0x00002D5D
		Optionality IPathItem.Optionality
		{
			[DebuggerStepThrough]
			get
			{
				return Optionality.Required;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00004555 File Offset: 0x00002755
		Cardinality IPathItem.ReverseCardinality
		{
			[DebuggerStepThrough]
			get
			{
				return Cardinality.One;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00004B5D File Offset: 0x00002D5D
		Optionality IPathItem.ReverseOptionality
		{
			[DebuggerStepThrough]
			get
			{
				return Optionality.Required;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00015125 File Offset: 0x00013325
		IQueryEntity IPathItem.TargetEntity
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_targetEntity;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0000334E File Offset: 0x0000154E
		IQueryEntity IPathItem.SourceEntity
		{
			[DebuggerStepThrough]
			get
			{
				return null;
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001512D File Offset: 0x0001332D
		public override string ToString()
		{
			return "[TA]";
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00015134 File Offset: 0x00013334
		public override bool Equals(object obj)
		{
			return obj is TotalAggregationPathItem && ((TotalAggregationPathItem)obj).m_targetEntity == this.m_targetEntity;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00015153 File Offset: 0x00013353
		public override int GetHashCode()
		{
			return this.m_targetEntity.GetHashCode();
		}

		// Token: 0x04000202 RID: 514
		private readonly IQueryEntity m_targetEntity;
	}
}
