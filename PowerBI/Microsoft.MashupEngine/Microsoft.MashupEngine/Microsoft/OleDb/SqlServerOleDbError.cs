using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001F2D RID: 7981
	[Serializable]
	public class SqlServerOleDbError : OleDbError
	{
		// Token: 0x0600C399 RID: 50073 RVA: 0x00272FE8 File Offset: 0x002711E8
		public SqlServerOleDbError(string source, string message, int nativeError, byte errorClass, byte errorState)
			: base(source, message, null, nativeError)
		{
			this.errorClass = errorClass;
			this.errorState = errorState;
		}

		// Token: 0x17002FC6 RID: 12230
		// (get) Token: 0x0600C39A RID: 50074 RVA: 0x00273004 File Offset: 0x00271204
		public int ErrorClass
		{
			get
			{
				return (int)this.errorClass;
			}
		}

		// Token: 0x17002FC7 RID: 12231
		// (get) Token: 0x0600C39B RID: 50075 RVA: 0x0027300C File Offset: 0x0027120C
		public int ErrorState
		{
			get
			{
				return (int)this.errorState;
			}
		}

		// Token: 0x04006498 RID: 25752
		private readonly byte errorClass;

		// Token: 0x04006499 RID: 25753
		private readonly byte errorState;
	}
}
