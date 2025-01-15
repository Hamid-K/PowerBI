using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x0200087C RID: 2172
	public class BNDSQLSTT : AbstractDdmObject
	{
		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x060044EC RID: 17644 RVA: 0x000EB002 File Offset: 0x000E9202
		// (set) Token: 0x060044ED RID: 17645 RVA: 0x000EB00A File Offset: 0x000E920A
		public string TransformedSqlstt
		{
			get
			{
				return this._sqlstt_transformed;
			}
			set
			{
				this._sqlstt_transformed = value;
			}
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x060044EE RID: 17646 RVA: 0x000EB013 File Offset: 0x000E9213
		// (set) Token: 0x060044EF RID: 17647 RVA: 0x000EB01B File Offset: 0x000E921B
		public object TransformerContext
		{
			get
			{
				return this._transformerContext;
			}
			set
			{
				this._transformerContext = value;
			}
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x060044F0 RID: 17648 RVA: 0x000EB024 File Offset: 0x000E9224
		// (set) Token: 0x060044F1 RID: 17649 RVA: 0x000EB02C File Offset: 0x000E922C
		public bool HasOutputParams
		{
			get
			{
				return this._hasOutputParams;
			}
			set
			{
				this._hasOutputParams = value;
			}
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x060044F2 RID: 17650 RVA: 0x000EB035 File Offset: 0x000E9235
		// (set) Token: 0x060044F3 RID: 17651 RVA: 0x000EB03D File Offset: 0x000E923D
		public bool CursorForUpdate
		{
			get
			{
				return this._cursorForUpdate;
			}
			set
			{
				this._cursorForUpdate = value;
			}
		}

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x060044F4 RID: 17652 RVA: 0x000EB046 File Offset: 0x000E9246
		// (set) Token: 0x060044F5 RID: 17653 RVA: 0x000EB04E File Offset: 0x000E924E
		public bool CursorWithHold
		{
			get
			{
				return this._cursorWithHold;
			}
			set
			{
				this._cursorWithHold = value;
			}
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x000EB057 File Offset: 0x000E9257
		public BNDSQLSTT(IDatabase database, int sqlamlevel, string accrdbTypedefname)
			: this(database, sqlamlevel, accrdbTypedefname, -1)
		{
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x000EB063 File Offset: 0x000E9263
		public BNDSQLSTT(IDatabase database, int sqlamlevel, string accrdbTypedefname, int encoding)
			: base(database, sqlamlevel, accrdbTypedefname)
		{
			this._sqlsttvrb = new ArrayList();
			this.IsMultipleRowInsert = false;
			this.IsSqlStatementAssumptionClassified = true;
			this._encoding = encoding;
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x000EB0A4 File Offset: 0x000E92A4
		public override string ToString()
		{
			return base.FixParenthis(string.Format("BNDSQLSTT[rdbnam={0};pkgnamcsn={1};sqlsttnbr={2};pkgsn={3};sqlstt={4};typedefnamr={5};typdefovr={6};sqlsttvrb={7}]", new object[]
			{
				this._rdbnam,
				this._pkgnamcsn,
				this._sqlsttnbr,
				this._pkgsn,
				this._sqlstt.Trim(),
				this._typdefnam,
				this._typedefovr,
				this.GetSqlsttVrbAsString()
			}));
		}

		// Token: 0x060044F9 RID: 17657 RVA: 0x000EB120 File Offset: 0x000E9320
		private object GetSqlsttVrbAsString()
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			foreach (object obj in this._sqlsttvrb)
			{
				STTVRBInfo sttvrbinfo = (STTVRBInfo)obj;
				stringBuilder.Append(string.Format("[precision={0};scale={1};length={2};sqltype={3};ccsid={4};sqlname={5};sqldiagname={6}]", new object[] { sttvrbinfo.precision, sttvrbinfo.scale, sttvrbinfo.length, sttvrbinfo.sqlType, sttvrbinfo.ccsid, sttvrbinfo.sqlName, sttvrbinfo.sqlDiagName }));
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x060044FA RID: 17658 RVA: 0x000EB204 File Offset: 0x000E9404
		// (set) Token: 0x060044FB RID: 17659 RVA: 0x000EB20C File Offset: 0x000E940C
		public int Pkgsn
		{
			get
			{
				return this._pkgsn;
			}
			set
			{
				this._pkgsn = value;
			}
		}

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x060044FC RID: 17660 RVA: 0x000EB215 File Offset: 0x000E9415
		// (set) Token: 0x060044FD RID: 17661 RVA: 0x000EB21D File Offset: 0x000E941D
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

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x060044FE RID: 17662 RVA: 0x000EB226 File Offset: 0x000E9426
		// (set) Token: 0x060044FF RID: 17663 RVA: 0x000EB22E File Offset: 0x000E942E
		public ArrayList Sqlsttvrb
		{
			get
			{
				return this._sqlsttvrb;
			}
			set
			{
				this._sqlsttvrb = value;
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06004500 RID: 17664 RVA: 0x000EB237 File Offset: 0x000E9437
		// (set) Token: 0x06004501 RID: 17665 RVA: 0x000EB23F File Offset: 0x000E943F
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

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06004502 RID: 17666 RVA: 0x000EB248 File Offset: 0x000E9448
		// (set) Token: 0x06004503 RID: 17667 RVA: 0x000EB250 File Offset: 0x000E9450
		public int Sqlsttnbr
		{
			get
			{
				return this._sqlsttnbr;
			}
			set
			{
				this._sqlsttnbr = value;
			}
		}

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x06004504 RID: 17668 RVA: 0x000EB259 File Offset: 0x000E9459
		// (set) Token: 0x06004505 RID: 17669 RVA: 0x000EB261 File Offset: 0x000E9461
		public bool IsSqlStatementAssumptionClassified { get; set; }

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x06004506 RID: 17670 RVA: 0x000EB26A File Offset: 0x000E946A
		// (set) Token: 0x06004507 RID: 17671 RVA: 0x000EB272 File Offset: 0x000E9472
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

		// Token: 0x06004508 RID: 17672 RVA: 0x000EB27C File Offset: 0x000E947C
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			List<CodePoint> requiredCodePoints = new List<CodePoint>(1);
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
				if (codepoint <= CodePoint.PKGSN)
				{
					if (codepoint <= CodePoint.TYPDEFOVR)
					{
						if (codepoint != CodePoint.TYPDEFNAM)
						{
							if (codepoint == CodePoint.TYPDEFOVR)
							{
								this._typedefovr = new TYPDEFOVR(this._database, this._sqlamLevel);
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
					else
					{
						switch (codepoint)
						{
						case CodePoint.CCSIDSBC:
							AbstractDdmObject.CheckLength(reader, CodePoint.CCSIDSBC, 2);
							if (this._typedefovr != null)
							{
								short num = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
								this._typedefovr.Ccsid._ccsidsbc = (int)num;
								continue;
							}
							await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
							continue;
						case CodePoint.CCSIDDBC:
							AbstractDdmObject.CheckLength(reader, CodePoint.CCSIDDBC, 2);
							if (this._typedefovr != null)
							{
								short num = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
								this._typedefovr.Ccsid._ccsiddbc = (int)num;
								continue;
							}
							await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
							continue;
						case CodePoint.CCSIDMBC:
							AbstractDdmObject.CheckLength(reader, CodePoint.CCSIDMBC, 2);
							if (this._typedefovr != null)
							{
								short num = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
								this._typedefovr.Ccsid._ccsidmbc = (int)num;
								continue;
							}
							await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
							continue;
						default:
							if (codepoint == CodePoint.PKGSN)
							{
								this._pkgsn = await reader.ReadInt32Async(EndianType.BigEndian, isAsync, cancellationToken);
								continue;
							}
							break;
						}
					}
				}
				else if (codepoint <= CodePoint.PKGNAMCSN)
				{
					if (codepoint == CodePoint.RDBNAM)
					{
						this._rdbnam = new RDBNAM();
						await this._rdbnam.ReadAsync(reader, isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.PKGNAMCSN)
					{
						this._pkgnamcsn = new PKGNAMCSN(this._database.PkgnamcsnCcsid);
						await this._pkgnamcsn.ReadAsync(reader, isAsync, cancellationToken);
						continue;
					}
				}
				else
				{
					if (codepoint == CodePoint.SQLSTTNBR)
					{
						this._sqlsttnbr = await reader.ReadInt32Async(EndianType.BigEndian, isAsync, cancellationToken);
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
						this._sqlstt = this._sqlstt.Replace(": ", ":").Replace(" . ", ".");
						if (Logger.maxTracingLevel >= 4)
						{
							Logger.Verbose(this._tracePoint, base.DatabaseSessionId, "    Sqlstt in BNDSQLSTT:" + this._sqlstt, Array.Empty<object>());
						}
						requiredCodePoints.Remove(cp);
						continue;
					}
					if (codepoint == CodePoint.SQLSTTVRB)
					{
						await this.ParseSqlSTTVRBAsync(reader, isAsync, cancellationToken);
						continue;
					}
				}
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "BNDSQLSTT::Read CodePoint not supported in " + this.ToString() + ": " + cp.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
			if (requiredCodePoints.Count != 0)
			{
				DrdaException.MissingCodePoint(requiredCodePoints[0]);
			}
		}

		// Token: 0x06004509 RID: 17673 RVA: 0x000EB2DC File Offset: 0x000E94DC
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.BNDSQLSTT);
			if (this._encoding == -1)
			{
				writer.WriteScalarPaddedString(CodePoint.RDBNAM, this._rdbnam.Name, 18);
			}
			else
			{
				writer.WriteScalarString(CodePoint.RDBNAM, this._rdbnam.Name.PadRight(18), this._encoding);
			}
			writer.WriteBeginDdm(CodePoint.PKGNAMCSN);
			this._pkgnamcsn.Write(writer);
			writer.WriteEndDdm();
			if (this._sqlsttnbr > 0)
			{
				writer.WriteScalar4Bytes(CodePoint.SQLSTTNBR, this._sqlsttnbr);
			}
			if (!this.IsSqlStatementAssumptionClassified)
			{
				writer.WriteScalar2Bytes(CodePoint.BNDSTTASM, 9271);
			}
			if (!string.IsNullOrWhiteSpace(this._sqlstt))
			{
				writer.WriteBeginDdm(CodePoint.SQLSTT);
				base.WriteNOCMorNOCS(writer, this._sqlstt);
				writer.WriteEndDdm();
			}
			if (this._sqlsttvrb.Count > 0)
			{
				writer.WriteBeginDdm(CodePoint.SQLSTTVRB);
				writer.WriteInt16(this._sqlsttvrb.Count);
				for (int i = 0; i < this._sqlsttvrb.Count; i++)
				{
					STTVRBInfo sttvrbinfo = (STTVRBInfo)this._sqlsttvrb[i];
					writer.WriteInt16(sttvrbinfo.precision);
					writer.WriteInt16(sttvrbinfo.scale);
					writer.WriteInt64((long)sttvrbinfo.length);
					writer.WriteInt16(sttvrbinfo.sqlType);
					writer.WriteInt16(sttvrbinfo.ccsid);
					base.WriteVCMorVCS(writer, sttvrbinfo.sqlName);
					base.WriteVCMorVCS(writer, sttvrbinfo.sqlDiagName);
					writer.WriteByte(255);
				}
				writer.WriteEndDdm();
			}
			writer.WriteEndDdm();
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x000EB47C File Offset: 0x000E967C
		private async Task ParseSqlSTTVRBAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			EndianType endianType = this._database.ByteOrder;
			short num = await reader.ReadInt16Async(endianType, isAsync, cancellationToken);
			int numParams = (int)num;
			if (Logger.maxTracingLevel >= 4)
			{
				Logger.Verbose(this._tracePoint, base.DatabaseSessionId, "  Reading SQLSTTVRB, Variable #: " + numParams.ToString(), Array.Empty<object>());
			}
			for (int i = 0; i < numParams; i++)
			{
				STTVRBInfo sttvrb = default(STTVRBInfo);
				num = await reader.ReadInt16Async(endianType, isAsync, cancellationToken);
				sttvrb.precision = (int)num;
				num = await reader.ReadInt16Async(endianType, isAsync, cancellationToken);
				sttvrb.scale = (int)num;
				sttvrb.length = (int)(await reader.ReadInt64Async(endianType, isAsync, cancellationToken));
				num = await reader.ReadInt16Async(endianType, isAsync, cancellationToken);
				sttvrb.sqlType = (int)num;
				num = await reader.ReadInt16Async(endianType, isAsync, cancellationToken);
				sttvrb.ccsid = (int)num;
				if (this._sqlamLevel >= 9)
				{
					await reader.ReadInt64Async(isAsync, cancellationToken);
				}
				sttvrb.sqlName = this.GetUniqueParamName(await base.ParseVCMorVCSAsync(reader, (this._typedefovr != null) ? this._typedefovr.Ccsid : this._database.Ccsid, isAsync, cancellationToken));
				sttvrb.sqlDiagName = await base.ParseVCMorVCSAsync(reader, (this._typedefovr != null) ? this._typedefovr.Ccsid : this._database.Ccsid, isAsync, cancellationToken);
				if (this._sqlamLevel > 6)
				{
					await reader.ReadByteAsync(isAsync, cancellationToken);
				}
				this._sqlsttvrb.Add(sttvrb);
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Verbose(this._tracePoint, base.DatabaseSessionId, "  Reading SQLSTTVRB, Name=" + sttvrb.sqlName, Array.Empty<object>());
				}
				sttvrb = default(STTVRBInfo);
			}
		}

		// Token: 0x0600450B RID: 17675 RVA: 0x000EB4DC File Offset: 0x000E96DC
		private string GetUniqueParamName(string sqlName)
		{
			using (IEnumerator enumerator = this._sqlsttvrb.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((STTVRBInfo)enumerator.Current).sqlName.Equals(sqlName, StringComparison.CurrentCultureIgnoreCase))
					{
						if (sqlName.Contains("__"))
						{
							string text = sqlName.Substring(sqlName.LastIndexOf("__") + 1);
							try
							{
								int num = int.Parse(text);
								sqlName = sqlName + "__" + (num + 1).ToString();
								goto IL_0089;
							}
							catch
							{
								sqlName += "__0";
								goto IL_0089;
							}
						}
						sqlName += "__0";
						IL_0089:
						return this.GetUniqueParamName(sqlName);
					}
				}
			}
			return sqlName;
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x0600450C RID: 17676 RVA: 0x000EB5BC File Offset: 0x000E97BC
		// (set) Token: 0x0600450D RID: 17677 RVA: 0x000EB5C4 File Offset: 0x000E97C4
		public string CursorName { get; set; }

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x0600450E RID: 17678 RVA: 0x000EB5CD File Offset: 0x000E97CD
		// (set) Token: 0x0600450F RID: 17679 RVA: 0x000EB5D5 File Offset: 0x000E97D5
		public bool IsMultipleRowInsert { get; set; }

		// Token: 0x040030BF RID: 12479
		private PKGNAMCSN _pkgnamcsn;

		// Token: 0x040030C0 RID: 12480
		private RDBNAM _rdbnam;

		// Token: 0x040030C1 RID: 12481
		private int _sqlsttnbr;

		// Token: 0x040030C2 RID: 12482
		private int _pkgsn;

		// Token: 0x040030C3 RID: 12483
		private ArrayList _sqlsttvrb;

		// Token: 0x040030C4 RID: 12484
		private string _sqlstt = string.Empty;

		// Token: 0x040030C5 RID: 12485
		private string _typdefnam;

		// Token: 0x040030C6 RID: 12486
		private TYPDEFOVR _typedefovr;

		// Token: 0x040030C7 RID: 12487
		private bool _cursorWithHold;

		// Token: 0x040030C8 RID: 12488
		private bool _cursorForUpdate;

		// Token: 0x040030C9 RID: 12489
		private bool _hasOutputParams;

		// Token: 0x040030CA RID: 12490
		private string _sqlstt_transformed;

		// Token: 0x040030CB RID: 12491
		private int _encoding = -1;

		// Token: 0x040030CC RID: 12492
		private object _transformerContext;
	}
}
