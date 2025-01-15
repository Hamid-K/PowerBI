using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002D4 RID: 724
	public static class TrampolineCreator
	{
		// Token: 0x06001366 RID: 4966 RVA: 0x000434D0 File Offset: 0x000416D0
		public static Type CreateConstructorTrampoline(string assembly, string type, Type baseType, object context)
		{
			AssemblyName assemblyName = new AssemblyName(assembly);
			string text = Path.ChangeExtension(assembly, ".dll");
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, TrampolineCreator.s_writeDynamicCodeToDisk ? AssemblyBuilderAccess.RunAndSave : AssemblyBuilderAccess.Run);
			ModuleBuilder moduleBuilder = (TrampolineCreator.s_writeDynamicCodeToDisk ? assemblyBuilder.DefineDynamicModule("DynamicModule", text) : assemblyBuilder.DefineDynamicModule("DynamicModule"));
			TrampolineCreator.SetCustomAttributeWithProperty(assemblyBuilder, typeof(RuntimeCompatibilityAttribute), "WrapNonExceptionThrows", true);
			TrampolineCreator.SetCustomAttributeWithCtor1(assemblyBuilder, typeof(DebuggableAttribute), typeof(DebuggableAttribute.DebuggingModes), DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints);
			TypeBuilder typeBuilder = moduleBuilder.DefineType(type, TypeAttributes.Public, baseType);
			FieldBuilder fieldBuilder = typeBuilder.DefineField("s_context", typeof(object), FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.Static);
			TrampolineCreator.CreateSetContextMethod(typeBuilder, fieldBuilder);
			TrampolineCreator.CreateDefaultConstructorMethod(typeBuilder, fieldBuilder, baseType);
			Type type2 = typeBuilder.CreateType();
			type2.GetMethod("SetContext").Invoke(null, new object[] { context });
			if (TrampolineCreator.s_writeDynamicCodeToDisk)
			{
				assemblyBuilder.Save(text);
			}
			return type2;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x000435C8 File Offset: 0x000417C8
		private static void CreateSetContextMethod(TypeBuilder typeBuilder, FieldInfo contextField)
		{
			ILGenerator ilgenerator = typeBuilder.DefineMethod("SetContext", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, null, new Type[] { typeof(object) }).GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Stsfld, contextField);
			ilgenerator.Emit(OpCodes.Ret);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0004361C File Offset: 0x0004181C
		private static void CreateDefaultConstructorMethod(TypeBuilder typeBuilder, FieldInfo contextField, Type baseType)
		{
			ILGenerator ilgenerator = typeBuilder.DefineMethod(".ctor", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, null, new Type[0]).GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldsfld, contextField);
			ConstructorInfo constructor = baseType.GetConstructor(new Type[] { typeof(object) });
			ilgenerator.Emit(OpCodes.Call, constructor);
			ilgenerator.Emit(OpCodes.Ret);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00043690 File Offset: 0x00041890
		private static void SetCustomAttributeWithProperty(AssemblyBuilder assemblyBuilder, Type attributeType, string propertyName, object propertyValue)
		{
			CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(attributeType.GetConstructor(new Type[0]), new object[0], new PropertyInfo[] { attributeType.GetProperty(propertyName) }, new object[] { propertyValue });
			assemblyBuilder.SetCustomAttribute(customAttributeBuilder);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x000436D8 File Offset: 0x000418D8
		private static void SetCustomAttributeWithCtor1(AssemblyBuilder assemblyBuilder, Type attributeType, Type arg1Type, object arg1Value)
		{
			CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(attributeType.GetConstructor(new Type[] { arg1Type }), new object[] { arg1Value });
			assemblyBuilder.SetCustomAttribute(customAttributeBuilder);
		}

		// Token: 0x0400074C RID: 1868
		private static bool s_writeDynamicCodeToDisk;
	}
}
