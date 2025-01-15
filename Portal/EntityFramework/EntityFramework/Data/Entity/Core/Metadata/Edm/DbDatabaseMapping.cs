using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200049D RID: 1181
	internal class DbDatabaseMapping
	{
		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06003A11 RID: 14865 RVA: 0x000C01C5 File Offset: 0x000BE3C5
		// (set) Token: 0x06003A12 RID: 14866 RVA: 0x000C01CD File Offset: 0x000BE3CD
		public EdmModel Model { get; set; }

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06003A13 RID: 14867 RVA: 0x000C01D6 File Offset: 0x000BE3D6
		// (set) Token: 0x06003A14 RID: 14868 RVA: 0x000C01DE File Offset: 0x000BE3DE
		public EdmModel Database { get; set; }

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06003A15 RID: 14869 RVA: 0x000C01E7 File Offset: 0x000BE3E7
		public DbProviderInfo ProviderInfo
		{
			get
			{
				return this.Database.ProviderInfo;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06003A16 RID: 14870 RVA: 0x000C01F4 File Offset: 0x000BE3F4
		public DbProviderManifest ProviderManifest
		{
			get
			{
				return this.Database.ProviderManifest;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06003A17 RID: 14871 RVA: 0x000C0201 File Offset: 0x000BE401
		internal IList<EntityContainerMapping> EntityContainerMappings
		{
			get
			{
				return this._entityContainerMappings;
			}
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000C0209 File Offset: 0x000BE409
		internal void AddEntityContainerMapping(EntityContainerMapping entityContainerMapping)
		{
			Check.NotNull<EntityContainerMapping>(entityContainerMapping, "entityContainerMapping");
			this._entityContainerMappings.Add(entityContainerMapping);
		}

		// Token: 0x04001366 RID: 4966
		private readonly List<EntityContainerMapping> _entityContainerMappings = new List<EntityContainerMapping>();
	}
}
