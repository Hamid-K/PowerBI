using System;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B52 RID: 2898
	public class DeadLetterHeader : MqHeader
	{
		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x06005B9E RID: 23454 RVA: 0x00178CE0 File Offset: 0x00176EE0
		// (set) Token: 0x06005B9F RID: 23455 RVA: 0x00178CE8 File Offset: 0x00176EE8
		public int Reason { get; set; }

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x06005BA0 RID: 23456 RVA: 0x00178CF1 File Offset: 0x00176EF1
		// (set) Token: 0x06005BA1 RID: 23457 RVA: 0x00178CF9 File Offset: 0x00176EF9
		public string OriginalQueue
		{
			get
			{
				return this.originalQueue;
			}
			set
			{
				this.originalQueue = Globals.CheckMaximumLengthTrimmedNonNull(value, "OriginalQueue", 48);
			}
		}

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x06005BA2 RID: 23458 RVA: 0x00178D0E File Offset: 0x00176F0E
		// (set) Token: 0x06005BA3 RID: 23459 RVA: 0x00178D16 File Offset: 0x00176F16
		public string OriginalQueueManager
		{
			get
			{
				return this.originalQueueManager;
			}
			set
			{
				this.originalQueueManager = Globals.CheckMaximumLengthTrimmedNonNull(value, "OriginalQueueManager", 48);
			}
		}

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06005BA4 RID: 23460 RVA: 0x00178D2B File Offset: 0x00176F2B
		// (set) Token: 0x06005BA5 RID: 23461 RVA: 0x00178D33 File Offset: 0x00176F33
		public PutApplicationType PutApplicationType { get; set; }

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x06005BA6 RID: 23462 RVA: 0x00178D3C File Offset: 0x00176F3C
		// (set) Token: 0x06005BA7 RID: 23463 RVA: 0x00178D44 File Offset: 0x00176F44
		public string PutApplicationName
		{
			get
			{
				return this.putApplicationName;
			}
			set
			{
				this.putApplicationName = Globals.CheckMaximumLengthTrimmedNonNull(value, "PutApplicationName", 28);
			}
		}

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x06005BA8 RID: 23464 RVA: 0x00178D59 File Offset: 0x00176F59
		// (set) Token: 0x06005BA9 RID: 23465 RVA: 0x00178D61 File Offset: 0x00176F61
		public string PutDate
		{
			get
			{
				return this.putDate;
			}
			set
			{
				this.putDate = Globals.CheckDateFormat(value, "PutDate", "yyyyMMdd");
			}
		}

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x06005BAA RID: 23466 RVA: 0x00178D79 File Offset: 0x00176F79
		// (set) Token: 0x06005BAB RID: 23467 RVA: 0x00178D81 File Offset: 0x00176F81
		public string PutTime
		{
			get
			{
				return this.putTime;
			}
			set
			{
				this.putTime = Globals.CheckTimeFormat(value, "PutTime", "HHmmssff");
			}
		}

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x06005BAC RID: 23468 RVA: 0x00178D99 File Offset: 0x00176F99
		public override int AsciiStructId
		{
			get
			{
				return 541609028;
			}
		}

		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x06005BAD RID: 23469 RVA: 0x00178DA0 File Offset: 0x00176FA0
		public override int EbcdicStructId
		{
			get
			{
				return 1086903236;
			}
		}

		// Token: 0x06005BAE RID: 23470 RVA: 0x00178DA7 File Offset: 0x00176FA7
		public DeadLetterHeader()
			: base(MqHeaderType.DeadLetter, OrderedMqHeaderType.DeadLetter, "Dead Letter Header", "MQDEAD", 172)
		{
			this.PutApplicationType = PutApplicationType.NoContext;
		}

		// Token: 0x06005BAF RID: 23471 RVA: 0x00178DC8 File Offset: 0x00176FC8
		internal unsafe override void GenerateBytes(byte[] buffer, int offset, string format, bool littleEndian, int ccsid, int numericEncodingValue, int ccsidValue)
		{
			HisEncoding encoding = HisEncoding.GetEncoding(ccsid);
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = 541609028;
				*(ptr3++) = 1;
				*(ptr3++) = this.Reason;
				byte* ptr4 = (byte*)ptr3;
				int num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.OriginalQueue, 48, true, encoding);
				ptr4 += 48;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.OriginalQueueManager, 48, true, encoding);
				ptr4 += 48;
				ptr3 = (int*)ptr4;
				*(ptr3++) = numericEncodingValue;
				*(ptr3++) = ccsidValue;
				ptr4 = (byte*)ptr3;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, format, 8, true, encoding);
				ptr4 += 8;
				ptr3 = (int*)ptr4;
				*(ptr3++) = (int)this.PutApplicationType;
				ptr4 = (byte*)ptr3;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.PutApplicationName, 28, true, encoding);
				ptr4 += 28;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.PutDate, 8, true, encoding);
				ptr4 += 8;
				num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, this.PutTime, 8, true, encoding);
				ptr4 += 8;
			}
		}

		// Token: 0x06005BB0 RID: 23472 RVA: 0x00178F0C File Offset: 0x0017710C
		internal unsafe override bool TryExtract(byte[] buffer, int numberOfBytesAvailable, int offset, bool truncationInEffect, ref int ccsidToUse, ref int numericEncodingToUse, out string nextFormat)
		{
			nextFormat = null;
			bool flag = NumericEncoding.EncodingValueIsLittleEndian(numericEncodingToUse);
			HisEncoding encoding = HisEncoding.GetEncoding(ccsidToUse);
			int num2;
			int num3;
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				ptr3 += 2;
				this.Reason = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				byte* ptr4 = (byte*)ptr3;
				int num = offset + (int)((long)(ptr4 - ptr2));
				this.OriginalQueue = ConversionHelpers.GetStringOrNull(buffer, num, 48, encoding);
				ptr4 += 48;
				num = offset + (int)((long)(ptr4 - ptr2));
				this.OriginalQueueManager = ConversionHelpers.GetStringOrNull(buffer, num, 48, encoding);
				ptr4 += 48;
				ptr3 = (int*)ptr4;
				num2 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				num3 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				ptr4 = (byte*)ptr3;
				num = offset + (int)((long)(ptr4 - ptr2));
				nextFormat = ConversionHelpers.GetStringOrNull(buffer, num, 8, encoding);
				ptr4 += 8;
				ptr3 = (int*)ptr4;
				this.PutApplicationType = (PutApplicationType)ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				ptr4 = (byte*)ptr3;
				num = offset + (int)((long)(ptr4 - ptr2));
				this.PutApplicationName = ConversionHelpers.GetStringOrNull(buffer, num, 28, encoding);
				ptr4 += 28;
				num = offset + (int)((long)(ptr4 - ptr2));
				this.PutDate = ConversionHelpers.GetStringOrNull(buffer, num, 8, encoding);
				ptr4 += 8;
				num = offset + (int)((long)(ptr4 - ptr2));
				this.PutTime = ConversionHelpers.GetStringOrNull(buffer, num, 8, encoding);
				ptr4 += 8;
			}
			numericEncodingToUse = num2;
			ccsidToUse = num3;
			return true;
		}

		// Token: 0x06005BB1 RID: 23473 RVA: 0x00179074 File Offset: 0x00177274
		internal override MqHeader GenerateCopy(bool deepCopy)
		{
			if (!deepCopy)
			{
				return this;
			}
			return new DeadLetterHeader
			{
				OriginalQueue = this.OriginalQueue,
				OriginalQueueManager = this.OriginalQueueManager,
				PutApplicationName = this.PutApplicationName,
				PutApplicationType = this.PutApplicationType,
				PutDate = this.PutDate,
				PutTime = this.PutTime,
				Reason = this.Reason
			};
		}

		// Token: 0x040047FE RID: 18430
		private string originalQueue;

		// Token: 0x040047FF RID: 18431
		private string originalQueueManager;

		// Token: 0x04004801 RID: 18433
		private string putApplicationName;

		// Token: 0x04004802 RID: 18434
		private string putDate;

		// Token: 0x04004803 RID: 18435
		private string putTime;
	}
}
