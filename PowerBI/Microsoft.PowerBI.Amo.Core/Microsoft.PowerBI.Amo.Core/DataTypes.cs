using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200003A RID: 58
	internal static class DataTypes
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000CED6 File Offset: 0x0000B0D6
		public static bool IsSupportedDataType(string dataType)
		{
			return string.Compare(dataType, "text/xml", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(dataType, "application/xml+xpress", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(dataType, "application/sx", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(dataType, "application/sx+xpress", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000CF14 File Offset: 0x0000B114
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

		// Token: 0x06000298 RID: 664 RVA: 0x0000CF64 File Offset: 0x0000B164
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

		// Token: 0x040001FB RID: 507
		public const string TextXml = "text/xml";

		// Token: 0x040001FC RID: 508
		public const string BinaryXml = "application/sx";

		// Token: 0x040001FD RID: 509
		public const string CompressedXml = "application/xml+xpress";

		// Token: 0x040001FE RID: 510
		public const string CompressedBinaryXml = "application/sx+xpress";
	}
}
