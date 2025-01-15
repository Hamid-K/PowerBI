using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000199 RID: 409
	[DataContract(Name = "Axis", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingAxis : IEquatable<DataShapeBindingAxis>
	{
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00015CF2 File Offset: 0x00013EF2
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x00015CFA File Offset: 0x00013EFA
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public IList<DataShapeBindingAxisGrouping> Groupings { get; set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00015D03 File Offset: 0x00013F03
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x00015D0B File Offset: 0x00013F0B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public DataShapeBindingAxisExpansionState Expansion { get; set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00015D14 File Offset: 0x00013F14
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x00015D1C File Offset: 0x00013F1C
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<DataShapeBindingAxisSynchronizedGroupingBlock> Synchronization { get; set; }

		// Token: 0x06000B10 RID: 2832 RVA: 0x00015D25 File Offset: 0x00013F25
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingAxis);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00015D33 File Offset: 0x00013F33
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash<DataShapeBindingAxisGrouping>(this.Groupings, null), Hashing.GetHashCode<DataShapeBindingAxisExpansionState>(this.Expansion, null), Hashing.CombineHash<DataShapeBindingAxisSynchronizedGroupingBlock>(this.Synchronization, null));
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00015D60 File Offset: 0x00013F60
		public bool Equals(DataShapeBindingAxis other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingAxis>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Groupings.SequenceEqual(other.Groupings) && this.Expansion == other.Expansion && this.Synchronization.SequenceEqual(other.Synchronization);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00015DC0 File Offset: 0x00013FC0
		public static bool operator ==(DataShapeBindingAxis left, DataShapeBindingAxis right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingAxis>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00015DED File Offset: 0x00013FED
		public static bool operator !=(DataShapeBindingAxis left, DataShapeBindingAxis right)
		{
			return !(left == right);
		}
	}
}
