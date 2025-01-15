using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000039 RID: 57
	internal class TransportCapabilities
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x0001216B File Offset: 0x0001036B
		public TransportCapabilities()
		{
			this.m_Data = new BitArray(32, false);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00012181 File Offset: 0x00010381
		private TransportCapabilities(BitArray bitArray)
		{
			this.m_Data = new BitArray(bitArray);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00012195 File Offset: 0x00010395
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x000121A3 File Offset: 0x000103A3
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002FA RID: 762 RVA: 0x000121B2 File Offset: 0x000103B2
		public bool RequestBinary
		{
			get
			{
				return this.m_Data[1];
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000121C0 File Offset: 0x000103C0
		public bool RequestCompression
		{
			get
			{
				return this.m_Data[2];
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000121CE File Offset: 0x000103CE
		// (set) Token: 0x060002FD RID: 765 RVA: 0x000121DC File Offset: 0x000103DC
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000121EB File Offset: 0x000103EB
		// (set) Token: 0x060002FF RID: 767 RVA: 0x000121F9 File Offset: 0x000103F9
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00012208 File Offset: 0x00010408
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

		// Token: 0x06000301 RID: 769 RVA: 0x0001223B File Offset: 0x0001043B
		public TransportCapabilities Clone()
		{
			return new TransportCapabilities(this.m_Data);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00012248 File Offset: 0x00010448
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

		// Token: 0x06000303 RID: 771 RVA: 0x00012290 File Offset: 0x00010490
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

		// Token: 0x06000304 RID: 772 RVA: 0x000122F4 File Offset: 0x000104F4
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

		// Token: 0x06000305 RID: 773 RVA: 0x000123BC File Offset: 0x000105BC
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

		// Token: 0x04000241 RID: 577
		public const int TotalSize = 4;

		// Token: 0x04000242 RID: 578
		private const int numberOptions = 5;

		// Token: 0x04000243 RID: 579
		private BitArray m_Data;
	}
}
