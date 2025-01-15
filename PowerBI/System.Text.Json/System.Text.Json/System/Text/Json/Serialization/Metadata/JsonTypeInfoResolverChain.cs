using System;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000A4 RID: 164
	internal class JsonTypeInfoResolverChain : ConfigurationList<IJsonTypeInfoResolver>, IJsonTypeInfoResolver, IBuiltInJsonTypeInfoResolver
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x000289BE File Offset: 0x00026BBE
		public JsonTypeInfoResolverChain()
			: base(null)
		{
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x000289C7 File Offset: 0x00026BC7
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x000289CA File Offset: 0x00026BCA
		protected override void OnCollectionModifying()
		{
			ThrowHelper.ThrowInvalidOperationException_TypeInfoResolverChainImmutable();
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x000289D4 File Offset: 0x00026BD4
		public JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
		{
			foreach (IJsonTypeInfoResolver jsonTypeInfoResolver in this._list)
			{
				JsonTypeInfo typeInfo = jsonTypeInfoResolver.GetTypeInfo(type, options);
				if (typeInfo != null)
				{
					return typeInfo;
				}
			}
			return null;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00028A34 File Offset: 0x00026C34
		internal void AddFlattened(IJsonTypeInfoResolver resolver)
		{
			if (resolver != null && !(resolver is EmptyJsonTypeInfoResolver))
			{
				JsonTypeInfoResolverChain jsonTypeInfoResolverChain = resolver as JsonTypeInfoResolverChain;
				if (jsonTypeInfoResolverChain != null)
				{
					this._list.AddRange(jsonTypeInfoResolverChain);
					return;
				}
				this._list.Add(resolver);
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00028A70 File Offset: 0x00026C70
		bool IBuiltInJsonTypeInfoResolver.IsCompatibleWithOptions(JsonSerializerOptions options)
		{
			foreach (IJsonTypeInfoResolver jsonTypeInfoResolver in this._list)
			{
				if (!jsonTypeInfoResolver.IsCompatibleWithOptions(options))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00028ACC File Offset: 0x00026CCC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			foreach (IJsonTypeInfoResolver jsonTypeInfoResolver in this._list)
			{
				stringBuilder.Append(jsonTypeInfoResolver);
				stringBuilder.Append(", ");
			}
			if (this._list.Count > 0)
			{
				stringBuilder.Length -= 2;
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}
	}
}
