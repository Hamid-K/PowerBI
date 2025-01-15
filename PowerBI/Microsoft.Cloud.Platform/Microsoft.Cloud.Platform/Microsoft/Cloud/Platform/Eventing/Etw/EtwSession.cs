using System;
using System.Globalization;

namespace Microsoft.Cloud.Platform.Eventing.Etw
{
	// Token: 0x020003E7 RID: 999
	public class EtwSession
	{
		// Token: 0x06001EB1 RID: 7857 RVA: 0x0007336C File Offset: 0x0007156C
		public EtwSession(SessionCreationProperties props)
		{
			if (props == null)
			{
				throw new ArgumentNullException("props");
			}
			if ((props.Kind & SessionKinds.RealTime) != (SessionKinds)0 && props.BufferSize > 512)
			{
				throw new ArgumentOutOfRangeException("props", "Real time session buffers should not be greater than 512K");
			}
			this.m_props = props.m_rep;
			this.m_hSession = -1L;
			NativeMethods.Verify(NativeMethods.StartTrace(out this.m_hSession, this.m_props.m_name, ref this.m_props), "advapi32!StartTraceW");
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x000733F0 File Offset: 0x000715F0
		public EtwSession(string name)
		{
			this.m_props = new NativeMethods.EVENT_TRACE_PROPERTIES_CUSTOM(1);
			this.m_hSession = -1L;
			NativeMethods.Verify(NativeMethods.ControlTrace(0L, name, ref this.m_props, 0U), "advapi32!ControlTrace/Query");
			this.m_hSession = (long)this.m_props.m_props.Wnode.HistoricalContext;
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x0007344C File Offset: 0x0007164C
		public static EtwSession FindExisting(string name)
		{
			EtwSession etwSession = null;
			try
			{
				etwSession = new EtwSession(name);
			}
			catch (EtwException)
			{
			}
			return etwSession;
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00073478 File Offset: 0x00071678
		internal EtwSession(long h)
		{
			this.m_props = new NativeMethods.EVENT_TRACE_PROPERTIES_CUSTOM(1);
			this.m_hSession = h;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00073493 File Offset: 0x00071693
		public void Stop()
		{
			NativeMethods.Verify(NativeMethods.ControlTrace(this.m_hSession, this.m_props.m_name, ref this.m_props, 1U), "advapi32!ControlTraceW/Stop");
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x000734BC File Offset: 0x000716BC
		public void Flush()
		{
			NativeMethods.Verify(NativeMethods.ControlTrace(this.m_hSession, this.m_props.m_name, ref this.m_props, 3U), "advapi32!ControlTraceW/Flush");
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x000734E8 File Offset: 0x000716E8
		public bool IsProviderEnabled(ProviderInfo provider)
		{
			if (provider == null)
			{
				string text = string.Format(CultureInfo.CurrentCulture, "'provider' may not be null", new object[0]);
				throw new ArgumentNullException("provider", text);
			}
			return this.Handle == (long)provider.LoggerId;
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x00073529 File Offset: 0x00071729
		public SessionCreationProperties Properties
		{
			get
			{
				return new SessionCreationProperties(this.m_props);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x00073536 File Offset: 0x00071736
		internal long Handle
		{
			get
			{
				return this.m_hSession;
			}
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00073540 File Offset: 0x00071740
		public int GetDroppedCount()
		{
			SessionCreationProperties sessionCreationProperties = new SessionCreationProperties(this.m_props);
			NativeMethods.ControlTrace(this.m_hSession, this.m_props.m_name, ref sessionCreationProperties.m_rep, 3U);
			return sessionCreationProperties.DroppedCount;
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x00073580 File Offset: 0x00071780
		internal void Fire(ref NativeMethods.EVENT_TRACE_HEADER_CUSTOM header)
		{
			uint num = NativeMethods.TraceEvent(this.m_hSession, ref header);
			if (num == 0U)
			{
				return;
			}
			if (num == 8U)
			{
				return;
			}
			NativeMethods.Verify(num, "advapi32!TraceEvent");
		}

		// Token: 0x04000AD0 RID: 2768
		private NativeMethods.EVENT_TRACE_PROPERTIES_CUSTOM m_props;

		// Token: 0x04000AD1 RID: 2769
		private long m_hSession;

		// Token: 0x04000AD2 RID: 2770
		private const int c_maxEtwEventSize = 65536;
	}
}
