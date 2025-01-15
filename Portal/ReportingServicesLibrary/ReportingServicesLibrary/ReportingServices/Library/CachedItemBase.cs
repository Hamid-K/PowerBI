using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000268 RID: 616
	internal abstract class CachedItemBase : ICachedItem
	{
		// Token: 0x06001635 RID: 5685 RVA: 0x000587D1 File Offset: 0x000569D1
		protected CachedItemBase(string key, DateTime expirationDate)
		{
			this.m_key = key;
			this.m_expirationDate = expirationDate;
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x000587E7 File Offset: 0x000569E7
		// (set) Token: 0x06001637 RID: 5687 RVA: 0x000587EF File Offset: 0x000569EF
		public string Key
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_key;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_key = value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x000587F8 File Offset: 0x000569F8
		// (set) Token: 0x06001639 RID: 5689 RVA: 0x00058800 File Offset: 0x00056A00
		public DateTime ExpirationDate
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_expirationDate;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_expirationDate = value;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x0600163A RID: 5690
		public abstract long SizeEstimateKb { get; }

		// Token: 0x0600163B RID: 5691 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public virtual void NotifyItemIsCached()
		{
		}

		// Token: 0x04000814 RID: 2068
		private string m_key;

		// Token: 0x04000815 RID: 2069
		private DateTime m_expirationDate;
	}
}
