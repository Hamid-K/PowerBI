using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001BC RID: 444
	[DataContract(Name = "Target", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingLimitTarget : IEquatable<DataShapeBindingLimitTarget>
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x00017085 File Offset: 0x00015285
		// (set) Token: 0x06000BC0 RID: 3008 RVA: 0x0001708D File Offset: 0x0001528D
		[DataMember(IsRequired = false, Order = 10)]
		public int? Primary { get; set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00017096 File Offset: 0x00015296
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x0001709E File Offset: 0x0001529E
		[DataMember(IsRequired = false, Order = 20)]
		public int? Secondary { get; set; }

		// Token: 0x06000BC3 RID: 3011 RVA: 0x000170A7 File Offset: 0x000152A7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingLimitTarget);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x000170B8 File Offset: 0x000152B8
		public override int GetHashCode()
		{
			if (this.Primary != null)
			{
				return this.Primary.Value;
			}
			if (this.Secondary != null)
			{
				return this.Secondary.Value;
			}
			return 0;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00017104 File Offset: 0x00015304
		public bool Equals(DataShapeBindingLimitTarget other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingLimitTarget>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			if (this.Primary != null)
			{
				int? num = this.Primary;
				int? num2 = other.Primary;
				if (!((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))))
				{
					return false;
				}
			}
			if (this.Secondary != null)
			{
				int? num2 = this.Secondary;
				int? num = other.Secondary;
				if (!((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null))))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x000171B4 File Offset: 0x000153B4
		public static bool operator ==(DataShapeBindingLimitTarget left, DataShapeBindingLimitTarget right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingLimitTarget>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x000171E1 File Offset: 0x000153E1
		public static bool operator !=(DataShapeBindingLimitTarget left, DataShapeBindingLimitTarget right)
		{
			return !(left == right);
		}
	}
}
