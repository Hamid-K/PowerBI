using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000C8 RID: 200
	public abstract class Session : IGetDataSource, IOpenRowset, ISessionProperties, IDBSchemaRowset, IDBCreateCommand
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000378 RID: 888
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public abstract DataSource DataSource
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000379 RID: 889
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public abstract IDBProperties Properties
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600037A RID: 890
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public abstract DbSchemaRowset SchemaRowset
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x0600037B RID: 891
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public abstract Command CreateCommand();

		// Token: 0x0600037C RID: 892 RVA: 0x0000A570 File Offset: 0x00008770
		int IGetDataSource.GetDataSource(ref Guid iid, out IntPtr dataSource)
		{
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(this.DataSource);
			int num = Marshal.QueryInterface(iunknownForObject, ref iid, out dataSource);
			Marshal.Release(iunknownForObject);
			return num;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000A598 File Offset: 0x00008798
		unsafe void IOpenRowset.OpenRowset(IntPtr punkOuter, DBID* tableID, DBID* indexID, ref Guid iid, uint propertySetCount, DBPROPSET* propertySets, out IntPtr rowset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000A59F File Offset: 0x0000879F
		unsafe int ISessionProperties.GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets)
		{
			return this.Properties.GetProperties(countPropertyIDSets, nativePropertyIDSets, out countPropertySets, out nativePropertySets);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000A5B1 File Offset: 0x000087B1
		unsafe int ISessionProperties.SetProperties(uint propertySetCount, DBPROPSET* propertySets)
		{
			return this.Properties.SetProperties(propertySetCount, propertySets);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000A5C0 File Offset: 0x000087C0
		unsafe int IDBSchemaRowset.GetRowset(IntPtr punkOuter, ref Guid schema, uint restrictionCount, VARIANT* restrictions, ref Guid iid, uint propertySetCount, DBPROPSET* propertySets, out IntPtr rowset)
		{
			IDBSchemaRowset schemaRowset = this.SchemaRowset;
			Guid guid = schema;
			return schemaRowset.GetRowset(punkOuter, ref guid, restrictionCount, restrictions, ref iid, propertySetCount, propertySets, out rowset);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000A5ED File Offset: 0x000087ED
		unsafe void IDBSchemaRowset.GetSchemas(out uint schemaCount, out Guid* schemas, out uint* restrictionSupport)
		{
			((IDBSchemaRowset)this.SchemaRowset).GetSchemas(out schemaCount, out schemas, out restrictionSupport);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000A600 File Offset: 0x00008800
		int IDBCreateCommand.CreateCommand(IntPtr punkOuter, ref Guid iid, out IntPtr ppv)
		{
			Command command = this.CreateCommand();
			return Aggregator.AggregateCommand(punkOuter, command, ref iid, out ppv);
		}
	}
}
