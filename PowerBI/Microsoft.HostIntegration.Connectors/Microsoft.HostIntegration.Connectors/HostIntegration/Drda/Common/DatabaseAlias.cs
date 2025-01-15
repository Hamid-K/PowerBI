using System;
using System.Collections;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007BD RID: 1981
	public class DatabaseAlias
	{
		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06003EEC RID: 16108 RVA: 0x000D2D41 File Offset: 0x000D0F41
		public ArrayList Entries
		{
			get
			{
				return this.entries;
			}
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x000D2D49 File Offset: 0x000D0F49
		public DatabaseAlias()
		{
			this.entries = new ArrayList();
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x000D2D5C File Offset: 0x000D0F5C
		public void AddEntry(DatabaseAliasEntry entry)
		{
			this.entries.Add(entry);
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x000D2D6C File Offset: 0x000D0F6C
		public bool Convert(string tableName, out string newTableName, bool matchRDBName)
		{
			using (IEnumerator enumerator = this.entries.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((DatabaseAliasEntry)enumerator.Current).Convert(tableName, out newTableName, matchRDBName))
					{
						return true;
					}
				}
			}
			newTableName = null;
			return false;
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x000D2DD4 File Offset: 0x000D0FD4
		public bool Convert(string tableName, out string newTableName)
		{
			return this.Convert(tableName, out newTableName, true);
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x000D2DE0 File Offset: 0x000D0FE0
		public bool Contains(DatabaseAliasEntry entry)
		{
			using (IEnumerator enumerator = this.entries.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((DatabaseAliasEntry)enumerator.Current).Equals(entry))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x000D2E40 File Offset: 0x000D1040
		public string MapRdbColId(string rdbName, string rdbcolid)
		{
			foreach (object obj in this.entries)
			{
				DatabaseAliasEntry databaseAliasEntry = (DatabaseAliasEntry)obj;
				string text = rdbcolid;
				if (databaseAliasEntry.MapRdbColId(rdbName, rdbcolid, out text))
				{
					return text;
				}
			}
			return rdbcolid;
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x000D2EA8 File Offset: 0x000D10A8
		public string MapRdbName(string rdbName)
		{
			foreach (object obj in this.entries)
			{
				DatabaseAliasEntry databaseAliasEntry = (DatabaseAliasEntry)obj;
				string text = rdbName;
				if (databaseAliasEntry.MapRdbName(rdbName, out text))
				{
					return text;
				}
			}
			return rdbName;
		}

		// Token: 0x04002BA9 RID: 11177
		private ArrayList entries;
	}
}
