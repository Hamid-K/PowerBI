using System;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000508 RID: 1288
	internal class CodeFirstOSpaceLoader
	{
		// Token: 0x06003F9A RID: 16282 RVA: 0x000D3D00 File Offset: 0x000D1F00
		public CodeFirstOSpaceLoader(CodeFirstOSpaceTypeFactory typeFactory = null)
		{
			this._typeFactory = typeFactory ?? new CodeFirstOSpaceTypeFactory();
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x000D3D18 File Offset: 0x000D1F18
		public void LoadTypes(EdmItemCollection edmItemCollection, ObjectItemCollection objectItemCollection)
		{
			foreach (EdmType edmType in from t in edmItemCollection.OfType<EdmType>()
				where t.BuiltInTypeKind == BuiltInTypeKind.EntityType || t.BuiltInTypeKind == BuiltInTypeKind.EnumType || t.BuiltInTypeKind == BuiltInTypeKind.ComplexType
				select t)
			{
				Type clrType = edmType.GetClrType();
				if (clrType != null)
				{
					EdmType edmType2 = this._typeFactory.TryCreateType(clrType, edmType);
					if (edmType2 != null)
					{
						this._typeFactory.CspaceToOspace.Add(edmType, edmType2);
					}
				}
			}
			this._typeFactory.CreateRelationships(edmItemCollection);
			foreach (Action action in this._typeFactory.ReferenceResolutions)
			{
				action();
			}
			foreach (EdmType edmType3 in this._typeFactory.LoadedTypes.Values)
			{
				edmType3.SetReadOnly();
			}
			objectItemCollection.AddLoadedTypes(this._typeFactory.LoadedTypes);
			objectItemCollection.OSpaceTypesLoaded = true;
		}

		// Token: 0x04001634 RID: 5684
		private readonly CodeFirstOSpaceTypeFactory _typeFactory;
	}
}
