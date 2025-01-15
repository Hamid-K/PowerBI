using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x0200088E RID: 2190
	public class EXCSQLIMM : AbstractDdmObject
	{
		// Token: 0x0600458F RID: 17807 RVA: 0x000F01D0 File Offset: 0x000EE3D0
		public EXCSQLIMM(IDatabase database, int sqlamLevel, string accrdbTypedefname)
			: base(database, sqlamLevel, accrdbTypedefname)
		{
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x000F01E8 File Offset: 0x000EE3E8
		public override string ToString()
		{
			return base.FixParenthis(string.Format("EXCSQLIMM[rdbnam={0};pkgnamcsn={1};typedefovr={2};qryinsid={3};typedefnam={4};sqlstt={5};cmdsrcid={6}]", new object[]
			{
				this._rdbnam,
				this._pkgnamcsn,
				this._typedefovr,
				this._qryinsid,
				this._typdefnam,
				this._sqlstt.Trim(),
				this._cmdsrcid
			}));
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x000F0254 File Offset: 0x000EE454
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
			if (this._typedefovr != null)
			{
				this._typedefovr.Reset();
			}
			this._cmdsrcid = 0L;
			this._qryinsid = null;
			this._typdefnam = null;
			this._sqlstt = null;
			this._rdbcmtok = false;
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x06004592 RID: 17810 RVA: 0x000F02BE File Offset: 0x000EE4BE
		// (set) Token: 0x06004593 RID: 17811 RVA: 0x000F02C6 File Offset: 0x000EE4C6
		public string Qryinsid
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

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x06004594 RID: 17812 RVA: 0x000F02CF File Offset: 0x000EE4CF
		// (set) Token: 0x06004595 RID: 17813 RVA: 0x000F02D7 File Offset: 0x000EE4D7
		public string Typdefnam
		{
			get
			{
				return this._typdefnam;
			}
			set
			{
				this._typdefnam = value;
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06004596 RID: 17814 RVA: 0x000F02E0 File Offset: 0x000EE4E0
		// (set) Token: 0x06004597 RID: 17815 RVA: 0x000F02E8 File Offset: 0x000EE4E8
		public string Sqlstt
		{
			get
			{
				return this._sqlstt;
			}
			set
			{
				this._sqlstt = value;
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06004598 RID: 17816 RVA: 0x000F02F1 File Offset: 0x000EE4F1
		// (set) Token: 0x06004599 RID: 17817 RVA: 0x000F02F9 File Offset: 0x000EE4F9
		public TYPDEFOVR Typedefovr
		{
			get
			{
				return this._typedefovr;
			}
			set
			{
				this._typedefovr = value;
			}
		}

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x0600459A RID: 17818 RVA: 0x000F0302 File Offset: 0x000EE502
		// (set) Token: 0x0600459B RID: 17819 RVA: 0x000F030A File Offset: 0x000EE50A
		public bool Rdbcmtok
		{
			get
			{
				return this._rdbcmtok;
			}
			set
			{
				this._rdbcmtok = value;
			}
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x0600459C RID: 17820 RVA: 0x000F0313 File Offset: 0x000EE513
		// (set) Token: 0x0600459D RID: 17821 RVA: 0x000F031B File Offset: 0x000EE51B
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

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x0600459E RID: 17822 RVA: 0x000F0324 File Offset: 0x000EE524
		// (set) Token: 0x0600459F RID: 17823 RVA: 0x000F032C File Offset: 0x000EE52C
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

		// Token: 0x060045A0 RID: 17824 RVA: 0x000F0335 File Offset: 0x000EE535
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.EXCSQLIMM);
			base.WriteCommandSourceId(writer);
			writer.WriteBeginDdm(CodePoint.PKGNAMCSN);
			this.Pkgnamcsn.Write(writer);
			writer.WriteEndDdm();
			writer.WriteEndDdm();
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x000F036C File Offset: 0x000EE56C
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			List<CodePoint> requiredCodePoints = new List<CodePoint>(2);
			requiredCodePoints.Add(CodePoint.PKGNAMCSN);
			requiredCodePoints.Add(CodePoint.SQLSTT);
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
				CodePoint codepoint = objectInfo.Codepoint;
				if (codepoint <= CodePoint.RDBCMTOK)
				{
					if (codepoint != CodePoint.TYPDEFNAM)
					{
						if (codepoint == CodePoint.TYPDEFOVR)
						{
							if (this._typedefovr == null)
							{
								this._typedefovr = new TYPDEFOVR(this._database, this._sqlamLevel);
							}
							await this._typedefovr.ReadAsync(reader, isAsync, cancellationToken);
							this._sqlstt = this._typedefovr.Sqlstt;
							continue;
						}
						if (codepoint == CodePoint.RDBCMTOK)
						{
							this._rdbcmtok = await reader.ReadBooleanAsync(isAsync, cancellationToken);
							continue;
						}
					}
					else
					{
						this._typdefnam = await reader.ReadStringAsync(isAsync, cancellationToken);
						if (this._typdefnam.Length > 255)
						{
							DrdaException.TooBig(CodePoint.TYPDEFNAM);
							continue;
						}
						continue;
					}
				}
				else if (codepoint <= CodePoint.RDBNAM)
				{
					if (codepoint == CodePoint.CMDSRCID)
					{
						this._cmdsrcid = await reader.ReadInt64Async(EndianType.BigEndian, isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.RDBNAM)
					{
						if (this._rdbnam == null)
						{
							this._rdbnam = new RDBNAM();
						}
						await this._rdbnam.ReadAsync(reader, isAsync, cancellationToken);
						continue;
					}
				}
				else
				{
					if (codepoint == CodePoint.PKGNAMCSN)
					{
						if (this._pkgnamcsn == null)
						{
							this._pkgnamcsn = new PKGNAMCSN(this._database.PkgnamcsnCcsid);
						}
						await this._pkgnamcsn.ReadAsync(reader, isAsync, cancellationToken);
						requiredCodePoints.Remove(cp);
						continue;
					}
					if (codepoint == CodePoint.SQLSTT)
					{
						if (this._typedefovr != null)
						{
							this._sqlstt = await base.ParseEncodedStringAsync(reader, this._typedefovr.Ccsid, this._sqlamLevel, isAsync, cancellationToken);
						}
						else
						{
							this._sqlstt = await base.ParseEncodedStringAsync(reader, this._database.Ccsid, this._sqlamLevel, isAsync, cancellationToken);
						}
						requiredCodePoints.Remove(cp);
						continue;
					}
				}
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "EXCSQLIMM::Read CodePoint not supported in " + this.ToString() + ": " + cp.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
			int count = requiredCodePoints.Count;
		}

		// Token: 0x0400316D RID: 12653
		private PKGNAMCSN _pkgnamcsn;

		// Token: 0x0400316E RID: 12654
		private RDBNAM _rdbnam;

		// Token: 0x0400316F RID: 12655
		private TYPDEFOVR _typedefovr;

		// Token: 0x04003170 RID: 12656
		private string _qryinsid;

		// Token: 0x04003171 RID: 12657
		private string _typdefnam;

		// Token: 0x04003172 RID: 12658
		private string _sqlstt = string.Empty;

		// Token: 0x04003173 RID: 12659
		private bool _rdbcmtok;
	}
}
