using System;
using System.Diagnostics;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Optimization
{
	// Token: 0x020000FB RID: 251
	internal sealed class ResolvedExpressionDataShapeEquivalenceComparer : ResolvedQueryExpressionEquivalenceComparer
	{
		// Token: 0x0600086A RID: 2154 RVA: 0x00021A4C File Offset: 0x0001FC4C
		internal ResolvedExpressionDataShapeEquivalenceComparer(ResolvedQueryDefinitionEquivalenceComparer structureComparer, ResolvedQueryEquivalenceComparerContext context, ITracer tracer)
			: base(structureComparer, context)
		{
			this._tracer = tracer;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00021A60 File Offset: 0x0001FC60
		public override bool Equals(ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			if (this._depth > 100)
			{
				this._tracer.SanitizedTrace(TraceLevel.Warning, "{0}.{1} - Expression has exceeded maximum depth. Stopping comparison.", new string[] { "ResolvedExpressionDataShapeEquivalenceComparer", "Equals" });
				return false;
			}
			this._depth++;
			bool flag = base.Equals(left, right);
			this._depth--;
			return flag;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00021AC4 File Offset: 0x0001FCC4
		public override int GetHashCode(ResolvedQueryExpression obj)
		{
			if (this._depth > 100)
			{
				this._tracer.SanitizedTrace(TraceLevel.Warning, "{0}.{1} - Expression has exceeded maximum depth. Stopping computation.", new string[] { "ResolvedExpressionDataShapeEquivalenceComparer", "GetHashCode" });
				return -48879;
			}
			this._depth++;
			base.GetHashCode(obj);
			this._depth--;
			return -48879;
		}

		// Token: 0x0400044D RID: 1101
		internal const int MaxExpressionDepth = 100;

		// Token: 0x0400044E RID: 1102
		private readonly ITracer _tracer;

		// Token: 0x0400044F RID: 1103
		private int _depth;
	}
}
