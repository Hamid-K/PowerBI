using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000179 RID: 377
	[DataContract(Name = "Max", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingMaxAggregate : IEquatable<DataShapeBindingMaxAggregate>
	{
		// Token: 0x060009E8 RID: 2536 RVA: 0x0001401D File Offset: 0x0001221D
		public DataShapeBindingMaxAggregate(IncludeAllTypes includeAllTypes)
		{
			this.IncludeAllTypes = includeAllTypes;
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0001402C File Offset: 0x0001222C
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x00014034 File Offset: 0x00012234
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public IncludeAllTypes IncludeAllTypes { get; set; }

		// Token: 0x060009EB RID: 2539 RVA: 0x0001403D File Offset: 0x0001223D
		internal static DataShapeBindingAggregateContainer CreateContainer(IncludeAllTypes includeAllTypes)
		{
			return new DataShapeBindingAggregateContainer
			{
				Max = new DataShapeBindingMaxAggregate(includeAllTypes)
			};
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00014050 File Offset: 0x00012250
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.IncludeAllTypes.GetHashCode());
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00014081 File Offset: 0x00012281
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingMaxAggregate);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00014090 File Offset: 0x00012290
		public bool Equals(DataShapeBindingMaxAggregate other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingMaxAggregate>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return other.IncludeAllTypes == this.IncludeAllTypes;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x000140C4 File Offset: 0x000122C4
		public static bool operator ==(DataShapeBindingMaxAggregate left, DataShapeBindingMaxAggregate right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingMaxAggregate>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000140F1 File Offset: 0x000122F1
		public static bool operator !=(DataShapeBindingMaxAggregate left, DataShapeBindingMaxAggregate right)
		{
			return !(left == right);
		}
	}
}
