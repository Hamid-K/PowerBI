using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000FD RID: 253
	internal class EdmMetadataContext : DbContext
	{
		// Token: 0x06001276 RID: 4726 RVA: 0x00030617 File Offset: 0x0002E817
		static EdmMetadataContext()
		{
			Database.SetInitializer<EdmMetadataContext>(null);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0003061F File Offset: 0x0002E81F
		public EdmMetadataContext(DbConnection existingConnection)
			: base(existingConnection, false)
		{
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00030629 File Offset: 0x0002E829
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x00030631 File Offset: 0x0002E831
		public virtual IDbSet<EdmMetadata> Metadata { get; set; }

		// Token: 0x0600127A RID: 4730 RVA: 0x0003063A File Offset: 0x0002E83A
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			EdmMetadataContext.ConfigureEdmMetadata(modelBuilder.ModelConfiguration);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00030647 File Offset: 0x0002E847
		public static void ConfigureEdmMetadata(ModelConfiguration modelConfiguration)
		{
			modelConfiguration.Entity(typeof(EdmMetadata)).ToTable("EdmMetadata");
		}

		// Token: 0x0400091D RID: 2333
		public const string TableName = "EdmMetadata";
	}
}
