using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000090 RID: 144
	[XmlRoot(Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
	public class ImpersonationInfo : ICloneable
	{
		// Token: 0x0600071C RID: 1820 RVA: 0x00024C73 File Offset: 0x00022E73
		public ImpersonationInfo()
		{
			this.impersonationMode = ImpersonationMode.Default;
			this.account = null;
			this.password = null;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00024C97 File Offset: 0x00022E97
		public ImpersonationInfo(ImpersonationMode mode)
		{
			this.impersonationMode = mode;
			this.account = null;
			this.password = null;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00024CBB File Offset: 0x00022EBB
		public ImpersonationInfo(string account, string password)
		{
			this.impersonationMode = ImpersonationMode.ImpersonateAccount;
			this.account = account;
			this.password = password;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00024CDF File Offset: 0x00022EDF
		public ImpersonationInfo(ImpersonationMode mode, string account, string password)
		{
			this.impersonationMode = mode;
			this.account = account;
			this.password = password;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00024D03 File Offset: 0x00022F03
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x00024D0B File Offset: 0x00022F0B
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
		public ImpersonationMode ImpersonationMode
		{
			get
			{
				return this.impersonationMode;
			}
			set
			{
				this.impersonationMode = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00024D14 File Offset: 0x00022F14
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x00024D1C File Offset: 0x00022F1C
		[XmlElement(IsNullable = false, Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
		public string Account
		{
			get
			{
				return this.account;
			}
			set
			{
				this.account = value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x00024D25 File Offset: 0x00022F25
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x00024D2D File Offset: 0x00022F2D
		[XmlElement(IsNullable = false, Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00024D36 File Offset: 0x00022F36
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x00024D3E File Offset: 0x00022F3E
		[ReadOnly(true)]
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
		public ImpersonationInfoSecurity ImpersonationInfoSecurity
		{
			get
			{
				return this.impersonationInfoSecurity;
			}
			set
			{
				this.impersonationInfoSecurity = value;
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00024D47 File Offset: 0x00022F47
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00024D50 File Offset: 0x00022F50
		public ImpersonationInfo CopyTo(ImpersonationInfo obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			obj.ImpersonationMode = this.ImpersonationMode;
			obj.Account = this.Account;
			obj.Password = this.Password;
			obj.ImpersonationInfoSecurity = this.ImpersonationInfoSecurity;
			return obj;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00024D9C File Offset: 0x00022F9C
		public ImpersonationInfo Clone()
		{
			return this.CopyTo(new ImpersonationInfo());
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00024DA9 File Offset: 0x00022FA9
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x04000475 RID: 1141
		private ImpersonationMode impersonationMode;

		// Token: 0x04000476 RID: 1142
		private string account;

		// Token: 0x04000477 RID: 1143
		private string password;

		// Token: 0x04000478 RID: 1144
		private ImpersonationInfoSecurity impersonationInfoSecurity = ImpersonationInfoSecurity.Unchanged;
	}
}
