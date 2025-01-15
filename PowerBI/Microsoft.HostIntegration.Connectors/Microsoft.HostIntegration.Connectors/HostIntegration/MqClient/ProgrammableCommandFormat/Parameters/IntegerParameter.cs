using System;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat.Parameters
{
	// Token: 0x02000BBC RID: 3004
	public class IntegerParameter : CommandParameter
	{
		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x06005D79 RID: 23929 RVA: 0x0017EA0C File Offset: 0x0017CC0C
		// (set) Token: 0x06005D7A RID: 23930 RVA: 0x0017EA14 File Offset: 0x0017CC14
		public int Value { get; set; }

		// Token: 0x06005D7B RID: 23931 RVA: 0x0017EA1D File Offset: 0x0017CC1D
		public IntegerParameter(int parameter)
			: base(ParameterType.Integer, parameter)
		{
		}

		// Token: 0x06005D7C RID: 23932 RVA: 0x0017EA28 File Offset: 0x0017CC28
		internal unsafe override void Extract(byte[] buffer, ref int offset, bool littleEndian, int encodingCcsid, HisEncoding encoding, bool embeddedCcsid)
		{
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				this.Value = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				offset += (int)((long)((byte*)ptr3 - (byte*)ptr2));
			}
		}

		// Token: 0x06005D7D RID: 23933 RVA: 0x0017EA60 File Offset: 0x0017CC60
		internal override CommandParameter GenerateCopy()
		{
			return new IntegerParameter(base.Parameter)
			{
				Value = this.Value
			};
		}

		// Token: 0x06005D7E RID: 23934 RVA: 0x0003A92B File Offset: 0x00038B2B
		internal override int ConvertStructureToBytes(HisEncoding encoding, int ccsid, bool embedCcsid)
		{
			return 16;
		}

		// Token: 0x06005D7F RID: 23935 RVA: 0x0017EA7C File Offset: 0x0017CC7C
		internal unsafe override void ConvertStructureToBytes(byte[] bytes, ref int index)
		{
			fixed (byte* ptr = &bytes[index])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = (int)base.ParameterType;
				*(ptr3++) = 16;
				*(ptr3++) = base.Parameter;
				*(ptr3++) = this.Value;
				byte* ptr4 = (byte*)ptr3;
				index += (int)((long)(ptr4 - ptr2));
			}
		}
	}
}
