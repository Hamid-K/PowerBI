using System;
using System.Globalization;

namespace Microsoft.Cloud.Platform.Eventing.Etw
{
	// Token: 0x020003E6 RID: 998
	public class SessionCreationProperties : IEquatable<SessionCreationProperties>
	{
		// Token: 0x06001E95 RID: 7829 RVA: 0x00072F74 File Offset: 0x00071174
		public SessionCreationProperties(Guid sessionId, string name, string path, SessionKinds kind)
			: this()
		{
			this.m_rep.m_props.LogFileMode = 16777216U;
			if ((kind & SessionKinds.Log) != (SessionKinds)0)
			{
				this.m_rep.m_props.LogFileMode = this.m_rep.m_props.LogFileMode | 2U;
			}
			if ((kind & SessionKinds.RealTime) != (SessionKinds)0)
			{
				this.m_rep.m_props.LogFileMode = this.m_rep.m_props.LogFileMode | 256U;
				this.m_rep.m_props.FlushTimer = 1U;
			}
			this.m_rep.m_props.Wnode.Guid = sessionId;
			this.m_rep.m_name = name;
			this.m_rep.m_path = path;
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x00073015 File Offset: 0x00071215
		public Guid Guid
		{
			get
			{
				return this.m_rep.m_props.Wnode.Guid;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001E97 RID: 7831 RVA: 0x0007302C File Offset: 0x0007122C
		public string Name
		{
			get
			{
				return this.m_rep.m_name;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x00073039 File Offset: 0x00071239
		public string Path
		{
			get
			{
				return this.m_rep.m_path;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001E99 RID: 7833 RVA: 0x00073046 File Offset: 0x00071246
		// (set) Token: 0x06001E9A RID: 7834 RVA: 0x00073059 File Offset: 0x00071259
		public int BufferSize
		{
			get
			{
				return checked((int)this.m_rep.m_props.BufferSize);
			}
			set
			{
				this.m_rep.m_props.BufferSize = (uint)value;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x0007306C File Offset: 0x0007126C
		// (set) Token: 0x06001E9C RID: 7836 RVA: 0x0007307F File Offset: 0x0007127F
		public int MinBuffers
		{
			get
			{
				return checked((int)this.m_rep.m_props.MinimumBuffers);
			}
			set
			{
				this.m_rep.m_props.MinimumBuffers = (uint)value;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x00073092 File Offset: 0x00071292
		// (set) Token: 0x06001E9E RID: 7838 RVA: 0x000730A5 File Offset: 0x000712A5
		public int MaxBuffers
		{
			get
			{
				return checked((int)this.m_rep.m_props.MaximumBuffers);
			}
			set
			{
				this.m_rep.m_props.MaximumBuffers = (uint)value;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x000730B8 File Offset: 0x000712B8
		// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x000730CB File Offset: 0x000712CB
		public int MaxFileSize
		{
			get
			{
				return checked((int)this.m_rep.m_props.MaximumFileSize);
			}
			set
			{
				this.m_rep.m_props.MaximumFileSize = (uint)value;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x000730DE File Offset: 0x000712DE
		// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x000730F1 File Offset: 0x000712F1
		public int FlushTimerPeriodInSeconds
		{
			get
			{
				return checked((int)this.m_rep.m_props.FlushTimer);
			}
			set
			{
				this.m_rep.m_props.FlushTimer = (uint)value;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x00073104 File Offset: 0x00071304
		// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x0007311B File Offset: 0x0007131B
		public TimerType TimerType
		{
			get
			{
				return (TimerType)this.m_rep.m_props.Wnode.ClientContext;
			}
			set
			{
				this.m_rep.m_props.Wnode.ClientContext = (uint)value;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x00073134 File Offset: 0x00071334
		public SessionKinds Kind
		{
			get
			{
				SessionKinds sessionKinds = (SessionKinds)0;
				if ((this.m_rep.m_props.LogFileMode & 256U) != 0U)
				{
					sessionKinds |= SessionKinds.RealTime;
				}
				if ((this.m_rep.m_props.LogFileMode & 15U) != 0U)
				{
					sessionKinds |= SessionKinds.Log;
				}
				return sessionKinds;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001EA6 RID: 7846 RVA: 0x00073179 File Offset: 0x00071379
		// (set) Token: 0x06001EA7 RID: 7847 RVA: 0x0007318E File Offset: 0x0007138E
		public LogFileMode LogFileMode
		{
			get
			{
				return (LogFileMode)(this.m_rep.m_props.LogFileMode & 15U);
			}
			set
			{
				this.m_rep.m_props.LogFileMode = this.m_rep.m_props.LogFileMode & 4294967280U;
				this.m_rep.m_props.LogFileMode = this.m_rep.m_props.LogFileMode | (uint)value;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x000731BB File Offset: 0x000713BB
		// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x000731D3 File Offset: 0x000713D3
		public SessionFlags SessionFlags
		{
			get
			{
				return (SessionFlags)(this.m_rep.m_props.LogFileMode & 17039080U);
			}
			set
			{
				this.m_rep.m_props.LogFileMode = this.m_rep.m_props.LogFileMode | (uint)value;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x000731EA File Offset: 0x000713EA
		public int DroppedCount
		{
			get
			{
				return (int)this.m_rep.m_props.EventsLost;
			}
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x000731FC File Offset: 0x000713FC
		public bool Equals(SessionCreationProperties other)
		{
			return other != null && (this.Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase) && this.Guid.Equals(other.Guid) && this.Kind == other.Kind && this.TimerType == other.TimerType && this.SessionFlags == other.SessionFlags && this.FlushTimerPeriodInSeconds == other.FlushTimerPeriodInSeconds && this.LogFileMode == other.LogFileMode && this.Path.Equals(other.Path, StringComparison.OrdinalIgnoreCase) && this.LogFileMode == other.LogFileMode) && this.BufferSize == other.BufferSize;
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x000732B6 File Offset: 0x000714B6
		public override bool Equals(object other)
		{
			return this.Equals(other as SessionCreationProperties);
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x000732C4 File Offset: 0x000714C4
		public override int GetHashCode()
		{
			return this.Guid.GetHashCode();
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x000732E8 File Offset: 0x000714E8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "<Name: {0}, Guid: {1}, Kind: {2}, Path: {3}>", new object[] { this.Name, this.Guid, this.Kind, this.Path });
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x00073338 File Offset: 0x00071538
		internal SessionCreationProperties(NativeMethods.EVENT_TRACE_PROPERTIES_CUSTOM rep)
		{
			this.m_rep = rep;
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x00073347 File Offset: 0x00071547
		private SessionCreationProperties()
		{
			this.m_rep = new NativeMethods.EVENT_TRACE_PROPERTIES_CUSTOM(1);
			this.TimerType = TimerType.SystemTimer;
			this.MaxFileSize = 20;
		}

		// Token: 0x04000ACF RID: 2767
		internal NativeMethods.EVENT_TRACE_PROPERTIES_CUSTOM m_rep;
	}
}
