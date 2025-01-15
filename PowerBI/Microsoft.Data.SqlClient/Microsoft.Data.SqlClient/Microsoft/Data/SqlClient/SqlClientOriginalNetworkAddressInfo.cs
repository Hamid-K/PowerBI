using System;
using System.Net;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000DB RID: 219
	internal sealed class SqlClientOriginalNetworkAddressInfo
	{
		// Token: 0x06000F60 RID: 3936 RVA: 0x000331D7 File Offset: 0x000313D7
		public SqlClientOriginalNetworkAddressInfo(IPAddress address, bool isFromDataSecurityProxy = false)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this._address = address;
			this._isFromDataSecurityProxy = isFromDataSecurityProxy;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x000331FB File Offset: 0x000313FB
		public override int GetHashCode()
		{
			if (this._address == null)
			{
				return 0;
			}
			return this._address.GetHashCode();
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x00033214 File Offset: 0x00031414
		public override bool Equals(object other)
		{
			SqlClientOriginalNetworkAddressInfo sqlClientOriginalNetworkAddressInfo = other as SqlClientOriginalNetworkAddressInfo;
			return sqlClientOriginalNetworkAddressInfo != null && sqlClientOriginalNetworkAddressInfo._address == this._address && this._isFromDataSecurityProxy == sqlClientOriginalNetworkAddressInfo._isFromDataSecurityProxy;
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0003324E File Offset: 0x0003144E
		public IPAddress Address
		{
			get
			{
				return this._address;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x00033256 File Offset: 0x00031456
		public bool IsFromDataSecurityProxy
		{
			get
			{
				return this._isFromDataSecurityProxy;
			}
		}

		// Token: 0x04000698 RID: 1688
		private IPAddress _address;

		// Token: 0x04000699 RID: 1689
		private bool _isFromDataSecurityProxy;
	}
}
