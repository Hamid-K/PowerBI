using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200018E RID: 398
	[DataContract(Name = "PlotAxisBinding", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataReductionPlotAxisBinding : IEquatable<DataReductionPlotAxisBinding>
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0001531D File Offset: 0x0001351D
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x00015325 File Offset: 0x00013525
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 10)]
		public int Index { get; set; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0001532E File Offset: 0x0001352E
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x00015336 File Offset: 0x00013536
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public DataReductionPlotAxisTransform Transform { get; set; }

		// Token: 0x06000AAC RID: 2732 RVA: 0x0001533F File Offset: 0x0001353F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataReductionPlotAxisBinding);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0001534D File Offset: 0x0001354D
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<int>(this.Index, null), Hashing.GetHashCode<DataReductionPlotAxisTransform>(this.Transform, null));
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0001536C File Offset: 0x0001356C
		public bool Equals(DataReductionPlotAxisBinding other)
		{
			bool? flag = Util.AreEqual<DataReductionPlotAxisBinding>(this, other);
			if (flag == null)
			{
				return this.Index == other.Index && this.Transform == other.Transform;
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000153B0 File Offset: 0x000135B0
		public static bool operator ==(DataReductionPlotAxisBinding left, DataReductionPlotAxisBinding right)
		{
			bool? flag = Util.AreEqual<DataReductionPlotAxisBinding>(left, right);
			if (flag == null)
			{
				return left.Equals(right);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000153DD File Offset: 0x000135DD
		public static bool operator !=(DataReductionPlotAxisBinding left, DataReductionPlotAxisBinding right)
		{
			return !(left == right);
		}
	}
}
