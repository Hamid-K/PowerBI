using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Data.Common;
using Microsoft.Data.Sql;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000065 RID: 101
	[DesignerCategory("")]
	public sealed class SqlCommandBuilder : DbCommandBuilder
	{
		// Token: 0x0600090A RID: 2314 RVA: 0x00017177 File Offset: 0x00015377
		public SqlCommandBuilder()
		{
			GC.SuppressFinalize(this);
			base.QuotePrefix = "[";
			base.QuoteSuffix = "]";
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001719B File Offset: 0x0001539B
		public SqlCommandBuilder(SqlDataAdapter adapter)
			: this()
		{
			this.DataAdapter = adapter;
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x000171AA File Offset: 0x000153AA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override CatalogLocation CatalogLocation
		{
			get
			{
				return CatalogLocation.Start;
			}
			set
			{
				if (CatalogLocation.Start != value)
				{
					throw ADP.SingleValuedProperty("CatalogLocation", "Start");
				}
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x000171C0 File Offset: 0x000153C0
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x000171C7 File Offset: 0x000153C7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string CatalogSeparator
		{
			get
			{
				return ".";
			}
			set
			{
				if ("." != value)
				{
					throw ADP.SingleValuedProperty("CatalogSeparator", ".");
				}
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x000171E6 File Offset: 0x000153E6
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x000171F3 File Offset: 0x000153F3
		[DefaultValue(null)]
		[ResCategory("Update")]
		[ResDescription("The DataAdapter for which to automatically generate SqlCommands")]
		public new SqlDataAdapter DataAdapter
		{
			get
			{
				return (SqlDataAdapter)base.DataAdapter;
			}
			set
			{
				base.DataAdapter = value;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x000171FC File Offset: 0x000153FC
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x00017204 File Offset: 0x00015404
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string QuotePrefix
		{
			get
			{
				return base.QuotePrefix;
			}
			set
			{
				if ("[" != value && "\"" != value)
				{
					throw ADP.DoubleValuedProperty("QuotePrefix", "[", "\"");
				}
				base.QuotePrefix = value;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x0001723C File Offset: 0x0001543C
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x00017244 File Offset: 0x00015444
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string QuoteSuffix
		{
			get
			{
				return base.QuoteSuffix;
			}
			set
			{
				if ("]" != value && "\"" != value)
				{
					throw ADP.DoubleValuedProperty("QuoteSuffix", "]", "\"");
				}
				base.QuoteSuffix = value;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x000171C0 File Offset: 0x000153C0
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x0001727C File Offset: 0x0001547C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string SchemaSeparator
		{
			get
			{
				return ".";
			}
			set
			{
				if ("." != value)
				{
					throw ADP.SingleValuedProperty("SchemaSeparator", ".");
				}
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0001729B File Offset: 0x0001549B
		private void SqlRowUpdatingHandler(object sender, SqlRowUpdatingEventArgs ruevent)
		{
			base.RowUpdatingHandler(ruevent);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000172A4 File Offset: 0x000154A4
		public new SqlCommand GetInsertCommand()
		{
			return (SqlCommand)base.GetInsertCommand();
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000172B1 File Offset: 0x000154B1
		public new SqlCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetInsertCommand(useColumnsForParameterNames);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000172BF File Offset: 0x000154BF
		public new SqlCommand GetUpdateCommand()
		{
			return (SqlCommand)base.GetUpdateCommand();
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000172CC File Offset: 0x000154CC
		public new SqlCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetUpdateCommand(useColumnsForParameterNames);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000172DA File Offset: 0x000154DA
		public new SqlCommand GetDeleteCommand()
		{
			return (SqlCommand)base.GetDeleteCommand();
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000172E7 File Offset: 0x000154E7
		public new SqlCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetDeleteCommand(useColumnsForParameterNames);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x000172F8 File Offset: 0x000154F8
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow datarow, StatementType statementType, bool whereClause)
		{
			SqlParameter sqlParameter = (SqlParameter)parameter;
			object obj = datarow[SchemaTableColumn.ProviderType];
			sqlParameter.SqlDbType = (SqlDbType)obj;
			sqlParameter.Offset = 0;
			if (sqlParameter.SqlDbType == SqlDbType.Udt && !sqlParameter.SourceColumnNullMapping)
			{
				sqlParameter.UdtTypeName = datarow["DataTypeName"] as string;
			}
			else
			{
				sqlParameter.UdtTypeName = string.Empty;
			}
			object obj2 = datarow[SchemaTableColumn.NumericPrecision];
			if (DBNull.Value != obj2)
			{
				byte b = (byte)((short)obj2);
				sqlParameter.PrecisionInternal = ((byte.MaxValue != b) ? b : 0);
			}
			obj2 = datarow[SchemaTableColumn.NumericScale];
			if (DBNull.Value != obj2)
			{
				byte b2 = (byte)((short)obj2);
				sqlParameter.ScaleInternal = ((byte.MaxValue != b2) ? b2 : 0);
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000173BF File Offset: 0x000155BF
		protected override string GetParameterName(int parameterOrdinal)
		{
			return "@p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x000173D7 File Offset: 0x000155D7
		protected override string GetParameterName(string parameterName)
		{
			return "@" + parameterName;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000173BF File Offset: 0x000155BF
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return "@p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000173E4 File Offset: 0x000155E4
		private void ConsistentQuoteDelimiters(string quotePrefix, string quoteSuffix)
		{
			if (("\"" == quotePrefix && "\"" != quoteSuffix) || ("[" == quotePrefix && "]" != quoteSuffix))
			{
				throw ADP.InvalidPrefixSuffix();
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00017420 File Offset: 0x00015620
		public static void DeriveParameters(SqlCommand command)
		{
			SqlConnection.ExecutePermission.Demand();
			if (command == null)
			{
				throw ADP.ArgumentNull("command");
			}
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(command.Connection);
				command.DeriveParameters();
			}
			catch (OutOfMemoryException ex)
			{
				if (command != null)
				{
					SqlConnection connection = command.Connection;
					if (connection != null)
					{
						connection.Abort(ex);
					}
				}
				throw;
			}
			catch (StackOverflowException ex2)
			{
				if (command != null)
				{
					SqlConnection connection2 = command.Connection;
					if (connection2 != null)
					{
						connection2.Abort(ex2);
					}
				}
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				if (command != null)
				{
					SqlConnection connection3 = command.Connection;
					if (connection3 != null)
					{
						connection3.Abort(ex3);
					}
				}
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000174D8 File Offset: 0x000156D8
		protected override DataTable GetSchemaTable(DbCommand srcCommand)
		{
			SqlCommand sqlCommand = srcCommand as SqlCommand;
			SqlNotificationRequest notification = sqlCommand.Notification;
			sqlCommand.Notification = null;
			bool notificationAutoEnlist = sqlCommand.NotificationAutoEnlist;
			sqlCommand.NotificationAutoEnlist = false;
			DataTable schemaTable;
			try
			{
				using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
				{
					schemaTable = sqlDataReader.GetSchemaTable();
				}
			}
			finally
			{
				sqlCommand.Notification = notification;
				sqlCommand.NotificationAutoEnlist = notificationAutoEnlist;
			}
			return schemaTable;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00017554 File Offset: 0x00015754
		protected override DbCommand InitializeCommand(DbCommand command)
		{
			return (SqlCommand)base.InitializeCommand(command);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00017564 File Offset: 0x00015764
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			ADP.CheckArgumentNull(unquotedIdentifier, "unquotedIdentifier");
			string quoteSuffix = this.QuoteSuffix;
			string quotePrefix = this.QuotePrefix;
			this.ConsistentQuoteDelimiters(quotePrefix, quoteSuffix);
			return ADP.BuildQuotedString(quotePrefix, quoteSuffix, unquotedIdentifier);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0001759A File Offset: 0x0001579A
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((SqlDataAdapter)adapter).RowUpdating -= this.SqlRowUpdatingHandler;
				return;
			}
			((SqlDataAdapter)adapter).RowUpdating += this.SqlRowUpdatingHandler;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000175D4 File Offset: 0x000157D4
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			ADP.CheckArgumentNull(quotedIdentifier, "quotedIdentifier");
			string quoteSuffix = this.QuoteSuffix;
			string quotePrefix = this.QuotePrefix;
			this.ConsistentQuoteDelimiters(quotePrefix, quoteSuffix);
			string text;
			ADP.RemoveStringQuotes(quotePrefix, quoteSuffix, quotedIdentifier, out text);
			return text;
		}
	}
}
