using System;
using System.Collections;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009AA RID: 2474
	public sealed class DrdaBulkCopyColumnMappingCollection : CollectionBase
	{
		// Token: 0x06004CA4 RID: 19620 RVA: 0x00132899 File Offset: 0x00130A99
		internal DrdaBulkCopyColumnMappingCollection()
		{
		}

		// Token: 0x06004CA5 RID: 19621 RVA: 0x001328A4 File Offset: 0x00130AA4
		public DrdaBulkCopyColumnMapping Add(int sourceColumnIndex, int destinationColumnIndex)
		{
			DrdaBulkCopyColumnMapping drdaBulkCopyColumnMapping = new DrdaBulkCopyColumnMapping(sourceColumnIndex, destinationColumnIndex);
			base.List.Add(drdaBulkCopyColumnMapping);
			return drdaBulkCopyColumnMapping;
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x001328C8 File Offset: 0x00130AC8
		public DrdaBulkCopyColumnMapping Add(string sourceColumn, int destinationColumnIndex)
		{
			DrdaBulkCopyColumnMapping drdaBulkCopyColumnMapping = new DrdaBulkCopyColumnMapping(sourceColumn, destinationColumnIndex);
			base.List.Add(drdaBulkCopyColumnMapping);
			return drdaBulkCopyColumnMapping;
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x001328EC File Offset: 0x00130AEC
		public DrdaBulkCopyColumnMapping Add(int sourceColumnIndex, string destinationColumn)
		{
			DrdaBulkCopyColumnMapping drdaBulkCopyColumnMapping = new DrdaBulkCopyColumnMapping(sourceColumnIndex, destinationColumn);
			base.List.Add(drdaBulkCopyColumnMapping);
			return drdaBulkCopyColumnMapping;
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x00132910 File Offset: 0x00130B10
		public DrdaBulkCopyColumnMapping Add(string sourceColumn, string destinationColumn)
		{
			DrdaBulkCopyColumnMapping drdaBulkCopyColumnMapping = new DrdaBulkCopyColumnMapping(sourceColumn, destinationColumn);
			base.List.Add(drdaBulkCopyColumnMapping);
			return drdaBulkCopyColumnMapping;
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x00132933 File Offset: 0x00130B33
		public DrdaBulkCopyColumnMapping Add(DrdaBulkCopyColumnMapping bulkCopyColumnMapping)
		{
			base.List.Add(bulkCopyColumnMapping);
			return bulkCopyColumnMapping;
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x00132943 File Offset: 0x00130B43
		public bool Contains(DrdaBulkCopyColumnMapping value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x00132951 File Offset: 0x00130B51
		public void CopyTo(DrdaBulkCopyColumnMapping[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06004CAC RID: 19628 RVA: 0x00132960 File Offset: 0x00130B60
		public int IndexOf(DrdaBulkCopyColumnMapping value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x0013296E File Offset: 0x00130B6E
		public void Insert(int index, DrdaBulkCopyColumnMapping value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x0013297D File Offset: 0x00130B7D
		public void Remove(DrdaBulkCopyColumnMapping value)
		{
			base.List.Remove(value);
		}

		// Token: 0x1700128F RID: 4751
		public DrdaBulkCopyColumnMapping this[int index]
		{
			get
			{
				return base.List[index] as DrdaBulkCopyColumnMapping;
			}
		}
	}
}
