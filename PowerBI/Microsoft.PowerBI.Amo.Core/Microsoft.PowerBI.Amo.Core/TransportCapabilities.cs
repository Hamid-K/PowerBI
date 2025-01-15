using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000055 RID: 85
	internal class TransportCapabilities
	{
		// Token: 0x06000396 RID: 918 RVA: 0x00015003 File Offset: 0x00013203
		public TransportCapabilities()
		{
			this.m_Data = new BitArray(32, false);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00015019 File Offset: 0x00013219
		private TransportCapabilities(BitArray bitArray)
		{
			this.m_Data = new BitArray(bitArray);
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0001502D File Offset: 0x0001322D
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0001503B File Offset: 0x0001323B
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0001504A File Offset: 0x0001324A
		public bool RequestBinary
		{
			get
			{
				return this.m_Data[1];
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00015058 File Offset: 0x00013258
		public bool RequestCompression
		{
			get
			{
				return this.m_Data[2];
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00015066 File Offset: 0x00013266
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00015074 File Offset: 0x00013274
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00015083 File Offset: 0x00013283
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00015091 File Offset: 0x00013291
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

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000150A0 File Offset: 0x000132A0
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

		// Token: 0x060003A1 RID: 929 RVA: 0x000150D3 File Offset: 0x000132D3
		public TransportCapabilities Clone()
		{
			return new TransportCapabilities(this.m_Data);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000150E0 File Offset: 0x000132E0
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

		// Token: 0x060003A3 RID: 931 RVA: 0x00015128 File Offset: 0x00013328
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

		// Token: 0x060003A4 RID: 932 RVA: 0x0001518C File Offset: 0x0001338C
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
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, "");
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
					throw new ResponseFormatException(ex);
				}
				if (num == 0)
				{
					this.m_Data[i] = false;
				}
				else
				{
					if (num != 1)
					{
						throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, "");
					}
					this.m_Data[i] = true;
				}
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00015254 File Offset: 0x00013454
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

		// Token: 0x04000285 RID: 645
		public const int TotalSize = 4;

		// Token: 0x04000286 RID: 646
		private const int numberOptions = 5;

		// Token: 0x04000287 RID: 647
		private BitArray m_Data;
	}
}
