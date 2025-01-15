using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002C9 RID: 713
	[DataContract(Name = "PercentileExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryPercentileExpression : QueryExpression
	{
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0002A658 File Offset: 0x00028858
		// (set) Token: 0x060017BF RID: 6079 RVA: 0x0002A665 File Offset: 0x00028865
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public bool Exclusive
		{
			get
			{
				return this.PercentileCore.Exclusive;
			}
			set
			{
				this.PercentileCore.Exclusive = value;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x0002A673 File Offset: 0x00028873
		// (set) Token: 0x060017C1 RID: 6081 RVA: 0x0002A680 File Offset: 0x00028880
		[DataMember(IsRequired = true, Order = 2)]
		public double K
		{
			get
			{
				return this.PercentileCore.K;
			}
			set
			{
				this.PercentileCore.K = value;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x0002A68E File Offset: 0x0002888E
		private QueryPercentile PercentileCore
		{
			get
			{
				if (this._percentileCore == null)
				{
					this._percentileCore = new QueryPercentile();
				}
				return this._percentileCore;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x0002A6A9 File Offset: 0x000288A9
		// (set) Token: 0x060017C4 RID: 6084 RVA: 0x0002A6B1 File Offset: 0x000288B1
		[DataMember(IsRequired = true, Order = 3)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x060017C5 RID: 6085 RVA: 0x0002A6BC File Offset: 0x000288BC
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("percentile(");
			w.Write(this.Exclusive ? "exclusive, " : "inclusive, ");
			w.Write(this.K.ToString(CultureInfo.InvariantCulture));
			w.Write(", ");
			this.Expression.WriteQueryString(w);
			w.Write(')');
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0002A728 File Offset: 0x00028928
		public override bool Equals(QueryExpression other)
		{
			QueryPercentileExpression queryPercentileExpression = other as QueryPercentileExpression;
			bool? flag = Util.AreEqual<QueryPercentileExpression>(this, queryPercentileExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryPercentileExpression.PercentileCore.Equals(this.PercentileCore) && queryPercentileExpression.Expression == this.Expression;
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0002A77B File Offset: 0x0002897B
		public override int GetHashCode()
		{
			return Hashing.CombineHash((this.Expression == null) ? base.GetType().GetHashCode() : this.Expression.GetHashCode(), this.PercentileCore.GetHashCode());
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0002A7B3 File Offset: 0x000289B3
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x0002A7BC File Offset: 0x000289BC
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000873 RID: 2163
		private QueryPercentile _percentileCore;
	}
}
