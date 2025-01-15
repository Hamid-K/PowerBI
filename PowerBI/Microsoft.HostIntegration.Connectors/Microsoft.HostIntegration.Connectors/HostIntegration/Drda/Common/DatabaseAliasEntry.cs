using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007BE RID: 1982
	public class DatabaseAliasEntry
	{
		// Token: 0x06003EF4 RID: 16116 RVA: 0x000D2F0C File Offset: 0x000D110C
		public DatabaseAliasEntry(string sourceCatalog, string sourceSchema, string targetCatalog, string targetSchema)
		{
			this.sourceCatalog = sourceCatalog;
			this.sourceSchema = sourceSchema;
			this.targetCatalog = targetCatalog;
			this.targetSchema = targetSchema;
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x000D2F34 File Offset: 0x000D1134
		public bool Convert(string tableName, out string newTableName, bool matchRDBName)
		{
			newTableName = tableName;
			if (string.IsNullOrWhiteSpace(tableName))
			{
				return false;
			}
			string[] array = tableName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 2)
			{
				if (array[0].Equals(this.sourceSchema, StringComparison.CurrentCultureIgnoreCase) && ((matchRDBName && string.IsNullOrWhiteSpace(this.sourceCatalog)) || !matchRDBName))
				{
					if (!matchRDBName || string.IsNullOrWhiteSpace(this.targetCatalog))
					{
						newTableName = string.Format("{0}.{1}", this.targetSchema, array[1]);
					}
					else
					{
						newTableName = string.Format("{0}.{1}.{2}", this.targetCatalog, this.targetSchema, array[1]);
					}
					return true;
				}
			}
			else if (array.Length == 3)
			{
				if (!string.IsNullOrWhiteSpace(this.sourceCatalog) && this.sourceCatalog.Trim() == "*" && !string.IsNullOrWhiteSpace(this.targetCatalog) && !string.IsNullOrWhiteSpace(this.sourceSchema) && this.sourceSchema.Trim() == "*" && !string.IsNullOrWhiteSpace(this.targetSchema) && this.targetSchema.Trim() == "*")
				{
					newTableName = string.Format("{0}.{1}.{2}", this.targetCatalog, array[1], array[2]);
					return true;
				}
				if (!string.IsNullOrWhiteSpace(this.sourceCatalog) && !string.IsNullOrWhiteSpace(this.targetCatalog) && !string.IsNullOrWhiteSpace(this.sourceSchema) && !string.IsNullOrWhiteSpace(this.targetSchema) && array[0].Equals(this.sourceCatalog, StringComparison.CurrentCultureIgnoreCase))
				{
					if (this.SourceSchema == "" || this.sourceSchema == "*")
					{
						if (this.targetSchema == "" || this.targetSchema == "*")
						{
							newTableName = string.Format("{0}.{1}.{2}", this.targetCatalog, array[1], array[2]);
						}
						else
						{
							newTableName = string.Format("{0}.{1}.{2}", this.targetCatalog, this.targetSchema, array[2]);
						}
						return true;
					}
					if (array[1].Equals(this.sourceSchema, StringComparison.CurrentCultureIgnoreCase))
					{
						if (this.targetSchema == "" || this.targetSchema == "*")
						{
							newTableName = string.Format("{0}.{1}.{2}", this.targetCatalog, array[1], array[2]);
						}
						else
						{
							newTableName = string.Format("{0}.{1}.{2}", this.targetCatalog, this.targetSchema, array[2]);
						}
						return true;
					}
				}
				if (!string.IsNullOrWhiteSpace(this.sourceCatalog) && !string.IsNullOrWhiteSpace(this.targetCatalog) && !string.IsNullOrWhiteSpace(this.sourceSchema) && this.sourceSchema.Trim() == "*" && !string.IsNullOrWhiteSpace(this.targetSchema) && this.targetSchema.Trim() != "*" && array[0].Equals(this.sourceCatalog, StringComparison.CurrentCultureIgnoreCase))
				{
					newTableName = string.Format("{0}.{1}.{2}", this.targetCatalog, this.targetSchema, array[2]);
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06003EF6 RID: 16118 RVA: 0x000D3247 File Offset: 0x000D1447
		// (set) Token: 0x06003EF7 RID: 16119 RVA: 0x000D324F File Offset: 0x000D144F
		public string SourceCatalog
		{
			get
			{
				return this.sourceCatalog;
			}
			set
			{
				this.sourceCatalog = value;
			}
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06003EF8 RID: 16120 RVA: 0x000D3258 File Offset: 0x000D1458
		// (set) Token: 0x06003EF9 RID: 16121 RVA: 0x000D3260 File Offset: 0x000D1460
		public string SourceSchema
		{
			get
			{
				return this.sourceSchema;
			}
			set
			{
				this.sourceSchema = value;
			}
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06003EFA RID: 16122 RVA: 0x000D3269 File Offset: 0x000D1469
		// (set) Token: 0x06003EFB RID: 16123 RVA: 0x000D3271 File Offset: 0x000D1471
		public string TargetCatalog
		{
			get
			{
				return this.targetCatalog;
			}
			set
			{
				this.targetCatalog = value;
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06003EFC RID: 16124 RVA: 0x000D327A File Offset: 0x000D147A
		// (set) Token: 0x06003EFD RID: 16125 RVA: 0x000D3282 File Offset: 0x000D1482
		public string TargetSchema
		{
			get
			{
				return this.targetSchema;
			}
			set
			{
				this.targetSchema = value;
			}
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x000D328C File Offset: 0x000D148C
		public override bool Equals(object obj)
		{
			if (obj is DatabaseAliasEntry)
			{
				DatabaseAliasEntry databaseAliasEntry = (DatabaseAliasEntry)obj;
				return databaseAliasEntry.sourceCatalog == this.sourceCatalog && databaseAliasEntry.sourceSchema == this.sourceSchema;
			}
			return false;
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x000D32D0 File Offset: 0x000D14D0
		public override int GetHashCode()
		{
			int num = 0;
			if (this.sourceCatalog != null)
			{
				num ^= this.sourceCatalog.GetHashCode();
			}
			if (this.sourceSchema != null)
			{
				num ^= this.sourceSchema.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x000D330C File Offset: 0x000D150C
		internal bool MapRdbColId(string rdbName, string rdbcolid, out string newRdbcolid)
		{
			newRdbcolid = rdbcolid;
			if (!string.IsNullOrWhiteSpace(this.sourceSchema) && !string.IsNullOrWhiteSpace(this.targetSchema) && this.sourceSchema.Equals(rdbcolid, StringComparison.CurrentCultureIgnoreCase) && (string.IsNullOrWhiteSpace(this.targetCatalog) || this.targetCatalog == "*" || string.IsNullOrWhiteSpace(rdbName) || rdbName.Equals(this.targetCatalog, StringComparison.CurrentCultureIgnoreCase)))
			{
				newRdbcolid = this.targetSchema;
				return true;
			}
			return false;
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x000D3386 File Offset: 0x000D1586
		internal bool MapRdbName(string rdbName, out string newRdbName)
		{
			newRdbName = rdbName;
			if (!string.IsNullOrWhiteSpace(this.sourceCatalog) && !string.IsNullOrWhiteSpace(this.targetCatalog) && this.sourceCatalog.Equals(rdbName, StringComparison.CurrentCultureIgnoreCase))
			{
				newRdbName = this.targetCatalog;
				return true;
			}
			return false;
		}

		// Token: 0x04002BAA RID: 11178
		private string sourceCatalog;

		// Token: 0x04002BAB RID: 11179
		private string sourceSchema;

		// Token: 0x04002BAC RID: 11180
		private string targetCatalog;

		// Token: 0x04002BAD RID: 11181
		private string targetSchema;
	}
}
