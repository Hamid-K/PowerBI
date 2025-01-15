using System;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000089 RID: 137
	internal class PerElementActivityTypeComparer<T> : ComparerBase<T, PerElementActivityType> where T : class, IPerElementActivityType
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x0000E86C File Offset: 0x0000CA6C
		protected override PerElementActivityType GetProperty(T obj)
		{
			return obj.PerElementActivityType;
		}
	}
}
