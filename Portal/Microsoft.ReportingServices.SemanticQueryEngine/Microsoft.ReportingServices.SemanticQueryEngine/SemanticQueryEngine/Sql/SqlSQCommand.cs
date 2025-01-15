using System;
using System.Data.Common;
using System.Diagnostics;
using System.Security.Permissions;
using System.Xml;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql
{
	// Token: 0x02000015 RID: 21
	public abstract class SqlSQCommand : ISemanticQueryCommand, IDbCommand, IDisposable, IExtension, ISetSemanticQueryConnection, ITraceableComponent
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00005B7C File Offset: 0x00003D7C
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public virtual void SetConfiguration(string configuration)
		{
			if (!string.IsNullOrEmpty(configuration))
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml("<Config>" + configuration + "</Config>");
					XmlElement xmlElement = xmlDocument.SelectSingleNode("//EnableMathOpCasting") as XmlElement;
					if (xmlElement != null)
					{
						this.m_enableMathOpCasting = bool.Parse(xmlElement.InnerText);
					}
				}
				catch (Exception ex)
				{
					throw new SemanticQueryEngineException(SR.InvalidConfiguration(this.LocalizedName, ex.Message));
				}
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000102 RID: 258
		public abstract string LocalizedName
		{
			[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
			get;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005BFC File Offset: 0x00003DFC
		public IDataReader ExecuteReader(CommandBehavior behavior)
		{
			if (this.m_compiledSql == null)
			{
				throw new InvalidOperationException(SR.QueryNotSet);
			}
			return this.CreateSqlSQDataReader(this.m_compiledSql, this.m_dataIsCaseSensitive, this.ExecuteTargetCommand(this.m_targetCommand, this.m_compiledSql, behavior), this.m_traceLog);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005C3C File Offset: 0x00003E3C
		public IDataParameter CreateParameter()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005C43 File Offset: 0x00003E43
		public void Cancel()
		{
			this.m_targetCommand.Cancel();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005C3C File Offset: 0x00003E3C
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00005C3C File Offset: 0x00003E3C
		public string CommandText
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005C50 File Offset: 0x00003E50
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00005C5D File Offset: 0x00003E5D
		public int CommandTimeout
		{
			get
			{
				return this.m_targetCommand.CommandTimeout;
			}
			set
			{
				this.m_targetCommand.CommandTimeout = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004B5D File Offset: 0x00002D5D
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00005C6B File Offset: 0x00003E6B
		public CommandType CommandType
		{
			get
			{
				return CommandType.Text;
			}
			set
			{
				if (value != CommandType.Text)
				{
					throw new NotSupportedException(SR.InvalidCommandType);
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005C3C File Offset: 0x00003E3C
		public IDataParameterCollection Parameters
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005C7C File Offset: 0x00003E7C
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00005C89 File Offset: 0x00003E89
		public IDbTransaction Transaction
		{
			get
			{
				return this.m_targetCommand.Transaction;
			}
			set
			{
				this.m_targetCommand.Transaction = value;
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005C97 File Offset: 0x00003E97
		public void Dispose()
		{
			this.m_targetCommand.Dispose();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005CA4 File Offset: 0x00003EA4
		void ISemanticQueryCommand.Initialize(IDbConnection targetConnection)
		{
			this.m_targetCommand = targetConnection.CreateCommand();
			this.m_targetCommand.CommandType = CommandType.Text;
			this.m_underlyingDbConnection = null;
			if (targetConnection is IDbConnectionWrapper)
			{
				this.m_underlyingDbConnection = ((IDbConnectionWrapper)targetConnection).Connection as DbConnection;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005CE4 File Offset: 0x00003EE4
		void ISemanticQueryCommand.SetQuery(SemanticQuery query)
		{
			this.m_compiledSql = null;
			this.m_targetCommand.CommandText = string.Empty;
			SqlBatch sqlBatch = this.CreateSqlBatch(query.Model, this.m_underlyingDbConnection);
			SqlQueryPlan sqlQueryPlan = new QueryPlanBuilder(query, new Predicate<DsvColumn>(sqlBatch.IsBlob)).Build();
			if (this.m_traceLog != null && this.m_traceLog.TraceVerbose)
			{
				try
				{
					this.m_traceLog.WriteTrace("QP:\r\n" + (sqlQueryPlan.Trace() ?? "empty"), TraceLevel.Verbose);
				}
				catch (Exception ex)
				{
					this.m_traceLog.WriteTrace("Error while tracing query plan: " + ex.ToString(), TraceLevel.Verbose);
				}
			}
			this.m_compiledSql = sqlQueryPlan.CompileSql(sqlBatch, query.GetAllResultExpressions());
			if (this.m_traceLog != null && this.m_traceLog.TraceInfo)
			{
				this.m_traceLog.WriteTrace("SQL:\r\n" + (this.m_compiledSql.CommandText ?? "empty"), TraceLevel.Info);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005DF4 File Offset: 0x00003FF4
		void ISetSemanticQueryConnection.SetSemanticQueryConnection(SemanticQueryConnection connection)
		{
			if (connection == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("connection"));
			}
			string text;
			bool flag;
			bool flag2;
			if (((IDbCollationProperties)connection).GetCollationProperties(out text, out flag, out flag2, out flag2, out flag2))
			{
				this.m_dataIsCaseSensitive = new bool?(flag);
				return;
			}
			this.m_dataIsCaseSensitive = null;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005E3F File Offset: 0x0000403F
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		void ITraceableComponent.SetTraceLog(ITraceLog traceLog)
		{
			this.m_traceLog = traceLog;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005E48 File Offset: 0x00004048
		protected bool EnableMathOpCasting
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_enableMathOpCasting;
			}
		}

		// Token: 0x06000115 RID: 277
		internal abstract SqlBatch CreateSqlBatch(SemanticModel model, DbConnection dbConnection);

		// Token: 0x06000116 RID: 278 RVA: 0x00005E50 File Offset: 0x00004050
		internal virtual IDataReader ExecuteTargetCommand(IDbCommand targetCommand, CompiledSql compiledSql, CommandBehavior behavior)
		{
			targetCommand.CommandText = compiledSql.CommandText;
			return targetCommand.ExecuteReader(behavior);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005E65 File Offset: 0x00004065
		internal virtual SqlSQDataReader CreateSqlSQDataReader(CompiledSql compiledSql, bool? dataIsCaseSensitive, IDataReader targetDataReader, ITraceLog traceLog)
		{
			return new SqlSQDataReader(compiledSql, dataIsCaseSensitive, targetDataReader, traceLog);
		}

		// Token: 0x04000057 RID: 87
		private IDbCommand m_targetCommand;

		// Token: 0x04000058 RID: 88
		private DbConnection m_underlyingDbConnection;

		// Token: 0x04000059 RID: 89
		private CompiledSql m_compiledSql;

		// Token: 0x0400005A RID: 90
		private bool? m_dataIsCaseSensitive;

		// Token: 0x0400005B RID: 91
		private ITraceLog m_traceLog;

		// Token: 0x0400005C RID: 92
		private bool m_enableMathOpCasting;
	}
}
