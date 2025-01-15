using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000039 RID: 57
	internal class TransportCapabilities
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x00011E3B File Offset: 0x0001003B
		public TransportCapabilities()
		{
			this.m_Data = new BitArray(32, false);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00011E51 File Offset: 0x00010051
		private TransportCapabilities(BitArray bitArray)
		{
			this.m_Data = new BitArray(bitArray);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00011E65 File Offset: 0x00010065
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00011E73 File Offset: 0x00010073
		public bool ContentTypeNegotiated
		{
			get
			{
				return this.m_Data[0];
			}
			set
			{
				this.m_Data[0] = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00011E82 File Offset: 0x00010082
		public bool RequestBinary
		{
			get
			{
				return this.m_Data[1];
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00011E90 File Offset: 0x00010090
		public bool RequestCompression
		{
			get
			{
				return this.m_Data[2];
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00011E9E File Offset: 0x0001009E
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00011EAC File Offset: 0x000100AC
		public bool ResponseBinary
		{
			get
			{
				return this.m_Data[3];
			}
			set
			{
				this.m_Data[3] = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00011EBB File Offset: 0x000100BB
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00011EC9 File Offset: 0x000100C9
		public bool ResponseCompression
		{
			get
			{
				return this.m_Data[4];
			}
			set
			{
				this.m_Data[4] = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00011ED8 File Offset: 0x000100D8
		public XmlaDataType RequestType
		{
			get
			{
				if (!this.ContentTypeNegotiated)
				{
					return XmlaDataType.TextXml;
				}
				if (this.RequestBinary && this.RequestCompression)
				{
					return XmlaDataType.CompressedBinaryXml;
				}
				if (this.RequestBinary)
				{
					return XmlaDataType.BinaryXml;
				}
				if (this.RequestCompression)
				{
					return XmlaDataType.CompressedXml;
				}
				return XmlaDataType.TextXml;
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00011F0B File Offset: 0x0001010B
		public TransportCapabilities Clone()
		{
			return new TransportCapabilities(this.m_Data);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00011F18 File Offset: 0x00010118
		public void FromBytes(byte[] bytes)
		{
			int i = 0;
			int num = 0;
			int num2 = 0;
			while (i < 32)
			{
				this.m_Data[i] = ((int)bytes[num] & (1 << num2)) != 0;
				if (num2 == 7)
				{
					num++;
					num2 = 0;
				}
				else
				{
					num2++;
				}
				i++;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00011F60 File Offset: 0x00010160
		public byte[] GetBytes()
		{
			byte[] array = new byte[4];
			Array.Clear(array, 0, 4);
			int i = 0;
			int num = 0;
			int num2 = 0;
			while (i < 32)
			{
				byte[] array2 = array;
				int num3 = num;
				array2[num3] |= (byte)(this.m_Data[i] ? (1 << num2) : 0);
				if (num2 == 7)
				{
					num++;
					num2 = 0;
				}
				else
				{
					num2++;
				}
				i++;
			}
			return array;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00011FC4 File Offset: 0x000101C4
		public void FromString(string optionsStr)
		{
			if (string.IsNullOrEmpty(optionsStr))
			{
				this.m_Data.SetAll(false);
				return;
			}
			string[] array = optionsStr.Split(new char[] { ',' });
			if (array.Length == 0)
			{
				this.m_Data.SetAll(false);
				return;
			}
			if (array.Length < 5)
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, "");
			}
			for (int i = 0; i < array.Length; i++)
			{
				int num;
				try
				{
					num = int.Parse(array[i], CultureInfo.InvariantCulture);
				}
				catch (Exception ex)
				{
					throw new AdomdUnknownResponseException(ex);
				}
				if (num == 0)
				{
					this.m_Data[i] = false;
				}
				else
				{
					if (num != 1)
					{
						throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, "");
					}
					this.m_Data[i] = true;
				}
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0001208C File Offset: 0x0001028C
		public string GetString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.m_Data[0] ? "1" : "0");
			for (int i = 1; i < 5; i++)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(this.m_Data[i] ? "1" : "0");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000234 RID: 564
		public const int TotalSize = 4;

		// Token: 0x04000235 RID: 565
		private const int numberOptions = 5;

		// Token: 0x04000236 RID: 566
		private BitArray m_Data;
	}
}
