using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000D9 RID: 217
	internal sealed class QueueOfTConverter<TCollection, TElement> : IEnumerableDefaultConverter<TCollection, TElement> where TCollection : Queue<TElement>
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0002F9E3 File Offset: 0x0002DBE3
		internal override bool CanPopulate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002F9E6 File Offset: 0x0002DBE6
		protected override void Add(in TElement value, ref ReadStack state)
		{
			((TCollection)((object)state.Current.ReturnValue)).Enqueue(value);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002FA08 File Offset: 0x0002DC08
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
