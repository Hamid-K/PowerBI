using System;
using System.Security.Principal;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000097 RID: 151
	public class UserContext
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000EAB7 File Offset: 0x0000CCB7
		public string UserName
		{
			get
			{
				return this.m_userName;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000EABF File Offset: 0x0000CCBF
		public object UserToken
		{
			get
			{
				return this.m_userToken;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000EAC7 File Offset: 0x0000CCC7
		public AuthenticationType AuthenticationType
		{
			get
			{
				return this.m_authType;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000EACF File Offset: 0x0000CCCF
		public bool IsInitialized
		{
			get
			{
				return this.m_initialized;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000EAD7 File Offset: 0x0000CCD7
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x0000EADF File Offset: 0x0000CCDF
		public byte[] AdditionalUserToken
		{
			get
			{
				return this.m_additionalUserToken;
			}
			set
			{
				this.m_additionalUserToken = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		internal virtual bool UseAdditionalToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		internal virtual WindowsIdentity GetWindowsIdentity()
		{
			if (this.m_authType != AuthenticationType.Windows)
			{
				throw new WindowsIntegratedSecurityDisabledException();
			}
			RSTrace.SecurityTracer.Assert(this.m_userToken != null && this.m_userToken is IntPtr);
			IntPtr intPtr = (IntPtr)this.m_userToken;
			if (intPtr != IntPtr.Zero)
			{
				return new WindowsIdentity(intPtr);
			}
			return null;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000EB4C File Offset: 0x0000CD4C
		public UserContext(string userName, object token, AuthenticationType authType)
		{
			this.m_userName = userName;
			this.m_userToken = token;
			this.m_authType = authType;
			this.m_initialized = true;
			this.m_additionalUserToken = null;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000EB77 File Offset: 0x0000CD77
		public UserContext()
		{
			this.m_userName = string.Empty;
			this.m_userToken = null;
			this.m_authType = AuthenticationType.None;
			this.m_additionalUserToken = null;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000EB9F File Offset: 0x0000CD9F
		public UserContext(AuthenticationType authType)
		{
			this.m_userName = string.Empty;
			this.m_userToken = null;
			this.m_authType = authType;
			this.m_initialized = false;
			this.m_additionalUserToken = null;
		}

		// Token: 0x040002C0 RID: 704
		protected string m_userName;

		// Token: 0x040002C1 RID: 705
		protected object m_userToken;

		// Token: 0x040002C2 RID: 706
		protected AuthenticationType m_authType;

		// Token: 0x040002C3 RID: 707
		protected bool m_initialized;

		// Token: 0x040002C4 RID: 708
		protected byte[] m_additionalUserToken;
	}
}
