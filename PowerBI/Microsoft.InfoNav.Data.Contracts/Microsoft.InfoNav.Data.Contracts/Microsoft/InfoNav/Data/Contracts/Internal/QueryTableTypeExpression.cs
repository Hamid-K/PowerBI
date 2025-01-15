using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002DA RID: 730
	[DataContract(Name = "TableTypeExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryTableTypeExpression : QueryExpression
	{
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0002BBE3 File Offset: 0x00029DE3
		// (set) Token: 0x06001869 RID: 6249 RVA: 0x0002BBEB File Offset: 0x00029DEB
		[DataMember(IsRequired = true, Order = 1)]
		public List<QueryExpressionContainer> Columns { get; set; }

		// Token: 0x0600186A RID: 6250 RVA: 0x0002BBF4 File Offset: 0x00029DF4
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("tableType(");
			if (this.Columns != null)
			{
				using (w.NewSeparatorScope(QueryStringWriter.Separator.Comma))
				{
					foreach (QueryExpressionContainer queryExpressionContainer in this.Columns)
					{
						w.WriteSeparator();
						if (queryExpressionContainer == null)
						{
							w.WriteError();
						}
						else
						{
							w.WriteExpressionAndName(queryExpressionContainer);
						}
					}
				}
			}
			w.Write(')');
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0002BC98 File Offset: 0x00029E98
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0002BCA1 File Offset: 0x00029EA1
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0002BCAA File Offset: 0x00029EAA
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.CombineHash<QueryExpressionContainer>(this.Columns, null));
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0002BCC8 File Offset: 0x00029EC8
		public override bool Equals(QueryExpression other)
		{
			QueryTableTypeExpression queryTableTypeExpression = other as QueryTableTypeExpression;
			bool? flag = Util.AreEqual<QueryTableTypeExpression>(this, queryTableTypeExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return queryTableTypeExpression.Columns.SequenceEqual(this.Columns);
		}
	}
}
