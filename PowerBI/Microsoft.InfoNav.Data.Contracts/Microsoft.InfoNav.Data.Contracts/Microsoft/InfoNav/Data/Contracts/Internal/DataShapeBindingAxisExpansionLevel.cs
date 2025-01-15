using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001C9 RID: 457
	[DataContract(Name = "ExpansionLevel", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingAxisExpansionLevel : IEquatable<DataShapeBindingAxisExpansionLevel>
	{
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x00017A6D File Offset: 0x00015C6D
		// (set) Token: 0x06000C1C RID: 3100 RVA: 0x00017A75 File Offset: 0x00015C75
		[DataMember(IsRequired = true, Order = 20)]
		public List<QueryExpressionContainer> Expressions { get; set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x00017A7E File Offset: 0x00015C7E
		// (set) Token: 0x06000C1E RID: 3102 RVA: 0x00017A86 File Offset: 0x00015C86
		[DataMember(IsRequired = true, Order = 30)]
		public ExpansionDefaultState Default { get; set; }

		// Token: 0x06000C1F RID: 3103 RVA: 0x00017A8F File Offset: 0x00015C8F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingAxisExpansionLevel);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00017A9D File Offset: 0x00015C9D
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash<QueryExpressionContainer>(this.Expressions, null), Hashing.GetHashCode<ExpansionDefaultState>(this.Default, null));
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00017ABC File Offset: 0x00015CBC
		public bool Equals(DataShapeBindingAxisExpansionLevel other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingAxisExpansionLevel>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Expressions.SequenceEqual(other.Expressions) && this.Default == other.Default;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00017B08 File Offset: 0x00015D08
		public static bool operator ==(DataShapeBindingAxisExpansionLevel left, DataShapeBindingAxisExpansionLevel right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingAxisExpansionLevel>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00017B35 File Offset: 0x00015D35
		public static bool operator !=(DataShapeBindingAxisExpansionLevel left, DataShapeBindingAxisExpansionLevel right)
		{
			return !(left == right);
		}
	}
}
