using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008EA RID: 2282
	public class SYNCRRD : AbstractDdmObject
	{
		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06004850 RID: 18512 RVA: 0x00107AC2 File Offset: 0x00105CC2
		// (set) Token: 0x06004851 RID: 18513 RVA: 0x00107ACA File Offset: 0x00105CCA
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

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06004852 RID: 18514 RVA: 0x00107AD3 File Offset: 0x00105CD3
		public byte ResyncType
		{
			get
			{
				return this._resyncType;
			}
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06004853 RID: 18515 RVA: 0x00107ADB File Offset: 0x00105CDB
		public byte UOWState
		{
			get
			{
				return this._uowState;
			}
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06004854 RID: 18516 RVA: 0x00107AE3 File Offset: 0x00105CE3
		public UOWid UOWid
		{
			get
			{
				return this._uowid;
			}
		}

		// Token: 0x06004855 RID: 18517 RVA: 0x00107AEB File Offset: 0x00105CEB
		public SYNCRRD(int sessionId, string resyncIpAddress, int resyncPort)
		{
			this._resyncIpAddress = resyncIpAddress;
			this._resyncPort = resyncPort;
			this._sessionId = sessionId;
		}

		// Token: 0x06004856 RID: 18518 RVA: 0x00107B08 File Offset: 0x00105D08
		public override string ToString()
		{
			return string.Format("SYNCRRD[resynctype={0};uowstate={1};uowid={2}]", this._resyncType, this._uowState, this._uowid);
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x00107B30 File Offset: 0x00105D30
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
				if (codepoint <= CodePoint.UOWID)
				{
					if (codepoint == CodePoint.SYNCLOG)
					{
						this._syncLog = new SYNCLOG(this._sessionId, this._resyncIpAddress, this._resyncPort);
						await this._syncLog.ReadAsync(reader, isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.UOWID)
					{
						this._uowid = new UOWid();
						await this._uowid.ReadAsync(reader, isAsync, cancellationToken);
						continue;
					}
				}
				else
				{
					if (codepoint == CodePoint.UOWSTATE)
					{
						this._uowState = await reader.ReadByteAsync(isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.RSYNCTYP)
					{
						this._resyncType = await reader.ReadByteAsync(isAsync, cancellationToken);
						continue;
					}
				}
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, this._sessionId, 4, "SYNCRRD::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
		}

		// Token: 0x040034EC RID: 13548
		private SYNCLOG _syncLog;

		// Token: 0x040034ED RID: 13549
		private byte _resyncType;

		// Token: 0x040034EE RID: 13550
		private byte _uowState;

		// Token: 0x040034EF RID: 13551
		private UOWid _uowid;

		// Token: 0x040034F0 RID: 13552
		private string _resyncIpAddress;

		// Token: 0x040034F1 RID: 13553
		private int _resyncPort;

		// Token: 0x040034F2 RID: 13554
		private int _sessionId;
	}
}
