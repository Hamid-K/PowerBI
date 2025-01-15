using System;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B33 RID: 2867
	public class ImsBridgeHeader : MqHeader
	{
		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x06005A83 RID: 23171 RVA: 0x00174D15 File Offset: 0x00172F15
		// (set) Token: 0x06005A84 RID: 23172 RVA: 0x00174D1D File Offset: 0x00172F1D
		public ImsBridgeFlag Flags { get; set; }

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x06005A85 RID: 23173 RVA: 0x00174D26 File Offset: 0x00172F26
		// (set) Token: 0x06005A86 RID: 23174 RVA: 0x00174D2E File Offset: 0x00172F2E
		public string LTermOverride
		{
			get
			{
				return this.lTermOverride;
			}
			set
			{
				this.lTermOverride = Globals.CheckMaximumLengthTrimmed(value, "LTermOverride", 8);
			}
		}

		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x06005A87 RID: 23175 RVA: 0x00174D42 File Offset: 0x00172F42
		// (set) Token: 0x06005A88 RID: 23176 RVA: 0x00174D4A File Offset: 0x00172F4A
		public string MfsMapName
		{
			get
			{
				return this.mfsMapName;
			}
			set
			{
				this.mfsMapName = Globals.CheckMaximumLengthTrimmed(value, "MfsMapName", 8);
			}
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x06005A89 RID: 23177 RVA: 0x00174D5E File Offset: 0x00172F5E
		// (set) Token: 0x06005A8A RID: 23178 RVA: 0x00174D66 File Offset: 0x00172F66
		public string ReplyToFormat
		{
			get
			{
				return this.replyToFormat;
			}
			set
			{
				this.replyToFormat = Globals.CheckMaximumLengthTrimmed(value, "ReplyToFormat", 8);
			}
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x06005A8B RID: 23179 RVA: 0x00174D7A File Offset: 0x00172F7A
		// (set) Token: 0x06005A8C RID: 23180 RVA: 0x00174D82 File Offset: 0x00172F82
		public string Authenticator
		{
			get
			{
				return this.authenticator;
			}
			set
			{
				this.authenticator = Globals.CheckMaximumLengthTrimmed(value, "Authenticator", 8);
			}
		}

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x06005A8D RID: 23181 RVA: 0x00174D96 File Offset: 0x00172F96
		// (set) Token: 0x06005A8E RID: 23182 RVA: 0x00174D9E File Offset: 0x00172F9E
		public byte[] TransactionInstanceIdentifier
		{
			get
			{
				return this.transactionInstanceIdentifier;
			}
			set
			{
				this.transactionInstanceIdentifier = Globals.CheckExactLength(value, "TransactionInstanceIdentifier", 16);
			}
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x06005A8F RID: 23183 RVA: 0x00174DB3 File Offset: 0x00172FB3
		// (set) Token: 0x06005A90 RID: 23184 RVA: 0x00174DBB File Offset: 0x00172FBB
		public ImsBridgeTransactionState TransactionState { get; set; }

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x06005A91 RID: 23185 RVA: 0x00174DC4 File Offset: 0x00172FC4
		// (set) Token: 0x06005A92 RID: 23186 RVA: 0x00174DCC File Offset: 0x00172FCC
		public ImsBridgeCommitMode CommitMode { get; set; }

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x06005A93 RID: 23187 RVA: 0x00174DD5 File Offset: 0x00172FD5
		// (set) Token: 0x06005A94 RID: 23188 RVA: 0x00174DDD File Offset: 0x00172FDD
		public ImsBridgeSecurityScope SecurityScope { get; set; }

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x06005A95 RID: 23189 RVA: 0x00174DE6 File Offset: 0x00172FE6
		public override int AsciiStructId
		{
			get
			{
				return 541608265;
			}
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x06005A96 RID: 23190 RVA: 0x00174DED File Offset: 0x00172FED
		public override int EbcdicStructId
		{
			get
			{
				return 1086900681;
			}
		}

		// Token: 0x06005A97 RID: 23191 RVA: 0x00174DF4 File Offset: 0x00172FF4
		public ImsBridgeHeader()
			: base(MqHeaderType.ImsBridge, OrderedMqHeaderType.ImsBridge, "IMS Bridge Header", "MQIMS", 84)
		{
			this.Flags = ImsBridgeFlag.None;
			this.CommitMode = ImsBridgeCommitMode.CommitThenSend;
			this.SecurityScope = ImsBridgeSecurityScope.Check;
			this.TransactionState = ImsBridgeTransactionState.None;
		}

		// Token: 0x06005A98 RID: 23192 RVA: 0x00174E28 File Offset: 0x00173028
		internal unsafe override void GenerateBytes(byte[] buffer, int offset, string format, bool littleEndian, int ccsid, int numericEncodingValue, int ccsidValue)
		{
			HisEncoding encoding = HisEncoding.GetEncoding(ccsid);
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = 541608265;
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, 1);
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, this.SendLength);
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				byte* ptr4 = (byte*)ptr3;
				int num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, format, 8, true, encoding);
				ptr3 += 2;
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, (int)this.Flags);
				ptr4 = (byte*)ptr3;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.LTermOverride, 8, true, encoding);
				ptr4 += 8;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.MfsMapName, 8, true, encoding);
				ptr4 += 8;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.ReplyToFormat, 8, true, encoding);
				ptr4 += 8;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.Authenticator, 8, true, encoding);
				ptr4 += 8;
				if (this.TransactionInstanceIdentifier == null)
				{
					ptr3 = (int*)ptr4;
					*(ptr3++) = 0;
					*(ptr3++) = 0;
					*(ptr3++) = 0;
					*(ptr3++) = 0;
					ptr4 = (byte*)ptr3;
				}
				else
				{
					for (int i = 0; i < 16; i++)
					{
						*(ptr4++) = this.TransactionInstanceIdentifier[i];
					}
				}
				num = offset + (int)((long)(ptr4 - ptr2));
				switch (this.TransactionState)
				{
				case ImsBridgeTransactionState.None:
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, null, 1, true, encoding);
					break;
				case ImsBridgeTransactionState.InConversation:
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, "C", 1, true, encoding);
					break;
				case ImsBridgeTransactionState.Architected:
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, "A", 1, true, encoding);
					break;
				default:
					throw new ArgumentOutOfRangeException("ImsBridgeHeader, TransactionState");
				}
				ptr4++;
				num = offset + (int)((long)(ptr4 - ptr2));
				ImsBridgeCommitMode commitMode = this.CommitMode;
				if (commitMode != ImsBridgeCommitMode.CommitThenSend)
				{
					if (commitMode != ImsBridgeCommitMode.SendThenCommit)
					{
						throw new ArgumentOutOfRangeException("ImsBridgeHeader, CommitMode");
					}
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, "1", 1, true, encoding);
				}
				else
				{
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, "0", 1, true, encoding);
				}
				ptr4++;
				num = offset + (int)((long)(ptr4 - ptr2));
				ImsBridgeSecurityScope securityScope = this.SecurityScope;
				if (securityScope != ImsBridgeSecurityScope.Check)
				{
					if (securityScope != ImsBridgeSecurityScope.Full)
					{
						throw new ArgumentOutOfRangeException("ImsBridgeHeader, SecurityScope");
					}
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, "F", 1, true, encoding);
				}
				else
				{
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, "C", 1, true, encoding);
				}
				ptr4++;
				*(ptr4++) = 0;
			}
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x001750B8 File Offset: 0x001732B8
		internal unsafe override bool TryExtract(byte[] buffer, int numberOfBytesAvailable, int offset, bool truncationInEffect, ref int ccsidToUse, ref int numericEncodingToUse, out string nextFormat)
		{
			nextFormat = null;
			bool flag = NumericEncoding.EncodingValueIsLittleEndian(numericEncodingToUse);
			HisEncoding encoding = HisEncoding.GetEncoding(ccsidToUse);
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				ptr3 += 5;
				byte* ptr4 = (byte*)ptr3;
				int num = offset + (int)((long)(ptr4 - ptr2));
				nextFormat = ConversionHelpers.GetStringOrNull(buffer, num, 8, encoding);
				ptr4 += 8;
				ptr3 = (int*)ptr4;
				this.Flags = (ImsBridgeFlag)ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				ptr4 = (byte*)ptr3;
				num = offset + (int)((long)(ptr4 - ptr2));
				this.LTermOverride = ConversionHelpers.GetStringOrNull(buffer, num, 8, encoding);
				ptr4 += 8;
				num = offset + (int)((long)(ptr4 - ptr2));
				this.MfsMapName = ConversionHelpers.GetStringOrNull(buffer, num, 8, encoding);
				ptr4 += 8;
				num = offset + (int)((long)(ptr4 - ptr2));
				this.ReplyToFormat = ConversionHelpers.GetStringOrNull(buffer, num, 8, encoding);
				ptr4 += 8;
				num = offset + (int)((long)(ptr4 - ptr2));
				this.Authenticator = ConversionHelpers.GetStringOrNull(buffer, num, 8, encoding);
				ptr4 += 8;
				byte[] array = new byte[16];
				bool flag2 = false;
				for (int i = 0; i < 16; i++)
				{
					array[i] = *(ptr4++);
					if (array[i] != 0)
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					this.TransactionInstanceIdentifier = array;
				}
				num = offset + (int)((long)(ptr4 - ptr2));
				string text = ConversionHelpers.GetStringOrNull(buffer, num, 1, encoding);
				if (text == null)
				{
					text = string.Empty;
				}
				if (text != null)
				{
					if (text == null || text.Length != 0)
					{
						if (!(text == "C"))
						{
							if (!(text == "A"))
							{
								goto IL_019B;
							}
							this.TransactionState = ImsBridgeTransactionState.Architected;
						}
						else
						{
							this.TransactionState = ImsBridgeTransactionState.InConversation;
						}
					}
					else
					{
						this.TransactionState = ImsBridgeTransactionState.None;
					}
					ptr4++;
					num = offset + (int)((long)(ptr4 - ptr2));
					string text2 = ConversionHelpers.GetStringOrNull(buffer, num, 1, encoding);
					if (text2 == null)
					{
						text2 = string.Empty;
					}
					if (text2 != null)
					{
						if (!(text2 == "0"))
						{
							if (!(text2 == "1"))
							{
								goto IL_0203;
							}
							this.CommitMode = ImsBridgeCommitMode.SendThenCommit;
						}
						else
						{
							this.CommitMode = ImsBridgeCommitMode.CommitThenSend;
						}
						ptr4++;
						num = offset + (int)((long)(ptr4 - ptr2));
						string text3 = ConversionHelpers.GetStringOrNull(buffer, num, 1, encoding);
						if (text3 == null)
						{
							text3 = string.Empty;
						}
						if (text3 != null)
						{
							if (!(text3 == "C"))
							{
								if (!(text3 == "F"))
								{
									goto IL_026B;
								}
								this.SecurityScope = ImsBridgeSecurityScope.Full;
							}
							else
							{
								this.SecurityScope = ImsBridgeSecurityScope.Check;
							}
							ptr4++;
							ptr4++;
							ptr = null;
							return true;
						}
						IL_026B:
						throw new InvalidProgramException("Unknown SecurityScope Received");
					}
					IL_0203:
					throw new InvalidProgramException("Unknown CommitMode Received");
				}
				IL_019B:
				throw new InvalidProgramException("Unknown TransactionState Received");
			}
		}

		// Token: 0x06005A9A RID: 23194 RVA: 0x0017534C File Offset: 0x0017354C
		internal override MqHeader GenerateCopy(bool deepCopy)
		{
			if (!deepCopy)
			{
				return this;
			}
			return new ImsBridgeHeader
			{
				Authenticator = this.Authenticator,
				CommitMode = this.CommitMode,
				Flags = this.Flags,
				LTermOverride = this.LTermOverride,
				MfsMapName = this.MfsMapName,
				ReplyToFormat = this.ReplyToFormat,
				SecurityScope = this.SecurityScope,
				TransactionInstanceIdentifier = ConversionHelpers.ByteArrayNullOrCopy(this.TransactionInstanceIdentifier, deepCopy),
				TransactionState = this.TransactionState
			};
		}

		// Token: 0x0400476B RID: 18283
		private string lTermOverride;

		// Token: 0x0400476C RID: 18284
		private string mfsMapName;

		// Token: 0x0400476D RID: 18285
		private string replyToFormat;

		// Token: 0x0400476E RID: 18286
		private string authenticator;

		// Token: 0x0400476F RID: 18287
		private byte[] transactionInstanceIdentifier;
	}
}
