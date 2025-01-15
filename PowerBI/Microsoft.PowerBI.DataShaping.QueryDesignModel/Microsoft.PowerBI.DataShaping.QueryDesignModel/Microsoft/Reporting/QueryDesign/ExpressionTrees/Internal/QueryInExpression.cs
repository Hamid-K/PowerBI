using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000194 RID: 404
	internal sealed class QueryInExpression : QueryExtensionExpressionBase
	{
		// Token: 0x0600156B RID: 5483 RVA: 0x0003C061 File Offset: 0x0003A261
		internal QueryInExpression(ConceptualResultType conceptualResultType, IReadOnlyList<QueryExpression> expressions, IReadOnlyList<IReadOnlyList<QueryExpression>> values, bool isStrict)
			: base(conceptualResultType)
		{
			this.Expressions = expressions;
			this.Values = values;
			this.IsStrict = isStrict;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0003C080 File Offset: 0x0003A280
		[Conditional("DEBUG")]
		private static void AssertArgumentCounts(IReadOnlyList<QueryExpression> expressions, IReadOnlyList<IReadOnlyList<QueryExpression>> values)
		{
			int count = expressions.Count;
			for (int i = 0; i < values.Count; i++)
			{
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x0003C0A5 File Offset: 0x0003A2A5
		public IReadOnlyList<QueryExpression> Expressions { get; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x0003C0AD File Offset: 0x0003A2AD
		public IReadOnlyList<IReadOnlyList<QueryExpression>> Values { get; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x0003C0B5 File Offset: 0x0003A2B5
		public bool IsStrict { get; }

		// Token: 0x06001570 RID: 5488 RVA: 0x0003C0BD File Offset: 0x0003A2BD
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0003C0C8 File Offset: 0x0003A2C8
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryInExpression queryInExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryInExpression>(this, other, out flag, out queryInExpression))
			{
				return flag;
			}
			return this.Expressions.SequenceEqualReadOnly(queryInExpression.Expressions) && this.Values.SequenceEqualReadOnly(queryInExpression.Values) && this.IsStrict == queryInExpression.IsStrict;
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x0003C11A File Offset: 0x0003A31A
		public bool HasOneTuple
		{
			get
			{
				return this.Values.Count == 1;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x0003C12A File Offset: 0x0003A32A
		public bool CanBePreservedAsTuples
		{
			get
			{
				if (this._exceedsMaxNumberOfTupleValuesWithDefaults == null)
				{
					this._exceedsMaxNumberOfTupleValuesWithDefaults = new bool?(QueryAlgorithms.CanBePreservedAsTuples(this));
				}
				return this._exceedsMaxNumberOfTupleValuesWithDefaults.Value;
			}
		}

		// Token: 0x04000B67 RID: 2919
		private bool? _exceedsMaxNumberOfTupleValuesWithDefaults;
	}
}
