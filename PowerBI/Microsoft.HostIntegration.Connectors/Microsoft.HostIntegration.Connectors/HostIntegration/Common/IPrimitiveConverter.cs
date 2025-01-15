using System;
using Microsoft.HostIntegration.Tracing.Common;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x02000508 RID: 1288
	public interface IPrimitiveConverter
	{
		// Token: 0x06002B43 RID: 11075
		void SetTracingAndLogging(object Logging);

		// Token: 0x06002B44 RID: 11076
		void SetDontUsePresentationFormsB(bool dontUsePresentationFormsB);

		// Token: 0x06002B45 RID: 11077
		void SetCodePage(int CodePage);

		// Token: 0x06002B46 RID: 11078
		void SetLCID(int InLCID);

		// Token: 0x06002B47 RID: 11079
		void SetSepValues(char wcTimeSep, char wcDateSep);

		// Token: 0x06002B48 RID: 11080
		void SizeOfRemoteType(DataType WindowsDataType, int conversionEncoding, out int ConvertedDataLength);

		// Token: 0x06002B49 RID: 11081
		void SizeOfRemoteType(DataType WindowsDataType, CEDAR_TYPE_ENCODING encoding, out int ConvertedDataLength);

		// Token: 0x06002B4A RID: 11082
		void PackInt16(short Int16Value, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B4B RID: 11083
		void UnpackInt16(ref byte Buffer, ref short ReturnedInt16, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B4C RID: 11084
		void PackInt32(int Int32Value, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B4D RID: 11085
		void UnpackInt32(ref byte Buffer, ref int ReturnedInt32, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B4E RID: 11086
		void PackInt64(long Int64Value, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B4F RID: 11087
		void UnpackInt64(ref byte Buffer, ref long ReturnedInt64, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B50 RID: 11088
		void PackFloat(float FloatValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B51 RID: 11089
		void UnpackFloat(ref byte Buffer, ref float ReturnedFloat, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B52 RID: 11090
		void PackDouble(double DoubleValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B53 RID: 11091
		void UnpackDouble(ref byte Buffer, ref double ReturnedDouble, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B54 RID: 11092
		void PackBool(bool BoolValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B55 RID: 11093
		void UnpackBool(ref byte Buffer, ref bool ReturnedBool, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B56 RID: 11094
		void PackByte(byte ByteValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B57 RID: 11095
		void UnpackByte(ref byte Buffer, ref byte ReturnedByte, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B58 RID: 11096
		void PackDecimal(decimal DecimalValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B59 RID: 11097
		void UnpackDecimal(ref byte Buffer, ref decimal ReturnedDecimal, ref int remainingBufferLength, int ulResultLen, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B5A RID: 11098
		void PackTime(TimeSpan TimeSpanValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding, char TimeSeparator, int CodePage);

		// Token: 0x06002B5B RID: 11099
		void PackTime(TimeSpan TimeSpanValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding, char TimeSeparator);

		// Token: 0x06002B5C RID: 11100
		void PackTime(TimeSpan TimeSpanValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding, int CodePage);

		// Token: 0x06002B5D RID: 11101
		void PackTime(TimeSpan TimeSpanValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B5E RID: 11102
		void UnpackTime(ref byte Buffer, ref TimeSpan ReturnedTimeSpan, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B5F RID: 11103
		void UnpackTime(ref byte Buffer, ref TimeSpan ReturnedTimeSpan, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding, int CodePage);

		// Token: 0x06002B60 RID: 11104
		void PackDate(DateTime DateTimeValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding, char TimeSeparator, char DateSeparator, int CodePage);

		// Token: 0x06002B61 RID: 11105
		void PackDate(DateTime DateTimeValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding, char TimeSeparator, char DateSeparator);

		// Token: 0x06002B62 RID: 11106
		void PackDate(DateTime DateTimeValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding, int CodePage);

		// Token: 0x06002B63 RID: 11107
		void PackDate(DateTime DateTimeValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B64 RID: 11108
		void UnpackDate(ref byte Buffer, ref DateTime ReturnedDateTime, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B65 RID: 11109
		void UnpackDate(ref byte Buffer, ref DateTime ReturnedDateTime, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding, int CodePage);

		// Token: 0x06002B66 RID: 11110
		void PackEditedTimeSpan(TimeSpan TimeSpanValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, string editPattern);

		// Token: 0x06002B67 RID: 11111
		void PackEditedTimeSpan(TimeSpan TimeSpanValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, string editPattern, int CodePage);

		// Token: 0x06002B68 RID: 11112
		bool UnpackEditedTimeSpan(ref byte Buffer, out TimeSpan ReturnedTimeSpan, ref int remainingBufferLength, int inputDataLength, string editPattern);

		// Token: 0x06002B69 RID: 11113
		bool UnpackEditedTimeSpan(ref byte Buffer, out TimeSpan ReturnedTimeSpan, ref int remainingBufferLength, int inputDataLength, string editPattern, int CodePage);

		// Token: 0x06002B6A RID: 11114
		bool UnpackEditedTimeSpan(ref byte Buffer, out TimeSpan ReturnedTimeSpan, ref int remainingBufferLength, int inputDataLength, string editPattern, bool allowTruncatedFractionalSeconds);

		// Token: 0x06002B6B RID: 11115
		bool UnpackEditedTimeSpan(ref byte Buffer, out TimeSpan ReturnedTimeSpan, ref int remainingBufferLength, int inputDataLength, string editPattern, int CodePage, bool allowTruncatedFractionalSeconds);

		// Token: 0x06002B6C RID: 11116
		bool UnpackEditedTimeSpan(string InputTimeSpan, out TimeSpan ReturnedTimeSpan, string editPattern, bool allowTruncatedFractionalSeconds);

		// Token: 0x06002B6D RID: 11117
		bool UnpackEditedTimeSpan(string InputTimeSpan, out TimeSpan ReturnedTimeSpan, string editPattern);

		// Token: 0x06002B6E RID: 11118
		void PackEditedDateTime(DateTime DateTimeValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, string editPattern);

		// Token: 0x06002B6F RID: 11119
		void PackEditedDateTime(DateTime DateTimeValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, string editPattern, int CodePage);

		// Token: 0x06002B70 RID: 11120
		bool UnpackEditedDateTime(ref byte Buffer, out DateTime ReturnedDateTime, ref int remainingBufferLength, int inputDataLength, string editPattern);

		// Token: 0x06002B71 RID: 11121
		bool UnpackEditedDateTime(ref byte Buffer, out DateTime ReturnedDateTime, ref int remainingBufferLength, int inputDataLength, string editPattern, int CodePage);

		// Token: 0x06002B72 RID: 11122
		bool UnpackEditedDateTime(ref byte Buffer, out DateTime ReturnedDateTime, ref int remainingBufferLength, int inputDataLength, string editPattern, bool allowTruncatedFractionalSeconds);

		// Token: 0x06002B73 RID: 11123
		bool UnpackEditedDateTime(ref byte Buffer, out DateTime ReturnedDateTime, ref int remainingBufferLength, int inputDataLength, string editPattern, int CodePage, bool allowTruncatedFractionalSeconds);

		// Token: 0x06002B74 RID: 11124
		bool UnpackEditedDateTime(string InputDate, out DateTime ReturnedDateTime, string editPattern, bool allowTruncatedFractionalSeconds);

		// Token: 0x06002B75 RID: 11125
		bool UnpackEditedDateTime(string InputDate, out DateTime ReturnedDateTime, string editPattern);

		// Token: 0x06002B76 RID: 11126
		int PackString(string StringValue, ref byte Buffer, ref int cumulativePackedDataLength, int clCharCount, bool fDataIsVariable, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B77 RID: 11127
		int PackString(string StringValue, ref byte Buffer, ref int cumulativePackedDataLength, int clCharCount, bool fDataIsVariable, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding, int CodePage);

		// Token: 0x06002B78 RID: 11128
		int UnpackString(ref byte Buffer, ref string ReturnedString, ref int remainingBufferLength, int maxCharCount, bool fDataIsVariable, int inputDataLength, CEDAR_TYPE_ENCODING encoding);

		// Token: 0x06002B79 RID: 11129
		int UnpackString(ref byte Buffer, ref string ReturnedString, ref int remainingBufferLength, int maxCharCount, bool fDataIsVariable, int inputDataLength, CEDAR_TYPE_ENCODING encoding, int CodePage);

		// Token: 0x06002B7A RID: 11130
		void PackInt16(short Int16Value, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine);

		// Token: 0x06002B7B RID: 11131
		void UnpackInt16(ref byte Buffer, ref short ReturnedInt16, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine);

		// Token: 0x06002B7C RID: 11132
		void PackInt32(int Int32Value, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine);

		// Token: 0x06002B7D RID: 11133
		void UnpackInt32(ref byte Buffer, ref int ReturnedInt32, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine);

		// Token: 0x06002B7E RID: 11134
		void PackInt64(long Int64Value, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine);

		// Token: 0x06002B7F RID: 11135
		void UnpackInt64(ref byte Buffer, ref long ReturnedInt64, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine);

		// Token: 0x06002B80 RID: 11136
		void PackDecimal(decimal DecimalValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine);

		// Token: 0x06002B81 RID: 11137
		void UnpackDecimal(ref byte Buffer, ref decimal ReturnedDecimal, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine);

		// Token: 0x06002B82 RID: 11138
		void PackFloat(float FloatValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine);

		// Token: 0x06002B83 RID: 11139
		void UnpackFloat(ref byte Buffer, ref float ReturnedFloat, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine);

		// Token: 0x06002B84 RID: 11140
		void PackDouble(double DoubleValue, ref byte Buffer, ref int cumulativePackedDataLength, int maxConversionResultLength, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine);

		// Token: 0x06002B85 RID: 11141
		void UnpackDouble(ref byte Buffer, ref double ReturnedDouble, ref int remainingBufferLength, int inputDataLength, CEDAR_TYPE_ENCODING encoding, string NumericEditStatemachine);

		// Token: 0x06002B86 RID: 11142
		bool VerifyMask(int hostLanguageType, CEDAR_TYPE_ENCODING encoding, string EditMask, out string OutStateMach, out int NumericItemLength, int CurrencySymbolLength);

		// Token: 0x06002B87 RID: 11143
		bool VerifyEditedDateTimeMask(string EditMask, bool toHost);

		// Token: 0x06002B88 RID: 11144
		bool VerifyEditedTimeSpanMask(string EditMask, bool toHost);

		// Token: 0x06002B89 RID: 11145
		void SetCodePage(int CodePage, bool IsPeriodComma, string Currency);

		// Token: 0x06002B8A RID: 11146
		void ExtractPrecisionAndScale(out short ManPrecision, out short ManScale, out short ExpPrecision, string EditStateMachine);

		// Token: 0x06002B8B RID: 11147
		bool IsSignedMask(int hostLanguageType, string EditMask);

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002B8C RID: 11148
		// (set) Token: 0x06002B8D RID: 11149
		PrimitiveConverterTracePoint TracePoint { get; set; }

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002B8E RID: 11150
		// (set) Token: 0x06002B8F RID: 11151
		bool UserCompatibleErrorCode { get; set; }
	}
}
