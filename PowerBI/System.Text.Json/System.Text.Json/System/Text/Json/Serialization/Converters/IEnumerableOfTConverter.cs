using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000D0 RID: 208
	internal sealed class IEnumerableOfTConverter<TCollection, TElement> : IEnumerableDefaultConverter<TCollection, TElement> where TCollection : IEnumerable<TElement>
	{
		// Token: 0x06000BDD RID: 3037 RVA: 0x0002F1AF File Offset: 0x0002D3AF
		protected override void Add(in TElement value, ref ReadStack state)
		{
			((List<TElement>)state.Current.ReturnValue).Add(value);
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0002F1CC File Offset: 0x0002D3CC
		internal override bool SupportsCreateObjectDelegate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002F1CF File Offset: 0x0002D3CF
		protected override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonSerializerOptions options)
		{
			if (!this._isDeserializable)
			{
				ThrowHelper.ThrowNotSupportedException_CannotPopulateCollection(this.Type, ref reader, ref state);
			}
			state.Current.ReturnValue = new List<TElement>();
		}

		// Token: 0x040003F8 RID: 1016
		private readonly bool _isDeserializable = typeof(TCollection).IsAssignableFrom(typeof(List<TElement>));
	}
}
