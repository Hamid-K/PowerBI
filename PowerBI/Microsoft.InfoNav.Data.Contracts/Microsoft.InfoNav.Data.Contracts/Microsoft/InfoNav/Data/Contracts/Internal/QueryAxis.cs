using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000281 RID: 641
	[DataContract(Name = "Axis", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryAxis : IEquatable<QueryAxis>
	{
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x00022CDF File Offset: 0x00020EDF
		// (set) Token: 0x06001358 RID: 4952 RVA: 0x00022CE7 File Offset: 0x00020EE7
		[DataMember(IsRequired = true, Order = 0)]
		public string Name { get; set; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x00022CF0 File Offset: 0x00020EF0
		// (set) Token: 0x0600135A RID: 4954 RVA: 0x00022CF8 File Offset: 0x00020EF8
		[DataMember(IsRequired = true, Order = 1)]
		public List<QueryAxisGroup> Groups { get; set; }

		// Token: 0x0600135B RID: 4955 RVA: 0x00022D04 File Offset: 0x00020F04
		public bool Equals(QueryAxis other)
		{
			bool? flag = Util.AreEqual<QueryAxis>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return QueryNameComparer.Instance.Equals(this.Name, other.Name) && this.Groups.SequenceEqual(other.Groups);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00022D55 File Offset: 0x00020F55
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QueryAxis);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00022D63 File Offset: 0x00020F63
		public override int GetHashCode()
		{
			return Hashing.CombineHash(QueryNameComparer.Instance.GetHashCode(this.Name), Hashing.CombineHash<QueryAxisGroup>(this.Groups, null));
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00022D88 File Offset: 0x00020F88
		public static bool operator ==(QueryAxis left, QueryAxis right)
		{
			bool? flag = Util.AreEqual<QueryAxis>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00022DB5 File Offset: 0x00020FB5
		public static bool operator !=(QueryAxis left, QueryAxis right)
		{
			return !(left == right);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00022DC4 File Offset: 0x00020FC4
		internal void WriteQueryString(QueryStringWriter w)
		{
			QueryStringWriterUtils.WriteFunction<QueryAxisGroup>("Axis", this.Groups, QueryStringWriter.Separator.CommaAndNewline, delegate(QueryAxisGroup group, QueryStringWriter writer)
			{
				if (group == null || !QueryDefinitionValidator.IsValid(group))
				{
					w.WriteError();
					return;
				}
				group.WriteQueryString(w);
			}, w);
			QueryStringWriterUtils.WriteName(this.Name, w);
		}
	}
}
