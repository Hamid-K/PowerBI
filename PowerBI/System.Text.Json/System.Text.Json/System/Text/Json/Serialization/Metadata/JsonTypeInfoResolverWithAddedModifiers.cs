using System;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000A5 RID: 165
	internal sealed class JsonTypeInfoResolverWithAddedModifiers : IJsonTypeInfoResolver
	{
		// Token: 0x06000974 RID: 2420 RVA: 0x00028B64 File Offset: 0x00026D64
		public JsonTypeInfoResolverWithAddedModifiers(IJsonTypeInfoResolver source, Action<JsonTypeInfo>[] modifiers)
		{
			this._source = source;
			this._modifiers = modifiers;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00028B7C File Offset: 0x00026D7C
		public JsonTypeInfoResolverWithAddedModifiers WithAddedModifier(Action<JsonTypeInfo> modifier)
		{
			Action<JsonTypeInfo>[] array = new Action<JsonTypeInfo>[this._modifiers.Length + 1];
			this._modifiers.CopyTo(array, 0);
			array[this._modifiers.Length] = modifier;
			return new JsonTypeInfoResolverWithAddedModifiers(this._source, array);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00028BC0 File Offset: 0x00026DC0
		public JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
		{
			JsonTypeInfo typeInfo = this._source.GetTypeInfo(type, options);
			if (typeInfo != null)
			{
				foreach (Action<JsonTypeInfo> action in this._modifiers)
				{
					action(typeInfo);
				}
			}
			return typeInfo;
		}

		// Token: 0x0400032C RID: 812
		private readonly IJsonTypeInfoResolver _source;

		// Token: 0x0400032D RID: 813
		private readonly Action<JsonTypeInfo>[] _modifiers;
	}
}
