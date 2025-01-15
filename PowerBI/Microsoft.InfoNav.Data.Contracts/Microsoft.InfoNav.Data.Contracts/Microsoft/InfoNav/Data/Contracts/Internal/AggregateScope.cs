using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000177 RID: 375
	[DataContract(Name = "AggregateScope", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class AggregateScope : IEquatable<AggregateScope>
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00013CE8 File Offset: 0x00011EE8
		// (set) Token: 0x060009CB RID: 2507 RVA: 0x00013CF0 File Offset: 0x00011EF0
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 10)]
		public int PrimaryDepth { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x00013CF9 File Offset: 0x00011EF9
		// (set) Token: 0x060009CD RID: 2509 RVA: 0x00013D01 File Offset: 0x00011F01
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public int? SecondaryDepth { get; set; }

		// Token: 0x060009CE RID: 2510 RVA: 0x00013D0A File Offset: 0x00011F0A
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AggregateScope);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00013D18 File Offset: 0x00011F18
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.PrimaryDepth.GetHashCode(), Hashing.GetHashCode<int?>(this.SecondaryDepth, null));
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00013D44 File Offset: 0x00011F44
		public bool Equals(AggregateScope other)
		{
			bool? flag = Util.AreEqual<AggregateScope>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			if (this.PrimaryDepth != other.PrimaryDepth)
			{
				return false;
			}
			int? secondaryDepth = this.SecondaryDepth;
			int? secondaryDepth2 = other.SecondaryDepth;
			return (secondaryDepth.GetValueOrDefault() == secondaryDepth2.GetValueOrDefault()) & (secondaryDepth != null == (secondaryDepth2 != null));
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00013DB0 File Offset: 0x00011FB0
		public static bool operator ==(AggregateScope left, AggregateScope right)
		{
			bool? flag = Util.AreEqual<AggregateScope>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00013DDD File Offset: 0x00011FDD
		public static bool operator !=(AggregateScope left, AggregateScope right)
		{
			return !(left == right);
		}
	}
}
