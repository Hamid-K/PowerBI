using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x020000DF RID: 223
	[System.Memory.IsReadOnly]
	[DebuggerTypeProxy(typeof(ReadOnlySequenceDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	internal struct ReadOnlySequence<T>
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00021B74 File Offset: 0x0001FD74
		public long Length
		{
			get
			{
				return this.GetLength();
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00021B7C File Offset: 0x0001FD7C
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0L;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00021B88 File Offset: 0x0001FD88
		public bool IsSingleSegment
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this._sequenceStart.GetObject() == this._sequenceEnd.GetObject();
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00021BA4 File Offset: 0x0001FDA4
		public ReadOnlyMemory<T> First
		{
			get
			{
				return this.GetFirstBuffer();
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00021BAC File Offset: 0x0001FDAC
		public SequencePosition Start
		{
			get
			{
				return this._sequenceStart;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00021BB4 File Offset: 0x0001FDB4
		public SequencePosition End
		{
			get
			{
				return this._sequenceEnd;
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00021BBC File Offset: 0x0001FDBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReadOnlySequence(object startSegment, int startIndexAndFlags, object endSegment, int endIndexAndFlags)
		{
			this._sequenceStart = new SequencePosition(startSegment, startIndexAndFlags);
			this._sequenceEnd = new SequencePosition(endSegment, endIndexAndFlags);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00021BDC File Offset: 0x0001FDDC
		public ReadOnlySequence(ReadOnlySequenceSegment<T> startSegment, int startIndex, ReadOnlySequenceSegment<T> endSegment, int endIndex)
		{
			if (startSegment == null || endSegment == null || (startSegment != endSegment && startSegment.RunningIndex > endSegment.RunningIndex) || startSegment.Memory.Length < startIndex || endSegment.Memory.Length < endIndex || (startSegment == endSegment && endIndex < startIndex))
			{
				ThrowHelper.ThrowArgumentValidationException<T>(startSegment, startIndex, endSegment);
			}
			this._sequenceStart = new SequencePosition(startSegment, ReadOnlySequence.SegmentToSequenceStart(startIndex));
			this._sequenceEnd = new SequencePosition(endSegment, ReadOnlySequence.SegmentToSequenceEnd(endIndex));
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00021C78 File Offset: 0x0001FE78
		public ReadOnlySequence(T[] array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			this._sequenceStart = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceStart(0));
			this._sequenceEnd = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceEnd(array.Length));
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00021CB0 File Offset: 0x0001FEB0
		public ReadOnlySequence(T[] array, int start, int length)
		{
			if (array == null || start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentValidationException(array, start);
			}
			this._sequenceStart = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceStart(start));
			this._sequenceEnd = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceEnd(start + length));
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00021D08 File Offset: 0x0001FF08
		public ReadOnlySequence(ReadOnlyMemory<T> memory)
		{
			MemoryManager<T> memoryManager;
			int num;
			int num2;
			if (MemoryMarshal.TryGetMemoryManager<T, MemoryManager<T>>(memory, out memoryManager, out num, out num2))
			{
				this._sequenceStart = new SequencePosition(memoryManager, ReadOnlySequence.MemoryManagerToSequenceStart(num));
				this._sequenceEnd = new SequencePosition(memoryManager, ReadOnlySequence.MemoryManagerToSequenceEnd(num2));
				return;
			}
			ArraySegment<T> arraySegment;
			if (MemoryMarshal.TryGetArray<T>(memory, out arraySegment))
			{
				T[] array = arraySegment.Array;
				int offset = arraySegment.Offset;
				this._sequenceStart = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceStart(offset));
				this._sequenceEnd = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceEnd(offset + arraySegment.Count));
				return;
			}
			if (typeof(T) == typeof(char))
			{
				string text;
				int num3;
				if (!MemoryMarshal.TryGetString((ReadOnlyMemory<char>)memory, out text, out num3, out num2))
				{
					ThrowHelper.ThrowInvalidOperationException();
				}
				this._sequenceStart = new SequencePosition(text, ReadOnlySequence.StringToSequenceStart(num3));
				this._sequenceEnd = new SequencePosition(text, ReadOnlySequence.StringToSequenceEnd(num3 + num2));
				return;
			}
			ThrowHelper.ThrowInvalidOperationException();
			this._sequenceStart = default(SequencePosition);
			this._sequenceEnd = default(SequencePosition);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00021E28 File Offset: 0x00020028
		public ReadOnlySequence<T> Slice(long start, long length)
		{
			if (start < 0L || length < 0L)
			{
				ThrowHelper.ThrowStartOrEndArgumentValidationException(start);
			}
			int num = ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			int index = ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object @object = this._sequenceStart.GetObject();
			object object2 = this._sequenceEnd.GetObject();
			SequencePosition sequencePosition;
			SequencePosition sequencePosition2;
			if (@object != object2)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				int num2 = readOnlySequenceSegment.Memory.Length - num;
				if ((long)num2 > start)
				{
					num += (int)start;
					sequencePosition = new SequencePosition(@object, num);
					sequencePosition2 = ReadOnlySequence<T>.GetEndPosition(readOnlySequenceSegment, @object, num, object2, index, length);
				}
				else
				{
					if (num2 < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					sequencePosition = ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, object2, index, start - (long)num2, ExceptionArgument.start);
					int index2 = ReadOnlySequence<T>.GetIndex(ref sequencePosition);
					object object3 = sequencePosition.GetObject();
					if (object3 != object2)
					{
						sequencePosition2 = ReadOnlySequence<T>.GetEndPosition((ReadOnlySequenceSegment<T>)object3, object3, index2, object2, index, length);
					}
					else
					{
						if ((long)(index - index2) < length)
						{
							ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
						}
						sequencePosition2 = new SequencePosition(object3, index2 + (int)length);
					}
				}
			}
			else
			{
				if ((long)(index - num) < start)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(-1L);
				}
				num += (int)start;
				sequencePosition = new SequencePosition(@object, num);
				if ((long)(index - num) < length)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
				sequencePosition2 = new SequencePosition(@object, num + (int)length);
			}
			return this.SliceImpl(ref sequencePosition, ref sequencePosition2);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00021F90 File Offset: 0x00020190
		public ReadOnlySequence<T> Slice(long start, SequencePosition end)
		{
			if (start < 0L)
			{
				ThrowHelper.ThrowStartOrEndArgumentValidationException(start);
			}
			uint index = (uint)ReadOnlySequence<T>.GetIndex(ref end);
			object @object = end.GetObject();
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			object object2 = this._sequenceStart.GetObject();
			uint index3 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object object3 = this._sequenceEnd.GetObject();
			if (object2 == object3)
			{
				if (!ReadOnlySequence<T>.InRange(index, index2, index3))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if ((ulong)(index - index2) < (ulong)start)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(-1L);
				}
			}
			else
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)object2;
				ulong num = (ulong)(readOnlySequenceSegment.RunningIndex + (long)((ulong)index2));
				ulong num2 = (ulong)(((ReadOnlySequenceSegment<T>)@object).RunningIndex + (long)((ulong)index));
				if (!ReadOnlySequence<T>.InRange(num2, num, (ulong)(((ReadOnlySequenceSegment<T>)object3).RunningIndex + (long)((ulong)index3))))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (num + (ulong)start > num2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				int num3 = readOnlySequenceSegment.Memory.Length - (int)index2;
				if ((long)num3 <= start)
				{
					if (num3 < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					SequencePosition sequencePosition = ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, @object, (int)index, start - (long)num3, ExceptionArgument.start);
					return this.SliceImpl(ref sequencePosition, ref end);
				}
			}
			SequencePosition sequencePosition2 = new SequencePosition(object2, (int)(index2 + (uint)((int)start)));
			return this.SliceImpl(ref sequencePosition2, ref end);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000220DC File Offset: 0x000202DC
		public ReadOnlySequence<T> Slice(SequencePosition start, long length)
		{
			uint index = (uint)ReadOnlySequence<T>.GetIndex(ref start);
			object @object = start.GetObject();
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			object object2 = this._sequenceStart.GetObject();
			uint index3 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object object3 = this._sequenceEnd.GetObject();
			if (object2 == object3)
			{
				if (!ReadOnlySequence<T>.InRange(index, index2, index3))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (length < 0L)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
				if ((ulong)(index3 - index) < (ulong)length)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
			}
			else
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				ulong num = (ulong)(readOnlySequenceSegment.RunningIndex + (long)((ulong)index));
				ulong num2 = (ulong)(((ReadOnlySequenceSegment<T>)object2).RunningIndex + (long)((ulong)index2));
				ulong num3 = (ulong)(((ReadOnlySequenceSegment<T>)object3).RunningIndex + (long)((ulong)index3));
				if (!ReadOnlySequence<T>.InRange(num, num2, num3))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (length < 0L)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
				if (num + (ulong)length > num3)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
				}
				int num4 = readOnlySequenceSegment.Memory.Length - (int)index;
				if ((long)num4 < length)
				{
					if (num4 < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					SequencePosition sequencePosition = ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, object3, (int)index3, length - (long)num4, ExceptionArgument.length);
					return this.SliceImpl(ref start, ref sequencePosition);
				}
			}
			SequencePosition sequencePosition2 = new SequencePosition(@object, (int)(index + (uint)((int)length)));
			return this.SliceImpl(ref start, ref sequencePosition2);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00022240 File Offset: 0x00020440
		public ReadOnlySequence<T> Slice(int start, int length)
		{
			return this.Slice((long)start, (long)length);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0002224C File Offset: 0x0002044C
		public ReadOnlySequence<T> Slice(int start, SequencePosition end)
		{
			return this.Slice((long)start, end);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00022258 File Offset: 0x00020458
		public ReadOnlySequence<T> Slice(SequencePosition start, int length)
		{
			return this.Slice(start, (long)length);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00022264 File Offset: 0x00020464
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySequence<T> Slice(SequencePosition start, SequencePosition end)
		{
			this.BoundsCheck((uint)ReadOnlySequence<T>.GetIndex(ref start), start.GetObject(), (uint)ReadOnlySequence<T>.GetIndex(ref end), end.GetObject());
			return this.SliceImpl(ref start, ref end);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x000222A4 File Offset: 0x000204A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySequence<T> Slice(SequencePosition start)
		{
			this.BoundsCheck(ref start);
			return this.SliceImpl(ref start, ref this._sequenceEnd);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000222BC File Offset: 0x000204BC
		public ReadOnlySequence<T> Slice(long start)
		{
			if (start < 0L)
			{
				ThrowHelper.ThrowStartOrEndArgumentValidationException(start);
			}
			if (start == 0L)
			{
				return this;
			}
			SequencePosition sequencePosition = this.Seek(ref this._sequenceStart, ref this._sequenceEnd, start, ExceptionArgument.start);
			return this.SliceImpl(ref sequencePosition, ref this._sequenceEnd);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0002230C File Offset: 0x0002050C
		public unsafe override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				ReadOnlySequence<T> readOnlySequence = this;
				ReadOnlySequence<char> readOnlySequence2 = *Unsafe.As<ReadOnlySequence<T>, ReadOnlySequence<char>>(ref readOnlySequence);
				string text;
				int num;
				int num2;
				if (SequenceMarshal.TryGetString(readOnlySequence2, out text, out num, out num2))
				{
					return text.Substring(num, num2);
				}
				if (this.Length < 2147483647L)
				{
					return new string((ref readOnlySequence2).ToArray<char>());
				}
			}
			return string.Format("System.Buffers.ReadOnlySequence<{0}>[{1}]", typeof(T).Name, this.Length);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000223AC File Offset: 0x000205AC
		public ReadOnlySequence<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlySequence<T>.Enumerator(ref this);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000223B4 File Offset: 0x000205B4
		public SequencePosition GetPosition(long offset)
		{
			return this.GetPosition(offset, this._sequenceStart);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000223C4 File Offset: 0x000205C4
		public SequencePosition GetPosition(long offset, SequencePosition origin)
		{
			if (offset < 0L)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_OffsetOutOfRange();
			}
			return this.Seek(ref origin, ref this._sequenceEnd, offset, ExceptionArgument.offset);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x000223E4 File Offset: 0x000205E4
		public bool TryGet(ref SequencePosition position, out ReadOnlyMemory<T> memory, bool advance = true)
		{
			SequencePosition sequencePosition;
			bool flag = this.TryGetBuffer(ref position, out memory, out sequencePosition);
			if (advance)
			{
				position = sequencePosition;
			}
			return flag;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00022410 File Offset: 0x00020610
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool TryGetBuffer([System.Memory.IsReadOnly] [In] ref SequencePosition position, out ReadOnlyMemory<T> memory, out SequencePosition next)
		{
			object @object = position.GetObject();
			next = default(SequencePosition);
			if (@object == null)
			{
				memory = default(ReadOnlyMemory<T>);
				return false;
			}
			ReadOnlySequence<T>.SequenceType sequenceType = this.GetSequenceType();
			object object2 = this._sequenceEnd.GetObject();
			int index = ReadOnlySequence<T>.GetIndex(ref position);
			int index2 = ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			if (sequenceType == ReadOnlySequence<T>.SequenceType.MultiSegment)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				if (readOnlySequenceSegment != object2)
				{
					ReadOnlySequenceSegment<T> next2 = readOnlySequenceSegment.Next;
					if (next2 == null)
					{
						ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
					}
					next = new SequencePosition(next2, 0);
					memory = readOnlySequenceSegment.Memory.Slice(index);
				}
				else
				{
					memory = readOnlySequenceSegment.Memory.Slice(index, index2 - index);
				}
			}
			else
			{
				if (@object != object2)
				{
					ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
				}
				if (sequenceType == ReadOnlySequence<T>.SequenceType.Array)
				{
					memory = new ReadOnlyMemory<T>((T[])@object, index, index2 - index);
				}
				else if (typeof(T) == typeof(char) && sequenceType == ReadOnlySequence<T>.SequenceType.String)
				{
					memory = (ReadOnlyMemory<T>)((string)@object).AsMemory(index, index2 - index);
				}
				else
				{
					memory = ((MemoryManager<T>)@object).Memory.Slice(index, index2 - index);
				}
			}
			return true;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00022578 File Offset: 0x00020778
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReadOnlyMemory<T> GetFirstBuffer()
		{
			object @object = this._sequenceStart.GetObject();
			if (@object == null)
			{
				return default(ReadOnlyMemory<T>);
			}
			int num = this._sequenceStart.GetInteger();
			int integer = this._sequenceEnd.GetInteger();
			bool flag = @object != this._sequenceEnd.GetObject();
			if (num >= 0)
			{
				if (integer < 0)
				{
					if (flag)
					{
						ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
					}
					return new ReadOnlyMemory<T>((T[])@object, num, (integer & int.MaxValue) - num);
				}
				ReadOnlyMemory<T> memory = ((ReadOnlySequenceSegment<T>)@object).Memory;
				if (flag)
				{
					return memory.Slice(num);
				}
				return memory.Slice(num, integer - num);
			}
			else
			{
				if (flag)
				{
					ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
				}
				if (typeof(T) == typeof(char) && integer < 0)
				{
					return (ReadOnlyMemory<T>)((string)@object).AsMemory(num & int.MaxValue, integer - num);
				}
				num &= int.MaxValue;
				return ((MemoryManager<T>)@object).Memory.Slice(num, integer - num);
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000226A0 File Offset: 0x000208A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private SequencePosition Seek([System.Memory.IsReadOnly] [In] ref SequencePosition start, [System.Memory.IsReadOnly] [In] ref SequencePosition end, long offset, ExceptionArgument argument)
		{
			int index = ReadOnlySequence<T>.GetIndex(ref start);
			int index2 = ReadOnlySequence<T>.GetIndex(ref end);
			object @object = start.GetObject();
			object object2 = end.GetObject();
			if (@object != object2)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				int num = readOnlySequenceSegment.Memory.Length - index;
				if ((long)num <= offset)
				{
					if (num < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					return ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, object2, index2, offset - (long)num, argument);
				}
			}
			else if ((long)(index2 - index) < offset)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(argument);
			}
			return new SequencePosition(@object, index + (int)offset);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00022738 File Offset: 0x00020938
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static SequencePosition SeekMultiSegment(ReadOnlySequenceSegment<T> currentSegment, object endObject, int endIndex, long offset, ExceptionArgument argument)
		{
			while (currentSegment != null && currentSegment != endObject)
			{
				int length = currentSegment.Memory.Length;
				if ((long)length > offset)
				{
					IL_0049:
					return new SequencePosition(currentSegment, (int)offset);
				}
				offset -= (long)length;
				currentSegment = currentSegment.Next;
			}
			if (currentSegment == null || (long)endIndex < offset)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(argument);
				goto IL_0049;
			}
			goto IL_0049;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0002279C File Offset: 0x0002099C
		private void BoundsCheck([System.Memory.IsReadOnly] [In] ref SequencePosition position)
		{
			uint index = (uint)ReadOnlySequence<T>.GetIndex(ref position);
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			uint index3 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object @object = this._sequenceStart.GetObject();
			object object2 = this._sequenceEnd.GetObject();
			if (@object == object2)
			{
				if (!ReadOnlySequence<T>.InRange(index, index2, index3))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					return;
				}
			}
			else
			{
				ulong num = (ulong)(((ReadOnlySequenceSegment<T>)@object).RunningIndex + (long)((ulong)index2));
				if (!ReadOnlySequence<T>.InRange((ulong)(((ReadOnlySequenceSegment<T>)position.GetObject()).RunningIndex + (long)((ulong)index)), num, (ulong)(((ReadOnlySequenceSegment<T>)object2).RunningIndex + (long)((ulong)index3))))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00022844 File Offset: 0x00020A44
		private void BoundsCheck(uint sliceStartIndex, object sliceStartObject, uint sliceEndIndex, object sliceEndObject)
		{
			uint index = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object @object = this._sequenceStart.GetObject();
			object object2 = this._sequenceEnd.GetObject();
			if (@object == object2)
			{
				if (sliceStartObject != sliceEndObject || sliceStartObject != @object || sliceStartIndex > sliceEndIndex || sliceStartIndex < index || sliceEndIndex > index2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					return;
				}
			}
			else
			{
				ulong num = (ulong)(((ReadOnlySequenceSegment<T>)sliceStartObject).RunningIndex + (long)((ulong)sliceStartIndex));
				ulong num2 = (ulong)(((ReadOnlySequenceSegment<T>)sliceEndObject).RunningIndex + (long)((ulong)sliceEndIndex));
				if (num > num2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (num < (ulong)(((ReadOnlySequenceSegment<T>)@object).RunningIndex + (long)((ulong)index)) || num2 > (ulong)(((ReadOnlySequenceSegment<T>)object2).RunningIndex + (long)((ulong)index2)))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00022914 File Offset: 0x00020B14
		private static SequencePosition GetEndPosition(ReadOnlySequenceSegment<T> startSegment, object startObject, int startIndex, object endObject, int endIndex, long length)
		{
			int num = startSegment.Memory.Length - startIndex;
			if ((long)num > length)
			{
				return new SequencePosition(startObject, startIndex + (int)length);
			}
			if (num < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
			}
			return ReadOnlySequence<T>.SeekMultiSegment(startSegment.Next, endObject, endIndex, length - (long)num, ExceptionArgument.length);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0002296C File Offset: 0x00020B6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReadOnlySequence<T>.SequenceType GetSequenceType()
		{
			return (ReadOnlySequence<T>.SequenceType)(-(ReadOnlySequence<T>.SequenceType)(2 * (this._sequenceStart.GetInteger() >> 31) + (this._sequenceEnd.GetInteger() >> 31)));
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00022990 File Offset: 0x00020B90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int GetIndex([System.Memory.IsReadOnly] [In] ref SequencePosition position)
		{
			return position.GetInteger() & int.MaxValue;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x000229A0 File Offset: 0x00020BA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReadOnlySequence<T> SliceImpl([System.Memory.IsReadOnly] [In] ref SequencePosition start, [System.Memory.IsReadOnly] [In] ref SequencePosition end)
		{
			return new ReadOnlySequence<T>(start.GetObject(), ReadOnlySequence<T>.GetIndex(ref start) | (this._sequenceStart.GetInteger() & int.MinValue), end.GetObject(), ReadOnlySequence<T>.GetIndex(ref end) | (this._sequenceEnd.GetInteger() & int.MinValue));
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000229F4 File Offset: 0x00020BF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private long GetLength()
		{
			int index = ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			int index2 = ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object @object = this._sequenceStart.GetObject();
			object object2 = this._sequenceEnd.GetObject();
			if (@object != object2)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				ReadOnlySequenceSegment<T> readOnlySequenceSegment2 = (ReadOnlySequenceSegment<T>)object2;
				return readOnlySequenceSegment2.RunningIndex + (long)index2 - (readOnlySequenceSegment.RunningIndex + (long)index);
			}
			return (long)(index2 - index);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00022A68 File Offset: 0x00020C68
		internal bool TryGetReadOnlySequenceSegment(out ReadOnlySequenceSegment<T> startSegment, out int startIndex, out ReadOnlySequenceSegment<T> endSegment, out int endIndex)
		{
			object @object = this._sequenceStart.GetObject();
			if (@object == null || this.GetSequenceType() != ReadOnlySequence<T>.SequenceType.MultiSegment)
			{
				startSegment = null;
				startIndex = 0;
				endSegment = null;
				endIndex = 0;
				return false;
			}
			startSegment = (ReadOnlySequenceSegment<T>)@object;
			startIndex = ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			endSegment = (ReadOnlySequenceSegment<T>)this._sequenceEnd.GetObject();
			endIndex = ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			return true;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00022ADC File Offset: 0x00020CDC
		internal bool TryGetArray(out ArraySegment<T> segment)
		{
			if (this.GetSequenceType() != ReadOnlySequence<T>.SequenceType.Array)
			{
				segment = default(ArraySegment<T>);
				return false;
			}
			int index = ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			segment = new ArraySegment<T>((T[])this._sequenceStart.GetObject(), index, ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd) - index);
			return true;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00022B38 File Offset: 0x00020D38
		internal bool TryGetString(out string text, out int start, out int length)
		{
			if (typeof(T) != typeof(char) || this.GetSequenceType() != ReadOnlySequence<T>.SequenceType.String)
			{
				start = 0;
				length = 0;
				text = null;
				return false;
			}
			start = ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			length = ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd) - start;
			text = (string)this._sequenceStart.GetObject();
			return true;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00022BB0 File Offset: 0x00020DB0
		private static bool InRange(uint value, uint start, uint end)
		{
			return value - start <= end - start;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00022BC0 File Offset: 0x00020DC0
		private static bool InRange(ulong value, ulong start, ulong end)
		{
			return value - start <= end - start;
		}

		// Token: 0x0400024C RID: 588
		private readonly SequencePosition _sequenceStart;

		// Token: 0x0400024D RID: 589
		private readonly SequencePosition _sequenceEnd;

		// Token: 0x0400024E RID: 590
		public static readonly ReadOnlySequence<T> Empty = new ReadOnlySequence<T>(SpanHelpers.PerTypeValues<T>.EmptyArray);

		// Token: 0x0200014D RID: 333
		public struct Enumerator
		{
			// Token: 0x06000A26 RID: 2598 RVA: 0x0002C6D0 File Offset: 0x0002A8D0
			public Enumerator([System.Memory.IsReadOnly] [In] ref ReadOnlySequence<T> sequence)
			{
				this._currentMemory = default(ReadOnlyMemory<T>);
				this._next = sequence.Start;
				this._sequence = sequence;
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0002C6F8 File Offset: 0x0002A8F8
			public ReadOnlyMemory<T> Current
			{
				get
				{
					return this._currentMemory;
				}
			}

			// Token: 0x06000A28 RID: 2600 RVA: 0x0002C700 File Offset: 0x0002A900
			public bool MoveNext()
			{
				return this._next.GetObject() != null && this._sequence.TryGet(ref this._next, out this._currentMemory, true);
			}

			// Token: 0x04000331 RID: 817
			private readonly ReadOnlySequence<T> _sequence;

			// Token: 0x04000332 RID: 818
			private SequencePosition _next;

			// Token: 0x04000333 RID: 819
			private ReadOnlyMemory<T> _currentMemory;
		}

		// Token: 0x0200014E RID: 334
		private enum SequenceType
		{
			// Token: 0x04000335 RID: 821
			MultiSegment,
			// Token: 0x04000336 RID: 822
			Array,
			// Token: 0x04000337 RID: 823
			MemoryManager,
			// Token: 0x04000338 RID: 824
			String,
			// Token: 0x04000339 RID: 825
			Empty
		}
	}
}
