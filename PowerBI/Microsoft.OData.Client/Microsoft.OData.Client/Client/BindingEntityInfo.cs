using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x020000B2 RID: 178
	internal class BindingEntityInfo
	{
		// Token: 0x060005CD RID: 1485 RVA: 0x000197D0 File Offset: 0x000179D0
		internal static IList<BindingEntityInfo.BindingPropertyInfo> GetObservableProperties(Type entityType, ClientEdmModel model)
		{
			return BindingEntityInfo.GetBindingEntityInfoFor(entityType, model).ObservableProperties;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000197DE File Offset: 0x000179DE
		internal static ClientTypeAnnotation GetClientType(Type entityType, ClientEdmModel model)
		{
			return BindingEntityInfo.GetBindingEntityInfoFor(entityType, model).ClientType;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000197EC File Offset: 0x000179EC
		internal static string GetEntitySet(object target, string targetEntitySet, ClientEdmModel model)
		{
			if (!string.IsNullOrEmpty(targetEntitySet))
			{
				return targetEntitySet;
			}
			return BindingEntityInfo.GetEntitySetAttribute(target.GetType(), model);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00019804 File Offset: 0x00017A04
		internal static bool IsDataServiceCollection(Type collectionType, ClientEdmModel model)
		{
			BindingEntityInfo.metadataCacheLock.EnterReadLock();
			try
			{
				object obj;
				if (BindingEntityInfo.knownObservableCollectionTypes.TryGetValue(collectionType, out obj))
				{
					return obj == BindingEntityInfo.TrueObject;
				}
			}
			finally
			{
				BindingEntityInfo.metadataCacheLock.ExitReadLock();
			}
			Type type = collectionType;
			bool flag = false;
			while (type != null)
			{
				if (type.IsGenericType())
				{
					Type[] genericArguments = type.GetGenericArguments();
					if (genericArguments != null && genericArguments.Length == 1 && ClientTypeUtil.TypeOrElementTypeIsEntity(genericArguments[0]))
					{
						Type dataServiceCollectionOfT = WebUtil.GetDataServiceCollectionOfT(genericArguments);
						if (dataServiceCollectionOfT != null && dataServiceCollectionOfT.IsAssignableFrom(type))
						{
							flag = true;
							break;
						}
					}
				}
				type = type.GetBaseType();
			}
			BindingEntityInfo.metadataCacheLock.EnterWriteLock();
			try
			{
				if (!BindingEntityInfo.knownObservableCollectionTypes.ContainsKey(collectionType))
				{
					BindingEntityInfo.knownObservableCollectionTypes[collectionType] = (flag ? BindingEntityInfo.TrueObject : BindingEntityInfo.FalseObject);
				}
			}
			finally
			{
				BindingEntityInfo.metadataCacheLock.ExitWriteLock();
			}
			return flag;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00019900 File Offset: 0x00017B00
		internal static bool IsEntityType(Type type, ClientEdmModel model)
		{
			BindingEntityInfo.metadataCacheLock.EnterReadLock();
			try
			{
				if (BindingEntityInfo.knownNonEntityTypes.Contains(type))
				{
					return false;
				}
			}
			finally
			{
				BindingEntityInfo.metadataCacheLock.ExitReadLock();
			}
			bool flag;
			try
			{
				if (BindingEntityInfo.IsDataServiceCollection(type, model))
				{
					return false;
				}
				flag = ClientTypeUtil.TypeOrElementTypeIsEntity(type);
			}
			catch (InvalidOperationException)
			{
				flag = false;
			}
			if (!flag)
			{
				BindingEntityInfo.metadataCacheLock.EnterWriteLock();
				try
				{
					if (!BindingEntityInfo.knownNonEntityTypes.Contains(type))
					{
						BindingEntityInfo.knownNonEntityTypes.Add(type);
					}
				}
				finally
				{
					BindingEntityInfo.metadataCacheLock.ExitWriteLock();
				}
			}
			return flag;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000199B0 File Offset: 0x00017BB0
		internal static bool TryGetPropertyValue(object source, string sourceProperty, ClientEdmModel model, out BindingEntityInfo.BindingPropertyInfo bindingPropertyInfo, out ClientPropertyAnnotation clientProperty, out object propertyValue)
		{
			Type type = source.GetType();
			bindingPropertyInfo = BindingEntityInfo.GetObservableProperties(type, model).SingleOrDefault((BindingEntityInfo.BindingPropertyInfo x) => x.PropertyInfo.PropertyName == sourceProperty);
			bool flag = bindingPropertyInfo != null;
			if (!flag)
			{
				clientProperty = BindingEntityInfo.GetClientType(type, model).GetProperty(sourceProperty, UndeclaredPropertyBehavior.Support);
				flag = clientProperty != null;
				if (!flag)
				{
					propertyValue = null;
				}
				else
				{
					propertyValue = clientProperty.GetValue(source);
				}
			}
			else
			{
				clientProperty = null;
				propertyValue = bindingPropertyInfo.PropertyInfo.GetValue(source);
			}
			return flag;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00019A40 File Offset: 0x00017C40
		private static BindingEntityInfo.BindingEntityInfoPerType GetBindingEntityInfoFor(Type entityType, ClientEdmModel model)
		{
			BindingEntityInfo.metadataCacheLock.EnterReadLock();
			BindingEntityInfo.BindingEntityInfoPerType bindingEntityInfoPerType;
			try
			{
				if (BindingEntityInfo.bindingEntityInfos.TryGetValue(entityType, out bindingEntityInfoPerType))
				{
					return bindingEntityInfoPerType;
				}
			}
			finally
			{
				BindingEntityInfo.metadataCacheLock.ExitReadLock();
			}
			bindingEntityInfoPerType = new BindingEntityInfo.BindingEntityInfoPerType();
			EntitySetAttribute entitySetAttribute = (EntitySetAttribute)entityType.GetCustomAttributes(typeof(EntitySetAttribute), true).SingleOrDefault<object>();
			bindingEntityInfoPerType.EntitySet = ((entitySetAttribute != null) ? entitySetAttribute.EntitySet : null);
			bindingEntityInfoPerType.ClientType = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(entityType));
			foreach (ClientPropertyAnnotation clientPropertyAnnotation in bindingEntityInfoPerType.ClientType.Properties())
			{
				BindingEntityInfo.BindingPropertyInfo bindingPropertyInfo = null;
				Type propertyType = clientPropertyAnnotation.PropertyType;
				if (!clientPropertyAnnotation.IsStreamLinkProperty)
				{
					if (clientPropertyAnnotation.IsPrimitiveOrEnumOrComplexCollection)
					{
						bindingPropertyInfo = new BindingEntityInfo.BindingPropertyInfo
						{
							PropertyKind = BindingPropertyKind.BindingPropertyKindPrimitiveOrComplexCollection
						};
					}
					else if (clientPropertyAnnotation.IsEntityCollection)
					{
						if (BindingEntityInfo.IsDataServiceCollection(propertyType, model))
						{
							bindingPropertyInfo = new BindingEntityInfo.BindingPropertyInfo
							{
								PropertyKind = BindingPropertyKind.BindingPropertyKindDataServiceCollection
							};
						}
					}
					else if (BindingEntityInfo.IsEntityType(propertyType, model))
					{
						bindingPropertyInfo = new BindingEntityInfo.BindingPropertyInfo
						{
							PropertyKind = BindingPropertyKind.BindingPropertyKindEntity
						};
					}
					else if (BindingEntityInfo.CanBeComplexType(propertyType))
					{
						bindingPropertyInfo = new BindingEntityInfo.BindingPropertyInfo
						{
							PropertyKind = BindingPropertyKind.BindingPropertyKindComplex
						};
					}
					if (bindingPropertyInfo != null)
					{
						bindingPropertyInfo.PropertyInfo = clientPropertyAnnotation;
						if (bindingEntityInfoPerType.ClientType.IsEntityType || bindingPropertyInfo.PropertyKind == BindingPropertyKind.BindingPropertyKindComplex || bindingPropertyInfo.PropertyKind == BindingPropertyKind.BindingPropertyKindPrimitiveOrComplexCollection)
						{
							bindingEntityInfoPerType.ObservableProperties.Add(bindingPropertyInfo);
						}
					}
				}
			}
			BindingEntityInfo.metadataCacheLock.EnterWriteLock();
			try
			{
				if (!BindingEntityInfo.bindingEntityInfos.ContainsKey(entityType))
				{
					BindingEntityInfo.bindingEntityInfos[entityType] = bindingEntityInfoPerType;
				}
			}
			finally
			{
				BindingEntityInfo.metadataCacheLock.ExitWriteLock();
			}
			return bindingEntityInfoPerType;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00019C10 File Offset: 0x00017E10
		private static bool CanBeComplexType(Type type)
		{
			return typeof(INotifyPropertyChanged).IsAssignableFrom(type);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00019C22 File Offset: 0x00017E22
		private static string GetEntitySetAttribute(Type entityType, ClientEdmModel model)
		{
			return BindingEntityInfo.GetBindingEntityInfoFor(entityType, model).EntitySet;
		}

		// Token: 0x040002A1 RID: 673
		private static readonly object FalseObject = new object();

		// Token: 0x040002A2 RID: 674
		private static readonly object TrueObject = new object();

		// Token: 0x040002A3 RID: 675
		private static readonly ReaderWriterLockSlim metadataCacheLock = new ReaderWriterLockSlim();

		// Token: 0x040002A4 RID: 676
		private static readonly HashSet<Type> knownNonEntityTypes = new HashSet<Type>(EqualityComparer<Type>.Default);

		// Token: 0x040002A5 RID: 677
		private static readonly Dictionary<Type, object> knownObservableCollectionTypes = new Dictionary<Type, object>(EqualityComparer<Type>.Default);

		// Token: 0x040002A6 RID: 678
		private static readonly Dictionary<Type, BindingEntityInfo.BindingEntityInfoPerType> bindingEntityInfos = new Dictionary<Type, BindingEntityInfo.BindingEntityInfoPerType>(EqualityComparer<Type>.Default);

		// Token: 0x0200018F RID: 399
		internal class BindingPropertyInfo
		{
			// Token: 0x1700036A RID: 874
			// (get) Token: 0x06000E3C RID: 3644 RVA: 0x00030E83 File Offset: 0x0002F083
			// (set) Token: 0x06000E3D RID: 3645 RVA: 0x00030E8B File Offset: 0x0002F08B
			public ClientPropertyAnnotation PropertyInfo { get; set; }

			// Token: 0x1700036B RID: 875
			// (get) Token: 0x06000E3E RID: 3646 RVA: 0x00030E94 File Offset: 0x0002F094
			// (set) Token: 0x06000E3F RID: 3647 RVA: 0x00030E9C File Offset: 0x0002F09C
			public BindingPropertyKind PropertyKind { get; set; }
		}

		// Token: 0x02000190 RID: 400
		private sealed class BindingEntityInfoPerType
		{
			// Token: 0x06000E41 RID: 3649 RVA: 0x00030EA5 File Offset: 0x0002F0A5
			public BindingEntityInfoPerType()
			{
				this.observableProperties = new List<BindingEntityInfo.BindingPropertyInfo>();
			}

			// Token: 0x1700036C RID: 876
			// (get) Token: 0x06000E42 RID: 3650 RVA: 0x00030EB8 File Offset: 0x0002F0B8
			// (set) Token: 0x06000E43 RID: 3651 RVA: 0x00030EC0 File Offset: 0x0002F0C0
			public string EntitySet { get; set; }

			// Token: 0x1700036D RID: 877
			// (get) Token: 0x06000E44 RID: 3652 RVA: 0x00030EC9 File Offset: 0x0002F0C9
			// (set) Token: 0x06000E45 RID: 3653 RVA: 0x00030ED1 File Offset: 0x0002F0D1
			public ClientTypeAnnotation ClientType { get; set; }

			// Token: 0x1700036E RID: 878
			// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00030EDA File Offset: 0x0002F0DA
			public List<BindingEntityInfo.BindingPropertyInfo> ObservableProperties
			{
				get
				{
					return this.observableProperties;
				}
			}

			// Token: 0x04000775 RID: 1909
			private List<BindingEntityInfo.BindingPropertyInfo> observableProperties;
		}
	}
}
