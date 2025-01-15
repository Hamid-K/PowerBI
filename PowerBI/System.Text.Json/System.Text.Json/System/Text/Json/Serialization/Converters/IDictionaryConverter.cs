using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000CB RID: 203
	internal sealed class IDictionaryConverter<TDictionary> : JsonDictionaryConverter<TDictionary, string, object> where TDictionary : IDictionary
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0002E855 File Offset: 0x0002CA55
		internal override bool CanPopulate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002E858 File Offset: 0x0002CA58
		protected override void Add(string key, in object value, JsonSerializerOptions options, ref ReadStack state)
		{
			TDictionary tdictionary = (TDictionary)((object)state.Current.ReturnValue);
			tdictionary[key] = value;
			if (base.IsValueType)
			{
				state.Current.ReturnValue = tdictionary;
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002E8A4 File Offset: 0x0002CAA4
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

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002E8F4 File Offset: 0x0002CAF4
		protected internal override bool OnWriteResume(Utf8JsonWriter writer, TDictionary value, JsonSerializerOptions options, ref WriteStack state)
		{
			IDictionaryEnumerator dictionaryEnumerator;
			if (state.Current.CollectionEnumerator == null)
			{
				dictionaryEnumerator = value.GetEnumerator();
				if (!dictionaryEnumerator.MoveNext())
				{
					return true;
				}
			}
			else
			{
				dictionaryEnumerator = (IDictionaryEnumerator)state.Current.CollectionEnumerator;
			}
			JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
			if (this._valueConverter == null)
			{
				this._valueConverter = JsonDictionaryConverter<TDictionary, string, object>.GetConverter<object>(jsonTypeInfo.ElementTypeInfo);
			}
			while (!JsonConverter.ShouldFlush(writer, ref state))
			{
				if (state.Current.PropertyState < StackFramePropertyState.Name)
				{
					state.Current.PropertyState = StackFramePropertyState.Name;
					object key = dictionaryEnumerator.Key;
					string text = key as string;
					if (text != null)
					{
						if (this._keyConverter == null)
						{
							this._keyConverter = JsonDictionaryConverter<TDictionary, string, object>.GetConverter<string>(jsonTypeInfo.KeyTypeInfo);
						}
						this._keyConverter.WriteAsPropertyNameCore(writer, text, options, state.Current.IsWritingExtensionDataProperty);
					}
					else
					{
						this._valueConverter.WriteAsPropertyNameCore(writer, key, options, state.Current.IsWritingExtensionDataProperty);
					}
				}
				object value2 = dictionaryEnumerator.Value;
				if (!this._valueConverter.TryWrite(writer, in value2, options, ref state))
				{
					state.Current.CollectionEnumerator = dictionaryEnumerator;
					return false;
				}
				state.Current.EndDictionaryEntry();
				if (!dictionaryEnumerator.MoveNext())
				{
					return true;
				}
			}
			state.Current.CollectionEnumerator = dictionaryEnumerator;
			return false;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002EA38 File Offset: 0x0002CC38
		internal override void ConfigureJsonTypeInfo(JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options)
		{
			if (jsonTypeInfo.CreateObject == null && this.Type.IsAssignableFrom(typeof(Dictionary<string, object>)))
			{
				jsonTypeInfo.CreateObject = () => new Dictionary<string, object>();
			}
		}
	}
}
