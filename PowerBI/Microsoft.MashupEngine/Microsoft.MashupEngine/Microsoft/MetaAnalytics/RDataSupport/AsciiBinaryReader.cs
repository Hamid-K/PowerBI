using System;
using System.Globalization;
using System.IO;
using Microsoft.Analytics.Modules.R.ErrorHandling.RException.Primitives;

namespace Microsoft.MetaAnalytics.RDataSupport
{
	// Token: 0x02000167 RID: 359
	internal sealed class AsciiBinaryReader : MultiEncodingBinaryReader
	{
		// Token: 0x060006CB RID: 1739 RVA: 0x0000B0D5 File Offset: 0x000092D5
		public AsciiBinaryReader(Stream input)
			: base(input)
		{
			this.reader = new StreamReader(input);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0000B0EA File Offset: 0x000092EA
		public override char[] ReadChars(int count)
		{
			return this.reader.ReadLine().ToCharArray();
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0000B0FC File Offset: 0x000092FC
		public override decimal ReadDecimal()
		{
			throw new NotImplementedTypeParserException(typeof(decimal));
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0000B10D File Offset: 0x0000930D
		public override double ReadDouble()
		{
			return double.Parse(this.reader.ReadLine(), CultureInfo.InvariantCulture);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0000B124 File Offset: 0x00009324
		public override short ReadInt16()
		{
			return short.Parse(this.reader.ReadLine(), CultureInfo.InvariantCulture);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0000B13B File Offset: 0x0000933B
		public override int ReadInt32()
		{
			return int.Parse(this.reader.ReadLine(), CultureInfo.InvariantCulture);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0000B152 File Offset: 0x00009352
		public override long ReadInt64()
		{
			return long.Parse(this.reader.ReadLine(), CultureInfo.InvariantCulture);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0000B169 File Offset: 0x00009369
		public override float ReadSingle()
		{
			return float.Parse(this.reader.ReadLine(), CultureInfo.InvariantCulture);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0000B180 File Offset: 0x00009380
		public override string ReadString()
		{
			throw new NotImplementedTypeParserException(typeof(string));
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0000B180 File Offset: 0x00009380
		public override string ReadString(MultiEncodingBinaryReader.EncodingOptions encoding)
		{
			throw new NotImplementedTypeParserException(typeof(string));
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0000B191 File Offset: 0x00009391
		public override ushort ReadUInt16()
		{
			return ushort.Parse(this.reader.ReadLine(), CultureInfo.InvariantCulture);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000B1A8 File Offset: 0x000093A8
		public override uint ReadUInt32()
		{
			return uint.Parse(this.reader.ReadLine(), CultureInfo.InvariantCulture);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0000B1BF File Offset: 0x000093BF
		public override ulong ReadUInt64()
		{
			return ulong.Parse(this.reader.ReadLine(), CultureInfo.InvariantCulture);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0000B1D8 File Offset: 0x000093D8
		internal int? ReadInteger()
		{
			string text = this.reader.ReadLine();
			if (text == "NA")
			{
				return null;
			}
			return new int?(int.Parse(text, CultureInfo.InvariantCulture));
		}

		// Token: 0x04000401 RID: 1025
		private readonly StreamReader reader;
	}
}
