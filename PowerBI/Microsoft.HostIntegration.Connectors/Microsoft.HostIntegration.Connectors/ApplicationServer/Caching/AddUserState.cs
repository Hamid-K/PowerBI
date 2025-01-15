using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002D8 RID: 728
	public sealed class AddUserState
	{
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x00051C9B File Offset: 0x0004FE9B
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x00051CA3 File Offset: 0x0004FEA3
		public bool NewUser
		{
			get
			{
				return this._newUser;
			}
			set
			{
				this._newUser = value;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x00051CAC File Offset: 0x0004FEAC
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x00051CB4 File Offset: 0x0004FEB4
		public bool NewLogOn
		{
			get
			{
				return this._newLogOn;
			}
			set
			{
				this._newLogOn = value;
			}
		}

		// Token: 0x04000E51 RID: 3665
		private bool _newUser;

		// Token: 0x04000E52 RID: 3666
		private bool _newLogOn;
	}
}
