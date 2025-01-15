using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002B6 RID: 694
	[DataContract(Name = "FloorExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryFloorExpression : QueryExpression
	{
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x0002949C File Offset: 0x0002769C
		// (set) Token: 0x0600171A RID: 5914 RVA: 0x000294A4 File Offset: 0x000276A4
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x000294AD File Offset: 0x000276AD
		// (set) Token: 0x0600171C RID: 5916 RVA: 0x000294B5 File Offset: 0x000276B5
		[DataMember(IsRequired = true, Order = 2)]
		public double Size { get; set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x000294BE File Offset: 0x000276BE
		// (set) Token: 0x0600171E RID: 5918 RVA: 0x000294C6 File Offset: 0x000276C6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public TimeUnit? TimeUnit { get; set; }

		// Token: 0x0600171F RID: 5919 RVA: 0x000294D0 File Offset: 0x000276D0
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("floor(");
			if (this.Expression != null)
			{
				this.Expression.WriteQueryString(w);
			}
			w.Write(", ");
			w.Write(this.Size.ToString(CultureInfo.InvariantCulture));
			if (this.TimeUnit != null)
			{
				w.Write(", ");
				w.Write(this.TimeUnit.ToString().ToLowerInvariant());
			}
			w.Write(')');
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x00029568 File Offset: 0x00027768
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00029571 File Offset: 0x00027771
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0002957C File Offset: 0x0002777C
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.GetHashCode<QueryExpressionContainer>(this.Expression, null), this.Size.GetHashCode(), Hashing.GetHashCode<TimeUnit?>(this.TimeUnit, null));
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x000295C0 File Offset: 0x000277C0
		public override bool Equals(QueryExpression other)
		{
			QueryFloorExpression queryFloorExpression = other as QueryFloorExpression;
			bool? flag = Util.AreEqual<QueryFloorExpression>(this, queryFloorExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			if (queryFloorExpression.Expression == this.Expression && queryFloorExpression.Size == this.Size)
			{
				TimeUnit? timeUnit = queryFloorExpression.TimeUnit;
				TimeUnit? timeUnit2 = this.TimeUnit;
				return (timeUnit.GetValueOrDefault() == timeUnit2.GetValueOrDefault()) & (timeUnit != null == (timeUnit2 != null));
			}
			return false;
		}
	}
}
