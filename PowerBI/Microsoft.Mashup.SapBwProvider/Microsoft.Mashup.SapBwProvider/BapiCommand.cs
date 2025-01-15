using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000005 RID: 5
	internal sealed class BapiCommand
	{
		// Token: 0x06000039 RID: 57 RVA: 0x0000234C File Offset: 0x0000054C
		public BapiCommand(SapBwCommand command)
		{
			this.command = command;
			this.connection = (SapBwConnection)command.Connection;
			this.parameters = (SapBwParameterCollection)command.Parameters;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000237D File Offset: 0x0000057D
		private bool EnhancedMetadata
		{
			get
			{
				return this.command.GetParameterValueOrDefault("ENHANCEDMETADATA", false);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002390 File Offset: 0x00000590
		public DbDataReader ExecuteReader()
		{
			string text = this.ValidateParametersAndExtractOutputTable();
			IRfcFunction function = this.connection.GetFunction(this.command.CommandText, true);
			foreach (SapBwParameter sapBwParameter in this.parameters.Where((SapBwParameter p) => p.Direction == ParameterDirection.Input))
			{
				function.SetValue(sapBwParameter.ParameterName, sapBwParameter.Value);
			}
			this.connection.InvokeFunction(function, true, this.command, true);
			IRfcTable table = function.GetTable(text);
			if (table == null)
			{
				throw this.connection.Helper.NewDataSourceError(Resources.TableNotFound(text));
			}
			BapiDataReader bapiDataReader = new BapiDataReader(this.connection, table, text);
			if (this.EnhancedMetadata && this.command.CommandText == "BAPI_MDPROVIDER_GET_DIMENSIONS")
			{
				return new ComputedColumnDataReader(bapiDataReader, new List<IComputedColumn>
				{
					new InfoObjectDataTypeColumn(this.connection, table.RowCount)
				});
			}
			return bapiDataReader;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000024C0 File Offset: 0x000006C0
		private string ValidateParametersAndExtractOutputTable()
		{
			if (string.IsNullOrEmpty(this.command.CommandText))
			{
				throw this.connection.Helper.NewDataSourceError(Resources.MissingBapiParameter);
			}
			if (this.parameters == null || this.parameters.Count == 0)
			{
				throw this.connection.Helper.NewDataSourceError(Resources.MissingParametersOnBapiCall);
			}
			string text = null;
			foreach (SapBwParameter sapBwParameter in this.parameters.Where((SapBwParameter x) => x.Direction == ParameterDirection.ReturnValue))
			{
				if (text != null)
				{
					throw this.connection.Helper.NewDataSourceError(Resources.MultipleReturnTablesFound);
				}
				text = sapBwParameter.Value as string;
				if (text == null)
				{
					throw this.connection.Helper.NewDataSourceError(Resources.ParameterValueMustBeString(sapBwParameter.ParameterName));
				}
			}
			if (text == null)
			{
				throw this.connection.Helper.NewDataSourceError(Resources.TableNotSpecified);
			}
			return text;
		}

		// Token: 0x04000003 RID: 3
		private readonly SapBwCommand command;

		// Token: 0x04000004 RID: 4
		private readonly SapBwConnection connection;

		// Token: 0x04000005 RID: 5
		private readonly SapBwParameterCollection parameters;
	}
}
