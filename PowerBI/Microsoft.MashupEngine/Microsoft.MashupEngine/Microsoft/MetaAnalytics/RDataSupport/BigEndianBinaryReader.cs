using System;
using System.IO;
using System.Text;
using Microsoft.Analytics.Modules.R.ErrorHandling.RException.Primitives;

namespace Microsoft.MetaAnalytics.RDataSupport
{
	// Token: 0x02000168 RID: 360
	internal sealed class BigEndianBinaryReader : MultiEncodingBinaryReader
	{
		// Token: 0x060006D9 RID: 1753 RVA: 0x0000B218 File Offset: 0x00009418
		public BigEndianBinaryReader(Stream input)
			: base(input)
		{
			this.buffer = new byte[16];
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0000B0FC File Offset: 0x000092FC
		public override decimal ReadDecimal()
		{
			throw new NotImplementedTypeParserException(typeof(decimal));
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0000B24C File Offset: 0x0000944C
		public unsafe override double ReadDouble()
		{
			this.FillMyBuffer(8);
			ulong num = (ulong)(((int)this.buffer[0] << 24) | ((int)this.buffer[1] << 16) | ((int)this.buffer[2] << 8) | (int)this.buffer[3]);
			uint num2 = (uint)(((int)this.buffer[4] << 24) | ((int)this.buffer[5] << 16) | ((int)this.buffer[6] << 8) | (int)this.buffer[7]);
			ulong num3 = (num << 32) | (ulong)num2;
			return *(double*)(&num3);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0000B2C3 File Offset: 0x000094C3
		public override short ReadInt16()
		{
			this.FillMyBuffer(2);
			return (short)(((int)this.buffer[2] << 8) | (int)this.buffer[3]);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0000B2E0 File Offset: 0x000094E0
		public override int ReadInt32()
		{
			this.FillMyBuffer(4);
			return ((int)this.buffer[0] << 24) | ((int)this.buffer[1] << 16) | ((int)this.buffer[2] << 8) | (int)this.buffer[3];
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0000B314 File Offset: 0x00009514
		public override long ReadInt64()
		{
			this.FillMyBuffer(8);
			ulong num = (ulong)(((int)this.buffer[0] << 24) | ((int)this.buffer[1] << 16) | ((int)this.buffer[2] << 8) | (int)this.buffer[3]);
			uint num2 = (uint)(((int)this.buffer[4] << 24) | ((int)this.buffer[5] << 16) | ((int)this.buffer[6] << 8) | (int)this.buffer[7]);
			return (long)((num << 32) | (ulong)num2);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0000B388 File Offset: 0x00009588
		public unsafe override float ReadSingle()
		{
			this.FillBuffer(4);
			uint num = (uint)(((int)this.buffer[0] << 24) | ((int)this.buffer[1] << 16) | ((int)this.buffer[2] << 8) | (int)this.buffer[3]);
			return *(float*)(&num);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0000B3CC File Offset: 0x000095CC
		public override string ReadString(MultiEncodingBinaryReader.EncodingOptions encoding)
		{
			int num = this.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			byte[] array = this.ReadBytes(num);
			string text = string.Empty;
			if (encoding != MultiEncodingBinaryReader.EncodingOptions.Latin1 && encoding == MultiEncodingBinaryReader.EncodingOptions.UTF8)
			{
				text = this.utf8Encoding.GetString(array);
			}
			else
			{
				text = this.latin1Encoding.GetString(array);
			}
			return text;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0000B418 File Offset: 0x00009618
		public override ushort ReadUInt16()
		{
			this.FillMyBuffer(2);
			return (ushort)(((int)this.buffer[0] << 8) | (int)this.buffer[1]);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0000B2E0 File Offset: 0x000094E0
		public override uint ReadUInt32()
		{
			this.FillMyBuffer(4);
			return (uint)(((int)this.buffer[0] << 24) | ((int)this.buffer[1] << 16) | ((int)this.buffer[2] << 8) | (int)this.buffer[3]);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0000B438 File Offset: 0x00009638
		public override ulong ReadUInt64()
		{
			this.FillMyBuffer(8);
			ulong num = (ulong)(((int)this.buffer[0] << 24) | ((int)this.buffer[1] << 16) | ((int)this.buffer[2] << 8) | (int)this.buffer[3]);
			uint num2 = (uint)(((int)this.buffer[4] << 24) | ((int)this.buffer[5] << 16) | ((int)this.buffer[6] << 8) | (int)this.buffer[7]);
			return (num << 32) | (ulong)num2;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0000B4AA File Offset: 0x000096AA
		private void FillMyBuffer(int count)
		{
			if (count == 1)
			{
				this.buffer[0] = this.ReadByte();
				return;
			}
			if (this.Read(this.buffer, 0, count) < count)
			{
				throw new NotImplementedTypeParserException();
			}
		}

		// Token: 0x04000402 RID: 1026
		private readonly byte[] buffer;

		// Token: 0x04000403 RID: 1027
		private readonly Encoding utf8Encoding = new UTF8Encoding(false, true);

		// Token: 0x04000404 RID: 1028
		private readonly Encoding latin1Encoding = Encoding.GetEncoding("ISO-8859-1");
	}
}
