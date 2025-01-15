using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000CC RID: 204
	internal sealed class IDictionaryOfTKeyTValueConverter<TDictionary, TKey, TValue> : DictionaryDefaultConverter<TDictionary, TKey, TValue> where TDictionary : IDictionary<TKey, TValue>
	{
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0002EA91 File Offset: 0x0002CC91
		internal override bool CanPopulate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002EA94 File Offset: 0x0002CC94
		protected override void Add(TKey key, in TValue value, JsonSerializerOptions options, ref ReadStack state)
		{
			TDictionary tdictionary = (TDictionary)((object)state.Current.ReturnValue);
			tdictionary[key] = value;
			if (base.IsValueType)
			{
				state.Current.ReturnValue = tdictionary;
			}
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0002EAE4 File Offset: 0x0002CCE4
		protected override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state)
		{
			base.CreateCollection(ref reader, ref state);
			TDictionary tdictionary = (TDictionary)((object)state.Current.ReturnValue);
			if (tdictionary.IsReadOnly)
			{
				state.Current.ReturnValue = null;
				ThrowHelper.ThrowNotSupportedException_CannotPopulateCollection(this.Type, ref reader, ref state);
			}
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002EB34 File Offset: 0x0002CD34
		internal override void ConfigureJsonTypeInfo(JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options)
		{
			if (jsonTypeInfo.CreateObject == null && this.Type.IsAssignableFrom(typeof(Dictionary<TKey, TValue>)))
			{
				jsonTypeInfo.CreateObject = () => new Dictionary<TKey, TValue>();
			}
		}
	}
}
