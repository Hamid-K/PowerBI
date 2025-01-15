using System;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat.Parameters
{
	// Token: 0x02000BBD RID: 3005
	public class StringFilterParameter : CommandParameter
	{
		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x06005D80 RID: 23936 RVA: 0x0017EAD4 File Offset: 0x0017CCD4
		// (set) Token: 0x06005D81 RID: 23937 RVA: 0x0017EADC File Offset: 0x0017CCDC
		public string FilterValue { get; set; }

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x06005D82 RID: 23938 RVA: 0x0017EAE5 File Offset: 0x0017CCE5
		// (set) Token: 0x06005D83 RID: 23939 RVA: 0x0017EAED File Offset: 0x0017CCED
		public StringOperator Operator { get; set; }

		// Token: 0x06005D84 RID: 23940 RVA: 0x0017EAF6 File Offset: 0x0017CCF6
		public StringFilterParameter(int parameter)
			: base(ParameterType.StringFilter, parameter)
		{
		}

		// Token: 0x06005D85 RID: 23941 RVA: 0x0017EB01 File Offset: 0x0017CD01
		internal StringFilterParameter(int parameter, int receivedLength)
			: base(ParameterType.StringFilter, parameter, receivedLength - 24)
		{
		}

		// Token: 0x06005D86 RID: 23942 RVA: 0x0017EB10 File Offset: 0x0017CD10
		internal unsafe override void Extract(byte[] buffer, ref int offset, bool littleEndian, int encodingCcsid, HisEncoding encoding, bool embeddedCcsid)
		{
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				this.Operator = (StringOperator)ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				if (embeddedCcsid)
				{
					int num = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
					if (num != encodingCcsid)
					{
						encoding = HisEncoding.GetEncoding(num);
					}
				}
				else
				{
					ptr3++;
				}
				int num2 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				byte* ptr4 = (byte*)ptr3;
				int num3 = offset + (int)((long)(ptr4 - ptr2));
				this.FilterValue = encoding.GetString(buffer, num3, num2);
				ptr4 += this.receivedStructureLength;
				offset += (int)((long)(ptr4 - ptr2));
			}
		}

		// Token: 0x06005D87 RID: 23943 RVA: 0x0017EBA1 File Offset: 0x0017CDA1
		internal override CommandParameter GenerateCopy()
		{
			return new StringFilterParameter(base.Parameter)
			{
				FilterValue = this.FilterValue,
				Operator = this.Operator
			};
		}

		// Token: 0x06005D88 RID: 23944 RVA: 0x0017EBC6 File Offset: 0x0017CDC6
		internal override int ConvertStructureToBytes(HisEncoding encoding, int ccsid, bool embedCcsid)
		{
			this.ccsidUsed = ccsid;
			this.needToEmbedCcsid = embedCcsid;
			this.preparedBytes = encoding.GetBytes(this.FilterValue);
			return 24 + base.MultipleOf4(this.preparedBytes.Length);
		}

		// Token: 0x06005D89 RID: 23945 RVA: 0x0017EBFC File Offset: 0x0017CDFC
		internal unsafe override void ConvertStructureToBytes(byte[] bytes, ref int index)
		{
			fixed (byte* ptr = &bytes[index])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = (int)base.ParameterType;
				*(ptr3++) = 24 + base.MultipleOf4(this.preparedBytes.Length);
				*(ptr3++) = base.Parameter;
				*(ptr3++) = (int)this.Operator;
				if (this.needToEmbedCcsid)
				{
					*(ptr3++) = this.ccsidUsed;
				}
				else
				{
					*(ptr3++) = 0;
				}
				*(ptr3++) = this.preparedBytes.Length;
				byte* ptr4 = (byte*)ptr3;
				int num = index + (int)((long)(ptr4 - ptr2));
				Array.Copy(this.preparedBytes, 0, bytes, num, this.preparedBytes.Length);
				ptr4 += base.MultipleOf4(this.preparedBytes.Length);
				index += (int)((long)(ptr4 - ptr2));
			}
		}

		// Token: 0x04004EA3 RID: 20131
		private int ccsidUsed;

		// Token: 0x04004EA4 RID: 20132
		private bool needToEmbedCcsid;

		// Token: 0x04004EA5 RID: 20133
		private byte[] preparedBytes;
	}
}
