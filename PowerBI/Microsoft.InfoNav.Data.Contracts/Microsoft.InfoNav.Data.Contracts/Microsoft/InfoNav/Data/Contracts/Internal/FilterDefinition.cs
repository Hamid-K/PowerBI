using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000273 RID: 627
	[DataContract(Name = "FilterDefinition", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class FilterDefinition : IEquatable<FilterDefinition>
	{
		// Token: 0x0600130E RID: 4878 RVA: 0x00022390 File Offset: 0x00020590
		public FilterDefinition()
		{
			this.From = new List<EntitySource>();
			this.Where = new List<QueryFilter>();
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x000223AE File Offset: 0x000205AE
		// (set) Token: 0x06001310 RID: 4880 RVA: 0x000223B6 File Offset: 0x000205B6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public int? Version { get; set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x000223BF File Offset: 0x000205BF
		// (set) Token: 0x06001312 RID: 4882 RVA: 0x000223C7 File Offset: 0x000205C7
		[DataMember(IsRequired = true, Order = 1)]
		public List<EntitySource> From { get; set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x000223D0 File Offset: 0x000205D0
		// (set) Token: 0x06001314 RID: 4884 RVA: 0x000223D8 File Offset: 0x000205D8
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public List<QueryFilter> Where { get; set; }

		// Token: 0x06001315 RID: 4885 RVA: 0x000223E4 File Offset: 0x000205E4
		public bool Equals(FilterDefinition other)
		{
			bool? flag = Util.AreEqual<FilterDefinition>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.From.SequenceEqual(other.From) && this.Where.SequenceEqual(other.Where);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00022430 File Offset: 0x00020630
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FilterDefinition);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0002243E File Offset: 0x0002063E
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash<EntitySource>(this.From, null), Hashing.CombineHash<QueryFilter>(this.Where, null));
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00022460 File Offset: 0x00020660
		public static bool operator ==(FilterDefinition left, FilterDefinition right)
		{
			bool? flag = Util.AreEqual<FilterDefinition>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0002248D File Offset: 0x0002068D
		public static bool operator !=(FilterDefinition left, FilterDefinition right)
		{
			return !(left == right);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0002249C File Offset: 0x0002069C
		internal void WriteQueryString(QueryStringWriter w)
		{
			using (w.NewSeparatorScope(QueryStringWriter.Separator.Newline))
			{
				QueryStringWriterUtils.WriteFrom(this.From, w);
				QueryStringWriterUtils.WriteWhere(this.Where, w, null);
			}
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x000224E8 File Offset: 0x000206E8
		internal string ToString(bool emitExpressionNames, bool traceString)
		{
			QueryStringWriter queryStringWriter = new QueryStringWriter(emitExpressionNames, traceString);
			this.WriteQueryString(queryStringWriter);
			return queryStringWriter.ToString();
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0002250A File Offset: 0x0002070A
		public override string ToString()
		{
			return this.ToString(false, false);
		}
	}
}
