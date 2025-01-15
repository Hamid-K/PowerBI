using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010F8 RID: 4344
	internal class OutputParameterCollection
	{
		// Token: 0x06007195 RID: 29077 RVA: 0x0018691C File Offset: 0x00184B1C
		public OutputParameterCollection(Query targetTable, IList<Alias> columnList, IList<Alias> outputParameters, DbEnvironment environment)
		{
			this.targetTable = targetTable;
			this.columnList = columnList;
			this.outputParameters = outputParameters;
			this.environment = environment;
		}

		// Token: 0x06007196 RID: 29078 RVA: 0x00186944 File Offset: 0x00184B44
		public void AddParametersToDbCommand(DbCommand command)
		{
			for (int i = 0; i < this.columnList.Count; i++)
			{
				TypeValue columnType = this.GetColumnType(this.columnList[i].Name);
				DbParameter dbParameter = command.CreateParameter();
				dbParameter.ParameterName = this.outputParameters[i].Name;
				dbParameter.Direction = ParameterDirection.Output;
				this.SetParameterType(dbParameter, columnType);
				if (dbParameter.DbType == DbType.String)
				{
					dbParameter.Size = this.environment.SqlSettings.MaxVariableStringLength;
					bool? isVariableLength = columnType.Facets.IsVariableLength;
					bool flag = true;
					if (!((isVariableLength.GetValueOrDefault() == flag) & (isVariableLength != null)) && columnType.Facets.MaxLength != null)
					{
						try
						{
							dbParameter.Size = Convert.ToInt32(columnType.Facets.MaxLength, CultureInfo.InvariantCulture);
						}
						catch (OverflowException)
						{
						}
					}
				}
				command.Parameters.Add(dbParameter);
			}
		}

		// Token: 0x06007197 RID: 29079 RVA: 0x00186A50 File Offset: 0x00184C50
		protected virtual void SetParameterType(DbParameter parameter, TypeValue columnType)
		{
			parameter.DbType = DbData.MapToDbType(columnType, DbType.String);
		}

		// Token: 0x06007198 RID: 29080 RVA: 0x00186A60 File Offset: 0x00184C60
		private TypeValue GetColumnType(string columnName)
		{
			int num = this.targetTable.Columns.IndexOfKey(columnName);
			return this.targetTable.GetColumnType(num);
		}

		// Token: 0x06007199 RID: 29081 RVA: 0x00186A8C File Offset: 0x00184C8C
		public void FillDataTable(DbCommand command, DataTable table)
		{
			foreach (Alias alias in this.columnList)
			{
				table.Columns.Add(alias.Name, this.GetColumnType(alias.Name).ToClrType());
			}
			object[] array = new object[command.Parameters.Count];
			for (int i = 0; i < command.Parameters.Count; i++)
			{
				array[i] = this.GetParameterValue(command.Parameters[i].Value);
			}
			table.Rows.Add(array);
		}

		// Token: 0x0600719A RID: 29082 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected virtual object GetParameterValue(object value)
		{
			return value;
		}

		// Token: 0x04003ED0 RID: 16080
		private readonly Query targetTable;

		// Token: 0x04003ED1 RID: 16081
		private readonly IList<Alias> columnList;

		// Token: 0x04003ED2 RID: 16082
		private readonly IList<Alias> outputParameters;

		// Token: 0x04003ED3 RID: 16083
		private readonly DbEnvironment environment;
	}
}
