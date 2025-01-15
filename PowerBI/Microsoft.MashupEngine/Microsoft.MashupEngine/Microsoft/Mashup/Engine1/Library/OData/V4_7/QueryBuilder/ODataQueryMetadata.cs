using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007ED RID: 2029
	internal sealed class ODataQueryMetadata
	{
		// Token: 0x06003AC5 RID: 15045 RVA: 0x000BEEC8 File Offset: 0x000BD0C8
		public ODataQueryMetadata(ODataEnvironment environment, Microsoft.OData.Edm.IEdmNavigationSource navigationSource, Microsoft.OData.Edm.IEdmEntityType entityType, ODataColumns columns, Keys keyColumns)
		{
			this.environment = environment;
			this.navigationSource = navigationSource;
			this.entityType = entityType;
			this.columns = columns;
			this.keyColumns = keyColumns;
			this.queryDomain = new ODataQueryDomain(environment.MetadataUri.AbsoluteUri);
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x06003AC6 RID: 15046 RVA: 0x000BEF16 File Offset: 0x000BD116
		public ODataColumns Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06003AC7 RID: 15047 RVA: 0x000BEF1E File Offset: 0x000BD11E
		public Keys ColumnNames
		{
			get
			{
				return this.columns.Names;
			}
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x06003AC8 RID: 15048 RVA: 0x000BEF2B File Offset: 0x000BD12B
		public Keys KeyColumns
		{
			get
			{
				return this.keyColumns;
			}
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x06003AC9 RID: 15049 RVA: 0x000BEF33 File Offset: 0x000BD133
		public Microsoft.OData.Edm.IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x06003ACA RID: 15050 RVA: 0x000BEF3B File Offset: 0x000BD13B
		public Microsoft.OData.Edm.IEdmEntityType EntityType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x06003ACB RID: 15051 RVA: 0x000BEF43 File Offset: 0x000BD143
		public ODataEnvironment Environment
		{
			get
			{
				return this.environment;
			}
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x06003ACC RID: 15052 RVA: 0x000BEF4B File Offset: 0x000BD14B
		public ODataQueryDomain QueryDomain
		{
			get
			{
				return this.queryDomain;
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06003ACD RID: 15053 RVA: 0x000BEF53 File Offset: 0x000BD153
		public bool IsSingleton
		{
			get
			{
				return this.navigationSource is Microsoft.OData.Edm.IEdmSingleton;
			}
		}

		// Token: 0x04001E7E RID: 7806
		private readonly ODataEnvironment environment;

		// Token: 0x04001E7F RID: 7807
		private readonly Microsoft.OData.Edm.IEdmNavigationSource navigationSource;

		// Token: 0x04001E80 RID: 7808
		private readonly Microsoft.OData.Edm.IEdmEntityType entityType;

		// Token: 0x04001E81 RID: 7809
		private readonly ODataColumns columns;

		// Token: 0x04001E82 RID: 7810
		private readonly Keys keyColumns;

		// Token: 0x04001E83 RID: 7811
		private readonly ODataQueryDomain queryDomain;
	}
}
