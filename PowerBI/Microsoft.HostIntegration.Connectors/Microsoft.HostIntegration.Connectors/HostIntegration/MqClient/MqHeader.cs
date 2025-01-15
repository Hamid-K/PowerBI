using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B2D RID: 2861
	public abstract class MqHeader
	{
		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x06005A13 RID: 23059
		public abstract int AsciiStructId { get; }

		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x06005A14 RID: 23060
		public abstract int EbcdicStructId { get; }

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x06005A15 RID: 23061 RVA: 0x00002B16 File Offset: 0x00000D16
		public virtual int MinimumVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x06005A16 RID: 23062 RVA: 0x00002B16 File Offset: 0x00000D16
		public virtual int MaximumVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x06005A17 RID: 23063 RVA: 0x00173D57 File Offset: 0x00171F57
		// (set) Token: 0x06005A18 RID: 23064 RVA: 0x00173D5F File Offset: 0x00171F5F
		public MqHeaderType HeaderType { get; private set; }

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x06005A19 RID: 23065 RVA: 0x00173D68 File Offset: 0x00171F68
		// (set) Token: 0x06005A1A RID: 23066 RVA: 0x00173D70 File Offset: 0x00171F70
		internal OrderedMqHeaderType OrderedHeaderType { get; private set; }

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x06005A1B RID: 23067 RVA: 0x00173D79 File Offset: 0x00171F79
		// (set) Token: 0x06005A1C RID: 23068 RVA: 0x00173D81 File Offset: 0x00171F81
		public string Name { get; private set; }

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x06005A1D RID: 23069 RVA: 0x00173D8A File Offset: 0x00171F8A
		// (set) Token: 0x06005A1E RID: 23070 RVA: 0x00173D92 File Offset: 0x00171F92
		public string FormatString { get; private set; }

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x06005A1F RID: 23071 RVA: 0x00173D9B File Offset: 0x00171F9B
		// (set) Token: 0x06005A20 RID: 23072 RVA: 0x00173DA3 File Offset: 0x00171FA3
		public bool AddToHeaderCollection { get; private set; }

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x06005A21 RID: 23073 RVA: 0x00173DAC File Offset: 0x00171FAC
		internal virtual int SendLength
		{
			get
			{
				return this.fixedPartOfHeaders;
			}
		}

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x06005A22 RID: 23074 RVA: 0x00173DB4 File Offset: 0x00171FB4
		internal virtual int MinimumReadLength
		{
			get
			{
				return this.minimumHeaderReadLength;
			}
		}

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x06005A23 RID: 23075 RVA: 0x00173DAC File Offset: 0x00171FAC
		internal virtual int BytesConsumed
		{
			get
			{
				return this.fixedPartOfHeaders;
			}
		}

		// Token: 0x06005A24 RID: 23076 RVA: 0x00173DBC File Offset: 0x00171FBC
		protected MqHeader(MqHeaderType headerType, OrderedMqHeaderType orderedHeaderType, string name, string formatString, int fixedHeaderLength)
			: this(headerType, orderedHeaderType, name, formatString, fixedHeaderLength, true)
		{
		}

		// Token: 0x06005A25 RID: 23077 RVA: 0x00173DCC File Offset: 0x00171FCC
		protected MqHeader(MqHeaderType headerType, OrderedMqHeaderType orderedHeaderType, string name, string formatString, int fixedHeaderLength, int minimumHeaderReadLength)
			: this(headerType, orderedHeaderType, name, formatString, fixedHeaderLength, minimumHeaderReadLength, true)
		{
		}

		// Token: 0x06005A26 RID: 23078 RVA: 0x00173DDE File Offset: 0x00171FDE
		protected MqHeader(MqHeaderType headerType, OrderedMqHeaderType orderedHeaderType, string name, string formatString, int fixedHeaderLength, bool addToHeaderCollection)
			: this(headerType, orderedHeaderType, name, formatString, fixedHeaderLength, fixedHeaderLength, true)
		{
		}

		// Token: 0x06005A27 RID: 23079 RVA: 0x00173DF0 File Offset: 0x00171FF0
		protected MqHeader(MqHeaderType headerType, OrderedMqHeaderType orderedHeaderType, string name, string formatString, int fixedHeaderLength, int minimumHeaderReadLength, bool addToHeaderCollection)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new InvalidOperationException("name of header is empty");
			}
			if (string.IsNullOrWhiteSpace(formatString))
			{
				throw new InvalidOperationException("format string is empty");
			}
			if (formatString.Length > 8)
			{
				throw new InvalidOperationException("format string is too long");
			}
			if (fixedHeaderLength < 0)
			{
				throw new InvalidOperationException("fixed header length < 0");
			}
			if (minimumHeaderReadLength < 0)
			{
				throw new InvalidOperationException("minimum header read length < 0");
			}
			this.HeaderType = headerType;
			this.OrderedHeaderType = orderedHeaderType;
			this.Name = name;
			this.FormatString = formatString;
			this.fixedPartOfHeaders = fixedHeaderLength;
			this.minimumHeaderReadLength = minimumHeaderReadLength;
			this.AddToHeaderCollection = addToHeaderCollection;
		}

		// Token: 0x06005A28 RID: 23080 RVA: 0x000036A9 File Offset: 0x000018A9
		internal virtual void Prepare()
		{
		}

		// Token: 0x06005A29 RID: 23081
		internal abstract void GenerateBytes(byte[] buffer, int offset, string formatOfNextheader, bool littleEndian, int ccsid, int numericEncodingValue, int ccsidValue);

		// Token: 0x06005A2A RID: 23082
		internal abstract bool TryExtract(byte[] buffer, int numberOfBytesAvailable, int offset, bool truncationInEffect, ref int ccsidToUse, ref int numericEncodingToUse, out string nextFormat);

		// Token: 0x06005A2B RID: 23083
		internal abstract MqHeader GenerateCopy(bool deepCopy);

		// Token: 0x04004746 RID: 18246
		private int fixedPartOfHeaders;

		// Token: 0x04004747 RID: 18247
		private int minimumHeaderReadLength;
	}
}
