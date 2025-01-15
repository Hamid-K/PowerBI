using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x02000898 RID: 2200
	public class OPNQRY : AbstractDdmObject
	{
		// Token: 0x06004602 RID: 17922 RVA: 0x000F339A File Offset: 0x000F159A
		public OPNQRY(IDatabase database, int level, string accrdbTypedefname)
			: base(database, level, accrdbTypedefname)
		{
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x000F33B4 File Offset: 0x000F15B4
		public override string ToString()
		{
			return base.FixParenthis(string.Format("OPNQRY[rdbnam={0};pkgnamcsn={1};typdefovr={2};typdefnam={3};sqldta={4};qryblksz={5};qryblkctl={6};maxblkext={7};qryrowset={8};qryclsimp={9};outovropt={10};cmdsrcid={11}]", new object[]
			{
				this._rdbnam, this._pkgnamcsn, this._typdefovr, this._typdefnam, this._sqldta, this._qryblksz, this._qryblkctl, this._maxblkext, this._qryrowset, this._qryclsimp,
				this._outovropt, this._cmdsrcid
			}));
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x000F346C File Offset: 0x000F166C
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
			this._typsqlda = TYPSQLDA.None;
			this._rtnsqlda = false;
			this._rtnOutput = true;
			this._sqldta = null;
			this._qryblksz = 0;
			this._qryblkctl = 0;
			this._maxblkext = 0;
			this._outovropt = 0;
			this._qryrowset = 0;
			this._qryclsimp = 0;
			this._smldtasz = 0;
			this._typdefnam = null;
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06004605 RID: 17925 RVA: 0x000F3507 File Offset: 0x000F1707
		// (set) Token: 0x06004606 RID: 17926 RVA: 0x000F3530 File Offset: 0x000F1730
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
			set
			{
				this._sqldta = value;
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x06004607 RID: 17927 RVA: 0x000F3539 File Offset: 0x000F1739
		// (set) Token: 0x06004608 RID: 17928 RVA: 0x000F3541 File Offset: 0x000F1741
		public int Qryclsimp
		{
			get
			{
				return this._qryclsimp;
			}
			set
			{
				this._qryclsimp = value;
			}
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06004609 RID: 17929 RVA: 0x000F354A File Offset: 0x000F174A
		// (set) Token: 0x0600460A RID: 17930 RVA: 0x000F3552 File Offset: 0x000F1752
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

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x0600460B RID: 17931 RVA: 0x000F355B File Offset: 0x000F175B
		// (set) Token: 0x0600460C RID: 17932 RVA: 0x000F3563 File Offset: 0x000F1763
		public int Qryblkctl
		{
			get
			{
				return this._qryblkctl;
			}
			set
			{
				this._qryblkctl = value;
			}
		}

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x0600460D RID: 17933 RVA: 0x000F356C File Offset: 0x000F176C
		// (set) Token: 0x0600460E RID: 17934 RVA: 0x000F3574 File Offset: 0x000F1774
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

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x0600460F RID: 17935 RVA: 0x000F357D File Offset: 0x000F177D
		// (set) Token: 0x06004610 RID: 17936 RVA: 0x000F3585 File Offset: 0x000F1785
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

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x06004611 RID: 17937 RVA: 0x000F358E File Offset: 0x000F178E
		// (set) Token: 0x06004612 RID: 17938 RVA: 0x000F3596 File Offset: 0x000F1796
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

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06004613 RID: 17939 RVA: 0x000F359F File Offset: 0x000F179F
		// (set) Token: 0x06004614 RID: 17940 RVA: 0x000F35A7 File Offset: 0x000F17A7
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

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06004615 RID: 17941 RVA: 0x000F35B0 File Offset: 0x000F17B0
		// (set) Token: 0x06004616 RID: 17942 RVA: 0x000F35B8 File Offset: 0x000F17B8
		public int Smldtasz
		{
			get
			{
				return this._smldtasz;
			}
			set
			{
				this._smldtasz = value;
			}
		}

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06004617 RID: 17943 RVA: 0x000F35C1 File Offset: 0x000F17C1
		public bool Rtnsqlda
		{
			get
			{
				return this._rtnsqlda;
			}
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06004618 RID: 17944 RVA: 0x000F35C9 File Offset: 0x000F17C9
		public bool RtnOutput
		{
			get
			{
				return this._rtnOutput;
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06004619 RID: 17945 RVA: 0x000F35D1 File Offset: 0x000F17D1
		// (set) Token: 0x0600461A RID: 17946 RVA: 0x000F35D9 File Offset: 0x000F17D9
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

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x0600461B RID: 17947 RVA: 0x000F35E2 File Offset: 0x000F17E2
		// (set) Token: 0x0600461C RID: 17948 RVA: 0x000F35EA File Offset: 0x000F17EA
		public int Qryrowset
		{
			get
			{
				return this._qryrowset;
			}
			set
			{
				this._qryrowset = value;
			}
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x0600461D RID: 17949 RVA: 0x000F35F3 File Offset: 0x000F17F3
		// (set) Token: 0x0600461E RID: 17950 RVA: 0x000F35FB File Offset: 0x000F17FB
		public TYPDEFOVR Typdefovr
		{
			get
			{
				return this._typdefovr;
			}
			set
			{
				this._typdefovr = value;
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x0600461F RID: 17951 RVA: 0x000F3604 File Offset: 0x000F1804
		// (set) Token: 0x06004620 RID: 17952 RVA: 0x000F360C File Offset: 0x000F180C
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

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x06004621 RID: 17953 RVA: 0x000F3615 File Offset: 0x000F1815
		// (set) Token: 0x06004622 RID: 17954 RVA: 0x000F361D File Offset: 0x000F181D
		public bool Dupqryok
		{
			get
			{
				return this._dupqryok;
			}
			set
			{
				this._dupqryok = value;
			}
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x000F3628 File Offset: 0x000F1828
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.OPNQRY);
			base.WriteCommandSourceId(writer);
			writer.WriteBeginDdm(CodePoint.PKGNAMCSN);
			this.Pkgnamcsn.Write(writer);
			writer.WriteEndDdm();
			writer.WriteBeginDdm(CodePoint.QRYBLKSZ);
			writer.WriteInt32(this._qryblksz, EndianType.BigEndian);
			writer.WriteEndDdm();
			if (this._qryclsimp > 0)
			{
				writer.WriteScalar1Byte(CodePoint.QRYCLSIMP, this._qryclsimp);
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
			if (this._qryblkctl != 0)
			{
				writer.WriteBeginDdm(CodePoint.QRYBLKCTL);
				writer.WriteScalar2Bytes(this._qryblkctl);
				writer.WriteEndDdm();
			}
			if (this._smldtasz != 0)
			{
				writer.WriteBeginDdm(CodePoint.SMLDTASZ);
				writer.WriteInt64((long)this._smldtasz, EndianType.BigEndian);
				writer.WriteEndDdm();
			}
			if (this._maxblkext != 0)
			{
				writer.WriteBeginDdm(CodePoint.MAXBLKEXT);
				writer.WriteInt16(this._maxblkext, EndianType.BigEndian);
				writer.WriteEndDdm();
			}
			writer.WriteEndDdm();
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x000F375C File Offset: 0x000F195C
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			List<CodePoint> requiredCodePoints = new List<CodePoint>(3);
			requiredCodePoints.Add(CodePoint.PKGNAMCSN);
			requiredCodePoints.Add(CodePoint.QRYBLKSZ);
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
				if (codepoint <= CodePoint.RTNSQLDA)
				{
					if (codepoint <= CodePoint.EXTDTA)
					{
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
							if (codepoint != CodePoint.EXTDTA)
							{
								goto IL_0DB8;
							}
							if (this._sqldta == null)
							{
								if (Logger.maxTracingLevel >= 4)
								{
									Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "OPNQRY:: SQLDTA has not been received before EXTDTA.", Array.Empty<object>());
								}
								throw new ArgumentException("LOB Data arrived earlier before SQLDTA.");
							}
							await this._sqldta.ParseEXDTAAsync(reader, isAsync, cancellationToken);
							continue;
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
					else if (codepoint <= CodePoint.CMDSRCID)
					{
						if (codepoint == CodePoint.MONITOR)
						{
							await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
							continue;
						}
						if (codepoint != CodePoint.CMDSRCID)
						{
							goto IL_0DB8;
						}
					}
					else
					{
						if (codepoint == CodePoint.DUPQRYOK)
						{
							this._dupqryok = await reader.ReadBooleanAsync(isAsync, cancellationToken);
							continue;
						}
						switch (codepoint)
						{
						case CodePoint.RDBNAM:
							if (this._rdbnam == null)
							{
								this._rdbnam = new RDBNAM();
							}
							await this._rdbnam.ReadAsync(reader, isAsync, cancellationToken);
							continue;
						case CodePoint.OUTEXP:
						case CodePoint.PKGNAMCT:
						case CodePoint.UOWDSP:
							goto IL_0DB8;
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
							}
							requiredCodePoints.Remove(CodePoint.QRYBLKSZ);
							continue;
						case CodePoint.RTNSQLDA:
							this._rtnsqlda = await reader.ReadBooleanAsync(isAsync, cancellationToken);
							continue;
						default:
							goto IL_0DB8;
						}
					}
				}
				else if (codepoint <= CodePoint.OUTOVROPT)
				{
					if (codepoint <= CodePoint.MAXBLKEXT)
					{
						if (codepoint == CodePoint.QRYBLKCTL)
						{
							this._qryblkctl = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
							continue;
						}
						if (codepoint != CodePoint.MAXBLKEXT)
						{
							goto IL_0DB8;
						}
						AbstractDdmObject.CheckLength(reader, CodePoint.MAXBLKEXT, 2);
						this._maxblkext = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
						continue;
					}
					else
					{
						if (codepoint == CodePoint.TYPSQLDA)
						{
							await this.ParseTypsqldaAsync(reader, isAsync, cancellationToken);
							continue;
						}
						if (codepoint != CodePoint.OUTOVROPT)
						{
							goto IL_0DB8;
						}
						AbstractDdmObject.CheckLength(reader, CodePoint.OUTOVROPT, 1);
						this._outovropt = await reader.ReadUnsignedByteAsync(isAsync, cancellationToken);
						if (this._outovropt != 1 && this._outovropt != 2 && Logger.maxTracingLevel >= 4)
						{
							Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "OPNQRY::Read OUTOVROPT value not defined: " + this._outovropt.ToString(), Array.Empty<object>());
							continue;
						}
						continue;
					}
				}
				else if (codepoint <= CodePoint.QRYCLSIMP)
				{
					if (codepoint != CodePoint.QRYROWSET)
					{
						if (codepoint != CodePoint.QRYCLSIMP)
						{
							goto IL_0DB8;
						}
						AbstractDdmObject.CheckLength(reader, CodePoint.QRYCLSIMP, 1);
						int num = await reader.ReadUnsignedByteAsync(isAsync, cancellationToken);
						if (num != 0 && num != 1 && num != 2 && num != 3 && num != 4)
						{
							DrdaException.InvalidValue(CodePoint.QRYCLSIMP);
							continue;
						}
						continue;
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
				}
				else
				{
					if (codepoint == CodePoint.QRYCLSRLS)
					{
						await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
						continue;
					}
					if (codepoint != CodePoint.SQLDTA)
					{
						goto IL_0DB8;
					}
					try
					{
						this._sqldta = new SQLDTA(this._database, (this._typdefovr != null) ? this._typdefovr.Ccsid : this._database.Ccsid);
						await this._sqldta.ReadAsync(reader, isAsync, cancellationToken);
						requiredCodePoints.Remove(CodePoint.SQLDTA);
						continue;
					}
					catch (Exception)
					{
						if (this._pkgnamcsn != null && Logger.maxTracingLevel >= 1)
						{
							Logger.Error(this._tracePoint, base.DatabaseSessionId, "OPNQRY::Read(). Error reading SQLDTA for OPNQRY with " + this._pkgnamcsn.ToString() + ".", Array.Empty<object>());
						}
						throw;
					}
				}
				this._cmdsrcid = await reader.ReadInt64Async(EndianType.BigEndian, isAsync, cancellationToken);
				continue;
				IL_0DB8:
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "OPNQRY::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
			if (requiredCodePoints.Count != 0)
			{
				DrdaException.MissingCodePoint(requiredCodePoints[0]);
			}
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x000F37BC File Offset: 0x000F19BC
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

		// Token: 0x040031D8 RID: 12760
		private RDBNAM _rdbnam;

		// Token: 0x040031D9 RID: 12761
		private PKGNAMCSN _pkgnamcsn;

		// Token: 0x040031DA RID: 12762
		private TYPDEFOVR _typdefovr;

		// Token: 0x040031DB RID: 12763
		private TYPSQLDA _typsqlda = TYPSQLDA.None;

		// Token: 0x040031DC RID: 12764
		private string _typdefnam;

		// Token: 0x040031DD RID: 12765
		private bool _rtnOutput = true;

		// Token: 0x040031DE RID: 12766
		private SQLDTA _sqldta;

		// Token: 0x040031DF RID: 12767
		private int _qryblksz;

		// Token: 0x040031E0 RID: 12768
		private int _qryblkctl;

		// Token: 0x040031E1 RID: 12769
		private int _maxblkext;

		// Token: 0x040031E2 RID: 12770
		private int _qryrowset;

		// Token: 0x040031E3 RID: 12771
		private int _qryclsimp;

		// Token: 0x040031E4 RID: 12772
		private int _outovropt;

		// Token: 0x040031E5 RID: 12773
		private int _smldtasz;

		// Token: 0x040031E6 RID: 12774
		private bool _rtnsqlda;

		// Token: 0x040031E7 RID: 12775
		private bool _dupqryok;
	}
}
