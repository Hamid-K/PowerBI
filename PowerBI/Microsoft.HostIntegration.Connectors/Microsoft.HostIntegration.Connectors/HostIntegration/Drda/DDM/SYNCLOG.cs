using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008E5 RID: 2277
	public class SYNCLOG : AbstractDdmObject
	{
		// Token: 0x0600482C RID: 18476 RVA: 0x00106834 File Offset: 0x00104A34
		static SYNCLOG()
		{
			string text = Dns.GetHostName();
			if (text.Length > 8)
			{
				text = text.Substring(0, 8);
			}
			else if (text.Length < 8)
			{
				text = text.PadRight(8, '0');
			}
			SYNCLOG._myLogName = "MSFT." + text.ToUpperInvariant();
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x0600482D RID: 18477 RVA: 0x001068B0 File Offset: 0x00104AB0
		// (set) Token: 0x0600482E RID: 18478 RVA: 0x001068B8 File Offset: 0x00104AB8
		public SYNCLOG SyncLog
		{
			get
			{
				return this._syncLog;
			}
			set
			{
				this._syncLog = value;
			}
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x001068C4 File Offset: 0x00104AC4
		public override string ToString()
		{
			return string.Format("SYNCLOG[rdbnam={0};lognam={1};svrlogtstmp={2};cnntkn={3};tcphost={4};ipaddport={5}]", new object[]
			{
				this._rdbName,
				this._logName,
				this._logtstmp,
				BitUtils.ConvertToHexString(this._cnntkn),
				this._tcpHost,
				BitUtils.ConvertToHexString(this._ipAddrPort)
			});
		}

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x06004830 RID: 18480 RVA: 0x00106921 File Offset: 0x00104B21
		// (set) Token: 0x06004831 RID: 18481 RVA: 0x00106929 File Offset: 0x00104B29
		public string RdbName
		{
			get
			{
				return this._rdbName;
			}
			set
			{
				this._rdbName = value;
			}
		}

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x06004832 RID: 18482 RVA: 0x00106932 File Offset: 0x00104B32
		// (set) Token: 0x06004833 RID: 18483 RVA: 0x0010693A File Offset: 0x00104B3A
		public string LogName
		{
			get
			{
				return this._logName;
			}
			set
			{
				this._logName = value;
			}
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x06004834 RID: 18484 RVA: 0x00106943 File Offset: 0x00104B43
		// (set) Token: 0x06004835 RID: 18485 RVA: 0x0010694B File Offset: 0x00104B4B
		public byte[] Cnntkn
		{
			get
			{
				return this._cnntkn;
			}
			set
			{
				if (value != null && this._cnntkn.Length == 4)
				{
					Array.Copy(value, this._cnntkn, 4);
				}
			}
		}

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x06004836 RID: 18486 RVA: 0x00106968 File Offset: 0x00104B68
		// (set) Token: 0x06004837 RID: 18487 RVA: 0x00106970 File Offset: 0x00104B70
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

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x06004838 RID: 18488 RVA: 0x00106979 File Offset: 0x00104B79
		// (set) Token: 0x06004839 RID: 18489 RVA: 0x00106981 File Offset: 0x00104B81
		public byte[] IpAddrPort
		{
			get
			{
				return this._ipAddrPort;
			}
			set
			{
				this._ipAddrPort = value;
			}
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x0600483A RID: 18490 RVA: 0x0010698A File Offset: 0x00104B8A
		// (set) Token: 0x0600483B RID: 18491 RVA: 0x00106992 File Offset: 0x00104B92
		public string LogStamp
		{
			get
			{
				return this._logtstmp;
			}
			set
			{
				this._logtstmp = value;
			}
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x0010699C File Offset: 0x00104B9C
		public SYNCLOG(int sessionId, string resyncIpAddress, int resyncPort)
		{
			this._resyncIpAddress = resyncIpAddress;
			this._resyncPort = resyncPort;
			this._sessionId = sessionId;
			this._tcpHost = SYNCLOG._tcpMyHost;
			this._logName = SYNCLOG._myLogName;
			this._logtstmp = SYNCLOG._svrlogtstmp;
			byte[] array = new byte[4];
			Array.Copy(SYNCLOG._ipaddress, array, 4);
			if (!string.IsNullOrEmpty(this._resyncIpAddress))
			{
				IPAddress ipaddress = null;
				if (IPAddress.TryParse(this._resyncIpAddress, out ipaddress))
				{
					array = ipaddress.GetAddressBytes();
				}
			}
			this._cnntkn = new byte[] { 181, 140, 147, 77 };
			this._ipAddrPort = new byte[6];
			byte[] bytes = BitConverter.GetBytes(this._resyncPort);
			Array.Copy(array, this._ipAddrPort, 4);
			this._ipAddrPort[4] = bytes[1];
			this._ipAddrPort[5] = bytes[0];
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x00106A70 File Offset: 0x00104C70
		public SYNCLOG Clone()
		{
			SYNCLOG synclog = new SYNCLOG(this._sessionId, this._resyncIpAddress, this._resyncPort);
			synclog._cnntkn = new byte[this._cnntkn.Length];
			Array.Copy(this._cnntkn, synclog._cnntkn, this._cnntkn.Length);
			synclog._ipAddrPort = new byte[this._ipAddrPort.Length];
			Array.Copy(this._ipAddrPort, synclog._ipAddrPort, this._ipAddrPort.Length);
			synclog._logName = this._logName;
			synclog._rdbName = this._rdbName;
			synclog._tcpHost = this._tcpHost;
			synclog._logtstmp = this._logtstmp;
			return synclog;
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x00106B20 File Offset: 0x00104D20
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.SYNCLOG);
			writer.WriteScalarPaddedString(CodePoint.RDBNAM, this._rdbName, 18);
			Logger.Verbose(this._tracePoint, this._sessionId, "Writing SYNCLOG, LOGNAME:" + this._logName, Array.Empty<object>());
			writer.WriteScalarPaddedString(CodePoint.LOGNAME, this._logName, 18);
			Logger.Verbose(this._tracePoint, this._sessionId, "Writing SYNCLOG, LOGTSTMP:" + this._logtstmp, Array.Empty<object>());
			writer.WriteScalarPaddedString(CodePoint.LOGTSTMP, this._logtstmp, 18);
			writer.WriteScalarBytes(CodePoint.CNNTKN, this._cnntkn);
			Logger.Verbose(this._tracePoint, this._sessionId, "Writing SYNCLOG, IPAddress+Port:" + this.ConvertBytesToHexString(this._ipAddrPort), Array.Empty<object>());
			writer.WriteScalarBytes(CodePoint.IPADDR, this._ipAddrPort);
			writer.WriteScalarBytes(CodePoint.TCPHOST, Encoding.GetEncoding(500).GetBytes(this._tcpHost));
			writer.WriteEndDdm();
		}

		// Token: 0x0600483F RID: 18495 RVA: 0x00106C34 File Offset: 0x00104E34
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			IEnumerator<Task<ObjectInfo>> taskEnumerator = (isAsync ? reader.ReadDdmObjectsAsync(cancellationToken).GetEnumerator() : null);
			IEnumerator<ObjectInfo> enumerator = (isAsync ? null : reader.ReadDdmObjects().GetEnumerator());
			while (isAsync ? taskEnumerator.MoveNext() : enumerator.MoveNext())
			{
				ObjectInfo objectInfo;
				if (isAsync)
				{
					objectInfo = await taskEnumerator.Current;
					if (objectInfo.Equals(ObjectInfo.InvalidInstance))
					{
						break;
					}
				}
				else
				{
					objectInfo = enumerator.Current;
				}
				CodePoint codepoint = objectInfo.Codepoint;
				base.LogCodePoint(codepoint);
				if (codepoint <= CodePoint.LOGNAME)
				{
					if (codepoint == CodePoint.SYNCLOG)
					{
						this._syncLog = new SYNCLOG(this._sessionId, this._resyncIpAddress, this._resyncPort);
						await this._syncLog.ReadAsync(reader, isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.CNNTKN)
					{
						this._cnntkn = await reader.ReadBytesAsync(4, isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.LOGNAME)
					{
						this._logName = reader.ReadString(18, 500);
						continue;
					}
				}
				else if (codepoint <= CodePoint.TCPHOST)
				{
					if (codepoint == CodePoint.LOGTSTMP)
					{
						this._logtstmp = reader.ReadString(18, 500);
						continue;
					}
					if (codepoint == CodePoint.TCPHOST)
					{
						this._tcpHost = reader.ReadString((int)reader.DdmObjectLength, 500);
						continue;
					}
				}
				else
				{
					if (codepoint == CodePoint.IPADDR)
					{
						this._ipAddrPort = await reader.ReadBytesAsync(6, isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.RDBNAM)
					{
						this._rdbName = reader.ReadString(18, 500);
						continue;
					}
				}
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "SYNCLOG::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x00106C94 File Offset: 0x00104E94
		private string ConvertBytesToHexString(byte[] ba)
		{
			StringBuilder stringBuilder = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x00106CD8 File Offset: 0x00104ED8
		private static byte[] GetHostAddressInBytes()
		{
			foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback && (!networkInterface.Description.Contains("Failover") || !networkInterface.Description.Contains("Cluster") || !networkInterface.Description.Contains("Virtual")))
				{
					UnicastIPAddressInformationCollection unicastAddresses = networkInterface.GetIPProperties().UnicastAddresses;
					if (unicastAddresses.Count > 0)
					{
						foreach (IPAddressInformation ipaddressInformation in unicastAddresses)
						{
							if (ipaddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
							{
								return ipaddressInformation.Address.GetAddressBytes();
							}
						}
					}
				}
			}
			return new byte[4];
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x00106DC0 File Offset: 0x00104FC0
		private static string DateTimeToString(DateTime timestamp)
		{
			int year = timestamp.Year;
			int month = timestamp.Month;
			int day = timestamp.Day;
			int hour = timestamp.Hour;
			int minute = timestamp.Minute;
			int second = timestamp.Second;
			int millisecond = timestamp.Millisecond;
			char[] array = new char[18];
			int num = 48;
			array[0] = (char)(year / 1000 + num);
			array[1] = (char)(year % 1000 / 100 + num);
			array[2] = (char)(year % 100 / 10 + num);
			array[3] = (char)(year % 10 + num);
			array[4] = (char)(month / 10 + num);
			array[5] = (char)(month % 10 + num);
			array[6] = (char)(day / 10 + num);
			array[7] = (char)(day % 10 + num);
			array[8] = (char)(hour / 10 + num);
			array[9] = (char)(hour % 10 + num);
			array[10] = (char)(minute / 10 + num);
			array[11] = (char)(minute % 10 + num);
			array[12] = (char)(second / 10 + num);
			array[13] = (char)(second % 10 + num);
			array[14] = (char)(millisecond / 100000 + num);
			array[15] = (char)(millisecond % 100000 / 10000 + num);
			array[16] = (char)(millisecond % 10000 / 1000 + num);
			array[17] = (char)(millisecond % 1000 / 100 + num);
			return new string(array);
		}

		// Token: 0x040034BD RID: 13501
		private static string _svrlogtstmp = SYNCLOG.DateTimeToString(DateTime.Now);

		// Token: 0x040034BE RID: 13502
		private static string _tcpMyHost = Dns.GetHostEntry("").HostName;

		// Token: 0x040034BF RID: 13503
		private static string _myLogName;

		// Token: 0x040034C0 RID: 13504
		private string _rdbName;

		// Token: 0x040034C1 RID: 13505
		private string _logName;

		// Token: 0x040034C2 RID: 13506
		private string _logtstmp;

		// Token: 0x040034C3 RID: 13507
		private byte[] _cnntkn;

		// Token: 0x040034C4 RID: 13508
		private byte[] _ipAddrPort;

		// Token: 0x040034C5 RID: 13509
		private string _tcpHost;

		// Token: 0x040034C6 RID: 13510
		private static byte[] _ipaddress = SYNCLOG.GetHostAddressInBytes();

		// Token: 0x040034C7 RID: 13511
		private SYNCLOG _syncLog;

		// Token: 0x040034C8 RID: 13512
		private string _resyncIpAddress;

		// Token: 0x040034C9 RID: 13513
		private int _resyncPort;

		// Token: 0x040034CA RID: 13514
		private int _sessionId;
	}
}
