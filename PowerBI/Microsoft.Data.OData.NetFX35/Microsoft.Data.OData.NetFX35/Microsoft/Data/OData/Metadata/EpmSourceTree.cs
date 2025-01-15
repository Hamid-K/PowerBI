using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000212 RID: 530
	internal sealed class EpmSourceTree
	{
		// Token: 0x06000F7F RID: 3967 RVA: 0x000390E9 File Offset: 0x000372E9
		internal EpmSourceTree(EpmTargetTree epmTargetTree)
		{
			this.root = new EpmSourcePathSegment();
			this.epmTargetTree = epmTargetTree;
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00039103 File Offset: 0x00037303
		internal EpmSourcePathSegment Root
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00039128 File Offset: 0x00037328
		internal void Add(EntityPropertyMappingInfo epmInfo)
		{
			List<EpmSourcePathSegment> list = new List<EpmSourcePathSegment>();
			EpmSourcePathSegment epmSourcePathSegment = this.Root;
			EpmSourcePathSegment epmSourcePathSegment2 = null;
			IEdmType edmType = epmInfo.ActualPropertyType;
			string[] array = epmInfo.Attribute.SourcePath.Split(new char[] { '/' });
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				string propertyName = array[i];
				if (propertyName.Length == 0)
				{
					throw new ODataException(Strings.EpmSourceTree_InvalidSourcePath(epmInfo.DefiningType.ODataFullName(), epmInfo.Attribute.SourcePath));
				}
				IEdmTypeReference propertyType = EpmSourceTree.GetPropertyType(edmType, propertyName);
				IEdmType edmType2 = ((propertyType == null) ? null : propertyType.Definition);
				if (edmType2 == null && i < num - 1)
				{
					throw new ODataException(Strings.EpmSourceTree_OpenComplexPropertyCannotBeMapped(propertyName, edmType.ODataFullName()));
				}
				edmType = edmType2;
				epmSourcePathSegment2 = Enumerable.SingleOrDefault<EpmSourcePathSegment>(epmSourcePathSegment.SubProperties, (EpmSourcePathSegment e) => e.PropertyName == propertyName);
				if (epmSourcePathSegment2 != null)
				{
					epmSourcePathSegment = epmSourcePathSegment2;
				}
				else
				{
					EpmSourcePathSegment epmSourcePathSegment3 = new EpmSourcePathSegment(propertyName);
					epmSourcePathSegment.SubProperties.Add(epmSourcePathSegment3);
					epmSourcePathSegment = epmSourcePathSegment3;
				}
				list.Add(epmSourcePathSegment);
			}
			if (edmType != null && !edmType.IsODataPrimitiveTypeKind())
			{
				throw new ODataException(Strings.EpmSourceTree_EndsWithNonPrimitiveType(epmSourcePathSegment.PropertyName));
			}
			if (epmSourcePathSegment2 != null)
			{
				if (epmSourcePathSegment2.EpmInfo.DefiningTypesAreEqual(epmInfo))
				{
					throw new ODataException(Strings.EpmSourceTree_DuplicateEpmAttributesWithSameSourceName(epmInfo.DefiningType.ODataFullName(), epmInfo.Attribute.SourcePath));
				}
				this.epmTargetTree.Remove(epmSourcePathSegment2.EpmInfo);
			}
			epmInfo.SetPropertyValuePath(list.ToArray());
			epmSourcePathSegment.EpmInfo = epmInfo;
			this.epmTargetTree.Add(epmInfo);
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x000392D5 File Offset: 0x000374D5
		internal void Validate(IEdmEntityType entityType)
		{
			EpmSourceTree.Validate(this.Root, entityType);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x000392E4 File Offset: 0x000374E4
		private static void Validate(EpmSourcePathSegment pathSegment, IEdmType type)
		{
			foreach (EpmSourcePathSegment epmSourcePathSegment in pathSegment.SubProperties)
			{
				IEdmTypeReference propertyType = EpmSourceTree.GetPropertyType(type, epmSourcePathSegment.PropertyName);
				IEdmType edmType = ((propertyType == null) ? null : propertyType.Definition);
				EpmSourceTree.Validate(epmSourcePathSegment, edmType);
			}
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x00039354 File Offset: 0x00037554
		private static IEdmTypeReference GetPropertyType(IEdmType type, string propertyName)
		{
			IEdmStructuredType edmStructuredType = type as IEdmStructuredType;
			IEdmProperty edmProperty = ((edmStructuredType == null) ? null : edmStructuredType.FindProperty(propertyName));
			if (edmProperty != null)
			{
				IEdmTypeReference type2 = edmProperty.Type;
				if (type2.IsNonEntityCollectionType())
				{
					throw new ODataException(Strings.EpmSourceTree_CollectionPropertyCannotBeMapped(propertyName, type.ODataFullName()));
				}
				if (type2.IsStream())
				{
					throw new ODataException(Strings.EpmSourceTree_StreamPropertyCannotBeMapped(propertyName, type.ODataFullName()));
				}
				if (type2.IsSpatial())
				{
					throw new ODataException(Strings.EpmSourceTree_SpatialTypeCannotBeMapped(propertyName, type.ODataFullName()));
				}
				return edmProperty.Type;
			}
			else
			{
				if (type != null && !type.IsOpenType())
				{
					throw new ODataException(Strings.EpmSourceTree_MissingPropertyOnType(propertyName, type.ODataFullName()));
				}
				return null;
			}
		}

		// Token: 0x040005F9 RID: 1529
		private readonly EpmSourcePathSegment root;

		// Token: 0x040005FA RID: 1530
		private readonly EpmTargetTree epmTargetTree;
	}
}
