using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A10 RID: 2576
	internal abstract class DrdaSchemaInformation
	{
		// Token: 0x06005123 RID: 20771 RVA: 0x00144BF4 File Offset: 0x00142DF4
		static DrdaSchemaInformation()
		{
			DrdaSchemaInformation._db2TypeMapping.Add("BIGINT", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.BigInt, typeof(long).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("CHAR", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Char, typeof(string).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("CHARACTER", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Char, typeof(string).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("BINARY", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Binary, typeof(byte[]).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("BLOB", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.BLOB, typeof(byte[]).ToString(), false, true, false));
			DrdaSchemaInformation._db2TypeMapping.Add("CLOB", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.CLOB, typeof(string).ToString(), false, true, false));
			DrdaSchemaInformation._db2TypeMapping.Add("DBCLOB", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.DBCLOB, typeof(string).ToString(), false, true, false));
			DrdaSchemaInformation._db2TypeMapping.Add("XML", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Xml, typeof(string).ToString(), false, true, false));
			DrdaSchemaInformation._db2TypeMapping.Add("DATE", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Date, typeof(DateTime).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("DOUBLE", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Double, typeof(double).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("DOUBLE PRECISION", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Double, typeof(double).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("INTEGER", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Int, typeof(int).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("INT", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Int, typeof(int).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("VARBINARY", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.VarBinary, typeof(byte[]).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("REAL", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Real, typeof(float).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("FLOAT", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Real, typeof(float).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("SMALLINT", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.SmallInt, typeof(short).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("TIME", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Time, typeof(TimeSpan).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("TIMESTAMP", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Timestamp, typeof(DateTime).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("TIMESTMP", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Timestamp, typeof(DateTime).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("VARCHAR", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.VarChar, typeof(string).ToString(), false, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("GRAPHIC", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Graphic, typeof(string).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("GRAPHIC () CCSID 13488", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Graphic, typeof(string).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("NUMERIC", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Numeric, typeof(decimal).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("DECIMAL", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Decimal, typeof(decimal).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("DECFLOAT", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Decimal, typeof(decimal).ToString(), true, false, true));
			DrdaSchemaInformation._db2TypeMapping.Add("VARGRAPHIC", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.VarGraphic, typeof(string).ToString(), false, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("VARGRAPHIC () CCSID 13488", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.VarGraphic, typeof(string).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("TIMESTAMP WITH TIME ZONE", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Timestamp, typeof(DateTime).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("BINARY LARGE OBJECT", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.BLOB, typeof(byte[]).ToString(), false, true, false));
			DrdaSchemaInformation._db2TypeMapping.Add("CHARACTER LARGE OBJECT", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.CLOB, typeof(string).ToString(), false, true, false));
			DrdaSchemaInformation._db2TypeMapping.Add("CHAR () FOR BIT DATA", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Binary, typeof(byte[]).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("LONG VARCHAR () FOR BIT DATA", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.VarBinary, typeof(byte[]).ToString(), false, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("VARCHAR () FOR BIT DATA", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.VarBinary, typeof(byte[]).ToString(), false, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("LONG VARCHAR", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.VarChar, typeof(string).ToString(), false, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("NCLOB", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.DBCLOB, typeof(string).ToString(), false, true, false));
			DrdaSchemaInformation._db2TypeMapping.Add("ROWID", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.VarChar, typeof(string).ToString(), false, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("BOOLEAN", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.SmallInt, typeof(bool).ToString(), true, false, false));
			DrdaSchemaInformation._db2TypeMapping.Add("LONG VARGRAPHIC", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.VarGraphic, typeof(string).ToString(), false, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("BIGINT", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.BigInt, typeof(long).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("BLOB", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.BLOB, typeof(byte[]).ToString(), false, true, false));
			DrdaSchemaInformation._informixTypeMapping.Add("CHAR", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Char, typeof(string).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("CLOB", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.CLOB, typeof(string).ToString(), false, true, false));
			DrdaSchemaInformation._informixTypeMapping.Add("DATE", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Date, typeof(DateTime).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("DATETIME HOUR TO MINUTE", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Time, typeof(TimeSpan).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("DATETIME HOUR TO SECOND", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Time, typeof(TimeSpan).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("DECIMAL", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Decimal, typeof(decimal).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("INTEGER", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Int, typeof(int).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("SMALLINT", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.SmallInt, typeof(short).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("TIME", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Time, typeof(TimeSpan).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("VARCHAR", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.VarChar, typeof(string).ToString(), false, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("BIGSERIAL", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.BigInt, typeof(long).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("BOOLEAN", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.SmallInt, typeof(bool).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("BYTE", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.VarBinary, typeof(byte).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("DATETIME", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Timestamp, typeof(DateTime).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("DATETIME YEAR TO SECOND", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Timestamp, typeof(DateTime).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("DATETIME YEAR TO FRACTION", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Timestamp, typeof(DateTime).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("DATETIME YEAR TO FRACTION(5)", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Timestamp, typeof(DateTime).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("FLOAT", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Double, typeof(float).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("INT8", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.BigInt, typeof(long).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("LVARCHAR", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.VarChar, typeof(string).ToString(), false, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("MONEY", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Numeric, typeof(decimal).ToString(), true, false, true));
			DrdaSchemaInformation._informixTypeMapping.Add("NCHAR", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Graphic, typeof(string).ToString(), false, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("NVARCHAR", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.VarGraphic, typeof(string).ToString(), false, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("SERIAL", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Int, typeof(int).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("SERIAL8", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.BigInt, typeof(long).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("SMALLFLOAT", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.Real, typeof(float).ToString(), true, false, false));
			DrdaSchemaInformation._informixTypeMapping.Add("TEXT", new Tuple<DrdaType, string, bool, bool, bool>(DrdaType.VarChar, typeof(string).ToString(), false, false, false));
		}

		// Token: 0x06005124 RID: 20772 RVA: 0x0014569F File Offset: 0x0014389F
		public DrdaSchemaInformation(string schemaName, DrdaSchemaRestriction[] restrictions, int identifierParts)
			: this(schemaName, restrictions, identifierParts, null)
		{
		}

		// Token: 0x06005125 RID: 20773 RVA: 0x001456AC File Offset: 0x001438AC
		public DrdaSchemaInformation(string schemaName, DrdaSchemaRestriction[] restrictions, int identifierParts, DrdaSchemaResultColumn[] resultColumns)
		{
			this._schemaName = schemaName;
			this._restrictions = restrictions;
			this._identifierParts = identifierParts;
			this._resultColumns = resultColumns;
			this._nullParameter.DrdaType = DrdaType.VarChar;
			this._nullParameter.Size = 4000;
			this._nullParameter.Value = null;
		}

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x06005126 RID: 20774 RVA: 0x00145717 File Offset: 0x00143917
		public DrdaSchemaRestriction[] Restrictions
		{
			get
			{
				return this._restrictions;
			}
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x06005127 RID: 20775 RVA: 0x0014571F File Offset: 0x0014391F
		public DrdaSchemaResultColumn[] ResultColumns
		{
			get
			{
				return this._resultColumns;
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06005128 RID: 20776 RVA: 0x00145727 File Offset: 0x00143927
		public string SchemaName
		{
			get
			{
				return this._schemaName;
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06005129 RID: 20777 RVA: 0x0014572F File Offset: 0x0014392F
		public int IdentifierParts
		{
			get
			{
				return this._identifierParts;
			}
		}

		// Token: 0x0600512A RID: 20778
		public abstract Task ExecuteAsync(DrdaConnection conn, ISqlStatement statement, DrdaParameterCollection parameters, string options, bool isAsync, CancellationToken cancellationToken);

		// Token: 0x0600512B RID: 20779 RVA: 0x00145737 File Offset: 0x00143937
		public virtual object GetRestrictionValue(DrdaConnection conn, int i)
		{
			if (i == 1 && conn.DrdaConnectionString.DefaultSchema.Length > 0)
			{
				return conn.DrdaConnectionString.DefaultSchema;
			}
			return this._restrictions[i].DefaultNull;
		}

		// Token: 0x0600512C RID: 20780 RVA: 0x00145770 File Offset: 0x00143970
		public virtual int GetPrecision(DrdaResultSet queryResultSet, int ordinal)
		{
			object value = queryResultSet.GetValue(ordinal);
			if (value != null && !value.Equals(DBNull.Value))
			{
				return Convert.ToInt32(value);
			}
			return 0;
		}

		// Token: 0x0600512D RID: 20781 RVA: 0x001457A0 File Offset: 0x001439A0
		public virtual void GetResultValues(DrdaConnection connection, DrdaSchemaResultSet resultSet, bool[] hasColumns)
		{
			for (int i = 0; i < resultSet.FieldCount; i++)
			{
				object obj = resultSet.QueryResultSet.GetValue(i);
				if (i == 0 && (obj == null || obj is DBNull || (obj is string && ((string)obj).ToString().Length == 0)))
				{
					obj = connection.Database;
				}
				resultSet.GetColumn(i).Value = obj;
			}
		}

		// Token: 0x0600512E RID: 20782 RVA: 0x00145808 File Offset: 0x00143A08
		public static DrdaType TypeNameToDrdaType(DrdaFlavor flavor, string typeName)
		{
			DrdaType drdaType = DrdaType.VarChar;
			if (typeName == null)
			{
				return drdaType;
			}
			typeName = typeName.Trim().ToUpperInvariant();
			Tuple<DrdaType, string, bool, bool, bool> tuple;
			if (!((flavor == DrdaFlavor.Informix) ? DrdaSchemaInformation._informixTypeMapping : DrdaSchemaInformation._db2TypeMapping).TryGetValue(typeName, out tuple))
			{
				Trace.MessageTrace("Unknown data type: {0}", typeName);
			}
			else
			{
				drdaType = tuple.Item1;
			}
			return drdaType;
		}

		// Token: 0x0600512F RID: 20783 RVA: 0x0014585C File Offset: 0x00143A5C
		public static DrdaParameterDirection ConvertParameterDirection(object dirName)
		{
			DrdaParameterDirection drdaParameterDirection = DrdaParameterDirection.Input;
			if (dirName != null)
			{
				string text = dirName.ToString();
				if (text.Equals("1", StringComparison.OrdinalIgnoreCase) || text.Equals("P", StringComparison.OrdinalIgnoreCase) || text.Equals("IN", StringComparison.OrdinalIgnoreCase) || text.Equals("INPUT", StringComparison.OrdinalIgnoreCase))
				{
					drdaParameterDirection = DrdaParameterDirection.Input;
				}
				else if (text.Equals("4", StringComparison.OrdinalIgnoreCase) || text.Equals("O", StringComparison.OrdinalIgnoreCase) || text.Equals("OUT", StringComparison.OrdinalIgnoreCase) || text.Equals("OUTPUT", StringComparison.OrdinalIgnoreCase))
				{
					drdaParameterDirection = DrdaParameterDirection.Output;
				}
				else if (text.Equals("2", StringComparison.OrdinalIgnoreCase) || text.Equals("B", StringComparison.OrdinalIgnoreCase) || text.Equals("INOUT", StringComparison.OrdinalIgnoreCase) || text.Equals("INOUTPUT", StringComparison.OrdinalIgnoreCase) || text.Equals("INPUTOUTPUT", StringComparison.OrdinalIgnoreCase))
				{
					drdaParameterDirection = DrdaParameterDirection.InputOutput;
				}
				else if (text.Equals("5", StringComparison.OrdinalIgnoreCase))
				{
					drdaParameterDirection = DrdaParameterDirection.ReturnValue;
				}
				else if (text.Equals("3", StringComparison.OrdinalIgnoreCase))
				{
					drdaParameterDirection = DrdaParameterDirection.ResultSetColumn;
				}
			}
			return drdaParameterDirection;
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x00145960 File Offset: 0x00143B60
		public static bool ConvertNullable(object nullable)
		{
			if (nullable == null)
			{
				return true;
			}
			if (nullable is int)
			{
				return (int)nullable == 1;
			}
			if (nullable is short)
			{
				return (short)nullable == 1;
			}
			return !(nullable is string) || StringComparer.OrdinalIgnoreCase.Compare((string)nullable, "YES") == 0 || StringComparer.OrdinalIgnoreCase.Compare((string)nullable, "Y") == 0 || StringComparer.OrdinalIgnoreCase.Compare((string)nullable, "1") == 0;
		}

		// Token: 0x06005131 RID: 20785 RVA: 0x001459E8 File Offset: 0x00143BE8
		public static int ConvertPrecision(object precision, DrdaClientType type)
		{
			int num = 0;
			if (precision is short)
			{
				num = (int)((short)precision);
			}
			else if (precision is int)
			{
				num = (int)precision;
			}
			if (type != DrdaClientType.BigInt)
			{
				switch (type)
				{
				case DrdaClientType.Decimal:
				case DrdaClientType.Numeric:
					return num;
				case DrdaClientType.Double:
				case DrdaClientType.Real:
					break;
				case DrdaClientType.Int:
					return 10;
				case DrdaClientType.SmallInt:
					return 5;
				default:
					if (type == DrdaClientType.DecFloat)
					{
						return num;
					}
					break;
				}
				num = 0;
			}
			else
			{
				num = 20;
			}
			return num;
		}

		// Token: 0x06005132 RID: 20786 RVA: 0x00145A54 File Offset: 0x00143C54
		public static int ConvertScale(object scale, DrdaClientType type)
		{
			int num = 0;
			if (scale is short)
			{
				num = (int)((short)scale);
			}
			else if (scale is int)
			{
				num = (int)scale;
			}
			if (type != DrdaClientType.Decimal && type != DrdaClientType.Numeric && type != DrdaClientType.DecFloat)
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06005133 RID: 20787 RVA: 0x00145A94 File Offset: 0x00143C94
		public static int ConvertLength(object length, DrdaClientType type)
		{
			int num = 0;
			if (length is short)
			{
				num = (int)((short)length);
			}
			else if (length is int)
			{
				num = (int)length;
			}
			if (type != DrdaClientType.BigInt)
			{
				if (type != DrdaClientType.Int)
				{
					if (type == DrdaClientType.SmallInt)
					{
						num = 2;
					}
				}
				else
				{
					num = 4;
				}
			}
			else
			{
				num = 8;
			}
			return num;
		}

		// Token: 0x06005134 RID: 20788 RVA: 0x00145ADC File Offset: 0x00143CDC
		public static string ConvertTableType(object tableType)
		{
			string text = tableType as string;
			if (text.Equals("P", StringComparison.OrdinalIgnoreCase) || text.Equals("T", StringComparison.OrdinalIgnoreCase) || text.Equals("TABLE", StringComparison.OrdinalIgnoreCase))
			{
				return "TABLE";
			}
			if (text.Equals("L", StringComparison.OrdinalIgnoreCase) || text.Equals("V", StringComparison.OrdinalIgnoreCase) || text.Equals("VIEW", StringComparison.OrdinalIgnoreCase))
			{
				return "VIEW";
			}
			return text;
		}

		// Token: 0x06005135 RID: 20789 RVA: 0x00145B51 File Offset: 0x00143D51
		protected bool CanUseSQL(DrdaConnection conn)
		{
			return conn.DrdaConnectionString.ShadowCatalog.Length > 0;
		}

		// Token: 0x06005136 RID: 20790 RVA: 0x00145B68 File Offset: 0x00143D68
		protected List<ISqlParameter> GetParameters(HostType hostType, DrdaParameterCollection parameters, int catalogParameterIndex, string options)
		{
			List<ISqlParameter> list = parameters.ToSqlParameters();
			if (string.IsNullOrEmpty(options))
			{
				list.Add(this._nullParameter);
			}
			else
			{
				list.Add(new SqlParameter
				{
					DrdaType = DrdaType.VarChar,
					Size = 4000,
					ParameterName = "OPTIONS",
					Value = options
				});
			}
			if (hostType != HostType.AS400 && catalogParameterIndex >= 0)
			{
				list[catalogParameterIndex] = this._nullParameter;
			}
			return list;
		}

		// Token: 0x06005137 RID: 20791 RVA: 0x00145BDC File Offset: 0x00143DDC
		protected Dictionary<string, Tuple<DrdaType, string, bool, bool, bool>> GetDataTypeMapping(DrdaFlavor flavor)
		{
			if (flavor != DrdaFlavor.DB2)
			{
				return DrdaSchemaInformation._informixTypeMapping;
			}
			return DrdaSchemaInformation._db2TypeMapping;
		}

		// Token: 0x06005138 RID: 20792 RVA: 0x00145BEC File Offset: 0x00143DEC
		protected DrdaType AdjustInformixDatetimeType(DrdaType currentDrdaType, DrdaResultSet resultSet)
		{
			if (currentDrdaType != DrdaType.Timestamp || resultSet.Connection.Requester.Flavor != DrdaFlavor.Informix)
			{
				return currentDrdaType;
			}
			if (this._dateTimeSubTypeIndex == -1)
			{
				int num = 0;
				if (!resultSet.TryGetOrdinal("SQL_DATETIME_SUB", out num))
				{
					return currentDrdaType;
				}
				this._dateTimeSubTypeIndex = num;
			}
			DrdaType drdaType = currentDrdaType;
			object value = resultSet.GetValue(this._dateTimeSubTypeIndex);
			if (value == null || value == DBNull.Value || (!(value is short) && !(value is int)))
			{
				return currentDrdaType;
			}
			int num2 = Convert.ToInt32(value);
			if (num2 != 56)
			{
				if (num2 == 58)
				{
					drdaType = DrdaType.Time;
				}
			}
			else
			{
				drdaType = DrdaType.Date;
			}
			return drdaType;
		}

		// Token: 0x04003FAC RID: 16300
		private static Dictionary<string, Tuple<DrdaType, string, bool, bool, bool>> _db2TypeMapping = new Dictionary<string, Tuple<DrdaType, string, bool, bool, bool>>();

		// Token: 0x04003FAD RID: 16301
		private static Dictionary<string, Tuple<DrdaType, string, bool, bool, bool>> _informixTypeMapping = new Dictionary<string, Tuple<DrdaType, string, bool, bool, bool>>();

		// Token: 0x04003FAE RID: 16302
		private DrdaSchemaRestriction[] _restrictions;

		// Token: 0x04003FAF RID: 16303
		private DrdaSchemaResultColumn[] _resultColumns;

		// Token: 0x04003FB0 RID: 16304
		private string _schemaName;

		// Token: 0x04003FB1 RID: 16305
		private int _identifierParts;

		// Token: 0x04003FB2 RID: 16306
		protected int _dateTimeSubTypeIndex = -1;

		// Token: 0x04003FB3 RID: 16307
		private const string DateTimeSubTypeColumnName = "SQL_DATETIME_SUB";

		// Token: 0x04003FB4 RID: 16308
		private const int InformixDateSubType = 56;

		// Token: 0x04003FB5 RID: 16309
		private const int InformixTimeSubType = 58;

		// Token: 0x04003FB6 RID: 16310
		private SqlParameter _nullParameter = new SqlParameter();
	}
}
