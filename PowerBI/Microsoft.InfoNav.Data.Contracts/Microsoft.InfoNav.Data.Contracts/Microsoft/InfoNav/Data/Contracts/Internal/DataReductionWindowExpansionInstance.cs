using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001C4 RID: 452
	[DataContract(Name = "WindowExpansionInstance", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataReductionWindowExpansionInstance : IEquatable<DataReductionWindowExpansionInstance>
	{
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00017673 File Offset: 0x00015873
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x0001767B File Offset: 0x0001587B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IList<QueryExpressionContainer> Values { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00017684 File Offset: 0x00015884
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x0001768C File Offset: 0x0001588C
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public IList<DataReductionWindowExpansionInstance> Children { get; set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00017695 File Offset: 0x00015895
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x0001769D File Offset: 0x0001589D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<DataReductionWindowExpansionInstanceValue> WindowExpansionInstanceWindowValue { get; set; }

		// Token: 0x06000BF5 RID: 3061 RVA: 0x000176A6 File Offset: 0x000158A6
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataReductionWindowExpansionInstance);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x000176B4 File Offset: 0x000158B4
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash<QueryExpressionContainer>(this.Values, null), Hashing.CombineHash<DataReductionWindowExpansionInstance>(this.Children, null), Hashing.CombineHash<DataReductionWindowExpansionInstanceValue>(this.WindowExpansionInstanceWindowValue, null));
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000176E0 File Offset: 0x000158E0
		public bool Equals(DataReductionWindowExpansionInstance other)
		{
			bool? flag = Util.AreEqual<DataReductionWindowExpansionInstance>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return base.GetType() == other.GetType() && this.Values.SequenceEqual(other.Values) && this.Children.SequenceEqual(other.Children) && this.WindowExpansionInstanceWindowValue.SequenceEqual(other.WindowExpansionInstanceWindowValue);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00017754 File Offset: 0x00015954
		public static bool operator ==(DataReductionWindowExpansionInstance left, DataReductionWindowExpansionInstance right)
		{
			bool? flag = Util.AreEqual<DataReductionWindowExpansionInstance>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00017781 File Offset: 0x00015981
		public static bool operator !=(DataReductionWindowExpansionInstance left, DataReductionWindowExpansionInstance right)
		{
			return !(left == right);
		}
	}
}
