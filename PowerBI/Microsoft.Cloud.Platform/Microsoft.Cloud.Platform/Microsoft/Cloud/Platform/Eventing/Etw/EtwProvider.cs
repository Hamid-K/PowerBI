using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Cloud.Platform.Eventing.Etw
{
	// Token: 0x020003E1 RID: 993
	public class EtwProvider : IDisposable, IEquatable<EtwProvider>
	{
		// Token: 0x06001E87 RID: 7815 RVA: 0x00072C06 File Offset: 0x00070E06
		public EtwProvider(Guid providerId, ControlDelegate callback)
		{
			this.m_guid = providerId;
			this.m_controlCallback = callback;
			this.m_hReg = -1L;
			this.m_delegate = new NativeMethods.EtwProc(EtwProvider.ControlCallback);
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x00072C38 File Offset: 0x00070E38
		public void Register()
		{
			GCHandle gchandle = default(GCHandle);
			GCHandle gchandle2 = default(GCHandle);
			try
			{
				this.m_hThis = GCHandle.Alloc(this);
				gchandle2 = GCHandle.Alloc(new Guid("f6005b5a-c105-4f77-95ef-8603fa480e7e"), GCHandleType.Pinned);
				NativeMethods.TRACE_GUID_REGISTRATION[] array = new NativeMethods.TRACE_GUID_REGISTRATION[1];
				array[0].Guid = gchandle2.AddrOfPinnedObject();
				array[0].Handle = IntPtr.Zero;
				gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
				IntPtr intPtr = gchandle.AddrOfPinnedObject();
				uint num = NativeMethods.RegisterTraceGuids(this.m_delegate, GCHandle.ToIntPtr(this.m_hThis), ref this.m_guid, 1U, intPtr, null, null, out this.m_hReg);
				if (num != 0U)
				{
					this.m_hReg = -1L;
				}
				NativeMethods.Verify(num, "advapi32!RegisterTraceGuids");
			}
			catch (EtwException)
			{
				this.Unregister();
				throw;
			}
			finally
			{
				try
				{
					gchandle.Free();
					gchandle2.Free();
				}
				catch (InvalidOperationException)
				{
				}
			}
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x00072D34 File Offset: 0x00070F34
		public void Unregister()
		{
			if (this.m_hReg != -1L)
			{
				NativeMethods.UnregisterTraceGuids(this.m_hReg);
				this.m_hReg = -1L;
			}
			if (this.m_hThis.IsAllocated)
			{
				this.m_hThis.Free();
			}
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x00072D6C File Offset: 0x00070F6C
		public void Enable(EtwSession session)
		{
			this.Enable(session, EtwTraceLevel.Unassigned, 0);
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x00072D77 File Offset: 0x00070F77
		public void Enable(EtwSession session, EtwTraceLevel traceLevel, int flags)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			NativeMethods.Verify(NativeMethods.EnableTrace(1U, (uint)flags, (uint)traceLevel, ref this.m_guid, session.Handle), "advapi32!EnableTrace/enable");
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x00072DA5 File Offset: 0x00070FA5
		public void Disable(EtwSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			NativeMethods.Verify(NativeMethods.EnableTrace(0U, 0U, 0U, ref this.m_guid, session.Handle), "advapi32!EnableTrace/disable");
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x00072DD3 File Offset: 0x00070FD3
		public Guid Id
		{
			get
			{
				return this.m_guid;
			}
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x00072DDC File Offset: 0x00070FDC
		public static ICollection<ProviderInfo> GetRegisteredProviders()
		{
			List<ProviderInfo> list = new List<ProviderInfo>();
			uint num = 1024U;
			NativeMethods.TRACE_GUID_PROPERTIES[] array;
			uint num4;
			uint num5;
			for (;;)
			{
				array = new NativeMethods.TRACE_GUID_PROPERTIES[num];
				GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
				IntPtr intPtr = gchandle.AddrOfPinnedObject();
				IntPtr[] array2 = new IntPtr[num];
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					long num3 = (long)((ulong)(num2 * (uint)Marshal.SizeOf(typeof(NativeMethods.TRACE_GUID_PROPERTIES))));
					array2[(int)num2] = new IntPtr(intPtr.ToInt64() + num3);
				}
				num4 = 0U;
				num5 = NativeMethods.EnumerateTraceGuids(array2, num, out num4);
				gchandle.Free();
				if (num5 != 234U)
				{
					break;
				}
				num = num4;
			}
			NativeMethods.Verify(num5, "advapi32!EnumerateTraceGuids");
			for (uint num6 = 0U; num6 < num4; num6 += 1U)
			{
				list.Add(new ProviderInfo(ref array[(int)num6]));
			}
			return list;
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x00009B3B File Offset: 0x00007D3B
		public static void EmptyControlDelegate(ControlCode code, EtwSession session)
		{
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x00072EA3 File Offset: 0x000710A3
		public bool Equals(EtwProvider other)
		{
			return other != null && this.m_guid.Equals(other.m_guid);
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x00072EBB File Offset: 0x000710BB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x00072ECA File Offset: 0x000710CA
		protected virtual void Dispose(bool disposing)
		{
			this.Unregister();
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x00072ED4 File Offset: 0x000710D4
		~EtwProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x00072F04 File Offset: 0x00071104
		private static uint ControlCallback(NativeMethods.WMIDPREQUESTCODE RequestCode, IntPtr Context, ref uint InOutBufferSize, IntPtr Buffer)
		{
			EtwProvider etwProvider = (EtwProvider)GCHandle.FromIntPtr(Context).Target;
			uint num;
			if (RequestCode != NativeMethods.WMIDPREQUESTCODE.WMI_ENABLE_EVENTS)
			{
				if (RequestCode != NativeMethods.WMIDPREQUESTCODE.WMI_DISABLE_EVENTS)
				{
					num = 87U;
				}
				else
				{
					etwProvider.m_controlCallback(ControlCode.Disabled, null);
					num = 0U;
				}
			}
			else
			{
				EtwSession etwSession = null;
				long traceLoggerHandle = NativeMethods.GetTraceLoggerHandle(Buffer);
				if (traceLoggerHandle != -1L)
				{
					etwSession = new EtwSession(traceLoggerHandle);
				}
				etwProvider.m_controlCallback(ControlCode.Enabled, etwSession);
				num = 0U;
			}
			InOutBufferSize = 0U;
			return num;
		}

		// Token: 0x04000AB9 RID: 2745
		private Guid m_guid;

		// Token: 0x04000ABA RID: 2746
		private ControlDelegate m_controlCallback;

		// Token: 0x04000ABB RID: 2747
		private long m_hReg;

		// Token: 0x04000ABC RID: 2748
		private GCHandle m_hThis;

		// Token: 0x04000ABD RID: 2749
		private NativeMethods.EtwProc m_delegate;
	}
}
