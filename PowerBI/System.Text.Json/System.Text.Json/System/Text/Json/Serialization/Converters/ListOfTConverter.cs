using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000D8 RID: 216
	internal sealed class ListOfTConverter<TCollection, TElement> : IEnumerableDefaultConverter<TCollection, TElement> where TCollection : List<TElement>
	{
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0002F885 File Offset: 0x0002DA85
		internal override bool CanPopulate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002F888 File Offset: 0x0002DA88
		protected override void Add(in TElement value, ref ReadStack state)
		{
			((TCollection)((object)state.Current.ReturnValue)).Add(value);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002F8AC File Offset: 0x0002DAAC
		protected override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonSerializerOptions options)
		{
			JsonPropertyInfo parentProperty = state.ParentProperty;
			if (parentProperty != null && parentProperty.TryGetPrePopulatedValue(ref state))
			{
				return;
			}
			if (state.Current.JsonTypeInfo.CreateObject == null)
			{
				ThrowHelper.ThrowNotSupportedException_SerializationNotSupported(state.Current.JsonTypeInfo.Type);
			}
			state.Current.ReturnValue = state.Current.JsonTypeInfo.CreateObject();
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0002F918 File Offset: 0x0002DB18
		protected override bool OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, ref WriteStack state)
		{
			List<TElement> list = value;
			int i = state.Current.EnumeratorIndex;
			JsonConverter<TElement> elementConverter = JsonCollectionConverter<TCollection, TElement>.GetElementConverter(ref state);
			if (elementConverter.CanUseDirectReadOrWrite && state.Current.NumberHandling == null)
			{
				while (i < list.Count)
				{
					elementConverter.Write(writer, list[i], options);
					i++;
				}
			}
			else
			{
				while (i < list.Count)
				{
					TElement telement = list[i];
					if (!elementConverter.TryWrite(writer, in telement, options, ref state))
					{
						state.Current.EnumeratorIndex = i;
						return false;
					}
					state.Current.EndCollectionElement();
					if (JsonConverter.ShouldFlush(writer, ref state))
					{
						state.Current.EnumeratorIndex = i + 1;
						return false;
					}
					i++;
				}
			}
			return true;
		}
	}
}
