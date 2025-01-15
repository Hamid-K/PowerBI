using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200017C RID: 380
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public abstract class DataShapeBindingSimpleAggregate<T> : IEquatable<T> where T : DataShapeBindingSimpleAggregate<T>
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x000142E5 File Offset: 0x000124E5
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x000142F2 File Offset: 0x000124F2
		public override bool Equals(object other)
		{
			return other is T;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000142FD File Offset: 0x000124FD
		public bool Equals(T other)
		{
			return other != null;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0001430C File Offset: 0x0001250C
		public static bool operator ==(DataShapeBindingSimpleAggregate<T> left, DataShapeBindingSimpleAggregate<T> right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingSimpleAggregate<T>>(left, right);
			return flag == null || flag.Value;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00014333 File Offset: 0x00012533
		public static bool operator !=(DataShapeBindingSimpleAggregate<T> left, DataShapeBindingSimpleAggregate<T> right)
		{
			return !(left == right);
		}
	}
}
