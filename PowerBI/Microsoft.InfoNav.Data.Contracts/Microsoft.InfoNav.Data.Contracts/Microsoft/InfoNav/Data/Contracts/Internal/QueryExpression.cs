using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002AA RID: 682
	[DataContract(Name = "Expression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public abstract class QueryExpression : IEquatable<QueryExpression>
	{
		// Token: 0x060014E1 RID: 5345
		internal abstract void WriteQueryString(QueryStringWriter w);

		// Token: 0x060014E2 RID: 5346
		public abstract void Accept(QueryExpressionVisitor visitor);

		// Token: 0x060014E3 RID: 5347
		public abstract T Accept<T>(QueryExpressionVisitor<T> visitor);

		// Token: 0x060014E4 RID: 5348
		public abstract bool Equals(QueryExpression other);

		// Token: 0x060014E5 RID: 5349 RVA: 0x00026546 File Offset: 0x00024746
		public sealed override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0002654F File Offset: 0x0002474F
		internal string ToTraceString()
		{
			return this.ToString(true);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x00026558 File Offset: 0x00024758
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QueryExpression);
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00026566 File Offset: 0x00024766
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.ToString().GetHashCode());
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00026584 File Offset: 0x00024784
		public static bool operator ==(QueryExpression left, QueryExpression right)
		{
			bool? flag = Util.AreEqual<QueryExpression>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x000265B1 File Offset: 0x000247B1
		public static bool operator !=(QueryExpression left, QueryExpression right)
		{
			return !(left == right);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x000265C0 File Offset: 0x000247C0
		private string ToString(bool traceString)
		{
			string text;
			try
			{
				QueryStringWriter queryStringWriter = new QueryStringWriter(false, traceString);
				this.WriteQueryString(queryStringWriter);
				text = queryStringWriter.ToString();
			}
			catch (Exception ex) when (!ex.IsStoppingException())
			{
				if (!traceString)
				{
					throw;
				}
				text = ex.ToString();
			}
			return text;
		}
	}
}
