using System;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000354 RID: 852
	internal class NullSentinelPropertyRef : PropertyRef
	{
		// Token: 0x0600294C RID: 10572 RVA: 0x00084336 File Offset: 0x00082536
		private NullSentinelPropertyRef()
		{
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x0600294D RID: 10573 RVA: 0x0008433E File Offset: 0x0008253E
		internal static NullSentinelPropertyRef Instance
		{
			get
			{
				return NullSentinelPropertyRef._singleton;
			}
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x00084345 File Offset: 0x00082545
		public override string ToString()
		{
			return "NULLSENTINEL";
		}

		// Token: 0x04000E3C RID: 3644
		private static readonly NullSentinelPropertyRef _singleton = new NullSentinelPropertyRef();
	}
}
