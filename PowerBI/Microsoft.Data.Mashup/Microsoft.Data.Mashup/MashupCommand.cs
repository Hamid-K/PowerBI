using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Data.Mashup.ProviderCommon.CommandText;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost;
using Microsoft.Mashup.EngineHost.Services;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000022 RID: 34
	public sealed class MashupCommand : DbCommand
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00006A75 File Offset: 0x00004C75
		public MashupCommand()
		{
			this.syncRoot = new object();
			this.commandType = CommandType.Text;
			this.commandTextDialect = MashupCommandTextDialect.Default;
			this.parameters = new MashupParameterCollection();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006AA1 File Offset: 0x00004CA1
		public MashupCommand(string commandText)
			: this()
		{
			this.commandText = commandText;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006AB0 File Offset: 0x00004CB0
		public MashupCommand(string commandText, MashupConnection connection)
			: this(commandText)
		{
			this.connection = connection;
			if (connection != null)
			{
				this.CommandTimeout = connection.ConnectionTimeout;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public override void Prepare()
		{
			this.ThrowIfDataSourceIsOpen();
			if (this.connection == null)
			{
				throw new MashupException(ProviderErrorStrings.ConnectionNotSet);
			}
			if (this.connection.State != ConnectionState.Open)
			{
				throw new MashupException(ProviderErrorStrings.ConnectionNotOpen(this.connection.State));
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006B1F File Offset: 0x00004D1F
		private void Unprepare()
		{
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00006B21 File Offset: 0x00004D21
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00006B29 File Offset: 0x00004D29
		public override string CommandText
		{
			get
			{
				return this.commandText;
			}
			set
			{
				this.ThrowIfDataSourceIsOpen();
				this.commandText = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00006B38 File Offset: 0x00004D38
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00006B40 File Offset: 0x00004D40
		public override int CommandTimeout
		{
			get
			{
				return this.commandTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw new MashupException(ProviderErrorStrings.CommandTimeoutNegative);
				}
				this.commandTimeout = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006B58 File Offset: 0x00004D58
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00006B60 File Offset: 0x00004D60
		public override CommandType CommandType
		{
			get
			{
				return this.commandType;
			}
			set
			{
				this.ThrowIfDataSourceIsOpen();
				if (value != CommandType.Text && value != CommandType.TableDirect && value != CommandType.StoredProcedure)
				{
					throw new NotSupportedException(ProviderErrorStrings.UnsupportedCommandType(value));
				}
				this.commandType = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006B90 File Offset: 0x00004D90
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00006B98 File Offset: 0x00004D98
		public MashupCommandTextDialect MashupCommandTextDialect
		{
			get
			{
				return this.commandTextDialect;
			}
			set
			{
				this.ThrowIfDataSourceIsOpen();
				this.commandTextDialect = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00006BA7 File Offset: 0x00004DA7
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00006BAE File Offset: 0x00004DAE
		public override UpdateRowSource UpdatedRowSource
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006BB5 File Offset: 0x00004DB5
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00006BBD File Offset: 0x00004DBD
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (MashupConnection)value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006BCB File Offset: 0x00004DCB
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00006BD3 File Offset: 0x00004DD3
		public new MashupConnection Connection
		{
			get
			{
				return this.connection;
			}
			set
			{
				this.ThrowIfDataSourceIsOpen();
				if (this.connection != value)
				{
					if (this.connection != null)
					{
						this.Unprepare();
					}
					this.connection = value;
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006BF9 File Offset: 0x00004DF9
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00006C01 File Offset: 0x00004E01
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00006C04 File Offset: 0x00004E04
		protected override DbTransaction DbTransaction
		{
			get
			{
				return null;
			}
			set
			{
				if (value != null)
				{
					throw new NotSupportedException(ProviderErrorStrings.TransactionsNotSupported);
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006C14 File Offset: 0x00004E14
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00006C17 File Offset: 0x00004E17
		public override bool DesignTimeVisible
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException(ProviderErrorStrings.DesignTimeVisibleNotSupported);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00006C23 File Offset: 0x00004E23
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00006C4C File Offset: 0x00004E4C
		public string CorrelationId
		{
			get
			{
				if (this.Connection == null || !string.IsNullOrEmpty(this.correlationId))
				{
					return this.correlationId;
				}
				return this.Connection.CorrelationId;
			}
			set
			{
				this.correlationId = value;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006C58 File Offset: 0x00004E58
		public override void Cancel()
		{
			object obj = this.syncRoot;
			MashupResource mashupResource;
			lock (obj)
			{
				mashupResource = this.resource;
				this.resource = null;
			}
			if (mashupResource != null)
			{
				mashupResource.Abort();
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006CAC File Offset: 0x00004EAC
		protected override DbParameter CreateDbParameter()
		{
			return new MashupParameter();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006CB3 File Offset: 0x00004EB3
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006CBC File Offset: 0x00004EBC
		public new MashupReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006CC5 File Offset: 0x00004EC5
		public new MashupReader ExecuteReader(CommandBehavior commandBehavior)
		{
			MashupConnection mashupConnection = this.connection;
			return this.ExecuteReader(commandBehavior, (mashupConnection != null) ? mashupConnection.MashupCommandBehavior : MashupCommandBehavior.Default);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006CE0 File Offset: 0x00004EE0
		public MashupReader ExecuteReader(CommandBehavior commandBehavior, MashupCommandBehavior mashupCommandBehavior)
		{
			this.ThrowIfDataSourceIsOpen();
			if (string.IsNullOrEmpty(this.commandText))
			{
				throw new MashupException(ProviderErrorStrings.CommandTextNotSet);
			}
			MashupCommand.ThrowIfCommandBehaviorSet(commandBehavior, CommandBehavior.KeyInfo);
			MashupCommand.ThrowIfCommandBehaviorSet(commandBehavior, CommandBehavior.SingleRow);
			MashupCommand.ThrowIfCommandBehaviorSet(commandBehavior, CommandBehavior.SequentialAccess);
			this.Prepare();
			string text;
			if (Util.IsSet(mashupCommandBehavior, MashupCommandBehavior.ExecuteAction))
			{
				text = "((a) => if a is action then Action.Sequence(\r\n{\r\n    a,\r\n    (r) => Action.Return(Table.FromValue(r))\r\n})\r\nelse Table.FromValue(a))";
			}
			else
			{
				text = "Table.FromValue";
			}
			IDataReaderSource dataReaderSource = this.EvaluateAndGetSource<IDataReaderSource>(this.commandText, this.commandType, this.commandTimeout, this.parameters, text, Util.IsSet(commandBehavior, CommandBehavior.SchemaOnly), Util.IsSet(mashupCommandBehavior, MashupCommandBehavior.ExecuteAction));
			MashupReader mashupReader2;
			try
			{
				MashupReader mashupReader = new MashupReader(this, commandBehavior, dataReaderSource);
				this.dataSource = mashupReader;
				mashupReader2 = mashupReader;
			}
			catch
			{
				dataReaderSource.Dispose();
				throw;
			}
			return mashupReader2;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006D9C File Offset: 0x00004F9C
		public override int ExecuteNonQuery()
		{
			this.ThrowIfDataSourceIsOpen();
			if (string.IsNullOrEmpty(this.commandText))
			{
				throw new MashupException(ProviderErrorStrings.CommandTextNotSet);
			}
			this.Prepare();
			IDataReaderSource dataReaderSource = this.EvaluateAndGetSource<IDataReaderSource>(this.commandText, this.commandType, this.commandTimeout, this.parameters, "((a) => Action.Sequence(\r\n{\r\n    () => a,\r\n    (r) => Action.Return(\r\n        #table(type table [Value = Int32.Type],\r\n        {\r\n            {try Int32.From(r) otherwise -1}\r\n        }))\r\n}))", false, true);
			int @int;
			using (dataReaderSource)
			{
				using (MashupReader mashupReader = new MashupReader(this, CommandBehavior.Default, dataReaderSource))
				{
					mashupReader.Read();
					@int = mashupReader.GetInt32(0);
				}
			}
			return @int;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006E40 File Offset: 0x00005040
		public override object ExecuteScalar()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006E47 File Offset: 0x00005047
		public Stream ExecuteStream()
		{
			MashupConnection mashupConnection = this.connection;
			return this.ExecuteStream((mashupConnection != null) ? mashupConnection.MashupCommandBehavior : MashupCommandBehavior.Default);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006E64 File Offset: 0x00005064
		public Stream ExecuteStream(MashupCommandBehavior mashupCommandBehavior)
		{
			this.ThrowIfDataSourceIsOpen();
			if (string.IsNullOrEmpty(this.commandText))
			{
				throw new MashupException(ProviderErrorStrings.CommandTextNotSet);
			}
			this.Prepare();
			string text;
			if (Util.IsSet(mashupCommandBehavior, MashupCommandBehavior.ExecuteAction))
			{
				text = "((a) => if a is action then Action.Sequence(\r\n{\r\n    a,\r\n    (r) => Action.Return(Table.FirstValue(Table.FromValue(r)))\r\n})\r\nelse Table.FirstValue(Table.FromValue(a)))";
			}
			else
			{
				text = "((r) => Table.FirstValue(Table.FromValue(r)))";
			}
			IStreamSource streamSource = this.EvaluateAndGetSource<IStreamSource>(this.commandText, this.commandType, this.commandTimeout, this.parameters, text, false, Util.IsSet(mashupCommandBehavior, MashupCommandBehavior.ExecuteAction));
			Stream stream2;
			try
			{
				Stream stream = new MashupStream(this, streamSource);
				this.dataSource = streamSource;
				stream2 = stream;
			}
			catch
			{
				streamSource.Dispose();
				throw;
			}
			return stream2;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00006F00 File Offset: 0x00005100
		internal bool IsCanceled
		{
			get
			{
				object obj = this.syncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = this.resource == null;
				}
				return flag2;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006F48 File Offset: 0x00005148
		private void ThrowIfDataSourceIsOpen()
		{
			if (this.dataSource != null)
			{
				throw new InvalidOperationException(ProviderErrorStrings.DataReaderIsOpen);
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006F5D File Offset: 0x0000515D
		private static void ThrowIfCommandBehaviorSet(CommandBehavior commandBehavior, CommandBehavior behavior)
		{
			if (Util.IsSet(commandBehavior, behavior))
			{
				throw new NotSupportedException(ProviderErrorStrings.CommandBehaviorNotSupported(behavior));
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006F7C File Offset: 0x0000517C
		private T EvaluateAndGetSource<T>(string commandText, CommandType commandType, int commandTimeout, MashupParameterCollection parameters, string resultTransform, bool forColumnInfo, bool executeAction)
		{
			if (parameters.Count > 0 && commandType != CommandType.StoredProcedure && commandType != CommandType.Text)
			{
				throw new NotSupportedException(ProviderErrorStrings.ParametersNotSupported(commandType));
			}
			MashupConnection mashupConnection = this.Connection;
			Guid? guid = ((mashupConnection != null) ? mashupConnection.ActivityId : null);
			CommandTextParser commandTextParser = new CommandTextParser((guid != null) ? new EvaluationConstants(this.Connection.ActivityId.Value, this.CorrelationId, null) : null);
			CommandTextParseResult commandTextParseResult = null;
			if (commandType == CommandType.TableDirect)
			{
				commandTextParseResult = commandTextParser.ParseAsTableName(commandText);
			}
			else if (commandType == CommandType.Text)
			{
				if (this.commandTextDialect == MashupCommandTextDialect.M)
				{
					commandTextParser.TryParseAsM(commandText, this.Connection.Documents, out commandTextParseResult);
				}
				else if (this.commandTextDialect == MashupCommandTextDialect.SQL)
				{
					commandTextParser.TryParseAsSql(commandText, out commandTextParseResult);
				}
				else if (!commandTextParser.TryParseAsSql(commandText, out commandTextParseResult))
				{
					commandTextParser.TryParseAsM(commandText, this.Connection.Documents, out commandTextParseResult);
				}
			}
			else if (commandType == CommandType.StoredProcedure)
			{
				commandTextParseResult = new CommandTextParseResult(string.Format(CultureInfo.InvariantCulture, "{0}[{1}]{2}", ExpressionTransforms.EnvironmentParameter, MashupEngines.Version1.EscapeIdentifier(this.CommandText), ExpressionTransforms.GetParameterSet(parameters.ToIParameterList())), new string[] { this.CommandText });
			}
			if (commandTextParseResult == null)
			{
				if (commandText.Length > 1024)
				{
					commandText = commandText.Substring(0, 1024);
					commandText += "...";
				}
				throw new MashupException(ProviderErrorStrings.Resource_UnsupportedCommand(commandText));
			}
			IEnumerable<string> resourceNames = commandTextParseResult.ResourceNames;
			foreach (string text in resourceNames)
			{
				if (!this.Connection.Documents.IsSharedResource(text))
				{
					throw new MashupException(ProviderErrorStrings.UnknownResourceName(text));
				}
				if (this.Connection.Location != null && !string.Equals(text, this.Connection.Location, StringComparison.Ordinal))
				{
					throw new MashupException(ProviderErrorStrings.ResourceNameAndLocationNeedToMatch(text, this.Connection.Location));
				}
			}
			string text2 = MashupCommand.ApplyResultTransform(commandTextParseResult.Expression, resultTransform, forColumnInfo);
			text2 = ExpressionTransforms.ApplyEnvironmentRecordTransform(AdoNetProviderContext.Instance, text2, parameters.ToIParameterList(), resourceNames);
			this.connection.ClearProgress();
			MashupResource mashupResource = new MashupResource(AdoNetProviderContext.Instance, this.connection.CreateContext(), resourceNames, text2, executeAction, Util.MapParameters(parameters), this.connection.OptionalModules.Union(LibraryService.LegacyLibrary.GetModules()).ToArray<string>(), this.CorrelationId);
			object obj = this.syncRoot;
			lock (obj)
			{
				this.resource = mashupResource;
			}
			T t;
			try
			{
				t = mashupResource.StartEvaluationAndGetResultSource<T>(commandTimeout);
			}
			catch (MashupHostingException ex)
			{
				if (ex.Reason == "Timeout")
				{
					this.Cancel();
					this.NotifyDataReaderClosing();
				}
				throw;
			}
			return t;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007274 File Offset: 0x00005474
		private static string ApplyResultTransform(string expression, string resultTransform, bool forColumnInfo)
		{
			expression = ExpressionTransforms.ApplyResultTransform(expression, resultTransform);
			if (forColumnInfo)
			{
				expression = ExpressionTransforms.ApplyTransformForColumnInfo(expression);
			}
			return expression;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000728B File Offset: 0x0000548B
		internal void NotifyDataReaderClosing()
		{
			this.dataSource = null;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007294 File Offset: 0x00005494
		internal void TryCheckException(Exception e)
		{
			AdoNetProviderContext.Instance.ThrowIfTranslatedException(e);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000072A1 File Offset: 0x000054A1
		internal void StopUpdatingProgress()
		{
			this.connection.SetProgressUpdater(null);
		}

		// Token: 0x040000AA RID: 170
		private readonly object syncRoot;

		// Token: 0x040000AB RID: 171
		private string commandText;

		// Token: 0x040000AC RID: 172
		private CommandType commandType;

		// Token: 0x040000AD RID: 173
		private MashupCommandTextDialect commandTextDialect;

		// Token: 0x040000AE RID: 174
		private IDisposable dataSource;

		// Token: 0x040000AF RID: 175
		private MashupConnection connection;

		// Token: 0x040000B0 RID: 176
		private MashupResource resource;

		// Token: 0x040000B1 RID: 177
		private int commandTimeout;

		// Token: 0x040000B2 RID: 178
		private readonly MashupParameterCollection parameters;

		// Token: 0x040000B3 RID: 179
		private string correlationId;

		// Token: 0x040000B4 RID: 180
		private const int commandTextTruncateLimit = 1024;
	}
}
