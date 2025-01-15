using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.PowerBI.Data.ModelSchemaAnalysis
{
	// Token: 0x02000007 RID: 7
	[ImmutableObject(true)]
	public sealed class RatioMeasureCalculationKind : MeasureCalculationKind
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000209D File Offset: 0x0000029D
		public RatioMeasureCalculationKind(MeasureCalculationKind numerator, MeasureCalculationKind denominator, ResolvedQueryExpression denominatorExpression)
			: base(numerator.VariesBy.Union(denominator.VariesBy))
		{
			this.Numerator = numerator;
			this.Denominator = denominator;
			this.DenominatorExpression = denominatorExpression;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020CB File Offset: 0x000002CB
		public MeasureCalculationKind Numerator { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020D3 File Offset: 0x000002D3
		public MeasureCalculationKind Denominator { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020DB File Offset: 0x000002DB
		public ResolvedQueryExpression DenominatorExpression { get; }

		// Token: 0x0600000D RID: 13 RVA: 0x000020E3 File Offset: 0x000002E3
		public override void Accept(MeasureCalculationKindVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
