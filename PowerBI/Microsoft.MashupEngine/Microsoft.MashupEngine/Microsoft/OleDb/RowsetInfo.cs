using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001F25 RID: 7973
	public class RowsetInfo : IRowsetInfo
	{
		// Token: 0x0600C35A RID: 50010 RVA: 0x00271ED2 File Offset: 0x002700D2
		public RowsetInfo(IDBProperties properties)
		{
			this.properties = properties;
		}

		// Token: 0x0600C35B RID: 50011 RVA: 0x00271EE1 File Offset: 0x002700E1
		unsafe int IRowsetInfo.GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets)
		{
			return this.properties.GetProperties(countPropertyIDSets, nativePropertyIDSets, out countPropertySets, out nativePropertySets);
		}

		// Token: 0x0600C35C RID: 50012 RVA: 0x00271EF3 File Offset: 0x002700F3
		void IRowsetInfo.GetReferencedRowset(DBORDINAL iOrdinal, ref Guid iid, out IntPtr referencedRowset)
		{
			referencedRowset = IntPtr.Zero;
			throw new NotImplementedException();
		}

		// Token: 0x0600C35D RID: 50013 RVA: 0x00271F01 File Offset: 0x00270101
		void IRowsetInfo.GetSpecification(ref Guid iid, out IntPtr specification)
		{
			specification = IntPtr.Zero;
			throw new NotImplementedException();
		}

		// Token: 0x04006477 RID: 25719
		private readonly IDBProperties properties;
	}
}
