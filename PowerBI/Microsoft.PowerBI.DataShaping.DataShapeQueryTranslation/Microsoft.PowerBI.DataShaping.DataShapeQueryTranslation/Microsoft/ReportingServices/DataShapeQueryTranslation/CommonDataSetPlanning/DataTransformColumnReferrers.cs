using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning
{
	// Token: 0x0200012C RID: 300
	internal sealed class DataTransformColumnReferrers
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0002C159 File Offset: 0x0002A359
		public IReadOnlyList<Expression> Expressions
		{
			get
			{
				return this.EmptyIfNull<Expression>(this.m_expressions);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0002C167 File Offset: 0x0002A367
		public IReadOnlyList<DataMember> Groups
		{
			get
			{
				return this.EmptyIfNull<DataMember>(this.m_groups);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0002C175 File Offset: 0x0002A375
		public IReadOnlyList<Calculation> Calculations
		{
			get
			{
				return this.EmptyIfNull<Calculation>(this.m_calculations);
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0002C183 File Offset: 0x0002A383
		private IReadOnlyList<T> EmptyIfNull<T>(IReadOnlyList<T> list)
		{
			return list ?? Util.EmptyReadOnlyCollection<T>();
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002C18F File Offset: 0x0002A38F
		public void AddExpression(Expression expression)
		{
			Util.AddToLazyList<Expression>(ref this.m_expressions, expression);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002C19D File Offset: 0x0002A39D
		public void AddGroup(DataMember group)
		{
			Util.AddToLazyList<DataMember>(ref this.m_groups, group);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002C1AB File Offset: 0x0002A3AB
		public void AddCalculation(Calculation calculation)
		{
			Util.AddToLazyList<Calculation>(ref this.m_calculations, calculation);
		}

		// Token: 0x040005B1 RID: 1457
		private List<Expression> m_expressions;

		// Token: 0x040005B2 RID: 1458
		private List<DataMember> m_groups;

		// Token: 0x040005B3 RID: 1459
		private List<Calculation> m_calculations;
	}
}
