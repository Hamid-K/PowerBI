using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000007 RID: 7
	internal static class ThrowHelper
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020E2 File Offset: 0x000002E2
		internal static void ThrowArgumentNullException(ExceptionArgument argument)
		{
			throw ThrowHelper.CreateArgumentNullException(argument);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020EA File Offset: 0x000002EA
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentNullException(ExceptionArgument argument)
		{
			return new ArgumentNullException(argument.ToString());
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020FE File Offset: 0x000002FE
		internal static void ThrowArrayTypeMismatchException()
		{
			throw ThrowHelper.CreateArrayTypeMismatchException();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002105 File Offset: 0x00000305
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArrayTypeMismatchException()
		{
			return new ArrayTypeMismatchException();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000210C File Offset: 0x0000030C
		internal static void ThrowArgumentException_InvalidTypeWithPointersNotSupported(Type type)
		{
			throw ThrowHelper.CreateArgumentException_InvalidTypeWithPointersNotSupported(type);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002114 File Offset: 0x00000314
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentException_InvalidTypeWithPointersNotSupported(Type type)
		{
			return new ArgumentException(SR.Format(SR.Argument_InvalidTypeWithPointersNotSupported, type));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002126 File Offset: 0x00000326
		internal static void ThrowArgumentException_DestinationTooShort()
		{
			throw ThrowHelper.CreateArgumentException_DestinationTooShort();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000212D File Offset: 0x0000032D
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentException_DestinationTooShort()
		{
			return new ArgumentException(SR.Argument_DestinationTooShort);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002139 File Offset: 0x00000339
		internal static void ThrowIndexOutOfRangeException()
		{
			throw ThrowHelper.CreateIndexOutOfRangeException();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002140 File Offset: 0x00000340
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateIndexOutOfRangeException()
		{
			return new IndexOutOfRangeException();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002147 File Offset: 0x00000347
		internal static void ThrowArgumentOutOfRangeException()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000214E File Offset: 0x0000034E
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException()
		{
			return new ArgumentOutOfRangeException();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002155 File Offset: 0x00000355
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException(argument);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000215D File Offset: 0x0000035D
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException(ExceptionArgument argument)
		{
			return new ArgumentOutOfRangeException(argument.ToString());
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002171 File Offset: 0x00000371
		internal static void ThrowArgumentOutOfRangeException_PrecisionTooLarge()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_PrecisionTooLarge();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002178 File Offset: 0x00000378
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_PrecisionTooLarge()
		{
			return new ArgumentOutOfRangeException("precision", SR.Format(SR.Argument_PrecisionTooLarge, 99));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002195 File Offset: 0x00000395
		internal static void ThrowArgumentOutOfRangeException_SymbolDoesNotFit()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_SymbolDoesNotFit();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000219C File Offset: 0x0000039C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_SymbolDoesNotFit()
		{
			return new ArgumentOutOfRangeException("symbol", SR.Argument_BadFormatSpecifier);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000021AD File Offset: 0x000003AD
		internal static void ThrowInvalidOperationException()
		{
			throw ThrowHelper.CreateInvalidOperationException();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000021B4 File Offset: 0x000003B4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException()
		{
			return new InvalidOperationException();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000021BB File Offset: 0x000003BB
		internal static void ThrowInvalidOperationException_OutstandingReferences()
		{
			throw ThrowHelper.CreateInvalidOperationException_OutstandingReferences();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000021C2 File Offset: 0x000003C2
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException_OutstandingReferences()
		{
			return new InvalidOperationException(SR.OutstandingReferences);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000021CE File Offset: 0x000003CE
		internal static void ThrowInvalidOperationException_UnexpectedSegmentType()
		{
			throw ThrowHelper.CreateInvalidOperationException_UnexpectedSegmentType();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000021D5 File Offset: 0x000003D5
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException_UnexpectedSegmentType()
		{
			return new InvalidOperationException(SR.UnexpectedSegmentType);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000021E1 File Offset: 0x000003E1
		internal static void ThrowInvalidOperationException_EndPositionNotReached()
		{
			throw ThrowHelper.CreateInvalidOperationException_EndPositionNotReached();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000021E8 File Offset: 0x000003E8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException_EndPositionNotReached()
		{
			return new InvalidOperationException(SR.EndPositionNotReached);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000021F4 File Offset: 0x000003F4
		internal static void ThrowArgumentOutOfRangeException_PositionOutOfRange()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_PositionOutOfRange();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000021FB File Offset: 0x000003FB
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_PositionOutOfRange()
		{
			return new ArgumentOutOfRangeException("position");
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002207 File Offset: 0x00000407
		internal static void ThrowArgumentOutOfRangeException_OffsetOutOfRange()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_OffsetOutOfRange();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000220E File Offset: 0x0000040E
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_OffsetOutOfRange()
		{
			return new ArgumentOutOfRangeException("offset");
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000221A File Offset: 0x0000041A
		internal static void ThrowObjectDisposedException_ArrayMemoryPoolBuffer()
		{
			throw ThrowHelper.CreateObjectDisposedException_ArrayMemoryPoolBuffer();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002221 File Offset: 0x00000421
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateObjectDisposedException_ArrayMemoryPoolBuffer()
		{
			return new ObjectDisposedException("ArrayMemoryPoolBuffer");
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000222D File Offset: 0x0000042D
		internal static void ThrowFormatException_BadFormatSpecifier()
		{
			throw ThrowHelper.CreateFormatException_BadFormatSpecifier();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002234 File Offset: 0x00000434
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateFormatException_BadFormatSpecifier()
		{
			return new FormatException(SR.Argument_BadFormatSpecifier);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002240 File Offset: 0x00000440
		internal static void ThrowArgumentException_OverlapAlignmentMismatch()
		{
			throw ThrowHelper.CreateArgumentException_OverlapAlignmentMismatch();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002247 File Offset: 0x00000447
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentException_OverlapAlignmentMismatch()
		{
			return new ArgumentException(SR.Argument_OverlapAlignmentMismatch);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002253 File Offset: 0x00000453
		internal static void ThrowNotSupportedException()
		{
			throw ThrowHelper.CreateThrowNotSupportedException();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000225A File Offset: 0x0000045A
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateThrowNotSupportedException()
		{
			return new NotSupportedException();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002261 File Offset: 0x00000461
		public static bool TryFormatThrowFormatException(out int bytesWritten)
		{
			bytesWritten = 0;
			ThrowHelper.ThrowFormatException_BadFormatSpecifier();
			return false;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000226C File Offset: 0x0000046C
		public static bool TryParseThrowFormatException<T>(out T value, out int bytesConsumed)
		{
			value = default(T);
			bytesConsumed = 0;
			ThrowHelper.ThrowFormatException_BadFormatSpecifier();
			return false;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000227E File Offset: 0x0000047E
		public static void ThrowArgumentValidationException<T>(ReadOnlySequenceSegment<T> startSegment, int startIndex, ReadOnlySequenceSegment<T> endSegment)
		{
			throw ThrowHelper.CreateArgumentValidationException<T>(startSegment, startIndex, endSegment);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002288 File Offset: 0x00000488
		private static Exception CreateArgumentValidationException<T>(ReadOnlySequenceSegment<T> startSegment, int startIndex, ReadOnlySequenceSegment<T> endSegment)
		{
			if (startSegment == null)
			{
				return ThrowHelper.CreateArgumentNullException(ExceptionArgument.startSegment);
			}
			if (endSegment == null)
			{
				return ThrowHelper.CreateArgumentNullException(ExceptionArgument.endSegment);
			}
			if (startSegment != endSegment && startSegment.RunningIndex > endSegment.RunningIndex)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.endSegment);
			}
			if (startSegment.Memory.Length < startIndex)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.startIndex);
			}
			return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.endIndex);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000022E4 File Offset: 0x000004E4
		public static void ThrowArgumentValidationException(Array array, int start)
		{
			throw ThrowHelper.CreateArgumentValidationException(array, start);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000022ED File Offset: 0x000004ED
		private static Exception CreateArgumentValidationException(Array array, int start)
		{
			if (array == null)
			{
				return ThrowHelper.CreateArgumentNullException(ExceptionArgument.array);
			}
			if (start > array.Length)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.length);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002310 File Offset: 0x00000510
		public static void ThrowStartOrEndArgumentValidationException(long start)
		{
			throw ThrowHelper.CreateStartOrEndArgumentValidationException(start);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002318 File Offset: 0x00000518
		private static Exception CreateStartOrEndArgumentValidationException(long start)
		{
			if (start < 0L)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.length);
		}
	}
}
