using System;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat.Parameters
{
	// Token: 0x02000BBA RID: 3002
	public class IntegerFilterParameter : CommandParameter
	{
		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x06005D6A RID: 23914 RVA: 0x0017E7AD File Offset: 0x0017C9AD
		// (set) Token: 0x06005D6B RID: 23915 RVA: 0x0017E7B5 File Offset: 0x0017C9B5
		public int FilterValue { get; set; }

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x06005D6C RID: 23916 RVA: 0x0017E7BE File Offset: 0x0017C9BE
		// (set) Token: 0x06005D6D RID: 23917 RVA: 0x0017E7C6 File Offset: 0x0017C9C6
		public IntegerOperator Operator { get; set; }

		// Token: 0x06005D6E RID: 23918 RVA: 0x0017E7CF File Offset: 0x0017C9CF
		public IntegerFilterParameter(int parameter)
			: base(ParameterType.IntegerFilter, parameter)
		{
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x0017E7DC File Offset: 0x0017C9DC
		internal unsafe override void Extract(byte[] buffer, ref int offset, bool littleEndian, int encodingCcsid, HisEncoding encoding, bool embeddedCcsid)
		{
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				this.Operator = (IntegerOperator)ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				this.FilterValue = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				offset += (int)((long)((byte*)ptr3 - (byte*)ptr2));
			}
		}

		// Token: 0x06005D70 RID: 23920 RVA: 0x0017E822 File Offset: 0x0017CA22
		internal override CommandParameter GenerateCopy()
		{
			return new IntegerFilterParameter(base.Parameter)
			{
				FilterValue = this.FilterValue,
				Operator = this.Operator
			};
		}

		// Token: 0x06005D71 RID: 23921 RVA: 0x0017E847 File Offset: 0x0017CA47
		internal override int ConvertStructureToBytes(HisEncoding encoding, int ccsid, bool embedCcsid)
		{
			return 20;
		}

		// Token: 0x06005D72 RID: 23922 RVA: 0x0017E84C File Offset: 0x0017CA4C
		internal unsafe override void ConvertStructureToBytes(byte[] bytes, ref int index)
		{
			fixed (byte* ptr = &bytes[index])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = (int)base.ParameterType;
				*(ptr3++) = 20;
				*(ptr3++) = base.Parameter;
				*(ptr3++) = (int)this.Operator;
				*(ptr3++) = this.FilterValue;
				byte* ptr4 = (byte*)ptr3;
				index += (int)((long)(ptr4 - ptr2));
			}
		}
	}
}
