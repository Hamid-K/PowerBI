using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002D4 RID: 724
	[DataContract(Name = "SparklineDataExpression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QuerySparklineDataExpression : QueryExpression
	{
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x0002B0BC File Offset: 0x000292BC
		// (set) Token: 0x06001820 RID: 6176 RVA: 0x0002B0C4 File Offset: 0x000292C4
		[DataMember(IsRequired = true, Order = 1)]
		public QueryExpressionContainer Measure { get; set; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x0002B0CD File Offset: 0x000292CD
		// (set) Token: 0x06001822 RID: 6178 RVA: 0x0002B0D5 File Offset: 0x000292D5
		[DataMember(IsRequired = true, Order = 2)]
		public List<QueryExpressionContainer> Groupings { get; set; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x0002B0DE File Offset: 0x000292DE
		// (set) Token: 0x06001824 RID: 6180 RVA: 0x0002B0E6 File Offset: 0x000292E6
		[DataMember(IsRequired = true, Order = 3)]
		public int PointsPerSparkline { get; set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x0002B0EF File Offset: 0x000292EF
		// (set) Token: 0x06001826 RID: 6182 RVA: 0x0002B0F7 File Offset: 0x000292F7
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
		public QueryExpressionContainer ScalarKey { get; set; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x0002B100 File Offset: 0x00029300
		// (set) Token: 0x06001828 RID: 6184 RVA: 0x0002B108 File Offset: 0x00029308
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 5)]
		public bool IncludeMinGroupingInterval { get; set; }

		// Token: 0x06001829 RID: 6185 RVA: 0x0002B114 File Offset: 0x00029314
		internal override void WriteQueryString(QueryStringWriter w)
		{
			w.Write("SparklineData(");
			this.Measure.WriteQueryString(w);
			w.Write(", GroupBy: (");
			using (w.NewSeparatorScope(QueryStringWriter.Separator.Comma))
			{
				for (int i = 0; i < this.Groupings.Count; i++)
				{
					w.WriteSeparator();
					this.Groupings[i].WriteQueryString(w);
				}
			}
			w.Write(")");
			w.Write(", ");
			w.Write((long)this.PointsPerSparkline);
			if (this.ScalarKey != null)
			{
				w.Write(", ScalarKey: ");
				this.ScalarKey.WriteQueryString(w);
			}
			if (this.IncludeMinGroupingInterval)
			{
				w.Write(", IncludeMinGroupingInterval");
			}
			w.Write(")");
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0002B1FC File Offset: 0x000293FC
		[DebuggerStepThrough]
		public override void Accept(QueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0002B205 File Offset: 0x00029405
		[DebuggerStepThrough]
		public override T Accept<T>(QueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0002B210 File Offset: 0x00029410
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), Hashing.GetHashCode<QueryExpressionContainer>(this.Measure, null), Hashing.CombineHash<QueryExpressionContainer>(this.Groupings, null), this.PointsPerSparkline.GetHashCode(), Hashing.GetHashCode<QueryExpressionContainer>(this.ScalarKey, null), Hashing.GetHashCode<bool>(this.IncludeMinGroupingInterval, null));
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0002B26C File Offset: 0x0002946C
		public override bool Equals(QueryExpression other)
		{
			QuerySparklineDataExpression querySparklineDataExpression = other as QuerySparklineDataExpression;
			bool? flag = Util.AreEqual<QuerySparklineDataExpression>(this, querySparklineDataExpression);
			if (flag != null)
			{
				return flag.Value;
			}
			return querySparklineDataExpression.Measure == this.Measure && querySparklineDataExpression.Groupings.SequenceEqual(this.Groupings) && querySparklineDataExpression.PointsPerSparkline == this.PointsPerSparkline && querySparklineDataExpression.ScalarKey == this.ScalarKey && querySparklineDataExpression.IncludeMinGroupingInterval == this.IncludeMinGroupingInterval;
		}
	}
}
