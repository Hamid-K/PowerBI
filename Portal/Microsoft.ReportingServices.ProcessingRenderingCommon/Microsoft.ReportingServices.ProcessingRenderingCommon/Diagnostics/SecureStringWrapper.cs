using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200002C RID: 44
	public sealed class SecureStringWrapper : IDisposable
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00005A7C File Offset: 0x00003C7C
		public SecureStringWrapper(string regularString)
		{
			this.m_secStr = new SecureString();
			if (regularString != null)
			{
				for (int i = 0; i < regularString.Length; i++)
				{
					this.m_secStr.AppendChar(regularString[i]);
				}
				this.m_secStr.MakeReadOnly();
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005ACB File Offset: 0x00003CCB
		public SecureStringWrapper(SecureString secureString)
		{
			this.m_secStr = ((secureString == null) ? new SecureString() : secureString.Copy());
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public SecureStringWrapper(SecureStringWrapper secureStringWrapper)
		{
			if (secureStringWrapper != null && secureStringWrapper.m_secStr != null)
			{
				this.m_secStr = secureStringWrapper.m_secStr.Copy();
				return;
			}
			this.m_secStr = new SecureString();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005B1C File Offset: 0x00003D1C
		public static string GetDecryptedString(SecureString secureString)
		{
			string text = string.Empty;
			IntPtr intPtr = IntPtr.Zero;
			if (secureString.Length != 0)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						intPtr = Marshal.SecureStringToBSTR(secureString);
					}
					text = Marshal.PtrToStringBSTR(intPtr);
				}
				finally
				{
					if (IntPtr.Zero != intPtr)
					{
						Marshal.ZeroFreeBSTR(intPtr);
					}
				}
			}
			return text;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005B8C File Offset: 0x00003D8C
		public override string ToString()
		{
			return SecureStringWrapper.GetDecryptedString(this.m_secStr);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005B99 File Offset: 0x00003D99
		internal SecureString GetUnderlyingSecureString()
		{
			return this.m_secStr;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005BA1 File Offset: 0x00003DA1
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005BAA File Offset: 0x00003DAA
		private void Dispose(bool disposing)
		{
			if (disposing && this.m_secStr != null)
			{
				this.m_secStr.Dispose();
				this.m_secStr = null;
			}
		}

		// Token: 0x040000A7 RID: 167
		private SecureString m_secStr;
	}
}
