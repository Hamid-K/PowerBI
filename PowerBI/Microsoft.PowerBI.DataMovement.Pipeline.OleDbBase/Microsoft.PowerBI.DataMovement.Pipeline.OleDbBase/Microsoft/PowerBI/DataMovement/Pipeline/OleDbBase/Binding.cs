using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200000E RID: 14
	public class Binding
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002890 File Offset: 0x00000A90
		public Binding(ref DBBINDING nativeBinding)
		{
			this.ordinal = nativeBinding.Ordinal;
			this.valueOffset = nativeBinding.Value;
			this.lengthOffset = nativeBinding.Length;
			this.statusOffset = nativeBinding.Status;
			this.part = nativeBinding.Part;
			this.memoryOwner = nativeBinding.MemOwner;
			this.paramIO = nativeBinding.ParamIO;
			this.destMaxLength = nativeBinding.MaxLen;
			this.flags = nativeBinding.Flags;
			this.destType = nativeBinding.Type;
			this.precision = nativeBinding.Precision;
			this.scale = nativeBinding.Scale;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002933 File Offset: 0x00000B33
		public DBORDINAL Ordinal
		{
			get
			{
				return this.ordinal;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000293B File Offset: 0x00000B3B
		public DBBYTEOFFSET ValueOffset
		{
			get
			{
				return this.valueOffset;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002943 File Offset: 0x00000B43
		public DBBYTEOFFSET LengthOffset
		{
			get
			{
				return this.lengthOffset;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000294B File Offset: 0x00000B4B
		public DBBYTEOFFSET StatusOffset
		{
			get
			{
				return this.statusOffset;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002953 File Offset: 0x00000B53
		public DBPART Part
		{
			get
			{
				return this.part;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000295B File Offset: 0x00000B5B
		public DBMEMOWNER MemoryOwner
		{
			get
			{
				return this.memoryOwner;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002963 File Offset: 0x00000B63
		public DBPARAMIO ParamIO
		{
			get
			{
				return this.paramIO;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000296B File Offset: 0x00000B6B
		public DBLENGTH DestMaxLength
		{
			get
			{
				return this.destMaxLength;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002973 File Offset: 0x00000B73
		public uint Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000297B File Offset: 0x00000B7B
		public DBTYPE DestType
		{
			get
			{
				return this.destType;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002983 File Offset: 0x00000B83
		public byte Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000298B File Offset: 0x00000B8B
		public byte Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x0400001D RID: 29
		private readonly DBORDINAL ordinal;

		// Token: 0x0400001E RID: 30
		private readonly DBBYTEOFFSET valueOffset;

		// Token: 0x0400001F RID: 31
		private readonly DBBYTEOFFSET lengthOffset;

		// Token: 0x04000020 RID: 32
		private readonly DBBYTEOFFSET statusOffset;

		// Token: 0x04000021 RID: 33
		private readonly DBPART part;

		// Token: 0x04000022 RID: 34
		private readonly DBMEMOWNER memoryOwner;

		// Token: 0x04000023 RID: 35
		private readonly DBPARAMIO paramIO;

		// Token: 0x04000024 RID: 36
		private readonly DBLENGTH destMaxLength;

		// Token: 0x04000025 RID: 37
		private readonly uint flags;

		// Token: 0x04000026 RID: 38
		private readonly DBTYPE destType;

		// Token: 0x04000027 RID: 39
		private readonly byte precision;

		// Token: 0x04000028 RID: 40
		private readonly byte scale;
	}
}
