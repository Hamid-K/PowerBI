using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000408 RID: 1032
	internal class NotFunc : UnaryFunc
	{
		// Token: 0x060023FE RID: 9214 RVA: 0x0006E557 File Offset: 0x0006C757
		private NotFunc()
		{
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x0006E55F File Offset: 0x0006C75F
		protected override object UnaryInvoke(object arg)
		{
			return arg == null || !(bool)arg;
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x0006E575 File Offset: 0x0006C775
		public override string ToString()
		{
			return "not";
		}

		// Token: 0x0400163F RID: 5695
		public static readonly NotFunc Singleton = new NotFunc();
	}
}
