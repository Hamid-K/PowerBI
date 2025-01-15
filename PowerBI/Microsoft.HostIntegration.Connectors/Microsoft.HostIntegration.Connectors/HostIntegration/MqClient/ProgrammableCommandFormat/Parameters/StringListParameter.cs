using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat.Parameters
{
	// Token: 0x02000BBE RID: 3006
	public class StringListParameter : CommandParameter
	{
		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x06005D8A RID: 23946 RVA: 0x0017ECC2 File Offset: 0x0017CEC2
		public List<string> Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x06005D8B RID: 23947 RVA: 0x0017ECCA File Offset: 0x0017CECA
		public StringListParameter(int parameter)
			: base(ParameterType.StringList, parameter)
		{
		}

		// Token: 0x06005D8C RID: 23948 RVA: 0x0017ECDF File Offset: 0x0017CEDF
		internal StringListParameter(int parameter, int receivedLength)
			: base(ParameterType.StringList, parameter, receivedLength - 24)
		{
		}

		// Token: 0x06005D8D RID: 23949 RVA: 0x0017ECF8 File Offset: 0x0017CEF8
		internal unsafe override void Extract(byte[] buffer, ref int offset, bool littleEndian, int encodingCcsid, HisEncoding encoding, bool embeddedCcsid)
		{
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
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
				int num3 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				byte* ptr4 = (byte*)ptr3;
				int num4 = offset + (int)((long)(ptr4 - ptr2));
				for (int i = 0; i < num2; i++)
				{
					this.Values.Add(encoding.GetString(buffer, num4, num3));
					num4 += num3;
				}
				ptr4 += this.receivedStructureLength;
				offset += (int)((long)(ptr4 - ptr2));
			}
		}

		// Token: 0x06005D8E RID: 23950 RVA: 0x0017EDA2 File Offset: 0x0017CFA2
		internal override CommandParameter GenerateCopy()
		{
			StringListParameter stringListParameter = new StringListParameter(base.Parameter);
			stringListParameter.Values.AddRange(this.values);
			return stringListParameter;
		}

		// Token: 0x06005D8F RID: 23951 RVA: 0x0017EDC0 File Offset: 0x0017CFC0
		internal override int ConvertStructureToBytes(HisEncoding encoding, int ccsid, bool embedCcsid)
		{
			this.ccsidUsed = ccsid;
			this.needToEmbedCcsid = embedCcsid;
			byte[] bytes = encoding.GetBytes(" ");
			List<byte[]> list = new List<byte[]>(this.Values.Count);
			foreach (string text in this.Values)
			{
				list.Add(encoding.GetBytes(text));
			}
			int num = 0;
			foreach (byte[] array in list)
			{
				if (array.Length > num)
				{
					num = array.Length;
				}
			}
			this.lengthOfEachPreparedString = num;
			this.preparedBytes = new byte[num * this.Values.Count];
			int num2 = 0;
			foreach (byte[] array2 in list)
			{
				Array.Copy(array2, 0, this.preparedBytes, num2, array2.Length);
				if (array2.Length != num)
				{
					int num3 = num - array2.Length;
					if (num3 % bytes.Length != 0)
					{
						throw new CustomMqClientException(SR.StringListLengths);
					}
					int num4 = num3 / bytes.Length;
					int num5 = num2 + array2.Length;
					for (int i = 0; i < num4; i++)
					{
						for (int j = 0; j < bytes.Length; j++)
						{
							this.preparedBytes[num5++] = bytes[j];
						}
					}
				}
				num2 += num;
			}
			return 24 + base.MultipleOf4(this.preparedBytes.Length);
		}

		// Token: 0x06005D90 RID: 23952 RVA: 0x0017EF78 File Offset: 0x0017D178
		internal unsafe override void ConvertStructureToBytes(byte[] bytes, ref int index)
		{
			fixed (byte* ptr = &bytes[index])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = (int)base.ParameterType;
				*(ptr3++) = 24 + base.MultipleOf4(this.preparedBytes.Length);
				*(ptr3++) = base.Parameter;
				if (this.needToEmbedCcsid)
				{
					*(ptr3++) = this.ccsidUsed;
				}
				else
				{
					*(ptr3++) = 0;
				}
				*(ptr3++) = this.Values.Count;
				*(ptr3++) = this.lengthOfEachPreparedString;
				byte* ptr4 = (byte*)ptr3;
				int num = index + (int)((long)(ptr4 - ptr2));
				Array.Copy(this.preparedBytes, 0, bytes, num, this.preparedBytes.Length);
				ptr4 += base.MultipleOf4(this.preparedBytes.Length);
				index += (int)((long)(ptr4 - ptr2));
			}
		}

		// Token: 0x04004EA8 RID: 20136
		private int ccsidUsed;

		// Token: 0x04004EA9 RID: 20137
		private bool needToEmbedCcsid;

		// Token: 0x04004EAA RID: 20138
		private int lengthOfEachPreparedString;

		// Token: 0x04004EAB RID: 20139
		private byte[] preparedBytes;

		// Token: 0x04004EAC RID: 20140
		private List<string> values = new List<string>();
	}
}
