using System;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat.Parameters
{
	// Token: 0x02000BBF RID: 3007
	public class StringParameter : CommandParameter
	{
		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x06005D91 RID: 23953 RVA: 0x0017F041 File Offset: 0x0017D241
		// (set) Token: 0x06005D92 RID: 23954 RVA: 0x0017F049 File Offset: 0x0017D249
		public string Value { get; set; }

		// Token: 0x06005D93 RID: 23955 RVA: 0x0017F052 File Offset: 0x0017D252
		public StringParameter(int parameter)
			: base(ParameterType.String, parameter)
		{
		}

		// Token: 0x06005D94 RID: 23956 RVA: 0x0017F05C File Offset: 0x0017D25C
		internal StringParameter(int parameter, int receivedLength)
			: base(ParameterType.String, parameter, receivedLength - 20)
		{
		}

		// Token: 0x06005D95 RID: 23957 RVA: 0x0017F06C File Offset: 0x0017D26C
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
				byte* ptr4 = (byte*)ptr3;
				int num3 = offset + (int)((long)(ptr4 - ptr2));
				this.Value = encoding.GetString(buffer, num3, num2);
				ptr4 += this.receivedStructureLength;
				offset += (int)((long)(ptr4 - ptr2));
			}
		}

		// Token: 0x06005D96 RID: 23958 RVA: 0x0017F0EF File Offset: 0x0017D2EF
		internal override CommandParameter GenerateCopy()
		{
			return new StringParameter(base.Parameter)
			{
				Value = this.Value
			};
		}

		// Token: 0x06005D97 RID: 23959 RVA: 0x0017F108 File Offset: 0x0017D308
		internal override int ConvertStructureToBytes(HisEncoding encoding, int ccsid, bool embedCcsid)
		{
			this.ccsidUsed = ccsid;
			this.needToEmbedCcsid = embedCcsid;
			this.preparedBytes = encoding.GetBytes(this.Value);
			return 20 + base.MultipleOf4(this.preparedBytes.Length);
		}

		// Token: 0x06005D98 RID: 23960 RVA: 0x0017F13C File Offset: 0x0017D33C
		internal unsafe override void ConvertStructureToBytes(byte[] bytes, ref int index)
		{
			fixed (byte* ptr = &bytes[index])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = (int)base.ParameterType;
				*(ptr3++) = 20 + base.MultipleOf4(this.preparedBytes.Length);
				*(ptr3++) = base.Parameter;
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

		// Token: 0x04004EAD RID: 20141
		private int ccsidUsed;

		// Token: 0x04004EAE RID: 20142
		private bool needToEmbedCcsid;

		// Token: 0x04004EAF RID: 20143
		private byte[] preparedBytes;
	}
}
