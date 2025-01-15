using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.HostIntegration.Nls;
using Microsoft.HostIntegration.StrictResources.BasePrimitiveConverter;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.Common;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020004CC RID: 1228
	public class BasePrimitiveConverter : IPrimitiveConverter
	{
		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060029EA RID: 10730 RVA: 0x0007E46E File Offset: 0x0007C66E
		// (set) Token: 0x060029EB RID: 10731 RVA: 0x0007E476 File Offset: 0x0007C676
		public virtual PrimitiveConverterTracePoint TracePoint
		{
			get
			{
				return this.tracePoint;
			}
			set
			{
				this.tracePoint = value;
				this.tracePoint[PrimitiveConverterPropertyIdentifiers.PrimitiveConverterType] = PrimitiveConverterTypes.SystemZ;
			}
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x0007E494 File Offset: 0x0007C694
		static BasePrimitiveConverter()
		{
			BasePrimitiveConverter.EditPatternCharacters['y'] = true;
			BasePrimitiveConverter.EditPatternCharacters['M'] = true;
			BasePrimitiveConverter.EditPatternCharacters['d'] = true;
			BasePrimitiveConverter.EditPatternCharacters['h'] = true;
			BasePrimitiveConverter.EditPatternCharacters['H'] = true;
			BasePrimitiveConverter.EditPatternCharacters['m'] = true;
			BasePrimitiveConverter.EditPatternCharacters['s'] = true;
			BasePrimitiveConverter.EditPatternCharacters['t'] = true;
			BasePrimitiveConverter.EditPatternCharacters['f'] = true;
			BasePrimitiveConverter.EditPatternCharacters['-'] = true;
			BasePrimitiveConverter.EditPatternCharacters['.'] = true;
			BasePrimitiveConverter.EditPatternCharacters[','] = true;
			BasePrimitiveConverter.EditPatternCharacters[':'] = true;
			BasePrimitiveConverter.EditPatternCharacters[' '] = true;
			BasePrimitiveConverter.EditPatternCharacters['?'] = true;
			BasePrimitiveConverter.EditPatternCharacters['/'] = true;
			BasePrimitiveConverter.EditPatternCharacters['A'] = true;
			BasePrimitiveConverter.FourDigitNumbers = new FourDigits[10000];
			for (int i = 0; i < 10000; i++)
			{
				string text = i.ToString("0000");
				FourDigits fourDigits = new FourDigits();
				fourDigits.First = text[0];
				fourDigits.Second = text[1];
				fourDigits.Third = text[2];
				fourDigits.Fourth = text[3];
				BasePrimitiveConverter.FourDigitNumbers[i] = fourDigits;
			}
			BasePrimitiveConverter.GenerateDecimalFloatTables();
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x0007E7C9 File Offset: 0x0007C9C9
		public BasePrimitiveConverter()
		{
			this.GetRegistryOverrides();
			this.HisLogging = null;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x0007E808 File Offset: 0x0007CA08
		public BasePrimitiveConverter(int CodePage)
		{
			this.SetCodePage(CodePage);
			this.SetSepValues(':', '/');
			this.SetLCID(-1);
			this.GetRegistryOverrides();
			this.HisLogging = null;
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x0007E868 File Offset: 0x0007CA68
		public BasePrimitiveConverter(Hashtable behaviors)
		{
			BasePrimitiveConverter.SpecialOverrides = behaviors;
			this.GetOverrides();
			this.HisLogging = null;
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x0007E8B8 File Offset: 0x0007CAB8
		protected override void Finalize()
		{
			try
			{
				if (this.CPInfos.Count != 0)
				{
					Dictionary<int, List<BasePrimitiveConverter.CodePageInfo>> cpinfoHashTable = BasePrimitiveConverter.CPInfoHashTable;
					lock (cpinfoHashTable)
					{
						foreach (BasePrimitiveConverter.CodePageInfo codePageInfo in this.CPInfos.Values)
						{
							if (BasePrimitiveConverter.CPInfoHashTable.ContainsKey(codePageInfo.codePage))
							{
								BasePrimitiveConverter.CPInfoHashTable[codePageInfo.codePage].Add(codePageInfo);
							}
						}
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x060029F1 RID: 10737 RVA: 0x0007E97C File Offset: 0x0007CB7C
		// (set) Token: 0x060029F2 RID: 10738 RVA: 0x0007E984 File Offset: 0x0007CB84
		public bool UserCompatibleErrorCode { get; set; }

		// Token: 0x060029F3 RID: 10739 RVA: 0x0007E990 File Offset: 0x0007CB90
		public unsafe static short DefaultShortConvert(byte[] BytesToSwap, int SwapBytesIndex)
		{
			BasePrimitiveConverter.Int16Union int16Union;
			int16Union.int16Val = 0;
			*((ref int16Union.byteVal.FixedElementField) + 1) = BytesToSwap[SwapBytesIndex];
			int16Union.byteVal.FixedElementField = BytesToSwap[SwapBytesIndex + 1];
			return int16Union.int16Val;
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x0007E9D0 File Offset: 0x0007CBD0
		public unsafe static short DefaultShortConvert(short SwapValue)
		{
			BasePrimitiveConverter.Int16Union int16Union;
			int16Union.int16Val = SwapValue;
			BasePrimitiveConverter.Int16Union int16Union2;
			int16Union2.int16Val = 0;
			int16Union2.byteVal.FixedElementField = *((ref int16Union.byteVal.FixedElementField) + 1);
			*((ref int16Union2.byteVal.FixedElementField) + 1) = int16Union.byteVal.FixedElementField;
			return int16Union2.int16Val;
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x0007EA2C File Offset: 0x0007CC2C
		public unsafe static int DefaultIntConvert(byte[] BytesToSwap, int SwapBytesIndex)
		{
			BasePrimitiveConverter.Int32Union int32Union;
			int32Union.int32Val = 0;
			*((ref int32Union.byteVal.FixedElementField) + 3) = BytesToSwap[SwapBytesIndex];
			*((ref int32Union.byteVal.FixedElementField) + 2) = BytesToSwap[SwapBytesIndex + 1];
			*((ref int32Union.byteVal.FixedElementField) + 1) = BytesToSwap[SwapBytesIndex + 2];
			int32Union.byteVal.FixedElementField = BytesToSwap[SwapBytesIndex + 3];
			return int32Union.int32Val;
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x0007EA94 File Offset: 0x0007CC94
		public unsafe static int DefaultIntConvert(int SwapValue)
		{
			BasePrimitiveConverter.Int32Union int32Union;
			int32Union.int32Val = SwapValue;
			BasePrimitiveConverter.Int32Union int32Union2;
			int32Union2.int32Val = 0;
			int32Union2.byteVal.FixedElementField = *((ref int32Union.byteVal.FixedElementField) + 3);
			*((ref int32Union2.byteVal.FixedElementField) + 1) = *((ref int32Union.byteVal.FixedElementField) + 2);
			*((ref int32Union2.byteVal.FixedElementField) + 2) = *((ref int32Union.byteVal.FixedElementField) + 1);
			*((ref int32Union2.byteVal.FixedElementField) + 3) = int32Union.byteVal.FixedElementField;
			return int32Union2.int32Val;
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x0007EB2C File Offset: 0x0007CD2C
		public unsafe static long DefaultLongConvert(byte[] BytesToSwap, int SwapBytesIndex)
		{
			BasePrimitiveConverter.Int64Union int64Union;
			int64Union.int64Val = 0L;
			*((ref int64Union.byteVal.FixedElementField) + 7) = BytesToSwap[SwapBytesIndex];
			*((ref int64Union.byteVal.FixedElementField) + 6) = BytesToSwap[SwapBytesIndex + 1];
			*((ref int64Union.byteVal.FixedElementField) + 5) = BytesToSwap[SwapBytesIndex + 2];
			*((ref int64Union.byteVal.FixedElementField) + 4) = BytesToSwap[SwapBytesIndex + 3];
			*((ref int64Union.byteVal.FixedElementField) + 3) = BytesToSwap[SwapBytesIndex + 4];
			*((ref int64Union.byteVal.FixedElementField) + 2) = BytesToSwap[SwapBytesIndex + 5];
			*((ref int64Union.byteVal.FixedElementField) + 1) = BytesToSwap[SwapBytesIndex + 6];
			int64Union.byteVal.FixedElementField = BytesToSwap[SwapBytesIndex + 7];
			return int64Union.int64Val;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x0007EBE4 File Offset: 0x0007CDE4
		public unsafe static long DefaultLongConvert(long SwapValue)
		{
			BasePrimitiveConverter.Int64Union int64Union;
			int64Union.int64Val = SwapValue;
			BasePrimitiveConverter.Int64Union int64Union2;
			int64Union2.int64Val = 0L;
			int64Union2.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
			*((ref int64Union2.byteVal.FixedElementField) + 1) = *((ref int64Union.byteVal.FixedElementField) + 6);
			*((ref int64Union2.byteVal.FixedElementField) + 2) = *((ref int64Union.byteVal.FixedElementField) + 5);
			*((ref int64Union2.byteVal.FixedElementField) + 3) = *((ref int64Union.byteVal.FixedElementField) + 4);
			*((ref int64Union2.byteVal.FixedElementField) + 4) = *((ref int64Union.byteVal.FixedElementField) + 3);
			*((ref int64Union2.byteVal.FixedElementField) + 5) = *((ref int64Union.byteVal.FixedElementField) + 2);
			*((ref int64Union2.byteVal.FixedElementField) + 6) = *((ref int64Union.byteVal.FixedElementField) + 1);
			*((ref int64Union2.byteVal.FixedElementField) + 7) = int64Union.byteVal.FixedElementField;
			return int64Union2.int64Val;
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x0007ECF4 File Offset: 0x0007CEF4
		private object GetOverrideValue(string OverrideName)
		{
			object obj = null;
			if (BasePrimitiveConverter.SpecialOverrides != null)
			{
				obj = BasePrimitiveConverter.SpecialOverrides[OverrideName];
			}
			return obj;
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x0007ED18 File Offset: 0x0007CF18
		private void GetOverrides()
		{
			if (this.GetOverrideValue("AcceptNullZoned") != null)
			{
				this.acceptNullZoned = true;
			}
			if (this.GetOverrideValue("AcceptNullPacked") != null)
			{
				this.acceptNullPacked = true;
			}
			if (this.GetOverrideValue("AcceptBadCOMP3Sign") != null)
			{
				this.acceptBadPacked = true;
			}
			if (this.GetOverrideValue("AcceptAllInvalidNumerics") != null)
			{
				this.acceptAllInvalidNumerics = true;
			}
			if (this.GetOverrideValue("StringsAreNullTerminatedAndSpacePadded") != null)
			{
				this.stringsAreNullTerminatedAndSpacePadded = true;
			}
			if (this.GetOverrideValue("TrimTrailingNulls") != null)
			{
				this.trimTrailingNulls = true;
			}
			if (this.GetOverrideValue("ConvertReceivedStringsAsIs") != null)
			{
				this.convertReceivedStringsAsIs = true;
			}
			if (this.GetOverrideValue("AlwaysCheckForNull") != null)
			{
				this.alwaysUseNullTerminate = true;
			}
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x0007EDC5 File Offset: 0x0007CFC5
		private void GetRegistryOverrides()
		{
			BasePrimitiveConverter.SpecialOverrides = Globals.GetSpecialOverrides();
			this.GetOverrides();
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x0007EDD7 File Offset: 0x0007CFD7
		public virtual void SetTracingAndLogging(object Logging)
		{
			this.HisLogging = (CommonHISEventLogging)Logging;
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x0007EDE5 File Offset: 0x0007CFE5
		public virtual void SetCodePage(int CodePage)
		{
			this.SetCodePage(CodePage, false, "$");
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x0007EDF4 File Offset: 0x0007CFF4
		public unsafe virtual void SetCodePage(int CodePage, bool IsPeriodComma, string Currency)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			if (CodePage < 0)
			{
				throw new CustomHISException(SR.InvalidCodepage(CodePage));
			}
			BasePrimitiveConverter.isCodePageSet = true;
			int num3 = CodePage;
			if (this.codePageInfo != null && this.codePageInfo.codePage == CodePage && this.codePageInfo.unicodeCurrency == Currency && this.codePageInfo.isPeriodComma == IsPeriodComma && this.codePageInfo.dontUsePresentationFormsB == this.dontUsePresentationFormsBForArabicStrings)
			{
				return;
			}
			bool flag2 = false;
			if (this.CPInfos.ContainsKey(CodePage))
			{
				flag2 = true;
				BasePrimitiveConverter.CodePageInfo codePageInfo = this.CPInfos[CodePage];
				if (codePageInfo.unicodeCurrency == Currency && codePageInfo.isPeriodComma == IsPeriodComma && codePageInfo.dontUsePresentationFormsB == this.dontUsePresentationFormsBForArabicStrings)
				{
					this.codePageInfo = codePageInfo;
					return;
				}
			}
			Dictionary<int, List<BasePrimitiveConverter.CodePageInfo>> cpinfoHashTable = BasePrimitiveConverter.CPInfoHashTable;
			lock (cpinfoHashTable)
			{
				if (!BasePrimitiveConverter.CPInfoHashTable.ContainsKey(CodePage))
				{
					BasePrimitiveConverter.CPInfoHashTable.Add(CodePage, new List<BasePrimitiveConverter.CodePageInfo>());
				}
				List<BasePrimitiveConverter.CodePageInfo> list = BasePrimitiveConverter.CPInfoHashTable[CodePage];
				if (list.Count != 0)
				{
					int num4 = list.Count - 1;
					BasePrimitiveConverter.CodePageInfo codePageInfo2 = list[num4];
					if (codePageInfo2.unicodeCurrency == Currency && codePageInfo2.isPeriodComma == IsPeriodComma && codePageInfo2.dontUsePresentationFormsB == this.dontUsePresentationFormsBForArabicStrings)
					{
						num3 = CodePage;
						list.RemoveAt(num4);
						this.codePageInfo = codePageInfo2;
						if (flag2)
						{
							this.CPInfos[CodePage] = this.codePageInfo;
							return;
						}
						this.CPInfos.Add(CodePage, this.codePageInfo);
						return;
					}
					else
					{
						list.Clear();
					}
				}
			}
			this.codePageInfo = new BasePrimitiveConverter.CodePageInfo();
			try
			{
				this.codePageInfo.EncodingIBM = HisEncoding.GetEncoding(num3);
				this.codePageInfo.dontUsePresentationFormsB = this.dontUsePresentationFormsBForArabicStrings;
				this.codePageInfo.EncodingIBM.DontUsePresentationFormsB = this.dontUsePresentationFormsBForArabicStrings;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.InvalidCodepage(num3), ex);
			}
			bool flag4 = !this.codePageInfo.EncodingIBM.IsSingleByte;
			this.codePageInfo.codePage = CodePage;
			this.codePageInfo.isDBCS = flag4;
			this.codePageInfo.isPeriodComma = IsPeriodComma;
			char[] array = new char[]
			{
				',', '.', 'B', 'b', 'P', 'p', 'V', 'v', 'Z', 'z',
				'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
				'/', '-', 'C', 'c', 'R', 'r', 'D', 'd', '*', 'E',
				'e', '+', ' ', ' ', ' ', '\0', '\0'
			};
			if (IsPeriodComma)
			{
				char c = array[0];
				array[0] = array[1];
				array[1] = c;
			}
			this.codePageInfo.DecoderIBM = this.codePageInfo.EncodingIBM.GetDecoder();
			this.codePageInfo.EncoderIBM = this.codePageInfo.EncodingIBM.GetEncoder();
			fixed (char* ptr = &array[0])
			{
				char* ptr2 = ptr;
				fixed (byte* ptr3 = &this.codePageInfo.fixedEBCDIC.COMMA)
				{
					byte* ptr4 = ptr3;
					this.codePageInfo.EncoderIBM.Reset();
					this.codePageInfo.EncoderIBM.Convert(ptr2, array.Length - 2, ptr4, array.Length - 2, true, out num, out num2, out flag);
				}
			}
			char[] array2 = Currency.ToCharArray();
			this.codePageInfo.unicodeCurrency = Currency;
			int num5 = (this.codePageInfo.EncodingIBM.IsSingleByte ? Currency.Length : (Currency.Length * 2));
			this.codePageInfo.ebcdicCurrency = new byte[num5];
			this.codePageInfo.EncoderIBM.Reset();
			this.codePageInfo.EncoderIBM.Convert(array2, 0, Currency.Length, this.codePageInfo.ebcdicCurrency, 0, num5, true, out num, out num2, out flag);
			if (flag2)
			{
				this.CPInfos[CodePage] = this.codePageInfo;
				return;
			}
			this.CPInfos.Add(CodePage, this.codePageInfo);
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x0007F1D4 File Offset: 0x0007D3D4
		public virtual void SetDontUsePresentationFormsB(bool dontUsePresentationFormsB)
		{
			this.dontUsePresentationFormsBForArabicStrings = dontUsePresentationFormsB;
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x0007F1DD File Offset: 0x0007D3DD
		public virtual void SetLCID(int InLCID)
		{
			this.inLCID = InLCID;
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x0007F1E6 File Offset: 0x0007D3E6
		public virtual void SetSepValues(char TimeSep, char DateSep)
		{
			this.timeSep = TimeSep;
			this.dateSep = DateSep;
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x0007F1F6 File Offset: 0x0007D3F6
		public virtual void SizeOfRemoteType(DataType dataType, int EncodeType, out int convertedLength)
		{
			this.SizeOfRemoteType(dataType, new CEDAR_TYPE_ENCODING(EncodeType), out convertedLength);
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x0007F208 File Offset: 0x0007D408
		public virtual void SizeOfRemoteType(DataType dataType, CEDAR_TYPE_ENCODING encoding, out int convertedLength)
		{
			convertedLength = 0;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				this.SetCodePage(37);
			}
			if (encoding.nCvtType == 64 || encoding.nCvtType == 65)
			{
				convertedLength = 1;
				return;
			}
			if (dataType == DataType.Short || dataType == DataType.ArrayOfShort)
			{
				ConvertedDataType convertedDataType = (ConvertedDataType)encoding.nCvtType;
				int num;
				if (convertedDataType > ConvertedDataType.CEDAR_COBOL_COMP3)
				{
					if (convertedDataType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
					{
						switch (convertedDataType)
						{
						case ConvertedDataType.CEDAR_COBOL_NUMERIC_STRING:
							break;
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_TIMESTAMP:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
							goto IL_00AA;
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
							goto IL_0076;
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
							goto IL_007A;
						default:
							if (convertedDataType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
							{
								goto IL_00AA;
							}
							break;
						}
					}
					num = (int)encoding.nPrecision;
					if (encoding.nSign != 1 && encoding.nOverpunch == 1)
					{
						num++;
					}
					convertedLength = num;
					return;
				}
				if (convertedDataType != ConvertedDataType.CEDAR_COBOL_COMP94)
				{
					if (convertedDataType != ConvertedDataType.CEDAR_COBOL_COMP3)
					{
						goto IL_00AA;
					}
					goto IL_007A;
				}
				IL_0076:
				convertedLength = 2;
				return;
				IL_007A:
				num = (int)encoding.nPrecision;
				convertedLength = (num + 2) / 2;
				return;
				IL_00AA:
				throw new CustomHISException(SR.UnsupportedConversion("SizeOfRemoteType data type short"));
			}
			if (dataType == DataType.Long || dataType == DataType.ArrayOfLong)
			{
				ConvertedDataType convertedDataType = (ConvertedDataType)encoding.nCvtType;
				int num;
				if (convertedDataType > ConvertedDataType.CEDAR_COBOL_COMP3)
				{
					if (convertedDataType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
					{
						switch (convertedDataType)
						{
						case ConvertedDataType.CEDAR_COBOL_NUMERIC_STRING:
							break;
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_TIMESTAMP:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
							goto IL_013B;
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
							goto IL_0107;
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
							goto IL_010B;
						default:
							if (convertedDataType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
							{
								goto IL_013B;
							}
							break;
						}
					}
					num = (int)encoding.nPrecision;
					if (encoding.nSign != 1 && encoding.nOverpunch == 1)
					{
						num++;
					}
					convertedLength = num;
					return;
				}
				if (convertedDataType != ConvertedDataType.CEDAR_COBOL_COMP99)
				{
					if (convertedDataType != ConvertedDataType.CEDAR_COBOL_COMP3)
					{
						goto IL_013B;
					}
					goto IL_010B;
				}
				IL_0107:
				convertedLength = 4;
				return;
				IL_010B:
				num = (int)encoding.nPrecision;
				convertedLength = (num + 2) / 2;
				return;
				IL_013B:
				throw new CustomHISException(SR.UnsupportedConversion("SizeOfRemoteType data type long"));
			}
			if (dataType == DataType.Single || dataType == DataType.ArrayOfSingle)
			{
				ConvertedDataType convertedDataType = (ConvertedDataType)encoding.nCvtType;
				if (convertedDataType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
				{
					switch (convertedDataType)
					{
					case ConvertedDataType.CEDAR_COBOL_COMP94:
						goto IL_01E5;
					case ConvertedDataType.CEDAR_COBOL_COMP99:
						goto IL_01E9;
					case ConvertedDataType.CEDAR_COBOL_COMP3:
						goto IL_01BE;
					case ConvertedDataType.CEDAR_COBOL_COMP1:
						break;
					default:
						if (convertedDataType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							goto IL_01ED;
						}
						goto IL_01CD;
					}
				}
				else
				{
					switch (convertedDataType)
					{
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
						goto IL_01E5;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
						goto IL_01E9;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
						goto IL_01BE;
					case ConvertedDataType.CEDAR_IEEE_NULLABLE_COMP1:
						break;
					case ConvertedDataType.CEDAR_IEEE_NULLABLE_COMP2:
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICXTRAN:
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICGTRAN:
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP918:
						goto IL_01ED;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM:
						goto IL_01CD;
					default:
						if (convertedDataType != ConvertedDataType.CEDAR_IEEE_COMP1 && convertedDataType != ConvertedDataType.CEDAR_IEEE_COMP1_BIG_ENDIAN)
						{
							goto IL_01ED;
						}
						break;
					}
				}
				convertedLength = 4;
				return;
				IL_01BE:
				int num = (int)encoding.nPrecision;
				convertedLength = (num + 2) / 2;
				return;
				IL_01CD:
				num = (int)encoding.nPrecision;
				if (encoding.nOverpunch == 1)
				{
					num++;
				}
				convertedLength = num;
				return;
				IL_01E5:
				convertedLength = 2;
				return;
				IL_01E9:
				convertedLength = 4;
				return;
				IL_01ED:
				throw new CustomHISException(SR.UnsupportedConversion("SizeOfRemoteType data type single"));
			}
			if (dataType == DataType.Double || dataType == DataType.ArrayOfDouble)
			{
				ConvertedDataType convertedDataType = (ConvertedDataType)encoding.nCvtType;
				if (convertedDataType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
				{
					switch (convertedDataType)
					{
					case ConvertedDataType.CEDAR_COBOL_COMP94:
						goto IL_029B;
					case ConvertedDataType.CEDAR_COBOL_COMP99:
						goto IL_029F;
					case ConvertedDataType.CEDAR_COBOL_COMP3:
						goto IL_0274;
					case ConvertedDataType.CEDAR_COBOL_COMP1:
						goto IL_02A3;
					case ConvertedDataType.CEDAR_COBOL_COMP2:
						break;
					default:
						if (convertedDataType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							goto IL_02A3;
						}
						goto IL_0283;
					}
				}
				else
				{
					switch (convertedDataType)
					{
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
						goto IL_029B;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
						goto IL_029F;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
						goto IL_0274;
					case ConvertedDataType.CEDAR_IEEE_NULLABLE_COMP1:
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICXTRAN:
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICGTRAN:
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP918:
						goto IL_02A3;
					case ConvertedDataType.CEDAR_IEEE_NULLABLE_COMP2:
						break;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM:
						goto IL_0283;
					default:
						if (convertedDataType != ConvertedDataType.CEDAR_IEEE_COMP2 && convertedDataType != ConvertedDataType.CEDAR_IEEE_COMP2_BIG_ENDIAN)
						{
							goto IL_02A3;
						}
						break;
					}
				}
				convertedLength = 8;
				return;
				IL_0274:
				int num = (int)encoding.nPrecision;
				convertedLength = (num + 2) / 2;
				return;
				IL_0283:
				num = (int)encoding.nPrecision;
				if (encoding.nOverpunch == 1)
				{
					num++;
				}
				convertedLength = num;
				return;
				IL_029B:
				convertedLength = 2;
				return;
				IL_029F:
				convertedLength = 4;
				return;
				IL_02A3:
				throw new CustomHISException(SR.UnsupportedConversion("SizeOfRemoteType data type double"));
			}
			if (dataType == DataType.String || dataType == DataType.ArrayOfString)
			{
				ConvertedDataType convertedDataType = (ConvertedDataType)encoding.nCvtType;
				if (convertedDataType != ConvertedDataType.CEDAR_COBOL_PICXTRAN)
				{
					if (convertedDataType != ConvertedDataType.CEDAR_COBOL_PICGTRAN)
					{
						switch (convertedDataType)
						{
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICGTRAN:
						case ConvertedDataType.CEDAR_COBOL_VAR_PICGTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_VAR_PICGTRAN:
						case ConvertedDataType.CEDAR_COBOL_LONGVAR_PICGTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_LONGVAR_PICGTRAN:
							goto IL_036A;
						case ConvertedDataType.CEDAR_COBOL_VAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_VAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_LONGVAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_LONGVAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_MIXED_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_MIXED_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_MIXED_VAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_MIXED_VAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_MIXED_LONGVAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_MIXED_LONGVAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NOTPADDED_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_NOTPADDED_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_MIXED_NOTPADDED_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_MIXED_NOTPADDED_PICXTRAN:
							goto IL_0366;
						}
						throw new CustomHISException(SR.UnsupportedConversion("SizeOfRemoteType data type string"));
					}
					IL_036A:
					convertedLength = 2;
					return;
				}
				IL_0366:
				convertedLength = 1;
				return;
			}
			if (dataType == DataType.Boolean || dataType == DataType.ArrayOfBoolean)
			{
				ConvertedDataType convertedDataType = (ConvertedDataType)encoding.nCvtType;
				if (convertedDataType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
				{
					switch (convertedDataType)
					{
					case ConvertedDataType.CEDAR_COBOL_COMP94:
						break;
					case ConvertedDataType.CEDAR_COBOL_COMP99:
						goto IL_03D3;
					case ConvertedDataType.CEDAR_COBOL_COMP3:
						goto IL_03D7;
					default:
						if (convertedDataType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							goto IL_03FE;
						}
						goto IL_03E6;
					}
				}
				else
				{
					switch (convertedDataType)
					{
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
						break;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
						goto IL_03D3;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
						goto IL_03D7;
					default:
						if (convertedDataType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
						{
							goto IL_03FE;
						}
						goto IL_03E6;
					}
				}
				convertedLength = 2;
				return;
				IL_03D3:
				convertedLength = 4;
				return;
				IL_03D7:
				int num = (int)encoding.nPrecision;
				convertedLength = (num + 2) / 2;
				return;
				IL_03E6:
				num = (int)encoding.nPrecision;
				if (encoding.nOverpunch == 1)
				{
					num++;
				}
				convertedLength = num;
				return;
				IL_03FE:
				throw new CustomHISException(SR.UnsupportedConversion("SizeOfRemoteType data type Boolean"));
			}
			if (dataType == DataType.Byte || dataType == DataType.ArrayOfByte)
			{
				ConvertedDataType convertedDataType = (ConvertedDataType)encoding.nCvtType;
				if (convertedDataType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
				{
					switch (convertedDataType)
					{
					case ConvertedDataType.CEDAR_COBOL_COMP94:
						break;
					case ConvertedDataType.CEDAR_COBOL_COMP99:
						goto IL_0478;
					case ConvertedDataType.CEDAR_COBOL_COMP3:
						goto IL_047C;
					case ConvertedDataType.CEDAR_COBOL_COMP1:
					case ConvertedDataType.CEDAR_COBOL_COMP2:
					case ConvertedDataType.CEDAR_COBOL_PICXTRAN:
						goto IL_04A7;
					case ConvertedDataType.CEDAR_COBOL_PICXNOTRAN:
						goto IL_04A3;
					default:
						if (convertedDataType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							goto IL_04A7;
						}
						goto IL_048B;
					}
				}
				else
				{
					switch (convertedDataType)
					{
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
						break;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
						goto IL_0478;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
						goto IL_047C;
					default:
						if (convertedDataType == ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
						{
							goto IL_048B;
						}
						if (convertedDataType != ConvertedDataType.CEDAR_COBOL_NULLABLE_PICXNOTRAN)
						{
							goto IL_04A7;
						}
						goto IL_04A3;
					}
				}
				convertedLength = 2;
				return;
				IL_0478:
				convertedLength = 4;
				return;
				IL_047C:
				int num = (int)encoding.nPrecision;
				convertedLength = (num + 2) / 2;
				return;
				IL_048B:
				num = (int)encoding.nPrecision;
				if (encoding.nOverpunch == 1)
				{
					num++;
				}
				convertedLength = num;
				return;
				IL_04A3:
				convertedLength = 1;
				return;
				IL_04A7:
				throw new CustomHISException(SR.UnsupportedConversion("SizeOfRemoteType data type Byte"));
			}
			if (dataType == DataType.Date || dataType == DataType.ArrayOfDate)
			{
				switch (encoding.nCvtType)
				{
				case 7:
					convertedLength = 8;
					return;
				case 11:
					convertedLength = 4;
					return;
				case 12:
					convertedLength = 4;
					return;
				case 14:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 19;
						return;
					}
					convertedLength = 38;
					return;
				case 15:
				case 45:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 10;
						return;
					}
					convertedLength = 20;
					return;
				case 16:
				case 46:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 8;
						return;
					}
					convertedLength = 16;
					return;
				case 17:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 19;
						return;
					}
					convertedLength = 38;
					return;
				case 18:
				case 47:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 10;
						return;
					}
					convertedLength = 20;
					return;
				case 19:
				case 48:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 8;
						return;
					}
					convertedLength = 16;
					return;
				case 20:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 19;
						return;
					}
					convertedLength = 38;
					return;
				case 21:
				case 49:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 10;
						return;
					}
					convertedLength = 20;
					return;
				case 22:
				case 50:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 8;
						return;
					}
					convertedLength = 16;
					return;
				case 23:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 19;
						return;
					}
					convertedLength = 38;
					return;
				case 24:
				case 51:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 10;
						return;
					}
					convertedLength = 20;
					return;
				case 25:
				case 52:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 8;
						return;
					}
					convertedLength = 16;
					return;
				case 26:
				case 34:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 26;
						return;
					}
					convertedLength = 52;
					return;
				case 27:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 8;
						return;
					}
					convertedLength = 16;
					return;
				case 28:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 8;
						return;
					}
					convertedLength = 16;
					return;
				case 29:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 8;
						return;
					}
					convertedLength = 16;
					return;
				case 30:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 6;
						return;
					}
					convertedLength = 12;
					return;
				case 31:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 8;
						return;
					}
					convertedLength = 16;
					return;
				case 32:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 8;
						return;
					}
					convertedLength = 16;
					return;
				}
				throw new CustomHISException(SR.UnsupportedConversion("SizeOfRemoteType data type Date"));
			}
			if (dataType == DataType.Time || dataType == DataType.ArrayOfTime)
			{
				ConvertedDataType convertedDataType = (ConvertedDataType)encoding.nCvtType;
				if (convertedDataType <= ConvertedDataType.CEDAR_COBOL_USA_TIME)
				{
					if (convertedDataType == ConvertedDataType.CEDAR_COBOL_DATEFMT3)
					{
						convertedLength = 4;
						return;
					}
					if (convertedDataType != ConvertedDataType.CEDAR_COBOL_ISO_TIME)
					{
						if (convertedDataType != ConvertedDataType.CEDAR_COBOL_USA_TIME)
						{
							goto IL_083E;
						}
						goto IL_07E6;
					}
				}
				else
				{
					if (convertedDataType <= ConvertedDataType.CEDAR_COBOL_EUR_TIME)
					{
						if (convertedDataType != ConvertedDataType.CEDAR_COBOL_JIS_TIME)
						{
							if (convertedDataType != ConvertedDataType.CEDAR_COBOL_EUR_TIME)
							{
								goto IL_083E;
							}
							goto IL_0812;
						}
					}
					else if (convertedDataType != ConvertedDataType.CEDAR_COBOL_HMS_TIME)
					{
						switch (convertedDataType)
						{
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_ISO_TIME:
							goto IL_07D0;
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_USA_DATE:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_JIS_DATE:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_EUR_DATE:
							goto IL_083E;
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_USA_TIME:
							goto IL_07E6;
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_JIS_TIME:
							break;
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_EUR_TIME:
							goto IL_0812;
						default:
							goto IL_083E;
						}
					}
					else
					{
						if (!this.codePageInfo.isDBCS)
						{
							convertedLength = 8;
							return;
						}
						convertedLength = 16;
						return;
					}
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 8;
						return;
					}
					convertedLength = 16;
					return;
					IL_0812:
					if (!this.codePageInfo.isDBCS)
					{
						convertedLength = 8;
						return;
					}
					convertedLength = 16;
					return;
				}
				IL_07D0:
				if (!this.codePageInfo.isDBCS)
				{
					convertedLength = 8;
					return;
				}
				convertedLength = 16;
				return;
				IL_07E6:
				if (!this.codePageInfo.isDBCS)
				{
					convertedLength = 8;
					return;
				}
				convertedLength = 16;
				return;
				IL_083E:
				throw new CustomHISException(SR.UnsupportedConversion("SizeOfRemoteType data type Time"));
			}
			if (dataType == DataType.Decimal || dataType == DataType.ArrayOfDecimal)
			{
				ConvertedDataType convertedDataType = (ConvertedDataType)encoding.nCvtType;
				if (convertedDataType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
				{
					switch (convertedDataType)
					{
					case ConvertedDataType.CEDAR_COBOL_COMP94:
						goto IL_08EC;
					case ConvertedDataType.CEDAR_COBOL_COMP99:
						goto IL_08F0;
					case ConvertedDataType.CEDAR_COBOL_COMP3:
						break;
					default:
						if (convertedDataType == ConvertedDataType.CEDAR_COBOL_COMP918)
						{
							goto IL_08E8;
						}
						if (convertedDataType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							goto IL_08FD;
						}
						goto IL_08D0;
					}
				}
				else
				{
					switch (convertedDataType)
					{
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
						goto IL_08EC;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
						goto IL_08F0;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
						break;
					case ConvertedDataType.CEDAR_IEEE_NULLABLE_COMP1:
					case ConvertedDataType.CEDAR_IEEE_NULLABLE_COMP2:
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICXTRAN:
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICGTRAN:
						goto IL_08FD;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP918:
						goto IL_08E8;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM:
						goto IL_08D0;
					default:
						if (convertedDataType == ConvertedDataType.CEDAR_DECIMAL_FLOAT_64)
						{
							convertedLength = 8;
							return;
						}
						if (convertedDataType != ConvertedDataType.CEDAR_DECIMAL_FLOAT_128)
						{
							goto IL_08FD;
						}
						convertedLength = 16;
						return;
					}
				}
				int num = (int)encoding.nPrecision;
				convertedLength = (num + 2) / 2;
				return;
				IL_08D0:
				num = (int)encoding.nPrecision;
				if (encoding.nOverpunch == 1)
				{
					num++;
				}
				convertedLength = num;
				return;
				IL_08E8:
				convertedLength = 8;
				return;
				IL_08EC:
				convertedLength = 2;
				return;
				IL_08F0:
				convertedLength = 4;
				return;
				IL_08FD:
				throw new CustomHISException(SR.UnsupportedConversion("SizeOfRemoteType data type Decimal"));
			}
			throw new CustomHISException(SR.UnsupportedConversion("SizeOfRemoteType data type invalid"));
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x0007FB34 File Offset: 0x0007DD34
		public virtual void PackTime(TimeSpan TimeSpanValue, ref byte Buffer, ref int cumulativePackedDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, char TimeSeparator, int CodePage)
		{
			DateTime dateTime = new DateTime(TimeSpanValue.Ticks);
			ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
			if (nCvtType != ConvertedDataType.CEDAR_COBOL_EUR_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_EUR_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_ISO_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_ISO_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_JIS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_JIS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_USA_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_USA_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_HMS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_DATEFMT3)
			{
				throw new CustomHISException(SR.UnsupportedConversion("PackTime"));
			}
			int num;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				num = CodePage;
			}
			else
			{
				num = this.codePageInfo.codePage;
			}
			this.SetCodePage(CodePage);
			this.PackDate(dateTime, ref Buffer, ref cumulativePackedDataLength, ResultLen, encoding, TimeSeparator, '/');
			this.SetCodePage(num);
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x0007FBD4 File Offset: 0x0007DDD4
		public virtual void PackTime(TimeSpan TimeSpanValue, ref byte Buffer, ref int cumulativePackedDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, int CodePage)
		{
			DateTime dateTime = new DateTime(TimeSpanValue.Ticks);
			ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
			if (nCvtType != ConvertedDataType.CEDAR_COBOL_EUR_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_EUR_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_ISO_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_ISO_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_JIS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_JIS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_USA_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_USA_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_HMS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_DATEFMT3)
			{
				throw new CustomHISException(SR.UnsupportedConversion("PackTime"));
			}
			int num;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				num = CodePage;
			}
			else
			{
				num = this.codePageInfo.codePage;
			}
			this.SetCodePage(CodePage);
			this.PackDate(dateTime, ref Buffer, ref cumulativePackedDataLength, ResultLen, encoding, this.timeSep, '/', CodePage);
			this.SetCodePage(num);
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x0007FC78 File Offset: 0x0007DE78
		public virtual void PackTime(TimeSpan TimeSpanValue, ref byte Buffer, ref int cumulativePackedDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			DateTime dateTime = new DateTime(TimeSpanValue.Ticks);
			ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
			if (nCvtType != ConvertedDataType.CEDAR_COBOL_EUR_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_EUR_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_ISO_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_ISO_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_JIS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_JIS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_USA_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_USA_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_HMS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_DATEFMT3)
			{
				throw new CustomHISException(SR.UnsupportedConversion("PackTime"));
			}
			this.PackDate(dateTime, ref Buffer, ref cumulativePackedDataLength, ResultLen, encoding, this.timeSep, '/');
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x0007FCF4 File Offset: 0x0007DEF4
		public virtual void PackTime(TimeSpan TimeSpanValue, ref byte Buffer, ref int cumulativePackedDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, char TimeSeparator)
		{
			DateTime dateTime = new DateTime(TimeSpanValue.Ticks);
			ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
			if (nCvtType != ConvertedDataType.CEDAR_COBOL_EUR_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_EUR_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_ISO_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_ISO_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_JIS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_JIS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_USA_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_USA_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_HMS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_DATEFMT3)
			{
				throw new CustomHISException(SR.UnsupportedConversion("PackTime"));
			}
			this.PackDate(dateTime, ref Buffer, ref cumulativePackedDataLength, ResultLen, encoding, TimeSeparator, '/');
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x0007FD6C File Offset: 0x0007DF6C
		public virtual void UnpackTime(ref byte Buffer, ref TimeSpan ReturnedTimeSpan, ref int RemainingBufferDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, int CodePage)
		{
			DateTime dateTime = default(DateTime);
			ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
			if (nCvtType != ConvertedDataType.CEDAR_COBOL_EUR_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_EUR_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_ISO_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_ISO_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_JIS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_JIS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_USA_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_USA_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_HMS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_DATEFMT3)
			{
				throw new CustomHISException(SR.UnsupportedConversion("UnpackTime with CodePage override"));
			}
			int num;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				num = CodePage;
			}
			else
			{
				num = this.codePageInfo.codePage;
			}
			this.SetCodePage(CodePage);
			this.UnpackDate(ref Buffer, ref dateTime, ref RemainingBufferDataLength, ResultLen, encoding);
			this.SetCodePage(num);
			ReturnedTimeSpan = new TimeSpan(0, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x0007FE28 File Offset: 0x0007E028
		public virtual void UnpackTime(ref byte Buffer, ref TimeSpan ReturnedTimeSpan, ref int RemainingBufferDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			DateTime dateTime = default(DateTime);
			ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
			if (nCvtType != ConvertedDataType.CEDAR_COBOL_EUR_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_EUR_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_ISO_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_ISO_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_JIS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_JIS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_USA_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_USA_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_HMS_TIME && nCvtType != ConvertedDataType.CEDAR_COBOL_DATEFMT3)
			{
				throw new CustomHISException(SR.UnsupportedConversion("UnpackTime"));
			}
			this.UnpackDate(ref Buffer, ref dateTime, ref RemainingBufferDataLength, ResultLen, encoding);
			ReturnedTimeSpan = new TimeSpan(0, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x0007FEC0 File Offset: 0x0007E0C0
		public virtual void PackDate(DateTime DateTimeValue, ref byte Buffer, ref int cumulativePackedDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, char TimeSeparator, char DateSeparator, int CodePage)
		{
			int num;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				num = CodePage;
			}
			else
			{
				num = this.codePageInfo.codePage;
			}
			this.SetCodePage(CodePage);
			this.PackDate(DateTimeValue, ref Buffer, ref cumulativePackedDataLength, ResultLen, encoding, TimeSeparator, DateSeparator);
			this.SetCodePage(num);
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x0007FF08 File Offset: 0x0007E108
		public virtual void PackDate(DateTime DateTimeValue, ref byte Buffer, ref int cumulativePackedDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, int CodePage)
		{
			int num;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				num = CodePage;
			}
			else
			{
				num = this.codePageInfo.codePage;
			}
			this.SetCodePage(CodePage);
			this.PackDate(DateTimeValue, ref Buffer, ref cumulativePackedDataLength, ResultLen, encoding, this.timeSep, this.dateSep);
			this.SetCodePage(num);
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x0007FF55 File Offset: 0x0007E155
		public virtual void PackDate(DateTime DateTimeValue, ref byte Buffer, ref int cumulativePackedDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			this.PackDate(DateTimeValue, ref Buffer, ref cumulativePackedDataLength, ResultLen, encoding, this.timeSep, this.dateSep);
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x0007FF70 File Offset: 0x0007E170
		public unsafe virtual void PackDate(DateTime DateTimeValue, ref byte Buffer, ref int cumulativePackedDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, char TimeSeparator, char DateSeparator)
		{
			string text = "0123456789";
			char[] array = new char[ResultLen];
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union2 = default(BasePrimitiveConverter.Int64Union);
			try
			{
				char c;
				if (TimeSeparator != '\0')
				{
					c = TimeSeparator;
				}
				else
				{
					c = this.timeSep;
				}
				char c2;
				if (DateSeparator != '\0')
				{
					c2 = DateSeparator;
				}
				else
				{
					c2 = this.dateSep;
				}
				if (26 == encoding.nCvtType || 34 == encoding.nCvtType)
				{
					c = '.';
				}
				int num = ResultLen + cumulativePackedDataLength;
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						for (int i = 0; i < ResultLen; i++)
						{
							ptr2[i] = 0;
						}
						int num3;
						switch (encoding.nCvtType)
						{
						case 7:
						{
							int64Union.int64Val = (long)(DateTimeValue.Year * 1000 + DateTimeValue.DayOfYear);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							if (int64Union.int64Val > 9999365L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							int num2 = 7;
							int j = 3;
							if (!this.BuildPackedDec(int64Union.int64Val, num2, 0, 4, ptr2, 15))
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							byte* ptr3 = ptr2 + j;
							*ptr3 |= 15;
							ptr2 += 4;
							int64Union.int64Val = (long)(DateTimeValue.Hour * 100000 + DateTimeValue.Minute * 1000 + DateTimeValue.Second * 10 + (DateTimeValue.Millisecond + 50) / 100);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							num2 = 7;
							j = 3;
							if (!this.BuildPackedDec(int64Union.int64Val, num2, 0, 4, ptr2, 15))
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							byte* ptr4 = ptr2 + j;
							*ptr4 |= 15;
							num = ResultLen;
							goto IL_1745;
						}
						case 8:
						case 9:
						case 10:
						case 13:
						case 33:
						case 35:
						case 36:
						case 37:
						case 38:
						case 39:
						case 40:
						case 41:
						case 42:
						case 43:
						case 44:
							goto IL_1745;
						case 11:
						{
							int64Union.int64Val = (long)(DateTimeValue.Year * 1000 + DateTimeValue.DayOfYear);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							if (int64Union.int64Val > 9999365L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							int num2 = 7;
							int j = 3;
							if (!this.BuildPackedDec(int64Union.int64Val, num2, 0, 4, ptr2, 15))
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							byte* ptr5 = ptr2 + j;
							*ptr5 |= 15;
							num = ResultLen;
							goto IL_1745;
						}
						case 12:
						{
							int64Union.int64Val = (long)(DateTimeValue.Hour * 100000 + DateTimeValue.Minute * 1000 + DateTimeValue.Second * 10 + (DateTimeValue.Millisecond + 50) / 100);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							int num2 = 7;
							int j = 3;
							if (!this.BuildPackedDec(int64Union.int64Val, num2, 0, 4, ptr2, 15))
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							byte* ptr6 = ptr2 + j;
							*ptr6 |= 15;
							num = ResultLen;
							goto IL_1745;
						}
						case 14:
						{
							int64Union.int64Val = (long)(DateTimeValue.Year * 10000 + DateTimeValue.Month * 100 + DateTimeValue.Day);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 9; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 4 || j == 7)
								{
									array[j] = '-';
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num3 = 10;
							array[num3] = ' ';
							num3++;
							int64Union.int64Val = (long)(DateTimeValue.Hour * 10000 + DateTimeValue.Minute * 100 + DateTimeValue.Second);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 7; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2 || j == 5)
								{
									array[num3 + j] = '.';
									j--;
								}
								array[num3 + j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 19, ResultLen);
							goto IL_1745;
						}
						case 15:
						case 45:
						{
							int64Union.int64Val = (long)(DateTimeValue.Year * 10000 + DateTimeValue.Month * 100 + DateTimeValue.Day);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 9; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 4 || j == 7)
								{
									array[j] = '-';
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 10, ResultLen);
							goto IL_1745;
						}
						case 16:
						case 46:
						{
							int64Union.int64Val = (long)(DateTimeValue.Hour * 10000 + DateTimeValue.Minute * 100 + DateTimeValue.Second);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 7; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2 || j == 5)
								{
									array[j] = '.';
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 8, ResultLen);
							goto IL_1745;
						}
						case 17:
						{
							int64Union.int64Val = (long)(DateTimeValue.Month * 1000000 + DateTimeValue.Day * 10000 + DateTimeValue.Year);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 9; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2 || j == 5)
								{
									array[j] = '/';
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num3 = 10;
							array[num3] = ' ';
							num3++;
							if (DateTimeValue.Hour > 11 && DateTimeValue.Hour < 24)
							{
								array[num3 + 6] = 'P';
							}
							else
							{
								array[num3 + 6] = 'A';
							}
							array[num3 + 7] = 'M';
							array[num3 + 5] = ' ';
							int num4 = DateTimeValue.Hour;
							if (num4 == 24)
							{
								num4 = 0;
							}
							else if (num4 > 11)
							{
								num4 -= 12;
							}
							int64Union.int64Val = (long)(num4 * 100 + DateTimeValue.Minute);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 4; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2)
								{
									array[num3 + j] = ':';
									j--;
								}
								array[num3 + j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 19, ResultLen);
							goto IL_1745;
						}
						case 18:
						case 47:
						{
							int64Union.int64Val = (long)(DateTimeValue.Month * 1000000 + DateTimeValue.Day * 10000 + DateTimeValue.Year);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 9; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2 || j == 5)
								{
									array[j] = '/';
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 10, ResultLen);
							goto IL_1745;
						}
						case 19:
						case 48:
						{
							if (DateTimeValue.Hour > 11 && DateTimeValue.Hour < 24)
							{
								array[6] = 'P';
							}
							else
							{
								array[6] = 'A';
							}
							array[7] = 'M';
							array[5] = ' ';
							int num4 = DateTimeValue.Hour;
							if (num4 == 24)
							{
								num4 = 0;
							}
							else if (num4 > 11)
							{
								num4 -= 12;
							}
							int64Union.int64Val = (long)(num4 * 100 + DateTimeValue.Minute);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 4; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2)
								{
									array[j] = ':';
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 8, ResultLen);
							goto IL_1745;
						}
						case 20:
						{
							int64Union.int64Val = (long)(DateTimeValue.Year * 10000 + DateTimeValue.Month * 100 + DateTimeValue.Day);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 9; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 4 || j == 7)
								{
									array[j] = '-';
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							array[10] = ' ';
							num3 = 11;
							int64Union.int64Val = (long)(DateTimeValue.Hour * 10000 + DateTimeValue.Minute * 100 + DateTimeValue.Second);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 7; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2 || j == 5)
								{
									array[num3 + j] = ':';
									j--;
								}
								array[num3 + j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 19, ResultLen);
							goto IL_1745;
						}
						case 21:
						case 49:
						{
							int64Union.int64Val = (long)(DateTimeValue.Year * 10000 + DateTimeValue.Month * 100 + DateTimeValue.Day);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 9; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 4 || j == 7)
								{
									array[j] = '-';
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 10, ResultLen);
							goto IL_1745;
						}
						case 22:
						case 50:
						{
							int64Union.int64Val = (long)(DateTimeValue.Hour * 10000 + DateTimeValue.Minute * 100 + DateTimeValue.Second);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 7; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2 || j == 5)
								{
									array[j] = ':';
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 8, ResultLen);
							goto IL_1745;
						}
						case 23:
						{
							int64Union.int64Val = (long)(DateTimeValue.Day * 1000000 + DateTimeValue.Month * 10000 + DateTimeValue.Year);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 9; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2 || j == 5)
								{
									array[j] = '.';
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							array[10] = ' ';
							num3 = 11;
							int64Union.int64Val = (long)(DateTimeValue.Hour * 10000 + DateTimeValue.Minute * 100 + DateTimeValue.Second);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 7; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2 || j == 5)
								{
									array[num3 + j] = '.';
									j--;
								}
								array[num3 + j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 19, ResultLen);
							goto IL_1745;
						}
						case 24:
						case 51:
						{
							int64Union.int64Val = (long)(DateTimeValue.Day * 1000000 + DateTimeValue.Month * 10000 + DateTimeValue.Year);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 9; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2 || j == 5)
								{
									array[j] = '.';
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 10, ResultLen);
							goto IL_1745;
						}
						case 25:
						case 52:
						{
							int64Union.int64Val = (long)(DateTimeValue.Hour * 10000 + DateTimeValue.Minute * 100 + DateTimeValue.Second);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 7; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2 || j == 5)
								{
									array[j] = '.';
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 8, ResultLen);
							goto IL_1745;
						}
						case 26:
							break;
						case 27:
						{
							int64Union.int64Val = (long)(DateTimeValue.Month * 10000 + DateTimeValue.Day * 100 + (DateTimeValue.Year - DateTimeValue.Year / 100 * 100));
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 7; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2 || j == 5)
								{
									array[j] = c2;
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 8, ResultLen);
							goto IL_1745;
						}
						case 28:
						{
							int64Union.int64Val = (long)(DateTimeValue.Day * 10000 + DateTimeValue.Month * 100 + (DateTimeValue.Year - DateTimeValue.Year / 100 * 100));
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 7; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2 || j == 5)
								{
									array[j] = c2;
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 8, ResultLen);
							goto IL_1745;
						}
						case 29:
						{
							int64Union.int64Val = (long)((DateTimeValue.Year - DateTimeValue.Year / 100 * 100) * 10000 + DateTimeValue.Month * 100 + DateTimeValue.Day);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 7; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2 || j == 5)
								{
									array[j] = c2;
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 8, ResultLen);
							goto IL_1745;
						}
						case 30:
						{
							int64Union.int64Val = (long)((DateTimeValue.Year - DateTimeValue.Year / 100 * 100) * 1000 + DateTimeValue.DayOfYear);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							if (int64Union.int64Val > 99365L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							for (int j = 5; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2)
								{
									array[j] = c2;
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 6, ResultLen);
							goto IL_1745;
						}
						case 31:
						{
							int64Union.int64Val = (long)(DateTimeValue.Hour * 10000 + DateTimeValue.Minute * 100 + DateTimeValue.Second);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							for (int j = 7; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 2 || j == 5)
								{
									array[j] = c;
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 8, ResultLen);
							goto IL_1745;
						}
						case 32:
						{
							int64Union.int64Val = (long)(DateTimeValue.Year * 1000 + DateTimeValue.DayOfYear);
							if (int64Union.int64Val < 0L)
							{
								int64Union.int64Val = -int64Union.int64Val;
							}
							if (int64Union.int64Val > 9999365L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							for (int j = 7; j > -1; j--)
							{
								int64Union2.int64Val = int64Union.int64Val % 10L;
								if (j == 4)
								{
									array[j] = c2;
									j--;
								}
								array[j] = text[int64Union2.int32Val];
								int64Union.int64Val /= 10L;
							}
							if (int64Union.int64Val != 0L)
							{
								throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
							}
							num = this.ConvertDateToString(array, ptr2, 8, ResultLen);
							goto IL_1745;
						}
						case 34:
							c = ':';
							break;
						default:
							goto IL_1745;
						}
						int64Union.int64Val = (long)(DateTimeValue.Year * 10000 + DateTimeValue.Month * 100 + DateTimeValue.Day);
						if (int64Union.int64Val < 0L)
						{
							int64Union.int64Val = -int64Union.int64Val;
						}
						for (int j = 9; j > -1; j--)
						{
							int64Union2.int64Val = int64Union.int64Val % 10L;
							if (j == 4 || j == 7)
							{
								array[j] = '-';
								j--;
							}
							array[j] = text[int64Union2.int32Val];
							int64Union.int64Val /= 10L;
						}
						if (int64Union.int64Val != 0L)
						{
							throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
						}
						array[10] = '-';
						num3 = 11;
						long num5 = DateTimeValue.Ticks;
						long num6 = num5 / 10000000L;
						num6 *= 10000000L;
						num5 -= num6;
						int64Union.int64Val = (long)DateTimeValue.Hour * 10000000000L + (long)DateTimeValue.Minute * 100000000L + (long)DateTimeValue.Second * 1000000L + num5;
						for (int j = 14; j > -1; j--)
						{
							int64Union2.int64Val = int64Union.int64Val % 10L;
							if (j == 2 || j == 5 || j == 8)
							{
								array[num3 + j] = c;
								j--;
							}
							array[num3 + j] = text[int64Union2.int32Val];
							int64Union.int64Val /= 10L;
						}
						if (int64Union.int64Val != 0L)
						{
							throw new CustomHISException(SR.DateConvertError2, 1526, this.UserCompatibleErrorCode);
						}
						num = this.ConvertDateToString(array, ptr2, 26, ResultLen);
						IL_1745:;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				cumulativePackedDataLength += num;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x00081734 File Offset: 0x0007F934
		private unsafe int ConvertDateToString(char[] InCharArray, byte* IOBuffer, int charCount, int lLen)
		{
			int num = 0;
			string text = new string(InCharArray, 0, charCount);
			if (this.PackString(text, ref *IOBuffer, ref num, charCount, false, lLen, CEDAR_TYPE_ENCODING.NotPaddedDefaultEncodingBstr) != 0)
			{
				throw new CustomHISException(SR.DateConvertError3);
			}
			return num;
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x0008176F File Offset: 0x0007F96F
		public virtual bool VerifyEditedDateTimeMask(string EditMask, bool toHost)
		{
			return this.VerifyEditedTimeMask(EditMask, false, toHost);
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x0008177A File Offset: 0x0007F97A
		public virtual bool VerifyEditedTimeSpanMask(string EditMask, bool toHost)
		{
			return this.VerifyEditedTimeMask(EditMask, true, toHost);
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x00081788 File Offset: 0x0007F988
		private bool VerifyEditedTimeMask(string EditMask, bool isTimeSpan, bool toHost)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			int num7;
			return this.PreProcessDateMask(EditMask, toHost, out num, out num2, out num3, out num4, out num5, out num6, out num7) && (!isTimeSpan || (num == 0 && num2 == 0 && num3 == 0));
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x000817C0 File Offset: 0x0007F9C0
		public virtual void PackEditedTimeSpan(TimeSpan TimeSpanValue, ref byte Buffer, ref int cumulativePackedDataLength, int ResultLen, string TimeSpanEditPattern, int CodePage)
		{
			int num;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				num = CodePage;
			}
			else
			{
				num = this.codePageInfo.codePage;
			}
			this.SetCodePage(CodePage);
			this.PackEditedTimeSpan(TimeSpanValue, ref Buffer, ref cumulativePackedDataLength, ResultLen, TimeSpanEditPattern);
			this.SetCodePage(num);
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x00081804 File Offset: 0x0007FA04
		public virtual void PackEditedTimeSpan(TimeSpan TimeSpanValue, ref byte Buffer, ref int cumulativePackedDataLength, int ResultLen, string TimeSpanEditPattern)
		{
			try
			{
				this.FormatTimeSpan(TimeSpanValue, TimeSpanEditPattern);
				this.PackString(this.editedTimeString, this.editedTimeStringLength, ref Buffer, ref cumulativePackedDataLength, this.editedTimeStringLength, false, ResultLen, CEDAR_TYPE_ENCODING.AsIsNotPaddedDefaultEncodingBstr);
			}
			catch (Exception ex)
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex);
				}
				throw;
			}
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x00081870 File Offset: 0x0007FA70
		public virtual void PackEditedDateTime(DateTime DateTimeValue, ref byte Buffer, ref int cumulativePackedDataLength, int ResultLen, string DateEditPattern, int CodePage)
		{
			int num;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				num = CodePage;
			}
			else
			{
				num = this.codePageInfo.codePage;
			}
			this.SetCodePage(CodePage);
			this.PackEditedDateTime(DateTimeValue, ref Buffer, ref cumulativePackedDataLength, ResultLen, DateEditPattern);
			this.SetCodePage(num);
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000818B4 File Offset: 0x0007FAB4
		public virtual void PackEditedDateTime(DateTime DateTimeValue, ref byte Buffer, ref int cumulativePackedDataLength, int ResultLen, string DateEditPattern)
		{
			try
			{
				this.FormatDate(DateTimeValue, DateEditPattern);
				this.PackString(this.editedTimeString, this.editedTimeStringLength, ref Buffer, ref cumulativePackedDataLength, this.editedTimeStringLength, false, ResultLen, CEDAR_TYPE_ENCODING.AsIsNotPaddedDefaultEncodingBstr);
			}
			catch (Exception ex)
			{
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex);
				}
				throw;
			}
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x00081920 File Offset: 0x0007FB20
		public virtual void UnpackDate(ref byte Buffer, ref DateTime ReturnedDateTime, ref int RemainingBufferDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, int CodePage)
		{
			int num;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				num = CodePage;
			}
			else
			{
				num = this.codePageInfo.codePage;
			}
			this.SetCodePage(CodePage);
			this.UnpackDate(ref Buffer, ref ReturnedDateTime, ref RemainingBufferDataLength, ResultLen, encoding);
			this.SetCodePage(num);
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x00081964 File Offset: 0x0007FB64
		public unsafe virtual void UnpackDate(ref byte Buffer, ref DateTime ReturnedDateTime, ref int RemainingBufferDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			bool flag = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			long num7 = 0L;
			string text = null;
			int cvtEncoding = encoding.CvtEncoding;
			try
			{
				int num8 = RemainingBufferDataLength - ResultLen;
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						switch (encoding.nCvtType)
						{
						case 7:
						{
							int num9 = 2;
							if (!this.ConvertPackedDec(ptr2, 1, num9, 4, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
							}
							ptr2 += num9;
							if (int64Union.int16Val < 100)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							num3 = (int)int64Union.int16Val;
							num9 = 2;
							if (!this.ConvertPackedDec(ptr2, 0, num9, 3, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
							}
							ptr2 += num9;
							short num10 = int64Union.int16Val;
							short num11 = 0;
							if (num3 % 4 == 0)
							{
								num11 = 1;
							}
							if (num3 % 100 == 0)
							{
								num11 = 0;
							}
							if (num3 % 400 == 0)
							{
								num11 = 1;
							}
							short num12 = 0;
							while (num10 > BasePrimitiveConverter.x_sDaysOfMonth[(int)num12])
							{
								num10 -= BasePrimitiveConverter.x_sDaysOfMonth[(int)num12];
								if (1 == num12 && num10 > 0)
								{
									num10 -= num11;
									if (num10 == 0)
									{
										num10 = 29;
										break;
									}
								}
								num12 += 1;
							}
							num2 = (int)(num12 + 1);
							num = (int)num10;
							num9 = 1;
							if (!this.ConvertPackedDec(ptr2, 1, num9, 2, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
							}
							ptr2 += num9;
							num4 = (int)int64Union.int16Val;
							num9 = 1;
							if (!this.ConvertPackedDec(ptr2, 1, num9, 2, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
							}
							ptr2 += num9;
							num5 = (int)int64Union.int16Val;
							num9 = 1;
							if (!this.ConvertPackedDec(ptr2, 1, num9, 2, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
							}
							ptr2 += num9;
							num6 = (int)int64Union.int16Val;
							num9 = 1;
							if (!this.ConvertPackedDec(ptr2, 0, num9, 1, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
							}
							num7 = (long)((int)int64Union.int16Val * 1000000);
							goto IL_150D;
						}
						case 11:
						{
							int num9 = 2;
							if (!this.ConvertPackedDec(ptr2, 1, num9, 4, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
							}
							ptr2 += num9;
							if (int64Union.int16Val < 100)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							num3 = (int)int64Union.int16Val;
							num9 = 2;
							if (!this.ConvertPackedDec(ptr2, 0, num9, 3, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
							}
							ptr2 += num9;
							short num10 = int64Union.int16Val;
							short num11 = 0;
							if (num3 % 4 == 0)
							{
								num11 = 1;
							}
							if (num3 % 100 == 0)
							{
								num11 = 0;
							}
							if (num3 % 400 == 0)
							{
								num11 = 1;
							}
							short num12 = 0;
							while (num10 > BasePrimitiveConverter.x_sDaysOfMonth[(int)num12])
							{
								num10 -= BasePrimitiveConverter.x_sDaysOfMonth[(int)num12];
								if (1 == num12 && num10 > 0)
								{
									num10 -= num11;
									if (num10 == 0)
									{
										num10 = 29;
										break;
									}
								}
								num12 += 1;
							}
							num2 = (int)(num12 + 1);
							num = (int)num10;
							num4 = 0;
							num5 = 0;
							num6 = 0;
							num7 = 0L;
							goto IL_150D;
						}
						case 12:
						{
							num3 = 1;
							num2 = 1;
							num = 1;
							int num9 = 1;
							if (!this.ConvertPackedDec(ptr2, 1, num9, 2, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
							}
							ptr2 += num9;
							num4 = (int)int64Union.int16Val;
							num9 = 1;
							if (!this.ConvertPackedDec(ptr2, 1, num9, 2, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
							}
							ptr2 += num9;
							num5 = (int)int64Union.int16Val;
							num9 = 1;
							if (!this.ConvertPackedDec(ptr2, 1, num9, 2, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
							}
							ptr2 += num9;
							num6 = (int)int64Union.int16Val;
							num9 = 1;
							if (!this.ConvertPackedDec(ptr2, 0, num9, 1, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
							}
							num7 = (long)((int)int64Union.int16Val * 1000000);
							goto IL_150D;
						}
						case 14:
						{
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num13 = 0;
							int num14 = 4;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 5;
							num3 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num2 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num4 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num5 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num6 = (int)int64Union.int16Val;
							num7 = 0L;
							goto IL_150D;
						}
						case 15:
						{
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num13 = 0;
							int num14 = 4;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 5;
							num3 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num2 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 2;
							num = (int)int64Union.int16Val;
							num4 = 0;
							num5 = 0;
							num6 = 0;
							num7 = 0L;
							goto IL_150D;
						}
						case 16:
						{
							num3 = 1;
							num2 = 1;
							num = 1;
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num13 = 0;
							int num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num4 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num5 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num6 = (int)int64Union.int16Val;
							num7 = 0L;
							goto IL_150D;
						}
						case 17:
						{
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num13 = 0;
							int num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num2 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num = (int)int64Union.int16Val;
							num14 = 4;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num3 = (int)int64Union.int16Val;
							num13 += 5;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num4 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num5 = (int)int64Union.int16Val;
							if (num4 > 12 || num5 > 59)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							if (text[num13] == 'A' || text[num13] == 'a')
							{
								if (num4 == 12)
								{
									num4 = 0;
								}
							}
							else
							{
								if (text[num13] != 'P' && text[num13] != 'p')
								{
									throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
								}
								if (num4 != 12)
								{
									num4 += 12;
								}
							}
							num6 = 0;
							num7 = 0L;
							goto IL_150D;
						}
						case 18:
						{
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num13 = 0;
							int num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num2 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num = (int)int64Union.int16Val;
							num14 = 4;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num3 = (int)int64Union.int16Val;
							num4 = 0;
							num5 = 0;
							num6 = 0;
							num7 = 0L;
							goto IL_150D;
						}
						case 19:
						{
							num3 = 1;
							num2 = 1;
							num = 1;
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num13 = 0;
							int num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num4 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num5 = (int)int64Union.int16Val;
							if (num4 <= 12)
							{
							}
							if (text[num13] == 'A' || text[num13] == 'a')
							{
								if (num4 == 12)
								{
									num4 = 0;
								}
							}
							else
							{
								if (text[num13] != 'P' && text[num13] != 'p')
								{
									throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
								}
								if (num4 != 12)
								{
									num4 += 12;
								}
							}
							num6 = 0;
							num7 = 0L;
							goto IL_150D;
						}
						case 20:
						{
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num13 = 0;
							int num14 = 4;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 5;
							num3 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num2 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num = (int)int64Union.int16Val;
							num13 += 3;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num4 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num5 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num6 = (int)int64Union.int16Val;
							num7 = 0L;
							goto IL_150D;
						}
						case 21:
						{
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num13 = 0;
							int num14 = 4;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 5;
							num3 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num2 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num = (int)int64Union.int16Val;
							num4 = 0;
							num5 = 0;
							num6 = 0;
							num7 = 0L;
							goto IL_150D;
						}
						case 22:
						case 31:
						{
							num3 = 1;
							num2 = 1;
							num = 1;
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num13 = 0;
							int num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num4 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num5 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num6 = (int)int64Union.int16Val;
							num7 = 0L;
							goto IL_150D;
						}
						case 23:
						{
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num13 = 0;
							int num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num2 = (int)int64Union.int16Val;
							num14 = 4;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 5;
							num3 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num4 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num5 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num6 = (int)int64Union.int16Val;
							num7 = 0L;
							goto IL_150D;
						}
						case 24:
						{
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num13 = 0;
							int num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num2 = (int)int64Union.int16Val;
							num14 = 4;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num3 = (int)int64Union.int16Val;
							num4 = 0;
							num5 = 0;
							num6 = 0;
							num7 = 0L;
							goto IL_150D;
						}
						case 25:
						{
							num3 = 1;
							num2 = 1;
							num = 1;
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num13 = 0;
							int num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num4 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num5 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num6 = (int)int64Union.int16Val;
							num7 = 0L;
							goto IL_150D;
						}
						case 26:
						case 34:
						{
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num15 = text.Length;
							int num13 = 0;
							int num14 = 4;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 5;
							num3 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num2 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num = (int)int64Union.int16Val;
							num4 = 0;
							num5 = 0;
							num6 = 0;
							num7 = 0L;
							if (num15 < 13)
							{
								goto IL_150D;
							}
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num4 = (int)int64Union.int16Val;
							if (num4 == 24)
							{
								num4 = 0;
								flag = true;
							}
							if (num15 < 16)
							{
								goto IL_150D;
							}
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num5 = (int)int64Union.int16Val;
							if (flag && num5 != 0)
							{
								throw new CustomHISException(SR.InvalidEditedDate("minutes"));
							}
							if (num15 < 19)
							{
								goto IL_150D;
							}
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num6 = (int)int64Union.int16Val;
							if (flag && num6 != 0)
							{
								throw new CustomHISException(SR.InvalidEditedDate("seconds"));
							}
							if (num15 < 21)
							{
								goto IL_150D;
							}
							if (num15 > 26)
							{
								num15 = 26;
							}
							num14 = num15 - 20;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num7 = (long)int64Union.int32Val * 10L;
							if (flag && num7 != 0L)
							{
								throw new CustomHISException(SR.InvalidEditedDate("milliseconds"));
							}
							goto IL_150D;
						}
						case 27:
						{
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num14 = 2;
							int num13 = 0;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num2 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							if (int64Union.int16Val > 39 && int64Union.int16Val <= 99)
							{
								num3 = (int)(int64Union.int16Val + 1900);
							}
							else
							{
								num3 = (int)(int64Union.int16Val + 2000);
							}
							num4 = 0;
							num5 = 0;
							num6 = 0;
							num7 = 0L;
							goto IL_150D;
						}
						case 28:
						{
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num14 = 2;
							int num13 = 0;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num2 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							if (int64Union.int16Val > 39 && int64Union.int16Val <= 99)
							{
								num3 = (int)(int64Union.int16Val + 1900);
							}
							else
							{
								num3 = (int)(int64Union.int16Val + 2000);
							}
							num4 = 0;
							num5 = 0;
							num6 = 0;
							num7 = 0L;
							goto IL_150D;
						}
						case 29:
						{
							if (this.ConvertDateStringToUnicode(out text, ref *ptr2, ResultLen) != 0)
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							int num13 = 0;
							int num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							if (int64Union.int16Val > 39 && int64Union.int16Val <= 99)
							{
								num3 = (int)(int64Union.int16Val + 1900);
							}
							else
							{
								num3 = (int)(int64Union.int16Val + 2000);
							}
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num13 += 3;
							num2 = (int)int64Union.int16Val;
							num14 = 2;
							int64Union.int32Val = this.ConvertDateStringToInt32(text, num14, num13);
							num = (int)int64Union.int16Val;
							num4 = 0;
							num5 = 0;
							num6 = 0;
							num7 = 0L;
							goto IL_150D;
						}
						case 30:
						{
							encoding.CvtEncoding = encoding.GetEncoding(13, 0, 2, 1, 1, 1, 0, 0, 0, 0);
							int num9 = 2;
							if (!this.ConvertZonedDec(ptr2, encoding, num9, 240, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							ptr2 += num9 + 1;
							if (int64Union.int16Val > 39 && int64Union.int16Val <= 99)
							{
								num3 = (int)(int64Union.int16Val + 1900);
							}
							else
							{
								num3 = (int)(int64Union.int16Val + 2000);
							}
							encoding.CvtEncoding = encoding.GetEncoding(13, 0, 3, 1, 1, 1, 0, 0, 0, 0);
							num9 = 3;
							if (!this.ConvertZonedDec(ptr2, encoding, num9, 240, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							short num10 = int64Union.int16Val;
							short num11 = 0;
							if (num3 % 4 == 0)
							{
								num11 = 1;
							}
							if (num3 % 100 == 0)
							{
								num11 = 0;
							}
							if (num3 % 400 == 0)
							{
								num11 = 1;
							}
							short num12 = 0;
							while (num10 > BasePrimitiveConverter.x_sDaysOfMonth[(int)num12])
							{
								num10 -= BasePrimitiveConverter.x_sDaysOfMonth[(int)num12];
								if (1 == num12 && num10 > 0)
								{
									num10 -= num11;
									if (num10 == 0)
									{
										num10 = 29;
										break;
									}
								}
								num12 += 1;
							}
							num2 = (int)(num12 + 1);
							num = (int)num10;
							num4 = 0;
							num5 = 0;
							num6 = 0;
							num7 = 0L;
							goto IL_150D;
						}
						case 32:
						{
							encoding.CvtEncoding = encoding.GetEncoding(13, 0, 4, 1, 1, 1, 0, 0, 0, 0);
							int num9 = 4;
							if (!this.ConvertZonedDec(ptr2, encoding, num9, 240, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							ptr2 += num9 + 1;
							num3 = (int)int64Union.int16Val;
							encoding.CvtEncoding = encoding.GetEncoding(13, 0, 3, 1, 1, 1, 0, 0, 0, 0);
							num9 = 3;
							if (!this.ConvertZonedDec(ptr2, encoding, num9, 240, ref int64Union.int64Val))
							{
								throw new CustomHISException(SR.DateConvertError2, 1528, this.UserCompatibleErrorCode);
							}
							short num10 = int64Union.int16Val;
							short num11 = 0;
							if (num3 % 4 == 0)
							{
								num11 = 1;
							}
							if (num3 % 100 == 0)
							{
								num11 = 0;
							}
							if (num3 % 400 == 0)
							{
								num11 = 1;
							}
							short num12 = 0;
							while (num10 > BasePrimitiveConverter.x_sDaysOfMonth[(int)num12])
							{
								num10 -= BasePrimitiveConverter.x_sDaysOfMonth[(int)num12];
								if (1 == num12 && num10 > 0)
								{
									num10 -= num11;
									if (num10 == 0)
									{
										num10 = 29;
										break;
									}
								}
								num12 += 1;
							}
							num2 = (int)(num12 + 1);
							num = (int)num10;
							num4 = 0;
							num5 = 0;
							num6 = 0;
							num7 = 0L;
							goto IL_150D;
						}
						}
						throw new CustomHISException(SR.UnsupportedCharConversion, 1582, this.UserCompatibleErrorCode);
						IL_150D:;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				encoding.CvtEncoding = cvtEncoding;
				ReturnedDateTime = new DateTime(num3, num2, num, num4, num5, num6, 0).AddTicks(num7);
				if (flag)
				{
					DateTime dateTime = ReturnedDateTime.AddDays(1.0);
					ReturnedDateTime = dateTime;
				}
				RemainingBufferDataLength = num8;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x00082F34 File Offset: 0x00081134
		private int ConvertDateStringToInt32(string InString, int ConvertLen, int StartPosition)
		{
			int num = 0;
			for (int i = StartPosition; i < ConvertLen + StartPosition; i++)
			{
				if (InString[i] < '0' || InString[i] > '9')
				{
					throw new CustomHISException(SR.DateConvertError4);
				}
				num = (int)(InString[i] ^ '0') + num * 10;
			}
			return num;
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x00082F84 File Offset: 0x00081184
		private int ConvertDateStringToUnicode(out string ReturnedString, ref byte Buffer, int CharCount)
		{
			int num = CharCount;
			CEDAR_TYPE_ENCODING notPaddedDefaultEncodingBstr = CEDAR_TYPE_ENCODING.NotPaddedDefaultEncodingBstr;
			ReturnedString = null;
			return this.UnpackString(ref Buffer, ref ReturnedString, ref num, CharCount, false, CharCount, notPaddedDefaultEncodingBstr);
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x00082FAC File Offset: 0x000811AC
		public virtual bool UnpackEditedTimeSpan(ref byte ForeignTimeSpanValue, out TimeSpan ReturnedTimeSpan, ref int RemainingBufferLength, int InputDataLength, string EditPattern, int CodePage)
		{
			int num;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				num = CodePage;
			}
			else
			{
				num = this.codePageInfo.codePage;
			}
			this.SetCodePage(CodePage);
			bool flag = this.UnpackEditedTimeSpan(ref ForeignTimeSpanValue, out ReturnedTimeSpan, ref RemainingBufferLength, InputDataLength, EditPattern, false);
			this.SetCodePage(num);
			return flag;
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x00082FF0 File Offset: 0x000811F0
		public virtual bool UnpackEditedTimeSpan(ref byte ForeignTimeSpanValue, out TimeSpan ReturnedTimeSpan, ref int RemainingBufferLength, int InputDataLength, string EditPattern, int CodePage, bool AllowTruncatedFractionalSeconds)
		{
			int num;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				num = CodePage;
			}
			else
			{
				num = this.codePageInfo.codePage;
			}
			this.SetCodePage(CodePage);
			bool flag = this.UnpackEditedTimeSpan(ref ForeignTimeSpanValue, out ReturnedTimeSpan, ref RemainingBufferLength, InputDataLength, EditPattern, AllowTruncatedFractionalSeconds);
			this.SetCodePage(num);
			return flag;
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x00083033 File Offset: 0x00081233
		public virtual bool UnpackEditedTimeSpan(ref byte ForeignTimeSpanValue, out TimeSpan ReturnedTimeSpan, ref int RemainingBufferLength, int InputDataLength, string EditPattern)
		{
			return this.UnpackEditedTimeSpan(ref ForeignTimeSpanValue, out ReturnedTimeSpan, ref RemainingBufferLength, InputDataLength, EditPattern, false);
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x00083044 File Offset: 0x00081244
		public virtual bool UnpackEditedTimeSpan(ref byte ForeignTimeSpanValue, out TimeSpan ReturnedTimeSpan, ref int RemainingBufferLength, int InputDataLength, string EditPattern, bool AllowTruncatedFractionalSeconds)
		{
			CEDAR_TYPE_ENCODING notPaddedDefaultEncodingBstr = CEDAR_TYPE_ENCODING.NotPaddedDefaultEncodingBstr;
			string text = null;
			this.UnpackString(ref ForeignTimeSpanValue, ref text, ref RemainingBufferLength, InputDataLength, false, InputDataLength, notPaddedDefaultEncodingBstr);
			return this.ExtractTimeSpan(text, EditPattern, AllowTruncatedFractionalSeconds, out ReturnedTimeSpan);
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x00083078 File Offset: 0x00081278
		public virtual bool UnpackEditedTimeSpan(string TimeSpanValue, out TimeSpan ReturnedTimeSpan, string EditPattern)
		{
			return this.UnpackEditedTimeSpan(TimeSpanValue, out ReturnedTimeSpan, EditPattern, false);
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x00083084 File Offset: 0x00081284
		public virtual bool UnpackEditedTimeSpan(string TimeSpanValue, out TimeSpan ReturnedTimeSpan, string EditPattern, bool AllowTruncatedFractionalSeconds)
		{
			return this.ExtractTimeSpan(TimeSpanValue, EditPattern, AllowTruncatedFractionalSeconds, out ReturnedTimeSpan);
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x00083094 File Offset: 0x00081294
		public virtual bool UnpackEditedDateTime(ref byte ForeignDateTimeValue, out DateTime ReturnedDateTime, ref int RemainingBufferLength, int InputDataLength, string EditPattern, int CodePage)
		{
			int num;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				num = CodePage;
			}
			else
			{
				num = this.codePageInfo.codePage;
			}
			this.SetCodePage(CodePage);
			bool flag = this.UnpackEditedDateTime(ref ForeignDateTimeValue, out ReturnedDateTime, ref RemainingBufferLength, InputDataLength, EditPattern, false);
			this.SetCodePage(num);
			return flag;
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000830D8 File Offset: 0x000812D8
		public virtual bool UnpackEditedDateTime(ref byte ForeignDateTimeValue, out DateTime ReturnedDateTime, ref int RemainingBufferLength, int InputDataLength, string EditPattern, int CodePage, bool AllowTruncatedFractionalSeconds)
		{
			int num;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				num = CodePage;
			}
			else
			{
				num = this.codePageInfo.codePage;
			}
			this.SetCodePage(CodePage);
			bool flag = this.UnpackEditedDateTime(ref ForeignDateTimeValue, out ReturnedDateTime, ref RemainingBufferLength, InputDataLength, EditPattern, AllowTruncatedFractionalSeconds);
			this.SetCodePage(num);
			return flag;
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x0008311B File Offset: 0x0008131B
		public virtual bool UnpackEditedDateTime(ref byte ForeignDateTimeValue, out DateTime ReturnedDateTime, ref int RemainingBufferLength, int InputDataLength, string EditPattern)
		{
			return this.UnpackEditedDateTime(ref ForeignDateTimeValue, out ReturnedDateTime, ref RemainingBufferLength, InputDataLength, EditPattern, false);
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x0008312C File Offset: 0x0008132C
		public virtual bool UnpackEditedDateTime(ref byte ForeignDateTimeValue, out DateTime ReturnedDateTime, ref int RemainingBufferLength, int InputDataLength, string EditPattern, bool AllowTruncatedFractionalSeconds)
		{
			CEDAR_TYPE_ENCODING notPaddedDefaultEncodingBstr = CEDAR_TYPE_ENCODING.NotPaddedDefaultEncodingBstr;
			string text = null;
			this.UnpackString(ref ForeignDateTimeValue, ref text, ref RemainingBufferLength, InputDataLength, false, InputDataLength, notPaddedDefaultEncodingBstr);
			return this.ExtractDate(text, EditPattern, AllowTruncatedFractionalSeconds, out ReturnedDateTime);
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x00083160 File Offset: 0x00081360
		public virtual bool UnpackEditedDateTime(string DateTimeValue, out DateTime ReturnedDateTime, string EditPattern)
		{
			return this.UnpackEditedDateTime(DateTimeValue, out ReturnedDateTime, EditPattern, false);
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x0008316C File Offset: 0x0008136C
		public virtual bool UnpackEditedDateTime(string DateTimeValue, out DateTime ReturnedDateTime, string EditPattern, bool AllowTruncatedFractionalSeconds)
		{
			return this.ExtractDate(DateTimeValue, EditPattern, AllowTruncatedFractionalSeconds, out ReturnedDateTime);
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x0008317C File Offset: 0x0008137C
		public unsafe virtual int PackString(string StringValue, ref byte Buffer, ref int cumulativePackedDataLength, int clCharCount, bool fDataIsVariable, int ResultLen, CEDAR_TYPE_ENCODING encoding, int CodePage)
		{
			if (string.IsNullOrEmpty(StringValue))
			{
				return this.PackString(null, 0, ref Buffer, ref cumulativePackedDataLength, clCharCount, fDataIsVariable, ResultLen, encoding, CodePage);
			}
			char* ptr = StringValue;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return this.PackString(ptr, StringValue.Length, ref Buffer, ref cumulativePackedDataLength, clCharCount, fDataIsVariable, ResultLen, encoding, CodePage);
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000831D0 File Offset: 0x000813D0
		public unsafe virtual int PackString(char[] characters, int numberOfCharactersInput, ref byte Buffer, ref int cumulativePackedDataLength, int clCharCount, bool fDataIsVariable, int ResultLen, CEDAR_TYPE_ENCODING encoding, int CodePage)
		{
			if (characters == null)
			{
				return this.PackString(null, 0, ref Buffer, ref cumulativePackedDataLength, clCharCount, fDataIsVariable, ResultLen, encoding, CodePage);
			}
			fixed (char* ptr = &characters[0])
			{
				char* ptr2 = ptr;
				return this.PackString(ptr2, numberOfCharactersInput, ref Buffer, ref cumulativePackedDataLength, clCharCount, fDataIsVariable, ResultLen, encoding, CodePage);
			}
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x00083218 File Offset: 0x00081418
		private unsafe int PackString(char* characters, int numberOfCharactersInput, ref byte Buffer, ref int cumulativePackedDataLength, int clCharCount, bool fDataIsVariable, int ResultLen, CEDAR_TYPE_ENCODING encoding, int CodePage)
		{
			int num;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				num = CodePage;
			}
			else
			{
				num = this.codePageInfo.codePage;
			}
			this.SetCodePage(CodePage);
			int num2 = this.PackString(characters, numberOfCharactersInput, ref Buffer, ref cumulativePackedDataLength, clCharCount, fDataIsVariable, ResultLen, encoding);
			this.SetCodePage(num);
			return num2;
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x00083260 File Offset: 0x00081460
		public unsafe virtual int PackString(string StringValue, ref byte Buffer, ref int cumulativePackedDataLength, int clCharCount, bool fDataIsVariable, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			if (string.IsNullOrEmpty(StringValue))
			{
				return this.PackString(null, 0, ref Buffer, ref cumulativePackedDataLength, clCharCount, fDataIsVariable, ResultLen, encoding);
			}
			char* ptr = StringValue;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return this.PackString(ptr, StringValue.Length, ref Buffer, ref cumulativePackedDataLength, clCharCount, fDataIsVariable, ResultLen, encoding);
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x000832B0 File Offset: 0x000814B0
		public unsafe virtual int PackString(char[] characters, int numberOfCharactersInput, ref byte Buffer, ref int cumulativePackedDataLength, int clCharCount, bool fDataIsVariable, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			if (characters == null)
			{
				return this.PackString(null, 0, ref Buffer, ref cumulativePackedDataLength, clCharCount, fDataIsVariable, ResultLen, encoding);
			}
			fixed (char* ptr = &characters[0])
			{
				char* ptr2 = ptr;
				return this.PackString(ptr2, numberOfCharactersInput, ref Buffer, ref cumulativePackedDataLength, clCharCount, fDataIsVariable, ResultLen, encoding);
			}
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x000832F4 File Offset: 0x000814F4
		private unsafe int PackString(char* characters, int numberOfCharactersInput, ref byte Buffer, ref int cumulativePackedDataLength, int clCharCount, bool fDataIsVariable, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			int num = 0;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				this.SetCodePage(37);
			}
			if (this.codePageInfo.isDBCS)
			{
				ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
				if (nCvtType != ConvertedDataType.CEDAR_COBOL_PICXTRAN)
				{
					if (nCvtType != ConvertedDataType.CEDAR_COBOL_PICGTRAN)
					{
						switch (nCvtType)
						{
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_VAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_VAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_LONGVAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_LONGVAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_MIXED_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_MIXED_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_MIXED_VAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_MIXED_VAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_MIXED_LONGVAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_MIXED_LONGVAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NOTPADDED_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_NOTPADDED_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_MIXED_NOTPADDED_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_MIXED_NOTPADDED_PICXTRAN:
							goto IL_00CA;
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICGTRAN:
						case ConvertedDataType.CEDAR_COBOL_VAR_PICGTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_VAR_PICGTRAN:
						case ConvertedDataType.CEDAR_COBOL_LONGVAR_PICGTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_LONGVAR_PICGTRAN:
							break;
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP918:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM:
						case (ConvertedDataType)44:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_ISO_DATE:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_ISO_TIME:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_USA_DATE:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_USA_TIME:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_JIS_DATE:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_JIS_TIME:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_EUR_DATE:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_EUR_TIME:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICXNOTRAN:
						case ConvertedDataType.CEDAR_NUMERIC_EDITED:
						case ConvertedDataType.CEDAR_EXTERNAL_FLOAT:
							return num;
						default:
							return num;
						}
					}
					return this.PackDBCS_String(characters, numberOfCharactersInput, ref Buffer, ref cumulativePackedDataLength, clCharCount, fDataIsVariable, ResultLen, encoding);
				}
				IL_00CA:
				num = this.PackMIXED_String(characters, numberOfCharactersInput, ref Buffer, ref cumulativePackedDataLength, clCharCount, fDataIsVariable, ResultLen, encoding);
			}
			else
			{
				num = this.PackSBCS_String(characters, numberOfCharactersInput, ref Buffer, ref cumulativePackedDataLength, clCharCount, fDataIsVariable, ResultLen, encoding);
			}
			return num;
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x0008340C File Offset: 0x0008160C
		private unsafe int HandleAsIs(char* characters, int numberOfCharactersInput, ref byte buffer, ref int cumulativePackedDataLength, int byteCount, bool dataIsVariable, int sizeOfBufferInBytes, bool doubleByteStart, bool automaticShiftInShiftOut)
		{
			bool flag = false;
			if (characters == null)
			{
				return 0;
			}
			int num = byteCount;
			bool flag2 = true;
			int num2 = sizeOfBufferInBytes - cumulativePackedDataLength;
			if (num2 < num)
			{
				flag2 = false;
				num = num2;
			}
			fixed (byte* ptr = &buffer)
			{
				byte* ptr2 = ptr;
				this.codePageInfo.EncodingIBM.DoubleByteStart = doubleByteStart;
				this.codePageInfo.EncodingIBM.AutomaticShiftInShiftOut = automaticShiftInShiftOut;
				this.codePageInfo.EncoderIBM.Reset();
				int num3;
				int num4;
				this.codePageInfo.EncoderIBM.Convert(characters, numberOfCharactersInput, ptr2, num, true, out num3, out num4, out flag);
				if (flag)
				{
					int num5 = num4;
					ptr = null;
					cumulativePackedDataLength += num5;
					return 0;
				}
				if (flag2)
				{
					throw new CustomHISException(SR.CharStringTooBig, 1513, this.UserCompatibleErrorCode);
				}
				return 1500;
			}
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x000834CC File Offset: 0x000816CC
		private unsafe int HandleEmptyString(ref byte buffer, ref int cumulativePackedDataLength, int byteCount, bool dataIsVariable, int sizeOfBufferInBytes, Padding padAction, bool singleByteBlanks, UseOfSOSI insertOption)
		{
			int num = 0;
			int num2 = 0;
			if (insertOption == UseOfSOSI.IncludeSOSI)
			{
				num2 = 2;
			}
			int num3 = sizeOfBufferInBytes - cumulativePackedDataLength;
			if (!dataIsVariable)
			{
				if (num3 - num2 < byteCount)
				{
					return 1500;
				}
				num = byteCount;
			}
			else if (num3 < num2)
			{
				return 1500;
			}
			fixed (byte* ptr = &buffer)
			{
				byte* ptr2 = ptr;
				if (insertOption == UseOfSOSI.IncludeSOSI)
				{
					*ptr2 = 14;
					ptr2++;
				}
				if (!dataIsVariable)
				{
					if (padAction == Padding.Padded || !singleByteBlanks)
					{
						if (singleByteBlanks)
						{
							this.SetMemory(ptr2, this.codePageInfo.fixedEBCDIC.SPACE, (long)byteCount);
						}
						else
						{
							for (int i = 0; i < byteCount; i += 2)
							{
								ptr2[i] = this.codePageInfo.fixedEBCDIC.DBCS_SPACE_Byte0;
								ptr2[i + 1] = this.codePageInfo.fixedEBCDIC.DBCS_SPACE_Byte1;
							}
						}
					}
					else
					{
						*ptr2 = 0;
					}
					ptr2 += byteCount;
				}
				if (insertOption == UseOfSOSI.IncludeSOSI)
				{
					*ptr2 = 15;
				}
				cumulativePackedDataLength += num + num2;
			}
			return 0;
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x000835AC File Offset: 0x000817AC
		private unsafe int PackSBCS_String(char* characters, int numberOfCharactersInput, ref byte buffer, ref int cumulativePackedDataLength, int characterCount, bool dataIsVariable, int sizeOfBufferInBytes, CEDAR_TYPE_ENCODING encoding)
		{
			bool flag = false;
			int num = 0;
			bool nAsIs = encoding.nAsIs != 0;
			try
			{
				if (nAsIs)
				{
					return this.HandleAsIs(characters, numberOfCharactersInput, ref buffer, ref cumulativePackedDataLength, characterCount, dataIsVariable, sizeOfBufferInBytes, false, false);
				}
				Padding nPad = (Padding)encoding.nPad;
				if (characters == null || characterCount == 0)
				{
					return this.HandleEmptyString(ref buffer, ref cumulativePackedDataLength, characterCount, dataIsVariable, sizeOfBufferInBytes, nPad, true, UseOfSOSI.NoSOSI);
				}
				OverflowHandling nTRE = (OverflowHandling)encoding.nTRE;
				int num2 = characterCount;
				bool flag2 = true;
				int num3 = sizeOfBufferInBytes - cumulativePackedDataLength;
				if (num3 < characterCount)
				{
					if (!dataIsVariable)
					{
						return 1500;
					}
					flag2 = false;
					num2 = num3;
				}
				if (nTRE == OverflowHandling.Truncate && numberOfCharactersInput > characterCount)
				{
					numberOfCharactersInput = characterCount;
				}
				try
				{
					fixed (byte* ptr = &buffer)
					{
						byte* ptr2 = ptr;
						this.codePageInfo.EncodingIBM.DoubleByteStart = false;
						this.codePageInfo.EncodingIBM.AutomaticShiftInShiftOut = false;
						this.codePageInfo.EncoderIBM.Reset();
						int num4;
						int num5;
						this.codePageInfo.EncoderIBM.Convert(characters, numberOfCharactersInput, ptr2, num2, true, out num4, out num5, out flag);
						if (num5 == 0 && nPad != Padding.Padded)
						{
							throw new CustomHISException(SR.StringTooShortPaddingNotAllowed);
						}
						if (!flag)
						{
							if (!flag2)
							{
								return 1500;
							}
							if (nTRE != OverflowHandling.Truncate)
							{
								throw new CustomHISException(SR.CharStringTooBig, 1513, this.UserCompatibleErrorCode);
							}
						}
						if (num5 != num4)
						{
							throw new CustomHISException(SR.SbcsConversionLengthError(num4, num5));
						}
						if (!dataIsVariable)
						{
							num = characterCount;
						}
						else
						{
							num = num5;
						}
						if (num5 < characterCount && !dataIsVariable)
						{
							if (nPad == Padding.Padded)
							{
								this.SetMemory(ptr2 + num5, this.codePageInfo.fixedEBCDIC.SPACE, (long)(characterCount - num5));
							}
							else
							{
								ptr2[num5] = 0;
							}
						}
					}
				}
				finally
				{
					byte* ptr = null;
				}
				cumulativePackedDataLength += num;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
			return 0;
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x000837C4 File Offset: 0x000819C4
		private unsafe int PackMIXED_String(char* characters, int numberOfCharactersInput, ref byte buffer, ref int cumulativePackedDataLength, int characterCount, bool dataIsVariable, int sizeOfBufferInBytes, CEDAR_TYPE_ENCODING encoding)
		{
			bool nAsIs = encoding.nAsIs != 0;
			try
			{
				if (nAsIs)
				{
					return this.HandleAsIs(characters, numberOfCharactersInput, ref buffer, ref cumulativePackedDataLength, characterCount, dataIsVariable, sizeOfBufferInBytes, false, true);
				}
				Padding nPad = (Padding)encoding.nPad;
				if (characters == null || characterCount == 0)
				{
					return this.HandleEmptyString(ref buffer, ref cumulativePackedDataLength, characterCount, dataIsVariable, sizeOfBufferInBytes, nPad, true, UseOfSOSI.NoSOSI);
				}
				int num = cumulativePackedDataLength;
				try
				{
					fixed (byte* ptr = &buffer)
					{
						byte* ptr2 = ptr;
						OverflowHandling nTRE = (OverflowHandling)encoding.nTRE;
						this.codePageInfo.EncodingIBM.DoubleByteStart = false;
						this.codePageInfo.EncodingIBM.AutomaticShiftInShiftOut = true;
						this.codePageInfo.EncoderIBM.Reset();
						int num2 = this.codePageInfo.EncoderIBM.GetByteCount(characters, numberOfCharactersInput, true);
						int num3 = num2;
						if (num2 == 0 && nPad != Padding.Padded)
						{
							throw new CustomHISException(SR.StringTooShortPaddingNotAllowed);
						}
						bool flag = true;
						if (num2 > characterCount)
						{
							if (nTRE != OverflowHandling.Truncate)
							{
								throw new CustomHISException(SR.CharStringTooBig, 1513, this.UserCompatibleErrorCode);
							}
							num2 = characterCount;
							flag = false;
						}
						if (!dataIsVariable)
						{
							num += characterCount;
						}
						else
						{
							num += num2;
						}
						if (num > sizeOfBufferInBytes)
						{
							return 1500;
						}
						int num9;
						if (!flag)
						{
							float num4 = (float)num3 / (float)characterCount;
							int num5 = (int)((float)numberOfCharactersInput / num4);
							if (num5 == 0)
							{
								num5 = 1;
							}
							this.codePageInfo.EncoderIBM.Reset();
							int num6 = this.codePageInfo.EncoderIBM.GetByteCount(characters, num5, true);
							int num7 = 0;
							int num8 = numberOfCharactersInput;
							while (num6 != characterCount && num7 != num5)
							{
								if (num6 > characterCount)
								{
									num8 = num5;
								}
								else
								{
									num7 = num5;
								}
								num5 = (num8 + num7) / 2;
								this.codePageInfo.EncoderIBM.Reset();
								num6 = this.codePageInfo.EncoderIBM.GetByteCount(characters, num5, true);
							}
							num9 = num5;
						}
						else
						{
							num9 = numberOfCharactersInput;
						}
						int num10 = 0;
						if (num9 != 0)
						{
							bool flag2 = false;
							this.codePageInfo.EncoderIBM.Reset();
							int num11;
							this.codePageInfo.EncoderIBM.Convert(characters, num9, ptr2, characterCount, true, out num11, out num10, out flag2);
						}
						if (num10 < characterCount && !dataIsVariable)
						{
							if (nPad == Padding.Padded)
							{
								this.SetMemory(ptr2 + num10, this.codePageInfo.fixedEBCDIC.SPACE, (long)(characterCount - num10));
							}
							else
							{
								ptr2[num10] = 0;
							}
						}
					}
				}
				finally
				{
					byte* ptr = null;
				}
				cumulativePackedDataLength = num;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
			return 0;
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x00083A5C File Offset: 0x00081C5C
		private unsafe int PackDBCS_String(char* characters, int numberOfCharactersInput, ref byte buffer, ref int cumulativePackedDataLength, int characterCount, bool dataIsVariable, int sizeOfBufferInBytes, CEDAR_TYPE_ENCODING encoding)
		{
			bool flag = false;
			int num = 0;
			int num2 = 0;
			bool nAsIs = encoding.nAsIs != 0;
			try
			{
				int num3 = characterCount * 2;
				if (nAsIs)
				{
					return this.HandleAsIs(characters, numberOfCharactersInput, ref buffer, ref cumulativePackedDataLength, num3, dataIsVariable, sizeOfBufferInBytes, true, false);
				}
				Padding nPad = (Padding)encoding.nPad;
				UseOfSOSI nSOSI = (UseOfSOSI)encoding.nSOSI;
				if (characters == null || characterCount == 0)
				{
					return this.HandleEmptyString(ref buffer, ref cumulativePackedDataLength, num3, dataIsVariable, sizeOfBufferInBytes, nPad, true, nSOSI);
				}
				if (nSOSI == UseOfSOSI.IncludeSOSI)
				{
					num2 = 2;
				}
				OverflowHandling nTRE = (OverflowHandling)encoding.nTRE;
				int num4 = num3;
				bool flag2 = true;
				int num5 = sizeOfBufferInBytes - cumulativePackedDataLength;
				if (num5 - num2 < num3)
				{
					if (!dataIsVariable)
					{
						return 1500;
					}
					flag2 = false;
					num4 = num5;
				}
				if (nTRE == OverflowHandling.Truncate && numberOfCharactersInput > characterCount)
				{
					numberOfCharactersInput = characterCount;
				}
				try
				{
					fixed (byte* ptr = &buffer)
					{
						byte* ptr2 = ptr;
						if (nSOSI == UseOfSOSI.IncludeSOSI)
						{
							if (num4 < 2 && !flag2)
							{
								return 1500;
							}
							*ptr2 = 14;
							ptr2++;
							if (!flag2)
							{
								num4--;
							}
						}
						this.codePageInfo.EncodingIBM.DoubleByteStart = true;
						this.codePageInfo.EncodingIBM.AutomaticShiftInShiftOut = false;
						this.codePageInfo.EncoderIBM.Reset();
						int num6;
						int num7;
						this.codePageInfo.EncoderIBM.Convert(characters, numberOfCharactersInput, ptr2, num4, true, out num6, out num7, out flag);
						if (num7 == 0 && nPad != Padding.Padded)
						{
							throw new CustomHISException(SR.StringTooShortPaddingNotAllowed);
						}
						if (!flag)
						{
							if (!flag2)
							{
								return 1500;
							}
							if (nTRE != OverflowHandling.Truncate)
							{
								throw new CustomHISException(SR.CharStringTooBig, 1513, this.UserCompatibleErrorCode);
							}
						}
						if (!dataIsVariable)
						{
							num = num3;
						}
						else
						{
							num = num7;
						}
						if (dataIsVariable)
						{
							ptr2 += num7;
						}
						else
						{
							if (num7 < num3)
							{
								byte* ptr3 = ptr2 + num7;
								int num8 = num3 - num7;
								for (int i = 0; i < num8; i += 2)
								{
									*ptr3 = this.codePageInfo.fixedEBCDIC.DBCS_SPACE_Byte0;
									ptr3[1] = this.codePageInfo.fixedEBCDIC.DBCS_SPACE_Byte1;
									ptr3 += 2;
								}
							}
							ptr2 += num3;
						}
						if (nSOSI == UseOfSOSI.IncludeSOSI)
						{
							if (sizeOfBufferInBytes - (cumulativePackedDataLength + num) < 2)
							{
								return 1500;
							}
							*ptr2 = 15;
						}
					}
				}
				finally
				{
					byte* ptr = null;
				}
				cumulativePackedDataLength += num + num2;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
			return 0;
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x00083CF0 File Offset: 0x00081EF0
		public virtual int UnpackString(ref byte Buffer, ref string ReturnedString, ref int RemainingBufferDataLength, int clCharCount, bool fDataIsVariable, int ResultLen, CEDAR_TYPE_ENCODING encoding, int CodePage)
		{
			int num;
			if (!BasePrimitiveConverter.isCodePageSet)
			{
				num = CodePage;
			}
			else
			{
				num = this.codePageInfo.codePage;
			}
			this.SetCodePage(CodePage);
			int num2 = this.UnpackString(ref Buffer, ref ReturnedString, ref RemainingBufferDataLength, clCharCount, fDataIsVariable, ResultLen, encoding);
			this.SetCodePage(num);
			return num2;
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x00083D38 File Offset: 0x00081F38
		public virtual int UnpackString(ref byte Buffer, ref string ReturnedString, ref int RemainingBufferDataLength, int clCharCount, bool fDataIsVariable, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			if (this.codePageInfo.isDBCS)
			{
				ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
				if (nCvtType != ConvertedDataType.CEDAR_COBOL_PICXTRAN)
				{
					if (nCvtType != ConvertedDataType.CEDAR_COBOL_PICGTRAN)
					{
						switch (nCvtType)
						{
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_VAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_VAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_LONGVAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_LONGVAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_MIXED_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_MIXED_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_MIXED_VAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_MIXED_VAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_MIXED_LONGVAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_MIXED_LONGVAR_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NOTPADDED_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_NOTPADDED_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_MIXED_NOTPADDED_PICXTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_MIXED_NOTPADDED_PICXTRAN:
							goto IL_00B9;
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICGTRAN:
						case ConvertedDataType.CEDAR_COBOL_VAR_PICGTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_VAR_PICGTRAN:
						case ConvertedDataType.CEDAR_COBOL_LONGVAR_PICGTRAN:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_LONGVAR_PICGTRAN:
							break;
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP918:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM:
						case (ConvertedDataType)44:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_ISO_DATE:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_ISO_TIME:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_USA_DATE:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_USA_TIME:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_JIS_DATE:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_JIS_TIME:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_EUR_DATE:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_EUR_TIME:
						case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICXNOTRAN:
						case ConvertedDataType.CEDAR_NUMERIC_EDITED:
						case ConvertedDataType.CEDAR_EXTERNAL_FLOAT:
							return 0;
						default:
							return 0;
						}
					}
					this.UnpackDBCS_String(ref Buffer, ref ReturnedString, ref RemainingBufferDataLength, clCharCount, fDataIsVariable, encoding);
					return 0;
				}
				IL_00B9:
				this.UnpackMIXED_String(ref Buffer, ref ReturnedString, ref RemainingBufferDataLength, clCharCount, fDataIsVariable, encoding);
			}
			else
			{
				this.UnpackSBCS_String(ref Buffer, ref ReturnedString, ref RemainingBufferDataLength, clCharCount, fDataIsVariable, encoding);
			}
			return 0;
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x00083E30 File Offset: 0x00082030
		private unsafe void UnpackSBCS_String(ref byte Buffer, ref string ReturnedString, ref int RemainingBufferDataLength, int clCharCount, bool fDataIsVariable, CEDAR_TYPE_ENCODING encoding)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			try
			{
				Padding padding = (Padding)(this.alwaysUseNullTerminate ? 1 : encoding.nPad);
				bool flag2 = encoding.nAsIs != 0;
				if (this.convertReceivedStringsAsIs)
				{
					flag2 = true;
				}
				int num3 = RemainingBufferDataLength;
				num3 -= clCharCount;
				int num4;
				if (num3 < 0)
				{
					if (!fDataIsVariable)
					{
						throw new CustomHISException(SR.InputBufferExhausted, 1508, this.UserCompatibleErrorCode);
					}
					num4 = clCharCount + num3;
					num3 = 0;
				}
				else
				{
					num4 = clCharCount;
				}
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						byte b;
						int i;
						if (this.stringsAreNullTerminatedAndSpacePadded && !flag2)
						{
							b = this.codePageInfo.fixedEBCDIC.SPACE;
							i = 0;
							while (i < num4 && ptr2[i] != 0)
							{
								i++;
							}
							if (i > 0)
							{
								for (i--; i >= 0; i--)
								{
									if (ptr2[i] != this.codePageInfo.fixedEBCDIC.SPACE)
									{
										i++;
										break;
									}
								}
							}
							if (i <= 0)
							{
								ReturnedString = "";
								RemainingBufferDataLength = num3;
								return;
							}
						}
						else if (padding == Padding.Padded)
						{
							b = this.codePageInfo.fixedEBCDIC.SPACE;
							i = num4;
							if (i == 0)
							{
								ReturnedString = "";
								RemainingBufferDataLength = num3;
								return;
							}
						}
						else
						{
							b = 0;
							if (flag2)
							{
								i = num4;
							}
							else if (!this.trimTrailingNulls)
							{
								for (i = 0; i < num4; i++)
								{
									if (ptr2[i] == 0)
									{
										break;
									}
								}
							}
							else
							{
								for (i = num4 - 1; i >= 0; i--)
								{
									if (ptr2[i] != 0)
									{
										i++;
										break;
									}
								}
								if (i < 0)
								{
									i++;
								}
							}
							if (i == 0)
							{
								ReturnedString = "";
								RemainingBufferDataLength = num3;
								return;
							}
						}
						short num5 = 0;
						if (!flag2)
						{
							do
							{
								if (ptr2[i - 1] != b)
								{
									num5 = 1;
								}
								else
								{
									i--;
								}
							}
							while (num5 == 0 && i > 0);
							if (num5 == 0 && padding == Padding.Padded)
							{
								ReturnedString = "";
								RemainingBufferDataLength = num3;
								return;
							}
						}
						char[] array = new char[clCharCount];
						this.codePageInfo.EncodingIBM.DoubleByteStart = false;
						this.codePageInfo.EncodingIBM.AutomaticShiftInShiftOut = false;
						this.codePageInfo.EncodingIBM.Truncate = false;
						try
						{
							fixed (char* ptr3 = &array[0])
							{
								char* ptr4 = ptr3;
								this.codePageInfo.DecoderIBM.Convert(ptr2, i, ptr4, i, true, out num2, out num, out flag);
								ReturnedString = new string(array, 0, i);
							}
						}
						finally
						{
							char* ptr3 = null;
						}
					}
				}
				finally
				{
					byte* ptr = null;
				}
				RemainingBufferDataLength = num3;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x0008410C File Offset: 0x0008230C
		private unsafe void UnpackMIXED_String(ref byte Buffer, ref string ReturnedString, ref int RemainingBufferDataLength, int clCharCount, bool fDataIsVariable, CEDAR_TYPE_ENCODING encoding)
		{
			int num = 0;
			byte[] array = new byte[]
			{
				this.codePageInfo.fixedEBCDIC.DBCS_SPACE_Byte0,
				this.codePageInfo.fixedEBCDIC.DBCS_SPACE_Byte1
			};
			int num2 = 0;
			bool flag = false;
			bool flag2 = false;
			try
			{
				Padding padding = (Padding)(this.alwaysUseNullTerminate ? 1 : encoding.nPad);
				bool flag3 = encoding.nAsIs != 0;
				if (this.convertReceivedStringsAsIs)
				{
					flag3 = true;
				}
				int num3 = RemainingBufferDataLength;
				num3 -= clCharCount;
				int num4;
				if (num3 < 0)
				{
					if (!fDataIsVariable)
					{
						throw new CustomHISException(SR.InputBufferExhausted, 1508, this.UserCompatibleErrorCode);
					}
					num4 = clCharCount + num3;
					num3 = 0;
				}
				else
				{
					num4 = clCharCount;
				}
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						byte b;
						int i;
						if (this.stringsAreNullTerminatedAndSpacePadded && !flag3)
						{
							b = this.codePageInfo.fixedEBCDIC.SPACE;
							i = 0;
							while (i < num4 && ptr2[i] != 0)
							{
								i++;
							}
							if (i > 0)
							{
								for (i--; i >= 0; i--)
								{
									if (ptr2[i] != this.codePageInfo.fixedEBCDIC.SPACE)
									{
										i++;
										break;
									}
								}
							}
							if (i <= 0)
							{
								ReturnedString = "";
								RemainingBufferDataLength = num3;
								return;
							}
						}
						else if (padding == Padding.Padded)
						{
							b = this.codePageInfo.fixedEBCDIC.SPACE;
							i = num4;
							if (i == 0)
							{
								ReturnedString = "";
								RemainingBufferDataLength = num3;
								return;
							}
						}
						else
						{
							b = 0;
							if (flag3)
							{
								i = num4;
							}
							else
							{
								array[0] = this.codePageInfo.fixedEBCDIC.DBCS_NULL_Byte0;
								array[1] = this.codePageInfo.fixedEBCDIC.DBCS_NULL_Byte1;
								if (!this.trimTrailingNulls)
								{
									for (i = 0; i < num4; i++)
									{
										if (ptr2[i] == 0)
										{
											break;
										}
									}
								}
								else
								{
									for (i = num4 - 1; i >= 0; i--)
									{
										if (ptr2[i] != 0)
										{
											i++;
											break;
										}
									}
									if (i < 0)
									{
										i++;
									}
								}
								if (i == 0)
								{
									ReturnedString = "";
									RemainingBufferDataLength = num3;
									return;
								}
							}
						}
						short num5 = 0;
						if (!flag3)
						{
							for (;;)
							{
								if (ptr2[i - 1] == 15)
								{
									flag2 = true;
									i--;
								}
								if (ptr2[i - 1] == 14)
								{
									flag2 = false;
									i--;
								}
								if (!flag2)
								{
									if (ptr2[i - 1] != b)
									{
										num5 = 1;
									}
									else
									{
										i--;
									}
								}
								else
								{
									if (i < 1)
									{
										break;
									}
									if (ptr2[i - 2] != array[0] && ptr2[i - 1] != array[1])
									{
										num5 = 1;
										i++;
									}
									else
									{
										i -= 2;
									}
								}
								if (num5 != 0 || i <= 0)
								{
									goto IL_0261;
								}
							}
							throw new CustomHISException(SR.InvalidDBCSString, 1581, this.UserCompatibleErrorCode);
						}
						IL_0261:
						if (flag2 && i < num4 && !flag3)
						{
							ptr2[i - 1] = 15;
						}
						if (num5 == 0 && padding == Padding.Padded && !flag3)
						{
							ReturnedString = "";
							RemainingBufferDataLength = num3;
							return;
						}
						this.codePageInfo.EncodingIBM.DoubleByteStart = false;
						this.codePageInfo.EncodingIBM.AutomaticShiftInShiftOut = true;
						this.codePageInfo.EncodingIBM.Truncate = false;
						num = this.codePageInfo.EncodingIBM.GetCharCount(ptr2, clCharCount);
						if (num > 0)
						{
							char[] array2 = new char[num];
							try
							{
								fixed (char* ptr3 = &array2[0])
								{
									char* ptr4 = ptr3;
									this.codePageInfo.DecoderIBM.Convert(ptr2, i, ptr4, num, true, out num2, out num, out flag);
									ReturnedString = new string(array2, 0, num);
									goto IL_032A;
								}
							}
							finally
							{
								char* ptr3 = null;
							}
						}
						ReturnedString = "";
						IL_032A:;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				RemainingBufferDataLength = num3;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000844C8 File Offset: 0x000826C8
		private unsafe void UnpackDBCS_String(ref byte Buffer, ref string ReturnedString, ref int RemainingBufferDataLength, int clCharCount, bool fDataIsVariable, CEDAR_TYPE_ENCODING encoding)
		{
			int num = 0;
			int num2 = clCharCount * 2;
			byte[] array = new byte[]
			{
				this.codePageInfo.fixedEBCDIC.DBCS_SPACE_Byte0,
				this.codePageInfo.fixedEBCDIC.DBCS_SPACE_Byte1
			};
			int num3 = 0;
			bool flag = false;
			try
			{
				Padding padding = (Padding)(this.alwaysUseNullTerminate ? 1 : encoding.nPad);
				UseOfSOSI nSOSI = (UseOfSOSI)encoding.nSOSI;
				bool flag2 = encoding.nAsIs != 0;
				if (this.convertReceivedStringsAsIs)
				{
					flag2 = true;
				}
				int num4 = RemainingBufferDataLength;
				int num5;
				if (nSOSI == UseOfSOSI.IncludeSOSI && !flag2)
				{
					num5 = 2;
				}
				else
				{
					num5 = 0;
				}
				num4 = num4 - num2 - num5;
				int num6;
				if (num4 < 0)
				{
					if (!fDataIsVariable)
					{
						throw new CustomHISException(SR.InputBufferExhausted, 1508, this.UserCompatibleErrorCode);
					}
					if (num4 == -num5)
					{
						ReturnedString = "";
						RemainingBufferDataLength = num4;
						return;
					}
					num6 = RemainingBufferDataLength - num5;
				}
				else
				{
					num6 = num2;
				}
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						if (nSOSI == UseOfSOSI.IncludeSOSI && !flag2)
						{
							ptr2++;
						}
						int num7 = num6;
						if (padding == Padding.Padded && !flag2)
						{
							if (num6 == 0)
							{
								ReturnedString = "";
								RemainingBufferDataLength = num4;
								return;
							}
						}
						else
						{
							array[0] = this.codePageInfo.fixedEBCDIC.DBCS_NULL_Byte0;
							array[1] = this.codePageInfo.fixedEBCDIC.DBCS_NULL_Byte1;
						}
						short num8 = 0;
						if (!flag2)
						{
							do
							{
								if (ptr2[num7 - 2] != array[0] && ptr2[num7 - 1] != array[1])
								{
									num8 = 1;
								}
								else
								{
									num7 -= 2;
								}
							}
							while (num8 == 0 && num7 > 0);
						}
						if (num8 == 0 && padding == Padding.Padded && !flag2)
						{
							ReturnedString = "";
							RemainingBufferDataLength = num4;
							return;
						}
						this.codePageInfo.EncodingIBM.DoubleByteStart = true;
						this.codePageInfo.EncodingIBM.AutomaticShiftInShiftOut = false;
						this.codePageInfo.EncodingIBM.Truncate = false;
						num = this.codePageInfo.EncodingIBM.GetCharCount(ptr2, num7);
						if (num > 0)
						{
							char[] array2 = new char[num];
							try
							{
								fixed (char* ptr3 = &array2[0])
								{
									char* ptr4 = ptr3;
									this.codePageInfo.DecoderIBM.Convert(ptr2, num7, ptr4, num, true, out num3, out num, out flag);
									ReturnedString = new string(array2, 0, num);
									goto IL_0216;
								}
							}
							finally
							{
								char* ptr3 = null;
							}
						}
						ReturnedString = "";
						IL_0216:;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				RemainingBufferDataLength = num4;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x00084770 File Offset: 0x00082970
		public virtual void PackDecimal(decimal DecimalValue, ref byte Buffer, ref int CumulativePackedLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine)
		{
			this.FormatNumber(ref Buffer, DecimalValue, encoding, NumericEditStatemachine, ref CumulativePackedLength);
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x00084784 File Offset: 0x00082984
		public unsafe virtual void PackDecimal(decimal DecimalValue, ref byte Buffer, ref int cumulativePackedDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			BasePrimitiveConverter.Int16Union int16Union = default(BasePrimitiveConverter.Int16Union);
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union2 = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union3 = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.DecimalStruct decimalStruct = default(BasePrimitiveConverter.DecimalStruct);
			try
			{
				int num = ResultLen + cumulativePackedDataLength;
				int[] bits = decimal.GetBits(DecimalValue);
				for (int i = 0; i < 4; i++)
				{
					*((ref decimalStruct.decIntValues.FixedElementField) + (IntPtr)i * 4) = bits[i];
				}
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
						if (nCvtType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							switch (nCvtType)
							{
							case ConvertedDataType.CEDAR_COBOL_COMP94:
								goto IL_08B2;
							case ConvertedDataType.CEDAR_COBOL_COMP99:
								goto IL_0A7A;
							case ConvertedDataType.CEDAR_COBOL_COMP3:
								break;
							default:
								if (nCvtType == ConvertedDataType.CEDAR_COBOL_COMP918)
								{
									goto IL_0C6A;
								}
								if (nCvtType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
								{
									goto IL_0EBB;
								}
								goto IL_04C7;
							}
						}
						else
						{
							switch (nCvtType)
							{
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
								goto IL_08B2;
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
								goto IL_0A7A;
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
								break;
							case ConvertedDataType.CEDAR_IEEE_NULLABLE_COMP1:
							case ConvertedDataType.CEDAR_IEEE_NULLABLE_COMP2:
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICXTRAN:
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICGTRAN:
								goto IL_0EBB;
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP918:
								goto IL_0C6A;
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM:
								goto IL_04C7;
							default:
								if (nCvtType == ConvertedDataType.CEDAR_DECIMAL_FLOAT_64)
								{
									BasePrimitiveConverter.SetDecimalFloatingPoint8(decimalStruct, ptr2);
									goto IL_0ECB;
								}
								if (nCvtType != ConvertedDataType.CEDAR_DECIMAL_FLOAT_128)
								{
									goto IL_0EBB;
								}
								BasePrimitiveConverter.SetDecimalFloatingPoint16(decimalStruct, ptr2);
								goto IL_0ECB;
							}
						}
						for (int j = 0; j < ResultLen; j++)
						{
							ptr2[j] = 0;
						}
						byte b;
						if (decimalStruct.sign == 0)
						{
							b = 12;
						}
						else
						{
							if (encoding.nSign == 1)
							{
								throw new CustomHISException(SR.UnsignedWindows);
							}
							b = 13;
						}
						if (encoding.nSign != 0)
						{
							b = 15;
						}
						int num2 = (int)encoding.nPrecision;
						int num3 = (int)encoding.nTRE;
						int64Union.int64Val = decimalStruct.Lo64;
						int64Union2.int64Val = 0L;
						int64Union2.int32Val = decimalStruct.Hi32;
						long num4;
						int k;
						long num5;
						int m;
						int num6;
						if (int64Union.int64Val != 0L || int64Union2.int32Val != 0)
						{
							*((ref int64Union2.byteVal.FixedElementField) + 4) = *((ref int64Union2.byteVal.FixedElementField) + 3);
							*((ref int64Union2.byteVal.FixedElementField) + 3) = *((ref int64Union2.byteVal.FixedElementField) + 2);
							*((ref int64Union2.byteVal.FixedElementField) + 2) = *((ref int64Union2.byteVal.FixedElementField) + 1);
							*((ref int64Union2.byteVal.FixedElementField) + 1) = int64Union2.byteVal.FixedElementField;
							int64Union2.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
							*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
							num4 = 0L;
							k = (int)(encoding.nScale - (short)decimalStruct.scale);
							if (k < 0)
							{
								for (int l = -k; l > 1; l--)
								{
									int64Union3.int64Val = int64Union2.int64Val % 10L;
									int64Union2.int64Val /= 10L;
									*((ref int64Union.byteVal.FixedElementField) + 7) = int64Union3.byteVal.FixedElementField;
									if ((int)(int64Union.int64Val % 10L) != 0 && 1 == num3)
									{
										throw new CustomHISException(SR.ScaleTooLarge, 1537, this.UserCompatibleErrorCode);
									}
									int64Union.int64Val /= 10L;
								}
								if (2 == num3)
								{
									int64Union.int64Val += 5L;
									int16Union.int16Val = 0;
									int16Union.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
									*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
									int64Union2.int64Val += (long)int16Union.int16Val;
								}
								int64Union3.int64Val = int64Union2.int64Val % 10L;
								int64Union2.int64Val /= 10L;
								*((ref int64Union.byteVal.FixedElementField) + 7) = int64Union3.byteVal.FixedElementField;
								if ((int)(int64Union.int64Val % 10L) != 0 && 1 == num3)
								{
									throw new CustomHISException(SR.ScaleTooLarge, 1537, this.UserCompatibleErrorCode);
								}
								int64Union.int64Val /= 10L;
								k = 0;
							}
							num5 = int64Union2.int64Val;
							m = ResultLen - 1;
							if (k % 2 == 0)
							{
								num6 = 0;
								m -= k / 2;
							}
							else
							{
								num6 = 1;
								m -= (k - 1) / 2;
							}
							int num7 = num2 - k;
							k = 0;
							while (k < num7)
							{
								if (num5 > 0L)
								{
									int64Union2.int64Val = num5 % 10L;
									ref byte ptr3 = (ref int64Union.byteVal.FixedElementField) + 7;
									ptr3 |= int64Union2.byteVal.FixedElementField;
									num5 /= 10L;
								}
								num4 = int64Union.int64Val;
								int l;
								if (num6 % 2 == 0)
								{
									l = 4;
								}
								else
								{
									m--;
									l = 0;
								}
								int64Union2.int64Val = num4 % 10L;
								int64Union2.int64Val <<= l;
								if (m < ResultLen)
								{
									byte* ptr4 = ptr2 + m;
									*ptr4 |= int64Union2.byteVal.FixedElementField;
								}
								num4 /= 10L;
								int64Union.int64Val = num4;
								k++;
								num6++;
							}
							if (num5 != 0L || num4 != 0L)
							{
								throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
							}
						}
						ptr2[ResultLen - 1] = (ptr2[ResultLen - 1] & 240) | b;
						goto IL_0ECB;
						IL_04C7:
						for (int n = 0; n < ResultLen; n++)
						{
							ptr2[n] = 240;
						}
						num3 = (int)encoding.nTRE;
						if (decimalStruct.sign != 0)
						{
							if (encoding.nSign == 1)
							{
								throw new CustomHISException(SR.UnsignedWindows);
							}
							if (encoding.nOverpunch == 1)
							{
								num6 = 1;
								b = this.codePageInfo.fixedEBCDIC.MINUS;
							}
							else
							{
								num6 = 0;
								b = 208;
							}
						}
						else if (encoding.nOverpunch == 1)
						{
							num6 = 1;
							b = this.codePageInfo.fixedEBCDIC.PLUS;
						}
						else
						{
							num6 = 0;
							if (encoding.nSign == 0)
							{
								b = 192;
							}
							else
							{
								b = 240;
							}
						}
						int64Union.int64Val = decimalStruct.Lo64;
						int64Union2.int64Val = 0L;
						int64Union2.int32Val = decimalStruct.Hi32;
						*((ref int64Union2.byteVal.FixedElementField) + 4) = *((ref int64Union2.byteVal.FixedElementField) + 3);
						*((ref int64Union2.byteVal.FixedElementField) + 3) = *((ref int64Union2.byteVal.FixedElementField) + 2);
						*((ref int64Union2.byteVal.FixedElementField) + 2) = *((ref int64Union2.byteVal.FixedElementField) + 1);
						*((ref int64Union2.byteVal.FixedElementField) + 1) = int64Union2.byteVal.FixedElementField;
						int64Union2.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
						*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
						k = (int)(encoding.nScale - (short)decimalStruct.scale);
						if (k < 0)
						{
							for (int l = -k; l > 1; l--)
							{
								int64Union3.int64Val = int64Union2.int64Val % 10L;
								int64Union2.int64Val /= 10L;
								*((ref int64Union.byteVal.FixedElementField) + 7) = int64Union3.byteVal.FixedElementField;
								if ((int)(int64Union.int64Val % 10L) != 0 && 1 == num3)
								{
									throw new CustomHISException(SR.ScaleTooLarge, 1537, this.UserCompatibleErrorCode);
								}
								int64Union.int64Val /= 10L;
							}
							if (2 == num3)
							{
								int64Union.int64Val += 5L;
								int16Union.int16Val = 0;
								int16Union.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
								*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
								int64Union2.int64Val += (long)int16Union.int16Val;
							}
							int64Union3.int64Val = int64Union2.int64Val % 10L;
							int64Union2.int64Val /= 10L;
							*((ref int64Union.byteVal.FixedElementField) + 7) = int64Union3.byteVal.FixedElementField;
							if ((int)(int64Union.int64Val % 10L) != 0 && 1 == num3)
							{
								throw new CustomHISException(SR.ScaleTooLarge, 1537, this.UserCompatibleErrorCode);
							}
							int64Union.int64Val /= 10L;
							k = 0;
						}
						if (encoding.nTrailing == 1)
						{
							m = ResultLen - 1 - k;
							num6 = 0;
						}
						else
						{
							m = ResultLen - 1 - num6 - k;
							num6 = ResultLen - 1;
						}
						num5 = int64Union2.int64Val;
						num4 = 0L;
						while (m > -1)
						{
							if (num5 > 0L)
							{
								int64Union2.int64Val = num5 % 10L;
								ref byte ptr5 = (ref int64Union.byteVal.FixedElementField) + 7;
								ptr5 |= int64Union2.byteVal.FixedElementField;
								num5 /= 10L;
							}
							num4 = int64Union.int64Val;
							int64Union2.int64Val = num4 % 10L;
							if (m < ResultLen)
							{
								byte* ptr6 = ptr2 + m;
								*ptr6 |= int64Union2.byteVal.FixedElementField;
							}
							byte* ptr7 = ptr2 + m;
							*ptr7 |= 240;
							num4 /= 10L;
							int64Union.int64Val = num4;
							m--;
						}
						if (num5 != 0L || num4 != 0L)
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						ptr2[num6] = (ptr2[num6] & 15) | b;
						goto IL_0ECB;
						IL_08B2:
						num2 = (int)encoding.nPrecision;
						int num8 = (int)encoding.nScale;
						num3 = (int)encoding.nTRE;
						if ((int)decimalStruct.scale > num8 && 1 == num3)
						{
							throw new CustomHISException(SR.ScaleTooLarge, 1537, this.UserCompatibleErrorCode);
						}
						if (decimalStruct.Hi32 != 0)
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						int64Union.int64Val = decimalStruct.Lo64;
						if (decimalStruct.sign != 0)
						{
							if (encoding.nSign == 1)
							{
								throw new CustomHISException(SR.UnsignedWindows);
							}
							int64Union.int64Val = -int64Union.int64Val;
						}
						if ((int)decimalStruct.scale > num8)
						{
							int num9 = (int)decimalStruct.scale - num8;
							int l;
							if (2 == num3)
							{
								l = num9;
								l--;
								int64Union.int64Val += BasePrimitiveConverter.longValues[l, 5];
							}
							l = num9;
							int64Union.int64Val /= BasePrimitiveConverter.longPower[l];
						}
						else if ((int)decimalStruct.scale < num8)
						{
							int l = num2 + (int)decimalStruct.scale - num8;
							int num10 = (int)BasePrimitiveConverter.MaxShortValues[l];
							int num11 = 0 - num10;
							if (int64Union.int64Val > (long)num10 || int64Union.int64Val < (long)num11)
							{
								throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
							}
							l = num8 - (int)decimalStruct.scale;
							int64Union.int64Val *= BasePrimitiveConverter.longPower[l];
						}
						else
						{
							int num10 = (int)BasePrimitiveConverter.MaxShortValues[num2];
							int num11 = 0 - num10;
							if (int64Union.int64Val > (long)num10 || int64Union.int64Val < (long)num11)
							{
								throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
							}
						}
						ptr2[1] = int64Union.byteVal.FixedElementField;
						*ptr2 = *((ref int64Union.byteVal.FixedElementField) + 1);
						goto IL_0ECB;
						IL_0A7A:
						num2 = (int)encoding.nPrecision;
						num8 = (int)encoding.nScale;
						num3 = (int)encoding.nTRE;
						if ((int)decimalStruct.scale > num8 && 1 == num3)
						{
							throw new CustomHISException(SR.ScaleTooLarge, 1537, this.UserCompatibleErrorCode);
						}
						if (decimalStruct.Hi32 != 0)
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						int64Union.int64Val = decimalStruct.Lo64;
						if (decimalStruct.sign != 0)
						{
							if (encoding.nSign == 1)
							{
								throw new CustomHISException(SR.UnsignedWindows);
							}
							int64Union.int64Val = -int64Union.int64Val;
						}
						if ((int)decimalStruct.scale > num8)
						{
							int num12 = (int)decimalStruct.scale - num8;
							int l;
							if (2 == num3)
							{
								l = num12;
								l--;
								int64Union.int64Val += BasePrimitiveConverter.longValues[l, 5];
							}
							l = num12;
							int64Union.int64Val /= BasePrimitiveConverter.longPower[l];
						}
						else if ((int)decimalStruct.scale < num8)
						{
							int l = num2 + (int)decimalStruct.scale - num8;
							long num13 = (long)BasePrimitiveConverter.MaxIntValues[l];
							long num14 = 0L - num13;
							if (int64Union.int64Val > num13 || int64Union.int64Val < num14)
							{
								throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
							}
							l = num8 - (int)decimalStruct.scale;
							int64Union.int64Val *= BasePrimitiveConverter.longPower[l];
						}
						else
						{
							long num13 = (long)BasePrimitiveConverter.MaxIntValues[num2];
							long num14 = 0L - num13;
							if (int64Union.int64Val > num13 || int64Union.int64Val < num14)
							{
								throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
							}
						}
						ptr2[3] = int64Union.byteVal.FixedElementField;
						ptr2[2] = *((ref int64Union.byteVal.FixedElementField) + 1);
						ptr2[1] = *((ref int64Union.byteVal.FixedElementField) + 2);
						*ptr2 = *((ref int64Union.byteVal.FixedElementField) + 3);
						goto IL_0ECB;
						IL_0C6A:
						num3 = (int)encoding.nTRE;
						num2 = (int)encoding.nPrecision;
						num8 = (int)encoding.nScale;
						if ((int)decimalStruct.scale > num8 && 1 == num3)
						{
							throw new CustomHISException(SR.ScaleTooLarge, 1537, this.UserCompatibleErrorCode);
						}
						if (decimalStruct.Hi32 != 0)
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						int64Union.int64Val = decimalStruct.Lo64;
						if (decimalStruct.sign != 0)
						{
							if (encoding.nSign == 1)
							{
								throw new CustomHISException(SR.UnsignedWindows);
							}
							int64Union.int64Val = -int64Union.int64Val;
						}
						if ((int)decimalStruct.scale > num8)
						{
							int num15 = (int)decimalStruct.scale - num8;
							int l;
							if (2 == num3)
							{
								l = num15;
								l--;
								int64Union.int64Val += BasePrimitiveConverter.longValues[l, 5];
							}
							l = num15;
							int64Union.int64Val /= BasePrimitiveConverter.longPower[l];
						}
						else if ((int)decimalStruct.scale < num8)
						{
							int l = num2 + (int)decimalStruct.scale - num8;
							long num13 = BasePrimitiveConverter.MaxLongValues[l];
							long num14 = 0L - num13;
							if (int64Union.int64Val > num13 || int64Union.int64Val < num14)
							{
								throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
							}
							l = num8 - (int)decimalStruct.scale;
							int64Union.int64Val *= BasePrimitiveConverter.longPower[l];
						}
						else
						{
							long num13 = BasePrimitiveConverter.MaxLongValues[num2];
							long num14 = 0L - num13;
							if (int64Union.int64Val > num13 || int64Union.int64Val < num14)
							{
								throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
							}
						}
						ptr2[7] = int64Union.byteVal.FixedElementField;
						ptr2[6] = *((ref int64Union.byteVal.FixedElementField) + 1);
						ptr2[5] = *((ref int64Union.byteVal.FixedElementField) + 2);
						ptr2[4] = *((ref int64Union.byteVal.FixedElementField) + 3);
						ptr2[3] = *((ref int64Union.byteVal.FixedElementField) + 4);
						ptr2[2] = *((ref int64Union.byteVal.FixedElementField) + 5);
						ptr2[1] = *((ref int64Union.byteVal.FixedElementField) + 6);
						*ptr2 = *((ref int64Union.byteVal.FixedElementField) + 7);
						goto IL_0ECB;
						IL_0EBB:
						throw new CustomHISException(SR.UnsupportedConversion("PackDecimal"));
						IL_0ECB:;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				cumulativePackedDataLength = num;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000856C8 File Offset: 0x000838C8
		public virtual void UnpackDecimal(ref byte Buffer, ref decimal ReturnedDecimal, ref int RemainingBufferLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine)
		{
			long num;
			short num2;
			short num3;
			long num4;
			short num5;
			int num6;
			if (!this.ExtractNumber(out num, out num2, out num3, out num4, out num5, out num6, ref Buffer, NumericEditStatemachine))
			{
				ReturnedDecimal = 0m;
				RemainingBufferLength -= ResultLen;
			}
			RemainingBufferLength -= num6;
			ReturnedDecimal = num;
			for (int i = 0; i < (int)num3; i++)
			{
				ReturnedDecimal /= 10m;
			}
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x00085738 File Offset: 0x00083938
		private unsafe bool HandleSignReturnZero(byte* buffer, int length, bool expectingUnsigned, out int signPosition, out bool isPositive)
		{
			signPosition = length - 1;
			isPositive = true;
			byte b = buffer[signPosition] & 15;
			bool flag = false;
			switch (b)
			{
			case 10:
			case 14:
				break;
			case 11:
			case 13:
				isPositive = false;
				break;
			case 12:
			case 15:
				flag = true;
				break;
			default:
				if (this.acceptNullPacked)
				{
					bool flag2 = true;
					for (int i = 0; i < length; i++)
					{
						if (buffer[i] != 0)
						{
							flag2 = false;
							break;
						}
					}
					if (flag2)
					{
						return true;
					}
				}
				if (this.acceptAllInvalidNumerics)
				{
					return true;
				}
				if (!this.acceptBadPacked)
				{
					throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
				}
				flag = true;
				break;
			}
			if (!flag && expectingUnsigned)
			{
				throw new CustomHISException(SR.UnsignedHost, 1596, this.UserCompatibleErrorCode);
			}
			return false;
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000857F8 File Offset: 0x000839F8
		private unsafe bool HandleSignReturnZero(byte* buffer, int length, bool expectingUnsigned, bool signIsSeparate, bool signIsLeading, out int signPosition, out int firstDigitPosition, out int lastDigitPosition, out bool isPositive)
		{
			firstDigitPosition = -1;
			lastDigitPosition = -1;
			signPosition = -1;
			isPositive = true;
			bool flag = true;
			if (signIsSeparate)
			{
				if (signIsLeading)
				{
					signPosition = 0;
					firstDigitPosition = 1;
					lastDigitPosition = length - 1;
				}
				else
				{
					signPosition = length - 1;
					firstDigitPosition = 0;
					lastDigitPosition = length - 2;
				}
				if (buffer[signPosition] == this.codePageInfo.fixedEBCDIC.PLUS)
				{
					isPositive = true;
				}
				else if (buffer[signPosition] == this.codePageInfo.fixedEBCDIC.MINUS)
				{
					isPositive = false;
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				if (signIsLeading)
				{
					signPosition = 0;
				}
				else
				{
					signPosition = length - 1;
				}
				firstDigitPosition = 0;
				lastDigitPosition = length - 1;
				if ((buffer[signPosition] & 240) == 192 || (buffer[signPosition] & 240) == 240)
				{
					isPositive = true;
				}
				else if ((buffer[signPosition] & 240) == 208)
				{
					isPositive = false;
				}
				else
				{
					flag = false;
				}
			}
			if (!flag)
			{
				if (this.acceptNullZoned)
				{
					bool flag2 = true;
					for (int i = 0; i < length; i++)
					{
						if (buffer[i] != 0)
						{
							flag2 = false;
							break;
						}
					}
					if (flag2)
					{
						return true;
					}
				}
				if (this.acceptAllInvalidNumerics)
				{
					return true;
				}
				throw new CustomHISException(SR.BadZonedDec, 1562, this.UserCompatibleErrorCode);
			}
			else
			{
				if (!isPositive && expectingUnsigned)
				{
					throw new CustomHISException(SR.UnsignedHost, 1596, this.UserCompatibleErrorCode);
				}
				return false;
			}
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x0008594C File Offset: 0x00083B4C
		public unsafe virtual void UnpackDecimal(ref byte Buffer, ref decimal ReturnedDecimal, ref int RemainingBufferDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			BasePrimitiveConverter.DecimalStruct decimalStruct = default(BasePrimitiveConverter.DecimalStruct);
			BasePrimitiveConverter.Int16Union int16Union = default(BasePrimitiveConverter.Int16Union);
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union2 = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union3 = default(BasePrimitiveConverter.Int64Union);
			bool flag = false;
			int num = RemainingBufferDataLength - ResultLen;
			try
			{
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
						if (nCvtType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							switch (nCvtType)
							{
							case ConvertedDataType.CEDAR_COBOL_COMP94:
								goto IL_05C2;
							case ConvertedDataType.CEDAR_COBOL_COMP99:
								goto IL_0669;
							case ConvertedDataType.CEDAR_COBOL_COMP3:
								break;
							default:
								if (nCvtType == ConvertedDataType.CEDAR_COBOL_COMP918)
								{
									goto IL_0737;
								}
								if (nCvtType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
								{
									goto IL_0A5E;
								}
								goto IL_035E;
							}
						}
						else
						{
							switch (nCvtType)
							{
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
								goto IL_05C2;
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
								goto IL_0669;
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
								break;
							case ConvertedDataType.CEDAR_IEEE_NULLABLE_COMP1:
							case ConvertedDataType.CEDAR_IEEE_NULLABLE_COMP2:
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICXTRAN:
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICGTRAN:
								goto IL_0A5E;
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP918:
								goto IL_0737;
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM:
								goto IL_035E;
							default:
							{
								if (nCvtType == ConvertedDataType.CEDAR_DECIMAL_FLOAT_64)
								{
									int64Union.byteVal.FixedElementField = ptr2[7];
									*((ref int64Union.byteVal.FixedElementField) + 1) = ptr2[6];
									*((ref int64Union.byteVal.FixedElementField) + 2) = ptr2[5];
									*((ref int64Union.byteVal.FixedElementField) + 3) = ptr2[4];
									*((ref int64Union.byteVal.FixedElementField) + 4) = ptr2[3];
									*((ref int64Union.byteVal.FixedElementField) + 5) = ptr2[2];
									*((ref int64Union.byteVal.FixedElementField) + 6) = ptr2[1];
									*((ref int64Union.byteVal.FixedElementField) + 7) = *ptr2;
									ulong uint64Val = int64Union.uint64Val;
									flag = true;
									BasePrimitiveConverter.GetDecimalFloatingPoint(uint64Val, ref decimalStruct);
									goto IL_0A6E;
								}
								if (nCvtType != ConvertedDataType.CEDAR_DECIMAL_FLOAT_128)
								{
									goto IL_0A5E;
								}
								int64Union.byteVal.FixedElementField = ptr2[15];
								*((ref int64Union.byteVal.FixedElementField) + 1) = ptr2[14];
								*((ref int64Union.byteVal.FixedElementField) + 2) = ptr2[13];
								*((ref int64Union.byteVal.FixedElementField) + 3) = ptr2[12];
								*((ref int64Union.byteVal.FixedElementField) + 4) = ptr2[11];
								*((ref int64Union.byteVal.FixedElementField) + 5) = ptr2[10];
								*((ref int64Union.byteVal.FixedElementField) + 6) = ptr2[9];
								*((ref int64Union.byteVal.FixedElementField) + 7) = ptr2[8];
								ulong uint64Val2 = int64Union.uint64Val;
								int64Union.byteVal.FixedElementField = ptr2[7];
								*((ref int64Union.byteVal.FixedElementField) + 1) = ptr2[6];
								*((ref int64Union.byteVal.FixedElementField) + 2) = ptr2[5];
								*((ref int64Union.byteVal.FixedElementField) + 3) = ptr2[4];
								*((ref int64Union.byteVal.FixedElementField) + 4) = ptr2[3];
								*((ref int64Union.byteVal.FixedElementField) + 5) = ptr2[2];
								*((ref int64Union.byteVal.FixedElementField) + 6) = ptr2[1];
								*((ref int64Union.byteVal.FixedElementField) + 7) = *ptr2;
								ulong uint64Val3 = int64Union.uint64Val;
								flag = true;
								BasePrimitiveConverter.GetDecimalFloatingPoint(uint64Val2, uint64Val3, ref decimalStruct);
								goto IL_0A6E;
							}
							}
						}
						int num2;
						bool flag2;
						if (this.HandleSignReturnZero(ptr2, ResultLen, encoding.nSign == 1, out num2, out flag2))
						{
							ReturnedDecimal = 0m;
							RemainingBufferDataLength -= ResultLen;
							return;
						}
						int num3 = (int)encoding.nPrecision;
						int64Union.int64Val = 0L;
						int64Union2.int64Val = 0L;
						int num4 = 0;
						int i = 0;
						int num5;
						while (i < num3)
						{
							int16Union.int16Val = 0;
							num5 = num4 % 2;
							if (num5 == 0)
							{
								int16Union.byteVal.FixedElementField = ptr2[num2] & 240;
								int16Union.int16Val = (short)(int16Union.int16Val >> 4);
								num2--;
							}
							else
							{
								int16Union.byteVal.FixedElementField = ptr2[num2] & 15;
							}
							if (int16Union.int16Val > 9)
							{
								flag = true;
								throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
							}
							num5 = (int)int16Union.int16Val;
							int64Union.int64Val += BasePrimitiveConverter.TwoPartVal[i, 0, num5];
							int64Union2.int64Val += BasePrimitiveConverter.TwoPartVal[i, 1, num5];
							int16Union.int16Val = 0;
							int16Union.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
							*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
							int64Union2.int64Val += (long)int16Union.int16Val;
							i++;
							num4++;
						}
						short num6 = encoding.nScale;
						if (num6 == 29)
						{
							if (encoding.nTRE == 2)
							{
								int64Union.int64Val += 5L;
								int16Union.int16Val = 0;
								int16Union.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
								*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
								int64Union2.int64Val += (long)int16Union.int16Val;
							}
							int64Union3.int64Val = int64Union2.int64Val % 10L;
							int64Union2.int64Val /= 10L;
							*((ref int64Union.byteVal.FixedElementField) + 7) = int64Union3.byteVal.FixedElementField;
							int64Union.int64Val /= 10L;
							num6 -= 1;
						}
						*((ref int64Union.byteVal.FixedElementField) + 7) = int64Union2.byteVal.FixedElementField;
						int64Union2.int64Val >>= 8;
						decimalStruct.Hi32 = int64Union2.int32Val;
						if (int64Union2.int64Val > (long)((ulong)(-1)))
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						decimalStruct.Lo64 = int64Union.int64Val;
						if (flag2)
						{
							decimalStruct.sign = 0;
						}
						else
						{
							decimalStruct.sign = 128;
						}
						decimalStruct.scale = (byte)num6;
						goto IL_0A6E;
						IL_035E:
						if (this.HandleSignReturnZero(ptr2, ResultLen, encoding.nSign == 1, encoding.nOverpunch == 1, encoding.nTrailing == 1, out i, out num5, out num2, out flag2))
						{
							ReturnedDecimal = 0m;
							RemainingBufferDataLength -= ResultLen;
							return;
						}
						num3 = (int)encoding.nPrecision;
						int64Union.int64Val = 0L;
						int64Union2.int64Val = 0L;
						i = 0;
						while (i < num3)
						{
							int16Union.int16Val = 0;
							int16Union.byteVal.FixedElementField = ptr2[num2] & 15;
							if (int16Union.int16Val > 9)
							{
								flag = true;
								throw new CustomHISException(SR.BadZonedDec, 1562, this.UserCompatibleErrorCode);
							}
							num5 = (int)int16Union.int16Val;
							int64Union.int64Val += BasePrimitiveConverter.TwoPartVal[i, 0, num5];
							int64Union2.int64Val += BasePrimitiveConverter.TwoPartVal[i, 1, num5];
							int16Union.int16Val = 0;
							int16Union.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
							*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
							int64Union2.int64Val += (long)int16Union.int16Val;
							i++;
							num2--;
						}
						num6 = encoding.nScale;
						if (num6 == 29)
						{
							if (encoding.nTRE == 2)
							{
								int64Union.int64Val += 5L;
								int16Union.int16Val = 0;
								int16Union.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
								*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
								int64Union2.int64Val += (long)int16Union.int16Val;
							}
							int64Union3.int64Val = int64Union2.int64Val % 10L;
							int64Union2.int64Val /= 10L;
							*((ref int64Union.byteVal.FixedElementField) + 7) = int64Union3.byteVal.FixedElementField;
							int64Union.int64Val /= 10L;
							num6 -= 1;
						}
						*((ref int64Union.byteVal.FixedElementField) + 7) = int64Union2.byteVal.FixedElementField;
						int64Union2.int64Val >>= 8;
						decimalStruct.Hi32 = int64Union2.int32Val;
						decimalStruct.Lo64 = int64Union.int64Val;
						if (flag2)
						{
							decimalStruct.sign = 0;
						}
						else
						{
							decimalStruct.sign = 128;
						}
						decimalStruct.scale = (byte)num6;
						goto IL_0A6E;
						IL_05C2:
						int64Union.int64Val = 0L;
						int64Union.byteVal.FixedElementField = ptr2[1];
						*((ref int64Union.byteVal.FixedElementField) + 1) = *ptr2;
						decimalStruct.Hi32 = 0;
						if (int64Union.int16Val < 0)
						{
							if (encoding.nSign == 1)
							{
								throw new CustomHISException(SR.UnsignedHost, 1596, this.UserCompatibleErrorCode);
							}
							decimalStruct.sign = 128;
							int64Union.int16Val = -int64Union.int16Val;
						}
						else
						{
							decimalStruct.sign = 0;
						}
						decimalStruct.Lo64 = int64Union.int64Val;
						decimalStruct.scale = (byte)encoding.nScale;
						goto IL_0A6E;
						IL_0669:
						int64Union.int64Val = 0L;
						int64Union.byteVal.FixedElementField = ptr2[3];
						*((ref int64Union.byteVal.FixedElementField) + 1) = ptr2[2];
						*((ref int64Union.byteVal.FixedElementField) + 2) = ptr2[1];
						*((ref int64Union.byteVal.FixedElementField) + 3) = *ptr2;
						decimalStruct.Hi32 = 0;
						if (int64Union.int32Val < 0)
						{
							if (encoding.nSign == 1)
							{
								throw new CustomHISException(SR.UnsignedHost, 1596, this.UserCompatibleErrorCode);
							}
							decimalStruct.sign = 128;
							int64Union.int32Val = -int64Union.int32Val;
						}
						else
						{
							decimalStruct.sign = 0;
						}
						decimalStruct.Lo64 = int64Union.int64Val;
						decimalStruct.scale = (byte)encoding.nScale;
						goto IL_0A6E;
						IL_0737:
						int64Union.byteVal.FixedElementField = ptr2[7];
						*((ref int64Union.byteVal.FixedElementField) + 1) = ptr2[6];
						*((ref int64Union.byteVal.FixedElementField) + 2) = ptr2[5];
						*((ref int64Union.byteVal.FixedElementField) + 3) = ptr2[4];
						*((ref int64Union.byteVal.FixedElementField) + 4) = ptr2[3];
						*((ref int64Union.byteVal.FixedElementField) + 5) = ptr2[2];
						*((ref int64Union.byteVal.FixedElementField) + 6) = ptr2[1];
						*((ref int64Union.byteVal.FixedElementField) + 7) = *ptr2;
						decimalStruct.Hi32 = 0;
						if (int64Union.int64Val < 0L)
						{
							if (encoding.nSign == 1)
							{
								throw new CustomHISException(SR.UnsignedHost, 1596, this.UserCompatibleErrorCode);
							}
							decimalStruct.sign = 128;
							int64Union.int64Val = -int64Union.int64Val;
						}
						else
						{
							decimalStruct.sign = 0;
						}
						decimalStruct.Lo64 = int64Union.int64Val;
						decimalStruct.scale = (byte)encoding.nScale;
						goto IL_0A6E;
						IL_0A5E:
						throw new CustomHISException(SR.UnsupportedConversion("UnpackDecimal"));
						IL_0A6E:;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				int[] array = new int[4];
				for (int j = 0; j < 4; j++)
				{
					array[j] = *((ref decimalStruct.decIntValues.FixedElementField) + (IntPtr)j * 4);
				}
				ReturnedDecimal = new decimal(array);
				RemainingBufferDataLength = num;
			}
			catch (CustomHISException)
			{
				if (!flag || !this.acceptAllInvalidNumerics)
				{
					throw;
				}
				ReturnedDecimal = 0m;
				RemainingBufferDataLength = num;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x0008648C File Offset: 0x0008468C
		public unsafe virtual void PackBool(bool BoolValue, ref byte Buffer, ref int cumulativePackedDataLength, int MaxResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			try
			{
				ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
				if (nCvtType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
				{
					switch (nCvtType)
					{
					case ConvertedDataType.CEDAR_COBOL_COMP94:
						break;
					case ConvertedDataType.CEDAR_COBOL_COMP99:
						goto IL_0072;
					case ConvertedDataType.CEDAR_COBOL_COMP3:
						goto IL_00B3;
					default:
						if (nCvtType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							goto IL_0243;
						}
						goto IL_0119;
					}
				}
				else
				{
					switch (nCvtType)
					{
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
						break;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
						goto IL_0072;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
						goto IL_00B3;
					default:
						if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
						{
							goto IL_0243;
						}
						goto IL_0119;
					}
				}
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						if (!BoolValue)
						{
							*ptr2 = 0;
							ptr2[1] = 0;
						}
						else
						{
							*ptr2 = 0;
							ptr2[1] = 1;
						}
						goto IL_0253;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				IL_0072:
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr3 = ptr;
						if (!BoolValue)
						{
							*ptr3 = 0;
							ptr3[1] = 0;
							ptr3[2] = 0;
							ptr3[3] = 0;
						}
						else
						{
							*ptr3 = 0;
							ptr3[1] = 0;
							ptr3[2] = 0;
							ptr3[3] = 1;
						}
						goto IL_0253;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				IL_00B3:
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr4 = ptr;
						for (int i = 0; i < MaxResultLen; i++)
						{
							ptr4[i] = 0;
						}
						if (encoding.nSign == 1)
						{
							if (!BoolValue)
							{
								ptr4[MaxResultLen - 1] = 15;
							}
							else
							{
								ptr4[MaxResultLen - 1] = 31;
							}
						}
						else if (!BoolValue)
						{
							ptr4[MaxResultLen - 1] = 12;
						}
						else
						{
							ptr4[MaxResultLen - 1] = 28;
						}
						goto IL_0253;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				IL_0119:
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr5 = ptr;
						for (int j = 0; j < MaxResultLen; j++)
						{
							ptr5[j] = 240;
						}
						if (encoding.nOverpunch == 1)
						{
							byte plus = this.codePageInfo.fixedEBCDIC.PLUS;
							if (encoding.nTrailing == 1)
							{
								if (!BoolValue)
								{
									ptr5[MaxResultLen - 1] = 240;
								}
								else
								{
									ptr5[MaxResultLen - 1] = 241;
								}
								*ptr5 = plus;
							}
							else
							{
								if (!BoolValue)
								{
									ptr5[MaxResultLen - 2] = 240;
								}
								else
								{
									ptr5[MaxResultLen - 2] = 241;
								}
								ptr5[MaxResultLen - 1] = plus;
							}
						}
						else if (encoding.nTrailing == 1)
						{
							if (!BoolValue)
							{
								ptr5[MaxResultLen - 1] = 240;
							}
							else
							{
								ptr5[MaxResultLen - 1] = 241;
							}
							if (encoding.nSign == 0)
							{
								byte* ptr6 = ptr5;
								*ptr6 &= 193;
							}
							else
							{
								byte* ptr7 = ptr5;
								*ptr7 &= 241;
							}
						}
						else
						{
							if (!BoolValue)
							{
								ptr5[MaxResultLen - 1] = 240;
							}
							else
							{
								ptr5[MaxResultLen - 1] = 241;
							}
							if (encoding.nSign == 0)
							{
								byte* ptr8 = ptr5 + (MaxResultLen - 1);
								*ptr8 &= 193;
							}
						}
						goto IL_0253;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				IL_0243:
				throw new CustomHISException(SR.UnsupportedConversion("PackBool"));
				IL_0253:
				cumulativePackedDataLength += MaxResultLen;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x0008679C File Offset: 0x0008499C
		public unsafe virtual void UnpackBool(ref byte Buffer, ref bool ReturnedBool, ref int RemainingBufferDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			try
			{
				int num = RemainingBufferDataLength - ResultLen;
				ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
				if (nCvtType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
				{
					switch (nCvtType)
					{
					case ConvertedDataType.CEDAR_COBOL_COMP94:
						break;
					case ConvertedDataType.CEDAR_COBOL_COMP99:
						goto IL_0081;
					case ConvertedDataType.CEDAR_COBOL_COMP3:
						goto IL_00B6;
					default:
						if (nCvtType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							goto IL_0191;
						}
						goto IL_00FE;
					}
				}
				else
				{
					switch (nCvtType)
					{
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
						break;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
						goto IL_0081;
					case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
						goto IL_00B6;
					default:
						if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
						{
							goto IL_0191;
						}
						goto IL_00FE;
					}
				}
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						if (*ptr2 == 0 && ptr2[1] == 0)
						{
							ReturnedBool = false;
						}
						else
						{
							ReturnedBool = true;
						}
						goto IL_01A1;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				IL_0081:
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr3 = ptr;
						if (*ptr3 == 0 && ptr3[1] == 0 && ptr3[2] == 0 && ptr3[3] == 0)
						{
							ReturnedBool = false;
						}
						else
						{
							ReturnedBool = true;
						}
						goto IL_01A1;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				IL_00B6:
				int i = ResultLen - 1;
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr4 = ptr;
						byte b = ptr4[i];
						if (b != 15 && b != 12)
						{
							ReturnedBool = true;
						}
						else
						{
							ReturnedBool = false;
							for (i = 0; i < ResultLen - 1; i++)
							{
								if (ptr4[i] != 0)
								{
									ReturnedBool = true;
								}
							}
						}
						goto IL_01A1;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				IL_00FE:
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr5 = ptr;
						if (encoding.nOverpunch == 1)
						{
							int num2 = 0;
							if (encoding.nTrailing == 1)
							{
								for (i = 1; i < ResultLen - 1; i++)
								{
									if ((ptr5[i] & 15) != 0)
									{
										num2 = 1;
									}
								}
							}
							else
							{
								for (i = 0; i < ResultLen - 2; i++)
								{
									if ((ptr5[i] & 15) != 0)
									{
										num2 = 1;
									}
								}
							}
							if (num2 != 0)
							{
								ReturnedBool = true;
							}
							else
							{
								ReturnedBool = false;
							}
						}
						else
						{
							int num2 = 0;
							for (i = 0; i < ResultLen - 1; i++)
							{
								if ((ptr5[i] & 15) != 0)
								{
									num2 = 1;
								}
							}
							if (num2 != 0)
							{
								ReturnedBool = true;
							}
							else
							{
								ReturnedBool = false;
							}
						}
						goto IL_01A1;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				IL_0191:
				throw new CustomHISException(SR.UnsupportedConversion("UnpackBool"));
				IL_01A1:
				RemainingBufferDataLength = num;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000869F8 File Offset: 0x00084BF8
		public unsafe virtual void PackByte(byte ByteValue, ref byte Buffer, ref int cumulativePackedDataLength, int MaxResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			try
			{
				int num = MaxResultLen + cumulativePackedDataLength;
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						for (int i = 0; i < MaxResultLen; i++)
						{
							ptr2[i] = 0;
						}
						ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
						if (nCvtType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							switch (nCvtType)
							{
							case ConvertedDataType.CEDAR_COBOL_COMP94:
								goto IL_01DB;
							case ConvertedDataType.CEDAR_COBOL_COMP99:
								goto IL_01E7;
							case ConvertedDataType.CEDAR_COBOL_COMP3:
								break;
							case ConvertedDataType.CEDAR_COBOL_COMP1:
							case ConvertedDataType.CEDAR_COBOL_COMP2:
							case ConvertedDataType.CEDAR_COBOL_PICXTRAN:
								goto IL_01FF;
							case ConvertedDataType.CEDAR_COBOL_PICXNOTRAN:
								*ptr2 = ByteValue;
								goto IL_020F;
							default:
								if (nCvtType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
								{
									goto IL_01FF;
								}
								goto IL_0111;
							}
						}
						else
						{
							switch (nCvtType)
							{
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
								goto IL_01DB;
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
								goto IL_01E7;
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
								break;
							default:
								if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
								{
									goto IL_01FF;
								}
								goto IL_0111;
							}
						}
						int num2 = (int)encoding.nPrecision;
						byte b;
						if (encoding.nSign == 0)
						{
							b = 12;
						}
						else
						{
							b = 15;
						}
						int num3 = MaxResultLen - 1;
						int64Union.int64Val = 0L;
						int64Union.byteVal.FixedElementField = ByteValue;
						ptr2[num3] = 0;
						if (!this.BuildPackedDec(int64Union.int64Val, num2, 0, MaxResultLen, ref Buffer, b))
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						byte* ptr3 = ptr2 + num3;
						*ptr3 |= b;
						goto IL_020F;
						IL_0111:
						num2 = (int)encoding.nPrecision;
						int64Union.int64Val = 0L;
						int64Union.byteVal.FixedElementField = ByteValue;
						int num4;
						if (encoding.nSign == 0)
						{
							if (encoding.nOverpunch == 1)
							{
								if (encoding.nTrailing == 1)
								{
									num4 = 0;
								}
								else
								{
									num4 = MaxResultLen - 1;
								}
								ptr2[num4] = this.codePageInfo.fixedEBCDIC.PLUS;
							}
							else if (encoding.nTrailing == 1)
							{
								num4 = 0;
							}
							else
							{
								num4 = MaxResultLen - 1;
							}
						}
						else
						{
							num4 = MaxResultLen - 1;
						}
						if (!this.BuildZonedDec(int64Union.int64Val, num2, encoding.nOverpunch, encoding.nTrailing, MaxResultLen, ref Buffer, 255))
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						byte* ptr4 = ptr2 + num4;
						*ptr4 &= byte.MaxValue;
						goto IL_020F;
						IL_01DB:
						*ptr2 = 0;
						ptr2[1] = ByteValue;
						goto IL_020F;
						IL_01E7:
						*ptr2 = 0;
						ptr2[1] = 0;
						ptr2[2] = 0;
						ptr2[3] = ByteValue;
						goto IL_020F;
						IL_01FF:
						throw new CustomHISException(SR.UnsupportedConversion("PackByte"));
						IL_020F:;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				cumulativePackedDataLength = num;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x00086C80 File Offset: 0x00084E80
		public unsafe virtual void UnpackByte(ref byte Buffer, ref byte ReturnedByte, ref int RemainingBufferDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			int num = (int)encoding.nPrecision;
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			short num2 = BasePrimitiveConverter.MaxShortValues[num];
			bool flag = false;
			int num3 = RemainingBufferDataLength - ResultLen;
			try
			{
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
						if (nCvtType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							switch (nCvtType)
							{
							case ConvertedDataType.CEDAR_COBOL_COMP94:
								goto IL_01F3;
							case ConvertedDataType.CEDAR_COBOL_COMP99:
								goto IL_01FC;
							case ConvertedDataType.CEDAR_COBOL_COMP3:
								break;
							case ConvertedDataType.CEDAR_COBOL_COMP1:
							case ConvertedDataType.CEDAR_COBOL_COMP2:
							case ConvertedDataType.CEDAR_COBOL_PICXTRAN:
								goto IL_0205;
							case ConvertedDataType.CEDAR_COBOL_PICXNOTRAN:
								ReturnedByte = *ptr2;
								goto IL_0215;
							default:
								if (nCvtType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
								{
									goto IL_0205;
								}
								goto IL_0136;
							}
						}
						else
						{
							switch (nCvtType)
							{
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
								goto IL_01F3;
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
								goto IL_01FC;
							case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
								break;
							default:
								if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
								{
									goto IL_0205;
								}
								goto IL_0136;
							}
						}
						int num4;
						bool flag2;
						if (this.HandleSignReturnZero(ptr2, ResultLen, encoding.nSign == 1, out num4, out flag2))
						{
							ReturnedByte = 0;
							RemainingBufferDataLength -= ResultLen;
							return;
						}
						num = (int)encoding.nPrecision;
						int64Union.int64Val = 0L;
						if (!this.ConvertPackedDec(ptr2, 0, ResultLen, num, ref int64Union.int64Val))
						{
							flag = true;
							throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
						}
						if (int64Union.int32Val > 255)
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						ReturnedByte = int64Union.byteVal.FixedElementField;
						goto IL_0215;
						IL_0136:
						int num5;
						int num6;
						if (this.HandleSignReturnZero(ptr2, ResultLen, encoding.nSign == 1, encoding.nOverpunch == 1, encoding.nTrailing == 1, out num4, out num5, out num6, out flag2))
						{
							ReturnedByte = 0;
							RemainingBufferDataLength -= ResultLen;
							return;
						}
						int64Union.int64Val = 0L;
						byte* ptr3 = ptr2 + num5;
						if (!this.ConvertZonedDec(ptr3, encoding, num6 - num5 + 1, 240, ref int64Union.int64Val))
						{
							flag = true;
							throw new CustomHISException(SR.BadPackedDec, 1562, this.UserCompatibleErrorCode);
						}
						if (int64Union.int32Val > 255)
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						ReturnedByte = int64Union.byteVal.FixedElementField;
						goto IL_0215;
						IL_01F3:
						ReturnedByte = ptr2[1];
						goto IL_0215;
						IL_01FC:
						ReturnedByte = ptr2[3];
						goto IL_0215;
						IL_0205:
						throw new CustomHISException(SR.UnsupportedConversion("UnpackByte"));
						IL_0215:;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				RemainingBufferDataLength = num3;
			}
			catch (CustomHISException)
			{
				if (!flag || !this.acceptAllInvalidNumerics)
				{
					throw;
				}
				ReturnedByte = 0;
				RemainingBufferDataLength = num3;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x00086F24 File Offset: 0x00085124
		public unsafe virtual void PackInt16(short Int16Value, ref byte Buffer, ref int cumulativePackedDataLength, int MaxResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			int nPrecision = (int)encoding.nPrecision;
			short num = BasePrimitiveConverter.MaxShortValues[nPrecision];
			short num2 = -num - 1;
			try
			{
				ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
				byte b;
				long num3;
				if (nCvtType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
				{
					if (nCvtType != ConvertedDataType.CEDAR_COBOL_COMP3)
					{
						if (nCvtType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							goto IL_02CD;
						}
						goto IL_00FA;
					}
				}
				else if (nCvtType != ConvertedDataType.CEDAR_COBOL_NUMERIC_STRING)
				{
					if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3)
					{
						if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
						{
							goto IL_02CD;
						}
						goto IL_00FA;
					}
				}
				else
				{
					if (Int16Value > num)
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					if (Int16Value < 0)
					{
						if (encoding.nSign == 1)
						{
							throw new CustomHISException(SR.UnsignedWindows, 1595, this.UserCompatibleErrorCode);
						}
						if (Int16Value < num2)
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						b = this.codePageInfo.fixedEBCDIC.MINUS;
						num3 = (long)(-(long)Int16Value);
					}
					else
					{
						b = this.codePageInfo.fixedEBCDIC.PLUS;
						num3 = (long)Int16Value;
					}
					if (encoding.nSign != 1 && encoding.nOverpunch != 1)
					{
						throw new CustomHISException(SR.NotSignSeparateOrUnsigned);
					}
					if (!this.BuildNumericStringDec(num3, nPrecision, encoding.nOverpunch, encoding.nTrailing, encoding.nSign, MaxResultLen, ref Buffer, b))
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					goto IL_0317;
				}
				if (Int16Value < 0)
				{
					if (Int16Value < 0 && encoding.nSign == 1)
					{
						throw new CustomHISException(SR.UnsignedWindows, 1595, this.UserCompatibleErrorCode);
					}
					if (Int16Value < num2)
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					b = 13;
					num3 = (long)(-(long)Int16Value);
				}
				else
				{
					if (Int16Value > num)
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					if (encoding.nSign == 1)
					{
						b = 15;
					}
					else
					{
						b = 12;
					}
					num3 = (long)Int16Value;
				}
				if (!this.BuildPackedDec(num3, nPrecision, 0, MaxResultLen, ref Buffer, b))
				{
					throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
				}
				goto IL_0317;
				IL_00FA:
				if (Int16Value < 0)
				{
					if (Int16Value < 0 && encoding.nSign == 1)
					{
						throw new CustomHISException(SR.UnsignedWindows, 1595, this.UserCompatibleErrorCode);
					}
					if (Int16Value < num2)
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					if (encoding.nOverpunch == 1)
					{
						b = this.codePageInfo.fixedEBCDIC.MINUS;
					}
					else
					{
						b = 223;
					}
					num3 = (long)(-(long)Int16Value);
				}
				else
				{
					if (Int16Value > num)
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					if (encoding.nOverpunch == 1)
					{
						b = this.codePageInfo.fixedEBCDIC.PLUS;
					}
					else if (encoding.nSign == 1)
					{
						b = byte.MaxValue;
					}
					else
					{
						b = 207;
					}
					num3 = (long)Int16Value;
				}
				if (!this.BuildZonedDec(num3, nPrecision, encoding.nOverpunch, encoding.nTrailing, MaxResultLen, ref Buffer, b))
				{
					throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
				}
				goto IL_0317;
				IL_02CD:
				if (Int16Value < 0 && encoding.nSign == 1)
				{
					throw new CustomHISException(SR.UnsignedWindows, 1595, this.UserCompatibleErrorCode);
				}
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						byte* ptr3 = (byte*)(&Int16Value) + 1;
						*ptr2 = *ptr3;
						ref byte ptr4 = ref ptr2[1];
						ptr3--;
						ptr4 = *ptr3;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				IL_0317:
				cumulativePackedDataLength += MaxResultLen;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x000872B0 File Offset: 0x000854B0
		public unsafe virtual void UnpackInt16(ref byte Buffer, ref short ReturnedInt16, ref int RemainingBufferDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			int num = (int)encoding.nPrecision;
			short num2 = BasePrimitiveConverter.MaxShortValues[num];
			short num3 = -num2 - 1;
			int num4 = 0;
			long num5 = 0L;
			bool flag = false;
			int num6 = RemainingBufferDataLength - ResultLen;
			try
			{
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						byte* ptr3 = ptr2;
						ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
						int num8;
						if (nCvtType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							if (nCvtType != ConvertedDataType.CEDAR_COBOL_COMP3)
							{
								if (nCvtType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
								{
									goto IL_02F8;
								}
								goto IL_0103;
							}
						}
						else if (nCvtType != ConvertedDataType.CEDAR_COBOL_NUMERIC_STRING)
						{
							if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3)
							{
								if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
								{
									goto IL_02F8;
								}
								goto IL_0103;
							}
						}
						else
						{
							int num7 = 1;
							num8 = 0;
							if (encoding.nSign == 1)
							{
								num4 = ResultLen;
							}
							else
							{
								if (encoding.nOverpunch != 1)
								{
									throw new CustomHISException(SR.NotSignSeparateOrUnsigned);
								}
								if (encoding.nTrailing == 1)
								{
									num4 = 0;
									num8 = 1;
								}
								else
								{
									num4 = ResultLen - 1;
									num8 = 0;
								}
								ptr3 += num4;
								if (*ptr3 == this.codePageInfo.fixedEBCDIC.PLUS)
								{
									num7 = 1;
								}
								else
								{
									if (*ptr3 != this.codePageInfo.fixedEBCDIC.MINUS)
									{
										flag = true;
										throw new CustomHISException(SR.BadZonedDec, 1562, this.UserCompatibleErrorCode);
									}
									num7 = -1;
								}
								num4 = ResultLen - 1;
							}
							if (num7 < 0 && encoding.nSign == 1)
							{
								throw new CustomHISException(SR.UnsignedHost, 1596, this.UserCompatibleErrorCode);
							}
							num = (int)encoding.nPrecision;
							num5 = 0L;
							ptr3 = ptr2 + num8;
							if (!this.ConvertNumericStringDec(ptr3, encoding, num4, ref num5))
							{
								flag = true;
								throw new CustomHISException(SR.BadZonedDec, 1562, this.UserCompatibleErrorCode);
							}
							if (num7 != 1)
							{
								num5 = -num5;
							}
							if (num5 > (long)num2 || num5 < (long)num3)
							{
								throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
							}
							ReturnedInt16 = (short)num5;
							goto IL_033E;
						}
						bool flag2;
						if (this.HandleSignReturnZero(ptr2, ResultLen, encoding.nSign == 1, out num4, out flag2))
						{
							ReturnedInt16 = 0;
							RemainingBufferDataLength -= ResultLen;
							return;
						}
						num = (int)encoding.nPrecision;
						if (!this.ConvertPackedDec(ptr3, 0, ResultLen, num, ref num5))
						{
							flag = true;
							throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
						}
						if (!flag2)
						{
							num5 = -num5;
						}
						if (num5 > (long)num2 || num5 < (long)num3)
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						ReturnedInt16 = (short)num5;
						goto IL_033E;
						IL_0103:
						int num9;
						if (this.HandleSignReturnZero(ptr2, ResultLen, encoding.nSign == 1, encoding.nOverpunch == 1, encoding.nTrailing == 1, out num4, out num8, out num9, out flag2))
						{
							ReturnedInt16 = 0;
							RemainingBufferDataLength -= ResultLen;
							return;
						}
						num = (int)encoding.nPrecision;
						num5 = 0L;
						ptr3 = ptr2 + num8;
						if (!this.ConvertZonedDec(ptr3, encoding, num9 - num8 + 1, 240, ref num5))
						{
							flag = true;
							throw new CustomHISException(SR.BadZonedDec, 1562, this.UserCompatibleErrorCode);
						}
						if (!flag2)
						{
							num5 = -num5;
						}
						if (num5 > (long)num2 || num5 < (long)num3)
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						ReturnedInt16 = (short)num5;
						goto IL_033E;
						IL_02F8:
						try
						{
							fixed (short* ptr4 = &ReturnedInt16)
							{
								byte* ptr5 = (byte*)ptr4 + 1;
								*ptr5 = *ptr3;
								ptr3++;
								*((byte*)ptr5 - 1) = *ptr3;
							}
						}
						finally
						{
							short* ptr4 = null;
						}
						if (ReturnedInt16 < 0 && encoding.nSign == 1)
						{
							throw new CustomHISException(SR.UnsignedHost, 1596, this.UserCompatibleErrorCode);
						}
						IL_033E:;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				RemainingBufferDataLength = num6;
			}
			catch (CustomHISException)
			{
				if (!flag || !this.acceptAllInvalidNumerics)
				{
					throw;
				}
				ReturnedInt16 = 0;
				RemainingBufferDataLength = num6;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x00087694 File Offset: 0x00085894
		public virtual void PackInt16(short Int16Value, ref byte Buffer, ref int CumulativePackedLength, int MaxResultLen, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine)
		{
			this.FormatNumber(ref Buffer, Int16Value, encoding, NumericEditStatemachine, ref CumulativePackedLength);
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x000876A8 File Offset: 0x000858A8
		public virtual void UnpackInt16(ref byte Buffer, ref short ReturnedInt16, ref int RemainingBufferLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine)
		{
			long num;
			short num2;
			short num3;
			long num4;
			short num5;
			int num6;
			if (!this.ExtractNumber(out num, out num2, out num3, out num4, out num5, out num6, ref Buffer, NumericEditStatemachine))
			{
				ReturnedInt16 = 0;
				RemainingBufferLength -= ResultLen;
			}
			RemainingBufferLength -= num6;
			ReturnedInt16 = (short)num;
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000876E4 File Offset: 0x000858E4
		public unsafe virtual void PackDefaultInt16(short Int16Value, ref byte Buffer)
		{
			fixed (byte* ptr = &Buffer)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = (byte*)(&Int16Value) + 1;
				*ptr2 = *ptr3;
				ref byte ptr4 = ref ptr2[1];
				ptr3--;
				ptr4 = *ptr3;
			}
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x0008770C File Offset: 0x0008590C
		public unsafe virtual void UnpackDefaultInt16(ref byte Buffer, ref short ReturnedInt16)
		{
			fixed (byte* ptr = &Buffer)
			{
				byte* ptr2 = ptr;
				fixed (short* ptr3 = &ReturnedInt16)
				{
					byte* ptr4 = (byte*)ptr3 + 1;
					*ptr4 = *ptr2;
					ptr2++;
					*((byte*)ptr4 - 1) = *ptr2;
				}
			}
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x00087738 File Offset: 0x00085938
		public unsafe virtual void PackInt32(int Int32Value, ref byte Buffer, ref int cumulativePackedDataLength, int MaxResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			int nPrecision = (int)encoding.nPrecision;
			int num = BasePrimitiveConverter.MaxIntValues[nPrecision];
			int num2 = -num - 1;
			try
			{
				ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
				byte b;
				long num3;
				if (nCvtType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
				{
					if (nCvtType != ConvertedDataType.CEDAR_COBOL_COMP3)
					{
						if (nCvtType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							goto IL_02E1;
						}
						goto IL_00F9;
					}
				}
				else if (nCvtType != ConvertedDataType.CEDAR_COBOL_NUMERIC_STRING)
				{
					if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3)
					{
						if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
						{
							goto IL_02E1;
						}
						goto IL_00F9;
					}
				}
				else
				{
					if (Int32Value > num)
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					if (Int32Value < 0)
					{
						if (encoding.nOverpunch != 1)
						{
							throw new CustomHISException(SR.ValueNegativeNoSeparateSign);
						}
						if (encoding.nSign == 1)
						{
							throw new CustomHISException(SR.UnsignedWindows, 1595, this.UserCompatibleErrorCode);
						}
						if (Int32Value < num2)
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						b = this.codePageInfo.fixedEBCDIC.MINUS;
						num3 = (long)(-(long)Int32Value);
					}
					else
					{
						b = this.codePageInfo.fixedEBCDIC.PLUS;
						num3 = (long)Int32Value;
					}
					if (encoding.nSign != 1 && encoding.nOverpunch != 1)
					{
						throw new CustomHISException(SR.NotSignSeparateOrUnsigned);
					}
					if (!this.BuildNumericStringDec(num3, nPrecision, encoding.nOverpunch, encoding.nTrailing, encoding.nSign, MaxResultLen, ref Buffer, b))
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					goto IL_0345;
				}
				if (Int32Value < 0)
				{
					if (Int32Value < 0 && encoding.nSign == 1)
					{
						throw new CustomHISException(SR.UnsignedWindows, 1595, this.UserCompatibleErrorCode);
					}
					if (Int32Value < num2)
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					b = 13;
					num3 = (long)(-(long)Int32Value);
				}
				else
				{
					if (Int32Value > num)
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					if (encoding.nSign == 1)
					{
						b = 15;
					}
					else
					{
						b = 12;
					}
					num3 = (long)Int32Value;
				}
				if (!this.BuildPackedDec(num3, nPrecision, 0, MaxResultLen, ref Buffer, b))
				{
					throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
				}
				goto IL_0345;
				IL_00F9:
				if (Int32Value < 0)
				{
					if (Int32Value < 0 && encoding.nSign == 1)
					{
						throw new CustomHISException(SR.UnsignedWindows, 1595, this.UserCompatibleErrorCode);
					}
					if (Int32Value < num2)
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					if (encoding.nOverpunch == 1)
					{
						b = this.codePageInfo.fixedEBCDIC.MINUS;
					}
					else
					{
						b = 223;
					}
					num3 = (long)(-(long)Int32Value);
				}
				else
				{
					if (Int32Value > num)
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					if (encoding.nOverpunch == 1)
					{
						b = this.codePageInfo.fixedEBCDIC.PLUS;
					}
					else if (encoding.nSign == 1)
					{
						b = byte.MaxValue;
					}
					else
					{
						b = 207;
					}
					num3 = (long)Int32Value;
				}
				if (!this.BuildZonedDec(num3, nPrecision, encoding.nOverpunch, encoding.nTrailing, MaxResultLen, ref Buffer, b))
				{
					throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
				}
				goto IL_0345;
				IL_02E1:
				if (Int32Value < 0 && encoding.nSign == 1)
				{
					throw new CustomHISException(SR.UnsignedWindows, 1595, this.UserCompatibleErrorCode);
				}
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						byte* ptr3 = (byte*)(&Int32Value) + 3;
						*ptr2 = *ptr3;
						byte* ptr4 = ptr2 + 1;
						ptr3--;
						*ptr4 = *ptr3;
						byte* ptr5 = ptr4 + 1;
						ptr3--;
						*ptr5 = *ptr3;
						ref byte ptr6 = ref ptr5[1];
						ptr3--;
						ptr6 = *ptr3;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				IL_0345:
				cumulativePackedDataLength += MaxResultLen;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x00087AF4 File Offset: 0x00085CF4
		public unsafe virtual void UnpackInt32(ref byte Buffer, ref int ReturnedInt32, ref int RemainingBufferDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			int num = (int)encoding.nPrecision;
			int num2 = BasePrimitiveConverter.MaxIntValues[num];
			int num3 = -num2 - 1;
			int num4 = 0;
			long num5 = 0L;
			bool flag = false;
			int num6 = RemainingBufferDataLength - ResultLen;
			try
			{
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						byte* ptr3 = ptr2;
						ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
						int num8;
						if (nCvtType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							if (nCvtType != ConvertedDataType.CEDAR_COBOL_COMP3)
							{
								if (nCvtType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
								{
									goto IL_02F7;
								}
								goto IL_0102;
							}
						}
						else if (nCvtType != ConvertedDataType.CEDAR_COBOL_NUMERIC_STRING)
						{
							if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3)
							{
								if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
								{
									goto IL_02F7;
								}
								goto IL_0102;
							}
						}
						else
						{
							int num7 = 1;
							num8 = 0;
							if (encoding.nSign == 1)
							{
								num4 = ResultLen;
							}
							else
							{
								if (encoding.nOverpunch != 1)
								{
									throw new CustomHISException(SR.NotSignSeparateOrUnsigned);
								}
								if (encoding.nTrailing == 1)
								{
									num4 = 0;
									num8 = 1;
								}
								else
								{
									num4 = ResultLen - 1;
									num8 = 0;
								}
								ptr3 += num4;
								if (*ptr3 == this.codePageInfo.fixedEBCDIC.PLUS)
								{
									num7 = 1;
								}
								else
								{
									if (*ptr3 != this.codePageInfo.fixedEBCDIC.MINUS)
									{
										flag = true;
										throw new CustomHISException(SR.BadZonedDec, 1562, this.UserCompatibleErrorCode);
									}
									num7 = -1;
								}
								num4 = ResultLen - 1;
							}
							if (num7 < 0 && encoding.nSign == 1)
							{
								throw new CustomHISException(SR.UnsignedHost, 1596, this.UserCompatibleErrorCode);
							}
							num = (int)encoding.nPrecision;
							num5 = 0L;
							ptr3 = ptr2 + num8;
							if (!this.ConvertNumericStringDec(ptr3, encoding, num4, ref num5))
							{
								flag = true;
								throw new CustomHISException(SR.BadZonedDec, 1562, this.UserCompatibleErrorCode);
							}
							if (num7 != 1)
							{
								num5 = -num5;
							}
							if (num5 > (long)num2 || num5 < (long)num3)
							{
								throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
							}
							ReturnedInt32 = (int)num5;
							goto IL_0357;
						}
						bool flag2;
						if (this.HandleSignReturnZero(ptr2, ResultLen, encoding.nSign == 1, out num4, out flag2))
						{
							ReturnedInt32 = 0;
							RemainingBufferDataLength -= ResultLen;
							return;
						}
						num = (int)encoding.nPrecision;
						if (!this.ConvertPackedDec(ptr3, 0, ResultLen, num, ref num5))
						{
							flag = true;
							throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
						}
						if (!flag2)
						{
							num5 = -num5;
						}
						if (num5 > (long)num2 || num5 < (long)num3)
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						ReturnedInt32 = (int)num5;
						goto IL_0357;
						IL_0102:
						int num9;
						if (this.HandleSignReturnZero(ptr2, ResultLen, encoding.nSign == 1, encoding.nOverpunch == 1, encoding.nTrailing == 1, out num4, out num8, out num9, out flag2))
						{
							ReturnedInt32 = 0;
							RemainingBufferDataLength -= ResultLen;
							return;
						}
						num = (int)encoding.nPrecision;
						num5 = 0L;
						ptr3 = ptr2 + num8;
						if (!this.ConvertZonedDec(ptr3, encoding, num9 - num8 + 1, 240, ref num5))
						{
							flag = true;
							throw new CustomHISException(SR.BadZonedDec, 1562, this.UserCompatibleErrorCode);
						}
						if (!flag2)
						{
							num5 = -num5;
						}
						if (num5 > (long)num2 || num5 < (long)num3)
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						ReturnedInt32 = (int)num5;
						goto IL_0357;
						IL_02F7:
						try
						{
							fixed (int* ptr4 = &ReturnedInt32)
							{
								byte* ptr5 = (byte*)ptr4 + 3;
								*ptr5 = *ptr3;
								ptr3++;
								byte* ptr6 = (byte*)ptr5 - 1;
								*ptr6 = *ptr3;
								ptr3++;
								byte* ptr7 = (byte*)ptr6 - 1;
								*ptr7 = *ptr3;
								ptr3++;
								*((byte*)ptr7 - 1) = *ptr3;
							}
						}
						finally
						{
							int* ptr4 = null;
						}
						if (ReturnedInt32 < 0 && encoding.nSign == 1)
						{
							throw new CustomHISException(SR.UnsignedHost, 1596, this.UserCompatibleErrorCode);
						}
						IL_0357:;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				RemainingBufferDataLength = num6;
			}
			catch (CustomHISException)
			{
				if (!flag || !this.acceptAllInvalidNumerics)
				{
					throw;
				}
				ReturnedInt32 = 0;
				RemainingBufferDataLength = num6;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x00087EF0 File Offset: 0x000860F0
		public virtual void PackInt32(int Int32Value, ref byte Buffer, ref int CumulativePackedLength, int MaxResultLen, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine)
		{
			this.FormatNumber(ref Buffer, Int32Value, encoding, NumericEditStatemachine, ref CumulativePackedLength);
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x00087F04 File Offset: 0x00086104
		public virtual void UnpackInt32(ref byte Buffer, ref int ReturnedInt32, ref int RemainingBufferLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine)
		{
			long num;
			short num2;
			short num3;
			long num4;
			short num5;
			int num6;
			if (!this.ExtractNumber(out num, out num2, out num3, out num4, out num5, out num6, ref Buffer, NumericEditStatemachine))
			{
				ReturnedInt32 = 0;
				RemainingBufferLength -= ResultLen;
			}
			RemainingBufferLength -= num6;
			ReturnedInt32 = (int)num;
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x00087F40 File Offset: 0x00086140
		public unsafe virtual void PackDefaultInt32(int Int32Value, ref byte Buffer)
		{
			fixed (byte* ptr = &Buffer)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = (byte*)(&Int32Value) + 3;
				*ptr2 = *ptr3;
				byte* ptr4 = ptr2 + 1;
				ptr3--;
				*ptr4 = *ptr3;
				byte* ptr5 = ptr4 + 1;
				ptr3--;
				*ptr5 = *ptr3;
				ref byte ptr6 = ref ptr5[1];
				ptr3--;
				ptr6 = *ptr3;
			}
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x00087F7C File Offset: 0x0008617C
		public unsafe virtual void UnpackDefaultInt32(ref byte Buffer, ref int ReturnedInt32)
		{
			fixed (byte* ptr = &Buffer)
			{
				byte* ptr2 = ptr;
				fixed (int* ptr3 = &ReturnedInt32)
				{
					byte* ptr4 = (byte*)ptr3 + 3;
					*ptr4 = *ptr2;
					ptr2++;
					byte* ptr5 = (byte*)ptr4 - 1;
					*ptr5 = *ptr2;
					ptr2++;
					byte* ptr6 = (byte*)ptr5 - 1;
					*ptr6 = *ptr2;
					ptr2++;
					*((byte*)ptr6 - 1) = *ptr2;
				}
			}
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x00087FBC File Offset: 0x000861BC
		public unsafe virtual void PackInt64(long Int64Value, ref byte Buffer, ref int cumulativePackedDataLength, int MaxResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			int nPrecision = (int)encoding.nPrecision;
			long num = BasePrimitiveConverter.MaxLongValues[nPrecision];
			long num2 = (long)((int)(-num - 1L));
			try
			{
				ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
				byte b;
				long num3;
				if (nCvtType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
				{
					if (nCvtType != ConvertedDataType.CEDAR_COBOL_COMP3)
					{
						if (nCvtType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							goto IL_02E6;
						}
						goto IL_00FC;
					}
				}
				else if (nCvtType != ConvertedDataType.CEDAR_COBOL_NUMERIC_STRING)
				{
					if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3)
					{
						if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
						{
							goto IL_02E6;
						}
						goto IL_00FC;
					}
				}
				else
				{
					if (Int64Value > num)
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					if (Int64Value < 0L)
					{
						if (encoding.nOverpunch != 1)
						{
							throw new CustomHISException(SR.ValueNegativeNoSeparateSign);
						}
						if (encoding.nSign == 1)
						{
							throw new CustomHISException(SR.UnsignedWindows, 1595, this.UserCompatibleErrorCode);
						}
						if (Int64Value < num2)
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						b = this.codePageInfo.fixedEBCDIC.MINUS;
						num3 = -Int64Value;
					}
					else
					{
						b = this.codePageInfo.fixedEBCDIC.PLUS;
						num3 = Int64Value;
					}
					if (encoding.nSign != 1 && encoding.nOverpunch != 1)
					{
						throw new CustomHISException(SR.NotSignSeparateOrUnsigned);
					}
					if (!this.BuildNumericStringDec(num3, nPrecision, encoding.nOverpunch, encoding.nTrailing, encoding.nSign, MaxResultLen, ref Buffer, b))
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					goto IL_037F;
				}
				if (Int64Value < 0L)
				{
					if (Int64Value < 0L && encoding.nSign == 1)
					{
						throw new CustomHISException(SR.UnsignedWindows, 1595, this.UserCompatibleErrorCode);
					}
					if (Int64Value < num2)
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					b = 13;
					num3 = -Int64Value;
				}
				else
				{
					if (Int64Value > num)
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					if (encoding.nSign == 1)
					{
						b = 15;
					}
					else
					{
						b = 12;
					}
					num3 = Int64Value;
				}
				if (!this.BuildPackedDec(num3, nPrecision, 0, MaxResultLen, ref Buffer, b))
				{
					throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
				}
				goto IL_037F;
				IL_00FC:
				if (Int64Value < 0L)
				{
					if (Int64Value < 0L && encoding.nSign == 1)
					{
						throw new CustomHISException(SR.UnsignedWindows, 1595, this.UserCompatibleErrorCode);
					}
					if (Int64Value < num2)
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					if (encoding.nOverpunch == 1)
					{
						b = this.codePageInfo.fixedEBCDIC.MINUS;
					}
					else
					{
						b = 223;
					}
					num3 = -Int64Value;
				}
				else
				{
					if (Int64Value > num)
					{
						throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
					}
					if (encoding.nOverpunch == 1)
					{
						b = this.codePageInfo.fixedEBCDIC.PLUS;
					}
					else if (encoding.nSign == 1)
					{
						b = byte.MaxValue;
					}
					else
					{
						b = 207;
					}
					num3 = Int64Value;
				}
				if (!this.BuildZonedDec(num3, nPrecision, encoding.nOverpunch, encoding.nTrailing, MaxResultLen, ref Buffer, b))
				{
					throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
				}
				goto IL_037F;
				IL_02E6:
				if (Int64Value < 0L && encoding.nSign == 1)
				{
					throw new CustomHISException(SR.UnsignedWindows, 1595, this.UserCompatibleErrorCode);
				}
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						byte* ptr3 = (byte*)(&Int64Value) + 7;
						*ptr2 = *ptr3;
						byte* ptr4 = ptr2 + 1;
						ptr3--;
						*ptr4 = *ptr3;
						byte* ptr5 = ptr4 + 1;
						ptr3--;
						*ptr5 = *ptr3;
						byte* ptr6 = ptr5 + 1;
						ptr3--;
						*ptr6 = *ptr3;
						byte* ptr7 = ptr6 + 1;
						ptr3--;
						*ptr7 = *ptr3;
						byte* ptr8 = ptr7 + 1;
						ptr3--;
						*ptr8 = *ptr3;
						byte* ptr9 = ptr8 + 1;
						ptr3--;
						*ptr9 = *ptr3;
						ref byte ptr10 = ref ptr9[1];
						ptr3--;
						ptr10 = *ptr3;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				IL_037F:
				cumulativePackedDataLength += MaxResultLen;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000883B0 File Offset: 0x000865B0
		public unsafe virtual void UnpackInt64(ref byte Buffer, ref long ReturnedInt64, ref int RemainingBufferDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			int num = (int)encoding.nPrecision;
			long num2 = BasePrimitiveConverter.MaxLongValues[num];
			long num3 = (long)((int)(-num2 - 1L));
			int num4 = 0;
			long num5 = 0L;
			bool flag = false;
			int num6 = RemainingBufferDataLength - ResultLen;
			try
			{
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						byte* ptr3 = ptr2;
						ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
						int num8;
						if (nCvtType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
						{
							if (nCvtType != ConvertedDataType.CEDAR_COBOL_COMP3)
							{
								if (nCvtType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
								{
									goto IL_02F3;
								}
								goto IL_0103;
							}
						}
						else if (nCvtType != ConvertedDataType.CEDAR_COBOL_NUMERIC_STRING)
						{
							if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3)
							{
								if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
								{
									goto IL_02F3;
								}
								goto IL_0103;
							}
						}
						else
						{
							int num7 = 1;
							num8 = 0;
							if (encoding.nOverpunch == 1)
							{
								if (encoding.nTrailing == 1)
								{
									num4 = 0;
									num8 = 1;
								}
								else
								{
									num4 = ResultLen - 1;
									num8 = 0;
								}
								ptr3 += num4;
								if (*ptr3 == this.codePageInfo.fixedEBCDIC.PLUS)
								{
									num7 = 1;
								}
								else
								{
									if (*ptr3 != this.codePageInfo.fixedEBCDIC.MINUS)
									{
										flag = true;
										throw new CustomHISException(SR.BadZonedDec, 1562, this.UserCompatibleErrorCode);
									}
									num7 = -1;
								}
								num4 = ResultLen - 1;
							}
							else
							{
								if (encoding.nSign != 1)
								{
									throw new CustomHISException(SR.NotSignSeparateOrUnsigned);
								}
								num4 = ResultLen;
							}
							if (num7 < 0 && encoding.nSign == 1)
							{
								throw new CustomHISException(SR.UnsignedHost, 1596, this.UserCompatibleErrorCode);
							}
							num = (int)encoding.nPrecision;
							num5 = 0L;
							ptr3 = ptr2 + num8;
							if (!this.ConvertNumericStringDec(ptr3, encoding, num4, ref num5))
							{
								flag = true;
								throw new CustomHISException(SR.BadZonedDec, 1562, this.UserCompatibleErrorCode);
							}
							if (num7 != 1)
							{
								num5 = -num5;
							}
							if (num5 > num2 || num5 < num3)
							{
								throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
							}
							ReturnedInt64 = num5;
							goto IL_0388;
						}
						bool flag2;
						if (this.HandleSignReturnZero(ptr2, ResultLen, encoding.nSign == 1, out num4, out flag2))
						{
							ReturnedInt64 = 0L;
							RemainingBufferDataLength -= ResultLen;
							return;
						}
						num = (int)encoding.nPrecision;
						if (!this.ConvertPackedDec(ptr3, 0, ResultLen, num, ref num5))
						{
							flag = true;
							throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
						}
						if (!flag2)
						{
							num5 = -num5;
						}
						if (num5 > num2 || num5 < num3)
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						ReturnedInt64 = num5;
						goto IL_0388;
						IL_0103:
						int num9;
						if (this.HandleSignReturnZero(ptr2, ResultLen, encoding.nSign == 1, encoding.nOverpunch == 1, encoding.nTrailing == 1, out num4, out num8, out num9, out flag2))
						{
							ReturnedInt64 = 0L;
							RemainingBufferDataLength -= ResultLen;
							return;
						}
						num = (int)encoding.nPrecision;
						num5 = 0L;
						ptr3 = ptr2 + num8;
						if (!this.ConvertZonedDec(ptr3, encoding, num9 - num8 + 1, 240, ref num5))
						{
							flag = true;
							throw new CustomHISException(SR.BadZonedDec, 1562, this.UserCompatibleErrorCode);
						}
						if (!flag2)
						{
							num5 = -num5;
						}
						if (num5 > num2 || num5 < num3)
						{
							throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
						}
						ReturnedInt64 = num5;
						goto IL_0388;
						IL_02F3:
						try
						{
							fixed (long* ptr4 = &ReturnedInt64)
							{
								byte* ptr5 = (byte*)ptr4 + 7;
								*ptr5 = *ptr3;
								ptr3++;
								byte* ptr6 = (byte*)ptr5 - 1;
								*ptr6 = *ptr3;
								ptr3++;
								byte* ptr7 = (byte*)ptr6 - 1;
								*ptr7 = *ptr3;
								ptr3++;
								byte* ptr8 = (byte*)ptr7 - 1;
								*ptr8 = *ptr3;
								ptr3++;
								byte* ptr9 = (byte*)ptr8 - 1;
								*ptr9 = *ptr3;
								ptr3++;
								byte* ptr10 = (byte*)ptr9 - 1;
								*ptr10 = *ptr3;
								ptr3++;
								byte* ptr11 = (byte*)ptr10 - 1;
								*ptr11 = *ptr3;
								ptr3++;
								*((byte*)ptr11 - 1) = *ptr3;
							}
						}
						finally
						{
							long* ptr4 = null;
						}
						if (ReturnedInt64 < 0L && encoding.nSign == 1)
						{
							throw new CustomHISException(SR.UnsignedHost, 1596, this.UserCompatibleErrorCode);
						}
						IL_0388:;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				RemainingBufferDataLength = num6;
			}
			catch (CustomHISException)
			{
				if (!flag || !this.acceptAllInvalidNumerics)
				{
					throw;
				}
				ReturnedInt64 = 0L;
				RemainingBufferDataLength = num6;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000887E0 File Offset: 0x000869E0
		public virtual void PackInt64(long Int64Value, ref byte Buffer, ref int CumulativePackedLength, int MaxResultLen, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine)
		{
			this.FormatNumber(ref Buffer, Int64Value, encoding, NumericEditStatemachine, ref CumulativePackedLength);
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000887F4 File Offset: 0x000869F4
		public virtual void UnpackInt64(ref byte Buffer, ref long ReturnedInt64, ref int RemainingBufferLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine)
		{
			long num;
			short num2;
			short num3;
			long num4;
			short num5;
			int num6;
			if (!this.ExtractNumber(out num, out num2, out num3, out num4, out num5, out num6, ref Buffer, NumericEditStatemachine))
			{
				ReturnedInt64 = 0L;
				RemainingBufferLength -= ResultLen;
			}
			RemainingBufferLength -= num6;
			ReturnedInt64 = num;
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x00088830 File Offset: 0x00086A30
		public unsafe virtual void PackDefaultInt64(long Int64Value, ref byte Buffer)
		{
			fixed (byte* ptr = &Buffer)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = (byte*)(&Int64Value) + 7;
				*ptr2 = *ptr3;
				byte* ptr4 = ptr2 + 1;
				ptr3--;
				*ptr4 = *ptr3;
				byte* ptr5 = ptr4 + 1;
				ptr3--;
				*ptr5 = *ptr3;
				byte* ptr6 = ptr5 + 1;
				ptr3--;
				*ptr6 = *ptr3;
				byte* ptr7 = ptr6 + 1;
				ptr3--;
				*ptr7 = *ptr3;
				byte* ptr8 = ptr7 + 1;
				ptr3--;
				*ptr8 = *ptr3;
				byte* ptr9 = ptr8 + 1;
				ptr3--;
				*ptr9 = *ptr3;
				ref byte ptr10 = ref ptr9[1];
				ptr3--;
				ptr10 = *ptr3;
			}
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x00088893 File Offset: 0x00086A93
		public virtual void UnpackDefaultInt64(ref byte Buffer, ref long ReturnedInt64)
		{
			this.Swap8Bytes(ref Buffer, ref ReturnedInt64);
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000888A0 File Offset: 0x00086AA0
		public unsafe void Swap8Bytes(ref byte Buffer, ref long ReturnedInt64)
		{
			fixed (byte* ptr = &Buffer)
			{
				byte* ptr2 = ptr;
				fixed (long* ptr3 = &ReturnedInt64)
				{
					byte* ptr4 = (byte*)ptr3 + 7;
					*ptr4 = *ptr2;
					ptr2++;
					byte* ptr5 = (byte*)ptr4 - 1;
					*ptr5 = *ptr2;
					ptr2++;
					byte* ptr6 = (byte*)ptr5 - 1;
					*ptr6 = *ptr2;
					ptr2++;
					byte* ptr7 = (byte*)ptr6 - 1;
					*ptr7 = *ptr2;
					ptr2++;
					byte* ptr8 = (byte*)ptr7 - 1;
					*ptr8 = *ptr2;
					ptr2++;
					byte* ptr9 = (byte*)ptr8 - 1;
					*ptr9 = *ptr2;
					ptr2++;
					byte* ptr10 = (byte*)ptr9 - 1;
					*ptr10 = *ptr2;
					ptr2++;
					*((byte*)ptr10 - 1) = *ptr2;
				}
			}
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x00088908 File Offset: 0x00086B08
		public unsafe virtual void PackFloat(float FloatValue, ref byte Buffer, ref int cumulativePackedDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			BasePrimitiveConverter.Int16Union int16Union = default(BasePrimitiveConverter.Int16Union);
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union2 = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union3 = default(BasePrimitiveConverter.Int64Union);
			try
			{
				int num = ResultLen + cumulativePackedDataLength;
				int64Union.int64Val = 0L;
				int64Union2.int64Val = 0L;
				int64Union2.fltVal = FloatValue;
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						if (38 == encoding.nCvtType || 74 == encoding.nCvtType)
						{
							*(float*)ptr2 = FloatValue;
						}
						else if (78 == encoding.nCvtType)
						{
							byte* ptr3 = (byte*)(&FloatValue);
							*ptr2 = ptr3[3];
							ptr2[1] = ptr3[2];
							ptr2[2] = ptr3[1];
							ptr2[3] = *ptr3;
						}
						else
						{
							if (2 == encoding.nCvtType || 37 == encoding.nCvtType || encoding.nCvtType == 0 || 35 == encoding.nCvtType || 1 == encoding.nCvtType || 36 == encoding.nCvtType || 13 == encoding.nCvtType || 43 == encoding.nCvtType)
							{
								this.CheckRoundFloat(ref int64Union2.fltVal, encoding);
							}
							int16Union.int16Val = 0;
							*((ref int16Union.byteVal.FixedElementField) + 1) = *((ref int64Union2.byteVal.FixedElementField) + 3) & 127;
							int16Union.byteVal.FixedElementField = *((ref int64Union2.byteVal.FixedElementField) + 2) & 128;
							int16Union.int16Val /= 128;
							if (255 == int16Union.int16Val)
							{
								throw new CustomHISException(SR.PrecisionOverflow, 1501, this.UserCompatibleErrorCode);
							}
							int16Union.int16Val = (int16Union.int16Val - 127) * 4;
							short num2 = int16Union.int16Val / 16;
							int16Union.int16Val = (int16Union.int16Val - num2 * 16) / 4;
							if (int16Union.int16Val < 0)
							{
								if (num2 > -65)
								{
									int16Union.int16Val += 4;
									num2 -= 1;
								}
								else if (-65 == num2 && 1 == encoding.nTRE)
								{
									throw new CustomHISException(SR.PrecisionOverflow, 1501, this.UserCompatibleErrorCode);
								}
							}
							if (num2 > 62 || num2 < -65)
							{
								throw new CustomHISException(SR.PrecisionOverflow, 1501, this.UserCompatibleErrorCode);
							}
							if (FloatValue != 0f)
							{
								num2 += 65;
								int64Union.int32Val = int64Union2.int32Val;
								ConvertedDataType convertedDataType = (ConvertedDataType)encoding.nCvtType;
								if (convertedDataType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
								{
									if (convertedDataType > ConvertedDataType.CEDAR_COBOL_COMP3 && convertedDataType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
									{
										goto IL_0879;
									}
								}
								else if (convertedDataType - ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94 > 2 && convertedDataType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
								{
									goto IL_0879;
								}
								byte b;
								if ((*((ref int64Union2.byteVal.FixedElementField) + 3) & 128) == 128)
								{
									if (encoding.nSign == 1)
									{
										throw new CustomHISException(SR.UnsignedWindows);
									}
									b = byte.MaxValue;
								}
								else
								{
									b = 0;
								}
								short num3 = 3 - int16Union.int16Val;
								int64Union.int32Val &= 8388607;
								int64Union.int32Val |= 8388608;
								long num4 = (long)int64Union.int32Val;
								int16Union.int16Val = 24 - (num2 - 64) * 4 + num3;
								bool flag = false;
								if (int16Union.int16Val > 0)
								{
									if (int16Union.int16Val < 54)
									{
										int64Union.int64Val = num4 >> (int)int16Union.int16Val;
									}
									else
									{
										int64Union.int64Val = 0L;
									}
								}
								else if (int16Union.int16Val > -8)
								{
									int64Union.int64Val = num4 << (int)(-(int)int16Union.int16Val);
									flag = true;
								}
								else
								{
									int16Union.int16Val = -int16Union.int16Val;
									*((ref int64Union.byteVal.FixedElementField) + 7) = int16Union.byteVal.FixedElementField;
								}
								short int16Val = int16Union.int16Val;
								int16Union.int16Val = (num2 - 64) * 4 + 32 - num3;
								if (int16Union.int16Val > 0)
								{
									if (int16Union.int16Val < 64)
									{
										int64Union2.int64Val = num4 << (int)int16Union.int16Val;
									}
									else
									{
										int64Union2.int64Val = 0L;
									}
									*((ref int64Union2.byteVal.FixedElementField) + 7) = 0;
								}
								else
								{
									int16Union.int16Val = -int16Union.int16Val;
									int64Union2.int64Val = num4;
									*((ref int64Union2.byteVal.FixedElementField) + 7) = int16Union.byteVal.FixedElementField;
								}
								int nScale = (int)encoding.nScale;
								if (nScale == 0 && 2 == encoding.nTRE)
								{
									int64Union2.int64Val = 0L;
								}
								int num5 = (int)encoding.nPrecision;
								ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
								if (nCvtType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
								{
									switch (nCvtType)
									{
									case ConvertedDataType.CEDAR_COBOL_COMP94:
										goto IL_067C;
									case ConvertedDataType.CEDAR_COBOL_COMP99:
										goto IL_0764;
									case ConvertedDataType.CEDAR_COBOL_COMP3:
										break;
									default:
										if (nCvtType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
										{
											goto IL_0C2D;
										}
										goto IL_055C;
									}
								}
								else
								{
									switch (nCvtType)
									{
									case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
										goto IL_067C;
									case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
										goto IL_0764;
									case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
										break;
									default:
										if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
										{
											goto IL_0C2D;
										}
										goto IL_055C;
									}
								}
								for (int i = 0; i < ResultLen; i++)
								{
									ptr2[i] = 0;
								}
								int num6 = num5 - nScale;
								int j = ResultLen - (nScale + 2) / 2;
								int num7 = j + 1;
								int k = nScale % 2;
								if (flag)
								{
									if (!this.BuildPackedDec(int64Union.int64Val, num6, k, num7, ptr2, 0))
									{
										throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
									}
								}
								else
								{
									this.BuildExtendedPackedDec(int64Union.int64Val, num6, k, num7, ref *ptr2);
								}
								this.BuildPackedDecFraction(int64Union2.int64Val, nScale, ref ptr2[j]);
								if (encoding.nSign == 0)
								{
									if (b == 255)
									{
										b = 13;
									}
									else
									{
										b = 12;
									}
								}
								else
								{
									b = 15;
								}
								byte* ptr4 = ptr2 + (ResultLen - 1);
								*ptr4 |= b;
								goto IL_0C2D;
								IL_055C:
								num6 = num5 - nScale;
								int num8;
								if (encoding.nOverpunch == 1)
								{
									if (b == 0)
									{
										b = this.codePageInfo.fixedEBCDIC.PLUS;
									}
									else
									{
										b = this.codePageInfo.fixedEBCDIC.MINUS;
									}
									if (encoding.nTrailing == 1)
									{
										j = 1;
										num8 = 0;
									}
									else
									{
										j = 0;
										num8 = ResultLen - 1;
									}
									ptr2[num8] = byte.MaxValue;
								}
								else
								{
									if (encoding.nSign == 0)
									{
										if (b == 255)
										{
											b = 223;
										}
										else
										{
											b = 207;
										}
									}
									else
									{
										b = byte.MaxValue;
									}
									j = 0;
									if (encoding.nTrailing == 1)
									{
										num8 = 0;
									}
									else
									{
										num8 = ResultLen - 1;
									}
								}
								if (flag)
								{
									if (!this.BuildZonedDec(int64Union.int64Val, num6, encoding.nOverpunch, encoding.nTrailing, ResultLen, ref Buffer, b))
									{
										throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
									}
								}
								else
								{
									this.BuildExtendedZonedDec(int64Union.int64Val, num6, ref ptr2[j]);
								}
								this.BuildZonedDecFraction(int64Union2.int64Val, nScale, ref ptr2[num6 + j]);
								byte* ptr5 = ptr2 + num8;
								*ptr5 &= b;
								goto IL_0C2D;
								IL_067C:
								*((ref int64Union2.byteVal.FixedElementField) + 7) = 0;
								int num9 = (int)BasePrimitiveConverter.MaxShortValues[num5];
								if (b == 255)
								{
									num9++;
								}
								if (int64Union.int64Val > (long)num9 || (int64Union.int64Val == (long)num9 && int64Union2.int64Val != 0L))
								{
									throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
								}
								num6 = num5 - nScale;
								int64Union3.int64Val = 0L;
								this.BuildFixedBinFraction(int64Union2.int64Val, int16Val, nScale, out int64Union3.int64Val);
								int64Union.int64Val *= BasePrimitiveConverter.longPower[nScale];
								int64Union.int64Val += int64Union3.int64Val;
								if (b == 255)
								{
									int64Union.int64Val = -int64Union.int64Val;
								}
								ptr2[1] = int64Union.byteVal.FixedElementField;
								*ptr2 = *((ref int64Union.byteVal.FixedElementField) + 1);
								goto IL_0C2D;
								IL_0764:
								*((ref int64Union2.byteVal.FixedElementField) + 7) = 0;
								long num10 = (long)BasePrimitiveConverter.MaxIntValues[num5];
								if (b == 255)
								{
									num10 += 1L;
								}
								if (int64Union.int64Val > num10 || (int64Union.int64Val == num10 && int64Union2.int64Val != 0L))
								{
									throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
								}
								num6 = num5 - nScale;
								int64Union3.int64Val = 0L;
								this.BuildFixedBinFraction(int64Union2.int64Val, int16Val, nScale, out int64Union3.int64Val);
								int64Union.int64Val *= BasePrimitiveConverter.longPower[nScale];
								int64Union.int64Val += int64Union3.int64Val;
								if (b == 255)
								{
									int64Union.int64Val = -int64Union.int64Val;
								}
								ptr2[3] = int64Union.byteVal.FixedElementField;
								ptr2[2] = *((ref int64Union.byteVal.FixedElementField) + 1);
								ptr2[1] = *((ref int64Union.byteVal.FixedElementField) + 2);
								*ptr2 = *((ref int64Union.byteVal.FixedElementField) + 3);
								goto IL_0C2D;
								IL_0879:
								*((ref int64Union.byteVal.FixedElementField) + 3) = 0;
								if (1 == int16Union.int16Val && 33 == num2)
								{
									num2 = 32;
									int64Union.int32Val <<= 1;
								}
								else
								{
									*((ref int64Union.byteVal.FixedElementField) + 2) = (*((ref int64Union2.byteVal.FixedElementField) + 2) & 127) | 128;
									j = (int)(3 - int16Union.int16Val);
									num4 = (long)int64Union.int32Val;
									int64Union.int64Val = 0L;
									int64Union.uint32Val = uint.MaxValue << j;
									if (2 == encoding.nTRE)
									{
										int64Union.int32Val = ~int64Union.int32Val;
										int64Union.int32Val >>= j - 1;
										int64Union.int32Val <<= j - 1;
										int64Union.int64Val += num4;
									}
									else
									{
										int64Union.int64Val &= num4;
									}
									if (int64Union.int64Val != num4 && 1 == encoding.nTRE)
									{
										throw new CustomHISException(SR.PrecisionLoss, 1510, this.UserCompatibleErrorCode);
									}
									int64Union.int32Val >>= j;
								}
								int16Union.int16Val = num2;
								*((ref int64Union.byteVal.FixedElementField) + 3) = int16Union.byteVal.FixedElementField;
								ref byte ptr6 = (ref int64Union.byteVal.FixedElementField) + 3;
								ptr6 |= *((ref int64Union2.byteVal.FixedElementField) + 3) & 128;
								ptr2[3] = int64Union.byteVal.FixedElementField;
								ptr2[2] = *((ref int64Union.byteVal.FixedElementField) + 1);
								ptr2[1] = *((ref int64Union.byteVal.FixedElementField) + 2);
								*ptr2 = *((ref int64Union.byteVal.FixedElementField) + 3);
							}
							else
							{
								ConvertedDataType convertedDataType = (ConvertedDataType)encoding.nCvtType;
								int num5;
								if (convertedDataType <= ConvertedDataType.CEDAR_COBOL_COMP918)
								{
									switch (convertedDataType)
									{
									case ConvertedDataType.CEDAR_COBOL_COMP94:
										goto IL_0BC2;
									case ConvertedDataType.CEDAR_COBOL_COMP99:
										goto IL_0BCE;
									case ConvertedDataType.CEDAR_COBOL_COMP3:
										break;
									default:
										if (convertedDataType != ConvertedDataType.CEDAR_COBOL_COMP918)
										{
											goto IL_0C16;
										}
										goto IL_0BE6;
									}
								}
								else
								{
									if (convertedDataType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
									{
										switch (convertedDataType)
										{
										case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
											goto IL_0BC2;
										case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
											goto IL_0BCE;
										case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
											goto IL_0A9E;
										case ConvertedDataType.CEDAR_IEEE_NULLABLE_COMP1:
										case ConvertedDataType.CEDAR_IEEE_NULLABLE_COMP2:
										case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICXTRAN:
										case ConvertedDataType.CEDAR_COBOL_NULLABLE_PICGTRAN:
											goto IL_0C16;
										case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP918:
											goto IL_0BE6;
										case ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM:
											break;
										default:
											goto IL_0C16;
										}
									}
									num5 = (int)encoding.nPrecision;
									byte b;
									int j;
									int num8;
									if (encoding.nOverpunch == 1)
									{
										b = this.codePageInfo.fixedEBCDIC.PLUS;
										if (encoding.nTrailing == 1)
										{
											j = 1;
											num8 = 0;
										}
										else
										{
											j = 0;
											num8 = ResultLen - 1;
										}
										for (int k = 0; k < num5; k++)
										{
											ptr2[j] = 240;
											j++;
										}
										ptr2[num8] = b;
										goto IL_0C2D;
									}
									j = 0;
									if (encoding.nSign == 0)
									{
										b = 192;
									}
									else
									{
										b = 240;
									}
									if (encoding.nTrailing == 1)
									{
										num8 = 0;
									}
									else
									{
										num8 = ResultLen - 1;
									}
									for (int k = 0; k < num5; k++)
									{
										ptr2[j] = 240;
										j++;
									}
									ptr2[num8] = b;
									goto IL_0C2D;
								}
								IL_0A9E:
								num5 = (int)encoding.nPrecision;
								int num6 = (num5 + 2) / 2;
								for (int j = 0; j < num6; j++)
								{
									ptr2[j] = 0;
								}
								if (encoding.nSign == 0)
								{
									byte* ptr7 = ptr2 + (num6 - 1);
									*ptr7 |= 12;
									goto IL_0C2D;
								}
								byte* ptr8 = ptr2 + (num6 - 1);
								*ptr8 |= 15;
								goto IL_0C2D;
								IL_0BC2:
								*ptr2 = 0;
								ptr2[1] = 0;
								goto IL_0C2D;
								IL_0BCE:
								*ptr2 = 0;
								ptr2[1] = 0;
								ptr2[2] = 0;
								ptr2[3] = 0;
								goto IL_0C2D;
								IL_0BE6:
								*ptr2 = 0;
								ptr2[1] = 0;
								ptr2[2] = 0;
								ptr2[3] = 0;
								ptr2[4] = 0;
								ptr2[5] = 0;
								ptr2[6] = 0;
								ptr2[7] = 0;
								goto IL_0C2D;
								IL_0C16:
								*ptr2 = 64;
								ptr2[1] = 0;
								ptr2[2] = 0;
								ptr2[3] = 0;
							}
						}
						IL_0C2D:;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				cumulativePackedDataLength = num;
			}
			catch (CustomHISException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000895B0 File Offset: 0x000877B0
		public virtual void PackFloat(float FloatValue, ref byte Buffer, ref int CumulativePackedLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine)
		{
			this.FormatNumber(ref Buffer, FloatValue, encoding, NumericEditStatemachine, ref CumulativePackedLength);
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000895C4 File Offset: 0x000877C4
		public unsafe virtual void UnpackFloat(ref byte Buffer, ref float ReturnedFloat, ref int RemainingBufferDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			int num = 0;
			BasePrimitiveConverter.Int16Union int16Union = default(BasePrimitiveConverter.Int16Union);
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union2 = default(BasePrimitiveConverter.Int64Union);
			bool flag = false;
			int num2 = RemainingBufferDataLength - ResultLen;
			try
			{
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						byte* ptr3 = ptr2;
						if (encoding.nCvtType == 74 || encoding.nCvtType == 38)
						{
							int64Union.fltVal = *(float*)ptr3;
						}
						else if (encoding.nCvtType == 78)
						{
							byte* ptr4 = (byte*)(&int64Union.fltVal);
							ptr4[3] = *ptr3;
							ptr4[2] = ptr3[1];
							ptr4[1] = ptr3[2];
							*ptr4 = ptr3[3];
						}
						else
						{
							ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
							switch (nCvtType)
							{
							case ConvertedDataType.CEDAR_COBOL_COMP94:
							{
								int num3 = (int)encoding.nScale;
								int64Union.int64Val = 0L;
								int64Union.byteVal.FixedElementField = ptr3[1];
								*((ref int64Union.byteVal.FixedElementField) + 1) = *ptr3;
								if (int64Union.int16Val < 0 && encoding.nSign == 1)
								{
									throw new CustomHISException(SR.UnsignedHost, 1596, this.UserCompatibleErrorCode);
								}
								int64Union.fltVal = (float)((double)int64Union.int64Val / (double)BasePrimitiveConverter.longPower[num3]);
								break;
							}
							case ConvertedDataType.CEDAR_COBOL_COMP99:
							{
								int num3 = (int)encoding.nScale;
								int64Union.int64Val = 0L;
								int64Union.byteVal.FixedElementField = ptr3[3];
								*((ref int64Union.byteVal.FixedElementField) + 1) = ptr3[2];
								*((ref int64Union.byteVal.FixedElementField) + 2) = ptr3[1];
								*((ref int64Union.byteVal.FixedElementField) + 3) = *ptr3;
								if (int64Union.int32Val < 0 && encoding.nSign == 1)
								{
									throw new CustomHISException(SR.UnsignedHost, 1596, this.UserCompatibleErrorCode);
								}
								int64Union.fltVal = (float)((double)int64Union.int64Val / (double)BasePrimitiveConverter.longPower[num3]);
								break;
							}
							case ConvertedDataType.CEDAR_COBOL_COMP3:
							{
								int num4;
								bool flag2;
								if (this.HandleSignReturnZero(ptr2, ResultLen, encoding.nSign == 1, out num4, out flag2))
								{
									ReturnedFloat = 0f;
									RemainingBufferDataLength -= ResultLen;
									return;
								}
								int nPrecision = (int)encoding.nPrecision;
								int num3 = (int)encoding.nScale;
								int num5 = nPrecision % 2;
								flag = true;
								this.ConvertExtendedPackedDec(ptr3, nPrecision, num5, ref num, ref int64Union.int64Val);
								if (num3 - num > 18)
								{
									num5 = 18;
								}
								else
								{
									num5 = num3 - num;
								}
								int64Union.fltVal = (float)((double)int64Union.int64Val / (double)BasePrimitiveConverter.longPower[num5]);
								if (num3 - num > 18)
								{
									num5 = num3 - num - 18;
									int64Union.fltVal /= (float)BasePrimitiveConverter.longPower[num5];
								}
								if (!flag2)
								{
									int64Union.fltVal = -int64Union.fltVal;
								}
								break;
							}
							default:
								if (nCvtType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
								{
									int64Union.int64Val = 0L;
									for (int i = 0; i < 4; i++)
									{
										*((ref int64Union2.byteVal.FixedElementField) + i) = ptr3[i];
									}
									if (*((ref int64Union2.byteVal.FixedElementField) + 1) == 0 && *((ref int64Union2.byteVal.FixedElementField) + 2) == 0 && *((ref int64Union2.byteVal.FixedElementField) + 3) == 0)
									{
										ReturnedFloat = 0f;
										RemainingBufferDataLength = num2;
										return;
									}
									int16Union.int16Val = 0;
									byte b = int64Union2.byteVal.FixedElementField & 128;
									int16Union.byteVal.FixedElementField = int64Union2.byteVal.FixedElementField & 127;
									short num6 = (int16Union.int16Val - 64) * 4;
									int64Union.byteVal.FixedElementField = *((ref int64Union2.byteVal.FixedElementField) + 3);
									*((ref int64Union.byteVal.FixedElementField) + 1) = *((ref int64Union2.byteVal.FixedElementField) + 2);
									*((ref int64Union.byteVal.FixedElementField) + 2) = *((ref int64Union2.byteVal.FixedElementField) + 1);
									*((ref int64Union.byteVal.FixedElementField) + 3) = 0;
									if (num6 > -127)
									{
										int num4 = 0;
										int num7 = 0;
										while (num4 < 24 && num7 != 1)
										{
											if ((128 & *((ref int64Union.byteVal.FixedElementField) + 2)) != 0)
											{
												num7 = 1;
												ref byte ptr5 = (ref int64Union.byteVal.FixedElementField) + 2;
												ptr5 &= 127;
											}
											else
											{
												int64Union.int32Val <<= 1;
											}
											num6 -= 1;
											if (num6 <= -127)
											{
												num7 = 1;
											}
											num4++;
										}
									}
									num6 += 127;
									if (num6 < 0 && num6 > -22)
									{
										long int64Val = int64Union.int64Val;
										int64Union.int64Val = 0L;
										int64Union.int32Val = -1 << (int)(-(int)num6);
										int64Union.int64Val &= int64Val;
										if (int64Union.int64Val != int64Val && 1 == encoding.nTRE)
										{
											throw new CustomHISException(SR.PrecisionLoss, 1510, this.UserCompatibleErrorCode);
										}
										int64Union.int32Val >>= (int)(-(int)num6);
										num6 = 0;
									}
									if (num6 > 254 || num6 < 0)
									{
										throw new CustomHISException(SR.PrecisionOverflow, 1501, this.UserCompatibleErrorCode);
									}
									int64Union2.int16Val = (short)(num6 << 7);
									*((ref int64Union.byteVal.FixedElementField) + 3) = *((ref int64Union2.byteVal.FixedElementField) + 1);
									ref byte ptr6 = (ref int64Union.byteVal.FixedElementField) + 2;
									ptr6 |= int64Union2.byteVal.FixedElementField & 128;
									ref byte ptr7 = (ref int64Union.byteVal.FixedElementField) + 3;
									ptr7 |= b;
								}
								else
								{
									int num4;
									bool flag2;
									int num7;
									int num8;
									if (this.HandleSignReturnZero(ptr2, ResultLen, encoding.nSign == 1, encoding.nOverpunch == 1, encoding.nTrailing == 1, out num4, out num7, out num8, out flag2))
									{
										ReturnedFloat = 0f;
										RemainingBufferDataLength -= ResultLen;
										return;
									}
									int num3 = (int)encoding.nScale;
									flag = true;
									this.ConvertExtendedZonedDec(ptr3 + num7, encoding, num8 - num7 + 1, ref num, ref int64Union.int64Val);
									int num5;
									if (num3 - num > 18)
									{
										num5 = 18;
									}
									else
									{
										num5 = num3 - num;
									}
									int64Union.fltVal = (float)((double)int64Union.int64Val / (double)BasePrimitiveConverter.longPower[num5]);
									if (num3 - num > 18)
									{
										num5 = num3 - num - 18;
										int64Union.fltVal /= (float)BasePrimitiveConverter.longPower[num5];
									}
									if (!flag2)
									{
										int64Union.fltVal = -int64Union.fltVal;
									}
								}
								break;
							}
						}
					}
				}
				finally
				{
					byte* ptr = null;
				}
				ReturnedFloat = int64Union.fltVal;
				RemainingBufferDataLength = num2;
			}
			catch (CustomHISException)
			{
				if (!flag || !this.acceptAllInvalidNumerics)
				{
					throw;
				}
				ReturnedFloat = 0f;
				RemainingBufferDataLength = num2;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x00089C7C File Offset: 0x00087E7C
		public virtual void UnpackFloat(ref byte Buffer, ref float ReturnedFloat, ref int RemainingBufferLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine)
		{
			long num;
			short num2;
			short num3;
			long num4;
			short num5;
			int num6;
			if (!this.ExtractNumber(out num, out num2, out num3, out num4, out num5, out num6, ref Buffer, NumericEditStatemachine))
			{
				ReturnedFloat = 0f;
				RemainingBufferLength -= ResultLen;
			}
			RemainingBufferLength -= num6;
			ReturnedFloat = (float)num;
			int num7 = (int)(num4 - (long)num3);
			if (num7 < 0)
			{
				num7 = -num7;
				for (int i = 0; i < num7; i++)
				{
					ReturnedFloat /= 10f;
				}
				return;
			}
			if (num7 > 0)
			{
				for (int j = 0; j < num7; j++)
				{
					ReturnedFloat *= 10f;
				}
			}
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x00089D08 File Offset: 0x00087F08
		public unsafe virtual void PackDouble(double DoubleValue, ref byte Buffer, ref int cumulativePackedDataLength, int MaxResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			BasePrimitiveConverter.Int16Union int16Union = default(BasePrimitiveConverter.Int16Union);
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union2 = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union3 = default(BasePrimitiveConverter.Int64Union);
			fixed (byte* ptr = &Buffer)
			{
				byte* ptr2 = ptr;
				int num = MaxResultLen + cumulativePackedDataLength;
				byte* ptr3 = ptr2;
				if (39 == encoding.nCvtType || 75 == encoding.nCvtType)
				{
					*(double*)ptr3 = DoubleValue;
				}
				else if (79 == encoding.nCvtType)
				{
					byte* ptr4 = (byte*)(&DoubleValue);
					*ptr3 = ptr4[7];
					ptr3[1] = ptr4[6];
					ptr3[2] = ptr4[5];
					ptr3[3] = ptr4[4];
					ptr3[4] = ptr4[3];
					ptr3[5] = ptr4[2];
					ptr3[6] = ptr4[1];
					ptr3[7] = *ptr4;
				}
				else
				{
					int64Union2.dblVal = DoubleValue;
					if (2 == encoding.nCvtType || encoding.nCvtType == 0 || 1 == encoding.nCvtType || 13 == encoding.nCvtType)
					{
						this.CheckRoundDouble(ref int64Union2.dblVal, encoding);
					}
					int16Union.int16Val = 0;
					*((ref int16Union.byteVal.FixedElementField) + 1) = *((ref int64Union2.byteVal.FixedElementField) + 7) & 127;
					int16Union.byteVal.FixedElementField = *((ref int64Union2.byteVal.FixedElementField) + 6) & 240;
					int16Union.int16Val /= 16;
					if (2047 == int16Union.int16Val)
					{
						throw new CustomHISException(SR.PrecisionOverflow, 1501, this.UserCompatibleErrorCode);
					}
					if (DoubleValue != 0.0)
					{
						int16Union.int16Val = (int16Union.int16Val - 1023) * 4;
						short num2 = int16Union.int16Val / 16;
						int16Union.int16Val = (int16Union.int16Val - num2 * 16) / 4;
						if (int16Union.int16Val < 0)
						{
							if (num2 > -65)
							{
								int16Union.int16Val += 4;
								num2 -= 1;
							}
							else if (-65 == num2 && 1 == encoding.nTRE)
							{
								throw new CustomHISException(SR.PrecisionOverflow, 1501, this.UserCompatibleErrorCode);
							}
						}
						if (num2 > 62 || num2 < -65)
						{
							throw new CustomHISException(SR.PrecisionOverflow, 1501, this.UserCompatibleErrorCode);
						}
						num2 += 65;
						int64Union.int64Val = int64Union2.int64Val;
						*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
						*((ref int64Union.byteVal.FixedElementField) + 6) = *((ref int64Union2.byteVal.FixedElementField) + 6) & 15;
						ref byte ptr5 = (ref int64Union.byteVal.FixedElementField) + 6;
						ptr5 |= 16;
						short num3 = encoding.nCvtType;
						if (num3 <= 2 || num3 == 13)
						{
							byte b;
							if ((*((ref int64Union2.byteVal.FixedElementField) + 7) & 128) == 128)
							{
								b = byte.MaxValue;
							}
							else
							{
								b = 0;
							}
							if (b == 255 && encoding.nSign == 1)
							{
								throw new CustomHISException(SR.UnsignedWindows, 1595, this.UserCompatibleErrorCode);
							}
							short num4 = 3 - int16Union.int16Val;
							long num5 = int64Union.int64Val;
							int16Union.int16Val = 53 - (num2 - 64) * 4 + num4;
							bool flag = false;
							if (int16Union.int16Val > 0)
							{
								if (int16Union.int16Val < 54)
								{
									int64Union.int64Val = num5 >> (int)int16Union.int16Val;
								}
								else
								{
									int64Union.int64Val = 0L;
								}
							}
							else if (int16Union.int16Val > -8)
							{
								int64Union.int64Val = num5 << (int)(-(int)int16Union.int16Val);
								flag = true;
							}
							else
							{
								int16Union.int16Val = -int16Union.int16Val;
								*((ref int64Union.byteVal.FixedElementField) + 7) = int16Union.byteVal.FixedElementField;
							}
							short num6 = int16Union.int16Val - 32;
							int16Union.int16Val = 3 + (num2 - 64) * 4 - num4;
							if (int16Union.int16Val > 0)
							{
								if (int16Union.int16Val < 64)
								{
									int64Union2.int64Val = num5 << (int)int16Union.int16Val;
								}
								else
								{
									int64Union2.int64Val = 0L;
								}
								*((ref int64Union2.byteVal.FixedElementField) + 7) = 0;
							}
							else
							{
								int16Union.int16Val = -int16Union.int16Val;
								int64Union2.int64Val = num5;
								*((ref int64Union2.byteVal.FixedElementField) + 7) = int16Union.byteVal.FixedElementField;
							}
							int nScale = (int)encoding.nScale;
							if (nScale == 0 && 2 == encoding.nTRE)
							{
								int64Union2.int64Val = 0L;
							}
							int num7 = (int)encoding.nPrecision;
							int num8 = num7 - nScale;
							short nCvtType = encoding.nCvtType;
							switch (nCvtType)
							{
							case 0:
							{
								*((ref int64Union2.byteVal.FixedElementField) + 7) = 0;
								int num9 = (int)BasePrimitiveConverter.MaxShortValues[num7];
								if (b == 255)
								{
									num9++;
								}
								if (int64Union.int64Val > (long)num9 || (int64Union.int64Val == (long)num9 && int64Union2.int64Val != 0L))
								{
									throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
								}
								num8 = num7 - nScale;
								int64Union3.int64Val = 0L;
								this.BuildFixedBinFraction(int64Union2.int64Val, num6, nScale, out int64Union3.int64Val);
								int64Union.int64Val *= BasePrimitiveConverter.longPower[nScale];
								int64Union.int64Val += int64Union3.int64Val;
								if (b == 255)
								{
									int64Union.int64Val = -int64Union.int64Val;
								}
								ptr3[1] = int64Union.byteVal.FixedElementField;
								*ptr3 = *((ref int64Union.byteVal.FixedElementField) + 1);
								break;
							}
							case 1:
							{
								*((ref int64Union2.byteVal.FixedElementField) + 7) = 0;
								long num10 = (long)BasePrimitiveConverter.MaxIntValues[num7];
								if (b == 255)
								{
									num10 += 1L;
								}
								if (int64Union.int64Val > num10 || (int64Union.int64Val == num10 && int64Union2.int64Val != 0L))
								{
									throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
								}
								num8 = num7 - nScale;
								int64Union3.int64Val = 0L;
								this.BuildFixedBinFraction(int64Union2.int64Val, num6, nScale, out int64Union3.int64Val);
								int64Union.int64Val *= BasePrimitiveConverter.longPower[nScale];
								int64Union.int64Val += int64Union3.int64Val;
								if (b == 255)
								{
									int64Union.int64Val = -int64Union.int64Val;
								}
								ptr3[3] = int64Union.byteVal.FixedElementField;
								ptr3[2] = *((ref int64Union.byteVal.FixedElementField) + 1);
								ptr3[1] = *((ref int64Union.byteVal.FixedElementField) + 2);
								*ptr3 = *((ref int64Union.byteVal.FixedElementField) + 3);
								break;
							}
							case 2:
							{
								for (int i = 0; i < MaxResultLen; i++)
								{
									ptr3[i] = 0;
								}
								int j = MaxResultLen - (nScale + 2) / 2;
								int num11 = j + 1;
								int k = nScale % 2;
								if (flag)
								{
									if (!this.BuildPackedDec(int64Union.int64Val, num8, k, num11, ptr3, 0))
									{
										throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
									}
								}
								else
								{
									this.BuildExtendedPackedDec(int64Union.int64Val, num8, k, num11, ref *ptr3);
								}
								this.BuildPackedDecFraction(int64Union2.int64Val, nScale, ref ptr3[j]);
								if (encoding.nSign == 0)
								{
									if (b == 255)
									{
										b = 13;
									}
									else
									{
										b = 12;
									}
								}
								else
								{
									b = 15;
								}
								byte* ptr6 = ptr3 + (MaxResultLen - 1);
								*ptr6 |= b;
								break;
							}
							default:
								if (nCvtType == 13)
								{
									num8 = num7 - nScale;
									int j;
									int num12;
									if (encoding.nOverpunch == 1)
									{
										if (b == 0)
										{
											b = this.codePageInfo.fixedEBCDIC.PLUS;
										}
										else
										{
											b = this.codePageInfo.fixedEBCDIC.MINUS;
										}
										if (encoding.nTrailing == 1)
										{
											j = 1;
											num12 = 0;
										}
										else
										{
											j = 0;
											num12 = MaxResultLen - 1;
										}
										ptr3[num12] = byte.MaxValue;
									}
									else
									{
										if (encoding.nSign == 0)
										{
											if (b == 255)
											{
												b = 223;
											}
											else
											{
												b = 207;
											}
										}
										else
										{
											b = byte.MaxValue;
										}
										j = 0;
										if (encoding.nTrailing == 1)
										{
											num12 = 0;
										}
										else
										{
											num12 = MaxResultLen - 1;
										}
									}
									if (flag)
									{
										if (!this.BuildZonedDec(int64Union.int64Val, num8, encoding.nOverpunch, encoding.nTrailing, MaxResultLen, ref Buffer, b))
										{
											throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
										}
									}
									else
									{
										this.BuildExtendedZonedDec(int64Union.int64Val, num8, ref ptr3[j]);
									}
									this.BuildZonedDecFraction(int64Union2.int64Val, nScale, ref ptr3[num8 + j]);
									byte* ptr7 = ptr3 + num12;
									*ptr7 &= b;
								}
								break;
							}
						}
						else
						{
							if (int16Union.int16Val < 0 && 2 == encoding.nTRE)
							{
								int j = (int)int16Union.int16Val;
								long num5 = int64Union.int64Val;
								int64Union.int64Val = -1L << -j;
								int64Union.int64Val = ~int64Union.int64Val;
								int64Union.int64Val >>= j - 1;
								int64Union.int64Val <<= j - 1;
								int64Union.int64Val += num5;
								if (int64Union.int64Val != num5 && 1 == encoding.nTRE)
								{
									throw new CustomHISException(SR.PrecisionLoss, 1510, this.UserCompatibleErrorCode);
								}
							}
							if (int16Union.int16Val > 0)
							{
								if (int16Union.int16Val < 64)
								{
									int64Union.int64Val <<= (int)int16Union.int16Val;
								}
								else
								{
									int64Union.int64Val = 0L;
								}
							}
							else if (int16Union.int16Val > -64)
							{
								int64Union.int64Val >>= (int)(-(int)int16Union.int16Val);
							}
							else
							{
								int64Union.int64Val = 0L;
							}
							int16Union.int16Val = num2;
							*((ref int64Union.byteVal.FixedElementField) + 7) = int16Union.byteVal.FixedElementField;
							ref byte ptr8 = (ref int64Union.byteVal.FixedElementField) + 7;
							ptr8 |= *((ref int64Union2.byteVal.FixedElementField) + 7) & 128;
							ptr3[7] = int64Union.byteVal.FixedElementField;
							ptr3[6] = *((ref int64Union.byteVal.FixedElementField) + 1);
							ptr3[5] = *((ref int64Union.byteVal.FixedElementField) + 2);
							ptr3[4] = *((ref int64Union.byteVal.FixedElementField) + 3);
							ptr3[3] = *((ref int64Union.byteVal.FixedElementField) + 4);
							ptr3[2] = *((ref int64Union.byteVal.FixedElementField) + 5);
							ptr3[1] = *((ref int64Union.byteVal.FixedElementField) + 6);
							*ptr3 = *((ref int64Union.byteVal.FixedElementField) + 7);
						}
					}
					else
					{
						short num3 = encoding.nCvtType;
						switch (num3)
						{
						case 0:
							*ptr3 = 0;
							ptr3[1] = 0;
							break;
						case 1:
							*ptr3 = 0;
							ptr3[1] = 0;
							ptr3[2] = 0;
							ptr3[3] = 0;
							break;
						case 2:
						{
							int num7 = (int)encoding.nPrecision;
							int num8 = (num7 + 2) / 2;
							for (int j = 0; j < num8; j++)
							{
								ptr3[j] = 0;
							}
							if (encoding.nSign == 0)
							{
								byte* ptr9 = ptr3 + (num8 - 1);
								*ptr9 |= 12;
							}
							else
							{
								byte* ptr10 = ptr3 + (num8 - 1);
								*ptr10 |= 15;
							}
							break;
						}
						default:
							if (num3 != 13)
							{
								*ptr3 = 64;
								ptr3[1] = 0;
								ptr3[2] = 0;
								ptr3[3] = 0;
								ptr3[4] = 0;
								ptr3[5] = 0;
								ptr3[6] = 0;
								ptr3[7] = 0;
							}
							else
							{
								int num7 = (int)encoding.nPrecision;
								if (encoding.nOverpunch == 1)
								{
									byte b = this.codePageInfo.fixedEBCDIC.PLUS;
									int j;
									int num12;
									if (encoding.nTrailing == 1)
									{
										j = 1;
										num12 = 0;
									}
									else
									{
										j = 0;
										num12 = MaxResultLen - 1;
									}
									for (int k = 0; k < num7; k++)
									{
										ptr3[j] = 240;
										j++;
									}
									ptr3[num12] = b;
								}
								else
								{
									int j = 0;
									byte b;
									if (encoding.nSign == 0)
									{
										b = 192;
									}
									else
									{
										b = 240;
									}
									int num12;
									if (encoding.nTrailing == 1)
									{
										num12 = 0;
									}
									else
									{
										num12 = MaxResultLen - 1;
									}
									for (int k = 0; k < num7; k++)
									{
										ptr3[j] = 240;
										j++;
									}
									ptr3[num12] = b;
								}
							}
							break;
						}
					}
				}
				cumulativePackedDataLength = num;
			}
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x0008A915 File Offset: 0x00088B15
		public virtual void PackDouble(double DoubleValue, ref byte Buffer, ref int CumulativePackedLength, int MaxResultLen, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine)
		{
			this.FormatNumber(ref Buffer, DoubleValue, encoding, NumericEditStatemachine, ref CumulativePackedLength);
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x0008A92C File Offset: 0x00088B2C
		public unsafe virtual void UnpackDouble(ref byte Buffer, ref double ReturnedDouble, ref int RemainingBufferDataLength, int ResultLen, CEDAR_TYPE_ENCODING encoding)
		{
			int num = 0;
			BasePrimitiveConverter.Int16Union int16Union = default(BasePrimitiveConverter.Int16Union);
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union2 = default(BasePrimitiveConverter.Int64Union);
			bool flag = false;
			int num2 = RemainingBufferDataLength - ResultLen;
			try
			{
				try
				{
					fixed (byte* ptr = &Buffer)
					{
						byte* ptr2 = ptr;
						byte* ptr3 = ptr2;
						if (encoding.nCvtType == 75 || encoding.nCvtType == 39)
						{
							int64Union.dblVal = *(double*)ptr3;
						}
						else if (encoding.nCvtType == 79)
						{
							byte* ptr4 = (byte*)(&int64Union.dblVal);
							ptr4[7] = *ptr3;
							ptr4[6] = ptr3[1];
							ptr4[5] = ptr3[2];
							ptr4[4] = ptr3[3];
							ptr4[3] = ptr3[4];
							ptr4[2] = ptr3[5];
							ptr4[1] = ptr3[6];
							*ptr4 = ptr3[7];
						}
						else
						{
							ConvertedDataType nCvtType = (ConvertedDataType)encoding.nCvtType;
							if (nCvtType <= ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
							{
								switch (nCvtType)
								{
								case ConvertedDataType.CEDAR_COBOL_COMP94:
									goto IL_02D3;
								case ConvertedDataType.CEDAR_COBOL_COMP99:
									goto IL_0351;
								case ConvertedDataType.CEDAR_COBOL_COMP3:
									break;
								default:
									if (nCvtType != ConvertedDataType.CEDAR_COBOL_DISPLAY_NUM)
									{
										goto IL_03F7;
									}
									goto IL_01E4;
								}
							}
							else
							{
								switch (nCvtType)
								{
								case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP94:
									goto IL_02D3;
								case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP99:
									goto IL_0351;
								case ConvertedDataType.CEDAR_COBOL_NULLABLE_COMP3:
									break;
								default:
									if (nCvtType != ConvertedDataType.CEDAR_COBOL_NULLABLE_DISPLAY_NUM)
									{
										goto IL_03F7;
									}
									goto IL_01E4;
								}
							}
							int num3;
							bool flag2;
							if (this.HandleSignReturnZero(ptr2, ResultLen, encoding.nSign == 1, out num3, out flag2))
							{
								ReturnedDouble = 0.0;
								RemainingBufferDataLength -= ResultLen;
								return;
							}
							int num4 = (int)encoding.nPrecision;
							int num5 = (int)encoding.nScale;
							int num6 = num4 % 2;
							flag = true;
							this.ConvertExtendedPackedDec(ptr3, num4, num6, ref num, ref int64Union.int64Val);
							if (num5 - num > 18)
							{
								num6 = 18;
							}
							else
							{
								num6 = num5 - num;
							}
							this.GetDoubleFraction(int64Union.int64Val, ref int64Union.dblVal, num5, num6, encoding);
							if (num5 - num > 18)
							{
								num6 = num5 - num - 18;
								int64Union.dblVal /= (double)BasePrimitiveConverter.longPower[num6];
							}
							if (!flag2)
							{
								int64Union.dblVal = -int64Union.dblVal;
								goto IL_0714;
							}
							goto IL_0714;
							IL_01E4:
							int num7;
							int num8;
							if (this.HandleSignReturnZero(ptr2, ResultLen, encoding.nSign == 1, encoding.nOverpunch == 1, encoding.nTrailing == 1, out num3, out num7, out num8, out flag2))
							{
								ReturnedDouble = 0.0;
								RemainingBufferDataLength -= ResultLen;
								return;
							}
							num4 = (int)encoding.nPrecision;
							num5 = (int)encoding.nScale;
							flag = true;
							this.ConvertExtendedZonedDec(ptr3 + num7, encoding, num8 - num7 + 1, ref num, ref int64Union.int64Val);
							if (num5 - num > 18)
							{
								num6 = 18;
							}
							else
							{
								num6 = num5 - num;
							}
							this.GetDoubleFraction(int64Union.int64Val, ref int64Union.dblVal, num5, num6, encoding);
							if (num5 - num > 18)
							{
								num6 = num5 - num - 18;
								int64Union.dblVal /= (double)BasePrimitiveConverter.longPower[num6];
							}
							if (!flag2)
							{
								int64Union.dblVal = -int64Union.dblVal;
								goto IL_0714;
							}
							goto IL_0714;
							IL_02D3:
							num5 = (int)encoding.nScale;
							int64Union.int64Val = 0L;
							int64Union.byteVal.FixedElementField = ptr3[1];
							*((ref int64Union.byteVal.FixedElementField) + 1) = *ptr3;
							if (int64Union.int16Val < 0 && encoding.nSign == 1)
							{
								throw new CustomHISException(SR.UnsignedHost, 1596, this.UserCompatibleErrorCode);
							}
							int64Union.dblVal = (double)int64Union.int64Val / (double)BasePrimitiveConverter.longPower[num5];
							goto IL_0714;
							IL_0351:
							num5 = (int)encoding.nScale;
							int64Union.int64Val = 0L;
							int64Union.byteVal.FixedElementField = ptr3[3];
							*((ref int64Union.byteVal.FixedElementField) + 1) = ptr3[2];
							*((ref int64Union.byteVal.FixedElementField) + 2) = ptr3[1];
							*((ref int64Union.byteVal.FixedElementField) + 3) = *ptr3;
							if (int64Union.int32Val < 0 && encoding.nSign == 1)
							{
								throw new CustomHISException(SR.UnsignedHost, 1596, this.UserCompatibleErrorCode);
							}
							int64Union.dblVal = (double)int64Union.int64Val / (double)BasePrimitiveConverter.longPower[num5];
							goto IL_0714;
							IL_03F7:
							int64Union.int64Val = 0L;
							for (int i = 0; i < 8; i++)
							{
								*((ref int64Union2.byteVal.FixedElementField) + i) = ptr3[i];
							}
							if (*((ref int64Union2.byteVal.FixedElementField) + 1) == 0 && *((ref int64Union2.byteVal.FixedElementField) + 2) == 0 && *((ref int64Union2.byteVal.FixedElementField) + 3) == 0 && *((ref int64Union2.byteVal.FixedElementField) + 4) == 0 && *((ref int64Union2.byteVal.FixedElementField) + 5) == 0 && *((ref int64Union2.byteVal.FixedElementField) + 6) == 0 && *((ref int64Union2.byteVal.FixedElementField) + 7) == 0)
							{
								int64Union.dblVal = 0.0;
							}
							else
							{
								int16Union.int16Val = 0;
								byte b = int64Union2.byteVal.FixedElementField & 128;
								int16Union.byteVal.FixedElementField = int64Union2.byteVal.FixedElementField & 127;
								short num9 = (int16Union.int16Val - 64) * 4;
								int64Union.byteVal.FixedElementField = *((ref int64Union2.byteVal.FixedElementField) + 7);
								*((ref int64Union.byteVal.FixedElementField) + 1) = *((ref int64Union2.byteVal.FixedElementField) + 6);
								*((ref int64Union.byteVal.FixedElementField) + 2) = *((ref int64Union2.byteVal.FixedElementField) + 5);
								*((ref int64Union.byteVal.FixedElementField) + 3) = *((ref int64Union2.byteVal.FixedElementField) + 4);
								*((ref int64Union.byteVal.FixedElementField) + 4) = *((ref int64Union2.byteVal.FixedElementField) + 3);
								*((ref int64Union.byteVal.FixedElementField) + 5) = *((ref int64Union2.byteVal.FixedElementField) + 2);
								*((ref int64Union.byteVal.FixedElementField) + 6) = *((ref int64Union2.byteVal.FixedElementField) + 1);
								*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
								num3 = 0;
								num7 = 0;
								while (num3 < 56 && num7 != 1)
								{
									if ((128 & *((ref int64Union.byteVal.FixedElementField) + 6)) != 0)
									{
										num7 = 1;
										ref byte ptr5 = (ref int64Union.byteVal.FixedElementField) + 6;
										ptr5 &= 127;
									}
									else
									{
										int64Union.int64Val <<= 1;
									}
									num9 -= 1;
									num3++;
								}
								num9 += 1023;
								if (num9 > 2046 || num9 < 0)
								{
									throw new CustomHISException(SR.PrecisionOverflow, 1501, this.UserCompatibleErrorCode);
								}
								if ((int64Union.byteVal.FixedElementField & 7) != 0)
								{
									if (1 == encoding.nTRE)
									{
										throw new CustomHISException(SR.PrecisionLoss, 1510, this.UserCompatibleErrorCode);
									}
									if (2 == encoding.nTRE)
									{
										int64Union.int64Val += 4L;
									}
								}
								int64Union.int64Val >>= 3;
								int64Union2.int32Val = (int)num9 << 4;
								*((ref int64Union.byteVal.FixedElementField) + 7) = *((ref int64Union2.byteVal.FixedElementField) + 1);
								ref byte ptr6 = (ref int64Union.byteVal.FixedElementField) + 6;
								ptr6 |= int64Union2.byteVal.FixedElementField & 240;
								ref byte ptr7 = (ref int64Union.byteVal.FixedElementField) + 7;
								ptr7 |= b;
							}
						}
						IL_0714:;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				ReturnedDouble = int64Union.dblVal;
				RemainingBufferDataLength = num2;
			}
			catch (CustomHISException)
			{
				if (!flag || !this.acceptAllInvalidNumerics)
				{
					throw;
				}
				ReturnedDouble = 0.0;
				RemainingBufferDataLength = num2;
			}
			catch (Exception ex)
			{
				throw new CustomHISException(SR.ExceptionOccurred(ex.Message));
			}
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x0008B0E0 File Offset: 0x000892E0
		public virtual void UnpackDouble(ref byte Buffer, ref double ReturnedDouble, ref int RemainingBufferLength, int ResultLen, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine)
		{
			long num;
			short num2;
			short num3;
			long num4;
			short num5;
			int num6;
			if (!this.ExtractNumber(out num, out num2, out num3, out num4, out num5, out num6, ref Buffer, NumericEditStatemachine))
			{
				ReturnedDouble = 0.0;
				RemainingBufferLength -= ResultLen;
			}
			RemainingBufferLength -= num6;
			ReturnedDouble = (double)num;
			int num7 = (int)(num4 - (long)num3);
			if (num7 < 0)
			{
				num7 = -num7;
				for (int i = 0; i < num7; i++)
				{
					ReturnedDouble /= 10.0;
				}
				return;
			}
			if (num7 > 0)
			{
				for (int j = 0; j < num7; j++)
				{
					ReturnedDouble *= 10.0;
				}
			}
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x0008B178 File Offset: 0x00089378
		private void CheckRoundFloat(ref float pflVal, CEDAR_TYPE_ENCODING encoding)
		{
			float num = pflVal;
			int nPrecision = (int)encoding.nPrecision;
			int nScale = (int)encoding.nScale;
			int num2 = nPrecision - nScale;
			float num3 = BasePrimitiveConverter.x_flValWhole[num2] + BasePrimitiveConverter.x_flValFrac[nScale + 1];
			if (2 == encoding.nTRE)
			{
				if (num > 0f)
				{
					num += BasePrimitiveConverter.x_flValRound[nScale];
				}
				else
				{
					num -= BasePrimitiveConverter.x_flValRound[nScale];
				}
			}
			if ((num > 0f && num > num3) || (num < 0f && num < -num3))
			{
				throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
			}
			pflVal = num;
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x0008B204 File Offset: 0x00089404
		private int CheckRoundDouble(ref double pdVal, CEDAR_TYPE_ENCODING encoding)
		{
			double num = pdVal;
			int nPrecision = (int)encoding.nPrecision;
			int nScale = (int)encoding.nScale;
			int num2 = nPrecision - nScale;
			double num3 = BasePrimitiveConverter.x_dValWhole[num2] + BasePrimitiveConverter.x_dValFrac[nScale + 1];
			if (2 == encoding.nTRE)
			{
				if (num > 0.0)
				{
					num += BasePrimitiveConverter.x_dValRound[nScale];
				}
				else
				{
					num -= BasePrimitiveConverter.x_dValRound[nScale];
				}
			}
			if ((num > 0.0 && num > num3) || (num < 0.0 && num < -num3))
			{
				throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
			}
			pdVal = num;
			return 0;
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x0008B29C File Offset: 0x0008949C
		private unsafe void BuildExtendedPackedDec(long llVal, int lPrecision, int lNibblePos, int ulOutLen, ref byte pbTo)
		{
			BasePrimitiveConverter.Int16Union int16Union = default(BasePrimitiveConverter.Int16Union);
			BasePrimitiveConverter.Int16Union int16Union2 = default(BasePrimitiveConverter.Int16Union);
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union2 = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union3 = default(BasePrimitiveConverter.Int64Union);
			int num = lNibblePos;
			int num2 = ulOutLen - 1;
			int64Union.int64Val = llVal;
			int64Union2.int64Val = 0L;
			int64Union3.int64Val = 0L;
			int64Union3.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
			*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
			int64Union2.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 6);
			*((ref int64Union.byteVal.FixedElementField) + 6) = 0;
			int i;
			for (i = (int)(int64Union3.int16Val / 8); i > 0; i--)
			{
				int64Union.int64Val *= 256L;
				int64Union2.int64Val *= 256L;
				int64Union2.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 6);
				*((ref int64Union.byteVal.FixedElementField) + 6) = 0;
			}
			i = (int)(int64Union3.int16Val % 8);
			int64Union.int64Val <<= i;
			int64Union2.int64Val <<= i;
			int j = 0;
			while (j < lPrecision)
			{
				if (num % 2 == 0)
				{
					i = 4;
				}
				else
				{
					num2--;
					i = 0;
				}
				int64Union3.int64Val = int64Union2.int64Val % 10L;
				int64Union2.int64Val /= 10L;
				int16Union.int16Val = 0;
				*((ref int16Union.byteVal.FixedElementField) + 1) = *((ref int64Union.byteVal.FixedElementField) + 6);
				int16Union2.int16Val = 0;
				*((ref int16Union2.byteVal.FixedElementField) + 1) = int64Union3.byteVal.FixedElementField;
				int16Union.int16Val += int16Union2.int16Val;
				*((ref int64Union.byteVal.FixedElementField) + 6) = *((ref int16Union.byteVal.FixedElementField) + 1);
				int64Union3.int64Val = int64Union.int64Val % 10L;
				int64Union3.int64Val <<= i;
				fixed (byte* ptr = &pbTo)
				{
					byte* ptr2 = ptr + num2;
					*ptr2 |= int64Union3.byteVal.FixedElementField;
				}
				int64Union.int64Val /= 10L;
				j++;
				num++;
			}
			if (int64Union.int64Val != 0L)
			{
				throw new CustomHISException(SR.PrecisionTooLarge, 1503, this.UserCompatibleErrorCode);
			}
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x0008B50C File Offset: 0x0008970C
		private unsafe void BuildPackedDecFraction(long llVal, int lScale, ref byte pbTo)
		{
			BasePrimitiveConverter.Int16Union int16Union = default(BasePrimitiveConverter.Int16Union);
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union2 = default(BasePrimitiveConverter.Int64Union);
			int64Union.int64Val = llVal;
			int64Union2.int64Val = 0L;
			int i;
			int num2;
			if (*((ref int64Union.byteVal.FixedElementField) + 7) != 0)
			{
				int16Union.int16Val = 0;
				int16Union.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
				*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
				int64Union2.int64Val = 0L;
				int num = (int)(int16Union.int16Val / 8);
				for (i = 0; i < num; i++)
				{
					*((ref int64Union2.byteVal.FixedElementField) + 7) = int64Union.byteVal.FixedElementField;
					int64Union.int64Val >>= 8;
					int64Union2.int64Val >>= 8;
					*((ref int64Union2.byteVal.FixedElementField) + 7) = 0;
				}
				num2 = (int)(int16Union.int16Val % 8);
				int16Union.int16Val = 0;
				*((ref int16Union.byteVal.FixedElementField) + 1) = int64Union.byteVal.FixedElementField;
				int16Union.int16Val = (short)(int16Union.int16Val >> num2);
				int64Union.int64Val >>= num2;
				int64Union2.int64Val >>= num2;
				ref byte ptr = (ref int64Union2.byteVal.FixedElementField) + 6;
				ptr |= int16Union.byteVal.FixedElementField;
			}
			int num3 = lScale;
			num2 = 0;
			i = 0;
			while (i < lScale)
			{
				int64Union2.int64Val *= 10L;
				int64Union.int64Val *= 10L;
				int16Union.int16Val = 0;
				int16Union.byteVal.FixedElementField = *((ref int64Union2.byteVal.FixedElementField) + 7) & 15;
				int64Union.int64Val += (long)int16Union.int16Val;
				int16Union.int16Val = 0;
				int16Union.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
				*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
				*((ref int64Union2.byteVal.FixedElementField) + 7) = 0;
				int num = num3 % 2;
				if (num != 0)
				{
					num = 4;
				}
				else
				{
					num = 0;
				}
				int16Union.int16Val = (short)(int16Union.int16Val << num);
				fixed (byte* ptr2 = &pbTo)
				{
					byte* ptr3 = ptr2 + num2;
					*ptr3 |= int16Union.byteVal.FixedElementField;
				}
				if (num == 0)
				{
					num2++;
				}
				i++;
				num3++;
			}
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x0008B760 File Offset: 0x00089960
		private unsafe bool BuildPackedDec(long inputValue, int precision, int nibblePos, int outLen, ref byte outBuffer, byte signValue)
		{
			bool flag;
			fixed (byte* ptr = &outBuffer)
			{
				byte* ptr2 = ptr;
				flag = this.BuildPackedDec(inputValue, precision, nibblePos, outLen, ptr2, signValue);
			}
			return flag;
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x0008B784 File Offset: 0x00089984
		private unsafe bool BuildPackedDec(long inputValue, int precision, int nibblePos, int outLen, byte* outBuffer, byte signValue)
		{
			long num = 0L;
			long num2;
			if (inputValue < 0L)
			{
				num2 = -inputValue;
			}
			else
			{
				num2 = inputValue;
			}
			int num3 = nibblePos;
			byte* ptr = (byte*)(&num);
			int i;
			for (i = 0; i < outLen; i++)
			{
				outBuffer[i] = 0;
			}
			byte* ptr2 = outBuffer + outLen - 1;
			byte* ptr3 = ptr2;
			*ptr3 |= signValue;
			i = 0;
			while (i < precision)
			{
				int num4;
				if (num3 % 2 == 0)
				{
					num4 = 4;
				}
				else
				{
					ptr2--;
					num4 = 0;
				}
				num = num2 % 10L;
				num <<= num4;
				byte* ptr4 = ptr2;
				*ptr4 |= *ptr;
				num2 /= 10L;
				i++;
				num3++;
			}
			return num2 == 0L;
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x0008B81C File Offset: 0x00089A1C
		private unsafe bool BuildZonedDec(long inputValue, int precision, short isSignSeparate, short isSignLeading, int MaxResultLen, ref byte Buffer, byte signValue)
		{
			long num;
			if (inputValue < 0L)
			{
				num = -inputValue;
			}
			else
			{
				num = inputValue;
			}
			fixed (byte* ptr = &Buffer)
			{
				byte* ptr2 = ptr;
				long num2;
				byte* ptr3 = (byte*)(&num2);
				int num3;
				if (isSignLeading == 1)
				{
					num3 = 0;
				}
				else
				{
					num3 = MaxResultLen - 1;
				}
				ptr2[num3] = byte.MaxValue;
				byte* ptr4 = ptr2 + precision - 1;
				if (isSignSeparate == 1 && isSignLeading == 1)
				{
					ptr4++;
				}
				for (int i = precision - 1; i > -1; i--)
				{
					num2 = num % 10L;
					*ptr4 = *ptr3 | 240;
					num /= 10L;
					ptr4--;
				}
				byte* ptr5 = ptr2 + num3;
				*ptr5 &= signValue;
			}
			return num == 0L;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x0008B8B8 File Offset: 0x00089AB8
		private unsafe void BuildExtendedZonedDec(long llVal, int lPrecision, ref byte pbTo)
		{
			BasePrimitiveConverter.Int16Union int16Union = default(BasePrimitiveConverter.Int16Union);
			BasePrimitiveConverter.Int16Union int16Union2 = default(BasePrimitiveConverter.Int16Union);
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union2 = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union3 = default(BasePrimitiveConverter.Int64Union);
			int64Union.int64Val = llVal;
			int64Union2.int64Val = 0L;
			int64Union3.int64Val = 0L;
			int64Union3.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
			*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
			int64Union2.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 6);
			*((ref int64Union.byteVal.FixedElementField) + 6) = 0;
			int i;
			for (i = (int)(int64Union3.int16Val / 8); i > 0; i--)
			{
				int64Union.int64Val *= 256L;
				int64Union2.int64Val *= 256L;
				int64Union2.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 6);
				*((ref int64Union.byteVal.FixedElementField) + 6) = 0;
			}
			i = (int)(int64Union3.int16Val % 8);
			int64Union.int64Val <<= i;
			int64Union2.int64Val <<= i;
			for (i = lPrecision - 1; i > -1; i--)
			{
				int64Union3.int64Val = int64Union2.int64Val % 10L;
				int64Union2.int64Val /= 10L;
				int16Union.int16Val = 0;
				*((ref int16Union.byteVal.FixedElementField) + 1) = *((ref int64Union.byteVal.FixedElementField) + 6);
				int16Union2.int16Val = 0;
				*((ref int16Union2.byteVal.FixedElementField) + 1) = int64Union3.byteVal.FixedElementField;
				int16Union.int16Val += int16Union2.int16Val;
				*((ref int64Union.byteVal.FixedElementField) + 6) = *((ref int16Union.byteVal.FixedElementField) + 1);
				int64Union3.int64Val = int64Union.int64Val % 10L;
				int64Union.int64Val /= 10L;
				fixed (byte* ptr = &pbTo)
				{
					ptr[i] = int64Union3.byteVal.FixedElementField | 240;
				}
			}
			if (int64Union.int64Val != 0L)
			{
				throw new CustomHISException(SR.PrecisionTooLarge, 1503, this.UserCompatibleErrorCode);
			}
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x0008BAFC File Offset: 0x00089CFC
		private unsafe void BuildZonedDecFraction(long llVal, int lScale, ref byte pbTo)
		{
			BasePrimitiveConverter.Int16Union int16Union = default(BasePrimitiveConverter.Int16Union);
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union2 = default(BasePrimitiveConverter.Int64Union);
			int64Union.int64Val = llVal;
			int64Union2.int64Val = 0L;
			if (*((ref int64Union.byteVal.FixedElementField) + 7) != 0)
			{
				int16Union.int16Val = 0;
				int16Union.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
				*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
				int64Union2.int64Val = 0L;
				int num = (int)(int16Union.int16Val / 8);
				for (int i = 0; i < num; i++)
				{
					*((ref int64Union2.byteVal.FixedElementField) + 7) = int64Union.byteVal.FixedElementField;
					int64Union.int64Val >>= 8;
					int64Union2.int64Val >>= 8;
					*((ref int64Union2.byteVal.FixedElementField) + 7) = 0;
				}
				int j = (int)(int16Union.int16Val % 8);
				int16Union.int16Val = 0;
				*((ref int16Union.byteVal.FixedElementField) + 1) = int64Union.byteVal.FixedElementField;
				int16Union.int16Val = (short)(int16Union.int16Val >> j);
				int64Union.int64Val >>= j;
				int64Union2.int64Val >>= j;
				ref byte ptr = (ref int64Union2.byteVal.FixedElementField) + 6;
				ptr |= int16Union.byteVal.FixedElementField;
			}
			for (int j = 0; j < lScale; j++)
			{
				int64Union2.int64Val *= 10L;
				int64Union.int64Val *= 10L;
				int16Union.int16Val = 0;
				int16Union.byteVal.FixedElementField = *((ref int64Union2.byteVal.FixedElementField) + 7) & 15;
				int64Union.int64Val += (long)int16Union.int16Val;
				int16Union.int16Val = 0;
				int16Union.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
				*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
				*((ref int64Union2.byteVal.FixedElementField) + 7) = 0;
				fixed (byte* ptr2 = &pbTo)
				{
					ptr2[j] = int16Union.byteVal.FixedElementField | 240;
				}
			}
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x0008BD24 File Offset: 0x00089F24
		private void BuildFixedBinFraction(long llVal, short sFractionLen, int lScale, out long pllVal)
		{
			pllVal = new BasePrimitiveConverter.Int64Union
			{
				int64Val = llVal
			}.int64Val >> (int)(56 - sFractionLen);
			pllVal *= BasePrimitiveConverter.longPower[lScale];
			long num;
			if (sFractionLen < 0)
			{
				num = 1L << (int)(-sFractionLen & 31);
				pllVal *= num;
				return;
			}
			num = 1L << (int)(sFractionLen & 31);
			pllVal /= num;
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x0008BD88 File Offset: 0x00089F88
		private unsafe bool BuildNumericStringDec(long inputValue, int precision, short isSignSeparate, short isSignLeading, short isUnsigned, int MaxResultLen, ref byte Buffer, byte signValue)
		{
			long num = inputValue;
			fixed (byte* ptr = &Buffer)
			{
				byte* ptr2 = ptr;
				fixed (byte* ptr3 = &this.codePageInfo.fixedEBCDIC.ZERO)
				{
					byte* ptr4 = ptr3;
					byte* ptr5 = ptr2 + MaxResultLen - 1;
					if (isUnsigned == 0 && isSignSeparate == 1)
					{
						int num2;
						if (isSignLeading == 1)
						{
							num2 = 0;
						}
						else
						{
							num2 = MaxResultLen - 1;
							ptr5--;
						}
						ptr2[num2] = signValue;
					}
					for (int i = precision - 1; i > -1; i--)
					{
						long num3 = num % 10L;
						*ptr5 = ptr4[num3];
						num /= 10L;
						ptr5--;
					}
				}
			}
			return num == 0L;
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x0008BE20 File Offset: 0x0008A020
		private unsafe bool ConvertPackedDec(byte* Buffer, int bytePortion, int ResultLen, int precision, ref long resultValue)
		{
			long num = 1L;
			resultValue = 0L;
			byte* ptr = Buffer + ResultLen - 1;
			int i = 0;
			while (i < precision)
			{
				byte b;
				if (bytePortion % 2 == 1)
				{
					b = *ptr & 15;
				}
				else
				{
					b = *ptr & 240;
					b = (byte)(b >> 4);
					ptr--;
				}
				if (b > 9)
				{
					return false;
				}
				resultValue += (long)((ulong)b * (ulong)num);
				num *= 10L;
				i++;
				bytePortion++;
			}
			return true;
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x0008BE8C File Offset: 0x0008A08C
		private unsafe void ConvertExtendedPackedDec(byte* pbFrom, int Precision, int NibblePos, ref int scaleAdjust, ref long resultValue)
		{
			int num = 0;
			BasePrimitiveConverter.Int16Union int16Union = default(BasePrimitiveConverter.Int16Union);
			bool flag = false;
			scaleAdjust = 0;
			int num2 = NibblePos;
			int num3 = 0;
			long num4 = 0L;
			int i = 0;
			while (i < Precision)
			{
				int16Union.int16Val = 0;
				int num5 = num2 % 2;
				if (1 == num5)
				{
					int16Union.byteVal.FixedElementField = pbFrom[num3] & 240;
					int16Union.int16Val = (short)(int16Union.int16Val >> 4);
				}
				else
				{
					int16Union.byteVal.FixedElementField = pbFrom[num3] & 15;
					num3++;
				}
				if (int16Union.int16Val > 9)
				{
					throw new CustomHISException(SR.BadPackedDec, 1524, this.UserCompatibleErrorCode);
				}
				if (Precision > 18)
				{
					if ((int16Union.int16Val != 0 || flag) && num < 18)
					{
						num4 *= 10L;
						num4 += (long)int16Union.int16Val;
						flag = true;
						num++;
					}
					else if (num >= 18)
					{
						scaleAdjust++;
					}
				}
				else
				{
					num4 *= 10L;
					num4 += (long)int16Union.int16Val;
				}
				i++;
				num2++;
			}
			resultValue = num4;
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x0008BFA0 File Offset: 0x0008A1A0
		private unsafe bool ConvertZonedDec(byte* buffer, CEDAR_TYPE_ENCODING encoding, int signPosition, byte signMask, ref long resultValue)
		{
			byte b = 0;
			if (encoding.nOverpunch != 1)
			{
				if (encoding.nTrailing == 1)
				{
					b = *buffer;
					*buffer |= signMask;
				}
				else
				{
					b = buffer[signPosition - 1];
					byte* ptr = buffer + (signPosition - 1);
					*ptr |= signMask;
				}
			}
			resultValue = 0L;
			int num = 1;
			for (int i = signPosition - 1; i > -1; i--)
			{
				if ((buffer[i] & signMask) != signMask)
				{
					return false;
				}
				byte b2 = buffer[i] & 15;
				if (b2 > 9)
				{
					return false;
				}
				resultValue += (long)((int)b2 * num);
				num *= 10;
			}
			if (encoding.nOverpunch != 1)
			{
				if (encoding.nTrailing == 1)
				{
					*buffer = b;
				}
				else
				{
					buffer[signPosition - 1] = b;
				}
			}
			return true;
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x0008C044 File Offset: 0x0008A244
		private unsafe void ConvertExtendedZonedDec(byte* pbFrom, CEDAR_TYPE_ENCODING EncodeType, int lDataLen, ref int plScaleAdjust, ref long pllTo)
		{
			BasePrimitiveConverter.Int16Union int16Union = default(BasePrimitiveConverter.Int16Union);
			byte b = 240;
			int num = 0;
			bool flag = false;
			plScaleAdjust = 0;
			if (EncodeType.nOverpunch != 1)
			{
				if (EncodeType.nTrailing == 1)
				{
					b = *pbFrom;
					*pbFrom |= 240;
				}
				else
				{
					b = pbFrom[lDataLen - 1];
					byte* ptr = pbFrom + (lDataLen - 1);
					*ptr |= 240;
				}
			}
			long num2 = 0L;
			for (int i = 0; i < lDataLen; i++)
			{
				int16Union.int16Val = 0;
				if ((pbFrom[i] & 240) != 240)
				{
					throw new CustomHISException(SR.BadZonedDec, 1562, this.UserCompatibleErrorCode);
				}
				int16Union.byteVal.FixedElementField = pbFrom[i] & 15;
				if (int16Union.int16Val > 9)
				{
					throw new CustomHISException(SR.BadZonedDec, 1562, this.UserCompatibleErrorCode);
				}
				if (lDataLen > 18)
				{
					if ((int16Union.int16Val != 0 || flag) && num < 18)
					{
						num2 *= 10L;
						num2 += (long)int16Union.int16Val;
						flag = true;
						num++;
					}
					else if (num >= 18)
					{
						plScaleAdjust++;
					}
				}
				else
				{
					num2 *= 10L;
					num2 += (long)int16Union.int16Val;
				}
			}
			pllTo = num2;
			if (EncodeType.nOverpunch != 1)
			{
				if (EncodeType.nTrailing == 1)
				{
					*pbFrom = b;
					return;
				}
				pbFrom[lDataLen - 1] = b;
			}
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x0008C190 File Offset: 0x0008A390
		private unsafe bool ConvertNumericStringDec(byte* buffer, CEDAR_TYPE_ENCODING encoding, int signPosition, ref long resultValue)
		{
			int num = 1;
			if (encoding.nOverpunch == 1)
			{
				byte b;
				if (encoding.nTrailing == 1)
				{
					b = *buffer;
				}
				else
				{
					b = buffer[signPosition - 1];
				}
				if (b == this.codePageInfo.fixedEBCDIC.MINUS)
				{
					num = -1;
				}
			}
			resultValue = 0L;
			int num2 = 1;
			fixed (byte* ptr = &this.codePageInfo.fixedEBCDIC.ZERO)
			{
				byte* ptr2 = ptr;
				for (int i = signPosition - 1; i > -1; i--)
				{
					long num3 = 10L;
					int num4 = 0;
					while (i < 10)
					{
						if (buffer[i] == ptr2[num4])
						{
							num3 = (long)num4;
							break;
						}
						num4++;
					}
					if (num3 > 9L)
					{
						return false;
					}
					resultValue += num3 * (long)num2;
					num2 *= 10;
				}
			}
			resultValue *= (long)num;
			return true;
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x0008C254 File Offset: 0x0008A454
		private void GetDoubleFraction(long llVal, ref double DResult, int lScale, int lTemp, CEDAR_TYPE_ENCODING encoding)
		{
			long num = 0L;
			long num2 = 0L;
			bool flag = false;
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union2 = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.ExpUnion expUnion = default(BasePrimitiveConverter.ExpUnion);
			if (lTemp >= 0)
			{
				num = (long)lTemp;
				num2 = BasePrimitiveConverter.longPower[lTemp];
			}
			else if (lTemp < 0)
			{
				num = (long)(-lTemp - 1);
				num2 = BasePrimitiveConverter.longPower[-lTemp - 1];
			}
			int64Union.int64Val = llVal / num2;
			ulong num3 = 0UL;
			short num4 = 1011;
			ulong num5;
			if (int64Union.int64Val < 9007199254740991L)
			{
				int64Union2.int64Val = llVal;
				int64Union2.int64Val -= int64Union.int64Val * num2;
				if (int64Union.int64Val == 0L && int64Union2.int64Val == 0L)
				{
					DResult = 0.0;
					return;
				}
				if (int64Union2.int64Val > 0L)
				{
					num = (long)lScale - num;
					int64Union2.int64Val *= BasePrimitiveConverter.longPower[(int)(checked((IntPtr)(unchecked((long)(18 - lScale) + num))))];
					while (int64Union2.int64Val < 36028797018963968L)
					{
						int64Union2.int64Val <<= 4;
						num4 -= 4;
					}
					num = 0L;
					do
					{
						int64Union2.int64Val *= 2L;
						if (int64Union2.int64Val >= 1000000000000000000L)
						{
							num3 |= 1UL;
							int64Union2.int64Val -= 1000000000000000000L;
						}
						num3 *= 2UL;
						num += 1L;
					}
					while (int64Union2.int64Val != 0L && num < 63L);
					if (num < 63L)
					{
						num3 <<= (int)(63L - num);
					}
				}
				num = 0L;
				if (num3 > 0UL)
				{
					if (num3 < 4503599627370496UL)
					{
						num5 = 0UL;
						while (num5 == 0UL)
						{
							if (num >= 52L)
							{
								break;
							}
							num3 *= 2UL;
							num5 = num3 & 4503599627370496UL;
							num4 -= 1;
							num += 1L;
						}
					}
					else
					{
						while ((num3 & 18437736874454810624UL) != 0UL)
						{
							num4 += 1;
							flag = (num3 & 1UL) != 0UL;
							num3 /= 2UL;
						}
					}
				}
			}
			short num6 = num4;
			num4 = 1075;
			if (int64Union.int64Val > 9007199254740991L && encoding.nTRE != 0 && (int64Union.int64Val + 5L) / 10L * 10L > int64Union.int64Val)
			{
				int64Union.int64Val += 1L;
			}
			while (int64Union.int64Val > 9007199254740991L)
			{
				int64Union.int64Val /= 2L;
				num4 += 1;
			}
			num5 = (ulong)(int64Union.int64Val & 4503599627370496L);
			num = 0L;
			if (int64Union.int64Val > 0L)
			{
				while (num5 == 0UL && num < 52L)
				{
					int64Union.int64Val *= 2L;
					num5 = (ulong)(int64Union.int64Val & 4503599627370496L);
					num4 -= 1;
					num += 1L;
				}
				int64Union.int64Val &= 4503599627370495L;
				num = (long)(num4 - num6);
			}
			else
			{
				num4 = num6;
				num = 0L;
				num3 &= 4503599627370495UL;
			}
			if (num3 > 0UL)
			{
				if (num > 0L)
				{
					num3 >>= (int)(num - 1L);
					if (encoding.nTRE != 0 && (num3 & 1UL) != 0UL)
					{
						num3 += 1UL;
						num3 /= 2UL;
						checked
						{
							if ((num3 & BasePrimitiveConverter.llBitFractionArray[(int)((IntPtr)(unchecked(num - 1L)))]) != 0UL)
							{
								num3 ^= BasePrimitiveConverter.llBitFractionArray[(int)((IntPtr)(unchecked(num - 1L)))];
								unchecked
								{
									int64Union.int64Val += 1L;
								}
							}
						}
						while ((int64Union.int64Val & 4503599627370496L) != 0L)
						{
							num3 /= 2UL;
							int64Union.int64Val /= 2L;
							num4 -= 1;
						}
					}
					else
					{
						num3 /= 2UL;
					}
				}
				else if (flag && encoding.nTRE != 0)
				{
					num3 += 1UL;
					if ((num3 & 4503599627370496UL) != 0UL)
					{
						num4 += 1;
						num3 /= 2UL;
					}
				}
			}
			int64Union.int64Val += (long)num3;
			expUnion.int64Val = 0L;
			expUnion.int16Val = num4 * 16;
			int64Union.int64Val |= expUnion.int64Val;
			DResult = int64Union.dblVal;
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x0008C64C File Offset: 0x0008A84C
		public bool IsSignedMask(int hostLanguageType, string EditMask)
		{
			string text;
			int num;
			bool flag;
			this.VerifyMask(hostLanguageType, CEDAR_TYPE_ENCODING.EmptyEncoding, EditMask, out text, out num, 1, out flag);
			return flag;
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x0008C670 File Offset: 0x0008A870
		public bool VerifyMask(int iLang, CEDAR_TYPE_ENCODING encodeType, string EditMask, out string OutStateMach, out int NumericItemLength, int CurrencySymbolLength)
		{
			bool flag;
			return this.VerifyMask(iLang, encodeType, EditMask, out OutStateMach, out NumericItemLength, CurrencySymbolLength, out flag);
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x0008C690 File Offset: 0x0008A890
		private bool VerifyMask(int iLang, CEDAR_TYPE_ENCODING encodeType, string EditMask, out string OutStateMach, out int NumericItemLength, int CurrencySymbolLength, out bool isSigned)
		{
			bool flag = true;
			OutStateMach = null;
			NumericItemLength = 0;
			isSigned = false;
			if (EditMask.Length >= 256)
			{
				return false;
			}
			string empty = string.Empty;
			if (!this.PreprocessMask(EditMask, out empty, iLang))
			{
				flag = false;
			}
			if (flag)
			{
				flag = this.BuildStateMach(empty, encodeType, CurrencySymbolLength, out NumericItemLength, out OutStateMach, iLang, out isSigned);
			}
			return flag;
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x0008C6E4 File Offset: 0x0008A8E4
		private bool PreprocessMask(string MaskIn, out string NewMask, int Lang)
		{
			int num = 0;
			int num2 = 0;
			char c = '\0';
			int num3 = 0;
			char[] array = new char[256];
			NewMask = null;
			for (;;)
			{
				if (MaskIn[num] == '(')
				{
					if (Lang == 1)
					{
						c = MaskIn[num - 1];
					}
					num++;
					int i = num;
					while (i < MaskIn.Length && MaskIn[i] != ')')
					{
						i++;
					}
					if (i == MaskIn.Length)
					{
						break;
					}
					int num4 = i + 1;
					if (Lang == 2)
					{
						c = MaskIn[num4];
					}
					int num5 = 0;
					int num6 = 1;
					for (i--; i >= num; i--)
					{
						switch (MaskIn[i])
						{
						case '0':
							goto IL_0122;
						case '1':
							num5 += num6;
							goto IL_0122;
						case '2':
							num5 += 2 * num6;
							goto IL_0122;
						case '3':
							num5 += 3 * num6;
							goto IL_0122;
						case '4':
							num5 += 4 * num6;
							goto IL_0122;
						case '5':
							num5 += 5 * num6;
							goto IL_0122;
						case '6':
							num5 += 6 * num6;
							goto IL_0122;
						case '7':
							num5 += 7 * num6;
							goto IL_0122;
						case '8':
							num5 += 8 * num6;
							goto IL_0122;
						case '9':
							num5 += 9 * num6;
							goto IL_0122;
						}
						return false;
						IL_0122:
						num6 *= 10;
					}
					if (num5 + num3 > 249)
					{
						return false;
					}
					i = 0;
					while (i < num5 - 1)
					{
						array[num2] = c;
						i++;
						num2++;
					}
					num3 += num5;
					num = num4;
				}
				else
				{
					array[num2] = MaskIn[num];
					num2++;
					num++;
					num3++;
				}
				if (num >= MaskIn.Length)
				{
					goto Block_10;
				}
			}
			return false;
			Block_10:
			if (array[num2 - 1] == '\0')
			{
				num2--;
			}
			NewMask = new string(array, 0, num2);
			return true;
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x0008C894 File Offset: 0x0008AA94
		private bool IsPrevFloatInsertion(string Mask, int CurrMaskPos, int MaskStartPos)
		{
			int num = CurrMaskPos - 1;
			if (num >= MaskStartPos)
			{
				char c = Mask[num];
				switch (c)
				{
				case ',':
				case '.':
				case '/':
					return false;
				case '-':
					break;
				default:
					if (c == 'B' || c == 'b')
					{
						return false;
					}
					break;
				}
				return Mask[num] == Mask[CurrMaskPos];
			}
			return false;
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x0008C8EC File Offset: 0x0008AAEC
		private bool IsNextFloatInsertion(string Mask, int iMaskPos)
		{
			int num = iMaskPos + 1;
			if (num < Mask.Length)
			{
				char c = Mask[num];
				switch (c)
				{
				case ',':
				case '/':
				case '0':
					return false;
				case '-':
				case '.':
					break;
				default:
					if (c == 'B' || c == 'b')
					{
						return false;
					}
					break;
				}
				return Mask[num] == Mask[iMaskPos];
			}
			return false;
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x0008C94C File Offset: 0x0008AB4C
		public bool BuildStateMach(string InMask, CEDAR_TYPE_ENCODING encodeType, int CurrencySymbolLength, out int ItemDataLength, out string EditStateMachine, int iLang, out bool isSigned)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 1;
			int num5 = 1;
			int num6 = 0;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			char[] array = new char[256];
			char[] array2 = new char[256];
			isSigned = false;
			EditStateMachine = null;
			ItemDataLength = 0;
			string text;
			if (!this.PreprocessMask(InMask, out text, iLang))
			{
				return false;
			}
			int cvtEncoding = encodeType.CvtEncoding;
			array2[0] = 'A';
			for (;;)
			{
				char c = text[num2];
				if (c <= 'I')
				{
					switch (c)
					{
					case '$':
						flag8 = true;
						if (this.IsNextFloatInsertion(text, num2))
						{
							if (flag)
							{
								array2[num4] = 'd';
							}
							else if (num6 != 0)
							{
								array2[num4] = 'l';
							}
							else
							{
								array2[num4] = '#';
							}
						}
						else if (this.IsPrevFloatInsertion(text, num2, num3))
						{
							if (flag)
							{
								array2[num4] = 'd';
							}
							else
							{
								array2[num4] = 'l';
							}
						}
						else
						{
							array2[num4] = '$';
						}
						if (num6 != 0)
						{
							char c2 = text[num2 - 1];
							if (c2 <= 'B')
							{
								switch (c2)
								{
								case '$':
								case '+':
								case ',':
								case '-':
								case '.':
								case '/':
								case '0':
									break;
								case '%':
								case '&':
								case '\'':
								case '(':
								case ')':
								case '*':
									return false;
								default:
									if (c2 != 'B')
									{
										return false;
									}
									break;
								}
							}
							else if (c2 != 'V' && c2 != 'b' && c2 != 'v')
							{
								return false;
							}
						}
						num2++;
						num4++;
						ItemDataLength++;
						num6++;
						num++;
						break;
					case '%':
					case '&':
					case '\'':
					case '(':
					case ')':
						return false;
					case '*':
						if (num6 != 0)
						{
							char c2 = text[num2 - 1];
							if (c2 <= 'B')
							{
								switch (c2)
								{
								case '$':
								case '*':
								case '+':
								case ',':
								case '-':
								case '.':
								case '/':
								case '0':
									break;
								case '%':
								case '&':
								case '\'':
								case '(':
								case ')':
									return false;
								default:
									if (c2 != 'B')
									{
										return false;
									}
									break;
								}
							}
							else if (c2 != 'V' && c2 != 'b' && c2 != 'v')
							{
								return false;
							}
						}
						if (flag)
						{
							array2[num4] = 'A';
						}
						else
						{
							array2[num4] = '*';
						}
						num4++;
						ItemDataLength++;
						num2++;
						num6++;
						num = 2;
						break;
					case '+':
						if (this.IsNextFloatInsertion(text, num2))
						{
							if (flag)
							{
								array2[num4] = 'p';
							}
							else if (num6 != 0)
							{
								array2[num4] = 'P';
							}
							else
							{
								array2[num4] = '>';
							}
						}
						else if (this.IsPrevFloatInsertion(text, num2, num3))
						{
							if (flag)
							{
								array2[num4] = 'p';
							}
							else
							{
								array2[num4] = 'P';
							}
						}
						else
						{
							array2[num4] = '+';
						}
						if (num6 != 0)
						{
							char c2 = text[num2 - 1];
							if (c2 <= 'V')
							{
								if (c2 <= '9')
								{
									switch (c2)
									{
									case '$':
									case '*':
									case '+':
									case ',':
									case '.':
									case '/':
									case '0':
										goto IL_0936;
									case '%':
									case '&':
									case '\'':
									case '(':
									case ')':
									case '-':
										return false;
									default:
										if (c2 != '9')
										{
											return false;
										}
										goto IL_0936;
									}
								}
								else
								{
									if (c2 == 'B')
									{
										goto IL_0936;
									}
									if (c2 != 'E')
									{
										if (c2 != 'V')
										{
											return false;
										}
										goto IL_0936;
									}
								}
							}
							else if (c2 <= 'b')
							{
								if (c2 != 'Z' && c2 != 'b')
								{
									return false;
								}
								goto IL_0936;
							}
							else if (c2 != 'e')
							{
								if (c2 == 'v' || c2 == 'z')
								{
									goto IL_0936;
								}
								return false;
							}
							array2[num4] = 'X';
							goto IL_094A;
							IL_0936:
							isSigned = true;
						}
						else
						{
							isSigned = true;
						}
						IL_094A:
						num4++;
						ItemDataLength++;
						num2++;
						num6++;
						num++;
						break;
					case ',':
						if (num6 != 0)
						{
							char c2 = text[num2 - 1];
							if (c2 <= 'V')
							{
								if (c2 <= '9')
								{
									switch (c2)
									{
									case '$':
									case '*':
									case '+':
									case ',':
									case '-':
									case '.':
									case '/':
									case '0':
										break;
									case '%':
									case '&':
									case '\'':
									case '(':
									case ')':
										return false;
									default:
										if (c2 != '9')
										{
											return false;
										}
										break;
									}
								}
								else if (c2 != 'B' && c2 != 'V')
								{
									return false;
								}
							}
							else if (c2 <= 'b')
							{
								if (c2 != 'Z' && c2 != 'b')
								{
									return false;
								}
							}
							else if (c2 != 'v' && c2 != 'z')
							{
								return false;
							}
						}
						array2[num4] = ',';
						num4++;
						ItemDataLength++;
						num2++;
						num6++;
						break;
					case '-':
						if (this.IsNextFloatInsertion(text, num2))
						{
							if (flag)
							{
								array2[num4] = 'm';
							}
							else if (num6 != 0)
							{
								array2[num4] = 'M';
							}
							else
							{
								array2[num4] = '<';
							}
						}
						else if (this.IsPrevFloatInsertion(text, num2, num3))
						{
							if (flag)
							{
								array2[num4] = 'm';
							}
							else
							{
								array2[num4] = 'M';
							}
						}
						else
						{
							array2[num4] = '-';
						}
						if (num6 != 0)
						{
							char c2 = text[num2 - 1];
							if (c2 <= 'V')
							{
								if (c2 <= '9')
								{
									switch (c2)
									{
									case '$':
									case '*':
									case ',':
									case '-':
									case '.':
									case '/':
									case '0':
										goto IL_0A59;
									case '%':
									case '&':
									case '\'':
									case '(':
									case ')':
									case '+':
										return false;
									default:
										if (c2 != '9')
										{
											return false;
										}
										goto IL_0A59;
									}
								}
								else
								{
									if (c2 == 'B')
									{
										goto IL_0A59;
									}
									if (c2 != 'E')
									{
										if (c2 != 'V')
										{
											return false;
										}
										goto IL_0A59;
									}
								}
							}
							else if (c2 <= 'b')
							{
								if (c2 != 'Z' && c2 != 'b')
								{
									return false;
								}
								goto IL_0A59;
							}
							else if (c2 != 'e')
							{
								if (c2 == 'v' || c2 == 'z')
								{
									goto IL_0A59;
								}
								return false;
							}
							array2[num4] = 'x';
							goto IL_0A6D;
							IL_0A59:
							isSigned = true;
						}
						else
						{
							isSigned = true;
						}
						IL_0A6D:
						num2++;
						num4++;
						ItemDataLength++;
						num6++;
						num++;
						break;
					case '.':
						if (num6 != 0)
						{
							char c2 = text[num2 - 1];
							if (c2 <= 'B')
							{
								switch (c2)
								{
								case '$':
								case '*':
								case '+':
								case ',':
								case '-':
								case '/':
								case '0':
									break;
								case '%':
								case '&':
								case '\'':
								case '(':
								case ')':
								case '.':
									return false;
								default:
									if (c2 != '9' && c2 != 'B')
									{
										return false;
									}
									break;
								}
							}
							else if (c2 != 'Z' && c2 != 'b' && c2 != 'z')
							{
								return false;
							}
						}
						array2[num4] = 'h';
						if (text[num2 + 1] == '9' || text[num2 + 1] == '*')
						{
							array2[num4] = '.';
						}
						flag = true;
						num4++;
						ItemDataLength++;
						num2++;
						num6++;
						break;
					case '/':
						if (num6 != 0)
						{
							char c2 = text[num2 - 1];
							if (c2 <= 'V')
							{
								if (c2 <= '9')
								{
									switch (c2)
									{
									case '$':
									case '*':
									case '+':
									case ',':
									case '-':
									case '.':
									case '/':
									case '0':
										break;
									case '%':
									case '&':
									case '\'':
									case '(':
									case ')':
										return false;
									default:
										if (c2 != '9')
										{
											return false;
										}
										break;
									}
								}
								else if (c2 != 'B' && c2 != 'V')
								{
									return false;
								}
							}
							else if (c2 <= 'b')
							{
								if (c2 != 'Z' && c2 != 'b')
								{
									return false;
								}
							}
							else if (c2 != 'v' && c2 != 'z')
							{
								return false;
							}
						}
						array2[num4] = '/';
						num4++;
						ItemDataLength++;
						num2++;
						num6++;
						break;
					case '0':
						if (num6 != 0)
						{
							char c2 = text[num2 - 1];
							if (c2 <= 'V')
							{
								if (c2 <= '9')
								{
									switch (c2)
									{
									case '$':
									case '*':
									case '+':
									case ',':
									case '-':
									case '.':
									case '/':
									case '0':
										break;
									case '%':
									case '&':
									case '\'':
									case '(':
									case ')':
										return false;
									default:
										if (c2 != '9')
										{
											return false;
										}
										break;
									}
								}
								else if (c2 != 'B' && c2 != 'V')
								{
									return false;
								}
							}
							else if (c2 <= 'b')
							{
								if (c2 != 'Z' && c2 != 'b')
								{
									return false;
								}
							}
							else if (c2 != 'v' && c2 != 'z')
							{
								return false;
							}
						}
						array2[num4] = '0';
						num4++;
						ItemDataLength++;
						num2++;
						num6++;
						break;
					default:
						if (c != '9')
						{
							switch (c)
							{
							case 'B':
								goto IL_017B;
							case 'C':
								goto IL_0A8C;
							case 'D':
								goto IL_0B4F;
							case 'E':
								goto IL_0479;
							case 'F':
								goto IL_0D45;
							case 'I':
								goto IL_0CF5;
							}
							goto Block_5;
						}
						if (num6 != 0)
						{
							char c2 = text[num2 - 1];
							if (c2 <= 'V')
							{
								if (c2 <= '9')
								{
									switch (c2)
									{
									case '$':
									case '*':
									case '+':
									case ',':
									case '-':
									case '.':
									case '/':
									case '0':
										break;
									case '%':
									case '&':
									case '\'':
									case '(':
									case ')':
										return false;
									default:
										if (c2 != '9')
										{
											return false;
										}
										break;
									}
								}
								else if (c2 != 'B' && c2 != 'S' && c2 != 'V')
								{
									return false;
								}
							}
							else if (c2 <= 'b')
							{
								if (c2 != 'Z' && c2 != 'b')
								{
									return false;
								}
							}
							else if (c2 != 's' && c2 != 'v' && c2 != 'z')
							{
								return false;
							}
						}
						if (flag2)
						{
							array2[num4] = '7';
						}
						else if (flag)
						{
							array2[num4] = '8';
						}
						else
						{
							array2[num4] = '9';
						}
						num4++;
						ItemDataLength++;
						num2++;
						num6++;
						num = 2;
						break;
					}
				}
				else
				{
					switch (c)
					{
					case 'R':
						goto IL_0D1D;
					case 'S':
						goto IL_0D6D;
					case 'T':
						goto IL_0D45;
					case 'U':
					case 'W':
					case 'X':
					case 'Y':
						return false;
					case 'V':
						break;
					case 'Z':
						goto IL_057F;
					default:
						switch (c)
						{
						case 'b':
							goto IL_017B;
						case 'c':
							goto IL_0A8C;
						case 'd':
							goto IL_0B4F;
						case 'e':
							goto IL_0479;
						case 'f':
							goto IL_0D45;
						case 'g':
						case 'h':
							return false;
						case 'i':
							goto IL_0CF5;
						default:
							switch (c)
							{
							case 'r':
								goto IL_0D1D;
							case 's':
								goto IL_0D6D;
							case 't':
								goto IL_0D45;
							case 'v':
								goto IL_04D5;
							case 'z':
								goto IL_057F;
							}
							goto Block_8;
						}
						break;
					}
					IL_04D5:
					if (num6 != 0)
					{
						char c2 = text[num2 - 1];
						if (c2 <= 'S')
						{
							if (c2 <= '9')
							{
								switch (c2)
								{
								case '$':
								case '*':
								case '+':
								case ',':
								case '-':
								case '/':
								case '0':
									break;
								case '%':
								case '&':
								case '\'':
								case '(':
								case ')':
									return false;
								case '.':
									if (iLang == 1)
									{
										return false;
									}
									break;
								default:
									if (c2 != '9')
									{
										goto Block_67;
									}
									break;
								}
							}
							else if (c2 != 'B' && c2 != 'S')
							{
								goto Block_69;
							}
						}
						else if (c2 <= 'b')
						{
							if (c2 != 'Z' && c2 != 'b')
							{
								goto Block_72;
							}
						}
						else if (c2 != 's' && c2 != 'z')
						{
							goto Block_74;
						}
					}
					flag = true;
					num2++;
					goto IL_0E9B;
					IL_057F:
					if (num6 != 0)
					{
						char c2 = text[num2 - 1];
						if (c2 <= 'V')
						{
							switch (c2)
							{
							case '$':
							case '+':
							case ',':
							case '-':
							case '.':
							case '/':
							case '0':
								break;
							case '%':
							case '&':
							case '\'':
							case '(':
							case ')':
							case '*':
								return false;
							default:
								if (c2 != 'B' && c2 != 'V')
								{
									return false;
								}
								break;
							}
						}
						else if (c2 <= 'b')
						{
							if (c2 != 'Z' && c2 != 'b')
							{
								return false;
							}
						}
						else if (c2 != 'v' && c2 != 'z')
						{
							return false;
						}
					}
					if (!flag)
					{
						array2[num4] = 'Z';
					}
					else
					{
						array2[num4] = 'z';
					}
					num4++;
					ItemDataLength++;
					num2++;
					num6++;
					num = 2;
					goto IL_0E9B;
					IL_0D1D:
					if (flag5 || flag6 || iLang == 1)
					{
						return false;
					}
					if (flag)
					{
						array2[num4] = 'r';
						goto IL_0E9B;
					}
					array2[num4] = 'R';
					goto IL_0E9B;
					IL_0D6D:
					if (iLang == 1)
					{
						if (num6 != 0)
						{
							char c2 = text[num2 - 1];
							if (c2 != '9' && c2 != 'V' && c2 != 'v')
							{
								return false;
							}
						}
						isSigned = true;
						if ((cvtEncoding & 13) == 13 || (cvtEncoding & 43) == 43 || (cvtEncoding & 64) == 64 || (cvtEncoding & 65) == 65)
						{
							flag4 = true;
						}
						if (encodeType.nOverpunch == 1)
						{
							if (encodeType.nTrailing == 1)
							{
								array2[num4] = '+';
								num4++;
								ItemDataLength++;
								num6++;
								array2[num4] = '9';
								flag4 = false;
							}
							else
							{
								flag3 = true;
							}
						}
					}
					else
					{
						array2[num4] = '+';
						if (num6 != 0)
						{
							char c2 = text[num2 - 1];
							if (c2 <= 'E')
							{
								switch (c2)
								{
								case ',':
								case '.':
								case '/':
									goto IL_0E6D;
								case '-':
									return false;
								default:
									if (c2 == 'B')
									{
										goto IL_0E6D;
									}
									if (c2 != 'E')
									{
										return false;
									}
									break;
								}
							}
							else if (c2 <= 'b')
							{
								if (c2 != 'V' && c2 != 'b')
								{
									return false;
								}
								goto IL_0E6D;
							}
							else if (c2 != 'e')
							{
								if (c2 == 'v')
								{
									goto IL_0E6D;
								}
								return false;
							}
							array2[num4] = 'X';
							goto IL_0E7D;
							IL_0E6D:
							isSigned = true;
						}
						IL_0E7D:
						num4++;
						ItemDataLength++;
						num6++;
						num++;
					}
					num2++;
				}
				IL_0E9B:
				if (num2 >= text.Length)
				{
					goto Block_212;
				}
				continue;
				IL_017B:
				if (num6 != 0)
				{
					char c2 = text[num2 - 1];
					if (c2 <= 'V')
					{
						if (c2 <= '9')
						{
							switch (c2)
							{
							case '$':
							case '*':
							case '+':
							case ',':
							case '-':
							case '.':
							case '/':
							case '0':
								break;
							case '%':
							case '&':
							case '\'':
							case '(':
							case ')':
								return false;
							default:
								if (c2 != '9')
								{
									return false;
								}
								break;
							}
						}
						else if (c2 != 'B' && c2 != 'V')
						{
							return false;
						}
					}
					else if (c2 <= 'b')
					{
						if (c2 != 'Z' && c2 != 'b')
						{
							return false;
						}
					}
					else if (c2 != 'v' && c2 != 'z')
					{
						return false;
					}
				}
				array2[num4] = 'b';
				num4++;
				ItemDataLength++;
				num2++;
				num6++;
				goto IL_0E9B;
				IL_0479:
				if (num6 != 0)
				{
					char c2 = text[num2 - 1];
					if (c2 <= '.')
					{
						if (c2 != ',' && c2 != '.')
						{
							return false;
						}
					}
					else if (c2 != '9' && c2 != 'V' && c2 != 'v')
					{
						return false;
					}
				}
				flag2 = true;
				array2[num4] = 'E';
				num4++;
				ItemDataLength++;
				num2++;
				num6++;
				goto IL_0E9B;
				IL_0A8C:
				if (text[num2 + 1] != 'R' && text[num2 + 1] != 'r')
				{
					return false;
				}
				if (num6 != 0)
				{
					char c2 = text[num2 - 1];
					if (c2 <= 'B')
					{
						if (c2 <= '0')
						{
							if (c2 != '$')
							{
								switch (c2)
								{
								case '*':
								case ',':
								case '.':
								case '/':
								case '0':
									goto IL_0B2A;
								}
								return false;
							}
						}
						else if (c2 != '9' && c2 != 'B')
						{
							return false;
						}
					}
					else if (c2 <= 'Z')
					{
						if (c2 != 'V' && c2 != 'Z')
						{
							return false;
						}
					}
					else if (c2 != 'b' && c2 != 'v' && c2 != 'z')
					{
						return false;
					}
				}
				IL_0B2A:
				isSigned = true;
				array2[num4] = 'C';
				num4++;
				ItemDataLength += 2;
				num2 += 2;
				num6++;
				goto IL_0E9B;
				IL_0B4F:
				if (text[num2 + 1] != 'B' && text[num2 + 1] != 'b')
				{
					return false;
				}
				if (num6 != 0)
				{
					char c2 = text[num2 - 1];
					if (c2 <= 'B')
					{
						if (c2 <= '0')
						{
							if (c2 != '$')
							{
								switch (c2)
								{
								case '*':
								case ',':
								case '.':
								case '/':
								case '0':
									goto IL_0BED;
								}
								return false;
							}
						}
						else if (c2 != '9' && c2 != 'B')
						{
							return false;
						}
					}
					else if (c2 <= 'Z')
					{
						if (c2 != 'V' && c2 != 'Z')
						{
							return false;
						}
					}
					else if (c2 != 'b' && c2 != 'v' && c2 != 'z')
					{
						return false;
					}
				}
				IL_0BED:
				isSigned = true;
				array2[num4] = 'D';
				num4++;
				ItemDataLength += 2;
				num2 += 2;
				num6++;
				goto IL_0E9B;
				IL_0CF5:
				if (flag5 || flag7 || iLang == 1)
				{
					return false;
				}
				if (flag)
				{
					array2[num4] = 'i';
					goto IL_0E9B;
				}
				array2[num4] = 'I';
				goto IL_0E9B;
				IL_0D45:
				if (flag6 || flag7 || iLang == 1)
				{
					return false;
				}
				if (flag)
				{
					array2[num4] = 'k';
					goto IL_0E9B;
				}
				array2[num4] = 'K';
				goto IL_0E9B;
			}
			Block_5:
			Block_8:
			return false;
			Block_67:
			Block_69:
			Block_72:
			Block_74:
			return false;
			Block_212:
			if (num < 2)
			{
				return false;
			}
			if (flag4)
			{
				if (!flag3)
				{
					if (array2[num4 - 1] != '9' && array2[num4 - 1] != '8')
					{
						return false;
					}
					if (encodeType.nOverpunch == 1)
					{
						int i = 0;
						int num7 = num5;
						while (i < num6 + 1)
						{
							array[i] = array2[num7];
							i++;
							num7++;
						}
						array2[num5] = '+';
						num4 = num5;
						num4++;
						i = num4;
						num7 = 0;
						while (i < num6 + 1)
						{
							array2[num7] = array[i];
							i++;
							num7++;
						}
						num4 += num6 + 1;
						ItemDataLength = num4 - 1;
						num6++;
					}
					else if (flag)
					{
						array2[num4 - 1] = 'j';
					}
					else
					{
						array2[num4 - 1] = 'J';
					}
				}
				else
				{
					if (array2[num4] != '9')
					{
						return false;
					}
					if (flag)
					{
						array2[num4] = 'g';
					}
					else
					{
						array2[num4] = 'G';
					}
				}
			}
			EditStateMachine = new string(array2, 0, num4);
			short num8;
			short num9;
			short num10;
			this.ExtractPrecisionAndScale(out num8, out num9, out num10, EditStateMachine);
			if (flag8)
			{
				ItemDataLength += CurrencySymbolLength - 1;
			}
			return true;
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x0008D904 File Offset: 0x0008BB04
		public unsafe void FormatNumber(ref byte FormattedNumber, object InNumber, CEDAR_TYPE_ENCODING Encoding, string EditStateMachine, ref int PackedLength)
		{
			short num = 0;
			bool flag = false;
			int num2 = 1;
			bool flag2 = false;
			bool flag3 = false;
			byte b = this.codePageInfo.fixedEBCDIC.SPACE;
			byte b2 = this.codePageInfo.fixedEBCDIC.SPACE;
			byte[] array;
			short num3;
			short num4;
			byte[] array2;
			short num5;
			short num6;
			this.ExtractDigits(InNumber, Encoding, out array, out num3, out num4, out num, out array2, out num5, out num6, out flag2, EditStateMachine);
			int num7 = 0;
			int num8 = 0;
			fixed (byte* ptr = &array[0])
			{
				byte* ptr2 = ptr;
				fixed (byte* ptr3 = &array2[0])
				{
					byte* ptr4 = ptr3;
					fixed (byte* ptr5 = &FormattedNumber)
					{
						byte* ptr6 = ptr5;
						byte* ptr7 = ptr2;
						byte* ptr8 = ptr6;
						for (;;)
						{
							char c = EditStateMachine[num2];
							switch (c)
							{
							case '#':
								goto IL_03FC;
							case '$':
							{
								byte* ptr9 = ptr8;
								int i = 0;
								while (i < this.codePageInfo.ebcdicCurrency.Length)
								{
									*ptr9 = this.codePageInfo.ebcdicCurrency[i];
									i++;
									ptr9++;
								}
								ptr8 += this.codePageInfo.ebcdicCurrency.Length;
								break;
							}
							case '%':
							case '&':
							case '\'':
							case '(':
							case ')':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case ':':
							case ';':
							case '=':
							case '?':
							case '@':
							case 'B':
							case 'F':
							case 'H':
							case 'L':
							case 'N':
							case 'O':
							case 'Q':
								return;
							case '*':
								if (*ptr7 == 0 && flag)
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.ASTERISK;
									b2 = this.codePageInfo.fixedEBCDIC.ASTERISK;
								}
								else
								{
									*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
									flag = false;
								}
								ptr7++;
								ptr8++;
								break;
							case '+':
								if (num < 0 && !flag2)
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.MINUS;
								}
								else
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.PLUS;
								}
								ptr8++;
								break;
							case ',':
								if (flag)
								{
									*ptr8 = b2;
								}
								else
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.COMMA;
								}
								ptr8++;
								break;
							case '-':
								if (flag && flag2)
								{
									*ptr8 = b2;
								}
								else if (num >= 0)
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
								}
								else
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.MINUS;
								}
								ptr8++;
								break;
							case '.':
								if (b != this.codePageInfo.fixedEBCDIC.SPACE)
								{
									byte* ptr9 = ptr8 - 1;
									*ptr9 = b;
									b = this.codePageInfo.fixedEBCDIC.SPACE;
								}
								b2 = this.codePageInfo.fixedEBCDIC.SPACE;
								*ptr8 = this.codePageInfo.fixedEBCDIC.PERIOD;
								ptr8++;
								break;
							case '/':
								if (flag)
								{
									*ptr8 = b2;
								}
								else
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.SLASH;
								}
								ptr8++;
								break;
							case '0':
								if (flag)
								{
									*ptr8 = b2;
								}
								else
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.ZERO;
								}
								ptr8++;
								break;
							case '7':
								flag = false;
								*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
								ptr7++;
								ptr8++;
								break;
							case '8':
								flag = false;
								*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
								ptr7++;
								ptr8++;
								break;
							case '9':
								if (flag)
								{
									byte* ptr9 = ptr8 - 1;
									if (flag3)
									{
										int i = 0;
										while (i < this.codePageInfo.ebcdicCurrency.Length)
										{
											*ptr9 = this.codePageInfo.ebcdicCurrency[i];
											i++;
											ptr9++;
										}
										flag3 = false;
										ptr8 += this.codePageInfo.ebcdicCurrency.Length - 1;
									}
									else
									{
										*ptr9 = b2;
									}
								}
								flag = false;
								*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
								ptr7++;
								ptr8++;
								break;
							case '<':
							case 'M':
								if (*ptr7 == 0 && num7 == 0)
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
									if (num >= 0)
									{
										b = this.codePageInfo.fixedEBCDIC.SPACE;
									}
									else
									{
										b = this.codePageInfo.fixedEBCDIC.MINUS;
									}
									flag = true;
									ptr8++;
								}
								else if (*ptr7 == 0 && flag && flag2)
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
									ptr7++;
									ptr8++;
								}
								else
								{
									if (*ptr7 == 0 && flag)
									{
										*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
									}
									else
									{
										*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
										if (b != this.codePageInfo.fixedEBCDIC.SPACE)
										{
											byte* ptr9 = ptr8 - 1;
											if (num >= 0)
											{
												*ptr9 = this.codePageInfo.fixedEBCDIC.SPACE;
											}
											else
											{
												*ptr9 = this.codePageInfo.fixedEBCDIC.MINUS;
											}
											b = this.codePageInfo.fixedEBCDIC.SPACE;
										}
										flag = false;
									}
									ptr7++;
									ptr8++;
								}
								num7++;
								break;
							case '>':
							case 'P':
								if (*ptr7 == 0 && num7 == 0)
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
									if (num >= 0)
									{
										b = this.codePageInfo.fixedEBCDIC.PLUS;
									}
									else
									{
										b = this.codePageInfo.fixedEBCDIC.SPACE;
									}
									flag = true;
									ptr8++;
								}
								else if (*ptr7 == 0 && flag && flag2)
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
									ptr7++;
									ptr8++;
								}
								else
								{
									if (*ptr7 == 0 && flag)
									{
										*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
									}
									else
									{
										*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
										if (b != this.codePageInfo.fixedEBCDIC.SPACE)
										{
											byte* ptr9 = ptr8 - 1;
											if (num >= 0)
											{
												*ptr9 = this.codePageInfo.fixedEBCDIC.PLUS;
											}
											else
											{
												*ptr9 = this.codePageInfo.fixedEBCDIC.SPACE;
											}
											b = this.codePageInfo.fixedEBCDIC.SPACE;
										}
										flag = false;
									}
									ptr7++;
									ptr8++;
								}
								num7++;
								break;
							case 'A':
								if (flag2)
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.ASTERISK;
									b2 = this.codePageInfo.fixedEBCDIC.ASTERISK;
								}
								else
								{
									*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
									flag = false;
								}
								ptr7++;
								ptr8++;
								break;
							case 'C':
								if (num < 0)
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.C;
									ptr8++;
									*ptr8 = this.codePageInfo.fixedEBCDIC.R;
								}
								else
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
									ptr8++;
									*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
								}
								ptr8++;
								break;
							case 'D':
								if (num < 0)
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.D;
									ptr8++;
									*ptr8 = this.codePageInfo.fixedEBCDIC.B;
								}
								else
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
									ptr8++;
									*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
								}
								ptr8++;
								break;
							case 'E':
								*ptr8 = this.codePageInfo.fixedEBCDIC.E;
								ptr7 = ptr4;
								ptr8++;
								break;
							case 'G':
							case 'J':
							case 'K':
								goto IL_0E46;
							case 'I':
								goto IL_0E78;
							case 'R':
								goto IL_0EB5;
							default:
								switch (c)
								{
								case 'X':
									if (num6 >= 0)
									{
										*ptr8 = this.codePageInfo.fixedEBCDIC.PLUS;
									}
									else
									{
										*ptr8 = this.codePageInfo.fixedEBCDIC.MINUS;
									}
									ptr8++;
									goto IL_0EF3;
								case 'Y':
									break;
								case 'Z':
									if (*ptr7 == 0 && (flag || num7 == 0))
									{
										flag = true;
										ptr7++;
										*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
										ptr8++;
									}
									else
									{
										flag = false;
										*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
										ptr7++;
										ptr8++;
									}
									num7++;
									goto IL_0EF3;
								case '[':
								case '\\':
								case ']':
								case '^':
								case '_':
								case '`':
								case 'a':
								case 'c':
								case 'e':
								case 'f':
								case 'n':
								case 'o':
								case 'q':
									return;
								case 'b':
									if (flag)
									{
										*ptr8 = b2;
									}
									else
									{
										*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
									}
									ptr8++;
									goto IL_0EF3;
								case 'd':
									if (*ptr7 == 0 && num8 == 0)
									{
										*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
										flag3 = true;
										flag = true;
										ptr8++;
									}
									else if (*ptr7 == 0 && flag && flag2)
									{
										*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
										ptr7++;
										ptr8++;
									}
									else
									{
										if (*ptr7 == 0 && flag)
										{
											*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
											ptr8++;
										}
										else
										{
											if (flag3)
											{
												byte* ptr9 = ptr8 - 1;
												int i = 0;
												while (i < this.codePageInfo.ebcdicCurrency.Length)
												{
													*ptr9 = this.codePageInfo.ebcdicCurrency[i];
													i++;
													ptr9++;
												}
												*ptr9 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
												flag3 = false;
												ptr8 += this.codePageInfo.ebcdicCurrency.Length;
												b = this.codePageInfo.fixedEBCDIC.SPACE;
											}
											else
											{
												*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
												ptr8++;
											}
											flag = false;
										}
										ptr7++;
									}
									num8++;
									goto IL_0EF3;
								case 'g':
								case 'j':
								case 'k':
									goto IL_0E46;
								case 'h':
									if (flag && flag2)
									{
										*ptr8 = b2;
									}
									else
									{
										if (b != this.codePageInfo.fixedEBCDIC.SPACE)
										{
											byte* ptr9 = ptr8 - 1;
											*ptr9 = b;
											b = this.codePageInfo.fixedEBCDIC.SPACE;
										}
										*ptr8 = this.codePageInfo.fixedEBCDIC.PERIOD;
									}
									ptr8++;
									goto IL_0EF3;
								case 'i':
									goto IL_0E78;
								case 'l':
									goto IL_03FC;
								case 'm':
									if (*ptr7 == 0 && flag && flag2)
									{
										*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
										ptr7++;
										ptr8++;
										goto IL_0EF3;
									}
									*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
									if (b != this.codePageInfo.fixedEBCDIC.SPACE)
									{
										byte* ptr9 = ptr8 - 1;
										if (num >= 0)
										{
											*ptr9 = this.codePageInfo.fixedEBCDIC.PLUS;
										}
										else
										{
											*ptr9 = this.codePageInfo.fixedEBCDIC.MINUS;
										}
										b = this.codePageInfo.fixedEBCDIC.SPACE;
									}
									flag = false;
									ptr7++;
									ptr8++;
									goto IL_0EF3;
								case 'p':
									if (*ptr7 == 0 && flag && flag2)
									{
										*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
										ptr7++;
										ptr8++;
										goto IL_0EF3;
									}
									*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
									if (b != this.codePageInfo.fixedEBCDIC.SPACE)
									{
										byte* ptr9 = ptr8 - 1;
										if (num >= 0)
										{
											*ptr9 = this.codePageInfo.fixedEBCDIC.PLUS;
										}
										else
										{
											*ptr9 = this.codePageInfo.fixedEBCDIC.MINUS;
										}
										b = this.codePageInfo.fixedEBCDIC.SPACE;
									}
									flag = false;
									ptr7++;
									ptr8++;
									goto IL_0EF3;
								case 'r':
									goto IL_0EB5;
								default:
									switch (c)
									{
									case 'x':
										if (num6 >= 0)
										{
											*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
										}
										else
										{
											*ptr8 = this.codePageInfo.fixedEBCDIC.MINUS;
										}
										ptr8++;
										goto IL_0EF3;
									case 'y':
										goto IL_02E9;
									case 'z':
										if (*ptr7 == 0 && flag && flag2)
										{
											ptr7++;
											*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
											ptr8++;
											goto IL_0EF3;
										}
										flag = false;
										*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
										ptr7++;
										ptr8++;
										goto IL_0EF3;
									}
									return;
								}
								IL_02E9:
								if (*ptr7 == 0)
								{
									ptr7++;
									*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
									ptr8++;
								}
								else
								{
									*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
									ptr7++;
									ptr8++;
								}
								break;
							}
							IL_0EF3:
							num2++;
							if (num2 >= EditStateMachine.Length)
							{
								goto Block_64;
							}
							continue;
							IL_03FC:
							if (*ptr7 == 0 && num8 == 0)
							{
								*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
								flag3 = true;
								flag = true;
								ptr8++;
							}
							else if (*ptr7 == 0 && flag && flag2)
							{
								*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
								ptr7++;
								ptr8++;
							}
							else
							{
								if (*ptr7 == 0 && flag)
								{
									*ptr8 = this.codePageInfo.fixedEBCDIC.SPACE;
									ptr8++;
								}
								else
								{
									if (flag3)
									{
										byte* ptr9 = ptr8 - 1;
										int i = 0;
										while (i < this.codePageInfo.ebcdicCurrency.Length)
										{
											*ptr9 = this.codePageInfo.ebcdicCurrency[i];
											i++;
											ptr9++;
										}
										*ptr9 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
										flag3 = false;
										ptr8 += this.codePageInfo.ebcdicCurrency.Length;
										b = this.codePageInfo.fixedEBCDIC.SPACE;
									}
									else
									{
										*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
										ptr8++;
									}
									flag = false;
								}
								ptr7++;
							}
							num8++;
							goto IL_0EF3;
							IL_0E46:
							flag = false;
							if (num == -1)
							{
								*ptr8 = *ptr7 | 208;
							}
							else
							{
								*ptr8 = *ptr7 | 192;
							}
							ptr7++;
							ptr8++;
							goto IL_0EF3;
							IL_0E78:
							flag = false;
							if (num == 1)
							{
								*ptr8 = *ptr7 | 192;
							}
							else
							{
								*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
							}
							ptr7++;
							ptr8++;
							goto IL_0EF3;
							IL_0EB5:
							flag = false;
							if (num == -1)
							{
								*ptr8 = *ptr7 | 192;
							}
							else
							{
								*ptr8 = *ptr7 | this.codePageInfo.fixedEBCDIC.ZERO;
							}
							ptr7++;
							ptr8++;
							goto IL_0EF3;
						}
						return;
						Block_64:
						PackedLength += (int)((long)(ptr8 - ptr6));
					}
				}
			}
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x0008E834 File Offset: 0x0008CA34
		public unsafe bool ExtractNumber(out long Mantissa, out short ManPrecision, out short ManScale, out long Exponent, out short ExpPrecision, out int ConsumedLength, ref byte InputNumber, string EditStateMachine)
		{
			int num = 1;
			int length = EditStateMachine.Length;
			bool flag = false;
			byte b = this.codePageInfo.fixedEBCDIC.SPACE;
			long[] array = new long[2];
			short[] array2 = new short[2];
			bool flag2 = false;
			Mantissa = 0L;
			ManPrecision = 0;
			ManScale = 0;
			Exponent = 0L;
			ExpPrecision = 0;
			ConsumedLength = 0;
			array[0] = 0L;
			array[1] = 0L;
			array2[0] = 1;
			array2[1] = 1;
			try
			{
				try
				{
					fixed (long* ptr = &array[0])
					{
						long* ptr2 = ptr;
						try
						{
							fixed (long* ptr3 = &array[1])
							{
								long* ptr4 = ptr3;
								try
								{
									fixed (short* ptr5 = &array2[0])
									{
										short* ptr6 = ptr5;
										try
										{
											fixed (short* ptr7 = &array2[1])
											{
												short* ptr8 = ptr7;
												try
												{
													fixed (byte* ptr9 = &InputNumber)
													{
														byte* ptr10 = ptr9;
														long* ptr11 = ptr2;
														short* ptr12 = ptr6;
														byte* ptr13 = ptr10;
														for (;;)
														{
															char c = EditStateMachine[num];
															switch (c)
															{
															case '#':
																if (*ptr13 == this.codePageInfo.fixedEBCDIC.SPACE)
																{
																	ptr13++;
																}
																else
																{
																	int i = 0;
																	while (i < this.codePageInfo.ebcdicCurrency.Length)
																	{
																		if (*ptr13 != this.codePageInfo.ebcdicCurrency[i])
																		{
																			goto Block_34;
																		}
																		i++;
																		ptr13++;
																	}
																}
																break;
															case '$':
															{
																int j = 0;
																while (j < this.codePageInfo.ebcdicCurrency.Length)
																{
																	if (*ptr13 != this.codePageInfo.ebcdicCurrency[j])
																	{
																		goto Block_44;
																	}
																	j++;
																	ptr13++;
																}
																break;
															}
															case '%':
															case '&':
															case '\'':
															case '(':
															case ')':
															case '1':
															case '2':
															case '3':
															case '4':
															case '5':
															case '6':
															case ':':
															case ';':
															case '=':
															case '?':
															case '@':
															case 'B':
															case 'F':
															case 'H':
															case 'L':
															case 'N':
															case 'O':
															case 'Q':
																goto IL_154F;
															case '*':
																if (*ptr13 == this.codePageInfo.fixedEBCDIC.ASTERISK)
																{
																	flag = true;
																	b = this.codePageInfo.fixedEBCDIC.ASTERISK;
																	ptr13++;
																}
																else
																{
																	if (!this.IsNumericEBCDICChar(*ptr13))
																	{
																		goto IL_1196;
																	}
																	*ptr11 *= 10L;
																	*ptr11 += (long)(*ptr13 & 15);
																	ptr13++;
																}
																ManPrecision += 1;
																break;
															case '+':
																if (*ptr13 != this.codePageInfo.fixedEBCDIC.PLUS && *ptr13 != this.codePageInfo.fixedEBCDIC.MINUS && *ptr13 != this.codePageInfo.fixedEBCDIC.SPACE && *ptr13 != this.codePageInfo.fixedEBCDIC.ASTERISK)
																{
																	goto IL_0C58;
																}
																if (*ptr13 == this.codePageInfo.fixedEBCDIC.MINUS)
																{
																	*ptr12 = -1;
																}
																ptr13++;
																break;
															case ',':
																if (*ptr13 == this.codePageInfo.fixedEBCDIC.COMMA || *ptr13 == this.codePageInfo.fixedEBCDIC.SPACE || *ptr13 == this.codePageInfo.fixedEBCDIC.ASTERISK)
																{
																	ptr13++;
																}
																else if (*ptr13 == this.codePageInfo.fixedEBCDIC.PLUS || *ptr13 == this.codePageInfo.fixedEBCDIC.MINUS)
																{
																	if (flag2)
																	{
																		goto Block_54;
																	}
																	flag2 = true;
																	if (num != 0)
																	{
																		char c2 = EditStateMachine[num - 1];
																		if (c2 != 'P' && c2 != 'M')
																		{
																			goto Block_57;
																		}
																	}
																	if (*ptr13 == this.codePageInfo.fixedEBCDIC.MINUS)
																	{
																		*ptr12 = -1;
																	}
																	ptr13++;
																}
																else
																{
																	int k = 0;
																	while (k < this.codePageInfo.ebcdicCurrency.Length)
																	{
																		if (*ptr13 != this.codePageInfo.ebcdicCurrency[k])
																		{
																			goto Block_59;
																		}
																		k++;
																		ptr13++;
																	}
																}
																break;
															case '-':
																if (*ptr13 != this.codePageInfo.fixedEBCDIC.PLUS && *ptr13 != this.codePageInfo.fixedEBCDIC.MINUS && *ptr13 != this.codePageInfo.fixedEBCDIC.SPACE && *ptr13 != this.codePageInfo.fixedEBCDIC.ASTERISK)
																{
																	goto IL_0F80;
																}
																if (*ptr13 == this.codePageInfo.fixedEBCDIC.MINUS)
																{
																	*ptr12 = -1;
																}
																ptr13++;
																break;
															case '.':
																if (*ptr13 != this.codePageInfo.fixedEBCDIC.PERIOD)
																{
																	goto IL_0A59;
																}
																ptr13++;
																break;
															case '/':
																if (*ptr13 != this.codePageInfo.fixedEBCDIC.SLASH)
																{
																	goto IL_0405;
																}
																ptr13++;
																break;
															case '0':
																if (*ptr13 != this.codePageInfo.fixedEBCDIC.ZERO)
																{
																	goto IL_0449;
																}
																ptr13++;
																break;
															case '7':
																if (!this.IsNumericEBCDICChar(*ptr13))
																{
																	goto IL_09AE;
																}
																*ptr11 *= 10L;
																*ptr11 += (long)(*ptr13 & 15);
																ptr13++;
																ExpPrecision += 1;
																break;
															case '8':
																if (!this.IsNumericEBCDICChar(*ptr13))
																{
																	goto IL_0955;
																}
																*ptr11 *= 10L;
																*ptr11 += (long)(*ptr13 & 15);
																ptr13++;
																ManScale += 1;
																break;
															case '9':
																if (!this.IsNumericEBCDICChar(*ptr13))
																{
																	goto IL_08FC;
																}
																*ptr11 *= 10L;
																*ptr11 += (long)(*ptr13 & 15);
																ptr13++;
																ManPrecision += 1;
																break;
															case '<':
															case '>':
																if (*ptr13 == this.codePageInfo.fixedEBCDIC.PLUS || *ptr13 == this.codePageInfo.fixedEBCDIC.MINUS || *ptr13 == this.codePageInfo.fixedEBCDIC.SPACE)
																{
																	if (*ptr13 != this.codePageInfo.fixedEBCDIC.SPACE)
																	{
																		if (flag2)
																		{
																			goto Block_68;
																		}
																		flag2 = true;
																		if (*ptr13 == this.codePageInfo.fixedEBCDIC.MINUS)
																		{
																			*ptr12 = -1;
																		}
																	}
																	ptr13++;
																}
																else
																{
																	if (!this.IsNumericEBCDICChar(*ptr13))
																	{
																		goto IL_0D4B;
																	}
																	*ptr11 *= 10L;
																	*ptr11 += (long)(*ptr13 & 15);
																	ptr13++;
																}
																break;
															case 'A':
																if (*ptr13 != this.codePageInfo.fixedEBCDIC.ASTERISK)
																{
																	goto IL_11E3;
																}
																ptr13++;
																ManScale += 1;
																break;
															case 'C':
															{
																byte* ptr14 = ptr13 + 1;
																if (((*ptr13 != this.codePageInfo.fixedEBCDIC.C && *ptr13 != this.codePageInfo.fixedEBCDIC.c) || (*ptr14 != this.codePageInfo.fixedEBCDIC.R && *ptr14 != this.codePageInfo.fixedEBCDIC.r)) && (*ptr13 != this.codePageInfo.fixedEBCDIC.SPACE || *ptr14 != this.codePageInfo.fixedEBCDIC.SPACE))
																{
																	goto IL_104C;
																}
																if (*ptr13 != this.codePageInfo.fixedEBCDIC.SPACE)
																{
																	*ptr12 = -1;
																}
																ptr13 += 2;
																break;
															}
															case 'D':
															{
																byte* ptr14 = ptr13 + 1;
																if (((*ptr13 != this.codePageInfo.fixedEBCDIC.D && *ptr13 != this.codePageInfo.fixedEBCDIC.d) || (*ptr14 != this.codePageInfo.fixedEBCDIC.B && *ptr14 != this.codePageInfo.fixedEBCDIC.b)) && (*ptr13 != this.codePageInfo.fixedEBCDIC.SPACE || *ptr14 != this.codePageInfo.fixedEBCDIC.SPACE))
																{
																	goto IL_1118;
																}
																if (*ptr13 != this.codePageInfo.fixedEBCDIC.SPACE)
																{
																	*ptr12 = -1;
																}
																ptr13 += 2;
																break;
															}
															case 'E':
																if (*ptr13 != this.codePageInfo.fixedEBCDIC.E && *ptr13 != this.codePageInfo.fixedEBCDIC.e)
																{
																	goto IL_02D9;
																}
																ptr13++;
																ptr12 = ptr8;
																break;
															case 'G':
															{
																short num2;
																if (!this.IsSignedNumericEBCDICChar(*ptr13, out num2))
																{
																	goto IL_12D0;
																}
																if (num2 < 0)
																{
																	*ptr12 = -1;
																}
																ptr13++;
																*ptr11 *= 10L;
																*ptr11 += (long)(*ptr13 & 15);
																ManPrecision += 1;
																break;
															}
															case 'I':
															{
																short num2;
																if (!this.IsSignedNumericEBCDICChar(*ptr13, out num2))
																{
																	goto IL_145C;
																}
																if (num2 != 1)
																{
																	*ptr12 = -1;
																}
																ptr13++;
																*ptr11 *= 10L;
																*ptr11 += (long)(*ptr13 & 15);
																ManPrecision += 1;
																break;
															}
															case 'J':
															case 'K':
															{
																short num2;
																if (!this.IsSignedNumericEBCDICChar(*ptr13, out num2))
																{
																	goto IL_1396;
																}
																if (num2 < 0)
																{
																	*ptr12 = -1;
																}
																ptr13++;
																*ptr11 *= 10L;
																*ptr11 += (long)(*ptr13 & 15);
																ManPrecision += 1;
																break;
															}
															case 'M':
															case 'P':
																if (*ptr13 == this.codePageInfo.fixedEBCDIC.PLUS || *ptr13 == this.codePageInfo.fixedEBCDIC.MINUS || *ptr13 == this.codePageInfo.fixedEBCDIC.SPACE)
																{
																	if (*ptr13 != this.codePageInfo.fixedEBCDIC.SPACE)
																	{
																		if (flag2)
																		{
																			goto Block_74;
																		}
																		flag2 = true;
																		if (*ptr13 == this.codePageInfo.fixedEBCDIC.MINUS)
																		{
																			*ptr12 = -1;
																		}
																	}
																	ptr13++;
																}
																else
																{
																	if (!this.IsNumericEBCDICChar(*ptr13))
																	{
																		goto IL_0E38;
																	}
																	*ptr11 *= 10L;
																	*ptr11 += (long)(*ptr13 & 15);
																	ptr13++;
																}
																ManPrecision += 1;
																break;
															case 'R':
															{
																short num2;
																if (!this.IsSignedNumericEBCDICChar(*ptr13, out num2))
																{
																	goto IL_1522;
																}
																if (num2 < 0)
																{
																	*ptr12 = -1;
																}
																ptr13++;
																*ptr11 *= 10L;
																*ptr11 += (long)(*ptr13 & 15);
																ManPrecision += 1;
																break;
															}
															default:
																switch (c)
																{
																case 'X':
																	if (*ptr13 == this.codePageInfo.fixedEBCDIC.PLUS)
																	{
																		ptr13++;
																		*ptr12 = 1;
																		ptr11 = ptr4;
																	}
																	else
																	{
																		if (*ptr13 != this.codePageInfo.fixedEBCDIC.MINUS)
																		{
																			goto IL_034D;
																		}
																		ptr13++;
																		*ptr12 = -1;
																		ptr11 = ptr4;
																	}
																	break;
																case 'Y':
																	if (*ptr13 == this.codePageInfo.fixedEBCDIC.SPACE)
																	{
																		*ptr11 *= 10L;
																		ptr13++;
																	}
																	else
																	{
																		if (!this.IsNumericEBCDICChar(*ptr13))
																		{
																			goto IL_04BC;
																		}
																		*ptr11 *= 10L;
																		*ptr11 += (long)(*ptr13 & 15);
																		ptr13++;
																	}
																	ManPrecision += 1;
																	break;
																case 'Z':
																	if (*ptr13 == this.codePageInfo.fixedEBCDIC.SPACE)
																	{
																		ptr13++;
																	}
																	else
																	{
																		if (!this.IsNumericEBCDICChar(*ptr13))
																		{
																			goto IL_05B1;
																		}
																		*ptr11 *= 10L;
																		*ptr11 += (long)(*ptr13 & 15);
																		ptr13++;
																	}
																	ManPrecision += 1;
																	break;
																case '[':
																case '\\':
																case ']':
																case '^':
																case '_':
																case '`':
																case 'a':
																case 'c':
																case 'e':
																case 'f':
																case 'n':
																case 'o':
																case 'q':
																	goto IL_154F;
																case 'b':
																	if ((flag && *ptr13 == b) || *ptr13 == this.codePageInfo.fixedEBCDIC.SPACE)
																	{
																		ptr13++;
																	}
																	else
																	{
																		int l = 0;
																		while (l < this.codePageInfo.ebcdicCurrency.Length)
																		{
																			if (*ptr13 != this.codePageInfo.ebcdicCurrency[l])
																			{
																				goto Block_16;
																			}
																			l++;
																			ptr13++;
																		}
																	}
																	break;
																case 'd':
																	if (*ptr13 == this.codePageInfo.fixedEBCDIC.SPACE)
																	{
																		ptr13++;
																	}
																	else
																	{
																		bool flag3 = false;
																		int m = 0;
																		while (m < this.codePageInfo.ebcdicCurrency.Length)
																		{
																			if (*ptr13 != this.codePageInfo.ebcdicCurrency[m])
																			{
																				flag3 = true;
																				break;
																			}
																			m++;
																			ptr13++;
																		}
																		if (!flag3)
																		{
																			ptr13 += this.codePageInfo.ebcdicCurrency.Length;
																		}
																		else
																		{
																			if (!this.IsNumericEBCDICChar(*ptr13))
																			{
																				goto IL_0845;
																			}
																			*ptr11 *= 10L;
																			*ptr11 += (long)(*ptr13 & 15);
																			ptr13++;
																		}
																	}
																	ManScale += 1;
																	break;
																case 'g':
																{
																	short num2;
																	if (!this.IsSignedNumericEBCDICChar(*ptr13, out num2))
																	{
																		goto IL_126D;
																	}
																	if (num2 < 0)
																	{
																		*ptr12 = -1;
																	}
																	ptr13++;
																	*ptr11 *= 10L;
																	*ptr11 += (long)(*ptr13 & 15);
																	ManScale += 1;
																	break;
																}
																case 'h':
																	if (*ptr13 != this.codePageInfo.fixedEBCDIC.PERIOD && *ptr13 != this.codePageInfo.fixedEBCDIC.SPACE)
																	{
																		goto IL_0A15;
																	}
																	ptr13++;
																	break;
																case 'i':
																{
																	short num2;
																	if (!this.IsSignedNumericEBCDICChar(*ptr13, out num2))
																	{
																		goto IL_13F9;
																	}
																	if (num2 != 1)
																	{
																		*ptr12 = -1;
																	}
																	ptr13++;
																	*ptr11 *= 10L;
																	*ptr11 += (long)(*ptr13 & 15);
																	ManScale += 1;
																	break;
																}
																case 'j':
																case 'k':
																{
																	short num2;
																	if (!this.IsSignedNumericEBCDICChar(*ptr13, out num2))
																	{
																		goto IL_1333;
																	}
																	if (num2 < 0)
																	{
																		*ptr12 = -1;
																	}
																	ptr13++;
																	*ptr11 *= 10L;
																	*ptr11 += (long)(*ptr13 & 15);
																	ManScale += 1;
																	break;
																}
																case 'l':
																	if (*ptr13 == this.codePageInfo.fixedEBCDIC.SPACE)
																	{
																		ptr13++;
																	}
																	else
																	{
																		bool flag4 = false;
																		byte* ptr14 = ptr13;
																		int n = 0;
																		while (n < this.codePageInfo.ebcdicCurrency.Length)
																		{
																			if (*ptr14 != this.codePageInfo.ebcdicCurrency[n])
																			{
																				flag4 = true;
																				break;
																			}
																			n++;
																			ptr14++;
																		}
																		if (!flag4)
																		{
																			ptr13 += this.codePageInfo.ebcdicCurrency.Length;
																		}
																		else
																		{
																			if (!this.IsNumericEBCDICChar(*ptr13))
																			{
																				goto IL_0777;
																			}
																			*ptr11 *= 10L;
																			*ptr11 += (long)(*ptr13 & 15);
																			ptr13++;
																		}
																	}
																	ManPrecision += 1;
																	break;
																case 'm':
																case 'p':
																	if (*ptr13 == this.codePageInfo.fixedEBCDIC.PLUS || *ptr13 == this.codePageInfo.fixedEBCDIC.MINUS || *ptr13 == this.codePageInfo.fixedEBCDIC.SPACE)
																	{
																		ptr13++;
																	}
																	else
																	{
																		if (!this.IsNumericEBCDICChar(*ptr13))
																		{
																			goto IL_0ED8;
																		}
																		*ptr11 *= 10L;
																		*ptr11 += (long)(*ptr13 & 15);
																		ptr13++;
																	}
																	ManScale += 1;
																	break;
																case 'r':
																{
																	short num2;
																	if (!this.IsSignedNumericEBCDICChar(*ptr13, out num2))
																	{
																		goto IL_14BF;
																	}
																	if (num2 < 0)
																	{
																		*ptr12 = -1;
																	}
																	ptr13++;
																	*ptr11 *= 10L;
																	*ptr11 += (long)(*ptr13 & 15);
																	ManScale += 1;
																	break;
																}
																default:
																	switch (c)
																	{
																	case 'x':
																		if (*ptr13 == this.codePageInfo.fixedEBCDIC.SPACE)
																		{
																			ptr13++;
																			*ptr12 = 1;
																			ptr11 = ptr4;
																			goto IL_1573;
																		}
																		if (*ptr13 == this.codePageInfo.fixedEBCDIC.MINUS)
																		{
																			ptr13++;
																			*ptr12 = -1;
																			ptr11 = ptr4;
																			goto IL_1573;
																		}
																		goto IL_03C1;
																	case 'y':
																		if (*ptr13 == this.codePageInfo.fixedEBCDIC.SPACE)
																		{
																			*ptr11 *= 10L;
																			ptr13++;
																		}
																		else
																		{
																			if (!this.IsNumericEBCDICChar(*ptr13))
																			{
																				goto IL_053B;
																			}
																			*ptr11 *= 10L;
																			*ptr11 += (long)(*ptr13 & 15);
																			ptr13++;
																		}
																		ManScale += 1;
																		goto IL_1573;
																	case 'z':
																		if (*ptr13 == this.codePageInfo.fixedEBCDIC.SPACE)
																		{
																			ptr13++;
																		}
																		else
																		{
																			if (!this.IsNumericEBCDICChar(*ptr13))
																			{
																				goto IL_0627;
																			}
																			*ptr11 *= 10L;
																			*ptr11 += (long)(*ptr13 & 15);
																			ptr13++;
																		}
																		ManScale += 1;
																		goto IL_1573;
																	}
																	goto Block_14;
																}
																break;
															}
															IL_1573:
															num++;
															if (num >= length)
															{
																goto Block_112;
															}
														}
														Block_14:
														goto IL_154F;
														Block_16:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_02D9:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_034D:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_03C1:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_0405:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_0449:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_04BC:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_053B:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_05B1:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_0627:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														Block_34:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_0777:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_0845:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														Block_44:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_08FC:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_0955:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_09AE:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_0A15:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_0A59:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														Block_54:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														Block_57:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														Block_59:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_0C58:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														Block_68:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_0D4B:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														Block_74:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_0E38:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_0ED8:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_0F80:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_104C:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_1118:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_1196:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_11E3:
														if (this.IsNumericEBCDICChar(*ptr13))
														{
															*ptr11 *= 10L;
															*ptr11 += (long)(*ptr13 & 15);
															ptr13++;
														}
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_126D:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_12D0:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_1333:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_1396:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_13F9:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_145C:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_14BF:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_1522:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														IL_154F:
														throw new CustomHISException(SR.BadInputNumericEdited(this.FormatByteDataForDisplay(ptr10, (int)((long)(ptr13 - ptr10))), EditStateMachine, num));
														Block_112:
														ConsumedLength = (int)((long)(ptr13 - ptr10));
													}
												}
												finally
												{
													byte* ptr9 = null;
												}
											}
										}
										finally
										{
											short* ptr7 = null;
										}
									}
								}
								finally
								{
									short* ptr5 = null;
								}
							}
						}
						finally
						{
							long* ptr3 = null;
						}
					}
				}
				finally
				{
					long* ptr = null;
				}
			}
			catch (CustomHISException)
			{
				if (this.acceptAllInvalidNumerics)
				{
					return false;
				}
				throw;
			}
			Mantissa = array[0] * (long)array2[0];
			Exponent = array[1] * (long)array2[1];
			return true;
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x0008FEB4 File Offset: 0x0008E0B4
		private void ExtractDigits(object InNumber, CEDAR_TYPE_ENCODING Encoding, out byte[] Mantissa, out short ManPrecision, out short ManScale, out short ManSign, out byte[] Exponent, out short ExpPrecision, out short ExpSign, out bool IsValueZero, string EditStateMachine)
		{
			ManSign = 0;
			ExpSign = 0;
			Exponent = new byte[1];
			ExpPrecision = 0;
			Mantissa = null;
			IsValueZero = false;
			this.ExtractPrecisionAndScale(out ManPrecision, out ManScale, out ExpPrecision, EditStateMachine);
			Type type = InNumber.GetType();
			if (type == typeof(short))
			{
				this.ExtractDigitsFromInt16((short)InNumber, (int)ManPrecision, out Mantissa, out ManSign, out IsValueZero);
				return;
			}
			if (type == typeof(int))
			{
				this.ExtractDigitsFromInt32((int)InNumber, (int)ManPrecision, out Mantissa, out ManSign, out IsValueZero);
				return;
			}
			if (type == typeof(float))
			{
				this.ExtractDigitsFromFloat((float)InNumber, (OverflowHandling)Encoding.nTRE, ManPrecision, ManScale, ExpPrecision, out Mantissa, out ManSign, out Exponent, out ExpSign);
				return;
			}
			if (type == typeof(double))
			{
				this.ExtractDigitsFromDouble((double)InNumber, (OverflowHandling)Encoding.nTRE, ManPrecision, ManScale, ExpPrecision, out Mantissa, out ManSign, out Exponent, out ExpSign);
				return;
			}
			if (type == typeof(decimal))
			{
				this.ExtractDigitsFromDecimal((decimal)InNumber, (OverflowHandling)Encoding.nTRE, (int)ManPrecision, (int)ManScale, out Mantissa, out ManSign, out IsValueZero);
			}
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x0008FFDC File Offset: 0x0008E1DC
		private void ExtractDigitsFromFloat(float InNumber, OverflowHandling TRE, short ManPrecision, short ManScale, short ExpPrecision, out byte[] Mantissa, out short ManSign, out byte[] Exponent, out short ExpSign)
		{
			int num = 0;
			bool flag = true;
			Mantissa = new byte[(int)ManPrecision];
			Exponent = new byte[(int)ExpPrecision];
			ManSign = 1;
			ExpSign = 1;
			string text = InNumber.ToString();
			int num2;
			int num3;
			if (text[0] == '-')
			{
				num2 = 1;
				num3 = 1;
				ManSign = -1;
			}
			else if (text[0] == '+')
			{
				num2 = 1;
				num3 = 1;
			}
			else
			{
				num2 = 0;
				num3 = 0;
			}
			int num4 = text.IndexOf('E');
			if (num4 == -1)
			{
				num4 = text.IndexOf('e');
			}
			int num5 = text.IndexOf('.');
			string text2;
			if (num4 == -1)
			{
				if ((short)(text.Length - num3 - num2) > ManPrecision && TRE == OverflowHandling.ReportAsError)
				{
					throw new CustomHISException(SR.FloatMantissaOverflow);
				}
				text2 = "00";
				flag = false;
				num4 = text.Length;
				if (num5 != -1)
				{
					num = text.Length - num5 - 1 - (int)ManScale;
				}
			}
			else
			{
				if (text[num4 + 1] == '-')
				{
					ExpSign = -1;
				}
				int num6 = text.Length - num4 - 2;
				if ((short)num6 > ExpPrecision && TRE == OverflowHandling.ReportAsError)
				{
					throw new CustomHISException(SR.FloatExponentOverflow);
				}
				text2 = text.Substring(num4 + 2, num6);
				if ((short)(num4 - num3 - num2) > ManPrecision && TRE == OverflowHandling.ReportAsError)
				{
					throw new CustomHISException(SR.FloatMantissaOverflow);
				}
			}
			int num7 = int.Parse(text2);
			string text3;
			if (num2 == 0)
			{
				if (num5 != -1)
				{
					text3 = text.Substring(num3, num5);
					text3 += text.Substring(num5 + 1, num4 - num5 - num3 - 1);
				}
				else
				{
					text3 = text.Substring(num3, num4);
				}
			}
			else if (num5 != -1)
			{
				text3 = text.Substring(num3, num5 - 1);
				text3 += text.Substring(num5 + 1, num4 - num5 - num3);
			}
			else
			{
				text3 = text.Substring(num3, num4);
			}
			int i = int.Parse(text3);
			int j;
			if (text3.Length > (int)ManPrecision)
			{
				j = text3.Length - (int)ManPrecision;
				if (TRE == OverflowHandling.Truncate)
				{
					while (j > 0)
					{
						i /= 10;
						j--;
					}
				}
				else
				{
					while (j > 1)
					{
						i /= 10;
						j--;
					}
					i += 5;
					i /= 10;
				}
			}
			j = (int)ManPrecision;
			BasePrimitiveConverter.Int32Union int32Union;
			while (i > 0)
			{
				int32Union.int32Val = i % 10;
				j--;
				Mantissa[j] = int32Union.byteVal.FixedElementField & 15;
				i /= 10;
			}
			if (j > 0)
			{
				int k;
				for (k = 0; k < (int)ManPrecision - j; k++)
				{
					Mantissa[k] = Mantissa[k + j];
				}
				while (k < (int)ManPrecision)
				{
					Mantissa[k] = 0;
					k++;
				}
				num += j;
			}
			if (!flag)
			{
				num7 = num;
				if (num > 0)
				{
					ExpSign = -1;
				}
			}
			else
			{
				num7 -= (int)(ManPrecision - ManScale) + num - 1;
				if (num7 < 0)
				{
					num7 = -num7;
					ExpSign = -1;
				}
			}
			for (j = (int)(ExpPrecision - 1); j > -1; j--)
			{
				int32Union.int32Val = num7 % 10;
				Exponent[j] = int32Union.byteVal.FixedElementField & 15;
				num7 /= 10;
			}
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000902B8 File Offset: 0x0008E4B8
		private void ExtractDigitsFromDouble(double InNumber, OverflowHandling TRE, short ManPrecision, short ManScale, short ExpPrecision, out byte[] Mantissa, out short ManSign, out byte[] Exponent, out short ExpSign)
		{
			int num = 0;
			bool flag = true;
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			Mantissa = new byte[(int)ManPrecision];
			Exponent = new byte[(int)ExpPrecision];
			ManSign = 1;
			ExpSign = 1;
			string text = InNumber.ToString();
			int num2;
			int num3;
			if (text[0] == '-')
			{
				num2 = 1;
				num3 = 1;
				ManSign = -1;
			}
			else if (text[0] == '+')
			{
				num2 = 1;
				num3 = 1;
			}
			else
			{
				num2 = 0;
				num3 = 0;
			}
			int num4 = text.IndexOf('E');
			if (num4 == -1)
			{
				num4 = text.IndexOf('e');
			}
			int num5 = text.IndexOf('.');
			string text2;
			if (num4 == -1)
			{
				if ((short)(text.Length - num3 - num2) > ManPrecision && TRE == OverflowHandling.ReportAsError)
				{
					throw new CustomHISException(SR.DoubleMantissaOverflow);
				}
				text2 = "00";
				flag = false;
				num4 = text.Length;
				if (num5 != -1)
				{
					num = text.Length - num5 - 1 - (int)ManScale;
				}
			}
			else
			{
				if (text[num4 + 1] == '-')
				{
					ExpSign = -1;
				}
				int num6 = text.Length - num4 - 2;
				if ((short)num6 > ExpPrecision && TRE == OverflowHandling.ReportAsError)
				{
					throw new CustomHISException(SR.DoubleExponentOverflow);
				}
				text2 = text.Substring(num4 + 2, num6);
				if ((short)(num4 - num3 - num2) > ManPrecision && TRE == OverflowHandling.ReportAsError)
				{
					throw new CustomHISException(SR.DoubleMantissaOverflow);
				}
			}
			int num7 = int.Parse(text2);
			string text3;
			if (num2 == 0)
			{
				if (num5 != -1)
				{
					text3 = text.Substring(num3, num5);
					text3 += text.Substring(num5 + 1, num4 - num5 - num3 - 1);
				}
				else
				{
					text3 = text.Substring(num3, num4);
				}
			}
			else if (num5 != -1)
			{
				text3 = text.Substring(num3, num5 - 1);
				text3 += text.Substring(num5 + 1, num4 - num5 - num3);
			}
			else
			{
				text3 = text.Substring(num3, num4);
			}
			int i = int.Parse(text3);
			int j;
			if (text3.Length > (int)ManPrecision)
			{
				j = text3.Length - (int)ManPrecision;
				if (TRE == OverflowHandling.Truncate)
				{
					while (j > 0)
					{
						i /= 10;
						j--;
					}
				}
				else
				{
					while (j > 1)
					{
						i /= 10;
						j--;
					}
					i += 5;
					i /= 10;
				}
			}
			j = (int)ManPrecision;
			while (i > 0)
			{
				int64Union.int64Val = (long)(i % 10);
				j--;
				Mantissa[j] = int64Union.byteVal.FixedElementField & 15;
				i /= 10;
			}
			if (j > 0)
			{
				int k;
				for (k = 0; k < (int)ManPrecision - j; k++)
				{
					Mantissa[k] = Mantissa[k + j];
				}
				while (k < (int)ManPrecision)
				{
					Mantissa[k] = 0;
					k++;
				}
				num += j;
			}
			if (!flag)
			{
				num7 = num;
				if (num > 0)
				{
					ExpSign = -1;
				}
			}
			else
			{
				num7 -= (int)(ManPrecision - ManScale) + num - 1;
				if (num7 < 0)
				{
					num7 = -num7;
					ExpSign = -1;
				}
			}
			for (j = (int)(ExpPrecision - 1); j > -1; j--)
			{
				int64Union.int64Val = (long)(num7 % 10);
				Exponent[j] = int64Union.byteVal.FixedElementField & 15;
				num7 /= 10;
			}
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000905A0 File Offset: 0x0008E7A0
		private void ExtractDigitsFromInt32(int Int32Input, int Precision, out byte[] DigitsOut, out short Sign, out bool IsValueZero)
		{
			DigitsOut = new byte[Precision];
			IsValueZero = true;
			int num = Int32Input;
			if (Int32Input < 0)
			{
				Sign = -1;
			}
			else
			{
				Sign = 1;
			}
			for (int i = Precision - 1; i > -1; i--)
			{
				BasePrimitiveConverter.Int32Union int32Union;
				int32Union.int32Val = num % 10 * (int)Sign;
				DigitsOut[i] = int32Union.byteVal.FixedElementField & 15;
				if (DigitsOut[i] != 0)
				{
					IsValueZero = false;
				}
				num /= 10;
			}
			if (num != 0)
			{
				throw new CustomHISException(SR.NumericOverflow, 1507, this.UserCompatibleErrorCode);
			}
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x00090624 File Offset: 0x0008E824
		private void ExtractDigitsFromInt16(short Int16Input, int Precision, out byte[] DigitsOut, out short Sign, out bool IsValueZero)
		{
			DigitsOut = new byte[Precision];
			IsValueZero = true;
			short num = Int16Input;
			if (Int16Input < 0)
			{
				Sign = -1;
			}
			else
			{
				Sign = 1;
			}
			for (int i = Precision - 1; i > -1; i--)
			{
				BasePrimitiveConverter.Int16Union int16Union;
				int16Union.int16Val = num % 10 * Sign;
				DigitsOut[i] = int16Union.byteVal.FixedElementField & 15;
				if (DigitsOut[i] != 0)
				{
					IsValueZero = false;
				}
				num /= 10;
			}
			if (num != 0)
			{
				throw new CustomHISException(SR.NumericOverflow);
			}
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x000906A0 File Offset: 0x0008E8A0
		private unsafe void ExtractDigitsFromDecimal(decimal DecimalInput, OverflowHandling TRE, int Precision, int Scale, out byte[] DigitsOut, out short Sign, out bool IsValueZero)
		{
			BasePrimitiveConverter.Int64Union int64Union = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int64Union int64Union2 = default(BasePrimitiveConverter.Int64Union);
			BasePrimitiveConverter.Int32Union int32Union = default(BasePrimitiveConverter.Int32Union);
			DigitsOut = new byte[Precision];
			IsValueZero = true;
			BasePrimitiveConverter.BigUnion bigUnion;
			bigUnion.decStructVal.lo64 = 0L;
			bigUnion.decStructVal.hi32 = 0;
			bigUnion.decStructVal.scale = 0;
			bigUnion.decStructVal.sign = 0;
			bigUnion.decVal = DecimalInput;
			int32Union.int32Val = 0;
			int32Union.byteVal.FixedElementField = bigUnion.decStructVal.scale;
			int num = Scale - int32Union.int32Val;
			int i;
			if (num > 0)
			{
				for (i = 0; i < num; i++)
				{
					bigUnion.decVal *= 10m;
				}
				num = 0;
			}
			if (bigUnion.decStructVal.sign != 0)
			{
				Sign = -1;
			}
			else
			{
				Sign = 1;
			}
			int64Union.int64Val = bigUnion.decStructVal.lo64;
			int64Union2.int64Val = 0L;
			int64Union2.int32Val = bigUnion.decStructVal.hi32;
			*((ref int64Union2.byteVal.FixedElementField) + 4) = *((ref int64Union2.byteVal.FixedElementField) + 3);
			*((ref int64Union2.byteVal.FixedElementField) + 3) = *((ref int64Union2.byteVal.FixedElementField) + 2);
			*((ref int64Union2.byteVal.FixedElementField) + 2) = *((ref int64Union2.byteVal.FixedElementField) + 1);
			*((ref int64Union2.byteVal.FixedElementField) + 1) = int64Union2.byteVal.FixedElementField;
			int64Union2.byteVal.FixedElementField = *((ref int64Union.byteVal.FixedElementField) + 7);
			*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
			int j;
			if (num < 0)
			{
				int num2;
				for (j = -num; j > 1; j--)
				{
					num2 = (int)(int64Union2.int64Val % 10L);
					int64Union2.int64Val /= 10L;
					*((ref int64Union.byteVal.FixedElementField) + 7) = (byte)num2;
					num2 = (int)(int64Union.int64Val % 10L);
					if (num2 != 0 && TRE == OverflowHandling.ReportAsError)
					{
						throw new CustomHISException(SR.DecimalOverflow);
					}
					int64Union.int64Val /= 10L;
				}
				if (TRE == OverflowHandling.Round)
				{
					int64Union.int64Val += 5L;
					int num3 = (int)(*((ref int64Union.byteVal.FixedElementField) + 7));
					*((ref int64Union.byteVal.FixedElementField) + 7) = 0;
					int64Union2.int64Val += (long)num3;
				}
				num2 = (int)(int64Union2.int64Val % 10L);
				int64Union2.int64Val /= 10L;
				*((ref int64Union.byteVal.FixedElementField) + 7) = (byte)num2;
				num2 = (int)(int64Union.int64Val % 10L);
				if (num2 != 0 && TRE == OverflowHandling.ReportAsError)
				{
					throw new CustomHISException(SR.DecimalOverflow);
				}
				int64Union.int64Val /= 10L;
			}
			long num4 = int64Union2.int64Val;
			int64Union2.int64Val = 0L;
			i = 0;
			j = Precision - 1;
			while (int64Union.int64Val > 0L && i < Precision)
			{
				IsValueZero = false;
				if (num4 > 0L)
				{
					int64Union2.int64Val = num4 % 10L;
					ref byte ptr = (ref int64Union.byteVal.FixedElementField) + 7;
					ptr |= int64Union2.byteVal.FixedElementField;
					num4 /= 10L;
				}
				int64Union2.int64Val = int64Union.int64Val % 10L;
				DigitsOut[j] = int64Union2.byteVal.FixedElementField;
				j--;
				int64Union.int64Val /= 10L;
				i++;
			}
			if (i == Precision && int64Union.int64Val > 0L)
			{
				throw new CustomHISException(SR.DecimalOverflow);
			}
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x00090A2C File Offset: 0x0008EC2C
		public void ExtractPrecisionAndScale(out short ManPrecision, out short ManScale, out short ExpPrecision, string EditStateMachine)
		{
			int num = 1;
			int length = EditStateMachine.Length;
			ManPrecision = 0;
			ManScale = 0;
			ExpPrecision = 0;
			for (;;)
			{
				char c = EditStateMachine[num];
				if (c <= 'X')
				{
					if (c <= 'J')
					{
						switch (c)
						{
						case '#':
						case '$':
						case '+':
						case ',':
						case '-':
						case '.':
						case '/':
						case '0':
						case '<':
						case '>':
						case 'C':
						case 'D':
						case 'E':
							break;
						case '%':
						case '&':
						case '\'':
						case '(':
						case ')':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case ':':
						case ';':
						case '=':
						case '?':
						case '@':
						case 'B':
							goto IL_01D6;
						case '*':
							ManPrecision += 1;
							break;
						case '7':
							ExpPrecision += 1;
							break;
						case '8':
							ManScale += 1;
							break;
						case '9':
							ManPrecision += 1;
							break;
						case 'A':
							ManScale += 1;
							break;
						default:
							if (c != 'J')
							{
								goto Block_4;
							}
							ManPrecision += 1;
							break;
						}
					}
					else if (c != 'M')
					{
						if (c != 'P')
						{
							if (c != 'X')
							{
								break;
							}
						}
						else
						{
							ManPrecision += 1;
						}
					}
					else
					{
						ManPrecision += 1;
					}
				}
				else if (c <= 'd')
				{
					if (c != 'Z')
					{
						if (c != 'b')
						{
							if (c != 'd')
							{
								break;
							}
							ManScale += 1;
						}
					}
					else
					{
						ManPrecision += 1;
					}
				}
				else
				{
					switch (c)
					{
					case 'h':
						break;
					case 'i':
					case 'k':
					case 'n':
					case 'o':
						goto IL_01D6;
					case 'j':
						ManScale += 1;
						break;
					case 'l':
						ManPrecision += 1;
						break;
					case 'm':
						ManScale += 1;
						break;
					case 'p':
						ManScale += 1;
						break;
					default:
						if (c != 'x')
						{
							if (c != 'z')
							{
								goto Block_14;
							}
							ManScale += 1;
						}
						break;
					}
				}
				num++;
				if (num >= length)
				{
					goto Block_15;
				}
			}
			Block_4:
			Block_14:
			IL_01D6:
			throw new CustomHISException(SR.ExpectedInputNotFound);
			Block_15:
			ManPrecision += ManScale;
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x00090C30 File Offset: 0x0008EE30
		private void FormatTimeSpan(TimeSpan InTimeSpan, string TimeSpanEditPattern)
		{
			int length = TimeSpanEditPattern.Length;
			if (this.editedTimeString == null || this.editedTimeString.Length <= TimeSpanEditPattern.Length)
			{
				if (TimeSpanEditPattern.Length < 127)
				{
					this.editedTimeString = new char[128];
				}
				else
				{
					this.editedTimeString = new char[TimeSpanEditPattern.Length + 1];
				}
			}
			int i = 0;
			int num = 0;
			while (i < length)
			{
				char c = TimeSpanEditPattern[i];
				if (c <= 'f')
				{
					if (c == 'H')
					{
						int hours = InTimeSpan.Hours;
						FourDigits fourDigits = BasePrimitiveConverter.FourDigitNumbers[hours];
						this.editedTimeString[num++] = fourDigits.Third;
						this.editedTimeString[num++] = fourDigits.Fourth;
						i += 2;
						continue;
					}
					if (c == 'f')
					{
						int num2 = (int)(InTimeSpan.Ticks % 10000000L);
						int num3 = i + 1;
						if (num3 + 4 < length && TimeSpanEditPattern[num3 + 4] == 'f')
						{
							num3 += 5;
						}
						while (num3 < length && TimeSpanEditPattern[num3] == 'f')
						{
							num3++;
						}
						int num4 = num3 - i;
						int j = num4;
						num2 /= BasePrimitiveConverter.FractionalSecondsDivisors[num4];
						int num5 = num + num4 - 1;
						while (j >= 3)
						{
							long num6 = (long)(num2 % 1000);
							FourDigits fourDigits = BasePrimitiveConverter.FourDigitNumbers[(int)(checked((IntPtr)num6))];
							this.editedTimeString[num5--] = fourDigits.Fourth;
							this.editedTimeString[num5--] = fourDigits.Third;
							this.editedTimeString[num5--] = fourDigits.Second;
							num2 /= 1000;
							j -= 3;
						}
						if (j != 0)
						{
							FourDigits fourDigits = BasePrimitiveConverter.FourDigitNumbers[num2];
							if (j == 3)
							{
								this.editedTimeString[num5--] = fourDigits.Fourth;
								this.editedTimeString[num5--] = fourDigits.Third;
								this.editedTimeString[num5--] = fourDigits.Second;
							}
							else if (j == 2)
							{
								this.editedTimeString[num5--] = fourDigits.Fourth;
								this.editedTimeString[num5--] = fourDigits.Third;
							}
							else if (j == 1)
							{
								this.editedTimeString[num5--] = fourDigits.Fourth;
							}
						}
						i += num4;
						num += num4;
						continue;
					}
				}
				else
				{
					if (c == 'm')
					{
						int minutes = InTimeSpan.Minutes;
						FourDigits fourDigits = BasePrimitiveConverter.FourDigitNumbers[minutes];
						this.editedTimeString[num++] = fourDigits.Third;
						this.editedTimeString[num++] = fourDigits.Fourth;
						i += 2;
						continue;
					}
					if (c == 's')
					{
						int seconds = InTimeSpan.Seconds;
						FourDigits fourDigits = BasePrimitiveConverter.FourDigitNumbers[seconds];
						this.editedTimeString[num++] = fourDigits.Third;
						this.editedTimeString[num++] = fourDigits.Fourth;
						i += 2;
						continue;
					}
				}
				this.editedTimeString[num++] = c;
				i++;
			}
			this.editedTimeStringLength = num;
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x00090F40 File Offset: 0x0008F140
		private void FormatDate(DateTime InDate, string DateEditPattern)
		{
			int length = DateEditPattern.Length;
			if (this.editedTimeString == null || this.editedTimeString.Length <= DateEditPattern.Length)
			{
				if (DateEditPattern.Length < 128)
				{
					this.editedTimeString = new char[128];
				}
				else
				{
					this.editedTimeString = new char[DateEditPattern.Length];
				}
			}
			int i = 0;
			int num = 0;
			while (i < length)
			{
				char c = DateEditPattern[i];
				if (c <= 'f')
				{
					if (c <= 'M')
					{
						FourDigits fourDigits;
						if (c == 'H')
						{
							int hour = InDate.Hour;
							fourDigits = BasePrimitiveConverter.FourDigitNumbers[hour];
							this.editedTimeString[num++] = fourDigits.Third;
							this.editedTimeString[num++] = fourDigits.Fourth;
							i += 2;
							continue;
						}
						if (c != 'M')
						{
							goto IL_04A1;
						}
						int month = InDate.Month;
						fourDigits = BasePrimitiveConverter.FourDigitNumbers[month];
						this.editedTimeString[num++] = fourDigits.Third;
						this.editedTimeString[num++] = fourDigits.Fourth;
						i += 2;
						continue;
					}
					else
					{
						if (c == 'd')
						{
							int num2 = 2;
							if (i + 2 < length && DateEditPattern[i + 2] == 'd')
							{
								num2 = 3;
							}
							if (num2 == 3)
							{
								int dayOfYear = InDate.DayOfYear;
								FourDigits fourDigits = BasePrimitiveConverter.FourDigitNumbers[dayOfYear];
								this.editedTimeString[num++] = fourDigits.Second;
								this.editedTimeString[num++] = fourDigits.Third;
								this.editedTimeString[num++] = fourDigits.Fourth;
							}
							else
							{
								int day = InDate.Day;
								FourDigits fourDigits = BasePrimitiveConverter.FourDigitNumbers[day];
								this.editedTimeString[num++] = fourDigits.Third;
								this.editedTimeString[num++] = fourDigits.Fourth;
							}
							i += num2;
							continue;
						}
						if (c != 'f')
						{
							goto IL_04A1;
						}
					}
				}
				else if (c <= 's')
				{
					FourDigits fourDigits;
					if (c == 'm')
					{
						int minute = InDate.Minute;
						fourDigits = BasePrimitiveConverter.FourDigitNumbers[minute];
						this.editedTimeString[num++] = fourDigits.Third;
						this.editedTimeString[num++] = fourDigits.Fourth;
						i += 2;
						continue;
					}
					if (c != 's')
					{
						goto IL_04A1;
					}
					int second = InDate.Second;
					fourDigits = BasePrimitiveConverter.FourDigitNumbers[second];
					this.editedTimeString[num++] = fourDigits.Third;
					this.editedTimeString[num++] = fourDigits.Fourth;
					i += 2;
					continue;
				}
				else if (c != 't')
				{
					if (c == 'y')
					{
						int year = InDate.Year;
						int num3 = 2;
						if (i + 3 < length && DateEditPattern[i + 3] == 'y')
						{
							num3 = 4;
						}
						FourDigits fourDigits = BasePrimitiveConverter.FourDigitNumbers[year];
						if (num3 == 4)
						{
							this.editedTimeString[num++] = fourDigits.First;
							this.editedTimeString[num++] = fourDigits.Second;
						}
						this.editedTimeString[num++] = fourDigits.Third;
						this.editedTimeString[num++] = fourDigits.Fourth;
						i += num3;
						continue;
					}
					goto IL_04A1;
				}
				int num4 = (int)(InDate.Ticks % 10000000L);
				int num5 = i + 1;
				if (num5 + 4 < length && DateEditPattern[num5 + 4] == c)
				{
					num5 += 5;
				}
				while (num5 < length && DateEditPattern[num5] == c)
				{
					num5++;
				}
				int num6 = num5 - i;
				int j = num6;
				num4 /= BasePrimitiveConverter.FractionalSecondsDivisors[num6];
				int num7 = num + num6 - 1;
				while (j >= 3)
				{
					long num8 = (long)(num4 % 1000);
					FourDigits fourDigits = BasePrimitiveConverter.FourDigitNumbers[(int)(checked((IntPtr)num8))];
					this.editedTimeString[num7--] = fourDigits.Fourth;
					this.editedTimeString[num7--] = fourDigits.Third;
					this.editedTimeString[num7--] = fourDigits.Second;
					num4 /= 1000;
					j -= 3;
				}
				if (j != 0)
				{
					FourDigits fourDigits = BasePrimitiveConverter.FourDigitNumbers[num4];
					if (j == 3)
					{
						this.editedTimeString[num7--] = fourDigits.Fourth;
						this.editedTimeString[num7--] = fourDigits.Third;
						this.editedTimeString[num7--] = fourDigits.Second;
					}
					else if (j == 2)
					{
						this.editedTimeString[num7--] = fourDigits.Fourth;
						this.editedTimeString[num7--] = fourDigits.Third;
					}
					else if (j == 1)
					{
						this.editedTimeString[num7--] = fourDigits.Fourth;
					}
				}
				i += num6;
				num += num6;
				continue;
				IL_04A1:
				this.editedTimeString[num++] = c;
				i++;
			}
			this.editedTimeStringLength = num;
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x00091410 File Offset: 0x0008F610
		private bool ExtractDate(string FormattedDate, string DateEditPattern, bool AllowTruncatedFractionalSeconds, out DateTime ReturnedDateTime)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			int length = DateEditPattern.Length;
			bool flag = false;
			int num9 = FormattedDate.Length;
			int i = 0;
			while (i < length)
			{
				char c = DateEditPattern[i];
				if (c <= 'd')
				{
					if (c <= 'H')
					{
						if (c != '?')
						{
							if (c != 'H')
							{
								goto IL_0657;
							}
							if (num9 < 2)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("hours"));
								}
								ReturnedDateTime = default(DateTime);
								return false;
							}
							num5 = this.ExtractInteger(FormattedDate, i, 2);
							if (num5 < 0)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("hours"));
								}
								ReturnedDateTime = default(DateTime);
								return false;
							}
							if (num5 > 24)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("hours"));
								}
								ReturnedDateTime = default(DateTime);
								return false;
							}
							if (num5 == 24)
							{
								num5 = 0;
								flag = true;
							}
							i++;
							num9 -= 2;
						}
						else
						{
							if ("- .,/:".IndexOf(FormattedDate[i]) == -1)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDateCharacter(c, FormattedDate[i]));
								}
								ReturnedDateTime = default(DateTime);
								return false;
							}
							num9--;
						}
					}
					else if (c != 'M')
					{
						if (c != 'd')
						{
							goto IL_0657;
						}
						int num10 = 2;
						if (i + 2 < length && DateEditPattern[i + 2] == 'd')
						{
							num10 = 3;
						}
						if (num10 == 3)
						{
							if (num9 < num10)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("day of year"));
								}
								ReturnedDateTime = default(DateTime);
								return false;
							}
							num4 = this.ExtractInteger(FormattedDate, i, num10);
							if (num4 < 0)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("day of year"));
								}
								ReturnedDateTime = default(DateTime);
								return false;
							}
							i += 2;
							num9 -= 3;
						}
						else
						{
							if (num9 < num10)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("days"));
								}
								ReturnedDateTime = default(DateTime);
								return false;
							}
							num3 = this.ExtractInteger(FormattedDate, i, num10);
							if (num3 < 0)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("days"));
								}
								ReturnedDateTime = default(DateTime);
								return false;
							}
							i++;
							num9 -= 2;
						}
					}
					else
					{
						if (num9 < 2)
						{
							if (this.tracePoint.IsEnabled(TraceFlags.Error))
							{
								this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("month"));
							}
							ReturnedDateTime = default(DateTime);
							return false;
						}
						num2 = this.ExtractInteger(FormattedDate, i, 2);
						if (num2 < 0)
						{
							if (this.tracePoint.IsEnabled(TraceFlags.Error))
							{
								this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("month"));
							}
							ReturnedDateTime = default(DateTime);
							return false;
						}
						i++;
						num9 -= 2;
					}
				}
				else
				{
					if (c <= 'm')
					{
						if (c != 'f')
						{
							if (c != 'm')
							{
								goto IL_0657;
							}
							if (num9 < 2)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("minutes"));
								}
								ReturnedDateTime = default(DateTime);
								return false;
							}
							num6 = this.ExtractInteger(FormattedDate, i, 2);
							if (num6 < 0)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("minutes"));
								}
								ReturnedDateTime = default(DateTime);
								return false;
							}
							if (num6 > 60)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("minutes"));
								}
								ReturnedDateTime = default(DateTime);
								return false;
							}
							i++;
							num9 -= 2;
							goto IL_06A4;
						}
					}
					else if (c != 's')
					{
						if (c != 't')
						{
							if (c != 'y')
							{
								goto IL_0657;
							}
							int num11 = 2;
							if (i + 3 < length && DateEditPattern[i + 3] == 'y')
							{
								num11 = 4;
							}
							if (num9 < num11)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("year"));
								}
								ReturnedDateTime = default(DateTime);
								return false;
							}
							num = this.ExtractInteger(FormattedDate, i, num11);
							if (num < 0)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("year"));
								}
								ReturnedDateTime = default(DateTime);
								return false;
							}
							i += num11 - 1;
							num9 -= num11;
							goto IL_06A4;
						}
					}
					else
					{
						if (num9 < 2)
						{
							if (this.tracePoint.IsEnabled(TraceFlags.Error))
							{
								this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("seconds"));
							}
							ReturnedDateTime = default(DateTime);
							return false;
						}
						num7 = this.ExtractInteger(FormattedDate, i, 2);
						if (num7 < 0)
						{
							if (this.tracePoint.IsEnabled(TraceFlags.Error))
							{
								this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("seconds"));
							}
							ReturnedDateTime = default(DateTime);
							return false;
						}
						if (num7 > 60)
						{
							if (this.tracePoint.IsEnabled(TraceFlags.Error))
							{
								this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("seconds"));
							}
							ReturnedDateTime = default(DateTime);
							return false;
						}
						i++;
						num9 -= 2;
						goto IL_06A4;
					}
					int num12 = i + 1;
					if (num12 + 4 < length && DateEditPattern[num12 + 4] == c)
					{
						num12 += 5;
					}
					while (num12 < length && DateEditPattern[num12] == c)
					{
						num12++;
					}
					int num13 = num12 - i;
					if (num9 < num13)
					{
						if (!AllowTruncatedFractionalSeconds)
						{
							if (this.tracePoint.IsEnabled(TraceFlags.Error))
							{
								this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("fractional seconds"));
							}
							ReturnedDateTime = default(DateTime);
							return false;
						}
						num13 = num9;
					}
					num8 = this.ExtractInteger(FormattedDate, i, num13);
					if (num8 < 0)
					{
						if (this.tracePoint.IsEnabled(TraceFlags.Error))
						{
							this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("fractional seconds"));
						}
						ReturnedDateTime = default(DateTime);
						return false;
					}
					num8 *= BasePrimitiveConverter.FractionalSecondsDivisors[num13];
					i += num13 - 1;
					num9 -= num13;
				}
				IL_06A4:
				i++;
				continue;
				IL_0657:
				if (c != FormattedDate[i])
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDateCharacter(c, FormattedDate[i]));
					}
					ReturnedDateTime = default(DateTime);
					return false;
				}
				num9--;
				goto IL_06A4;
			}
			if (flag)
			{
				if (num6 != 0)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("minutes"));
					}
					ReturnedDateTime = default(DateTime);
					return false;
				}
				if (num7 != 0)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("seconds"));
					}
					ReturnedDateTime = default(DateTime);
					return false;
				}
				if (num8 != 0)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedDate("fractional seconds"));
					}
					ReturnedDateTime = default(DateTime);
					return false;
				}
			}
			DateTime dateTime;
			if (num4 != 0)
			{
				dateTime = new DateTime(num, 1, 1, num5, num6, num7, 0);
				dateTime = dateTime.AddDays((double)num4);
			}
			else
			{
				dateTime = new DateTime(num, num2, num3, num5, num6, num7, 0);
			}
			ReturnedDateTime = dateTime.AddTicks((long)num8);
			if (flag)
			{
				ReturnedDateTime = ReturnedDateTime.AddDays(1.0);
			}
			return true;
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x00091BD0 File Offset: 0x0008FDD0
		private bool ExtractTimeSpan(string FormattedTimeSpan, string TimeSpanEditPattern, bool AllowTruncatedFractionalSeconds, out TimeSpan ResultTimeSpan)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int length = TimeSpanEditPattern.Length;
			int num5 = FormattedTimeSpan.Length;
			bool flag = false;
			int i = 0;
			while (i < length)
			{
				char c = TimeSpanEditPattern[i];
				if (c <= 'f')
				{
					if (c != '?')
					{
						if (c != 'H')
						{
							if (c != 'f')
							{
								goto IL_03C6;
							}
							goto IL_028F;
						}
						else
						{
							if (num5 < 2)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpan("hours"));
								}
								ResultTimeSpan = default(TimeSpan);
								return false;
							}
							num = this.ExtractInteger(FormattedTimeSpan, i, 2);
							if (num < 0)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpan("hours"));
								}
								ResultTimeSpan = default(TimeSpan);
								return false;
							}
							if (num > 24)
							{
								if (this.tracePoint.IsEnabled(TraceFlags.Error))
								{
									this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpan("hours"));
								}
								ResultTimeSpan = default(TimeSpan);
								return false;
							}
							if (num == 24)
							{
								num = 0;
								flag = true;
							}
							i++;
							num5 -= 2;
						}
					}
					else
					{
						if (":. ,".IndexOf(FormattedTimeSpan[i]) == -1)
						{
							if (this.tracePoint.IsEnabled(TraceFlags.Error))
							{
								this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpanCharacter(c, FormattedTimeSpan[i]));
							}
							ResultTimeSpan = default(TimeSpan);
							return false;
						}
						num5--;
					}
				}
				else if (c != 'm')
				{
					if (c != 's')
					{
						if (c != 't')
						{
							goto IL_03C6;
						}
						goto IL_028F;
					}
					else
					{
						if (num5 < 2)
						{
							if (this.tracePoint.IsEnabled(TraceFlags.Error))
							{
								this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpan("seconds"));
							}
							ResultTimeSpan = default(TimeSpan);
							return false;
						}
						num3 = this.ExtractInteger(FormattedTimeSpan, i, 2);
						if (num3 < 0)
						{
							if (this.tracePoint.IsEnabled(TraceFlags.Error))
							{
								this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpan("seconds"));
							}
							ResultTimeSpan = default(TimeSpan);
							return false;
						}
						if (num3 > 60)
						{
							if (this.tracePoint.IsEnabled(TraceFlags.Error))
							{
								this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpan("seconds"));
							}
							ResultTimeSpan = default(TimeSpan);
							return false;
						}
						i++;
						num5 -= 2;
					}
				}
				else
				{
					if (num5 < 2)
					{
						if (this.tracePoint.IsEnabled(TraceFlags.Error))
						{
							this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpan("minutes"));
						}
						ResultTimeSpan = default(TimeSpan);
						return false;
					}
					num2 = this.ExtractInteger(FormattedTimeSpan, i, 2);
					if (num2 < 0)
					{
						if (this.tracePoint.IsEnabled(TraceFlags.Error))
						{
							this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpan("minutes"));
						}
						ResultTimeSpan = default(TimeSpan);
						return false;
					}
					if (num2 > 60)
					{
						if (this.tracePoint.IsEnabled(TraceFlags.Error))
						{
							this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpan("minutes"));
						}
						ResultTimeSpan = default(TimeSpan);
						return false;
					}
					i++;
					num5 -= 2;
				}
				IL_0413:
				i++;
				continue;
				IL_028F:
				int num6 = i + 1;
				if (num6 + 4 < length && TimeSpanEditPattern[num6 + 4] == c)
				{
					num6 += 5;
				}
				while (num6 < length && TimeSpanEditPattern[num6] == c)
				{
					num6++;
				}
				int num7 = num6 - i;
				if (num5 < num7)
				{
					if (!AllowTruncatedFractionalSeconds)
					{
						if (this.tracePoint.IsEnabled(TraceFlags.Error))
						{
							this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpan("fractional seconds"));
						}
						ResultTimeSpan = default(TimeSpan);
						return false;
					}
					num7 = num5;
				}
				num4 = this.ExtractInteger(FormattedTimeSpan, i, num7);
				if (num4 < 0)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpan("fractional seconds"));
					}
					ResultTimeSpan = default(TimeSpan);
					return false;
				}
				num4 *= BasePrimitiveConverter.FractionalSecondsDivisors[num7];
				i += num7 - 1;
				num5 -= num7;
				goto IL_0413;
				IL_03C6:
				if (c != FormattedTimeSpan[i])
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpanCharacter(c, FormattedTimeSpan[i]));
					}
					ResultTimeSpan = default(TimeSpan);
					return false;
				}
				num5--;
				goto IL_0413;
			}
			if (flag)
			{
				if (num2 != 0)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpan("minutes"));
					}
					ResultTimeSpan = default(TimeSpan);
					return false;
				}
				if (num3 != 0)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpan("seconds"));
					}
					ResultTimeSpan = default(TimeSpan);
					return false;
				}
				if (num4 != 0)
				{
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, SR.InvalidEditedTimeSpan("fractional seconds"));
					}
					ResultTimeSpan = default(TimeSpan);
					return false;
				}
			}
			long num8 = (long)((num * 60 + num2) * 60 + num3) * 10000000L + (long)num4;
			ResultTimeSpan = new TimeSpan(num8);
			return true;
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000920C4 File Offset: 0x000902C4
		private int ExtractInteger(string formattedInteger, int firstCharacterIndex, int numberOfCharacters)
		{
			int num = 0;
			int num2 = firstCharacterIndex + numberOfCharacters;
			for (int i = firstCharacterIndex; i < num2; i++)
			{
				char c = formattedInteger[i];
				if (c < '0' || c > '9')
				{
					return -1;
				}
				byte b = (byte)c;
				num *= 10;
				num += (int)(b - 48);
			}
			return num;
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x0009210C File Offset: 0x0009030C
		private bool PreProcessDateMask(string DateEditPattern, bool toHost, out int YearDigits, out int DayDigits, out int MonthDigits, out int HourDigits, out int MinuteDigits, out int SecondDigits, out int FractionalSecondDigits)
		{
			if (BasePrimitiveConverter.dateEditPatterns.ContainsKey(DateEditPattern))
			{
				BasePrimitiveConverter.EditPattern editPattern = BasePrimitiveConverter.dateEditPatterns[DateEditPattern];
				YearDigits = editPattern.YearDigits;
				DayDigits = editPattern.DayDigits;
				MonthDigits = editPattern.MonthDigits;
				HourDigits = editPattern.HourDigits;
				MinuteDigits = editPattern.MinuteDigits;
				SecondDigits = editPattern.SecondDigits;
				FractionalSecondDigits = editPattern.FractionalSecondDigits;
				return true;
			}
			int num = 0;
			int num2 = 0;
			YearDigits = 0;
			DayDigits = 0;
			MonthDigits = 0;
			HourDigits = 0;
			MinuteDigits = 0;
			SecondDigits = 0;
			FractionalSecondDigits = 0;
			for (;;)
			{
				char c = DateEditPattern[num];
				if (c <= 'M')
				{
					if (c <= ':')
					{
						if (c != ' ')
						{
							switch (c)
							{
							case ',':
							case '-':
							case '.':
							case '/':
								break;
							default:
								if (c != ':')
								{
									goto IL_02F0;
								}
								break;
							}
						}
					}
					else if (c != '?')
					{
						if (c != 'H')
						{
							if (c != 'M')
							{
								goto IL_02F0;
							}
							if (MonthDigits != 0)
							{
								return false;
							}
							if (num + 1 >= DateEditPattern.Length)
							{
								return false;
							}
							if (DateEditPattern[num + 1] != c)
							{
								return false;
							}
							num += 2;
							MonthDigits = 2;
							num2++;
							goto IL_02F0;
						}
						else
						{
							if (HourDigits != 0)
							{
								return false;
							}
							if (num + 1 >= DateEditPattern.Length)
							{
								return false;
							}
							if (DateEditPattern[num + 1] != c)
							{
								return false;
							}
							num += 2;
							HourDigits = 2;
							num2++;
							goto IL_02F0;
						}
					}
					if (c == '?' && toHost)
					{
						break;
					}
					if (num2 != 0 && !BasePrimitiveConverter.EditPatternCharacters.ContainsKey(DateEditPattern[num - 1]))
					{
						return false;
					}
					num++;
					num2++;
				}
				else
				{
					if (c <= 'm')
					{
						if (c != 'd')
						{
							if (c != 'f')
							{
								if (c != 'm')
								{
									goto IL_02F0;
								}
								if (MinuteDigits != 0)
								{
									return false;
								}
								if (num + 1 >= DateEditPattern.Length)
								{
									return false;
								}
								if (DateEditPattern[num + 1] != c)
								{
									return false;
								}
								num += 2;
								MinuteDigits = 2;
								num2++;
								goto IL_02F0;
							}
						}
						else
						{
							if (DayDigits != 0)
							{
								return false;
							}
							if (num + 1 >= DateEditPattern.Length)
							{
								return false;
							}
							if (DateEditPattern[num + 1] != c)
							{
								return false;
							}
							num += 2;
							DayDigits = 2;
							if (num < DateEditPattern.Length && DateEditPattern[num] == c)
							{
								num++;
								DayDigits = 3;
							}
							num2++;
							goto IL_02F0;
						}
					}
					else if (c != 's')
					{
						if (c != 't')
						{
							if (c != 'y')
							{
								goto IL_02F0;
							}
							if (YearDigits != 0)
							{
								return false;
							}
							if (num + 1 >= DateEditPattern.Length)
							{
								return false;
							}
							if (DateEditPattern[num + 1] != c)
							{
								return false;
							}
							num += 2;
							YearDigits = 2;
							if (num + 1 < DateEditPattern.Length && DateEditPattern[num] == c && DateEditPattern[num + 1] == c)
							{
								num += 2;
								YearDigits = 4;
							}
							num2++;
							goto IL_02F0;
						}
					}
					else
					{
						if (SecondDigits != 0)
						{
							return false;
						}
						if (num + 1 >= DateEditPattern.Length)
						{
							return false;
						}
						if (DateEditPattern[num + 1] != c)
						{
							return false;
						}
						num += 2;
						SecondDigits = 2;
						num2++;
						goto IL_02F0;
					}
					if (FractionalSecondDigits != 0 && DateEditPattern[num - 1] != c)
					{
						return false;
					}
					FractionalSecondDigits++;
					if (FractionalSecondDigits > 7)
					{
						return false;
					}
					num++;
					num2++;
				}
				IL_02F0:
				if (num >= DateEditPattern.Length)
				{
					goto Block_46;
				}
			}
			return false;
			Block_46:
			if (DayDigits == 3 && MonthDigits != 0)
			{
				return false;
			}
			BasePrimitiveConverter.EditPattern editPattern2 = new BasePrimitiveConverter.EditPattern();
			editPattern2.YearDigits = YearDigits;
			editPattern2.DayDigits = DayDigits;
			editPattern2.MonthDigits = MonthDigits;
			editPattern2.HourDigits = HourDigits;
			editPattern2.MinuteDigits = MinuteDigits;
			editPattern2.SecondDigits = SecondDigits;
			editPattern2.FractionalSecondDigits = FractionalSecondDigits;
			Dictionary<string, BasePrimitiveConverter.EditPattern> dictionary = BasePrimitiveConverter.dateEditPatterns;
			lock (dictionary)
			{
				BasePrimitiveConverter.dateEditPatterns[DateEditPattern] = editPattern2;
			}
			return true;
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000924A4 File Offset: 0x000906A4
		private bool IsNumericEBCDICChar(byte EBCDICChar)
		{
			switch (EBCDICChar)
			{
			case 240:
			case 241:
			case 242:
			case 243:
			case 244:
			case 245:
			case 246:
			case 247:
			case 248:
			case 249:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000924E0 File Offset: 0x000906E0
		private bool IsSignedNumericEBCDICChar(byte EBCDICChar, out short Sign)
		{
			Sign = 0;
			switch (EBCDICChar)
			{
			case 160:
				Sign = 1;
				return true;
			case 161:
				Sign = 1;
				return true;
			case 162:
				Sign = 1;
				return true;
			case 163:
				Sign = 1;
				return true;
			case 164:
				Sign = 1;
				return true;
			case 165:
				Sign = 1;
				return true;
			case 166:
				Sign = 1;
				return true;
			case 167:
				Sign = 1;
				return true;
			case 168:
				Sign = 1;
				return true;
			case 169:
				Sign = 1;
				return true;
			case 176:
				Sign = -1;
				return true;
			case 177:
				Sign = -1;
				return true;
			case 178:
				Sign = -1;
				return true;
			case 179:
				Sign = -1;
				return true;
			case 180:
				Sign = -1;
				return true;
			case 181:
				Sign = -1;
				return true;
			case 182:
				Sign = -1;
				return true;
			case 183:
				Sign = -1;
				return true;
			case 184:
				Sign = -1;
				return true;
			case 185:
				Sign = -1;
				return true;
			case 192:
				Sign = 1;
				return true;
			case 193:
				Sign = 1;
				return true;
			case 194:
				Sign = 1;
				return true;
			case 195:
				Sign = 1;
				return true;
			case 196:
				Sign = 1;
				return true;
			case 197:
				Sign = 1;
				return true;
			case 198:
				Sign = 1;
				return true;
			case 199:
				Sign = 1;
				return true;
			case 200:
				Sign = 1;
				return true;
			case 201:
				Sign = 1;
				return true;
			case 208:
				Sign = -1;
				return true;
			case 209:
				Sign = -1;
				return true;
			case 210:
				Sign = -1;
				return true;
			case 211:
				Sign = -1;
				return true;
			case 212:
				Sign = -1;
				return true;
			case 213:
				Sign = -1;
				return true;
			case 214:
				Sign = -1;
				return true;
			case 215:
				Sign = -1;
				return true;
			case 216:
				Sign = -1;
				return true;
			case 217:
				Sign = -1;
				return true;
			case 224:
				Sign = 1;
				return true;
			case 225:
				Sign = 1;
				return true;
			case 226:
				Sign = 1;
				return true;
			case 227:
				Sign = 1;
				return true;
			case 228:
				Sign = 1;
				return true;
			case 229:
				Sign = 1;
				return true;
			case 230:
				Sign = 1;
				return true;
			case 231:
				Sign = 1;
				return true;
			case 232:
				Sign = 1;
				return true;
			case 233:
				Sign = 1;
				return true;
			case 240:
				Sign = 0;
				return true;
			case 241:
				Sign = 0;
				return true;
			case 242:
				Sign = 0;
				return true;
			case 243:
				Sign = 0;
				return true;
			case 244:
				Sign = 0;
				return true;
			case 245:
				Sign = 0;
				return true;
			case 246:
				Sign = 0;
				return true;
			case 247:
				Sign = 0;
				return true;
			case 248:
				Sign = 0;
				return true;
			case 249:
				Sign = 0;
				return true;
			}
			return false;
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x00092800 File Offset: 0x00090A00
		private unsafe string FormatByteDataForDisplay(byte* ByteData, int DataLength)
		{
			DataLength++;
			char[] array = new char[DataLength * 2];
			int i = 0;
			int num = 0;
			while (i < DataLength)
			{
				byte b = ByteData[i] & 240;
				if (b <= 112)
				{
					if (b <= 48)
					{
						if (b <= 16)
						{
							if (b != 0)
							{
								if (b == 16)
								{
									array[num] = '1';
								}
							}
							else
							{
								array[num] = '0';
							}
						}
						else if (b != 32)
						{
							if (b == 48)
							{
								array[num] = '3';
							}
						}
						else
						{
							array[num] = '2';
						}
					}
					else if (b <= 80)
					{
						if (b != 64)
						{
							if (b == 80)
							{
								array[num] = '5';
							}
						}
						else
						{
							array[num] = '4';
						}
					}
					else if (b != 96)
					{
						if (b == 112)
						{
							array[num] = '7';
						}
					}
					else
					{
						array[num] = '6';
					}
				}
				else if (b <= 176)
				{
					if (b <= 144)
					{
						if (b != 128)
						{
							if (b == 144)
							{
								array[num] = '9';
							}
						}
						else
						{
							array[num] = '8';
						}
					}
					else if (b != 160)
					{
						if (b == 176)
						{
							array[num] = 'B';
						}
					}
					else
					{
						array[num] = 'A';
					}
				}
				else if (b <= 208)
				{
					if (b != 192)
					{
						if (b == 208)
						{
							array[num] = 'D';
						}
					}
					else
					{
						array[num] = 'C';
					}
				}
				else if (b != 224)
				{
					if (b == 240)
					{
						array[num] = 'F';
					}
				}
				else
				{
					array[num] = 'E';
				}
				num++;
				switch (ByteData[i] & 15)
				{
				case 0:
					array[num] = '0';
					break;
				case 1:
					array[num] = '1';
					break;
				case 2:
					array[num] = '2';
					break;
				case 3:
					array[num] = '3';
					break;
				case 4:
					array[num] = '4';
					break;
				case 5:
					array[num] = '5';
					break;
				case 6:
					array[num] = '6';
					break;
				case 7:
					array[num] = '7';
					break;
				case 8:
					array[num] = '8';
					break;
				case 9:
					array[num] = '9';
					break;
				case 10:
					array[num] = 'A';
					break;
				case 11:
					array[num] = 'B';
					break;
				case 12:
					array[num] = 'C';
					break;
				case 13:
					array[num] = 'D';
					break;
				case 14:
					array[num] = 'E';
					break;
				case 15:
					array[num] = 'F';
					break;
				}
				i++;
				num++;
			}
			return new string(array);
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x00092A40 File Offset: 0x00090C40
		private unsafe void SetMemory(byte* Destination, byte SetValue, long SetCount)
		{
			long num = 0L;
			byte* ptr = Destination;
			if (SetCount > 24L)
			{
				ulong num2;
				ptr = (byte*)(&num2);
				for (num = 0L; num < 8L; num += 1L)
				{
					*ptr = SetValue;
					ptr++;
				}
				ptr = Destination;
				for (num = 0L; num < SetCount / 8L; num += 1L)
				{
					*(long*)ptr = (long)num2;
					ptr += 8;
				}
			}
			for (num *= 8L; num < SetCount; num += 1L)
			{
				*ptr = SetValue;
				ptr++;
			}
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x00092AA3 File Offset: 0x00090CA3
		private static void GenerateDecimalFloatTables()
		{
			BasePrimitiveConverter.GenerateDecimalFloatAllSmall();
			BasePrimitiveConverter.GenerateDecimalFloatTwoSmall();
			BasePrimitiveConverter.GenerateDecimalFloatOneSmall();
			BasePrimitiveConverter.GenerateDecimalFloatAllBig();
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x00092ABC File Offset: 0x00090CBC
		private static void GenerateDecimalFloatAllSmall()
		{
			for (int i = 0; i <= 7; i++)
			{
				int num = i * 100;
				int num2 = i << 7;
				for (int j = 0; j <= 7; j++)
				{
					int num3 = j * 10;
					int num4 = j << 4;
					for (int k = 0; k <= 7; k++)
					{
						int num5 = num + num3 + k;
						int num6 = num2 + num4 + k;
						BasePrimitiveConverter.valueToTenBits[num5] = num6;
						BasePrimitiveConverter.tenBitsToValue[num6] = num5;
					}
				}
			}
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x00092B2C File Offset: 0x00090D2C
		private static void GenerateDecimalFloatTwoSmall()
		{
			for (int i = 0; i <= 7; i++)
			{
				int num = i * 100;
				int num2 = i << 7;
				for (int j = 0; j <= 7; j++)
				{
					int num3 = j * 10;
					int num4 = j << 4;
					for (int k = 0; k <= 1; k++)
					{
						int num5 = num + num3 + k + 8;
						int num6 = num2 + num4 + k;
						num6 += 8;
						BasePrimitiveConverter.valueToTenBits[num5] = num6;
						BasePrimitiveConverter.tenBitsToValue[num6] = num5;
					}
				}
			}
			for (int l = 0; l <= 7; l++)
			{
				int num7 = l * 100;
				int num8 = l << 7;
				for (int m = 0; m <= 1; m++)
				{
					int num9 = (m + 8) * 10;
					int num10 = m << 4;
					for (int n = 0; n <= 7; n++)
					{
						int num11 = num7 + num9 + n;
						int num12 = ((n & 6) << 4) + (n & 1);
						int num13 = num8 + num10 + num12;
						num13 += 10;
						BasePrimitiveConverter.valueToTenBits[num11] = num13;
						BasePrimitiveConverter.tenBitsToValue[num13] = num11;
					}
				}
			}
			for (int num14 = 0; num14 <= 1; num14++)
			{
				int num15 = (num14 + 8) * 100;
				int num16 = num14 << 7;
				for (int num17 = 0; num17 <= 7; num17++)
				{
					int num18 = num17 * 10;
					int num19 = num17 << 4;
					for (int num20 = 0; num20 <= 7; num20++)
					{
						int num21 = num15 + num18 + num20;
						int num22 = ((num20 & 6) << 7) + (num20 & 1);
						int num23 = num16 + num19 + num22;
						num23 += 12;
						BasePrimitiveConverter.valueToTenBits[num21] = num23;
						BasePrimitiveConverter.tenBitsToValue[num23] = num21;
					}
				}
			}
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x00092CBC File Offset: 0x00090EBC
		private static void GenerateDecimalFloatOneSmall()
		{
			for (int i = 0; i <= 1; i++)
			{
				int num = (i + 8) * 100;
				int num2 = i << 7;
				for (int j = 0; j <= 1; j++)
				{
					int num3 = (j + 8) * 10;
					int num4 = j << 4;
					for (int k = 0; k <= 7; k++)
					{
						int num5 = num + num3 + k;
						int num6 = ((k & 6) << 7) + (k & 1);
						int num7 = num2 + num4 + num6;
						num7 += 14;
						BasePrimitiveConverter.valueToTenBits[num5] = num7;
						BasePrimitiveConverter.tenBitsToValue[num7] = num5;
					}
				}
			}
			for (int l = 0; l <= 1; l++)
			{
				int num8 = (l + 8) * 100;
				int num9 = l << 7;
				for (int m = 0; m <= 7; m++)
				{
					int num10 = m * 10;
					int num11 = ((m & 6) << 7) + ((m & 1) << 4);
					for (int n = 0; n <= 1; n++)
					{
						int num12 = num8 + num10 + n + 8;
						int num13 = num9 + num11 + n;
						num13 += 46;
						BasePrimitiveConverter.valueToTenBits[num12] = num13;
						BasePrimitiveConverter.tenBitsToValue[num13] = num12;
					}
				}
			}
			for (int num14 = 0; num14 <= 7; num14++)
			{
				int num15 = num14 * 100;
				int num16 = num14 << 7;
				for (int num17 = 0; num17 <= 1; num17++)
				{
					int num18 = (num17 + 8) * 10;
					int num19 = num17 << 4;
					for (int num20 = 0; num20 <= 1; num20++)
					{
						int num21 = num15 + num18 + num20 + 8;
						int num22 = num16 + num19 + num20;
						num22 += 78;
						BasePrimitiveConverter.valueToTenBits[num21] = num22;
						BasePrimitiveConverter.tenBitsToValue[num22] = num21;
					}
				}
			}
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x00092E4C File Offset: 0x0009104C
		private static void GenerateDecimalFloatAllBig()
		{
			for (int i = 0; i <= 1; i++)
			{
				int num = (i + 8) * 100;
				int num2 = i << 7;
				for (int j = 0; j <= 1; j++)
				{
					int num3 = (j + 8) * 10;
					int num4 = j << 4;
					for (int k = 0; k <= 1; k++)
					{
						int num5 = num + num3 + k + 8;
						int num6 = num2 + num4 + k;
						num6 += 110;
						BasePrimitiveConverter.valueToTenBits[num5] = num6;
						BasePrimitiveConverter.tenBitsToValue[num6] = num5;
						BasePrimitiveConverter.tenBitsToValue[num6 + 256] = num5;
						BasePrimitiveConverter.tenBitsToValue[num6 + 512] = num5;
						BasePrimitiveConverter.tenBitsToValue[num6 + 768] = num5;
					}
				}
			}
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x00092F04 File Offset: 0x00091104
		private static int Get3DigitsFrom10Bits(ulong uLongvalue, int indexTo10Bits)
		{
			ulong num = 1023UL << indexTo10Bits;
			ulong num2 = (uLongvalue & num) >> indexTo10Bits;
			return BasePrimitiveConverter.tenBitsToValue[(int)num2];
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x00092F30 File Offset: 0x00091130
		private static void Set10BitsFrom3Digits(ref ulong uLongvalue, int indexTo10Bits, ulong threeDigits)
		{
			ulong num = (ulong)((ulong)((long)BasePrimitiveConverter.valueToTenBits[(int)threeDigits]) << indexTo10Bits);
			uLongvalue |= num;
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x00092F54 File Offset: 0x00091154
		private static void GetDecimalFloatingPoint(ulong uLongValue, ref BasePrimitiveConverter.DecimalStruct refDecimal)
		{
			bool flag = (uLongValue & 9223372036854775808UL) > 0UL;
			uint num = (uint)(uLongValue >> 58) & 31U;
			if (num > 29U)
			{
				if (num != 30U)
				{
					throw new CustomHISException(SR.DecimalFloatFailedNaN);
				}
				if (flag)
				{
					throw new CustomHISException(SR.DecimalFloatFailedInfinityNegative);
				}
				throw new CustomHISException(SR.DecimalFloatFailedInfinityPositive);
			}
			else
			{
				uint num2 = (uint)(uLongValue >> 50) & 255U;
				int[] array = new int[6];
				int num3 = 0;
				for (int i = 0; i < 5; i++)
				{
					array[i] = BasePrimitiveConverter.Get3DigitsFrom10Bits(uLongValue, num3);
					num3 += 10;
				}
				uint num4 = num >> 3;
				uint num5;
				if (num4 != 3U)
				{
					num5 = (num4 << 8) + num2;
					array[5] = (int)(num & 7U);
				}
				else
				{
					num5 = (((num >> 1) & 3U) << 8) + num2;
					array[5] = (int)((num & 1U) + 8U);
				}
				int num6 = (int)(num5 - 398U);
				if (num6 > 0)
				{
					throw new CustomHISException(SR.DecimalFloatFailedPositiveExponent(num6));
				}
				int num7 = -num6;
				if (num7 > 28)
				{
					throw new CustomHISException(SR.DecimalFloatFailedExponent(num7));
				}
				long num8 = (long)array[5];
				for (int j = 4; j >= 0; j--)
				{
					num8 = num8 * 1000L + (long)array[j];
				}
				refDecimal = new BasePrimitiveConverter.DecimalStruct
				{
					Lo64 = num8,
					Hi32 = 0,
					sign = (flag ? 128 : 0),
					scale = (byte)num7
				};
				return;
			}
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000930B4 File Offset: 0x000912B4
		private unsafe static void GetDecimalFloatingPoint(ulong uLongValueLow, ulong uLongValueHigh, ref BasePrimitiveConverter.DecimalStruct refDecimal)
		{
			bool flag = (uLongValueHigh & 9223372036854775808UL) > 0UL;
			uint num = (uint)(uLongValueHigh >> 58) & 31U;
			if (num > 29U)
			{
				if (num != 30U)
				{
					throw new CustomHISException(SR.DecimalFloatFailedNaN);
				}
				if (flag)
				{
					throw new CustomHISException(SR.DecimalFloatFailedInfinityNegative);
				}
				throw new CustomHISException(SR.DecimalFloatFailedInfinityPositive);
			}
			else
			{
				uint num2 = (uint)(uLongValueHigh >> 46) & 4095U;
				int[] array = new int[12];
				int num3 = 0;
				for (int i = 0; i < 6; i++)
				{
					array[i] = BasePrimitiveConverter.Get3DigitsFrom10Bits(uLongValueLow, num3);
					num3 += 10;
				}
				ulong num4 = (uLongValueLow >> 60) + ((uLongValueHigh & 63UL) << 4);
				array[6] = BasePrimitiveConverter.Get3DigitsFrom10Bits(num4, 0);
				num3 = 6;
				for (int j = 7; j < 11; j++)
				{
					array[j] = BasePrimitiveConverter.Get3DigitsFrom10Bits(uLongValueHigh, num3);
					num3 += 10;
				}
				uint num5 = num >> 3;
				uint num6;
				if (num5 != 3U)
				{
					num6 = (num5 << 12) + num2;
					array[11] = (int)(num & 7U);
				}
				else
				{
					num6 = (((num >> 1) & 3U) << 12) + num2;
					array[11] = (int)((num & 1U) + 8U);
				}
				int num7 = (int)(num6 - 6176U);
				if (num7 > 0)
				{
					throw new CustomHISException(SR.DecimalFloatFailedPositiveExponent(num7));
				}
				int num8 = -num7;
				if (num8 > 28)
				{
					throw new CustomHISException(SR.DecimalFloatFailedExponent(num8));
				}
				long num9 = (long)array[5];
				for (int k = 4; k >= 0; k--)
				{
					num9 = num9 * 1000L + (long)array[k];
				}
				long num10 = (long)array[11];
				for (int l = 10; l >= 6; l--)
				{
					num10 = num10 * 1000L + (long)array[l];
				}
				byte[] array2 = BigInteger.Add(BigInteger.Multiply(new BigInteger(num10), BasePrimitiveConverter.bigInt10Power18), num9).ToByteArray();
				int num11 = array2.Length;
				if (array2[num11 - 1] == 0)
				{
					num11--;
				}
				if (num11 > 12)
				{
					throw new CustomHISException(SR.DecimalFloatFailedSignificand);
				}
				BasePrimitiveConverter.DecimalStruct decimalStruct = default(BasePrimitiveConverter.DecimalStruct);
				for (int m = 0; m < num11; m++)
				{
					*((ref decimalStruct.decByteValues.FixedElementField) + m) = array2[m];
				}
				decimalStruct.sign = (flag ? 128 : 0);
				decimalStruct.scale = (byte)num8;
				refDecimal = decimalStruct;
				return;
			}
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000932E4 File Offset: 0x000914E4
		private unsafe static void SetDecimalFloatingPoint8(BasePrimitiveConverter.DecimalStruct workDecimal, byte* bufferPtr)
		{
			if (workDecimal.Hi32 != 0 || workDecimal.Lo64 > 9999999999999999L)
			{
				throw new CustomHISException(SR.DecimalFloatFailedSignificand);
			}
			bool flag = workDecimal.sign > 0;
			ulong num = 0UL;
			int num2 = 0;
			ulong num3 = (ulong)workDecimal.Lo64;
			for (int i = 0; i < 5; i++)
			{
				BasePrimitiveConverter.Set10BitsFrom3Digits(ref num, num2, num3 % 1000UL);
				num3 /= 1000UL;
				num2 += 10;
			}
			int num4 = (int)(-(int)workDecimal.scale) + 398;
			int num5 = num4 & 255;
			num |= (ulong)((ulong)((long)num5) << 50);
			int num6 = num4 >> 8;
			ulong num7;
			if (num3 < 8UL)
			{
				num7 = (ulong)(((long)num6 << 3) + (long)num3);
			}
			else
			{
				num7 = (ulong)(((long)num6 << 1) + 16L + (long)num3);
			}
			num |= num7 << 58;
			BasePrimitiveConverter.Int64Union int64Union;
			int64Union.uint64Val = num;
			if (flag)
			{
				ref byte ptr = (ref int64Union.byteVal.FixedElementField) + 7;
				ptr |= 128;
			}
			bufferPtr[7] = int64Union.byteVal.FixedElementField;
			bufferPtr[6] = *((ref int64Union.byteVal.FixedElementField) + 1);
			bufferPtr[5] = *((ref int64Union.byteVal.FixedElementField) + 2);
			bufferPtr[4] = *((ref int64Union.byteVal.FixedElementField) + 3);
			bufferPtr[3] = *((ref int64Union.byteVal.FixedElementField) + 4);
			bufferPtr[2] = *((ref int64Union.byteVal.FixedElementField) + 5);
			bufferPtr[1] = *((ref int64Union.byteVal.FixedElementField) + 6);
			*bufferPtr = *((ref int64Union.byteVal.FixedElementField) + 7);
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x0009345C File Offset: 0x0009165C
		private unsafe static void SetDecimalFloatingPoint16(BasePrimitiveConverter.DecimalStruct workDecimal, byte* bufferPtr)
		{
			bool flag = workDecimal.sign > 0;
			BigInteger bigInteger = (uint)workDecimal.Hi32;
			BigInteger bigInteger2;
			ulong num = (ulong)BigInteger.DivRem(bigInteger * ulong.MaxValue + bigInteger + (ulong)workDecimal.Lo64, BasePrimitiveConverter.bigInt10Power18, out bigInteger2);
			ulong num2 = (ulong)bigInteger2;
			ulong num3 = 0UL;
			ulong num4 = 0UL;
			int num5 = 0;
			for (int i = 0; i < 6; i++)
			{
				BasePrimitiveConverter.Set10BitsFrom3Digits(ref num3, num5, num2 % 1000UL);
				num2 /= 1000UL;
				num5 += 10;
			}
			ulong num6 = 0UL;
			BasePrimitiveConverter.Set10BitsFrom3Digits(ref num6, 0, num % 1000UL);
			num /= 1000UL;
			num3 |= (num6 & 15UL) << 60;
			num4 = num6 >> 4;
			num5 = 6;
			for (int j = 7; j < 11; j++)
			{
				BasePrimitiveConverter.Set10BitsFrom3Digits(ref num4, num5, num % 1000UL);
				num /= 1000UL;
				num5 += 10;
			}
			int num7 = (int)(-(int)workDecimal.scale) + 6176;
			int num8 = num7 & 4095;
			num4 |= (ulong)((ulong)((long)num8) << 46);
			int num9 = num7 >> 12;
			ulong num10;
			if (num < 8UL)
			{
				num10 = (ulong)(((long)num9 << 3) + (long)num);
			}
			else
			{
				num10 = (ulong)(((long)num9 << 1) + 16L + (long)num);
			}
			num4 |= num10 << 58;
			BasePrimitiveConverter.Int64Union int64Union;
			int64Union.uint64Val = num4;
			if (flag)
			{
				ref byte ptr = (ref int64Union.byteVal.FixedElementField) + 7;
				ptr |= 128;
			}
			bufferPtr[7] = int64Union.byteVal.FixedElementField;
			bufferPtr[6] = *((ref int64Union.byteVal.FixedElementField) + 1);
			bufferPtr[5] = *((ref int64Union.byteVal.FixedElementField) + 2);
			bufferPtr[4] = *((ref int64Union.byteVal.FixedElementField) + 3);
			bufferPtr[3] = *((ref int64Union.byteVal.FixedElementField) + 4);
			bufferPtr[2] = *((ref int64Union.byteVal.FixedElementField) + 5);
			bufferPtr[1] = *((ref int64Union.byteVal.FixedElementField) + 6);
			*bufferPtr = *((ref int64Union.byteVal.FixedElementField) + 7);
			int64Union.uint64Val = num3;
			bufferPtr[15] = int64Union.byteVal.FixedElementField;
			bufferPtr[14] = *((ref int64Union.byteVal.FixedElementField) + 1);
			bufferPtr[13] = *((ref int64Union.byteVal.FixedElementField) + 2);
			bufferPtr[12] = *((ref int64Union.byteVal.FixedElementField) + 3);
			bufferPtr[11] = *((ref int64Union.byteVal.FixedElementField) + 4);
			bufferPtr[10] = *((ref int64Union.byteVal.FixedElementField) + 5);
			bufferPtr[9] = *((ref int64Union.byteVal.FixedElementField) + 6);
			bufferPtr[8] = *((ref int64Union.byteVal.FixedElementField) + 7);
		}

		// Token: 0x040018A5 RID: 6309
		private bool trimTrailingNulls;

		// Token: 0x040018A6 RID: 6310
		private bool acceptNullPacked;

		// Token: 0x040018A7 RID: 6311
		private bool acceptNullZoned;

		// Token: 0x040018A8 RID: 6312
		private bool acceptBadPacked;

		// Token: 0x040018A9 RID: 6313
		private bool acceptAllInvalidNumerics;

		// Token: 0x040018AA RID: 6314
		private bool stringsAreNullTerminatedAndSpacePadded;

		// Token: 0x040018AB RID: 6315
		private bool alwaysUseNullTerminate;

		// Token: 0x040018AC RID: 6316
		private bool convertReceivedStringsAsIs;

		// Token: 0x040018AD RID: 6317
		private CommonHISEventLogging HisLogging;

		// Token: 0x040018AE RID: 6318
		private int inLCID;

		// Token: 0x040018AF RID: 6319
		private char timeSep = ':';

		// Token: 0x040018B0 RID: 6320
		private char dateSep = '/';

		// Token: 0x040018B1 RID: 6321
		private BasePrimitiveConverter.CodePageInfo codePageInfo;

		// Token: 0x040018B2 RID: 6322
		private static bool isCodePageSet = false;

		// Token: 0x040018B3 RID: 6323
		private bool dontUsePresentationFormsBForArabicStrings;

		// Token: 0x040018B4 RID: 6324
		private static Dictionary<int, List<BasePrimitiveConverter.CodePageInfo>> CPInfoHashTable = new Dictionary<int, List<BasePrimitiveConverter.CodePageInfo>>();

		// Token: 0x040018B5 RID: 6325
		private Dictionary<int, BasePrimitiveConverter.CodePageInfo> CPInfos = new Dictionary<int, BasePrimitiveConverter.CodePageInfo>(5);

		// Token: 0x040018B6 RID: 6326
		private static Dictionary<char, bool> EditPatternCharacters = new Dictionary<char, bool>();

		// Token: 0x040018B7 RID: 6327
		private static Hashtable SpecialOverrides = null;

		// Token: 0x040018B8 RID: 6328
		private static PrimitiveConverterDummyTracePoint dummyTracePoint = new PrimitiveConverterDummyTracePoint();

		// Token: 0x040018B9 RID: 6329
		private PrimitiveConverterTracePoint tracePoint = BasePrimitiveConverter.dummyTracePoint;

		// Token: 0x040018BA RID: 6330
		private static char[] charDigits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

		// Token: 0x040018BB RID: 6331
		private static Dictionary<string, BasePrimitiveConverter.EditPattern> dateEditPatterns = new Dictionary<string, BasePrimitiveConverter.EditPattern>();

		// Token: 0x040018BC RID: 6332
		private char[] editedTimeString;

		// Token: 0x040018BD RID: 6333
		private int editedTimeStringLength;

		// Token: 0x040018BE RID: 6334
		private static FourDigits[] FourDigitNumbers;

		// Token: 0x040018BF RID: 6335
		private static int[] FractionalSecondsDivisors = new int[] { 0, 1000000, 100000, 10000, 1000, 100, 10, 1 };

		// Token: 0x040018C0 RID: 6336
		private static int[] valueToTenBits = new int[1000];

		// Token: 0x040018C1 RID: 6337
		private static int[] tenBitsToValue = new int[1024];

		// Token: 0x040018C2 RID: 6338
		private const ulong TenBitMask = 1023UL;

		// Token: 0x040018C3 RID: 6339
		private static BigInteger bigInt10Power18 = new BigInteger(1000000000000000000L);

		// Token: 0x040018C5 RID: 6341
		private const int zonePortion = 0;

		// Token: 0x040018C6 RID: 6342
		private const int numericPortion = 1;

		// Token: 0x040018C7 RID: 6343
		private const int IBM390_SHORT_SIZE = 2;

		// Token: 0x040018C8 RID: 6344
		private const int IBM390_LONG_SIZE = 4;

		// Token: 0x040018C9 RID: 6345
		private const int IBM390_FLOAT_SIZE = 4;

		// Token: 0x040018CA RID: 6346
		private const int IBM390_DOUBLE_SIZE = 8;

		// Token: 0x040018CB RID: 6347
		private const int DecimalFloat64Size = 8;

		// Token: 0x040018CC RID: 6348
		private const int DecimalFloat128Size = 16;

		// Token: 0x040018CD RID: 6349
		private const int PackedDateSize = 4;

		// Token: 0x040018CE RID: 6350
		private const int PackedTimeSize = 4;

		// Token: 0x040018CF RID: 6351
		private const int ISODateTimeSize = 19;

		// Token: 0x040018D0 RID: 6352
		private const int ISODateSize = 10;

		// Token: 0x040018D1 RID: 6353
		private const int ISOTimeSize = 8;

		// Token: 0x040018D2 RID: 6354
		private const int USADateTimeSize = 19;

		// Token: 0x040018D3 RID: 6355
		private const int USADateSize = 10;

		// Token: 0x040018D4 RID: 6356
		private const int USATimeSize = 8;

		// Token: 0x040018D5 RID: 6357
		private const int JISDateTimeSize = 19;

		// Token: 0x040018D6 RID: 6358
		private const int JISDateSize = 10;

		// Token: 0x040018D7 RID: 6359
		private const int JISTimeSize = 8;

		// Token: 0x040018D8 RID: 6360
		private const int EURDateTimeSize = 19;

		// Token: 0x040018D9 RID: 6361
		private const int EURDateSize = 10;

		// Token: 0x040018DA RID: 6362
		private const int EURTimeSize = 8;

		// Token: 0x040018DB RID: 6363
		private const int TimeStampSize = 26;

		// Token: 0x040018DC RID: 6364
		private const int HMSTimeSize = 8;

		// Token: 0x040018DD RID: 6365
		private const int MDYDateSize = 8;

		// Token: 0x040018DE RID: 6366
		private const int DMYDateSize = 8;

		// Token: 0x040018DF RID: 6367
		private const int YMDDateSize = 8;

		// Token: 0x040018E0 RID: 6368
		private const int JULDateSize = 6;

		// Token: 0x040018E1 RID: 6369
		private const int LongJULDateSize = 8;

		// Token: 0x040018E2 RID: 6370
		private const int HighBits = 0;

		// Token: 0x040018E3 RID: 6371
		private const int LowBits = 1;

		// Token: 0x040018E4 RID: 6372
		private const int MaxLongValue = 2147483647;

		// Token: 0x040018E5 RID: 6373
		private const long MaxULongValue = 4294967295L;

		// Token: 0x040018E6 RID: 6374
		private const short MaxByteValue = 255;

		// Token: 0x040018E7 RID: 6375
		private const int MaxPackedExtPrecision = 29;

		// Token: 0x040018E8 RID: 6376
		private const int MaxCyScale = 4;

		// Token: 0x040018E9 RID: 6377
		private const short MaxValue = 32767;

		// Token: 0x040018EA RID: 6378
		private static short[] x_sDaysOfMonth = new short[]
		{
			31, 28, 31, 30, 31, 30, 31, 31, 30, 31,
			30, 31
		};

		// Token: 0x040018EB RID: 6379
		private static short[] MaxShortValues = new short[] { 0, 9, 99, 999, 9999, short.MaxValue };

		// Token: 0x040018EC RID: 6380
		private static int[] MaxIntValues = new int[]
		{
			0, 9, 99, 999, 9999, 99999, 999999, 9999999, 99999999, 999999999,
			int.MaxValue
		};

		// Token: 0x040018ED RID: 6381
		private static long[] MaxLongValues = new long[]
		{
			0L, 9L, 99L, 999L, 9999L, 99999L, 999999L, 9999999L, 99999999L, 999999999L,
			9999999999L, 99999999999L, 999999999999L, 9999999999999L, 99999999999999L, 999999999999999L, 9999999999999999L, 99999999999999999L, 999999999999999999L, long.MaxValue
		};

		// Token: 0x040018EE RID: 6382
		private static long[] longPower = new long[]
		{
			1L, 10L, 100L, 1000L, 10000L, 100000L, 1000000L, 10000000L, 100000000L, 1000000000L,
			10000000000L, 100000000000L, 1000000000000L, 10000000000000L, 100000000000000L, 1000000000000000L, 10000000000000000L, 100000000000000000L, 1000000000000000000L
		};

		// Token: 0x040018EF RID: 6383
		private static ulong[] llBitFractionArray = new ulong[]
		{
			9223372036854775808UL, 4611686018427387904UL, 2305843009213693952UL, 1152921504606846976UL, 576460752303423488UL, 288230376151711744UL, 144115188075855872UL, 72057594037927936UL, 36028797018963968UL, 18014398509481984UL,
			9007199254740992UL, 4503599627370496UL, 2251799813685248UL, 1125899906842624UL, 562949953421312UL, 281474976710656UL, 140737488355328UL, 70368744177664UL, 35184372088832UL, 17592186044416UL,
			8796093022208UL, 4398046511104UL, 2199023255552UL, 1099511627776UL, 549755813888UL, 274877906944UL, 137438953472UL, 68719476736UL, 34359738368UL, 17179869184UL,
			8589934592UL, 4294967296UL, 2147483648UL, 1073741824UL, 536870912UL, 268435456UL, 134217728UL, 67108864UL, 33554432UL, 16777216UL,
			8388608UL, 4194304UL, 2097152UL, 1048576UL, 524288UL, 262144UL, 131072UL, 65536UL, 32768UL, 16384UL,
			8192UL, 4096UL, 2048UL, 1024UL, 512UL, 256UL, 128UL, 64UL, 32UL, 16UL,
			8UL, 4UL, 2UL, 1UL
		};

		// Token: 0x040018F0 RID: 6384
		private static long[,] longValues = new long[,]
		{
			{ 0L, 1L, 2L, 3L, 4L, 5L, 6L, 7L, 8L, 9L },
			{ 0L, 10L, 20L, 30L, 40L, 50L, 60L, 70L, 80L, 90L },
			{ 0L, 100L, 200L, 300L, 400L, 500L, 600L, 700L, 800L, 900L },
			{ 0L, 1000L, 2000L, 3000L, 4000L, 5000L, 6000L, 7000L, 8000L, 9000L },
			{ 0L, 10000L, 20000L, 30000L, 40000L, 50000L, 60000L, 70000L, 80000L, 90000L },
			{ 0L, 100000L, 200000L, 300000L, 400000L, 500000L, 600000L, 700000L, 800000L, 900000L },
			{ 0L, 1000000L, 2000000L, 3000000L, 4000000L, 5000000L, 6000000L, 7000000L, 8000000L, 9000000L },
			{ 0L, 10000000L, 20000000L, 30000000L, 40000000L, 50000000L, 60000000L, 70000000L, 80000000L, 90000000L },
			{ 0L, 100000000L, 200000000L, 300000000L, 400000000L, 500000000L, 600000000L, 700000000L, 800000000L, 900000000L },
			{ 0L, 1000000000L, 2000000000L, 3000000000L, 4000000000L, 5000000000L, 6000000000L, 7000000000L, 8000000000L, 9000000000L },
			{ 0L, 10000000000L, 20000000000L, 30000000000L, 40000000000L, 50000000000L, 60000000000L, 70000000000L, 80000000000L, 90000000000L },
			{ 0L, 100000000000L, 200000000000L, 300000000000L, 400000000000L, 500000000000L, 600000000000L, 700000000000L, 800000000000L, 900000000000L },
			{ 0L, 1000000000000L, 2000000000000L, 3000000000000L, 4000000000000L, 5000000000000L, 6000000000000L, 7000000000000L, 8000000000000L, 9000000000000L },
			{ 0L, 10000000000000L, 20000000000000L, 30000000000000L, 40000000000000L, 50000000000000L, 60000000000000L, 70000000000000L, 80000000000000L, 90000000000000L },
			{ 0L, 100000000000000L, 200000000000000L, 300000000000000L, 400000000000000L, 500000000000000L, 600000000000000L, 700000000000000L, 800000000000000L, 900000000000000L },
			{ 0L, 1000000000000000L, 2000000000000000L, 3000000000000000L, 4000000000000000L, 5000000000000000L, 6000000000000000L, 7000000000000000L, 8000000000000000L, 9000000000000000L },
			{ 0L, 10000000000000000L, 20000000000000000L, 30000000000000000L, 40000000000000000L, 50000000000000000L, 60000000000000000L, 70000000000000000L, 80000000000000000L, 90000000000000000L },
			{ 0L, 100000000000000000L, 200000000000000000L, 300000000000000000L, 400000000000000000L, 500000000000000000L, 600000000000000000L, 700000000000000000L, 800000000000000000L, 900000000000000000L },
			{ 0L, 1000000000000000000L, 2000000000000000000L, 3000000000000000000L, 4000000000000000000L, 5000000000000000000L, 6000000000000000000L, 7000000000000000000L, 8000000000000000000L, 9000000000000000000L }
		};

		// Token: 0x040018F1 RID: 6385
		private static long[,,] TwoPartVal = new long[,,]
		{
			{
				{ 0L, 1L, 2L, 3L, 4L, 5L, 6L, 7L, 8L, 9L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 10L, 20L, 30L, 40L, 50L, 60L, 70L, 80L, 90L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 100L, 200L, 300L, 400L, 500L, 600L, 700L, 800L, 900L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 1000L, 2000L, 3000L, 4000L, 5000L, 6000L, 7000L, 8000L, 9000L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 10000L, 20000L, 30000L, 40000L, 50000L, 60000L, 70000L, 80000L, 90000L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 100000L, 200000L, 300000L, 400000L, 500000L, 600000L, 700000L, 800000L, 900000L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 1000000L, 2000000L, 3000000L, 4000000L, 5000000L, 6000000L, 7000000L, 8000000L, 9000000L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 10000000L, 20000000L, 30000000L, 40000000L, 50000000L, 60000000L, 70000000L, 80000000L, 90000000L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 100000000L, 200000000L, 300000000L, 400000000L, 500000000L, 600000000L, 700000000L, 800000000L, 900000000L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 1000000000L, 2000000000L, 3000000000L, 4000000000L, 5000000000L, 6000000000L, 7000000000L, 8000000000L, 9000000000L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 10000000000L, 20000000000L, 30000000000L, 40000000000L, 50000000000L, 60000000000L, 70000000000L, 80000000000L, 90000000000L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 100000000000L, 200000000000L, 300000000000L, 400000000000L, 500000000000L, 600000000000L, 700000000000L, 800000000000L, 900000000000L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 1000000000000L, 2000000000000L, 3000000000000L, 4000000000000L, 5000000000000L, 6000000000000L, 7000000000000L, 8000000000000L, 9000000000000L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 10000000000000L, 20000000000000L, 30000000000000L, 40000000000000L, 50000000000000L, 60000000000000L, 70000000000000L, 80000000000000L, 90000000000000L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 100000000000000L, 200000000000000L, 300000000000000L, 400000000000000L, 500000000000000L, 600000000000000L, 700000000000000L, 800000000000000L, 900000000000000L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 1000000000000000L, 2000000000000000L, 3000000000000000L, 4000000000000000L, 5000000000000000L, 6000000000000000L, 7000000000000000L, 8000000000000000L, 9000000000000000L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L }
			},
			{
				{ 0L, 10000000000000000L, 20000000000000000L, 30000000000000000L, 40000000000000000L, 50000000000000000L, 60000000000000000L, 70000000000000000L, 7942405962072064L, 17942405962072064L },
				{ 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 1L, 1L }
			},
			{
				{ 0L, 27942405962072064L, 55884811924144128L, 11769623848288256L, 39712029810360320L, 67654435772432384L, 23539247696576512L, 51481653658648576L, 7366465582792704L, 35308871544864768L },
				{ 0L, 1L, 2L, 4L, 5L, 6L, 8L, 9L, 11L, 12L }
			},
			{
				{ 0L, 63251277506936832L, 54444960975945728L, 45638644444954624L, 36832327913963520L, 28026011382972416L, 19219694851981312L, 10413378320990208L, 1607061789999104L, 64858339296935936L },
				{ 0L, 13L, 27L, 41L, 55L, 69L, 83L, 97L, 111L, 124L }
			},
			{
				{ 0L, 56052022765944832L, 40046451493961728L, 24040880221978624L, 8035308949995520L, 64087331715940352L, 48081760443957248L, 32076189171974144L, 16070617899991040L, 65046628007936L },
				{ 0L, 138L, 277L, 416L, 555L, 693L, 832L, 971L, 1110L, 1249L }
			},
			{
				{ 0L, 56117069393952768L, 40176544749977600L, 24236020106002432L, 8295495462027264L, 64412564855980032L, 48472040212004864L, 32531515568029696L, 16590990924054528L, 650466280079360L },
				{ 0L, 1387L, 2775L, 4163L, 5551L, 6938L, 8326L, 9714L, 11102L, 12490L }
			},
			{
				{ 0L, 56767535674032128L, 41477477310136320L, 26187418946240512L, 10897360582344704L, 67664896256376832L, 52374837892481024L, 37084779528585216L, 21794721164689408L, 6504662800793600L },
				{ 0L, 13877L, 27755L, 41633L, 55511L, 69388L, 83266L, 97144L, 111022L, 124900L }
			},
			{
				{ 0L, 63272198474825728L, 54486802911723520L, 45701407348621312L, 36916011785519104L, 28130616222416896L, 19345220659314688L, 10559825096212480L, 1774429533110272L, 65046628007936000L },
				{ 0L, 138777L, 277555L, 416333L, 555111L, 693889L, 832667L, 971445L, 1110223L, 1249000L }
			},
			{
				{ 0L, 56261232444833792L, 40464870851739648L, 24668509258645504L, 8872147665551360L, 65133380110385152L, 49337018517291008L, 33540656924196864L, 17744295331102720L, 1947933738008576L },
				{ 0L, 1387778L, 2775557L, 4163336L, 5551115L, 6938893L, 8326672L, 9714451L, 11102230L, 12490009L }
			},
			{
				{ 0L, 58209166182842368L, 44360738327756800L, 30512310472671232L, 16663882617585664L, 2815454762500096L, 61024620945342464L, 47176193090256896L, 33327765235171328L, 19479337380085760L },
				{ 0L, 13877787L, 27755575L, 41633363L, 55511151L, 69388939L, 83266726L, 97144514L, 111022302L, 124900090L }
			},
			{
				{ 0L, 5630909525000192L, 11261819050000384L, 16892728575000576L, 22523638100000768L, 28154547625000960L, 33785457150001152L, 39416366675001344L, 45047276200001536L, 50678185725001728L },
				{ 0L, 138777878L, 277555756L, 416333634L, 555111512L, 693889390L, 832667268L, 971445146L, 1110223024L, 1249000902L }
			},
			{
				{ 0L, 56309095250001920L, 40560596462075904L, 24812097674149888L, 9063598886223872L, 65372694136225792L, 49624195348299776L, 33875696560373760L, 18127197772447744L, 2378698984521728L },
				{ 0L, 1387778780L, 2775557561L, 4163336342L, 5551115123L, 6938893903L, 8326672684L, 9714451465L, 11102230246L, 12490009027L }
			},
			{
				{ 0L, 58687794234523648L, 45317994431119360L, 31948194627715072L, 18578394824310784L, 5208595020906496L, 63896389255430144L, 50526589452025856L, 37156789648621568L, 23786989845217280L },
				{ 0L, 13877787807L, 27755575615L, 41633363423L, 55511151231L, 69388939039L, 83266726846L, 97144514654L, 111022302462L, 124900090270L }
			},
			{
				{ 0L, 10417190041812992L, 20834380083625984L, 31251570125438976L, 41668760167251968L, 52085950209064960L, 62503140250877952L, 862736254763008L, 11279926296616000L, 21697116338388992L },
				{ 0L, 138777878078L, 277555756156L, 416333634234L, 555111512312L, 693889390390L, 832667268468L, 971445146547L, 1110223024625L, 1249000902703L }
			}
		};

		// Token: 0x040018F2 RID: 6386
		private static float[] x_flValWhole = new float[]
		{
			0f, 9f, 99f, 999f, 9999f, 99999f, 999999f, 9999999f, 100000000f, 1E+09f,
			1E+10f, 1E+11f, 1E+12f, 1E+13f, 1E+14f, 1E+15f, 1E+16f, 1E+17f, 1E+18f, 1E+19f,
			1E+20f, 1E+21f, 1E+22f, 1E+23f, 1E+24f, 1E+25f, 1E+26f, 1E+27f, 1E+28f, 1E+29f,
			1E+30f
		};

		// Token: 0x040018F3 RID: 6387
		private static float[] x_flValFrac = new float[]
		{
			0f, 0.9f, 0.99f, 0.999f, 0.9999f, 0.99999f, 0.999999f, 0.9999999f, 1f, 1f,
			1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f,
			1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f,
			1f, 1f
		};

		// Token: 0x040018F4 RID: 6388
		private static float[] x_flValRound = new float[]
		{
			0.5f, 0.05f, 0.005f, 0.0005f, 5E-05f, 5E-06f, 5E-07f, 5E-08f, 5E-09f, 5E-10f,
			5E-11f, 5E-12f, 5E-13f, 5E-14f, 5E-15f, 5E-16f, 5E-17f, 5E-18f, 5E-19f, 5E-20f,
			5E-21f, 5E-22f, 5E-23f, 5E-24f, 5E-25f, 5E-26f, 5E-27f, 5E-28f, 5E-29f, 5E-30f,
			5E-31f
		};

		// Token: 0x040018F5 RID: 6389
		private static double[] x_dValWhole = new double[]
		{
			0.0, 9.0, 99.0, 999.0, 9999.0, 99999.0, 999999.0, 9999999.0, 99999999.0, 999999999.0,
			9999999999.0, 99999999999.0, 999999999999.0, 9999999999999.0, 99999999999999.0, 999999999999999.0, 10000000000000000.0, 1E+17, 1E+18, 1E+19,
			1E+20, 1E+21, 1E+22, 1E+23, 1E+24, 1E+25, 1E+26, 1E+27, 1E+28, 1E+29,
			1E+30
		};

		// Token: 0x040018F6 RID: 6390
		private static double[] x_dValFrac = new double[]
		{
			0.0, 0.9, 0.99, 0.999, 0.9999, 0.99999, 0.999999, 0.9999999, 0.99999999, 0.999999999,
			0.9999999999, 0.99999999999, 0.999999999999, 0.9999999999999, 0.99999999999999, 0.999999999999999, 0.9999999999999999, 1.0, 1.0, 1.0,
			1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0,
			1.0, 1.0
		};

		// Token: 0x040018F7 RID: 6391
		private static double[] x_dValRound = new double[]
		{
			0.5, 0.05, 0.005, 0.0005, 5E-05, 5E-06, 5E-07, 5E-08, 5E-09, 5E-10,
			5E-11, 5E-12, 5E-13, 5E-14, 5E-15, 5E-16, 5E-17, 5E-18, 5E-19, 5E-20,
			5E-21, 5E-22, 5E-23, 5E-24, 5E-25, 5E-26, 5E-27, 5E-28, 5E-29, 5E-30,
			5E-31
		};

		// Token: 0x040018F8 RID: 6392
		private const char LeadingSignedIntDigit = 'G';

		// Token: 0x040018F9 RID: 6393
		private const char LeadingSignedFracDigit = 'g';

		// Token: 0x040018FA RID: 6394
		private const char TrailingSignedIntDigit = 'J';

		// Token: 0x040018FB RID: 6395
		private const char TrailingSignedFracDigit = 'j';

		// Token: 0x040018FC RID: 6396
		private const char SignedIntDigit = 'K';

		// Token: 0x040018FD RID: 6397
		private const char SignedFracDigit = 'k';

		// Token: 0x040018FE RID: 6398
		private const char PosIntDigit = 'I';

		// Token: 0x040018FF RID: 6399
		private const char PosFracDigit = 'i';

		// Token: 0x04001900 RID: 6400
		private const char NegIntDigit = 'R';

		// Token: 0x04001901 RID: 6401
		private const char NegFracDigit = 'r';

		// Token: 0x04001902 RID: 6402
		private const char ProcessExpDigit = '7';

		// Token: 0x04001903 RID: 6403
		private const char ProcessFracDigit = '8';

		// Token: 0x04001904 RID: 6404
		private const char ProcessIntDigit = '9';

		// Token: 0x04001905 RID: 6405
		private const char SuppressIntZero = 'Y';

		// Token: 0x04001906 RID: 6406
		private const char SuppressFracZero = 'y';

		// Token: 0x04001907 RID: 6407
		private const char SuppressIntDigit = 'Z';

		// Token: 0x04001908 RID: 6408
		private const char SuppressFracDigit = 'z';

		// Token: 0x04001909 RID: 6409
		private const char InsertFracAst = 'A';

		// Token: 0x0400190A RID: 6410
		private const char InsertIntAst = '*';

		// Token: 0x0400190B RID: 6411
		private const char SuppressDecPoint = 'h';

		// Token: 0x0400190C RID: 6412
		private const char FloatFracPlus = 'p';

		// Token: 0x0400190D RID: 6413
		private const char FloatIntPlus = 'P';

		// Token: 0x0400190E RID: 6414
		private const char FirstIntPlus = '>';

		// Token: 0x0400190F RID: 6415
		private const char InsertExpPlus = 'X';

		// Token: 0x04001910 RID: 6416
		private const char FloatFracMinus = 'm';

		// Token: 0x04001911 RID: 6417
		private const char FloatIntMinus = 'M';

		// Token: 0x04001912 RID: 6418
		private const char FirstIntMinus = '<';

		// Token: 0x04001913 RID: 6419
		private const char InsertExpMinus = 'x';

		// Token: 0x04001914 RID: 6420
		private const char FloatFracCurr = 'd';

		// Token: 0x04001915 RID: 6421
		private const char FloatIntCurr = 'l';

		// Token: 0x04001916 RID: 6422
		private const char FirstIntCurr = '#';

		// Token: 0x04001917 RID: 6423
		private const char InsertE = 'E';

		// Token: 0x04001918 RID: 6424
		private const char InsertDebit = 'D';

		// Token: 0x04001919 RID: 6425
		private const char InsertCredit = 'C';

		// Token: 0x0400191A RID: 6426
		private const char InsertPlus = '+';

		// Token: 0x0400191B RID: 6427
		private const char InsertCurrency = '$';

		// Token: 0x0400191C RID: 6428
		private const char InsertZero = '0';

		// Token: 0x0400191D RID: 6429
		private const char InsertComma = ',';

		// Token: 0x0400191E RID: 6430
		private const char InsertSpace = 'b';

		// Token: 0x0400191F RID: 6431
		private const char InsertPeriod = '.';

		// Token: 0x04001920 RID: 6432
		private const char InsertSlash = '/';

		// Token: 0x04001921 RID: 6433
		private const char InsertMinus = '-';

		// Token: 0x04001922 RID: 6434
		private const char InsertColon = ':';

		// Token: 0x04001923 RID: 6435
		private const char InsertDateSep = 'd';

		// Token: 0x04001924 RID: 6436
		private const char InsertTimeSep = 't';

		// Token: 0x04001925 RID: 6437
		private const char InsertYearDigit = 'y';

		// Token: 0x04001926 RID: 6438
		private const char InsertMonthDigit = 'M';

		// Token: 0x04001927 RID: 6439
		private const char InsertDayDigit = 'd';

		// Token: 0x04001928 RID: 6440
		private const char InsertJulianDigit = 'J';

		// Token: 0x04001929 RID: 6441
		private const char InsertHourDigit = 'h';

		// Token: 0x0400192A RID: 6442
		private const char InsertMinuteDigit = 'm';

		// Token: 0x0400192B RID: 6443
		private const char InsertSecondDigit = 's';

		// Token: 0x0400192C RID: 6444
		private const char InsertFractionOfSecondDigit = 'f';

		// Token: 0x0400192D RID: 6445
		private const char InsertAMPMSymbol = 'A';

		// Token: 0x0400192E RID: 6446
		private const int UnpaddedStringEncoding = 184549381;

		// Token: 0x0400192F RID: 6447
		private const byte EBCDIC_SO = 14;

		// Token: 0x04001930 RID: 6448
		private const byte EBCDIC_SI = 15;

		// Token: 0x04001931 RID: 6449
		private const byte hex00 = 0;

		// Token: 0x04001932 RID: 6450
		private const byte hex01 = 1;

		// Token: 0x04001933 RID: 6451
		private const byte hex07 = 7;

		// Token: 0x04001934 RID: 6452
		private const byte hex0A = 10;

		// Token: 0x04001935 RID: 6453
		private const byte hex0B = 11;

		// Token: 0x04001936 RID: 6454
		private const byte hex0C = 12;

		// Token: 0x04001937 RID: 6455
		private const byte hex0D = 13;

		// Token: 0x04001938 RID: 6456
		private const byte hex0E = 14;

		// Token: 0x04001939 RID: 6457
		private const byte hex0F = 15;

		// Token: 0x0400193A RID: 6458
		private const byte hex10 = 16;

		// Token: 0x0400193B RID: 6459
		private const byte hex1C = 28;

		// Token: 0x0400193C RID: 6460
		private const byte hex1F = 31;

		// Token: 0x0400193D RID: 6461
		private const byte hex7F = 127;

		// Token: 0x0400193E RID: 6462
		private const byte hex40 = 64;

		// Token: 0x0400193F RID: 6463
		private const byte hex80 = 128;

		// Token: 0x04001940 RID: 6464
		private const byte hexA0 = 160;

		// Token: 0x04001941 RID: 6465
		private const byte hexA1 = 161;

		// Token: 0x04001942 RID: 6466
		private const byte hexA2 = 162;

		// Token: 0x04001943 RID: 6467
		private const byte hexA3 = 163;

		// Token: 0x04001944 RID: 6468
		private const byte hexA4 = 164;

		// Token: 0x04001945 RID: 6469
		private const byte hexA5 = 165;

		// Token: 0x04001946 RID: 6470
		private const byte hexA6 = 166;

		// Token: 0x04001947 RID: 6471
		private const byte hexA7 = 167;

		// Token: 0x04001948 RID: 6472
		private const byte hexA8 = 168;

		// Token: 0x04001949 RID: 6473
		private const byte hexA9 = 169;

		// Token: 0x0400194A RID: 6474
		private const byte hexB0 = 176;

		// Token: 0x0400194B RID: 6475
		private const byte hexB1 = 177;

		// Token: 0x0400194C RID: 6476
		private const byte hexB2 = 178;

		// Token: 0x0400194D RID: 6477
		private const byte hexB3 = 179;

		// Token: 0x0400194E RID: 6478
		private const byte hexB4 = 180;

		// Token: 0x0400194F RID: 6479
		private const byte hexB5 = 181;

		// Token: 0x04001950 RID: 6480
		private const byte hexB6 = 182;

		// Token: 0x04001951 RID: 6481
		private const byte hexB7 = 183;

		// Token: 0x04001952 RID: 6482
		private const byte hexB8 = 184;

		// Token: 0x04001953 RID: 6483
		private const byte hexB9 = 185;

		// Token: 0x04001954 RID: 6484
		private const byte hexC0 = 192;

		// Token: 0x04001955 RID: 6485
		private const byte hexC1 = 193;

		// Token: 0x04001956 RID: 6486
		private const byte hexC2 = 194;

		// Token: 0x04001957 RID: 6487
		private const byte hexC3 = 195;

		// Token: 0x04001958 RID: 6488
		private const byte hexC4 = 196;

		// Token: 0x04001959 RID: 6489
		private const byte hexC5 = 197;

		// Token: 0x0400195A RID: 6490
		private const byte hexC6 = 198;

		// Token: 0x0400195B RID: 6491
		private const byte hexC7 = 199;

		// Token: 0x0400195C RID: 6492
		private const byte hexC8 = 200;

		// Token: 0x0400195D RID: 6493
		private const byte hexC9 = 201;

		// Token: 0x0400195E RID: 6494
		private const byte hexCF = 207;

		// Token: 0x0400195F RID: 6495
		private const byte hexD0 = 208;

		// Token: 0x04001960 RID: 6496
		private const byte hexD1 = 209;

		// Token: 0x04001961 RID: 6497
		private const byte hexD2 = 210;

		// Token: 0x04001962 RID: 6498
		private const byte hexD3 = 211;

		// Token: 0x04001963 RID: 6499
		private const byte hexD4 = 212;

		// Token: 0x04001964 RID: 6500
		private const byte hexD5 = 213;

		// Token: 0x04001965 RID: 6501
		private const byte hexD6 = 214;

		// Token: 0x04001966 RID: 6502
		private const byte hexD7 = 215;

		// Token: 0x04001967 RID: 6503
		private const byte hexD8 = 216;

		// Token: 0x04001968 RID: 6504
		private const byte hexD9 = 217;

		// Token: 0x04001969 RID: 6505
		private const byte hexDF = 223;

		// Token: 0x0400196A RID: 6506
		private const byte hexE0 = 224;

		// Token: 0x0400196B RID: 6507
		private const byte hexE1 = 225;

		// Token: 0x0400196C RID: 6508
		private const byte hexE2 = 226;

		// Token: 0x0400196D RID: 6509
		private const byte hexE3 = 227;

		// Token: 0x0400196E RID: 6510
		private const byte hexE4 = 228;

		// Token: 0x0400196F RID: 6511
		private const byte hexE5 = 229;

		// Token: 0x04001970 RID: 6512
		private const byte hexE6 = 230;

		// Token: 0x04001971 RID: 6513
		private const byte hexE7 = 231;

		// Token: 0x04001972 RID: 6514
		private const byte hexE8 = 232;

		// Token: 0x04001973 RID: 6515
		private const byte hexE9 = 233;

		// Token: 0x04001974 RID: 6516
		private const byte hexF0 = 240;

		// Token: 0x04001975 RID: 6517
		private const byte hexF1 = 241;

		// Token: 0x04001976 RID: 6518
		private const byte hexF2 = 242;

		// Token: 0x04001977 RID: 6519
		private const byte hexF3 = 243;

		// Token: 0x04001978 RID: 6520
		private const byte hexF4 = 244;

		// Token: 0x04001979 RID: 6521
		private const byte hexF5 = 245;

		// Token: 0x0400197A RID: 6522
		private const byte hexF6 = 246;

		// Token: 0x0400197B RID: 6523
		private const byte hexF7 = 247;

		// Token: 0x0400197C RID: 6524
		private const byte hexF8 = 248;

		// Token: 0x0400197D RID: 6525
		private const byte hexF9 = 249;

		// Token: 0x0400197E RID: 6526
		private const byte hexFF = 255;

		// Token: 0x020004CD RID: 1229
		private class CodePageInfo
		{
			// Token: 0x0400197F RID: 6527
			public int codePage;

			// Token: 0x04001980 RID: 6528
			public bool isDBCS;

			// Token: 0x04001981 RID: 6529
			public bool isPeriodComma;

			// Token: 0x04001982 RID: 6530
			public HisEncoding EncodingIBM;

			// Token: 0x04001983 RID: 6531
			public Encoder EncoderIBM;

			// Token: 0x04001984 RID: 6532
			public Decoder DecoderIBM;

			// Token: 0x04001985 RID: 6533
			public string unicodeCurrency;

			// Token: 0x04001986 RID: 6534
			public byte[] ebcdicCurrency;

			// Token: 0x04001987 RID: 6535
			public BasePrimitiveConverter.FixedChars fixedEBCDIC;

			// Token: 0x04001988 RID: 6536
			public bool dontUsePresentationFormsB;
		}

		// Token: 0x020004CE RID: 1230
		private struct FixedChars
		{
			// Token: 0x04001989 RID: 6537
			public byte COMMA;

			// Token: 0x0400198A RID: 6538
			public byte PERIOD;

			// Token: 0x0400198B RID: 6539
			public byte B;

			// Token: 0x0400198C RID: 6540
			public byte b;

			// Token: 0x0400198D RID: 6541
			public byte P;

			// Token: 0x0400198E RID: 6542
			public byte p;

			// Token: 0x0400198F RID: 6543
			public byte V;

			// Token: 0x04001990 RID: 6544
			public byte v;

			// Token: 0x04001991 RID: 6545
			public byte Z;

			// Token: 0x04001992 RID: 6546
			public byte z;

			// Token: 0x04001993 RID: 6547
			public byte ZERO;

			// Token: 0x04001994 RID: 6548
			public byte ONE;

			// Token: 0x04001995 RID: 6549
			public byte TWO;

			// Token: 0x04001996 RID: 6550
			public byte THREE;

			// Token: 0x04001997 RID: 6551
			public byte FOUR;

			// Token: 0x04001998 RID: 6552
			public byte FIVE;

			// Token: 0x04001999 RID: 6553
			public byte SIX;

			// Token: 0x0400199A RID: 6554
			public byte SEVEN;

			// Token: 0x0400199B RID: 6555
			public byte EIGHT;

			// Token: 0x0400199C RID: 6556
			public byte NINE;

			// Token: 0x0400199D RID: 6557
			public byte SLASH;

			// Token: 0x0400199E RID: 6558
			public byte MINUS;

			// Token: 0x0400199F RID: 6559
			public byte C;

			// Token: 0x040019A0 RID: 6560
			public byte c;

			// Token: 0x040019A1 RID: 6561
			public byte R;

			// Token: 0x040019A2 RID: 6562
			public byte r;

			// Token: 0x040019A3 RID: 6563
			public byte D;

			// Token: 0x040019A4 RID: 6564
			public byte d;

			// Token: 0x040019A5 RID: 6565
			public byte ASTERISK;

			// Token: 0x040019A6 RID: 6566
			public byte E;

			// Token: 0x040019A7 RID: 6567
			public byte e;

			// Token: 0x040019A8 RID: 6568
			public byte PLUS;

			// Token: 0x040019A9 RID: 6569
			public byte SPACE;

			// Token: 0x040019AA RID: 6570
			public byte DBCS_SPACE_Byte0;

			// Token: 0x040019AB RID: 6571
			public byte DBCS_SPACE_Byte1;

			// Token: 0x040019AC RID: 6572
			public byte DBCS_NULL_Byte0;

			// Token: 0x040019AD RID: 6573
			public byte DBCS_NULL_Byte1;
		}

		// Token: 0x020004CF RID: 1231
		private struct decimalLayout
		{
			// Token: 0x040019AE RID: 6574
			public short reserved;

			// Token: 0x040019AF RID: 6575
			public byte scale;

			// Token: 0x040019B0 RID: 6576
			public byte sign;

			// Token: 0x040019B1 RID: 6577
			public int hi32;

			// Token: 0x040019B2 RID: 6578
			public long lo64;
		}

		// Token: 0x020004D0 RID: 1232
		[StructLayout(LayoutKind.Explicit)]
		private struct DecimalStruct
		{
			// Token: 0x040019B3 RID: 6579
			[FieldOffset(0)]
			public long Lo64;

			// Token: 0x040019B4 RID: 6580
			[FieldOffset(8)]
			public int Hi32;

			// Token: 0x040019B5 RID: 6581
			[FieldOffset(12)]
			public int wReserved;

			// Token: 0x040019B6 RID: 6582
			[FieldOffset(14)]
			public byte scale;

			// Token: 0x040019B7 RID: 6583
			[FieldOffset(15)]
			public byte sign;

			// Token: 0x040019B8 RID: 6584
			[FixedBuffer(typeof(int), 4)]
			[FieldOffset(0)]
			public BasePrimitiveConverter.DecimalStruct.<decIntValues>e__FixedBuffer decIntValues;

			// Token: 0x040019B9 RID: 6585
			[FixedBuffer(typeof(byte), 12)]
			[FieldOffset(0)]
			public BasePrimitiveConverter.DecimalStruct.<decByteValues>e__FixedBuffer decByteValues;

			// Token: 0x020004D1 RID: 1233
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 16)]
			public struct <decIntValues>e__FixedBuffer
			{
				// Token: 0x040019BA RID: 6586
				public int FixedElementField;
			}

			// Token: 0x020004D2 RID: 1234
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 12)]
			public struct <decByteValues>e__FixedBuffer
			{
				// Token: 0x040019BB RID: 6587
				public byte FixedElementField;
			}
		}

		// Token: 0x020004D3 RID: 1235
		[StructLayout(LayoutKind.Explicit)]
		private struct BigUnion
		{
			// Token: 0x040019BC RID: 6588
			[FieldOffset(0)]
			public decimal decVal;

			// Token: 0x040019BD RID: 6589
			[FieldOffset(0)]
			public BasePrimitiveConverter.decimalLayout decStructVal;

			// Token: 0x040019BE RID: 6590
			[FixedBuffer(typeof(byte), 16)]
			[FieldOffset(0)]
			public BasePrimitiveConverter.BigUnion.<byteArrayVal>e__FixedBuffer byteArrayVal;

			// Token: 0x040019BF RID: 6591
			[FieldOffset(8)]
			public long int64Val;

			// Token: 0x040019C0 RID: 6592
			[FieldOffset(8)]
			public double dblVal;

			// Token: 0x040019C1 RID: 6593
			[FieldOffset(8)]
			public float fltVal;

			// Token: 0x040019C2 RID: 6594
			[FieldOffset(8)]
			public int int32Val;

			// Token: 0x040019C3 RID: 6595
			[FieldOffset(8)]
			public short int16Val;

			// Token: 0x040019C4 RID: 6596
			[FixedBuffer(typeof(byte), 8)]
			[FieldOffset(8)]
			public BasePrimitiveConverter.BigUnion.<byteVal>e__FixedBuffer byteVal;

			// Token: 0x040019C5 RID: 6597
			[FieldOffset(8)]
			public byte bVal;

			// Token: 0x020004D4 RID: 1236
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 16)]
			public struct <byteArrayVal>e__FixedBuffer
			{
				// Token: 0x040019C6 RID: 6598
				public byte FixedElementField;
			}

			// Token: 0x020004D5 RID: 1237
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 8)]
			public struct <byteVal>e__FixedBuffer
			{
				// Token: 0x040019C7 RID: 6599
				public byte FixedElementField;
			}
		}

		// Token: 0x020004D6 RID: 1238
		[StructLayout(LayoutKind.Explicit)]
		private struct Int64Union
		{
			// Token: 0x040019C8 RID: 6600
			[FixedBuffer(typeof(byte), 8)]
			[FieldOffset(0)]
			public BasePrimitiveConverter.Int64Union.<byteVal>e__FixedBuffer byteVal;

			// Token: 0x040019C9 RID: 6601
			[FieldOffset(0)]
			public long int64Val;

			// Token: 0x040019CA RID: 6602
			[FieldOffset(0)]
			public ulong uint64Val;

			// Token: 0x040019CB RID: 6603
			[FieldOffset(0)]
			public double dblVal;

			// Token: 0x040019CC RID: 6604
			[FieldOffset(0)]
			public float fltVal;

			// Token: 0x040019CD RID: 6605
			[FieldOffset(0)]
			public int int32Val;

			// Token: 0x040019CE RID: 6606
			[FieldOffset(0)]
			public uint uint32Val;

			// Token: 0x040019CF RID: 6607
			[FieldOffset(0)]
			public short int16Val;

			// Token: 0x040019D0 RID: 6608
			[FieldOffset(0)]
			public ushort uint16Val;

			// Token: 0x020004D7 RID: 1239
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 8)]
			public struct <byteVal>e__FixedBuffer
			{
				// Token: 0x040019D1 RID: 6609
				public byte FixedElementField;
			}
		}

		// Token: 0x020004D8 RID: 1240
		[StructLayout(LayoutKind.Explicit)]
		private struct ExpUnion
		{
			// Token: 0x040019D2 RID: 6610
			[FieldOffset(0)]
			public long int64Val;

			// Token: 0x040019D3 RID: 6611
			[FieldOffset(6)]
			public short int16Val;

			// Token: 0x040019D4 RID: 6612
			[FieldOffset(6)]
			public ushort uint16Val;
		}

		// Token: 0x020004D9 RID: 1241
		[StructLayout(LayoutKind.Explicit)]
		private struct Int32Union
		{
			// Token: 0x040019D5 RID: 6613
			[FixedBuffer(typeof(byte), 4)]
			[FieldOffset(0)]
			public BasePrimitiveConverter.Int32Union.<byteVal>e__FixedBuffer byteVal;

			// Token: 0x040019D6 RID: 6614
			[FieldOffset(0)]
			public float fltVal;

			// Token: 0x040019D7 RID: 6615
			[FieldOffset(0)]
			public int int32Val;

			// Token: 0x040019D8 RID: 6616
			[FieldOffset(0)]
			public uint uint32Val;

			// Token: 0x040019D9 RID: 6617
			[FieldOffset(0)]
			public short int16Val;

			// Token: 0x040019DA RID: 6618
			[FieldOffset(0)]
			public ushort uint16Val;

			// Token: 0x020004DA RID: 1242
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 4)]
			public struct <byteVal>e__FixedBuffer
			{
				// Token: 0x040019DB RID: 6619
				public byte FixedElementField;
			}
		}

		// Token: 0x020004DB RID: 1243
		[StructLayout(LayoutKind.Explicit)]
		private struct Int16Union
		{
			// Token: 0x040019DC RID: 6620
			[FieldOffset(0)]
			public short int16Val;

			// Token: 0x040019DD RID: 6621
			[FieldOffset(0)]
			public ushort uint16Val;

			// Token: 0x040019DE RID: 6622
			[FixedBuffer(typeof(byte), 2)]
			[FieldOffset(0)]
			public BasePrimitiveConverter.Int16Union.<byteVal>e__FixedBuffer byteVal;

			// Token: 0x020004DC RID: 1244
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 2)]
			public struct <byteVal>e__FixedBuffer
			{
				// Token: 0x040019DF RID: 6623
				public byte FixedElementField;
			}
		}

		// Token: 0x020004DD RID: 1245
		private class EditPattern
		{
			// Token: 0x040019E0 RID: 6624
			public int YearDigits;

			// Token: 0x040019E1 RID: 6625
			public int DayDigits;

			// Token: 0x040019E2 RID: 6626
			public int MonthDigits;

			// Token: 0x040019E3 RID: 6627
			public int HourDigits;

			// Token: 0x040019E4 RID: 6628
			public int MinuteDigits;

			// Token: 0x040019E5 RID: 6629
			public int SecondDigits;

			// Token: 0x040019E6 RID: 6630
			public int FractionalSecondDigits;
		}
	}
}
