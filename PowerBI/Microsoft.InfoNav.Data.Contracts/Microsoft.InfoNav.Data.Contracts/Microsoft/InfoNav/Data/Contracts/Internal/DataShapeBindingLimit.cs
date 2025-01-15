using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001BA RID: 442
	[DataContract(Name = "Limit", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingLimit : IEquatable<DataShapeBindingLimit>
	{
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x00016F89 File Offset: 0x00015189
		// (set) Token: 0x06000BB4 RID: 2996 RVA: 0x00016F91 File Offset: 0x00015191
		[DataMember(IsRequired = true, Order = 10)]
		public DataShapeBindingLimitType Type { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x00016F9A File Offset: 0x0001519A
		// (set) Token: 0x06000BB6 RID: 2998 RVA: 0x00016FA2 File Offset: 0x000151A2
		[DataMember(IsRequired = false, Order = 20)]
		public int Count { get; set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x00016FAB File Offset: 0x000151AB
		// (set) Token: 0x06000BB8 RID: 3000 RVA: 0x00016FB3 File Offset: 0x000151B3
		[DataMember(IsRequired = true, Order = 30)]
		public DataShapeBindingLimitTarget Target { get; set; }

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00016FBC File Offset: 0x000151BC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingLimit);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00016FCA File Offset: 0x000151CA
		public override int GetHashCode()
		{
			return Hashing.CombineHash((int)this.Type, this.Count, this.Target.GetHashCode());
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00016FE8 File Offset: 0x000151E8
		public bool Equals(DataShapeBindingLimit other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingLimit>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Type == other.Type && this.Count == other.Count && !(this.Target != other.Target);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00017044 File Offset: 0x00015244
		public static bool operator ==(DataShapeBindingLimit left, DataShapeBindingLimit right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingLimit>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00017071 File Offset: 0x00015271
		public static bool operator !=(DataShapeBindingLimit left, DataShapeBindingLimit right)
		{
			return !(left == right);
		}
	}
}
