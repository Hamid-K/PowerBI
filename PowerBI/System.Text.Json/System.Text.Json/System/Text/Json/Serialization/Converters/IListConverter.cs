using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000D2 RID: 210
	internal sealed class IListConverter<TCollection> : JsonCollectionConverter<TCollection, object> where TCollection : IList
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0002F356 File Offset: 0x0002D556
		internal override bool CanPopulate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002F35C File Offset: 0x0002D55C
		protected override void Add(in object value, ref ReadStack state)
		{
			TCollection tcollection = (TCollection)((object)state.Current.ReturnValue);
			tcollection.Add(value);
			if (base.IsValueType)
			{
				state.Current.ReturnValue = tcollection;
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002F3A4 File Offset: 0x0002D5A4
		protected override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonSerializerOptions options)
		{
			base.CreateCollection(ref reader, ref state, options);
			TCollection tcollection = (TCollection)((object)state.Current.ReturnValue);
			if (tcollection.IsReadOnly)
			{
				state.Current.ReturnValue = null;
				ThrowHelper.ThrowNotSupportedException_CannotPopulateCollection(this.Type, ref reader, ref state);
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002F3F4 File Offset: 0x0002D5F4
		protected override bool OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, ref WriteStack state)
		{
			IList list = value;
			int i = state.Current.EnumeratorIndex;
			JsonConverter<object> elementConverter = JsonCollectionConverter<TCollection, object>.GetElementConverter(ref state);
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
					object obj = list[i];
					if (!elementConverter.TryWrite(writer, in obj, options, ref state))
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

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002F4B8 File Offset: 0x0002D6B8
		internal override void ConfigureJsonTypeInfo(JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options)
		{
			if (jsonTypeInfo.CreateObject == null && this.Type.IsAssignableFrom(typeof(List<object>)))
			{
				jsonTypeInfo.CreateObject = () => new List<object>();
			}
		}
	}
}
