using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Internal;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200022B RID: 555
	public class DbModel : IEdmModelAdapter
	{
		// Token: 0x06001D31 RID: 7473 RVA: 0x000531AB File Offset: 0x000513AB
		internal DbModel(DbDatabaseMapping databaseMapping, DbModelBuilder modelBuilder)
		{
			this._databaseMapping = databaseMapping;
			this._cachedModelBuilder = modelBuilder;
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x000531C1 File Offset: 0x000513C1
		internal DbModel(DbProviderInfo providerInfo, DbProviderManifest providerManifest)
		{
			this._databaseMapping = new DbDatabaseMapping().Initialize(EdmModel.CreateConceptualModel(3.0), EdmModel.CreateStoreModel(providerInfo, providerManifest, 3.0));
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x000531F7 File Offset: 0x000513F7
		internal DbModel(EdmModel conceptualModel, EdmModel storeModel)
		{
			this._databaseMapping = new DbDatabaseMapping
			{
				Model = conceptualModel,
				Database = storeModel
			};
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x00053218 File Offset: 0x00051418
		public DbProviderInfo ProviderInfo
		{
			get
			{
				return this.StoreModel.ProviderInfo;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x00053225 File Offset: 0x00051425
		public DbProviderManifest ProviderManifest
		{
			get
			{
				return this.StoreModel.ProviderManifest;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x00053232 File Offset: 0x00051432
		public EdmModel ConceptualModel
		{
			get
			{
				return this._databaseMapping.Model;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0005323F File Offset: 0x0005143F
		public EdmModel StoreModel
		{
			get
			{
				return this._databaseMapping.Database;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001D38 RID: 7480 RVA: 0x0005324C File Offset: 0x0005144C
		public EntityContainerMapping ConceptualToStoreMapping
		{
			get
			{
				return this._databaseMapping.EntityContainerMappings.SingleOrDefault<EntityContainerMapping>();
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x0005325E File Offset: 0x0005145E
		internal DbModelBuilder CachedModelBuilder
		{
			get
			{
				return this._cachedModelBuilder;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x00053266 File Offset: 0x00051466
		internal DbDatabaseMapping DatabaseMapping
		{
			get
			{
				return this._databaseMapping;
			}
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x0005326E File Offset: 0x0005146E
		public DbCompiledModel Compile()
		{
			return new DbCompiledModel(CodeFirstCachedMetadataWorkspace.Create(this.DatabaseMapping), this.CachedModelBuilder);
		}

		// Token: 0x04000B1C RID: 2844
		private readonly DbDatabaseMapping _databaseMapping;

		// Token: 0x04000B1D RID: 2845
		private readonly DbModelBuilder _cachedModelBuilder;
	}
}
