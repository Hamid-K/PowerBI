using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000BB RID: 187
	internal sealed class ReadOnlyMemoryConverter<T> : JsonCollectionConverter<ReadOnlyMemory<T>, T>
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0002DBC9 File Offset: 0x0002BDC9
		internal override bool CanHaveMetadata
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0002DBCC File Offset: 0x0002BDCC
		public override bool HandleNull
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002DBCF File Offset: 0x0002BDCF
		internal override bool OnTryRead(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out ReadOnlyMemory<T> value)
		{
			if (reader.TokenType == JsonTokenType.Null)
			{
				value = default(ReadOnlyMemory<T>);
				return true;
			}
			return base.OnTryRead(ref reader, typeToConvert, options, ref state, out value);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002DBF2 File Offset: 0x0002BDF2
		protected override void Add(in T value, ref ReadStack state)
		{
			((List<T>)state.Current.ReturnValue).Add(value);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002DC0F File Offset: 0x0002BE0F
		protected override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonSerializerOptions options)
		{
			state.Current.ReturnValue = new List<T>();
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002DC24 File Offset: 0x0002BE24
		protected override void ConvertCollection(ref ReadStack state, JsonSerializerOptions options)
		{
			ReadOnlyMemory<T> readOnlyMemory = ((List<T>)state.Current.ReturnValue).ToArray().AsMemory<T>();
			state.Current.ReturnValue = readOnlyMemory;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002DC62 File Offset: 0x0002BE62
		protected override bool OnWriteResume(Utf8JsonWriter writer, ReadOnlyMemory<T> value, JsonSerializerOptions options, ref WriteStack state)
		{
			return ReadOnlyMemoryConverter<T>.OnWriteResume(writer, value.Span, options, ref state);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002DC74 File Offset: 0x0002BE74
		internal unsafe static bool OnWriteResume(Utf8JsonWriter writer, ReadOnlySpan<T> value, JsonSerializerOptions options, ref WriteStack state)
		{
			int i = state.Current.EnumeratorIndex;
			JsonConverter<T> elementConverter = JsonCollectionConverter<ReadOnlyMemory<T>, T>.GetElementConverter(ref state);
			if (elementConverter.CanUseDirectReadOrWrite && state.Current.NumberHandling == null)
			{
				while (i < value.Length)
				{
					elementConverter.Write(writer, *value[i], options);
					i++;
				}
			}
			else
			{
				while (i < value.Length)
				{
					if (!elementConverter.TryWrite(writer, value[i], options, ref state))
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
