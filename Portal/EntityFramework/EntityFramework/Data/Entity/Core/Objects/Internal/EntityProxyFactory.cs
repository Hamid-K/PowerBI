using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml.Serialization;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200043A RID: 1082
	internal class EntityProxyFactory
	{
		// Token: 0x060034BD RID: 13501 RVA: 0x000A965C File Offset: 0x000A785C
		private static ModuleBuilder GetDynamicModule(EntityType ospaceEntityType)
		{
			Assembly assembly = ospaceEntityType.ClrType.Assembly();
			ModuleBuilder moduleBuilder;
			if (!EntityProxyFactory._moduleBuilders.TryGetValue(assembly, out moduleBuilder))
			{
				moduleBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(string.Format(CultureInfo.InvariantCulture, "EntityFrameworkDynamicProxies-{0}", new object[] { assembly.FullName }))
				{
					Version = new Version(1, 0, 0, 0)
				}, AssemblyBuilderAccess.Run).DefineDynamicModule("EntityProxyModule");
				EntityProxyFactory._moduleBuilders.Add(assembly, moduleBuilder);
			}
			return moduleBuilder;
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000A96D4 File Offset: 0x000A78D4
		private static void DiscardDynamicModule(EntityType ospaceEntityType)
		{
			EntityProxyFactory._moduleBuilders.Remove(ospaceEntityType.ClrType.Assembly());
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000A96EC File Offset: 0x000A78EC
		internal static bool TryGetProxyType(Type clrType, string entityTypeName, out EntityProxyTypeInfo proxyTypeInfo)
		{
			EntityProxyFactory._typeMapLock.EnterReadLock();
			bool flag;
			try
			{
				flag = EntityProxyFactory._proxyNameMap.TryGetValue(new Tuple<Type, string>(clrType, entityTypeName), out proxyTypeInfo);
			}
			finally
			{
				EntityProxyFactory._typeMapLock.ExitReadLock();
			}
			return flag;
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000A9734 File Offset: 0x000A7934
		internal static bool TryGetProxyType(Type proxyType, out EntityProxyTypeInfo proxyTypeInfo)
		{
			EntityProxyFactory._typeMapLock.EnterReadLock();
			bool flag;
			try
			{
				flag = EntityProxyFactory._proxyTypeMap.TryGetValue(proxyType, out proxyTypeInfo);
			}
			finally
			{
				EntityProxyFactory._typeMapLock.ExitReadLock();
			}
			return flag;
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000A9778 File Offset: 0x000A7978
		internal static bool TryGetProxyWrapper(object instance, out IEntityWrapper wrapper)
		{
			wrapper = null;
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (EntityProxyFactory.IsProxyType(instance.GetType()) && EntityProxyFactory.TryGetProxyType(instance.GetType(), out entityProxyTypeInfo))
			{
				wrapper = entityProxyTypeInfo.GetEntityWrapper(instance);
			}
			return wrapper != null;
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000A97B4 File Offset: 0x000A79B4
		internal static EntityProxyTypeInfo GetProxyType(ClrEntityType ospaceEntityType, MetadataWorkspace workspace)
		{
			EntityProxyTypeInfo entityProxyTypeInfo = null;
			if (EntityProxyFactory.TryGetProxyType(ospaceEntityType.ClrType, ospaceEntityType.CSpaceTypeName, out entityProxyTypeInfo))
			{
				if (entityProxyTypeInfo != null)
				{
					entityProxyTypeInfo.ValidateType(ospaceEntityType);
				}
				return entityProxyTypeInfo;
			}
			EntityProxyFactory._typeMapLock.EnterUpgradeableReadLock();
			EntityProxyTypeInfo entityProxyTypeInfo2;
			try
			{
				entityProxyTypeInfo2 = EntityProxyFactory.TryCreateProxyType(ospaceEntityType, workspace);
			}
			finally
			{
				EntityProxyFactory._typeMapLock.ExitUpgradeableReadLock();
			}
			return entityProxyTypeInfo2;
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000A9814 File Offset: 0x000A7A14
		internal static bool TryGetAssociationTypeFromProxyInfo(IEntityWrapper wrappedEntity, string relationshipName, out AssociationType associationType)
		{
			associationType = null;
			EntityProxyTypeInfo entityProxyTypeInfo;
			return EntityProxyFactory.TryGetProxyType(wrappedEntity.Entity.GetType(), out entityProxyTypeInfo) && entityProxyTypeInfo != null && entityProxyTypeInfo.TryGetNavigationPropertyAssociationType(relationshipName, out associationType);
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000A9848 File Offset: 0x000A7A48
		internal static IEnumerable<AssociationType> TryGetAllAssociationTypesFromProxyInfo(IEntityWrapper wrappedEntity)
		{
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (!EntityProxyFactory.TryGetProxyType(wrappedEntity.Entity.GetType(), out entityProxyTypeInfo))
			{
				return null;
			}
			return entityProxyTypeInfo.GetAllAssociationTypes();
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x000A9874 File Offset: 0x000A7A74
		internal static void TryCreateProxyTypes(IEnumerable<EntityType> ospaceEntityTypes, MetadataWorkspace workspace)
		{
			EntityProxyFactory._typeMapLock.EnterUpgradeableReadLock();
			try
			{
				foreach (EntityType entityType in ospaceEntityTypes)
				{
					EntityProxyFactory.TryCreateProxyType(entityType, workspace);
				}
			}
			finally
			{
				EntityProxyFactory._typeMapLock.ExitUpgradeableReadLock();
			}
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x000A98DC File Offset: 0x000A7ADC
		private static EntityProxyTypeInfo TryCreateProxyType(EntityType ospaceEntityType, MetadataWorkspace workspace)
		{
			ClrEntityType clrEntityType = (ClrEntityType)ospaceEntityType;
			Tuple<Type, string> tuple = new Tuple<Type, string>(clrEntityType.ClrType, clrEntityType.HashedDescription);
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (!EntityProxyFactory._proxyNameMap.TryGetValue(tuple, out entityProxyTypeInfo) && EntityProxyFactory.CanProxyType(ospaceEntityType))
			{
				try
				{
					entityProxyTypeInfo = EntityProxyFactory.BuildType(EntityProxyFactory.GetDynamicModule(ospaceEntityType), clrEntityType, workspace);
					EntityProxyFactory._typeMapLock.EnterWriteLock();
					try
					{
						EntityProxyFactory._proxyNameMap[tuple] = entityProxyTypeInfo;
						if (entityProxyTypeInfo != null)
						{
							EntityProxyFactory._proxyTypeMap[entityProxyTypeInfo.ProxyType] = entityProxyTypeInfo;
						}
					}
					finally
					{
						EntityProxyFactory._typeMapLock.ExitWriteLock();
					}
				}
				catch
				{
					EntityProxyFactory.DiscardDynamicModule(ospaceEntityType);
					throw;
				}
			}
			return entityProxyTypeInfo;
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x000A9988 File Offset: 0x000A7B88
		internal static bool IsProxyType(Type type)
		{
			return type != null && EntityProxyFactory._proxyRuntimeAssemblies.Contains(type.Assembly());
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000A99A8 File Offset: 0x000A7BA8
		internal static IEnumerable<Type> GetKnownProxyTypes()
		{
			EntityProxyFactory._typeMapLock.EnterReadLock();
			IEnumerable<Type> enumerable;
			try
			{
				enumerable = (from info in EntityProxyFactory._proxyNameMap.Values
					where info != null
					select info.ProxyType).ToArray<Type>();
			}
			finally
			{
				EntityProxyFactory._typeMapLock.ExitReadLock();
			}
			return enumerable;
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000A9A38 File Offset: 0x000A7C38
		public virtual Func<object, object> CreateBaseGetter(Type declaringType, PropertyInfo propertyInfo)
		{
			ParameterExpression parameterExpression;
			Func<object, object> nonProxyGetter = Expression.Lambda<Func<object, object>>(Expression.Property(Expression.Convert(parameterExpression, declaringType), propertyInfo), new ParameterExpression[] { parameterExpression }).Compile();
			string propertyName = propertyInfo.Name;
			return delegate(object entity)
			{
				Type type = entity.GetType();
				object obj;
				if (EntityProxyFactory.IsProxyType(type) && EntityProxyFactory.TryGetBasePropertyValue(type, propertyName, entity, out obj))
				{
					return obj;
				}
				return nonProxyGetter(entity);
			};
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000A9AA0 File Offset: 0x000A7CA0
		private static bool TryGetBasePropertyValue(Type proxyType, string propertyName, object entity, out object value)
		{
			value = null;
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (EntityProxyFactory.TryGetProxyType(proxyType, out entityProxyTypeInfo) && entityProxyTypeInfo.ContainsBaseGetter(propertyName))
			{
				value = entityProxyTypeInfo.BaseGetter(entity, propertyName);
				return true;
			}
			return false;
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000A9AD5 File Offset: 0x000A7CD5
		public virtual Action<object, object> CreateBaseSetter(Type declaringType, PropertyInfo propertyInfo)
		{
			Action<object, object> nonProxySetter = DelegateFactory.CreateNavigationPropertySetter(declaringType, propertyInfo);
			string propertyName = propertyInfo.Name;
			return delegate(object entity, object value)
			{
				Type type = entity.GetType();
				if (EntityProxyFactory.IsProxyType(type) && EntityProxyFactory.TrySetBasePropertyValue(type, propertyName, entity, value))
				{
					return;
				}
				nonProxySetter(entity, value);
			};
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000A9B00 File Offset: 0x000A7D00
		private static bool TrySetBasePropertyValue(Type proxyType, string propertyName, object entity, object value)
		{
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (EntityProxyFactory.TryGetProxyType(proxyType, out entityProxyTypeInfo) && entityProxyTypeInfo.ContainsBaseSetter(propertyName))
			{
				entityProxyTypeInfo.BaseSetter(entity, propertyName, value);
				return true;
			}
			return false;
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000A9B34 File Offset: 0x000A7D34
		private static EntityProxyTypeInfo BuildType(ModuleBuilder moduleBuilder, ClrEntityType ospaceEntityType, MetadataWorkspace workspace)
		{
			EntityProxyFactory.ProxyTypeBuilder proxyTypeBuilder = new EntityProxyFactory.ProxyTypeBuilder(ospaceEntityType);
			Type type = proxyTypeBuilder.CreateType(moduleBuilder);
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (type != null)
			{
				Assembly assembly = type.Assembly();
				if (!EntityProxyFactory._proxyRuntimeAssemblies.Contains(assembly))
				{
					EntityProxyFactory._proxyRuntimeAssemblies.Add(assembly);
					EntityProxyFactory.AddAssemblyToResolveList(assembly);
				}
				entityProxyTypeInfo = new EntityProxyTypeInfo(type, ospaceEntityType, proxyTypeBuilder.CreateInitializeCollectionMethod(type), proxyTypeBuilder.BaseGetters, proxyTypeBuilder.BaseSetters, workspace);
				foreach (EdmMember edmMember in proxyTypeBuilder.LazyLoadMembers)
				{
					EntityProxyFactory.InterceptMember(edmMember, type, entityProxyTypeInfo);
				}
				EntityProxyFactory.SetResetFKSetterFlagDelegate(type, entityProxyTypeInfo);
				EntityProxyFactory.SetCompareByteArraysDelegate(type);
			}
			else
			{
				entityProxyTypeInfo = null;
			}
			return entityProxyTypeInfo;
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000A9BF4 File Offset: 0x000A7DF4
		private static void AddAssemblyToResolveList(Assembly assembly)
		{
			try
			{
				AppDomain.CurrentDomain.AssemblyResolve += delegate(object _, ResolveEventArgs args)
				{
					if (!(args.Name == assembly.FullName))
					{
						return null;
					}
					return assembly;
				};
			}
			catch (MethodAccessException)
			{
			}
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000A9C3C File Offset: 0x000A7E3C
		private static void InterceptMember(EdmMember member, Type proxyType, EntityProxyTypeInfo proxyTypeInfo)
		{
			PropertyInfo topProperty = proxyType.GetTopProperty(member.Name);
			FieldInfo field = proxyType.GetField(LazyLoadImplementor.GetInterceptorFieldName(member.Name), BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);
			EntityProxyFactory.AssignInterceptionDelegate(EntityProxyFactory.GetInterceptorDelegateMethod.MakeGenericMethod(new Type[] { proxyType, topProperty.PropertyType }).Invoke(null, new object[] { member, proxyTypeInfo.EntityWrapperDelegate }) as Delegate, field);
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000A9CAB File Offset: 0x000A7EAB
		private static void AssignInterceptionDelegate(Delegate interceptorDelegate, FieldInfo interceptorField)
		{
			interceptorField.SetValue(null, interceptorDelegate);
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000A9CB8 File Offset: 0x000A7EB8
		private static void SetResetFKSetterFlagDelegate(Type proxyType, EntityProxyTypeInfo proxyTypeInfo)
		{
			FieldInfo field = proxyType.GetField("_resetFKSetterFlag", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);
			EntityProxyFactory.AssignInterceptionDelegate(EntityProxyFactory.GetResetFKSetterFlagDelegate(proxyTypeInfo.EntityWrapperDelegate), field);
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000A9CE4 File Offset: 0x000A7EE4
		private static Action<object> GetResetFKSetterFlagDelegate(Func<object, object> getEntityWrapperDelegate)
		{
			return delegate(object proxy)
			{
				EntityProxyFactory.ResetFKSetterFlag(getEntityWrapperDelegate(proxy));
			};
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000A9D00 File Offset: 0x000A7F00
		private static void ResetFKSetterFlag(object wrappedEntityAsObject)
		{
			IEntityWrapper entityWrapper = (IEntityWrapper)wrappedEntityAsObject;
			if (entityWrapper != null && entityWrapper.Context != null)
			{
				entityWrapper.Context.ObjectStateManager.EntityInvokingFKSetter = null;
			}
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000A9D30 File Offset: 0x000A7F30
		private static void SetCompareByteArraysDelegate(Type proxyType)
		{
			FieldInfo field = proxyType.GetField("_compareByteArrays", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);
			EntityProxyFactory.AssignInterceptionDelegate(new Func<object, object, bool>(ByValueEqualityComparer.Default.Equals), field);
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000A9D64 File Offset: 0x000A7F64
		private static bool CanProxyType(EntityType ospaceEntityType)
		{
			Type clrType = ospaceEntityType.ClrType;
			if (!clrType.IsPublic() || clrType.IsSealed() || typeof(IEntityWithRelationships).IsAssignableFrom(clrType) || ospaceEntityType.Abstract)
			{
				return false;
			}
			ConstructorInfo declaredConstructor = clrType.GetDeclaredConstructor(new Type[0]);
			return declaredConstructor != null && ((declaredConstructor.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public || (declaredConstructor.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Family || (declaredConstructor.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamORAssem);
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000A9DE0 File Offset: 0x000A7FE0
		private static bool CanProxyMethod(MethodInfo method)
		{
			bool flag = false;
			if (method != null)
			{
				MethodAttributes methodAttributes = method.Attributes & MethodAttributes.MemberAccessMask;
				flag = method.IsVirtual && !method.IsFinal && (methodAttributes == MethodAttributes.Public || methodAttributes == MethodAttributes.Family || methodAttributes == MethodAttributes.FamORAssem);
			}
			return flag;
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000A9E25 File Offset: 0x000A8025
		internal static bool CanProxyGetter(PropertyInfo clrProperty)
		{
			return EntityProxyFactory.CanProxyMethod(clrProperty.Getter());
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000A9E32 File Offset: 0x000A8032
		internal static bool CanProxySetter(PropertyInfo clrProperty)
		{
			return EntityProxyFactory.CanProxyMethod(clrProperty.Setter());
		}

		// Token: 0x04001100 RID: 4352
		internal const string ResetFKSetterFlagFieldName = "_resetFKSetterFlag";

		// Token: 0x04001101 RID: 4353
		internal const string CompareByteArraysFieldName = "_compareByteArrays";

		// Token: 0x04001102 RID: 4354
		private static readonly Dictionary<Tuple<Type, string>, EntityProxyTypeInfo> _proxyNameMap = new Dictionary<Tuple<Type, string>, EntityProxyTypeInfo>();

		// Token: 0x04001103 RID: 4355
		private static readonly Dictionary<Type, EntityProxyTypeInfo> _proxyTypeMap = new Dictionary<Type, EntityProxyTypeInfo>();

		// Token: 0x04001104 RID: 4356
		private static readonly Dictionary<Assembly, ModuleBuilder> _moduleBuilders = new Dictionary<Assembly, ModuleBuilder>();

		// Token: 0x04001105 RID: 4357
		private static readonly ReaderWriterLockSlim _typeMapLock = new ReaderWriterLockSlim();

		// Token: 0x04001106 RID: 4358
		private static readonly HashSet<Assembly> _proxyRuntimeAssemblies = new HashSet<Assembly>();

		// Token: 0x04001107 RID: 4359
		internal static readonly MethodInfo GetInterceptorDelegateMethod = typeof(LazyLoadBehavior).GetOnlyDeclaredMethod("GetInterceptorDelegate");

		// Token: 0x02000A43 RID: 2627
		internal class ProxyTypeBuilder
		{
			// Token: 0x06006157 RID: 24919 RVA: 0x0014F564 File Offset: 0x0014D764
			public ProxyTypeBuilder(ClrEntityType ospaceEntityType)
			{
				this._ospaceEntityType = ospaceEntityType;
				this._baseImplementor = new BaseProxyImplementor();
				this._ipocoImplementor = new IPocoImplementor(ospaceEntityType);
				this._lazyLoadImplementor = new LazyLoadImplementor(ospaceEntityType);
				this._dataContractImplementor = new DataContractImplementor(ospaceEntityType);
				this._iserializableImplementor = new SerializableImplementor(ospaceEntityType);
			}

			// Token: 0x170010AF RID: 4271
			// (get) Token: 0x06006158 RID: 24920 RVA: 0x0014F5C5 File Offset: 0x0014D7C5
			public Type BaseType
			{
				get
				{
					return this._ospaceEntityType.ClrType;
				}
			}

			// Token: 0x06006159 RID: 24921 RVA: 0x0014F5D2 File Offset: 0x0014D7D2
			public DynamicMethod CreateInitializeCollectionMethod(Type proxyType)
			{
				return this._ipocoImplementor.CreateInitializeCollectionMethod(proxyType);
			}

			// Token: 0x170010B0 RID: 4272
			// (get) Token: 0x0600615A RID: 24922 RVA: 0x0014F5E0 File Offset: 0x0014D7E0
			public List<PropertyInfo> BaseGetters
			{
				get
				{
					return this._baseImplementor.BaseGetters;
				}
			}

			// Token: 0x170010B1 RID: 4273
			// (get) Token: 0x0600615B RID: 24923 RVA: 0x0014F5ED File Offset: 0x0014D7ED
			public List<PropertyInfo> BaseSetters
			{
				get
				{
					return this._baseImplementor.BaseSetters;
				}
			}

			// Token: 0x170010B2 RID: 4274
			// (get) Token: 0x0600615C RID: 24924 RVA: 0x0014F5FA File Offset: 0x0014D7FA
			public IEnumerable<EdmMember> LazyLoadMembers
			{
				get
				{
					return this._lazyLoadImplementor.Members;
				}
			}

			// Token: 0x0600615D RID: 24925 RVA: 0x0014F608 File Offset: 0x0014D808
			public Type CreateType(ModuleBuilder moduleBuilder)
			{
				this._moduleBuilder = moduleBuilder;
				bool flag = false;
				if (this._iserializableImplementor.TypeIsSuitable)
				{
					foreach (EdmMember edmMember in this._ospaceEntityType.Members)
					{
						if (this._ipocoImplementor.CanProxyMember(edmMember) || this._lazyLoadImplementor.CanProxyMember(edmMember))
						{
							PropertyInfo topProperty = this.BaseType.GetTopProperty(edmMember.Name);
							PropertyBuilder propertyBuilder = this.TypeBuilder.DefineProperty(edmMember.Name, PropertyAttributes.None, topProperty.PropertyType, Type.EmptyTypes);
							if (!this._ipocoImplementor.EmitMember(this.TypeBuilder, edmMember, propertyBuilder, topProperty, this._baseImplementor))
							{
								EntityProxyFactory.ProxyTypeBuilder.EmitBaseSetter(this.TypeBuilder, propertyBuilder, topProperty);
							}
							if (!this._lazyLoadImplementor.EmitMember(this.TypeBuilder, edmMember, propertyBuilder, topProperty, this._baseImplementor))
							{
								EntityProxyFactory.ProxyTypeBuilder.EmitBaseGetter(this.TypeBuilder, propertyBuilder, topProperty);
							}
							flag = true;
						}
					}
					if (this._typeBuilder != null)
					{
						this._baseImplementor.Implement(this.TypeBuilder);
						this._iserializableImplementor.Implement(this.TypeBuilder, this._serializedFields);
					}
				}
				if (!flag)
				{
					return null;
				}
				return this.TypeBuilder.CreateType();
			}

			// Token: 0x170010B3 RID: 4275
			// (get) Token: 0x0600615E RID: 24926 RVA: 0x0014F768 File Offset: 0x0014D968
			private TypeBuilder TypeBuilder
			{
				get
				{
					if (this._typeBuilder == null)
					{
						TypeAttributes typeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
						if ((this.BaseType.Attributes() & TypeAttributes.Serializable) == TypeAttributes.Serializable)
						{
							typeAttributes |= TypeAttributes.Serializable;
						}
						string text = ((this.BaseType.Name.Length <= 20) ? this.BaseType.Name : this.BaseType.Name.Substring(0, 20));
						string text2 = string.Format(CultureInfo.InvariantCulture, "System.Data.Entity.DynamicProxies.{0}_{1}", new object[]
						{
							text,
							this._ospaceEntityType.HashedDescription
						});
						this._typeBuilder = this._moduleBuilder.DefineType(text2, typeAttributes, this.BaseType, this._ipocoImplementor.Interfaces);
						this._typeBuilder.DefineDefaultConstructor(MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
						Action<FieldBuilder, bool> action = new Action<FieldBuilder, bool>(this.RegisterInstanceField);
						this._ipocoImplementor.Implement(this._typeBuilder, action);
						this._lazyLoadImplementor.Implement(this._typeBuilder, action);
						if (!this._iserializableImplementor.TypeImplementsISerializable)
						{
							this._dataContractImplementor.Implement(this._typeBuilder);
						}
					}
					return this._typeBuilder;
				}
			}

			// Token: 0x0600615F RID: 24927 RVA: 0x0014F894 File Offset: 0x0014DA94
			private static void EmitBaseGetter(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, PropertyInfo baseProperty)
			{
				if (EntityProxyFactory.CanProxyGetter(baseProperty))
				{
					MethodInfo methodInfo = baseProperty.Getter();
					MethodAttributes methodAttributes = methodInfo.Attributes & MethodAttributes.MemberAccessMask;
					MethodBuilder methodBuilder = typeBuilder.DefineMethod("get_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), baseProperty.PropertyType, Type.EmptyTypes);
					ILGenerator ilgenerator = methodBuilder.GetILGenerator();
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.Emit(OpCodes.Call, methodInfo);
					ilgenerator.Emit(OpCodes.Ret);
					propertyBuilder.SetGetMethod(methodBuilder);
				}
			}

			// Token: 0x06006160 RID: 24928 RVA: 0x0014F910 File Offset: 0x0014DB10
			private static void EmitBaseSetter(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, PropertyInfo baseProperty)
			{
				if (EntityProxyFactory.CanProxySetter(baseProperty))
				{
					MethodInfo methodInfo = baseProperty.Setter();
					MethodAttributes methodAttributes = methodInfo.Attributes & MethodAttributes.MemberAccessMask;
					MethodBuilder methodBuilder = typeBuilder.DefineMethod("set_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), null, new Type[] { baseProperty.PropertyType });
					ILGenerator ilgenerator = methodBuilder.GetILGenerator();
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.Emit(OpCodes.Ldarg_1);
					ilgenerator.Emit(OpCodes.Call, methodInfo);
					ilgenerator.Emit(OpCodes.Ret);
					propertyBuilder.SetSetMethod(methodBuilder);
				}
			}

			// Token: 0x06006161 RID: 24929 RVA: 0x0014F99C File Offset: 0x0014DB9C
			private void RegisterInstanceField(FieldBuilder field, bool serializable)
			{
				if (serializable)
				{
					this._serializedFields.Add(field);
					return;
				}
				EntityProxyFactory.ProxyTypeBuilder.MarkAsNotSerializable(field);
			}

			// Token: 0x06006162 RID: 24930 RVA: 0x0014F9B4 File Offset: 0x0014DBB4
			private static ConstructorInfo TryGetScriptIgnoreAttributeConstructor()
			{
				try
				{
					if (AspProxy.IsSystemWebLoaded())
					{
						Type type = Assembly.Load("System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35").GetType("System.Web.Script.Serialization.ScriptIgnoreAttribute");
						if (type != null)
						{
							return type.GetDeclaredConstructor(new Type[0]);
						}
					}
				}
				catch
				{
				}
				return null;
			}

			// Token: 0x06006163 RID: 24931 RVA: 0x0014FA10 File Offset: 0x0014DC10
			public static void MarkAsNotSerializable(FieldBuilder field)
			{
				object[] array = new object[0];
				field.SetCustomAttribute(new CustomAttributeBuilder(EntityProxyFactory.ProxyTypeBuilder._nonSerializedAttributeConstructor, array));
				if (field.IsPublic)
				{
					field.SetCustomAttribute(new CustomAttributeBuilder(EntityProxyFactory.ProxyTypeBuilder._ignoreDataMemberAttributeConstructor, array));
					field.SetCustomAttribute(new CustomAttributeBuilder(EntityProxyFactory.ProxyTypeBuilder._xmlIgnoreAttributeConstructor, array));
					if (EntityProxyFactory.ProxyTypeBuilder._scriptIgnoreAttributeConstructor.Value != null)
					{
						field.SetCustomAttribute(new CustomAttributeBuilder(EntityProxyFactory.ProxyTypeBuilder._scriptIgnoreAttributeConstructor.Value, array));
					}
				}
			}

			// Token: 0x04002A35 RID: 10805
			private TypeBuilder _typeBuilder;

			// Token: 0x04002A36 RID: 10806
			private readonly BaseProxyImplementor _baseImplementor;

			// Token: 0x04002A37 RID: 10807
			private readonly IPocoImplementor _ipocoImplementor;

			// Token: 0x04002A38 RID: 10808
			private readonly LazyLoadImplementor _lazyLoadImplementor;

			// Token: 0x04002A39 RID: 10809
			private readonly DataContractImplementor _dataContractImplementor;

			// Token: 0x04002A3A RID: 10810
			private readonly SerializableImplementor _iserializableImplementor;

			// Token: 0x04002A3B RID: 10811
			private readonly ClrEntityType _ospaceEntityType;

			// Token: 0x04002A3C RID: 10812
			private ModuleBuilder _moduleBuilder;

			// Token: 0x04002A3D RID: 10813
			private readonly List<FieldBuilder> _serializedFields = new List<FieldBuilder>(3);

			// Token: 0x04002A3E RID: 10814
			private static readonly ConstructorInfo _nonSerializedAttributeConstructor = typeof(NonSerializedAttribute).GetDeclaredConstructor(new Type[0]);

			// Token: 0x04002A3F RID: 10815
			private static readonly ConstructorInfo _ignoreDataMemberAttributeConstructor = typeof(IgnoreDataMemberAttribute).GetDeclaredConstructor(new Type[0]);

			// Token: 0x04002A40 RID: 10816
			private static readonly ConstructorInfo _xmlIgnoreAttributeConstructor = typeof(XmlIgnoreAttribute).GetDeclaredConstructor(new Type[0]);

			// Token: 0x04002A41 RID: 10817
			private static readonly Lazy<ConstructorInfo> _scriptIgnoreAttributeConstructor = new Lazy<ConstructorInfo>(new Func<ConstructorInfo>(EntityProxyFactory.ProxyTypeBuilder.TryGetScriptIgnoreAttributeConstructor));
		}
	}
}
