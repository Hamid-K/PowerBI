using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000EE RID: 238
	internal class CodeFirstCachedMetadataWorkspace : ICachedMetadataWorkspace
	{
		// Token: 0x06001203 RID: 4611 RVA: 0x0002EB92 File Offset: 0x0002CD92
		private CodeFirstCachedMetadataWorkspace(MetadataWorkspace metadataWorkspace, IEnumerable<Assembly> assemblies, DbProviderInfo providerInfo, string defaultContainerName)
		{
			this._metadataWorkspace = metadataWorkspace;
			this._assemblies = assemblies;
			this._providerInfo = providerInfo;
			this._defaultContainerName = defaultContainerName;
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0002EBB8 File Offset: 0x0002CDB8
		public MetadataWorkspace GetMetadataWorkspace(DbConnection connection)
		{
			string providerInvariantName = connection.GetProviderInvariantName();
			if (!string.Equals(this._providerInfo.ProviderInvariantName, providerInvariantName, StringComparison.Ordinal))
			{
				throw Error.CodeFirstCachedMetadataWorkspace_SameModelDifferentProvidersNotSupported();
			}
			return this._metadataWorkspace;
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x0002EBEC File Offset: 0x0002CDEC
		public string DefaultContainerName
		{
			get
			{
				return this._defaultContainerName;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x0002EBF4 File Offset: 0x0002CDF4
		public IEnumerable<Assembly> Assemblies
		{
			get
			{
				return this._assemblies;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x0002EBFC File Offset: 0x0002CDFC
		public DbProviderInfo ProviderInfo
		{
			get
			{
				return this._providerInfo;
			}
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0002EC04 File Offset: 0x0002CE04
		public static CodeFirstCachedMetadataWorkspace Create(DbDatabaseMapping databaseMapping)
		{
			EdmModel model = databaseMapping.Model;
			return new CodeFirstCachedMetadataWorkspace(databaseMapping.ToMetadataWorkspace(), (from t in model.GetClrTypes()
				select t.Assembly()).Distinct<Assembly>().ToArray<Assembly>(), databaseMapping.ProviderInfo, model.Container.Name);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0002EC68 File Offset: 0x0002CE68
		public static CodeFirstCachedMetadataWorkspace Create(StorageMappingItemCollection mappingItemCollection, DbProviderInfo providerInfo)
		{
			EdmItemCollection edmItemCollection = mappingItemCollection.EdmItemCollection;
			IEnumerable<Type> enumerable = from et in edmItemCollection.GetItems<EntityType>()
				select et.GetClrType();
			IEnumerable<Type> enumerable2 = from ct in edmItemCollection.GetItems<ComplexType>()
				select ct.GetClrType();
			return new CodeFirstCachedMetadataWorkspace(mappingItemCollection.Workspace, (from t in enumerable.Union(enumerable2)
				select t.Assembly()).Distinct<Assembly>().ToArray<Assembly>(), providerInfo, edmItemCollection.GetItems<EntityContainer>().Single<EntityContainer>().Name);
		}

		// Token: 0x040008FA RID: 2298
		private readonly MetadataWorkspace _metadataWorkspace;

		// Token: 0x040008FB RID: 2299
		private readonly IEnumerable<Assembly> _assemblies;

		// Token: 0x040008FC RID: 2300
		private readonly DbProviderInfo _providerInfo;

		// Token: 0x040008FD RID: 2301
		private readonly string _defaultContainerName;
	}
}
