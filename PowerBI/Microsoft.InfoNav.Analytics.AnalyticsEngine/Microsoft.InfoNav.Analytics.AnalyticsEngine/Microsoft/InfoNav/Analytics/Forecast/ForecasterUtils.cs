using System;
using Microsoft.InfoNav.Analytics.TimeSeriesPreProcessing;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x0200002F RID: 47
	internal static class ForecasterUtils
	{
		// Token: 0x060000BD RID: 189 RVA: 0x0000548C File Offset: 0x0000368C
		internal static TransformException CreateNotEnoughDataException()
		{
			string text = ForecastErrorType.DataIsTooSmall.ToErrorCode();
			return new TransformException("Not enough data in input", text, ErrorSource.User);
		}
	}
}
