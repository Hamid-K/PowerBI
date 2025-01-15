using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000256 RID: 598
	public class ModelContainerConvention : IConceptualModelConvention<EntityContainer>, IConvention
	{
		// Token: 0x06001EC7 RID: 7879 RVA: 0x000559EA File Offset: 0x00053BEA
		internal ModelContainerConvention(string containerName)
		{
			this._containerName = containerName;
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x000559F9 File Offset: 0x00053BF9
		public virtual void Apply(EntityContainer item, DbModel model)
		{
			Check.NotNull<DbModel>(model, "model");
			item.Name = this._containerName;
		}

		// Token: 0x04000B35 RID: 2869
		private readonly string _containerName;
	}
}
