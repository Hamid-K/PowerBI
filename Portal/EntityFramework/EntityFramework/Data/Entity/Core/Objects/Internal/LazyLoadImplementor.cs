using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200044B RID: 1099
	internal class LazyLoadImplementor
	{
		// Token: 0x0600357C RID: 13692 RVA: 0x000AC6C7 File Offset: 0x000AA8C7
		public LazyLoadImplementor(EntityType ospaceEntityType)
		{
			this.CheckType(ospaceEntityType);
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x0600357D RID: 13693 RVA: 0x000AC6D6 File Offset: 0x000AA8D6
		public IEnumerable<EdmMember> Members
		{
			get
			{
				return this._members;
			}
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x000AC6E0 File Offset: 0x000AA8E0
		private void CheckType(EntityType ospaceEntityType)
		{
			this._members = new HashSet<EdmMember>();
			foreach (EdmMember edmMember in ospaceEntityType.Members)
			{
				PropertyInfo topProperty = ospaceEntityType.ClrType.GetTopProperty(edmMember.Name);
				if (topProperty != null && EntityProxyFactory.CanProxyGetter(topProperty) && LazyLoadBehavior.IsLazyLoadCandidate(ospaceEntityType, edmMember))
				{
					this._members.Add(edmMember);
				}
			}
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000AC770 File Offset: 0x000AA970
		public bool CanProxyMember(EdmMember member)
		{
			return this._members.Contains(member);
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x000AC780 File Offset: 0x000AA980
		public virtual void Implement(TypeBuilder typeBuilder, Action<FieldBuilder, bool> registerField)
		{
			FieldBuilder fieldBuilder = typeBuilder.DefineField("_entityWrapper", typeof(object), FieldAttributes.Public);
			registerField(fieldBuilder, false);
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000AC7AC File Offset: 0x000AA9AC
		public bool EmitMember(TypeBuilder typeBuilder, EdmMember member, PropertyBuilder propertyBuilder, PropertyInfo baseProperty, BaseProxyImplementor baseImplementor)
		{
			if (this._members.Contains(member))
			{
				MethodInfo methodInfo = baseProperty.Getter();
				MethodAttributes methodAttributes = methodInfo.Attributes & MethodAttributes.MemberAccessMask;
				Type type = typeof(Func<, , >).MakeGenericType(new Type[]
				{
					typeBuilder,
					baseProperty.PropertyType,
					typeof(bool)
				});
				MethodInfo method = TypeBuilder.GetMethod(type, typeof(Func<, , >).GetOnlyDeclaredMethod("Invoke"));
				FieldBuilder fieldBuilder = typeBuilder.DefineField(LazyLoadImplementor.GetInterceptorFieldName(baseProperty.Name), type, FieldAttributes.Private | FieldAttributes.Static);
				MethodBuilder methodBuilder = typeBuilder.DefineMethod("get_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), baseProperty.PropertyType, Type.EmptyTypes);
				ILGenerator ilgenerator = methodBuilder.GetILGenerator();
				Label label = ilgenerator.DefineLabel();
				ilgenerator.DeclareLocal(baseProperty.PropertyType);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Call, methodInfo);
				ilgenerator.Emit(OpCodes.Stloc_0);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, fieldBuilder);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldloc_0);
				ilgenerator.Emit(OpCodes.Callvirt, method);
				ilgenerator.Emit(OpCodes.Brtrue_S, label);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Call, methodInfo);
				ilgenerator.Emit(OpCodes.Ret);
				ilgenerator.MarkLabel(label);
				ilgenerator.Emit(OpCodes.Ldloc_0);
				ilgenerator.Emit(OpCodes.Ret);
				propertyBuilder.SetGetMethod(methodBuilder);
				baseImplementor.AddBasePropertyGetter(baseProperty);
				return true;
			}
			return false;
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000AC943 File Offset: 0x000AAB43
		internal static string GetInterceptorFieldName(string memberName)
		{
			return "ef_proxy_interceptorFor" + memberName;
		}

		// Token: 0x04001155 RID: 4437
		private HashSet<EdmMember> _members;
	}
}
