using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.DDM;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.DrdaClient;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x0200090D RID: 2317
	internal class SqlStatement : ISqlStatement
	{
		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x06004932 RID: 18738 RVA: 0x0010D38F File Offset: 0x0010B58F
		// (set) Token: 0x06004933 RID: 18739 RVA: 0x0010D397 File Offset: 0x0010B597
		internal SQLCAGRP CurrentErrorSqlca { get; set; }

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06004934 RID: 18740 RVA: 0x0010D3A0 File Offset: 0x0010B5A0
		// (set) Token: 0x06004935 RID: 18741 RVA: 0x0010D3A8 File Offset: 0x0010B5A8
		internal bool IsPrepared
		{
			get
			{
				return this._isPrepared;
			}
			set
			{
				this._isPrepared = value;
			}
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06004936 RID: 18742 RVA: 0x0010D3B1 File Offset: 0x0010B5B1
		internal bool AutoCommit
		{
			get
			{
				return !this.turnOffAutoCommit && this._requester.AutoCommit;
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06004937 RID: 18743 RVA: 0x0010D3C8 File Offset: 0x0010B5C8
		// (set) Token: 0x06004938 RID: 18744 RVA: 0x0010D3D0 File Offset: 0x0010B5D0
		public SqlStatement.SqlState State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06004939 RID: 18745 RVA: 0x0010D3D9 File Offset: 0x0010B5D9
		// (set) Token: 0x0600493A RID: 18746 RVA: 0x0010D3E1 File Offset: 0x0010B5E1
		public int SqlcaCode { get; set; }

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x0600493B RID: 18747 RVA: 0x0010D3EA File Offset: 0x0010B5EA
		// (set) Token: 0x0600493C RID: 18748 RVA: 0x0010D3F2 File Offset: 0x0010B5F2
		public Func<bool, bool> LiteralReplacementInvestigator { get; set; }

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x0600493D RID: 18749 RVA: 0x0010D3FB File Offset: 0x0010B5FB
		// (set) Token: 0x0600493E RID: 18750 RVA: 0x0010D41B File Offset: 0x0010B61B
		public long CommandSourceId
		{
			get
			{
				if (this._commandSourceId == 0L)
				{
					this._commandSourceId = RequesterFactory.Instance.GetCommandSourceId();
				}
				return this._commandSourceId;
			}
			set
			{
				this._commandSourceId = value;
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x0600493F RID: 18751 RVA: 0x0010D424 File Offset: 0x0010B624
		// (set) Token: 0x06004940 RID: 18752 RVA: 0x0010D43C File Offset: 0x0010B63C
		public string CommandText
		{
			get
			{
				if (this._overrideCommandText != null)
				{
					return this._overrideCommandText;
				}
				return this._commandText;
			}
			set
			{
				if (string.Compare(this.CommandText, value, StringComparison.InvariantCultureIgnoreCase) != 0)
				{
					this.InternalReset(false, false);
					this._commandText = value;
					if (!string.IsNullOrEmpty(this._commandText))
					{
						this._parser.Parse(this._commandText);
						if (this._parser.Type == Parser.StatementType.Static)
						{
							if (this._parser.QualifiedNameParts.Count == 1)
							{
								if (this._tracePoint.IsEnabled(TraceFlags.Information))
								{
									this._tracePoint.Trace(TraceFlags.Information, "SqlStatement::CommandText_Set: replacing alias for: " + this._commandText);
								}
								string text = null;
								if (this._requester.PackageManager.AliasMappings.TryGetValue(this._parser.QualifiedNameParts[0].ToUpperInvariant(), out text))
								{
									if (this._tracePoint.IsEnabled(TraceFlags.Information))
									{
										this._tracePoint.Trace(TraceFlags.Information, "SqlStatement::CommandText_Set: find alias : " + text);
									}
									this._commandText = this._commandText.Replace(this._parser.QualifiedNameParts[0], text);
									if (this._tracePoint.IsEnabled(TraceFlags.Information))
									{
										this._tracePoint.Trace(TraceFlags.Information, "SqlStatement::CommandText_Set: new statement : " + this._commandText);
									}
									this._parser.Reset();
									this._parser.Parse(this._commandText);
								}
							}
							if (this._parser.QualifiedNameParts.Count < 3)
							{
								if (this._tracePoint.IsEnabled(TraceFlags.Error))
								{
									this._tracePoint.Trace(TraceFlags.Error, "SqlStatement::CommandText_Set: parser.QualifiedNameParts should have at least 3 parts: " + this._commandText);
								}
								throw this._requester.MakeException(RequesterResource.InvalidStaticPackageName(this._commandText), "HY000", -1035);
							}
							this._preparable = false;
							return;
						}
						else if (this._parser.Type == Parser.StatementType.Call)
						{
							if (this._requester.HostType == HostType.MVS || this._requester.HostType == HostType.DB2)
							{
								this._preparable = false;
							}
							if (this._requester.HostType == HostType.AS400)
							{
								this._isDescribable = false;
							}
						}
					}
				}
			}
		}

		// Token: 0x06004941 RID: 18753 RVA: 0x0010D64C File Offset: 0x0010B84C
		private void SetStatements(string statements)
		{
			if (string.Compare(this._statements, statements, StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return;
			}
			bool isPrepared = this.IsPrepared;
			this.InternalReset(false, isPrepared);
			if (isPrepared)
			{
				this.IsPrepared = true;
			}
			this._statements = statements;
			this._statementList.Clear();
			if (!string.IsNullOrEmpty(this._statements))
			{
				this._parser.Parse(this._statements);
				this._statementList.AddRange(this._parser.Statements);
			}
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06004942 RID: 18754 RVA: 0x0010D6C8 File Offset: 0x0010B8C8
		public SqlResultSet CurrentResultSet
		{
			get
			{
				return this._currentResultSet;
			}
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06004943 RID: 18755 RVA: 0x0010D6D0 File Offset: 0x0010B8D0
		public PKGNAMCSN Pkgnamcsn
		{
			get
			{
				if (this._pkgnamcsn == null)
				{
					int num = (this._requester.IsUnicodeMgrSupported ? 1208 : 500);
					this._pkgnamcsn = new PKGNAMCSN(num);
					this._pkgnamcsn.RDBNAM = this._requester.RdbName;
					if (this._parser.Type == Parser.StatementType.Static)
					{
						this._pkgnamcsn.Pkgid = this._parser.QualifiedNameParts[this._parser.QualifiedNameParts.Count - 3];
						this._pkgnamcsn.Pkgsn = int.Parse(this._parser.QualifiedNameParts[this._parser.QualifiedNameParts.Count - 1]);
						this._pkgnamcsn.Pkgcnstkn = new ConsistencyToken(this._requester.PackageManager.ConvertToPackageToken(this._parser.QualifiedNameParts[this._parser.QualifiedNameParts.Count - 2]));
						if (this._parser.QualifiedNameParts.Count > 3)
						{
							this._pkgnamcsn.Rdbcolid = this._parser.QualifiedNameParts[this._parser.QualifiedNameParts.Count - 4];
						}
						else
						{
							this._pkgnamcsn.Rdbcolid = this._requester.PackageCollection;
						}
					}
					else
					{
						this._pkgnamcsn.Rdbcolid = this._requester.PackageCollection;
						this._pkgnamcsn.Pkgcnstkn = new ConsistencyToken(this._requester.TransactionManager.PackageConsistencyToken);
						this._pkgnamcsn.Pkgid = this._requester.TransactionManager.PackageId;
					}
				}
				if (this._parser.Type != Parser.StatementType.Static)
				{
					this._pkgnamcsn.Pkgsn = this._section;
				}
				return this._pkgnamcsn;
			}
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06004944 RID: 18756 RVA: 0x0010D8A6 File Offset: 0x0010BAA6
		public List<ISqlParameter> ParameterInfoList
		{
			get
			{
				return this._parameterList;
			}
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x0010D8B0 File Offset: 0x0010BAB0
		public SqlStatement(Requester requester)
		{
			this._requester = requester;
			this._tracePoint = this._requester.SqlManager.TracePoint;
			this._parser = new Parser(this._tracePoint);
			this._parser.SyntaxFlavor = ((requester.Flavor == DrdaFlavor.Informix) ? Parser.Syntax.Informix : Parser.Syntax.DB2);
			this.CurrentErrorSqlca = null;
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x06004946 RID: 18758 RVA: 0x0010D94D File Offset: 0x0010BB4D
		internal DrdaArTracePoint TracePoint
		{
			get
			{
				return this._tracePoint;
			}
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06004947 RID: 18759 RVA: 0x0010D958 File Offset: 0x0010BB58
		internal bool CanCommitNow
		{
			get
			{
				if (this.Committed)
				{
					return false;
				}
				if (!this.IsReader)
				{
					return true;
				}
				foreach (IResultSet resultSet in this._resultSetList)
				{
					SqlResultSet sqlResultSet = (SqlResultSet)resultSet;
					if (!sqlResultSet.EndOfQuery)
					{
						if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
						{
							this._tracePoint.Trace(TraceFlags.Verbose, "SqlStatement::CanCommitNow: Resultset not end, no commit: " + sqlResultSet.QueryInstanceId.ToString());
						}
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06004948 RID: 18760 RVA: 0x0010DA00 File Offset: 0x0010BC00
		// (set) Token: 0x06004949 RID: 18761 RVA: 0x0010DA08 File Offset: 0x0010BC08
		internal bool Committed { get; set; }

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x0600494A RID: 18762 RVA: 0x0010DA11 File Offset: 0x0010BC11
		// (set) Token: 0x0600494B RID: 18763 RVA: 0x0010DA19 File Offset: 0x0010BC19
		internal bool IsReader { get; set; }

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x0600494C RID: 18764 RVA: 0x0010DA22 File Offset: 0x0010BC22
		// (set) Token: 0x0600494D RID: 18765 RVA: 0x0010DA2A File Offset: 0x0010BC2A
		internal bool NeedCommit { get; set; }

		// Token: 0x0600494E RID: 18766 RVA: 0x0010DA34 File Offset: 0x0010BC34
		public void SwitchOut()
		{
			if (this._state == SqlStatement.SqlState.CNTQRY || this._state == SqlStatement.SqlState.OPNQRY)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "SqlStatement::SwitchOut(): SqlStatement could not be switched out: " + this._state.ToString());
				}
				throw this._requester.MakeException(RequesterResource.SqlStatementInUse, "HY000", -1032);
			}
			this._state = SqlStatement.SqlState.Initialized;
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x0600494F RID: 18767 RVA: 0x0010DAAC File Offset: 0x0010BCAC
		public string CursorName
		{
			get
			{
				if (this._cursorName == null)
				{
					if (this._requester.Flavor == DrdaFlavor.Informix)
					{
						this._cursorName = "SQL_CURSN500C" + this._section.ToString();
					}
					else
					{
						switch (this._requester.TransactionManager.IsolationLevel)
						{
						case Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationCHG:
							this._cursorName = "SQLCURUR" + this._section.ToString();
							break;
						case Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationCS:
							this._cursorName = "SQLCURCS" + this._section.ToString();
							break;
						case Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationALL:
							this._cursorName = "SQLCURRS" + this._section.ToString();
							break;
						case Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationRR:
							this._cursorName = "SQLCURRR" + this._section.ToString();
							break;
						case Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationNC:
							this._cursorName = "SQLCURNC" + this._section.ToString();
							break;
						}
					}
				}
				if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this._tracePoint.Trace(TraceFlags.Verbose, "SqlStatement::CursorName: " + this._cursorName);
				}
				return this._cursorName;
			}
		}

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06004950 RID: 18768 RVA: 0x0010DBE9 File Offset: 0x0010BDE9
		// (set) Token: 0x06004951 RID: 18769 RVA: 0x0010DBF1 File Offset: 0x0010BDF1
		public SqlAttribute SqlAttribute
		{
			get
			{
				return this._sqlAttribute;
			}
			set
			{
				if (this._sqlAttribute != value)
				{
					this._sqlAttribute = value;
					this._isPrepared = false;
				}
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06004952 RID: 18770 RVA: 0x0010DC0A File Offset: 0x0010BE0A
		public int AffectedRowCount
		{
			get
			{
				return this._affectedRowCount;
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06004953 RID: 18771 RVA: 0x0010DC12 File Offset: 0x0010BE12
		public IRequester Requester
		{
			get
			{
				return this._requester;
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06004954 RID: 18772 RVA: 0x0010DC1A File Offset: 0x0010BE1A
		public IList<IResultSet> ResultSets
		{
			get
			{
				return this._resultSetList;
			}
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06004955 RID: 18773 RVA: 0x0010DC22 File Offset: 0x0010BE22
		public List<ISqlParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x0010DC2C File Offset: 0x0010BE2C
		public async Task ExecuteAsync(string statement, List<ISqlParameter> parameters, bool isExecReader, bool identityInsert, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlStatement::ExecuteAsync");
			}
			await this._requester.Enter(isAsync, cancellationToken);
			try
			{
				this.SetStatements(statement);
				if (this._statementList.Count > 1)
				{
					this.turnOffAutoCommit = true;
					int currentParameterIndex = 0;
					foreach (string text in this._statementList)
					{
						this.CommandText = text;
						List<ISqlParameter> list = null;
						if (this._parser.ParameterNumber > 0)
						{
							list = new List<ISqlParameter>(this._parser.ParameterNumber);
							for (int i = 0; i < this._parser.ParameterNumber; i++)
							{
								list.Add(parameters[currentParameterIndex + i]);
							}
							currentParameterIndex += this._parser.ParameterNumber;
						}
						await this.InternalExecuteAsync(text, list, isExecReader, identityInsert, isAsync, cancellationToken);
					}
					List<string>.Enumerator enumerator = default(List<string>.Enumerator);
					if (this._requester.AutoCommit)
					{
						await this._requester.LocalTransactionManager.CommitAsync(isAsync, cancellationToken);
					}
				}
				else
				{
					await this.InternalExecuteAsync(statement, parameters, isExecReader, identityInsert, isAsync, cancellationToken);
				}
			}
			finally
			{
				this.turnOffAutoCommit = false;
				this._requester.Leave();
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlStatement::ExecuteAsync");
			}
		}

		// Token: 0x06004957 RID: 18775 RVA: 0x0010DCA4 File Offset: 0x0010BEA4
		public async Task InsertRowsAsync(List<object[]> rows, bool isAsync, CancellationToken cancellationToken)
		{
			await this._requester.Enter(isAsync, cancellationToken);
			try
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Debug))
				{
					this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlStatement::InsertRowsAsync");
				}
				if (this._requester.State != Microsoft.HostIntegration.Drda.Requester.Requester.RequesterState.Accrdb)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "Requester has not connected to server yet.");
					}
					throw this._requester.MakeException(RequesterResource.RequesterNotConnected, "HY000", 8);
				}
				if (rows == null || rows.Count<object[]>() == 0)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "InsertRows need non-empty rows.");
					}
					throw this._requester.MakeException(RequesterResource.EmptyRows, "HY000", 8);
				}
				if (this._statementList.Count > 1)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "InsertRows doesn't support multiple statements.");
					}
					throw this._requester.MakeException(RequesterResource.MultipleStatementsNotSupported, "HY000", 8);
				}
				this.GetSection(false);
				if (!this._isPrepared && this._preparable)
				{
					await this.InternalPrepareAsync(this.CommandText, isAsync, cancellationToken);
				}
				await this.InternalGetParametersAsync(this.CommandText, isAsync, cancellationToken);
				this._affectedRowCount = await this._requester.SqlManager.InsertRowsAsync(this, rows, isAsync, cancellationToken);
				TaskAwaiter<bool> taskAwaiter = this.CreatePackageOnTheFly(isAsync, cancellationToken).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<bool> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (taskAwaiter.GetResult())
				{
					this._affectedRowCount = await this._requester.SqlManager.InsertRowsAsync(this, rows, isAsync, cancellationToken);
				}
				if (this._requester.AutoCommit && this._requester.HostType != HostType.AS400)
				{
					await this._requester.LocalTransactionManager.CommitAsync(isAsync, cancellationToken);
					this.Committed = true;
				}
				this._state = SqlStatement.SqlState.EXCSQLSTT;
			}
			finally
			{
				this._requester.Leave();
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlStatement::InsertRowsAsync");
			}
		}

		// Token: 0x06004958 RID: 18776 RVA: 0x0010DD04 File Offset: 0x0010BF04
		public async Task PrepareAsync(string statement, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlStatement::PrepareAsync");
			}
			await this._requester.Enter(isAsync, cancellationToken);
			try
			{
				this.SetStatements(statement);
				if (this._statementList.Count > 1)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "SqlStatement::PrepareAsync: Cannot do prepare for multiple statement: " + this._statements);
					}
					return;
				}
				await this.InternalPrepareAsync(statement, isAsync, cancellationToken);
			}
			finally
			{
				this._requester.Leave();
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlStatement::PrepareAsync");
			}
		}

		// Token: 0x06004959 RID: 18777 RVA: 0x0010DD64 File Offset: 0x0010BF64
		private async Task InternalPrepareAsync(string statement, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._requester.State != Microsoft.HostIntegration.Drda.Requester.Requester.RequesterState.Accrdb)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "Requester has not connected to server yet.");
				}
				throw this._requester.MakeException(RequesterResource.RequesterNotConnected, "HY000", 8);
			}
			this.CommandText = statement;
			if (!this._isPrepared)
			{
				if (this._preparable)
				{
					this.GetSection(false);
					if (this._section != PackageManager.ProcedureSectionNumuber)
					{
						await this.SetDefaultQualifierSet(isAsync, cancellationToken);
					}
					await this._requester.SqlManager.SubmitPrpsqlstt(this, isAsync, cancellationToken);
					TaskAwaiter<bool> taskAwaiter = this.CreatePackageOnTheFly(isAsync, cancellationToken).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (taskAwaiter.GetResult())
					{
						await this._requester.SqlManager.SubmitPrpsqlstt(this, isAsync, cancellationToken);
					}
					this._state = SqlStatement.SqlState.PRPSQLSTT;
					this._isPrepared = true;
				}
			}
		}

		// Token: 0x0600495A RID: 18778 RVA: 0x0010DDC4 File Offset: 0x0010BFC4
		public async Task<IList<ISqlParameter>> GetParametersAsync(string statement, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlStatement::GetParametersAsync");
			}
			IList<ISqlParameter> parameterList = null;
			await this._requester.Enter(isAsync, cancellationToken);
			try
			{
				this.SetStatements(statement);
				if (this._statementList.Count > 1)
				{
					parameterList = new List<ISqlParameter>();
					foreach (string text in this._statementList)
					{
						this.CommandText = text;
						IList<ISqlParameter> list = await this.InternalGetParametersAsync(text, isAsync, cancellationToken);
						((List<ISqlParameter>)parameterList).AddRange(list);
					}
					List<string>.Enumerator enumerator = default(List<string>.Enumerator);
				}
				else
				{
					parameterList = await this.InternalGetParametersAsync(statement, isAsync, cancellationToken);
				}
			}
			finally
			{
				this._requester.Leave();
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlStatement::GetParametersAsync");
			}
			return parameterList;
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x0010DE24 File Offset: 0x0010C024
		private async Task<IList<ISqlParameter>> InternalGetParametersAsync(string statement, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._requester.State != Microsoft.HostIntegration.Drda.Requester.Requester.RequesterState.Accrdb)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "Requester has not connected to server yet.");
				}
				throw this._requester.MakeException(RequesterResource.RequesterNotConnected, "HY000", 8);
			}
			IList<ISqlParameter> list;
			if (this._isDescribed)
			{
				list = this._parameterList;
			}
			else
			{
				if (!this._isPrepared && this._preparable)
				{
					await this.InternalPrepareAsync(statement, isAsync, cancellationToken);
				}
				if (!this._isDescribable)
				{
					list = this._parameterList;
				}
				else
				{
					this._isDescribed = false;
					this._parameterList.Clear();
					if (this._isPrepared)
					{
						await this._requester.SqlManager.SubmitDscsqlstt(this, isAsync, cancellationToken);
						TaskAwaiter<bool> taskAwaiter = this.CreatePackageOnTheFly(isAsync, cancellationToken).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							await taskAwaiter;
							TaskAwaiter<bool> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<bool>);
						}
						if (taskAwaiter.GetResult())
						{
							await this._requester.SqlManager.SubmitDscsqlstt(this, isAsync, cancellationToken);
						}
						this._state = SqlStatement.SqlState.DSCSQLSTT;
						this._isDescribed = true;
					}
					list = this._parameterList;
				}
			}
			return list;
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x0010DE84 File Offset: 0x0010C084
		public async Task CloseAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this._requester.Enter(isAsync, cancellationToken);
			try
			{
				await this.CloseAsync(this._statementList.Count > 1, isAsync, cancellationToken);
			}
			finally
			{
				this._requester.Leave();
			}
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x0010DEDC File Offset: 0x0010C0DC
		internal async Task CloseAsync(bool isQuiet, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlStatement::CloseAsync");
			}
			if (this._requester.State != Microsoft.HostIntegration.Drda.Requester.Requester.RequesterState.Accrdb)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Warning))
				{
					this._tracePoint.Trace(TraceFlags.Warning, "SqlStatement::CloseAsync: Requester is not in Accrdb: " + this._requester.State.ToString());
				}
			}
			else
			{
				if (!this.Committed)
				{
					if (this._state == SqlStatement.SqlState.OPNQRY || this._state == SqlStatement.SqlState.CNTQRY)
					{
						if (this._requester.HostType == HostType.DB2 || this._requester.HostType == HostType.MVS || this._requester.HostType == HostType.AS400)
						{
							await this._requester.SqlManager.SubmitClsqry(this, this._currentResultSet, isQuiet, isAsync, cancellationToken);
						}
						else
						{
							await this._requester.SqlManager.SubmitClsqry(this, this._currentResultSet, true, isAsync, cancellationToken);
						}
					}
					else
					{
						foreach (IResultSet resultSet in this._resultSetList)
						{
							SqlResultSet sqlResultSet = (SqlResultSet)resultSet;
							if (sqlResultSet.State == SqlStatement.SqlState.OPNQRY || sqlResultSet.State == SqlStatement.SqlState.CNTQRY)
							{
								await this._requester.SqlManager.SubmitClsqry(this, sqlResultSet, isQuiet, isAsync, cancellationToken);
							}
						}
						List<IResultSet>.Enumerator enumerator = default(List<IResultSet>.Enumerator);
					}
				}
				if (this.NeedCommit && this._requester.AutoCommit && this._requester.HostType != HostType.AS400 && this.CanCommitNow)
				{
					await this._requester.LocalTransactionManager.CommitAsync(isAsync, cancellationToken);
					this.Committed = true;
				}
				this._requester.LobLength.Clear();
				this._requester.Mode.Clear();
				if (this._requester.ProgRef != null)
				{
					this._requester.ProgRef = null;
				}
				this.Reset();
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "Exit SqlStatement::CloseAsync");
				}
			}
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x0010DF39 File Offset: 0x0010C139
		public void Reset()
		{
			this.InternalReset(true, false);
		}

		// Token: 0x0600495F RID: 18783 RVA: 0x0010DF44 File Offset: 0x0010C144
		internal void InternalReset(bool fullReset, bool prepared)
		{
			this._cursorName = null;
			this._affectedRowCount = -1;
			this._state = SqlStatement.SqlState.Initialized;
			this._pkgnamcsn = null;
			this._parameters = null;
			this._commandText = null;
			this._isPrepared = false;
			this._preparable = true;
			this._isDescribed = false;
			this._isDescribable = true;
			this._parameterList.Clear();
			this._parser.Reset();
			this._overrideStatementType = Parser.StatementType.Unknown;
			this._overrideCommandText = null;
			this.Committed = false;
			this.IsReader = false;
			this.NeedCommit = false;
			this.CurrentErrorSqlca = null;
			if (!prepared)
			{
				this.ReleaseSection();
			}
			if (fullReset)
			{
				this._commandSourceId = 0L;
				this.LiteralReplacementInvestigator = null;
				this._sqlAttribute = SqlAttribute.Null;
				this._resultSetList.Clear();
				this._currentResultSet = null;
				this._statementList.Clear();
				this._statements = null;
			}
		}

		// Token: 0x06004960 RID: 18784 RVA: 0x0010E020 File Offset: 0x0010C220
		internal void ProcessSqldard(SQLDARD sqldard, SqlStatement.SqlState towardState)
		{
			if (towardState == SqlStatement.SqlState.PRPSQLSTT)
			{
				if (sqldard.SqlNum > 0)
				{
					if (this._currentResultSet == null)
					{
						this._currentResultSet = new SqlResultSet(this);
						this._resultSetList.Add(this._currentResultSet);
					}
					this._currentResultSet.SetColumnInfos(sqldard);
				}
				if (this._tracePoint.IsEnabled(TraceFlags.Debug))
				{
					this._tracePoint.Trace(TraceFlags.Debug, "Adding new result set. Total result sets = " + this._resultSetList.Count.ToString());
					return;
				}
			}
			else if (towardState == SqlStatement.SqlState.DSCSQLSTT)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Debug))
				{
					this._tracePoint.Trace(TraceFlags.Debug, "Creating parameter list, count = " + sqldard.SqlNum.ToString());
				}
				if (sqldard.SqlNum > 0)
				{
					this._parameterList.Clear();
					for (int i = 0; i < (int)sqldard.SqlNum; i++)
					{
						SQLDAGRP sqldagrp = sqldard.ListSqldagrp[i];
						SqlParameter parameter = new SqlParameter();
						if (sqldagrp.SqldOptGrp != null && !string.IsNullOrWhiteSpace(sqldagrp.SqldOptGrp.SqlName))
						{
							parameter.ParameterName = sqldagrp.SqldOptGrp.SqlName;
						}
						else
						{
							parameter.ParameterName = string.Empty;
						}
						parameter.Scale = (byte)sqldagrp.SqlScale;
						parameter.Size = (int)sqldagrp.SqlLength;
						parameter.Precision = (byte)sqldagrp.SqlPrecision;
						parameter.Ccsid = (ushort)sqldagrp.SqlCcsid;
						parameter.IsNullable = SqlType.IsNullable(sqldagrp.SqlType);
						parameter.SqlType = sqldagrp.SqlType;
						SqlType.ProcessSqlType(parameter.SqlType, parameter.Size, parameter.Ccsid, 0, this._requester, delegate(DrdaClientType drdaClientType, short scale, short precision, int size, DbType dbType, byte drdaServerType, int dataLength)
						{
							parameter.DrdaType = drdaClientType;
							if (scale > 0)
							{
								parameter.Scale = (byte)scale;
							}
							if (precision > 0)
							{
								parameter.Precision = (byte)precision;
							}
							if (size > 0)
							{
								parameter.Size = size;
							}
							parameter.DbType = dbType;
							parameter.DrdaServerType = drdaServerType;
						});
						this._parameterList.Add(parameter);
						if (this._tracePoint.IsEnabled(TraceFlags.Debug))
						{
							this._tracePoint.Trace(TraceFlags.Debug, "Added parameter: " + (string.IsNullOrEmpty(parameter.ParameterName) ? i.ToString() : parameter.ParameterName));
						}
					}
					return;
				}
				if (this._tracePoint.IsEnabled(TraceFlags.Warning))
				{
					this._tracePoint.Trace(TraceFlags.Warning, "No parameter in the SQLDARD!");
				}
			}
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x0010E2A0 File Offset: 0x0010C4A0
		internal void SetDrdaInfoToResultSet(int columnCount, int columnIndex, byte drdaType, ushort sqlLength, ushort ccsid)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, string.Format("Set Drda Info: columnIndex={0}, drdaType={1}, sqlLength={2}", columnIndex, drdaType, sqlLength));
			}
			if (this._currentResultSet != null)
			{
				this._currentResultSet.SetColumnDrdaInfo(columnCount, columnIndex, drdaType, sqlLength, ccsid);
				return;
			}
			throw this._requester.MakeException(RequesterResource.NoCurrentResultSet, "HY000", -343);
		}

		// Token: 0x06004962 RID: 18786 RVA: 0x0010E31C File Offset: 0x0010C51C
		internal void ProcessOpnqryrm(OPNQRYRM opnqryrm)
		{
			if (this._currentResultSet == null || (this._currentResultSet.QueryInstanceId != opnqryrm.Qryinsid && this._currentResultSet.QueryInstanceId != -1L))
			{
				this._currentResultSet = new SqlResultSet(this);
				this._resultSetList.Add(this._currentResultSet);
			}
			this._currentResultSet.QueryInstanceId = opnqryrm.Qryinsid;
			this._currentResultSet.IsCursorScrollable = opnqryrm.Qryattscr;
			this._currentResultSet.State = SqlStatement.SqlState.OPNQRY;
		}

		// Token: 0x06004963 RID: 18787 RVA: 0x0010E39E File Offset: 0x0010C59E
		internal void CreateNewCurrentResultSet()
		{
			this._currentResultSet = new SqlResultSet(this);
			this._resultSetList.Add(this._currentResultSet);
		}

		// Token: 0x06004964 RID: 18788 RVA: 0x0010E3BD File Offset: 0x0010C5BD
		internal void ClearResultSets()
		{
			this._resultSetList.Clear();
			this._currentResultSet = null;
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x0010E3D1 File Offset: 0x0010C5D1
		internal void ProcessEndqryrm()
		{
			if (this._currentResultSet != null)
			{
				this.ProcessEndqryrm(this._currentResultSet);
				this._currentResultSet = null;
				return;
			}
			throw this._requester.MakeException(RequesterResource.NoCurrentResultSet, "HY000", -343);
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x0010E40B File Offset: 0x0010C60B
		internal void ProcessEndqryrm(SqlResultSet resultSet)
		{
			resultSet.EndOfQuery = true;
			resultSet.State = SqlStatement.SqlState.CLSQRY;
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x0010E41B File Offset: 0x0010C61B
		internal void AddSqlParameter(ISqlParameter parameter)
		{
			this._parameterList.Add(parameter);
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06004968 RID: 18792 RVA: 0x0010E429 File Offset: 0x0010C629
		internal Parser.StatementType StatementType
		{
			get
			{
				if (this._overrideStatementType != Parser.StatementType.Unknown)
				{
					return this._overrideStatementType;
				}
				return this._parser.Type;
			}
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06004969 RID: 18793 RVA: 0x0010E446 File Offset: 0x0010C646
		internal string ProcedureName
		{
			get
			{
				if (this._parser.Type != Parser.StatementType.Call)
				{
					return null;
				}
				return this._parser.QualifiedName;
			}
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x0600496A RID: 18794 RVA: 0x0010E464 File Offset: 0x0010C664
		internal string SqlAttributeString
		{
			get
			{
				switch (this._sqlAttribute)
				{
				case SqlAttribute.Atomic:
					return "FOR MUTIPLE ROWS ATOMIC";
				case SqlAttribute.NotAtomic:
					return "FOR MULTIPLE ROWS NOT ATOMIC CONTINUE ON SQLEXCEPTION";
				case SqlAttribute.ReadOnly:
					return "FOR READ ONLY";
				case SqlAttribute.FetchOnly:
					return "FOR FETCH ONLY";
				case SqlAttribute.ForUpdate:
					return "FOR UPDATE";
				case SqlAttribute.ExternalIndicators:
					return "WITH EXTENDED INDICATORS";
				case SqlAttribute.ExternalIndicatorsMultiRow:
					return "FOR MULTIPLE ROWS NOT ATOMIC CONTINUE ON SQLEXCEPTION WITH EXTENDED INDICATORS";
				default:
					return null;
				}
			}
		}

		// Token: 0x0600496B RID: 18795 RVA: 0x0010E4CC File Offset: 0x0010C6CC
		internal void UpdateResultSetsWithPackageList(List<PKGNAMCSN> pkgsnList)
		{
			for (int i = 0; i < this._resultSetList.Count; i++)
			{
				((SqlResultSet)this._resultSetList[i]).Pkgnamcsn = pkgsnList[i];
			}
		}

		// Token: 0x0600496C RID: 18796 RVA: 0x0010E50C File Offset: 0x0010C70C
		internal async Task InternalExecuteSetAsync(List<string> settings, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlStatement::InternalExecuteSetAsync");
			}
			if (this._requester.State != Microsoft.HostIntegration.Drda.Requester.Requester.RequesterState.Accrdb)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "Requester has not connected to server yet.");
				}
				throw this._requester.MakeException(RequesterResource.RequesterNotConnected, "HY000", 8);
			}
			this.GetSection(false);
			await this._requester.SqlManager.SubmitExcsqlset(this, settings, isAsync, cancellationToken);
			TaskAwaiter<bool> taskAwaiter = this.CreatePackageOnTheFly(isAsync, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<bool> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<bool>);
			}
			if (taskAwaiter.GetResult())
			{
				await this._requester.SqlManager.SubmitExcsqlset(this, settings, isAsync, cancellationToken);
			}
			this._state = SqlStatement.SqlState.EXCSQLSET;
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlStatement::InternalExecuteSetAsync");
			}
		}

		// Token: 0x0600496D RID: 18797 RVA: 0x0010E56C File Offset: 0x0010C76C
		internal static async Task<string> GetSqlcaMessage(SQLCAGRP sqlca, SqlStatement failedStatement, Manager callingMananger, bool isAsync, CancellationToken cancellationToken)
		{
			if (callingMananger.TracePoint.IsEnabled(TraceFlags.Debug))
			{
				callingMananger.TracePoint.Trace(TraceFlags.Debug, "Enter SqlStatement::GetSqlcaMessage");
			}
			string sqlCaStoredProcedureCallStatement = SqlStatement.GetSqlCaStoredProcedureCallStatement(callingMananger.Requester.Flavor);
			string text;
			if (failedStatement != null && string.Compare(failedStatement.CommandText, sqlCaStoredProcedureCallStatement) == 0)
			{
				if (callingMananger.TracePoint.IsEnabled(TraceFlags.Warning))
				{
					callingMananger.TracePoint.Trace(TraceFlags.Warning, "SqlStatement::GetSqlcaMessage failed on SqlCaStoredProcedureCall.");
				}
				text = sqlca.SqlErrorMessage;
			}
			else
			{
				if (string.IsNullOrEmpty(sqlca.SqlErrorMessage) && callingMananger.TracePoint.IsEnabled(TraceFlags.Warning))
				{
					callingMananger.TracePoint.Trace(TraceFlags.Warning, "SqlStatement::GetSqlcaMessage No error message.");
				}
				string errorMessage = sqlca.SqlErrorMessage;
				try
				{
					SqlStatement sqlStatement = new SqlStatement(callingMananger.Requester);
					List<ISqlParameter> parameters = new List<ISqlParameter>(16);
					parameters.Add(new SqlParameter
					{
						DrdaType = DrdaClientType.Int,
						Value = sqlca.SqlCode
					});
					SqlParameter sqlParameter = new SqlParameter();
					sqlParameter.DrdaType = DrdaClientType.SmallInt;
					if (string.IsNullOrEmpty(sqlca.SqlErrorMessage))
					{
						sqlParameter.Value = 0;
					}
					else
					{
						sqlParameter.Value = sqlca.SqlErrorMessage.Length;
					}
					parameters.Add(sqlParameter);
					sqlParameter = new SqlParameter();
					sqlParameter.DrdaType = DrdaClientType.VarChar;
					if (string.IsNullOrEmpty(sqlca.SqlErrorMessage))
					{
						sqlParameter.Value = DBNull.Value;
					}
					else
					{
						StringBuilder stringBuilder = new StringBuilder(sqlca.SqlErrorMessage.Length);
						for (int i = 0; i < sqlca.SqlErrorMessage.Length; i++)
						{
							char c = sqlca.SqlErrorMessage[i];
							if ((c & '\uff00') == '\uff00')
							{
								c = ';';
							}
							stringBuilder.Append(c);
						}
						sqlParameter.Value = stringBuilder.ToString();
					}
					parameters.Add(sqlParameter);
					parameters.Add(new SqlParameter
					{
						DrdaType = DrdaClientType.VarChar,
						Value = sqlca.SqlServerName
					});
					parameters.Add(new SqlParameter
					{
						DrdaType = DrdaClientType.Int,
						Value = sqlca.SqlErrorCode[0]
					});
					parameters.Add(new SqlParameter
					{
						DrdaType = DrdaClientType.Int,
						Value = sqlca.SqlErrorCode[1]
					});
					parameters.Add(new SqlParameter
					{
						DrdaType = DrdaClientType.Int,
						Value = sqlca.SqlErrorCode[2]
					});
					parameters.Add(new SqlParameter
					{
						DrdaType = DrdaClientType.Int,
						Value = sqlca.SqlErrorCode[3]
					});
					parameters.Add(new SqlParameter
					{
						DrdaType = DrdaClientType.Int,
						Value = sqlca.SqlErrorCode[4]
					});
					parameters.Add(new SqlParameter
					{
						DrdaType = DrdaClientType.Int,
						Value = sqlca.SqlErrorCode[5]
					});
					parameters.Add(new SqlParameter
					{
						DrdaType = DrdaClientType.VarChar,
						Value = sqlca.SqlWarningMessage
					});
					parameters.Add(new SqlParameter
					{
						DrdaType = DrdaClientType.VarChar,
						Value = sqlca.SqlState
					});
					parameters.Add(new SqlParameter
					{
						DrdaType = DrdaClientType.VarChar,
						Value = DBNull.Value
					});
					parameters.Add(new SqlParameter
					{
						DrdaType = DrdaClientType.VarChar,
						Value = DBNull.Value,
						Direction = ParameterDirection.InputOutput
					});
					parameters.Add(new SqlParameter
					{
						DrdaType = DrdaClientType.VarChar,
						Value = DBNull.Value,
						Direction = ParameterDirection.Output
					});
					parameters.Add(new SqlParameter
					{
						DrdaType = DrdaClientType.Int,
						Value = DBNull.Value,
						Direction = ParameterDirection.Output
					});
					if (callingMananger.Requester.Flavor == DrdaFlavor.Informix)
					{
						parameters.Add(new SqlParameter
						{
							DrdaType = DrdaClientType.Int,
							Value = callingMananger.Requester.CcsidRead._ccsidsbc,
							Direction = ParameterDirection.InputOutput
						});
					}
					callingMananger.Requester.ExternalExceptionMakerEnabled = false;
					await sqlStatement.InternalExecuteAsync(sqlCaStoredProcedureCallStatement, parameters, false, false, isAsync, cancellationToken);
					if (callingMananger.TracePoint.IsEnabled(TraceFlags.Information))
					{
						callingMananger.TracePoint.Trace(TraceFlags.Information, "SqlStatement::GetSqlcaMessage: " + sqlca.ToString());
					}
					errorMessage = parameters[14].Value as string;
					await sqlStatement.CloseAsync(true, isAsync, cancellationToken);
					sqlStatement = null;
					parameters = null;
				}
				catch (DrdaRequesterException ex)
				{
					if (callingMananger.TracePoint.IsEnabled(TraceFlags.Warning))
					{
						callingMananger.TracePoint.Trace(TraceFlags.Warning, ex.ToString());
					}
				}
				catch (Exception ex2)
				{
					if (callingMananger.TracePoint.IsEnabled(TraceFlags.Error))
					{
						callingMananger.TracePoint.Trace(TraceFlags.Error, ex2.ToString());
					}
					throw ex2;
				}
				finally
				{
					callingMananger.Requester.ExternalExceptionMakerEnabled = true;
				}
				if (callingMananger.TracePoint.IsEnabled(TraceFlags.Debug))
				{
					callingMananger.TracePoint.Trace(TraceFlags.Debug, "Exit SqlStatement::GetSqlcaMessage");
				}
				text = errorMessage;
			}
			return text;
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x0010E5D4 File Offset: 0x0010C7D4
		private void GetSection(bool isExecSqlImm)
		{
			if (this._section == 0)
			{
				if (this._requester.HostType == HostType.DB2 || this._requester.HostType == HostType.MVS)
				{
					if (this._parser.Type == Parser.StatementType.Call)
					{
						this._section = PackageManager.ProcedureSectionNumuber;
					}
					else if (isExecSqlImm && (this._parser.Type == Parser.StatementType.Delete || this._parser.Type == Parser.StatementType.Insert || this._parser.Type == Parser.StatementType.Update || this._parser.Type == Parser.StatementType.Grant))
					{
						this._section = PackageManager.PhantomSectionNumuber;
					}
				}
				if (this._section == 0)
				{
					this._section = this._requester.PackageManager.GetSection();
				}
				if (this._section == 0)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "SqlStatement::GetSection:  No package section available.");
					}
					throw this._requester.MakeException(RequesterResource.MaxSectionsReached, "HY000", -1500);
				}
			}
			else if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "SqlStatement::GetSection:  Package section already exists.");
			}
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x0010E6E9 File Offset: 0x0010C8E9
		private void ReleaseSection()
		{
			if (this._section > 0)
			{
				this._requester.PackageManager.ReleaseSection(this._section);
				this._section = 0;
			}
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x0010E714 File Offset: 0x0010C914
		private async Task SetDefaultQualifierSet(bool isAsync, CancellationToken cancellationToken)
		{
			if (this._requester.NeedSetDefaultQualifierSet)
			{
				this._requester.NeedSetDefaultQualifierSet = false;
				try
				{
					SqlStatement sqlStatement = new SqlStatement(this._requester);
					string text = this._requester.ConnectionInfo[19];
					string sqlQualifierSet = string.Format("SET CURRENT SQLID = '{0}'", text.PadRight(8, ' '));
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "SqlStatement::SetDefaultQualifierSet: " + sqlQualifierSet);
					}
					await sqlStatement.InternalExecuteAsync(sqlQualifierSet, null, false, false, isAsync, cancellationToken);
					TaskAwaiter<bool> taskAwaiter = this.CreatePackageOnTheFly(isAsync, cancellationToken).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (taskAwaiter.GetResult())
					{
						await sqlStatement.InternalExecuteAsync(sqlQualifierSet, null, false, false, isAsync, cancellationToken);
					}
					await sqlStatement.CloseAsync(true, isAsync, cancellationToken);
					sqlStatement = null;
					sqlQualifierSet = null;
				}
				catch (Exception ex)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, ex.ToString());
					}
					this._requester.NeedSetDefaultQualifierSet = true;
					throw ex;
				}
			}
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x0010E76C File Offset: 0x0010C96C
		internal async Task InternalExecuteAsync(string statement, List<ISqlParameter> parameters, bool isExecReader, bool identityInsert, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter SqlStatement::InternalExecuteAsync");
			}
			this.CommandText = statement;
			if (this._requester.State != Microsoft.HostIntegration.Drda.Requester.Requester.RequesterState.Accrdb)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "Requester has not connected to server yet.");
				}
				throw this._requester.MakeException(RequesterResource.RequesterNotConnected, "HY000", 8);
			}
			this._parameters = parameters;
			this.IsReader = isExecReader;
			if ((!identityInsert && !this._isPrepared && (this._parser.Type == Parser.StatementType.Delete || this._parser.Type == Parser.StatementType.Insert || this._parser.Type == Parser.StatementType.Update) && (parameters == null || parameters.Count == 0)) || this._parser.Type == Parser.StatementType.Set || this._parser.Type == Parser.StatementType.Create || this._parser.Type == Parser.StatementType.Drop || this._parser.Type == Parser.StatementType.Grant)
			{
				this.GetSection(true);
				if (this._parser.Type == Parser.StatementType.Delete || this._parser.Type == Parser.StatementType.Insert || this._parser.Type == Parser.StatementType.Update)
				{
					await this.SetDefaultQualifierSet(isAsync, cancellationToken);
				}
				this._affectedRowCount = await this._requester.SqlManager.SubmitExcsqlimm(this, isAsync, cancellationToken);
				TaskAwaiter<bool> taskAwaiter = this.CreatePackageOnTheFly(isAsync, cancellationToken).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<bool> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (taskAwaiter.GetResult())
				{
					this._affectedRowCount = await this._requester.SqlManager.SubmitExcsqlimm(this, isAsync, cancellationToken);
				}
				this._state = SqlStatement.SqlState.EXCSQLIMM;
			}
			else
			{
				this.GetSection(false);
				bool found = false;
				TaskAwaiter<bool> taskAwaiter2;
				if (identityInsert && this._parser.Type == Parser.StatementType.Insert)
				{
					this._overrideCommandText = string.Format("SELECT * FROM FINAL TABLE ( {0} )", this._commandText);
					this._overrideStatementType = Parser.StatementType.Select;
					await this._requester.SqlManager.SubmitPrpsqlstt(this, isAsync, cancellationToken);
					TaskAwaiter<bool> taskAwaiter = this.CreatePackageOnTheFly(isAsync, cancellationToken).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (taskAwaiter.GetResult())
					{
						await this._requester.SqlManager.SubmitPrpsqlstt(this, isAsync, cancellationToken);
					}
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("SELECT ");
					if (this._currentResultSet != null && this._currentResultSet.ColumnInfos != null)
					{
						for (int i = 0; i < this._currentResultSet.ColumnInfos.Length; i++)
						{
							SqlColumnInfo sqlColumnInfo = (SqlColumnInfo)this._currentResultSet.ColumnInfos[i];
							if (sqlColumnInfo.GeneratedIdType > 0)
							{
								if (found)
								{
									stringBuilder.Append(", ");
								}
								else
								{
									found = true;
								}
								stringBuilder.Append(sqlColumnInfo.ColumnName);
							}
						}
					}
					if (found)
					{
						stringBuilder.Append(" FROM FINAL TABLE (");
						stringBuilder.Append(this._commandText);
						stringBuilder.Append(")");
						this._overrideCommandText = stringBuilder.ToString();
					}
					this.NeedCommit = true;
				}
				if (!found)
				{
					this._overrideCommandText = null;
					this._overrideStatementType = Parser.StatementType.Unknown;
				}
				if (!this._isPrepared && this._preparable)
				{
					await this.InternalPrepareAsync(this.CommandText, isAsync, cancellationToken);
				}
				if (parameters != null && parameters.Count > 0)
				{
					await this.InternalGetParametersAsync(this.CommandText, isAsync, cancellationToken);
				}
				if (this.StatementType == Parser.StatementType.Select || (isExecReader && this.StatementType == Parser.StatementType.Static))
				{
					this._affectedRowCount = await this._requester.SqlManager.SubmitOpnqry(this, isAsync, cancellationToken);
					TaskAwaiter<bool> taskAwaiter = this.CreatePackageOnTheFly(isAsync, cancellationToken).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (taskAwaiter.GetResult())
					{
						this._affectedRowCount = await this._requester.SqlManager.SubmitOpnqry(this, isAsync, cancellationToken);
					}
					if (this._state != SqlStatement.SqlState.CLSQRY)
					{
						this._state = SqlStatement.SqlState.OPNQRY;
					}
				}
				else
				{
					this._affectedRowCount = await this._requester.SqlManager.SubmitExcsqlstt(this, isAsync, cancellationToken);
					TaskAwaiter<bool> taskAwaiter = this.CreatePackageOnTheFly(isAsync, cancellationToken).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (taskAwaiter.GetResult())
					{
						this._affectedRowCount = await this._requester.SqlManager.SubmitExcsqlstt(this, isAsync, cancellationToken);
					}
					this._state = SqlStatement.SqlState.EXCSQLSTT;
				}
				if (found)
				{
					if (this._resultSetList.Count != 1 && this._tracePoint.IsEnabled(TraceFlags.Warning))
					{
						this._tracePoint.Trace(TraceFlags.Warning, "Resultset is over one: " + this._resultSetList.Count.ToString());
					}
					this._overrideCommandText = null;
					this._overrideStatementType = Parser.StatementType.Unknown;
				}
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit SqlStatement::InternalExecuteAsync");
			}
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x0010E7E4 File Offset: 0x0010C9E4
		private async Task<bool> CreatePackageOnTheFly(bool isAsync, CancellationToken cancellationToken)
		{
			bool flag;
			if (this.SqlcaCode == -805)
			{
				try
				{
					await this._requester.PackageManager.SetupHostPackagesAsync(this._requester.TransactionManager.IsolationLevel, isAsync, cancellationToken);
				}
				catch (Exception ex)
				{
					if (this.CurrentErrorSqlca != null)
					{
						SQLCAGRP currentErrorSqlca = this.CurrentErrorSqlca;
						this.CurrentErrorSqlca = null;
						throw this._requester.MakeException(RequesterResource.PackageNotPresent, currentErrorSqlca.SqlState, currentErrorSqlca.SqlCode);
					}
					throw ex;
				}
				this.Committed = false;
				this.NeedCommit = false;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x0010E839 File Offset: 0x0010CA39
		private static string GetSqlCaStoredProcedureCallStatement(DrdaFlavor flavor)
		{
			if (flavor == DrdaFlavor.Informix)
			{
				return "CALL SYSIBM.SQLCAMESSAGECCSID(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
			}
			return "CALL SYSIBM.SQLCAMESSAGE(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
		}

		// Token: 0x0400363E RID: 13886
		private const string _sqlCaStoredProcedureCallDb2 = "CALL SYSIBM.SQLCAMESSAGE(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

		// Token: 0x0400363F RID: 13887
		private const string _sqlCaStoredProcedureCallInformix = "CALL SYSIBM.SQLCAMESSAGECCSID(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

		// Token: 0x04003640 RID: 13888
		private string _cursorName;

		// Token: 0x04003641 RID: 13889
		private int _affectedRowCount = -1;

		// Token: 0x04003642 RID: 13890
		private Requester _requester;

		// Token: 0x04003643 RID: 13891
		private long _commandSourceId;

		// Token: 0x04003644 RID: 13892
		private SqlStatement.SqlState _state;

		// Token: 0x04003645 RID: 13893
		private string _commandText;

		// Token: 0x04003646 RID: 13894
		private PKGNAMCSN _pkgnamcsn;

		// Token: 0x04003647 RID: 13895
		private DrdaArTracePoint _tracePoint;

		// Token: 0x04003648 RID: 13896
		private int _section;

		// Token: 0x04003649 RID: 13897
		private List<ISqlParameter> _parameters;

		// Token: 0x0400364A RID: 13898
		private List<IResultSet> _resultSetList = new List<IResultSet>();

		// Token: 0x0400364B RID: 13899
		private SqlResultSet _currentResultSet;

		// Token: 0x0400364C RID: 13900
		private List<ISqlParameter> _parameterList = new List<ISqlParameter>();

		// Token: 0x0400364D RID: 13901
		private Parser _parser;

		// Token: 0x0400364E RID: 13902
		private bool _isPrepared;

		// Token: 0x0400364F RID: 13903
		private bool _isDescribed;

		// Token: 0x04003650 RID: 13904
		private bool _preparable = true;

		// Token: 0x04003651 RID: 13905
		private bool _isDescribable = true;

		// Token: 0x04003652 RID: 13906
		private SqlAttribute _sqlAttribute;

		// Token: 0x04003653 RID: 13907
		private Parser.StatementType _overrideStatementType = Parser.StatementType.Unknown;

		// Token: 0x04003654 RID: 13908
		private string _overrideCommandText;

		// Token: 0x04003655 RID: 13909
		private string _statements;

		// Token: 0x04003656 RID: 13910
		private List<string> _statementList = new List<string>();

		// Token: 0x04003658 RID: 13912
		private bool turnOffAutoCommit;

		// Token: 0x0200090E RID: 2318
		internal enum SqlState
		{
			// Token: 0x0400365F RID: 13919
			Initialized,
			// Token: 0x04003660 RID: 13920
			OPNQRY,
			// Token: 0x04003661 RID: 13921
			CNTQRY,
			// Token: 0x04003662 RID: 13922
			CLSQRY,
			// Token: 0x04003663 RID: 13923
			PRPSQLSTT,
			// Token: 0x04003664 RID: 13924
			DSCSQLSTT,
			// Token: 0x04003665 RID: 13925
			EXCSQLSTT,
			// Token: 0x04003666 RID: 13926
			EXCSQLIMM,
			// Token: 0x04003667 RID: 13927
			EXCSQLSET
		}
	}
}
