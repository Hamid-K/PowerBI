using System;
using System.Collections;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ProcessingRenderingCommon;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x02000015 RID: 21
	public class CommandWrapper : BaseDataWrapper, Microsoft.ReportingServices.DataProcessing.IDbCommand, IDisposable
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00004695 File Offset: 0x00002895
		protected internal CommandWrapper(global::System.Data.IDbCommand command)
			: base(command)
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000046A0 File Offset: 0x000028A0
		protected internal virtual void SaveCommandObject(out ArrayList sysParameters, out int sysParameterCount, out string sysCommandText)
		{
			global::System.Data.IDbCommand underlyingCommand = this.UnderlyingCommand;
			sysParameterCount = ((underlyingCommand.Parameters == null) ? 0 : underlyingCommand.Parameters.Count);
			sysCommandText = string.Copy(underlyingCommand.CommandText);
			if (sysParameterCount == 0)
			{
				sysParameters = null;
				return;
			}
			sysParameters = new ArrayList(sysParameterCount);
			foreach (ParameterWrapper parameterWrapper in this.m_parameterCollection)
			{
				sysParameters.Add(parameterWrapper);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004710 File Offset: 0x00002910
		protected internal virtual void RestoreCommandObject(ArrayList sysParameters, int sysParameterCount, string sysCommandText)
		{
			global::System.Data.IDbCommand underlyingCommand = this.UnderlyingCommand;
			underlyingCommand.CommandText = sysCommandText;
			underlyingCommand.Parameters.Clear();
			if (sysParameterCount != 0)
			{
				foreach (object obj in sysParameters)
				{
					ParameterWrapper parameterWrapper = (ParameterWrapper)obj;
					underlyingCommand.Parameters.Add(parameterWrapper.UnderlyingParameter);
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000478C File Offset: 0x0000298C
		protected internal virtual bool RewriteMultiValueParameters(int sysParameterCount, ServerType serverType)
		{
			global::System.Data.IDbCommand underlyingCommand = this.UnderlyingCommand;
			RSTrace.DataExtensionTracer.Assert(underlyingCommand is SqlCommand || underlyingCommand is OracleCommand || underlyingCommand.GetType().FullName.Contains("Oracle.ManagedDataAccess.Client.OracleCommand") || underlyingCommand.GetType().FullName.Contains("Microsoft.SqlServer.DataWarehouse") || underlyingCommand.GetType().FullName.Equals("Teradata.Client.Provider.TdCommand", StringComparison.Ordinal) || underlyingCommand.GetType().FullName.Contains("Castle.Proxies"), "MultiValueRewrite was called for unsupported data provider");
			string text = CommandWrapperHelper.WriteMultiValueParametersIntoCommandText(underlyingCommand, this.m_parameterCollection, sysParameterCount, serverType);
			if (!text.Equals(underlyingCommand.CommandText, StringComparison.Ordinal))
			{
				underlyingCommand.CommandText = text;
				return true;
			}
			return false;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004845 File Offset: 0x00002A45
		public virtual Microsoft.ReportingServices.DataProcessing.IDataReader ExecuteReader(Microsoft.ReportingServices.DataProcessing.CommandBehavior behavior)
		{
			return new DataReaderWrapper(this.UnderlyingCommand.ExecuteReader(this.ConvertCommandBehavior(behavior)));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000485E File Offset: 0x00002A5E
		protected global::System.Data.CommandBehavior ConvertCommandBehavior(Microsoft.ReportingServices.DataProcessing.CommandBehavior behavior)
		{
			if (Microsoft.ReportingServices.DataProcessing.CommandBehavior.SingleResult == behavior && !this.IsConnectedToAS2005OrLater())
			{
				return global::System.Data.CommandBehavior.Default;
			}
			return (global::System.Data.CommandBehavior)behavior;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004870 File Offset: 0x00002A70
		private bool IsConnectedToAS2005OrLater()
		{
			if (this.UnderlyingCommand is OleDbCommand)
			{
				try
				{
					OleDbConnection connection = ((OleDbCommand)this.UnderlyingCommand).Connection;
					if (connection.Provider.Contains("MSOLAP"))
					{
						string serverVersion = connection.ServerVersion;
						if (Convert.ToInt32(serverVersion.Substring(0, serverVersion.IndexOf(".", StringComparison.Ordinal)), CultureInfo.InvariantCulture) >= 9)
						{
							return true;
						}
					}
				}
				catch (Exception)
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000048F4 File Offset: 0x00002AF4
		protected ArrayList GetStoredProcedureParameters()
		{
			ArrayList arrayList;
			return this.GetStoredProcedureParameters(out arrayList);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000490C File Offset: 0x00002B0C
		protected ArrayList GetStoredProcedureParameters(out ArrayList oracleParameters)
		{
			oracleParameters = null;
			global::System.Data.IDbCommand underlyingCommand = this.UnderlyingCommand;
			if (global::System.Data.CommandType.StoredProcedure != underlyingCommand.CommandType)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList(underlyingCommand.Parameters.Count);
			foreach (object obj in underlyingCommand.Parameters)
			{
				global::System.Data.IDataParameter dataParameter = (global::System.Data.IDataParameter)obj;
				arrayList2.Add(dataParameter);
			}
			underlyingCommand.Parameters.Clear();
			string commandText = underlyingCommand.CommandText;
			if (commandText.Length >= 2 && commandText[0] == '[' && commandText[commandText.Length - 1] == ']')
			{
				underlyingCommand.CommandText = commandText.Substring(1, commandText.Length - 2);
			}
			bool flag = false;
			if (underlyingCommand is SqlCommand)
			{
				SqlCommandBuilder.DeriveParameters((SqlCommand)underlyingCommand);
			}
			else if (underlyingCommand is OleDbCommand)
			{
				OleDbCommandBuilder.DeriveParameters((OleDbCommand)underlyingCommand);
			}
			else if (underlyingCommand is OdbcCommand)
			{
				OdbcCommandBuilder.DeriveParameters((OdbcCommand)underlyingCommand);
			}
			else if (underlyingCommand.GetType().FullName.Contains("Oracle.ManagedDataAccess.Client.OracleCommand"))
			{
				object[] array = new object[] { underlyingCommand };
				underlyingCommand.GetType().Assembly.GetType("Oracle.ManagedDataAccess.Client.OracleCommandBuilder").GetMethod("DeriveParameters", BindingFlags.Static | BindingFlags.Public).Invoke(null, array);
				oracleParameters = new ArrayList();
				flag = true;
			}
			else if (underlyingCommand is OracleCommand)
			{
				OracleCommandBuilder.DeriveParameters((OracleCommand)underlyingCommand);
				oracleParameters = new ArrayList();
				flag = true;
			}
			if (underlyingCommand.CommandText != commandText)
			{
				underlyingCommand.CommandText = commandText;
			}
			foreach (object obj2 in underlyingCommand.Parameters)
			{
				global::System.Data.IDataParameter dataParameter2 = (global::System.Data.IDataParameter)obj2;
				bool flag2 = false;
				if (underlyingCommand is OracleCommand)
				{
					flag2 = flag && ((OracleParameter)dataParameter2).OracleType == OracleType.Cursor;
				}
				else
				{
					int num = 121;
					if (flag)
					{
						flag2 = (int)dataParameter2.GetType().GetProperty("OracleDbType").GetValue(dataParameter2, null) == num;
					}
				}
				if (!flag2 && (dataParameter2.Direction == ParameterDirection.Input || dataParameter2.Direction == ParameterDirection.InputOutput))
				{
					arrayList.Add(dataParameter2);
				}
				else if (flag2)
				{
					if (dataParameter2.Direction == ParameterDirection.InputOutput)
					{
						dataParameter2.Direction = ParameterDirection.Output;
					}
					oracleParameters.Add(dataParameter2);
				}
			}
			underlyingCommand.Parameters.Clear();
			foreach (object obj3 in arrayList2)
			{
				global::System.Data.IDataParameter dataParameter3 = (global::System.Data.IDataParameter)obj3;
				underlyingCommand.Parameters.Add(dataParameter3);
			}
			return arrayList;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004BFC File Offset: 0x00002DFC
		public virtual Microsoft.ReportingServices.DataProcessing.IDataParameter CreateParameter()
		{
			return new ParameterWrapper(this.UnderlyingCommand.CreateParameter());
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004C10 File Offset: 0x00002E10
		public virtual void Cancel()
		{
			if (this.UnderlyingCommand != null && this.UnderlyingCommand.Connection != null && this.UnderlyingCommand.Connection.State != ConnectionState.Closed && this.UnderlyingCommand.Connection.State != ConnectionState.Broken)
			{
				this.UnderlyingCommand.Cancel();
				return;
			}
			if (RSTrace.DataExtensionTracer.TraceWarning)
			{
				RSTrace.DataExtensionTracer.Trace(TraceLevel.Warning, "CommandWrapper.Cancel not called, connection is not valid");
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004C80 File Offset: 0x00002E80
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00004C8D File Offset: 0x00002E8D
		public virtual string CommandText
		{
			get
			{
				return this.UnderlyingCommand.CommandText;
			}
			set
			{
				this.UnderlyingCommand.CommandText = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004C9B File Offset: 0x00002E9B
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00004CA8 File Offset: 0x00002EA8
		public virtual int CommandTimeout
		{
			get
			{
				return this.UnderlyingCommand.CommandTimeout;
			}
			set
			{
				this.UnderlyingCommand.CommandTimeout = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004CB6 File Offset: 0x00002EB6
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00004CC3 File Offset: 0x00002EC3
		public virtual Microsoft.ReportingServices.DataProcessing.CommandType CommandType
		{
			get
			{
				return (Microsoft.ReportingServices.DataProcessing.CommandType)this.UnderlyingCommand.CommandType;
			}
			set
			{
				this.UnderlyingCommand.CommandType = (global::System.Data.CommandType)value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004CD1 File Offset: 0x00002ED1
		public virtual Microsoft.ReportingServices.DataProcessing.IDataParameterCollection Parameters
		{
			get
			{
				if (this.m_parameterCollection == null)
				{
					this.m_parameterCollection = this.CreateParameterCollection();
				}
				return this.m_parameterCollection;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004CED File Offset: 0x00002EED
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004CF5 File Offset: 0x00002EF5
		public virtual Microsoft.ReportingServices.DataProcessing.IDbTransaction Transaction
		{
			get
			{
				return this.m_transactionWrapper;
			}
			set
			{
				this.m_transactionWrapper = (TransactionWrapper)value;
				this.UnderlyingCommand.Transaction = this.m_transactionWrapper.UnderlyingTransaction;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004D19 File Offset: 0x00002F19
		protected override void Dispose(bool disposing)
		{
			this.m_transactionWrapper = null;
			this.m_parameterCollection = null;
			base.Dispose(disposing);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004D32 File Offset: 0x00002F32
		protected virtual ParameterCollectionWrapper CreateParameterCollection()
		{
			return new ParameterCollectionWrapper(this.UnderlyingCommand.Parameters);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004D44 File Offset: 0x00002F44
		public global::System.Data.IDbCommand UnderlyingCommand
		{
			get
			{
				return (global::System.Data.IDbCommand)base.UnderlyingObject;
			}
		}

		// Token: 0x04000080 RID: 128
		private const string OracleCommandTypeFullName = "Oracle.ManagedDataAccess.Client.OracleCommand";

		// Token: 0x04000081 RID: 129
		private const string OracleCommandBuilderTypeFullName = "Oracle.ManagedDataAccess.Client.OracleCommandBuilder";

		// Token: 0x04000082 RID: 130
		private TransactionWrapper m_transactionWrapper;

		// Token: 0x04000083 RID: 131
		protected ParameterCollectionWrapper m_parameterCollection;
	}
}
