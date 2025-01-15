using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000044 RID: 68
	internal class SqlNoneIntervalEnumerator : SqlRetryIntervalBaseEnumerator
	{
		// Token: 0x0600077C RID: 1916 RVA: 0x0000FFC1 File Offset: 0x0000E1C1
		protected override TimeSpan GetNextInterval()
		{
			return base.Current;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0000FDA1 File Offset: 0x0000DFA1
		public override object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
