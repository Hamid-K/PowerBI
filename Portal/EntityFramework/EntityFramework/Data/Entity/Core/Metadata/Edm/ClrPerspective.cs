using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200048E RID: 1166
	internal sealed class ClrPerspective : Perspective
	{
		// Token: 0x060039BE RID: 14782 RVA: 0x000BE3B6 File Offset: 0x000BC5B6
		internal ClrPerspective(MetadataWorkspace metadataWorkspace)
			: base(metadataWorkspace, DataSpace.CSpace)
		{
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x000BE3C0 File Offset: 0x000BC5C0
		internal bool TryGetType(Type clrType, out TypeUsage outTypeUsage)
		{
			return this.TryGetTypeByName(clrType.FullNameWithNesting(), false, out outTypeUsage);
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x000BE3D0 File Offset: 0x000BC5D0
		internal override bool TryGetMember(StructuralType type, string memberName, bool ignoreCase, out EdmMember outMember)
		{
			outMember = null;
			MappingBase mappingBase = null;
			if (base.MetadataWorkspace.TryGetMap(type, DataSpace.OCSpace, out mappingBase))
			{
				ObjectTypeMapping objectTypeMapping = mappingBase as ObjectTypeMapping;
				if (objectTypeMapping != null)
				{
					ObjectMemberMapping memberMapForClrMember = objectTypeMapping.GetMemberMapForClrMember(memberName, ignoreCase);
					if (memberMapForClrMember != null)
					{
						outMember = memberMapForClrMember.EdmMember;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x000BE418 File Offset: 0x000BC618
		internal override bool TryGetTypeByName(string fullName, bool ignoreCase, out TypeUsage typeUsage)
		{
			typeUsage = null;
			MappingBase mappingBase = null;
			if (base.MetadataWorkspace.TryGetMap(fullName, DataSpace.OSpace, ignoreCase, DataSpace.OCSpace, out mappingBase))
			{
				if (mappingBase.EdmItem.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType)
				{
					PrimitiveType mappedPrimitiveType = base.MetadataWorkspace.GetMappedPrimitiveType(((PrimitiveType)mappingBase.EdmItem).PrimitiveTypeKind, DataSpace.CSpace);
					if (mappedPrimitiveType != null)
					{
						typeUsage = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(mappedPrimitiveType.PrimitiveTypeKind);
					}
				}
				else
				{
					typeUsage = ClrPerspective.GetMappedTypeUsage(mappingBase);
				}
			}
			return typeUsage != null;
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x000BE48D File Offset: 0x000BC68D
		internal override EntityContainer GetDefaultContainer()
		{
			return this._defaultContainer;
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x000BE498 File Offset: 0x000BC698
		internal void SetDefaultContainer(string defaultContainerName)
		{
			EntityContainer entityContainer = null;
			if (!string.IsNullOrEmpty(defaultContainerName) && !base.MetadataWorkspace.TryGetEntityContainer(defaultContainerName, DataSpace.CSpace, out entityContainer))
			{
				throw new ArgumentException(Strings.ObjectContext_InvalidDefaultContainerName(defaultContainerName), "defaultContainerName");
			}
			this._defaultContainer = entityContainer;
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x000BE4D8 File Offset: 0x000BC6D8
		private static TypeUsage GetMappedTypeUsage(MappingBase map)
		{
			TypeUsage typeUsage = null;
			if (map != null)
			{
				MetadataItem edmItem = map.EdmItem;
				EdmType edmType = edmItem as EdmType;
				if (edmItem != null && edmType != null)
				{
					typeUsage = TypeUsage.Create(edmType);
				}
			}
			return typeUsage;
		}

		// Token: 0x04001348 RID: 4936
		private EntityContainer _defaultContainer;
	}
}
