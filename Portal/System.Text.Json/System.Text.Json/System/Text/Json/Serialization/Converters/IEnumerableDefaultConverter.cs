using System;
using System.Collections.Generic;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000CF RID: 207
	internal abstract class IEnumerableDefaultConverter<TCollection, TElement> : JsonCollectionConverter<TCollection, TElement> where TCollection : IEnumerable<TElement>
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0002F0F8 File Offset: 0x0002D2F8
		internal override bool CanHaveMetadata
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002F0FC File Offset: 0x0002D2FC
		protected override bool OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, ref WriteStack state)
		{
			IEnumerator<TElement> enumerator;
			if (state.Current.CollectionEnumerator == null)
			{
				enumerator = value.GetEnumerator();
				if (!enumerator.MoveNext())
				{
					enumerator.Dispose();
					return true;
				}
			}
			else
			{
				enumerator = (IEnumerator<TElement>)state.Current.CollectionEnumerator;
			}
			JsonConverter<TElement> elementConverter = JsonCollectionConverter<TCollection, TElement>.GetElementConverter(ref state);
			while (!JsonConverter.ShouldFlush(writer, ref state))
			{
				TElement telement = enumerator.Current;
				if (!elementConverter.TryWrite(writer, in telement, options, ref state))
				{
					state.Current.CollectionEnumerator = enumerator;
					return false;
				}
				state.Current.EndCollectionElement();
				if (!enumerator.MoveNext())
				{
					enumerator.Dispose();
					return true;
				}
			}
			state.Current.CollectionEnumerator = enumerator;
			return false;
		}
	}
}
