using System;
using System.Buffers.Text;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x02000104 RID: 260
	internal sealed class TimeSpanConverter : JsonPrimitiveConverter<TimeSpan>
	{
		// Token: 0x06000CFB RID: 3323 RVA: 0x00032EC3 File Offset: 0x000310C3
		public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.String)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedString(reader.TokenType);
			}
			return TimeSpanConverter.ReadCore(ref reader);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00032EDF File Offset: 0x000310DF
		internal override TimeSpan ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return TimeSpanConverter.ReadCore(ref reader);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00032EE8 File Offset: 0x000310E8
		private unsafe static TimeSpan ReadCore(ref Utf8JsonReader reader)
		{
			if (!JsonHelpers.IsInRangeInclusive(reader.ValueLength, 8, 156))
			{
				ThrowHelper.ThrowFormatException(DataType.TimeSpan);
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (!reader.HasValueSequence && !reader.ValueIsEscaped)
			{
				readOnlySpan = reader.ValueSpan;
			}
			else
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)156], 156);
				Span<byte> span2 = span;
				int num = reader.CopyString(span2);
				readOnlySpan = span2.Slice(0, num);
			}
			byte b = *readOnlySpan[0];
			if (!JsonHelpers.IsDigit(b) && b != 45)
			{
				ThrowHelper.ThrowFormatException(DataType.TimeSpan);
			}
			TimeSpan timeSpan;
			int num2;
			bool flag = Utf8Parser.TryParse(readOnlySpan, out timeSpan, out num2, 'c');
			if (!flag || readOnlySpan.Length != num2)
			{
				ThrowHelper.ThrowFormatException(DataType.TimeSpan);
			}
			return timeSpan;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00032F98 File Offset: 0x00031198
		public unsafe override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)26], 26);
			Span<byte> span2 = span;
			int num;
			bool flag = Utf8Formatter.TryFormat(value, span2, out num, 'c');
			writer.WriteStringValue(span2.Slice(0, num));
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00032FDC File Offset: 0x000311DC
		internal unsafe override void WriteAsPropertyNameCore(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)26], 26);
			Span<byte> span2 = span;
			int num;
			bool flag = Utf8Formatter.TryFormat(value, span2, out num, 'c');
			writer.WritePropertyName(span2.Slice(0, num));
		}

		// Token: 0x04000417 RID: 1047
		private const int MinimumTimeSpanFormatLength = 8;

		// Token: 0x04000418 RID: 1048
		private const int MaximumTimeSpanFormatLength = 26;

		// Token: 0x04000419 RID: 1049
		private const int MaximumEscapedTimeSpanFormatLength = 156;
	}
}
