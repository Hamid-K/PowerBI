using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001BE RID: 446
	[DataContract(Name = "OrderBy", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingOrderBy : IEquatable<DataShapeBindingOrderBy>
	{
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x000173E4 File Offset: 0x000155E4
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x000173EC File Offset: 0x000155EC
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public IList<QuerySortDirection?> Overrides { get; set; }

		// Token: 0x06000BD2 RID: 3026 RVA: 0x000173F5 File Offset: 0x000155F5
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingOrderBy);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00017403 File Offset: 0x00015603
		public override int GetHashCode()
		{
			if (this.Overrides == null)
			{
				return 0;
			}
			return Hashing.CombineHash<QuerySortDirection?>(this.Overrides, null);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0001741C File Offset: 0x0001561C
		public bool Equals(DataShapeBindingOrderBy other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingOrderBy>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			bool? flag2 = Util.AreEqual<IList<QuerySortDirection?>>(this.Overrides, other.Overrides);
			if (flag2 != null)
			{
				return flag2.Value;
			}
			return this.Overrides.SequenceEqual(other.Overrides);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00017478 File Offset: 0x00015678
		public static bool operator ==(DataShapeBindingOrderBy left, DataShapeBindingOrderBy right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingOrderBy>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000174A5 File Offset: 0x000156A5
		public static bool operator !=(DataShapeBindingOrderBy left, DataShapeBindingOrderBy right)
		{
			return !(left == right);
		}
	}
}
