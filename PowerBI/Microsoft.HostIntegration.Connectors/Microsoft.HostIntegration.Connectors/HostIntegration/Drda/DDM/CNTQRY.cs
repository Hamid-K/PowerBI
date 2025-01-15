using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x02000881 RID: 2177
	public class CNTQRY : AbstractDdmObject
	{
		// Token: 0x06004521 RID: 17697 RVA: 0x000ECF58 File Offset: 0x000EB158
		public override string ToString()
		{
			return base.FixParenthis(string.Format("CNTQRY[rdbnam={0};pkgnamcsn={1};qryinsid={2};cmdsrcid={3};qryrelscr={4};maxblkext={5};nbrrow={6};qryblkrst={7};qryrownbr={8};qryrowset={9};qryrowsns={10};qryrtndta={11};qryscrorn={12};rtnextdta={13};qryrfrtbl=={14}]", new object[]
			{
				this._rdbnam, this._pkgnamcsn, this._qryinsid, this._cmdsrcid, this._qryrelscr, this._maxblkext, this._nbrrow, this._qryblkrst, this._qryrownbr, this._qryrowset,
				this._qryrowsns, this._qryrtndta, this._qryscrorn, this._rtnextdta, this._qryrfrtbl
			}));
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x000ED04C File Offset: 0x000EB24C
		public override void Reset()
		{
			if (this._rdbnam != null)
			{
				this._rdbnam.Reset();
			}
			if (this._pkgnamcsn != null)
			{
				this._pkgnamcsn.Reset();
			}
			if (this._outovr != null)
			{
				this._outovr.Reset();
			}
			this._qryrelscr = false;
			this._maxblkext = 0;
			this._nbrrow = 0;
			this._qryblkrst = false;
			this._qryblksz = 0;
			this._qryinsid = 0L;
			this._qryrownbr = 0L;
			this._qryrownbrSet = false;
			this._qryrowset = 1;
			this._qryrowsetSet = false;
			this._qryrowsns = false;
			this._qryrtndta = false;
			this._qryscrorn = 1;
			this._rtnextdta = 1;
			this._qryrfrtbl = false;
			this._qryscrornSet = false;
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06004523 RID: 17699 RVA: 0x000ED104 File Offset: 0x000EB304
		// (set) Token: 0x06004524 RID: 17700 RVA: 0x000ED10C File Offset: 0x000EB30C
		public bool Qryrfrtbl
		{
			get
			{
				return this._qryrfrtbl;
			}
			set
			{
				this._qryrfrtbl = value;
			}
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x000ED115 File Offset: 0x000EB315
		public CNTQRY(IDatabase database, int level, string accrdbTypedefname)
			: base(database, level, accrdbTypedefname)
		{
		}

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06004526 RID: 17702 RVA: 0x000ED12E File Offset: 0x000EB32E
		// (set) Token: 0x06004527 RID: 17703 RVA: 0x000ED136 File Offset: 0x000EB336
		public int Qryscrorn
		{
			get
			{
				return this._qryscrorn;
			}
			set
			{
				this._qryscrorn = value;
				this._qryscrornSet = true;
			}
		}

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06004528 RID: 17704 RVA: 0x000ED146 File Offset: 0x000EB346
		// (set) Token: 0x06004529 RID: 17705 RVA: 0x000ED14E File Offset: 0x000EB34E
		public int Maxblkext
		{
			get
			{
				return this._maxblkext;
			}
			set
			{
				this._maxblkext = value;
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x0600452A RID: 17706 RVA: 0x000ED157 File Offset: 0x000EB357
		// (set) Token: 0x0600452B RID: 17707 RVA: 0x000ED15F File Offset: 0x000EB35F
		public long Qryrownbr
		{
			get
			{
				return this._qryrownbr;
			}
			set
			{
				this._qryrownbr = value;
				this._qryrownbrSet = true;
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x0600452C RID: 17708 RVA: 0x000ED16F File Offset: 0x000EB36F
		// (set) Token: 0x0600452D RID: 17709 RVA: 0x000ED177 File Offset: 0x000EB377
		public int Rtnextdta
		{
			get
			{
				return this._rtnextdta;
			}
			set
			{
				this._rtnextdta = value;
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x0600452E RID: 17710 RVA: 0x000ED180 File Offset: 0x000EB380
		// (set) Token: 0x0600452F RID: 17711 RVA: 0x000ED188 File Offset: 0x000EB388
		public int Nbrrow
		{
			get
			{
				return this._nbrrow;
			}
			set
			{
				this._nbrrow = value;
			}
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06004530 RID: 17712 RVA: 0x000ED191 File Offset: 0x000EB391
		// (set) Token: 0x06004531 RID: 17713 RVA: 0x000ED199 File Offset: 0x000EB399
		public bool Qryrelscr
		{
			get
			{
				return this._qryrelscr;
			}
			set
			{
				this._qryrelscr = value;
			}
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06004532 RID: 17714 RVA: 0x000ED1A2 File Offset: 0x000EB3A2
		// (set) Token: 0x06004533 RID: 17715 RVA: 0x000ED1AA File Offset: 0x000EB3AA
		public bool Qryrowsns
		{
			get
			{
				return this._qryrowsns;
			}
			set
			{
				this._qryrowsns = value;
			}
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06004534 RID: 17716 RVA: 0x000ED1B3 File Offset: 0x000EB3B3
		// (set) Token: 0x06004535 RID: 17717 RVA: 0x000ED1BB File Offset: 0x000EB3BB
		public bool Qryrtndta
		{
			get
			{
				return this._qryrtndta;
			}
			set
			{
				this._qryrtndta = value;
			}
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06004536 RID: 17718 RVA: 0x000ED1C4 File Offset: 0x000EB3C4
		// (set) Token: 0x06004537 RID: 17719 RVA: 0x000ED1CC File Offset: 0x000EB3CC
		public bool Qryblkrst
		{
			get
			{
				return this._qryblkrst;
			}
			set
			{
				this._qryblkrst = value;
			}
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06004538 RID: 17720 RVA: 0x000ED1D5 File Offset: 0x000EB3D5
		// (set) Token: 0x06004539 RID: 17721 RVA: 0x000ED1DD File Offset: 0x000EB3DD
		public int Qryblksz
		{
			get
			{
				return this._qryblksz;
			}
			set
			{
				this._qryblksz = value;
			}
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x0600453A RID: 17722 RVA: 0x000ED1E6 File Offset: 0x000EB3E6
		// (set) Token: 0x0600453B RID: 17723 RVA: 0x000ED1EE File Offset: 0x000EB3EE
		public long Qryinsid
		{
			get
			{
				return this._qryinsid;
			}
			set
			{
				this._qryinsid = value;
			}
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x0600453C RID: 17724 RVA: 0x000ED1F7 File Offset: 0x000EB3F7
		// (set) Token: 0x0600453D RID: 17725 RVA: 0x000ED1FF File Offset: 0x000EB3FF
		public RDBNAM Rdbnam
		{
			get
			{
				return this._rdbnam;
			}
			set
			{
				this._rdbnam = value;
			}
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x0600453E RID: 17726 RVA: 0x000ED208 File Offset: 0x000EB408
		// (set) Token: 0x0600453F RID: 17727 RVA: 0x000ED210 File Offset: 0x000EB410
		public PKGNAMCSN Pkgnamcsn
		{
			get
			{
				return this._pkgnamcsn;
			}
			set
			{
				this._pkgnamcsn = value;
			}
		}

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06004540 RID: 17728 RVA: 0x000ED219 File Offset: 0x000EB419
		// (set) Token: 0x06004541 RID: 17729 RVA: 0x000ED221 File Offset: 0x000EB421
		public OUTOVR Outovr
		{
			get
			{
				return this._outovr;
			}
			set
			{
				this._outovr = value;
			}
		}

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06004542 RID: 17730 RVA: 0x000ED22A File Offset: 0x000EB42A
		// (set) Token: 0x06004543 RID: 17731 RVA: 0x000ED232 File Offset: 0x000EB432
		public int Qryrowset
		{
			get
			{
				return this._qryrowset;
			}
			set
			{
				this._qryrowset = value;
				this._qryrowsetSet = true;
			}
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x000ED244 File Offset: 0x000EB444
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.CNTQRY);
			base.WriteCommandSourceId(writer);
			writer.WriteBeginDdm(CodePoint.PKGNAMCSN);
			this.Pkgnamcsn.Write(writer);
			writer.WriteEndDdm();
			writer.WriteBeginDdm(CodePoint.QRYBLKSZ);
			writer.WriteInt32(this._qryblksz, EndianType.BigEndian);
			writer.WriteEndDdm();
			if (this._qryrowsetSet)
			{
				writer.WriteScalar4Bytes(CodePoint.QRYROWSET, this._qryrowset);
			}
			if (this._qryscrornSet)
			{
				writer.WriteScalar1Byte(CodePoint.QRYSCRORN, this._qryscrorn);
			}
			if (this._qryrownbrSet)
			{
				writer.WriteScalar8Bytes(CodePoint.QRYROWNBR, this._qryrownbr);
			}
			if (this._rtnextdta != 0)
			{
				writer.WriteScalar1Byte(CodePoint.RTNEXTDTA, this._rtnextdta);
			}
			writer.WriteBeginDdm(CodePoint.QRYINSID);
			writer.WriteInt64(this._qryinsid, EndianType.BigEndian);
			writer.WriteEndDdm();
			writer.WriteEndDdm();
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x000ED328 File Offset: 0x000EB528
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			List<CodePoint> requiredCodePoints = new List<CodePoint>(3);
			requiredCodePoints.Add(CodePoint.PKGNAMCSN);
			requiredCodePoints.Add(CodePoint.QRYBLKSZ);
			if (this._sqlamLevel >= 7)
			{
				requiredCodePoints.Add(CodePoint.QRYINSID);
			}
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
				if (codepoint <= CodePoint.QRYRFRTBL)
				{
					if (codepoint <= CodePoint.CMDSRCID)
					{
						if (codepoint == CodePoint.MONITOR)
						{
							await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
							continue;
						}
						if (codepoint == CodePoint.CMDSRCID)
						{
							this._cmdsrcid = await reader.ReadInt64Async(EndianType.BigEndian, isAsync, cancellationToken);
							continue;
						}
					}
					else
					{
						switch (codepoint)
						{
						case CodePoint.RDBNAM:
							this._rdbnam = new RDBNAM();
							await this._rdbnam.ReadAsync(reader, isAsync, cancellationToken);
							continue;
						case CodePoint.OUTEXP:
						case CodePoint.PKGNAMCT:
							break;
						case CodePoint.PKGNAMCSN:
							this._pkgnamcsn = new PKGNAMCSN(this._database.PkgnamcsnCcsid);
							await this._pkgnamcsn.ReadAsync(reader, isAsync, cancellationToken);
							requiredCodePoints.Remove(CodePoint.PKGNAMCSN);
							continue;
						case CodePoint.QRYBLKSZ:
							AbstractDdmObject.CheckLength(reader, CodePoint.QRYBLKSZ, 4);
							this._qryblksz = await reader.ReadInt32Async(EndianType.BigEndian, isAsync, cancellationToken);
							if (this._qryblksz < 512 || this._qryblksz > 10485760)
							{
								DrdaException.InvalidValue(CodePoint.QRYBLKSZ);
							}
							requiredCodePoints.Remove(CodePoint.QRYBLKSZ);
							continue;
						default:
							switch (codepoint)
							{
							case CodePoint.NBRROW:
								AbstractDdmObject.CheckLength(reader, CodePoint.NBRROW, 4);
								this._nbrrow = await reader.ReadInt32Async(EndianType.BigEndian, isAsync, cancellationToken);
								continue;
							case CodePoint.QRYROWNBR:
								AbstractDdmObject.CheckLength(reader, CodePoint.QRYROWNBR, 8);
								this._qryrownbr = await reader.ReadInt64Async(EndianType.BigEndian, isAsync, cancellationToken);
								continue;
							case CodePoint.QRYRFRTBL:
							{
								int num = (int)(await reader.ReadByteAsync(isAsync, cancellationToken));
								if (num == -15)
								{
									this._qryrfrtbl = true;
									continue;
								}
								if (num == -16)
								{
									this._qryrfrtbl = false;
									continue;
								}
								continue;
							}
							}
							break;
						}
					}
				}
				else if (codepoint <= CodePoint.RTNEXTDTA)
				{
					if (codepoint == CodePoint.MAXBLKEXT)
					{
						AbstractDdmObject.CheckLength(reader, CodePoint.MAXBLKEXT, 2);
						this._maxblkext = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
						continue;
					}
					if (codepoint == CodePoint.RTNEXTDTA)
					{
						AbstractDdmObject.CheckLength(reader, CodePoint.RTNEXTDTA, 1);
						this._rtnextdta = await reader.ReadUnsignedByteAsync(isAsync, cancellationToken);
						if (this._rtnextdta != 1 && this._rtnextdta != 2)
						{
							DrdaException.InvalidValue(CodePoint.RTNEXTDTA);
							continue;
						}
						continue;
					}
				}
				else
				{
					switch (codepoint)
					{
					case CodePoint.QRYSCRORN:
						AbstractDdmObject.CheckLength(reader, CodePoint.QRYSCRORN, 1);
						this._qryscrorn = await reader.ReadUnsignedByteAsync(isAsync, cancellationToken);
						continue;
					case CodePoint.QRYROWSNS:
					{
						AbstractDdmObject.CheckLength(reader, CodePoint.QRYROWSNS, 1);
						int num2 = (int)(await reader.ReadByteAsync(isAsync, cancellationToken));
						if (num2 == -15)
						{
							this._qryrowsns = true;
							continue;
						}
						if (num2 == -16)
						{
							this._qryrowsns = false;
							continue;
						}
						DrdaException.InvalidValue(CodePoint.QRYROWSNS);
						continue;
					}
					case CodePoint.QRYBLKRST:
					{
						int num3 = (int)(await reader.ReadByteAsync(isAsync, cancellationToken));
						if (num3 == -15)
						{
							this._qryblkrst = true;
							continue;
						}
						if (num3 == -16)
						{
							this._qryblkrst = false;
							continue;
						}
						continue;
					}
					case CodePoint.QRYRTNDTA:
					{
						TaskAwaiter<byte> taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							await taskAwaiter;
							TaskAwaiter<byte> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<byte>);
						}
						if (taskAwaiter.GetResult() == 241)
						{
							this._qryrtndta = true;
							continue;
						}
						this._qryrtndta = false;
						continue;
					}
					case CodePoint.QRYROWSET:
						AbstractDdmObject.CheckLength(reader, CodePoint.QRYROWSET, 4);
						this._qryrowset = await reader.ReadInt32Async(EndianType.BigEndian, isAsync, cancellationToken);
						if (this._qryrowset < 0 || this._qryrowset > 32767)
						{
							DrdaException.InvalidValue(CodePoint.QRYROWSET);
							continue;
						}
						continue;
					case CodePoint.QRYATTSNS:
					case (CodePoint)8536:
					case (CodePoint)8537:
					case (CodePoint)8538:
						break;
					case CodePoint.QRYINSID:
						AbstractDdmObject.CheckLength(reader, CodePoint.QRYINSID, 8);
						this._qryinsid = await reader.ReadInt64Async(EndianType.BigEndian, isAsync, cancellationToken);
						requiredCodePoints.Remove(CodePoint.QRYINSID);
						continue;
					default:
						if (codepoint == CodePoint.OUTOVR)
						{
							this._outovr = new OUTOVR();
							await this._outovr.ReadAsync(reader, isAsync, cancellationToken);
							continue;
						}
						break;
					}
				}
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "CNTQRY::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
			if (requiredCodePoints.Count != 0)
			{
				DrdaException.MissingCodePoint(requiredCodePoints[0]);
			}
		}

		// Token: 0x040030FC RID: 12540
		private RDBNAM _rdbnam;

		// Token: 0x040030FD RID: 12541
		private PKGNAMCSN _pkgnamcsn;

		// Token: 0x040030FE RID: 12542
		private OUTOVR _outovr;

		// Token: 0x040030FF RID: 12543
		private bool _qryrelscr;

		// Token: 0x04003100 RID: 12544
		private int _maxblkext;

		// Token: 0x04003101 RID: 12545
		private int _nbrrow;

		// Token: 0x04003102 RID: 12546
		private bool _qryblkrst;

		// Token: 0x04003103 RID: 12547
		private int _qryblksz;

		// Token: 0x04003104 RID: 12548
		private long _qryinsid;

		// Token: 0x04003105 RID: 12549
		private long _qryrownbr;

		// Token: 0x04003106 RID: 12550
		private bool _qryrownbrSet;

		// Token: 0x04003107 RID: 12551
		private int _qryrowset = -1;

		// Token: 0x04003108 RID: 12552
		private bool _qryrowsetSet;

		// Token: 0x04003109 RID: 12553
		private bool _qryrowsns;

		// Token: 0x0400310A RID: 12554
		private bool _qryrtndta;

		// Token: 0x0400310B RID: 12555
		private int _qryscrorn = 1;

		// Token: 0x0400310C RID: 12556
		private bool _qryscrornSet;

		// Token: 0x0400310D RID: 12557
		private int _rtnextdta;

		// Token: 0x0400310E RID: 12558
		private bool _qryrfrtbl;
	}
}
