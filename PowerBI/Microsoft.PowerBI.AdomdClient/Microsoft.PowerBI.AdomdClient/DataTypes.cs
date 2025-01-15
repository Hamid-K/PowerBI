using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000022 RID: 34
	internal static class DataTypes
	{
		// Token: 0x060001FD RID: 509 RVA: 0x00009CDA File Offset: 0x00007EDA
		public static bool IsSupportedDataType(string dataType)
		{
			return string.Compare(dataType, "text/xml", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(dataType, "application/xml+xpress", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(dataType, "application/sx", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(dataType, "application/sx+xpress", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009D18 File Offset: 0x00007F18
		public static XmlaDataType GetDataTypeFromString(string dataType)
		{
			if (string.Compare(dataType, "text/xml", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return XmlaDataType.TextXml;
			}
			if (string.Compare(dataType, "application/xml+xpress", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return XmlaDataType.CompressedXml;
			}
			if (string.Compare(dataType, "application/sx", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return XmlaDataType.BinaryXml;
			}
			if (string.Compare(dataType, "application/sx+xpress", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return XmlaDataType.CompressedBinaryXml;
			}
			return XmlaDataType.Unknown;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009D68 File Offset: 0x00007F68
		public static string GetDataTypeFromEnum(XmlaDataType dataType)
		{
			switch (dataType)
			{
			case XmlaDataType.TextXml:
				return "text/xml";
			case XmlaDataType.BinaryXml:
				return "application/sx";
			case XmlaDataType.CompressedXml:
				return "application/xml+xpress";
			case XmlaDataType.CompressedBinaryXml:
				return "application/sx+xpress";
			default:
				throw new ArgumentOutOfRangeException("dataType", XmlaSR.Dime_DataTypeNotSupported(dataType.ToString()));
			}
		}

		// Token: 0x040001A9 RID: 425
		public const string TextXml = "text/xml";

		// Token: 0x040001AA RID: 426
		public const string BinaryXml = "application/sx";

		// Token: 0x040001AB RID: 427
		public const string CompressedXml = "application/xml+xpress";

		// Token: 0x040001AC RID: 428
		public const string CompressedBinaryXml = "application/sx+xpress";
	}
}
