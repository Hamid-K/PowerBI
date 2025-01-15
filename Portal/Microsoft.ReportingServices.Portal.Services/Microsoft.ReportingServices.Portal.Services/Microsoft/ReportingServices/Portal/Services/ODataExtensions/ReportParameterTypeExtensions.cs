using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x02000049 RID: 73
	internal static class ReportParameterTypeExtensions
	{
		// Token: 0x06000288 RID: 648 RVA: 0x0001121B File Offset: 0x0000F41B
		public static ReportParameterType ToReportParameterType(this DataTypes dataType)
		{
			switch (dataType)
			{
			case DataTypes.String:
				return ReportParameterType.String;
			case DataTypes.Boolean:
				return ReportParameterType.Boolean;
			case DataTypes.DateTime:
				return ReportParameterType.DateTime;
			case DataTypes.Integer:
				return ReportParameterType.Integer;
			case DataTypes.Float:
				return ReportParameterType.Float;
			default:
				throw new ArgumentException("DataType {0} unkown", dataType.ToString());
			}
		}
	}
}
