using System;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x02000505 RID: 1285
	[Serializable]
	public class CEDAR_TYPE_ENCODING
	{
		// Token: 0x06002B17 RID: 11031 RVA: 0x00094410 File Offset: 0x00092610
		public CEDAR_TYPE_ENCODING()
		{
			this.dateFormat = DateFormats.ISO;
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x0009441F File Offset: 0x0009261F
		public CEDAR_TYPE_ENCODING(int TheEncoding)
		{
			this.CvtEncoding = TheEncoding;
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x0009442E File Offset: 0x0009262E
		// (set) Token: 0x06002B1A RID: 11034 RVA: 0x00094436 File Offset: 0x00092636
		public DrdaTypes DrdaType
		{
			get
			{
				return this.DrdaTypeFromCvtEncoding();
			}
			set
			{
				this.CvtEncodingFromDrdaType(value);
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x0009443F File Offset: 0x0009263F
		// (set) Token: 0x06002B1C RID: 11036 RVA: 0x0009444D File Offset: 0x0009264D
		public short nCvtType
		{
			get
			{
				return (short)this.stCvtEncoding.nCvtType;
			}
			set
			{
				this.stCvtEncoding.nCvtType = (byte)value;
				this.encoding = (this.encoding & -256) | (int)((byte)value);
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002B1D RID: 11037 RVA: 0x00094471 File Offset: 0x00092671
		// (set) Token: 0x06002B1E RID: 11038 RVA: 0x0009447F File Offset: 0x0009267F
		public short nScale
		{
			get
			{
				return (short)this.stCvtEncoding.nScale;
			}
			set
			{
				this.stCvtEncoding.nScale = (byte)value;
				this.encoding = (this.encoding & -65281) | ((int)((byte)value) << 8);
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x000944A5 File Offset: 0x000926A5
		// (set) Token: 0x06002B20 RID: 11040 RVA: 0x000944B3 File Offset: 0x000926B3
		public short nPrecision
		{
			get
			{
				return (short)this.stCvtEncoding.nPrecision;
			}
			set
			{
				this.stCvtEncoding.nPrecision = (byte)value;
				this.encoding = (this.encoding & -16711681) | ((int)((byte)value) << 16);
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002B21 RID: 11041 RVA: 0x000944DA File Offset: 0x000926DA
		// (set) Token: 0x06002B22 RID: 11042 RVA: 0x000944E8 File Offset: 0x000926E8
		public short nPad
		{
			get
			{
				return (short)this.stCvtEncoding.nPad;
			}
			set
			{
				this.stCvtEncoding.nPad = (byte)value;
				this.encoding = (this.encoding & -16777217) | ((int)((byte)value) << 24);
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x0009450F File Offset: 0x0009270F
		// (set) Token: 0x06002B24 RID: 11044 RVA: 0x0009451D File Offset: 0x0009271D
		public short nTRE
		{
			get
			{
				return (short)this.stCvtEncoding.nTRE;
			}
			set
			{
				this.stCvtEncoding.nTRE = (byte)value;
				this.encoding = (this.encoding & -100663297) | ((int)((byte)value) << 25);
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x00094544 File Offset: 0x00092744
		// (set) Token: 0x06002B26 RID: 11046 RVA: 0x00094552 File Offset: 0x00092752
		public short nSign
		{
			get
			{
				return (short)this.stCvtEncoding.nSign;
			}
			set
			{
				this.stCvtEncoding.nSign = (byte)value;
				this.encoding = (this.encoding & -134217729) | ((int)((byte)value) << 27);
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002B27 RID: 11047 RVA: 0x00094579 File Offset: 0x00092779
		// (set) Token: 0x06002B28 RID: 11048 RVA: 0x00094587 File Offset: 0x00092787
		public short nTrailing
		{
			get
			{
				return (short)this.stCvtEncoding.nTrailing;
			}
			set
			{
				this.stCvtEncoding.nTrailing = (byte)value;
				this.encoding = (this.encoding & -268435457) | ((int)((byte)value) << 28);
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06002B29 RID: 11049 RVA: 0x000945AE File Offset: 0x000927AE
		// (set) Token: 0x06002B2A RID: 11050 RVA: 0x000945BC File Offset: 0x000927BC
		public short nOverpunch
		{
			get
			{
				return (short)this.stCvtEncoding.nOverpunch;
			}
			set
			{
				this.stCvtEncoding.nOverpunch = (byte)value;
				this.encoding = (this.encoding & -536870913) | ((int)((byte)value) << 29);
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x000945E3 File Offset: 0x000927E3
		// (set) Token: 0x06002B2C RID: 11052 RVA: 0x000945F1 File Offset: 0x000927F1
		public short nSOSI
		{
			get
			{
				return (short)this.stCvtEncoding.nSOSI;
			}
			set
			{
				this.stCvtEncoding.nSOSI = (byte)value;
				this.encoding = (this.encoding & -1073741825) | ((int)((byte)value) << 30);
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002B2D RID: 11053 RVA: 0x00094618 File Offset: 0x00092818
		// (set) Token: 0x06002B2E RID: 11054 RVA: 0x00094626 File Offset: 0x00092826
		public short nAsIs
		{
			get
			{
				return (short)this.stCvtEncoding.nAsIs;
			}
			set
			{
				this.stCvtEncoding.nAsIs = (byte)value;
				this.encoding = (this.encoding & int.MaxValue) | ((int)((byte)value) << 31);
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x0009464D File Offset: 0x0009284D
		// (set) Token: 0x06002B30 RID: 11056 RVA: 0x00094658 File Offset: 0x00092858
		public int CvtEncoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = value;
				this.stCvtEncoding.nCvtType = (byte)(value & 255);
				this.stCvtEncoding.nScale = (byte)((value & 65280) >> 8);
				this.stCvtEncoding.nPrecision = (byte)((value & 16711680) >> 16);
				this.stCvtEncoding.nPad = (byte)((value & 16777216) >> 24);
				this.stCvtEncoding.nTRE = (byte)((value & 100663296) >> 25);
				this.stCvtEncoding.nSign = (byte)((value & 134217728) >> 27);
				this.stCvtEncoding.nTrailing = (byte)((value & 268435456) >> 28);
				this.stCvtEncoding.nOverpunch = (byte)((value & 536870912) >> 29);
				this.stCvtEncoding.nSOSI = (byte)((value & 1073741824) >> 30);
				this.stCvtEncoding.nAsIs = (byte)(((long)value & (long)((ulong)int.MinValue)) >> 31);
			}
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x00094746 File Offset: 0x00092946
		public int GetEncoding(short cvttype, short scale, short precision, short pad, short tre, short sign, short trailing, short overpunch, short sosi, short embnuls)
		{
			return (int)((ushort)cvttype) | ((int)scale << 8) | ((int)precision << 16) | ((int)pad << 24) | ((int)tre << 25) | ((int)sign << 27) | ((int)trailing << 28) | ((int)overpunch << 29) | ((int)sosi << 30) | ((int)embnuls << 31);
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x00094780 File Offset: 0x00092980
		private void CvtEncodingFromDrdaType(DrdaTypes DrdaType)
		{
			switch (DrdaType)
			{
			case DrdaTypes.DRDA_TYPE_INTEGER:
				this.nCvtType = 1;
				this.nSign = 0;
				this.nTRE = 1;
				return;
			case DrdaTypes.DRDA_TYPE_NINTEGER:
				this.nCvtType = 36;
				this.nSign = 0;
				this.nTRE = 1;
				return;
			case DrdaTypes.DRDA_TYPE_SMALL:
				this.nCvtType = 0;
				this.nSign = 0;
				this.nTRE = 1;
				return;
			case DrdaTypes.DRDA_TYPE_NSMALL:
				this.nCvtType = 35;
				this.nSign = 0;
				this.nTRE = 1;
				return;
			case DrdaTypes.DRDA_TYPE_1BYTE_INT:
				this.nCvtType = 6;
				return;
			case DrdaTypes.DRDA_TYPE_N1BYTE_INT:
				this.nCvtType = 53;
				return;
			case DrdaTypes.DRDA_TYPE_FLOAT16:
			case DrdaTypes.DRDA_TYPE_NFLOAT16:
			case DrdaTypes.DRDA_TYPE_NUMERIC_CHAR:
			case DrdaTypes.DRDA_TYPE_NNUMERIC_CHAR:
			case DrdaTypes.DRDA_TYPE_RSET_LOC:
			case DrdaTypes.DRDA_TYPE_NRSET_LOC:
			case DrdaTypes.DRDA_TYPE_LOBLOC:
			case DrdaTypes.DRDA_TYPE_NLOBLOC:
			case DrdaTypes.DRDA_TYPE_CLOBLOC:
			case DrdaTypes.DRDA_TYPE_NCLOBLOC:
			case DrdaTypes.DRDA_TYPE_DBCSCLOBLOC:
			case DrdaTypes.DRDA_TYPE_NDBCSCLOBLOC:
			case DrdaTypes.DRDA_TYPE_ROWID:
			case DrdaTypes.DRDA_TYPE_NROWID:
			case DrdaTypes.DRDA_TYPE_FIXBYTE:
			case DrdaTypes.DRDA_TYPE_NFIXBYTE:
			case DrdaTypes.DRDA_TYPE_VARBYTE:
			case DrdaTypes.DRDA_TYPE_NVARBYTE:
			case DrdaTypes.DRDA_TYPE_LONGVARBYTE:
			case DrdaTypes.DRDA_TYPE_NLONGVARBYTE:
			case DrdaTypes.DRDA_TYPE_NTERMBYTE:
			case DrdaTypes.DRDA_TYPE_NNTERMBYTE:
				break;
			case DrdaTypes.DRDA_TYPE_FLOAT8:
				this.nCvtType = 75;
				this.nTRE = 2;
				return;
			case DrdaTypes.DRDA_TYPE_NFLOAT8:
				this.nCvtType = 39;
				this.nTRE = 2;
				return;
			case DrdaTypes.DRDA_TYPE_FLOAT4:
				this.nCvtType = 74;
				this.nTRE = 2;
				return;
			case DrdaTypes.DRDA_TYPE_NFLOAT4:
				this.nCvtType = 38;
				this.nTRE = 2;
				return;
			case DrdaTypes.DRDA_TYPE_DECIMAL:
				this.nCvtType = 2;
				this.nSign = 0;
				this.nTRE = 1;
				return;
			case DrdaTypes.DRDA_TYPE_NDECIMAL:
				this.nCvtType = 37;
				this.nSign = 0;
				this.nTRE = 1;
				return;
			case DrdaTypes.DRDA_TYPE_ZDECIMAL:
				this.nCvtType = 13;
				this.nSign = 0;
				this.nTrailing = 0;
				this.nOverpunch = 0;
				this.nTRE = 1;
				return;
			case DrdaTypes.DRDA_TYPE_NZDECIMAL:
				this.nCvtType = 43;
				this.nSign = 0;
				this.nTrailing = 0;
				this.nOverpunch = 0;
				this.nTRE = 1;
				return;
			case DrdaTypes.DRDA_TYPE_INTEGER8:
				this.nCvtType = 10;
				this.nSign = 0;
				this.nTRE = 1;
				return;
			case DrdaTypes.DRDA_TYPE_NINTEGER8:
				this.nCvtType = 42;
				this.nSign = 0;
				this.nTRE = 1;
				return;
			case DrdaTypes.DRDA_TYPE_DATE:
				switch (this.dateFormat)
				{
				case DateFormats.ISO:
					this.nCvtType = 15;
					return;
				case DateFormats.JIS:
					this.nCvtType = 21;
					return;
				case DateFormats.EUR:
					this.nCvtType = 24;
					return;
				}
				this.nCvtType = 18;
				return;
			case DrdaTypes.DRDA_TYPE_NDATE:
				switch (this.dateFormat)
				{
				case DateFormats.ISO:
					this.nCvtType = 45;
					return;
				case DateFormats.JIS:
					this.nCvtType = 49;
					return;
				case DateFormats.EUR:
					this.nCvtType = 51;
					return;
				}
				this.nCvtType = 47;
				return;
			case DrdaTypes.DRDA_TYPE_TIME:
				switch (this.dateFormat)
				{
				case DateFormats.ISO:
					this.nCvtType = 16;
					return;
				case DateFormats.JIS:
					this.nCvtType = 22;
					return;
				case DateFormats.EUR:
					this.nCvtType = 25;
					return;
				}
				this.nCvtType = 19;
				return;
			case DrdaTypes.DRDA_TYPE_NTIME:
				switch (this.dateFormat)
				{
				case DateFormats.ISO:
					this.nCvtType = 46;
					return;
				case DateFormats.JIS:
					this.nCvtType = 50;
					return;
				case DateFormats.EUR:
					this.nCvtType = 52;
					return;
				}
				this.nCvtType = 48;
				return;
			case DrdaTypes.DRDA_TYPE_TIMESTAMP:
				this.nCvtType = 26;
				return;
			case DrdaTypes.DRDA_TYPE_NTIMESTAMP:
				this.nCvtType = 34;
				return;
			case DrdaTypes.DRDA_TYPE_CSTR:
				this.nCvtType = 70;
				this.nPad = 1;
				return;
			case DrdaTypes.DRDA_TYPE_NCSTR:
				this.nCvtType = 71;
				this.nPad = 1;
				return;
			case DrdaTypes.DRDA_TYPE_CHAR:
				this.nCvtType = 5;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_NCHAR:
				this.nCvtType = 40;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_VARCHAR:
				this.nCvtType = 54;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_NVARCHAR:
				this.nCvtType = 55;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_LONGVARCHAR:
				this.nCvtType = 58;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_NLONGVARCHAR:
				this.nCvtType = 59;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_GRAPHIC:
				this.nCvtType = 8;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_NGRAPHIC:
				this.nCvtType = 41;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_VARGRAPHIC:
				this.nCvtType = 56;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_NVARGRAPHIC:
				this.nCvtType = 57;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_LONGGRAPHIC:
				this.nCvtType = 60;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_NLONGGRAPHIC:
				this.nCvtType = 61;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_CHARMIX:
				this.nCvtType = 62;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_NCHARMIX:
				this.nCvtType = 63;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_VARCHARMIX:
				this.nCvtType = 66;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_NVARCHARMIX:
				this.nCvtType = 67;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_LONGVARCHARMIX:
				this.nCvtType = 68;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_NLONGVARCHARMIX:
				this.nCvtType = 69;
				this.nPad = 0;
				return;
			case DrdaTypes.DRDA_TYPE_CSTRMIX:
				this.nCvtType = 72;
				this.nPad = 1;
				return;
			case DrdaTypes.DRDA_TYPE_NCSTRMIX:
				this.nCvtType = 73;
				this.nPad = 1;
				return;
			default:
				if (DrdaType == DrdaTypes.DRDA_TYPE_DECFLOAT)
				{
					this.nCvtType = 77;
					this.nSign = 0;
					this.nTRE = 1;
					return;
				}
				if (DrdaType == DrdaTypes.DRDA_TYPE_DECFLOAT_64)
				{
					this.nCvtType = 76;
					this.nSign = 0;
					this.nTRE = 1;
					return;
				}
				break;
			}
			throw new Exception("Unsupported DrdaType.");
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x00094CBC File Offset: 0x00092EBC
		private DrdaTypes DrdaTypeFromCvtEncoding()
		{
			switch (this.nCvtType)
			{
			case 0:
				return DrdaTypes.DRDA_TYPE_SMALL;
			case 1:
				return DrdaTypes.DRDA_TYPE_INTEGER;
			case 2:
				return DrdaTypes.DRDA_TYPE_DECIMAL;
			case 5:
				return DrdaTypes.DRDA_TYPE_CHAR;
			case 6:
				return DrdaTypes.DRDA_TYPE_1BYTE_INT;
			case 8:
				return DrdaTypes.DRDA_TYPE_GRAPHIC;
			case 10:
				return DrdaTypes.DRDA_TYPE_INTEGER8;
			case 13:
				return DrdaTypes.DRDA_TYPE_ZDECIMAL;
			case 15:
				return DrdaTypes.DRDA_TYPE_DATE;
			case 16:
				return DrdaTypes.DRDA_TYPE_TIME;
			case 18:
				return DrdaTypes.DRDA_TYPE_DATE;
			case 19:
				return DrdaTypes.DRDA_TYPE_TIME;
			case 21:
				return DrdaTypes.DRDA_TYPE_DATE;
			case 22:
				return DrdaTypes.DRDA_TYPE_TIME;
			case 24:
				return DrdaTypes.DRDA_TYPE_DATE;
			case 25:
				return DrdaTypes.DRDA_TYPE_TIME;
			case 26:
				return DrdaTypes.DRDA_TYPE_TIMESTAMP;
			case 34:
				return DrdaTypes.DRDA_TYPE_NTIMESTAMP;
			case 35:
				return DrdaTypes.DRDA_TYPE_NSMALL;
			case 36:
				return DrdaTypes.DRDA_TYPE_NINTEGER;
			case 37:
				return DrdaTypes.DRDA_TYPE_NDECIMAL;
			case 38:
				return DrdaTypes.DRDA_TYPE_NFLOAT4;
			case 39:
				return DrdaTypes.DRDA_TYPE_NFLOAT8;
			case 40:
				return DrdaTypes.DRDA_TYPE_NCHAR;
			case 41:
				return DrdaTypes.DRDA_TYPE_NGRAPHIC;
			case 42:
				return DrdaTypes.DRDA_TYPE_NINTEGER8;
			case 43:
				return DrdaTypes.DRDA_TYPE_NZDECIMAL;
			case 45:
				return DrdaTypes.DRDA_TYPE_NDATE;
			case 46:
				return DrdaTypes.DRDA_TYPE_NTIME;
			case 47:
				return DrdaTypes.DRDA_TYPE_NDATE;
			case 48:
				return DrdaTypes.DRDA_TYPE_NTIME;
			case 49:
				return DrdaTypes.DRDA_TYPE_NDATE;
			case 50:
				return DrdaTypes.DRDA_TYPE_NTIME;
			case 51:
				return DrdaTypes.DRDA_TYPE_NDATE;
			case 52:
				return DrdaTypes.DRDA_TYPE_NTIME;
			case 53:
				return DrdaTypes.DRDA_TYPE_N1BYTE_INT;
			case 54:
				return DrdaTypes.DRDA_TYPE_VARCHAR;
			case 55:
				return DrdaTypes.DRDA_TYPE_NVARCHAR;
			case 56:
				return DrdaTypes.DRDA_TYPE_VARGRAPHIC;
			case 57:
				return DrdaTypes.DRDA_TYPE_NVARGRAPHIC;
			case 58:
				return DrdaTypes.DRDA_TYPE_LONGVARCHAR;
			case 59:
				return DrdaTypes.DRDA_TYPE_NLONGVARCHAR;
			case 60:
				return DrdaTypes.DRDA_TYPE_LONGGRAPHIC;
			case 61:
				return DrdaTypes.DRDA_TYPE_NLONGGRAPHIC;
			case 62:
				return DrdaTypes.DRDA_TYPE_CHARMIX;
			case 63:
				return DrdaTypes.DRDA_TYPE_NCHARMIX;
			case 66:
				return DrdaTypes.DRDA_TYPE_VARCHARMIX;
			case 67:
				return DrdaTypes.DRDA_TYPE_NVARCHARMIX;
			case 68:
				return DrdaTypes.DRDA_TYPE_LONGVARCHARMIX;
			case 69:
				return DrdaTypes.DRDA_TYPE_NLONGVARCHARMIX;
			case 70:
				return DrdaTypes.DRDA_TYPE_CSTR;
			case 71:
				return DrdaTypes.DRDA_TYPE_NCSTR;
			case 72:
				return DrdaTypes.DRDA_TYPE_CSTRMIX;
			case 73:
				return DrdaTypes.DRDA_TYPE_NCSTRMIX;
			case 74:
				return DrdaTypes.DRDA_TYPE_FLOAT4;
			case 75:
				return DrdaTypes.DRDA_TYPE_FLOAT8;
			case 76:
				return DrdaTypes.DRDA_TYPE_DECFLOAT_64;
			case 77:
				return DrdaTypes.DRDA_TYPE_DECFLOAT;
			}
			throw new Exception("DrdaType no mapping to a DrdaType exists.");
		}

		// Token: 0x04001B68 RID: 7016
		public CVTEncodingValue stCvtEncoding;

		// Token: 0x04001B69 RID: 7017
		private int encoding;

		// Token: 0x04001B6A RID: 7018
		private DateFormats dateFormat;

		// Token: 0x04001B6B RID: 7019
		public static CEDAR_TYPE_ENCODING EmptyEncoding = new CEDAR_TYPE_ENCODING(0);

		// Token: 0x04001B6C RID: 7020
		public static CEDAR_TYPE_ENCODING EmptyReportAsError = new CEDAR_TYPE_ENCODING(33554432);

		// Token: 0x04001B6D RID: 7021
		public static CEDAR_TYPE_ENCODING DefaultEncodingI2 = new CEDAR_TYPE_ENCODING(50593792);

		// Token: 0x04001B6E RID: 7022
		public static CEDAR_TYPE_ENCODING DefaultEncodingI4 = new CEDAR_TYPE_ENCODING(50921473);

		// Token: 0x04001B6F RID: 7023
		public static CEDAR_TYPE_ENCODING DefaultEncodingBstr = new CEDAR_TYPE_ENCODING(167772165);

		// Token: 0x04001B70 RID: 7024
		public static CEDAR_TYPE_ENCODING TraceCvtEncodingBstr = new CEDAR_TYPE_ENCODING(184549381);

		// Token: 0x04001B71 RID: 7025
		public static CEDAR_TYPE_ENCODING NotPaddedDefaultEncodingBstr = new CEDAR_TYPE_ENCODING(184549381);

		// Token: 0x04001B72 RID: 7026
		public static CEDAR_TYPE_ENCODING AsIsNotPaddedDefaultEncodingBstr = new CEDAR_TYPE_ENCODING(-1962934267);
	}
}
