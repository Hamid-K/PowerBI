using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001CB RID: 459
	[DataContract(Name = "ExpansionState", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingAxisExpansionState : IEquatable<DataShapeBindingAxisExpansionState>
	{
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x00017B49 File Offset: 0x00015D49
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x00017B51 File Offset: 0x00015D51
		[DataMember(IsRequired = false, Order = 5)]
		public List<EntitySource> From { get; set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00017B5A File Offset: 0x00015D5A
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x00017B62 File Offset: 0x00015D62
		[DataMember(IsRequired = true, Order = 10)]
		public IList<DataShapeBindingAxisExpansionLevel> Levels { get; set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00017B6B File Offset: 0x00015D6B
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x00017B73 File Offset: 0x00015D73
		[DataMember(IsRequired = true, Order = 20)]
		public DataShapeBindingAxisExpansionInstance Instances { get; set; }

		// Token: 0x06000C2B RID: 3115 RVA: 0x00017B7C File Offset: 0x00015D7C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingAxisExpansionState);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00017B8A File Offset: 0x00015D8A
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash<EntitySource>(this.From, null), Hashing.CombineHash<DataShapeBindingAxisExpansionLevel>(this.Levels, null), Hashing.GetHashCode<DataShapeBindingAxisExpansionInstance>(this.Instances, null));
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00017BB8 File Offset: 0x00015DB8
		public bool Equals(DataShapeBindingAxisExpansionState other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingAxisExpansionState>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.From.SequenceEqual(other.From) && this.Levels.SequenceEqual(other.Levels) && this.Instances == other.Instances;
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00017C18 File Offset: 0x00015E18
		public static bool operator ==(DataShapeBindingAxisExpansionState left, DataShapeBindingAxisExpansionState right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingAxisExpansionState>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00017C45 File Offset: 0x00015E45
		public static bool operator !=(DataShapeBindingAxisExpansionState left, DataShapeBindingAxisExpansionState right)
		{
			return !(left == right);
		}
	}
}
