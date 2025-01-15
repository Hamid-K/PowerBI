using System;
using System.Text;
using Microsoft.Diagnostics.Tracing.Internal;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000036 RID: 54
	internal class FieldMetadata
	{
		// Token: 0x060001BA RID: 442 RVA: 0x0000BB5E File Offset: 0x00009D5E
		public FieldMetadata(string name, TraceLoggingDataType type, EventFieldTags tags, bool variableCount)
			: this(name, type, tags, variableCount ? 64 : 0, 0, null)
		{
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000BB74 File Offset: 0x00009D74
		public FieldMetadata(string name, TraceLoggingDataType type, EventFieldTags tags, ushort fixedCount)
			: this(name, type, tags, 32, fixedCount, null)
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000BB84 File Offset: 0x00009D84
		public FieldMetadata(string name, TraceLoggingDataType type, EventFieldTags tags, byte[] custom)
			: this(name, type, tags, 96, checked((ushort)((custom == null) ? 0 : custom.Length)), custom)
		{
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000BBA0 File Offset: 0x00009DA0
		private FieldMetadata(string name, TraceLoggingDataType dataType, EventFieldTags tags, byte countFlags, ushort fixedCount = 0, byte[] custom = null)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", "This usually means that the object passed to Write is of a type that does not support being used as the top-level object in an event, e.g. a primitive or built-in type.");
			}
			Statics.CheckName(name);
			int num = (int)(dataType & (TraceLoggingDataType)31);
			this.name = name;
			this.nameSize = Encoding.UTF8.GetByteCount(this.name) + 1;
			this.inType = (byte)(num | (int)countFlags);
			this.outType = (byte)((dataType >> 8) & (TraceLoggingDataType)127);
			this.tags = tags;
			this.fixedCount = fixedCount;
			this.custom = custom;
			if (countFlags != 0)
			{
				if (num == 0)
				{
					throw new NotSupportedException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_NotSupportedArrayOfNil", Array.Empty<object>()));
				}
				if (num == 14)
				{
					throw new NotSupportedException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_NotSupportedArrayOfBinary", Array.Empty<object>()));
				}
				if (num == 1 || num == 2)
				{
					throw new NotSupportedException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_NotSupportedArrayOfNullTerminatedString", Array.Empty<object>()));
				}
			}
			if ((this.tags & (EventFieldTags)268435455) != EventFieldTags.None)
			{
				this.outType |= 128;
			}
			if (this.outType != 0)
			{
				this.inType |= 128;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		public void IncrementStructFieldCount()
		{
			this.inType |= 128;
			this.outType += 1;
			if ((this.outType & 127) == 0)
			{
				throw new NotSupportedException(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_TooManyFields", Array.Empty<object>()));
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000BD00 File Offset: 0x00009F00
		public void Encode(ref int pos, byte[] metadata)
		{
			if (metadata != null)
			{
				Encoding.UTF8.GetBytes(this.name, 0, this.name.Length, metadata, pos);
			}
			pos += this.nameSize;
			if (metadata != null)
			{
				metadata[pos] = this.inType;
			}
			pos++;
			if ((this.inType & 128) != 0)
			{
				if (metadata != null)
				{
					metadata[pos] = this.outType;
				}
				pos++;
				if ((this.outType & 128) != 0)
				{
					Statics.EncodeTags((int)this.tags, ref pos, metadata);
				}
			}
			if ((this.inType & 32) != 0)
			{
				if (metadata != null)
				{
					metadata[pos] = (byte)this.fixedCount;
					metadata[pos + 1] = (byte)(this.fixedCount >> 8);
				}
				pos += 2;
				if (96 == (this.inType & 96) && this.fixedCount != 0)
				{
					if (metadata != null)
					{
						Buffer.BlockCopy(this.custom, 0, metadata, pos, (int)this.fixedCount);
					}
					pos += (int)this.fixedCount;
				}
			}
		}

		// Token: 0x040000E5 RID: 229
		private readonly string name;

		// Token: 0x040000E6 RID: 230
		private readonly int nameSize;

		// Token: 0x040000E7 RID: 231
		private readonly EventFieldTags tags;

		// Token: 0x040000E8 RID: 232
		private readonly byte[] custom;

		// Token: 0x040000E9 RID: 233
		private readonly ushort fixedCount;

		// Token: 0x040000EA RID: 234
		private byte inType;

		// Token: 0x040000EB RID: 235
		private byte outType;
	}
}
