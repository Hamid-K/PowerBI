using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x02000879 RID: 2169
	public class BGNBND : AbstractDdmObject
	{
		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x060044CE RID: 17614 RVA: 0x000E9426 File Offset: 0x000E7626
		// (set) Token: 0x060044CF RID: 17615 RVA: 0x000E942E File Offset: 0x000E762E
		public Hashtable BndOptions
		{
			get
			{
				return this._bndOptions;
			}
			set
			{
				this._bndOptions = value;
			}
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x000E9438 File Offset: 0x000E7638
		public override string ToString()
		{
			return base.FixParenthis(string.Format("BGNBND[rdbnam={0};pkgnamct={1};vrsnam={2};pkgisolvl={3};qryblkctl={4};]", new object[] { this._rdbnam, this._pkgnamct, this._vrsnam, this._pkgisolvl, this._qryblkctl }));
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x000E9494 File Offset: 0x000E7694
		public BGNBND(IDatabase database, int sqlamLevel)
			: base(database, sqlamLevel)
		{
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x000E94F8 File Offset: 0x000E76F8
		public BGNBND(IDatabase iDatabase, int level, string accrdbTypedefname)
			: base(iDatabase, level, accrdbTypedefname)
		{
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x000E955C File Offset: 0x000E775C
		public override void Reset()
		{
			if (this._rdbnam != null)
			{
				this._rdbnam.Reset();
			}
			if (this._pkgnamct != null)
			{
				this._pkgnamct.Reset();
			}
			if (this._vrsnam != null)
			{
				this._vrsnam.Reset();
			}
			this._title = null;
			this._pkgisolvl = 0;
			this._qryblkctl = 9239;
			this._prpsttkp = 0;
			this._bndcrtctl = 9250;
			this._bndchkexs = 9245;
			this._bndexpopt = 9274;
			this._rdbrlsopt = 9272;
			this._pkgrplopt = 9247;
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x060044D4 RID: 17620 RVA: 0x000E95F9 File Offset: 0x000E77F9
		// (set) Token: 0x060044D5 RID: 17621 RVA: 0x000E9601 File Offset: 0x000E7801
		public string Dftrdbcol
		{
			get
			{
				return this._dftrdbcol;
			}
			set
			{
				this._dftrdbcol = value;
			}
		}

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x060044D6 RID: 17622 RVA: 0x000E960A File Offset: 0x000E780A
		// (set) Token: 0x060044D7 RID: 17623 RVA: 0x000E9612 File Offset: 0x000E7812
		public short Pkgisolvl
		{
			get
			{
				return this._pkgisolvl;
			}
			set
			{
				this._pkgisolvl = value;
			}
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x060044D8 RID: 17624 RVA: 0x000E961B File Offset: 0x000E781B
		public VRSNAM Vrsnam
		{
			get
			{
				return this._vrsnam;
			}
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x060044D9 RID: 17625 RVA: 0x000E9623 File Offset: 0x000E7823
		public int Bndcrtctl
		{
			get
			{
				return this._bndcrtctl;
			}
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x060044DA RID: 17626 RVA: 0x000E962B File Offset: 0x000E782B
		public int Bndchkexs
		{
			get
			{
				return this._bndchkexs;
			}
		}

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x060044DB RID: 17627 RVA: 0x000E9633 File Offset: 0x000E7833
		public int Bndexpopt
		{
			get
			{
				return this._bndexpopt;
			}
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x060044DC RID: 17628 RVA: 0x000E963B File Offset: 0x000E783B
		public int Rdbrlsopt
		{
			get
			{
				return this._rdbrlsopt;
			}
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x060044DD RID: 17629 RVA: 0x000E9643 File Offset: 0x000E7843
		public int Pkgrplopt
		{
			get
			{
				return this._pkgrplopt;
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x060044DE RID: 17630 RVA: 0x000E964B File Offset: 0x000E784B
		public byte Prpsttkp
		{
			get
			{
				return this._prpsttkp;
			}
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x060044DF RID: 17631 RVA: 0x000E9653 File Offset: 0x000E7853
		// (set) Token: 0x060044E0 RID: 17632 RVA: 0x000E965B File Offset: 0x000E785B
		public IDatabase Database
		{
			get
			{
				return this._database;
			}
			set
			{
				this._database = value;
			}
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x000E9664 File Offset: 0x000E7864
		public int Qryblkctl
		{
			get
			{
				return this._qryblkctl;
			}
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x060044E2 RID: 17634 RVA: 0x000E966C File Offset: 0x000E786C
		public RDBNAM Rdbnam
		{
			get
			{
				return this._rdbnam;
			}
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x060044E3 RID: 17635 RVA: 0x000E9674 File Offset: 0x000E7874
		// (set) Token: 0x060044E4 RID: 17636 RVA: 0x000E967C File Offset: 0x000E787C
		public PKGNAMCT Pkgnamct
		{
			get
			{
				return this._pkgnamct;
			}
			set
			{
				this._pkgnamct = value;
			}
		}

		// Token: 0x060044E5 RID: 17637 RVA: 0x000E9688 File Offset: 0x000E7888
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.BGNBND);
			writer.WriteBeginDdm(CodePoint.PKGNAMCT);
			this._pkgnamct.Write(writer);
			writer.WriteEndDdm();
			writer.WriteScalar2Bytes(CodePoint.PKGISOLVL, (int)this._pkgisolvl);
			writer.WriteEndDdm();
		}

		// Token: 0x060044E6 RID: 17638 RVA: 0x000E96D4 File Offset: 0x000E78D4
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			List<CodePoint> requiredCodePoints = new List<CodePoint>(2);
			requiredCodePoints.Add(CodePoint.PKGNAMCT);
			requiredCodePoints.Add(CodePoint.PKGISOLVL);
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
				CodePoint cp = objectInfo.Codepoint;
				base.LogCodePoint(cp);
				CodePoint codePoint = cp;
				if (codePoint <= CodePoint.DECPRC)
				{
					if (codePoint <= CodePoint.VRSNAM)
					{
						if (codePoint == CodePoint.TITLE)
						{
							this._title = await reader.ReadStringAsync(isAsync, cancellationToken);
							this._bndOptions[cp] = this._title;
							continue;
						}
						if (codePoint == CodePoint.VRSNAM)
						{
							if (this._vrsnam == null)
							{
								this._vrsnam = new VRSNAM();
							}
							await this._vrsnam.ReadAsync(reader, isAsync, cancellationToken);
							continue;
						}
					}
					else
					{
						if (codePoint == CodePoint.PKGDFTCC)
						{
							this._ccsid = await this.ReadCCSIDAsync(reader, isAsync, cancellationToken);
							this._bndOptions[cp] = this._ccsid;
							continue;
						}
						if (codePoint == CodePoint.DECPRC)
						{
							this._decProc = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
							this._bndOptions[cp] = this._decProc;
							continue;
						}
					}
				}
				else if (codePoint <= CodePoint.PKGATHRUL)
				{
					switch (codePoint)
					{
					case CodePoint.RDBNAM:
						if (this._rdbnam == null)
						{
							this._rdbnam = new RDBNAM();
						}
						await this._rdbnam.ReadAsync(reader, isAsync, cancellationToken);
						continue;
					case CodePoint.OUTEXP:
					case CodePoint.PKGNAMCSN:
					case CodePoint.QRYBLKSZ:
					case CodePoint.UOWDSP:
					case CodePoint.RTNSQLDA:
					case CodePoint.SQLSTTNBR:
					case CodePoint.FDODSCOFF:
					case CodePoint.FDODTAOFF:
					case CodePoint.RDBALWUPD:
					case CodePoint.SQLCSRHLD:
					case CodePoint.BNDSTTASM:
					case CodePoint.MAXSCTNBR:
					case CodePoint.FDOTRPOFF:
					case CodePoint.FDOPRMOFF:
					case (CodePoint)8492:
					case (CodePoint)8494:
						break;
					case CodePoint.PKGNAMCT:
						if (this._pkgnamct == null)
						{
							this._pkgnamct = new PKGNAMCT(this._database.PkgnamcsnCcsid);
						}
						await this._pkgnamct.ReadAsync(reader, isAsync, cancellationToken);
						requiredCodePoints.Remove(CodePoint.PKGNAMCT);
						continue;
					case CodePoint.BNDCHKEXS:
						this._bndchkexs = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
						this._bndOptions[cp] = this._bndchkexs;
						continue;
					case CodePoint.PKGRPLOPT:
						this._pkgrplopt = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
						this._bndOptions[cp] = this._pkgrplopt;
						continue;
					case CodePoint.BNDCRTCTL:
						this._bndcrtctl = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
						this._bndOptions[cp] = this._bndcrtctl;
						continue;
					case CodePoint.PKGATHOPT:
						this._pkgathopt = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
						this._bndOptions[cp] = this._pkgathopt;
						continue;
					case CodePoint.STTSTRDEL:
						this._sttstrdel = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
						this._bndOptions[cp] = this._sttstrdel;
						continue;
					case CodePoint.STTDECDEL:
						this._sttdecdel = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
						this._bndOptions[cp] = this._sttdecdel;
						continue;
					case CodePoint.STTDATFMT:
						this._sttdatfmt = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
						this._bndOptions[cp] = this._sttdatfmt;
						continue;
					case CodePoint.STTTIMFMT:
						this._stttimfmt = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
						this._bndOptions[cp] = this._stttimfmt;
						continue;
					case CodePoint.PKGISOLVL:
						this._pkgisolvl = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
						requiredCodePoints.Remove(CodePoint.PKGISOLVL);
						this._bndOptions[cp] = this._pkgisolvl;
						continue;
					case CodePoint.PKGDFTCST:
						this._pkgdftcst = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
						this._bndOptions[cp] = this._pkgdftcst;
						continue;
					case CodePoint.DFTRDBCOL:
						this._dftrdbcol = await reader.ReadStringAsync(isAsync, cancellationToken);
						this._bndOptions[cp] = this._dftrdbcol;
						continue;
					case CodePoint.RDBRLSOPT:
						this._rdbrlsopt = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
						this._bndOptions[cp] = this._rdbrlsopt;
						continue;
					case CodePoint.PKGRPLVRS:
						this._pkgrplopt = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
						this._bndOptions[cp] = this._pkgrplopt;
						continue;
					case CodePoint.DGRIOPRL:
						this._dgrioprl = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
						this._bndOptions[cp] = this._dgrioprl;
						continue;
					case CodePoint.BNDEXPOPT:
						this._bndexpopt = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
						this._bndOptions[cp] = this._bndexpopt;
						continue;
					case CodePoint.PKGOWNID:
						this._pkgownid = await reader.ReadStringAsync(isAsync, cancellationToken);
						this._bndOptions[cp] = this._pkgownid;
						continue;
					case CodePoint.QRYBLKCTL:
						this._qryblkctl = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
						this._bndOptions[cp] = this._qryblkctl;
						continue;
					default:
						if (codePoint == CodePoint.PKGATHRUL)
						{
							this._pkgathrul = await reader.ReadByteAsync(isAsync, cancellationToken);
							this._bndOptions[cp] = this._pkgathrul;
							continue;
						}
						break;
					}
				}
				else
				{
					if (codePoint == CodePoint.PRPSTTKP)
					{
						this._prpsttkp = await reader.ReadByteAsync(isAsync, cancellationToken);
						this._bndOptions[cp] = this._prpsttkp;
						continue;
					}
					if (codePoint == CodePoint.BNDOPT)
					{
						await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
						continue;
					}
				}
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "BGNBND::Read CodePoint not supported in " + this.ToString() + ": " + cp.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
			if (requiredCodePoints.Count != 0)
			{
				DrdaException.MissingCodePoint(requiredCodePoints[0]);
			}
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x000E9734 File Offset: 0x000E7934
		private async Task<Ccsid> ReadCCSIDAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			Ccsid ccsid = new Ccsid();
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
				switch (codepoint)
				{
				case CodePoint.CCSIDSBC:
					AbstractDdmObject.CheckLength(reader, CodePoint.CCSIDSBC, 2);
					ccsid._ccsidsbc = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
					break;
				case CodePoint.CCSIDDBC:
					AbstractDdmObject.CheckLength(reader, CodePoint.CCSIDDBC, 2);
					ccsid._ccsiddbc = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
					break;
				case CodePoint.CCSIDMBC:
					AbstractDdmObject.CheckLength(reader, CodePoint.CCSIDMBC, 2);
					ccsid._ccsidmbc = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
					break;
				default:
					if (Logger.maxTracingLevel >= 4)
					{
						Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "TYPDEFOVR::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
					}
					await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
					break;
				}
			}
			return ccsid;
		}

		// Token: 0x0400308A RID: 12426
		private RDBNAM _rdbnam;

		// Token: 0x0400308B RID: 12427
		private PKGNAMCT _pkgnamct;

		// Token: 0x0400308C RID: 12428
		private VRSNAM _vrsnam;

		// Token: 0x0400308D RID: 12429
		private string _title;

		// Token: 0x0400308E RID: 12430
		private short _pkgisolvl;

		// Token: 0x0400308F RID: 12431
		private int _qryblkctl = 9239;

		// Token: 0x04003090 RID: 12432
		private byte _prpsttkp;

		// Token: 0x04003091 RID: 12433
		private int _bndcrtctl = 9250;

		// Token: 0x04003092 RID: 12434
		private int _bndchkexs = 9245;

		// Token: 0x04003093 RID: 12435
		private int _bndexpopt = 9274;

		// Token: 0x04003094 RID: 12436
		private int _rdbrlsopt = 9272;

		// Token: 0x04003095 RID: 12437
		private int _pkgrplopt = 9247;

		// Token: 0x04003096 RID: 12438
		private short _decProc;

		// Token: 0x04003097 RID: 12439
		private string _dftrdbcol;

		// Token: 0x04003098 RID: 12440
		private byte _pkgathrul;

		// Token: 0x04003099 RID: 12441
		private string _pkgownid;

		// Token: 0x0400309A RID: 12442
		private short _pkgdftcst;

		// Token: 0x0400309B RID: 12443
		private short _pkgathopt;

		// Token: 0x0400309C RID: 12444
		private short _dgrioprl;

		// Token: 0x0400309D RID: 12445
		private short _sttdatfmt;

		// Token: 0x0400309E RID: 12446
		private short _stttimfmt;

		// Token: 0x0400309F RID: 12447
		private short _sttdecdel;

		// Token: 0x040030A0 RID: 12448
		private short _sttstrdel;

		// Token: 0x040030A1 RID: 12449
		private Ccsid _ccsid;

		// Token: 0x040030A2 RID: 12450
		private Hashtable _bndOptions = new Hashtable();
	}
}
