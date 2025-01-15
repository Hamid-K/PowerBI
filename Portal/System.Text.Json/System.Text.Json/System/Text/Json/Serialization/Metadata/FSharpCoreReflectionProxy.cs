using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000A6 RID: 166
	internal sealed class FSharpCoreReflectionProxy
	{
		// Token: 0x06000977 RID: 2423 RVA: 0x00028C00 File Offset: 0x00026E00
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
		public static bool IsFSharpType(Type type)
		{
			if (FSharpCoreReflectionProxy.s_singletonInstance != null)
			{
				return FSharpCoreReflectionProxy.s_singletonInstance.GetFSharpCompilationMappingAttribute(type) != null;
			}
			Assembly fsharpCoreAssembly = FSharpCoreReflectionProxy.GetFSharpCoreAssembly(type);
			if (fsharpCoreAssembly != null)
			{
				if (FSharpCoreReflectionProxy.s_singletonInstance == null)
				{
					FSharpCoreReflectionProxy.s_singletonInstance = new FSharpCoreReflectionProxy(fsharpCoreAssembly);
				}
				return true;
			}
			return false;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00028C45 File Offset: 0x00026E45
		public static FSharpCoreReflectionProxy Instance
		{
			get
			{
				return FSharpCoreReflectionProxy.s_singletonInstance;
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00028C4C File Offset: 0x00026E4C
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
		private FSharpCoreReflectionProxy(Assembly fsharpCoreAssembly)
		{
			Type type = fsharpCoreAssembly.GetType("Microsoft.FSharp.Core.CompilationMappingAttribute");
			this._sourceConstructFlagsGetter = type.GetMethod("get_SourceConstructFlags", BindingFlags.Instance | BindingFlags.Public);
			this._compilationMappingAttributeType = type;
			this._fsharpOptionType = fsharpCoreAssembly.GetType("Microsoft.FSharp.Core.FSharpOption`1");
			this._fsharpValueOptionType = fsharpCoreAssembly.GetType("Microsoft.FSharp.Core.FSharpValueOption`1");
			this._fsharpListType = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.FSharpList`1");
			this._fsharpSetType = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.FSharpSet`1");
			this._fsharpMapType = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.FSharpMap`2");
			Type type2 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.ListModule");
			this._fsharpListCtor = ((type2 != null) ? type2.GetMethod("OfSeq", BindingFlags.Static | BindingFlags.Public) : null);
			Type type3 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.SetModule");
			this._fsharpSetCtor = ((type3 != null) ? type3.GetMethod("OfSeq", BindingFlags.Static | BindingFlags.Public) : null);
			Type type4 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.MapModule");
			this._fsharpMapCtor = ((type4 != null) ? type4.GetMethod("OfSeq", BindingFlags.Static | BindingFlags.Public) : null);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00028D48 File Offset: 0x00026F48
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
		public FSharpCoreReflectionProxy.FSharpKind DetectFSharpKind(Type type)
		{
			Attribute fsharpCompilationMappingAttribute = this.GetFSharpCompilationMappingAttribute(type);
			if (fsharpCompilationMappingAttribute == null)
			{
				return FSharpCoreReflectionProxy.FSharpKind.Unrecognized;
			}
			if (type.IsGenericType)
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				if (genericTypeDefinition == this._fsharpOptionType)
				{
					return FSharpCoreReflectionProxy.FSharpKind.Option;
				}
				if (genericTypeDefinition == this._fsharpValueOptionType)
				{
					return FSharpCoreReflectionProxy.FSharpKind.ValueOption;
				}
				if (genericTypeDefinition == this._fsharpListType)
				{
					return FSharpCoreReflectionProxy.FSharpKind.List;
				}
				if (genericTypeDefinition == this._fsharpSetType)
				{
					return FSharpCoreReflectionProxy.FSharpKind.Set;
				}
				if (genericTypeDefinition == this._fsharpMapType)
				{
					return FSharpCoreReflectionProxy.FSharpKind.Map;
				}
			}
			FSharpCoreReflectionProxy.SourceConstructFlags sourceConstructFlags = this.GetSourceConstructFlags(fsharpCompilationMappingAttribute) & FSharpCoreReflectionProxy.SourceConstructFlags.KindMask;
			FSharpCoreReflectionProxy.FSharpKind fsharpKind;
			if (sourceConstructFlags != FSharpCoreReflectionProxy.SourceConstructFlags.SumType)
			{
				if (sourceConstructFlags == FSharpCoreReflectionProxy.SourceConstructFlags.RecordType)
				{
					fsharpKind = FSharpCoreReflectionProxy.FSharpKind.Record;
				}
				else
				{
					fsharpKind = FSharpCoreReflectionProxy.FSharpKind.Unrecognized;
				}
			}
			else
			{
				fsharpKind = FSharpCoreReflectionProxy.FSharpKind.Union;
			}
			return fsharpKind;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00028DE0 File Offset: 0x00026FE0
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
		public Func<TFSharpOption, T> CreateFSharpOptionValueGetter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] TFSharpOption, T>()
		{
			MethodInfo methodInfo = FSharpCoreReflectionProxy.EnsureMemberExists<MethodInfo>(typeof(TFSharpOption).GetMethod("get_Value", BindingFlags.Instance | BindingFlags.Public), "Microsoft.FSharp.Core.FSharpOption<T>.get_Value()");
			return FSharpCoreReflectionProxy.CreateDelegate<Func<TFSharpOption, T>>(methodInfo);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00028E14 File Offset: 0x00027014
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
		public Func<TElement, TFSharpOption> CreateFSharpOptionSomeConstructor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] TFSharpOption, TElement>()
		{
			MethodInfo methodInfo = FSharpCoreReflectionProxy.EnsureMemberExists<MethodInfo>(typeof(TFSharpOption).GetMethod("Some", BindingFlags.Static | BindingFlags.Public), "Microsoft.FSharp.Core.FSharpOption<T>.Some(T value)");
			return FSharpCoreReflectionProxy.CreateDelegate<Func<TElement, TFSharpOption>>(methodInfo);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00028E48 File Offset: 0x00027048
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
		public FSharpCoreReflectionProxy.StructGetter<TFSharpValueOption, TElement> CreateFSharpValueOptionValueGetter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] TFSharpValueOption, TElement>() where TFSharpValueOption : struct
		{
			MethodInfo methodInfo = FSharpCoreReflectionProxy.EnsureMemberExists<MethodInfo>(typeof(TFSharpValueOption).GetMethod("get_Value", BindingFlags.Instance | BindingFlags.Public), "Microsoft.FSharp.Core.FSharpValueOption<T>.get_Value()");
			return FSharpCoreReflectionProxy.CreateDelegate<FSharpCoreReflectionProxy.StructGetter<TFSharpValueOption, TElement>>(methodInfo);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00028E7C File Offset: 0x0002707C
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
		public Func<TElement, TFSharpOption> CreateFSharpValueOptionSomeConstructor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] TFSharpOption, TElement>()
		{
			MethodInfo methodInfo = FSharpCoreReflectionProxy.EnsureMemberExists<MethodInfo>(typeof(TFSharpOption).GetMethod("Some", BindingFlags.Static | BindingFlags.Public), "Microsoft.FSharp.Core.FSharpValueOption<T>.ValueSome(T value)");
			return FSharpCoreReflectionProxy.CreateDelegate<Func<TElement, TFSharpOption>>(methodInfo);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00028EB0 File Offset: 0x000270B0
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
		public Func<IEnumerable<TElement>, TFSharpList> CreateFSharpListConstructor<TFSharpList, TElement>()
		{
			return FSharpCoreReflectionProxy.CreateDelegate<Func<IEnumerable<TElement>, TFSharpList>>(FSharpCoreReflectionProxy.EnsureMemberExists<MethodInfo>(this._fsharpListCtor, "Microsoft.FSharp.Collections.ListModule.OfSeq<T>(IEnumerable<T> source)").MakeGenericMethod(new Type[] { typeof(TElement) }));
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00028EDF File Offset: 0x000270DF
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
		public Func<IEnumerable<TElement>, TFSharpSet> CreateFSharpSetConstructor<TFSharpSet, TElement>()
		{
			return FSharpCoreReflectionProxy.CreateDelegate<Func<IEnumerable<TElement>, TFSharpSet>>(FSharpCoreReflectionProxy.EnsureMemberExists<MethodInfo>(this._fsharpSetCtor, "Microsoft.FSharp.Collections.SetModule.OfSeq<T>(IEnumerable<T> source)").MakeGenericMethod(new Type[] { typeof(TElement) }));
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00028F0E File Offset: 0x0002710E
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
		public Func<IEnumerable<Tuple<TKey, TValue>>, TFSharpMap> CreateFSharpMapConstructor<TFSharpMap, TKey, TValue>()
		{
			return FSharpCoreReflectionProxy.CreateDelegate<Func<IEnumerable<Tuple<TKey, TValue>>, TFSharpMap>>(FSharpCoreReflectionProxy.EnsureMemberExists<MethodInfo>(this._fsharpMapCtor, "Microsoft.FSharp.Collections.MapModule.OfSeq<TKey, TValue>(IEnumerable<Tuple<TKey, TValue>> source)").MakeGenericMethod(new Type[]
			{
				typeof(TKey),
				typeof(TValue)
			}));
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00028F4A File Offset: 0x0002714A
		private Attribute GetFSharpCompilationMappingAttribute(Type type)
		{
			return type.GetCustomAttribute(this._compilationMappingAttributeType, true);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00028F59 File Offset: 0x00027159
		private FSharpCoreReflectionProxy.SourceConstructFlags GetSourceConstructFlags(Attribute compilationMappingAttribute)
		{
			if (this._sourceConstructFlagsGetter != null)
			{
				return (FSharpCoreReflectionProxy.SourceConstructFlags)this._sourceConstructFlagsGetter.Invoke(compilationMappingAttribute, null);
			}
			return FSharpCoreReflectionProxy.SourceConstructFlags.None;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00028F78 File Offset: 0x00027178
		private static Assembly GetFSharpCoreAssembly(Type type)
		{
			foreach (Attribute attribute in type.GetCustomAttributes(true))
			{
				Type type2 = attribute.GetType();
				if (type2.FullName == "Microsoft.FSharp.Core.CompilationMappingAttribute")
				{
					return type2.Assembly;
				}
			}
			return null;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00028FC5 File Offset: 0x000271C5
		private static TDelegate CreateDelegate<TDelegate>(MethodInfo methodInfo) where TDelegate : Delegate
		{
			return (TDelegate)((object)Delegate.CreateDelegate(typeof(TDelegate), methodInfo, true));
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00028FDD File Offset: 0x000271DD
		private static TMemberInfo EnsureMemberExists<TMemberInfo>(TMemberInfo memberInfo, string memberName) where TMemberInfo : MemberInfo
		{
			if (memberInfo == null)
			{
				ThrowHelper.ThrowMissingMemberException_MissingFSharpCoreMember(memberName);
			}
			return memberInfo;
		}

		// Token: 0x0400032E RID: 814
		public const string FSharpCoreUnreferencedCodeMessage = "Uses Reflection to access FSharp.Core components at runtime.";

		// Token: 0x0400032F RID: 815
		private static FSharpCoreReflectionProxy s_singletonInstance;

		// Token: 0x04000330 RID: 816
		private const string CompilationMappingAttributeTypeName = "Microsoft.FSharp.Core.CompilationMappingAttribute";

		// Token: 0x04000331 RID: 817
		private readonly Type _compilationMappingAttributeType;

		// Token: 0x04000332 RID: 818
		private readonly MethodInfo _sourceConstructFlagsGetter;

		// Token: 0x04000333 RID: 819
		private readonly Type _fsharpOptionType;

		// Token: 0x04000334 RID: 820
		private readonly Type _fsharpValueOptionType;

		// Token: 0x04000335 RID: 821
		private readonly Type _fsharpListType;

		// Token: 0x04000336 RID: 822
		private readonly Type _fsharpSetType;

		// Token: 0x04000337 RID: 823
		private readonly Type _fsharpMapType;

		// Token: 0x04000338 RID: 824
		private readonly MethodInfo _fsharpListCtor;

		// Token: 0x04000339 RID: 825
		private readonly MethodInfo _fsharpSetCtor;

		// Token: 0x0400033A RID: 826
		private readonly MethodInfo _fsharpMapCtor;

		// Token: 0x0200013C RID: 316
		public enum FSharpKind
		{
			// Token: 0x040004F2 RID: 1266
			Unrecognized,
			// Token: 0x040004F3 RID: 1267
			Option,
			// Token: 0x040004F4 RID: 1268
			ValueOption,
			// Token: 0x040004F5 RID: 1269
			List,
			// Token: 0x040004F6 RID: 1270
			Set,
			// Token: 0x040004F7 RID: 1271
			Map,
			// Token: 0x040004F8 RID: 1272
			Record,
			// Token: 0x040004F9 RID: 1273
			Union
		}

		// Token: 0x0200013D RID: 317
		// (Invoke) Token: 0x06000DEC RID: 3564
		public delegate TResult StructGetter<TStruct, TResult>(ref TStruct @this) where TStruct : struct;

		// Token: 0x0200013E RID: 318
		private enum SourceConstructFlags
		{
			// Token: 0x040004FB RID: 1275
			None,
			// Token: 0x040004FC RID: 1276
			SumType,
			// Token: 0x040004FD RID: 1277
			RecordType,
			// Token: 0x040004FE RID: 1278
			ObjectType,
			// Token: 0x040004FF RID: 1279
			Field,
			// Token: 0x04000500 RID: 1280
			Exception,
			// Token: 0x04000501 RID: 1281
			Closure,
			// Token: 0x04000502 RID: 1282
			Module,
			// Token: 0x04000503 RID: 1283
			UnionCase,
			// Token: 0x04000504 RID: 1284
			Value,
			// Token: 0x04000505 RID: 1285
			KindMask = 31,
			// Token: 0x04000506 RID: 1286
			NonPublicRepresentation
		}
	}
}
