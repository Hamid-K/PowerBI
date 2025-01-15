using System;
using System.Collections.Generic;
using Microsoft.Diagnostics.Tracing.Internal;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000072 RID: 114
	internal class TraceLoggingMetadataCollector
	{
		// Token: 0x060002BF RID: 703 RVA: 0x0000DDE5 File Offset: 0x0000BFE5
		internal TraceLoggingMetadataCollector()
		{
			this.impl = new TraceLoggingMetadataCollector.Impl();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000DE03 File Offset: 0x0000C003
		private TraceLoggingMetadataCollector(TraceLoggingMetadataCollector other, FieldMetadata group)
		{
			this.impl = other.impl;
			this.currentGroup = group;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000DE29 File Offset: 0x0000C029
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000DE31 File Offset: 0x0000C031
		internal EventFieldTags Tags { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000DE3A File Offset: 0x0000C03A
		internal int ScratchSize
		{
			get
			{
				return (int)this.impl.scratchSize;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000DE47 File Offset: 0x0000C047
		internal int DataCount
		{
			get
			{
				return (int)this.impl.dataCount;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000DE54 File Offset: 0x0000C054
		internal int PinCount
		{
			get
			{
				return (int)this.impl.pinCount;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000DE61 File Offset: 0x0000C061
		private bool BeginningBufferedArray
		{
			get
			{
				return this.bufferedArrayFieldCount == 0;
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000DE6C File Offset: 0x0000C06C
		public TraceLoggingMetadataCollector AddGroup(string name)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = this;
			if (name != null || this.BeginningBufferedArray)
			{
				FieldMetadata fieldMetadata = new FieldMetadata(name, TraceLoggingDataType.Struct, EventFieldTags.None, this.BeginningBufferedArray);
				this.AddField(fieldMetadata);
				traceLoggingMetadataCollector = new TraceLoggingMetadataCollector(this, fieldMetadata);
			}
			return traceLoggingMetadataCollector;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		public void AddScalar(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			int num;
			switch (traceLoggingDataType)
			{
			case TraceLoggingDataType.Int8:
			case TraceLoggingDataType.UInt8:
				break;
			case TraceLoggingDataType.Int16:
			case TraceLoggingDataType.UInt16:
				goto IL_006F;
			case TraceLoggingDataType.Int32:
			case TraceLoggingDataType.UInt32:
			case TraceLoggingDataType.Float:
			case TraceLoggingDataType.Boolean32:
			case TraceLoggingDataType.HexInt32:
				num = 4;
				goto IL_008B;
			case TraceLoggingDataType.Int64:
			case TraceLoggingDataType.UInt64:
			case TraceLoggingDataType.Double:
			case TraceLoggingDataType.FileTime:
			case TraceLoggingDataType.HexInt64:
				num = 8;
				goto IL_008B;
			case TraceLoggingDataType.Binary:
			case (TraceLoggingDataType)16:
			case (TraceLoggingDataType)19:
				goto IL_0080;
			case TraceLoggingDataType.Guid:
			case TraceLoggingDataType.SystemTime:
				num = 16;
				goto IL_008B;
			default:
				if (traceLoggingDataType != TraceLoggingDataType.Char8)
				{
					if (traceLoggingDataType != TraceLoggingDataType.Char16)
					{
						goto IL_0080;
					}
					goto IL_006F;
				}
				break;
			}
			num = 1;
			goto IL_008B;
			IL_006F:
			num = 2;
			goto IL_008B;
			IL_0080:
			throw new ArgumentOutOfRangeException("type");
			IL_008B:
			this.impl.AddScalar(num);
			this.AddField(new FieldMetadata(name, type, this.Tags, this.BeginningBufferedArray));
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000DF68 File Offset: 0x0000C168
		public void AddBinary(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			if (traceLoggingDataType != TraceLoggingDataType.Binary && traceLoggingDataType - TraceLoggingDataType.CountedUtf16String > 1)
			{
				throw new ArgumentOutOfRangeException("type");
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, this.BeginningBufferedArray));
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000DFC4 File Offset: 0x0000C1C4
		public void AddArray(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			switch (traceLoggingDataType)
			{
			case TraceLoggingDataType.Utf16String:
			case TraceLoggingDataType.MbcsString:
			case TraceLoggingDataType.Int8:
			case TraceLoggingDataType.UInt8:
			case TraceLoggingDataType.Int16:
			case TraceLoggingDataType.UInt16:
			case TraceLoggingDataType.Int32:
			case TraceLoggingDataType.UInt32:
			case TraceLoggingDataType.Int64:
			case TraceLoggingDataType.UInt64:
			case TraceLoggingDataType.Float:
			case TraceLoggingDataType.Double:
			case TraceLoggingDataType.Boolean32:
			case TraceLoggingDataType.Guid:
			case TraceLoggingDataType.FileTime:
			case TraceLoggingDataType.HexInt32:
			case TraceLoggingDataType.HexInt64:
				goto IL_007C;
			case TraceLoggingDataType.Binary:
			case (TraceLoggingDataType)16:
			case TraceLoggingDataType.SystemTime:
			case (TraceLoggingDataType)19:
				break;
			default:
				if (traceLoggingDataType == TraceLoggingDataType.Char8 || traceLoggingDataType == TraceLoggingDataType.Char16)
				{
					goto IL_007C;
				}
				break;
			}
			throw new ArgumentOutOfRangeException("type");
			IL_007C:
			if (this.BeginningBufferedArray)
			{
				throw new NotSupportedException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_NotSupportedNestedArraysEnums", Array.Empty<object>()));
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, true));
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000E095 File Offset: 0x0000C295
		public void BeginBufferedArray()
		{
			if (this.bufferedArrayFieldCount >= 0)
			{
				throw new NotSupportedException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_NotSupportedNestedArraysEnums", Array.Empty<object>()));
			}
			this.bufferedArrayFieldCount = 0;
			this.impl.BeginBuffered();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000E0C7 File Offset: 0x0000C2C7
		public void EndBufferedArray()
		{
			if (this.bufferedArrayFieldCount != 1)
			{
				throw new InvalidOperationException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_IncorrentlyAuthoredTypeInfo", Array.Empty<object>()));
			}
			this.bufferedArrayFieldCount = int.MinValue;
			this.impl.EndBuffered();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000E100 File Offset: 0x0000C300
		public void AddCustom(string name, TraceLoggingDataType type, byte[] metadata)
		{
			if (this.BeginningBufferedArray)
			{
				throw new NotSupportedException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_NotSupportedCustomSerializedData", Array.Empty<object>()));
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, metadata));
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000E158 File Offset: 0x0000C358
		internal byte[] GetMetadata()
		{
			byte[] array = new byte[this.impl.Encode(null)];
			this.impl.Encode(array);
			return array;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000E185 File Offset: 0x0000C385
		private void AddField(FieldMetadata fieldMetadata)
		{
			this.Tags = EventFieldTags.None;
			this.bufferedArrayFieldCount++;
			this.impl.fields.Add(fieldMetadata);
			if (this.currentGroup != null)
			{
				this.currentGroup.IncrementStructFieldCount();
			}
		}

		// Token: 0x04000144 RID: 324
		private readonly TraceLoggingMetadataCollector.Impl impl;

		// Token: 0x04000145 RID: 325
		private readonly FieldMetadata currentGroup;

		// Token: 0x04000146 RID: 326
		private int bufferedArrayFieldCount = int.MinValue;

		// Token: 0x02000090 RID: 144
		private class Impl
		{
			// Token: 0x0600031E RID: 798 RVA: 0x0000EF0A File Offset: 0x0000D10A
			public void AddScalar(int size)
			{
				checked
				{
					if (this.bufferNesting == 0)
					{
						if (!this.scalar)
						{
							this.dataCount += 1;
						}
						this.scalar = true;
						this.scratchSize = (short)((int)this.scratchSize + size);
					}
				}
			}

			// Token: 0x0600031F RID: 799 RVA: 0x0000EF41 File Offset: 0x0000D141
			public void AddNonscalar()
			{
				checked
				{
					if (this.bufferNesting == 0)
					{
						this.scalar = false;
						this.pinCount += 1;
						this.dataCount += 1;
					}
				}
			}

			// Token: 0x06000320 RID: 800 RVA: 0x0000EF70 File Offset: 0x0000D170
			public void BeginBuffered()
			{
				if (this.bufferNesting == 0)
				{
					this.AddNonscalar();
				}
				this.bufferNesting++;
			}

			// Token: 0x06000321 RID: 801 RVA: 0x0000EF8E File Offset: 0x0000D18E
			public void EndBuffered()
			{
				this.bufferNesting--;
			}

			// Token: 0x06000322 RID: 802 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
			public int Encode(byte[] metadata)
			{
				int num = 0;
				foreach (FieldMetadata fieldMetadata in this.fields)
				{
					fieldMetadata.Encode(ref num, metadata);
				}
				return num;
			}

			// Token: 0x040001BC RID: 444
			internal readonly List<FieldMetadata> fields = new List<FieldMetadata>();

			// Token: 0x040001BD RID: 445
			internal short scratchSize;

			// Token: 0x040001BE RID: 446
			internal sbyte dataCount;

			// Token: 0x040001BF RID: 447
			internal sbyte pinCount;

			// Token: 0x040001C0 RID: 448
			private int bufferNesting;

			// Token: 0x040001C1 RID: 449
			private bool scalar;
		}
	}
}
