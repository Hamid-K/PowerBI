using System;
using System.Collections;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000052 RID: 82
	public sealed class SqlBulkCopyColumnMappingCollection : CollectionBase
	{
		// Token: 0x06000825 RID: 2085 RVA: 0x00012C9E File Offset: 0x00010E9E
		internal SqlBulkCopyColumnMappingCollection()
		{
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00012CA6 File Offset: 0x00010EA6
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x00012CAE File Offset: 0x00010EAE
		internal bool ReadOnly { get; set; }

		// Token: 0x17000677 RID: 1655
		public SqlBulkCopyColumnMapping this[int index]
		{
			get
			{
				return (SqlBulkCopyColumnMapping)base.List[index];
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00012CCC File Offset: 0x00010ECC
		public SqlBulkCopyColumnMapping Add(SqlBulkCopyColumnMapping bulkCopyColumnMapping)
		{
			this.AssertWriteAccess();
			if ((string.IsNullOrEmpty(bulkCopyColumnMapping.SourceColumn) && bulkCopyColumnMapping.SourceOrdinal == -1) || (string.IsNullOrEmpty(bulkCopyColumnMapping.DestinationColumn) && bulkCopyColumnMapping.DestinationOrdinal == -1))
			{
				throw SQL.BulkLoadNonMatchingColumnMapping();
			}
			base.InnerList.Add(bulkCopyColumnMapping);
			return bulkCopyColumnMapping;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00012D1F File Offset: 0x00010F1F
		public SqlBulkCopyColumnMapping Add(string sourceColumn, string destinationColumn)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumn, destinationColumn));
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00012D34 File Offset: 0x00010F34
		public SqlBulkCopyColumnMapping Add(int sourceColumnIndex, string destinationColumn)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumnIndex, destinationColumn));
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00012D49 File Offset: 0x00010F49
		public SqlBulkCopyColumnMapping Add(string sourceColumn, int destinationColumnIndex)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumn, destinationColumnIndex));
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00012D5E File Offset: 0x00010F5E
		public SqlBulkCopyColumnMapping Add(int sourceColumnIndex, int destinationColumnIndex)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumnIndex, destinationColumnIndex));
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00012D73 File Offset: 0x00010F73
		private void AssertWriteAccess()
		{
			if (this.ReadOnly)
			{
				throw SQL.BulkLoadMappingInaccessible();
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00012D83 File Offset: 0x00010F83
		public new void Clear()
		{
			this.AssertWriteAccess();
			base.Clear();
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00012D91 File Offset: 0x00010F91
		public bool Contains(SqlBulkCopyColumnMapping value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00012D9F File Offset: 0x00010F9F
		public void CopyTo(SqlBulkCopyColumnMapping[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00012DB0 File Offset: 0x00010FB0
		internal void CreateDefaultMapping(int columnCount)
		{
			for (int i = 0; i < columnCount; i++)
			{
				base.InnerList.Add(new SqlBulkCopyColumnMapping(i, i));
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00012DDC File Offset: 0x00010FDC
		public int IndexOf(SqlBulkCopyColumnMapping value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00012DEA File Offset: 0x00010FEA
		public void Insert(int index, SqlBulkCopyColumnMapping value)
		{
			this.AssertWriteAccess();
			base.InnerList.Insert(index, value);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00012DFF File Offset: 0x00010FFF
		public void Remove(SqlBulkCopyColumnMapping value)
		{
			this.AssertWriteAccess();
			base.InnerList.Remove(value);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00012E13 File Offset: 0x00011013
		public new void RemoveAt(int index)
		{
			this.AssertWriteAccess();
			base.RemoveAt(index);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00012E24 File Offset: 0x00011024
		internal void ValidateCollection()
		{
			foreach (object obj in base.InnerList)
			{
				SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = (SqlBulkCopyColumnMapping)obj;
				SqlBulkCopyColumnMappingCollection.MappingSchema mappingSchema = ((sqlBulkCopyColumnMapping.SourceOrdinal != -1) ? ((sqlBulkCopyColumnMapping.DestinationOrdinal != -1) ? SqlBulkCopyColumnMappingCollection.MappingSchema.OrdinalsOrdinals : SqlBulkCopyColumnMappingCollection.MappingSchema.OrdinalsNames) : ((sqlBulkCopyColumnMapping.DestinationOrdinal != -1) ? SqlBulkCopyColumnMappingCollection.MappingSchema.NamesOrdinals : SqlBulkCopyColumnMappingCollection.MappingSchema.NamesNames));
				if (this._mappingSchema == SqlBulkCopyColumnMappingCollection.MappingSchema.Undefined)
				{
					this._mappingSchema = mappingSchema;
				}
				else if (this._mappingSchema != mappingSchema)
				{
					throw SQL.BulkLoadMappingsNamesOrOrdinalsOnly();
				}
			}
		}

		// Token: 0x04000128 RID: 296
		private SqlBulkCopyColumnMappingCollection.MappingSchema _mappingSchema;

		// Token: 0x020001B9 RID: 441
		private enum MappingSchema
		{
			// Token: 0x0400130E RID: 4878
			Undefined,
			// Token: 0x0400130F RID: 4879
			NamesNames,
			// Token: 0x04001310 RID: 4880
			NamesOrdinals,
			// Token: 0x04001311 RID: 4881
			OrdinalsNames,
			// Token: 0x04001312 RID: 4882
			OrdinalsOrdinals
		}
	}
}
