using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using NLog.Common;
using NLog.Internal;

namespace NLog.Targets.Wrappers
{
	// Token: 0x0200006B RID: 107
	[SecuritySafeCritical]
	[Target("ImpersonatingWrapper", IsWrapper = true)]
	public class ImpersonatingTargetWrapper : WrapperTargetBase
	{
		// Token: 0x060008D3 RID: 2259 RVA: 0x00016E55 File Offset: 0x00015055
		public ImpersonatingTargetWrapper()
			: this(null)
		{
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00016E5E File Offset: 0x0001505E
		public ImpersonatingTargetWrapper(string name, Target wrappedTarget)
			: this(wrappedTarget)
		{
			base.Name = name;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00016E6E File Offset: 0x0001506E
		public ImpersonatingTargetWrapper(Target wrappedTarget)
		{
			this.Domain = ".";
			this.LogOnType = SecurityLogOnType.Interactive;
			this.LogOnProvider = LogOnProviderType.Default;
			this.ImpersonationLevel = SecurityImpersonationLevel.Impersonation;
			base.WrappedTarget = wrappedTarget;
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00016EA8 File Offset: 0x000150A8
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x00016EB0 File Offset: 0x000150B0
		public string UserName { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00016EB9 File Offset: 0x000150B9
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x00016EC1 File Offset: 0x000150C1
		public string Password { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00016ECA File Offset: 0x000150CA
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x00016ED2 File Offset: 0x000150D2
		[DefaultValue(".")]
		public string Domain { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00016EDB File Offset: 0x000150DB
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x00016EE3 File Offset: 0x000150E3
		public SecurityLogOnType LogOnType { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00016EEC File Offset: 0x000150EC
		// (set) Token: 0x060008DF RID: 2271 RVA: 0x00016EF4 File Offset: 0x000150F4
		public LogOnProviderType LogOnProvider { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00016EFD File Offset: 0x000150FD
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x00016F05 File Offset: 0x00015105
		public SecurityImpersonationLevel ImpersonationLevel { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00016F0E File Offset: 0x0001510E
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x00016F16 File Offset: 0x00015116
		[DefaultValue(false)]
		public bool RevertToSelf { get; set; }

		// Token: 0x060008E4 RID: 2276 RVA: 0x00016F20 File Offset: 0x00015120
		protected override void InitializeTarget()
		{
			if (!this.RevertToSelf)
			{
				this._newIdentity = this.CreateWindowsIdentity(out this._duplicateTokenHandle);
			}
			using (this.DoImpersonate())
			{
				base.InitializeTarget();
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00016F70 File Offset: 0x00015170
		protected override void CloseTarget()
		{
			using (this.DoImpersonate())
			{
				base.CloseTarget();
			}
			if (this._duplicateTokenHandle != IntPtr.Zero)
			{
				NativeMethods.CloseHandle(this._duplicateTokenHandle);
				this._duplicateTokenHandle = IntPtr.Zero;
			}
			if (this._newIdentity != null)
			{
				this._newIdentity.Dispose();
				this._newIdentity = null;
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00016FEC File Offset: 0x000151EC
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			using (this.DoImpersonate())
			{
				base.WrappedTarget.WriteAsyncLogEvent(logEvent);
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00017028 File Offset: 0x00015228
		[Obsolete("Instead override Write(IList<AsyncLogEventInfo> logEvents. Marked obsolete on NLog 4.5")]
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			this.Write(logEvents);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00017034 File Offset: 0x00015234
		protected override void Write(IList<AsyncLogEventInfo> logEvents)
		{
			using (this.DoImpersonate())
			{
				base.WrappedTarget.WriteAsyncLogEvents(logEvents);
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00017070 File Offset: 0x00015270
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			using (this.DoImpersonate())
			{
				base.WrappedTarget.Flush(asyncContinuation);
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x000170AC File Offset: 0x000152AC
		private IDisposable DoImpersonate()
		{
			if (this.RevertToSelf)
			{
				return new ImpersonatingTargetWrapper.ContextReverter(WindowsIdentity.Impersonate(IntPtr.Zero));
			}
			return new ImpersonatingTargetWrapper.ContextReverter(this._newIdentity.Impersonate());
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x000170D8 File Offset: 0x000152D8
		private WindowsIdentity CreateWindowsIdentity(out IntPtr handle)
		{
			IntPtr intPtr;
			if (!NativeMethods.LogonUser(this.UserName, this.Domain, this.Password, (int)this.LogOnType, (int)this.LogOnProvider, out intPtr))
			{
				throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			if (!NativeMethods.DuplicateToken(intPtr, (int)this.ImpersonationLevel, out handle))
			{
				NativeMethods.CloseHandle(intPtr);
				throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			NativeMethods.CloseHandle(intPtr);
			return new WindowsIdentity(handle);
		}

		// Token: 0x040001F4 RID: 500
		private WindowsIdentity _newIdentity;

		// Token: 0x040001F5 RID: 501
		private IntPtr _duplicateTokenHandle = IntPtr.Zero;

		// Token: 0x02000244 RID: 580
		internal sealed class ContextReverter : IDisposable
		{
			// Token: 0x0600158E RID: 5518 RVA: 0x000390FF File Offset: 0x000372FF
			public ContextReverter(WindowsImpersonationContext windowsImpersonationContext)
			{
				this._impersonationContext = windowsImpersonationContext;
			}

			// Token: 0x0600158F RID: 5519 RVA: 0x0003910E File Offset: 0x0003730E
			public void Dispose()
			{
				this._impersonationContext.Undo();
			}

			// Token: 0x04000636 RID: 1590
			private readonly WindowsImpersonationContext _impersonationContext;
		}
	}
}
