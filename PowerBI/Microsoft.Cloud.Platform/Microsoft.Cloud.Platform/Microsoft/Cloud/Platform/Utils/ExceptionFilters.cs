using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001FF RID: 511
	public static class ExceptionFilters
	{
		// Token: 0x06000D86 RID: 3462 RVA: 0x0002F2C8 File Offset: 0x0002D4C8
		public static Exception TryFilterCatchFaultFinally(Action tryBlock, Func<Exception, ExceptionDisposition> filterBlock, Action<Exception> catchBlock, Action faultBlock, Action finallyBlock)
		{
			Exception ex = null;
			bool flag = true;
			try
			{
				ex = ExceptionFilters.TryFilterCatch(tryBlock, filterBlock, catchBlock);
				flag = false;
			}
			finally
			{
				if (flag && faultBlock != null)
				{
					faultBlock();
				}
				if (finallyBlock != null)
				{
					finallyBlock();
				}
			}
			return ex;
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0002F314 File Offset: 0x0002D514
		[CanBeNull]
		private static Exception NeuteredTryFilterCatch(Action tryBlock, Func<Exception, ExceptionDisposition> filterBlock, Action<Exception> catchBlock)
		{
			try
			{
				tryBlock();
			}
			catch (Exception ex)
			{
				if (filterBlock(ex) == ExceptionDisposition.ExecuteHandler)
				{
					catchBlock(ex);
					return ex;
				}
				throw;
			}
			return null;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0002F354 File Offset: 0x0002D554
		private static Func<Action, Func<Exception, ExceptionDisposition>, Action<Exception>, Exception> CreateTryFilterCatch()
		{
			if (ExceptionFilters.s_neuterTryFilterCatch)
			{
				return new Func<Action, Func<Exception, ExceptionDisposition>, Action<Exception>, Exception>(ExceptionFilters.NeuteredTryFilterCatch);
			}
			string text = null;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				text = Path.GetDirectoryName(currentProcess.MainModule.FileName);
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				string text2 = Path.Combine(text, "Microsoft.Cloud.Platform.Utils.Dynamic.ExceptionFilters.dll");
				if (File.Exists(text2))
				{
					foreach (Type type in Assembly.LoadFile(text2).GetExportedTypes())
					{
						if (type.FullName.Equals("Microsoft.Cloud.Platform.Utils.Dynamic.ExceptionFilters"))
						{
							MethodInfo method = type.GetMethod("TryFilterCatch");
							Func<Action, Func<Exception, ExceptionDisposition>, Action<Exception>, Exception> func = Delegate.CreateDelegate(typeof(Func<Action, Func<Exception, ExceptionDisposition>, Action<Exception>, Exception>), method) as Func<Action, Func<Exception, ExceptionDisposition>, Action<Exception>, Exception>;
							if (func != null)
							{
								return func;
							}
						}
					}
				}
			}
			AssemblyName assemblyName = new AssemblyName("Microsoft.Cloud.Platform.Utils.Dynamic.ExceptionFilters");
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, ExceptionFilters.s_writeDynamicCodeToDisk ? AssemblyBuilderAccess.RunAndSave : AssemblyBuilderAccess.Run);
			ModuleBuilder moduleBuilder = (ExceptionFilters.s_writeDynamicCodeToDisk ? assemblyBuilder.DefineDynamicModule("Code", "Microsoft.Cloud.Platform.Utils.Dynamic.ExceptionFilters.dll", true) : assemblyBuilder.DefineDynamicModule("Code"));
			ExceptionFilters.SetCustomAttributeWithProperty(assemblyBuilder, typeof(RuntimeCompatibilityAttribute), "WrapNonExceptionThrows", true);
			ExceptionFilters.SetCustomAttributeWithCtor1(assemblyBuilder, typeof(DebuggableAttribute), typeof(DebuggableAttribute.DebuggingModes), DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints);
			TypeBuilder typeBuilder = moduleBuilder.DefineType("Microsoft.Cloud.Platform.Utils.Dynamic.ExceptionFilters", TypeAttributes.Public);
			ExceptionFilters.GenerateIlForTryFilterCatch(ExceptionFilters.CreateMethodBuilderByMethodInfo(typeBuilder, "TryFilterCatch", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, typeof(Func<Action, Func<Exception, ExceptionDisposition>, Action<Exception>, Exception>).GetMethod("Invoke")).GetILGenerator());
			MethodInfo method2 = typeBuilder.CreateType().GetMethod("TryFilterCatch");
			Delegate @delegate = Delegate.CreateDelegate(typeof(Func<Action, Func<Exception, ExceptionDisposition>, Action<Exception>, Exception>), method2);
			if (ExceptionFilters.s_writeDynamicCodeToDisk)
			{
				assemblyBuilder.Save("Microsoft.Cloud.Platform.Utils.Dynamic.ExceptionFilters.dll");
			}
			return (Func<Action, Func<Exception, ExceptionDisposition>, Action<Exception>, Exception>)@delegate;
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0002F524 File Offset: 0x0002D724
		private static void GenerateIlForTryFilterCatch(ILGenerator il)
		{
			LocalBuilder localBuilder = il.DeclareLocal(typeof(Exception));
			Label label = il.DefineLabel();
			Label label2 = il.DefineLabel();
			Label label3 = il.DefineLabel();
			Label label4 = il.DefineLabel();
			il.BeginExceptionBlock();
			ExceptionFilters.EmitDebug(il, "TryBlock");
			ExceptionFilters.EmitCallvirt(il, "TryBlock", typeof(Action).GetMethod("Invoke"), OpCodes.Ldarg_0);
			il.BeginExceptFilterBlock();
			ExceptionFilters.EmitDebug(il, "FilterBlock");
			Ensure.IsTrue(localBuilder.LocalIndex == 0, "The first local in TryFilterCatch must be the exception");
			il.Emit(OpCodes.Castclass, typeof(Exception));
			il.Emit(OpCodes.Stloc_0);
			il.Emit(OpCodes.Ldarg_1);
			il.Emit(OpCodes.Brfalse_S, label);
			ExceptionFilters.EmitDebug(il, "FilterBlock invokes filterBlock(locEx)");
			il.Emit(OpCodes.Ldarg_1);
			il.Emit(OpCodes.Ldloc_0);
			il.EmitCall(OpCodes.Callvirt, typeof(Func<Exception, ExceptionDisposition>).GetMethod("Invoke"), null);
			il.Emit(OpCodes.Br_S, label2);
			il.MarkLabel(label);
			il.Emit(OpCodes.Ldarg_2);
			il.Emit(OpCodes.Brfalse_S, label3);
			ExceptionFilters.EmitDebug(il, "FilterBlock returns true (ExecuteHandler), because filterBlock is null but catchBlock is not");
			il.Emit(OpCodes.Ldc_I4_1);
			il.Emit(OpCodes.Br_S, label2);
			il.MarkLabel(label3);
			ExceptionFilters.EmitDebug(il, "FilterBlock returns false (ContinueSearch), because filterBlock is null and catchBlock is also null");
			il.Emit(OpCodes.Ldc_I4_0);
			il.MarkLabel(label2);
			il.Emit(OpCodes.Nop);
			il.BeginCatchBlock(null);
			ExceptionFilters.EmitDebug(il, "CatchBlock");
			Ensure.IsTrue(localBuilder.LocalIndex == 0, "The first local in TryFilterCatch must be the exception");
			il.Emit(OpCodes.Castclass, typeof(Exception));
			il.Emit(OpCodes.Stloc_0);
			il.Emit(OpCodes.Ldarg_2);
			il.Emit(OpCodes.Brtrue_S, label4);
			il.Emit(OpCodes.Ldloc_0);
			il.Emit(OpCodes.Rethrow);
			il.MarkLabel(label4);
			il.Emit(OpCodes.Ldarg_2);
			il.Emit(OpCodes.Ldloc_0);
			il.EmitCall(OpCodes.Callvirt, typeof(Action<Exception>).GetMethod("Invoke"), null);
			il.EndExceptionBlock();
			il.Emit(OpCodes.Ldloc_0);
			il.Emit(OpCodes.Ret);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0002F774 File Offset: 0x0002D974
		private static MethodBuilder CreateMethodBuilderByMethodInfo(TypeBuilder typeBuilder, string methodName, MethodAttributes methodAttributes, MethodInfo methodInfo)
		{
			Type[] array = (from parameter in methodInfo.GetParameters()
				select parameter.ParameterType).ToArray<Type>();
			Type returnType = methodInfo.ReturnType;
			return typeBuilder.DefineMethod(methodName, methodAttributes, returnType, array);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0002F7C2 File Offset: 0x0002D9C2
		private static void EmitCallvirt(ILGenerator il, string banner, MethodInfo method, OpCode load)
		{
			ExceptionFilters.EmitDebug(il, banner);
			il.Emit(load);
			il.EmitCall(OpCodes.Callvirt, method, null);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0002F7DF File Offset: 0x0002D9DF
		private static void EmitDebug(ILGenerator il, string text)
		{
			if (ExceptionFilters.s_emitDebugCalls)
			{
				il.Emit(OpCodes.Ldstr, text);
				il.EmitCall(OpCodes.Call, ExceptionFilters.s_dumpMethod, null);
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0002F805 File Offset: 0x0002DA05
		public static void Dump(string text)
		{
			Console.WriteLine("TryFilterCatch: {0}", text);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0002F814 File Offset: 0x0002DA14
		private static void SetCustomAttributeWithProperty(AssemblyBuilder assemblyBuilder, Type attributeType, string propertyName, object propertyValue)
		{
			CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(attributeType.GetConstructor(new Type[0]), new object[0], new PropertyInfo[] { attributeType.GetProperty(propertyName) }, new object[] { propertyValue });
			assemblyBuilder.SetCustomAttribute(customAttributeBuilder);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0002F85C File Offset: 0x0002DA5C
		private static void SetCustomAttributeWithCtor1(AssemblyBuilder assemblyBuilder, Type attributeType, Type arg1Type, object arg1Value)
		{
			CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(attributeType.GetConstructor(new Type[] { arg1Type }), new object[] { arg1Value });
			assemblyBuilder.SetCustomAttribute(customAttributeBuilder);
		}

		// Token: 0x04000550 RID: 1360
		private const string c_dynamicCodeAssemblyName = "Microsoft.Cloud.Platform.Utils.Dynamic.ExceptionFilters";

		// Token: 0x04000551 RID: 1361
		private const string c_dynamicCodeAssemblyFileName = "Microsoft.Cloud.Platform.Utils.Dynamic.ExceptionFilters.dll";

		// Token: 0x04000552 RID: 1362
		private const string c_dynamicCodeModuleName = "Code";

		// Token: 0x04000553 RID: 1363
		private static bool s_neuterTryFilterCatch = !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TWEAKS_TOP_LEVEL_HANDLER_WITHOUT_EXCEPTION_FILTERS"));

		// Token: 0x04000554 RID: 1364
		private static bool s_writeDynamicCodeToDisk = !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TWEAKS_DEBUG_EXCEPTION_FILTERS"));

		// Token: 0x04000555 RID: 1365
		private static bool s_emitDebugCalls = false;

		// Token: 0x04000556 RID: 1366
		private static MethodInfo s_dumpMethod = typeof(ExceptionFilters).GetMethod("Dump", BindingFlags.Static | BindingFlags.Public);

		// Token: 0x04000557 RID: 1367
		public static readonly Func<Action, Func<Exception, ExceptionDisposition>, Action<Exception>, Exception> TryFilterCatch = ExceptionFilters.CreateTryFilterCatch();
	}
}
