using System;

namespace Microsoft.Cloud.Platform.Eventing.Etw
{
	// Token: 0x020003DE RID: 990
	public class ProviderInfo
	{
		// Token: 0x06001E7D RID: 7805 RVA: 0x00072B98 File Offset: 0x00070D98
		internal ProviderInfo(ref NativeMethods.TRACE_GUID_PROPERTIES props)
		{
			this.m_props = props;
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x00072BAC File Offset: 0x00070DAC
		public Guid Guid
		{
			get
			{
				return this.m_props.Guid;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x00072BB9 File Offset: 0x00070DB9
		public int LoggerId
		{
			get
			{
				return (int)this.m_props.LoggerId;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x00072BC6 File Offset: 0x00070DC6
		public bool IsEnabled
		{
			get
			{
				return this.m_props.IsEnable > 0;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x00072BD6 File Offset: 0x00070DD6
		public EnabledProviderInfo EnabledProperties
		{
			get
			{
				return new EnabledProviderInfo(this.Guid, (EtwTraceLevel)this.m_props.EnableLevel, (int)this.m_props.EnableFlags);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x00072BF9 File Offset: 0x00070DF9
		public int Flags
		{
			get
			{
				return (int)this.m_props.EnableFlags;
			}
		}

		// Token: 0x04000AB5 RID: 2741
		private NativeMethods.TRACE_GUID_PROPERTIES m_props;
	}
}
