using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200029F RID: 671
	[DataContract(Name = "Definition", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryDefinition : IEquatable<QueryDefinition>
	{
		// Token: 0x06001428 RID: 5160 RVA: 0x000240A8 File Offset: 0x000222A8
		public QueryDefinition()
		{
			this.From = new List<EntitySource>();
			this.Where = new List<QueryFilter>();
			this.OrderBy = new List<QuerySortClause>();
			this.Select = new List<QueryExpressionContainer>();
			this.GroupBy = new List<QueryExpressionContainer>();
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x000240E7 File Offset: 0x000222E7
		// (set) Token: 0x0600142A RID: 5162 RVA: 0x000240EF File Offset: 0x000222EF
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public int? Version { get; set; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x000240F8 File Offset: 0x000222F8
		// (set) Token: 0x0600142C RID: 5164 RVA: 0x00024100 File Offset: 0x00022300
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public string DatabaseName { get; set; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x00024109 File Offset: 0x00022309
		// (set) Token: 0x0600142E RID: 5166 RVA: 0x00024111 File Offset: 0x00022311
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public List<QueryExpressionContainer> Parameters { get; set; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0002411A File Offset: 0x0002231A
		// (set) Token: 0x06001430 RID: 5168 RVA: 0x00024122 File Offset: 0x00022322
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public List<QueryExpressionContainer> Let { get; set; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0002412B File Offset: 0x0002232B
		// (set) Token: 0x06001432 RID: 5170 RVA: 0x00024146 File Offset: 0x00022346
		[DataMember(IsRequired = false, Order = 4)]
		public List<EntitySource> From
		{
			get
			{
				if (this._from == null)
				{
					this._from = new List<EntitySource>();
				}
				return this._from;
			}
			set
			{
				this._from = value;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0002414F File Offset: 0x0002234F
		// (set) Token: 0x06001434 RID: 5172 RVA: 0x00024157 File Offset: 0x00022357
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 5)]
		public List<QueryFilter> Where { get; set; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x00024160 File Offset: 0x00022360
		// (set) Token: 0x06001436 RID: 5174 RVA: 0x00024168 File Offset: 0x00022368
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 6)]
		public List<QuerySortClause> OrderBy { get; set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x00024171 File Offset: 0x00022371
		// (set) Token: 0x06001438 RID: 5176 RVA: 0x00024179 File Offset: 0x00022379
		[DataMember(IsRequired = true, Order = 7)]
		public List<QueryExpressionContainer> Select { get; set; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x00024182 File Offset: 0x00022382
		// (set) Token: 0x0600143A RID: 5178 RVA: 0x0002418A File Offset: 0x0002238A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 8)]
		public List<QueryAxis> VisualShape { get; set; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00024193 File Offset: 0x00022393
		// (set) Token: 0x0600143C RID: 5180 RVA: 0x0002419B File Offset: 0x0002239B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 9)]
		public List<QueryExpressionContainer> GroupBy { get; set; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x000241A4 File Offset: 0x000223A4
		// (set) Token: 0x0600143E RID: 5182 RVA: 0x000241AC File Offset: 0x000223AC
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public List<QueryTransform> Transform { get; set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x000241B5 File Offset: 0x000223B5
		// (set) Token: 0x06001440 RID: 5184 RVA: 0x000241BD File Offset: 0x000223BD
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 11)]
		public long? Skip { get; set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x000241C6 File Offset: 0x000223C6
		// (set) Token: 0x06001442 RID: 5186 RVA: 0x000241CE File Offset: 0x000223CE
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 12)]
		public int? Top { get; set; }

		// Token: 0x06001443 RID: 5187 RVA: 0x000241D7 File Offset: 0x000223D7
		public override string ToString()
		{
			return this.ToString(false, false, null);
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x000241E2 File Offset: 0x000223E2
		public string ToTraceString()
		{
			return this.ToString(false, true, null);
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x000241ED File Offset: 0x000223ED
		public bool Equals(QueryDefinition other)
		{
			return other != null && (this == other || string.Equals(this.ToString(), other.ToString(), StringComparison.Ordinal));
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0002420C File Offset: 0x0002240C
		public override bool Equals(object other)
		{
			return this.Equals(other as QueryDefinition);
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0002421A File Offset: 0x0002241A
		public static bool operator ==(QueryDefinition left, QueryDefinition right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x00024230 File Offset: 0x00022430
		public static bool operator !=(QueryDefinition left, QueryDefinition right)
		{
			return !(left == right);
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0002423C File Offset: 0x0002243C
		internal static QueryDefinition FromJsonString(string s)
		{
			return QueryDefinition._jsonSerializer.FromJsonString(s);
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x00024249 File Offset: 0x00022449
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00024256 File Offset: 0x00022456
		internal QueryDefinition Clone()
		{
			return QueryDefinition.FromJsonString(QueryDefinition._jsonSerializer.ToJsonString(this));
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x00024268 File Offset: 0x00022468
		internal string ToString(bool emitExpressionNames, bool traceString, string[] filterRestatements = null)
		{
			QueryStringWriter queryStringWriter = new QueryStringWriter(emitExpressionNames, traceString);
			this.WriteQueryString(queryStringWriter, filterRestatements);
			return queryStringWriter.ToString();
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0002428B File Offset: 0x0002248B
		internal string ToJsonString()
		{
			return QueryDefinition._jsonSerializer.ToJsonString(this);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x00024298 File Offset: 0x00022498
		internal void WriteQueryString(QueryStringWriter w, string[] filterRestatements = null)
		{
			using (w.NewSeparatorScope(QueryStringWriter.Separator.Newline))
			{
				QueryStringWriterUtils.WriteParameters(this.Parameters, w);
				QueryStringWriterUtils.WriteLet(this.Let, w);
				QueryStringWriterUtils.WriteFrom(this.From, w);
				QueryStringWriterUtils.WriteWhere(this.Where, w, filterRestatements);
				this.WriteTransform(w);
				this.WriteOrderBy(w);
				this.WriteSelect(w);
				QueryStringWriterUtils.WriteVisualShape(this.VisualShape, w);
				this.WriteGroupBy(w);
				this.WriteSkip(w);
				this.WriteTop(w);
			}
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x00024330 File Offset: 0x00022530
		private void WriteTransform(QueryStringWriter w)
		{
			if (this.Transform == null || this.Transform.Count == 0)
			{
				return;
			}
			w.WriteSeparator();
			using (w.NewSeparatorScope(QueryStringWriter.Separator.Newline))
			{
				foreach (QueryTransform queryTransform in this.Transform)
				{
					w.WriteSeparator();
					if (queryTransform == null || !QueryDefinitionValidator.IsValid(queryTransform))
					{
						using (w.NewClauseScope("transform", QueryStringWriter.Separator.Newline))
						{
							w.WriteError();
							continue;
						}
					}
					queryTransform.WriteQueryString(w);
				}
			}
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x000243FC File Offset: 0x000225FC
		private void WriteOrderBy(QueryStringWriter w)
		{
			if (this.OrderBy == null || this.OrderBy.Count == 0)
			{
				return;
			}
			w.WriteSeparator();
			using (w.NewClauseScope("orderby", QueryStringWriter.Separator.Comma))
			{
				foreach (QuerySortClause querySortClause in this.OrderBy)
				{
					w.WriteSeparator();
					if (querySortClause == null || !QueryDefinitionValidator.IsValid(querySortClause))
					{
						w.WriteError();
					}
					else
					{
						querySortClause.WriteQueryString(w);
					}
				}
			}
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x000244A8 File Offset: 0x000226A8
		private void WriteSelect(QueryStringWriter w)
		{
			if (this.Select == null || this.Select.Count == 0)
			{
				return;
			}
			w.WriteSeparator();
			using (w.NewClauseScope("select", QueryStringWriter.Separator.Comma))
			{
				foreach (QueryExpressionContainer queryExpressionContainer in this.Select)
				{
					w.WriteSeparator();
					if (queryExpressionContainer == null || !QueryExpressionValidator.IsValid(queryExpressionContainer))
					{
						w.WriteError();
					}
					else
					{
						queryExpressionContainer.WriteQueryString(w);
					}
				}
			}
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0002455C File Offset: 0x0002275C
		private void WriteGroupBy(QueryStringWriter w)
		{
			if (this.GroupBy == null || this.GroupBy.Count == 0)
			{
				return;
			}
			w.WriteSeparator();
			using (w.NewClauseScope("groupby", QueryStringWriter.Separator.Comma))
			{
				foreach (QueryExpressionContainer queryExpressionContainer in this.GroupBy)
				{
					w.WriteSeparator();
					if (queryExpressionContainer == null || !QueryExpressionValidator.IsValid(queryExpressionContainer))
					{
						w.WriteError();
					}
					else
					{
						queryExpressionContainer.WriteQueryString(w);
					}
				}
			}
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x00024610 File Offset: 0x00022810
		private void WriteTop(QueryStringWriter w)
		{
			if (this.Top == null)
			{
				return;
			}
			w.WriteSeparator();
			w.Write("top " + this.Top.Value.ToString());
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0002465C File Offset: 0x0002285C
		private void WriteSkip(QueryStringWriter w)
		{
			if (this.Skip == null)
			{
				return;
			}
			w.WriteSeparator();
			w.Write("skip " + this.Skip.Value.ToString());
		}

		// Token: 0x04000831 RID: 2097
		private static readonly DataContractJsonSerializer _jsonSerializer = new DataContractJsonSerializer(typeof(QueryDefinition));

		// Token: 0x04000832 RID: 2098
		private List<EntitySource> _from;
	}
}
