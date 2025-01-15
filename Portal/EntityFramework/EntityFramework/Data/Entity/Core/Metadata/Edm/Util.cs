using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000501 RID: 1281
	internal static class Util
	{
		// Token: 0x06003F82 RID: 16258 RVA: 0x000D3775 File Offset: 0x000D1975
		internal static void ThrowIfReadOnly(MetadataItem item)
		{
			if (item.IsReadOnly)
			{
				throw new InvalidOperationException(Strings.OperationOnReadOnlyItem);
			}
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x000D378A File Offset: 0x000D198A
		[Conditional("DEBUG")]
		internal static void AssertItemHasIdentity(MetadataItem item, string argumentName)
		{
			Check.NotNull<MetadataItem>(item, argumentName);
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x000D3794 File Offset: 0x000D1994
		internal static ObjectTypeMapping GetObjectMapping(EdmType type, MetadataWorkspace workspace)
		{
			ItemCollection itemCollection;
			if (workspace.TryGetItemCollection(DataSpace.CSpace, out itemCollection))
			{
				return (ObjectTypeMapping)workspace.GetMap(type, DataSpace.OCSpace);
			}
			EdmType edmType;
			EdmType edmType2;
			if (type.DataSpace == DataSpace.CSpace)
			{
				if (Helper.IsPrimitiveType(type))
				{
					edmType = workspace.GetMappedPrimitiveType(((PrimitiveType)type).PrimitiveTypeKind, DataSpace.OSpace);
				}
				else
				{
					edmType = workspace.GetItem<EdmType>(type.FullName, DataSpace.OSpace);
				}
				edmType2 = type;
			}
			else
			{
				edmType = type;
				edmType2 = type;
			}
			if (!Helper.IsPrimitiveType(edmType) && !Helper.IsEntityType(edmType) && !Helper.IsComplexType(edmType))
			{
				throw new NotSupportedException(Strings.Materializer_UnsupportedType);
			}
			ObjectTypeMapping objectTypeMapping;
			if (Helper.IsPrimitiveType(edmType))
			{
				objectTypeMapping = new ObjectTypeMapping(edmType, edmType2);
			}
			else
			{
				objectTypeMapping = DefaultObjectMappingItemCollection.LoadObjectMapping(edmType2, edmType, null);
			}
			return objectTypeMapping;
		}
	}
}
