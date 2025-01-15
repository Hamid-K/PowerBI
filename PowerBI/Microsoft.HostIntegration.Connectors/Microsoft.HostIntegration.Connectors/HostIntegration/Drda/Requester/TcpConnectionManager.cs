using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Numerics;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.DDM;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.DrdaClient;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x02000983 RID: 2435
	internal class TcpConnectionManager : ConnectionManager
	{
		// Token: 0x06004B5F RID: 19295 RVA: 0x0012C3B2 File Offset: 0x0012A5B2
		public TcpConnectionManager(Requester requester)
			: base(requester)
		{
			this._tracePoint = new TcpConnectionManagerTracePoint(requester.TracePoint);
			this._managerCodepoint = ManagerCodePoint.CMNTCPIP;
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x0012C3D7 File Offset: 0x0012A5D7
		public override void Initialize()
		{
			base.Initialize();
			this.ResetValues();
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06004B61 RID: 19297 RVA: 0x0012C3E5 File Offset: 0x0012A5E5
		public override Stream Stream
		{
			get
			{
				if (this._networkStream != null)
				{
					return this._networkStream;
				}
				return this._sslStream;
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06004B62 RID: 19298 RVA: 0x0012C3FC File Offset: 0x0012A5FC
		public override bool Connected
		{
			get
			{
				return this._tcpClient != null && this._tcpClient.Connected;
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06004B63 RID: 19299 RVA: 0x0012C413 File Offset: 0x0012A613
		// (set) Token: 0x06004B64 RID: 19300 RVA: 0x0012C41B File Offset: 0x0012A61B
		public bool PerformanceCountersOn
		{
			get
			{
				return this._perfcountersOn;
			}
			set
			{
				this._perfcountersOn = value;
			}
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06004B65 RID: 19301 RVA: 0x0012C424 File Offset: 0x0012A624
		public override DdmWriter DdmWriter
		{
			get
			{
				return this._ddmWriter;
			}
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06004B66 RID: 19302 RVA: 0x0012C42C File Offset: 0x0012A62C
		public override DdmReader DdmReader
		{
			get
			{
				return this._ddmReader;
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06004B67 RID: 19303 RVA: 0x0012C434 File Offset: 0x0012A634
		// (set) Token: 0x06004B68 RID: 19304 RVA: 0x0012C43C File Offset: 0x0012A63C
		public Converter Converter
		{
			get
			{
				return this._primitiveConverter;
			}
			set
			{
				this._primitiveConverter = value;
			}
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x0012C448 File Offset: 0x0012A648
		public override void Disconnect()
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "TcpConnectionManager::Disconnect()");
			}
			if (this._sslStream != null)
			{
				try
				{
					this._sslStream.Close();
				}
				catch (Exception ex)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Warning))
					{
						this._tracePoint.Trace(TraceFlags.Warning, "TcpConnectionManager::Disconnect(), error on closing SslStream:" + ex.ToString());
					}
				}
				this._sslStream = null;
			}
			if (this._networkStream != null)
			{
				try
				{
					this._networkStream.Close();
				}
				catch (Exception ex2)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Warning))
					{
						this._tracePoint.Trace(TraceFlags.Warning, "TcpConnectionManager::Disconnect(), error on closing NetworkStream: " + ex2.ToString());
					}
				}
				this._networkStream = null;
			}
			if (this._tcpClient != null)
			{
				this._tcpClient.Close();
				this._tcpClient = null;
			}
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x0012C53C File Offset: 0x0012A73C
		public override async Task ConnectAsync(X509Certificate clientCert, bool isAsync, CancellationToken cancellationToken)
		{
			string text = this._requester.ConnectionInfo[10];
			string text2 = this._requester.ConnectionInfo[11];
			string text3 = this._requester.ConnectionInfo[37];
			string rdbName = this._requester.RdbName;
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "TcpConnectionManager::Connect()::Server=" + text + ", Port=" + text2);
			}
			this._tcpClient = new TcpClient();
			this._tcpClient.NoDelay = this._tcpNoDelay;
			int num = 0;
			bool flag = false;
			int num2 = -1;
			Tuple<object, int, ushort> tuple = null;
			IEnumerator<Tuple<object, int, ushort>> enumerator = null;
			if (this._loadBalancing)
			{
				enumerator = this.GetTargetServer(rdbName).GetEnumerator();
				if (enumerator.MoveNext())
				{
					tuple = enumerator.Current;
				}
			}
			bool flag2 = false;
			do
			{
				try
				{
					if (num2 == -1)
					{
						num2 = int.Parse(text2);
					}
					if (tuple == null)
					{
						this._tcpClient.Connect(text, num2);
					}
					else if (tuple.Item1 is IPAddress)
					{
						this._tcpClient.Connect((IPAddress)tuple.Item1, tuple.Item2);
					}
					else
					{
						this._tcpClient.Connect((string)tuple.Item1, tuple.Item2);
					}
					if (!this._tcpClient.Connected)
					{
						if (this._tracePoint.IsEnabled(TraceFlags.Error))
						{
							this._tracePoint.Trace(TraceFlags.Error, string.Concat(new string[] { "TcpConnectionManager::Connect()::Server=", text, ", Port=", text2, "  failed due to connect timeout." }));
						}
						flag2 = true;
						throw this._requester.MakeException(RequesterResource.NETWORK_CONNECT_TIMEOUT, "HYT01", -7049);
					}
					flag = false;
				}
				catch (SocketException ex)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "TcpConnectionManager::Connect(), failed to connect to server: " + ex.Message + ex.ToString());
					}
					if (this._loadBalancing)
					{
						if (enumerator.MoveNext())
						{
							tuple = enumerator.Current;
							flag = true;
						}
						else
						{
							flag = false;
						}
					}
					else if (num < 1)
					{
						num++;
						flag = true;
					}
					else
					{
						flag = false;
					}
					if (!flag)
					{
						int num3 = ((ex.SocketErrorCode == SocketError.HostNotFound) ? (-1336) : (-1037));
						flag2 = true;
						throw this._requester.MakeException(ex.Message, "08S01", num3, ex.ErrorCode);
					}
				}
				catch (SystemException ex2)
				{
					if (flag2)
					{
						throw;
					}
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "TcpConnectionManager::Connect(), failed to connect to server: " + ex2.ToString());
					}
					throw this._requester.MakeException(ex2.Message, "HY000", -1037, ex2.HResult);
				}
			}
			while (flag);
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "TcpConnectionManager::Connect(), connect to server successfully");
			}
			NetworkStream stream = this._tcpClient.GetStream();
			if (string.IsNullOrWhiteSpace(text3))
			{
				this._networkStream = stream;
				this._sslStream = null;
			}
			else
			{
				bool isSslOK = false;
				try
				{
					this._networkStream = null;
					LocalCertificateSelectionCallback localCertificateSelectionCallback = null;
					X509CertificateCollection x509CertificateCollection = null;
					if (clientCert != null)
					{
						x509CertificateCollection = new X509CertificateCollection();
						x509CertificateCollection.Add(clientCert);
						localCertificateSelectionCallback = delegate(object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
						{
							if (localCertificates != null && localCertificates.Count > 0)
							{
								if (acceptableIssuers != null && acceptableIssuers.Length != 0)
								{
									using (X509CertificateCollection.X509CertificateEnumerator enumerator2 = localCertificates.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											X509Certificate x509Certificate = enumerator2.Current;
											string issuer = x509Certificate.Issuer;
											if (Array.IndexOf<string>(acceptableIssuers, issuer) != -1)
											{
												return x509Certificate;
											}
										}
										goto IL_0061;
									}
								}
								return localCertificates[0];
							}
							IL_0061:
							return null;
						};
					}
					this._sslStream = new SslStream(stream, false, delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
					{
						if (sslPolicyErrors.Equals(SslPolicyErrors.None))
						{
							return true;
						}
						if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) > SslPolicyErrors.None)
						{
							if (chain == null || chain.ChainStatus == null || chain.ChainStatus.Length == 0)
							{
								return false;
							}
							foreach (X509ChainStatus x509ChainStatus in chain.ChainStatus)
							{
								if (x509ChainStatus.Status != X509ChainStatusFlags.RevocationStatusUnknown && x509ChainStatus.Status != X509ChainStatusFlags.OfflineRevocation)
								{
									return false;
								}
							}
						}
						return true;
					}, localCertificateSelectionCallback, EncryptionPolicy.RequireEncryption);
					if (isAsync)
					{
						await this._sslStream.AuthenticateAsClientAsync(text3, x509CertificateCollection, SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12, true);
					}
					else
					{
						this._sslStream.AuthenticateAsClient(text3, x509CertificateCollection, SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12, true);
					}
					isSslOK = true;
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "TcpConnectionManager::Connect(), Successful to connect to SSL server.");
					}
				}
				catch (AuthenticationException ex3)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "TcpConnectionManager::Connect(), failed to connect to SSL server: " + ex3.ToString());
					}
					throw this._requester.MakeException(ex3.Message, "08S01", -1038, ex3.HResult);
				}
				catch (Exception ex4)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "TcpConnectionManager::Connect(), failed to connect to SSL server: " + ex4.ToString());
					}
					throw this._requester.MakeException(ex4.Message, "HY000", -1038, ex4.HResult);
				}
				finally
				{
					if (!isSslOK)
					{
						this.Reset();
					}
				}
			}
			this._ddmWriter.Reset(this.Stream);
			this._ddmReader.Reset(this.Stream);
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x0012C594 File Offset: 0x0012A794
		public override bool ProcessDdmObject(AbstractDdmObject ddmObject)
		{
			if (!(ddmObject is SRVLST) || !this._loadBalancing)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "TcpConnectionManager::ProcessDdmObject(), No need processing SRVLST.");
				}
				return false;
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "TcpConnectionManager::ProcessDdmObject(), Processing SRVLST...");
			}
			SRVLST srvlst = (SRVLST)ddmObject;
			if (srvlst.List.Count == 0)
			{
				return false;
			}
			List<Tuple<object, int, ushort>> list = new List<Tuple<object, int, ushort>>();
			foreach (SRVLSRV srvlsrv in srvlst.List)
			{
				if (!string.IsNullOrWhiteSpace(srvlsrv.TcpHost))
				{
					list.Add(new Tuple<object, int, ushort>(srvlsrv.TcpHost.ToUpperInvariant(), srvlsrv.Tcpport, srvlsrv.Srvprty));
				}
				else if (srvlsrv.Tcpaddr != null)
				{
					list.Add(new Tuple<object, int, ushort>(srvlsrv.Tcpaddr, srvlsrv.Tcpport, srvlsrv.Srvprty));
				}
			}
			list.Sort(new Comparison<Tuple<object, int, ushort>>(TcpConnectionManager.CompareServerEntry));
			string rdbName = this._requester.RdbName;
			TcpConnectionManager._serverListLock.EnterReadLock();
			bool flag = this.IsServerListDirty(rdbName, list);
			TcpConnectionManager._serverListLock.ExitReadLock();
			if (flag)
			{
				TcpConnectionManager._serverListLock.EnterWriteLock();
				TcpConnectionManager._serverList[rdbName] = list;
				TcpConnectionManager._serverListLock.ExitWriteLock();
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "TcpConnectionManager::ProcessDdmObject(), SRVLST has been updated for " + rdbName);
				}
			}
			else if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "TcpConnectionManager::ProcessDdmObject(), SRVLST has no change for " + rdbName);
			}
			return true;
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x0012C754 File Offset: 0x0012A954
		public override void Reset()
		{
			this.Disconnect();
			base.Reset();
			this.ResetValues();
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x0012C768 File Offset: 0x0012A968
		private IEnumerable<Tuple<object, int, ushort>> GetTargetServer(string rdbName)
		{
			Tuple<object, int, ushort> result = null;
			List<Tuple<object, int, ushort>> listServer = null;
			List<Tuple<object, int, ushort>> currentListServer = null;
			TcpConnectionManager._serverListLock.EnterReadLock();
			TcpConnectionManager._serverList.TryGetValue(rdbName, out listServer);
			TcpConnectionManager._serverListLock.ExitReadLock();
			if (listServer == null)
			{
				yield break;
			}
			int retryTimes = 0;
			int prioritySum = -1;
			do
			{
				if (prioritySum == -1)
				{
					prioritySum = listServer.Sum((Tuple<object, int, ushort> serverEntry) => (int)serverEntry.Item3);
				}
				Random random = new Random();
				int currentPrioritySum = prioritySum;
				do
				{
					if (currentPrioritySum == prioritySum)
					{
						currentListServer = listServer;
					}
					int priority = random.Next(currentPrioritySum);
					int currentSum = 0;
					int index = currentListServer.FindIndex(delegate(Tuple<object, int, ushort> serverEntry)
					{
						currentSum += (int)serverEntry.Item3;
						return priority <= currentSum;
					});
					result = currentListServer[index];
					yield return result;
					if (currentListServer == listServer)
					{
						currentListServer = listServer.ToList<Tuple<object, int, ushort>>();
					}
					currentListServer.RemoveAt(index);
					currentPrioritySum -= (int)result.Item3;
				}
				while (currentPrioritySum > 0);
				int num = retryTimes + 1;
				retryTimes = num;
				random = null;
			}
			while (retryTimes < 2);
			IEnumerable<Tuple<object, int, ushort>> failOverList = listServer.Where((Tuple<object, int, ushort> serverEntry) => serverEntry.Item3 == 0);
			retryTimes = 0;
			do
			{
				foreach (Tuple<object, int, ushort> tuple in failOverList)
				{
					yield return tuple;
				}
				IEnumerator<Tuple<object, int, ushort>> enumerator = null;
				int num = retryTimes + 1;
				retryTimes = num;
			}
			while (retryTimes < 2);
			yield break;
			yield break;
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x0012C778 File Offset: 0x0012A978
		private static int CompareServerEntry(Tuple<object, int, ushort> server1, Tuple<object, int, ushort> server2)
		{
			if (server1 == null)
			{
				if (server2 == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (server2 == null)
				{
					return 1;
				}
				Type type = server1.Item1.GetType();
				Type type2 = server2.Item1.GetType();
				if (type != type2)
				{
					if (type == typeof(string))
					{
						return 1;
					}
					return -1;
				}
				else
				{
					int num = 0;
					if (type == typeof(string))
					{
						num = string.Compare((string)server1.Item1, (string)server2.Item1);
					}
					else if (type == typeof(IPAddress))
					{
						BigInteger bigInteger = new BigInteger(((IPAddress)server1.Item1).GetAddressBytes());
						BigInteger bigInteger2 = new BigInteger(((IPAddress)server2.Item1).GetAddressBytes());
						num = BigInteger.Compare(bigInteger, bigInteger2);
					}
					if (num != 0)
					{
						return num;
					}
					num = server1.Item2 - server2.Item2;
					if (num != 0)
					{
						return num;
					}
					return (int)(server1.Item3 - server2.Item3);
				}
			}
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x0012C868 File Offset: 0x0012AA68
		private bool IsServerListDirty(string rdbName, List<Tuple<object, int, ushort>> listServer)
		{
			List<Tuple<object, int, ushort>> list = null;
			if (!TcpConnectionManager._serverList.TryGetValue(rdbName, out list))
			{
				return true;
			}
			if (listServer.Count != list.Count)
			{
				return true;
			}
			for (int i = 0; i < listServer.Count; i++)
			{
				if (TcpConnectionManager.CompareServerEntry(listServer[i], list[i]) != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x0012C8C4 File Offset: 0x0012AAC4
		private void ResetValues()
		{
			this._level = (Dns.GetHostEntry(Dns.GetHostName()).AddressList.Any((IPAddress addr) => addr.AddressFamily == AddressFamily.InterNetworkV6) ? 8 : 5);
			if (this._primitiveConverter == null || (this._requester.TracePoint != null && this._tracePoint.TraceContainer != this._requester.TracePoint.TraceContainer))
			{
				this._tracePoint = new TcpConnectionManagerTracePoint(this._requester.TracePoint);
				this._primitiveConverter = new Converter(null, this._requester.CommonTracePoint);
				this._ddmWriter = new DdmWriter(null, EbcdicManager.Instance, this._primitiveConverter, null, 0, this._requester.CommonTracePoint);
				this._ddmReader = new DdmReader(null, EbcdicManager.Instance, this._primitiveConverter, null, this._requester.CommonTracePoint);
				this._ddmReader.IsManagedMsDrda = true;
			}
			else
			{
				this._ddmReader.CcsidManager = EbcdicManager.Instance;
				this._ddmWriter.CcsidManager = EbcdicManager.Instance;
			}
			Ccsid ccsid = new Ccsid
			{
				_ccsidsbc = 37,
				_ccsidmbc = 37
			};
			this._ddmReader.EndianType = this._requester.Endian;
			this._ddmReader.Ccsid = ccsid;
			this._ddmWriter.EndianType = this._requester.Endian;
			this._ddmWriter.Ccsid = ccsid;
		}

		// Token: 0x04003B5A RID: 15194
		private static Dictionary<string, List<Tuple<object, int, ushort>>> _serverList = new Dictionary<string, List<Tuple<object, int, ushort>>>();

		// Token: 0x04003B5B RID: 15195
		private static ReaderWriterLockSlim _serverListLock = new ReaderWriterLockSlim();

		// Token: 0x04003B5C RID: 15196
		private NetworkStream _networkStream;

		// Token: 0x04003B5D RID: 15197
		private SslStream _sslStream;

		// Token: 0x04003B5E RID: 15198
		private DdmWriter _ddmWriter;

		// Token: 0x04003B5F RID: 15199
		private DdmReader _ddmReader;

		// Token: 0x04003B60 RID: 15200
		private TcpClient _tcpClient;

		// Token: 0x04003B61 RID: 15201
		private bool _tcpNoDelay;

		// Token: 0x04003B62 RID: 15202
		private bool _perfcountersOn;

		// Token: 0x04003B63 RID: 15203
		private Converter _primitiveConverter;
	}
}
