using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000184 RID: 388
	[DataContract(Name = "DataQueryColumn", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public abstract class DataQueryColumn : IEquatable<DataQueryColumn>
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0001483D File Offset: 0x00012A3D
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x00014845 File Offset: 0x00012A45
		[DataMember(IsRequired = true, Order = 10)]
		public string QueryName { get; set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0001484E File Offset: 0x00012A4E
		// (set) Token: 0x06000A3C RID: 2620 RVA: 0x00014856 File Offset: 0x00012A56
		[DataMember(IsRequired = true, Order = 20)]
		public string Name { get; set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0001485F File Offset: 0x00012A5F
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x00014867 File Offset: 0x00012A67
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string Role { get; set; }

		// Token: 0x06000A3F RID: 2623 RVA: 0x00014870 File Offset: 0x00012A70
		public bool Equals(DataQueryColumn other)
		{
			bool? flag = Util.AreEqual<DataQueryColumn>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return string.Equals(this.QueryName, other.QueryName, StringComparison.Ordinal) && string.Equals(this.Name, other.Name, StringComparison.Ordinal) && string.Equals(this.Role, other.Role, StringComparison.Ordinal);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000148D2 File Offset: 0x00012AD2
		public override bool Equals(object other)
		{
			return this.Equals(other as DataQueryColumn);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000148E0 File Offset: 0x00012AE0
		public override int GetHashCode()
		{
			return Hashing.CombineHash((this.QueryName != null) ? this.QueryName.GetHashCode() : 0, (this.Name != null) ? this.Name.GetHashCode() : 0, (this.Role != null) ? this.Role.GetHashCode() : 0);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00014934 File Offset: 0x00012B34
		public static bool operator ==(DataQueryColumn left, DataQueryColumn right)
		{
			bool? flag = Util.AreEqual<DataQueryColumn>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00014961 File Offset: 0x00012B61
		public static bool operator !=(DataQueryColumn left, DataQueryColumn right)
		{
			return !(left == right);
		}
	}
}
