using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B29 RID: 2857
	public class SendMessage : Message
	{
		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x060059D4 RID: 22996 RVA: 0x001723CE File Offset: 0x001705CE
		// (set) Token: 0x060059D5 RID: 22997 RVA: 0x001723D6 File Offset: 0x001705D6
		public SendOptions Options { get; set; }

		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x060059D6 RID: 22998 RVA: 0x001723DF File Offset: 0x001705DF
		// (set) Token: 0x060059D7 RID: 22999 RVA: 0x001723E7 File Offset: 0x001705E7
		public new string Format
		{
			get
			{
				return base.Format;
			}
			set
			{
				base.Format = value;
			}
		}

		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x060059D8 RID: 23000 RVA: 0x001723F0 File Offset: 0x001705F0
		// (set) Token: 0x060059D9 RID: 23001 RVA: 0x001723F8 File Offset: 0x001705F8
		internal ReceiveMessage ReceivedMessage { get; private set; }

		// Token: 0x060059DA RID: 23002 RVA: 0x00172401 File Offset: 0x00170601
		public SendMessage()
			: this(MessageType.Datagram, null, new SendOptions())
		{
		}

		// Token: 0x060059DB RID: 23003 RVA: 0x00172410 File Offset: 0x00170610
		public SendMessage(MessageType messageType)
			: this(messageType, null, new SendOptions())
		{
		}

		// Token: 0x060059DC RID: 23004 RVA: 0x0017241F File Offset: 0x0017061F
		public SendMessage(SendOptions options)
			: this(MessageType.Datagram, null, options)
		{
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x0017242A File Offset: 0x0017062A
		public SendMessage(MessageType messageType, SendOptions options)
			: this(messageType, null, options)
		{
		}

		// Token: 0x060059DE RID: 23006 RVA: 0x00172435 File Offset: 0x00170635
		public SendMessage(List<MqHeader> headers)
			: this(MessageType.Datagram, headers, new SendOptions())
		{
		}

		// Token: 0x060059DF RID: 23007 RVA: 0x00172444 File Offset: 0x00170644
		public SendMessage(MessageType messageType, List<MqHeader> headers)
			: this(messageType, headers, new SendOptions())
		{
		}

		// Token: 0x060059E0 RID: 23008 RVA: 0x00172453 File Offset: 0x00170653
		public SendMessage(List<MqHeader> headers, SendOptions options)
			: this(MessageType.Datagram, headers, options)
		{
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x0017245E File Offset: 0x0017065E
		public SendMessage(MessageType messageType, List<MqHeader> headers, SendOptions options)
			: base(messageType, headers)
		{
			this.Options = options;
			base.ContextOption = ContextOption.Default;
			base.Priority = Priority.AsQueueDefined;
			base.Persistence = Persistence.AsQueueDefinition;
		}

		// Token: 0x060059E2 RID: 23010 RVA: 0x00172485 File Offset: 0x00170685
		public void SetContext(ContextOption option)
		{
			if (option == ContextOption.PassAll || option == ContextOption.PassIdentity)
			{
				throw new ArgumentOutOfRangeException("option");
			}
			base.ContextOption = option;
		}

		// Token: 0x060059E3 RID: 23011 RVA: 0x001724AC File Offset: 0x001706AC
		public void FillFromMessage(ReceiveMessage message, ContextOption contextOption, ActionOption action, FillOption fillOption)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (this.alreadyFilled)
			{
				throw new InvalidOperationException();
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (contextOption <= ContextOption.PassAll)
			{
				if (contextOption == ContextOption.Default)
				{
					goto IL_0072;
				}
				if (contextOption == ContextOption.PassIdentity || contextOption == ContextOption.PassAll)
				{
					flag3 = true;
					goto IL_0072;
				}
			}
			else
			{
				if (contextOption == ContextOption.SetIdentity)
				{
					flag2 = true;
					goto IL_0072;
				}
				if (contextOption == ContextOption.SetAll)
				{
					flag = true;
					goto IL_0072;
				}
				if (contextOption == ContextOption.None)
				{
					goto IL_0072;
				}
			}
			throw new ArgumentOutOfRangeException("contextOption");
			IL_0072:
			if (action > ActionOption.Reply)
			{
				throw new ArgumentOutOfRangeException("action");
			}
			if ((fillOption & FillOption.AllContext) == FillOption.AllContext && (fillOption & FillOption.IdentityContext) == FillOption.IdentityContext)
			{
				throw new ArgumentOutOfRangeException("fillOption");
			}
			if (!flag && (fillOption & FillOption.AllContext) == FillOption.AllContext)
			{
				throw new ArgumentOutOfRangeException("fillOption");
			}
			if (!flag && !flag2 && (fillOption & FillOption.IdentityContext) == FillOption.IdentityContext)
			{
				throw new ArgumentOutOfRangeException("fillOption");
			}
			bool flag4 = (fillOption & FillOption.DeepCopy) == FillOption.DeepCopy;
			if (flag3)
			{
				if (!message.ReceiveQueue.SavesContext)
				{
					throw new ArgumentException("message");
				}
				this.ReceivedMessage = message;
			}
			base.ContextOption = contextOption;
			if (action == ActionOption.New)
			{
				this.CopyMqmdValues(message, fillOption);
				this.alreadyFilled = true;
				return;
			}
			if (action == ActionOption.Forward)
			{
				this.CopyMqmdValues(message, fillOption);
				this.CopyHeadersAndData(message, fillOption);
				this.alreadyFilled = true;
				return;
			}
			if (action != ActionOption.Reply)
			{
				this.alreadyFilled = false;
				throw new ArgumentOutOfRangeException("action");
			}
			if (base.MessageType != MessageType.Reply)
			{
				throw new ArgumentOutOfRangeException("action");
			}
			if (message.MessageType != MessageType.Request)
			{
				throw new ArgumentOutOfRangeException("message");
			}
			this.alreadyFilled = true;
			if ((message.Report & Report.PassDiscardAndExpiry) == Report.PassDiscardAndExpiry && (message.Report & Report.DiscardMessage) == Report.DiscardMessage)
			{
				base.Report = Report.DiscardMessage;
			}
			else
			{
				base.Report = Report.None;
			}
			if ((message.Report & Report.PassDiscardAndExpiry) == Report.PassDiscardAndExpiry)
			{
				base.ExpirationInterval = message.ExpirationInterval;
			}
			else
			{
				base.ExpirationInterval = -1;
			}
			if ((message.Report & Report.PassMessageId) == Report.PassMessageId)
			{
				base.MessageId = ConversionHelpers.ByteArrayNullOrCopy(message.MessageId, flag4);
			}
			else
			{
				base.MessageId = null;
			}
			if ((message.Report & Report.PassCorrelator) == Report.PassCorrelator)
			{
				base.Correlator = ConversionHelpers.ByteArrayNullOrCopy(message.Correlator, flag4);
			}
			else
			{
				base.Correlator = ConversionHelpers.ByteArrayNullOrCopy(message.MessageId, flag4);
			}
			base.ReplyToQueue = null;
			base.ReplyToQueueManager = null;
			base.GroupId = null;
			base.SequenceNumber = 1;
			base.Offset = 0;
			base.MessageFlags = MessageFlags.None;
			this.CopyHeadersAndData(message, fillOption);
		}

		// Token: 0x060059E4 RID: 23012 RVA: 0x00172734 File Offset: 0x00170934
		private void CopyHeadersAndData(ReceiveMessage message, FillOption fillOption)
		{
			bool flag = (fillOption & FillOption.DeepCopy) == FillOption.DeepCopy;
			if ((fillOption & FillOption.Data) == FillOption.Data)
			{
				base.Data = ConversionHelpers.ByteArrayNullOrCopy(message.Data, flag);
			}
			if ((fillOption & FillOption.Headers) == FillOption.Headers)
			{
				base.Headers = this.GenerateCopyHeaders(message.Headers, flag);
			}
		}

		// Token: 0x060059E5 RID: 23013 RVA: 0x00172784 File Offset: 0x00170984
		private void CopyMqmdValues(ReceiveMessage message, FillOption fillOption)
		{
			bool flag = (fillOption & FillOption.DeepCopy) == FillOption.DeepCopy;
			base.Ccsid = message.Ccsid;
			base.Correlator = ConversionHelpers.ByteArrayNullOrCopy(message.Correlator, flag);
			base.ExpirationInterval = message.ExpirationInterval;
			base.GroupId = ConversionHelpers.ByteArrayNullOrCopy(message.GroupId, flag);
			if ((fillOption & FillOption.IdentityContext) == FillOption.IdentityContext || (fillOption & FillOption.AllContext) == FillOption.AllContext)
			{
				this.identityContext = IdentityContext.GenerateCopy(message.IdentityContext, flag);
			}
			base.MessageFlags = message.MessageFlags;
			base.MessageId = ConversionHelpers.ByteArrayNullOrCopy(message.MessageId, flag);
			base.Offset = message.Offset;
			if ((fillOption & FillOption.AllContext) == FillOption.AllContext)
			{
				this.originContext = OriginContext.GenerateCopy(message.OriginContext, flag);
			}
			base.Persistence = message.Persistence;
			base.Priority = message.Priority;
			base.ReplyToQueue = message.ReplyToQueue;
			base.ReplyToQueueManager = message.ReplyToQueueManager;
			base.Report = message.Report;
			base.SequenceNumber = message.SequenceNumber;
		}

		// Token: 0x060059E6 RID: 23014 RVA: 0x0017288C File Offset: 0x00170A8C
		private List<MqHeader> GenerateCopyHeaders(List<MqHeader> headers, bool deepCopy)
		{
			if (!deepCopy)
			{
				return headers;
			}
			List<MqHeader> list = new List<MqHeader>();
			for (int i = 0; i < headers.Count; i++)
			{
				MqHeader mqHeader = base.Headers[i];
				if (mqHeader != null)
				{
					MqHeader mqHeader2 = mqHeader.GenerateCopy(deepCopy);
					list.Add(mqHeader2);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return list;
		}

		// Token: 0x060059E7 RID: 23015 RVA: 0x001728E0 File Offset: 0x00170AE0
		public unsafe void GenerateMqmd(byte[] buffer, int offset)
		{
			MqHeader mqHeader = null;
			if (base.SortedHeaders != null)
			{
				mqHeader = base.SortedHeaders[0];
			}
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = 538985549;
				*(ptr3++) = 2;
				*(ptr3++) = (int)base.Report;
				*(ptr3++) = (int)base.MessageType;
				*(ptr3++) = base.ExpirationInterval;
				*(ptr3++) = 0;
				int num = 546;
				int num2 = 1252;
				if (mqHeader == null || (base.SortedHeaders.Length == 1 && base.LastHeaderIsCicsOrIms))
				{
					num = base.NumericEncoding.NumericValue;
					num2 = base.Ccsid;
				}
				*(ptr3++) = num;
				*(ptr3++) = num2;
				byte* ptr4 = (byte*)ptr3;
				if (mqHeader == null)
				{
					if (string.IsNullOrWhiteSpace(this.Format))
					{
						for (int i = 0; i < 8; i++)
						{
							*(ptr4++) = 32;
						}
					}
					else
					{
						ConversionHelpers.MoveStringToBufferAscii(buffer, (int)((long)(ptr4 - ptr2) + (long)offset), this.Format, 8, true);
						ptr4 += 8;
					}
				}
				else
				{
					ConversionHelpers.MoveStringToBufferAscii(buffer, (int)((long)(ptr4 - ptr2) + (long)offset), mqHeader.FormatString, 8, true);
					ptr4 += 8;
				}
				ptr3 = (int*)ptr4;
				*(ptr3++) = (int)base.Priority;
				*(ptr3++) = (int)base.Persistence;
				ptr4 = (byte*)ptr3;
				if (base.MessageId == null)
				{
					for (int j = 0; j < 24; j++)
					{
						*(ptr4++) = 0;
					}
				}
				else
				{
					for (int k = 0; k < 24; k++)
					{
						*(ptr4++) = base.MessageId[k];
					}
				}
				if (base.Correlator == null)
				{
					for (int l = 0; l < 24; l++)
					{
						*(ptr4++) = 0;
					}
				}
				else
				{
					for (int m = 0; m < 24; m++)
					{
						*(ptr4++) = base.Correlator[m];
					}
				}
				ptr3 = (int*)ptr4;
				*(ptr3++) = 0;
				ptr4 = (byte*)ptr3;
				if (base.ReplyToQueue == null)
				{
					for (int n = 0; n < 48; n++)
					{
						*(ptr4++) = 0;
					}
				}
				else
				{
					ConversionHelpers.MoveStringToBufferAscii(buffer, (int)((long)(ptr4 - ptr2) + (long)offset), base.ReplyToQueue, 48, false);
					ptr4 += 48;
				}
				if (base.ReplyToQueueManager == null)
				{
					for (int num3 = 0; num3 < 48; num3++)
					{
						*(ptr4++) = 0;
					}
				}
				else
				{
					ConversionHelpers.MoveStringToBufferAscii(buffer, (int)((long)(ptr4 - ptr2) + (long)offset), base.ReplyToQueueManager, 48, false);
					ptr4 += 48;
				}
				ptr4 += IdentityContext.GenerateMqmdBytes(base.IdentityContext, buffer, (int)((long)(ptr4 - ptr2) + (long)offset));
				ptr4 += OriginContext.GenerateMqmdBytes(base.OriginContext, buffer, (int)((long)(ptr4 - ptr2) + (long)offset));
				if (base.GroupId == null)
				{
					for (int num4 = 0; num4 < 24; num4++)
					{
						*(ptr4++) = 0;
					}
				}
				else
				{
					for (int num5 = 0; num5 < 24; num5++)
					{
						*(ptr4++) = base.GroupId[num5];
					}
				}
				ptr3 = (int*)ptr4;
				*(ptr3++) = base.SequenceNumber;
				*(ptr3++) = base.Offset;
				*(ptr3++) = (int)base.MessageFlags;
				*(ptr3++) = -1;
			}
		}

		// Token: 0x060059E8 RID: 23016 RVA: 0x00172BF8 File Offset: 0x00170DF8
		public unsafe void ExtractMqmd(byte[] buffer, int offset, bool littleEndian, int ccsid)
		{
			HisEncoding encoding = HisEncoding.GetEncoding(ccsid);
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				ptr3++;
				int num = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				ptr3 += 2;
				base.ExpirationInterval = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				ptr3 += 7;
				byte* ptr4 = (byte*)ptr3;
				byte[] array = new byte[24];
				bool flag = false;
				for (int i = 0; i < 24; i++)
				{
					array[i] = *(ptr4++);
					if (array[i] != 0)
					{
						flag = true;
					}
				}
				base.MessageId = (flag ? array : null);
				byte[] array2 = (flag ? new byte[24] : array);
				flag = false;
				for (int j = 0; j < 24; j++)
				{
					array2[j] = *(ptr4++);
					if (array2[j] != 0)
					{
						flag = true;
					}
				}
				base.Correlator = (flag ? array2 : null);
				ptr3 = (int*)ptr4;
				ptr3++;
				ptr4 = (byte*)ptr3;
				ptr4 += 48;
				ptr4 += 48;
				int num2;
				this.identityContext = IdentityContext.ExtractMqmd(buffer, (int)((long)(ptr4 - ptr2) + (long)offset), littleEndian, encoding, out num2);
				ptr4 += num2;
				this.originContext = OriginContext.ExtractMqmd(buffer, (int)((long)(ptr4 - ptr2) + (long)offset), littleEndian, encoding, out num2);
				ptr4 += num2;
				if (num > 1)
				{
					byte[] array3 = (flag ? new byte[24] : array2);
					flag = false;
					for (int k = 0; k < 24; k++)
					{
						array3[k] = *(ptr4++);
						if (array3[k] != 0)
						{
							flag = true;
						}
					}
					base.GroupId = (flag ? array3 : null);
					ptr3 = (int*)ptr4;
					base.SequenceNumber = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
					base.Offset = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
					base.MessageFlags = (MessageFlags)ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				}
				else
				{
					base.GroupId = null;
					base.SequenceNumber = 0;
					base.Offset = 0;
					base.MessageFlags = MessageFlags.None;
				}
			}
		}

		// Token: 0x060059E9 RID: 23017 RVA: 0x00172DDC File Offset: 0x00170FDC
		internal bool HeadersContainPcf()
		{
			using (List<MqHeader>.Enumerator enumerator = base.Headers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HeaderType == MqHeaderType.ProgrammableCommandFormat)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400472B RID: 18219
		private bool alreadyFilled;
	}
}
