using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000083 RID: 131
	public abstract class JsonSerializerContext : IJsonTypeInfoResolver, IBuiltInJsonTypeInfoResolver
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00024BE0 File Offset: 0x00022DE0
		[Nullable(1)]
		public JsonSerializerOptions Options
		{
			[NullableContext(1)]
			get
			{
				JsonSerializerOptions jsonSerializerOptions = this._options;
				if (jsonSerializerOptions == null)
				{
					jsonSerializerOptions = new JsonSerializerOptions
					{
						TypeInfoResolver = this
					};
					jsonSerializerOptions.MakeReadOnly();
					this._options = jsonSerializerOptions;
				}
				return jsonSerializerOptions;
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00024C12 File Offset: 0x00022E12
		internal void AssociateWithOptions(JsonSerializerOptions options)
		{
			options.TypeInfoResolver = this;
			options.MakeReadOnly();
			this._options = options;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00024C28 File Offset: 0x00022E28
		bool IBuiltInJsonTypeInfoResolver.IsCompatibleWithOptions(JsonSerializerOptions options)
		{
			JsonSerializerOptions generatedSerializerOptions = this.GeneratedSerializerOptions;
			return generatedSerializerOptions != null && options.Converters.Count == 0 && options.Encoder == null && !JsonHelpers.RequiresSpecialNumberHandlingOnWrite(new JsonNumberHandling?(options.NumberHandling)) && options.ReferenceHandlingStrategy == ReferenceHandlingStrategy.None && !options.IgnoreNullValues && options.DefaultIgnoreCondition == generatedSerializerOptions.DefaultIgnoreCondition && options.IgnoreReadOnlyFields == generatedSerializerOptions.IgnoreReadOnlyFields && options.IgnoreReadOnlyProperties == generatedSerializerOptions.IgnoreReadOnlyProperties && options.IncludeFields == generatedSerializerOptions.IncludeFields && options.PropertyNamingPolicy == generatedSerializerOptions.PropertyNamingPolicy && options.DictionaryKeyPolicy == null;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000836 RID: 2102
		[Nullable(2)]
		protected abstract JsonSerializerOptions GeneratedSerializerOptions
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00024CCA File Offset: 0x00022ECA
		[NullableContext(2)]
		protected JsonSerializerContext(JsonSerializerOptions options)
		{
			if (options != null)
			{
				options.VerifyMutable();
				this.AssociateWithOptions(options);
			}
		}

		// Token: 0x06000838 RID: 2104
		[NullableContext(1)]
		[return: Nullable(2)]
		public abstract JsonTypeInfo GetTypeInfo(Type type);

		// Token: 0x06000839 RID: 2105 RVA: 0x00024CE2 File Offset: 0x00022EE2
		JsonTypeInfo IJsonTypeInfoResolver.GetTypeInfo(Type type, JsonSerializerOptions options)
		{
			if (options != null && options != this._options)
			{
				ThrowHelper.ThrowInvalidOperationException_ResolverTypeInfoOptionsNotCompatible();
			}
			return this.GetTypeInfo(type);
		}

		// Token: 0x040002E4 RID: 740
		private JsonSerializerOptions _options;
	}
}
