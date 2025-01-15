using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200017A RID: 378
	[DataContract(Name = "Min", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingMinAggregate : IEquatable<DataShapeBindingMinAggregate>
	{
		// Token: 0x060009F1 RID: 2545 RVA: 0x000140FD File Offset: 0x000122FD
		public DataShapeBindingMinAggregate(IncludeAllTypes includeAllTypes)
		{
			this.IncludeAllTypes = includeAllTypes;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0001410C File Offset: 0x0001230C
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x00014114 File Offset: 0x00012314
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public IncludeAllTypes IncludeAllTypes { get; set; }

		// Token: 0x060009F4 RID: 2548 RVA: 0x0001411D File Offset: 0x0001231D
		internal static DataShapeBindingAggregateContainer CreateContainer(IncludeAllTypes includeAllTypes)
		{
			return new DataShapeBindingAggregateContainer
			{
				Min = new DataShapeBindingMinAggregate(includeAllTypes)
			};
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00014130 File Offset: 0x00012330
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), this.IncludeAllTypes.GetHashCode());
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00014161 File Offset: 0x00012361
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingMinAggregate);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00014170 File Offset: 0x00012370
		public bool Equals(DataShapeBindingMinAggregate other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingMinAggregate>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return other.IncludeAllTypes == this.IncludeAllTypes;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x000141A4 File Offset: 0x000123A4
		public static bool operator ==(DataShapeBindingMinAggregate left, DataShapeBindingMinAggregate right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingMinAggregate>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x000141D1 File Offset: 0x000123D1
		public static bool operator !=(DataShapeBindingMinAggregate left, DataShapeBindingMinAggregate right)
		{
			return !(left == right);
		}
	}
}
