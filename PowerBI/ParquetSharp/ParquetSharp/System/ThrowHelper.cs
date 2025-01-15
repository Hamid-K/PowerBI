using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000C5 RID: 197
	internal static class ThrowHelper
	{
		// Token: 0x0600064A RID: 1610 RVA: 0x00018A64 File Offset: 0x00016C64
		internal static void ThrowArgumentNullException(ExceptionArgument argument)
		{
			throw ThrowHelper.CreateArgumentNullException(argument);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00018A6C File Offset: 0x00016C6C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentNullException(ExceptionArgument argument)
		{
			return new ArgumentNullException(argument.ToString());
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00018A80 File Offset: 0x00016C80
		internal static void ThrowArrayTypeMismatchException()
		{
			throw ThrowHelper.CreateArrayTypeMismatchException();
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00018A88 File Offset: 0x00016C88
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArrayTypeMismatchException()
		{
			return new ArrayTypeMismatchException();
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00018A90 File Offset: 0x00016C90
		internal static void ThrowArgumentException_InvalidTypeWithPointersNotSupported(Type type)
		{
			throw ThrowHelper.CreateArgumentException_InvalidTypeWithPointersNotSupported(type);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00018A98 File Offset: 0x00016C98
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentException_InvalidTypeWithPointersNotSupported(Type type)
		{
			return new ArgumentException(System.Memory189091.SR.Format(System.Memory189091.SR.Argument_InvalidTypeWithPointersNotSupported, type));
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00018AAC File Offset: 0x00016CAC
		internal static void ThrowArgumentException_DestinationTooShort()
		{
			throw ThrowHelper.CreateArgumentException_DestinationTooShort();
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00018AB4 File Offset: 0x00016CB4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentException_DestinationTooShort()
		{
			return new ArgumentException(System.Memory189091.SR.Argument_DestinationTooShort);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00018AC0 File Offset: 0x00016CC0
		internal static void ThrowIndexOutOfRangeException()
		{
			throw ThrowHelper.CreateIndexOutOfRangeException();
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00018AC8 File Offset: 0x00016CC8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateIndexOutOfRangeException()
		{
			return new IndexOutOfRangeException();
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00018AD0 File Offset: 0x00016CD0
		internal static void ThrowArgumentOutOfRangeException()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException();
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00018AD8 File Offset: 0x00016CD8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException()
		{
			return new ArgumentOutOfRangeException();
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00018AE0 File Offset: 0x00016CE0
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException(argument);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00018AE8 File Offset: 0x00016CE8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException(ExceptionArgument argument)
		{
			return new ArgumentOutOfRangeException(argument.ToString());
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00018AFC File Offset: 0x00016CFC
		internal static void ThrowArgumentOutOfRangeException_PrecisionTooLarge()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_PrecisionTooLarge();
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00018B04 File Offset: 0x00016D04
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_PrecisionTooLarge()
		{
			return new ArgumentOutOfRangeException("precision", System.Memory189091.SR.Format(System.Memory189091.SR.Argument_PrecisionTooLarge, 99));
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00018B24 File Offset: 0x00016D24
		internal static void ThrowArgumentOutOfRangeException_SymbolDoesNotFit()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_SymbolDoesNotFit();
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00018B2C File Offset: 0x00016D2C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_SymbolDoesNotFit()
		{
			return new ArgumentOutOfRangeException("symbol", System.Memory189091.SR.Argument_BadFormatSpecifier);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00018B40 File Offset: 0x00016D40
		internal static void ThrowInvalidOperationException()
		{
			throw ThrowHelper.CreateInvalidOperationException();
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00018B48 File Offset: 0x00016D48
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException()
		{
			return new InvalidOperationException();
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00018B50 File Offset: 0x00016D50
		internal static void ThrowInvalidOperationException_OutstandingReferences()
		{
			throw ThrowHelper.CreateInvalidOperationException_OutstandingReferences();
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00018B58 File Offset: 0x00016D58
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException_OutstandingReferences()
		{
			return new InvalidOperationException(System.Memory189091.SR.OutstandingReferences);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00018B64 File Offset: 0x00016D64
		internal static void ThrowInvalidOperationException_UnexpectedSegmentType()
		{
			throw ThrowHelper.CreateInvalidOperationException_UnexpectedSegmentType();
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00018B6C File Offset: 0x00016D6C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException_UnexpectedSegmentType()
		{
			return new InvalidOperationException(System.Memory189091.SR.UnexpectedSegmentType);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00018B78 File Offset: 0x00016D78
		internal static void ThrowInvalidOperationException_EndPositionNotReached()
		{
			throw ThrowHelper.CreateInvalidOperationException_EndPositionNotReached();
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00018B80 File Offset: 0x00016D80
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException_EndPositionNotReached()
		{
			return new InvalidOperationException(System.Memory189091.SR.EndPositionNotReached);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00018B8C File Offset: 0x00016D8C
		internal static void ThrowArgumentOutOfRangeException_PositionOutOfRange()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_PositionOutOfRange();
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00018B94 File Offset: 0x00016D94
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_PositionOutOfRange()
		{
			return new ArgumentOutOfRangeException("position");
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00018BA0 File Offset: 0x00016DA0
		internal static void ThrowArgumentOutOfRangeException_OffsetOutOfRange()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_OffsetOutOfRange();
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00018BA8 File Offset: 0x00016DA8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_OffsetOutOfRange()
		{
			return new ArgumentOutOfRangeException("offset");
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00018BB4 File Offset: 0x00016DB4
		internal static void ThrowObjectDisposedException_ArrayMemoryPoolBuffer()
		{
			throw ThrowHelper.CreateObjectDisposedException_ArrayMemoryPoolBuffer();
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00018BBC File Offset: 0x00016DBC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateObjectDisposedException_ArrayMemoryPoolBuffer()
		{
			return new ObjectDisposedException("ArrayMemoryPoolBuffer");
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00018BC8 File Offset: 0x00016DC8
		internal static void ThrowFormatException_BadFormatSpecifier()
		{
			throw ThrowHelper.CreateFormatException_BadFormatSpecifier();
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00018BD0 File Offset: 0x00016DD0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateFormatException_BadFormatSpecifier()
		{
			return new FormatException(System.Memory189091.SR.Argument_BadFormatSpecifier);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00018BDC File Offset: 0x00016DDC
		internal static void ThrowArgumentException_OverlapAlignmentMismatch()
		{
			throw ThrowHelper.CreateArgumentException_OverlapAlignmentMismatch();
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00018BE4 File Offset: 0x00016DE4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentException_OverlapAlignmentMismatch()
		{
			return new ArgumentException(System.Memory189091.SR.Argument_OverlapAlignmentMismatch);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00018BF0 File Offset: 0x00016DF0
		internal static void ThrowNotSupportedException()
		{
			throw ThrowHelper.CreateThrowNotSupportedException();
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00018BF8 File Offset: 0x00016DF8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateThrowNotSupportedException()
		{
			return new NotSupportedException();
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00018C00 File Offset: 0x00016E00
		public static bool TryFormatThrowFormatException(out int bytesWritten)
		{
			bytesWritten = 0;
			ThrowHelper.ThrowFormatException_BadFormatSpecifier();
			return false;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00018C0C File Offset: 0x00016E0C
		public static bool TryParseThrowFormatException<T>(out T value, out int bytesConsumed)
		{
			value = default(T);
			bytesConsumed = 0;
			ThrowHelper.ThrowFormatException_BadFormatSpecifier();
			return false;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00018C20 File Offset: 0x00016E20
		public static void ThrowArgumentValidationException<T>(ReadOnlySequenceSegment<T> startSegment, int startIndex, ReadOnlySequenceSegment<T> endSegment)
		{
			throw ThrowHelper.CreateArgumentValidationException<T>(startSegment, startIndex, endSegment);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00018C2C File Offset: 0x00016E2C
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

		// Token: 0x06000674 RID: 1652 RVA: 0x00018C9C File Offset: 0x00016E9C
		public static void ThrowArgumentValidationException(Array array, int start)
		{
			throw ThrowHelper.CreateArgumentValidationException(array, start);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00018CA8 File Offset: 0x00016EA8
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

		// Token: 0x06000676 RID: 1654 RVA: 0x00018CD4 File Offset: 0x00016ED4
		public static void ThrowStartOrEndArgumentValidationException(long start)
		{
			throw ThrowHelper.CreateStartOrEndArgumentValidationException(start);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00018CDC File Offset: 0x00016EDC
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
