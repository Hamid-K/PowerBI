using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	public sealed class EventExtension : Extension
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00006768 File Offset: 0x00004968
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00006770 File Offset: 0x00004970
		public string EventType
		{
			get
			{
				return this.m_eventType;
			}
			set
			{
				this.m_eventType = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00006779 File Offset: 0x00004979
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00006781 File Offset: 0x00004981
		public int MaxRetries
		{
			get
			{
				return this.m_maxRetries;
			}
			set
			{
				this.m_maxRetries = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000678A File Offset: 0x0000498A
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00006792 File Offset: 0x00004992
		public int SecondsBeforeRetry
		{
			get
			{
				return this.m_secondsBeforeRetry;
			}
			set
			{
				this.m_secondsBeforeRetry = value;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000679B File Offset: 0x0000499B
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000067A4 File Offset: 0x000049A4
		public override bool Equals(object extension)
		{
			bool flag = true;
			if (!(extension is EventExtension))
			{
				return false;
			}
			EventExtension eventExtension = (EventExtension)extension;
			if (this.EventType != eventExtension.EventType)
			{
				flag = false;
			}
			if (!flag)
			{
				return flag;
			}
			return base.Equals(eventExtension);
		}

		// Token: 0x040000B6 RID: 182
		private string m_eventType = "";

		// Token: 0x040000B7 RID: 183
		private int m_maxRetries;

		// Token: 0x040000B8 RID: 184
		private int m_secondsBeforeRetry = 300;
	}
}
