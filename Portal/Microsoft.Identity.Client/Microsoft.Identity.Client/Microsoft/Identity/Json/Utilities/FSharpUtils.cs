using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Json.Serialization;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x0200005B RID: 91
	internal class FSharpUtils
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x000160A8 File Offset: 0x000142A8
		private FSharpUtils(Assembly fsharpCoreAssembly)
		{
			this.FSharpCoreAssembly = fsharpCoreAssembly;
			Type type = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.FSharpType");
			MethodInfo methodWithNonPublicFallback = FSharpUtils.GetMethodWithNonPublicFallback(type, "IsUnion", BindingFlags.Static | BindingFlags.Public);
			this.IsUnion = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
			MethodInfo methodWithNonPublicFallback2 = FSharpUtils.GetMethodWithNonPublicFallback(type, "GetUnionCases", BindingFlags.Static | BindingFlags.Public);
			this.GetUnionCases = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback2);
			Type type2 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.FSharpValue");
			this.PreComputeUnionTagReader = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionTagReader");
			this.PreComputeUnionReader = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionReader");
			this.PreComputeUnionConstructor = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionConstructor");
			Type type3 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.UnionCaseInfo");
			this.GetUnionCaseInfoName = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("Name"));
			this.GetUnionCaseInfoTag = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("Tag"));
			this.GetUnionCaseInfoDeclaringType = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("DeclaringType"));
			this.GetUnionCaseInfoFields = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(type3.GetMethod("GetFields"));
			Type type4 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.ListModule");
			this._ofSeq = type4.GetMethod("OfSeq");
			this._mapType = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.FSharpMap`2");
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x000161F1 File Offset: 0x000143F1
		public static FSharpUtils Instance
		{
			get
			{
				return FSharpUtils._instance;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x000161F8 File Offset: 0x000143F8
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x00016200 File Offset: 0x00014400
		public Assembly FSharpCoreAssembly { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00016209 File Offset: 0x00014409
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x00016211 File Offset: 0x00014411
		[Nullable(new byte[] { 0, 2, 0 })]
		public MethodCall<object, object> IsUnion
		{
			[return: Nullable(new byte[] { 0, 2, 0 })]
			get;
			[param: Nullable(new byte[] { 0, 2, 0 })]
			private set;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001621A File Offset: 0x0001441A
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x00016222 File Offset: 0x00014422
		[Nullable(new byte[] { 0, 2, 0 })]
		public MethodCall<object, object> GetUnionCases
		{
			[return: Nullable(new byte[] { 0, 2, 0 })]
			get;
			[param: Nullable(new byte[] { 0, 2, 0 })]
			private set;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0001622B File Offset: 0x0001442B
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x00016233 File Offset: 0x00014433
		[Nullable(new byte[] { 0, 2, 0 })]
		public MethodCall<object, object> PreComputeUnionTagReader
		{
			[return: Nullable(new byte[] { 0, 2, 0 })]
			get;
			[param: Nullable(new byte[] { 0, 2, 0 })]
			private set;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0001623C File Offset: 0x0001443C
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x00016244 File Offset: 0x00014444
		[Nullable(new byte[] { 0, 2, 0 })]
		public MethodCall<object, object> PreComputeUnionReader
		{
			[return: Nullable(new byte[] { 0, 2, 0 })]
			get;
			[param: Nullable(new byte[] { 0, 2, 0 })]
			private set;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001624D File Offset: 0x0001444D
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x00016255 File Offset: 0x00014455
		[Nullable(new byte[] { 0, 2, 0 })]
		public MethodCall<object, object> PreComputeUnionConstructor
		{
			[return: Nullable(new byte[] { 0, 2, 0 })]
			get;
			[param: Nullable(new byte[] { 0, 2, 0 })]
			private set;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001625E File Offset: 0x0001445E
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x00016266 File Offset: 0x00014466
		public Func<object, object> GetUnionCaseInfoDeclaringType { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001626F File Offset: 0x0001446F
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x00016277 File Offset: 0x00014477
		public Func<object, object> GetUnionCaseInfoName { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00016280 File Offset: 0x00014480
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x00016288 File Offset: 0x00014488
		public Func<object, object> GetUnionCaseInfoTag { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00016291 File Offset: 0x00014491
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x00016299 File Offset: 0x00014499
		[Nullable(new byte[] { 0, 0, 2 })]
		public MethodCall<object, object> GetUnionCaseInfoFields
		{
			[return: Nullable(new byte[] { 0, 0, 2 })]
			get;
			[param: Nullable(new byte[] { 0, 0, 2 })]
			private set;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000162A4 File Offset: 0x000144A4
		public static void EnsureInitialized(Assembly fsharpCoreAssembly)
		{
			if (FSharpUtils._instance == null)
			{
				object @lock = FSharpUtils.Lock;
				lock (@lock)
				{
					if (FSharpUtils._instance == null)
					{
						FSharpUtils._instance = new FSharpUtils(fsharpCoreAssembly);
					}
				}
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000162F8 File Offset: 0x000144F8
		private static MethodInfo GetMethodWithNonPublicFallback(Type type, string methodName, BindingFlags bindingFlags)
		{
			MethodInfo methodInfo = type.GetMethod(methodName, bindingFlags);
			if (methodInfo == null && (bindingFlags & BindingFlags.NonPublic) != BindingFlags.NonPublic)
			{
				methodInfo = type.GetMethod(methodName, bindingFlags | BindingFlags.NonPublic);
			}
			return methodInfo;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001632C File Offset: 0x0001452C
		[return: Nullable(new byte[] { 0, 2, 0 })]
		private static MethodCall<object, object> CreateFSharpFuncCall(Type type, string methodName)
		{
			MethodInfo methodWithNonPublicFallback = FSharpUtils.GetMethodWithNonPublicFallback(type, methodName, BindingFlags.Static | BindingFlags.Public);
			MethodInfo method = methodWithNonPublicFallback.ReturnType.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
			MethodCall<object, object> call = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
			MethodCall<object, object> invoke = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return ([Nullable(2)] object target, [Nullable(new byte[] { 0, 2 })] object[] args) => new FSharpFunction(call(target, args), invoke);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00016388 File Offset: 0x00014588
		public ObjectConstructor<object> CreateSeq(Type t)
		{
			MethodInfo methodInfo = this._ofSeq.MakeGenericMethod(new Type[] { t });
			return JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(methodInfo);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000163B6 File Offset: 0x000145B6
		public ObjectConstructor<object> CreateMap(Type keyType, Type valueType)
		{
			return (ObjectConstructor<object>)typeof(FSharpUtils).GetMethod("BuildMapCreator").MakeGenericMethod(new Type[] { keyType, valueType }).Invoke(this, null);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000163EC File Offset: 0x000145EC
		public ObjectConstructor<object> BuildMapCreator<TKey, TValue>()
		{
			ConstructorInfo constructor = this._mapType.MakeGenericType(new Type[]
			{
				typeof(TKey),
				typeof(TValue)
			}).GetConstructor(new Type[] { typeof(IEnumerable<Tuple<TKey, TValue>>) });
			ObjectConstructor<object> ctorDelegate = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			return delegate([Nullable(new byte[] { 0, 2 })] object[] args)
			{
				IEnumerable<Tuple<TKey, TValue>> enumerable = ((IEnumerable<KeyValuePair<TKey, TValue>>)args[0]).Select((KeyValuePair<TKey, TValue> kv) => new Tuple<TKey, TValue>(kv.Key, kv.Value));
				return ctorDelegate(new object[] { enumerable });
			};
		}

		// Token: 0x040001CE RID: 462
		private static readonly object Lock = new object();

		// Token: 0x040001CF RID: 463
		[Nullable(2)]
		private static FSharpUtils _instance;

		// Token: 0x040001D0 RID: 464
		private MethodInfo _ofSeq;

		// Token: 0x040001D1 RID: 465
		private Type _mapType;

		// Token: 0x040001DC RID: 476
		public const string FSharpSetTypeName = "FSharpSet`1";

		// Token: 0x040001DD RID: 477
		public const string FSharpListTypeName = "FSharpList`1";

		// Token: 0x040001DE RID: 478
		public const string FSharpMapTypeName = "FSharpMap`2";
	}
}
