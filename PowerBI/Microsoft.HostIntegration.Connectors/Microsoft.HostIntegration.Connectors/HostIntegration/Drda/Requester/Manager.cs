using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.DDM;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.DrdaClient;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x02000931 RID: 2353
	internal abstract class Manager
	{
		// Token: 0x060049D2 RID: 18898 RVA: 0x00113CE0 File Offset: 0x00111EE0
		static Manager()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Host Integration Server\\Data Integration", RegistryKeyPermissionCheck.Default, RegistryRights.QueryValues))
				{
					if (registryKey != null && registryKey.GetValueKind("Db2WarningsAsError") == RegistryValueKind.MultiString)
					{
						string[] array = (string[])registryKey.GetValue("Db2WarningsAsError");
						if (array != null)
						{
							Array.ForEach<string>(array, delegate(string warningCode)
							{
								try
								{
									Manager._warningToErrorSet.Add(Convert.ToInt32(warningCode));
								}
								catch (FormatException)
								{
								}
								catch (OverflowException)
								{
								}
							});
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (IOException)
			{
			}
			catch (SecurityException)
			{
			}
		}

		// Token: 0x060049D3 RID: 18899 RVA: 0x00113D90 File Offset: 0x00111F90
		public Manager(Requester requester)
		{
			this._level = 0;
			this._requester = requester;
		}

		// Token: 0x060049D4 RID: 18900 RVA: 0x00113DA6 File Offset: 0x00111FA6
		public virtual void Reset()
		{
			this._level = 0;
		}

		// Token: 0x060049D5 RID: 18901 RVA: 0x000036A9 File Offset: 0x000018A9
		public virtual void Initialize()
		{
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x060049D6 RID: 18902 RVA: 0x00113DAF File Offset: 0x00111FAF
		public ManagerCodePoint ManagerCodePoint
		{
			get
			{
				return this._managerCodepoint;
			}
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x060049D7 RID: 18903 RVA: 0x00113DB7 File Offset: 0x00111FB7
		// (set) Token: 0x060049D8 RID: 18904 RVA: 0x00113DC0 File Offset: 0x00111FC0
		public int Level
		{
			get
			{
				return this._level;
			}
			set
			{
				if (this.IsLevelSupported(value))
				{
					this._level = value;
					return;
				}
				Exception ex = this._requester.MakeException(RequesterResource.UnsupportedManagerLevel(this._managerCodepoint, value), "HY000", -343);
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, ex);
				}
				throw ex;
			}
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x060049D9 RID: 18905 RVA: 0x00113E26 File Offset: 0x00112026
		public DrdaArTracePoint TracePoint
		{
			get
			{
				return this._tracePoint;
			}
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x060049DA RID: 18906 RVA: 0x00113E2E File Offset: 0x0011202E
		public Requester Requester
		{
			get
			{
				return this._requester;
			}
		}

		// Token: 0x060049DB RID: 18907 RVA: 0x00002B16 File Offset: 0x00000D16
		protected virtual bool IsLevelSupported(int newLevel)
		{
			return true;
		}

		// Token: 0x060049DC RID: 18908 RVA: 0x00113E38 File Offset: 0x00112038
		protected async Task WriteDDMCodepoint(CodePoint codePoint, object value, DssType dssType, int correlationId, byte chainState, bool flush, bool isAsync, CancellationToken cancellationToken)
		{
			if (dssType != DssType.Request)
			{
				if (dssType == DssType.Object)
				{
					this._requester.ConnectionManager.DdmWriter.CreateDssObject(correlationId);
				}
			}
			else
			{
				this._requester.ConnectionManager.DdmWriter.CreateDssRequest(correlationId);
			}
			if (value is byte[])
			{
				this._requester.ConnectionManager.DdmWriter.WriteScalarBytes(codePoint, value as byte[]);
			}
			else if (value is DBNull)
			{
				this._requester.ConnectionManager.DdmWriter.WriteScalarHeader(codePoint, 0);
			}
			else if (value is CodePoint)
			{
				CodePoint codePoint2 = (CodePoint)value;
				if (codePoint2 == CodePoint.RLSCONV)
				{
					this._requester.ConnectionManager.DdmWriter.WriteBeginDdm(codePoint);
					this._requester.ConnectionManager.DdmWriter.WriteScalar1Byte(codePoint2, 242);
					this._requester.ConnectionManager.DdmWriter.WriteEndDdm();
				}
			}
			this._requester.ConnectionManager.DdmWriter.WriteEndDss();
			await this._requester.ConnectionManager.DdmWriter.WriteEndChainAsync(chainState, flush, isAsync, cancellationToken);
		}

		// Token: 0x060049DD RID: 18909 RVA: 0x00113EC4 File Offset: 0x001120C4
		protected async Task<Manager.ReplyInfo> ProcessReplyCodepointsAsync(CodePoint codePoint, bool isAsync, CancellationToken cancellationToken)
		{
			Manager.ReplyInfo info = null;
			CodePoint codePoint2 = codePoint;
			if (codePoint2 <= CodePoint.PRCCNVRM)
			{
				if (codePoint2 <= CodePoint.CMDATHRM)
				{
					if (codePoint2 == CodePoint.MGRDEPRM)
					{
						Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
						info = replyInfo;
						info.SqlCode = -331;
						info.Message = RequesterResource.DDM_MGRDEPRM;
						info.SqlState = "HY000";
						goto IL_10BA;
					}
					if (codePoint2 == CodePoint.CMDATHRM)
					{
						Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
						info = replyInfo;
						info.SqlCode = -269;
						info.Message = RequesterResource.DDM_CMDATHRM;
						info.SqlState = "HY000";
						goto IL_10BA;
					}
				}
				else
				{
					if (codePoint2 == CodePoint.AGNPRMRM)
					{
						Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
						info = replyInfo;
						info.SqlCode = -261;
						info.Message = RequesterResource.DDM_AGNPRMRM;
						info.SqlState = "HY000";
						goto IL_10BA;
					}
					if (codePoint2 == CodePoint.RSCLMTRM)
					{
						Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
						info = replyInfo;
						info.SqlCode = -370;
						info.Message = RequesterResource.DDM_RSCLMTRM;
						info.SqlState = "HY000";
						goto IL_10BA;
					}
					if (codePoint2 == CodePoint.PRCCNVRM)
					{
						Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
						info = replyInfo;
						info.SqlCode = -343;
						info.Message = RequesterResource.DDM_PRCCNVRM;
						info.SqlState = "HY000";
						goto IL_10BA;
					}
				}
			}
			else if (codePoint2 <= CodePoint.TRGNSPRM)
			{
				switch (codePoint2)
				{
				case CodePoint.SYNTAXRM:
				{
					Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
					info = replyInfo;
					info.SqlCode = -379;
					info.Message = RequesterResource.DDM_SYNTAXRM;
					info.SqlState = "HY000";
					goto IL_10BA;
				}
				case CodePoint.UPDCSRRM:
				case CodePoint.UPDINTRM:
				case CodePoint.NEWNAMRM:
					break;
				case CodePoint.CMDNSPRM:
				{
					Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
					info = replyInfo;
					info.SqlCode = -272;
					info.Message = RequesterResource.DDM_CMDNSPRM;
					info.SqlState = "HY000";
					goto IL_10BA;
				}
				case CodePoint.PRMNSPRM:
				{
					Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
					info = replyInfo;
					info.SqlCode = -344;
					info.Message = RequesterResource.DDM_PRMNSPRM;
					info.SqlState = "HY000";
					goto IL_10BA;
				}
				case CodePoint.VALNSPRM:
				{
					Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
					info = replyInfo;
					info.SqlCode = -385;
					info.Message = RequesterResource.DDM_VALNSPRM;
					info.SqlState = "HY000";
					goto IL_10BA;
				}
				case CodePoint.OBJNSPRM:
				{
					Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
					info = replyInfo;
					info.SqlCode = -335;
					info.Message = RequesterResource.DDM_OBJNSPRM;
					info.SqlState = "HY000";
					goto IL_10BA;
				}
				case CodePoint.CMDCHKRM:
				{
					Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
					info = replyInfo;
					info.SqlCode = -270;
					info.Message = RequesterResource.DDM_CMDCHKRM;
					info.SqlState = "HY000";
					goto IL_10BA;
				}
				default:
					if (codePoint2 == CodePoint.TRGNSPRM)
					{
						Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
						info = replyInfo;
						info.SqlCode = -381;
						info.Message = RequesterResource.DDM_TRGNSPRM;
						info.SqlState = "HY000";
						goto IL_10BA;
					}
					break;
				}
			}
			else
			{
				switch (codePoint2)
				{
				case CodePoint.QRYNOPRM:
				{
					Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
					info = replyInfo;
					info.SqlCode = -345;
					info.Message = RequesterResource.DDM_QRYNOPRM;
					info.SqlState = "HY000";
					goto IL_10BA;
				}
				case CodePoint.RDBATHRM:
				{
					Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
					info = replyInfo;
					info.SqlCode = -358;
					info.Message = RequesterResource.DDM_RDBATHRM;
					info.SqlState = "HY000";
					goto IL_10BA;
				}
				case CodePoint.RDBNACRM:
				case CodePoint.OPNQRYRM:
				case CodePoint.PKGBNARM:
				case CodePoint.BGNBNDRM:
					break;
				case CodePoint.RDBACCRM:
				{
					Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
					info = replyInfo;
					info.SqlCode = -356;
					info.Message = RequesterResource.DDM_RDBACCRM;
					info.SqlState = "HY000";
					goto IL_10BA;
				}
				case CodePoint.PKGBPARM:
				{
					Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
					info = replyInfo;
					info.SqlCode = -342;
					info.Message = RequesterResource.DDM_PKGBPARM;
					info.SqlState = "HY000";
					goto IL_10BA;
				}
				case CodePoint.DSCINVRM:
				{
					Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
					info = replyInfo;
					info.SqlCode = -290;
					info.Message = RequesterResource.DDM_DSCINVRM;
					info.SqlState = "HY000";
					goto IL_10BA;
				}
				default:
					switch (codePoint2)
					{
					case CodePoint.DTAMCHRM:
					{
						Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
						info = replyInfo;
						info.SqlCode = -292;
						info.Message = RequesterResource.DDM_DTAMCHRM;
						info.SqlState = "HY000";
						goto IL_10BA;
					}
					case CodePoint.QRYPOPRM:
					case CodePoint.INTTKNRM:
						break;
					case CodePoint.RDBNFNRM:
					{
						Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
						info = replyInfo;
						info.SqlCode = -360;
						info.Message = RequesterResource.DDM_RDBNFNRM;
						info.SqlState = "HY000";
						goto IL_10BA;
					}
					case CodePoint.OPNQFLRM:
					{
						Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
						info = replyInfo;
						info.SqlCode = -339;
						info.Message = RequesterResource.DDM_OPNQFLRM;
						info.SqlState = "HY000";
						goto IL_10BA;
					}
					case CodePoint.SQLERRRM:
					{
						Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
						info = replyInfo;
						info.SqlCode = -372;
						info.Message = RequesterResource.DDM_SQLERRRM;
						info.SqlState = "HY000";
						goto IL_10BA;
					}
					default:
						if (codePoint2 == CodePoint.RDBAFLRM)
						{
							Manager.ReplyInfo replyInfo = await this.GetReplyDetail(isAsync, cancellationToken);
							info = replyInfo;
							info.SqlCode = -357;
							info.Message = RequesterResource.DDM_RDBAFLRM;
							info.SqlState = "HY000";
							goto IL_10BA;
						}
						break;
					}
					break;
				}
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Warning))
			{
				this._tracePoint.Trace(TraceFlags.Warning, "Read CodePoint not supported: " + codePoint.ToString());
			}
			await this._requester.ConnectionManager.DdmReader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			IL_10BA:
			return info;
		}

		// Token: 0x060049DE RID: 18910 RVA: 0x00113F21 File Offset: 0x00112121
		protected bool NeedReadMoreDdmCodepoint(int correlationId)
		{
			return this.NeedReadMoreDdmCodepoint(correlationId, false);
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x00113F2C File Offset: 0x0011212C
		protected bool NeedReadMoreDdmCodepoint(int correlationId, bool isIMSDB)
		{
			if ((this._requester.ConnectionManager.DdmReader.GetCurrentChainState() & 64) == 0)
			{
				return false;
			}
			if (this._requester.ConnectionManager.DdmReader.DssCorrelationID < correlationId)
			{
				return true;
			}
			if (isIMSDB)
			{
				return this._requester.ConnectionManager.DdmReader.DssCorrelationID == correlationId && (this._requester.ConnectionManager.DdmReader.GetCurrentChainState() & 64) == 64;
			}
			return this._requester.ConnectionManager.DdmReader.DssCorrelationID == correlationId && (this._requester.ConnectionManager.DdmReader.GetCurrentChainState() & 80) == 80;
		}

		// Token: 0x060049E0 RID: 18912 RVA: 0x00113FDF File Offset: 0x001121DF
		protected void InitializeCodepoint(AbstractDdmObject ddmCodepoint)
		{
			ddmCodepoint.TracePoint = this._requester.CommonTracePoint;
			ddmCodepoint.DrdaFlavor = this._requester.Flavor;
			ddmCodepoint.Initializer = new Action<AbstractDdmObject>(this.InitializeCodepoint);
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x00114018 File Offset: 0x00112218
		protected async Task ReadDdmCodepoint(bool isAsync, CancellationToken cancellationToken, Func<ObjectInfo, Task<bool>> ddmObjectFunction)
		{
			long parentDdmObjectLength = this._requester.ConnectionManager.DdmReader.DdmObjectLength;
			IEnumerator<Task<ObjectInfo>> taskEnumerator = (isAsync ? this._requester.ConnectionManager.DdmReader.ReadDdmObjectsAsync(cancellationToken).GetEnumerator() : null);
			IEnumerator<ObjectInfo> enumerator = (isAsync ? null : this._requester.ConnectionManager.DdmReader.ReadDdmObjects().GetEnumerator());
			long childDdmObjectLengthSum = 0L;
			while (childDdmObjectLengthSum < parentDdmObjectLength && (isAsync ? taskEnumerator.MoveNext() : enumerator.MoveNext()))
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
				childDdmObjectLengthSum += objectInfo.Length + 4L;
				bool flag = false;
				if (ddmObjectFunction != null)
				{
					flag = await ddmObjectFunction(objectInfo);
				}
				if (!flag)
				{
					await this._requester.ConnectionManager.DdmReader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
				}
			}
		}

		// Token: 0x060049E2 RID: 18914 RVA: 0x00114078 File Offset: 0x00112278
		protected async Task<Manager.ReplyInfo> ProcessDssCodepoints(int requiredCorrelationId, bool isAsync, CancellationToken cancellationToken, Func<CodePoint, Task<bool>> ddmObjectFunction)
		{
			Manager.ReplyInfo replyInfo = null;
			do
			{
				await this._requester.ConnectionManager.DdmReader.ReadDssAsync(isAsync, cancellationToken);
				int dssLength = this._requester.ConnectionManager.DdmReader.DssLength;
				long ddmObjectLengthSum = 0L;
				while (ddmObjectLengthSum < (long)dssLength)
				{
					CodePoint currentCodePoint = await this._requester.ConnectionManager.DdmReader.ReadDdmObjectLengthAndCodePointAsync(isAsync, cancellationToken);
					ddmObjectLengthSum += this._requester.ConnectionManager.DdmReader.DdmObjectLength + 4L;
					if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this._tracePoint.Trace(TraceFlags.Verbose, "Receiving codepoint: " + currentCodePoint.ToString());
					}
					bool flag = false;
					if (ddmObjectFunction != null)
					{
						flag = await ddmObjectFunction(currentCodePoint);
					}
					if (!flag)
					{
						Manager.ReplyInfo replyInfo2 = await this.ProcessReplyCodepointsAsync(currentCodePoint, isAsync, cancellationToken);
						if (replyInfo2 != null && this._requester.ConnectionManager.DdmReader.DssCorrelationID == 1)
						{
							replyInfo = replyInfo2;
						}
					}
				}
			}
			while (this.NeedReadMoreDdmCodepoint(requiredCorrelationId));
			return replyInfo;
		}

		// Token: 0x060049E3 RID: 18915 RVA: 0x001140E0 File Offset: 0x001122E0
		protected async Task<Manager.ReplyInfo> GetReplyDetail(bool isAsync, CancellationToken cancellationToken)
		{
			Manager.<>c__DisplayClass28_0 CS$<>8__locals1 = new Manager.<>c__DisplayClass28_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.isAsync = isAsync;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			CS$<>8__locals1.replyInfo = new Manager.ReplyInfo();
			await this.ReadDdmCodepoint(CS$<>8__locals1.isAsync, CS$<>8__locals1.cancellationToken, delegate(ObjectInfo ddmObj)
			{
				Manager.<>c__DisplayClass28_0.<<GetReplyDetail>b__0>d <<GetReplyDetail>b__0>d;
				<<GetReplyDetail>b__0>d.<>4__this = CS$<>8__locals1;
				<<GetReplyDetail>b__0>d.ddmObj = ddmObj;
				<<GetReplyDetail>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
				<<GetReplyDetail>b__0>d.<>1__state = -1;
				AsyncTaskMethodBuilder<bool> <>t__builder = <<GetReplyDetail>b__0>d.<>t__builder;
				<>t__builder.Start<Manager.<>c__DisplayClass28_0.<<GetReplyDetail>b__0>d>(ref <<GetReplyDetail>b__0>d);
				return <<GetReplyDetail>b__0>d.<>t__builder.Task;
			});
			return CS$<>8__locals1.replyInfo;
		}

		// Token: 0x060049E4 RID: 18916 RVA: 0x00114138 File Offset: 0x00112338
		protected void ProcessReplyInfo(SqlStatement statement, Manager.ReplyInfo replyInfo, string errorMessageBase)
		{
			if (statement != null && statement.CurrentErrorSqlca != null)
			{
				return;
			}
			if (replyInfo == null)
			{
				return;
			}
			if ((ushort)replyInfo.Severity >= 8)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, errorMessageBase + " : Error in reply: " + replyInfo.Message);
				}
				throw this._requester.MakeException(replyInfo);
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Warning))
			{
				this._tracePoint.Trace(TraceFlags.Warning, errorMessageBase + " : Warning in reply: " + replyInfo.Message);
			}
		}

		// Token: 0x060049E5 RID: 18917 RVA: 0x001141C4 File Offset: 0x001123C4
		protected async Task<int> ProcessSqlCa(SQLCAGRP sqlcagrp, SqlStatement statement, bool isAsync, CancellationToken cancellationToken)
		{
			return await this.ProcessSqlCa(sqlcagrp, statement, null, isAsync, cancellationToken);
		}

		// Token: 0x060049E6 RID: 18918 RVA: 0x0011422C File Offset: 0x0011242C
		protected async Task<int> ProcessSqlCa(SQLCAGRP sqlcagrp, SqlStatement statement, Func<Task> funcIfFailed, bool isAsync, CancellationToken cancellationToken)
		{
			int num = -1;
			if (sqlcagrp != null)
			{
				bool flag = false;
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "Received sqlcagrp: " + sqlcagrp.ToString());
				}
				if (statement != null)
				{
					statement.SqlcaCode = sqlcagrp.SqlCode;
				}
				if (sqlcagrp.SqlCode == 0)
				{
					num = sqlcagrp.UpdateCount;
				}
				else if (sqlcagrp.SqlCode == 100)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "End of Query: sqlca.SqlCode = " + sqlcagrp.SqlCode.ToString());
					}
					num = sqlcagrp.UpdateCount;
				}
				else
				{
					bool needExtendedMessage = false;
					if (sqlcagrp.SqlCode < 0)
					{
						if (statement == null)
						{
							flag = true;
						}
						else if (statement.CurrentErrorSqlca == null)
						{
							statement.CurrentErrorSqlca = sqlcagrp;
							flag = true;
						}
						if (sqlcagrp.SqlCode == -805 && (statement == null || statement.StatementType != Parser.StatementType.Static))
						{
							if (this._tracePoint.IsEnabled(TraceFlags.Information))
							{
								this._tracePoint.Trace(TraceFlags.Information, "sqlca.SqlCode = SQLCA_PACKAGE_NOT_PRESENT, will create packages on the fly.");
							}
							flag = false;
						}
						else
						{
							needExtendedMessage = true;
						}
					}
					else
					{
						if (this._tracePoint.IsEnabled(TraceFlags.Warning))
						{
							this._tracePoint.Trace(TraceFlags.Warning, "sqlca.SqlCode = " + sqlcagrp.SqlCode.ToString());
						}
						if (Manager._warningToErrorSet.Contains(sqlcagrp.SqlCode))
						{
							needExtendedMessage = true;
							if (this._tracePoint.IsEnabled(TraceFlags.Information))
							{
								this._tracePoint.Trace(TraceFlags.Information, "sqlca.SqlCode is in the list of warning as error.");
							}
							if (statement != null && statement.CurrentErrorSqlca == null)
							{
								statement.CurrentErrorSqlca = sqlcagrp;
								flag = true;
							}
						}
					}
					if (flag)
					{
						if (funcIfFailed != null)
						{
							await funcIfFailed();
						}
						string text = sqlcagrp.SqlErrorMessage;
						if (needExtendedMessage)
						{
							text = await SqlStatement.GetSqlcaMessage(sqlcagrp, statement, this, isAsync, cancellationToken);
						}
						if (statement != null)
						{
							statement.CurrentErrorSqlca = null;
						}
						if (this._tracePoint.IsEnabled(TraceFlags.Error))
						{
							this._tracePoint.Trace(TraceFlags.Error, "sqlca.SqlCode = " + sqlcagrp.SqlCode.ToString() + " error message: " + text);
						}
						throw this._requester.MakeException(text, sqlcagrp.SqlState, sqlcagrp.SqlCode);
					}
				}
			}
			return num;
		}

		// Token: 0x040037AB RID: 14251
		private static SortedSet<int> _warningToErrorSet = new SortedSet<int>();

		// Token: 0x040037AC RID: 14252
		protected Requester _requester;

		// Token: 0x040037AD RID: 14253
		protected int _level;

		// Token: 0x040037AE RID: 14254
		protected ManagerCodePoint _managerCodepoint;

		// Token: 0x040037AF RID: 14255
		protected DrdaArTracePoint _tracePoint;

		// Token: 0x040037B0 RID: 14256
		protected SQLCAGRP _currentErrorSqlca;

		// Token: 0x02000932 RID: 2354
		public class ReplyInfo
		{
			// Token: 0x170011E9 RID: 4585
			// (get) Token: 0x060049E7 RID: 18919 RVA: 0x0011429B File Offset: 0x0011249B
			// (set) Token: 0x060049E8 RID: 18920 RVA: 0x001142A3 File Offset: 0x001124A3
			public int CorrelationId { get; set; }

			// Token: 0x170011EA RID: 4586
			// (get) Token: 0x060049E9 RID: 18921 RVA: 0x001142AC File Offset: 0x001124AC
			// (set) Token: 0x060049EA RID: 18922 RVA: 0x001142B4 File Offset: 0x001124B4
			public string Message { get; set; }

			// Token: 0x170011EB RID: 4587
			// (get) Token: 0x060049EB RID: 18923 RVA: 0x001142BD File Offset: 0x001124BD
			// (set) Token: 0x060049EC RID: 18924 RVA: 0x001142C5 File Offset: 0x001124C5
			public string SqlState { get; set; }

			// Token: 0x170011EC RID: 4588
			// (get) Token: 0x060049ED RID: 18925 RVA: 0x001142CE File Offset: 0x001124CE
			// (set) Token: 0x060049EE RID: 18926 RVA: 0x001142D6 File Offset: 0x001124D6
			public int SqlCode { get; set; }

			// Token: 0x170011ED RID: 4589
			// (get) Token: 0x060049EF RID: 18927 RVA: 0x001142DF File Offset: 0x001124DF
			// (set) Token: 0x060049F0 RID: 18928 RVA: 0x001142E7 File Offset: 0x001124E7
			public SeverityCode Severity { get; set; }

			// Token: 0x170011EE RID: 4590
			// (get) Token: 0x060049F1 RID: 18929 RVA: 0x001142F0 File Offset: 0x001124F0
			// (set) Token: 0x060049F2 RID: 18930 RVA: 0x001142F8 File Offset: 0x001124F8
			public string DiagMessage { get; set; }
		}
	}
}
