using System;
using Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B53 RID: 2899
	public class EmbeddedProgrammableCommandFormatHeader : MqStructuredHeader
	{
		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x06005BB2 RID: 23474 RVA: 0x001790DF File Offset: 0x001772DF
		// (set) Token: 0x06005BB3 RID: 23475 RVA: 0x001790E7 File Offset: 0x001772E7
		public EmbeddedProgrammableCommandFormatFlag Flags { get; set; }

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x06005BB4 RID: 23476 RVA: 0x001790F0 File Offset: 0x001772F0
		// (set) Token: 0x06005BB5 RID: 23477 RVA: 0x001790F8 File Offset: 0x001772F8
		public CommandHeader ProgrammableCommandFormatHeader { get; set; }

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x06005BB6 RID: 23478 RVA: 0x00179101 File Offset: 0x00177301
		public override int AsciiStructId
		{
			get
			{
				return 541610053;
			}
		}

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x06005BB7 RID: 23479 RVA: 0x00179108 File Offset: 0x00177308
		public override int EbcdicStructId
		{
			get
			{
				return 1086904261;
			}
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x0017910F File Offset: 0x0017730F
		public EmbeddedProgrammableCommandFormatHeader()
			: base(MqHeaderType.EmbeddedProgrammableCommandFormat, OrderedMqHeaderType.AllOthers, "Embedded PCF Header", "MQHEPCF", 68)
		{
			this.Flags = EmbeddedProgrammableCommandFormatFlag.None;
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x0017912C File Offset: 0x0017732C
		internal unsafe override void GenerateBytes(byte[] buffer, int offset, string format, bool littleEndian, int ccsid, int numericEncodingValue, int ccsidValue)
		{
			HisEncoding encoding = HisEncoding.GetEncoding(ccsid);
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = 541610053;
				*(ptr3++) = 1;
				*(ptr3++) = this.SendLength;
				*(ptr3++) = numericEncodingValue;
				*(ptr3++) = ccsidValue;
				byte* ptr4 = (byte*)ptr3;
				int num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, format, 8, true, encoding);
				ptr3 += 2;
				*(ptr3++) = (int)this.Flags;
				ptr4 = (byte*)ptr3;
				num = offset + (int)((long)(ptr4 - ptr2));
				this.ProgrammableCommandFormatHeader.GenerateBytes(buffer, num);
			}
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x001791CC File Offset: 0x001773CC
		internal unsafe override bool TryExtract(byte[] buffer, int numberOfBytesAvailable, int offset, bool truncationInEffect, ref int ccsidToUse, ref int numericEncodingToUse, out string nextFormat)
		{
			nextFormat = null;
			bool flag = NumericEncoding.EncodingValueIsLittleEndian(numericEncodingToUse);
			HisEncoding encoding = HisEncoding.GetEncoding(ccsidToUse);
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				ptr3 += 2;
				int num = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				if (numberOfBytesAvailable - offset >= num)
				{
					this.consumedBytesLength = num - 68;
					int num2 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
					int num3 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
					byte* ptr4 = (byte*)ptr3;
					int num4 = offset + (int)((long)(ptr4 - ptr2));
					nextFormat = ConversionHelpers.GetStringOrNull(buffer, num4, 8, encoding);
					ptr4 += 8;
					ptr3 = (int*)ptr4;
					this.Flags = (EmbeddedProgrammableCommandFormatFlag)ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
					ptr4 = (byte*)ptr3;
					num4 = offset + (int)((long)(ptr4 - ptr2));
					ptr = null;
					this.ProgrammableCommandFormatHeader = new CommandHeader();
					this.ProgrammableCommandFormatHeader.Extract(buffer, num4, flag, ccsidToUse, encoding, this.Flags == EmbeddedProgrammableCommandFormatFlag.CcsidEmbedded);
					numericEncodingToUse = num2;
					ccsidToUse = num3;
					return true;
				}
				if (truncationInEffect)
				{
					return false;
				}
				throw new InvalidOperationException("Not enough bytes for all of the header!");
			}
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x001792C8 File Offset: 0x001774C8
		internal override MqHeader GenerateCopy(bool deepCopy)
		{
			if (!deepCopy)
			{
				return this;
			}
			return new EmbeddedProgrammableCommandFormatHeader
			{
				Flags = this.Flags,
				ProgrammableCommandFormatHeader = this.ProgrammableCommandFormatHeader.GenerateCopy()
			};
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x001792F1 File Offset: 0x001774F1
		protected override byte[] ConvertStructureToBytes()
		{
			return this.ProgrammableCommandFormatHeader.ConvertStructureToBytes(1252, this.Flags == EmbeddedProgrammableCommandFormatFlag.CcsidEmbedded);
		}
	}
}
