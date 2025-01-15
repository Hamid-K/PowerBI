using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb.PInvokeInterop
{
	// Token: 0x02001F69 RID: 8041
	public class ErrorInfo : IErrorInfo, IErrorReported, IManagedInterop, IManagedDisposable
	{
		// Token: 0x0600C484 RID: 50308 RVA: 0x00274093 File Offset: 0x00272293
		public ErrorInfo(Exception exception, ref Guid guid)
		{
			this.exception = exception;
			this.guid = guid;
		}

		// Token: 0x0600C485 RID: 50309 RVA: 0x002740AE File Offset: 0x002722AE
		public int GetGUID(out Guid pGUID)
		{
			pGUID = this.guid;
			return 0;
		}

		// Token: 0x0600C486 RID: 50310 RVA: 0x002740BD File Offset: 0x002722BD
		public int GetSource([MarshalAs(UnmanagedType.BStr)] out string pBstrSource)
		{
			pBstrSource = this.exception.Source;
			return 0;
		}

		// Token: 0x0600C487 RID: 50311 RVA: 0x002740CD File Offset: 0x002722CD
		public int GetDescription([MarshalAs(UnmanagedType.BStr)] out string pBstrDescription)
		{
			pBstrDescription = this.exception.Message;
			if (pBstrDescription == null || pBstrDescription.Length == 0)
			{
				pBstrDescription = this.exception.GetType().FullName;
			}
			return 0;
		}

		// Token: 0x0600C488 RID: 50312 RVA: 0x002740FC File Offset: 0x002722FC
		public int GetHelpFile([MarshalAs(UnmanagedType.BStr)] out string pBstrHelpFile)
		{
			uint num;
			this.GetExceptionHelp(out pBstrHelpFile, out num);
			return 0;
		}

		// Token: 0x0600C489 RID: 50313 RVA: 0x00274114 File Offset: 0x00272314
		public int GetHelpContext(out uint pdwHelpContext)
		{
			string text;
			this.GetExceptionHelp(out text, out pdwHelpContext);
			return 0;
		}

		// Token: 0x0600C48A RID: 50314 RVA: 0x0027412C File Offset: 0x0027232C
		private void GetExceptionHelp(out string helpFile, out uint pdwHelpContext)
		{
			helpFile = this.exception.HelpLink;
			pdwHelpContext = 0U;
			if (helpFile == null)
			{
				return;
			}
			int num = helpFile.LastIndexOf('#');
			if (num != -1)
			{
				string text = helpFile.Substring(num + 1);
				text.Trim();
				uint num2;
				if (uint.TryParse(text, out num2))
				{
					pdwHelpContext = num2;
					helpFile = helpFile.Substring(0, num);
				}
			}
		}

		// Token: 0x0600C48B RID: 50315 RVA: 0x00274184 File Offset: 0x00272384
		public int IsReported()
		{
			return (!this.exception.Data.Contains("OLEDB_ErrorInfo_IsReported")) ? 1 : 0;
		}

		// Token: 0x0600C48C RID: 50316 RVA: 0x0027419E File Offset: 0x0027239E
		public void Dispose()
		{
			this.pUnknown = IntPtr.Zero;
		}

		// Token: 0x0600C48D RID: 50317 RVA: 0x002741AB File Offset: 0x002723AB
		public void AttachPUnknown(IntPtr punk)
		{
			if (this.pUnknown != IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			if (punk == IntPtr.Zero)
			{
				throw new ArgumentNullException();
			}
			this.pUnknown = punk;
		}

		// Token: 0x17002FD1 RID: 12241
		// (get) Token: 0x0600C48E RID: 50318 RVA: 0x002741DF File Offset: 0x002723DF
		public IntPtr PUnknown
		{
			get
			{
				return this.pUnknown;
			}
		}

		// Token: 0x0600C48F RID: 50319 RVA: 0x002741E7 File Offset: 0x002723E7
		public static void OverrideResult(Exception exception, int hr)
		{
			exception.Data["OLEDB_ErrorInfo_Hr"] = hr.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600C490 RID: 50320 RVA: 0x00274208 File Offset: 0x00272408
		public static bool TryGetOverridenResult(Exception exception, out int hr)
		{
			hr = 0;
			if (!exception.Data.Contains("OLEDB_ErrorInfo_Hr"))
			{
				return false;
			}
			string text = exception.Data["OLEDB_ErrorInfo_Hr"] as string;
			return text != null && int.TryParse(text, out hr);
		}

		// Token: 0x040064BE RID: 25790
		public const string OLEDB_ErrorInfo_IsReported = "OLEDB_ErrorInfo_IsReported";

		// Token: 0x040064BF RID: 25791
		private const string OLEDB_ErrorInfo_Hr = "OLEDB_ErrorInfo_Hr";

		// Token: 0x040064C0 RID: 25792
		private readonly Exception exception;

		// Token: 0x040064C1 RID: 25793
		private readonly Guid guid;

		// Token: 0x040064C2 RID: 25794
		private IntPtr pUnknown;
	}
}
