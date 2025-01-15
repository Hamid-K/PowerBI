using System;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B32 RID: 2866
	public class CicsBridgeHeader : MqHeader
	{
		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x06005A42 RID: 23106 RVA: 0x00173FC7 File Offset: 0x001721C7
		// (set) Token: 0x06005A43 RID: 23107 RVA: 0x00173FCF File Offset: 0x001721CF
		public CicsBridgeFlag Flags { get; set; }

		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x06005A44 RID: 23108 RVA: 0x00173FD8 File Offset: 0x001721D8
		// (set) Token: 0x06005A45 RID: 23109 RVA: 0x00173FE0 File Offset: 0x001721E0
		public CicsBridgeReturnCode ReturnCode { get; private set; }

		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x06005A46 RID: 23110 RVA: 0x00173FE9 File Offset: 0x001721E9
		// (set) Token: 0x06005A47 RID: 23111 RVA: 0x00173FF1 File Offset: 0x001721F1
		public int CompletionCode { get; private set; }

		// Token: 0x170015B2 RID: 5554
		// (get) Token: 0x06005A48 RID: 23112 RVA: 0x00173FFA File Offset: 0x001721FA
		// (set) Token: 0x06005A49 RID: 23113 RVA: 0x00174002 File Offset: 0x00172202
		public int ReasonCode { get; private set; }

		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x06005A4A RID: 23114 RVA: 0x0017400B File Offset: 0x0017220B
		// (set) Token: 0x06005A4B RID: 23115 RVA: 0x00174013 File Offset: 0x00172213
		public CicsBridgeUowControl UowControl { get; set; }

		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x06005A4C RID: 23116 RVA: 0x0017401C File Offset: 0x0017221C
		// (set) Token: 0x06005A4D RID: 23117 RVA: 0x00174024 File Offset: 0x00172224
		public int GetWaitInterval { get; set; }

		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x06005A4E RID: 23118 RVA: 0x0017402D File Offset: 0x0017222D
		// (set) Token: 0x06005A4F RID: 23119 RVA: 0x00174035 File Offset: 0x00172235
		public CicsBridgeLinkType LinkType { get; set; }

		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x06005A50 RID: 23120 RVA: 0x0017403E File Offset: 0x0017223E
		// (set) Token: 0x06005A51 RID: 23121 RVA: 0x00174046 File Offset: 0x00172246
		public int OutputDataLength { get; set; }

		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x06005A52 RID: 23122 RVA: 0x0017404F File Offset: 0x0017224F
		// (set) Token: 0x06005A53 RID: 23123 RVA: 0x00174057 File Offset: 0x00172257
		public int FacilityKeepTime { get; set; }

		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x06005A54 RID: 23124 RVA: 0x00174060 File Offset: 0x00172260
		// (set) Token: 0x06005A55 RID: 23125 RVA: 0x00174068 File Offset: 0x00172268
		public CicsBridgeAdsDescriptor AdsDescriptors { get; set; }

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x06005A56 RID: 23126 RVA: 0x00174071 File Offset: 0x00172271
		// (set) Token: 0x06005A57 RID: 23127 RVA: 0x00174079 File Offset: 0x00172279
		public bool IsConversationalTask { get; set; }

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x06005A58 RID: 23128 RVA: 0x00174082 File Offset: 0x00172282
		// (set) Token: 0x06005A59 RID: 23129 RVA: 0x0017408A File Offset: 0x0017228A
		public CicsBridgeTaskEndStatus TaskEndStatus { get; private set; }

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x06005A5A RID: 23130 RVA: 0x00174093 File Offset: 0x00172293
		// (set) Token: 0x06005A5B RID: 23131 RVA: 0x0017409B File Offset: 0x0017229B
		public byte[] Token
		{
			get
			{
				return this.token;
			}
			set
			{
				this.token = Globals.CheckExactLength(value, "Token", 8);
			}
		}

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x06005A5C RID: 23132 RVA: 0x001740AF File Offset: 0x001722AF
		// (set) Token: 0x06005A5D RID: 23133 RVA: 0x001740B7 File Offset: 0x001722B7
		public string Function { get; private set; }

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x06005A5E RID: 23134 RVA: 0x001740C0 File Offset: 0x001722C0
		// (set) Token: 0x06005A5F RID: 23135 RVA: 0x001740C8 File Offset: 0x001722C8
		public string AbendCode { get; private set; }

		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x06005A60 RID: 23136 RVA: 0x001740D1 File Offset: 0x001722D1
		// (set) Token: 0x06005A61 RID: 23137 RVA: 0x001740D9 File Offset: 0x001722D9
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

		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x06005A62 RID: 23138 RVA: 0x001740ED File Offset: 0x001722ED
		// (set) Token: 0x06005A63 RID: 23139 RVA: 0x001740F5 File Offset: 0x001722F5
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

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x06005A64 RID: 23140 RVA: 0x00174109 File Offset: 0x00172309
		// (set) Token: 0x06005A65 RID: 23141 RVA: 0x00174111 File Offset: 0x00172311
		public string RemoteSysId
		{
			get
			{
				return this.remoteSysId;
			}
			set
			{
				this.remoteSysId = Globals.CheckMaximumLengthTrimmed(value, "RemoteSysId", 4);
			}
		}

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x06005A66 RID: 23142 RVA: 0x00174125 File Offset: 0x00172325
		// (set) Token: 0x06005A67 RID: 23143 RVA: 0x0017412D File Offset: 0x0017232D
		public string RemoteTransactionId
		{
			get
			{
				return this.remoteTransId;
			}
			set
			{
				this.remoteTransId = Globals.CheckMaximumLengthTrimmed(value, "RemoteTransactionId", 4);
			}
		}

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x06005A68 RID: 23144 RVA: 0x00174141 File Offset: 0x00172341
		// (set) Token: 0x06005A69 RID: 23145 RVA: 0x00174149 File Offset: 0x00172349
		public string TransactionId
		{
			get
			{
				return this.transactionId;
			}
			set
			{
				this.transactionId = Globals.CheckMaximumLengthTrimmed(value, "TransactionId", 4);
			}
		}

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x06005A6A RID: 23146 RVA: 0x0017415D File Offset: 0x0017235D
		// (set) Token: 0x06005A6B RID: 23147 RVA: 0x00174165 File Offset: 0x00172365
		public string FacilityIsLike
		{
			get
			{
				return this.facilityIsLike;
			}
			set
			{
				this.facilityIsLike = Globals.CheckMaximumLengthTrimmed(value, "FacilityIsLike", 4);
			}
		}

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x06005A6C RID: 23148 RVA: 0x00174179 File Offset: 0x00172379
		// (set) Token: 0x06005A6D RID: 23149 RVA: 0x00174181 File Offset: 0x00172381
		public byte AttentionId { get; set; }

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x06005A6E RID: 23150 RVA: 0x0017418A File Offset: 0x0017238A
		// (set) Token: 0x06005A6F RID: 23151 RVA: 0x00174192 File Offset: 0x00172392
		public CicsBridgeStartCode StartCode { get; set; }

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x06005A70 RID: 23152 RVA: 0x0017419B File Offset: 0x0017239B
		// (set) Token: 0x06005A71 RID: 23153 RVA: 0x001741A3 File Offset: 0x001723A3
		public string CancelCode
		{
			get
			{
				return this.cancelCode;
			}
			set
			{
				this.cancelCode = Globals.CheckMaximumLengthTrimmed(value, "CancelCode", 4);
			}
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x06005A72 RID: 23154 RVA: 0x001741B7 File Offset: 0x001723B7
		// (set) Token: 0x06005A73 RID: 23155 RVA: 0x001741BF File Offset: 0x001723BF
		public string NextTransactionId { get; private set; }

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x06005A74 RID: 23156 RVA: 0x001741C8 File Offset: 0x001723C8
		// (set) Token: 0x06005A75 RID: 23157 RVA: 0x001741D0 File Offset: 0x001723D0
		public int CursorPosition { get; set; }

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x06005A76 RID: 23158 RVA: 0x001741D9 File Offset: 0x001723D9
		// (set) Token: 0x06005A77 RID: 23159 RVA: 0x001741E1 File Offset: 0x001723E1
		public int ErrorOffset { get; private set; }

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x06005A78 RID: 23160 RVA: 0x001741EA File Offset: 0x001723EA
		// (set) Token: 0x06005A79 RID: 23161 RVA: 0x001741F2 File Offset: 0x001723F2
		public string ProgramName
		{
			get
			{
				return this.programName;
			}
			set
			{
				this.programName = Globals.CheckMaximumLength(value, "ProgramName", 8);
			}
		}

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x06005A7A RID: 23162 RVA: 0x00174206 File Offset: 0x00172406
		internal override int SendLength
		{
			get
			{
				return base.SendLength + ((this.LinkType == CicsBridgeLinkType.Program) ? 8 : 0);
			}
		}

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x06005A7B RID: 23163 RVA: 0x0017421C File Offset: 0x0017241C
		internal override int BytesConsumed
		{
			get
			{
				return this.versionDependentBytesConsumed;
			}
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x06005A7C RID: 23164 RVA: 0x00174224 File Offset: 0x00172424
		public override int AsciiStructId
		{
			get
			{
				return 541608259;
			}
		}

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x06005A7D RID: 23165 RVA: 0x0017422B File Offset: 0x0017242B
		public override int EbcdicStructId
		{
			get
			{
				return 1086900675;
			}
		}

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x06005A7E RID: 23166 RVA: 0x0003A22E File Offset: 0x0003842E
		public override int MaximumVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06005A7F RID: 23167 RVA: 0x00174234 File Offset: 0x00172434
		public CicsBridgeHeader()
			: base(MqHeaderType.CicsBridge, OrderedMqHeaderType.CicsBridge, "CICS Bridge Header", "MQCICS", 180, 164)
		{
			this.Flags = CicsBridgeFlag.None;
			this.UowControl = CicsBridgeUowControl.Only;
			this.GetWaitInterval = -2;
			this.LinkType = CicsBridgeLinkType.Program;
			this.OutputDataLength = -1;
			this.AdsDescriptors = CicsBridgeAdsDescriptor.None;
			this.TaskEndStatus = CicsBridgeTaskEndStatus.NoSync;
			this.StartCode = CicsBridgeStartCode.None;
		}

		// Token: 0x06005A80 RID: 23168 RVA: 0x0017429C File Offset: 0x0017249C
		internal unsafe override void GenerateBytes(byte[] buffer, int offset, string format, bool littleEndian, int ccsid, int numericEncodingValue, int ccsidValue)
		{
			HisEncoding encoding = HisEncoding.GetEncoding(ccsid);
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = 541608259;
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, 2);
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, base.SendLength);
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				byte* ptr4 = (byte*)ptr3;
				int num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, format, 8, true, encoding);
				ptr3 += 2;
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, (int)this.Flags);
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, (int)this.UowControl);
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, this.GetWaitInterval);
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, (int)this.LinkType);
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, (this.LinkType == CicsBridgeLinkType.Program) ? this.OutputDataLength : (-1));
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, (this.LinkType != CicsBridgeLinkType.Program) ? this.FacilityKeepTime : 0);
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, (int)((this.LinkType != CicsBridgeLinkType.Program) ? this.AdsDescriptors : CicsBridgeAdsDescriptor.None));
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, this.IsConversationalTask ? 1 : 0);
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, 0);
				ptr4 = (byte*)ptr3;
				if (this.LinkType == CicsBridgeLinkType.Program || this.Token == null)
				{
					*(ptr3++) = 0;
					*(ptr3++) = 0;
					ptr4 = (byte*)ptr3;
				}
				else
				{
					for (int i = 0; i < 8; i++)
					{
						*(ptr4++) = this.Token[i];
					}
				}
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, null, 4, true, encoding);
				ptr4 += 4;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, null, 4, true, encoding);
				ptr4 += 4;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.Authenticator, 8, true, encoding);
				ptr4 += 8;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, null, 8, true, encoding);
				ptr4 += 8;
				num = offset + (int)((long)(ptr4 - ptr2));
				if (this.LinkType != CicsBridgeLinkType.Program)
				{
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, null, 8, true, encoding);
				}
				else
				{
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.ReplyToFormat, 8, true, encoding);
				}
				ptr4 += 8;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.RemoteSysId, 4, true, encoding);
				ptr4 += 4;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.RemoteTransactionId, 4, true, encoding);
				ptr4 += 4;
				num = offset + (int)((long)(ptr4 - ptr2));
				if (string.IsNullOrWhiteSpace(this.TransactionId))
				{
					if (this.LinkType != CicsBridgeLinkType.Program)
					{
						throw new ArgumentOutOfRangeException("CicsBridgeHeader, TransactionId");
					}
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, null, 4, true, encoding);
				}
				else
				{
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.TransactionId, 4, true, encoding);
				}
				ptr4 += 4;
				num = offset + (int)((long)(ptr4 - ptr2));
				if (this.LinkType == CicsBridgeLinkType.Program)
				{
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, null, 4, true, encoding);
				}
				else
				{
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.FacilityIsLike, 4, true, encoding);
				}
				ptr4 += 4;
				num = offset + (int)((long)(ptr4 - ptr2));
				if (this.LinkType == CicsBridgeLinkType.Program)
				{
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, null, 4, true, encoding);
				}
				else
				{
					*ptr4 = this.AttentionId;
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num + 1, null, 3, true, encoding);
				}
				ptr4 += 4;
				num = offset + (int)((long)(ptr4 - ptr2));
				if (this.LinkType == CicsBridgeLinkType.Program)
				{
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, null, 4, true, encoding);
				}
				else
				{
					switch (this.StartCode)
					{
					case CicsBridgeStartCode.None:
						ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, null, 4, true, encoding);
						break;
					case CicsBridgeStartCode.Start:
						ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, "S", 4, true, encoding);
						break;
					case CicsBridgeStartCode.StartData:
						ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, "SD", 4, true, encoding);
						break;
					case CicsBridgeStartCode.TerminalInput:
						ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, "TD", 4, true, encoding);
						break;
					default:
						throw new ArgumentOutOfRangeException("CicsBridgeHeader, StartCode");
					}
				}
				ptr4 += 4;
				num = offset + (int)((long)(ptr4 - ptr2));
				if (this.LinkType == CicsBridgeLinkType.Program)
				{
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, null, 4, true, encoding);
				}
				else
				{
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.CancelCode, 4, true, encoding);
				}
				ptr4 += 4;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, null, 4, true, encoding);
				ptr4 += 4;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, null, 16, true, encoding);
				ptr4 += 16;
				ptr3 = (int*)ptr4;
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, this.CursorPosition);
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				if (this.LinkType == CicsBridgeLinkType.Program)
				{
					ptr4 = (byte*)ptr3;
					num = offset + (int)((long)(ptr4 - ptr2));
					ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.ProgramName, 8, true, encoding);
				}
			}
		}

		// Token: 0x06005A81 RID: 23169 RVA: 0x00174778 File Offset: 0x00172978
		internal unsafe override bool TryExtract(byte[] buffer, int numberOfBytesAvailable, int offset, bool truncationInEffect, ref int ccsidToUse, ref int numericEncodingToUse, out string nextFormat)
		{
			nextFormat = null;
			bool flag = NumericEncoding.EncodingValueIsLittleEndian(numericEncodingToUse);
			HisEncoding encoding = HisEncoding.GetEncoding(ccsidToUse);
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				ptr3++;
				int num = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				int num2 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				if (num < 1 || num > 2)
				{
					throw new InvalidProgramException("Version not 1 or 2");
				}
				if (num == 1 && num2 != 164)
				{
					throw new InvalidProgramException("Version is 1 - incorrect length");
				}
				if (num == 2 && num2 != 180)
				{
					throw new InvalidProgramException("Version is 2 - incorrect length");
				}
				this.versionDependentBytesConsumed = num2;
				ptr3 += 2;
				byte* ptr4 = (byte*)ptr3;
				int num3 = offset + (int)((long)(ptr4 - ptr2));
				nextFormat = ConversionHelpers.GetStringOrNull(buffer, num3, 8, encoding);
				ptr4 += 8;
				ptr3 = (int*)ptr4;
				this.Flags = (CicsBridgeFlag)ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				this.ReturnCode = (CicsBridgeReturnCode)ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				this.CompletionCode = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				this.ReasonCode = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				this.UowControl = (CicsBridgeUowControl)ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				this.GetWaitInterval = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				this.LinkType = (CicsBridgeLinkType)ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				this.OutputDataLength = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				this.FacilityKeepTime = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				this.AdsDescriptors = (CicsBridgeAdsDescriptor)ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				int num4 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				this.IsConversationalTask = num4 == 1;
				this.TaskEndStatus = (CicsBridgeTaskEndStatus)ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				ptr4 = (byte*)ptr3;
				byte[] array = new byte[8];
				bool flag2 = false;
				for (int i = 0; i < 8; i++)
				{
					array[i] = *(ptr4++);
					if (array[i] != 0)
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					this.Token = array;
				}
				num3 = offset + (int)((long)(ptr4 - ptr2));
				this.Function = ConversionHelpers.GetStringOrNull(buffer, num3, 4, encoding);
				ptr4 += 4;
				num3 = offset + (int)((long)(ptr4 - ptr2));
				this.AbendCode = ConversionHelpers.GetStringOrNull(buffer, num3, 4, encoding);
				ptr4 += 4;
				num3 = offset + (int)((long)(ptr4 - ptr2));
				this.Authenticator = ConversionHelpers.GetStringOrNull(buffer, num3, 8, encoding);
				ptr4 += 8;
				ptr4 += 8;
				num3 = offset + (int)((long)(ptr4 - ptr2));
				this.ReplyToFormat = ConversionHelpers.GetStringOrNull(buffer, num3, 8, encoding);
				ptr4 += 8;
				num3 = offset + (int)((long)(ptr4 - ptr2));
				this.RemoteSysId = ConversionHelpers.GetStringOrNull(buffer, num3, 4, encoding);
				ptr4 += 4;
				num3 = offset + (int)((long)(ptr4 - ptr2));
				this.RemoteTransactionId = ConversionHelpers.GetStringOrNull(buffer, num3, 4, encoding);
				ptr4 += 4;
				num3 = offset + (int)((long)(ptr4 - ptr2));
				this.TransactionId = ConversionHelpers.GetStringOrNull(buffer, num3, 4, encoding);
				ptr4 += 4;
				num3 = offset + (int)((long)(ptr4 - ptr2));
				this.FacilityIsLike = ConversionHelpers.GetStringOrNull(buffer, num3, 4, encoding);
				ptr4 += 4;
				this.AttentionId = *ptr4;
				ptr4 += 4;
				num3 = offset + (int)((long)(ptr4 - ptr2));
				string text = ConversionHelpers.GetStringOrNull(buffer, num3, 4, encoding);
				if (text == null)
				{
					text = string.Empty;
				}
				if (text != null)
				{
					if (text == null || text.Length != 0)
					{
						if (!(text == "S"))
						{
							if (!(text == "SD"))
							{
								if (!(text == "TD"))
								{
									goto IL_0357;
								}
								this.StartCode = CicsBridgeStartCode.TerminalInput;
							}
							else
							{
								this.StartCode = CicsBridgeStartCode.StartData;
							}
						}
						else
						{
							this.StartCode = CicsBridgeStartCode.Start;
						}
					}
					else
					{
						this.StartCode = CicsBridgeStartCode.None;
					}
					ptr4 += 4;
					num3 = offset + (int)((long)(ptr4 - ptr2));
					this.CancelCode = ConversionHelpers.GetStringOrNull(buffer, num3, 4, encoding);
					ptr4 += 4;
					num3 = offset + (int)((long)(ptr4 - ptr2));
					this.NextTransactionId = ConversionHelpers.GetStringOrNull(buffer, num3, 4, encoding);
					ptr4 += 4;
					ptr4 += 16;
					ptr3 = (int*)ptr4;
					if (num > 1)
					{
						this.CursorPosition = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
						this.ErrorOffset = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
						ptr3 += 2;
					}
					if (this.LinkType == CicsBridgeLinkType.Program)
					{
						ptr4 = (byte*)ptr3;
						num3 = offset + (int)((long)(ptr4 - ptr2));
						if (numberOfBytesAvailable - num3 >= 8)
						{
							this.programName = ConversionHelpers.GetStringOrNull(buffer, num3, 8, encoding);
							this.versionDependentBytesConsumed += 8;
						}
					}
					ptr = null;
					return true;
				}
				IL_0357:
				throw new InvalidProgramException("Unknown StartCode Received");
			}
		}

		// Token: 0x06005A82 RID: 23170 RVA: 0x00174BA8 File Offset: 0x00172DA8
		internal override MqHeader GenerateCopy(bool deepCopy)
		{
			if (!deepCopy)
			{
				return this;
			}
			return new CicsBridgeHeader
			{
				AbendCode = this.AbendCode,
				AdsDescriptors = this.AdsDescriptors,
				AttentionId = this.AttentionId,
				Authenticator = this.Authenticator,
				CancelCode = this.CancelCode,
				CompletionCode = this.CompletionCode,
				CursorPosition = this.CursorPosition,
				ErrorOffset = this.ErrorOffset,
				FacilityIsLike = this.FacilityIsLike,
				FacilityKeepTime = this.FacilityKeepTime,
				Flags = this.Flags,
				Function = this.Function,
				GetWaitInterval = this.GetWaitInterval,
				IsConversationalTask = this.IsConversationalTask,
				LinkType = this.LinkType,
				NextTransactionId = this.NextTransactionId,
				OutputDataLength = this.OutputDataLength,
				ProgramName = this.ProgramName,
				ReasonCode = this.ReasonCode,
				RemoteSysId = this.RemoteSysId,
				RemoteTransactionId = this.RemoteTransactionId,
				ReplyToFormat = this.ReplyToFormat,
				ReturnCode = this.ReturnCode,
				StartCode = this.StartCode,
				TaskEndStatus = this.TaskEndStatus,
				Token = ConversionHelpers.ByteArrayNullOrCopy(this.Token, deepCopy),
				TransactionId = this.TransactionId,
				UowControl = this.UowControl
			};
		}

		// Token: 0x0400474B RID: 18251
		public const int GetWaitIntervalDefault = -2;

		// Token: 0x0400474C RID: 18252
		public const int OutputLengthAsInput = -1;

		// Token: 0x04004759 RID: 18265
		private byte[] token;

		// Token: 0x0400475C RID: 18268
		private string authenticator;

		// Token: 0x0400475D RID: 18269
		private string replyToFormat;

		// Token: 0x0400475E RID: 18270
		private string remoteSysId;

		// Token: 0x0400475F RID: 18271
		private string remoteTransId;

		// Token: 0x04004760 RID: 18272
		private string transactionId;

		// Token: 0x04004761 RID: 18273
		private string facilityIsLike;

		// Token: 0x04004764 RID: 18276
		private string cancelCode;

		// Token: 0x04004768 RID: 18280
		private string programName;

		// Token: 0x04004769 RID: 18281
		private int versionDependentBytesConsumed;
	}
}
