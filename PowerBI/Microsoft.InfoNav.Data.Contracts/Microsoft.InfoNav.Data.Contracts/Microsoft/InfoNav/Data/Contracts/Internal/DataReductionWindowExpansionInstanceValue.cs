using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001C5 RID: 453
	[DataContract(Name = "WindowExpansionInstanceValue", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataReductionWindowExpansionInstanceValue : IEquatable<DataReductionWindowExpansionInstanceValue>
	{
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00017795 File Offset: 0x00015995
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x0001779D File Offset: 0x0001599D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IList<QueryExpressionContainer> Values { get; set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x000177A6 File Offset: 0x000159A6
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x000177AE File Offset: 0x000159AE
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public WindowKind WindowStartKind { get; set; }

		// Token: 0x06000BFF RID: 3071 RVA: 0x000177B7 File Offset: 0x000159B7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataReductionWindowExpansionInstanceValue);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x000177C5 File Offset: 0x000159C5
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash<QueryExpressionContainer>(this.Values, null), Hashing.GetHashCode<WindowKind>(this.WindowStartKind, null));
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x000177E4 File Offset: 0x000159E4
		public bool Equals(DataReductionWindowExpansionInstanceValue other)
		{
			bool? flag = Util.AreEqual<DataReductionWindowExpansionInstanceValue>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Values.SequenceEqual(other.Values) && this.WindowStartKind.Equals(other.WindowStartKind);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00017840 File Offset: 0x00015A40
		public static bool operator ==(DataReductionWindowExpansionInstanceValue left, DataReductionWindowExpansionInstanceValue right)
		{
			bool? flag = Util.AreEqual<DataReductionWindowExpansionInstanceValue>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0001786D File Offset: 0x00015A6D
		public static bool operator !=(DataReductionWindowExpansionInstanceValue left, DataReductionWindowExpansionInstanceValue right)
		{
			return !(left == right);
		}
	}
}
