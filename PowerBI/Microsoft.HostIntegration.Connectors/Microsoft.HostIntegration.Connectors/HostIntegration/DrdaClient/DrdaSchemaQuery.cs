using System;
using System.Text;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A28 RID: 2600
	internal class DrdaSchemaQuery
	{
		// Token: 0x0600517C RID: 20860 RVA: 0x0014C11C File Offset: 0x0014A31C
		public DrdaSchemaQuery(string[] columnNames, Type[] columnTypes, int[] orderBy, string[] as400ColumnNames, string as400BaseTable, string[] mvsColumnNames, string mvsBaseTable, string[] udbColumnNames, string udbBaseTable)
		{
			this._columnNames = columnNames;
			this._columnTypes = columnTypes;
			this._orderBy = orderBy;
			this._as400ColumnNames = as400ColumnNames;
			this._as400BaseTable = as400BaseTable;
			this._mvsColumnNames = mvsColumnNames;
			this._mvsBaseTable = mvsBaseTable;
			this._udbColumnNames = udbColumnNames;
			this._udbBaseTable = udbBaseTable;
		}

		// Token: 0x0600517D RID: 20861 RVA: 0x0014C174 File Offset: 0x0014A374
		public static HostType GetHostType(DrdaConnection conn)
		{
			return conn.Requester.HostType;
		}

		// Token: 0x0600517E RID: 20862 RVA: 0x0014C181 File Offset: 0x0014A381
		public string GetCatalog(DrdaConnection conn, HostType hostType)
		{
			if (conn.DrdaConnectionString.ShadowCatalog.Length > 0)
			{
				return conn.DrdaConnectionString.ShadowCatalog;
			}
			if (hostType == HostType.AS400)
			{
				return "QSYS2";
			}
			if (hostType == HostType.MVS || hostType == HostType.DB2)
			{
				return "SYSIBM";
			}
			return "SYSCAT";
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x0014C1BF File Offset: 0x0014A3BF
		public string GetSchema(DrdaConnection conn)
		{
			return conn.DrdaConnectionString.DefaultSchema;
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x06005180 RID: 20864 RVA: 0x0014C1CC File Offset: 0x0014A3CC
		public int ColumnCount
		{
			get
			{
				return this._columnNames.Length;
			}
		}

		// Token: 0x06005181 RID: 20865 RVA: 0x0014C1D6 File Offset: 0x0014A3D6
		public string GetColumnName(int i)
		{
			return this._columnNames[i];
		}

		// Token: 0x06005182 RID: 20866 RVA: 0x0014C1E0 File Offset: 0x0014A3E0
		public string[] GetMVSColumnNames()
		{
			return this._mvsColumnNames;
		}

		// Token: 0x06005183 RID: 20867 RVA: 0x0014C1E8 File Offset: 0x0014A3E8
		public string[] GetAS400ColumnNames()
		{
			return this._as400ColumnNames;
		}

		// Token: 0x06005184 RID: 20868 RVA: 0x0014C1F0 File Offset: 0x0014A3F0
		public string[] GetUDBColumnNames()
		{
			return this._udbColumnNames;
		}

		// Token: 0x06005185 RID: 20869 RVA: 0x0014C1F8 File Offset: 0x0014A3F8
		public virtual string GetMVSTable(string catalog)
		{
			return string.Format("{0}.{1}", catalog, this._mvsBaseTable);
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x0014C20B File Offset: 0x0014A40B
		public virtual string GetAS400Table(string catalog)
		{
			return string.Format("{0}.{1}", catalog, this._as400BaseTable);
		}

		// Token: 0x06005187 RID: 20871 RVA: 0x0014C21E File Offset: 0x0014A41E
		public virtual string GetUDBTable(string catalog)
		{
			return string.Format("{0}.{1}", catalog, this._udbBaseTable);
		}

		// Token: 0x06005188 RID: 20872 RVA: 0x0014C231 File Offset: 0x0014A431
		public virtual string GetMVSRestriction(int restriction)
		{
			return this._mvsColumnNames[restriction];
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x0014C23B File Offset: 0x0014A43B
		public virtual string GetAS400Restriction(int restriction)
		{
			return this._as400ColumnNames[restriction];
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x0014C248 File Offset: 0x0014A448
		private string GetAS400RestrictionValue(int restriction, DrdaParameterCollection parameters)
		{
			string text = parameters[restriction].Value.ToString();
			if (this.GetAS400Restriction(restriction).Equals("TABLE_TYPE") && text != null)
			{
				if (!(text == "TABLE"))
				{
					if (!(text == "VIEW"))
					{
						if (text == "VIEW,TABLE" || text == "TABLE,VIEW")
						{
							text = "T,V";
						}
					}
					else
					{
						text = "V";
					}
				}
				else
				{
					text = "T";
				}
			}
			return text;
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x0014C2CA File Offset: 0x0014A4CA
		public virtual string GetUDBRestriction(int restriction)
		{
			return this._udbColumnNames[restriction];
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x0014C2D4 File Offset: 0x0014A4D4
		public string GetQuery(DrdaConnection conn, DrdaParameterCollection parameters)
		{
			HostType hostType = DrdaSchemaQuery.GetHostType(conn);
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			string catalog = this.GetCatalog(conn, hostType);
			string[] array;
			if (hostType != HostType.AS400)
			{
				if (hostType == HostType.MVS || hostType == HostType.DB2)
				{
					array = this.GetMVSColumnNames();
					text = this.GetMVSTable(catalog);
				}
				else
				{
					array = this.GetUDBColumnNames();
					text = this.GetUDBTable(catalog);
				}
			}
			else
			{
				array = this.GetAS400ColumnNames();
				text = this.GetAS400Table(catalog);
			}
			if (text.Length == 0)
			{
				return string.Empty;
			}
			stringBuilder.Append("SELECT ");
			for (int i = 0; i < this.ColumnCount; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				if (array[i] == null)
				{
					if (this._columnTypes[i] == typeof(string))
					{
						stringBuilder.Append("'' ");
					}
					else
					{
						stringBuilder.Append("0 ");
					}
				}
				else
				{
					if (this._columnTypes[i] == typeof(string))
					{
						stringBuilder.Append("RTRIM(");
					}
					stringBuilder.Append(array[i]);
					if (this._columnTypes[i] == typeof(string))
					{
						stringBuilder.Append(") ");
					}
				}
				stringBuilder.Append(" AS ");
				stringBuilder.Append(this.GetColumnName(i));
			}
			stringBuilder.Append(" FROM ");
			stringBuilder.Append(text);
			bool flag = false;
			for (int j = 1; j < parameters.Count; j++)
			{
				if (parameters[j].Value != null && ((string)parameters[j].Value).Length > 0)
				{
					if (!flag)
					{
						stringBuilder.Append(" WHERE ");
						flag = true;
					}
					else
					{
						stringBuilder.Append(" AND ");
					}
					string text2 = string.Empty;
					string text3 = string.Empty;
					if (hostType != HostType.AS400)
					{
						if (hostType == HostType.MVS || hostType == HostType.DB2)
						{
							text2 = this.GetMVSRestriction(j);
							text3 = this.GetAS400RestrictionValue(j, parameters);
						}
						else
						{
							text2 = this.GetUDBRestriction(j);
							text3 = parameters[j].Value.ToString();
						}
					}
					else
					{
						text2 = this.GetAS400Restriction(j);
						text3 = this.GetAS400RestrictionValue(j, parameters);
					}
					if (text3 == "T,V" && text2 == "TYPE")
					{
						stringBuilder.Append(text2);
						stringBuilder.Append(" = 'T' OR ");
						stringBuilder.Append(text2);
						stringBuilder.Append(" = ");
						stringBuilder.Append("'V' ");
					}
					else
					{
						stringBuilder.Append(text2);
						stringBuilder.Append(" = '");
						stringBuilder.Append(text3);
						stringBuilder.Append("'");
					}
				}
			}
			stringBuilder.Append(" ORDER BY ");
			for (int k = 0; k < this._orderBy.Length; k++)
			{
				if (k != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(this.GetColumnName(this._orderBy[k]));
			}
			stringBuilder.Append(" FOR FETCH ONLY");
			return stringBuilder.ToString();
		}

		// Token: 0x04004037 RID: 16439
		private string[] _columnNames;

		// Token: 0x04004038 RID: 16440
		private Type[] _columnTypes;

		// Token: 0x04004039 RID: 16441
		private int[] _orderBy;

		// Token: 0x0400403A RID: 16442
		private string[] _as400ColumnNames;

		// Token: 0x0400403B RID: 16443
		private string[] _mvsColumnNames;

		// Token: 0x0400403C RID: 16444
		private string[] _udbColumnNames;

		// Token: 0x0400403D RID: 16445
		private string _as400BaseTable;

		// Token: 0x0400403E RID: 16446
		private string _mvsBaseTable;

		// Token: 0x0400403F RID: 16447
		private string _udbBaseTable;
	}
}
