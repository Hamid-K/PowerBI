using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000424 RID: 1060
	internal abstract class SapHanaCubeCollectionBase : IEnumerable<SapHanaCubeBase>, IEnumerable
	{
		// Token: 0x06002418 RID: 9240 RVA: 0x00065DCB File Offset: 0x00063FCB
		protected SapHanaCubeCollectionBase(SapHanaOdbcDataSource dataSource, string cubesTableName, SapHanaCatalog catalog, bool useHierarchies)
		{
			this.dataSource = dataSource;
			this.cubesTableName = cubesTableName;
			this.catalog = catalog;
			this.useHierarchies = useHierarchies;
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06002419 RID: 9241
		protected abstract string CubesQuery { get; }

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x0600241A RID: 9242
		protected abstract string[] CubesQueryColumns { get; }

		// Token: 0x0600241B RID: 9243 RVA: 0x00065DF0 File Offset: 0x00063FF0
		public IEnumerator<SapHanaCubeBase> GetEnumerator()
		{
			this.EnsureCubesFetched();
			return this.cubes.Values.GetEnumerator();
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x00065E0D File Offset: 0x0006400D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x00065E15 File Offset: 0x00064015
		public bool TryGetCube(string name, out SapHanaCubeBase cube)
		{
			this.EnsureCubesFetched();
			return this.cubes.TryGetValue(name, out cube);
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x00065E2C File Offset: 0x0006402C
		private void EnsureCubesFetched()
		{
			if (this.cubes == null)
			{
				Dictionary<string, SapHanaCubeBase> dictionary = new Dictionary<string, SapHanaCubeBase>();
				using (IDataReader dataReader = this.dataSource.Execute(this.dataSource.Host.GetMetadataCache(), string.Format(CultureInfo.InvariantCulture, this.CubesQuery, this.cubesTableName), null, new OdbcParameter[]
				{
					new OdbcParameter(this.catalog.Name, OdbcTypeMap.WVarchar)
				}, RowRange.All, this.CubesQueryColumns, true, null))
				{
					while (dataReader.Read())
					{
						SapHanaCubeBase sapHanaCubeBase;
						if (this.TryReadCube(dataReader, out sapHanaCubeBase))
						{
							dictionary[sapHanaCubeBase.Name] = sapHanaCubeBase;
						}
					}
				}
				this.cubes = dictionary;
			}
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x00065EF0 File Offset: 0x000640F0
		private bool TryReadCube(IDataReader reader, out SapHanaCubeBase cube)
		{
			return SapHanaCubeBase.TryCreateCube(reader, this.catalog.Resource, this.dataSource, this.catalog.Name, this.useHierarchies, out cube);
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x00065F1B File Offset: 0x0006411B
		public static SapHanaCubeCollectionBase CreateCubeCollection(SapHanaOdbcDataSource dataSource, string cubesTableName, SapHanaCatalog catalog, bool useHierarchies, Version sapHanaVersion)
		{
			if (sapHanaVersion != null && ((sapHanaVersion.Major == 1 && sapHanaVersion.Build >= 110) || sapHanaVersion.Major >= 2))
			{
				return new SapHanaHDICapableCubeCollection(dataSource, cubesTableName, catalog, useHierarchies);
			}
			return new SapHanaCubeCollection(dataSource, cubesTableName, catalog, useHierarchies);
		}

		// Token: 0x04000E95 RID: 3733
		protected readonly SapHanaOdbcDataSource dataSource;

		// Token: 0x04000E96 RID: 3734
		protected readonly SapHanaCatalog catalog;

		// Token: 0x04000E97 RID: 3735
		protected readonly string cubesTableName;

		// Token: 0x04000E98 RID: 3736
		protected readonly bool useHierarchies;

		// Token: 0x04000E99 RID: 3737
		protected Dictionary<string, SapHanaCubeBase> cubes;
	}
}
