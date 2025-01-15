using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008DE RID: 2270
	public class SRVLST : AbstractDdmObject
	{
		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x060047E7 RID: 18407 RVA: 0x00104DF9 File Offset: 0x00102FF9
		// (set) Token: 0x060047E8 RID: 18408 RVA: 0x00104E01 File Offset: 0x00103001
		public bool ForFailover
		{
			get
			{
				return this._forFailover;
			}
			set
			{
				this._forFailover = value;
			}
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x060047E9 RID: 18409 RVA: 0x00104E0A File Offset: 0x0010300A
		public HashSet<SRVLSRV> List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x00104E12 File Offset: 0x00103012
		public override string ToString()
		{
			return string.Format("SRVLST{0}", this.GetSrvLstAsString());
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x00104E24 File Offset: 0x00103024
		private string GetSrvLstAsString()
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			foreach (SRVLSRV srvlsrv in this._list)
			{
				stringBuilder.Append(string.Format("[ipaddress={0};port={1};isprimary={2}]", srvlsrv.Tcpaddr, srvlsrv.Tcpport, srvlsrv.IsPrimary));
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x00104EBC File Offset: 0x001030BC
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.SRVLST);
			writer.WriteScalar2Bytes(CodePoint.SRVLCNT, this._list.Count);
			writer.WriteBeginDdm(CodePoint.SRVLSRV);
			foreach (SRVLSRV srvlsrv in this._list)
			{
				if (this.ForFailover)
				{
					if (srvlsrv.IsPrimary)
					{
						writer.WriteScalar2Bytes(CodePoint.SRVPRTY, (int)this.MaxFailOverSRVPRTY);
					}
					else
					{
						writer.WriteScalar2Bytes(CodePoint.SRVPRTY, (int)this.MinFailOverSRVPRTY);
					}
				}
				else if (srvlsrv.IsPrimary)
				{
					writer.WriteScalar2Bytes(CodePoint.SRVPRTY, (int)this.MaxSRVPRTY);
				}
				else
				{
					writer.WriteScalar2Bytes(CodePoint.SRVPRTY, (int)this.MinSRVPRTY);
				}
				writer.WriteBeginDdm(CodePoint.IPADDR);
				writer.WriteBytes(srvlsrv.Tcpaddr.GetAddressBytes());
				writer.WriteInt16(srvlsrv.Tcpport, EndianType.BigEndian);
				writer.WriteEndDdm();
			}
			writer.WriteEndDdm();
			writer.WriteEndDdm();
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x00104FD8 File Offset: 0x001031D8
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			try
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
					if (codepoint != CodePoint.SRVLCNT)
					{
						if (codepoint != CodePoint.SRVLSRV)
						{
							if (Logger.maxTracingLevel >= 4)
							{
								Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "SRVLST::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
							}
							await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
						}
						else
						{
							SRVLSRV srvlsrv = null;
							IEnumerator<Task<ObjectInfo>> taskEnumerator2 = (isAsync ? reader.ReadDdmObjectsAsync(cancellationToken).GetEnumerator() : null);
							IEnumerator<ObjectInfo> enumerator2 = (isAsync ? null : reader.ReadDdmObjects().GetEnumerator());
							while (isAsync ? taskEnumerator2.MoveNext() : enumerator2.MoveNext())
							{
								ObjectInfo objectInfo2;
								if (isAsync)
								{
									objectInfo2 = await taskEnumerator2.Current;
									if (objectInfo2.Equals(ObjectInfo.InvalidInstance))
									{
										break;
									}
								}
								else
								{
									objectInfo2 = enumerator2.Current;
								}
								CodePoint codepoint2 = objectInfo2.Codepoint;
								IPADDR ipaddr;
								if (codepoint2 != CodePoint.IPADDR)
								{
									if (codepoint2 != CodePoint.TCPPORTHOST)
									{
										if (codepoint2 == CodePoint.SRVPRTY)
										{
											if (srvlsrv == null)
											{
												srvlsrv = new SRVLSRV();
											}
											srvlsrv.TracePoint = this._tracePoint;
											SRVLSRV srvlsrv2 = srvlsrv;
											srvlsrv2.Srvprty = await reader.ReadUInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
											srvlsrv2 = null;
											if (srvlsrv.Srvprty == Constants.MINSRVPRTY)
											{
												srvlsrv.IsPrimary = false;
											}
											else
											{
												srvlsrv.IsPrimary = true;
											}
										}
										else
										{
											if (Logger.maxTracingLevel >= 2)
											{
												Logger.Warning(this._tracePoint, 0, "SRVLST::Read CodePoint not supported in " + this.ToString() + ": " + codepoint2.ToString(), Array.Empty<object>());
											}
											await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
										}
									}
									else
									{
										if (srvlsrv == null)
										{
											srvlsrv = new SRVLSRV();
										}
										SRVLSRV srvlsrv2 = srvlsrv;
										srvlsrv2.Tcpport = (int)(await reader.ReadUInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
										srvlsrv2 = null;
										srvlsrv2 = srvlsrv;
										srvlsrv2.TcpHost = await reader.ReadStringAsync(isAsync, cancellationToken);
										srvlsrv2 = null;
									}
								}
								else
								{
									if (srvlsrv == null)
									{
										srvlsrv = new SRVLSRV();
									}
									ipaddr = new IPADDR((int)objectInfo2.Length);
									await ipaddr.ReadAsync(reader, isAsync, cancellationToken);
									srvlsrv.Tcpaddr = new IPAddress(ipaddr.IPAddressBytes);
									srvlsrv.Tcpport = (int)ipaddr.Port;
								}
								ipaddr = null;
							}
							if (srvlsrv != null)
							{
								this._list.Add(srvlsrv);
							}
							srvlsrv = null;
							taskEnumerator2 = null;
							enumerator2 = null;
						}
					}
					else
					{
						this._srvlstCount = await reader.ReadUInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
					}
				}
				taskEnumerator = null;
				enumerator = null;
			}
			catch (Exception ex)
			{
				Logger.LogException(this._tracePoint, base.DatabaseSessionId, "SRVLST::Read Message ", ex);
				throw;
			}
		}

		// Token: 0x060047EE RID: 18414 RVA: 0x00105038 File Offset: 0x00103238
		public bool Contains(SRVLSRV partner)
		{
			foreach (SRVLSRV srvlsrv in this._list)
			{
				if (srvlsrv.Tcpaddr.Equals(partner.Tcpaddr) && srvlsrv.Tcpport == partner.Tcpport)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x060047EF RID: 18415 RVA: 0x001050AC File Offset: 0x001032AC
		// (set) Token: 0x060047F0 RID: 18416 RVA: 0x001050B4 File Offset: 0x001032B4
		public ushort MinSRVPRTY
		{
			get
			{
				return this._minSRVPRTY;
			}
			set
			{
				this._minSRVPRTY = value;
			}
		}

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x060047F1 RID: 18417 RVA: 0x001050BD File Offset: 0x001032BD
		// (set) Token: 0x060047F2 RID: 18418 RVA: 0x001050C5 File Offset: 0x001032C5
		public ushort MaxSRVPRTY
		{
			get
			{
				return this._maxSRVPRTY;
			}
			set
			{
				this._maxSRVPRTY = value;
			}
		}

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x060047F3 RID: 18419 RVA: 0x001050CE File Offset: 0x001032CE
		// (set) Token: 0x060047F4 RID: 18420 RVA: 0x001050D6 File Offset: 0x001032D6
		public ushort MaxFailOverSRVPRTY
		{
			get
			{
				return this._maxFailOverSRVPRTY;
			}
			set
			{
				this._maxFailOverSRVPRTY = value;
			}
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x060047F5 RID: 18421 RVA: 0x001050DF File Offset: 0x001032DF
		// (set) Token: 0x060047F6 RID: 18422 RVA: 0x001050E7 File Offset: 0x001032E7
		public ushort MinFailOverSRVPRTY
		{
			get
			{
				return this._minFailOverSRVPRTY;
			}
			set
			{
				this._minFailOverSRVPRTY = value;
			}
		}

		// Token: 0x060047F7 RID: 18423 RVA: 0x001050F0 File Offset: 0x001032F0
		public void SetPrimary(IPAddress ipaddr)
		{
			foreach (SRVLSRV srvlsrv in this._list)
			{
				if (!srvlsrv.Tcpaddr.Equals(ipaddr))
				{
					srvlsrv.IsPrimary = false;
					break;
				}
			}
			foreach (SRVLSRV srvlsrv2 in this._list)
			{
				if (srvlsrv2.Tcpaddr.Equals(ipaddr))
				{
					srvlsrv2.IsPrimary = true;
					break;
				}
			}
		}

		// Token: 0x060047F8 RID: 18424 RVA: 0x001051A8 File Offset: 0x001033A8
		public void UnsetPrimary(IPAddress ipaddr)
		{
			foreach (SRVLSRV srvlsrv in this._list)
			{
				if (!srvlsrv.Tcpaddr.Equals(ipaddr))
				{
					srvlsrv.IsPrimary = true;
					break;
				}
			}
			foreach (SRVLSRV srvlsrv2 in this._list)
			{
				if (srvlsrv2.Tcpaddr.Equals(ipaddr))
				{
					srvlsrv2.IsPrimary = false;
					break;
				}
			}
		}

		// Token: 0x060047F9 RID: 18425 RVA: 0x00105260 File Offset: 0x00103460
		public bool GetSrvprty(IPAddress ipAddress, out ushort srvprty)
		{
			srvprty = 0;
			foreach (SRVLSRV srvlsrv in this._list)
			{
				if (srvlsrv.Tcpaddr.Equals(ipAddress))
				{
					srvprty = srvlsrv.Srvprty;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060047FA RID: 18426 RVA: 0x001052CC File Offset: 0x001034CC
		public SRVLST Clone()
		{
			SRVLST srvlst = new SRVLST();
			foreach (SRVLSRV srvlsrv in this._list)
			{
				srvlst._list.Add(srvlsrv.Clone());
			}
			srvlst.ForFailover = this._forFailover;
			srvlst.MaxFailOverSRVPRTY = this._maxFailOverSRVPRTY;
			srvlst.MinFailOverSRVPRTY = this._minFailOverSRVPRTY;
			srvlst.MinSRVPRTY = this._minSRVPRTY;
			srvlst.MaxSRVPRTY = this._maxSRVPRTY;
			return srvlst;
		}

		// Token: 0x0400347D RID: 13437
		private HashSet<SRVLSRV> _list = new HashSet<SRVLSRV>();

		// Token: 0x0400347E RID: 13438
		private bool _forFailover;

		// Token: 0x0400347F RID: 13439
		private ushort _minFailOverSRVPRTY = Constants.MINFAILOVERSRVPRTY;

		// Token: 0x04003480 RID: 13440
		private ushort _maxFailOverSRVPRTY = Constants.MAXFAILOVERSRVPRTY;

		// Token: 0x04003481 RID: 13441
		private ushort _minSRVPRTY = Constants.MINSRVPRTY;

		// Token: 0x04003482 RID: 13442
		private ushort _maxSRVPRTY = Constants.MAXSRVPRTY;

		// Token: 0x04003483 RID: 13443
		private ushort _srvlstCount;
	}
}
