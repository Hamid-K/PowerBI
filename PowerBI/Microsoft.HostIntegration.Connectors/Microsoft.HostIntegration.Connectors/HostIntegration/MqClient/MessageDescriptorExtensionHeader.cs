using System;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B31 RID: 2865
	public class MessageDescriptorExtensionHeader : MqHeader
	{
		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x06005A3A RID: 23098 RVA: 0x00173F22 File Offset: 0x00172122
		public override int AsciiStructId
		{
			get
			{
				return 541410381;
			}
		}

		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x06005A3B RID: 23099 RVA: 0x00173F29 File Offset: 0x00172129
		public override int EbcdicStructId
		{
			get
			{
				return 1086702804;
			}
		}

		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x06005A3C RID: 23100 RVA: 0x0003A22E File Offset: 0x0003842E
		public override int MinimumVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x06005A3D RID: 23101 RVA: 0x0003A22E File Offset: 0x0003842E
		public override int MaximumVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06005A3E RID: 23102 RVA: 0x00173F30 File Offset: 0x00172130
		public MessageDescriptorExtensionHeader()
			: base(MqHeaderType.MessageDescriptorExtension, OrderedMqHeaderType.AllOthers, "Message Descriptor Extension", "MQHMDE", 72, false)
		{
		}

		// Token: 0x06005A3F RID: 23103 RVA: 0x00003CAB File Offset: 0x00001EAB
		internal override void GenerateBytes(byte[] buffer, int offset, string format, bool littleEndian, int ccsid, int numericEncodingValue, int ccsidValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005A40 RID: 23104 RVA: 0x00173F48 File Offset: 0x00172148
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
			}
			numericEncodingToUse = num;
			ccsidToUse = num2;
			return true;
		}

		// Token: 0x06005A41 RID: 23105 RVA: 0x00003CAB File Offset: 0x00001EAB
		internal override MqHeader GenerateCopy(bool deepCopy)
		{
			throw new NotImplementedException();
		}
	}
}
