using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.CounterTelemetry;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.DDM;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.DrdaClient;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x02000962 RID: 2402
	internal class SqlManager : Manager
	{
		// Token: 0x06004AF0 RID: 19184 RVA: 0x0011F968 File Offset: 0x0011DB68
		public SqlManager(Requester requester)
			: base(requester)
		{
			this._tracePoint = new SqlApplicationManagerTracePoint(requester.TracePoint);
			this._managerCodepoint = ManagerCodePoint.SQLAM;
			this.BinaryCcsid = null;
			if (string.Compare(requester.ConnectionInfo[31], "1", StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				this.autoBinaryCodepage = true;
			}
			else
			{
				this.BinaryCcsid = Utility.ParseCcsid(requester.ConnectionInfo[31]);
			}
			if (this.BinaryCcsid == null && this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Binary CCSID: " + requester.ConnectionInfo[31]);
			}
			this.DatetimeAsChar = Utility.ParseBoolean(requester.ConnectionInfo[28]);
			this.DatetimeAsDate = Utility.ParseBoolean(requester.ConnectionInfo[33]);
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06004AF1 RID: 19185 RVA: 0x0011FA2F File Offset: 0x0011DC2F
		// (set) Token: 0x06004AF2 RID: 19186 RVA: 0x0011FA37 File Offset: 0x0011DC37
		public Ccsid BinaryCcsid { get; private set; }

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06004AF3 RID: 19187 RVA: 0x0011FA40 File Offset: 0x0011DC40
		// (set) Token: 0x06004AF4 RID: 19188 RVA: 0x0011FA48 File Offset: 0x0011DC48
		public bool DatetimeAsChar { get; private set; }

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06004AF5 RID: 19189 RVA: 0x0011FA51 File Offset: 0x0011DC51
		// (set) Token: 0x06004AF6 RID: 19190 RVA: 0x0011FA59 File Offset: 0x0011DC59
		public bool DatetimeAsDate { get; private set; }

		// Token: 0x06004AF7 RID: 19191 RVA: 0x0011FA62 File Offset: 0x0011DC62
		public override void Initialize()
		{
			base.Initialize();
			this._level = 11;
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x0011FA72 File Offset: 0x0011DC72
		protected override bool IsLevelSupported(int newLevel)
		{
			return newLevel >= 8;
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x0011FA7C File Offset: 0x0011DC7C
		public override void Reset()
		{
			if (this._requester.TracePoint != null && this._tracePoint.TraceContainer != this._requester.TracePoint.TraceContainer)
			{
				this._tracePoint = new SqlApplicationManagerTracePoint(this._requester.TracePoint);
			}
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x0011FACC File Offset: 0x0011DCCC
		public async Task SubmitSecchkAndAccrdbAsync(bool isAsync, CancellationToken cancellationToken)
		{
			string rdbName = this._requester.RdbName;
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SecurityManager::SubmitSecchkAsync");
			}
			byte[] secToken = null;
			SECCHK secchk = new SECCHK();
			base.InitializeCodepoint(secchk);
			secchk.Secmec = this._requester.SecurityManager.TypeOfSecMec;
			secchk.RDBNAM = this._requester.RdbName;
			secchk.NewPassword = this._requester.SecurityManager.NewPassword;
			ACCRDB accrdb = new ACCRDB(null, this._level);
			base.InitializeCodepoint(accrdb);
			accrdb.RDBNAM = new RDBNAM(this._requester.RdbName);
			accrdb.Prdid = SqlManager.productId;
			accrdb.Rdbalwupd = true;
			accrdb.Typdefnam = this._requester.TypeDefinitionName;
			accrdb.Trgdftrt = true;
			TYPDEFOVR typdefovr = new TYPDEFOVR(null, this._level);
			typdefovr.Ccsid = this._requester.CcsidHost;
			if (typdefovr.Ccsid == null)
			{
				typdefovr.Ccsid = new Ccsid();
			}
			accrdb.Typedefovr = typdefovr;
			string text = Requester.ProcessName;
			if (this._requester.ServerMajorVersion <= 8U || this._requester.IsUdb)
			{
				if (text.Length > 12)
				{
					text = text.Substring(0, 12);
				}
			}
			else if (text.Length > 255)
			{
				text = text.Substring(0, 255);
			}
			accrdb.Crrtkn = Encoding.ASCII.GetBytes(text);
			ACCRDBRM accrdbrm = new ACCRDBRM(this._level);
			base.InitializeCodepoint(accrdbrm);
			Manager.ReplyInfo replyInfo = null;
			switch (secchk.Secmec)
			{
			case SecurityMechanism.USRENCPWD:
				secchk.Usrid = (this._requester.SecurityManager.IsEsso ? this._requester.SecurityManager.EssoUserId : this._requester.ConnectionInfo[1]);
				secchk.Sectkn = this._requester.SecurityManager.DecryptionHelper.EncryptText(this._requester.SecurityManager.IsEsso ? this._requester.SecurityManager.EssoUserId : this._requester.ConnectionInfo[2], this._requester.SecurityManager.Key, this._requester.SecurityManager.Iv);
				goto IL_04EA;
			case SecurityMechanism.EUSRIDPWD:
				secchk.Sectkn = this._requester.SecurityManager.DecryptionHelper.EncryptText(this._requester.SecurityManager.IsEsso ? this._requester.SecurityManager.EssoUserId : this._requester.ConnectionInfo[1], this._requester.SecurityManager.Key, this._requester.SecurityManager.Iv);
				secchk.Sectkn2 = this._requester.SecurityManager.DecryptionHelper.EncryptText(this._requester.SecurityManager.IsEsso ? this._requester.SecurityManager.EssoPassword : this._requester.ConnectionInfo[2], this._requester.SecurityManager.Key, this._requester.SecurityManager.Iv);
				goto IL_04EA;
			case SecurityMechanism.KERSEC:
				if (this._requester.SecurityManager.KerbManager == null)
				{
					this._requester.SecurityManager.KerbManager = new KerberosManager(this._requester, (SecurityManagerTracePoint)this._tracePoint);
					this._requester.SecurityManager.KerbManager.Initialize();
				}
				else
				{
					this._requester.SecurityManager.KerbManager.Reset();
				}
				this._requester.SecurityManager.KerbManager.PrincipleName = this._requester.SecurityManager.Principal;
				this._requester.SecurityManager.KerbManager.ProcessSecurityToken(ref secToken);
				goto IL_04EA;
			}
			secchk.Usrid = (this._requester.SecurityManager.IsEsso ? this._requester.SecurityManager.EssoUserId : this._requester.ConnectionInfo[1]);
			secchk.Password = (this._requester.SecurityManager.IsEsso ? this._requester.SecurityManager.EssoPassword : this._requester.ConnectionInfo[2]);
			IL_04EA:
			SECCHKRM secchkrm;
			for (;;)
			{
				secchkrm = new SECCHKRM();
				base.InitializeCodepoint(secchkrm);
				secchk.AutoFlush = false;
				try
				{
					await secchk.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, (secToken == null) ? 64 : 80, isAsync, cancellationToken);
					if (secToken != null)
					{
						await base.WriteDDMCodepoint(CodePoint.SECTKN, secToken, DssType.Object, 1, 0, false, isAsync, cancellationToken);
					}
					secToken = null;
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitAccrdbAsync:  sending ACCRDB...");
					}
					await accrdb.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, (this._requester.Flavor == DrdaFlavor.Informix) ? 80 : 0, isAsync, cancellationToken);
					if (this._requester.Flavor == DrdaFlavor.Informix)
					{
						this._requester.ConnectionManager.DdmWriter.CreateDssObject(1);
						this._requester.ConnectionManager.DdmWriter.WriteBeginDdm(CodePoint.UNKNOWNC204);
						this._requester.ConnectionManager.DdmWriter.WriteStringSBCS("DELIMIDENT=Y;");
						this._requester.ConnectionManager.DdmWriter.WriteEndDdm();
						this._requester.ConnectionManager.DdmWriter.WriteEndDss();
						await this._requester.ConnectionManager.DdmWriter.WriteEndChainAsync(0, true, isAsync, cancellationToken);
					}
					do
					{
						CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
						if (this._tracePoint.IsEnabled(TraceFlags.Information))
						{
							this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
						}
						CodePoint codePoint = currentCP;
						if (codePoint != CodePoint.SECCHKRM)
						{
							if (codePoint != CodePoint.ACCRDBRM)
							{
								if (codePoint != CodePoint.SQLCARD)
								{
									Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
									if (this._tracePoint.IsEnabled(TraceFlags.Warning))
									{
										this._tracePoint.Trace(TraceFlags.Warning, "SecurityManager::SubmitSecchkAsync: Read unexpected CodePoint: " + currentCP.ToString());
									}
									if (replyInfo2 != null)
									{
										replyInfo = replyInfo2;
									}
								}
								else
								{
									await this.ReadSqlcagrpAsync(isAsync, cancellationToken);
								}
							}
							else
							{
								await accrdbrm.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
								this._requester.TypeDefinitionName = accrdbrm.Typdefnam;
								if (accrdbrm.Srvlst != null)
								{
									this._requester.ConnectionManager.ProcessDdmObject(accrdbrm.Srvlst);
								}
								this._requester.RdbInterruptToken = accrdbrm.RdbIntToken;
								if (accrdbrm.IpAddresss != null)
								{
									this._requester.RdbInterruptEndPoint = new IPEndPoint(new IPAddress(accrdbrm.IpAddresss.IPAddressBytes), (int)accrdbrm.IpAddresss.Port);
								}
								if (accrdbrm.Typedefovr != null)
								{
									this._requester.CcsidRead = accrdbrm.Typedefovr.Ccsid;
									if (this.autoBinaryCodepage)
									{
										this.BinaryCcsid = this._requester.CcsidRead;
									}
								}
								this._requester.CcsidWrite = typdefovr.Ccsid;
							}
						}
						else
						{
							await secchkrm.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
						}
					}
					while (base.NeedReadMoreDdmCodepoint(1));
				}
				catch (Exception ex)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "SecurityManager::SubmitSecchkAsync(): " + ex.ToString());
					}
					if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
					{
						this._requester.ConnectionManager.DdmWriter.Reset();
					}
					throw this._requester.MakeException(ex.Message, "HY000", -1041, ex.HResult);
				}
				base.ProcessReplyInfo(null, replyInfo, "SecurityManager::SubmitSecchkAsync");
				bool flag;
				if (secchk.Secmec == SecurityMechanism.KERSEC && secchkrm.Secchkcd == 25)
				{
					flag = true;
					secToken = secchkrm.Sectkn;
					this._requester.SecurityManager.KerbManager.ProcessSecurityToken(ref secToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "SecurityManager::SubmitSecchkAsync Kerberos need more security context.");
					}
				}
				else
				{
					if (secchkrm.Secchkcd != 0)
					{
						break;
					}
					flag = false;
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "SecurityManager::SubmitSecchkAsync authentication is successful.");
					}
					secchkrm = null;
				}
				if (!flag)
				{
					goto Block_21;
				}
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Error))
			{
				this._tracePoint.Trace(TraceFlags.Error, "SecurityManager::SubmitSecchkAsync(): Unexpected Secckcd code: " + secchkrm.Secchkcd.ToString());
			}
			throw this._requester.MakeException(RequesterResource.UnauthorizedAccess, "HY000", -1030);
			Block_21:
			base.ProcessReplyInfo(null, replyInfo, "SqlManager::SubmitAccrdbAsync");
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlManager::SubmitAccrdbAsync");
			}
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x0011FB24 File Offset: 0x0011DD24
		public async Task SubmitAccrdbAsync(bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlManager::SubmitAccrdbAsync");
			}
			ACCRDB accrdb = new ACCRDB(null, this._level, this._requester.IsIMSDB);
			base.InitializeCodepoint(accrdb);
			accrdb.RDBNAM = new RDBNAM(this._requester.RdbName);
			accrdb.Prdid = SqlManager.productId;
			TYPDEFOVR typdefovr = new TYPDEFOVR(null, this._level);
			if (!this._requester.IsIMSDB)
			{
				accrdb.Rdbalwupd = true;
				accrdb.Trgdftrt = true;
				typdefovr.Ccsid = this._requester.CcsidHost;
				if (typdefovr.Ccsid == null)
				{
					typdefovr.Ccsid = new Ccsid();
				}
				accrdb.Typedefovr = typdefovr;
				string text = Requester.ProcessName;
				if (this._requester.ServerMajorVersion <= 8U || this._requester.IsUdb)
				{
					if (text.Length > 12)
					{
						text = text.Substring(0, 12);
					}
				}
				else if (text.Length > 255)
				{
					text = text.Substring(0, 255);
				}
				accrdb.Crrtkn = Encoding.ASCII.GetBytes(text);
			}
			else
			{
				typdefovr.Ccsid = this._requester.CcsidHost;
			}
			if (this._requester.IsIMSDB)
			{
				accrdb.Typdefnam = "QTDSQL370";
			}
			else
			{
				accrdb.Typdefnam = this._requester.TypeDefinitionName;
			}
			ACCRDBRM accrdbrm = new ACCRDBRM(this._level);
			base.InitializeCodepoint(accrdbrm);
			Manager.ReplyInfo replyInfo = null;
			try
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitAccrdbAsync:  sending ACCRDB...");
				}
				await accrdb.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, (this._requester.Flavor == DrdaFlavor.Informix) ? 80 : 0, isAsync, cancellationToken);
				if (this._requester.Flavor == DrdaFlavor.Informix)
				{
					this._requester.ConnectionManager.DdmWriter.CreateDssObject(1);
					this._requester.ConnectionManager.DdmWriter.WriteBeginDdm(CodePoint.UNKNOWNC204);
					this._requester.ConnectionManager.DdmWriter.WriteStringSBCS("DELIMIDENT=Y;");
					this._requester.ConnectionManager.DdmWriter.WriteEndDdm();
					this._requester.ConnectionManager.DdmWriter.WriteEndDss();
					await this._requester.ConnectionManager.DdmWriter.WriteEndChainAsync(0, true, isAsync, cancellationToken);
				}
				do
				{
					CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
					}
					CodePoint codePoint = currentCP;
					if (codePoint != CodePoint.ACCRDBRM)
					{
						if (codePoint != CodePoint.SQLCARD)
						{
							Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
							if (this._tracePoint.IsEnabled(TraceFlags.Warning))
							{
								this._tracePoint.Trace(TraceFlags.Warning, "SqlManager::SubmitExcsqlset(): Read unexpected CodePoint: " + currentCP.ToString());
							}
							if (replyInfo2 != null)
							{
								replyInfo = replyInfo2;
							}
						}
						else
						{
							await this.ReadSqlcagrpAsync(isAsync, cancellationToken);
						}
					}
					else
					{
						await accrdbrm.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
						this._requester.TypeDefinitionName = accrdbrm.Typdefnam;
						if (accrdbrm.Srvlst != null)
						{
							this._requester.ConnectionManager.ProcessDdmObject(accrdbrm.Srvlst);
						}
						this._requester.RdbInterruptToken = accrdbrm.RdbIntToken;
						if (accrdbrm.IpAddresss != null)
						{
							this._requester.RdbInterruptEndPoint = new IPEndPoint(new IPAddress(accrdbrm.IpAddresss.IPAddressBytes), (int)accrdbrm.IpAddresss.Port);
						}
						if (accrdbrm.Typedefovr != null)
						{
							this._requester.CcsidRead = accrdbrm.Typedefovr.Ccsid;
							if (this.autoBinaryCodepage)
							{
								this.BinaryCcsid = this._requester.CcsidRead;
							}
						}
						this._requester.CcsidWrite = typdefovr.Ccsid;
					}
				}
				while (base.NeedReadMoreDdmCodepoint(1));
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "SqlManager::SubmitAccrdbAsync: " + ex.ToString());
				}
				if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
				{
					this._requester.ConnectionManager.DdmWriter.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -1043, ex.HResult);
			}
			base.ProcessReplyInfo(null, replyInfo, "SqlManager::SubmitAccrdbAsync");
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlManager::SubmitAccrdbAsync");
			}
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x0011FB7C File Offset: 0x0011DD7C
		internal async Task<int> SubmitExcsqlset(SqlStatement sqlStatement, List<string> settings, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlManager::SubmitExcsqlimm");
			}
			this.ValidateStatement(sqlStatement, SqlStatement.SqlState.EXCSQLSET);
			sqlStatement.SqlcaCode = 0;
			EXCSQLSET excsqlset = new EXCSQLSET(null, this._level, this._requester.TypeDefinitionName);
			base.InitializeCodepoint(excsqlset);
			excsqlset.AutoFlush = false;
			excsqlset.Pkgnamcsn = sqlStatement.Pkgnamcsn;
			Manager.ReplyInfo replyInfo = null;
			SQLCAGRP sqlcagrp = null;
			try
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitExcsqlset:  sending EXCSQLSET...");
				}
				await excsqlset.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 80, isAsync, cancellationToken);
				int count = 0;
				foreach (string text in settings)
				{
					count++;
					SQLSTT sqlstt = new SQLSTT(null, this._level, this._requester.TypeDefinitionName);
					base.InitializeCodepoint(sqlstt);
					sqlstt.AutoFlush = false;
					sqlstt.Sqlstt = text;
					byte b = 80;
					if (count == settings.Count)
					{
						b = 0;
					}
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitExcsqlset:  sending SQLSTT... " + text);
					}
					await sqlstt.WriteObjectDssAsync(this._requester.ConnectionManager.DdmWriter, 1, b, isAsync, cancellationToken);
				}
				List<string>.Enumerator enumerator = default(List<string>.Enumerator);
				await this._requester.ConnectionManager.DdmWriter.FlushAsync(isAsync, cancellationToken);
				do
				{
					CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
					}
					if (currentCP == CodePoint.SQLCARD)
					{
						sqlcagrp = await this.ReadSqlcagrpAsync(isAsync, cancellationToken);
					}
					else
					{
						Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
						if (this._tracePoint.IsEnabled(TraceFlags.Warning))
						{
							this._tracePoint.Trace(TraceFlags.Warning, "SqlManager::SubmitExcsqlset(): Read unexpected CodePoint: " + currentCP.ToString());
						}
						if (replyInfo2 != null)
						{
							replyInfo = replyInfo2;
						}
					}
				}
				while (base.NeedReadMoreDdmCodepoint(1));
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "SqlManager::SubmitExcsqlset: " + ex.ToString());
				}
				if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
				{
					this._requester.ConnectionManager.DdmWriter.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			int num = await base.ProcessSqlCa(sqlcagrp, sqlStatement, isAsync, cancellationToken);
			base.ProcessReplyInfo(sqlStatement, replyInfo, "SqlManager::SubmitExcsqlset");
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlManager::SubmitExcsqlset");
			}
			return num;
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x0011FBE4 File Offset: 0x0011DDE4
		public async Task SubmitPrpsqlstt(SqlStatement sqlStatement, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlManager::SubmitPrpsqlstt");
			}
			this.ValidateStatement(sqlStatement, SqlStatement.SqlState.PRPSQLSTT);
			sqlStatement.SqlcaCode = 0;
			PRPSQLSTT prpsqlstt = new PRPSQLSTT(null, this._level, this._requester.TypeDefinitionName);
			base.InitializeCodepoint(prpsqlstt);
			prpsqlstt.AutoFlush = false;
			prpsqlstt.Pkgnamcsn = sqlStatement.Pkgnamcsn;
			prpsqlstt.Rtnsqlda = true;
			prpsqlstt.Cmdsrcid = sqlStatement.CommandSourceId;
			prpsqlstt.Typsqlda = TYPSQLDA.ExtendedOutput;
			Manager.ReplyInfo replyInfo = null;
			SQLCAGRP sqlcagrp = null;
			SQLDARD sqldard = null;
			try
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitPrpsqlstt:  sending PRPSQLSTT...");
				}
				await prpsqlstt.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 80, isAsync, cancellationToken);
				string sqlAttributeString = sqlStatement.SqlAttributeString;
				if (!string.IsNullOrEmpty(sqlAttributeString))
				{
					await this.WriteSqlAttr(sqlAttributeString, isAsync, cancellationToken);
				}
				if ((sqlStatement.LiteralReplacementInvestigator != null && sqlStatement.LiteralReplacementInvestigator(this._requester.LiteralReplacement)) || (sqlStatement.LiteralReplacementInvestigator == null && this._requester.LiteralReplacement))
				{
					await this.WriteSqlAttr("CONCENTRATE STATEMENTS WITH LITERALS", isAsync, cancellationToken);
				}
				SQLSTT sqlstt = new SQLSTT(null, this._level, this._requester.TypeDefinitionName);
				base.InitializeCodepoint(sqlstt);
				sqlstt.AutoFlush = false;
				sqlstt.Sqlstt = sqlStatement.CommandText;
				Ccsid defaultCcsid = this._requester.ConnectionManager.DdmReader.Ccsid;
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitPrpsqlstt:  sending SQLSTT...");
				}
				await sqlstt.WriteObjectDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 0, isAsync, cancellationToken);
				await this._requester.ConnectionManager.DdmWriter.FlushAsync(isAsync, cancellationToken);
				if (this._requester.ServerClass == "DB2/6000")
				{
					Thread.Sleep(500);
				}
				do
				{
					CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
					}
					CodePoint codePoint = currentCP;
					TYPDEFOVR typedefovr;
					if (codePoint != CodePoint.TYPDEFOVR)
					{
						if (codePoint != CodePoint.SQLCARD)
						{
							if (codePoint != CodePoint.SQLDARD)
							{
								Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
								if (this._tracePoint.IsEnabled(TraceFlags.Warning))
								{
									this._tracePoint.Trace(TraceFlags.Warning, "SqlManager::SubmitPrpsqlstt(): Read unexpected CodePoint: " + currentCP.ToString());
								}
								if (replyInfo2 != null)
								{
									replyInfo = replyInfo2;
								}
							}
							else
							{
								sqldard = await this.ReadSqldardAsync(isAsync, cancellationToken);
							}
						}
						else
						{
							sqlcagrp = await this.ReadSqlcagrpAsync(isAsync, cancellationToken);
						}
					}
					else
					{
						typedefovr = new TYPDEFOVR(null, -1);
						await typedefovr.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
						this._requester.ConnectionManager.DdmReader.Ccsid = typedefovr.Ccsid;
					}
					typedefovr = null;
				}
				while (base.NeedReadMoreDdmCodepoint(1));
				this._requester.ConnectionManager.DdmReader.Ccsid = defaultCcsid;
				defaultCcsid = null;
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "SqlManager::SubmitPrpsqlstt: " + ex.ToString());
				}
				if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
				{
					this._requester.ConnectionManager.DdmWriter.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			await base.ProcessSqlCa(sqlcagrp, sqlStatement, isAsync, cancellationToken);
			if (sqldard != null)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitPrpsqlstt:  received sqldard: " + sqldard.ToString());
				}
				if (sqlcagrp == null)
				{
					await base.ProcessSqlCa(sqldard.SqlCa, sqlStatement, isAsync, cancellationToken);
				}
				sqlStatement.ProcessSqldard(sqldard, SqlStatement.SqlState.PRPSQLSTT);
			}
			base.ProcessReplyInfo(sqlStatement, replyInfo, "SqlManager::SubmitPrpsqlstt");
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlManager::SubmitPrpsqlstt");
			}
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x0011FC44 File Offset: 0x0011DE44
		public async Task SubmitDscsqlstt(SqlStatement sqlStatement, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlManager::SubmitDscsqlstt");
			}
			this.ValidateStatement(sqlStatement, SqlStatement.SqlState.DSCSQLSTT);
			sqlStatement.SqlcaCode = 0;
			DSCSQLSTT dscsqlstt = new DSCSQLSTT(null, this._level, this._requester.TypeDefinitionName);
			base.InitializeCodepoint(dscsqlstt);
			dscsqlstt.Pkgnamcsn = sqlStatement.Pkgnamcsn;
			dscsqlstt.Typsqlda = TYPSQLDA.StandardInput;
			dscsqlstt.Cmdsrcid = sqlStatement.CommandSourceId;
			Manager.ReplyInfo replyInfo = null;
			SQLCAGRP sqlcagrp = null;
			SQLDARD sqldard = null;
			Ccsid defaultCcsid = this._requester.ConnectionManager.DdmReader.Ccsid;
			try
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitDscsqlstt:  sending DSCSQLSTT...");
				}
				await dscsqlstt.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 0, isAsync, cancellationToken);
				do
				{
					CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
					}
					CodePoint codePoint = currentCP;
					TYPDEFOVR typedefovr;
					if (codePoint != CodePoint.TYPDEFOVR)
					{
						if (codePoint != CodePoint.SQLCARD)
						{
							if (codePoint != CodePoint.SQLDARD)
							{
								Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
								if (this._tracePoint.IsEnabled(TraceFlags.Warning))
								{
									this._tracePoint.Trace(TraceFlags.Warning, "SqlManager::SubmitDscsqlstt(): Read unexpected CodePoint: " + currentCP.ToString());
								}
								if (replyInfo2 != null)
								{
									replyInfo = replyInfo2;
								}
							}
							else
							{
								sqldard = await this.ReadSqldardAsync(isAsync, cancellationToken);
							}
						}
						else
						{
							sqlcagrp = await this.ReadSqlcagrpAsync(isAsync, cancellationToken);
						}
					}
					else
					{
						typedefovr = new TYPDEFOVR(null, -1);
						await typedefovr.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
						this._requester.ConnectionManager.DdmReader.Ccsid = typedefovr.Ccsid;
					}
					typedefovr = null;
				}
				while (base.NeedReadMoreDdmCodepoint(1));
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "SqlManager::SubmitDscsqlstt: " + ex.ToString());
				}
				if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
				{
					this._requester.ConnectionManager.DdmWriter.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			this._requester.ConnectionManager.DdmReader.Ccsid = defaultCcsid;
			if (this._requester.Flavor != DrdaFlavor.Informix || sqlStatement.StatementType != Parser.StatementType.Call)
			{
				await base.ProcessSqlCa(sqlcagrp, sqlStatement, isAsync, cancellationToken);
			}
			if (sqldard != null)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitDscsqlstt:  received sqldard: " + sqldard.ToString());
				}
				if (sqlcagrp == null)
				{
					await base.ProcessSqlCa(sqldard.SqlCa, sqlStatement, isAsync, cancellationToken);
				}
				sqlStatement.ProcessSqldard(sqldard, SqlStatement.SqlState.DSCSQLSTT);
			}
			base.ProcessReplyInfo(sqlStatement, replyInfo, "SqlManager::SubmitDscsqlstt");
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlManager::SubmitDscsqlstt");
			}
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x0011FCA4 File Offset: 0x0011DEA4
		public async Task ReadLargeLobData(int column, int row, int largeLobCounter, bool decCounter, SqlStatement sqlStatement, bool isAsync, CancellationToken cancellationToken)
		{
			if (sqlStatement.CurrentResultSet.ColumnInfos[column].IsLob && this._requester.Mode[largeLobCounter] == 3)
			{
				await this.SubmitGetnxtchk(sqlStatement, true, this._requester.ProgRef[largeLobCounter], isAsync, cancellationToken);
				await this._requester.ConnectionManager.DdmWriter.FlushAsync(isAsync, cancellationToken);
				CodePoint codePoint = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
				if (codePoint != CodePoint.EXTDTA)
				{
					throw new Exception();
				}
				await sqlStatement.CurrentResultSet.ReadLargeLobDataAsync(row, column, decCounter, true, isAsync, cancellationToken);
				if (base.NeedReadMoreDdmCodepoint(1))
				{
					codePoint = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
					if (codePoint == CodePoint.ENDDTARM || codePoint == CodePoint.ENDQRYRM)
					{
						await this._requester.ConnectionManager.DdmReader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
					}
				}
			}
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x0011FD28 File Offset: 0x0011DF28
		public async Task<int> SubmitOpnqry(SqlStatement sqlStatement, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlManager::SubmitOpnqry");
			}
			Requester.drdaArCounterTelemetry.Increment(DrdArCounterCollection.Processes, DrdaAsProcess.OpenQuery);
			this.ValidateStatement(sqlStatement, SqlStatement.SqlState.OPNQRY);
			sqlStatement.SqlcaCode = 0;
			EndianType endianType = this._requester.ConnectionManager.DdmReader.EndianType;
			this._requester.ConnectionManager.DdmReader.Reset(this._requester.ConnectionManager.Stream);
			this._requester.ConnectionManager.DdmReader.EndianType = endianType;
			OPNQRY opnqry = new OPNQRY(null, this._level, this._requester.TypeDefinitionName);
			base.InitializeCodepoint(opnqry);
			opnqry.AutoFlush = false;
			opnqry.Pkgnamcsn = sqlStatement.Pkgnamcsn;
			opnqry.Cmdsrcid = sqlStatement.CommandSourceId;
			opnqry.Qryblksz = Constants.MaxQueryBlockSize;
			opnqry.Maxblkext = Constants.MaxQueryBlockSize;
			if (this._requester.Flavor == DrdaFlavor.DB2)
			{
				opnqry.Qryclsimp = 3;
			}
			else
			{
				opnqry.Qryclsimp = 1;
			}
			opnqry.Qryblkctl = ((this._requester.HostType == HostType.AS400) ? 9239 : 0);
			Manager.ReplyInfo replyInfo = null;
			SQLCAGRP sqlcagrp = null;
			OPNQRYRM opnqryrm = null;
			Manager.ReplyInfo endqryrm = null;
			bool needParameter = sqlStatement.Parameters != null && sqlStatement.Parameters.Count != 0;
			bool needAutoCommit = sqlStatement.AutoCommit && this._requester.HostType != HostType.AS400;
			int i;
			if (sqlStatement.CurrentResultSet != null && sqlStatement.CurrentResultSet.ColumnInfos != null)
			{
				IColumnInfo[] columnInfos = sqlStatement.CurrentResultSet.ColumnInfos;
				for (i = 0; i < columnInfos.Length; i++)
				{
					if (((SqlColumnInfo)columnInfos[i]).IsLob)
					{
						opnqry.Outovropt = 3;
						break;
					}
				}
			}
			try
			{
				byte b = 0;
				if (needParameter)
				{
					b = 80;
				}
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitOpnqry:  sending OPNQRY...");
				}
				await opnqry.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, b, isAsync, cancellationToken);
				if (needParameter)
				{
					SQLDTA sqldta = this.WriteParameterToSqldta(sqlStatement);
					sqldta.AutoFlush = false;
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitOpnqry:  sending SQLDTA...");
					}
					await this.WriteParameters(sqldta, true, isAsync, cancellationToken);
				}
				await this._requester.ConnectionManager.DdmWriter.FlushAsync(isAsync, cancellationToken);
				Ccsid defaultCcsid = this._requester.ConnectionManager.DdmReader.Ccsid;
				string originalVal = this._requester.TypeDefinitionName;
				for (;;)
				{
					CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
					}
					CodePoint codePoint = currentCP;
					TYPDEFOVR typedefovr;
					if (codePoint <= CodePoint.OPNQRYRM)
					{
						if (codePoint <= CodePoint.TYPDEFOVR)
						{
							if (codePoint != CodePoint.TYPDEFNAM)
							{
								if (codePoint != CodePoint.TYPDEFOVR)
								{
									goto IL_0C55;
								}
								typedefovr = new TYPDEFOVR(null, -1);
								await typedefovr.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
								this._requester.ConnectionManager.DdmReader.Ccsid = typedefovr.Ccsid;
							}
							else
							{
								Requester requester = this._requester;
								requester.TypeDefinitionName = await this._requester.ConnectionManager.DdmReader.ReadStringAsync(isAsync, cancellationToken);
								requester = null;
								if (this._requester.TypeDefinitionName.Length > 255)
								{
									DrdaException.TooBig(CodePoint.TYPDEFNAM);
								}
								if (this._requester.IsDb2Gateway)
								{
									this._requester._typeDefNamOrig = this._requester.TypeDefinitionName;
								}
							}
						}
						else if (codePoint != CodePoint.EXTDTA)
						{
							if (codePoint != CodePoint.MGRLVLOVR)
							{
								if (codePoint != CodePoint.OPNQRYRM)
								{
									goto IL_0C55;
								}
								opnqryrm = new OPNQRYRM(null, this._level);
								base.InitializeCodepoint(opnqryrm);
								await opnqryrm.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
								sqlStatement.ProcessOpnqryrm(opnqryrm);
							}
							else
							{
								await this.ReadMgrlvlovr(isAsync, cancellationToken);
							}
						}
						else
						{
							await sqlStatement.CurrentResultSet.ReadLobDataAsync(isAsync, cancellationToken);
						}
					}
					else if (codePoint <= CodePoint.RDBUPDRM)
					{
						if (codePoint != CodePoint.ENDQRYRM)
						{
							if (codePoint != CodePoint.RDBUPDRM)
							{
								goto IL_0C55;
							}
							await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
							sqlStatement.NeedCommit = true;
						}
						else
						{
							endqryrm = await base.GetReplyDetail(isAsync, cancellationToken);
							sqlStatement.ProcessEndqryrm();
						}
					}
					else if (codePoint != CodePoint.SQLCARD)
					{
						if (codePoint != CodePoint.QRYDSC)
						{
							if (codePoint != CodePoint.QRYDTA)
							{
								goto IL_0C55;
							}
							await this.ReadQrydta(sqlStatement, isAsync, cancellationToken);
						}
						else
						{
							await this.ReadQrydsc(sqlStatement, isAsync, cancellationToken);
						}
					}
					else if (this._requester.ConnectionManager.DdmReader.DssCorrelationID == 1 && sqlcagrp == null)
					{
						sqlcagrp = await this.ReadSqlcagrpAsync(isAsync, cancellationToken);
						if (sqlcagrp != null)
						{
							this._requester.RetDatabaseName = sqlcagrp.DatabaseName;
						}
					}
					else
					{
						await this.ReadSqlcagrpAsync(isAsync, cancellationToken);
					}
					IL_0D09:
					typedefovr = null;
					if (!base.NeedReadMoreDdmCodepoint(1))
					{
						break;
					}
					continue;
					IL_0C55:
					Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Warning))
					{
						this._tracePoint.Trace(TraceFlags.Warning, "SqlManager::SubmitOpnqry(): Read unexpected CodePoint: " + currentCP.ToString());
					}
					if (replyInfo2 != null)
					{
						replyInfo = replyInfo2;
						goto IL_0D09;
					}
					goto IL_0D09;
				}
				if (this._requester.IsDb2Gateway)
				{
					this._requester.ConnectionManager.DdmReader.Ccsid = defaultCcsid;
					this._requester.TypeDefinitionName = originalVal;
				}
				defaultCcsid = null;
				originalVal = null;
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "SqlManager::SubmitOpnqry: " + ex.ToString());
				}
				if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
				{
					this._requester.ConnectionManager.DdmWriter.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			i = await base.ProcessSqlCa(sqlcagrp, sqlStatement, isAsync, cancellationToken);
			int affectRows = i;
			base.ProcessReplyInfo(sqlStatement, replyInfo, "SqlManager::SubmitOpnqry");
			if (endqryrm != null && sqlStatement.StatementType != Parser.StatementType.Call && sqlStatement.StatementType != Parser.StatementType.Static)
			{
				sqlStatement.State = SqlStatement.SqlState.CLSQRY;
			}
			if (needAutoCommit)
			{
				if (this._requester.Flavor == DrdaFlavor.Informix)
				{
					sqlStatement.NeedCommit = true;
				}
				if (sqlStatement.NeedCommit && sqlStatement.CanCommitNow)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this._tracePoint.Trace(TraceFlags.Verbose, "SqlManager::SubmitOpnqry:  sending commit ...");
					}
					await this._requester.LocalTransactionManager.CommitAsync(isAsync, cancellationToken);
					sqlStatement.Committed = true;
				}
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlManager::SubmitOpnqry");
			}
			return affectRows;
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x0011FD88 File Offset: 0x0011DF88
		public async Task SubmitCntqry(SqlStatement sqlStatement, SqlResultSet resultSet, QueryScrollOrientation orientation, long number, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlManager::SubmitCntqry");
			}
			this.ValidateStatement(sqlStatement, resultSet.State, SqlStatement.SqlState.CNTQRY);
			sqlStatement.SqlcaCode = 0;
			EndianType endianType = this._requester.ConnectionManager.DdmReader.EndianType;
			this._requester.ConnectionManager.DdmReader.Reset(this._requester.ConnectionManager.Stream);
			this._requester.ConnectionManager.DdmReader.EndianType = endianType;
			CNTQRY cntqry = null;
			if (resultSet.State == SqlStatement.SqlState.OPNQRY && resultSet.IsCursorScrollable && orientation != QueryScrollOrientation.Before && orientation != QueryScrollOrientation.After)
			{
				cntqry = new CNTQRY(null, this._level, this._requester.TypeDefinitionName);
				cntqry.Pkgnamcsn = ((resultSet.Pkgnamcsn == null) ? sqlStatement.Pkgnamcsn : resultSet.Pkgnamcsn);
				cntqry.Qryblksz = Constants.MaxQueryBlockSize;
				cntqry.Qryinsid = resultSet.QueryInstanceId;
				cntqry.Cmdsrcid = sqlStatement.CommandSourceId;
				cntqry.Qryrowset = 32767;
				cntqry.Qryscrorn = 4;
				cntqry.AutoFlush = false;
			}
			CNTQRY cntqry2 = new CNTQRY(null, this._level, this._requester.TypeDefinitionName);
			cntqry2.Pkgnamcsn = ((resultSet.Pkgnamcsn == null) ? sqlStatement.Pkgnamcsn : resultSet.Pkgnamcsn);
			cntqry2.Qryblksz = Constants.MaxQueryBlockSize;
			cntqry2.Qryinsid = resultSet.QueryInstanceId;
			cntqry2.Cmdsrcid = sqlStatement.CommandSourceId;
			cntqry2.AutoFlush = false;
			cntqry2.Rtnextdta = 2;
			switch (orientation)
			{
			case QueryScrollOrientation.Relative:
			case QueryScrollOrientation.Absolute:
				cntqry2.Qryrownbr = number;
				cntqry2.Qryscrorn = (int)orientation;
				break;
			case QueryScrollOrientation.After:
			case QueryScrollOrientation.Before:
			case QueryScrollOrientation.Prior:
			case QueryScrollOrientation.First:
			case QueryScrollOrientation.Last:
				cntqry2.Qryscrorn = (int)orientation;
				break;
			}
			Manager.ReplyInfo replyInfo = null;
			SQLCAGRP sqlcagrp = null;
			Manager.ReplyInfo endqryrm = null;
			try
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitCntqry:  sending CNTQRY...");
				}
				int correlationId = 1;
				bool readCntqry1Response = false;
				if (cntqry != null)
				{
					await cntqry.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 64, isAsync, cancellationToken);
					correlationId = 2;
				}
				await cntqry2.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, correlationId, 0, isAsync, cancellationToken);
				await this._requester.ConnectionManager.DdmWriter.FlushAsync(isAsync, cancellationToken);
				string originalVal = this._requester.TypeDefinitionName;
				if (this._requester.IsDb2Gateway)
				{
					this._requester.TypeDefinitionName = this._requester._typeDefNamOrig;
				}
				for (;;)
				{
					CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
					}
					CodePoint codePoint = currentCP;
					TYPDEFOVR typedefovr;
					if (codePoint <= CodePoint.EXTDTA)
					{
						if (codePoint != CodePoint.TYPDEFOVR)
						{
							if (codePoint != CodePoint.EXTDTA)
							{
								goto IL_095A;
							}
							await resultSet.ReadLobDataAsync(isAsync, cancellationToken);
						}
						else
						{
							typedefovr = new TYPDEFOVR(null, -1);
							await typedefovr.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
							this._requester.ConnectionManager.DdmReader.Ccsid = typedefovr.Ccsid;
						}
					}
					else if (codePoint != CodePoint.ENDQRYRM)
					{
						if (codePoint != CodePoint.SQLCARD)
						{
							if (codePoint != CodePoint.QRYDTA)
							{
								goto IL_095A;
							}
							if (cntqry != null && !readCntqry1Response)
							{
								await this._requester.ConnectionManager.DdmReader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
								readCntqry1Response = true;
							}
							else
							{
								await this.ReadQrydta(sqlStatement, resultSet, isAsync, cancellationToken);
							}
						}
						else
						{
							sqlcagrp = await this.ReadSqlcagrpAsync(isAsync, cancellationToken);
						}
					}
					else
					{
						endqryrm = await base.GetReplyDetail(isAsync, cancellationToken);
						sqlStatement.ProcessEndqryrm(resultSet);
					}
					IL_0A0E:
					typedefovr = null;
					if (!base.NeedReadMoreDdmCodepoint(correlationId))
					{
						break;
					}
					continue;
					IL_095A:
					Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Warning))
					{
						this._tracePoint.Trace(TraceFlags.Warning, "SqlManager::SubmitCntqry(): Read unexpected CodePoint: " + currentCP.ToString());
					}
					if (replyInfo2 != null)
					{
						replyInfo = replyInfo2;
						goto IL_0A0E;
					}
					goto IL_0A0E;
				}
				if (this._requester.IsDb2Gateway)
				{
					this._requester.TypeDefinitionName = originalVal;
				}
				originalVal = null;
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "SqlManager::SubmitCntqry: " + ex.ToString());
				}
				if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
				{
					this._requester.ConnectionManager.DdmWriter.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			await base.ProcessSqlCa(sqlcagrp, sqlStatement, isAsync, cancellationToken);
			base.ProcessReplyInfo(sqlStatement, replyInfo, "SqlManager::SubmitCntqry");
			if (endqryrm != null)
			{
				if (sqlStatement.StatementType != Parser.StatementType.Call && sqlStatement.StatementType != Parser.StatementType.Static)
				{
					sqlStatement.State = SqlStatement.SqlState.CLSQRY;
				}
				resultSet.State = SqlStatement.SqlState.CLSQRY;
				resultSet.EndOfQuery = true;
			}
			if (sqlStatement.NeedCommit && sqlStatement.AutoCommit && this._requester.HostType != HostType.AS400 && sqlStatement.CanCommitNow)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this._tracePoint.Trace(TraceFlags.Verbose, "SqlManager::SubmitCntqry:  sending commit ...");
				}
				await this._requester.LocalTransactionManager.CommitAsync(isAsync, cancellationToken);
				sqlStatement.Committed = true;
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlManager::SubmitCntqry");
			}
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x0011FE00 File Offset: 0x0011E000
		public async Task SubmitClsqry(SqlStatement sqlStatement, SqlResultSet resultSet, bool isQuiet, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlManager::SubmitClsqry");
			}
			if (resultSet == null)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitClsqry: Result set is null.");
				}
			}
			else
			{
				if (!isQuiet)
				{
					if (resultSet == null)
					{
						this.ValidateStatement(sqlStatement, SqlStatement.SqlState.CLSQRY);
					}
					else
					{
						this.ValidateStatement(sqlStatement, resultSet.State, SqlStatement.SqlState.CLSQRY);
					}
				}
				sqlStatement.SqlcaCode = 0;
				CLSQRY clsqry = new CLSQRY(null, this._level, this._requester.TypeDefinitionName);
				if (resultSet != null && resultSet.Pkgnamcsn != null)
				{
					clsqry.Pkgnamcsn = resultSet.Pkgnamcsn;
				}
				else
				{
					clsqry.Pkgnamcsn = sqlStatement.Pkgnamcsn;
				}
				clsqry.Qryinsid = resultSet.QueryInstanceId;
				clsqry.Cmdsrcid = sqlStatement.CommandSourceId;
				Manager.ReplyInfo replyInfo = null;
				SQLCAGRP sqlcagrp = null;
				try
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitClsqry:  sending CNTQRY...");
					}
					await clsqry.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 0, isAsync, cancellationToken);
					do
					{
						CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
						if (this._tracePoint.IsEnabled(TraceFlags.Information))
						{
							this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
						}
						CodePoint codePoint = currentCP;
						if (codePoint != CodePoint.QRYNOPRM)
						{
							if (codePoint == CodePoint.SQLCARD)
							{
								sqlcagrp = await this.ReadSqlcagrpAsync(isAsync, cancellationToken);
							}
							else
							{
								Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
								if (this._tracePoint.IsEnabled(TraceFlags.Warning))
								{
									this._tracePoint.Trace(TraceFlags.Warning, "SqlManager::SubmitClsqry(): Read unexpected CodePoint: " + currentCP.ToString());
								}
								if (replyInfo2 != null)
								{
									replyInfo = replyInfo2;
								}
							}
						}
					}
					while (base.NeedReadMoreDdmCodepoint(1));
				}
				catch (Exception ex)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "SqlManager::SubmitClsqry: " + ex.ToString());
					}
					if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
					{
						this._requester.ConnectionManager.DdmWriter.Reset();
					}
					if (!isQuiet)
					{
						throw this._requester.MakeException(ex.Message, "HY000", -343);
					}
				}
				if (!isQuiet)
				{
					await base.ProcessSqlCa(sqlcagrp, sqlStatement, isAsync, cancellationToken);
					base.ProcessReplyInfo(sqlStatement, replyInfo, "SqlManager::SubmitClsqry");
				}
				if (sqlStatement.StatementType != Parser.StatementType.Call && sqlStatement.StatementType != Parser.StatementType.Static)
				{
					sqlStatement.State = SqlStatement.SqlState.CLSQRY;
				}
				resultSet.State = SqlStatement.SqlState.CLSQRY;
				if (sqlStatement.StatementType == Parser.StatementType.Static)
				{
					EndianType endianType = this._requester.ConnectionManager.DdmReader.EndianType;
					this._requester.ConnectionManager.DdmReader.Reset(this._requester.ConnectionManager.Stream);
					this._requester.ConnectionManager.DdmReader.EndianType = endianType;
				}
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "Exit SqlManager::SubmitClsqry");
				}
			}
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x0011FE70 File Offset: 0x0011E070
		public async Task<int> SubmitExcsqlstt(SqlStatement sqlStatement, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlManager::SubmitExcsqlstt");
			}
			Requester.drdaArCounterTelemetry.Increment(DrdArCounterCollection.Processes, DrdaAsProcess.ExecuteSqlStatement);
			int affectedRows = -1;
			this.ValidateStatement(sqlStatement, SqlStatement.SqlState.EXCSQLSTT);
			sqlStatement.SqlcaCode = 0;
			EndianType endianType = this._requester.ConnectionManager.DdmReader.EndianType;
			this._requester.ConnectionManager.DdmReader.Reset(this._requester.ConnectionManager.Stream);
			this._requester.ConnectionManager.DdmReader.EndianType = endianType;
			bool needParameter = false;
			bool flag = false;
			if (sqlStatement.Parameters != null && sqlStatement.Parameters.Count > 0)
			{
				if (sqlStatement.StatementType != Parser.StatementType.Static)
				{
					needParameter = true;
				}
				foreach (ISqlParameter sqlParameter in sqlStatement.Parameters)
				{
					if (sqlParameter.Direction == ParameterDirection.Output)
					{
						flag = true;
					}
					else
					{
						if (sqlParameter.Direction == ParameterDirection.InputOutput)
						{
							flag = true;
						}
						needParameter = true;
					}
					if (needParameter && flag)
					{
						break;
					}
				}
			}
			EXCSQLSTT excsqlstt = new EXCSQLSTT(null, this._level, this._requester.TypeDefinitionName);
			base.InitializeCodepoint(excsqlstt);
			excsqlstt.AutoFlush = false;
			excsqlstt.Pkgnamcsn = sqlStatement.Pkgnamcsn;
			excsqlstt.Cmdsrcid = sqlStatement.CommandSourceId;
			excsqlstt.Rdbcmtok = this.IsRdbCmtOk(sqlStatement);
			if (this._level < 9)
			{
				if (sqlStatement.StatementType == Parser.StatementType.Call)
				{
					excsqlstt.Outexp = needParameter;
				}
				else if (sqlStatement.StatementType == Parser.StatementType.Static)
				{
					excsqlstt.Outexp = flag;
				}
				else
				{
					excsqlstt.Outexp = false;
				}
			}
			else
			{
				excsqlstt.Outexp = flag;
			}
			excsqlstt.Rslsetflg = Constants.DefaultRslsetflg;
			excsqlstt.Maxrslcnt = 65535;
			excsqlstt.Qryblksz = Constants.MaxQueryBlockSize;
			if (!sqlStatement.IsPrepared)
			{
				excsqlstt.Prcnam = sqlStatement.ProcedureName;
			}
			excsqlstt.Encoding = (this._requester.IsUnicodeMgrSupported ? 1208 : 500);
			excsqlstt.Nbrrow = -1;
			excsqlstt.Outovropt = 3;
			Manager.ReplyInfo replyInfo = null;
			SQLCAGRP sqlcagrp = null;
			OPNQRYRM opnqryrm = null;
			SQLCINRD sqlcinrd = null;
			List<PKGNAMCSN> pkgsnList = null;
			bool needAutoCommit = sqlStatement.AutoCommit && this._requester.HostType != HostType.AS400;
			bool receivedResultSet = false;
			try
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitExcsqlstt:  sending EXCSQLSTT...");
				}
				byte b = 0;
				if (needParameter)
				{
					b = 80;
				}
				await excsqlstt.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, b, isAsync, cancellationToken);
				if (needParameter)
				{
					SQLDTA sqldta = this.WriteParameterToSqldta(sqlStatement);
					sqldta.AutoFlush = false;
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitExcsqlstt:  sending SQLDTA...");
					}
					await this.WriteParameters(sqldta, true, isAsync, cancellationToken);
				}
				await this._requester.ConnectionManager.DdmWriter.FlushAsync(isAsync, cancellationToken);
				Queue<Tuple<ISqlParameter, DrdaParameterInfo>> extdtaWaitingQueue = new Queue<Tuple<ISqlParameter, DrdaParameterInfo>>();
				Ccsid defaultCcsid = this._requester.ConnectionManager.DdmReader.Ccsid;
				string originalVal = this._requester.TypeDefinitionName;
				for (;;)
				{
					CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
					}
					CodePoint codePoint = currentCP;
					TYPDEFOVR typedefovr;
					SQLDTARD sqldtard;
					if (codePoint <= CodePoint.RDBUPDRM)
					{
						if (codePoint <= CodePoint.EXTDTA)
						{
							if (codePoint != CodePoint.TYPDEFNAM)
							{
								if (codePoint != CodePoint.TYPDEFOVR)
								{
									if (codePoint != CodePoint.EXTDTA)
									{
										goto IL_0FC6;
									}
									if (extdtaWaitingQueue.Count == 0)
									{
										await sqlStatement.CurrentResultSet.ReadLobDataAsync(isAsync, cancellationToken);
									}
									else
									{
										Tuple<ISqlParameter, DrdaParameterInfo> tuple = extdtaWaitingQueue.Dequeue();
										EXTDTA extdta = await this._requester.SqlManager.ReadExtdta(SqlType.DrdaTypeToSqlTypeMappings[(int)tuple.Item2.Type], Convert.ToUInt16((int)((tuple.Item2.CCSID < 0) ? 0 : tuple.Item2.CCSID)), tuple.Item2.LobLength, isAsync, cancellationToken);
										tuple.Item1.Value = extdta.Value;
										tuple = null;
									}
								}
								else
								{
									typedefovr = new TYPDEFOVR(null, -1);
									await typedefovr.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
									this._requester.ConnectionManager.DdmReader.Ccsid = typedefovr.Ccsid;
								}
							}
							else
							{
								Requester requester = this._requester;
								requester.TypeDefinitionName = await this._requester.ConnectionManager.DdmReader.ReadStringAsync(isAsync, cancellationToken);
								requester = null;
								if (this._requester.TypeDefinitionName.Length > 255)
								{
									DrdaException.TooBig(CodePoint.TYPDEFNAM);
								}
								if (this._requester.IsDb2Gateway)
								{
									this._requester._typeDefNamOrig = this._requester.TypeDefinitionName;
								}
							}
						}
						else if (codePoint != CodePoint.OPNQRYRM)
						{
							if (codePoint != CodePoint.ENDQRYRM)
							{
								if (codePoint != CodePoint.RDBUPDRM)
								{
									goto IL_0FC6;
								}
								await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
								sqlStatement.NeedCommit = true;
							}
							else
							{
								await base.GetReplyDetail(isAsync, cancellationToken);
								sqlStatement.ProcessEndqryrm();
							}
						}
						else
						{
							opnqryrm = new OPNQRYRM(null, this._level);
							base.InitializeCodepoint(opnqryrm);
							await opnqryrm.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
							sqlStatement.CreateNewCurrentResultSet();
							sqlStatement.ProcessOpnqryrm(opnqryrm);
							receivedResultSet = true;
						}
					}
					else if (codePoint <= CodePoint.SQLCINRD)
					{
						if (codePoint != CodePoint.RSLSETRM)
						{
							if (codePoint != CodePoint.SQLCARD)
							{
								if (codePoint != CodePoint.SQLCINRD)
								{
									goto IL_0FC6;
								}
								sqlcinrd = new SQLCINRD(null, this._level, this._requester.TypeDefinitionName);
								base.InitializeCodepoint(sqlcinrd);
								await sqlcinrd.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, this._requester.UseAccelerator, cancellationToken);
								sqlStatement.ProcessSqldard(sqlcinrd, SqlStatement.SqlState.PRPSQLSTT);
							}
							else
							{
								sqlcagrp = await this.ReadSqlcagrpAsync(isAsync, cancellationToken);
							}
						}
						else
						{
							pkgsnList = await this.ReadRslsetrmAsync(sqlStatement, isAsync, cancellationToken);
							sqlStatement.ClearResultSets();
						}
					}
					else if (codePoint != CodePoint.SQLDTARD)
					{
						if (codePoint != CodePoint.QRYDSC)
						{
							if (codePoint != CodePoint.QRYDTA)
							{
								goto IL_0FC6;
							}
							await this.ReadQrydta(sqlStatement, isAsync, cancellationToken);
							receivedResultSet = true;
						}
						else
						{
							await this.ReadQrydsc(sqlStatement, isAsync, cancellationToken);
						}
					}
					else
					{
						sqldtard = null;
						if (this._requester.IsDb2Gateway)
						{
							sqldtard = new SQLDTARD(null, this._requester.ConnectionManager.DdmReader.Ccsid);
						}
						else
						{
							sqldtard = new SQLDTARD(null, this._requester.CcsidRead);
						}
						base.InitializeCodepoint(sqldtard);
						sqldtard.TypeDefName = this._requester.TypeDefinitionName;
						sqldtard.SetSqlAmLevel(this._level);
						sqldtard.CcsidConvert = new Func<ushort, ushort>(Utility.MapCcsidCodeToCodePage);
						await sqldtard.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
						this.UpdateStatementParameters(sqlStatement, sqldtard, extdtaWaitingQueue);
					}
					IL_107A:
					typedefovr = null;
					sqldtard = null;
					if (!base.NeedReadMoreDdmCodepoint(1))
					{
						break;
					}
					continue;
					IL_0FC6:
					Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Warning))
					{
						this._tracePoint.Trace(TraceFlags.Warning, "SqlManager::SubmitExcsqlstt(): Read unexpected CodePoint: " + currentCP.ToString());
					}
					if (replyInfo2 != null)
					{
						replyInfo = replyInfo2;
						goto IL_107A;
					}
					goto IL_107A;
				}
				if (this._requester.IsDb2Gateway)
				{
					this._requester.ConnectionManager.DdmReader.Ccsid = defaultCcsid;
					this._requester.TypeDefinitionName = originalVal;
				}
				extdtaWaitingQueue = null;
				defaultCcsid = null;
				originalVal = null;
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "SqlManager::SubmitExcsqlstt: " + ex.ToString());
				}
				if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
				{
					this._requester.ConnectionManager.DdmWriter.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			affectedRows = await base.ProcessSqlCa(sqlcagrp, sqlStatement, isAsync, cancellationToken);
			base.ProcessReplyInfo(sqlStatement, replyInfo, "SqlManager::SubmitExcsqlstt");
			if (pkgsnList != null)
			{
				sqlStatement.UpdateResultSetsWithPackageList(pkgsnList);
			}
			if (needAutoCommit)
			{
				if (this._requester.Flavor == DrdaFlavor.Informix)
				{
					sqlStatement.NeedCommit = true;
				}
				if (sqlStatement.NeedCommit && sqlStatement.CanCommitNow)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this._tracePoint.Trace(TraceFlags.Verbose, "SqlManager::SubmitExcsqlstt:  sending commit ...");
					}
					await this._requester.LocalTransactionManager.CommitAsync(isAsync, cancellationToken);
					sqlStatement.Committed = true;
				}
			}
			if (!receivedResultSet)
			{
				sqlStatement.ClearResultSets();
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlManager::SubmitExcsqlstt");
			}
			return affectedRows;
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x0011FED0 File Offset: 0x0011E0D0
		public async Task<int> SubmitExcsqlimm(SqlStatement sqlStatement, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlManager::SubmitExcsqlimm");
			}
			Requester.drdaArCounterTelemetry.Increment(DrdArCounterCollection.Processes, DrdaAsProcess.ExecuteSqlStatementImmediate);
			this.ValidateStatement(sqlStatement, SqlStatement.SqlState.EXCSQLIMM);
			sqlStatement.SqlcaCode = 0;
			EXCSQLIMM excsqlimm = new EXCSQLIMM(null, this._level, this._requester.TypeDefinitionName);
			base.InitializeCodepoint(excsqlimm);
			excsqlimm.AutoFlush = false;
			excsqlimm.Pkgnamcsn = sqlStatement.Pkgnamcsn;
			excsqlimm.Cmdsrcid = sqlStatement.CommandSourceId;
			Manager.ReplyInfo replyInfo = null;
			SQLCAGRP sqlcagrp = null;
			bool needAutoCommit = sqlStatement.AutoCommit && this._requester.HostType != HostType.AS400;
			try
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitExcsqlimm:  sending EXCSQLIMM...");
				}
				await excsqlimm.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 80, isAsync, cancellationToken);
				SQLSTT sqlstt = new SQLSTT(null, this._level, this._requester.TypeDefinitionName);
				base.InitializeCodepoint(sqlstt);
				sqlstt.AutoFlush = false;
				sqlstt.Sqlstt = sqlStatement.CommandText;
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitExcsqlimm:  sending SQLSTT...");
				}
				if (needAutoCommit)
				{
					await sqlstt.WriteObjectDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 64, isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this._tracePoint.Trace(TraceFlags.Verbose, "SqlManager::SubmitExcsqlimm:  sending RDBCMM...");
					}
					await base.WriteDDMCodepoint(CodePoint.RDBCMM, DBNull.Value, DssType.Request, 2, 0, false, isAsync, cancellationToken);
					sqlStatement.Committed = true;
				}
				else
				{
					await sqlstt.WriteObjectDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 0, isAsync, cancellationToken);
				}
				await this._requester.ConnectionManager.DdmWriter.FlushAsync(isAsync, cancellationToken);
				do
				{
					CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
					}
					if (currentCP == CodePoint.SQLCARD)
					{
						if (this._requester.ConnectionManager.DdmReader.DssCorrelationID == 1)
						{
							sqlcagrp = await this.ReadSqlcagrpAsync(isAsync, cancellationToken);
						}
						else
						{
							await this.ReadSqlcagrpAsync(isAsync, cancellationToken);
						}
					}
					else
					{
						Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
						if (this._tracePoint.IsEnabled(TraceFlags.Warning))
						{
							this._tracePoint.Trace(TraceFlags.Warning, "SqlManager::SubmitExcsqlimm(): Read unexpected CodePoint: " + currentCP.ToString());
						}
						if (replyInfo2 != null)
						{
							replyInfo = replyInfo2;
						}
					}
				}
				while (base.NeedReadMoreDdmCodepoint(needAutoCommit ? 2 : 1));
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "SqlManager::SubmitExcsqlimm: " + ex.ToString());
				}
				if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
				{
					this._requester.ConnectionManager.DdmWriter.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			int num = await base.ProcessSqlCa(sqlcagrp, sqlStatement, isAsync, cancellationToken);
			base.ProcessReplyInfo(sqlStatement, replyInfo, "SqlManager::SubmitExcsqlimm");
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlManager::SubmitExcsqlimm");
			}
			return num;
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x0011FF30 File Offset: 0x0011E130
		internal async Task<int> InsertRowsAsync(SqlStatement sqlStatement, List<object[]> rows, bool isAsync, CancellationToken cancellationToken)
		{
			SqlManager.<>c__DisplayClass31_0 CS$<>8__locals1 = new SqlManager.<>c__DisplayClass31_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.isAsync = isAsync;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlManager::InsertRowsAsync");
			}
			EXCSQLSTT excsqlstt = new EXCSQLSTT(null, this._level, this._requester.TypeDefinitionName);
			base.InitializeCodepoint(excsqlstt);
			excsqlstt.AutoFlush = false;
			excsqlstt.Pkgnamcsn = sqlStatement.Pkgnamcsn;
			excsqlstt.Cmdsrcid = sqlStatement.CommandSourceId;
			excsqlstt.Rdbcmtok = this.IsRdbCmtOk(sqlStatement);
			excsqlstt.Outexp = false;
			excsqlstt.Rslsetflg = null;
			excsqlstt.Maxrslcnt = -1;
			excsqlstt.Qryblksz = -1;
			excsqlstt.Prcnam = null;
			excsqlstt.Nbrrow = rows.Count<object[]>();
			excsqlstt.Encoding = (this._requester.IsUnicodeMgrSupported ? 1208 : 500);
			Manager.ReplyInfo replyInfo = null;
			SQLCAGRP sqlcagrp = null;
			try
			{
				CS$<>8__locals1.sqldta = new SQLDTA(null, this._requester.CcsidWrite);
				base.InitializeCodepoint(CS$<>8__locals1.sqldta);
				CS$<>8__locals1.sqldta.AutoFlush = false;
				CS$<>8__locals1.sqldta.TypeDefName = this._requester.TypeDefinitionName;
				CS$<>8__locals1.extdtaCount = 0;
				CS$<>8__locals1.sqldta.Parms.Clear();
				for (int i = 0; i < sqlStatement.ParameterInfoList.Count; i++)
				{
					SqlParameter sqlParameter = (SqlParameter)sqlStatement.ParameterInfoList[i];
					DrdaParameterInfo drdaParameterInfo = new DrdaParameterInfo(sqlParameter.DrdaServerType, 1, (ushort)sqlParameter.Size, (ushort)sqlParameter.Precision, (ushort)sqlParameter.Scale, null);
					drdaParameterInfo.SqlType = sqlParameter.SqlType;
					this.UpdateDrdaParameterInfoCcsidFromSqlParameter(drdaParameterInfo, sqlParameter);
					CS$<>8__locals1.sqldta.Parms.Add(drdaParameterInfo);
					if (SqlType.IsLob(drdaParameterInfo.SqlType))
					{
						foreach (object[] array in rows)
						{
							drdaParameterInfo.Value = array[i];
							if (this.NeedExtdta(drdaParameterInfo))
							{
								int num = CS$<>8__locals1.extdtaCount + 1;
								CS$<>8__locals1.extdtaCount = num;
							}
						}
					}
				}
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::InsertRowsAsync:  sending EXCSQLSTT...");
				}
				await excsqlstt.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 80, CS$<>8__locals1.isAsync, CS$<>8__locals1.cancellationToken);
				CS$<>8__locals1.chainState = 0;
				Func<object[], bool, Task> writeRowExtdtaAsync = delegate(object[] row, bool needCheckChainState)
				{
					SqlManager.<>c__DisplayClass31_0.<<InsertRowsAsync>b__0>d <<InsertRowsAsync>b__0>d;
					<<InsertRowsAsync>b__0>d.<>4__this = CS$<>8__locals1;
					<<InsertRowsAsync>b__0>d.row = row;
					<<InsertRowsAsync>b__0>d.needCheckChainState = needCheckChainState;
					<<InsertRowsAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<InsertRowsAsync>b__0>d.<>1__state = -1;
					AsyncTaskMethodBuilder <>t__builder = <<InsertRowsAsync>b__0>d.<>t__builder;
					<>t__builder.Start<SqlManager.<>c__DisplayClass31_0.<<InsertRowsAsync>b__0>d>(ref <<InsertRowsAsync>b__0>d);
					return <<InsertRowsAsync>b__0>d.<>t__builder.Task;
				};
				if (CS$<>8__locals1.extdtaCount > 0 && (this._requester.Flavor == DrdaFlavor.Informix || this._requester.IsUdb))
				{
					int num;
					for (int rowIndex = 0; rowIndex < rows.Count; rowIndex = num + 1)
					{
						object[] row2 = rows[rowIndex];
						for (int j = 0; j < CS$<>8__locals1.sqldta.Parms.Count; j++)
						{
							CS$<>8__locals1.sqldta.Parms[j].Value = row2[j];
						}
						if (rowIndex < rows.Count - 1 || CS$<>8__locals1.extdtaCount > 0)
						{
							CS$<>8__locals1.chainState = 80;
						}
						else
						{
							CS$<>8__locals1.chainState = 0;
						}
						if (this._tracePoint.IsEnabled(TraceFlags.Information))
						{
							this._tracePoint.Trace(TraceFlags.Information, "SqlManager::InsertRowsAsync:  sending SQLDTA for Informix ...");
						}
						await CS$<>8__locals1.sqldta.WriteObjectDssAsync(this._requester.ConnectionManager.DdmWriter, 1, CS$<>8__locals1.chainState, CS$<>8__locals1.isAsync, CS$<>8__locals1.cancellationToken);
						if (CS$<>8__locals1.extdtaCount > 0)
						{
							await writeRowExtdtaAsync(row2, rowIndex == rows.Count - 1);
						}
						row2 = null;
						num = rowIndex;
					}
				}
				else
				{
					CS$<>8__locals1.sqldta.IsInsertMultipleRows = true;
					CS$<>8__locals1.sqldta.Rows = rows;
					if (this._requester.HostType != HostType.MVS && this._requester.HostType != HostType.DB2)
					{
						CS$<>8__locals1.sqldta.RowMode = SQLDTA.BatchMode.RowWide;
					}
					else
					{
						CS$<>8__locals1.sqldta.RowMode = SQLDTA.BatchMode.ColumnWide;
					}
					if (CS$<>8__locals1.extdtaCount > 0)
					{
						CS$<>8__locals1.chainState = 80;
					}
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "SqlManager::InsertRowsAsync:  sending SQLDTA...");
					}
					await CS$<>8__locals1.sqldta.WriteObjectDssAsync(this._requester.ConnectionManager.DdmWriter, 1, CS$<>8__locals1.chainState, CS$<>8__locals1.isAsync, CS$<>8__locals1.cancellationToken);
					foreach (object[] array2 in rows)
					{
						await writeRowExtdtaAsync(array2, true);
						if (CS$<>8__locals1.extdtaCount == 0)
						{
							break;
						}
					}
					List<object[]>.Enumerator enumerator2 = default(List<object[]>.Enumerator);
				}
				await this._requester.ConnectionManager.DdmWriter.FlushAsync(CS$<>8__locals1.isAsync, CS$<>8__locals1.cancellationToken);
				do
				{
					CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(CS$<>8__locals1.isAsync, CS$<>8__locals1.cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
					}
					if (currentCP == CodePoint.SQLCARD)
					{
						sqlcagrp = await this.ReadSqlcagrpAsync(CS$<>8__locals1.isAsync, CS$<>8__locals1.cancellationToken);
					}
					else
					{
						Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, CS$<>8__locals1.isAsync, CS$<>8__locals1.cancellationToken);
						if (this._tracePoint.IsEnabled(TraceFlags.Warning))
						{
							this._tracePoint.Trace(TraceFlags.Warning, "SqlManager::InsertRowsAsync(): Read unexpected CodePoint: " + currentCP.ToString());
						}
						if (replyInfo2 != null)
						{
							replyInfo = replyInfo2;
						}
					}
				}
				while (base.NeedReadMoreDdmCodepoint(1));
				writeRowExtdtaAsync = null;
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "SqlManager::InsertRowsAsync: " + ex.ToString());
				}
				if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
				{
					this._requester.ConnectionManager.DdmWriter.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			int num2 = await base.ProcessSqlCa(sqlcagrp, sqlStatement, CS$<>8__locals1.isAsync, CS$<>8__locals1.cancellationToken);
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Processed rows: " + num2.ToString());
			}
			base.ProcessReplyInfo(sqlStatement, replyInfo, "SqlManager::InsertRowsAsync");
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlManager::InsertRowsAsync");
			}
			return num2;
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x0011FF98 File Offset: 0x0011E198
		public async Task SubmitIntrdbrqs(byte[] interruptToken, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlManager::SubmitIntrdbrqs");
			}
			Manager.ReplyInfo replyInfo = null;
			try
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitIntrdbrqs:  sending INTRDBRQS...");
				}
				this._requester.ConnectionManager.DdmWriter.CreateDssRequest(1);
				this._requester.ConnectionManager.DdmWriter.WriteBeginDdm(CodePoint.INTRDBRQS);
				this._requester.ConnectionManager.DdmWriter.WriteScalarBytes(CodePoint.RDBINTTKN, interruptToken);
				this._requester.ConnectionManager.DdmWriter.WriteEndDdm();
				this._requester.ConnectionManager.DdmWriter.WriteEndDss();
				await this._requester.ConnectionManager.DdmWriter.WriteEndChainAsync(0, isAsync, cancellationToken);
				do
				{
					CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
					}
					Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Warning))
					{
						this._tracePoint.Trace(TraceFlags.Warning, "SqlManager::SubmitIntrdbrqs(): Read unexpected CodePoint: " + currentCP.ToString());
					}
					if (replyInfo2 != null)
					{
						replyInfo = replyInfo2;
					}
				}
				while (base.NeedReadMoreDdmCodepoint(1));
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "SqlManager::SubmitIntrdbrqs: " + ex.ToString());
				}
				if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
				{
					this._requester.ConnectionManager.DdmWriter.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			base.ProcessReplyInfo(null, replyInfo, "SqlManager::SubmitIntrdbrqs");
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlManager::SubmitExcsqlimm");
			}
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x0011FFF8 File Offset: 0x0011E1F8
		public async Task SubmitGetnxtchk(SqlStatement sqlStatement, bool reset, byte[] progRef, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlManager::SubmitGetnxtchk");
			}
			if (sqlStatement == null)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitGetnxtchk: Result set is null.");
				}
			}
			else
			{
				try
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "SqlManager::SubmitGetnxtchk:  sending GETNXTCHK...");
					}
					this._requester.ConnectionManager.DdmWriter.CreateDssRequest(1);
					this._requester.ConnectionManager.DdmWriter.WriteBeginDdm(CodePoint.GETNXTCHK);
					this._requester.ConnectionManager.DdmWriter.WriteBeginDdm(CodePoint.PKGNAMCSN);
					sqlStatement.Pkgnamcsn.Write(this._requester.ConnectionManager.DdmWriter);
					this._requester.ConnectionManager.DdmWriter.WriteEndDdm();
					this._requester.ConnectionManager.DdmWriter.WriteBeginDdm(CodePoint.QRYINSID);
					this._requester.ConnectionManager.DdmWriter.WriteInt64(sqlStatement.CurrentResultSet.QueryInstanceId, EndianType.BigEndian);
					this._requester.ConnectionManager.DdmWriter.WriteEndDdm();
					if (this._requester.HostType == HostType.MVS || this._requester.HostType == HostType.DB2)
					{
						this._requester.ConnectionManager.DdmWriter.WriteBeginDdm(CodePoint.CMDSRCID);
						this._requester.ConnectionManager.DdmWriter.WriteInt64(sqlStatement.CommandSourceId, EndianType.BigEndian);
						this._requester.ConnectionManager.DdmWriter.WriteEndDdm();
					}
					this._requester.ConnectionManager.DdmWriter.WriteBeginDdm(CodePoint.GETNXTREF);
					this._requester.ConnectionManager.DdmWriter.WriteBytes(progRef);
					this._requester.ConnectionManager.DdmWriter.WriteEndDdm();
					this._requester.ConnectionManager.DdmWriter.WriteBeginDdm(CodePoint.GETNXTLEN);
					this._requester.ConnectionManager.DdmWriter.WriteInt64((long)Constants.MAX_LOB_SIZE, EndianType.BigEndian);
					this._requester.ConnectionManager.DdmWriter.WriteEndDdm();
					this._requester.ConnectionManager.DdmWriter.WriteEndDdm();
					this._requester.ConnectionManager.DdmWriter.WriteEndDss();
					await this._requester.ConnectionManager.DdmWriter.WriteEndChainAsync(0, isAsync, cancellationToken);
				}
				catch (Exception ex)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "SqlManager::SubmitGetnxtchk: " + ex.ToString());
					}
					if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
					{
						this._requester.ConnectionManager.DdmWriter.Reset();
					}
				}
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "Exit SqlManager::SubmitGetnxtchk");
				}
			}
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x0012005F File Offset: 0x0011E25F
		private void ValidateStatement(SqlStatement sqlStatement, SqlStatement.SqlState newState)
		{
			this.ValidateStatement(sqlStatement, sqlStatement.State, newState);
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x00120070 File Offset: 0x0011E270
		private void ValidateStatement(SqlStatement sqlStatement, SqlStatement.SqlState currentState, SqlStatement.SqlState newState)
		{
			if (currentState != newState)
			{
				bool flag = false;
				switch (currentState)
				{
				case SqlStatement.SqlState.Initialized:
				case SqlStatement.SqlState.EXCSQLSET:
					if (newState == SqlStatement.SqlState.CLSQRY || newState == SqlStatement.SqlState.CNTQRY)
					{
						flag = true;
					}
					break;
				case SqlStatement.SqlState.OPNQRY:
					if (newState != SqlStatement.SqlState.CNTQRY && newState != SqlStatement.SqlState.CLSQRY)
					{
						flag = true;
					}
					break;
				case SqlStatement.SqlState.CNTQRY:
					if (newState != SqlStatement.SqlState.CLSQRY)
					{
						flag = true;
					}
					break;
				case SqlStatement.SqlState.CLSQRY:
					if (newState == SqlStatement.SqlState.CNTQRY)
					{
						flag = true;
					}
					break;
				case SqlStatement.SqlState.PRPSQLSTT:
					if (newState != SqlStatement.SqlState.OPNQRY && newState != SqlStatement.SqlState.EXCSQLSTT && newState != SqlStatement.SqlState.DSCSQLSTT)
					{
						flag = true;
					}
					break;
				case SqlStatement.SqlState.DSCSQLSTT:
					if (newState != SqlStatement.SqlState.OPNQRY && newState != SqlStatement.SqlState.EXCSQLSTT && newState != SqlStatement.SqlState.Initialized)
					{
						flag = true;
					}
					break;
				case SqlStatement.SqlState.EXCSQLSTT:
				case SqlStatement.SqlState.EXCSQLIMM:
					if (sqlStatement.StatementType == Parser.StatementType.Call || sqlStatement.StatementType == Parser.StatementType.Static)
					{
						if (newState == SqlStatement.SqlState.CLSQRY)
						{
							break;
						}
						if (sqlStatement.CurrentResultSet != null)
						{
							this.ValidateStatement(sqlStatement, sqlStatement.CurrentResultSet.State, newState);
							break;
						}
					}
					if (newState != SqlStatement.SqlState.Initialized)
					{
						flag = true;
					}
					break;
				}
				if (flag)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "SqlManager::ValidateStatement(): SqlStatement could not be changed: " + currentState.ToString() + " to " + newState.ToString());
					}
					throw this._requester.MakeException(RequesterResource.SqlStatementStateNoChange, "HY000", -1033);
				}
			}
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x00120194 File Offset: 0x0011E394
		private async Task<SQLDARD> ReadSqldardAsync(bool isAsync, CancellationToken cancellationToken)
		{
			SQLDARD sqldard = new SQLDARD(null, this._level, this._requester.TypeDefinitionName);
			base.InitializeCodepoint(sqldard);
			await sqldard.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
			return sqldard;
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x001201EC File Offset: 0x0011E3EC
		private async Task ReadQrydsc(SqlStatement sqlStatement, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Reading QRYDSC...");
			}
			int ddmLength = (int)this._requester.ConnectionManager.DdmReader.DdmObjectLength;
			Dictionary<byte, FdocscDrdaTypeMapping> typeMap = new Dictionary<byte, FdocscDrdaTypeMapping>();
			List<Tuple<int, byte, ushort>> columnInfoList = new List<Tuple<int, byte, ushort>>();
			while (ddmLength > 0)
			{
				byte b = await this._requester.ConnectionManager.DdmReader.ReadByteAsync(isAsync, cancellationToken);
				int tripletLength = (int)b;
				b = await this._requester.ConnectionManager.DdmReader.ReadByteAsync(isAsync, cancellationToken);
				int tripletType = (int)b;
				int num = tripletType;
				if (num == 118)
				{
					goto IL_01DA;
				}
				if (num != 120)
				{
					if (num == 127)
					{
						goto IL_01DA;
					}
					await this._requester.ConnectionManager.DdmReader.SkipBytesAsync(tripletLength - 2, isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "Skip reading QRYDSC triplet, type: " + tripletType.ToString());
					}
				}
				else
				{
					await this._requester.ConnectionManager.DdmReader.ReadInt16Async(isAsync, cancellationToken);
					b = await this._requester.ConnectionManager.DdmReader.ReadByteAsync(isAsync, cancellationToken);
					int descriptorType = (int)b;
					if (descriptorType == 1)
					{
						b = await this._requester.ConnectionManager.DdmReader.ReadByteAsync(isAsync, cancellationToken);
						byte drdaType = b;
						b = await this._requester.ConnectionManager.DdmReader.ReadByteAsync(isAsync, cancellationToken);
						byte fromDrdaType = b;
						b = await this._requester.ConnectionManager.DdmReader.ReadByteAsync(isAsync, cancellationToken);
						int i = (int)b;
						int num2 = (int)(await this._requester.ConnectionManager.DdmReader.ReadByteAsync(isAsync, cancellationToken));
						if (num2 == 112)
						{
							FdocscDrdaTypeMapping drdaTypeMapping = new FdocscDrdaTypeMapping();
							drdaTypeMapping.ReferencType = drdaType;
							drdaTypeMapping.FromDrdaType = fromDrdaType;
							FdocscDrdaTypeMapping fdocscDrdaTypeMapping = drdaTypeMapping;
							b = await this._requester.ConnectionManager.DdmReader.ReadByteAsync(isAsync, cancellationToken);
							fdocscDrdaTypeMapping.TripletLID = b;
							fdocscDrdaTypeMapping = null;
							fdocscDrdaTypeMapping = drdaTypeMapping;
							b = await this._requester.ConnectionManager.DdmReader.ReadByteAsync(isAsync, cancellationToken);
							fdocscDrdaTypeMapping.ToDrdaType = b;
							fdocscDrdaTypeMapping = null;
							await this._requester.ConnectionManager.DdmReader.ReadInt16Async(isAsync, cancellationToken);
							fdocscDrdaTypeMapping = drdaTypeMapping;
							fdocscDrdaTypeMapping.CCSID = await this._requester.ConnectionManager.DdmReader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
							fdocscDrdaTypeMapping = null;
							fdocscDrdaTypeMapping = drdaTypeMapping;
							b = await this._requester.ConnectionManager.DdmReader.ReadByteAsync(isAsync, cancellationToken);
							fdocscDrdaTypeMapping.CharSize = b;
							fdocscDrdaTypeMapping = null;
							fdocscDrdaTypeMapping = drdaTypeMapping;
							b = await this._requester.ConnectionManager.DdmReader.ReadByteAsync(isAsync, cancellationToken);
							fdocscDrdaTypeMapping.Mode = b;
							fdocscDrdaTypeMapping = null;
							fdocscDrdaTypeMapping = drdaTypeMapping;
							fdocscDrdaTypeMapping.Precision = await this._requester.ConnectionManager.DdmReader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
							fdocscDrdaTypeMapping = null;
							typeMap.Add(drdaTypeMapping.TripletLID, drdaTypeMapping);
							drdaTypeMapping = null;
						}
						else
						{
							if (this._tracePoint.IsEnabled(TraceFlags.Information))
							{
								this._tracePoint.Trace(TraceFlags.Information, "MDD triplet does not have mapping SDA triplet type: " + num2.ToString());
							}
							await this._requester.ConnectionManager.DdmReader.SkipBytesAsync(i - 2, isAsync, cancellationToken);
						}
						tripletLength += i;
					}
					else
					{
						await this._requester.ConnectionManager.DdmReader.SkipBytesAsync(tripletLength - 5, isAsync, cancellationToken);
						if (this._tracePoint.IsEnabled(TraceFlags.Information))
						{
							this._tracePoint.Trace(TraceFlags.Information, "Skip reading QRYDSC triplet MDD_TRIPLET_TYPE, Descriptor Type: " + descriptorType.ToString());
						}
					}
				}
				IL_0DD2:
				ddmLength -= tripletLength;
				continue;
				IL_01DA:
				await this._requester.ConnectionManager.DdmReader.ReadByteAsync(isAsync, cancellationToken);
				int columns = (tripletLength - 3) / 3;
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "Reading QRYDSC N-GDA triplet, columns = " + columns.ToString());
				}
				int startColumnIndex = columnInfoList.Count;
				for (int i = 0; i < columns; i++)
				{
					b = await this._requester.ConnectionManager.DdmReader.ReadByteAsync(isAsync, cancellationToken);
					byte drdaType = b;
					ushort num3 = await this._requester.ConnectionManager.DdmReader.ReadUInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
					columnInfoList.Add(new Tuple<int, byte, ushort>(startColumnIndex + i, drdaType, num3));
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, string.Format("Reading QRYDSC N-GDA triplet, column {0}, DrdaType = {1}, SqlLength = {2}.", startColumnIndex + i, drdaType, num3));
					}
				}
				goto IL_0DD2;
			}
			foreach (Tuple<int, byte, ushort> tuple in columnInfoList)
			{
				byte b2 = tuple.Item2;
				ushort num4 = 0;
				FdocscDrdaTypeMapping fdocscDrdaTypeMapping2 = null;
				if (typeMap.TryGetValue(b2, out fdocscDrdaTypeMapping2))
				{
					b2 = fdocscDrdaTypeMapping2.FromDrdaType;
					num4 = (ushort)fdocscDrdaTypeMapping2.CCSID;
				}
				sqlStatement.SetDrdaInfoToResultSet(columnInfoList.Count, tuple.Item1, b2, tuple.Item3, num4);
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Finished reading QRYDSC.");
			}
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x0012024C File Offset: 0x0011E44C
		private async Task ReadQrydta(SqlStatement sqlStatement, bool isAsync, CancellationToken cancellationToken)
		{
			await this.ReadQrydta(sqlStatement, sqlStatement.CurrentResultSet, isAsync, cancellationToken);
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x001202AC File Offset: 0x0011E4AC
		private async Task ReadMgrlvlovr(bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Reading MGRLVLOVR...");
			}
			for (int ddmLength = (int)this._requester.ConnectionManager.DdmReader.DdmObjectLength; ddmLength > 0; ddmLength -= 4)
			{
				short num = await this._requester.ConnectionManager.DdmReader.ReadInt16Async(isAsync, cancellationToken);
				CodePoint manager = (CodePoint)num;
				short num2 = await this._requester.ConnectionManager.DdmReader.ReadInt16Async(isAsync, cancellationToken);
				if (manager == CodePoint.SQLAM && num2 > 6)
				{
					base.Level = (int)num2;
				}
			}
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x00120304 File Offset: 0x0011E504
		private async Task ReadQrydta(SqlStatement sqlStatement, SqlResultSet resultSet, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Reading QRYDTA...");
			}
			int ddmLength = (int)this._requester.ConnectionManager.DdmReader.DdmObjectLength;
			int startLength = ddmLength;
			if (ddmLength == 0 && this._requester.IsDb2Gateway)
			{
				ddmLength = this._requester.ConnectionManager.DdmReader.InputBufferLength - (this._requester.ConnectionManager.DdmReader.Position + 2);
			}
			int leftOverLength = ((resultSet.LeftoverData == null) ? 0 : resultSet.LeftoverData.Length);
			byte[] array = await this._requester.ConnectionManager.DdmReader.ReadBytesAsync(leftOverLength, ddmLength, isAsync, cancellationToken);
			byte[] qryData = array;
			if (leftOverLength > 0)
			{
				resultSet.PartialRow = false;
				global::System.Buffer.BlockCopy(resultSet.LeftoverData, 0, qryData, 0, resultSet.LeftoverData.Length);
			}
			this._requester.ConnectionManager.DdmReader.SetMemeoryBuffer(qryData);
			ddmLength = qryData.Length;
			resultSet.LeftoverData = null;
			while (ddmLength > 0)
			{
				int rowStartOffset = qryData.Length - ddmLength;
				int sqlcaStartPosition = this._requester.ConnectionManager.DdmReader.Position;
				try
				{
					SQLCAGRP sqlca = await this.ReadSqlcagrpAsync(isAsync, cancellationToken);
					if (sqlca != null)
					{
						await base.ProcessSqlCa(sqlca, sqlStatement, isAsync, cancellationToken);
						if (sqlca.SqlCode == 100)
						{
							resultSet.EndOfQuery = true;
							this._requester.ConnectionManager.DdmReader.SetMemeoryBuffer(null);
							return;
						}
					}
					ddmLength -= this._requester.ConnectionManager.DdmReader.Position - sqlcaStartPosition;
					int num = (int)(await this._requester.ConnectionManager.DdmReader.ReadByteAsync(isAsync, cancellationToken));
					ddmLength--;
					if (num == 255)
					{
						if (this._tracePoint.IsEnabled(TraceFlags.Warning))
						{
							this._tracePoint.Trace(TraceFlags.Warning, "The row is null, skip it...");
						}
					}
					else
					{
						ddmLength -= await resultSet.SetDataRow(isAsync, cancellationToken);
					}
					sqlca = null;
				}
				catch (DrdaException ex)
				{
					if (ex.ErrorCodePoint == ErrorCodePoint.SYNTAXRM && ex.ErrorCode == 11)
					{
						if (rowStartOffset == 0)
						{
							resultSet.PartialRow = true;
						}
						resultSet.LeftoverData = new byte[qryData.Length - sqlcaStartPosition];
						global::System.Buffer.BlockCopy(qryData, sqlcaStartPosition, resultSet.LeftoverData, 0, resultSet.LeftoverData.Length);
						break;
					}
					throw ex;
				}
			}
			this._requester.ConnectionManager.DdmReader.SetMemeoryBuffer(null);
			if (startLength == 0)
			{
				EndianType endianType = this._requester.ConnectionManager.DdmReader.EndianType;
				this._requester.ConnectionManager.DdmReader.Reset(this._requester.ConnectionManager.Stream);
				this._requester.ConnectionManager.DdmReader.EndianType = endianType;
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Finished reading QRYDTA.");
			}
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x0012036C File Offset: 0x0011E56C
		private async Task<List<PKGNAMCSN>> ReadRslsetrmAsync(SqlStatement sqlStatement, bool isAsync, CancellationToken cancellationToken)
		{
			SqlManager.<>c__DisplayClass41_0 CS$<>8__locals1 = new SqlManager.<>c__DisplayClass41_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.isAsync = isAsync;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Reading RSLSETRM...");
			}
			CS$<>8__locals1.pkgList = null;
			await base.ReadDdmCodepoint(CS$<>8__locals1.isAsync, CS$<>8__locals1.cancellationToken, delegate(ObjectInfo ddmObj)
			{
				SqlManager.<>c__DisplayClass41_0.<<ReadRslsetrmAsync>b__0>d <<ReadRslsetrmAsync>b__0>d;
				<<ReadRslsetrmAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<ReadRslsetrmAsync>b__0>d.ddmObj = ddmObj;
				<<ReadRslsetrmAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
				<<ReadRslsetrmAsync>b__0>d.<>1__state = -1;
				AsyncTaskMethodBuilder<bool> <>t__builder = <<ReadRslsetrmAsync>b__0>d.<>t__builder;
				<>t__builder.Start<SqlManager.<>c__DisplayClass41_0.<<ReadRslsetrmAsync>b__0>d>(ref <<ReadRslsetrmAsync>b__0>d);
				return <<ReadRslsetrmAsync>b__0>d.<>t__builder.Task;
			});
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Finished reading RSLSETRM.");
			}
			return CS$<>8__locals1.pkgList;
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x001203C4 File Offset: 0x0011E5C4
		private bool IsRdbCmtOk(SqlStatement sqlStatement)
		{
			Parser.StatementType statementType = sqlStatement.StatementType;
			return statementType - Parser.StatementType.Static <= 1 && this._requester.TransactionManager.IsolationLevel != Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationNC;
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x001203FC File Offset: 0x0011E5FC
		private SQLDTA WriteParameterToSqldta(SqlStatement sqlStatement)
		{
			List<ISqlParameter> parameters = sqlStatement.Parameters;
			List<ISqlParameter> parameterInfoList = sqlStatement.ParameterInfoList;
			SQLDTA sqldta = new SQLDTA(null, this._requester.CcsidWrite);
			base.InitializeCodepoint(sqldta);
			sqldta.TypeDefName = this._requester.TypeDefinitionName;
			sqldta.Parms.Clear();
			for (int i = 0; i < parameters.Count; i++)
			{
				ISqlParameter sqlParameter = parameters[i];
				short num = 4;
				ParameterDirection direction = sqlParameter.Direction;
				if (direction != ParameterDirection.Input)
				{
					if (direction == ParameterDirection.InputOutput)
					{
						num = 2;
					}
				}
				else
				{
					num = 1;
				}
				if (sqlStatement.StatementType == Parser.StatementType.Static && num == 4)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "Skip output parameter for static call at parameter " + i.ToString());
					}
				}
				else
				{
					SqlParameter sqlParameter2;
					if (parameterInfoList != null && parameterInfoList.Count > i)
					{
						sqlParameter2 = (SqlParameter)parameterInfoList[i];
					}
					else
					{
						sqlParameter2 = SqlType.ProcessParameter(sqlParameter, this._requester);
					}
					DrdaParameterInfo drdaParameterInfo = new DrdaParameterInfo(sqlParameter2.DrdaServerType, num, (ushort)sqlParameter2.Size, (ushort)sqlParameter2.Precision, (ushort)sqlParameter2.Scale, sqlParameter.Value);
					drdaParameterInfo.SqlType = sqlParameter2.SqlType;
					this.UpdateDrdaParameterInfoCcsidFromSqlParameter(drdaParameterInfo, sqlParameter2);
					sqldta.Parms.Add(drdaParameterInfo);
				}
			}
			return sqldta;
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x00120550 File Offset: 0x0011E750
		private bool NeedExtdta(DrdaParameterInfo parameterInfo)
		{
			if (SqlType.IsLob(parameterInfo.SqlType) && parameterInfo.InOutType != 4 && parameterInfo.Value != null && parameterInfo.Value != DBNull.Value)
			{
				string text = parameterInfo.Value as string;
				if (text != null)
				{
					if (text.Length > 0)
					{
						return true;
					}
				}
				else
				{
					byte[] array = parameterInfo.Value as byte[];
					if (array == null)
					{
						return true;
					}
					if (array.Length != 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x001205BC File Offset: 0x0011E7BC
		private int GetExtdtaCount(SQLDTA sqldta)
		{
			int count = 0;
			sqldta.Parms.ForEach(delegate(DrdaParameterInfo parameterInfo)
			{
				if (this.NeedExtdta(parameterInfo))
				{
					int num = count + 1;
					count = num;
				}
			});
			return count;
		}

		// Token: 0x06004B14 RID: 19220 RVA: 0x001205FC File Offset: 0x0011E7FC
		private async Task<SQLCAGRP> ReadSqlcagrpAsync(bool isAsync, CancellationToken cancellationToken)
		{
			SQLCAGRP sqlcagrp = new SQLCAGRP(null, this._level);
			base.InitializeCodepoint(sqlcagrp);
			await sqlcagrp.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
			SQLCAGRP sqlcagrp2;
			if (sqlcagrp.IsNull)
			{
				sqlcagrp2 = null;
			}
			else
			{
				sqlcagrp2 = sqlcagrp;
			}
			return sqlcagrp2;
		}

		// Token: 0x06004B15 RID: 19221 RVA: 0x00120654 File Offset: 0x0011E854
		private void UpdateStatementParameters(SqlStatement sqlStatement, SQLDTARD sqldtard, Queue<Tuple<ISqlParameter, DrdaParameterInfo>> extdtaWaitingList)
		{
			extdtaWaitingList.Clear();
			if (sqldtard == null)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < sqlStatement.Parameters.Count; i++)
			{
				ISqlParameter sqlParameter = sqlStatement.Parameters[i];
				if (sqlParameter.Direction == ParameterDirection.InputOutput || sqlParameter.Direction == ParameterDirection.Output)
				{
					if (sqldtard.Parms.Count > num)
					{
						DrdaParameterInfo drdaParameterInfo = sqldtard.Parms[num];
						object obj = drdaParameterInfo.Value;
						if ((obj is DateTime || obj is TimeSpan) && (sqlParameter.DrdaType == DrdaClientType.VarChar || sqlParameter.DrdaType == DrdaClientType.Char || sqlParameter.DrdaType == DrdaClientType.Graphic || sqlParameter.DrdaType == DrdaClientType.VarGraphic || sqlParameter.DrdaType == DrdaClientType.CLOB || sqlParameter.DrdaType == DrdaClientType.DBCLOB || sqlParameter.DrdaType == DrdaClientType.LongVarChar || sqlParameter.DrdaType == DrdaClientType.LongVarGraphic || sqlParameter.DrdaType == DrdaClientType.NChar || sqlParameter.DrdaType == DrdaClientType.NVarChar))
						{
							obj = sqldtard.Parms[num].OriginalDateTimeString;
						}
						else if (drdaParameterInfo.LobPosition == 2)
						{
							if (drdaParameterInfo.LobLength == 0)
							{
								obj = (drdaParameterInfo.IsClob ? string.Empty : null);
							}
							else
							{
								extdtaWaitingList.Enqueue(new Tuple<ISqlParameter, DrdaParameterInfo>(sqlParameter, drdaParameterInfo));
							}
						}
						sqlParameter.Value = obj;
						num++;
					}
					else if (this._tracePoint.IsEnabled(TraceFlags.Warning))
					{
						this._tracePoint.Trace(TraceFlags.Warning, string.Format("Output parameter from server is less than required: {0} / {1}.", i, sqldtard.Parms.Count));
					}
				}
				else if (sqldtard.Parms.Count == sqlStatement.Parameters.Count)
				{
					num++;
				}
			}
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x001207F4 File Offset: 0x0011E9F4
		private async Task WriteParameters(SQLDTA sqldta, bool isChainEnd, bool isAsync, CancellationToken cancellationToken)
		{
			byte b = 0;
			int extdtaCount = this.GetExtdtaCount(sqldta);
			if (extdtaCount > 0)
			{
				b = 80;
			}
			else if (!isChainEnd)
			{
				b = 64;
			}
			await sqldta.WriteObjectDssAsync(this._requester.ConnectionManager.DdmWriter, 1, b, isAsync, cancellationToken);
			int columnIndex = 0;
			while (extdtaCount > 0 && columnIndex < sqldta.Parms.Count)
			{
				if (extdtaCount > 1)
				{
					b = 80;
				}
				else if (isChainEnd)
				{
					b = 0;
				}
				else
				{
					b = 64;
				}
				DrdaParameterInfo drdaParameterInfo = sqldta.Parms[columnIndex];
				if (this.NeedExtdta(drdaParameterInfo))
				{
					extdtaCount--;
					EXTDTA extdta = ((!SqlType.IsClob(drdaParameterInfo.SqlType)) ? new EXTDTA(columnIndex, drdaParameterInfo.Value as byte[]) : new EXTDTA(columnIndex, drdaParameterInfo.Value as string));
					extdta.AutoFlush = false;
					await extdta.WriteObjectDssAsync(this._requester.ConnectionManager.DdmWriter, 1, b, isAsync, cancellationToken);
				}
				columnIndex++;
			}
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x0012085C File Offset: 0x0011EA5C
		private async Task WriteSqlAttr(string sqlAttr, bool isAsync, CancellationToken cancellationToken)
		{
			this._requester.ConnectionManager.DdmWriter.CreateDssObject(1);
			this._requester.ConnectionManager.DdmWriter.WriteBeginDdm(CodePoint.SQLATTR);
			this._requester.ConnectionManager.DdmWriter.WriteByte(0);
			this._requester.ConnectionManager.DdmWriter.WriteInt32(sqlAttr.Length, EndianType.BigEndian);
			this._requester.ConnectionManager.DdmWriter.WriteStringSBCS(sqlAttr);
			this._requester.ConnectionManager.DdmWriter.WriteByte(255);
			this._requester.ConnectionManager.DdmWriter.WriteEndDdm();
			this._requester.ConnectionManager.DdmWriter.WriteEndDss();
			await this._requester.ConnectionManager.DdmWriter.WriteEndChainAsync(80, false, isAsync, cancellationToken);
		}

		// Token: 0x06004B18 RID: 19224 RVA: 0x001208B9 File Offset: 0x0011EAB9
		private void UpdateDrdaParameterInfoCcsidFromSqlParameter(DrdaParameterInfo parameterInfo, SqlParameter sqlParameter)
		{
			if (sqlParameter.Ccsid > 0 && this._requester.HostType != HostType.DB2 && this._requester.HostType != HostType.MVS)
			{
				parameterInfo.MDDoverride = true;
				parameterInfo.CCSID = (short)Utility.MapCcsidCodeToCodePage(sqlParameter.Ccsid);
			}
		}

		// Token: 0x06004B19 RID: 19225 RVA: 0x001208FC File Offset: 0x0011EAFC
		public async Task<EXTDTA> ReadExtdta(short sqlType, ushort ccsid, int dataLength, bool isAsync, CancellationToken cancellationToken)
		{
			EXTDTA extdta = new EXTDTA();
			extdta.IsBlob = !SqlType.IsClob(sqlType);
			extdta.Encoding = ccsid;
			if (!extdta.IsBlob)
			{
				if (ccsid <= 0 && (sqlType == 988 || sqlType == 989))
				{
					extdta.Encoding = (ushort)this._requester.ConnectionManager.DdmReader.Ccsid._ccsidxml;
				}
				if (extdta.Encoding <= 0)
				{
					extdta.Encoding = (ushort)this._requester.ConnectionManager.DdmReader.Ccsid._ccsidsbc;
				}
			}
			extdta.Length = dataLength;
			extdta.IsNullable = sqlType % 2 == 1;
			if (sqlType == 413 || sqlType == 412)
			{
				extdta.IsGraphic = true;
			}
			await extdta.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
			return extdta;
		}

		// Token: 0x040039C5 RID: 14789
		private static string productId = "MSDRDAAR";

		// Token: 0x040039C6 RID: 14790
		private bool autoBinaryCodepage;

		// Token: 0x040039C7 RID: 14791
		private const string DefaultBinaryCodepage = "1";
	}
}
