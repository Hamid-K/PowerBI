using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200027F RID: 639
	[DataContract(Name = "ArithmeticExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryArithmeticExpression : QueryBinaryExpression
	{
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x00022B8A File Offset: 0x00020D8A
		// (set) Token: 0x06001350 RID: 4944 RVA: 0x00022B92 File Offset: 0x00020D92
		[DataMember(IsRequired = true, Order = 10)]
		public QueryArithmeticOperatorKind Operator { get; set; }

		// Token: 0x06001351 RID: 4945 RVA: 0x00022B9C File Offset: 0x00020D9C
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write('(');
			base.Left.WriteQueryString(w);
			w.Write(' ');
			switch (this.Operator)
			{
			case QueryArithmeticOperatorKind.Add:
				w.Write('+');
				break;
			case QueryArithmeticOperatorKind.Subtract:
				w.Write('-');
				break;
			case QueryArithmeticOperatorKind.Multiply:
				w.Write("*");
				break;
			case QueryArithmeticOperatorKind.Divide:
				w.Write('/');
				break;
			default:
				w.Write("unk");
				break;
			}
			w.Write(' ');
			base.Right.WriteQueryString(w);
			w.Write(')');
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00022C36 File Offset: 0x00020E36
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00022C3F File Offset: 0x00020E3F
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00022C48 File Offset: 0x00020E48
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.Operator.GetHashCode(), base.GetHashCode());
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00022C80 File Offset: 0x00020E80
		public override bool Equals(QueryExpression other)
		{
			QueryArithmeticExpression queryArithmeticExpression = other as QueryArithmeticExpression;
			bool? flag = Util.AreEqual<QueryArithmeticExpression>(this, queryArithmeticExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryArithmeticExpression.Operator.Equals(this.Operator) && base.Equals(other);
		}
	}
}
