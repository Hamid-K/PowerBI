using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200049E RID: 1182
	[Obsolete("ConceptualModel and StoreModel are now available as properties directly on DbModel.")]
	public static class DbModelExtensions
	{
		// Token: 0x06003A1A RID: 14874 RVA: 0x000C0236 File Offset: 0x000BE436
		[Obsolete("ConceptualModel is now available as a property directly on DbModel.")]
		public static EdmModel GetConceptualModel(this IEdmModelAdapter model)
		{
			Check.NotNull<IEdmModelAdapter>(model, "model");
			return model.ConceptualModel;
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000C024A File Offset: 0x000BE44A
		[Obsolete("StoreModel is now available as a property directly on DbModel.")]
		public static EdmModel GetStoreModel(this IEdmModelAdapter model)
		{
			Check.NotNull<IEdmModelAdapter>(model, "model");
			return model.StoreModel;
		}
	}
}
