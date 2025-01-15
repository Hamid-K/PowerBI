using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000016 RID: 22
	internal sealed class SecureStringWrapper : IDisposable
	{
		// Token: 0x0600006F RID: 111 RVA: 0x0000332C File Offset: 0x0000152C
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

		// Token: 0x06000070 RID: 112 RVA: 0x0000337B File Offset: 0x0000157B
		public SecureStringWrapper(SecureString secureString)
		{
			this.m_secStr = ((secureString == null) ? new SecureString() : secureString.Copy());
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003399 File Offset: 0x00001599
		public SecureStringWrapper(SecureStringWrapper secureStringWrapper)
		{
			if (secureStringWrapper != null && secureStringWrapper.m_secStr != null)
			{
				this.m_secStr = secureStringWrapper.m_secStr.Copy();
				return;
			}
			this.m_secStr = new SecureString();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000033CC File Offset: 0x000015CC
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

		// Token: 0x06000073 RID: 115 RVA: 0x0000343C File Offset: 0x0000163C
		public override string ToString()
		{
			return SecureStringWrapper.GetDecryptedString(this.m_secStr);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003449 File Offset: 0x00001649
		internal SecureString GetUnderlyingSecureString()
		{
			return this.m_secStr;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003451 File Offset: 0x00001651
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000345A File Offset: 0x0000165A
		private void Dispose(bool disposing)
		{
			if (disposing && this.m_secStr != null)
			{
				this.m_secStr.Dispose();
				this.m_secStr = null;
			}
		}

		// Token: 0x0400005F RID: 95
		private SecureString m_secStr;
	}
}
