using System;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000A0 RID: 160
	[NullableContext(1)]
	[Nullable(0)]
	public static class JsonTypeInfoResolver
	{
		// Token: 0x06000964 RID: 2404 RVA: 0x000288E0 File Offset: 0x00026AE0
		public static IJsonTypeInfoResolver Combine([Nullable(new byte[] { 1, 2 })] params IJsonTypeInfoResolver[] resolvers)
		{
			if (resolvers == null)
			{
				ThrowHelper.ThrowArgumentNullException("resolvers");
			}
			JsonTypeInfoResolverChain jsonTypeInfoResolverChain = new JsonTypeInfoResolverChain();
			foreach (IJsonTypeInfoResolver jsonTypeInfoResolver in resolvers)
			{
				jsonTypeInfoResolverChain.AddFlattened(jsonTypeInfoResolver);
			}
			if (jsonTypeInfoResolverChain.Count != 1)
			{
				return jsonTypeInfoResolverChain;
			}
			return jsonTypeInfoResolverChain[0];
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00028934 File Offset: 0x00026B34
		public static IJsonTypeInfoResolver WithAddedModifier(this IJsonTypeInfoResolver resolver, Action<JsonTypeInfo> modifier)
		{
			if (resolver == null)
			{
				ThrowHelper.ThrowArgumentNullException("resolver");
			}
			if (modifier == null)
			{
				ThrowHelper.ThrowArgumentNullException("modifier");
			}
			JsonTypeInfoResolverWithAddedModifiers jsonTypeInfoResolverWithAddedModifiers = resolver as JsonTypeInfoResolverWithAddedModifiers;
			if (jsonTypeInfoResolverWithAddedModifiers == null)
			{
				return new JsonTypeInfoResolverWithAddedModifiers(resolver, new Action<JsonTypeInfo>[] { modifier });
			}
			return jsonTypeInfoResolverWithAddedModifiers.WithAddedModifier(modifier);
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0002897D File Offset: 0x00026B7D
		internal static IJsonTypeInfoResolver Empty { get; } = new EmptyJsonTypeInfoResolver();

		// Token: 0x06000967 RID: 2407 RVA: 0x00028984 File Offset: 0x00026B84
		internal static bool IsCompatibleWithOptions(this IJsonTypeInfoResolver resolver, JsonSerializerOptions options)
		{
			IBuiltInJsonTypeInfoResolver builtInJsonTypeInfoResolver = resolver as IBuiltInJsonTypeInfoResolver;
			return builtInJsonTypeInfoResolver != null && builtInJsonTypeInfoResolver.IsCompatibleWithOptions(options);
		}
	}
}
