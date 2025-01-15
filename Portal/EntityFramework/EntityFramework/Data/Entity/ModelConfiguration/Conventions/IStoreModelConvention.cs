using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001B9 RID: 441
	public interface IStoreModelConvention<T> : IConvention where T : MetadataItem
	{
		// Token: 0x060017A3 RID: 6051
		void Apply(T item, DbModel model);
	}
}
