using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001B9 RID: 441
	[DataContract(Name = "HiddenProjections", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingHiddenProjections : IEquatable<DataShapeBindingHiddenProjections>, IQueryReferenceContainer
	{
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00016EC5 File Offset: 0x000150C5
		// (set) Token: 0x06000BAC RID: 2988 RVA: 0x00016ECD File Offset: 0x000150CD
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 0)]
		public DataShapeBindingQueryReference QueryReference { get; set; }

		// Token: 0x06000BAD RID: 2989 RVA: 0x00016ED6 File Offset: 0x000150D6
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingHiddenProjections);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00016EE4 File Offset: 0x000150E4
		public override int GetHashCode()
		{
			return Hashing.GetHashCode<DataShapeBindingQueryReference>(this.QueryReference, null);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00016EF4 File Offset: 0x000150F4
		public bool Equals(DataShapeBindingHiddenProjections other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingHiddenProjections>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			if (this.QueryReference == null)
			{
				return other.QueryReference == null;
			}
			return this.QueryReference.Equals(other.QueryReference);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00016F48 File Offset: 0x00015148
		public static bool operator ==(DataShapeBindingHiddenProjections left, DataShapeBindingHiddenProjections right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingHiddenProjections>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00016F75 File Offset: 0x00015175
		public static bool operator !=(DataShapeBindingHiddenProjections left, DataShapeBindingHiddenProjections right)
		{
			return !(left == right);
		}
	}
}
