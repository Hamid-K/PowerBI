using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001F2B RID: 7979
	public abstract class Session : IGetDataSource, IOpenRowset, ISessionProperties, IDBSchemaRowset, IDBCreateCommand
	{
		// Token: 0x17002FBF RID: 12223
		// (get) Token: 0x0600C387 RID: 50055
		public abstract DataSource DataSource { get; }

		// Token: 0x17002FC0 RID: 12224
		// (get) Token: 0x0600C388 RID: 50056
		public abstract IDBProperties Properties { get; }

		// Token: 0x17002FC1 RID: 12225
		// (get) Token: 0x0600C389 RID: 50057
		public abstract DbSchemaRowset SchemaRowset { get; }

		// Token: 0x17002FC2 RID: 12226
		// (get) Token: 0x0600C38A RID: 50058 RVA: 0x00247E02 File Offset: 0x00246002
		public virtual Guid RowsetDialect
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x0600C38B RID: 50059
		public abstract Command CreateCommand();

		// Token: 0x17002FC3 RID: 12227
		// (get) Token: 0x0600C38C RID: 50060 RVA: 0x00272E6A File Offset: 0x0027106A
		public IInteropServices InteropServices
		{
			get
			{
				return this.DataSource.InteropServices;
			}
		}

		// Token: 0x17002FC4 RID: 12228
		// (get) Token: 0x0600C38D RID: 50061 RVA: 0x00272E77 File Offset: 0x00271077
		public IManagedDataConvert DataConvert
		{
			get
			{
				return this.DataSource.DataConvert;
			}
		}

		// Token: 0x0600C38E RID: 50062 RVA: 0x00272E84 File Offset: 0x00271084
		int IGetDataSource.GetDataSource(ref Guid iid, out IntPtr dataSource)
		{
			dataSource = IntPtr.Zero;
			return this.InteropServices.QueryInterface(this.DataSource, ref iid, out dataSource);
		}

		// Token: 0x0600C38F RID: 50063 RVA: 0x00272EA0 File Offset: 0x002710A0
		unsafe int IOpenRowset.OpenRowset(IntPtr pUnkOuter, DBID* pTableID, DBID* pIndexID, ref Guid iid, uint cPropertySets, DBPROPSET* rgPropertySets, out IntPtr ppRowset)
		{
			Guid rowsetDialect = this.RowsetDialect;
			ppRowset = IntPtr.Zero;
			if (pTableID == null || pTableID->eKind != DBKIND.NAME || pIndexID != null || rowsetDialect == Guid.Empty)
			{
				return -2147467263;
			}
			Command command = this.CreateCommand();
			ICommandText commandText = command;
			int num = commandText.SetCommandText(ref rowsetDialect, pTableID->pwszName);
			if (num >= 0)
			{
				num = ((ICommandProperties)command).SetProperties(cPropertySets, rgPropertySets);
			}
			if (num >= 0)
			{
				DBROWCOUNT dbrowcount = default(DBROWCOUNT);
				num = commandText.Execute(pUnkOuter, ref iid, null, &dbrowcount, out ppRowset);
			}
			return num;
		}

		// Token: 0x0600C390 RID: 50064 RVA: 0x00272F27 File Offset: 0x00271127
		unsafe int ISessionProperties.GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets)
		{
			return this.Properties.GetProperties(countPropertyIDSets, nativePropertyIDSets, out countPropertySets, out nativePropertySets);
		}

		// Token: 0x0600C391 RID: 50065 RVA: 0x00272F39 File Offset: 0x00271139
		unsafe int ISessionProperties.SetProperties(uint cPropertySets, DBPROPSET* rgPropertySets)
		{
			return this.Properties.SetProperties(cPropertySets, rgPropertySets);
		}

		// Token: 0x0600C392 RID: 50066 RVA: 0x00272F48 File Offset: 0x00271148
		unsafe int IDBSchemaRowset.GetRowset(IntPtr punkOuter, ref Guid guidSchema, uint cRestrictions, VARIANT* rgRestrictions, ref Guid iid, uint cPropertySets, DBPROPSET* rgPropertySets, out IntPtr rowset)
		{
			return ((IDBSchemaRowset)this.SchemaRowset).GetRowset(punkOuter, ref guidSchema, cRestrictions, rgRestrictions, ref iid, cPropertySets, rgPropertySets, out rowset);
		}

		// Token: 0x0600C393 RID: 50067 RVA: 0x00272F6D File Offset: 0x0027116D
		unsafe void IDBSchemaRowset.GetSchemas(out uint cSchemas, out Guid* rgSchemas, out uint* rgRestrictionSupport)
		{
			((IDBSchemaRowset)this.SchemaRowset).GetSchemas(out cSchemas, out rgSchemas, out rgRestrictionSupport);
		}

		// Token: 0x0600C394 RID: 50068 RVA: 0x00272F80 File Offset: 0x00271180
		int IDBCreateCommand.CreateCommand(IntPtr punkOuter, ref Guid iid, out IntPtr ppv)
		{
			Command command = this.CreateCommand();
			return this.InteropServices.AggregateCommand(punkOuter, command, ref iid, out ppv);
		}
	}
}
