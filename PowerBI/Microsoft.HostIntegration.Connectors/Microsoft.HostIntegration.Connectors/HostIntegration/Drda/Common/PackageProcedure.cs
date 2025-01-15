using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200082E RID: 2094
	public class PackageProcedure
	{
		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06004291 RID: 17041 RVA: 0x000DFEB2 File Offset: 0x000DE0B2
		// (set) Token: 0x06004292 RID: 17042 RVA: 0x000DFEBA File Offset: 0x000DE0BA
		public DateTime LastInvokeTime
		{
			get
			{
				return this.lastInvokeTime;
			}
			set
			{
				this.lastInvokeTime = value;
			}
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06004293 RID: 17043 RVA: 0x000DFEC3 File Offset: 0x000DE0C3
		// (set) Token: 0x06004294 RID: 17044 RVA: 0x000DFECB File Offset: 0x000DE0CB
		public string Sqlstt
		{
			get
			{
				return this.sqlstt;
			}
			set
			{
				this.sqlstt = value;
			}
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06004295 RID: 17045 RVA: 0x000DFED4 File Offset: 0x000DE0D4
		public string QualifiedName
		{
			get
			{
				return string.Format("{0}.{1}.{2}", this.procedure_catalog, this.procedure_schema, this.procedure_name);
			}
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06004296 RID: 17046 RVA: 0x000DFEF2 File Offset: 0x000DE0F2
		// (set) Token: 0x06004297 RID: 17047 RVA: 0x000DFEFC File Offset: 0x000DE0FC
		public string BndOptions
		{
			get
			{
				return this.bnd_options;
			}
			set
			{
				this.bnd_options = value;
				if (!string.IsNullOrWhiteSpace(this.bnd_options))
				{
					try
					{
						XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
						xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
						xmlReaderSettings.XmlResolver = null;
						XmlReader xmlReader = XmlReader.Create(new StringReader(this.bnd_options), xmlReaderSettings);
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.XmlResolver = null;
						xmlDocument.Load(xmlReader);
						XmlNode xmlNode = xmlDocument.SelectSingleNode("Options/DFTRDBCOL");
						if (xmlNode != null)
						{
							this.procedure_qualifier = xmlNode.InnerText;
						}
					}
					catch (Exception)
					{
						if (Logger.maxTracingLevel >= 2)
						{
							Logger.Warning(0, "Bind options in the PackageProcedures table is not a valid Xml segment", Array.Empty<object>());
						}
					}
				}
			}
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06004298 RID: 17048 RVA: 0x000DFFA0 File Offset: 0x000DE1A0
		public string ProcedureCatalog
		{
			get
			{
				return this.procedure_catalog;
			}
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06004299 RID: 17049 RVA: 0x000DFFA8 File Offset: 0x000DE1A8
		public string ProcedureSchema
		{
			get
			{
				return this.procedure_schema;
			}
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x0600429A RID: 17050 RVA: 0x000DFFB0 File Offset: 0x000DE1B0
		public string ProcedureQualifier
		{
			get
			{
				return this.procedure_qualifier;
			}
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x0600429B RID: 17051 RVA: 0x000DFFB8 File Offset: 0x000DE1B8
		public string ProcedureName
		{
			get
			{
				return this.procedure_name;
			}
		}

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x0600429C RID: 17052 RVA: 0x000DFFC0 File Offset: 0x000DE1C0
		// (set) Token: 0x0600429D RID: 17053 RVA: 0x000DFFC8 File Offset: 0x000DE1C8
		public bool HasOutputParams
		{
			get
			{
				return this.hasOutputParams;
			}
			set
			{
				this.hasOutputParams = value;
			}
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x0600429E RID: 17054 RVA: 0x000DFFD1 File Offset: 0x000DE1D1
		// (set) Token: 0x0600429F RID: 17055 RVA: 0x000DFFD9 File Offset: 0x000DE1D9
		public bool CursorForUpdate
		{
			get
			{
				return this.cursor_for_update;
			}
			set
			{
				this.cursor_for_update = value;
			}
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x060042A0 RID: 17056 RVA: 0x000DFFE2 File Offset: 0x000DE1E2
		// (set) Token: 0x060042A1 RID: 17057 RVA: 0x000DFFEA File Offset: 0x000DE1EA
		public bool CursorWithHold
		{
			get
			{
				return this.cursor_with_hold;
			}
			set
			{
				this.cursor_with_hold = value;
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x060042A2 RID: 17058 RVA: 0x000DFFF3 File Offset: 0x000DE1F3
		// (set) Token: 0x060042A3 RID: 17059 RVA: 0x000DFFFB File Offset: 0x000DE1FB
		public bool SqlDelete
		{
			get
			{
				return this.sqlDelete;
			}
			set
			{
				this.sqlDelete = value;
			}
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x060042A4 RID: 17060 RVA: 0x000E0004 File Offset: 0x000DE204
		// (set) Token: 0x060042A5 RID: 17061 RVA: 0x000E000C File Offset: 0x000DE20C
		public bool SqlUpdate
		{
			get
			{
				return this.sqlUpdate;
			}
			set
			{
				this.sqlUpdate = value;
			}
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x000E0018 File Offset: 0x000DE218
		public PackageProcedure(string procedure_catalog, string procedure_schema, string procedure_name, string parameter_name, short ordinal_position, short parameter_type, object parameter_default, bool is_nullable, int parameter_length, byte parameter_precision, byte parameter_scale, string type_name, SqlDbType data_type_sqldb, bool return_resultset, bool cursor_with_hold, bool cursor_for_update, bool hasOutputParams, string bnd_options, bool sqlDelete, bool sqlUpdate, string sqlstt)
		{
			this.procedure_catalog = procedure_catalog;
			this.procedure_schema = procedure_schema;
			this.procedure_name = procedure_name;
			this.cursor_with_hold = cursor_with_hold;
			this.cursor_for_update = cursor_for_update;
			this.hasOutputParams = hasOutputParams;
			this.BndOptions = bnd_options;
			this.sqlDelete = sqlDelete;
			this.sqlUpdate = sqlUpdate;
			this.sqlstt = sqlstt;
			this.return_resultset = return_resultset;
			this.AddSqlParameter(parameter_name, ordinal_position, parameter_type, parameter_default, is_nullable, parameter_length, parameter_precision, parameter_scale, type_name, data_type_sqldb);
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x000E00B4 File Offset: 0x000DE2B4
		public void AddSqlParameter(string paramName, short ordinal_position, short parameter_type, object parameter_default, bool is_nullable, int parameter_length, byte parameter_precision, byte parameter_scale, string type_name, SqlDbType data_type_sqldb)
		{
			SqlParameter sqlParameter = new SqlParameter(paramName, data_type_sqldb, parameter_length, (ParameterDirection)((parameter_type == 4) ? 6 : parameter_type), is_nullable, parameter_precision, parameter_scale, "", DataRowVersion.Default, parameter_default);
			if (parameter_length == 0 && (data_type_sqldb == SqlDbType.VarBinary || data_type_sqldb == SqlDbType.VarChar))
			{
				sqlParameter.Size = -1;
			}
			if ((int)ordinal_position > this.parameters.Length - 1)
			{
				Array.Resize<SqlParameter>(ref this.parameters, (int)(ordinal_position + 1));
			}
			this.parameters[(int)ordinal_position] = sqlParameter;
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x060042A8 RID: 17064 RVA: 0x000E0121 File Offset: 0x000DE321
		public SqlParameter[] Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x060042A9 RID: 17065 RVA: 0x000E0129 File Offset: 0x000DE329
		// (set) Token: 0x060042AA RID: 17066 RVA: 0x000E0131 File Offset: 0x000DE331
		public bool Return_resultset
		{
			get
			{
				return this.return_resultset;
			}
			set
			{
				this.return_resultset = value;
			}
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x000E013C File Offset: 0x000DE33C
		public void InitDatetimeFlags()
		{
			foreach (SqlParameter sqlParameter in this.parameters)
			{
				if (sqlParameter.SqlDbType == SqlDbType.Char || sqlParameter.SqlDbType == SqlDbType.VarChar)
				{
					if (this._datetimeFlags == null)
					{
						this._datetimeFlags = new Dictionary<string, bool>();
					}
					this._datetimeFlags[sqlParameter.ParameterName] = true;
				}
			}
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x000E019A File Offset: 0x000DE39A
		public bool IsDatetime(string paramName)
		{
			return !this._datetimeFlags.ContainsKey(paramName) || this._datetimeFlags[paramName];
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x000E01B8 File Offset: 0x000DE3B8
		public void SetDatetimeFlag(string paramName, bool isDatetime)
		{
			this._datetimeFlags[paramName] = isDatetime;
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x060042AE RID: 17070 RVA: 0x000E01C7 File Offset: 0x000DE3C7
		// (set) Token: 0x060042AF RID: 17071 RVA: 0x000E01CF File Offset: 0x000DE3CF
		public bool IsInsertStatement { get; set; }

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x060042B0 RID: 17072 RVA: 0x000E01D8 File Offset: 0x000DE3D8
		// (set) Token: 0x060042B1 RID: 17073 RVA: 0x000E01E0 File Offset: 0x000DE3E0
		public bool IsBulkCopyCompatible { get; set; }

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x060042B2 RID: 17074 RVA: 0x000E01E9 File Offset: 0x000DE3E9
		// (set) Token: 0x060042B3 RID: 17075 RVA: 0x000E01F1 File Offset: 0x000DE3F1
		public List<ColumnMapping> ColumnMappings { get; set; }

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x060042B4 RID: 17076 RVA: 0x000E01FA File Offset: 0x000DE3FA
		// (set) Token: 0x060042B5 RID: 17077 RVA: 0x000E0202 File Offset: 0x000DE402
		public string TableName { get; set; }

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x060042B6 RID: 17078 RVA: 0x000E020B File Offset: 0x000DE40B
		// (set) Token: 0x060042B7 RID: 17079 RVA: 0x000E0213 File Offset: 0x000DE413
		public bool IsSysProc { get; set; }

		// Token: 0x04002EA0 RID: 11936
		private string procedure_catalog;

		// Token: 0x04002EA1 RID: 11937
		private string procedure_schema;

		// Token: 0x04002EA2 RID: 11938
		private string procedure_qualifier;

		// Token: 0x04002EA3 RID: 11939
		private string procedure_name;

		// Token: 0x04002EA4 RID: 11940
		private string bnd_options;

		// Token: 0x04002EA5 RID: 11941
		private string sqlstt;

		// Token: 0x04002EA6 RID: 11942
		private bool return_resultset;

		// Token: 0x04002EA7 RID: 11943
		private bool cursor_with_hold;

		// Token: 0x04002EA8 RID: 11944
		private bool cursor_for_update;

		// Token: 0x04002EA9 RID: 11945
		private bool hasOutputParams;

		// Token: 0x04002EAA RID: 11946
		private bool sqlDelete;

		// Token: 0x04002EAB RID: 11947
		private bool sqlUpdate;

		// Token: 0x04002EAC RID: 11948
		private DateTime lastInvokeTime = DateTime.Now;

		// Token: 0x04002EAD RID: 11949
		private SqlParameter[] parameters = new SqlParameter[1];

		// Token: 0x04002EAE RID: 11950
		private Dictionary<string, bool> _datetimeFlags;
	}
}
