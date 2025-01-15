using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001C0 RID: 448
	[DataContract(Name = "SuppressedJoinPredicate", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingSuppressedJoinPredicate : IEquatable<DataShapeBindingSuppressedJoinPredicate>, IQueryReferenceContainer
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x000175A1 File Offset: 0x000157A1
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x000175A9 File Offset: 0x000157A9
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 0)]
		public DataShapeBindingQueryReference QueryReference { get; set; }

		// Token: 0x06000BE4 RID: 3044 RVA: 0x000175B2 File Offset: 0x000157B2
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingSuppressedJoinPredicate);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x000175C0 File Offset: 0x000157C0
		public override int GetHashCode()
		{
			return Hashing.GetHashCode<DataShapeBindingQueryReference>(this.QueryReference, null);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000175D0 File Offset: 0x000157D0
		public bool Equals(DataShapeBindingSuppressedJoinPredicate other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingSuppressedJoinPredicate>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.QueryReference.Equals(other.QueryReference);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00017608 File Offset: 0x00015808
		public static bool operator ==(DataShapeBindingSuppressedJoinPredicate left, DataShapeBindingSuppressedJoinPredicate right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingSuppressedJoinPredicate>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00017635 File Offset: 0x00015835
		public static bool operator !=(DataShapeBindingSuppressedJoinPredicate left, DataShapeBindingSuppressedJoinPredicate right)
		{
			return !(left == right);
		}
	}
}
