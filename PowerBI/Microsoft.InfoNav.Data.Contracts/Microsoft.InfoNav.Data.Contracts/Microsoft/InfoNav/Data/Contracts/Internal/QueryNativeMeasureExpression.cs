using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002C2 RID: 706
	[DataContract(Name = "NativeMeasureExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryNativeMeasureExpression : QueryExpression
	{
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x0002A153 File Offset: 0x00028353
		// (set) Token: 0x06001787 RID: 6023 RVA: 0x0002A15B File Offset: 0x0002835B
		[DataMember(IsRequired = true, Order = 1)]
		public string Language { get; set; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x0002A164 File Offset: 0x00028364
		// (set) Token: 0x06001789 RID: 6025 RVA: 0x0002A16C File Offset: 0x0002836C
		[DataMember(IsRequired = true, Order = 2)]
		public string Expression { get; set; }

		// Token: 0x0600178A RID: 6026 RVA: 0x0002A175 File Offset: 0x00028375
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0002A17E File Offset: 0x0002837E
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0002A187 File Offset: 0x00028387
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("NativeMeasure(\"");
			w.Write(this.Language);
			w.Write("\", \"");
			w.Write(this.Expression);
			w.Write("\")");
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0002A1C4 File Offset: 0x000283C4
		public override bool Equals(QueryExpression other)
		{
			QueryNativeMeasureExpression queryNativeMeasureExpression = other as QueryNativeMeasureExpression;
			bool? flag = Util.AreEqual<QueryNativeMeasureExpression>(this, queryNativeMeasureExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryNativeMeasureExpression.Language == this.Language && queryNativeMeasureExpression.Expression == this.Expression;
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0002A217 File Offset: 0x00028417
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.GetHashCode<string>(this.Language, null), Hashing.GetHashCode<string>(this.Expression, null));
		}
	}
}
