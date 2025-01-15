using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008EC RID: 2284
	public class TYPDEFOVR : AbstractDdmObject
	{
		// Token: 0x0600485A RID: 18522 RVA: 0x0010803A File Offset: 0x0010623A
		public TYPDEFOVR(IDatabase database, int sqlamLevel)
			: base(database, sqlamLevel)
		{
			this._ccsid = new Ccsid();
			this._sqlsttvrb = new ArrayList();
		}

		// Token: 0x0600485B RID: 18523 RVA: 0x00108070 File Offset: 0x00106270
		public override string ToString()
		{
			return string.Format("TYPDEFOVR[ccsid={0};sqlstt={1};sqldta={2}]", this._ccsid, this._sqlstt.Trim(), this._sqldta);
		}

		// Token: 0x0600485C RID: 18524 RVA: 0x00108093 File Offset: 0x00106293
		public override void Reset()
		{
			this._ccsid = new Ccsid();
			this._trgdftrt = false;
			this._userId = "";
			this._sqldta = null;
			this._sqlstt = null;
			this._sqlsttvrb = new ArrayList();
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x0600485D RID: 18525 RVA: 0x001080CB File Offset: 0x001062CB
		// (set) Token: 0x0600485E RID: 18526 RVA: 0x001080D3 File Offset: 0x001062D3
		public SQLDTA Sqldta
		{
			get
			{
				return this._sqldta;
			}
			set
			{
				this._sqldta = value;
			}
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x0600485F RID: 18527 RVA: 0x001080DC File Offset: 0x001062DC
		// (set) Token: 0x06004860 RID: 18528 RVA: 0x001080E4 File Offset: 0x001062E4
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

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06004861 RID: 18529 RVA: 0x001080ED File Offset: 0x001062ED
		public ArrayList Sqlsttvrb
		{
			get
			{
				return this._sqlsttvrb;
			}
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06004862 RID: 18530 RVA: 0x001080F5 File Offset: 0x001062F5
		// (set) Token: 0x06004863 RID: 18531 RVA: 0x001080FD File Offset: 0x001062FD
		public string UserId
		{
			get
			{
				return this._userId;
			}
			set
			{
				this._userId = value;
			}
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06004864 RID: 18532 RVA: 0x00108106 File Offset: 0x00106306
		// (set) Token: 0x06004865 RID: 18533 RVA: 0x0010810E File Offset: 0x0010630E
		public bool Trgdftrt
		{
			get
			{
				return this._trgdftrt;
			}
			set
			{
				this._trgdftrt = value;
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06004866 RID: 18534 RVA: 0x00108117 File Offset: 0x00106317
		// (set) Token: 0x06004867 RID: 18535 RVA: 0x0010811F File Offset: 0x0010631F
		public Ccsid Ccsid
		{
			get
			{
				return this._ccsid;
			}
			set
			{
				this._ccsid = value;
			}
		}

		// Token: 0x06004868 RID: 18536 RVA: 0x00108128 File Offset: 0x00106328
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			bool isMbcSet = false;
			bool isSbcSet = false;
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
				if (codepoint > CodePoint.CCSIDXML)
				{
					if (codepoint != CodePoint.SQLDTA)
					{
						if (codepoint != CodePoint.SQLSTT)
						{
							if (codepoint != CodePoint.SQLSTTVRB)
							{
								goto IL_05FD;
							}
						}
						else
						{
							this._sqlstt = await base.ParseEncodedStringAsync(reader, this._ccsid, this._sqlamLevel, isAsync, cancellationToken);
							if (Logger.maxTracingLevel >= 4)
							{
								Logger.Verbose(this._tracePoint, base.DatabaseSessionId, "Sqlstt in TYPDEFOVR:" + this._sqlstt, Array.Empty<object>());
								continue;
							}
							continue;
						}
					}
					else
					{
						try
						{
							this._sqldta = new SQLDTA(this._database, this.Ccsid);
							await this._sqldta.ReadAsync(reader, isAsync, cancellationToken);
							continue;
						}
						catch (Exception)
						{
							if (!string.IsNullOrEmpty(this._sqlstt) && Logger.maxTracingLevel >= 1)
							{
								Logger.Error(this._tracePoint, base.DatabaseSessionId, "TYPDEFOVR::Read(). Error reading SQLDTA for TYPDEFOVR with SQLSTT " + this._sqlstt + ".", Array.Empty<object>());
							}
							throw;
						}
					}
					await this.ParseSqlSTTVRBAsync(reader, isAsync, cancellationToken);
					continue;
				}
				switch (codepoint)
				{
				case CodePoint.CCSIDSBC:
				{
					AbstractDdmObject.CheckLength(reader, CodePoint.CCSIDSBC, 2);
					short num = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
					this._ccsid._ccsidsbc = (int)num;
					isSbcSet = true;
					continue;
				}
				case CodePoint.CCSIDDBC:
				{
					AbstractDdmObject.CheckLength(reader, CodePoint.CCSIDDBC, 2);
					short num = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
					this._ccsid._ccsiddbc = (int)num;
					continue;
				}
				case CodePoint.CCSIDMBC:
				{
					AbstractDdmObject.CheckLength(reader, CodePoint.CCSIDMBC, 2);
					short num = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
					this._ccsid._ccsidmbc = (int)num;
					isMbcSet = true;
					continue;
				}
				default:
					if (codepoint == CodePoint.CCSIDXML)
					{
						AbstractDdmObject.CheckLength(reader, CodePoint.CCSIDXML, 2);
						short num = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
						this._ccsid._ccsidxml = (int)num;
						continue;
					}
					break;
				}
				IL_05FD:
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "TYPDEFOVR::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
			if (isSbcSet && !isMbcSet)
			{
				this._ccsid._ccsidmbc = this._ccsid._ccsidsbc;
			}
			if (!isSbcSet && isMbcSet)
			{
				this._ccsid._ccsidsbc = this._ccsid._ccsidmbc;
			}
			if (this._ccsid._ccsidsbc == -1)
			{
				this._ccsid._ccsidsbc = 500;
			}
			if (this._ccsid._ccsidmbc == -1)
			{
				this._ccsid._ccsidmbc = 500;
			}
			if (this._ccsid._ccsiddbc == -1)
			{
				this._ccsid._ccsiddbc = 1200;
			}
		}

		// Token: 0x06004869 RID: 18537 RVA: 0x00108188 File Offset: 0x00106388
		private async Task ParseSqlSTTVRBAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			short num = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
			int numParams = (int)num;
			if (Logger.maxTracingLevel >= 4)
			{
				Logger.Verbose(this._tracePoint, base.DatabaseSessionId, "  Reading SQLSTTVRB in TYPDEFOVR, Variable #: " + numParams.ToString(), Array.Empty<object>());
			}
			for (int i = 0; i < numParams; i++)
			{
				STTVRBInfo sttvrb = default(STTVRBInfo);
				num = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
				sttvrb.precision = (int)num;
				num = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
				sttvrb.scale = (int)num;
				sttvrb.length = (int)(await reader.ReadInt64Async(EndianType.BigEndian, isAsync, cancellationToken));
				num = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
				sttvrb.sqlType = (int)num;
				num = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
				sttvrb.ccsid = (int)num;
				sttvrb.sqlName = this.GetUniqueParamName(await base.ParseVCMorVCSAsync(reader, (this.Ccsid != null) ? this.Ccsid : this._database.Ccsid, isAsync, cancellationToken));
				sttvrb.sqlDiagName = await base.ParseVCMorVCSAsync(reader, (this.Ccsid != null) ? this.Ccsid : this._database.Ccsid, isAsync, cancellationToken);
				if (this._sqlamLevel > 6)
				{
					await reader.ReadByteAsync(isAsync, cancellationToken);
				}
				this._sqlsttvrb.Add(sttvrb);
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Verbose(this._tracePoint, base.DatabaseSessionId, "  Reading SQLSTTVRB in TYPDEFOVR, Name=" + sttvrb.sqlName, Array.Empty<object>());
				}
				sttvrb = default(STTVRBInfo);
			}
		}

		// Token: 0x0600486A RID: 18538 RVA: 0x001081E8 File Offset: 0x001063E8
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

		// Token: 0x0600486B RID: 18539 RVA: 0x001082C8 File Offset: 0x001064C8
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.TYPDEFOVR);
			writer.WriteScalar2Bytes(CodePoint.CCSIDSBC, this.Ccsid._ccsidsbc);
			writer.WriteScalar2Bytes(CodePoint.CCSIDMBC, this.Ccsid._ccsidmbc);
			writer.WriteScalar2Bytes(CodePoint.CCSIDDBC, this.Ccsid._ccsiddbc);
			writer.WriteScalar2Bytes(CodePoint.CCSIDXML, this.Ccsid._ccsidxml);
			if (this.Trgdftrt)
			{
				writer.WriteBeginDdm(CodePoint.PKGDFTCST);
				writer.WriteInt16(9269, EndianType.BigEndian);
				writer.WriteEndDdm();
				writer.WriteBeginDdm(CodePoint.USRID);
				writer.WriteBytes(Encoding.GetEncoding(500).GetBytes(this.UserId));
				writer.WriteEndDdm();
			}
			writer.WriteEndDdm();
		}

		// Token: 0x040034FE RID: 13566
		private Ccsid _ccsid;

		// Token: 0x040034FF RID: 13567
		private bool _trgdftrt;

		// Token: 0x04003500 RID: 13568
		private string _userId = "";

		// Token: 0x04003501 RID: 13569
		private string _sqlstt = string.Empty;

		// Token: 0x04003502 RID: 13570
		private SQLDTA _sqldta;

		// Token: 0x04003503 RID: 13571
		private ArrayList _sqlsttvrb;
	}
}
