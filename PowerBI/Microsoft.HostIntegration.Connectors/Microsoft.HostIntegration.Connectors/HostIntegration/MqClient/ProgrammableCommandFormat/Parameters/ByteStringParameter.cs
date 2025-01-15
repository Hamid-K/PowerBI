using System;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat.Parameters
{
	// Token: 0x02000BB9 RID: 3001
	public class ByteStringParameter : CommandParameter
	{
		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06005D62 RID: 23906 RVA: 0x0017E61D File Offset: 0x0017C81D
		// (set) Token: 0x06005D63 RID: 23907 RVA: 0x0017E625 File Offset: 0x0017C825
		public byte[] Bytes { get; set; }

		// Token: 0x06005D64 RID: 23908 RVA: 0x0017E62E File Offset: 0x0017C82E
		public ByteStringParameter(int parameter)
			: base(ParameterType.ByteString, parameter)
		{
		}

		// Token: 0x06005D65 RID: 23909 RVA: 0x0017E639 File Offset: 0x0017C839
		internal ByteStringParameter(int parameter, int receivedLength)
			: base(ParameterType.ByteString, parameter, receivedLength - 16)
		{
		}

		// Token: 0x06005D66 RID: 23910 RVA: 0x0017E648 File Offset: 0x0017C848
		internal unsafe override void Extract(byte[] buffer, ref int offset, bool littleEndian, int encodingCcsid, HisEncoding encoding, bool embeddedCcsid)
		{
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				int num = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				this.Bytes = new byte[num];
				byte* ptr4 = (byte*)ptr3;
				int num2 = offset + (int)((long)(ptr4 - ptr2));
				Array.Copy(buffer, num2, this.Bytes, 0, num);
				ptr4 += this.receivedStructureLength;
				offset += (int)((long)(ptr4 - ptr2));
			}
		}

		// Token: 0x06005D67 RID: 23911 RVA: 0x0017E6B4 File Offset: 0x0017C8B4
		internal override CommandParameter GenerateCopy()
		{
			ByteStringParameter byteStringParameter = new ByteStringParameter(base.Parameter);
			byteStringParameter.Bytes = new byte[this.Bytes.Length];
			Array.Copy(this.Bytes, byteStringParameter.Bytes, this.Bytes.Length);
			return byteStringParameter;
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x0017E6FA File Offset: 0x0017C8FA
		internal override int ConvertStructureToBytes(HisEncoding encoding, int ccsid, bool embedCcsid)
		{
			return 16 + base.MultipleOf4(this.Bytes.Length);
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x0017E710 File Offset: 0x0017C910
		internal unsafe override void ConvertStructureToBytes(byte[] bytes, ref int index)
		{
			fixed (byte* ptr = &bytes[index])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = (int)base.ParameterType;
				*(ptr3++) = 16 + base.MultipleOf4(this.Bytes.Length);
				*(ptr3++) = base.Parameter;
				*(ptr3++) = this.Bytes.Length;
				byte* ptr4 = (byte*)ptr3;
				int num = index + (int)((long)(ptr4 - ptr2));
				Array.Copy(this.Bytes, 0, bytes, num, this.Bytes.Length);
				ptr4 += base.MultipleOf4(this.Bytes.Length);
				index += (int)((long)(ptr4 - ptr2));
			}
		}
	}
}
