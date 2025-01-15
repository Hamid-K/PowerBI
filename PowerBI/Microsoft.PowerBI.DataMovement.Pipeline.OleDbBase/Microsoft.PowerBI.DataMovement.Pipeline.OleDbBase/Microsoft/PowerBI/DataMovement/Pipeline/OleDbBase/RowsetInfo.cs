using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000C0 RID: 192
	public class RowsetInfo : IRowsetInfo
	{
		// Token: 0x0600034C RID: 844 RVA: 0x0000995A File Offset: 0x00007B5A
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public RowsetInfo(IDBProperties properties)
		{
			this.properties = properties;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00009969 File Offset: 0x00007B69
		unsafe int IRowsetInfo.GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets)
		{
			return this.properties.GetProperties(countPropertyIDSets, nativePropertyIDSets, out countPropertySets, out nativePropertySets);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000997B File Offset: 0x00007B7B
		void IRowsetInfo.GetReferencedRowset(DBORDINAL ordinal, ref Guid iid, out IntPtr referencedRowset)
		{
			referencedRowset = IntPtr.Zero;
			throw new NotImplementedException();
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00009989 File Offset: 0x00007B89
		void IRowsetInfo.GetSpecification(ref Guid iid, out IntPtr specification)
		{
			specification = IntPtr.Zero;
			throw new NotImplementedException();
		}

		// Token: 0x04000377 RID: 887
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private readonly IDBProperties properties;
	}
}
