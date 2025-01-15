using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000042 RID: 66
	public class DbSchemaRowset : IDBSchemaRowset
	{
		// Token: 0x06000257 RID: 599 RVA: 0x00007AE8 File Offset: 0x00005CE8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private DbSchemaRowset(IEnumerable<Schema> schemas)
		{
			this.schemas = new Dictionary<Guid, Schema>();
			foreach (Schema schema in schemas)
			{
				this.schemas.Add(schema.Guid, schema);
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00007B4C File Offset: 0x00005D4C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static DbSchemaRowset Create(params Schema[] schemas)
		{
			return new DbSchemaRowset(schemas);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00007B54 File Offset: 0x00005D54
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

		// Token: 0x0600025A RID: 602 RVA: 0x00007C30 File Offset: 0x00005E30
		unsafe int IDBSchemaRowset.GetRowset(IntPtr punkOuter, ref Guid guidSchema, uint countRestrictions, VARIANT* nativeRestrictions, ref Guid iid, uint countPropertySets, DBPROPSET* nativePropertySets, out IntPtr ppv)
		{
			ppv = IntPtr.Zero;
			Dictionary<int, object> dictionary = new Dictionary<int, object>();
			int num = 0;
			while ((long)num < (long)((ulong)countRestrictions))
			{
				if (nativeRestrictions[num].Type != VARTYPE.EMPTY)
				{
					object @object = Variant.GetObject(nativeRestrictions + num);
					dictionary.Add(num, @object);
				}
				num++;
			}
			DictionaryDBProperties dictionaryDBProperties = new DictionaryDBProperties(Array.Empty<PropertyInfo>());
			Marshal.ThrowExceptionForHR(((IDBProperties)dictionaryDBProperties).SetProperties(countPropertySets, nativePropertySets));
			IRowset rowset = this.schemas[guidSchema].CreateRowset(dictionary, dictionaryDBProperties);
			return Aggregator.AggregateRowset(punkOuter, rowset, ref iid, out ppv);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007CC8 File Offset: 0x00005EC8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static uint GetRestrictions(Schema schema)
		{
			uint num = 0U;
			foreach (int num2 in schema.ColumnRestrictions)
			{
				num |= 1U << num2;
			}
			return num;
		}

		// Token: 0x04000093 RID: 147
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private readonly Dictionary<Guid, Schema> schemas;
	}
}
