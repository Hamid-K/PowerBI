using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B34 RID: 2868
	public class RulesAndFormattingHeader : MqStructuredHeader
	{
		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x06005A9B RID: 23195 RVA: 0x001753D5 File Offset: 0x001735D5
		// (set) Token: 0x06005A9C RID: 23196 RVA: 0x001753DD File Offset: 0x001735DD
		public RulesAndFormattingFlag Flags { get; set; }

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x06005A9D RID: 23197 RVA: 0x001753E6 File Offset: 0x001735E6
		public Dictionary<string, string> NamesToValues
		{
			get
			{
				return this.namesToValues;
			}
		}

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x06005A9E RID: 23198 RVA: 0x001753EE File Offset: 0x001735EE
		public override int AsciiStructId
		{
			get
			{
				return 541607506;
			}
		}

		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x06005A9F RID: 23199 RVA: 0x001753F5 File Offset: 0x001735F5
		public override int EbcdicStructId
		{
			get
			{
				return 1086899929;
			}
		}

		// Token: 0x06005AA0 RID: 23200 RVA: 0x001753FC File Offset: 0x001735FC
		public RulesAndFormattingHeader()
			: base(MqHeaderType.RulesAndFormatting, OrderedMqHeaderType.RulesAndFormatting, "Rules and Formatting Header", "MQHRF", 32)
		{
			this.Flags = RulesAndFormattingFlag.None;
		}

		// Token: 0x06005AA1 RID: 23201 RVA: 0x00175424 File Offset: 0x00173624
		internal unsafe override void GenerateBytes(byte[] buffer, int offset, string format, bool littleEndian, int ccsid, int numericEncodingValue, int ccsidValue)
		{
			HisEncoding encoding = HisEncoding.GetEncoding(ccsid);
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = 541607506;
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
				Buffer.BlockCopy(this.preparedBytes, 0, buffer, num, this.preparedBytes.Length);
			}
		}

		// Token: 0x06005AA2 RID: 23202 RVA: 0x001754CC File Offset: 0x001736CC
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
					int num2 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
					int num3 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
					byte* ptr4 = (byte*)ptr3;
					int num4 = offset + (int)((long)(ptr4 - ptr2));
					nextFormat = ConversionHelpers.GetStringOrNull(buffer, num4, 8, encoding);
					ptr4 += 8;
					ptr3 = (int*)ptr4;
					this.Flags = (RulesAndFormattingFlag)ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
					ptr4 = (byte*)ptr3;
					num4 = offset + (int)((long)(ptr4 - ptr2));
					this.consumedBytesLength = num - 32;
					string stringOrNull = ConversionHelpers.GetStringOrNull(buffer, num4, this.consumedBytesLength, encoding);
					if (stringOrNull != null)
					{
						this.ExtractNameValuePairs(stringOrNull);
					}
					ptr = null;
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

		// Token: 0x06005AA3 RID: 23203 RVA: 0x001755C0 File Offset: 0x001737C0
		internal override MqHeader GenerateCopy(bool deepCopy)
		{
			if (!deepCopy)
			{
				return this;
			}
			RulesAndFormattingHeader rulesAndFormattingHeader = new RulesAndFormattingHeader();
			rulesAndFormattingHeader.Flags = this.Flags;
			foreach (KeyValuePair<string, string> keyValuePair in this.NamesToValues)
			{
				rulesAndFormattingHeader.NamesToValues.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return rulesAndFormattingHeader;
		}

		// Token: 0x06005AA4 RID: 23204 RVA: 0x00175640 File Offset: 0x00173840
		protected override byte[] ConvertStructureToBytes()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			bool flag = true;
			foreach (KeyValuePair<string, string> keyValuePair in this.NamesToValues)
			{
				string text = this.RfEscape(keyValuePair.Key);
				string text2 = this.RfEscape(keyValuePair.Value);
				stringBuilder.AppendFormat(flag ? "{0} {1}" : " {0} {1}", text, text2);
				flag = false;
			}
			string text3 = stringBuilder.ToString();
			byte[] bytes = HisEncoding.GetEncoding(1252).GetBytes(text3);
			int num = bytes.Length % 4;
			byte[] array;
			if (num != 0)
			{
				int num2 = 4 - num;
				array = new byte[bytes.Length + num2];
				Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
			}
			else
			{
				array = bytes;
			}
			return array;
		}

		// Token: 0x06005AA5 RID: 23205 RVA: 0x00175724 File Offset: 0x00173924
		private string RfEscape(string text)
		{
			string text2 = text.Replace("\"", "\"\"");
			bool flag = text2.Length != text.Length;
			if (!flag)
			{
				flag = text2.IndexOf(' ') != -1;
			}
			if (!flag)
			{
				return text;
			}
			return "\"" + text2 + "\"";
		}

		// Token: 0x06005AA6 RID: 23206 RVA: 0x0017577C File Offset: 0x0017397C
		private void ExtractNameValuePairs(string nameValueString)
		{
			int i = 0;
			while (i < nameValueString.Length)
			{
				string text = this.ExtractText(nameValueString, ref i);
				if (text == null)
				{
					break;
				}
				string text2 = this.ExtractText(nameValueString, ref i);
				if (text2 == null)
				{
					throw new CustomMqClientException(SR.RulesAndFormattingHeaderValue);
				}
				this.NamesToValues.Add(text, text2);
			}
		}

		// Token: 0x06005AA7 RID: 23207 RVA: 0x001757C8 File Offset: 0x001739C8
		private string ExtractText(string escapedStrings, ref int index)
		{
			while (index < escapedStrings.Length && escapedStrings[index] == ' ')
			{
				index++;
			}
			if (index == escapedStrings.Length)
			{
				return null;
			}
			if (escapedStrings[index] == '\0')
			{
				return null;
			}
			if (escapedStrings[index] == '"')
			{
				int num = index;
				int num2 = -1;
				index++;
				bool flag = false;
				while (!flag)
				{
					while (index < escapedStrings.Length && escapedStrings[index] != '"' && escapedStrings[index] != '\0')
					{
						index++;
					}
					if (index == escapedStrings.Length || escapedStrings[index] == '\0')
					{
						break;
					}
					if (index + 1 == escapedStrings.Length)
					{
						num2 = index;
						flag = true;
					}
					else if (escapedStrings[index + 1] == ' ' || escapedStrings[index + 1] == '\0')
					{
						num2 = index;
						flag = true;
					}
					else
					{
						if (escapedStrings[index + 1] != '"')
						{
							throw new CustomMqClientException(SR.RulesAndFormattingHeaderSingleEndInvalid);
						}
						index += 2;
					}
				}
				if (!flag)
				{
					throw new CustomMqClientException(SR.RulesAndFormattingHeaderQuotationMarks);
				}
				string text = escapedStrings.Substring(num + 1, num2 - num - 1);
				index++;
				return text.Replace("\"\"", "\"");
			}
			else
			{
				int num3 = index;
				index++;
				while (index < escapedStrings.Length && escapedStrings[index] != ' ' && escapedStrings[index] != '"')
				{
					index++;
				}
				if (index == escapedStrings.Length)
				{
					return escapedStrings.Substring(num3);
				}
				if (escapedStrings[index] != ' ' && escapedStrings[index] != '\0')
				{
					throw new CustomMqClientException(SR.RulesAndFormattingHeaderUnescapedQuotationMarks);
				}
				int num4 = index;
				return escapedStrings.Substring(num3, num4 - num3);
			}
		}

		// Token: 0x04004774 RID: 18292
		private Dictionary<string, string> namesToValues = new Dictionary<string, string>();
	}
}
