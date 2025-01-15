using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002C1 RID: 705
	[DataContract(Name = "NativeFormatExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryNativeFormatExpression : QueryExpression
	{
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x0002A044 File Offset: 0x00028244
		// (set) Token: 0x0600177D RID: 6013 RVA: 0x0002A04C File Offset: 0x0002824C
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x0002A055 File Offset: 0x00028255
		// (set) Token: 0x0600177F RID: 6015 RVA: 0x0002A05D File Offset: 0x0002825D
		[DataMember(IsRequired = true, Order = 2)]
		public string FormatString { get; set; }

		// Token: 0x06001780 RID: 6016 RVA: 0x0002A068 File Offset: 0x00028268
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("NativeFormat(");
			if (this.Expression != null)
			{
				this.Expression.WriteQueryString(w);
			}
			w.Write(", \"");
			w.Write(this.FormatString);
			w.Write("\")");
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0002A0BC File Offset: 0x000282BC
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0002A0C5 File Offset: 0x000282C5
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0002A0CE File Offset: 0x000282CE
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.GetHashCode<QueryExpressionContainer>(this.Expression, null), Hashing.GetHashCode<string>(this.FormatString, null));
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0002A0F8 File Offset: 0x000282F8
		public override bool Equals(QueryExpression other)
		{
			QueryNativeFormatExpression queryNativeFormatExpression = other as QueryNativeFormatExpression;
			bool? flag = Util.AreEqual<QueryNativeFormatExpression>(this, queryNativeFormatExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryNativeFormatExpression.Expression == this.Expression && queryNativeFormatExpression.FormatString == this.FormatString;
		}
	}
}
