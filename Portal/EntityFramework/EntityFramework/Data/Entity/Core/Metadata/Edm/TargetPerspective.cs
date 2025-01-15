using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004FE RID: 1278
	internal class TargetPerspective : Perspective
	{
		// Token: 0x06003F1D RID: 16157 RVA: 0x000D20A4 File Offset: 0x000D02A4
		internal TargetPerspective(MetadataWorkspace metadataWorkspace)
			: base(metadataWorkspace, DataSpace.SSpace)
		{
			this._modelPerspective = new ModelPerspective(metadataWorkspace);
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x000D20BC File Offset: 0x000D02BC
		internal override bool TryGetTypeByName(string fullName, bool ignoreCase, out TypeUsage usage)
		{
			Check.NotEmpty(fullName, "fullName");
			EdmType edmType = null;
			if (base.MetadataWorkspace.TryGetItem<EdmType>(fullName, ignoreCase, base.TargetDataspace, out edmType))
			{
				usage = TypeUsage.Create(edmType);
				usage = Helper.GetModelTypeUsage(usage);
				return true;
			}
			return this._modelPerspective.TryGetTypeByName(fullName, ignoreCase, out usage);
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x000D210F File Offset: 0x000D030F
		internal override bool TryGetEntityContainer(string name, bool ignoreCase, out EntityContainer entityContainer)
		{
			return base.TryGetEntityContainer(name, ignoreCase, out entityContainer) || this._modelPerspective.TryGetEntityContainer(name, ignoreCase, out entityContainer);
		}

		// Token: 0x0400158A RID: 5514
		internal const DataSpace TargetPerspectiveDataSpace = DataSpace.SSpace;

		// Token: 0x0400158B RID: 5515
		private readonly ModelPerspective _modelPerspective;
	}
}
