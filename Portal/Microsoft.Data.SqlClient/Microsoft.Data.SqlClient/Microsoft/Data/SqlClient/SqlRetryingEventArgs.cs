using System;
using System.Collections.Generic;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000034 RID: 52
	public sealed class SqlRetryingEventArgs : EventArgs
	{
		// Token: 0x060006FA RID: 1786 RVA: 0x0000E6E8 File Offset: 0x0000C8E8
		public SqlRetryingEventArgs(int retryCount, TimeSpan delay, IList<Exception> exceptions)
		{
			this.RetryCount = retryCount;
			this.Delay = delay;
			this.Exceptions = exceptions;
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0000E705 File Offset: 0x0000C905
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x0000E70D File Offset: 0x0000C90D
		public int RetryCount { get; private set; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0000E716 File Offset: 0x0000C916
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x0000E71E File Offset: 0x0000C91E
		public TimeSpan Delay { get; private set; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0000E727 File Offset: 0x0000C927
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x0000E72F File Offset: 0x0000C92F
		public bool Cancel { get; set; }

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0000E738 File Offset: 0x0000C938
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x0000E740 File Offset: 0x0000C940
		public IList<Exception> Exceptions { get; private set; }
	}
}
