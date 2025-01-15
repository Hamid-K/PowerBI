using System;
using System.Runtime.Serialization;
using Microsoft.SqlServer.XEvent.Internal;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class XEventEngineStartFailure : Exception
	{
		// Token: 0x0600005A RID: 90 RVA: 0x0000194C File Offset: 0x0000194C
		protected XEventEngineStartFailure(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00001868 File Offset: 0x00001868
		public XEventEngineStartFailure(int reason, int major, int minor, int state)
		{
			this.m_reason = reason;
			this.m_major = major;
			this.m_minor = minor;
			this.m_state = state;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000184C File Offset: 0x0000184C
		public XEventEngineStartFailure(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00001830 File Offset: 0x00001830
		public XEventEngineStartFailure(string message)
			: base(message)
		{
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00001814 File Offset: 0x00001814
		public XEventEngineStartFailure()
		{
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000018A0 File Offset: 0x000018A0
		public virtual int Major
		{
			get
			{
				return this.m_major;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000018BC File Offset: 0x000018BC
		public virtual int Minor
		{
			get
			{
				return this.m_minor;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000018D8 File Offset: 0x000018D8
		public virtual int State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000018F4 File Offset: 0x000018F4
		public override string ToString()
		{
			if (this.m_reason == 1)
			{
				return ResMgr.GetString("XEAPIinitFailedException");
			}
			return string.Format(ResMgr.GetString("XEEngineInitFailedException"), this.m_major, this.m_minor, this.m_state);
		}

		// Token: 0x0400003E RID: 62
		private int m_reason;

		// Token: 0x0400003F RID: 63
		private int m_major;

		// Token: 0x04000040 RID: 64
		private int m_minor;

		// Token: 0x04000041 RID: 65
		private int m_state;
	}
}
