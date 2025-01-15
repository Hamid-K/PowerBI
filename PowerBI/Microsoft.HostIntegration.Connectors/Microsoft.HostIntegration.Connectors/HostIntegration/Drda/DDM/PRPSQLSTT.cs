using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008A1 RID: 2209
	public class PRPSQLSTT : AbstractDdmObject
	{
		// Token: 0x06004654 RID: 18004 RVA: 0x000F56DE File Offset: 0x000F38DE
		public PRPSQLSTT(IDatabase database, int sqlamLevel, string accrdbTypedefname)
			: base(database, sqlamLevel, accrdbTypedefname)
		{
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x000F5704 File Offset: 0x000F3904
		public override string ToString()
		{
			return base.FixParenthis(string.Format("PRPSQLSTT[rdbnam={0};pkgnamcsn={1};rtnsqlda={2};rtnoutput={3};typesqlda={4};cmdsrcid={5};sqlstt={6};typedefnam={7};typedefovr={8};sqlattr={9};]", new object[]
			{
				this._rdbnam,
				this._pkgnamcsn,
				this._rtnsqlda,
				this._rtnOutput,
				this._typsqlda,
				this._cmdsrcid,
				this._sqlstt.Trim(),
				this._typdefnam,
				this._typedefovr,
				this._sqlattr
			}));
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x000F579C File Offset: 0x000F399C
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
			if (this._typedefovr != null)
			{
				this._typedefovr.Reset();
			}
			this._typsqlda = TYPSQLDA.None;
			this._rtnOutput = true;
			this._rtnsqlda = false;
			this._typdefnam = null;
			this._sqlstt = null;
			this._sqlattr = null;
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x06004657 RID: 18007 RVA: 0x000F580D File Offset: 0x000F3A0D
		// (set) Token: 0x06004658 RID: 18008 RVA: 0x000F5815 File Offset: 0x000F3A15
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

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x06004659 RID: 18009 RVA: 0x000F581E File Offset: 0x000F3A1E
		// (set) Token: 0x0600465A RID: 18010 RVA: 0x000F5826 File Offset: 0x000F3A26
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

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x0600465B RID: 18011 RVA: 0x000F582F File Offset: 0x000F3A2F
		// (set) Token: 0x0600465C RID: 18012 RVA: 0x000F5837 File Offset: 0x000F3A37
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

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x0600465D RID: 18013 RVA: 0x000F5840 File Offset: 0x000F3A40
		// (set) Token: 0x0600465E RID: 18014 RVA: 0x000F5848 File Offset: 0x000F3A48
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

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x0600465F RID: 18015 RVA: 0x000F5851 File Offset: 0x000F3A51
		// (set) Token: 0x06004660 RID: 18016 RVA: 0x000F5859 File Offset: 0x000F3A59
		public bool Rtnsqlda
		{
			get
			{
				return this._rtnsqlda;
			}
			set
			{
				this._rtnsqlda = value;
			}
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06004661 RID: 18017 RVA: 0x000F5862 File Offset: 0x000F3A62
		// (set) Token: 0x06004662 RID: 18018 RVA: 0x000F586A File Offset: 0x000F3A6A
		public bool RtnOutput
		{
			get
			{
				return this._rtnOutput;
			}
			set
			{
				this._rtnOutput = value;
			}
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x06004663 RID: 18019 RVA: 0x000F5873 File Offset: 0x000F3A73
		// (set) Token: 0x06004664 RID: 18020 RVA: 0x000F587B File Offset: 0x000F3A7B
		public string Sqlattr
		{
			get
			{
				return this._sqlattr;
			}
			set
			{
				this._sqlattr = value;
			}
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06004665 RID: 18021 RVA: 0x000F5884 File Offset: 0x000F3A84
		// (set) Token: 0x06004666 RID: 18022 RVA: 0x000F588C File Offset: 0x000F3A8C
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

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x06004667 RID: 18023 RVA: 0x000F5895 File Offset: 0x000F3A95
		// (set) Token: 0x06004668 RID: 18024 RVA: 0x000F589D File Offset: 0x000F3A9D
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

		// Token: 0x06004669 RID: 18025 RVA: 0x000F58A8 File Offset: 0x000F3AA8
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.PRPSQLSTT);
			base.WriteCommandSourceId(writer);
			writer.WriteBeginDdm(CodePoint.PKGNAMCSN);
			this.Pkgnamcsn.Write(writer);
			writer.WriteEndDdm();
			writer.WriteScalar1Byte(CodePoint.RTNSQLDA, this.Rtnsqlda ? 241 : 240);
			if (this._typsqlda != TYPSQLDA.None)
			{
				writer.WriteScalar1Byte(CodePoint.TYPSQLDA, (int)this._typsqlda);
			}
			writer.WriteEndDdm();
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x000F5924 File Offset: 0x000F3B24
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			List<CodePoint> requiredCodePoints = new List<CodePoint>(2);
			requiredCodePoints.Add(CodePoint.PKGNAMCSN);
			IEnumerator<Task<ObjectInfo>> taskEnumerator = (isAsync ? reader.ReadDdmObjectsAsync(cancellationToken).GetEnumerator() : null);
			IEnumerator<ObjectInfo> enumerator = (isAsync ? null : reader.ReadDdmObjects().GetEnumerator());
			while (isAsync ? taskEnumerator.MoveNext() : enumerator.MoveNext())
			{
				ObjectInfo ddmObj;
				if (isAsync)
				{
					ObjectInfo objectInfo = await taskEnumerator.Current;
					ddmObj = objectInfo;
					if (ddmObj.Equals(ObjectInfo.InvalidInstance))
					{
						break;
					}
				}
				else
				{
					ddmObj = enumerator.Current;
				}
				CodePoint codepoint = ddmObj.Codepoint;
				base.LogCodePoint(codepoint);
				CodePoint codepoint2 = ddmObj.Codepoint;
				if (codepoint2 <= CodePoint.RDBNAM)
				{
					if (codepoint2 <= CodePoint.TYPDEFOVR)
					{
						if (codepoint2 != CodePoint.TYPDEFNAM)
						{
							if (codepoint2 != CodePoint.TYPDEFOVR)
							{
								goto IL_097F;
							}
							if (this._typedefovr == null)
							{
								this._typedefovr = new TYPDEFOVR(this._database, this._sqlamLevel);
							}
							await this._typedefovr.ReadAsync(reader, isAsync, cancellationToken);
							if (this._typedefovr.Sqlstt != null)
							{
								this._sqlstt = this._typedefovr.Sqlstt;
							}
						}
						else
						{
							this._typdefnam = await reader.ReadStringAsync(isAsync, cancellationToken);
							if (this._typdefnam.Length > 255)
							{
								DrdaException.TooBig(CodePoint.TYPDEFNAM);
							}
						}
					}
					else if (codepoint2 != CodePoint.MONITOR)
					{
						if (codepoint2 != CodePoint.CMDSRCID)
						{
							if (codepoint2 != CodePoint.RDBNAM)
							{
								goto IL_097F;
							}
							if (this._rdbnam == null)
							{
								this._rdbnam = new RDBNAM();
							}
							await this._rdbnam.ReadAsync(reader, isAsync, cancellationToken);
						}
						else
						{
							this._cmdsrcid = await reader.ReadInt64Async(EndianType.BigEndian, isAsync, cancellationToken);
						}
					}
					else
					{
						await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
					}
				}
				else if (codepoint2 <= CodePoint.RTNSQLDA)
				{
					if (codepoint2 != CodePoint.PKGNAMCSN)
					{
						if (codepoint2 != CodePoint.RTNSQLDA)
						{
							goto IL_097F;
						}
						this._rtnsqlda = await reader.ReadBooleanAsync(isAsync, cancellationToken);
					}
					else
					{
						if (this._pkgnamcsn == null)
						{
							this._pkgnamcsn = new PKGNAMCSN(this._database.PkgnamcsnCcsid);
						}
						await this._pkgnamcsn.ReadAsync(reader, isAsync, cancellationToken);
						requiredCodePoints.Remove(ddmObj.Codepoint);
					}
				}
				else if (codepoint2 != CodePoint.TYPSQLDA)
				{
					if (codepoint2 != CodePoint.SQLSTT)
					{
						if (codepoint2 != CodePoint.SQLATTR)
						{
							goto IL_097F;
						}
						if (this._typedefovr != null)
						{
							this._sqlattr = await base.ParseEncodedStringAsync(reader, this._typedefovr.Ccsid, this._sqlamLevel, isAsync, cancellationToken);
						}
						else
						{
							this._sqlattr = await base.ParseEncodedStringAsync(reader, this._database.Ccsid, this._sqlamLevel, isAsync, cancellationToken);
						}
					}
					else
					{
						if (this._typedefovr != null)
						{
							this._sqlstt = await base.ParseEncodedStringAsync(reader, this._typedefovr.Ccsid, this._sqlamLevel, isAsync, cancellationToken);
						}
						else
						{
							this._sqlstt = await base.ParseEncodedStringAsync(reader, this._database.Ccsid, this._sqlamLevel, isAsync, cancellationToken);
						}
						requiredCodePoints.Remove(ddmObj.Codepoint);
						if (this._sqlstt == null)
						{
							await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
						}
					}
				}
				else
				{
					await this.ParseTypsqldaAsync(reader, isAsync, cancellationToken);
				}
				IL_0A30:
				ddmObj = default(ObjectInfo);
				continue;
				IL_097F:
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "PRPSQLSTT::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
				goto IL_0A30;
			}
			if (requiredCodePoints.Count != 0)
			{
				DrdaException.MissingCodePoint(requiredCodePoints[0]);
			}
		}

		// Token: 0x0600466B RID: 18027 RVA: 0x000F5984 File Offset: 0x000F3B84
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

		// Token: 0x04003231 RID: 12849
		private RDBNAM _rdbnam;

		// Token: 0x04003232 RID: 12850
		private PKGNAMCSN _pkgnamcsn;

		// Token: 0x04003233 RID: 12851
		private bool _rtnsqlda;

		// Token: 0x04003234 RID: 12852
		private bool _rtnOutput = true;

		// Token: 0x04003235 RID: 12853
		private TYPSQLDA _typsqlda = TYPSQLDA.None;

		// Token: 0x04003236 RID: 12854
		private string _sqlstt = string.Empty;

		// Token: 0x04003237 RID: 12855
		private string _typdefnam;

		// Token: 0x04003238 RID: 12856
		private TYPDEFOVR _typedefovr;

		// Token: 0x04003239 RID: 12857
		private string _sqlattr;
	}
}
