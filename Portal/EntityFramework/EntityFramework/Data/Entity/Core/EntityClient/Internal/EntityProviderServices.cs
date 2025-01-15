using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.EntityClient.Internal
{
	// Token: 0x020005E6 RID: 1510
	internal sealed class EntityProviderServices : DbProviderServices
	{
		// Token: 0x060049C7 RID: 18887 RVA: 0x00105F58 File Offset: 0x00104158
		protected override DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
		{
			Check.NotNull<DbProviderManifest>(providerManifest, "providerManifest");
			Check.NotNull<DbCommandTree>(commandTree, "commandTree");
			return this.CreateDbCommandDefinition(providerManifest, commandTree, new DbInterceptionContext());
		}

		// Token: 0x060049C8 RID: 18888 RVA: 0x00105F7F File Offset: 0x0010417F
		internal static EntityCommandDefinition CreateCommandDefinition(DbProviderFactory storeProviderFactory, DbCommandTree commandTree, DbInterceptionContext interceptionContext, IDbDependencyResolver resolver = null)
		{
			return new EntityCommandDefinition(storeProviderFactory, commandTree, interceptionContext, resolver, null, null);
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x00105F8C File Offset: 0x0010418C
		internal override DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree, DbInterceptionContext interceptionContext)
		{
			return EntityProviderServices.CreateCommandDefinition(((StoreItemCollection)commandTree.MetadataWorkspace.GetItemCollection(DataSpace.SSpace)).ProviderFactory, commandTree, interceptionContext, null);
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x00105FAC File Offset: 0x001041AC
		internal override void ValidateDataSpace(DbCommandTree commandTree)
		{
			if (commandTree.DataSpace != DataSpace.CSpace)
			{
				throw new ProviderIncompatibleException(Strings.EntityClient_RequiresNonStoreCommandTree);
			}
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x00105FC2 File Offset: 0x001041C2
		public override DbCommandDefinition CreateCommandDefinition(DbCommand prototype)
		{
			Check.NotNull<DbCommand>(prototype, "prototype");
			return ((EntityCommand)prototype).GetCommandDefinition();
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x00105FDC File Offset: 0x001041DC
		protected override string GetDbProviderManifestToken(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			if (connection.GetType() != typeof(EntityConnection))
			{
				throw new ArgumentException(Strings.Mapping_Provider_WrongConnectionType(typeof(EntityConnection)));
			}
			return MetadataItem.EdmProviderManifest.Token;
		}

		// Token: 0x060049CD RID: 18893 RVA: 0x0010602B File Offset: 0x0010422B
		protected override DbProviderManifest GetDbProviderManifest(string manifestToken)
		{
			Check.NotNull<string>(manifestToken, "manifestToken");
			return MetadataItem.EdmProviderManifest;
		}

		// Token: 0x04001A0B RID: 6667
		internal static readonly EntityProviderServices Instance = new EntityProviderServices();
	}
}
