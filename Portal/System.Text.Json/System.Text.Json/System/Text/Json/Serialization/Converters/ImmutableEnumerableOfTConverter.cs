using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000D5 RID: 213
	internal class ImmutableEnumerableOfTConverter<TCollection, TElement> : IEnumerableDefaultConverter<TCollection, TElement> where TCollection : IEnumerable<TElement>
	{
		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002F691 File Offset: 0x0002D891
		protected sealed override void Add(in TElement value, ref ReadStack state)
		{
			((List<TElement>)state.Current.ReturnValue).Add(value);
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x0002F6AE File Offset: 0x0002D8AE
		internal sealed override bool CanHaveMetadata
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0002F6B1 File Offset: 0x0002D8B1
		internal override bool SupportsCreateObjectDelegate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002F6B4 File Offset: 0x0002D8B4
		protected sealed override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonSerializerOptions options)
		{
			state.Current.ReturnValue = new List<TElement>();
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002F6C8 File Offset: 0x0002D8C8
		protected sealed override void ConvertCollection(ref ReadStack state, JsonSerializerOptions options)
		{
			JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
			Func<IEnumerable<TElement>, TCollection> func = (Func<IEnumerable<TElement>, TCollection>)jsonTypeInfo.CreateObjectWithArgs;
			state.Current.ReturnValue = func((List<TElement>)state.Current.ReturnValue);
		}
	}
}
