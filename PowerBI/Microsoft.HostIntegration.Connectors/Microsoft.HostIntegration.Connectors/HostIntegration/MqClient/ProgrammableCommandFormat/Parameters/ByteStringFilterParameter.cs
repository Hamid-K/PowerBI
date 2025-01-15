using System;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat.Parameters
{
	// Token: 0x02000BB8 RID: 3000
	public class ByteStringFilterParameter : CommandParameter
	{
		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x06005D58 RID: 23896 RVA: 0x0017E453 File Offset: 0x0017C653
		// (set) Token: 0x06005D59 RID: 23897 RVA: 0x0017E45B File Offset: 0x0017C65B
		public ByteStringOperator Operator { get; set; }

		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x06005D5A RID: 23898 RVA: 0x0017E464 File Offset: 0x0017C664
		// (set) Token: 0x06005D5B RID: 23899 RVA: 0x0017E46C File Offset: 0x0017C66C
		public byte[] FilterValue { get; set; }

		// Token: 0x06005D5C RID: 23900 RVA: 0x0017E475 File Offset: 0x0017C675
		public ByteStringFilterParameter(int parameter)
			: base(ParameterType.ByteStringFilter, parameter)
		{
		}

		// Token: 0x06005D5D RID: 23901 RVA: 0x0017E480 File Offset: 0x0017C680
		internal ByteStringFilterParameter(int parameter, int receivedLength)
			: base(ParameterType.ByteStringFilter, parameter, receivedLength - 20)
		{
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x0017E490 File Offset: 0x0017C690
		internal unsafe override void Extract(byte[] buffer, ref int offset, bool littleEndian, int encodingCcsid, HisEncoding encoding, bool embeddedCcsid)
		{
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				this.Operator = (ByteStringOperator)ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				int num = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				this.FilterValue = new byte[num];
				byte* ptr4 = (byte*)ptr3;
				int num2 = offset + (int)((long)(ptr4 - ptr2));
				Array.Copy(buffer, num2, this.FilterValue, 0, num);
				ptr4 += this.receivedStructureLength;
				offset += (int)((long)(ptr4 - ptr2));
			}
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x0017E50C File Offset: 0x0017C70C
		internal override CommandParameter GenerateCopy()
		{
			ByteStringFilterParameter byteStringFilterParameter = new ByteStringFilterParameter(base.Parameter);
			byteStringFilterParameter.Operator = this.Operator;
			byteStringFilterParameter.FilterValue = new byte[this.FilterValue.Length];
			Array.Copy(this.FilterValue, byteStringFilterParameter.FilterValue, this.FilterValue.Length);
			return byteStringFilterParameter;
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x0017E55E File Offset: 0x0017C75E
		internal override int ConvertStructureToBytes(HisEncoding encoding, int ccsid, bool embedCcsid)
		{
			return 20 + base.MultipleOf4(this.FilterValue.Length);
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x0017E574 File Offset: 0x0017C774
		internal unsafe override void ConvertStructureToBytes(byte[] bytes, ref int index)
		{
			fixed (byte* ptr = &bytes[index])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = (int)base.ParameterType;
				*(ptr3++) = 20 + base.MultipleOf4(this.FilterValue.Length);
				*(ptr3++) = base.Parameter;
				*(ptr3++) = (int)this.Operator;
				*(ptr3++) = this.FilterValue.Length;
				byte* ptr4 = (byte*)ptr3;
				int num = index + (int)((long)(ptr4 - ptr2));
				Array.Copy(this.FilterValue, 0, bytes, num, this.FilterValue.Length);
				ptr4 += base.MultipleOf4(this.FilterValue.Length);
				index += (int)((long)(ptr4 - ptr2));
			}
		}
	}
}
