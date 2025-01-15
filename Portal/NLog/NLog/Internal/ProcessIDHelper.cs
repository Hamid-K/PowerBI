using System;
using System.IO;

namespace NLog.Internal
{
	// Token: 0x02000134 RID: 308
	internal abstract class ProcessIDHelper
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x000265BC File Offset: 0x000247BC
		public static ProcessIDHelper Instance
		{
			get
			{
				ProcessIDHelper processIDHelper;
				if ((processIDHelper = ProcessIDHelper._threadIDHelper) == null)
				{
					processIDHelper = (ProcessIDHelper._threadIDHelper = ProcessIDHelper.Create());
				}
				return processIDHelper;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000F43 RID: 3907
		public abstract int CurrentProcessID { get; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000F44 RID: 3908
		public abstract string CurrentProcessFilePath { get; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x000265D4 File Offset: 0x000247D4
		public string CurrentProcessBaseName
		{
			get
			{
				string text;
				if ((text = this._currentProcessBaseName) == null)
				{
					text = (this._currentProcessBaseName = (string.IsNullOrEmpty(this.CurrentProcessFilePath) ? "<unknown>" : Path.GetFileNameWithoutExtension(this.CurrentProcessFilePath)));
				}
				return text;
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00026613 File Offset: 0x00024813
		private static ProcessIDHelper Create()
		{
			if (PlatformDetector.IsWin32)
			{
				return new Win32ProcessIDHelper();
			}
			return new PortableProcessIDHelper();
		}

		// Token: 0x04000413 RID: 1043
		private const string UnknownProcessName = "<unknown>";

		// Token: 0x04000414 RID: 1044
		private static ProcessIDHelper _threadIDHelper;

		// Token: 0x04000415 RID: 1045
		private string _currentProcessBaseName;
	}
}
