using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000022 RID: 34
	internal static class DataTypes
	{
		// Token: 0x0600020A RID: 522 RVA: 0x00009FDA File Offset: 0x000081DA
		public static bool IsSupportedDataType(string dataType)
		{
			return string.Compare(dataType, "text/xml", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(dataType, "application/xml+xpress", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(dataType, "application/sx", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(dataType, "application/sx+xpress", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000A018 File Offset: 0x00008218
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

		// Token: 0x0600020C RID: 524 RVA: 0x0000A068 File Offset: 0x00008268
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

		// Token: 0x040001B6 RID: 438
		public const string TextXml = "text/xml";

		// Token: 0x040001B7 RID: 439
		public const string BinaryXml = "application/sx";

		// Token: 0x040001B8 RID: 440
		public const string CompressedXml = "application/xml+xpress";

		// Token: 0x040001B9 RID: 441
		public const string CompressedBinaryXml = "application/sx+xpress";
	}
}
