using System;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B54 RID: 2900
	public class WorkInformationHeader : MqHeader
	{
		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x06005BBD RID: 23485 RVA: 0x0017930C File Offset: 0x0017750C
		// (set) Token: 0x06005BBE RID: 23486 RVA: 0x00179314 File Offset: 0x00177514
		public WorkInformationFlag Flags { get; set; }

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x06005BBF RID: 23487 RVA: 0x0017931D File Offset: 0x0017751D
		// (set) Token: 0x06005BC0 RID: 23488 RVA: 0x00179325 File Offset: 0x00177525
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
			set
			{
				this.serviceName = Globals.CheckMaximumLengthTrimmedNonNull(value, "ServiceName", 32);
			}
		}

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x06005BC1 RID: 23489 RVA: 0x0017933A File Offset: 0x0017753A
		// (set) Token: 0x06005BC2 RID: 23490 RVA: 0x00179342 File Offset: 0x00177542
		public string ServiceStep
		{
			get
			{
				return this.serviceStep;
			}
			set
			{
				this.serviceStep = Globals.CheckMaximumLengthTrimmedNonNull(value, "ServiceStep", 8);
			}
		}

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x06005BC3 RID: 23491 RVA: 0x00179356 File Offset: 0x00177556
		// (set) Token: 0x06005BC4 RID: 23492 RVA: 0x0017935E File Offset: 0x0017755E
		public byte[] MessageToken
		{
			get
			{
				return this.messageToken;
			}
			set
			{
				this.messageToken = Globals.CheckExactLength(value, "MessageToken", 16);
			}
		}

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x06005BC5 RID: 23493 RVA: 0x00179373 File Offset: 0x00177573
		public override int AsciiStructId
		{
			get
			{
				return 541608279;
			}
		}

		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x06005BC6 RID: 23494 RVA: 0x0017937A File Offset: 0x0017757A
		public override int EbcdicStructId
		{
			get
			{
				return 1086900710;
			}
		}

		// Token: 0x06005BC7 RID: 23495 RVA: 0x00179381 File Offset: 0x00177581
		public WorkInformationHeader()
			: base(MqHeaderType.WorkInformation, OrderedMqHeaderType.AllOthers, "Work Information Header", "MQHWIH", 120)
		{
			this.Flags = WorkInformationFlag.None;
		}

		// Token: 0x06005BC8 RID: 23496 RVA: 0x001793A0 File Offset: 0x001775A0
		internal unsafe override void GenerateBytes(byte[] buffer, int offset, string format, bool littleEndian, int ccsid, int numericEncodingValue, int ccsidValue)
		{
			HisEncoding encoding = HisEncoding.GetEncoding(ccsid);
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = 541608279;
				*(ptr3++) = 1;
				*(ptr3++) = this.SendLength;
				*(ptr3++) = numericEncodingValue;
				*(ptr3++) = ccsidValue;
				byte* ptr4 = (byte*)ptr3;
				int num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, format, 8, true, encoding);
				ptr4 += 8;
				ptr3 = (int*)ptr4;
				*(ptr3++) = (int)this.Flags;
				ptr4 = (byte*)ptr3;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.ServiceName, 32, true, encoding);
				ptr4 += 32;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.ServiceStep, 8, true, encoding);
				ptr4 += 8;
				for (int i = 0; i < 16; i++)
				{
					*(ptr4++) = this.MessageToken[i];
				}
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, null, 32, true, encoding);
			}
		}

		// Token: 0x06005BC9 RID: 23497 RVA: 0x001794AC File Offset: 0x001776AC
		internal unsafe override bool TryExtract(byte[] buffer, int numberOfBytesAvailable, int offset, bool truncationInEffect, ref int ccsidToUse, ref int numericEncodingToUse, out string nextFormat)
		{
			nextFormat = null;
			bool flag = NumericEncoding.EncodingValueIsLittleEndian(numericEncodingToUse);
			HisEncoding encoding = HisEncoding.GetEncoding(ccsidToUse);
			int num;
			int num2;
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				ptr3 += 3;
				num = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				num2 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				byte* ptr4 = (byte*)ptr3;
				int num3 = offset + (int)((long)(ptr4 - ptr2));
				nextFormat = ConversionHelpers.GetStringOrNull(buffer, num3, 8, encoding);
				ptr4 += 8;
				ptr3 = (int*)ptr4;
				this.Flags = (WorkInformationFlag)ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				ptr4 = (byte*)ptr3;
				num3 = offset + (int)((long)(ptr4 - ptr2));
				this.ServiceName = ConversionHelpers.GetStringOrNull(buffer, num3, 32, encoding);
				ptr4 += 32;
				num3 = offset + (int)((long)(ptr4 - ptr2));
				this.ServiceStep = ConversionHelpers.GetStringOrNull(buffer, num3, 8, encoding);
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
					this.MessageToken = array;
				}
			}
			numericEncodingToUse = num;
			ccsidToUse = num2;
			return true;
		}

		// Token: 0x06005BCA RID: 23498 RVA: 0x001795CF File Offset: 0x001777CF
		internal override MqHeader GenerateCopy(bool deepCopy)
		{
			if (!deepCopy)
			{
				return this;
			}
			return new WorkInformationHeader
			{
				Flags = this.Flags,
				MessageToken = this.MessageToken,
				ServiceName = this.ServiceName,
				ServiceStep = this.ServiceStep
			};
		}

		// Token: 0x04004807 RID: 18439
		private string serviceName;

		// Token: 0x04004808 RID: 18440
		private string serviceStep;

		// Token: 0x04004809 RID: 18441
		private byte[] messageToken;
	}
}
