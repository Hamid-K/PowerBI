using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E1 RID: 1249
	internal class ModelPerspective : Perspective
	{
		// Token: 0x06003E37 RID: 15927 RVA: 0x000CEAED File Offset: 0x000CCCED
		internal ModelPerspective(MetadataWorkspace metadataWorkspace)
			: base(metadataWorkspace, DataSpace.CSpace)
		{
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x000CEAF8 File Offset: 0x000CCCF8
		internal override bool TryGetTypeByName(string fullName, bool ignoreCase, out TypeUsage typeUsage)
		{
			Check.NotEmpty(fullName, "fullName");
			typeUsage = null;
			EdmType edmType = null;
			if (base.MetadataWorkspace.TryGetItem<EdmType>(fullName, ignoreCase, base.TargetDataspace, out edmType))
			{
				if (Helper.IsPrimitiveType(edmType))
				{
					typeUsage = MetadataWorkspace.GetCanonicalModelTypeUsage(((PrimitiveType)edmType).PrimitiveTypeKind);
				}
				else
				{
					typeUsage = TypeUsage.Create(edmType);
				}
			}
			return typeUsage != null;
		}
	}
}
