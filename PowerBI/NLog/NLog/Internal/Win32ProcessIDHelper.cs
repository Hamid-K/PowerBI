using System;
using System.Security;
using System.Text;

namespace NLog.Internal
{
	// Token: 0x0200014F RID: 335
	[SecuritySafeCritical]
	internal class Win32ProcessIDHelper : ProcessIDHelper
	{
		// Token: 0x06001004 RID: 4100 RVA: 0x00029470 File Offset: 0x00027670
		public Win32ProcessIDHelper()
		{
			this._currentProcessId = NativeMethods.GetCurrentProcessId();
			StringBuilder stringBuilder = new StringBuilder(512);
			if (NativeMethods.GetModuleFileName(IntPtr.Zero, stringBuilder, stringBuilder.Capacity) == 0U)
			{
				throw new InvalidOperationException("Cannot determine program name.");
			}
			this._currentProcessFilePath = stringBuilder.ToString();
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x000294CE File Offset: 0x000276CE
		public override int CurrentProcessID
		{
			get
			{
				return this._currentProcessId;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x000294D6 File Offset: 0x000276D6
		public override string CurrentProcessFilePath
		{
			get
			{
				return this._currentProcessFilePath;
			}
		}

		// Token: 0x0400044E RID: 1102
		private readonly int _currentProcessId;

		// Token: 0x0400044F RID: 1103
		private readonly string _currentProcessFilePath = string.Empty;
	}
}
