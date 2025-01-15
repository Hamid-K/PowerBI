using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B28 RID: 2856
	public abstract class Message
	{
		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x0600599B RID: 22939 RVA: 0x00171A9B File Offset: 0x0016FC9B
		// (set) Token: 0x0600599C RID: 22940 RVA: 0x00171AA3 File Offset: 0x0016FCA3
		public MessageType MessageType { get; protected set; }

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x0600599D RID: 22941 RVA: 0x00171AAC File Offset: 0x0016FCAC
		// (set) Token: 0x0600599E RID: 22942 RVA: 0x00171AB4 File Offset: 0x0016FCB4
		public Report Report { get; set; }

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x0600599F RID: 22943 RVA: 0x00171ABD File Offset: 0x0016FCBD
		// (set) Token: 0x060059A0 RID: 22944 RVA: 0x00171AC5 File Offset: 0x0016FCC5
		public int Ccsid { get; set; }

		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x060059A1 RID: 22945 RVA: 0x00171ACE File Offset: 0x0016FCCE
		// (set) Token: 0x060059A2 RID: 22946 RVA: 0x00171AD6 File Offset: 0x0016FCD6
		public NumericEncoding NumericEncoding { get; set; }

		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x060059A3 RID: 22947 RVA: 0x00171ADF File Offset: 0x0016FCDF
		// (set) Token: 0x060059A4 RID: 22948 RVA: 0x00171AE7 File Offset: 0x0016FCE7
		public byte[] Data { get; set; }

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x060059A5 RID: 22949 RVA: 0x00171AF0 File Offset: 0x0016FCF0
		// (set) Token: 0x060059A6 RID: 22950 RVA: 0x00171AF8 File Offset: 0x0016FCF8
		public int ExpirationInterval
		{
			get
			{
				return this.expirationInterval;
			}
			set
			{
				if (value < 1 && value != -1)
				{
					throw new ArgumentOutOfRangeException("ExpirationInterval", SR.PositiveOrUnlimited);
				}
				this.expirationInterval = value;
			}
		}

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x060059A7 RID: 22951 RVA: 0x00171B19 File Offset: 0x0016FD19
		// (set) Token: 0x060059A8 RID: 22952 RVA: 0x00171B21 File Offset: 0x0016FD21
		public Priority Priority { get; set; }

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x060059A9 RID: 22953 RVA: 0x00171B2A File Offset: 0x0016FD2A
		// (set) Token: 0x060059AA RID: 22954 RVA: 0x00171B32 File Offset: 0x0016FD32
		public Persistence Persistence { get; set; }

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x060059AB RID: 22955 RVA: 0x00171B3B File Offset: 0x0016FD3B
		// (set) Token: 0x060059AC RID: 22956 RVA: 0x00171B43 File Offset: 0x0016FD43
		public byte[] MessageId
		{
			get
			{
				return this.messageId;
			}
			set
			{
				this.messageId = Globals.CheckExactLength(value, "MessageId", 24);
			}
		}

		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x060059AD RID: 22957 RVA: 0x00171B58 File Offset: 0x0016FD58
		// (set) Token: 0x060059AE RID: 22958 RVA: 0x00171B60 File Offset: 0x0016FD60
		public byte[] Correlator
		{
			get
			{
				return this.correlator;
			}
			set
			{
				this.correlator = Globals.CheckExactLength(value, "Correlator", 24);
			}
		}

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x060059AF RID: 22959 RVA: 0x00171B75 File Offset: 0x0016FD75
		// (set) Token: 0x060059B0 RID: 22960 RVA: 0x00171B7D File Offset: 0x0016FD7D
		public string ReplyToQueue
		{
			get
			{
				return this.replyToQueue;
			}
			set
			{
				this.replyToQueue = Globals.CheckMaximumLength(value, "ReplyToQueue", 48);
			}
		}

		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x060059B1 RID: 22961 RVA: 0x00171B92 File Offset: 0x0016FD92
		// (set) Token: 0x060059B2 RID: 22962 RVA: 0x00171B9A File Offset: 0x0016FD9A
		public string ReplyToQueueManager
		{
			get
			{
				return this.replyToQueueManager;
			}
			set
			{
				this.replyToQueueManager = Globals.CheckMaximumLength(value, "ReplyToQueueManager", 48);
			}
		}

		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x060059B3 RID: 22963 RVA: 0x00171BAF File Offset: 0x0016FDAF
		// (set) Token: 0x060059B4 RID: 22964 RVA: 0x00171BB7 File Offset: 0x0016FDB7
		public byte[] GroupId
		{
			get
			{
				return this.groupId;
			}
			set
			{
				this.groupId = Globals.CheckExactLength(value, "GroupId", 24);
			}
		}

		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x060059B5 RID: 22965 RVA: 0x00171BCC File Offset: 0x0016FDCC
		// (set) Token: 0x060059B6 RID: 22966 RVA: 0x00171BD4 File Offset: 0x0016FDD4
		public int SequenceNumber
		{
			get
			{
				return this.sequenceNumber;
			}
			set
			{
				this.sequenceNumber = Globals.CheckRange(value, "SequenceNumber", 1, 999999999);
			}
		}

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x060059B7 RID: 22967 RVA: 0x00171BED File Offset: 0x0016FDED
		// (set) Token: 0x060059B8 RID: 22968 RVA: 0x00171BF5 File Offset: 0x0016FDF5
		public int Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = Globals.CheckRange(value, "Offset", 0, 999999999);
			}
		}

		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x060059B9 RID: 22969 RVA: 0x00171C0E File Offset: 0x0016FE0E
		// (set) Token: 0x060059BA RID: 22970 RVA: 0x00171C16 File Offset: 0x0016FE16
		public MessageFlags MessageFlags { get; set; }

		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x060059BB RID: 22971 RVA: 0x00171C1F File Offset: 0x0016FE1F
		// (set) Token: 0x060059BC RID: 22972 RVA: 0x00171C27 File Offset: 0x0016FE27
		public string Format { get; protected set; }

		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x060059BD RID: 22973 RVA: 0x00171C30 File Offset: 0x0016FE30
		// (set) Token: 0x060059BE RID: 22974 RVA: 0x00171C38 File Offset: 0x0016FE38
		private protected MqHeader[] SortedHeaders { protected get; private set; }

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x060059BF RID: 22975 RVA: 0x00171C41 File Offset: 0x0016FE41
		// (set) Token: 0x060059C0 RID: 22976 RVA: 0x00171C49 File Offset: 0x0016FE49
		private protected bool LastHeaderIsCicsOrIms { protected get; private set; }

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x060059C1 RID: 22977 RVA: 0x00171C52 File Offset: 0x0016FE52
		// (set) Token: 0x060059C2 RID: 22978 RVA: 0x00171C5A File Offset: 0x0016FE5A
		public List<MqHeader> Headers
		{
			get
			{
				return this.headers;
			}
			protected set
			{
				this.headers = value;
			}
		}

		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x060059C3 RID: 22979 RVA: 0x00171C64 File Offset: 0x0016FE64
		public int TotalHeaderLength
		{
			get
			{
				int num = 0;
				if (this.Headers != null && this.Headers.Count != 0)
				{
					foreach (MqHeader mqHeader in this.Headers)
					{
						if (mqHeader != null)
						{
							num += mqHeader.SendLength;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x060059C4 RID: 22980 RVA: 0x00171CD4 File Offset: 0x0016FED4
		// (set) Token: 0x060059C5 RID: 22981 RVA: 0x00171CDC File Offset: 0x0016FEDC
		public MessagePropertyCollection Properties { get; private set; }

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x060059C6 RID: 22982 RVA: 0x00171CE5 File Offset: 0x0016FEE5
		// (set) Token: 0x060059C7 RID: 22983 RVA: 0x00171CED File Offset: 0x0016FEED
		public ContextOption ContextOption { get; protected set; }

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x060059C8 RID: 22984 RVA: 0x00171CF6 File Offset: 0x0016FEF6
		// (set) Token: 0x060059C9 RID: 22985 RVA: 0x00171CFE File Offset: 0x0016FEFE
		public IdentityContext IdentityContext
		{
			get
			{
				return this.identityContext;
			}
			set
			{
				if (this.ContextOption != ContextOption.SetAll && this.ContextOption != ContextOption.SetIdentity)
				{
					throw new ArgumentException("IdentityContext");
				}
				this.identityContext = value;
			}
		}

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x060059CA RID: 22986 RVA: 0x00171D2C File Offset: 0x0016FF2C
		// (set) Token: 0x060059CB RID: 22987 RVA: 0x00171D34 File Offset: 0x0016FF34
		public OriginContext OriginContext
		{
			get
			{
				return this.originContext;
			}
			set
			{
				if (this.ContextOption != ContextOption.SetAll)
				{
					throw new ArgumentException("OriginContext");
				}
				this.originContext = value;
			}
		}

		// Token: 0x060059CC RID: 22988 RVA: 0x00171D55 File Offset: 0x0016FF55
		static Message()
		{
			Message.GetHeaderTypesFromAssembly(Assembly.GetExecutingAssembly());
		}

		// Token: 0x060059CD RID: 22989 RVA: 0x00171D78 File Offset: 0x0016FF78
		private static void GetHeaderTypesFromAssembly(Assembly assembly)
		{
			List<Type> list = new List<Type>();
			foreach (Type type in assembly.GetTypes())
			{
				if (type.IsClass && type.IsSubclassOf(typeof(MqHeader)) && !type.IsAbstract)
				{
					list.Add(type);
				}
			}
			foreach (Type type2 in list)
			{
				ConstructorInfo constructor = type2.GetConstructor(Type.EmptyTypes);
				MqHeader mqHeader = (MqHeader)constructor.Invoke(null);
				string formatString = mqHeader.FormatString;
				Message.formatToConstructorInfo.Add(formatString, constructor);
				int asciiStructId = mqHeader.AsciiStructId;
				int ebcdicStructId = mqHeader.EbcdicStructId;
				int minimumVersion = mqHeader.MinimumVersion;
				int maximumVersion = mqHeader.MaximumVersion;
				if (asciiStructId != -1)
				{
					Dictionary<int, string> dictionary;
					if (!Message.idsVersionsToFormat.TryGetValue(asciiStructId, out dictionary))
					{
						dictionary = new Dictionary<int, string>();
						Message.idsVersionsToFormat.Add(asciiStructId, dictionary);
					}
					for (int j = minimumVersion; j <= maximumVersion; j++)
					{
						dictionary.Add(j, formatString);
					}
					if (!Message.idsVersionsToFormat.TryGetValue(ebcdicStructId, out dictionary))
					{
						dictionary = new Dictionary<int, string>();
						Message.idsVersionsToFormat.Add(ebcdicStructId, dictionary);
					}
					for (int k = minimumVersion; k <= maximumVersion; k++)
					{
						dictionary.Add(k, formatString);
					}
				}
			}
		}

		// Token: 0x060059CE RID: 22990 RVA: 0x00171EE8 File Offset: 0x001700E8
		protected Message(MessageType messageType, List<MqHeader> headers)
		{
			this.MessageType = messageType;
			if (headers != null)
			{
				this.Headers = headers;
			}
			this.Ccsid = 0;
			this.NumericEncoding = NumericEncoding.NativeWindowsEncoding;
			this.ContextOption = ContextOption.None;
			this.Properties = new MessagePropertyCollection(this);
		}

		// Token: 0x060059CF RID: 22991 RVA: 0x00171F50 File Offset: 0x00170150
		internal void PrepareHeaders()
		{
			this.LastHeaderIsCicsOrIms = false;
			this.SortedHeaders = null;
			if (this.Headers.Count == 0)
			{
				return;
			}
			int num = 6;
			List<MqHeader>[] array = new List<MqHeader>[num];
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			for (int i = 0; i < this.Headers.Count; i++)
			{
				MqHeader mqHeader = this.Headers[i];
				if (mqHeader == null)
				{
					throw new CustomMqClientException(SR.HeaderNull);
				}
				int orderedHeaderType = (int)mqHeader.OrderedHeaderType;
				if (array[orderedHeaderType] == null)
				{
					array[orderedHeaderType] = new List<MqHeader>();
				}
				array[orderedHeaderType].Add(mqHeader);
				mqHeader.Prepare();
				num2++;
				if (mqHeader.HeaderType == MqHeaderType.CicsBridge)
				{
					num3++;
				}
				if (mqHeader.HeaderType == MqHeaderType.ImsBridge)
				{
					num4++;
				}
			}
			if (num2 == 0)
			{
				return;
			}
			if (num3 + num4 > 1)
			{
				throw new CustomMqClientException(SR.CicsImsHeaders);
			}
			this.SortedHeaders = new MqHeader[num2];
			int num5 = 0;
			for (int j = 0; j < num; j++)
			{
				if (array[j] != null)
				{
					foreach (MqHeader mqHeader2 in array[j])
					{
						this.SortedHeaders[num5++] = mqHeader2;
					}
				}
			}
			MqHeader mqHeader3 = this.SortedHeaders[num2 - 1];
			this.LastHeaderIsCicsOrIms = mqHeader3.HeaderType == MqHeaderType.CicsBridge || mqHeader3.HeaderType == MqHeaderType.ImsBridge;
		}

		// Token: 0x060059D0 RID: 22992 RVA: 0x001720C0 File Offset: 0x001702C0
		public void GenerateHeaderBytes(byte[] buffer, ref int index)
		{
			if (this.SortedHeaders == null)
			{
				return;
			}
			for (int i = 0; i < this.SortedHeaders.Length; i++)
			{
				MqHeader mqHeader = this.SortedHeaders[i];
				MqHeader mqHeader2 = ((i == this.SortedHeaders.Length - 1) ? null : this.SortedHeaders[i + 1]);
				int num = 546;
				int num2 = 1252;
				bool flag = true;
				int num3 = num2;
				if (mqHeader.HeaderType == MqHeaderType.RulesAndFormattingVersion2)
				{
					flag = (mqHeader as RulesAndFormattingVersion2Header).NumericEncodingForLengths.IsLittleEndian;
				}
				string text = this.Format;
				if (mqHeader2 == null || (i == this.SortedHeaders.Length - 2 && this.LastHeaderIsCicsOrIms))
				{
					num = this.NumericEncoding.NumericValue;
					num2 = this.Ccsid;
					if (mqHeader2 != null)
					{
						text = mqHeader2.FormatString;
					}
					if (mqHeader2 == null && this.LastHeaderIsCicsOrIms)
					{
						flag = this.NumericEncoding.IsLittleEndian;
						num3 = this.Ccsid;
					}
				}
				else
				{
					RulesAndFormattingVersion2Header rulesAndFormattingVersion2Header = mqHeader2 as RulesAndFormattingVersion2Header;
					if (rulesAndFormattingVersion2Header != null)
					{
						num = rulesAndFormattingVersion2Header.NumericEncodingForLengths.NumericValue;
					}
					text = mqHeader2.FormatString;
				}
				mqHeader.GenerateBytes(buffer, index, text, flag, num3, num, num2);
				index += mqHeader.SendLength;
			}
		}

		// Token: 0x060059D1 RID: 22993 RVA: 0x001721E1 File Offset: 0x001703E1
		public int ExtractHeaders(byte[] buffer, int numberOfBytesAvailable, int indexOfHeader)
		{
			return this.ExtractHeaders(buffer, numberOfBytesAvailable, indexOfHeader, false);
		}

		// Token: 0x060059D2 RID: 22994 RVA: 0x001721F0 File Offset: 0x001703F0
		public int ExtractHeaders(byte[] buffer, int numberOfBytesAvailable, int indexOfHeader, bool truncationInEffect)
		{
			int num = 0;
			string text = ((this.Format == null) ? string.Empty : this.Format.Trim());
			if (text != "MQSTR")
			{
				bool flag = false;
				while (!flag)
				{
					int num2 = numberOfBytesAvailable - indexOfHeader;
					if (num2 < 8)
					{
						break;
					}
					string formatFromStructIdAndVersion = this.GetFormatFromStructIdAndVersion(buffer, indexOfHeader);
					if (formatFromStructIdAndVersion == null)
					{
						break;
					}
					bool flag2 = false;
					if (text == null || text.Length == 0)
					{
						text = formatFromStructIdAndVersion;
						flag2 = true;
					}
					else if (text != formatFromStructIdAndVersion)
					{
						break;
					}
					ConstructorInfo constructorInfo = null;
					if (!Message.formatToConstructorInfo.TryGetValue(text, out constructorInfo))
					{
						throw new InvalidOperationException("Don't know how to make this header");
					}
					MqHeader mqHeader = (MqHeader)constructorInfo.Invoke(null);
					if (num2 < mqHeader.MinimumReadLength)
					{
						if (!truncationInEffect && !flag2)
						{
							throw new InvalidOperationException("Not enough bytes for fixed part of header!");
						}
						break;
					}
					else
					{
						string text2;
						bool flag3 = mqHeader.TryExtract(buffer, numberOfBytesAvailable, indexOfHeader, truncationInEffect, ref this.ccsidToUse, ref this.numericEncodingToUse, out text2);
						if (text2 != null)
						{
							text2 = text2.Trim();
						}
						if (text2 == "MQSTR")
						{
							flag = true;
						}
						text = text2;
						if (!flag3)
						{
							break;
						}
						if (mqHeader.AddToHeaderCollection)
						{
							this.Headers.Add(mqHeader);
						}
						indexOfHeader += mqHeader.BytesConsumed;
						num += mqHeader.BytesConsumed;
					}
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				this.Format = null;
			}
			else
			{
				this.Format = text;
			}
			this.NumericEncoding = NumericEncoding.GetAnyInstance(this.numericEncodingToUse);
			this.Ccsid = this.ccsidToUse;
			return num;
		}

		// Token: 0x060059D3 RID: 22995 RVA: 0x00172364 File Offset: 0x00170564
		private unsafe string GetFormatFromStructIdAndVersion(byte[] buffer, int indexOfHeader)
		{
			if (!NumericEncoding.IsValidIntegerEndianness(this.numericEncodingToUse))
			{
				return null;
			}
			bool flag = NumericEncoding.EncodingValueIsLittleEndian(this.numericEncodingToUse);
			int num;
			int num2;
			fixed (byte* ptr = &buffer[indexOfHeader])
			{
				int* ptr2 = (int*)ptr;
				num = *(ptr2++);
				num2 = ConversionHelpers.ExtractIntFromAddress(ref ptr2, flag);
			}
			Dictionary<int, string> dictionary;
			string text;
			if (Message.idsVersionsToFormat.TryGetValue(num, out dictionary) && dictionary.TryGetValue(num2, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x0400470C RID: 18188
		public const int UnlimitedLifetime = -1;

		// Token: 0x0400470D RID: 18189
		private static Dictionary<string, ConstructorInfo> formatToConstructorInfo = new Dictionary<string, ConstructorInfo>();

		// Token: 0x0400470E RID: 18190
		private static Dictionary<int, Dictionary<int, string>> idsVersionsToFormat = new Dictionary<int, Dictionary<int, string>>();

		// Token: 0x04004714 RID: 18196
		private int expirationInterval = -1;

		// Token: 0x04004717 RID: 18199
		private byte[] messageId;

		// Token: 0x04004718 RID: 18200
		private byte[] correlator;

		// Token: 0x04004719 RID: 18201
		private string replyToQueue;

		// Token: 0x0400471A RID: 18202
		private string replyToQueueManager;

		// Token: 0x0400471B RID: 18203
		private byte[] groupId;

		// Token: 0x0400471C RID: 18204
		private int sequenceNumber = 1;

		// Token: 0x0400471D RID: 18205
		private int offset;

		// Token: 0x04004722 RID: 18210
		protected int numericEncodingToUse;

		// Token: 0x04004723 RID: 18211
		protected int ccsidToUse;

		// Token: 0x04004724 RID: 18212
		private List<MqHeader> headers = new List<MqHeader>();

		// Token: 0x04004727 RID: 18215
		protected IdentityContext identityContext;

		// Token: 0x04004728 RID: 18216
		protected OriginContext originContext;
	}
}
