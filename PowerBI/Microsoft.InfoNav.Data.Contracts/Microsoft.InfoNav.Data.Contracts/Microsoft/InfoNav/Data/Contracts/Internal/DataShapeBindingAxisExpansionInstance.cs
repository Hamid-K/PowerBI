using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001C8 RID: 456
	[DataContract(Name = "ExpansionInstance", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingAxisExpansionInstance : IEquatable<DataShapeBindingAxisExpansionInstance>
	{
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00017991 File Offset: 0x00015B91
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x00017999 File Offset: 0x00015B99
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IList<QueryExpressionContainer> Values { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x000179A2 File Offset: 0x00015BA2
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x000179AA File Offset: 0x00015BAA
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public IList<DataShapeBindingAxisExpansionInstance> Children { get; set; }

		// Token: 0x06000C15 RID: 3093 RVA: 0x000179B3 File Offset: 0x00015BB3
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingAxisExpansionInstance);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x000179C1 File Offset: 0x00015BC1
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash<QueryExpressionContainer>(this.Values, null), Hashing.CombineHash<DataShapeBindingAxisExpansionInstance>(this.Children, null));
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x000179E0 File Offset: 0x00015BE0
		public bool Equals(DataShapeBindingAxisExpansionInstance other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingAxisExpansionInstance>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Values.SequenceEqual(other.Values) && this.Children.SequenceEqual(other.Children);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00017A2C File Offset: 0x00015C2C
		public static bool operator ==(DataShapeBindingAxisExpansionInstance left, DataShapeBindingAxisExpansionInstance right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingAxisExpansionInstance>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00017A59 File Offset: 0x00015C59
		public static bool operator !=(DataShapeBindingAxisExpansionInstance left, DataShapeBindingAxisExpansionInstance right)
		{
			return !(left == right);
		}
	}
}
