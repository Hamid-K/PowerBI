using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000C4 RID: 196
	internal sealed class ArrayConverter<TCollection, TElement> : IEnumerableDefaultConverter<TElement[], TElement>
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0002E0E6 File Offset: 0x0002C2E6
		internal override bool CanHaveMetadata
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002E0E9 File Offset: 0x0002C2E9
		protected override void Add(in TElement value, ref ReadStack state)
		{
			((List<TElement>)state.Current.ReturnValue).Add(value);
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0002E106 File Offset: 0x0002C306
		internal override bool SupportsCreateObjectDelegate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002E109 File Offset: 0x0002C309
		protected override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonSerializerOptions options)
		{
			state.Current.ReturnValue = new List<TElement>();
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002E11C File Offset: 0x0002C31C
		protected override void ConvertCollection(ref ReadStack state, JsonSerializerOptions options)
		{
			List<TElement> list = (List<TElement>)state.Current.ReturnValue;
			state.Current.ReturnValue = list.ToArray();
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002E14C File Offset: 0x0002C34C
		protected override bool OnWriteResume(Utf8JsonWriter writer, TElement[] array, JsonSerializerOptions options, ref WriteStack state)
		{
			int i = state.Current.EnumeratorIndex;
			JsonConverter<TElement> elementConverter = JsonCollectionConverter<TElement[], TElement>.GetElementConverter(ref state);
			if (elementConverter.CanUseDirectReadOrWrite && state.Current.NumberHandling == null)
			{
				while (i < array.Length)
				{
					elementConverter.Write(writer, array[i], options);
					i++;
				}
			}
			else
			{
				while (i < array.Length)
				{
					TElement telement = array[i];
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
