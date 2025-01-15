using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000CD RID: 205
	internal sealed class IEnumerableConverter<TCollection> : JsonCollectionConverter<TCollection, object> where TCollection : IEnumerable
	{
		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002EB8D File Offset: 0x0002CD8D
		protected override void Add(in object value, ref ReadStack state)
		{
			((List<object>)state.Current.ReturnValue).Add(value);
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0002EBA6 File Offset: 0x0002CDA6
		internal override bool SupportsCreateObjectDelegate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002EBA9 File Offset: 0x0002CDA9
		protected override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonSerializerOptions options)
		{
			if (!this._isDeserializable)
			{
				ThrowHelper.ThrowNotSupportedException_CannotPopulateCollection(this.Type, ref reader, ref state);
			}
			state.Current.ReturnValue = new List<object>();
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002EBD0 File Offset: 0x0002CDD0
		protected override bool OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, ref WriteStack state)
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

		// Token: 0x040003F4 RID: 1012
		private readonly bool _isDeserializable = typeof(TCollection).IsAssignableFrom(typeof(List<object>));
	}
}
