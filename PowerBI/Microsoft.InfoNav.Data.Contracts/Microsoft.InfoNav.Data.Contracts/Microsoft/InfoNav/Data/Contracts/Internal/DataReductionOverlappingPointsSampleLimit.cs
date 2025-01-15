using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200018D RID: 397
	[DataContract(Name = "OverlappingPointsSample", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataReductionOverlappingPointsSampleLimit : IEquatable<DataReductionOverlappingPointsSampleLimit>
	{
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x000151ED File Offset: 0x000133ED
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x000151F5 File Offset: 0x000133F5
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public int? Count { get; set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x000151FE File Offset: 0x000133FE
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x00015206 File Offset: 0x00013406
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public DataReductionPlotAxisBinding X { get; set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0001520F File Offset: 0x0001340F
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x00015217 File Offset: 0x00013417
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public DataReductionPlotAxisBinding Y { get; set; }

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00015220 File Offset: 0x00013420
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataReductionOverlappingPointsSampleLimit);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001522E File Offset: 0x0001342E
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<int?>(this.Count, null), Hashing.GetHashCode<DataReductionPlotAxisBinding>(this.X, null), Hashing.GetHashCode<DataReductionPlotAxisBinding>(this.Y, null));
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001525C File Offset: 0x0001345C
		public bool Equals(DataReductionOverlappingPointsSampleLimit other)
		{
			bool? flag = Util.AreEqual<DataReductionOverlappingPointsSampleLimit>(this, other);
			if (flag == null)
			{
				int? count = this.Count;
				int? count2 = other.Count;
				return ((count.GetValueOrDefault() == count2.GetValueOrDefault()) & (count != null == (count2 != null))) && this.X == other.X && this.Y == other.Y;
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000152DC File Offset: 0x000134DC
		public static bool operator ==(DataReductionOverlappingPointsSampleLimit left, DataReductionOverlappingPointsSampleLimit right)
		{
			bool? flag = Util.AreEqual<DataReductionOverlappingPointsSampleLimit>(left, right);
			if (flag == null)
			{
				return left.Equals(right);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00015309 File Offset: 0x00013509
		public static bool operator !=(DataReductionOverlappingPointsSampleLimit left, DataReductionOverlappingPointsSampleLimit right)
		{
			return !(left == right);
		}
	}
}
