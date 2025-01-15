using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Security;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000455 RID: 1109
	internal sealed class SerializableImplementor
	{
		// Token: 0x06003610 RID: 13840 RVA: 0x000AE3B0 File Offset: 0x000AC5B0
		internal SerializableImplementor(EntityType ospaceEntityType)
		{
			this._baseClrType = ospaceEntityType.ClrType;
			this._baseImplementsISerializable = this._baseClrType.IsSerializable() && typeof(ISerializable).IsAssignableFrom(this._baseClrType);
			if (this._baseImplementsISerializable)
			{
				InterfaceMapping interfaceMap = this._baseClrType.GetInterfaceMap(typeof(ISerializable));
				this._getObjectDataMethod = interfaceMap.TargetMethods[0];
				if (this._getObjectDataMethod.IsVirtual && !this._getObjectDataMethod.IsFinal && this._getObjectDataMethod.IsPublic)
				{
					this._serializationConstructor = this._baseClrType.GetDeclaredConstructor((ConstructorInfo c) => c.IsPublic || c.IsFamily || c.IsFamilyOrAssembly, new Type[][]
					{
						new Type[]
						{
							typeof(SerializationInfo),
							typeof(StreamingContext)
						},
						new Type[]
						{
							typeof(SerializationInfo),
							typeof(object)
						},
						new Type[]
						{
							typeof(object),
							typeof(StreamingContext)
						},
						new Type[]
						{
							typeof(object),
							typeof(object)
						}
					});
					this._canOverride = this._serializationConstructor != null;
				}
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06003611 RID: 13841 RVA: 0x000AE52A File Offset: 0x000AC72A
		internal bool TypeIsSuitable
		{
			get
			{
				return !this._baseImplementsISerializable || this._canOverride;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06003612 RID: 13842 RVA: 0x000AE53C File Offset: 0x000AC73C
		internal bool TypeImplementsISerializable
		{
			get
			{
				return this._baseImplementsISerializable;
			}
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x000AE544 File Offset: 0x000AC744
		internal void Implement(TypeBuilder typeBuilder, IEnumerable<FieldBuilder> serializedFields)
		{
			if (this._baseImplementsISerializable && this._canOverride)
			{
				Type[] array = new Type[]
				{
					typeof(SerializationInfo),
					typeof(StreamingContext)
				};
				MethodBuilder methodBuilder = typeBuilder.DefineMethod(this._getObjectDataMethod.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig, null, array);
				methodBuilder.SetCustomAttribute(new CustomAttributeBuilder(typeof(SecurityCriticalAttribute).GetDeclaredConstructor(new Type[0]), new object[0]));
				ILGenerator ilgenerator = methodBuilder.GetILGenerator();
				foreach (FieldBuilder fieldBuilder in serializedFields)
				{
					ilgenerator.Emit(OpCodes.Ldarg_1);
					ilgenerator.Emit(OpCodes.Ldstr, fieldBuilder.Name);
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.Emit(OpCodes.Ldfld, fieldBuilder);
					ilgenerator.Emit(OpCodes.Ldtoken, fieldBuilder.FieldType);
					ilgenerator.Emit(OpCodes.Call, SerializableImplementor.GetTypeFromHandleMethod);
					ilgenerator.Emit(OpCodes.Callvirt, SerializableImplementor.AddValueMethod);
				}
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(OpCodes.Ldarg_2);
				ilgenerator.Emit(OpCodes.Call, this._getObjectDataMethod);
				ilgenerator.Emit(OpCodes.Ret);
				MethodAttributes methodAttributes = MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
				methodAttributes |= (this._serializationConstructor.IsPublic ? MethodAttributes.Public : MethodAttributes.Private);
				ILGenerator ilgenerator2 = typeBuilder.DefineConstructor(methodAttributes, CallingConventions.Standard | CallingConventions.HasThis, array).GetILGenerator();
				ilgenerator2.Emit(OpCodes.Ldarg_0);
				ilgenerator2.Emit(OpCodes.Ldarg_1);
				ilgenerator2.Emit(OpCodes.Ldarg_2);
				ilgenerator2.Emit(OpCodes.Call, this._serializationConstructor);
				foreach (FieldBuilder fieldBuilder2 in serializedFields)
				{
					ilgenerator2.Emit(OpCodes.Ldarg_0);
					ilgenerator2.Emit(OpCodes.Ldarg_1);
					ilgenerator2.Emit(OpCodes.Ldstr, fieldBuilder2.Name);
					ilgenerator2.Emit(OpCodes.Ldtoken, fieldBuilder2.FieldType);
					ilgenerator2.Emit(OpCodes.Call, SerializableImplementor.GetTypeFromHandleMethod);
					ilgenerator2.Emit(OpCodes.Callvirt, SerializableImplementor.GetValueMethod);
					ilgenerator2.Emit(OpCodes.Castclass, fieldBuilder2.FieldType);
					ilgenerator2.Emit(OpCodes.Stfld, fieldBuilder2);
				}
				ilgenerator2.Emit(OpCodes.Ret);
			}
		}

		// Token: 0x04001177 RID: 4471
		private readonly Type _baseClrType;

		// Token: 0x04001178 RID: 4472
		private readonly bool _baseImplementsISerializable;

		// Token: 0x04001179 RID: 4473
		private readonly bool _canOverride;

		// Token: 0x0400117A RID: 4474
		private readonly MethodInfo _getObjectDataMethod;

		// Token: 0x0400117B RID: 4475
		private readonly ConstructorInfo _serializationConstructor;

		// Token: 0x0400117C RID: 4476
		internal static readonly MethodInfo GetTypeFromHandleMethod = typeof(Type).GetDeclaredMethod("GetTypeFromHandle", new Type[] { typeof(RuntimeTypeHandle) });

		// Token: 0x0400117D RID: 4477
		internal static readonly MethodInfo AddValueMethod = typeof(SerializationInfo).GetDeclaredMethod("AddValue", new Type[]
		{
			typeof(string),
			typeof(object),
			typeof(Type)
		});

		// Token: 0x0400117E RID: 4478
		internal static readonly MethodInfo GetValueMethod = typeof(SerializationInfo).GetDeclaredMethod("GetValue", new Type[]
		{
			typeof(string),
			typeof(Type)
		});
	}
}
