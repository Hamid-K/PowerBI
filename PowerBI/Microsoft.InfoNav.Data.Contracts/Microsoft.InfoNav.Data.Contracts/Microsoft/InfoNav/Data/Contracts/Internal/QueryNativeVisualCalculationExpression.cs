using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002C3 RID: 707
	[DataContract(Name = "NativeVisualCalculationExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryNativeVisualCalculationExpression : QueryExpression
	{
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x0002A249 File Offset: 0x00028449
		// (set) Token: 0x06001791 RID: 6033 RVA: 0x0002A251 File Offset: 0x00028451
		[DataMember(IsRequired = true, Order = 0)]
		public string Expression { get; set; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x0002A25A File Offset: 0x0002845A
		// (set) Token: 0x06001793 RID: 6035 RVA: 0x0002A262 File Offset: 0x00028462
		[DataMember(IsRequired = true, Order = 1)]
		public string Language { get; set; }

		// Token: 0x06001794 RID: 6036 RVA: 0x0002A26B File Offset: 0x0002846B
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0002A274 File Offset: 0x00028474
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0002A280 File Offset: 0x00028480
		public override bool Equals(QueryExpression other)
		{
			QueryNativeVisualCalculationExpression queryNativeVisualCalculationExpression = other as QueryNativeVisualCalculationExpression;
			bool? flag = Util.AreEqual<QueryNativeVisualCalculationExpression>(this, queryNativeVisualCalculationExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryNativeVisualCalculationExpression.Language == this.Language && queryNativeVisualCalculationExpression.Expression == this.Expression;
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0002A2D3 File Offset: 0x000284D3
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.GetHashCode<string>(this.Language, null), Hashing.GetHashCode<string>(this.Expression, null));
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0002A2FD File Offset: 0x000284FD
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("NativeVisualCalculation(\"");
			w.Write(this.Language);
			w.Write("\", \"");
			w.Write(this.Expression);
			w.Write("\")");
		}
	}
}
