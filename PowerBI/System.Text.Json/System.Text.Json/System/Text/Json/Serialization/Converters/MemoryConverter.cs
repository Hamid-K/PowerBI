using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000C0 RID: 192
	internal sealed class MemoryConverter<T> : JsonCollectionConverter<Memory<T>, T>
	{
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0002DF82 File Offset: 0x0002C182
		internal override bool CanHaveMetadata
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0002DF85 File Offset: 0x0002C185
		public override bool HandleNull
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002DF88 File Offset: 0x0002C188
		internal override bool OnTryRead(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out Memory<T> value)
		{
			if (reader.TokenType == JsonTokenType.Null)
			{
				value = default(Memory<T>);
				return true;
			}
			return base.OnTryRead(ref reader, typeToConvert, options, ref state, out value);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002DFAB File Offset: 0x0002C1AB
		protected override void Add(in T value, ref ReadStack state)
		{
			((List<T>)state.Current.ReturnValue).Add(value);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002DFC8 File Offset: 0x0002C1C8
		protected override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonSerializerOptions options)
		{
			state.Current.ReturnValue = new List<T>();
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002DFDC File Offset: 0x0002C1DC
		protected override void ConvertCollection(ref ReadStack state, JsonSerializerOptions options)
		{
			Memory<T> memory = ((List<T>)state.Current.ReturnValue).ToArray().AsMemory<T>();
			state.Current.ReturnValue = memory;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002E015 File Offset: 0x0002C215
		protected override bool OnWriteResume(Utf8JsonWriter writer, Memory<T> value, JsonSerializerOptions options, ref WriteStack state)
		{
			return ReadOnlyMemoryConverter<T>.OnWriteResume(writer, value.Span, options, ref state);
		}
	}
}
