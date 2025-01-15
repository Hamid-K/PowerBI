using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000D1 RID: 209
	internal class StackOrQueueConverter<TCollection> : JsonCollectionConverter<TCollection, object> where TCollection : IEnumerable
	{
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0002F21D File Offset: 0x0002D41D
		internal override bool CanPopulate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002F220 File Offset: 0x0002D420
		protected sealed override void Add(in object value, ref ReadStack state)
		{
			Action<TCollection, object> action = (Action<TCollection, object>)state.Current.JsonTypeInfo.AddMethodDelegate;
			action((TCollection)((object)state.Current.ReturnValue), value);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002F25C File Offset: 0x0002D45C
		protected sealed override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonSerializerOptions options)
		{
			JsonPropertyInfo parentProperty = state.ParentProperty;
			if (parentProperty != null && parentProperty.TryGetPrePopulatedValue(ref state))
			{
				return;
			}
			JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
			Func<object> createObject = jsonTypeInfo.CreateObject;
			if (createObject == null)
			{
				ThrowHelper.ThrowNotSupportedException_CannotPopulateCollection(this.Type, ref reader, ref state);
			}
			state.Current.ReturnValue = createObject();
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002F2B4 File Offset: 0x0002D4B4
		protected sealed override bool OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, ref WriteStack state)
		{
			IEnumerator enumerator;
			if (state.Current.CollectionEnumerator == null)
			{
				enumerator = value.GetEnumerator();
				if (!enumerator.MoveNext())
				{
					return true;
				}
			}
			else
			{
				enumerator = state.Current.CollectionEnumerator;
			}
			JsonConverter<object> elementConverter = JsonCollectionConverter<TCollection, object>.GetElementConverter(ref state);
			while (!JsonConverter.ShouldFlush(writer, ref state))
			{
				object obj = enumerator.Current;
				if (!elementConverter.TryWrite(writer, in obj, options, ref state))
				{
					state.Current.CollectionEnumerator = enumerator;
					return false;
				}
				state.Current.EndCollectionElement();
				if (!enumerator.MoveNext())
				{
					return true;
				}
			}
			state.Current.CollectionEnumerator = enumerator;
			return false;
		}
	}
}
