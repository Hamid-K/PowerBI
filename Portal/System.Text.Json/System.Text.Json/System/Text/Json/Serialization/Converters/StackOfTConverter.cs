using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000DA RID: 218
	internal sealed class StackOfTConverter<TCollection, TElement> : IEnumerableDefaultConverter<TCollection, TElement> where TCollection : Stack<TElement>
	{
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0002FA7A File Offset: 0x0002DC7A
		internal override bool CanPopulate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002FA7D File Offset: 0x0002DC7D
		protected override void Add(in TElement value, ref ReadStack state)
		{
			((TCollection)((object)state.Current.ReturnValue)).Push(value);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002FAA0 File Offset: 0x0002DCA0
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
	}
}
