using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EA9 RID: 7849
	public static class IOpenRowsetExtensions
	{
		// Token: 0x0600C210 RID: 49680 RVA: 0x0026FE50 File Offset: 0x0026E050
		public unsafe static IRowset OpenRowset(this IOpenRowset openRowset, string tableName, DbPropertySet[] propertySets)
		{
			IntPtr zero = IntPtr.Zero;
			Guid irowset = IID.IRowset;
			IRowset rowset;
			try
			{
				using (ComHeap comHeap = new ComHeap())
				{
					char* ptr = comHeap.AllocString(tableName);
					DBID dbid = new DBID
					{
						eKind = DBKIND.NAME,
						pwszName = ptr
					};
					uint num;
					DBPROPSET* ptr2;
					DbPropertySets.GetPropertySets(propertySets, comHeap, out num, out ptr2);
					openRowset.OpenRowset(IntPtr.Zero, &dbid, null, ref irowset, num, ptr2, out zero);
					rowset = (IRowset)Marshal.GetObjectForIUnknown(zero);
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Marshal.Release(zero);
				}
			}
			return rowset;
		}
	}
}
