using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008E3 RID: 2275
	public class SYNCCTL : AbstractDdmObject
	{
		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06004816 RID: 18454 RVA: 0x00105F52 File Offset: 0x00104152
		// (set) Token: 0x06004817 RID: 18455 RVA: 0x00105F5A File Offset: 0x0010415A
		public int Xaflags { get; set; }

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06004818 RID: 18456 RVA: 0x00105F63 File Offset: 0x00104163
		// (set) Token: 0x06004819 RID: 18457 RVA: 0x00105F6B File Offset: 0x0010416B
		public Xid Xid { get; set; }

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x0600481A RID: 18458 RVA: 0x00105F74 File Offset: 0x00104174
		// (set) Token: 0x0600481B RID: 18459 RVA: 0x00105F7C File Offset: 0x0010417C
		public bool ReceivedRLVS
		{
			get
			{
				return this._receivedRLVS;
			}
			set
			{
				this._receivedRLVS = value;
			}
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x0600481C RID: 18460 RVA: 0x00105F85 File Offset: 0x00104185
		// (set) Token: 0x0600481D RID: 18461 RVA: 0x00105F8D File Offset: 0x0010418D
		public byte Rlsconv
		{
			get
			{
				return this._rlsconv;
			}
			set
			{
				this._rlsconv = value;
			}
		}

		// Token: 0x0600481E RID: 18462 RVA: 0x00105F96 File Offset: 0x00104196
		public SYNCCTL(IDatabase database, string resyncIpAddress, int resyncPort)
			: base(database)
		{
			this._resyncIpAddress = resyncIpAddress;
			this._resyncPort = resyncPort;
			this._receivedRLVS = false;
			this.Xid = null;
			this.Xaflags = 0;
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x00105FCD File Offset: 0x001041CD
		public override string ToString()
		{
			return string.Format("SYNCCTL[uowid={0};synctype={1};synclog={2};forget={3:X2}]", new object[] { this._uowid, this._synctype, this._syncLog, this._forget });
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06004820 RID: 18464 RVA: 0x0010600D File Offset: 0x0010420D
		// (set) Token: 0x06004821 RID: 18465 RVA: 0x00106015 File Offset: 0x00104215
		public byte Forget
		{
			get
			{
				return this._forget;
			}
			set
			{
				this._forget = value;
			}
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x06004822 RID: 18466 RVA: 0x0010601E File Offset: 0x0010421E
		// (set) Token: 0x06004823 RID: 18467 RVA: 0x00106026 File Offset: 0x00104226
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

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x06004824 RID: 18468 RVA: 0x0010602F File Offset: 0x0010422F
		// (set) Token: 0x06004825 RID: 18469 RVA: 0x00106037 File Offset: 0x00104237
		public SyncType SyncType
		{
			get
			{
				return this._synctype;
			}
			set
			{
				this._synctype = value;
			}
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x06004826 RID: 18470 RVA: 0x00106040 File Offset: 0x00104240
		// (set) Token: 0x06004827 RID: 18471 RVA: 0x00106048 File Offset: 0x00104248
		public UOWid UOWid
		{
			get
			{
				return this._uowid;
			}
			set
			{
				this._uowid = value;
			}
		}

		// Token: 0x06004828 RID: 18472 RVA: 0x00106054 File Offset: 0x00104254
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			List<CodePoint> requiredCodePoints = new List<CodePoint>(1);
			requiredCodePoints.Add(CodePoint.SYNCTYPE);
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
				if (codepoint <= CodePoint.SYNCTYPE)
				{
					if (codepoint == CodePoint.SYNCLOG)
					{
						this._syncLog = new SYNCLOG(base.DatabaseSessionId, this._resyncIpAddress, this._resyncPort);
						await this._syncLog.ReadAsync(reader, isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.FORGET)
					{
						this._forget = await reader.ReadByteAsync(isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.SYNCTYPE)
					{
						this._synctype = (SyncType)(await reader.ReadByteAsync(isAsync, cancellationToken));
						requiredCodePoints.Remove(CodePoint.SYNCTYPE);
						continue;
					}
				}
				else if (codepoint <= CodePoint.UOWID)
				{
					if (codepoint == CodePoint.RLSCONV)
					{
						this._rlsconv = await reader.ReadByteAsync(isAsync, cancellationToken);
						this._receivedRLVS = true;
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
					if (codepoint == CodePoint.XIDCPT)
					{
						this.Xid = new Xid();
						await this.Xid.ReadAsync(reader, isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.XAFLAGS)
					{
						this.Xaflags = await reader.ReadInt32Async(EndianType.BigEndian, isAsync, cancellationToken);
						continue;
					}
				}
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "SYNCCTL::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
			if (requiredCodePoints.Count != 0)
			{
				DrdaException.MissingCodePoint(requiredCodePoints[0]);
			}
		}

		// Token: 0x06004829 RID: 18473 RVA: 0x001060B4 File Offset: 0x001042B4
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.SYNCCTL);
			writer.WriteScalar1Byte(CodePoint.SYNCTYPE, (int)this._synctype);
			if (this.Xaflags != 0)
			{
				writer.WriteScalar4Bytes(CodePoint.XAFLAGS, this.Xaflags);
			}
			if (this.Xid != null)
			{
				writer.WriteBeginDdm(CodePoint.XIDCPT);
				writer.WriteInt32(this.Xid.FormatId, EndianType.BigEndian);
				writer.WriteInt32(this.Xid.GlobalTransactionId.Length, EndianType.BigEndian);
				writer.WriteInt32(this.Xid.BranchQualifier.Length, EndianType.BigEndian);
				writer.WriteBytes(this.Xid.GlobalTransactionId);
				writer.WriteBytes(this.Xid.BranchQualifier);
				writer.WriteEndDdm();
			}
			writer.WriteEndDdm();
		}

		// Token: 0x040034A6 RID: 13478
		private UOWid _uowid;

		// Token: 0x040034A7 RID: 13479
		private SyncType _synctype;

		// Token: 0x040034A8 RID: 13480
		private byte _forget;

		// Token: 0x040034A9 RID: 13481
		private bool _receivedRLVS;

		// Token: 0x040034AA RID: 13482
		private SYNCLOG _syncLog;

		// Token: 0x040034AB RID: 13483
		private byte _rlsconv = 240;

		// Token: 0x040034AC RID: 13484
		private string _resyncIpAddress;

		// Token: 0x040034AD RID: 13485
		private int _resyncPort;
	}
}
