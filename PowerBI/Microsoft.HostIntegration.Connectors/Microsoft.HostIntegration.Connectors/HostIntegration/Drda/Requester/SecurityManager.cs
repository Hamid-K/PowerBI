using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EnterpriseSingleSignOn.Interop;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.DDM;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.DrdaClient;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x0200095F RID: 2399
	internal class SecurityManager : Manager
	{
		// Token: 0x06004AD7 RID: 19159 RVA: 0x0011E0B4 File Offset: 0x0011C2B4
		public SecurityManager(Requester requester)
			: base(requester)
		{
			this._tracePoint = new SecurityManagerTracePoint(requester.TracePoint);
			this._managerCodepoint = ManagerCodePoint.SECMGR;
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x0011E0D9 File Offset: 0x0011C2D9
		public override void Initialize()
		{
			base.Initialize();
			this._level = 9;
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x0011E0EC File Offset: 0x0011C2EC
		public override void Reset()
		{
			if (this._requester.TracePoint != null && this._tracePoint.TraceContainer != this._requester.TracePoint.TraceContainer)
			{
				this._tracePoint = new SecurityManagerTracePoint(this._requester.TracePoint);
			}
			if (this._kerberosManager != null)
			{
				this._kerberosManager.ReleaseResource();
				this._kerberosManager = null;
			}
			this._secmec = SecurityMechanism.Unknown;
			this._isEsso = false;
			this._essoUserId = null;
			this._essoPassword = null;
			this._newPassword = null;
			this._decryptionHelper = null;
			this._key = null;
			this._iv = null;
			this._algo = EncryptionAlgorithm.EncAlgNone;
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x06004ADA RID: 19162 RVA: 0x0011E192 File Offset: 0x0011C392
		public SecurityMechanism TypeOfSecMec
		{
			get
			{
				return this._secmec;
			}
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x06004ADB RID: 19163 RVA: 0x0011E19A File Offset: 0x0011C39A
		public string NewPassword
		{
			get
			{
				return this._newPassword;
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x06004ADC RID: 19164 RVA: 0x0011E1A2 File Offset: 0x0011C3A2
		public string Principal
		{
			get
			{
				return this._principal;
			}
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06004ADD RID: 19165 RVA: 0x0011E1AA File Offset: 0x0011C3AA
		public string EssoUserId
		{
			get
			{
				return this._essoUserId;
			}
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06004ADE RID: 19166 RVA: 0x0011E1B2 File Offset: 0x0011C3B2
		public string EssoPassword
		{
			get
			{
				return this._essoPassword;
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06004ADF RID: 19167 RVA: 0x0011E1BA File Offset: 0x0011C3BA
		public bool IsEsso
		{
			get
			{
				return this._isEsso;
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06004AE0 RID: 19168 RVA: 0x0011E1C2 File Offset: 0x0011C3C2
		public IDecryptionHelper DecryptionHelper
		{
			get
			{
				return this._decryptionHelper;
			}
		}

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06004AE1 RID: 19169 RVA: 0x0011E1CA File Offset: 0x0011C3CA
		public byte[] Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06004AE2 RID: 19170 RVA: 0x0011E1D2 File Offset: 0x0011C3D2
		public byte[] Iv
		{
			get
			{
				return this._iv;
			}
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06004AE3 RID: 19171 RVA: 0x0011E1DA File Offset: 0x0011C3DA
		// (set) Token: 0x06004AE4 RID: 19172 RVA: 0x0011E1E2 File Offset: 0x0011C3E2
		public KerberosManager KerbManager
		{
			get
			{
				return this._kerberosManager;
			}
			set
			{
				this._kerberosManager = value;
			}
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x0011E1EC File Offset: 0x0011C3EC
		public async Task SubmitAccsecAsync(SecurityMechanism secMechOverride, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SecurityManager::SubmitAccsec");
			}
			this.AnalyzeAuthenticationSettings();
			string rdbName = this._requester.RdbName;
			bool authModeUnknown = false;
			SecurityMechanism secmec = this._secmec;
			if (secmec == SecurityMechanism.Unknown)
			{
				if (secMechOverride != SecurityMechanism.NotSupported)
				{
					secmec = secMechOverride;
				}
				else if (base.Requester.IsIMSDB)
				{
					secmec = SecurityMechanism.DCESEC;
				}
				else
				{
					secmec = SecurityMechanism.EUSRIDPWD;
				}
				authModeUnknown = true;
			}
			bool securityMechAccepted = false;
			bool algoAccepted = false;
			int supportedMechIndex = -1;
			string principal = string.Empty;
			for (;;)
			{
				ACCSEC accsec = new ACCSEC(null);
				base.InitializeCodepoint(accsec);
				accsec.Secmec = secmec;
				if (base.Requester.IsIMSDB)
				{
					accsec.RDBNAM = string.Empty;
				}
				else
				{
					accsec.RDBNAM = rdbName;
				}
				bool needEncryption = false;
				SecurityMechanism securityMechanism = secmec;
				if (securityMechanism == SecurityMechanism.USRENCPWD || securityMechanism == SecurityMechanism.EUSRIDPWD)
				{
					if (this._algo == EncryptionAlgorithm.EncAlgNone)
					{
						if (this._requester.EncryptionAlgorithm == "AES")
						{
							this._algo = EncryptionAlgorithm.EncAlgAES;
						}
						else
						{
							this._algo = EncryptionAlgorithm.EncAlgDES;
						}
					}
					accsec.EncAlg = this._algo;
					byte[] array = null;
					EncryptionAlgorithm algo = this._algo;
					if (algo != EncryptionAlgorithm.EncAlgDES)
					{
						if (algo == EncryptionAlgorithm.EncAlgAES)
						{
							this._decryptionHelper = new AESDecryptionHelper(this._requester.IsUnicodeMgrSupported);
						}
					}
					else
					{
						this._decryptionHelper = new DESDecryptionHelper(this._requester.IsUnicodeMgrSupported);
					}
					this._decryptionHelper.GetPublicKey(out array);
					accsec.Sectkn = array;
					needEncryption = true;
				}
				ACCSECRD accsecrd = new ACCSECRD();
				base.InitializeCodepoint(accsecrd);
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "Sending ACCSEC... authentication mode: " + accsec.Secmec.ToString());
				}
				Manager.ReplyInfo replyInfo = null;
				try
				{
					await accsec.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 0, isAsync, cancellationToken);
					await this._requester.ConnectionManager.DdmWriter.FlushAsync(isAsync, cancellationToken);
					do
					{
						CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
						if (this._tracePoint.IsEnabled(TraceFlags.Information))
						{
							this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
						}
						CodePoint codePoint = currentCP;
						EXCSATRD excsatrd;
						if (codePoint != CodePoint.EXCSATRD)
						{
							if (codePoint != CodePoint.ACCSECRD)
							{
								if (codePoint != CodePoint.KERSECPPL)
								{
									Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
									if (this._tracePoint.IsEnabled(TraceFlags.Warning))
									{
										this._tracePoint.Trace(TraceFlags.Warning, "SecurityManager::SubmitAccsecAsync: Read unexpected CodePoint: " + currentCP.ToString());
									}
									if (replyInfo2 != null)
									{
										replyInfo = replyInfo2;
									}
								}
								else
								{
									principal = await this._requester.ConnectionManager.DdmReader.ReadStringAsync(isAsync, cancellationToken);
								}
							}
							else
							{
								await accsecrd.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
							}
						}
						else
						{
							if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
							{
								this._tracePoint.Trace(TraceFlags.Verbose, "Processing EXCSATRD");
							}
							excsatrd = new EXCSATRD();
							excsatrd.TracePoint = this._requester.CommonTracePoint;
							await excsatrd.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
							base.Requester.Supervisor._srvclsnm = excsatrd.Srvclsnm;
							base.Requester.Supervisor._srvrlslv = excsatrd.Srvrlslv;
							base.Requester.Supervisor._managerLevels = excsatrd.MgrLvlls;
						}
						excsatrd = null;
					}
					while (base.NeedReadMoreDdmCodepoint(1, this._requester.IsIMSDB));
				}
				catch (Exception ex)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "SecurityManager::SubmitAccsecAsync(): " + ex.ToString());
					}
					if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
					{
						this._requester.ConnectionManager.DdmWriter.Reset();
					}
					throw this._requester.MakeException(ex.Message, "HY000", -1036, ex.HResult);
				}
				base.ProcessReplyInfo(null, replyInfo, "SecurityManager::SubmitAccsecAsync");
				if (accsecrd.SecurityMechanisms.Contains(secmec))
				{
					securityMechAccepted = true;
					if (needEncryption)
					{
						bool flag = true;
						int secchkcd = accsecrd.Secchkcd;
						switch (secchkcd)
						{
						case 0:
						case 2:
						case 5:
						case 8:
							this.BuildKey(accsecrd.Sectkn);
							goto IL_09F7;
						case 1:
							goto IL_09F7;
						case 3:
						case 6:
						case 9:
							break;
						case 4:
						case 7:
						case 10:
							flag = false;
							break;
						default:
						{
							if (secchkcd != 27)
							{
								goto IL_09F7;
							}
							if (this._tracePoint.IsEnabled(TraceFlags.Warning))
							{
								this._tracePoint.Trace(TraceFlags.Warning, "SecurityManager::SubmitAccsecAsync(): DRDA Server doesn't support AES 256. But support: " + string.Join<EncryptionAlgorithm>(", ", accsecrd.EncryptionAlgorithms));
							}
							bool flag2 = false;
							foreach (EncryptionAlgorithm encryptionAlgorithm in accsecrd.EncryptionAlgorithms)
							{
								if (encryptionAlgorithm != EncryptionAlgorithm.EncAlgNone && encryptionAlgorithm != this._algo)
								{
									this._algo = encryptionAlgorithm;
									flag2 = true;
									break;
								}
							}
							if (flag2)
							{
								goto IL_0ACA;
							}
							goto IL_09F7;
						}
						}
						if (this._algo == EncryptionAlgorithm.EncAlgAES)
						{
							this._algo = EncryptionAlgorithm.EncAlgDES;
						}
						else
						{
							secmec = SecurityMechanism.USRIDPWD;
							supportedMechIndex = SecurityManager._supportedMechs.Count - 1;
						}
						if (!flag)
						{
							goto Block_26;
						}
						goto IL_0ACA;
					}
					IL_09F7:
					if (accsecrd.Secchkcd != 0)
					{
						goto Block_27;
					}
					algoAccepted = true;
					if (this._secmec == SecurityMechanism.KERSEC)
					{
						if (!string.IsNullOrWhiteSpace(principal))
						{
							this._principal = principal;
						}
						else if (!string.IsNullOrWhiteSpace(accsecrd.ServerPrincipal))
						{
							this._principal = accsecrd.ServerPrincipal;
						}
						else
						{
							this._principal = this._requester.ConnectionInfo[27];
						}
					}
					accsecrd = null;
					replyInfo = null;
				}
				else
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Warning))
					{
						this._tracePoint.Trace(TraceFlags.Warning, string.Format("SecurityManager::SubmitAccsecAsync(): Required Security Mechanism {0} is not supported by this provider which supports {1}.", secmec.ToString(), string.Join<SecurityMechanism>(", ", accsecrd.SecurityMechanisms)));
					}
					bool flag3 = false;
					if (secmec != SecurityMechanism.KERSEC)
					{
						for (supportedMechIndex++; supportedMechIndex < SecurityManager._supportedMechs.Count; supportedMechIndex++)
						{
							SecurityMechanism securityMechanism2 = SecurityManager._supportedMechs[supportedMechIndex];
							if (securityMechanism2 == this._secmec && !authModeUnknown)
							{
								break;
							}
							if (accsecrd.SecurityMechanisms.Contains(securityMechanism2))
							{
								secmec = securityMechanism2;
								flag3 = true;
								break;
							}
						}
					}
					if (!flag3)
					{
						break;
					}
				}
				IL_0ACA:
				if (securityMechAccepted && algoAccepted)
				{
					goto Block_33;
				}
			}
			throw this._requester.MakeException(RequesterResource.AuthenticationNotSupported, "HY000", -1004);
			Block_26:
			throw new ReconnectException(this._algo, secmec);
			Block_27:
			if (this._tracePoint.IsEnabled(TraceFlags.Error))
			{
				ACCSECRD accsecrd;
				this._tracePoint.Trace(TraceFlags.Error, "SecurityManager::SubmitAccsecAsync(): unexpected secchkcd: " + accsecrd.Secchkcd.ToString());
			}
			throw this._requester.MakeException(RequesterResource.UnauthorizedAccess, "HY000", -1030);
			Block_33:
			this._secmec = secmec;
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SecurityManager::SubmitAccsec");
			}
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x0011E24C File Offset: 0x0011C44C
		public async Task SubmitSecchkAsync(bool isAsync, CancellationToken cancellationToken)
		{
			string rdbName = this._requester.RdbName;
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SecurityManager::SubmitSecchkAsync");
			}
			byte[] secToken = null;
			SECCHK secchk = new SECCHK();
			base.InitializeCodepoint(secchk);
			secchk.Secmec = this._secmec;
			secchk.RDBNAM = this._requester.RdbName;
			secchk.NewPassword = this._newPassword;
			switch (this._secmec)
			{
			case SecurityMechanism.USRENCPWD:
				secchk.Usrid = (this._isEsso ? this._essoUserId : this._requester.ConnectionInfo[1]);
				secchk.Sectkn = this._decryptionHelper.EncryptText(this._isEsso ? this._essoUserId : this._requester.ConnectionInfo[2], this._key, this._iv);
				goto IL_025F;
			case SecurityMechanism.EUSRIDPWD:
				secchk.Sectkn = this._decryptionHelper.EncryptText(this._isEsso ? this._essoUserId : this._requester.ConnectionInfo[1], this._key, this._iv);
				secchk.Sectkn2 = this._decryptionHelper.EncryptText(this._isEsso ? this._essoPassword : this._requester.ConnectionInfo[2], this._key, this._iv);
				goto IL_025F;
			case SecurityMechanism.KERSEC:
				if (this._kerberosManager == null)
				{
					this._kerberosManager = new KerberosManager(this._requester, (SecurityManagerTracePoint)this._tracePoint);
					this._kerberosManager.Initialize();
				}
				else
				{
					this._kerberosManager.Reset();
				}
				this._kerberosManager.PrincipleName = this._principal;
				this._kerberosManager.ProcessSecurityToken(ref secToken);
				goto IL_025F;
			}
			secchk.Usrid = (this._isEsso ? this._essoUserId : this._requester.ConnectionInfo[1]);
			secchk.Password = (this._isEsso ? this._essoPassword : this._requester.ConnectionInfo[2]);
			IL_025F:
			Manager.ReplyInfo replyInfo = null;
			SECCHKRM secchkrm;
			for (;;)
			{
				secchkrm = new SECCHKRM();
				base.InitializeCodepoint(secchkrm);
				secchk.AutoFlush = false;
				try
				{
					await secchk.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, (secToken == null) ? 0 : 80, isAsync, cancellationToken);
					if (secToken != null)
					{
						await base.WriteDDMCodepoint(CodePoint.SECTKN, secToken, DssType.Object, 1, 0, false, isAsync, cancellationToken);
					}
					await this._requester.ConnectionManager.DdmWriter.FlushAsync(isAsync, cancellationToken);
					secToken = null;
					do
					{
						CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
						if (this._tracePoint.IsEnabled(TraceFlags.Information))
						{
							this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
						}
						if (currentCP == CodePoint.SECCHKRM)
						{
							await secchkrm.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
						}
						else
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
				if (this._secmec == SecurityMechanism.KERSEC && secchkrm.Secchkcd == 25)
				{
					flag = true;
					secToken = secchkrm.Sectkn;
					this._kerberosManager.ProcessSecurityToken(ref secToken);
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
					goto Block_17;
				}
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Error))
			{
				this._tracePoint.Trace(TraceFlags.Error, "SecurityManager::SubmitSecchkAsync(): Unexpected Secckcd code: " + secchkrm.Secchkcd.ToString());
			}
			throw this._requester.MakeException(RequesterResource.UnauthorizedAccess, "HY000", -1030);
			Block_17:
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SecurityManager::SubmitSecchkAsync");
			}
		}

		// Token: 0x06004AE7 RID: 19175 RVA: 0x0011E2A1 File Offset: 0x0011C4A1
		private void BuildKey(byte[] publicKey)
		{
			this._key = this._decryptionHelper.DecryptKey(publicKey);
			this._iv = this._decryptionHelper.GetKeyIV(publicKey);
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x0011E2C8 File Offset: 0x0011C4C8
		private void AnalyzeAuthenticationSettings()
		{
			string text = this._requester.ConnectionInfo[1];
			string text2 = this._requester.ConnectionInfo[2];
			if (string.Compare(text, "MS$KERBEROS", StringComparison.InvariantCulture) == 0 && string.Compare(text2, "MS$KERBEROS", StringComparison.InvariantCulture) == 0)
			{
				this._secmec = SecurityMechanism.KERSEC;
				return;
			}
			if (string.Compare(text, "MS$SAME", StringComparison.InvariantCulture) == 0 && string.Compare(text2, "MS$SAME", StringComparison.InvariantCulture) == 0)
			{
				this.RetrieveESSOUserIdAndPassword();
				if (this._requester.ConnectionInfo[17] == "DUW")
				{
					this._requester.ConnectionInfo[1] = this._essoUserId;
					this._requester.ConnectionInfo[2] = this._essoPassword;
					this._requester.ConnectionString = Requester.GenerateConnectionString(this._requester.ConnectInfo);
				}
			}
			this._newPassword = this._requester.ConnectionInfo[24];
			if (string.IsNullOrWhiteSpace(this._newPassword))
			{
				string text3 = this._requester.ConnectionInfo[36];
				if (string.Compare(text3, "Server_Encrypt_Pwd", StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					this._secmec = SecurityMechanism.USRENCPWD;
				}
				else if (string.Compare(text3, "Server_Encrypt_UsrPwd", StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					this._secmec = SecurityMechanism.EUSRIDPWD;
				}
				else if (string.Compare(text3, "Server", StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					this._secmec = SecurityMechanism.USRIDPWD;
				}
				else
				{
					this._secmec = SecurityMechanism.Unknown;
				}
				this._newPassword = null;
				return;
			}
			this._secmec = SecurityMechanism.USRIDNWPWD;
		}

		// Token: 0x06004AE9 RID: 19177 RVA: 0x0011E420 File Offset: 0x0011C620
		private void RetrieveESSOUserIdAndPassword()
		{
			string text = this._requester.ConnectionInfo[21];
			if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this._tracePoint.Trace(TraceFlags.Verbose, "Using user Id and Password from ESSO application: " + text);
			}
			try
			{
				string[] credentials = ((ISSOLookup2)new SSOLookup()).GetCredentials(text, 4, out this._essoUserId);
				if (credentials.Length != 0)
				{
					this._essoPassword = credentials[0];
				}
				if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this._tracePoint.Trace(TraceFlags.Verbose, string.Format("Retrieved user Id ({0} and Password from ESSO application", this._essoUserId));
				}
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "Failed to retrieve user Id and password for specified affiliate app: " + text);
					this._tracePoint.Trace(TraceFlags.Error, ex.ToString());
				}
				throw this._requester.MakeException(ex.Message, "08S01", -606);
			}
			this._isEsso = true;
		}

		// Token: 0x04003996 RID: 14742
		private IDecryptionHelper _decryptionHelper;

		// Token: 0x04003997 RID: 14743
		private SecurityMechanism _secmec;

		// Token: 0x04003998 RID: 14744
		private KerberosManager _kerberosManager;

		// Token: 0x04003999 RID: 14745
		private string _principal;

		// Token: 0x0400399A RID: 14746
		private byte[] _key;

		// Token: 0x0400399B RID: 14747
		private byte[] _iv;

		// Token: 0x0400399C RID: 14748
		private string _newPassword;

		// Token: 0x0400399D RID: 14749
		private bool _isEsso;

		// Token: 0x0400399E RID: 14750
		private string _essoUserId;

		// Token: 0x0400399F RID: 14751
		private string _essoPassword;

		// Token: 0x040039A0 RID: 14752
		public EncryptionAlgorithm _algo;

		// Token: 0x040039A1 RID: 14753
		private static readonly List<SecurityMechanism> _supportedMechs = new List<SecurityMechanism>
		{
			SecurityMechanism.EUSRIDPWD,
			SecurityMechanism.USRENCPWD,
			SecurityMechanism.USRIDPWD,
			SecurityMechanism.DCESEC
		};
	}
}
