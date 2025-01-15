using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000435 RID: 1077
	internal class BaseProxyImplementor
	{
		// Token: 0x0600345F RID: 13407 RVA: 0x000A8B20 File Offset: 0x000A6D20
		public BaseProxyImplementor()
		{
			this._baseGetters = new List<PropertyInfo>();
			this._baseSetters = new List<PropertyInfo>();
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06003460 RID: 13408 RVA: 0x000A8B3E File Offset: 0x000A6D3E
		public List<PropertyInfo> BaseGetters
		{
			get
			{
				return this._baseGetters;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06003461 RID: 13409 RVA: 0x000A8B46 File Offset: 0x000A6D46
		public List<PropertyInfo> BaseSetters
		{
			get
			{
				return this._baseSetters;
			}
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x000A8B4E File Offset: 0x000A6D4E
		public void AddBasePropertyGetter(PropertyInfo baseProperty)
		{
			this._baseGetters.Add(baseProperty);
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x000A8B5C File Offset: 0x000A6D5C
		public void AddBasePropertySetter(PropertyInfo baseProperty)
		{
			this._baseSetters.Add(baseProperty);
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x000A8B6A File Offset: 0x000A6D6A
		public void Implement(TypeBuilder typeBuilder)
		{
			if (this._baseGetters.Count > 0)
			{
				this.ImplementBaseGetter(typeBuilder);
			}
			if (this._baseSetters.Count > 0)
			{
				this.ImplementBaseSetter(typeBuilder);
			}
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x000A8B98 File Offset: 0x000A6D98
		private void ImplementBaseGetter(TypeBuilder typeBuilder)
		{
			ILGenerator ilgenerator = typeBuilder.DefineMethod("GetBasePropertyValue", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig, typeof(object), new Type[] { typeof(string) }).GetILGenerator();
			Label[] array = new Label[this._baseGetters.Count];
			for (int i = 0; i < this._baseGetters.Count; i++)
			{
				array[i] = ilgenerator.DefineLabel();
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(OpCodes.Ldstr, this._baseGetters[i].Name);
				ilgenerator.Emit(OpCodes.Call, BaseProxyImplementor.StringEquals);
				ilgenerator.Emit(OpCodes.Brfalse_S, array[i]);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Call, this._baseGetters[i].Getter());
				ilgenerator.Emit(OpCodes.Ret);
				ilgenerator.MarkLabel(array[i]);
			}
			ilgenerator.Emit(OpCodes.Newobj, BaseProxyImplementor._invalidOperationConstructor);
			ilgenerator.Emit(OpCodes.Throw);
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x000A8CB8 File Offset: 0x000A6EB8
		private void ImplementBaseSetter(TypeBuilder typeBuilder)
		{
			ILGenerator ilgenerator = typeBuilder.DefineMethod("SetBasePropertyValue", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig, typeof(void), new Type[]
			{
				typeof(string),
				typeof(object)
			}).GetILGenerator();
			Label[] array = new Label[this._baseSetters.Count];
			for (int i = 0; i < this._baseSetters.Count; i++)
			{
				array[i] = ilgenerator.DefineLabel();
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(OpCodes.Ldstr, this._baseSetters[i].Name);
				ilgenerator.Emit(OpCodes.Call, BaseProxyImplementor.StringEquals);
				ilgenerator.Emit(OpCodes.Brfalse_S, array[i]);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldarg_2);
				ilgenerator.Emit(OpCodes.Castclass, this._baseSetters[i].PropertyType);
				ilgenerator.Emit(OpCodes.Call, this._baseSetters[i].Setter());
				ilgenerator.Emit(OpCodes.Ret);
				ilgenerator.MarkLabel(array[i]);
			}
			ilgenerator.Emit(OpCodes.Newobj, BaseProxyImplementor._invalidOperationConstructor);
			ilgenerator.Emit(OpCodes.Throw);
		}

		// Token: 0x040010E6 RID: 4326
		private readonly List<PropertyInfo> _baseGetters;

		// Token: 0x040010E7 RID: 4327
		private readonly List<PropertyInfo> _baseSetters;

		// Token: 0x040010E8 RID: 4328
		internal static readonly MethodInfo StringEquals = typeof(string).GetDeclaredMethod("op_Equality", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x040010E9 RID: 4329
		private static readonly ConstructorInfo _invalidOperationConstructor = typeof(InvalidOperationException).GetDeclaredConstructor(new Type[0]);
	}
}
