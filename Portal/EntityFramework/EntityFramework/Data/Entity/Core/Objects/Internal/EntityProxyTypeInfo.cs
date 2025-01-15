using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200043C RID: 1084
	internal sealed class EntityProxyTypeInfo
	{
		// Token: 0x060034DE RID: 13534 RVA: 0x000A9EC8 File Offset: 0x000A80C8
		internal EntityProxyTypeInfo(Type proxyType, ClrEntityType ospaceEntityType, DynamicMethod initializeCollections, List<PropertyInfo> baseGetters, List<PropertyInfo> baseSetters, MetadataWorkspace workspace)
		{
			this._proxyType = proxyType;
			this._entityType = ospaceEntityType;
			this._initializeCollections = initializeCollections;
			foreach (AssociationType associationType in EntityProxyTypeInfo.GetAllRelationshipsForType(workspace, proxyType))
			{
				this._navigationPropertyAssociationTypes.Add(associationType.FullName, associationType);
				if (associationType.Name != associationType.FullName)
				{
					this._navigationPropertyAssociationTypes.Add(associationType.Name, associationType);
				}
			}
			FieldInfo field = proxyType.GetField("_entityWrapper", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "proxy");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "value");
			Expression<Func<object, object>> expression = Expression.Lambda<Func<object, object>>(Expression.Field(Expression.Convert(parameterExpression, field.DeclaringType), field), new ParameterExpression[] { parameterExpression });
			Func<object, object> getEntityWrapperDelegate = expression.Compile();
			this.Proxy_GetEntityWrapper = delegate(object proxy)
			{
				IEntityWrapper entityWrapper = (IEntityWrapper)getEntityWrapperDelegate(proxy);
				if (entityWrapper != null && entityWrapper.Entity != proxy)
				{
					throw new InvalidOperationException(Strings.EntityProxyTypeInfo_ProxyHasWrongWrapper);
				}
				return entityWrapper;
			};
			this.Proxy_SetEntityWrapper = Expression.Lambda<Func<object, object, object>>(Expression.Assign(Expression.Field(Expression.Convert(parameterExpression, field.DeclaringType), field), parameterExpression2), new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile();
			ParameterExpression parameterExpression3 = Expression.Parameter(typeof(string), "propertyName");
			MethodInfo publicInstanceMethod = proxyType.GetPublicInstanceMethod("GetBasePropertyValue", new Type[] { typeof(string) });
			if (publicInstanceMethod != null)
			{
				this._baseGetter = Expression.Lambda<Func<object, string, object>>(Expression.Call(Expression.Convert(parameterExpression, proxyType), publicInstanceMethod, new Expression[] { parameterExpression3 }), new ParameterExpression[] { parameterExpression, parameterExpression3 }).Compile();
			}
			ParameterExpression parameterExpression4 = Expression.Parameter(typeof(object), "propertyName");
			MethodInfo publicInstanceMethod2 = proxyType.GetPublicInstanceMethod("SetBasePropertyValue", new Type[]
			{
				typeof(string),
				typeof(object)
			});
			if (publicInstanceMethod2 != null)
			{
				this._baseSetter = Expression.Lambda<Action<object, string, object>>(Expression.Call(Expression.Convert(parameterExpression, proxyType), publicInstanceMethod2, parameterExpression3, parameterExpression4), new ParameterExpression[] { parameterExpression, parameterExpression3, parameterExpression4 }).Compile();
			}
			this._propertiesWithBaseGetter = new HashSet<string>(baseGetters.Select((PropertyInfo p) => p.Name));
			this._propertiesWithBaseSetter = new HashSet<string>(baseSetters.Select((PropertyInfo p) => p.Name));
			this._createObject = DelegateFactory.CreateConstructor(proxyType);
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x000AA198 File Offset: 0x000A8398
		internal static IEnumerable<AssociationType> GetAllRelationshipsForType(MetadataWorkspace workspace, Type clrType)
		{
			return from a in ((ObjectItemCollection)workspace.GetItemCollection(DataSpace.OSpace)).GetItems<AssociationType>()
				where EntityProxyTypeInfo.IsEndMemberForType(a.AssociationEndMembers[0], clrType) || EntityProxyTypeInfo.IsEndMemberForType(a.AssociationEndMembers[1], clrType)
				select a;
		}

		// Token: 0x060034E0 RID: 13536 RVA: 0x000AA1D4 File Offset: 0x000A83D4
		private static bool IsEndMemberForType(AssociationEndMember end, Type clrType)
		{
			RefType refType = end.TypeUsage.EdmType as RefType;
			return refType != null && refType.ElementType.ClrType.IsAssignableFrom(clrType);
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000AA208 File Offset: 0x000A8408
		internal object CreateProxyObject()
		{
			return this._createObject();
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060034E2 RID: 13538 RVA: 0x000AA215 File Offset: 0x000A8415
		internal Type ProxyType
		{
			get
			{
				return this._proxyType;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060034E3 RID: 13539 RVA: 0x000AA21D File Offset: 0x000A841D
		internal DynamicMethod InitializeEntityCollections
		{
			get
			{
				return this._initializeCollections;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060034E4 RID: 13540 RVA: 0x000AA225 File Offset: 0x000A8425
		public Func<object, string, object> BaseGetter
		{
			get
			{
				return this._baseGetter;
			}
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000AA22D File Offset: 0x000A842D
		public bool ContainsBaseGetter(string propertyName)
		{
			return this.BaseGetter != null && this._propertiesWithBaseGetter.Contains(propertyName);
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x000AA245 File Offset: 0x000A8445
		public bool ContainsBaseSetter(string propertyName)
		{
			return this.BaseSetter != null && this._propertiesWithBaseSetter.Contains(propertyName);
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060034E7 RID: 13543 RVA: 0x000AA25D File Offset: 0x000A845D
		public Action<object, string, object> BaseSetter
		{
			get
			{
				return this._baseSetter;
			}
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x000AA265 File Offset: 0x000A8465
		public bool TryGetNavigationPropertyAssociationType(string relationshipName, out AssociationType associationType)
		{
			return this._navigationPropertyAssociationTypes.TryGetValue(relationshipName, out associationType);
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000AA274 File Offset: 0x000A8474
		public IEnumerable<AssociationType> GetAllAssociationTypes()
		{
			return this._navigationPropertyAssociationTypes.Values.Distinct<AssociationType>();
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000AA286 File Offset: 0x000A8486
		public void ValidateType(ClrEntityType ospaceEntityType)
		{
			if (ospaceEntityType != this._entityType && ospaceEntityType.HashedDescription != this._entityType.HashedDescription)
			{
				throw new InvalidOperationException(Strings.EntityProxyTypeInfo_DuplicateOSpaceType(ospaceEntityType.ClrType.FullName));
			}
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000AA2BF File Offset: 0x000A84BF
		internal IEntityWrapper SetEntityWrapper(IEntityWrapper wrapper)
		{
			return this.Proxy_SetEntityWrapper(wrapper.Entity, wrapper) as IEntityWrapper;
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x000AA2D8 File Offset: 0x000A84D8
		internal IEntityWrapper GetEntityWrapper(object entity)
		{
			return this.Proxy_GetEntityWrapper(entity) as IEntityWrapper;
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060034ED RID: 13549 RVA: 0x000AA2EB File Offset: 0x000A84EB
		internal Func<object, object> EntityWrapperDelegate
		{
			get
			{
				return this.Proxy_GetEntityWrapper;
			}
		}

		// Token: 0x0400110A RID: 4362
		private readonly Type _proxyType;

		// Token: 0x0400110B RID: 4363
		private readonly ClrEntityType _entityType;

		// Token: 0x0400110C RID: 4364
		internal const string EntityWrapperFieldName = "_entityWrapper";

		// Token: 0x0400110D RID: 4365
		private const string InitializeEntityCollectionsName = "InitializeEntityCollections";

		// Token: 0x0400110E RID: 4366
		private readonly DynamicMethod _initializeCollections;

		// Token: 0x0400110F RID: 4367
		private readonly Func<object, string, object> _baseGetter;

		// Token: 0x04001110 RID: 4368
		private readonly HashSet<string> _propertiesWithBaseGetter;

		// Token: 0x04001111 RID: 4369
		private readonly Action<object, string, object> _baseSetter;

		// Token: 0x04001112 RID: 4370
		private readonly HashSet<string> _propertiesWithBaseSetter;

		// Token: 0x04001113 RID: 4371
		private readonly Func<object, object> Proxy_GetEntityWrapper;

		// Token: 0x04001114 RID: 4372
		private readonly Func<object, object, object> Proxy_SetEntityWrapper;

		// Token: 0x04001115 RID: 4373
		private readonly Func<object> _createObject;

		// Token: 0x04001116 RID: 4374
		private readonly Dictionary<string, AssociationType> _navigationPropertyAssociationTypes = new Dictionary<string, AssociationType>();
	}
}
