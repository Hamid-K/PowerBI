using System;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200081A RID: 2074
	[DataContract]
	public class ConnectionInfo
	{
		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x060041AB RID: 16811 RVA: 0x000DCCD4 File Offset: 0x000DAED4
		// (set) Token: 0x060041AC RID: 16812 RVA: 0x000DCCDC File Offset: 0x000DAEDC
		[DataMember]
		public string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x060041AD RID: 16813 RVA: 0x000DCCE5 File Offset: 0x000DAEE5
		// (set) Token: 0x060041AE RID: 16814 RVA: 0x000DCCED File Offset: 0x000DAEED
		[DataMember]
		public string Extnam
		{
			get
			{
				return this._extnam;
			}
			set
			{
				this._extnam = value;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x060041AF RID: 16815 RVA: 0x000DCCF6 File Offset: 0x000DAEF6
		// (set) Token: 0x060041B0 RID: 16816 RVA: 0x000DCCFE File Offset: 0x000DAEFE
		[DataMember]
		public string Spvnam
		{
			get
			{
				return this._spvnam;
			}
			set
			{
				this._spvnam = value;
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x060041B1 RID: 16817 RVA: 0x000DCD07 File Offset: 0x000DAF07
		// (set) Token: 0x060041B2 RID: 16818 RVA: 0x000DCD0F File Offset: 0x000DAF0F
		[DataMember]
		public string Srvclsnm
		{
			get
			{
				return this._srvclsnm;
			}
			set
			{
				this._srvclsnm = value;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x060041B3 RID: 16819 RVA: 0x000DCD18 File Offset: 0x000DAF18
		// (set) Token: 0x060041B4 RID: 16820 RVA: 0x000DCD20 File Offset: 0x000DAF20
		[DataMember]
		public string Srvnam
		{
			get
			{
				return this._srvnam;
			}
			set
			{
				this._srvnam = value;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x060041B5 RID: 16821 RVA: 0x000DCD29 File Offset: 0x000DAF29
		// (set) Token: 0x060041B6 RID: 16822 RVA: 0x000DCD31 File Offset: 0x000DAF31
		[DataMember]
		public string Srvrlslv
		{
			get
			{
				return this._srvrlslv;
			}
			set
			{
				this._srvrlslv = value;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x060041B7 RID: 16823 RVA: 0x000DCD3A File Offset: 0x000DAF3A
		// (set) Token: 0x060041B8 RID: 16824 RVA: 0x000DCD42 File Offset: 0x000DAF42
		[DataMember]
		public string RemoteAddress
		{
			get
			{
				return this._remoteAddress;
			}
			set
			{
				this._remoteAddress = value;
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x060041B9 RID: 16825 RVA: 0x000DCD4B File Offset: 0x000DAF4B
		// (set) Token: 0x060041BA RID: 16826 RVA: 0x000DCD53 File Offset: 0x000DAF53
		[DataMember]
		public ConnectionState State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x04002E49 RID: 11849
		private string _id;

		// Token: 0x04002E4A RID: 11850
		private string _extnam;

		// Token: 0x04002E4B RID: 11851
		private string _spvnam;

		// Token: 0x04002E4C RID: 11852
		private string _srvclsnm;

		// Token: 0x04002E4D RID: 11853
		private string _srvnam;

		// Token: 0x04002E4E RID: 11854
		private string _srvrlslv;

		// Token: 0x04002E4F RID: 11855
		private string _remoteAddress;

		// Token: 0x04002E50 RID: 11856
		private ConnectionState _state;
	}
}
