using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200041F RID: 1055
	internal sealed class SapHanaCatalogCollection : IEnumerable<SapHanaCatalog>, IEnumerable
	{
		// Token: 0x060023EF RID: 9199 RVA: 0x0006541F File Offset: 0x0006361F
		public SapHanaCatalogCollection(IResource resource, SapHanaOdbcDataSource dataSource, string cubesTableName, Version sapHanaVersion, bool useHierarchies)
		{
			this.resource = resource;
			this.dataSource = dataSource;
			this.cubesTableName = cubesTableName;
			this.useHierarchies = useHierarchies;
			this.version = sapHanaVersion;
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x0006544C File Offset: 0x0006364C
		public IEnumerator<SapHanaCatalog> GetEnumerator()
		{
			this.EnsureFetchCatalogs();
			return this.catalogs.Values.GetEnumerator();
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x00065469 File Offset: 0x00063669
		public bool TryGetCatalog(string name, out SapHanaCatalog catalog)
		{
			this.EnsureFetchCatalogs();
			return this.catalogs.TryGetValue(name, out catalog);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x0006547E File Offset: 0x0006367E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x00065488 File Offset: 0x00063688
		private void EnsureFetchCatalogs()
		{
			if (this.catalogs == null)
			{
				Dictionary<string, SapHanaCatalog> dictionary = new Dictionary<string, SapHanaCatalog>();
				using (IDataReader dataReader = this.dataSource.Execute(this.dataSource.Host.GetMetadataCache(), string.Format(CultureInfo.InvariantCulture, "SELECT CATALOG_NAME\r\nFROM _SYS_BI.{0}\r\nGROUP BY CATALOG_NAME", this.cubesTableName), null, EmptyArray<OdbcParameter>.Instance, RowRange.All, SapHanaCatalogCollection.columnNames, true, null))
				{
					while (dataReader.Read())
					{
						SapHanaCatalog sapHanaCatalog = new SapHanaCatalog(this.resource, this.dataSource, this.cubesTableName, (string)dataReader[0], this.version, this.useHierarchies);
						dictionary.Add(sapHanaCatalog.Name, sapHanaCatalog);
					}
				}
				this.catalogs = dictionary;
			}
		}

		// Token: 0x04000E78 RID: 3704
		private const string query = "SELECT CATALOG_NAME\r\nFROM _SYS_BI.{0}\r\nGROUP BY CATALOG_NAME";

		// Token: 0x04000E79 RID: 3705
		private static readonly string[] columnNames = new string[] { "CATALOG" };

		// Token: 0x04000E7A RID: 3706
		private readonly IResource resource;

		// Token: 0x04000E7B RID: 3707
		private readonly SapHanaOdbcDataSource dataSource;

		// Token: 0x04000E7C RID: 3708
		private readonly string cubesTableName;

		// Token: 0x04000E7D RID: 3709
		private readonly bool useHierarchies;

		// Token: 0x04000E7E RID: 3710
		private readonly Version version;

		// Token: 0x04000E7F RID: 3711
		private Dictionary<string, SapHanaCatalog> catalogs;
	}
}
