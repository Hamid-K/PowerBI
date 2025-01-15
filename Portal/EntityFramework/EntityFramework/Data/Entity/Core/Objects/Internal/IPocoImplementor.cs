using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000448 RID: 1096
	internal class IPocoImplementor
	{
		// Token: 0x06003565 RID: 13669 RVA: 0x000AB4F0 File Offset: 0x000A96F0
		public IPocoImplementor(EntityType ospaceEntityType)
		{
			Type clrType = ospaceEntityType.ClrType;
			this._referenceProperties = new List<KeyValuePair<NavigationProperty, PropertyInfo>>();
			this._collectionProperties = new List<KeyValuePair<NavigationProperty, PropertyInfo>>();
			this._implementIEntityWithChangeTracker = null == clrType.GetInterface(typeof(IEntityWithChangeTracker).Name);
			this._implementIEntityWithRelationships = null == clrType.GetInterface(typeof(IEntityWithRelationships).Name);
			this.CheckType(ospaceEntityType);
			this._ospaceEntityType = ospaceEntityType;
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000AB570 File Offset: 0x000A9770
		private void CheckType(EntityType ospaceEntityType)
		{
			this._scalarMembers = new HashSet<EdmMember>();
			this._relationshipMembers = new HashSet<EdmMember>();
			foreach (EdmMember edmMember in ospaceEntityType.Members)
			{
				PropertyInfo topProperty = ospaceEntityType.ClrType.GetTopProperty(edmMember.Name);
				if (topProperty != null && EntityProxyFactory.CanProxySetter(topProperty))
				{
					if (edmMember.BuiltInTypeKind == BuiltInTypeKind.EdmProperty)
					{
						if (this._implementIEntityWithChangeTracker)
						{
							this._scalarMembers.Add(edmMember);
						}
					}
					else if (edmMember.BuiltInTypeKind == BuiltInTypeKind.NavigationProperty && this._implementIEntityWithRelationships)
					{
						if (((NavigationProperty)edmMember).ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
						{
							if (topProperty.PropertyType.IsGenericType() && topProperty.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
							{
								this._relationshipMembers.Add(edmMember);
							}
						}
						else
						{
							this._relationshipMembers.Add(edmMember);
						}
					}
				}
			}
			if (ospaceEntityType.Members.Count != this._scalarMembers.Count + this._relationshipMembers.Count)
			{
				this._scalarMembers.Clear();
				this._relationshipMembers.Clear();
				this._implementIEntityWithChangeTracker = false;
				this._implementIEntityWithRelationships = false;
			}
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000AB6D4 File Offset: 0x000A98D4
		public void Implement(TypeBuilder typeBuilder, Action<FieldBuilder, bool> registerField)
		{
			if (this._implementIEntityWithChangeTracker)
			{
				this.ImplementIEntityWithChangeTracker(typeBuilder, registerField);
			}
			if (this._implementIEntityWithRelationships)
			{
				this.ImplementIEntityWithRelationships(typeBuilder, registerField);
			}
			this._resetFKSetterFlagField = typeBuilder.DefineField("_resetFKSetterFlag", typeof(Action<object>), FieldAttributes.Private | FieldAttributes.Static);
			this._compareByteArraysField = typeBuilder.DefineField("_compareByteArrays", typeof(Func<object, object, bool>), FieldAttributes.Private | FieldAttributes.Static);
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06003568 RID: 13672 RVA: 0x000AB73C File Offset: 0x000A993C
		public Type[] Interfaces
		{
			get
			{
				List<Type> list = new List<Type>();
				if (this._implementIEntityWithChangeTracker)
				{
					list.Add(typeof(IEntityWithChangeTracker));
				}
				if (this._implementIEntityWithRelationships)
				{
					list.Add(typeof(IEntityWithRelationships));
				}
				return list.ToArray();
			}
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x000AB785 File Offset: 0x000A9985
		private static DynamicMethod CreateDynamicMethod(string name, Type returnType, Type[] parameterTypes)
		{
			return new DynamicMethod(name, returnType, parameterTypes, true);
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x000AB790 File Offset: 0x000A9990
		public DynamicMethod CreateInitializeCollectionMethod(Type proxyType)
		{
			if (this._collectionProperties.Count > 0)
			{
				DynamicMethod dynamicMethod = IPocoImplementor.CreateDynamicMethod(proxyType.Name + "_InitializeEntityCollections", typeof(IEntityWrapper), new Type[] { typeof(IEntityWrapper) });
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.DeclareLocal(proxyType);
				ilgenerator.DeclareLocal(typeof(RelationshipManager));
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Callvirt, IPocoImplementor.GetEntityMethod);
				ilgenerator.Emit(OpCodes.Castclass, proxyType);
				ilgenerator.Emit(OpCodes.Stloc_0);
				ilgenerator.Emit(OpCodes.Ldloc_0);
				ilgenerator.Emit(OpCodes.Callvirt, IPocoImplementor.GetRelationshipManagerMethod);
				ilgenerator.Emit(OpCodes.Stloc_1);
				foreach (KeyValuePair<NavigationProperty, PropertyInfo> keyValuePair in this._collectionProperties)
				{
					MethodInfo methodInfo = IPocoImplementor.GetRelatedCollectionMethod.MakeGenericMethod(new Type[] { EntityUtil.GetCollectionElementType(keyValuePair.Value.PropertyType) });
					ilgenerator.Emit(OpCodes.Ldloc_0);
					ilgenerator.Emit(OpCodes.Ldloc_1);
					ilgenerator.Emit(OpCodes.Ldstr, keyValuePair.Key.RelationshipType.FullName);
					ilgenerator.Emit(OpCodes.Ldstr, keyValuePair.Key.ToEndMember.Name);
					ilgenerator.Emit(OpCodes.Callvirt, methodInfo);
					ilgenerator.Emit(OpCodes.Callvirt, keyValuePair.Value.Setter());
				}
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ret);
				return dynamicMethod;
			}
			return null;
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x000AB950 File Offset: 0x000A9B50
		public bool CanProxyMember(EdmMember member)
		{
			return this._scalarMembers.Contains(member) || this._relationshipMembers.Contains(member);
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x000AB970 File Offset: 0x000A9B70
		public bool EmitMember(TypeBuilder typeBuilder, EdmMember member, PropertyBuilder propertyBuilder, PropertyInfo baseProperty, BaseProxyImplementor baseImplementor)
		{
			if (this._scalarMembers.Contains(member))
			{
				bool flag = this._ospaceEntityType.KeyMembers.Contains(member.Identity);
				this.EmitScalarSetter(typeBuilder, propertyBuilder, baseProperty, flag);
				return true;
			}
			if (this._relationshipMembers.Contains(member))
			{
				NavigationProperty navigationProperty = member as NavigationProperty;
				if (navigationProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					this.EmitCollectionProperty(typeBuilder, propertyBuilder, baseProperty, navigationProperty);
				}
				else
				{
					this.EmitReferenceProperty(typeBuilder, propertyBuilder, baseProperty, navigationProperty);
				}
				baseImplementor.AddBasePropertySetter(baseProperty);
				return true;
			}
			return false;
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000AB9F8 File Offset: 0x000A9BF8
		private void EmitScalarSetter(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, PropertyInfo baseProperty, bool isKeyMember)
		{
			MethodInfo methodInfo = baseProperty.Setter();
			MethodAttributes methodAttributes = methodInfo.Attributes & MethodAttributes.MemberAccessMask;
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("set_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), null, new Type[] { baseProperty.PropertyType });
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			Label label = ilgenerator.DefineLabel();
			if (isKeyMember)
			{
				MethodInfo methodInfo2 = baseProperty.Getter();
				if (methodInfo2 != null)
				{
					Type propertyType = baseProperty.PropertyType;
					if (propertyType == typeof(int) || propertyType == typeof(short) || propertyType == typeof(long) || propertyType == typeof(bool) || propertyType == typeof(byte) || propertyType == typeof(uint) || propertyType == typeof(ulong) || propertyType == typeof(float) || propertyType == typeof(double) || propertyType.IsEnum())
					{
						ilgenerator.Emit(OpCodes.Ldarg_0);
						ilgenerator.Emit(OpCodes.Call, methodInfo2);
						ilgenerator.Emit(OpCodes.Ldarg_1);
						ilgenerator.Emit(OpCodes.Beq_S, label);
					}
					else if (propertyType == typeof(byte[]))
					{
						ilgenerator.Emit(OpCodes.Ldsfld, this._compareByteArraysField);
						ilgenerator.Emit(OpCodes.Ldarg_0);
						ilgenerator.Emit(OpCodes.Call, methodInfo2);
						ilgenerator.Emit(OpCodes.Ldarg_1);
						ilgenerator.Emit(OpCodes.Callvirt, IPocoImplementor.FuncInvokeMethod);
						ilgenerator.Emit(OpCodes.Brtrue_S, label);
					}
					else
					{
						MethodInfo declaredMethod = propertyType.GetDeclaredMethod("op_Inequality", new Type[] { propertyType, propertyType });
						if (declaredMethod != null)
						{
							ilgenerator.Emit(OpCodes.Ldarg_0);
							ilgenerator.Emit(OpCodes.Call, methodInfo2);
							ilgenerator.Emit(OpCodes.Ldarg_1);
							ilgenerator.Emit(OpCodes.Call, declaredMethod);
							ilgenerator.Emit(OpCodes.Brfalse_S, label);
						}
						else
						{
							ilgenerator.Emit(OpCodes.Ldarg_0);
							ilgenerator.Emit(OpCodes.Call, methodInfo2);
							if (propertyType.IsValueType())
							{
								ilgenerator.Emit(OpCodes.Box, propertyType);
							}
							ilgenerator.Emit(OpCodes.Ldarg_1);
							if (propertyType.IsValueType())
							{
								ilgenerator.Emit(OpCodes.Box, propertyType);
							}
							ilgenerator.Emit(OpCodes.Call, IPocoImplementor.ObjectEqualsMethod);
							ilgenerator.Emit(OpCodes.Brtrue_S, label);
						}
					}
				}
			}
			ilgenerator.BeginExceptionBlock();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldstr, baseProperty.Name);
			ilgenerator.Emit(OpCodes.Call, this._entityMemberChanging);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Call, methodInfo);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldstr, baseProperty.Name);
			ilgenerator.Emit(OpCodes.Call, this._entityMemberChanged);
			ilgenerator.BeginFinallyBlock();
			ilgenerator.Emit(OpCodes.Ldsfld, this._resetFKSetterFlagField);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Callvirt, IPocoImplementor.InvokeMethod);
			ilgenerator.EndExceptionBlock();
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ret);
			propertyBuilder.SetSetMethod(methodBuilder);
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000ABD78 File Offset: 0x000A9F78
		private void EmitReferenceProperty(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, PropertyInfo baseProperty, NavigationProperty navProperty)
		{
			MethodAttributes methodAttributes = baseProperty.Setter().Attributes & MethodAttributes.MemberAccessMask;
			MethodInfo methodInfo = IPocoImplementor.GetRelatedReferenceMethod.MakeGenericMethod(new Type[] { baseProperty.PropertyType });
			MethodInfo onlyDeclaredMethod = typeof(EntityReference<>).MakeGenericType(new Type[] { baseProperty.PropertyType }).GetOnlyDeclaredMethod("set_Value");
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("set_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), null, new Type[] { baseProperty.PropertyType });
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Callvirt, this._getRelationshipManager);
			ilgenerator.Emit(OpCodes.Ldstr, navProperty.RelationshipType.FullName);
			ilgenerator.Emit(OpCodes.Ldstr, navProperty.ToEndMember.Name);
			ilgenerator.Emit(OpCodes.Callvirt, methodInfo);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Callvirt, onlyDeclaredMethod);
			ilgenerator.Emit(OpCodes.Ret);
			propertyBuilder.SetSetMethod(methodBuilder);
			this._referenceProperties.Add(new KeyValuePair<NavigationProperty, PropertyInfo>(navProperty, baseProperty));
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000ABE9C File Offset: 0x000AA09C
		private void EmitCollectionProperty(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, PropertyInfo baseProperty, NavigationProperty navProperty)
		{
			MethodAttributes methodAttributes = baseProperty.Setter().Attributes & MethodAttributes.MemberAccessMask;
			string text = Strings.EntityProxyTypeInfo_CannotSetEntityCollectionProperty(propertyBuilder.Name, typeBuilder.Name);
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("set_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), null, new Type[] { baseProperty.PropertyType });
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			Label label = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, this._getRelationshipManager);
			ilgenerator.Emit(OpCodes.Ldstr, navProperty.RelationshipType.FullName);
			ilgenerator.Emit(OpCodes.Ldstr, navProperty.ToEndMember.Name);
			ilgenerator.Emit(OpCodes.Callvirt, IPocoImplementor.GetRelatedEndMethod);
			ilgenerator.Emit(OpCodes.Beq_S, label);
			ilgenerator.Emit(OpCodes.Ldstr, text);
			ilgenerator.Emit(OpCodes.Newobj, IPocoImplementor._invalidOperationConstructorMethod);
			ilgenerator.Emit(OpCodes.Throw);
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Call, baseProperty.Setter());
			ilgenerator.Emit(OpCodes.Ret);
			propertyBuilder.SetSetMethod(methodBuilder);
			this._collectionProperties.Add(new KeyValuePair<NavigationProperty, PropertyInfo>(navProperty, baseProperty));
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000ABFF0 File Offset: 0x000AA1F0
		private void ImplementIEntityWithChangeTracker(TypeBuilder typeBuilder, Action<FieldBuilder, bool> registerField)
		{
			this._changeTrackerField = typeBuilder.DefineField("_changeTracker", typeof(IEntityChangeTracker), FieldAttributes.Private);
			registerField(this._changeTrackerField, false);
			this._entityMemberChanging = typeBuilder.DefineMethod("EntityMemberChanging", MethodAttributes.Private | MethodAttributes.HideBySig, typeof(void), new Type[] { typeof(string) });
			ILGenerator ilgenerator = this._entityMemberChanging.GetILGenerator();
			Label label = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldfld, this._changeTrackerField);
			ilgenerator.Emit(OpCodes.Brfalse_S, label);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldfld, this._changeTrackerField);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Callvirt, IPocoImplementor.EntityMemberChangingMethod);
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ret);
			this._entityMemberChanged = typeBuilder.DefineMethod("EntityMemberChanged", MethodAttributes.Private | MethodAttributes.HideBySig, typeof(void), new Type[] { typeof(string) });
			ILGenerator ilgenerator2 = this._entityMemberChanged.GetILGenerator();
			label = ilgenerator2.DefineLabel();
			ilgenerator2.Emit(OpCodes.Ldarg_0);
			ilgenerator2.Emit(OpCodes.Ldfld, this._changeTrackerField);
			ilgenerator2.Emit(OpCodes.Brfalse_S, label);
			ilgenerator2.Emit(OpCodes.Ldarg_0);
			ilgenerator2.Emit(OpCodes.Ldfld, this._changeTrackerField);
			ilgenerator2.Emit(OpCodes.Ldarg_1);
			ilgenerator2.Emit(OpCodes.Callvirt, IPocoImplementor.EntityMemberChangedMethod);
			ilgenerator2.MarkLabel(label);
			ilgenerator2.Emit(OpCodes.Ret);
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("IEntityWithChangeTracker.SetChangeTracker", MethodAttributes.Private | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask, typeof(void), new Type[] { typeof(IEntityChangeTracker) });
			ILGenerator ilgenerator3 = methodBuilder.GetILGenerator();
			ilgenerator3.Emit(OpCodes.Ldarg_0);
			ilgenerator3.Emit(OpCodes.Ldarg_1);
			ilgenerator3.Emit(OpCodes.Stfld, this._changeTrackerField);
			ilgenerator3.Emit(OpCodes.Ret);
			typeBuilder.DefineMethodOverride(methodBuilder, IPocoImplementor.SetChangeTrackerMethod);
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000AC204 File Offset: 0x000AA404
		private void ImplementIEntityWithRelationships(TypeBuilder typeBuilder, Action<FieldBuilder, bool> registerField)
		{
			this._relationshipManagerField = typeBuilder.DefineField("_relationshipManager", typeof(RelationshipManager), FieldAttributes.Private);
			registerField(this._relationshipManagerField, true);
			PropertyBuilder propertyBuilder = typeBuilder.DefineProperty("RelationshipManager", PropertyAttributes.None, typeof(RelationshipManager), Type.EmptyTypes);
			this._getRelationshipManager = typeBuilder.DefineMethod("IEntityWithRelationships.get_RelationshipManager", MethodAttributes.Private | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask | MethodAttributes.SpecialName, typeof(RelationshipManager), Type.EmptyTypes);
			ILGenerator ilgenerator = this._getRelationshipManager.GetILGenerator();
			Label label = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldfld, this._relationshipManagerField);
			ilgenerator.Emit(OpCodes.Brtrue_S, label);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, IPocoImplementor.CreateRelationshipManagerMethod);
			ilgenerator.Emit(OpCodes.Stfld, this._relationshipManagerField);
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldfld, this._relationshipManagerField);
			ilgenerator.Emit(OpCodes.Ret);
			propertyBuilder.SetGetMethod(this._getRelationshipManager);
			typeBuilder.DefineMethodOverride(this._getRelationshipManager, IPocoImplementor.GetRelationshipManagerMethod);
		}

		// Token: 0x0400113A RID: 4410
		private readonly EntityType _ospaceEntityType;

		// Token: 0x0400113B RID: 4411
		private FieldBuilder _changeTrackerField;

		// Token: 0x0400113C RID: 4412
		private FieldBuilder _relationshipManagerField;

		// Token: 0x0400113D RID: 4413
		private FieldBuilder _resetFKSetterFlagField;

		// Token: 0x0400113E RID: 4414
		private FieldBuilder _compareByteArraysField;

		// Token: 0x0400113F RID: 4415
		private MethodBuilder _entityMemberChanging;

		// Token: 0x04001140 RID: 4416
		private MethodBuilder _entityMemberChanged;

		// Token: 0x04001141 RID: 4417
		private MethodBuilder _getRelationshipManager;

		// Token: 0x04001142 RID: 4418
		private readonly List<KeyValuePair<NavigationProperty, PropertyInfo>> _referenceProperties;

		// Token: 0x04001143 RID: 4419
		private readonly List<KeyValuePair<NavigationProperty, PropertyInfo>> _collectionProperties;

		// Token: 0x04001144 RID: 4420
		private bool _implementIEntityWithChangeTracker;

		// Token: 0x04001145 RID: 4421
		private bool _implementIEntityWithRelationships;

		// Token: 0x04001146 RID: 4422
		private HashSet<EdmMember> _scalarMembers;

		// Token: 0x04001147 RID: 4423
		private HashSet<EdmMember> _relationshipMembers;

		// Token: 0x04001148 RID: 4424
		internal static readonly MethodInfo EntityMemberChangingMethod = typeof(IEntityChangeTracker).GetDeclaredMethod("EntityMemberChanging", new Type[] { typeof(string) });

		// Token: 0x04001149 RID: 4425
		internal static readonly MethodInfo EntityMemberChangedMethod = typeof(IEntityChangeTracker).GetDeclaredMethod("EntityMemberChanged", new Type[] { typeof(string) });

		// Token: 0x0400114A RID: 4426
		internal static readonly MethodInfo CreateRelationshipManagerMethod = typeof(RelationshipManager).GetDeclaredMethod("Create", new Type[] { typeof(IEntityWithRelationships) });

		// Token: 0x0400114B RID: 4427
		internal static readonly MethodInfo GetRelationshipManagerMethod = typeof(IEntityWithRelationships).GetDeclaredProperty("RelationshipManager").Getter();

		// Token: 0x0400114C RID: 4428
		internal static readonly MethodInfo GetRelatedReferenceMethod = typeof(RelationshipManager).GetDeclaredMethod("GetRelatedReference", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x0400114D RID: 4429
		internal static readonly MethodInfo GetRelatedCollectionMethod = typeof(RelationshipManager).GetDeclaredMethod("GetRelatedCollection", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x0400114E RID: 4430
		internal static readonly MethodInfo GetRelatedEndMethod = typeof(RelationshipManager).GetDeclaredMethod("GetRelatedEnd", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x0400114F RID: 4431
		internal static readonly MethodInfo ObjectEqualsMethod = typeof(object).GetDeclaredMethod("Equals", new Type[]
		{
			typeof(object),
			typeof(object)
		});

		// Token: 0x04001150 RID: 4432
		private static readonly ConstructorInfo _invalidOperationConstructorMethod = typeof(InvalidOperationException).GetDeclaredConstructor(new Type[] { typeof(string) });

		// Token: 0x04001151 RID: 4433
		internal static readonly MethodInfo GetEntityMethod = typeof(IEntityWrapper).GetDeclaredProperty("Entity").Getter();

		// Token: 0x04001152 RID: 4434
		internal static readonly MethodInfo InvokeMethod = typeof(Action<object>).GetDeclaredMethod("Invoke", new Type[] { typeof(object) });

		// Token: 0x04001153 RID: 4435
		internal static readonly MethodInfo FuncInvokeMethod = typeof(Func<object, object, bool>).GetDeclaredMethod("Invoke", new Type[]
		{
			typeof(object),
			typeof(object)
		});

		// Token: 0x04001154 RID: 4436
		internal static readonly MethodInfo SetChangeTrackerMethod = typeof(IEntityWithChangeTracker).GetOnlyDeclaredMethod("SetChangeTracker");
	}
}
