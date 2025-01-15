using System;
using System.Globalization;
using System.Threading;

namespace Microsoft.Internal
{
	// Token: 0x02000181 RID: 385
	internal class CloneCurrentCultures
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x0000CA9F File Offset: 0x0000AC9F
		public static ParameterizedThreadStart CreateWrapper(ParameterizedThreadStart threadStart)
		{
			CloneCurrentCultures.Cultures cultures = CloneCurrentCultures.CaptureCurrentCultures();
			return delegate(object o)
			{
				CloneCurrentCultures.InitializeThreadCultures(cultures);
				threadStart(o);
			};
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0000CAC3 File Offset: 0x0000ACC3
		public static ThreadStart CreateWrapper(ThreadStart threadStart)
		{
			CloneCurrentCultures.Cultures cultures = CloneCurrentCultures.CaptureCurrentCultures();
			return delegate
			{
				CloneCurrentCultures.InitializeThreadCultures(cultures);
				threadStart();
			};
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0000CAE7 File Offset: 0x0000ACE7
		public static WaitCallback CreateWrapper(WaitCallback waitCallback)
		{
			CloneCurrentCultures.Cultures cultures = CloneCurrentCultures.CaptureCurrentCultures();
			return delegate(object state)
			{
				CloneCurrentCultures.InitializeThreadCultures(cultures);
				waitCallback(state);
			};
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0000CB0B File Offset: 0x0000AD0B
		private static CloneCurrentCultures.Cultures CaptureCurrentCultures()
		{
			return new CloneCurrentCultures.Cultures
			{
				Culture = Thread.CurrentThread.CurrentCulture,
				UICulture = Thread.CurrentThread.CurrentUICulture
			};
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000CB32 File Offset: 0x0000AD32
		private static void InitializeThreadCultures(CloneCurrentCultures.Cultures cultures)
		{
			Thread.CurrentThread.CurrentCulture = cultures.Culture;
			Thread.CurrentThread.CurrentUICulture = cultures.UICulture;
		}

		// Token: 0x02000182 RID: 386
		private class Cultures
		{
			// Token: 0x17000243 RID: 579
			// (get) Token: 0x0600074D RID: 1869 RVA: 0x0000CB54 File Offset: 0x0000AD54
			// (set) Token: 0x0600074E RID: 1870 RVA: 0x0000CB5C File Offset: 0x0000AD5C
			public CultureInfo Culture { get; set; }

			// Token: 0x17000244 RID: 580
			// (get) Token: 0x0600074F RID: 1871 RVA: 0x0000CB65 File Offset: 0x0000AD65
			// (set) Token: 0x06000750 RID: 1872 RVA: 0x0000CB6D File Offset: 0x0000AD6D
			public CultureInfo UICulture { get; set; }
		}
	}
}
