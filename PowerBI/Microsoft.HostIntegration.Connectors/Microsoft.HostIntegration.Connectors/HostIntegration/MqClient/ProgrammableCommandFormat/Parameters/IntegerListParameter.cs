using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat.Parameters
{
	// Token: 0x02000BBB RID: 3003
	public class IntegerListParameter : CommandParameter
	{
		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x06005D73 RID: 23923 RVA: 0x0017E8B0 File Offset: 0x0017CAB0
		public List<int> Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x06005D74 RID: 23924 RVA: 0x0017E8B8 File Offset: 0x0017CAB8
		public IntegerListParameter(int parameter)
			: base(ParameterType.IntegerList, parameter)
		{
		}

		// Token: 0x06005D75 RID: 23925 RVA: 0x0017E8D0 File Offset: 0x0017CAD0
		internal unsafe override void Extract(byte[] buffer, ref int offset, bool littleEndian, int encodingCcsid, HisEncoding encoding, bool embeddedCcsid)
		{
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				int num = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				for (int i = 0; i < num; i++)
				{
					this.values.Add(ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian));
				}
				offset += (int)((long)((byte*)ptr3 - (byte*)ptr2));
			}
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x0017E926 File Offset: 0x0017CB26
		internal override CommandParameter GenerateCopy()
		{
			IntegerListParameter integerListParameter = new IntegerListParameter(base.Parameter);
			integerListParameter.Values.AddRange(this.Values);
			return integerListParameter;
		}

		// Token: 0x06005D77 RID: 23927 RVA: 0x0017E944 File Offset: 0x0017CB44
		internal override int ConvertStructureToBytes(HisEncoding encoding, int ccsid, bool embedCcsid)
		{
			return 16 + 4 * this.Values.Count;
		}

		// Token: 0x06005D78 RID: 23928 RVA: 0x0017E958 File Offset: 0x0017CB58
		internal unsafe override void ConvertStructureToBytes(byte[] bytes, ref int index)
		{
			fixed (byte* ptr = &bytes[index])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = (int)base.ParameterType;
				*(ptr3++) = 16 + 4 * this.Values.Count;
				*(ptr3++) = base.Parameter;
				*(ptr3++) = this.Values.Count;
				foreach (int num in this.Values)
				{
					*(ptr3++) = num;
				}
				byte* ptr4 = (byte*)ptr3;
				index += (int)((long)(ptr4 - ptr2));
			}
		}

		// Token: 0x04004EA1 RID: 20129
		private List<int> values = new List<int>();
	}
}
