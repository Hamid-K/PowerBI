using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200005B RID: 91
	[NullableContext(1)]
	[Nullable(0)]
	internal class FSharpUtils
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x000165E8 File Offset: 0x000147E8
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
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00016731 File Offset: 0x00014931
		public static FSharpUtils Instance
		{
			get
			{
				return FSharpUtils._instance;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00016738 File Offset: 0x00014938
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00016740 File Offset: 0x00014940
		public Assembly FSharpCoreAssembly { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00016749 File Offset: 0x00014949
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x00016751 File Offset: 0x00014951
		[Nullable(new byte[] { 1, 2, 1 })]
		public MethodCall<object, object> IsUnion
		{
			[return: Nullable(new byte[] { 1, 2, 1 })]
			get;
			[param: Nullable(new byte[] { 1, 2, 1 })]
			private set;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0001675A File Offset: 0x0001495A
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00016762 File Offset: 0x00014962
		[Nullable(new byte[] { 1, 2, 1 })]
		public MethodCall<object, object> GetUnionCases
		{
			[return: Nullable(new byte[] { 1, 2, 1 })]
			get;
			[param: Nullable(new byte[] { 1, 2, 1 })]
			private set;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0001676B File Offset: 0x0001496B
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x00016773 File Offset: 0x00014973
		[Nullable(new byte[] { 1, 2, 1 })]
		public MethodCall<object, object> PreComputeUnionTagReader
		{
			[return: Nullable(new byte[] { 1, 2, 1 })]
			get;
			[param: Nullable(new byte[] { 1, 2, 1 })]
			private set;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0001677C File Offset: 0x0001497C
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00016784 File Offset: 0x00014984
		[Nullable(new byte[] { 1, 2, 1 })]
		public MethodCall<object, object> PreComputeUnionReader
		{
			[return: Nullable(new byte[] { 1, 2, 1 })]
			get;
			[param: Nullable(new byte[] { 1, 2, 1 })]
			private set;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001678D File Offset: 0x0001498D
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x00016795 File Offset: 0x00014995
		[Nullable(new byte[] { 1, 2, 1 })]
		public MethodCall<object, object> PreComputeUnionConstructor
		{
			[return: Nullable(new byte[] { 1, 2, 1 })]
			get;
			[param: Nullable(new byte[] { 1, 2, 1 })]
			private set;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0001679E File Offset: 0x0001499E
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x000167A6 File Offset: 0x000149A6
		public Func<object, object> GetUnionCaseInfoDeclaringType { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x000167AF File Offset: 0x000149AF
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x000167B7 File Offset: 0x000149B7
		public Func<object, object> GetUnionCaseInfoName { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x000167C0 File Offset: 0x000149C0
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x000167C8 File Offset: 0x000149C8
		public Func<object, object> GetUnionCaseInfoTag { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x000167D1 File Offset: 0x000149D1
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x000167D9 File Offset: 0x000149D9
		[Nullable(new byte[] { 1, 1, 2 })]
		public MethodCall<object, object> GetUnionCaseInfoFields
		{
			[return: Nullable(new byte[] { 1, 1, 2 })]
			get;
			[param: Nullable(new byte[] { 1, 1, 2 })]
			private set;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000167E4 File Offset: 0x000149E4
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

		// Token: 0x06000542 RID: 1346 RVA: 0x00016838 File Offset: 0x00014A38
		private static MethodInfo GetMethodWithNonPublicFallback(Type type, string methodName, BindingFlags bindingFlags)
		{
			MethodInfo methodInfo = type.GetMethod(methodName, bindingFlags);
			if (methodInfo == null && (bindingFlags & BindingFlags.NonPublic) != BindingFlags.NonPublic)
			{
				methodInfo = type.GetMethod(methodName, bindingFlags | BindingFlags.NonPublic);
			}
			return methodInfo;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001686C File Offset: 0x00014A6C
		[return: Nullable(new byte[] { 1, 2, 1 })]
		private static MethodCall<object, object> CreateFSharpFuncCall(Type type, string methodName)
		{
			MethodInfo methodWithNonPublicFallback = FSharpUtils.GetMethodWithNonPublicFallback(type, methodName, BindingFlags.Static | BindingFlags.Public);
			MethodInfo method = methodWithNonPublicFallback.ReturnType.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
			MethodCall<object, object> call = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
			MethodCall<object, object> invoke = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return ([Nullable(2)] object target, [Nullable(new byte[] { 1, 2 })] object[] args) => new FSharpFunction(call(target, args), invoke);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000168C8 File Offset: 0x00014AC8
		public ObjectConstructor<object> CreateSeq(Type t)
		{
			MethodInfo methodInfo = this._ofSeq.MakeGenericMethod(new Type[] { t });
			return JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(methodInfo);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000168F6 File Offset: 0x00014AF6
		public ObjectConstructor<object> CreateMap(Type keyType, Type valueType)
		{
			return (ObjectConstructor<object>)typeof(FSharpUtils).GetMethod("BuildMapCreator").MakeGenericMethod(new Type[] { keyType, valueType }).Invoke(this, null);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001692C File Offset: 0x00014B2C
		[NullableContext(2)]
		[return: Nullable(1)]
		public ObjectConstructor<object> BuildMapCreator<TKey, TValue>()
		{
			ConstructorInfo constructor = this._mapType.MakeGenericType(new Type[]
			{
				typeof(TKey),
				typeof(TValue)
			}).GetConstructor(new Type[] { typeof(IEnumerable<Tuple<TKey, TValue>>) });
			ObjectConstructor<object> ctorDelegate = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			return delegate([Nullable(new byte[] { 1, 2 })] object[] args)
			{
				IEnumerable<Tuple<TKey, TValue>> enumerable = ((IEnumerable<KeyValuePair<TKey, TValue>>)args[0]).Select((KeyValuePair<TKey, TValue> kv) => new Tuple<TKey, TValue>(kv.Key, kv.Value));
				return ctorDelegate(new object[] { enumerable });
			};
		}

		// Token: 0x040001E8 RID: 488
		private static readonly object Lock = new object();

		// Token: 0x040001E9 RID: 489
		[Nullable(2)]
		private static FSharpUtils _instance;

		// Token: 0x040001EA RID: 490
		private MethodInfo _ofSeq;

		// Token: 0x040001EB RID: 491
		private Type _mapType;

		// Token: 0x040001F6 RID: 502
		public const string FSharpSetTypeName = "FSharpSet`1";

		// Token: 0x040001F7 RID: 503
		public const string FSharpListTypeName = "FSharpList`1";

		// Token: 0x040001F8 RID: 504
		public const string FSharpMapTypeName = "FSharpMap`2";
	}
}
