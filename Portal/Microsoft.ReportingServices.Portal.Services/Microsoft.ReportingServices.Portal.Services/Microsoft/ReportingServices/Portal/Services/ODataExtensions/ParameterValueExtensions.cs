using System;
using System.Globalization;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x02000047 RID: 71
	internal static class ParameterValueExtensions
	{
		// Token: 0x06000275 RID: 629 RVA: 0x00010CEC File Offset: 0x0000EEEC
		public static Microsoft.SqlServer.ReportingServices2010.ParameterValue ToSoapParameterValue(this global::Model.ParameterValue parameterValue, ReportParameterType reportParameterType)
		{
			if (parameterValue == null)
			{
				throw new ArgumentNullException("parameterValue");
			}
			return new Microsoft.SqlServer.ReportingServices2010.ParameterValue
			{
				Name = parameterValue.Name,
				Value = ParameterValueExtensions.ToSoapValue(parameterValue.Value, reportParameterType)
			};
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00010D1F File Offset: 0x0000EF1F
		public static Microsoft.SqlServer.ReportingServices2010.ParameterValue ToSoapParameterValue(this DataSetParameter parameterValue, ReportParameterType reportParameterType)
		{
			if (parameterValue == null)
			{
				throw new ArgumentNullException("parameterValue");
			}
			return new Microsoft.SqlServer.ReportingServices2010.ParameterValue
			{
				Name = parameterValue.Name,
				Value = ParameterValueExtensions.ToSoapValue(parameterValue.Value, reportParameterType)
			};
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00010D54 File Offset: 0x0000EF54
		public static global::Model.ParameterValue ToWebAPIParameterValue(this Microsoft.ReportingServices.Library.Soap.ParameterValueOrFieldReference parameterValue)
		{
			global::Model.ParameterValue parameterValue2 = new global::Model.ParameterValue();
			if (parameterValue is Microsoft.ReportingServices.Library.Soap.ParameterValue)
			{
				Microsoft.ReportingServices.Library.Soap.ParameterValue parameterValue3 = (Microsoft.ReportingServices.Library.Soap.ParameterValue)parameterValue;
				parameterValue2 = new global::Model.ParameterValue
				{
					Name = parameterValue3.Name,
					Value = parameterValue3.Value,
					IsValueFieldReference = false
				};
			}
			else if (parameterValue is Microsoft.ReportingServices.Library.Soap.ParameterFieldReference)
			{
				Microsoft.ReportingServices.Library.Soap.ParameterFieldReference parameterFieldReference = (Microsoft.ReportingServices.Library.Soap.ParameterFieldReference)parameterValue;
				parameterValue2 = new global::Model.ParameterValue
				{
					Name = parameterFieldReference.ParameterName,
					Value = parameterFieldReference.FieldAlias,
					IsValueFieldReference = true
				};
			}
			return parameterValue2;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00010DD4 File Offset: 0x0000EFD4
		public static Microsoft.SqlServer.ReportingServices2010.ParameterValueOrFieldReference ToSoapParameterValueOrFieldReference(this global::Model.ParameterValue parameterValue, ReportParameterType reportParameterType)
		{
			if (parameterValue == null)
			{
				throw new ArgumentNullException("parameterValue");
			}
			if (parameterValue.IsValueFieldReference)
			{
				return new Microsoft.SqlServer.ReportingServices2010.ParameterFieldReference
				{
					ParameterName = parameterValue.Name,
					FieldAlias = parameterValue.Value
				};
			}
			return new Microsoft.SqlServer.ReportingServices2010.ParameterValue
			{
				Name = parameterValue.Name,
				Value = ParameterValueExtensions.ToSoapValue(parameterValue.Value, reportParameterType)
			};
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00010E38 File Offset: 0x0000F038
		public static Microsoft.SqlServer.ReportingServices2010.ParameterValueOrFieldReference ToSoapParameterValueOrFieldReference(this global::Model.ParameterValue parameterValue)
		{
			if (parameterValue.IsValueFieldReference)
			{
				return new Microsoft.SqlServer.ReportingServices2010.ParameterFieldReference
				{
					ParameterName = parameterValue.Name,
					FieldAlias = parameterValue.Value
				};
			}
			return new Microsoft.SqlServer.ReportingServices2010.ParameterValue
			{
				Name = parameterValue.Name,
				Value = parameterValue.Value
			};
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00010E88 File Offset: 0x0000F088
		public static global::Model.ParameterValue ToWebAPIParameterValue(this Microsoft.SqlServer.ReportingServices2010.ParameterValueOrFieldReference parameterValue)
		{
			global::Model.ParameterValue parameterValue2 = new global::Model.ParameterValue();
			if (parameterValue is Microsoft.SqlServer.ReportingServices2010.ParameterValue)
			{
				Microsoft.SqlServer.ReportingServices2010.ParameterValue parameterValue3 = (Microsoft.SqlServer.ReportingServices2010.ParameterValue)parameterValue;
				parameterValue2 = new global::Model.ParameterValue
				{
					Name = parameterValue3.Name,
					Value = parameterValue3.Value,
					IsValueFieldReference = false
				};
			}
			else if (parameterValue is Microsoft.SqlServer.ReportingServices2010.ParameterFieldReference)
			{
				Microsoft.SqlServer.ReportingServices2010.ParameterFieldReference parameterFieldReference = (Microsoft.SqlServer.ReportingServices2010.ParameterFieldReference)parameterValue;
				parameterValue2 = new global::Model.ParameterValue
				{
					Name = parameterFieldReference.ParameterName,
					Value = parameterFieldReference.FieldAlias,
					IsValueFieldReference = true
				};
			}
			return parameterValue2;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00010F06 File Offset: 0x0000F106
		public static Microsoft.ReportingServices.Library.Soap.ParameterValue ToActionParameterValue(this global::Model.ParameterValue parameterValue, ReportParameterType reportParameterType)
		{
			if (parameterValue == null)
			{
				throw new ArgumentNullException("parameterValue");
			}
			return new Microsoft.ReportingServices.Library.Soap.ParameterValue
			{
				Name = parameterValue.Name,
				Value = ParameterValueExtensions.ToSoapValue(parameterValue.Value, reportParameterType)
			};
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00010F39 File Offset: 0x0000F139
		public static global::Model.ParameterValue ToWebApiReportParameterValue(this Microsoft.ReportingServices.Library.Soap.ParameterValue parameterValue, ReportParameterType reportParameterType, string culture)
		{
			if (parameterValue == null)
			{
				throw new ArgumentNullException("parameterValue");
			}
			return new global::Model.ParameterValue
			{
				Name = parameterValue.Name,
				Value = ParameterValueExtensions.ToWebApiValue(parameterValue.Value, reportParameterType, culture),
				IsValueFieldReference = false
			};
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00010F74 File Offset: 0x0000F174
		public static global::Model.ParameterValue ToWebApiReportParameterValue(this Microsoft.ReportingServices.Library.Soap.ParameterFieldReference parameterValue)
		{
			if (parameterValue == null)
			{
				throw new ArgumentNullException("parameterValue");
			}
			return new global::Model.ParameterValue
			{
				Name = parameterValue.ParameterName,
				Value = parameterValue.FieldAlias,
				IsValueFieldReference = true
			};
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00010FA8 File Offset: 0x0000F1A8
		public static global::Model.ParameterValue ToWebApiReportParameterValue(this Microsoft.SqlServer.ReportingServices2010.ParameterValue parameterValue, ReportParameterType reportParameterType)
		{
			if (parameterValue == null)
			{
				throw new ArgumentNullException("parameterValue");
			}
			return new global::Model.ParameterValue
			{
				Name = parameterValue.Name,
				Value = ParameterValueExtensions.ToWebApiValue(parameterValue.Value, reportParameterType, null),
				IsValueFieldReference = false
			};
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00010FE3 File Offset: 0x0000F1E3
		public static string ToWebApiValue(string soapValue, ReportParameterType reportParameterType, string culture = null)
		{
			if (soapValue != null)
			{
				if (reportParameterType == ReportParameterType.DateTime)
				{
					return ParameterValueExtensions.FormatAsISO8601Date(soapValue, culture);
				}
				if (reportParameterType == ReportParameterType.Boolean)
				{
					return soapValue.ToLowerInvariant();
				}
				if (reportParameterType == ReportParameterType.Float)
				{
					return ParameterValueExtensions.FormatAsInvariantFloat(soapValue, culture);
				}
			}
			return soapValue;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0001100B File Offset: 0x0000F20B
		public static string ToSoapValue(string webApiValue, ReportParameterType reportParameterType)
		{
			if (webApiValue != null)
			{
				if (reportParameterType == ReportParameterType.DateTime)
				{
					return ParameterValueExtensions.FormatAsGeneralDateShortTime(webApiValue);
				}
				if (reportParameterType == ReportParameterType.Float)
				{
					return ParameterValueExtensions.FormatAsLocalizedFloat(webApiValue);
				}
			}
			return webApiValue;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00011028 File Offset: 0x0000F228
		public static string FormatAsISO8601Date(string date, string culture = null)
		{
			DateTime dateTime;
			if (culture != null)
			{
				try
				{
					CultureInfo cultureInfo = new CultureInfo(culture);
					dateTime = DateTime.Parse(date, cultureInfo);
					goto IL_0025;
				}
				catch (CultureNotFoundException)
				{
					dateTime = DateTime.Parse(date);
					goto IL_0025;
				}
			}
			dateTime = DateTime.Parse(date);
			IL_0025:
			dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
			return dateTime.ToString("o", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00011084 File Offset: 0x0000F284
		public static string FormatAsGeneralDateShortTime(string Iso8601Date)
		{
			return DateTime.Parse(Iso8601Date).ToString("G");
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000110A4 File Offset: 0x0000F2A4
		public static string FormatAsLocalizedFloat(string invariantFloat)
		{
			string text;
			try
			{
				text = double.Parse(invariantFloat, CultureInfo.InvariantCulture).ToString();
			}
			catch (Exception)
			{
				text = invariantFloat;
			}
			return text;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000110E0 File Offset: 0x0000F2E0
		public static string FormatAsInvariantFloat(string localizedFloat, string culture)
		{
			double num;
			if (culture != null)
			{
				try
				{
					CultureInfo cultureInfo = new CultureInfo(culture);
					num = double.Parse(localizedFloat, cultureInfo);
					goto IL_0025;
				}
				catch (Exception)
				{
					num = double.Parse(localizedFloat);
					goto IL_0025;
				}
			}
			num = double.Parse(localizedFloat);
			IL_0025:
			return num.ToString(CultureInfo.InvariantCulture);
		}
	}
}
