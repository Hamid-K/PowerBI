using System;
using System.Diagnostics;

namespace NLog.Internal
{
	// Token: 0x02000133 RID: 307
	internal class PortableProcessIDHelper : ProcessIDHelper
	{
		// Token: 0x06000F3F RID: 3903 RVA: 0x00026538 File Offset: 0x00024738
		public PortableProcessIDHelper()
		{
			try
			{
				Process currentProcess = Process.GetCurrentProcess();
				this._currentProcessId = ((currentProcess != null) ? currentProcess.Id : 0);
				this._currentProcessFilePath = ((currentProcess != null) ? currentProcess.MainModule.FileName : null) ?? string.Empty;
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x000265AC File Offset: 0x000247AC
		public override int CurrentProcessID
		{
			get
			{
				return this._currentProcessId;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x000265B4 File Offset: 0x000247B4
		public override string CurrentProcessFilePath
		{
			get
			{
				return this._currentProcessFilePath;
			}
		}

		// Token: 0x04000411 RID: 1041
		private readonly int _currentProcessId;

		// Token: 0x04000412 RID: 1042
		private readonly string _currentProcessFilePath = string.Empty;
	}
}
