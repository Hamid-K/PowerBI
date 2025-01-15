using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200017B RID: 379
	[DataContract(Name = "Percentile", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingPercentileAggregate : IEquatable<DataShapeBindingPercentileAggregate>
	{
		// Token: 0x060009FA RID: 2554 RVA: 0x000141DD File Offset: 0x000123DD
		public DataShapeBindingPercentileAggregate(double k, bool exclusive = false)
		{
			this.K = k;
			this.Exclusive = exclusive;
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x000141F3 File Offset: 0x000123F3
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x00014200 File Offset: 0x00012400
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public bool Exclusive
		{
			get
			{
				return this.PercentileCore.Exclusive;
			}
			set
			{
				this.PercentileCore.Exclusive = value;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0001420E File Offset: 0x0001240E
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x0001421B File Offset: 0x0001241B
		[DataMember(IsRequired = true, Order = 2)]
		public double K
		{
			get
			{
				return this.PercentileCore.K;
			}
			set
			{
				this.PercentileCore.K = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00014229 File Offset: 0x00012429
		private QueryPercentile PercentileCore
		{
			get
			{
				if (this._percentileCore == null)
				{
					this._percentileCore = new QueryPercentile();
				}
				return this._percentileCore;
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00014244 File Offset: 0x00012444
		internal static DataShapeBindingAggregateContainer Create(double k, bool exclusive = false)
		{
			return new DataShapeBindingAggregateContainer
			{
				Percentile = new DataShapeBindingPercentileAggregate(k, exclusive)
			};
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00014258 File Offset: 0x00012458
		public override int GetHashCode()
		{
			return this.PercentileCore.GetHashCode();
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00014265 File Offset: 0x00012465
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingPercentileAggregate);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00014274 File Offset: 0x00012474
		public bool Equals(DataShapeBindingPercentileAggregate other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingPercentileAggregate>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.PercentileCore.Equals(other.PercentileCore);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000142AC File Offset: 0x000124AC
		public static bool operator ==(DataShapeBindingPercentileAggregate left, DataShapeBindingPercentileAggregate right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingPercentileAggregate>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000142D9 File Offset: 0x000124D9
		public static bool operator !=(DataShapeBindingPercentileAggregate left, DataShapeBindingPercentileAggregate right)
		{
			return !(left == right);
		}

		// Token: 0x0400058D RID: 1421
		private QueryPercentile _percentileCore;
	}
}
