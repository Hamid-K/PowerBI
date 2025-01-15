using System;
using System.Net;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008E0 RID: 2272
	public class SRVLSRV
	{
		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x060047FD RID: 18429 RVA: 0x00105BD6 File Offset: 0x00103DD6
		// (set) Token: 0x060047FE RID: 18430 RVA: 0x00105BDE File Offset: 0x00103DDE
		public ushort Srvprty
		{
			get
			{
				return this._srvprty;
			}
			set
			{
				this._srvprty = value;
			}
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x060047FF RID: 18431 RVA: 0x00105BE7 File Offset: 0x00103DE7
		// (set) Token: 0x06004800 RID: 18432 RVA: 0x00105BEF File Offset: 0x00103DEF
		public object TracePoint
		{
			get
			{
				return this._tracePoint;
			}
			set
			{
				this._tracePoint = value;
			}
		}

		// Token: 0x06004801 RID: 18433 RVA: 0x00105BF8 File Offset: 0x00103DF8
		public SRVLSRV(IPEndPoint endPoint, bool prim)
		{
			this._tcpaddr = endPoint.Address;
			this._tcpport = endPoint.Port;
			this._isPrimary = prim;
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x00105C1F File Offset: 0x00103E1F
		public SRVLSRV(IPAddress tcpaddr, int port, bool prim)
		{
			this._tcpaddr = tcpaddr;
			this._tcpport = port;
			this._isPrimary = prim;
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x00002061 File Offset: 0x00000261
		public SRVLSRV()
		{
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x06004804 RID: 18436 RVA: 0x00105C3C File Offset: 0x00103E3C
		// (set) Token: 0x06004805 RID: 18437 RVA: 0x00105C44 File Offset: 0x00103E44
		public bool IsPrimary
		{
			get
			{
				return this._isPrimary;
			}
			set
			{
				this._isPrimary = value;
			}
		}

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x06004806 RID: 18438 RVA: 0x00105C4D File Offset: 0x00103E4D
		// (set) Token: 0x06004807 RID: 18439 RVA: 0x00105C55 File Offset: 0x00103E55
		public int Tcpport
		{
			get
			{
				return this._tcpport;
			}
			set
			{
				this._tcpport = value;
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x06004808 RID: 18440 RVA: 0x00105C5E File Offset: 0x00103E5E
		// (set) Token: 0x06004809 RID: 18441 RVA: 0x00105C66 File Offset: 0x00103E66
		public IPAddress Tcpaddr
		{
			get
			{
				return this._tcpaddr;
			}
			set
			{
				this._tcpaddr = value;
			}
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x0600480A RID: 18442 RVA: 0x00105C6F File Offset: 0x00103E6F
		// (set) Token: 0x0600480B RID: 18443 RVA: 0x00105C77 File Offset: 0x00103E77
		public string TcpHost
		{
			get
			{
				return this._tcpHost;
			}
			set
			{
				this._tcpHost = value;
			}
		}

		// Token: 0x0600480C RID: 18444 RVA: 0x00105C80 File Offset: 0x00103E80
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			SRVLSRV srvlsrv = (SRVLSRV)obj;
			return this._tcpaddr.Equals(srvlsrv.Tcpaddr) && this._tcpport == srvlsrv.Tcpport && this._isPrimary == srvlsrv.IsPrimary && string.Compare(this._tcpHost, srvlsrv.TcpHost, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x00105CF2 File Offset: 0x00103EF2
		public override int GetHashCode()
		{
			return this._tcpaddr.GetHashCode() & this._tcpport.GetHashCode();
		}

		// Token: 0x0600480E RID: 18446 RVA: 0x00105D0B File Offset: 0x00103F0B
		internal SRVLSRV Clone()
		{
			return new SRVLSRV(this.Tcpaddr, this.Tcpport, this.IsPrimary)
			{
				TracePoint = this._tracePoint,
				TcpHost = this._tcpHost
			};
		}

		// Token: 0x04003495 RID: 13461
		private int _tcpport;

		// Token: 0x04003496 RID: 13462
		private IPAddress _tcpaddr;

		// Token: 0x04003497 RID: 13463
		private bool _isPrimary;

		// Token: 0x04003498 RID: 13464
		private ushort _srvprty;

		// Token: 0x04003499 RID: 13465
		private object _tracePoint;

		// Token: 0x0400349A RID: 13466
		private string _tcpHost;
	}
}
