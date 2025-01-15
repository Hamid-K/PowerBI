using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000885 RID: 2181
	internal sealed class ODataQueryMetadata
	{
		// Token: 0x06003EBE RID: 16062 RVA: 0x000CD3F0 File Offset: 0x000CB5F0
		public ODataQueryMetadata(ODataEnvironment environment, Microsoft.OData.Edm.IEdmNavigationSource navigationSource, Microsoft.OData.Edm.IEdmEntityType entityType, ODataColumns columns, Keys keyColumns)
		{
			this.environment = environment;
			this.navigationSource = navigationSource;
			this.entityType = entityType;
			this.columns = columns;
			this.keyColumns = keyColumns;
			this.queryDomain = new ODataQueryDomain(environment.MetadataUri.AbsoluteUri);
		}

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x06003EBF RID: 16063 RVA: 0x000CD43E File Offset: 0x000CB63E
		public ODataColumns Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x000CD446 File Offset: 0x000CB646
		public Keys ColumnNames
		{
			get
			{
				return this.columns.Names;
			}
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x06003EC1 RID: 16065 RVA: 0x000CD453 File Offset: 0x000CB653
		public Keys KeyColumns
		{
			get
			{
				return this.keyColumns;
			}
		}

		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x000CD45B File Offset: 0x000CB65B
		public Microsoft.OData.Edm.IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x06003EC3 RID: 16067 RVA: 0x000CD463 File Offset: 0x000CB663
		public Microsoft.OData.Edm.IEdmEntityType EntityType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x000CD46B File Offset: 0x000CB66B
		public ODataEnvironment Environment
		{
			get
			{
				return this.environment;
			}
		}

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x06003EC5 RID: 16069 RVA: 0x000CD473 File Offset: 0x000CB673
		public ODataQueryDomain QueryDomain
		{
			get
			{
				return this.queryDomain;
			}
		}

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x06003EC6 RID: 16070 RVA: 0x000CD47B File Offset: 0x000CB67B
		public bool IsSingleton
		{
			get
			{
				return this.navigationSource is Microsoft.OData.Edm.IEdmSingleton;
			}
		}

		// Token: 0x040020E8 RID: 8424
		private readonly ODataEnvironment environment;

		// Token: 0x040020E9 RID: 8425
		private readonly Microsoft.OData.Edm.IEdmNavigationSource navigationSource;

		// Token: 0x040020EA RID: 8426
		private readonly Microsoft.OData.Edm.IEdmEntityType entityType;

		// Token: 0x040020EB RID: 8427
		private readonly ODataColumns columns;

		// Token: 0x040020EC RID: 8428
		private readonly Keys keyColumns;

		// Token: 0x040020ED RID: 8429
		private readonly ODataQueryDomain queryDomain;
	}
}
