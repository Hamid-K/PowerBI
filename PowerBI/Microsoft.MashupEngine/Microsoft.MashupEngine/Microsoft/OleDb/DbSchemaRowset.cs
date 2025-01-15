using System;
using System.Collections.Generic;
using Microsoft.OleDb.Marshallers;

namespace Microsoft.OleDb
{
	// Token: 0x02001E9B RID: 7835
	public class DbSchemaRowset : IDBSchemaRowset
	{
		// Token: 0x0600C1B6 RID: 49590 RVA: 0x0026F2D6 File Offset: 0x0026D4D6
		public static DbSchemaRowset Create(IInteropServices interopServices, params Schema[] schemas)
		{
			return new DbSchemaRowset(interopServices, schemas);
		}

		// Token: 0x0600C1B7 RID: 49591 RVA: 0x0026F2E0 File Offset: 0x0026D4E0
		private DbSchemaRowset(IInteropServices interopServices, IEnumerable<Schema> schemas)
		{
			this.interopServices = interopServices;
			this.schemas = new Dictionary<Guid, Schema>();
			foreach (Schema schema in schemas)
			{
				this.schemas.Add(schema.Guid, schema);
			}
		}

		// Token: 0x0600C1B8 RID: 49592 RVA: 0x0026F34C File Offset: 0x0026D54C
		unsafe void IDBSchemaRowset.GetSchemas(out uint countSchemas, out Guid* nativeSchemas, out uint* nativeRestrictionSupport)
		{
			using (ComHeap comHeap = new ComHeap())
			{
				countSchemas = (uint)this.schemas.Count;
				nativeSchemas = comHeap.AllocArray(this.schemas.Count, sizeof(Guid));
				nativeRestrictionSupport = comHeap.AllocArray(this.schemas.Count, 4);
				int num = 0;
				foreach (Schema schema in this.schemas.Values)
				{
					*(nativeSchemas + (IntPtr)num * (IntPtr)sizeof(Guid)) = schema.Guid;
					*(nativeRestrictionSupport + (IntPtr)num * 4) = (int)DbSchemaRowset.GetRestrictions(schema);
					num++;
				}
				comHeap.Commit();
			}
		}

		// Token: 0x0600C1B9 RID: 49593 RVA: 0x0026F428 File Offset: 0x0026D628
		unsafe int IDBSchemaRowset.GetRowset(IntPtr punkOuter, ref Guid guidSchema, uint countRestrictions, VARIANT* nativeRestrictions, ref Guid iid, uint countPropertySets, DBPROPSET* nativePropertySets, out IntPtr ppv)
		{
			ppv = IntPtr.Zero;
			Dictionary<int, object> dictionary = new Dictionary<int, object>();
			int num = 0;
			while ((long)num < (long)((ulong)countRestrictions))
			{
				if (nativeRestrictions[num].vt != VARTYPE.EMPTY)
				{
					object managed = VariantMarshaller.Instance.GetManaged((IntPtr)((void*)(nativeRestrictions + num)));
					dictionary.Add(num, managed);
				}
				num++;
			}
			Dictionary<DBPROPID, object> dictionary2 = new Dictionary<DBPROPID, object>();
			int num2 = 0;
			while ((long)num2 < (long)((ulong)countPropertySets))
			{
				DBPROPSET* ptr = nativePropertySets + num2;
				int num3 = 0;
				while ((long)num3 < (long)((ulong)ptr->cProperties))
				{
					DBPROP* ptr2 = ptr->rgProperties + num3;
					dictionary2.Add(ptr2->dwPropertyID, VariantMarshaller.Instance.GetManaged((IntPtr)((void*)(&ptr2->variant))));
					num3++;
				}
				num2++;
			}
			IRowset rowset = this.schemas[guidSchema].CreateRowset(dictionary, dictionary2);
			return this.interopServices.AggregateRowset(punkOuter, rowset, ref iid, out ppv);
		}

		// Token: 0x0600C1BA RID: 49594 RVA: 0x0026F530 File Offset: 0x0026D730
		private static uint GetRestrictions(Schema schema)
		{
			uint num = 0U;
			foreach (int num2 in schema.ColumnRestrictions)
			{
				num |= 1U << num2;
			}
			return num;
		}

		// Token: 0x040061A6 RID: 24998
		private readonly Dictionary<Guid, Schema> schemas;

		// Token: 0x040061A7 RID: 24999
		private readonly IInteropServices interopServices;
	}
}
