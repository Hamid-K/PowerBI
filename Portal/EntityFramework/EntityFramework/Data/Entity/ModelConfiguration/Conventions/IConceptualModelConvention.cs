using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001B7 RID: 439
	public interface IConceptualModelConvention<T> : IConvention where T : MetadataItem
	{
		// Token: 0x060017A2 RID: 6050
		void Apply(T item, DbModel model);
	}
}
