using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Navigation;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.OleDb
{
	// Token: 0x02000572 RID: 1394
	internal sealed class OleDbDataSource : IOleDbDataSource
	{
		// Token: 0x06002C56 RID: 11350 RVA: 0x0008713C File Offset: 0x0008533C
		public OleDbDataSource(IEngineHost host, IResource resource, Value connectionAttributes, Value options, OptionRecordDefinition validOptions)
		{
			this.host = host;
			this.resource = resource;
			this.connectionAttributes = connectionAttributes;
			this.options = options;
			this.validOptions = validOptions;
			this.initialEnvironment = new OleDbEnvironment(this.host, this.resource, this.connectionAttributes, this.options, this.validOptions);
			this.catalogEnvironmentMap = new Dictionary<string, OleDbEnvironment>();
		}

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06002C57 RID: 11351 RVA: 0x000871A8 File Offset: 0x000853A8
		public IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x06002C58 RID: 11352 RVA: 0x000871B0 File Offset: 0x000853B0
		public IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x06002C59 RID: 11353 RVA: 0x000871B8 File Offset: 0x000853B8
		public string Provider
		{
			get
			{
				return ((IOleDbDataSource)this.initialEnvironment).Provider;
			}
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x000871C5 File Offset: 0x000853C5
		public OleDbDataSourceInfo Info
		{
			get
			{
				return this.initialEnvironment.OleDbDataSourceInfo;
			}
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000871D4 File Offset: 0x000853D4
		public TableValue CreateTable()
		{
			OleDbMultiLevelNavigationProvider oleDbMultiLevelNavigationProvider = new OleDbMultiLevelNavigationProvider(this);
			string text;
			if (this.initialEnvironment.UserOptions.TryGetString("Query", out text))
			{
				return new NativeQueryTableValue(this.Host, this.initialEnvironment, new FlatNavigationTableValue(oleDbMultiLevelNavigationProvider), text);
			}
			TableValue tableValue = (this.initialEnvironment.UserOptions.GetBool("HierarchicalNavigation", true) ? HierarchicalNavigationQuery.New(this.host, oleDbMultiLevelNavigationProvider) : new FlatNavigationTableValue(oleDbMultiLevelNavigationProvider));
			return new OleDbDataSource.TestConnectionTableWrapper(this.Info, tableValue);
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x00087253 File Offset: 0x00085453
		public IPageReader OpenTable(string tableIdentifier)
		{
			return this.initialEnvironment.OpenTable(tableIdentifier);
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x00087261 File Offset: 0x00085461
		public bool TryGetProperty(Guid propertyGroup, DBPROPID propertyId, out object value)
		{
			return this.initialEnvironment.TryGetProperty(propertyGroup, propertyId, out value);
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x00087271 File Offset: 0x00085471
		public DataTable GetSchemas()
		{
			return ((IOleDbDataSource)this.initialEnvironment).GetSchemas();
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x0008727E File Offset: 0x0008547E
		public DataTable GetSchemaTable(Guid schema, params object[] restrictions)
		{
			return this.initialEnvironment.GetSchemaTable(schema, restrictions);
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x0008728D File Offset: 0x0008548D
		public DataTable GetSchemaTableSafe(Guid schema, params object[] restrictions)
		{
			return this.initialEnvironment.GetSchemaTableSafe(new Func<Guid, object[], DataTable>(this.GetSchemaTable), schema, restrictions);
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000872A9 File Offset: 0x000854A9
		public DataTable GetLiteralInfo()
		{
			return this.initialEnvironment.GetLiteralInfo();
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000872B8 File Offset: 0x000854B8
		public OleDbEnvironment GetCatalogEnvironemnt(string catalogName)
		{
			if (catalogName == null)
			{
				return this.initialEnvironment;
			}
			OleDbEnvironment oleDbEnvironment;
			if (!this.catalogEnvironmentMap.TryGetValue(catalogName, out oleDbEnvironment))
			{
				oleDbEnvironment = OleDbEnvironment.CreateForCatalog(this.initialEnvironment.Host, this.initialEnvironment.Resource, this.connectionAttributes, catalogName, this.options, this.validOptions, this.Info);
				this.catalogEnvironmentMap.Add(catalogName, oleDbEnvironment);
			}
			return oleDbEnvironment;
		}

		// Token: 0x04001348 RID: 4936
		private readonly IEngineHost host;

		// Token: 0x04001349 RID: 4937
		private readonly IResource resource;

		// Token: 0x0400134A RID: 4938
		private readonly Value connectionAttributes;

		// Token: 0x0400134B RID: 4939
		private readonly Value options;

		// Token: 0x0400134C RID: 4940
		private readonly OptionRecordDefinition validOptions;

		// Token: 0x0400134D RID: 4941
		private readonly OleDbEnvironment initialEnvironment;

		// Token: 0x0400134E RID: 4942
		private readonly Dictionary<string, OleDbEnvironment> catalogEnvironmentMap;

		// Token: 0x02000573 RID: 1395
		private class TestConnectionTableWrapper : WrappingTableValue
		{
			// Token: 0x06002C63 RID: 11363 RVA: 0x00087322 File Offset: 0x00085522
			public TestConnectionTableWrapper(OleDbDataSourceInfo info, TableValue table)
				: base(table)
			{
				this.info = info;
			}

			// Token: 0x17001067 RID: 4199
			// (get) Token: 0x06002C64 RID: 11364 RVA: 0x00087332 File Offset: 0x00085532
			public override TypeValue Type
			{
				get
				{
					this.Validate();
					return base.Type;
				}
			}

			// Token: 0x06002C65 RID: 11365 RVA: 0x00087340 File Offset: 0x00085540
			public override IPageReader GetReader()
			{
				this.Validate();
				return base.GetReader();
			}

			// Token: 0x06002C66 RID: 11366 RVA: 0x0008734E File Offset: 0x0008554E
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				this.Validate();
				return base.GetEnumerator();
			}

			// Token: 0x06002C67 RID: 11367 RVA: 0x0008735C File Offset: 0x0008555C
			protected override TableValue New(TableValue table)
			{
				return new OleDbDataSource.TestConnectionTableWrapper(this.info, table);
			}

			// Token: 0x06002C68 RID: 11368 RVA: 0x0008736A File Offset: 0x0008556A
			private void Validate()
			{
				if (this.info.SupportsOLAP)
				{
					throw ValueException.NotImplemented<Message0>(Strings.OleDbOlapNotSupported);
				}
			}

			// Token: 0x0400134F RID: 4943
			private readonly OleDbDataSourceInfo info;
		}
	}
}
