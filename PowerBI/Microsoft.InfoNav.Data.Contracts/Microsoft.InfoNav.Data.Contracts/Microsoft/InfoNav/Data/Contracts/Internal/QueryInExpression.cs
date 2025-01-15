using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002B9 RID: 697
	[DataContract(Name = "InExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryInExpression : QueryExpression
	{
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x00029867 File Offset: 0x00027A67
		// (set) Token: 0x0600173A RID: 5946 RVA: 0x0002986F File Offset: 0x00027A6F
		[DataMember(IsRequired = true, Order = 1)]
		public List<QueryExpressionContainer> Expressions { get; set; }

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x00029878 File Offset: 0x00027A78
		// (set) Token: 0x0600173C RID: 5948 RVA: 0x00029880 File Offset: 0x00027A80
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public List<List<QueryExpressionContainer>> Values { get; set; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00029889 File Offset: 0x00027A89
		// (set) Token: 0x0600173E RID: 5950 RVA: 0x00029891 File Offset: 0x00027A91
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public QueryExpressionContainer Table { get; set; }

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x0002989A File Offset: 0x00027A9A
		// (set) Token: 0x06001740 RID: 5952 RVA: 0x000298A2 File Offset: 0x00027AA2
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
		public QueryEqualitySemanticsKind? EqualityKind { get; set; }

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x000298AB File Offset: 0x00027AAB
		public bool HasValues
		{
			get
			{
				return !this.Values.IsNullOrEmpty<List<QueryExpressionContainer>>();
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x000298BB File Offset: 0x00027ABB
		public bool HasTable
		{
			get
			{
				return this.Table != null;
			}
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000298CC File Offset: 0x00027ACC
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.WriteExpressions(this.Expressions);
			w.Write(" in ");
			if (this.HasValues)
			{
				w.Write("(");
				using (w.NewSeparatorScope(QueryStringWriter.Separator.Comma))
				{
					for (int i = 0; i < this.Values.Count; i++)
					{
						w.WriteSeparator();
						w.WriteExpressions(this.Values[i]);
					}
				}
				w.Write(")");
				if (this.EqualityKind != null)
				{
					w.Write(" using " + this.EqualityKind.ToString());
					return;
				}
			}
			else
			{
				this.Table.WriteQueryString(w);
			}
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x000299A4 File Offset: 0x00027BA4
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000299AD File Offset: 0x00027BAD
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x000299B8 File Offset: 0x00027BB8
		public override int GetHashCode()
		{
			int num = Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.CombineHash<QueryExpressionContainer>(this.Expressions, null));
			if (this.HasValues)
			{
				for (int i = 0; i < this.Values.Count; i++)
				{
					num = Hashing.CombineHash(num, Hashing.CombineHash<QueryExpressionContainer>(this.Values[i], null), Hashing.GetHashCode<QueryEqualitySemanticsKind?>(this.EqualityKind, null));
				}
			}
			else
			{
				num = Hashing.CombineHash(num, this.Table.GetHashCode(), Hashing.GetHashCode<QueryEqualitySemanticsKind?>(this.EqualityKind, null));
			}
			return num;
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00029A48 File Offset: 0x00027C48
		public override bool Equals(QueryExpression other)
		{
			QueryInExpression queryInExpression = other as QueryInExpression;
			bool? flag = Util.AreEqual<QueryInExpression>(this, queryInExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			if (!queryInExpression.Expressions.SequenceEqual(this.Expressions))
			{
				return false;
			}
			if (this.HasValues)
			{
				if (!queryInExpression.HasValues)
				{
					return false;
				}
				if (queryInExpression.Values.Count != this.Values.Count)
				{
					return false;
				}
				for (int i = 0; i < this.Values.Count; i++)
				{
					if (!queryInExpression.Values[i].SequenceEqual(this.Values[i]))
					{
						return false;
					}
				}
			}
			else
			{
				if (!queryInExpression.HasTable)
				{
					return false;
				}
				if (!this.Table.Equals(queryInExpression.Table))
				{
					return false;
				}
			}
			QueryEqualitySemanticsKind? equalityKind = this.EqualityKind;
			QueryEqualitySemanticsKind? equalityKind2 = queryInExpression.EqualityKind;
			return (equalityKind.GetValueOrDefault() == equalityKind2.GetValueOrDefault()) & (equalityKind != null == (equalityKind2 != null));
		}
	}
}
