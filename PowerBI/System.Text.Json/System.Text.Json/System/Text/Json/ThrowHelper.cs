using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json
{
	// Token: 0x0200002B RID: 43
	internal static class ThrowHelper
	{
		// Token: 0x06000168 RID: 360 RVA: 0x000037DC File Offset: 0x000019DC
		[DoesNotReturn]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void ThrowOutOfMemoryException_BufferMaximumSizeExceeded(uint capacity)
		{
			throw new OutOfMemoryException(SR.Format(SR.BufferMaximumSizeExceeded, capacity));
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000037F3 File Offset: 0x000019F3
		[DoesNotReturn]
		public static void ThrowArgumentNullException(string parameterName)
		{
			throw new ArgumentNullException(parameterName);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000037FB File Offset: 0x000019FB
		[DoesNotReturn]
		public static void ThrowArgumentOutOfRangeException_MaxDepthMustBePositive(string parameterName)
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(parameterName, SR.MaxDepthMustBePositive);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00003808 File Offset: 0x00001A08
		private static ArgumentOutOfRangeException GetArgumentOutOfRangeException(string parameterName, string message)
		{
			return new ArgumentOutOfRangeException(parameterName, message);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00003811 File Offset: 0x00001A11
		[DoesNotReturn]
		public static void ThrowArgumentOutOfRangeException_CommentEnumMustBeInRange(string parameterName)
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(parameterName, SR.CommentHandlingMustBeValid);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000381E File Offset: 0x00001A1E
		[DoesNotReturn]
		public static void ThrowArgumentOutOfRangeException_ArrayIndexNegative(string paramName)
		{
			throw new ArgumentOutOfRangeException(paramName, SR.ArrayIndexNegative);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000382B File Offset: 0x00001A2B
		[DoesNotReturn]
		public static void ThrowArgumentOutOfRangeException_JsonConverterFactory_TypeNotSupported(Type typeToConvert)
		{
			throw new ArgumentOutOfRangeException("typeToConvert", SR.Format(SR.SerializerConverterFactoryInvalidArgument, typeToConvert.FullName));
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00003847 File Offset: 0x00001A47
		[DoesNotReturn]
		public static void ThrowArgumentException_ArrayTooSmall(string paramName)
		{
			throw new ArgumentException(SR.ArrayTooSmall, paramName);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00003854 File Offset: 0x00001A54
		private static ArgumentException GetArgumentException(string message)
		{
			return new ArgumentException(message);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000385C File Offset: 0x00001A5C
		[DoesNotReturn]
		public static void ThrowArgumentException(string message)
		{
			throw ThrowHelper.GetArgumentException(message);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00003864 File Offset: 0x00001A64
		public static InvalidOperationException GetInvalidOperationException_CallFlushFirst(int _buffered)
		{
			return ThrowHelper.GetInvalidOperationException(SR.Format(SR.CallFlushToAvoidDataLoss, _buffered));
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000387B File Offset: 0x00001A7B
		[DoesNotReturn]
		public static void ThrowArgumentException_DestinationTooShort()
		{
			throw ThrowHelper.GetArgumentException(SR.DestinationTooShort);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00003887 File Offset: 0x00001A87
		[DoesNotReturn]
		public static void ThrowArgumentException_PropertyNameTooLarge(int tokenLength)
		{
			throw ThrowHelper.GetArgumentException(SR.Format(SR.PropertyNameTooLarge, tokenLength));
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000389E File Offset: 0x00001A9E
		[DoesNotReturn]
		public static void ThrowArgumentException_ValueTooLarge(long tokenLength)
		{
			throw ThrowHelper.GetArgumentException(SR.Format(SR.ValueTooLarge, tokenLength));
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000038B5 File Offset: 0x00001AB5
		[DoesNotReturn]
		public static void ThrowArgumentException_ValueNotSupported()
		{
			throw ThrowHelper.GetArgumentException(SR.SpecialNumberValuesNotSupported);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000038C1 File Offset: 0x00001AC1
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_NeedLargerSpan()
		{
			throw ThrowHelper.GetInvalidOperationException(SR.FailedToGetLargerSpan);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000038CD File Offset: 0x00001ACD
		[DoesNotReturn]
		public static void ThrowPropertyNameTooLargeArgumentException(int length)
		{
			throw ThrowHelper.GetArgumentException(SR.Format(SR.PropertyNameTooLarge, length));
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000038E4 File Offset: 0x00001AE4
		[DoesNotReturn]
		public static void ThrowArgumentException(ReadOnlySpan<byte> propertyName, ReadOnlySpan<byte> value)
		{
			if (propertyName.Length > 166666666)
			{
				ThrowHelper.ThrowArgumentException(SR.Format(SR.PropertyNameTooLarge, propertyName.Length));
				return;
			}
			ThrowHelper.ThrowArgumentException(SR.Format(SR.ValueTooLarge, value.Length));
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00003938 File Offset: 0x00001B38
		[DoesNotReturn]
		public static void ThrowArgumentException(ReadOnlySpan<byte> propertyName, ReadOnlySpan<char> value)
		{
			if (propertyName.Length > 166666666)
			{
				ThrowHelper.ThrowArgumentException(SR.Format(SR.PropertyNameTooLarge, propertyName.Length));
				return;
			}
			ThrowHelper.ThrowArgumentException(SR.Format(SR.ValueTooLarge, value.Length));
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000398C File Offset: 0x00001B8C
		[DoesNotReturn]
		public static void ThrowArgumentException(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> value)
		{
			if (propertyName.Length > 166666666)
			{
				ThrowHelper.ThrowArgumentException(SR.Format(SR.PropertyNameTooLarge, propertyName.Length));
				return;
			}
			ThrowHelper.ThrowArgumentException(SR.Format(SR.ValueTooLarge, value.Length));
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000039E0 File Offset: 0x00001BE0
		[DoesNotReturn]
		public static void ThrowArgumentException(ReadOnlySpan<char> propertyName, ReadOnlySpan<char> value)
		{
			if (propertyName.Length > 166666666)
			{
				ThrowHelper.ThrowArgumentException(SR.Format(SR.PropertyNameTooLarge, propertyName.Length));
				return;
			}
			ThrowHelper.ThrowArgumentException(SR.Format(SR.ValueTooLarge, value.Length));
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00003A34 File Offset: 0x00001C34
		[DoesNotReturn]
		public static void ThrowInvalidOperationOrArgumentException(ReadOnlySpan<byte> propertyName, int currentDepth, int maxDepth)
		{
			currentDepth &= int.MaxValue;
			if (currentDepth >= maxDepth)
			{
				ThrowHelper.ThrowInvalidOperationException(SR.Format(SR.DepthTooLarge, currentDepth, maxDepth));
				return;
			}
			ThrowHelper.ThrowArgumentException(SR.Format(SR.PropertyNameTooLarge, propertyName.Length));
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00003A85 File Offset: 0x00001C85
		[DoesNotReturn]
		public static void ThrowInvalidOperationException(int currentDepth, int maxDepth)
		{
			currentDepth &= int.MaxValue;
			ThrowHelper.ThrowInvalidOperationException(SR.Format(SR.DepthTooLarge, currentDepth, maxDepth));
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00003AAB File Offset: 0x00001CAB
		[DoesNotReturn]
		public static void ThrowInvalidOperationException(string message)
		{
			throw ThrowHelper.GetInvalidOperationException(message);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00003AB3 File Offset: 0x00001CB3
		private static InvalidOperationException GetInvalidOperationException(string message)
		{
			return new InvalidOperationException(message)
			{
				Source = "System.Text.Json.Rethrowable"
			};
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00003AC6 File Offset: 0x00001CC6
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_DepthNonZeroOrEmptyJson(int currentDepth)
		{
			throw ThrowHelper.GetInvalidOperationException(currentDepth);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00003ACE File Offset: 0x00001CCE
		private static InvalidOperationException GetInvalidOperationException(int currentDepth)
		{
			currentDepth &= int.MaxValue;
			if (currentDepth != 0)
			{
				return ThrowHelper.GetInvalidOperationException(SR.Format(SR.ZeroDepthAtEnd, currentDepth));
			}
			return ThrowHelper.GetInvalidOperationException(SR.EmptyJsonIsInvalid);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00003AFC File Offset: 0x00001CFC
		[DoesNotReturn]
		public static void ThrowInvalidOperationOrArgumentException(ReadOnlySpan<char> propertyName, int currentDepth, int maxDepth)
		{
			currentDepth &= int.MaxValue;
			if (currentDepth >= maxDepth)
			{
				ThrowHelper.ThrowInvalidOperationException(SR.Format(SR.DepthTooLarge, currentDepth, maxDepth));
				return;
			}
			ThrowHelper.ThrowArgumentException(SR.Format(SR.PropertyNameTooLarge, propertyName.Length));
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00003B4D File Offset: 0x00001D4D
		public static InvalidOperationException GetInvalidOperationException_ExpectedArray(JsonTokenType tokenType)
		{
			return ThrowHelper.GetInvalidOperationException("array", tokenType);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00003B5A File Offset: 0x00001D5A
		public static InvalidOperationException GetInvalidOperationException_ExpectedObject(JsonTokenType tokenType)
		{
			return ThrowHelper.GetInvalidOperationException("object", tokenType);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00003B67 File Offset: 0x00001D67
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ExpectedNumber(JsonTokenType tokenType)
		{
			throw ThrowHelper.GetInvalidOperationException("number", tokenType);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00003B74 File Offset: 0x00001D74
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ExpectedBoolean(JsonTokenType tokenType)
		{
			throw ThrowHelper.GetInvalidOperationException("boolean", tokenType);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00003B81 File Offset: 0x00001D81
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ExpectedString(JsonTokenType tokenType)
		{
			throw ThrowHelper.GetInvalidOperationException("string", tokenType);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00003B8E File Offset: 0x00001D8E
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ExpectedPropertyName(JsonTokenType tokenType)
		{
			throw ThrowHelper.GetInvalidOperationException("propertyName", tokenType);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00003B9B File Offset: 0x00001D9B
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ExpectedStringComparison(JsonTokenType tokenType)
		{
			throw ThrowHelper.GetInvalidOperationException(tokenType);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00003BA3 File Offset: 0x00001DA3
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ExpectedComment(JsonTokenType tokenType)
		{
			throw ThrowHelper.GetInvalidOperationException("comment", tokenType);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00003BB0 File Offset: 0x00001DB0
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_CannotSkipOnPartial()
		{
			throw ThrowHelper.GetInvalidOperationException(SR.CannotSkip);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00003BBC File Offset: 0x00001DBC
		private static InvalidOperationException GetInvalidOperationException(string message, JsonTokenType tokenType)
		{
			return ThrowHelper.GetInvalidOperationException(SR.Format(SR.InvalidCast, tokenType, message));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00003BD4 File Offset: 0x00001DD4
		private static InvalidOperationException GetInvalidOperationException(JsonTokenType tokenType)
		{
			return ThrowHelper.GetInvalidOperationException(SR.Format(SR.InvalidComparison, tokenType));
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00003BEB File Offset: 0x00001DEB
		[DoesNotReturn]
		internal static void ThrowJsonElementWrongTypeException(JsonTokenType expectedType, JsonTokenType actualType)
		{
			throw ThrowHelper.GetJsonElementWrongTypeException(expectedType.ToValueKind(), actualType.ToValueKind());
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00003BFE File Offset: 0x00001DFE
		internal static InvalidOperationException GetJsonElementWrongTypeException(JsonValueKind expectedType, JsonValueKind actualType)
		{
			return ThrowHelper.GetInvalidOperationException(SR.Format(SR.JsonElementHasWrongType, expectedType, actualType));
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00003C1B File Offset: 0x00001E1B
		internal static InvalidOperationException GetJsonElementWrongTypeException(string expectedTypeName, JsonValueKind actualType)
		{
			return ThrowHelper.GetInvalidOperationException(SR.Format(SR.JsonElementHasWrongType, expectedTypeName, actualType));
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00003C33 File Offset: 0x00001E33
		[DoesNotReturn]
		public static void ThrowJsonReaderException(ref Utf8JsonReader json, ExceptionResource resource, byte nextByte = 0, ReadOnlySpan<byte> bytes = default(ReadOnlySpan<byte>))
		{
			throw ThrowHelper.GetJsonReaderException(ref json, resource, nextByte, bytes);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00003C40 File Offset: 0x00001E40
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static JsonException GetJsonReaderException(ref Utf8JsonReader json, ExceptionResource resource, byte nextByte, ReadOnlySpan<byte> bytes)
		{
			string text = ThrowHelper.GetResourceString(ref json, resource, nextByte, JsonHelpers.Utf8GetString(bytes));
			long lineNumber = json.CurrentState._lineNumber;
			long bytePositionInLine = json.CurrentState._bytePositionInLine;
			text += string.Format(" LineNumber: {0} | BytePositionInLine: {1}.", lineNumber, bytePositionInLine);
			return new JsonReaderException(text, lineNumber, bytePositionInLine);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00003C99 File Offset: 0x00001E99
		private static bool IsPrintable(byte value)
		{
			return value >= 32 && value < 127;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00003CA8 File Offset: 0x00001EA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string GetPrintableString(byte value)
		{
			if (!ThrowHelper.IsPrintable(value))
			{
				return string.Format("0x{0:X2}", value);
			}
			char c = (char)value;
			return c.ToString();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00003CD8 File Offset: 0x00001ED8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static string GetResourceString(ref Utf8JsonReader json, ExceptionResource resource, byte nextByte, string characters)
		{
			string printableString = ThrowHelper.GetPrintableString(nextByte);
			string text = "";
			switch (resource)
			{
			case ExceptionResource.ArrayDepthTooLarge:
				text = SR.Format(SR.ArrayDepthTooLarge, json.CurrentState.Options.MaxDepth);
				break;
			case ExceptionResource.EndOfCommentNotFound:
				text = SR.EndOfCommentNotFound;
				break;
			case ExceptionResource.EndOfStringNotFound:
				text = SR.EndOfStringNotFound;
				break;
			case ExceptionResource.RequiredDigitNotFoundAfterDecimal:
				text = SR.Format(SR.RequiredDigitNotFoundAfterDecimal, printableString);
				break;
			case ExceptionResource.RequiredDigitNotFoundAfterSign:
				text = SR.Format(SR.RequiredDigitNotFoundAfterSign, printableString);
				break;
			case ExceptionResource.RequiredDigitNotFoundEndOfData:
				text = SR.RequiredDigitNotFoundEndOfData;
				break;
			case ExceptionResource.ExpectedEndAfterSingleJson:
				text = SR.Format(SR.ExpectedEndAfterSingleJson, printableString);
				break;
			case ExceptionResource.ExpectedEndOfDigitNotFound:
				text = SR.Format(SR.ExpectedEndOfDigitNotFound, printableString);
				break;
			case ExceptionResource.ExpectedFalse:
				text = SR.Format(SR.ExpectedFalse, characters);
				break;
			case ExceptionResource.ExpectedNextDigitEValueNotFound:
				text = SR.Format(SR.ExpectedNextDigitEValueNotFound, printableString);
				break;
			case ExceptionResource.ExpectedNull:
				text = SR.Format(SR.ExpectedNull, characters);
				break;
			case ExceptionResource.ExpectedSeparatorAfterPropertyNameNotFound:
				text = SR.Format(SR.ExpectedSeparatorAfterPropertyNameNotFound, printableString);
				break;
			case ExceptionResource.ExpectedStartOfPropertyNotFound:
				text = SR.Format(SR.ExpectedStartOfPropertyNotFound, printableString);
				break;
			case ExceptionResource.ExpectedStartOfPropertyOrValueNotFound:
				text = SR.ExpectedStartOfPropertyOrValueNotFound;
				break;
			case ExceptionResource.ExpectedStartOfPropertyOrValueAfterComment:
				text = SR.Format(SR.ExpectedStartOfPropertyOrValueAfterComment, printableString);
				break;
			case ExceptionResource.ExpectedStartOfValueNotFound:
				text = SR.Format(SR.ExpectedStartOfValueNotFound, printableString);
				break;
			case ExceptionResource.ExpectedTrue:
				text = SR.Format(SR.ExpectedTrue, characters);
				break;
			case ExceptionResource.ExpectedValueAfterPropertyNameNotFound:
				text = SR.ExpectedValueAfterPropertyNameNotFound;
				break;
			case ExceptionResource.FoundInvalidCharacter:
				text = SR.Format(SR.FoundInvalidCharacter, printableString);
				break;
			case ExceptionResource.InvalidCharacterWithinString:
				text = SR.Format(SR.InvalidCharacterWithinString, printableString);
				break;
			case ExceptionResource.InvalidCharacterAfterEscapeWithinString:
				text = SR.Format(SR.InvalidCharacterAfterEscapeWithinString, printableString);
				break;
			case ExceptionResource.InvalidHexCharacterWithinString:
				text = SR.Format(SR.InvalidHexCharacterWithinString, printableString);
				break;
			case ExceptionResource.InvalidEndOfJsonNonPrimitive:
				text = SR.Format(SR.InvalidEndOfJsonNonPrimitive, json.TokenType);
				break;
			case ExceptionResource.MismatchedObjectArray:
				text = SR.Format(SR.MismatchedObjectArray, printableString);
				break;
			case ExceptionResource.ObjectDepthTooLarge:
				text = SR.Format(SR.ObjectDepthTooLarge, json.CurrentState.Options.MaxDepth);
				break;
			case ExceptionResource.ZeroDepthAtEnd:
				text = SR.Format(SR.ZeroDepthAtEnd, Array.Empty<object>());
				break;
			case ExceptionResource.ExpectedJsonTokens:
				text = SR.ExpectedJsonTokens;
				break;
			case ExceptionResource.TrailingCommaNotAllowedBeforeArrayEnd:
				text = SR.TrailingCommaNotAllowedBeforeArrayEnd;
				break;
			case ExceptionResource.TrailingCommaNotAllowedBeforeObjectEnd:
				text = SR.TrailingCommaNotAllowedBeforeObjectEnd;
				break;
			case ExceptionResource.InvalidCharacterAtStartOfComment:
				text = SR.Format(SR.InvalidCharacterAtStartOfComment, printableString);
				break;
			case ExceptionResource.UnexpectedEndOfDataWhileReadingComment:
				text = SR.Format(SR.UnexpectedEndOfDataWhileReadingComment, Array.Empty<object>());
				break;
			case ExceptionResource.UnexpectedEndOfLineSeparator:
				text = SR.Format(SR.UnexpectedEndOfLineSeparator, Array.Empty<object>());
				break;
			case ExceptionResource.ExpectedOneCompleteToken:
				text = SR.ExpectedOneCompleteToken;
				break;
			case ExceptionResource.NotEnoughData:
				text = SR.NotEnoughData;
				break;
			case ExceptionResource.InvalidLeadingZeroInNumber:
				text = SR.Format(SR.InvalidLeadingZeroInNumber, printableString);
				break;
			}
			return text;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00003FE0 File Offset: 0x000021E0
		[DoesNotReturn]
		public static void ThrowInvalidOperationException(ExceptionResource resource, int currentDepth, int maxDepth, byte token, JsonTokenType tokenType)
		{
			throw ThrowHelper.GetInvalidOperationException(resource, currentDepth, maxDepth, token, tokenType);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00003FED File Offset: 0x000021ED
		[DoesNotReturn]
		public static void ThrowArgumentException_InvalidCommentValue()
		{
			throw new ArgumentException(SR.CannotWriteCommentWithEmbeddedDelimiter);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00003FFC File Offset: 0x000021FC
		[DoesNotReturn]
		public unsafe static void ThrowArgumentException_InvalidUTF8(ReadOnlySpan<byte> value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = Math.Min(value.Length, 10);
			for (int i = 0; i < num; i++)
			{
				byte b = *value[i];
				if (ThrowHelper.IsPrintable(b))
				{
					stringBuilder.Append((char)b);
				}
				else
				{
					stringBuilder.Append(string.Format("0x{0:X2}", b));
				}
			}
			if (num < value.Length)
			{
				stringBuilder.Append("...");
			}
			throw new ArgumentException(SR.Format(SR.CannotEncodeInvalidUTF8, stringBuilder));
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00004083 File Offset: 0x00002283
		[DoesNotReturn]
		public static void ThrowArgumentException_InvalidUTF16(int charAsInt)
		{
			throw new ArgumentException(SR.Format(SR.CannotEncodeInvalidUTF16, string.Format("0x{0:X2}", charAsInt)));
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000040A4 File Offset: 0x000022A4
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ReadInvalidUTF16(int charAsInt)
		{
			throw ThrowHelper.GetInvalidOperationException(SR.Format(SR.CannotReadInvalidUTF16, string.Format("0x{0:X2}", charAsInt)));
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000040C5 File Offset: 0x000022C5
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ReadIncompleteUTF16()
		{
			throw ThrowHelper.GetInvalidOperationException(SR.CannotReadIncompleteUTF16);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000040D1 File Offset: 0x000022D1
		public static InvalidOperationException GetInvalidOperationException_ReadInvalidUTF8(DecoderFallbackException innerException = null)
		{
			return ThrowHelper.GetInvalidOperationException(SR.CannotTranscodeInvalidUtf8, innerException);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000040DE File Offset: 0x000022DE
		public static ArgumentException GetArgumentException_ReadInvalidUTF16(EncoderFallbackException innerException)
		{
			return new ArgumentException(SR.CannotTranscodeInvalidUtf16, innerException);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000040EC File Offset: 0x000022EC
		public static InvalidOperationException GetInvalidOperationException(string message, Exception innerException)
		{
			return new InvalidOperationException(message, innerException)
			{
				Source = "System.Text.Json.Rethrowable"
			};
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00004110 File Offset: 0x00002310
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static InvalidOperationException GetInvalidOperationException(ExceptionResource resource, int currentDepth, int maxDepth, byte token, JsonTokenType tokenType)
		{
			string resourceString = ThrowHelper.GetResourceString(resource, currentDepth, maxDepth, token, tokenType);
			InvalidOperationException invalidOperationException = ThrowHelper.GetInvalidOperationException(resourceString);
			invalidOperationException.Source = "System.Text.Json.Rethrowable";
			return invalidOperationException;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000413C File Offset: 0x0000233C
		[DoesNotReturn]
		public static void ThrowOutOfMemoryException(uint capacity)
		{
			throw new OutOfMemoryException(SR.Format(SR.BufferMaximumSizeExceeded, capacity));
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00004154 File Offset: 0x00002354
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static string GetResourceString(ExceptionResource resource, int currentDepth, int maxDepth, byte token, JsonTokenType tokenType)
		{
			string text = "";
			switch (resource)
			{
			case ExceptionResource.MismatchedObjectArray:
				text = ((tokenType == JsonTokenType.PropertyName) ? SR.Format(SR.CannotWriteEndAfterProperty, (char)token) : SR.Format(SR.MismatchedObjectArray, (char)token));
				break;
			case ExceptionResource.DepthTooLarge:
				text = SR.Format(SR.DepthTooLarge, currentDepth & int.MaxValue, maxDepth);
				break;
			case ExceptionResource.CannotStartObjectArrayWithoutProperty:
				text = SR.Format(SR.CannotStartObjectArrayWithoutProperty, tokenType);
				break;
			case ExceptionResource.CannotStartObjectArrayAfterPrimitiveOrClose:
				text = SR.Format(SR.CannotStartObjectArrayAfterPrimitiveOrClose, tokenType);
				break;
			case ExceptionResource.CannotWriteValueWithinObject:
				text = SR.Format(SR.CannotWriteValueWithinObject, tokenType);
				break;
			case ExceptionResource.CannotWriteValueAfterPrimitiveOrClose:
				text = SR.Format(SR.CannotWriteValueAfterPrimitiveOrClose, tokenType);
				break;
			case ExceptionResource.CannotWritePropertyWithinArray:
				text = ((tokenType == JsonTokenType.PropertyName) ? SR.Format(SR.CannotWritePropertyAfterProperty, Array.Empty<object>()) : SR.Format(SR.CannotWritePropertyWithinArray, tokenType));
				break;
			}
			return text;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000425E File Offset: 0x0000245E
		[DoesNotReturn]
		public static void ThrowFormatException()
		{
			throw new FormatException
			{
				Source = "System.Text.Json.Rethrowable"
			};
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00004270 File Offset: 0x00002470
		public static void ThrowFormatException(NumericType numericType)
		{
			string text = "";
			switch (numericType)
			{
			case NumericType.Byte:
				text = SR.FormatByte;
				break;
			case NumericType.SByte:
				text = SR.FormatSByte;
				break;
			case NumericType.Int16:
				text = SR.FormatInt16;
				break;
			case NumericType.Int32:
				text = SR.FormatInt32;
				break;
			case NumericType.Int64:
				text = SR.FormatInt64;
				break;
			case NumericType.Int128:
				text = SR.FormatInt128;
				break;
			case NumericType.UInt16:
				text = SR.FormatUInt16;
				break;
			case NumericType.UInt32:
				text = SR.FormatUInt32;
				break;
			case NumericType.UInt64:
				text = SR.FormatUInt64;
				break;
			case NumericType.UInt128:
				text = SR.FormatUInt128;
				break;
			case NumericType.Half:
				text = SR.FormatHalf;
				break;
			case NumericType.Single:
				text = SR.FormatSingle;
				break;
			case NumericType.Double:
				text = SR.FormatDouble;
				break;
			case NumericType.Decimal:
				text = SR.FormatDecimal;
				break;
			}
			throw new FormatException(text)
			{
				Source = "System.Text.Json.Rethrowable"
			};
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00004344 File Offset: 0x00002544
		[DoesNotReturn]
		public static void ThrowFormatException(DataType dataType)
		{
			string text = "";
			switch (dataType)
			{
			case DataType.Boolean:
			case DataType.DateOnly:
			case DataType.DateTime:
			case DataType.DateTimeOffset:
			case DataType.TimeOnly:
			case DataType.TimeSpan:
			case DataType.Guid:
			case DataType.Version:
				text = SR.Format(SR.UnsupportedFormat, dataType);
				break;
			case DataType.Base64String:
				text = SR.CannotDecodeInvalidBase64;
				break;
			}
			throw new FormatException(text)
			{
				Source = "System.Text.Json.Rethrowable"
			};
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000043AD File Offset: 0x000025AD
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ExpectedChar(JsonTokenType tokenType)
		{
			throw ThrowHelper.GetInvalidOperationException("char", tokenType);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000043BA File Offset: 0x000025BA
		[DoesNotReturn]
		public static void ThrowObjectDisposedException_Utf8JsonWriter()
		{
			throw new ObjectDisposedException("Utf8JsonWriter");
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000043C6 File Offset: 0x000025C6
		[DoesNotReturn]
		public static void ThrowObjectDisposedException_JsonDocument()
		{
			throw new ObjectDisposedException("JsonDocument");
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000043D2 File Offset: 0x000025D2
		[DoesNotReturn]
		public static void ThrowArgumentException_NodeValueNotAllowed(string paramName)
		{
			throw new ArgumentException(SR.NodeValueNotAllowed, paramName);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000043DF File Offset: 0x000025DF
		[DoesNotReturn]
		public static void ThrowArgumentException_DuplicateKey(string paramName, string propertyName)
		{
			throw new ArgumentException(SR.Format(SR.NodeDuplicateKey, propertyName), paramName);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000043F2 File Offset: 0x000025F2
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_NodeAlreadyHasParent()
		{
			throw new InvalidOperationException(SR.NodeAlreadyHasParent);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000043FE File Offset: 0x000025FE
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_NodeCycleDetected()
		{
			throw new InvalidOperationException(SR.NodeCycleDetected);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000440A File Offset: 0x0000260A
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_NodeElementCannotBeObjectOrArray()
		{
			throw new InvalidOperationException(SR.NodeElementCannotBeObjectOrArray);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00004416 File Offset: 0x00002616
		[DoesNotReturn]
		public static void ThrowNotSupportedException_CollectionIsReadOnly()
		{
			throw ThrowHelper.GetNotSupportedException_CollectionIsReadOnly();
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000441D File Offset: 0x0000261D
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_NodeWrongType(string typeName)
		{
			throw new InvalidOperationException(SR.Format(SR.NodeWrongType, typeName));
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000442F File Offset: 0x0000262F
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_NodeParentWrongType(string typeName)
		{
			throw new InvalidOperationException(SR.Format(SR.NodeParentWrongType, typeName));
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00004441 File Offset: 0x00002641
		public static NotSupportedException GetNotSupportedException_CollectionIsReadOnly()
		{
			return new NotSupportedException(SR.CollectionIsReadOnly);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000444D File Offset: 0x0000264D
		[DoesNotReturn]
		public static void ThrowArgumentException_DeserializeWrongType(Type type, object value)
		{
			throw new ArgumentException(SR.Format(SR.DeserializeWrongType, type, value.GetType()));
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00004465 File Offset: 0x00002665
		[DoesNotReturn]
		public static void ThrowArgumentException_SerializerDoesNotSupportComments(string paramName)
		{
			throw new ArgumentException(SR.JsonSerializerDoesNotSupportComments, paramName);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00004472 File Offset: 0x00002672
		[DoesNotReturn]
		public static void ThrowNotSupportedException_SerializationNotSupported(Type propertyType)
		{
			throw new NotSupportedException(SR.Format(SR.SerializationNotSupportedType, propertyType));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00004484 File Offset: 0x00002684
		[DoesNotReturn]
		public static void ThrowNotSupportedException_TypeRequiresAsyncSerialization(Type propertyType)
		{
			throw new NotSupportedException(SR.Format(SR.TypeRequiresAsyncSerialization, propertyType));
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00004496 File Offset: 0x00002696
		[DoesNotReturn]
		public static void ThrowNotSupportedException_DictionaryKeyTypeNotSupported(Type keyType, JsonConverter converter)
		{
			throw new NotSupportedException(SR.Format(SR.DictionaryKeyTypeNotSupported, keyType, converter.GetType()));
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000044AE File Offset: 0x000026AE
		[DoesNotReturn]
		public static void ThrowJsonException_DeserializeUnableToConvertValue(Type propertyType)
		{
			throw new JsonException(SR.Format(SR.DeserializeUnableToConvertValue, propertyType))
			{
				AppendPathInformation = true
			};
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000044C7 File Offset: 0x000026C7
		[DoesNotReturn]
		public static void ThrowInvalidCastException_DeserializeUnableToAssignValue(Type typeOfValue, Type declaredType)
		{
			throw new InvalidCastException(SR.Format(SR.DeserializeUnableToAssignValue, typeOfValue, declaredType));
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000044DA File Offset: 0x000026DA
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_DeserializeUnableToAssignNull(Type declaredType)
		{
			throw new InvalidOperationException(SR.Format(SR.DeserializeUnableToAssignNull, declaredType));
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000044EC File Offset: 0x000026EC
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ObjectCreationHandlingPopulateNotSupportedByConverter(JsonPropertyInfo propertyInfo)
		{
			throw new InvalidOperationException(SR.Format(SR.ObjectCreationHandlingPopulateNotSupportedByConverter, propertyInfo.Name, propertyInfo.DeclaringType));
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00004509 File Offset: 0x00002709
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ObjectCreationHandlingPropertyMustHaveAGetter(JsonPropertyInfo propertyInfo)
		{
			throw new InvalidOperationException(SR.Format(SR.ObjectCreationHandlingPropertyMustHaveAGetter, propertyInfo.Name, propertyInfo.DeclaringType));
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00004526 File Offset: 0x00002726
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ObjectCreationHandlingPropertyValueTypeMustHaveASetter(JsonPropertyInfo propertyInfo)
		{
			throw new InvalidOperationException(SR.Format(SR.ObjectCreationHandlingPropertyValueTypeMustHaveASetter, propertyInfo.Name, propertyInfo.DeclaringType));
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00004543 File Offset: 0x00002743
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ObjectCreationHandlingPropertyCannotAllowPolymorphicDeserialization(JsonPropertyInfo propertyInfo)
		{
			throw new InvalidOperationException(SR.Format(SR.ObjectCreationHandlingPropertyCannotAllowPolymorphicDeserialization, propertyInfo.Name, propertyInfo.DeclaringType));
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00004560 File Offset: 0x00002760
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ObjectCreationHandlingPropertyCannotAllowReadOnlyMember(JsonPropertyInfo propertyInfo)
		{
			throw new InvalidOperationException(SR.Format(SR.ObjectCreationHandlingPropertyCannotAllowReadOnlyMember, propertyInfo.Name, propertyInfo.DeclaringType));
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000457D File Offset: 0x0000277D
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ObjectCreationHandlingPropertyCannotAllowReferenceHandling()
		{
			throw new InvalidOperationException(SR.ObjectCreationHandlingPropertyCannotAllowReferenceHandling);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00004589 File Offset: 0x00002789
		[DoesNotReturn]
		public static void ThrowNotSupportedException_ObjectCreationHandlingPropertyDoesNotSupportParameterizedConstructors()
		{
			throw new NotSupportedException(SR.ObjectCreationHandlingPropertyDoesNotSupportParameterizedConstructors);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00004595 File Offset: 0x00002795
		[DoesNotReturn]
		public static void ThrowJsonException_SerializationConverterRead(JsonConverter converter)
		{
			throw new JsonException(SR.Format(SR.SerializationConverterRead, converter))
			{
				AppendPathInformation = true
			};
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000045AE File Offset: 0x000027AE
		[DoesNotReturn]
		public static void ThrowJsonException_SerializationConverterWrite(JsonConverter converter)
		{
			throw new JsonException(SR.Format(SR.SerializationConverterWrite, converter))
			{
				AppendPathInformation = true
			};
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000045C7 File Offset: 0x000027C7
		[DoesNotReturn]
		public static void ThrowJsonException_SerializerCycleDetected(int maxDepth)
		{
			throw new JsonException(SR.Format(SR.SerializerCycleDetected, maxDepth))
			{
				AppendPathInformation = true
			};
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000045E5 File Offset: 0x000027E5
		[DoesNotReturn]
		public static void ThrowJsonException(string message = null)
		{
			throw new JsonException(message)
			{
				AppendPathInformation = true
			};
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000045F4 File Offset: 0x000027F4
		[DoesNotReturn]
		public static void ThrowArgumentException_CannotSerializeInvalidType(string paramName, Type typeToConvert, Type declaringType, string propertyName)
		{
			if (declaringType == null)
			{
				throw new ArgumentException(SR.Format(SR.CannotSerializeInvalidType, typeToConvert), paramName);
			}
			throw new ArgumentException(SR.Format(SR.CannotSerializeInvalidMember, typeToConvert, propertyName, declaringType), paramName);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00004624 File Offset: 0x00002824
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_CannotSerializeInvalidType(Type typeToConvert, Type declaringType, MemberInfo memberInfo)
		{
			if (declaringType == null)
			{
				throw new InvalidOperationException(SR.Format(SR.CannotSerializeInvalidType, typeToConvert));
			}
			throw new InvalidOperationException(SR.Format(SR.CannotSerializeInvalidMember, typeToConvert, memberInfo.Name, declaringType));
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00004657 File Offset: 0x00002857
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_SerializationConverterNotCompatible(Type converterType, Type type)
		{
			throw new InvalidOperationException(SR.Format(SR.SerializationConverterNotCompatible, converterType, type));
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000466A File Offset: 0x0000286A
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ResolverTypeNotCompatible(Type requestedType, Type actualType)
		{
			throw new InvalidOperationException(SR.Format(SR.ResolverTypeNotCompatible, actualType, requestedType));
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000467D File Offset: 0x0000287D
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ResolverTypeInfoOptionsNotCompatible()
		{
			throw new InvalidOperationException(SR.ResolverTypeInfoOptionsNotCompatible);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00004689 File Offset: 0x00002889
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_JsonSerializerOptionsNoTypeInfoResolverSpecified()
		{
			throw new InvalidOperationException(SR.JsonSerializerOptionsNoTypeInfoResolverSpecified);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00004695 File Offset: 0x00002895
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_JsonSerializerIsReflectionDisabled()
		{
			throw new InvalidOperationException(SR.JsonSerializerIsReflectionDisabled);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000046A4 File Offset: 0x000028A4
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_SerializationConverterOnAttributeInvalid(Type classType, MemberInfo memberInfo)
		{
			string text = classType.ToString();
			if (memberInfo != null)
			{
				text = text + "." + memberInfo.Name;
			}
			throw new InvalidOperationException(SR.Format(SR.SerializationConverterOnAttributeInvalid, text));
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000046E4 File Offset: 0x000028E4
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_SerializationConverterOnAttributeNotCompatible(Type classTypeAttributeIsOn, MemberInfo memberInfo, Type typeToConvert)
		{
			string text = classTypeAttributeIsOn.ToString();
			if (memberInfo != null)
			{
				text = text + "." + memberInfo.Name;
			}
			throw new InvalidOperationException(SR.Format(SR.SerializationConverterOnAttributeNotCompatible, text, typeToConvert));
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00004724 File Offset: 0x00002924
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_SerializerOptionsReadOnly(JsonSerializerContext context)
		{
			string text = ((context == null) ? SR.SerializerOptionsReadOnly : SR.SerializerContextOptionsReadOnly);
			throw new InvalidOperationException(text);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00004747 File Offset: 0x00002947
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_DefaultTypeInfoResolverImmutable()
		{
			throw new InvalidOperationException(SR.DefaultTypeInfoResolverImmutable);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00004753 File Offset: 0x00002953
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_TypeInfoResolverChainImmutable()
		{
			throw new InvalidOperationException(SR.TypeInfoResolverChainImmutable);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000475F File Offset: 0x0000295F
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_TypeInfoImmutable()
		{
			throw new InvalidOperationException(SR.TypeInfoImmutable);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000476B File Offset: 0x0000296B
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_InvalidChainedResolver()
		{
			throw new InvalidOperationException(SR.SerializerOptions_InvalidChainedResolver);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00004777 File Offset: 0x00002977
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_SerializerPropertyNameConflict(Type type, string propertyName)
		{
			throw new InvalidOperationException(SR.Format(SR.SerializerPropertyNameConflict, type, propertyName));
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000478A File Offset: 0x0000298A
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_SerializerPropertyNameNull(JsonPropertyInfo jsonPropertyInfo)
		{
			throw new InvalidOperationException(SR.Format(SR.SerializerPropertyNameNull, jsonPropertyInfo.DeclaringType, jsonPropertyInfo.MemberName));
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000047A7 File Offset: 0x000029A7
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_JsonPropertyRequiredAndNotDeserializable(JsonPropertyInfo jsonPropertyInfo)
		{
			throw new InvalidOperationException(SR.Format(SR.JsonPropertyRequiredAndNotDeserializable, jsonPropertyInfo.Name, jsonPropertyInfo.DeclaringType));
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000047C4 File Offset: 0x000029C4
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_JsonPropertyRequiredAndExtensionData(JsonPropertyInfo jsonPropertyInfo)
		{
			throw new InvalidOperationException(SR.Format(SR.JsonPropertyRequiredAndExtensionData, jsonPropertyInfo.Name, jsonPropertyInfo.DeclaringType));
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000047E4 File Offset: 0x000029E4
		[DoesNotReturn]
		public static void ThrowJsonException_JsonRequiredPropertyMissing(JsonTypeInfo parent, BitArray requiredPropertiesSet)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (KeyValuePair<string, JsonPropertyInfo> keyValuePair in parent.PropertyCache.List)
			{
				JsonPropertyInfo value = keyValuePair.Value;
				if (value.IsRequired && !requiredPropertiesSet[value.RequiredPropertyIndex])
				{
					if (!flag)
					{
						stringBuilder.Append(CultureInfo.CurrentUICulture.TextInfo.ListSeparator);
						stringBuilder.Append(' ');
					}
					stringBuilder.Append(value.Name);
					flag = false;
					if (stringBuilder.Length >= 50)
					{
						break;
					}
				}
			}
			throw new JsonException(SR.Format(SR.JsonRequiredPropertiesMissing, parent.Type, stringBuilder.ToString()));
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000048B8 File Offset: 0x00002AB8
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_NamingPolicyReturnNull(JsonNamingPolicy namingPolicy)
		{
			throw new InvalidOperationException(SR.Format(SR.NamingPolicyReturnNull, namingPolicy));
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000048CA File Offset: 0x00002ACA
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_SerializerConverterFactoryReturnsNull(Type converterType)
		{
			throw new InvalidOperationException(SR.Format(SR.SerializerConverterFactoryReturnsNull, converterType));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000048DC File Offset: 0x00002ADC
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_SerializerConverterFactoryReturnsJsonConverterFactorty(Type converterType)
		{
			throw new InvalidOperationException(SR.Format(SR.SerializerConverterFactoryReturnsJsonConverterFactory, converterType));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000048EE File Offset: 0x00002AEE
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_MultiplePropertiesBindToConstructorParameters(Type parentType, string parameterName, string firstMatchName, string secondMatchName)
		{
			throw new InvalidOperationException(SR.Format(SR.MultipleMembersBindWithConstructorParameter, new object[] { firstMatchName, secondMatchName, parentType, parameterName }));
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00004915 File Offset: 0x00002B15
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ConstructorParameterIncompleteBinding(Type parentType)
		{
			throw new InvalidOperationException(SR.Format(SR.ConstructorParamIncompleteBinding, parentType));
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00004927 File Offset: 0x00002B27
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ExtensionDataCannotBindToCtorParam(string propertyName, JsonPropertyInfo jsonPropertyInfo)
		{
			throw new InvalidOperationException(SR.Format(SR.ExtensionDataCannotBindToCtorParam, propertyName, jsonPropertyInfo.DeclaringType));
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000493F File Offset: 0x00002B3F
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_JsonIncludeOnInaccessibleProperty(string memberName, Type declaringType)
		{
			throw new InvalidOperationException(SR.Format(SR.JsonIncludeOnInaccessibleProperty, memberName, declaringType));
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00004952 File Offset: 0x00002B52
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_IgnoreConditionOnValueTypeInvalid(string clrPropertyName, Type propertyDeclaringType)
		{
			throw new InvalidOperationException(SR.Format(SR.IgnoreConditionOnValueTypeInvalid, clrPropertyName, propertyDeclaringType));
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00004965 File Offset: 0x00002B65
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_NumberHandlingOnPropertyInvalid(JsonPropertyInfo jsonPropertyInfo)
		{
			throw new InvalidOperationException(SR.Format(SR.NumberHandlingOnPropertyInvalid, jsonPropertyInfo.MemberName, jsonPropertyInfo.DeclaringType));
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00004982 File Offset: 0x00002B82
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ConverterCanConvertMultipleTypes(Type runtimePropertyType, JsonConverter jsonConverter)
		{
			throw new InvalidOperationException(SR.Format(SR.ConverterCanConvertMultipleTypes, jsonConverter.GetType(), jsonConverter.Type, runtimePropertyType));
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000049A0 File Offset: 0x00002BA0
		[DoesNotReturn]
		public static void ThrowNotSupportedException_ObjectWithParameterizedCtorRefMetadataNotSupported(ReadOnlySpan<byte> propertyName, ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state)
		{
			JsonTypeInfo topJsonTypeInfoWithParameterizedConstructor = state.GetTopJsonTypeInfoWithParameterizedConstructor();
			state.Current.JsonPropertyName = propertyName.ToArray();
			NotSupportedException ex = new NotSupportedException(SR.Format(SR.ObjectWithParameterizedCtorRefMetadataNotSupported, topJsonTypeInfoWithParameterizedConstructor.Type));
			ThrowHelper.ThrowNotSupportedException(ref state, in reader, ex);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000049E4 File Offset: 0x00002BE4
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_JsonTypeInfoOperationNotPossibleForKind(JsonTypeInfoKind kind)
		{
			throw new InvalidOperationException(SR.Format(SR.InvalidJsonTypeInfoOperationForKind, kind));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000049FB File Offset: 0x00002BFB
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_CreateObjectConverterNotCompatible(Type type)
		{
			throw new InvalidOperationException(SR.Format(SR.CreateObjectConverterNotCompatible, type));
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00004A10 File Offset: 0x00002C10
		[DoesNotReturn]
		public static void ReThrowWithPath([ScopedRef] ref ReadStack state, JsonReaderException ex)
		{
			string text = state.JsonPath();
			string text2 = ex.Message;
			int num = text2.LastIndexOf(" LineNumber: ", StringComparison.Ordinal);
			if (num >= 0)
			{
				text2 = string.Concat(new string[]
				{
					text2.Substring(0, num),
					" Path: ",
					text,
					" |",
					text2.Substring(num)
				});
			}
			else
			{
				text2 = text2 + " Path: " + text + ".";
			}
			throw new JsonException(text2, text, ex.LineNumber, ex.BytePositionInLine, ex);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00004A9C File Offset: 0x00002C9C
		[DoesNotReturn]
		public static void ReThrowWithPath([ScopedRef] ref ReadStack state, in Utf8JsonReader reader, Exception ex)
		{
			JsonException ex2 = new JsonException(null, ex);
			ThrowHelper.AddJsonExceptionInformation(ref state, in reader, ex2);
			throw ex2;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00004ABC File Offset: 0x00002CBC
		public static void AddJsonExceptionInformation([ScopedRef] ref ReadStack state, in Utf8JsonReader reader, JsonException ex)
		{
			long lineNumber = reader.CurrentState._lineNumber;
			ex.LineNumber = new long?(lineNumber);
			long bytePositionInLine = reader.CurrentState._bytePositionInLine;
			ex.BytePositionInLine = new long?(bytePositionInLine);
			string text = state.JsonPath();
			ex.Path = text;
			string text2 = ex._message;
			if (string.IsNullOrEmpty(text2))
			{
				JsonPropertyInfo jsonPropertyInfo = state.Current.JsonPropertyInfo;
				Type type = ((jsonPropertyInfo != null) ? jsonPropertyInfo.PropertyType : null) ?? state.Current.JsonTypeInfo.Type;
				text2 = SR.Format(SR.DeserializeUnableToConvertValue, type);
				ex.AppendPathInformation = true;
			}
			if (ex.AppendPathInformation)
			{
				text2 += string.Format(" Path: {0} | LineNumber: {1} | BytePositionInLine: {2}.", text, lineNumber, bytePositionInLine);
				ex.SetMessage(text2);
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00004B84 File Offset: 0x00002D84
		[DoesNotReturn]
		public static void ReThrowWithPath(ref WriteStack state, Exception ex)
		{
			JsonException ex2 = new JsonException(null, ex);
			ThrowHelper.AddJsonExceptionInformation(ref state, ex2);
			throw ex2;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00004BA4 File Offset: 0x00002DA4
		public static void AddJsonExceptionInformation(ref WriteStack state, JsonException ex)
		{
			string text = state.PropertyPath();
			ex.Path = text;
			string text2 = ex._message;
			if (string.IsNullOrEmpty(text2))
			{
				text2 = SR.Format(SR.SerializeUnableToSerialize, Array.Empty<object>());
				ex.AppendPathInformation = true;
			}
			if (ex.AppendPathInformation)
			{
				text2 = text2 + " Path: " + text + ".";
				ex.SetMessage(text2);
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00004C08 File Offset: 0x00002E08
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_SerializationDuplicateAttribute(Type attribute, MemberInfo memberInfo)
		{
			Type type = memberInfo as Type;
			string text = ((type != null) ? type.ToString() : string.Format("{0}.{1}", memberInfo.DeclaringType, memberInfo.Name));
			throw new InvalidOperationException(SR.Format(SR.SerializationDuplicateAttribute, attribute, text));
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00004C4F File Offset: 0x00002E4F
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_SerializationDuplicateTypeAttribute(Type classType, Type attribute)
		{
			throw new InvalidOperationException(SR.Format(SR.SerializationDuplicateTypeAttribute, classType, attribute));
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00004C62 File Offset: 0x00002E62
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_SerializationDuplicateTypeAttribute<TAttribute>(Type classType)
		{
			throw new InvalidOperationException(SR.Format(SR.SerializationDuplicateTypeAttribute, classType, typeof(TAttribute)));
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00004C7E File Offset: 0x00002E7E
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_ExtensionDataConflictsWithUnmappedMemberHandling(Type classType, JsonPropertyInfo jsonPropertyInfo)
		{
			throw new InvalidOperationException(SR.Format(SR.ExtensionDataConflictsWithUnmappedMemberHandling, classType, jsonPropertyInfo.MemberName));
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00004C96 File Offset: 0x00002E96
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_SerializationDataExtensionPropertyInvalid(JsonPropertyInfo jsonPropertyInfo)
		{
			throw new InvalidOperationException(SR.Format(SR.SerializationDataExtensionPropertyInvalid, jsonPropertyInfo.PropertyType, jsonPropertyInfo.MemberName));
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00004CB3 File Offset: 0x00002EB3
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_NodeJsonObjectCustomConverterNotAllowedOnExtensionProperty()
		{
			throw new InvalidOperationException(SR.NodeJsonObjectCustomConverterNotAllowedOnExtensionProperty);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00004CC0 File Offset: 0x00002EC0
		[DoesNotReturn]
		public static void ThrowNotSupportedException([ScopedRef] ref ReadStack state, in Utf8JsonReader reader, NotSupportedException ex)
		{
			string text = ex.Message;
			JsonPropertyInfo jsonPropertyInfo = state.Current.JsonPropertyInfo;
			Type type = ((jsonPropertyInfo != null) ? jsonPropertyInfo.PropertyType : null) ?? state.Current.JsonTypeInfo.Type;
			if (!text.Contains(type.ToString()))
			{
				if (text.Length > 0)
				{
					text += " ";
				}
				text += SR.Format(SR.SerializationNotSupportedParentType, type);
			}
			long lineNumber = reader.CurrentState._lineNumber;
			long bytePositionInLine = reader.CurrentState._bytePositionInLine;
			text += string.Format(" Path: {0} | LineNumber: {1} | BytePositionInLine: {2}.", state.JsonPath(), lineNumber, bytePositionInLine);
			throw new NotSupportedException(text, ex);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00004D78 File Offset: 0x00002F78
		[DoesNotReturn]
		public static void ThrowNotSupportedException(ref WriteStack state, NotSupportedException ex)
		{
			string text = ex.Message;
			JsonPropertyInfo jsonPropertyInfo = state.Current.JsonPropertyInfo;
			Type type = ((jsonPropertyInfo != null) ? jsonPropertyInfo.PropertyType : null) ?? state.Current.JsonTypeInfo.Type;
			if (!text.Contains(type.ToString()))
			{
				if (text.Length > 0)
				{
					text += " ";
				}
				text += SR.Format(SR.SerializationNotSupportedParentType, type);
			}
			text = text + " Path: " + state.PropertyPath() + ".";
			throw new NotSupportedException(text, ex);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00004E0C File Offset: 0x0000300C
		[DoesNotReturn]
		public static void ThrowNotSupportedException_DeserializeNoConstructor(Type type, ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state)
		{
			string text;
			if (type.IsInterface)
			{
				text = SR.Format(SR.DeserializePolymorphicInterface, type);
			}
			else
			{
				text = SR.Format(SR.DeserializeNoConstructor, "JsonConstructorAttribute", type);
			}
			ThrowHelper.ThrowNotSupportedException(ref state, in reader, new NotSupportedException(text));
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00004E4D File Offset: 0x0000304D
		[DoesNotReturn]
		public static void ThrowNotSupportedException_CannotPopulateCollection(Type type, ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state)
		{
			ThrowHelper.ThrowNotSupportedException(ref state, in reader, new NotSupportedException(SR.Format(SR.CannotPopulateCollection, type)));
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00004E66 File Offset: 0x00003066
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataValuesInvalidToken(JsonTokenType tokenType)
		{
			ThrowHelper.ThrowJsonException(SR.Format(SR.MetadataInvalidTokenAfterValues, tokenType));
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00004E7D File Offset: 0x0000307D
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataReferenceNotFound(string id)
		{
			ThrowHelper.ThrowJsonException(SR.Format(SR.MetadataReferenceNotFound, id));
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00004E8F File Offset: 0x0000308F
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataValueWasNotString(JsonTokenType tokenType)
		{
			ThrowHelper.ThrowJsonException(SR.Format(SR.MetadataValueWasNotString, tokenType));
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00004EA6 File Offset: 0x000030A6
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataValueWasNotString(JsonValueKind valueKind)
		{
			ThrowHelper.ThrowJsonException(SR.Format(SR.MetadataValueWasNotString, valueKind));
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00004EBD File Offset: 0x000030BD
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataReferenceObjectCannotContainOtherProperties(ReadOnlySpan<byte> propertyName, [ScopedRef] ref ReadStack state)
		{
			state.Current.JsonPropertyName = propertyName.ToArray();
			ThrowHelper.ThrowJsonException_MetadataReferenceObjectCannotContainOtherProperties();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00004ED6 File Offset: 0x000030D6
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataUnexpectedProperty(ReadOnlySpan<byte> propertyName, [ScopedRef] ref ReadStack state)
		{
			state.Current.JsonPropertyName = propertyName.ToArray();
			ThrowHelper.ThrowJsonException(SR.Format(SR.MetadataUnexpectedProperty, Array.Empty<object>()));
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00004EFE File Offset: 0x000030FE
		[DoesNotReturn]
		public static void ThrowJsonException_UnmappedJsonProperty(Type type, string unmappedPropertyName)
		{
			throw new JsonException(SR.Format(SR.UnmappedJsonProperty, unmappedPropertyName, type));
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00004F11 File Offset: 0x00003111
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataReferenceObjectCannotContainOtherProperties()
		{
			ThrowHelper.ThrowJsonException(SR.MetadataReferenceCannotContainOtherProperties);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00004F1D File Offset: 0x0000311D
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataIdIsNotFirstProperty(ReadOnlySpan<byte> propertyName, [ScopedRef] ref ReadStack state)
		{
			state.Current.JsonPropertyName = propertyName.ToArray();
			ThrowHelper.ThrowJsonException(SR.MetadataIdIsNotFirstProperty);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00004F3B File Offset: 0x0000313B
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataStandaloneValuesProperty([ScopedRef] ref ReadStack state, ReadOnlySpan<byte> propertyName)
		{
			state.Current.JsonPropertyName = propertyName.ToArray();
			ThrowHelper.ThrowJsonException(SR.MetadataStandaloneValuesProperty);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00004F5C File Offset: 0x0000315C
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataInvalidPropertyWithLeadingDollarSign(ReadOnlySpan<byte> propertyName, [ScopedRef] ref ReadStack state, in Utf8JsonReader reader)
		{
			if (state.Current.IsProcessingDictionary())
			{
				Utf8JsonReader utf8JsonReader = reader;
				state.Current.JsonPropertyNameAsString = utf8JsonReader.GetString();
			}
			else
			{
				state.Current.JsonPropertyName = propertyName.ToArray();
			}
			ThrowHelper.ThrowJsonException(SR.MetadataInvalidPropertyWithLeadingDollarSign);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00004FAD File Offset: 0x000031AD
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataDuplicateIdFound(string id)
		{
			ThrowHelper.ThrowJsonException(SR.Format(SR.MetadataDuplicateIdFound, id));
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00004FBF File Offset: 0x000031BF
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataDuplicateTypeProperty()
		{
			ThrowHelper.ThrowJsonException(SR.MetadataDuplicateTypeProperty);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00004FCB File Offset: 0x000031CB
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataInvalidReferenceToValueType(Type propertyType)
		{
			ThrowHelper.ThrowJsonException(SR.Format(SR.MetadataInvalidReferenceToValueType, propertyType));
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00004FE0 File Offset: 0x000031E0
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataInvalidPropertyInArrayMetadata([ScopedRef] ref ReadStack state, Type propertyType, in Utf8JsonReader reader)
		{
			byte[] array;
			if (!reader.HasValueSequence)
			{
				array = reader.ValueSpan.ToArray();
			}
			else
			{
				ReadOnlySequence<byte> valueSequence = reader.ValueSequence;
				array = (in valueSequence).ToArray<byte>();
			}
			state.Current.JsonPropertyName = array;
			Utf8JsonReader utf8JsonReader = reader;
			string @string = utf8JsonReader.GetString();
			ThrowHelper.ThrowJsonException(SR.Format(SR.MetadataPreservedArrayFailed, SR.Format(SR.MetadataInvalidPropertyInArrayMetadata, @string), SR.Format(SR.DeserializeUnableToConvertValue, propertyType)));
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00005052 File Offset: 0x00003252
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataPreservedArrayValuesNotFound([ScopedRef] ref ReadStack state, Type propertyType)
		{
			state.Current.JsonPropertyName = null;
			ThrowHelper.ThrowJsonException(SR.Format(SR.MetadataPreservedArrayFailed, SR.MetadataStandaloneValuesProperty, SR.Format(SR.DeserializeUnableToConvertValue, propertyType)));
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000507F File Offset: 0x0000327F
		[DoesNotReturn]
		public static void ThrowJsonException_MetadataCannotParsePreservedObjectIntoImmutable(Type propertyType)
		{
			ThrowHelper.ThrowJsonException(SR.Format(SR.MetadataCannotParsePreservedObjectToImmutable, propertyType));
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00005091 File Offset: 0x00003291
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_MetadataReferenceOfTypeCannotBeAssignedToType(string referenceId, Type currentType, Type typeToConvert)
		{
			throw new InvalidOperationException(SR.Format(SR.MetadataReferenceOfTypeCannotBeAssignedToType, referenceId, currentType, typeToConvert));
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000050A5 File Offset: 0x000032A5
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_JsonPropertyInfoIsBoundToDifferentJsonTypeInfo(JsonPropertyInfo propertyInfo)
		{
			throw new InvalidOperationException(SR.Format(SR.JsonPropertyInfoBoundToDifferentParent, propertyInfo.Name, propertyInfo.ParentTypeInfo.Type.FullName));
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000050CC File Offset: 0x000032CC
		[DoesNotReturn]
		internal static void ThrowUnexpectedMetadataException(ReadOnlySpan<byte> propertyName, ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state)
		{
			MetadataPropertyName metadataPropertyName = JsonSerializer.GetMetadataPropertyName(propertyName, state.Current.BaseJsonTypeInfo.PolymorphicTypeResolver);
			if (metadataPropertyName != MetadataPropertyName.None)
			{
				ThrowHelper.ThrowJsonException_MetadataUnexpectedProperty(propertyName, ref state);
				return;
			}
			ThrowHelper.ThrowJsonException_MetadataInvalidPropertyWithLeadingDollarSign(propertyName, ref state, in reader);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00005103 File Offset: 0x00003303
		[DoesNotReturn]
		public static void ThrowNotSupportedException_NoMetadataForType(Type type, IJsonTypeInfoResolver resolver)
		{
			throw new NotSupportedException(SR.Format(SR.NoMetadataForType, type, ((resolver != null) ? resolver.ToString() : null) ?? "<null>"));
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000512A File Offset: 0x0000332A
		public static NotSupportedException GetNotSupportedException_AmbiguousMetadataForType(Type type, Type match1, Type match2)
		{
			return new NotSupportedException(SR.Format(SR.AmbiguousMetadataForType, type, match1, match2));
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000513E File Offset: 0x0000333E
		[DoesNotReturn]
		public static void ThrowNotSupportedException_ConstructorContainsNullParameterNames(Type declaringType)
		{
			throw new NotSupportedException(SR.Format(SR.ConstructorContainsNullParameterNames, declaringType));
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00005150 File Offset: 0x00003350
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_NoMetadataForType(Type type, IJsonTypeInfoResolver resolver)
		{
			throw new InvalidOperationException(SR.Format(SR.NoMetadataForType, type, ((resolver != null) ? resolver.ToString() : null) ?? "<null>"));
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00005177 File Offset: 0x00003377
		public static Exception GetInvalidOperationException_NoMetadataForTypeProperties(IJsonTypeInfoResolver resolver, Type type)
		{
			return new InvalidOperationException(SR.Format(SR.NoMetadataForTypeProperties, ((resolver != null) ? resolver.ToString() : null) ?? "<null>", type));
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000519E File Offset: 0x0000339E
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_NoMetadataForTypeProperties(IJsonTypeInfoResolver resolver, Type type)
		{
			throw ThrowHelper.GetInvalidOperationException_NoMetadataForTypeProperties(resolver, type);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000051A7 File Offset: 0x000033A7
		[DoesNotReturn]
		public static void ThrowMissingMemberException_MissingFSharpCoreMember(string missingFsharpCoreMember)
		{
			throw new MissingMemberException(SR.Format(SR.MissingFSharpCoreMember, missingFsharpCoreMember));
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000051B9 File Offset: 0x000033B9
		[DoesNotReturn]
		public static void ThrowNotSupportedException_BaseConverterDoesNotSupportMetadata(Type derivedType)
		{
			throw new NotSupportedException(SR.Format(SR.Polymorphism_DerivedConverterDoesNotSupportMetadata, derivedType));
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000051CB File Offset: 0x000033CB
		[DoesNotReturn]
		public static void ThrowNotSupportedException_DerivedConverterDoesNotSupportMetadata(Type derivedType)
		{
			throw new NotSupportedException(SR.Format(SR.Polymorphism_DerivedConverterDoesNotSupportMetadata, derivedType));
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000051DD File Offset: 0x000033DD
		[DoesNotReturn]
		public static void ThrowNotSupportedException_RuntimeTypeNotSupported(Type baseType, Type runtimeType)
		{
			throw new NotSupportedException(SR.Format(SR.Polymorphism_RuntimeTypeNotSupported, runtimeType, baseType));
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000051F0 File Offset: 0x000033F0
		[DoesNotReturn]
		public static void ThrowNotSupportedException_RuntimeTypeDiamondAmbiguity(Type baseType, Type runtimeType, Type derivedType1, Type derivedType2)
		{
			throw new NotSupportedException(SR.Format(SR.Polymorphism_RuntimeTypeDiamondAmbiguity, new object[] { runtimeType, derivedType1, derivedType2, baseType }));
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00005217 File Offset: 0x00003417
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_TypeDoesNotSupportPolymorphism(Type baseType)
		{
			throw new InvalidOperationException(SR.Format(SR.Polymorphism_TypeDoesNotSupportPolymorphism, baseType));
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00005229 File Offset: 0x00003429
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_DerivedTypeNotSupported(Type baseType, Type derivedType)
		{
			throw new InvalidOperationException(SR.Format(SR.Polymorphism_DerivedTypeIsNotSupported, derivedType, baseType));
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000523C File Offset: 0x0000343C
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_DerivedTypeIsAlreadySpecified(Type baseType, Type derivedType)
		{
			throw new InvalidOperationException(SR.Format(SR.Polymorphism_DerivedTypeIsAlreadySpecified, baseType, derivedType));
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000524F File Offset: 0x0000344F
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_TypeDicriminatorIdIsAlreadySpecified(Type baseType, object typeDiscriminator)
		{
			throw new InvalidOperationException(SR.Format(SR.Polymorphism_TypeDicriminatorIdIsAlreadySpecified, baseType, typeDiscriminator));
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00005262 File Offset: 0x00003462
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_InvalidCustomTypeDiscriminatorPropertyName()
		{
			throw new InvalidOperationException(SR.Polymorphism_InvalidCustomTypeDiscriminatorPropertyName);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000526E File Offset: 0x0000346E
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_PolymorphicTypeConfigurationDoesNotSpecifyDerivedTypes(Type baseType)
		{
			throw new InvalidOperationException(SR.Format(SR.Polymorphism_ConfigurationDoesNotSpecifyDerivedTypes, baseType));
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00005280 File Offset: 0x00003480
		[DoesNotReturn]
		public static void ThrowInvalidOperationException_InvalidEnumTypeWithSpecialChar(Type enumType, string enumName)
		{
			throw new InvalidOperationException(SR.Format(SR.InvalidEnumTypeWithSpecialChar, enumType.Name, enumName));
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00005298 File Offset: 0x00003498
		[DoesNotReturn]
		public static void ThrowJsonException_UnrecognizedTypeDiscriminator(object typeDiscriminator)
		{
			ThrowHelper.ThrowJsonException(SR.Format(SR.Polymorphism_UnrecognizedTypeDiscriminator, typeDiscriminator));
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000052AA File Offset: 0x000034AA
		[DoesNotReturn]
		public static void ThrowArgumentException_JsonPolymorphismOptionsAssociatedWithDifferentJsonTypeInfo(string parameterName)
		{
			throw new ArgumentException(SR.JsonPolymorphismOptionsAssociatedWithDifferentJsonTypeInfo, parameterName);
		}

		// Token: 0x040000C7 RID: 199
		public const string ExceptionSourceValueToRethrowAsJsonException = "System.Text.Json.Rethrowable";
	}
}
