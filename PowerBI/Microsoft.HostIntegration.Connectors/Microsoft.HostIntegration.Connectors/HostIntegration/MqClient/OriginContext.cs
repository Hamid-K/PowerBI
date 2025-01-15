using System;
using System.Text;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B55 RID: 2901
	public class OriginContext
	{
		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x06005BCB RID: 23499 RVA: 0x0017960B File Offset: 0x0017780B
		// (set) Token: 0x06005BCC RID: 23500 RVA: 0x00179613 File Offset: 0x00177813
		public PutApplicationType PutApplicationType { get; set; }

		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x06005BCD RID: 23501 RVA: 0x0017961C File Offset: 0x0017781C
		// (set) Token: 0x06005BCE RID: 23502 RVA: 0x00179624 File Offset: 0x00177824
		public string PutApplicationName
		{
			get
			{
				return this.putApplicationName;
			}
			set
			{
				this.putApplicationName = Globals.CheckMaximumLength(value, "PutApplicationName", 28);
			}
		}

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x06005BCF RID: 23503 RVA: 0x00179639 File Offset: 0x00177839
		// (set) Token: 0x06005BD0 RID: 23504 RVA: 0x00179641 File Offset: 0x00177841
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

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x06005BD1 RID: 23505 RVA: 0x00179659 File Offset: 0x00177859
		// (set) Token: 0x06005BD2 RID: 23506 RVA: 0x00179661 File Offset: 0x00177861
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

		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x06005BD3 RID: 23507 RVA: 0x00179679 File Offset: 0x00177879
		// (set) Token: 0x06005BD4 RID: 23508 RVA: 0x00179681 File Offset: 0x00177881
		public string ApplicationData
		{
			get
			{
				return this.applicationData;
			}
			set
			{
				this.applicationData = Globals.CheckMaximumLength(value, "ApplicationData", 4);
			}
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x00179698 File Offset: 0x00177898
		internal static OriginContext GenerateCopy(OriginContext other, bool create)
		{
			if (other == null || !create)
			{
				return other;
			}
			return new OriginContext
			{
				ApplicationData = other.ApplicationData,
				PutApplicationName = other.PutApplicationName,
				PutApplicationType = other.PutApplicationType,
				PutDate = other.PutDate,
				PutTime = other.PutTime
			};
		}

		// Token: 0x06005BD6 RID: 23510 RVA: 0x001796F0 File Offset: 0x001778F0
		internal unsafe static int GenerateMqmdBytes(OriginContext context, byte[] buffer, int offset)
		{
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				if (context == null)
				{
					*(ptr3++) = 0;
				}
				else
				{
					*(ptr3++) = (int)context.PutApplicationType;
				}
				byte* ptr4 = (byte*)ptr3;
				if (context == null || context.PutApplicationName == null)
				{
					*ptr4 = 0;
				}
				else
				{
					ConversionHelpers.MoveStringToBufferAscii(buffer, (int)((long)(ptr4 - ptr2) + (long)offset), context.PutApplicationName, 28, false);
				}
				ptr4 += 28;
				if (context == null || context.PutDate == null)
				{
					*ptr4 = 0;
				}
				else
				{
					ConversionHelpers.MoveStringToBufferAscii(buffer, (int)((long)(ptr4 - ptr2) + (long)offset), context.PutDate, 8, false);
				}
				ptr4 += 8;
				if (context == null || context.PutTime == null)
				{
					*ptr4 = 0;
				}
				else
				{
					ConversionHelpers.MoveStringToBufferAscii(buffer, (int)((long)(ptr4 - ptr2) + (long)offset), context.PutTime, 8, false);
				}
				ptr4 += 8;
				if (context == null || context.ApplicationData == null)
				{
					*ptr4 = 0;
				}
				else
				{
					ConversionHelpers.MoveStringToBufferAscii(buffer, (int)((long)(ptr4 - ptr2) + (long)offset), context.ApplicationData, 4, false);
				}
				ptr4 += 4;
				return (int)((long)(ptr4 - ptr2));
			}
		}

		// Token: 0x06005BD7 RID: 23511 RVA: 0x001797E0 File Offset: 0x001779E0
		internal unsafe static OriginContext ExtractMqmd(byte[] buffer, int offset, bool littleEndian, Encoding encoding, out int bytesConsumed)
		{
			bytesConsumed = 52;
			PutApplicationType putApplicationType;
			fixed (byte* ptr = &buffer[offset])
			{
				int* ptr2 = (int*)ptr;
				putApplicationType = (PutApplicationType)ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
				if (putApplicationType == PutApplicationType.NoContext)
				{
					return null;
				}
			}
			offset += 4;
			OriginContext originContext = new OriginContext();
			originContext.PutApplicationType = putApplicationType;
			originContext.PutApplicationName = ConversionHelpers.GetStringOrNull(buffer, offset, 28, encoding);
			offset += 28;
			originContext.PutDate = ConversionHelpers.GetStringOrNull(buffer, offset, 8, encoding);
			offset += 8;
			originContext.PutTime = ConversionHelpers.GetStringOrNull(buffer, offset, 8, encoding);
			offset += 8;
			originContext.ApplicationData = ConversionHelpers.GetStringOrNull(buffer, offset, 4, encoding);
			return originContext;
		}

		// Token: 0x0400480B RID: 18443
		private string putApplicationName;

		// Token: 0x0400480C RID: 18444
		private string putDate;

		// Token: 0x0400480D RID: 18445
		private string putTime;

		// Token: 0x0400480E RID: 18446
		private string applicationData;
	}
}
