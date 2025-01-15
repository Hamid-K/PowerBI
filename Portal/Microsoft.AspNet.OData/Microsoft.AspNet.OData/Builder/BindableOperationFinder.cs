using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000126 RID: 294
	internal class BindableOperationFinder
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x00029710 File Offset: 0x00027910
		public BindableOperationFinder(IEdmModel model)
		{
			foreach (IGrouping<IEdmType, IEdmOperation> grouping in from op in model.SchemaElements.OfType<IEdmOperation>()
				where op.IsBound && (op.Parameters.First<IEdmOperationParameter>().Type.TypeKind() == EdmTypeKind.Entity || op.Parameters.First<IEdmOperationParameter>().Type.TypeKind() == EdmTypeKind.Collection)
				group op by op.Parameters.First<IEdmOperationParameter>().Type.Definition)
			{
				IEdmEntityType edmEntityType = grouping.Key as IEdmEntityType;
				if (edmEntityType != null)
				{
					this._map[edmEntityType] = grouping.ToList<IEdmOperation>();
				}
				IEdmCollectionType edmCollectionType = grouping.Key as IEdmCollectionType;
				if (edmCollectionType != null)
				{
					IEdmEntityType edmEntityType2 = edmCollectionType.ElementType.Definition as IEdmEntityType;
					if (edmEntityType2 != null)
					{
						List<IEdmOperation> list;
						if (this._collectionMap.TryGetValue(edmEntityType2, out list))
						{
							list.AddRange(grouping);
						}
						else
						{
							this._collectionMap[edmEntityType2] = grouping.ToList<IEdmOperation>();
						}
					}
				}
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00029838 File Offset: 0x00027A38
		public virtual IEnumerable<IEdmOperation> FindOperations(IEdmEntityType entityType)
		{
			return BindableOperationFinder.GetTypeHierarchy(entityType).SelectMany(new Func<IEdmEntityType, IEnumerable<IEdmOperation>>(this.FindDeclaredOperations));
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00029851 File Offset: 0x00027A51
		public virtual IEnumerable<IEdmOperation> FindOperationsBoundToCollection(IEdmEntityType entityType)
		{
			return BindableOperationFinder.GetTypeHierarchy(entityType).SelectMany(new Func<IEdmEntityType, IEnumerable<IEdmOperation>>(this.FindDeclaredOperationsBoundToCollection));
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002986A File Offset: 0x00027A6A
		private static IEnumerable<IEdmEntityType> GetTypeHierarchy(IEdmEntityType entityType)
		{
			for (IEdmEntityType current = entityType; current != null; current = current.BaseEntityType())
			{
				yield return current;
			}
			yield break;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002987C File Offset: 0x00027A7C
		private IEnumerable<IEdmOperation> FindDeclaredOperations(IEdmEntityType entityType)
		{
			List<IEdmOperation> list;
			if (this._map.TryGetValue(entityType, out list))
			{
				return list;
			}
			return Enumerable.Empty<IEdmFunction>();
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x000298A0 File Offset: 0x00027AA0
		private IEnumerable<IEdmOperation> FindDeclaredOperationsBoundToCollection(IEdmEntityType entityType)
		{
			List<IEdmOperation> list;
			if (this._collectionMap.TryGetValue(entityType, out list))
			{
				return list;
			}
			return Enumerable.Empty<IEdmFunction>();
		}

		// Token: 0x04000338 RID: 824
		private Dictionary<IEdmEntityType, List<IEdmOperation>> _map = new Dictionary<IEdmEntityType, List<IEdmOperation>>();

		// Token: 0x04000339 RID: 825
		private Dictionary<IEdmEntityType, List<IEdmOperation>> _collectionMap = new Dictionary<IEdmEntityType, List<IEdmOperation>>();
	}
}
