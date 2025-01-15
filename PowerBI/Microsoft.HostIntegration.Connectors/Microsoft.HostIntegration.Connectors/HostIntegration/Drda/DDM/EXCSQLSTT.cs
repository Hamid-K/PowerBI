using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x02000892 RID: 2194
	public class EXCSQLSTT : AbstractDdmObject
	{
		// Token: 0x060045B5 RID: 17845 RVA: 0x000F153A File Offset: 0x000EF73A
		public EXCSQLSTT(IDatabase database, int sqlamLevel, string accrdbTypedefname)
			: base(database, sqlamLevel, accrdbTypedefname)
		{
			this.Reset();
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x000F1568 File Offset: 0x000EF768
		public override string ToString()
		{
			return base.FixParenthis(string.Format("EXCSQLSTT[rdbnam={0};pkgnamcsn={1};typesqlda={2};cmdsrcid={3};rtnoutput={4};maxblkext={5};nbrrow={6};outexp={7};prcnam={8};qryrowset={9};qryblksz={10};maxrslcnt={11};rtnsqlda={12};outovropt={13};rdbcmtok=={14};sqldta={15}]", new object[]
			{
				this._rdbnam, this._pkgnamcsn, this._typsqlda, this._cmdsrcid, this._rtnOutput, this._maxblkext, this._nbrrow, this._outexp, this._prcnam, this._qryrowset,
				this._qryblksz, this._maxrslcnt, this._rtnsqlda, this._outovropt, this._rdbcmtok, this._sqldta
			}));
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x000F1660 File Offset: 0x000EF860
		public override void Reset()
		{
			if (this._pkgnamcsn != null)
			{
				this._pkgnamcsn.Reset();
			}
			if (this._rdbnam != null)
			{
				this._rdbnam.Reset();
			}
			if (this._typdefovr != null)
			{
				this._typdefovr.Reset();
			}
			this._sqldta = null;
			this._typsqlda = TYPSQLDA.None;
			this._rtnOutput = true;
			this._outexp = false;
			this._nbrrow = 0;
			this._prcnam = null;
			this._qryblksz = 0;
			this._maxrslcnt = 0;
			this._maxblkext = 0;
			this._rslsetflg = null;
			this._rdbcmtok = false;
			this._outovropt = 0;
			this._qryrowset = 0;
			this._typdefnam = null;
			this._rtnsqlda = false;
			this.Encoding = -1;
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x060045B8 RID: 17848 RVA: 0x000F1717 File Offset: 0x000EF917
		public SQLDTA Sqldta
		{
			get
			{
				if (this._typdefovr == null || this._typdefovr.Sqldta == null)
				{
					return this._sqldta;
				}
				return this._typdefovr.Sqldta;
			}
		}

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x060045B9 RID: 17849 RVA: 0x000F1740 File Offset: 0x000EF940
		public bool RtnOutput
		{
			get
			{
				return this._rtnOutput;
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x060045BA RID: 17850 RVA: 0x000F1748 File Offset: 0x000EF948
		// (set) Token: 0x060045BB RID: 17851 RVA: 0x000F1750 File Offset: 0x000EF950
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

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x060045BC RID: 17852 RVA: 0x000F1759 File Offset: 0x000EF959
		// (set) Token: 0x060045BD RID: 17853 RVA: 0x000F1761 File Offset: 0x000EF961
		public TYPSQLDA Typsqlda
		{
			get
			{
				return this._typsqlda;
			}
			set
			{
				this._typsqlda = value;
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x060045BE RID: 17854 RVA: 0x000F176A File Offset: 0x000EF96A
		// (set) Token: 0x060045BF RID: 17855 RVA: 0x000F1772 File Offset: 0x000EF972
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

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x060045C0 RID: 17856 RVA: 0x000F177B File Offset: 0x000EF97B
		public bool Rtnsqlda
		{
			get
			{
				return this._rtnsqlda;
			}
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x060045C1 RID: 17857 RVA: 0x000F1783 File Offset: 0x000EF983
		public bool Rtnsqlstt
		{
			get
			{
				return this._rtnsqlstt;
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x060045C2 RID: 17858 RVA: 0x000F178B File Offset: 0x000EF98B
		// (set) Token: 0x060045C3 RID: 17859 RVA: 0x000F1793 File Offset: 0x000EF993
		public bool Outexp
		{
			get
			{
				return this._outexp;
			}
			set
			{
				this._outexp = value;
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x060045C4 RID: 17860 RVA: 0x000F179C File Offset: 0x000EF99C
		// (set) Token: 0x060045C5 RID: 17861 RVA: 0x000F17A4 File Offset: 0x000EF9A4
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

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x060045C6 RID: 17862 RVA: 0x000F17AD File Offset: 0x000EF9AD
		// (set) Token: 0x060045C7 RID: 17863 RVA: 0x000F17B5 File Offset: 0x000EF9B5
		public string Prcnam
		{
			get
			{
				return this._prcnam;
			}
			set
			{
				this._prcnam = value;
			}
		}

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x060045C8 RID: 17864 RVA: 0x000F17BE File Offset: 0x000EF9BE
		// (set) Token: 0x060045C9 RID: 17865 RVA: 0x000F17C6 File Offset: 0x000EF9C6
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

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x060045CA RID: 17866 RVA: 0x000F17CF File Offset: 0x000EF9CF
		// (set) Token: 0x060045CB RID: 17867 RVA: 0x000F17D7 File Offset: 0x000EF9D7
		public int Maxrslcnt
		{
			get
			{
				return this._maxrslcnt;
			}
			set
			{
				this._maxrslcnt = value;
			}
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x060045CC RID: 17868 RVA: 0x000F17E0 File Offset: 0x000EF9E0
		public int Maxblkext
		{
			get
			{
				return this._maxblkext;
			}
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x060045CD RID: 17869 RVA: 0x000F17E8 File Offset: 0x000EF9E8
		// (set) Token: 0x060045CE RID: 17870 RVA: 0x000F17F0 File Offset: 0x000EF9F0
		public byte[] Rslsetflg
		{
			get
			{
				return this._rslsetflg;
			}
			set
			{
				this._rslsetflg = value;
			}
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x060045CF RID: 17871 RVA: 0x000F17F9 File Offset: 0x000EF9F9
		// (set) Token: 0x060045D0 RID: 17872 RVA: 0x000F1801 File Offset: 0x000EFA01
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

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x060045D1 RID: 17873 RVA: 0x000F180A File Offset: 0x000EFA0A
		// (set) Token: 0x060045D2 RID: 17874 RVA: 0x000F1812 File Offset: 0x000EFA12
		public int Outovropt
		{
			get
			{
				return this._outovropt;
			}
			set
			{
				this._outovropt = value;
			}
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x060045D3 RID: 17875 RVA: 0x000F181B File Offset: 0x000EFA1B
		public int Qryrowset
		{
			get
			{
				return this._qryrowset;
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x060045D4 RID: 17876 RVA: 0x000F1823 File Offset: 0x000EFA23
		public TYPDEFOVR Typdefovr
		{
			get
			{
				return this._typdefovr;
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x060045D5 RID: 17877 RVA: 0x000F182B File Offset: 0x000EFA2B
		public string Typdefnam
		{
			get
			{
				return this._typdefnam;
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x060045D7 RID: 17879 RVA: 0x000F183C File Offset: 0x000EFA3C
		// (set) Token: 0x060045D6 RID: 17878 RVA: 0x000F1833 File Offset: 0x000EFA33
		public int Encoding { get; set; }

		// Token: 0x060045D8 RID: 17880 RVA: 0x000F1844 File Offset: 0x000EFA44
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.EXCSQLSTT);
			base.WriteCommandSourceId(writer);
			writer.WriteBeginDdm(CodePoint.PKGNAMCSN);
			this.Pkgnamcsn.Write(writer);
			writer.WriteEndDdm();
			writer.WriteScalar1Byte(CodePoint.OUTEXP, this._outexp ? 241 : 240);
			writer.WriteScalar1Byte(CodePoint.RDBCMTOK, this._rdbcmtok ? 241 : 240);
			if (!string.IsNullOrEmpty(this._prcnam))
			{
				writer.WriteScalarString(CodePoint.PRCNAM, this._prcnam, (this.Encoding == -1) ? writer.Ccsid._ccsidsbc : this.Encoding);
			}
			if (this._qryblksz > 0)
			{
				writer.WriteBeginDdm(CodePoint.QRYBLKSZ);
				writer.WriteInt32(this._qryblksz, EndianType.BigEndian);
				writer.WriteEndDdm();
			}
			if (this._maxrslcnt > 0)
			{
				writer.WriteBeginDdm(CodePoint.MAXRSLCNT);
				writer.WriteInt16(this._maxrslcnt, EndianType.BigEndian);
				writer.WriteEndDdm();
			}
			if (this._rslsetflg != null && this._rslsetflg.Length != 0)
			{
				writer.WriteBeginDdm(CodePoint.RSLSETFLG);
				writer.WriteBytes(this._rslsetflg);
				writer.WriteEndDdm();
			}
			if (this._nbrrow > 0)
			{
				writer.WriteBeginDdm(CodePoint.NBRROW);
				writer.WriteInt32(this._nbrrow, EndianType.BigEndian);
				writer.WriteEndDdm();
			}
			if (this._outovropt >= 1 && this._outovropt <= 3)
			{
				writer.WriteBeginDdm(CodePoint.OUTOVROPT);
				writer.WriteByte(this._outovropt);
				writer.WriteEndDdm();
				if (this._outovropt == 3)
				{
					writer.WriteScalar1Byte(CodePoint.DYNDTAFMT, 241);
				}
			}
			writer.WriteEndDdm();
		}

		// Token: 0x060045D9 RID: 17881 RVA: 0x000F19EC File Offset: 0x000EFBEC
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			List<CodePoint> requiredCodePoints = new List<CodePoint>(2);
			requiredCodePoints.Add(CodePoint.PKGNAMCSN);
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
				if (codepoint > CodePoint.RDBCMTOK)
				{
					if (codepoint <= CodePoint.PRCNAM)
					{
						if (codepoint != CodePoint.CMDSRCID)
						{
							switch (codepoint)
							{
							case CodePoint.RTNSQLSTT:
								this._rtnsqlstt = await reader.ReadBooleanAsync(isAsync, cancellationToken);
								continue;
							case CodePoint.RDBACCCL:
							case CodePoint.PKGNAMCT:
							case CodePoint.UOWDSP:
								goto IL_0FBC;
							case CodePoint.RDBNAM:
								if (this._rdbnam == null)
								{
									this._rdbnam = new RDBNAM();
								}
								await this._rdbnam.ReadAsync(reader, isAsync, cancellationToken);
								continue;
							case CodePoint.OUTEXP:
								this._outexp = await reader.ReadBooleanAsync(isAsync, cancellationToken);
								continue;
							case CodePoint.PKGNAMCSN:
								if (this._pkgnamcsn == null)
								{
									this._pkgnamcsn = new PKGNAMCSN(this._database.PkgnamcsnCcsid);
								}
								await this._pkgnamcsn.ReadAsync(reader, isAsync, cancellationToken);
								requiredCodePoints.Remove(CodePoint.PKGNAMCSN);
								continue;
							case CodePoint.QRYBLKSZ:
								AbstractDdmObject.CheckLength(reader, CodePoint.QRYBLKSZ, 4);
								this._qryblksz = await reader.ReadInt32Async(EndianType.BigEndian, isAsync, cancellationToken);
								if (this._qryblksz < 512 || this._qryblksz > 10485760)
								{
									DrdaException.InvalidValue(CodePoint.QRYBLKSZ);
									continue;
								}
								continue;
							case CodePoint.RTNSQLDA:
								this._rtnsqlda = await reader.ReadBooleanAsync(isAsync, cancellationToken);
								continue;
							default:
								if (codepoint != CodePoint.PRCNAM)
								{
									goto IL_0FBC;
								}
								this._prcnam = await reader.ReadStringAsync((int)reader.DdmObjectLength, (this.Encoding == -1) ? reader.Ccsid._ccsidsbc : this.Encoding, isAsync, cancellationToken);
								continue;
							}
						}
					}
					else if (codepoint <= CodePoint.OUTOVROPT)
					{
						if (codepoint == CodePoint.NBRROW)
						{
							AbstractDdmObject.CheckLength(reader, CodePoint.NBRROW, 4);
							this._nbrrow = await reader.ReadInt32Async(EndianType.BigEndian, isAsync, cancellationToken);
							continue;
						}
						switch (codepoint)
						{
						case CodePoint.MAXRSLCNT:
							AbstractDdmObject.CheckLength(reader, CodePoint.MAXRSLCNT, 2);
							this._maxrslcnt = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
							continue;
						case CodePoint.MAXBLKEXT:
							AbstractDdmObject.CheckLength(reader, CodePoint.MAXBLKEXT, 2);
							this._maxblkext = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
							continue;
						case CodePoint.RSLSETFLG:
							this._rslsetflg = await reader.ReadBytesAsync(isAsync, cancellationToken);
							continue;
						case CodePoint.CMMTYP:
						case CodePoint.BNDOPTNM:
						case CodePoint.BNDOPTVL:
							goto IL_0FBC;
						case CodePoint.TYPSQLDA:
							await this.ParseTypsqldaAsync(reader, isAsync, cancellationToken);
							continue;
						case CodePoint.OUTOVROPT:
							AbstractDdmObject.CheckLength(reader, CodePoint.OUTOVROPT, 1);
							this._outovropt = await reader.ReadUnsignedByteAsync(isAsync, cancellationToken);
							if (this._outovropt != 1 && this._outovropt != 2 && Logger.maxTracingLevel >= 4)
							{
								Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "EXCSQLSTT::Read OUTOVROPT value not defined: " + this._outovropt.ToString(), Array.Empty<object>());
								continue;
							}
							continue;
						default:
							goto IL_0FBC;
						}
					}
					else if (codepoint != CodePoint.QRYROWSET)
					{
						if (codepoint != CodePoint.SQLDTA)
						{
							goto IL_0FBC;
						}
						try
						{
							this._sqldta = new SQLDTA(this._database, (this._typdefovr != null) ? this._typdefovr.Ccsid : this._database.Ccsid);
							this._sqldta.Nbrrow = this._nbrrow;
							await this._sqldta.ReadAsync(reader, isAsync, cancellationToken);
							requiredCodePoints.Remove(CodePoint.SQLDTA);
							continue;
						}
						catch (Exception)
						{
							if (this._pkgnamcsn != null && Logger.maxTracingLevel >= 1)
							{
								Logger.Error(this._tracePoint, base.DatabaseSessionId, "EXCSQLSTT::Read(). Error reading SQLDTA for EXCSQLSTT with " + this._pkgnamcsn.ToString() + ".", Array.Empty<object>());
							}
							throw;
						}
					}
					else
					{
						AbstractDdmObject.CheckLength(reader, CodePoint.QRYROWSET, 4);
						this._qryrowset = await reader.ReadInt32Async(EndianType.BigEndian, isAsync, cancellationToken);
						if (this._qryrowset < 0 || this._qryrowset > 32767)
						{
							DrdaException.InvalidValue(CodePoint.QRYROWSET);
							continue;
						}
						continue;
					}
					this._cmdsrcid = await reader.ReadInt64Async(EndianType.BigEndian, isAsync, cancellationToken);
					continue;
				}
				if (codepoint <= CodePoint.TYPDEFOVR)
				{
					if (codepoint == CodePoint.BITSTRDR)
					{
						await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
						continue;
					}
					if (codepoint != CodePoint.TYPDEFNAM)
					{
						if (codepoint == CodePoint.TYPDEFOVR)
						{
							if (this._typdefovr == null)
							{
								this._typdefovr = new TYPDEFOVR(this._database, this._sqlamLevel);
							}
							await this._typdefovr.ReadAsync(reader, isAsync, cancellationToken);
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
				else if (codepoint != CodePoint.EXTDTA)
				{
					if (codepoint == CodePoint.MONITOR)
					{
						await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
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
					if (this._sqldta == null)
					{
						if (Logger.maxTracingLevel >= 4)
						{
							Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "EXCSQLSTT:: SQLDTA has not been received before EXTDTA.", Array.Empty<object>());
						}
						throw new ArgumentException("LOB Data arrived earlier before SQLDTA.");
					}
					await this._sqldta.ParseEXDTAAsync(reader, isAsync, cancellationToken);
					continue;
				}
				IL_0FBC:
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "EXCSQLSTT::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
			if (requiredCodePoints.Count != 0)
			{
				DrdaException.MissingCodePoint(requiredCodePoints[0]);
			}
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x000F1A4C File Offset: 0x000EFC4C
		private async Task ParseTypsqldaAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			AbstractDdmObject.CheckLength(reader, CodePoint.TYPSQLDA, 1);
			byte b = await reader.ReadByteAsync(isAsync, cancellationToken);
			this._typsqlda = (TYPSQLDA)b;
			switch (this._typsqlda)
			{
			case TYPSQLDA.StandardOutput:
			case TYPSQLDA.LightOutput:
			case TYPSQLDA.ExtendedOutput:
				this._rtnOutput = true;
				break;
			case TYPSQLDA.StandardInput:
			case TYPSQLDA.LightInput:
			case TYPSQLDA.ExtendedInput:
				this._rtnOutput = false;
				break;
			default:
				DrdaException.InvalidValue(CodePoint.TYPSQLDA);
				break;
			}
		}

		// Token: 0x04003195 RID: 12693
		private RDBNAM _rdbnam;

		// Token: 0x04003196 RID: 12694
		private PKGNAMCSN _pkgnamcsn;

		// Token: 0x04003197 RID: 12695
		private TYPSQLDA _typsqlda = TYPSQLDA.None;

		// Token: 0x04003198 RID: 12696
		private bool _rtnOutput = true;

		// Token: 0x04003199 RID: 12697
		private bool _outexp;

		// Token: 0x0400319A RID: 12698
		private bool _rtnsqlstt;

		// Token: 0x0400319B RID: 12699
		private int _nbrrow = 1;

		// Token: 0x0400319C RID: 12700
		private string _prcnam;

		// Token: 0x0400319D RID: 12701
		private int _qryblksz;

		// Token: 0x0400319E RID: 12702
		private int _maxrslcnt;

		// Token: 0x0400319F RID: 12703
		private int _maxblkext;

		// Token: 0x040031A0 RID: 12704
		private byte[] _rslsetflg;

		// Token: 0x040031A1 RID: 12705
		private bool _rdbcmtok;

		// Token: 0x040031A2 RID: 12706
		private int _outovropt;

		// Token: 0x040031A3 RID: 12707
		private int _qryrowset = -1;

		// Token: 0x040031A4 RID: 12708
		private TYPDEFOVR _typdefovr;

		// Token: 0x040031A5 RID: 12709
		private string _typdefnam;

		// Token: 0x040031A6 RID: 12710
		private SQLDTA _sqldta;

		// Token: 0x040031A7 RID: 12711
		private bool _rtnsqlda;
	}
}
