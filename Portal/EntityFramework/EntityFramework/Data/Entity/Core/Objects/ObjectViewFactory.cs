using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000427 RID: 1063
	internal static class ObjectViewFactory
	{
		// Token: 0x060033B7 RID: 13239 RVA: 0x000A6E88 File Offset: 0x000A5088
		internal static IBindingList CreateViewForQuery<TElement>(TypeUsage elementEdmTypeUsage, IEnumerable<TElement> queryResults, ObjectContext objectContext, bool forceReadOnly, EntitySet singleEntitySet)
		{
			TypeUsage ospaceTypeUsage = ObjectViewFactory.GetOSpaceTypeUsage(elementEdmTypeUsage, objectContext);
			Type type;
			if (ospaceTypeUsage == null)
			{
				type = typeof(TElement);
			}
			type = ObjectViewFactory.GetClrType<TElement>(ospaceTypeUsage.EdmType);
			object objectStateManager = objectContext.ObjectStateManager;
			IBindingList bindingList;
			if (type == typeof(TElement))
			{
				bindingList = new ObjectView<TElement>(new ObjectViewQueryResultData<TElement>(queryResults, objectContext, forceReadOnly, singleEntitySet), objectStateManager);
			}
			else if (type == null)
			{
				bindingList = new DataRecordObjectView(new ObjectViewQueryResultData<DbDataRecord>(queryResults, objectContext, true, null), objectStateManager, (RowType)ospaceTypeUsage.EdmType, typeof(TElement));
			}
			else
			{
				if (!typeof(TElement).IsAssignableFrom(type))
				{
					throw EntityUtil.ValueInvalidCast(type, typeof(TElement));
				}
				Type type2 = ObjectViewFactory._genericObjectViewQueryResultDataType.MakeGenericType(new Type[] { type });
				object obj = type2.GetDeclaredConstructor(new Type[]
				{
					typeof(IEnumerable),
					typeof(ObjectContext),
					typeof(bool),
					typeof(EntitySet)
				}).Invoke(new object[] { queryResults, objectContext, forceReadOnly, singleEntitySet });
				bindingList = ObjectViewFactory.CreateObjectView(type, type2, obj, objectStateManager);
			}
			return bindingList;
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x000A6FC8 File Offset: 0x000A51C8
		internal static IBindingList CreateViewForEntityCollection<TElement>(EntityType entityType, EntityCollection<TElement> entityCollection) where TElement : class
		{
			TypeUsage ospaceTypeUsage = ObjectViewFactory.GetOSpaceTypeUsage((entityType == null) ? null : TypeUsage.Create(entityType), entityCollection.ObjectContext);
			Type type;
			if (ospaceTypeUsage == null)
			{
				type = typeof(TElement);
			}
			else
			{
				type = ObjectViewFactory.GetClrType<TElement>(ospaceTypeUsage.EdmType);
				if (type == null)
				{
					type = typeof(TElement);
				}
			}
			IBindingList bindingList;
			if (type == typeof(TElement))
			{
				bindingList = new ObjectView<TElement>(new ObjectViewEntityCollectionData<TElement, TElement>(entityCollection), entityCollection);
			}
			else
			{
				if (!typeof(TElement).IsAssignableFrom(type))
				{
					throw EntityUtil.ValueInvalidCast(type, typeof(TElement));
				}
				Type type2 = ObjectViewFactory._genericObjectViewEntityCollectionDataType.MakeGenericType(new Type[]
				{
					type,
					typeof(TElement)
				});
				object obj = type2.GetDeclaredConstructor(new Type[] { typeof(EntityCollection<TElement>) }).Invoke(new object[] { entityCollection });
				bindingList = ObjectViewFactory.CreateObjectView(type, type2, obj, entityCollection);
			}
			return bindingList;
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x000A70BC File Offset: 0x000A52BC
		private static IBindingList CreateObjectView(Type clrElementType, Type objectViewDataType, object viewData, object eventDataSource)
		{
			Type type2 = ObjectViewFactory._genericObjectViewType.MakeGenericType(new Type[] { clrElementType });
			Type[] array = objectViewDataType.FindInterfaces((Type type, object unusedFilter) => type.Name == ObjectViewFactory._genericObjectViewDataInterfaceType.Name, null);
			return (IBindingList)type2.GetDeclaredConstructor(new Type[]
			{
				array[0],
				typeof(object)
			}).Invoke(new object[] { viewData, eventDataSource });
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x000A713C File Offset: 0x000A533C
		private static TypeUsage GetOSpaceTypeUsage(TypeUsage typeUsage, ObjectContext objectContext)
		{
			TypeUsage typeUsage2;
			if (typeUsage == null || typeUsage.EdmType == null)
			{
				typeUsage2 = null;
			}
			else if (typeUsage.EdmType.DataSpace == DataSpace.OSpace)
			{
				typeUsage2 = typeUsage;
			}
			else if (objectContext == null)
			{
				typeUsage2 = null;
			}
			else
			{
				typeUsage2 = objectContext.Perspective.MetadataWorkspace.GetOSpaceTypeUsage(typeUsage);
			}
			return typeUsage2;
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x000A7184 File Offset: 0x000A5384
		private static Type GetClrType<TElement>(EdmType ospaceEdmType)
		{
			Type type;
			if (ospaceEdmType.BuiltInTypeKind == BuiltInTypeKind.RowType)
			{
				RowType rowType = (RowType)ospaceEdmType;
				if (rowType.InitializerMetadata != null && rowType.InitializerMetadata.ClrType != null)
				{
					type = rowType.InitializerMetadata.ClrType;
				}
				else
				{
					Type typeFromHandle = typeof(TElement);
					if (typeof(IDataRecord).IsAssignableFrom(typeFromHandle) || typeFromHandle == typeof(object))
					{
						type = null;
					}
					else
					{
						type = typeof(TElement);
					}
				}
			}
			else
			{
				type = ospaceEdmType.ClrType;
				if (type == null)
				{
					type = typeof(TElement);
				}
			}
			return type;
		}

		// Token: 0x040010B9 RID: 4281
		private static readonly Type _genericObjectViewType = typeof(ObjectView<>);

		// Token: 0x040010BA RID: 4282
		private static readonly Type _genericObjectViewDataInterfaceType = typeof(IObjectViewData<>);

		// Token: 0x040010BB RID: 4283
		private static readonly Type _genericObjectViewQueryResultDataType = typeof(ObjectViewQueryResultData<>);

		// Token: 0x040010BC RID: 4284
		private static readonly Type _genericObjectViewEntityCollectionDataType = typeof(ObjectViewEntityCollectionData<, >);
	}
}
