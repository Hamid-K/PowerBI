using System;
using System.Globalization;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200065F RID: 1631
	internal class Formatter
	{
		// Token: 0x06005A99 RID: 23193 RVA: 0x00174B05 File Offset: 0x00172D05
		internal Formatter(Microsoft.ReportingServices.ReportIntermediateFormat.Style styleClass, OnDemandProcessingContext context, ObjectType objectType, string objectName)
		{
			this.m_context = context;
			this.m_styleClass = styleClass;
			this.m_objectType = objectType;
			this.m_objectName = objectName;
		}

		// Token: 0x06005A9A RID: 23194 RVA: 0x00174B2C File Offset: 0x00172D2C
		internal static string FormatWithClientCulture(object value)
		{
			bool flag;
			return Formatter.FormatWithSpecificCulture(value, Localization.ClientPrimaryCulture, out flag);
		}

		// Token: 0x06005A9B RID: 23195 RVA: 0x00174B48 File Offset: 0x00172D48
		internal static string FormatWithInvariantCulture(object value)
		{
			bool flag;
			return Formatter.FormatWithInvariantCulture(value, out flag);
		}

		// Token: 0x06005A9C RID: 23196 RVA: 0x00174B5D File Offset: 0x00172D5D
		internal static string FormatWithInvariantCulture(object value, out bool errorOccurred)
		{
			return Formatter.FormatWithSpecificCulture(value, CultureInfo.InvariantCulture, out errorOccurred);
		}

		// Token: 0x06005A9D RID: 23197 RVA: 0x00174B6C File Offset: 0x00172D6C
		internal static string FormatWithSpecificCulture(object value, CultureInfo culture, out bool errorOccurred)
		{
			errorOccurred = false;
			if (value == null)
			{
				return null;
			}
			string text = null;
			if (value is IFormattable)
			{
				try
				{
					return ((IFormattable)value).ToString(null, culture);
				}
				catch (Exception)
				{
					errorOccurred = true;
					return text;
				}
			}
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = culture;
			try
			{
				text = value.ToString();
			}
			catch (Exception)
			{
				errorOccurred = true;
			}
			finally
			{
				Thread.CurrentThread.CurrentCulture = currentCulture;
			}
			return text;
		}

		// Token: 0x06005A9E RID: 23198 RVA: 0x00174BFC File Offset: 0x00172DFC
		internal static string Format(object value, ref Formatter formatter, Microsoft.ReportingServices.ReportIntermediateFormat.Style reportItemStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Style reportElementStyle, OnDemandProcessingContext context, ObjectType objectType, string objectName)
		{
			if (formatter == null)
			{
				formatter = new Formatter(reportItemStyle, context, objectType, objectName);
			}
			TypeCode typeCode = Type.GetTypeCode(value.GetType());
			bool flag = false;
			string text = "";
			if (reportElementStyle != null)
			{
				reportElementStyle.GetStyleAttribute(objectType, objectName, "Format", context, ref flag, out text);
			}
			return formatter.FormatValue(value, text, typeCode);
		}

		// Token: 0x06005A9F RID: 23199 RVA: 0x00174C52 File Offset: 0x00172E52
		internal string FormatValue(object value, TypeCode typeCode)
		{
			return this.FormatValue(value, null, typeCode);
		}

		// Token: 0x06005AA0 RID: 23200 RVA: 0x00174C5D File Offset: 0x00172E5D
		internal string FormatValue(object value, string formatString, TypeCode typeCode)
		{
			return this.FormatValue(value, formatString, typeCode, false);
		}

		// Token: 0x06005AA1 RID: 23201 RVA: 0x00174C6C File Offset: 0x00172E6C
		internal string FormatValue(object value, string formatString, TypeCode typeCode, bool addDateTimeOffsetSuffix)
		{
			CultureInfo cultureInfo = null;
			string text = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num = 0;
			bool flag4 = false;
			string text2 = null;
			Calendar calendar = null;
			bool flag5 = false;
			try
			{
				if (this.m_styleClass != null)
				{
					if (formatString == null)
					{
						this.m_styleClass.GetStyleAttribute(this.m_objectType, this.m_objectName, "Format", this.m_context, ref this.m_sharedFormatSettings, out formatString);
					}
					num = this.m_styleClass.GetStyleAttribute(this.m_objectType, this.m_objectName, "Language", this.m_context, ref this.m_sharedFormatSettings, out text);
					if (!this.GetCulture(text, ref cultureInfo, ref flag2, ref num))
					{
						text2 = RPRes.rsExpressionErrorValue;
						flag5 = true;
					}
					if (!flag5 && typeCode == TypeCode.DateTime && !this.m_calendarValidated)
					{
						this.CreateAndValidateCalendar(num, cultureInfo);
					}
				}
				if (!flag5 && cultureInfo != null && this.m_formattingCalendar != null)
				{
					if (flag2)
					{
						if (cultureInfo.DateTimeFormat.IsReadOnly)
						{
							cultureInfo = (CultureInfo)cultureInfo.Clone();
							flag3 = true;
						}
						else
						{
							calendar = cultureInfo.DateTimeFormat.Calendar;
						}
					}
					cultureInfo.DateTimeFormat.Calendar = this.m_formattingCalendar;
				}
				if (!flag5 && formatString != null && value is IFormattable)
				{
					try
					{
						if (cultureInfo == null)
						{
							cultureInfo = Thread.CurrentThread.CurrentCulture;
							flag2 = true;
						}
						if (ReportProcessing.CompareWithInvariantCulture(formatString, "x", true) == 0)
						{
							flag4 = true;
						}
						text2 = ((IFormattable)value).ToString(formatString, cultureInfo);
						if (addDateTimeOffsetSuffix)
						{
							text2 += " +0".ToString(cultureInfo);
						}
					}
					catch (Exception ex)
					{
						text2 = RPRes.rsExpressionErrorValue;
						this.m_context.ErrorContext.Register(ProcessingErrorCode.rsInvalidFormatString, Severity.Warning, this.m_objectType, this.m_objectName, "Format", new string[] { ex.Message });
					}
					flag5 = true;
				}
				if (!flag5)
				{
					CultureInfo cultureInfo2 = null;
					if ((!flag2 && cultureInfo != null) || flag3)
					{
						cultureInfo2 = Thread.CurrentThread.CurrentCulture;
						Thread.CurrentThread.CurrentCulture = cultureInfo;
						try
						{
							text2 = value.ToString();
							goto IL_023E;
						}
						finally
						{
							if (cultureInfo2 != null)
							{
								Thread.CurrentThread.CurrentCulture = cultureInfo2;
							}
						}
					}
					text2 = value.ToString();
				}
			}
			finally
			{
				if (flag2 && calendar != null)
				{
					Global.Tracer.Assert(!Thread.CurrentThread.CurrentCulture.DateTimeFormat.IsReadOnly, "(!System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.IsReadOnly)");
					Thread.CurrentThread.CurrentCulture.DateTimeFormat.Calendar = calendar;
				}
			}
			IL_023E:
			if (!flag4 && this.m_styleClass != null)
			{
				if (typeCode - TypeCode.SByte <= 10)
				{
					flag = true;
				}
				if (flag)
				{
					int num2 = 1;
					this.m_styleClass.GetStyleAttribute(this.m_objectType, this.m_objectName, "NumeralVariant", this.m_context, ref this.m_sharedFormatSettings, out num2);
					if (num2 > 2)
					{
						CultureInfo cultureInfo3 = cultureInfo;
						if (cultureInfo3 == null)
						{
							cultureInfo3 = Thread.CurrentThread.CurrentCulture;
						}
						string numberDecimalSeparator = cultureInfo3.NumberFormat.NumberDecimalSeparator;
						this.m_styleClass.GetStyleAttribute(this.m_objectType, this.m_objectName, "NumeralLanguage", this.m_context, ref this.m_sharedFormatSettings, out text);
						if (text != null)
						{
							cultureInfo = new CultureInfo(text, false);
						}
						else if (cultureInfo == null)
						{
							cultureInfo = cultureInfo3;
						}
						bool flag6 = true;
						text2 = FormatDigitReplacement.FormatNumeralVariant(text2, num2, cultureInfo, numberDecimalSeparator, out flag6);
						if (!flag6)
						{
							this.m_context.ErrorContext.Register(ProcessingErrorCode.rsInvalidNumeralVariantForLanguage, Severity.Warning, this.m_objectType, this.m_objectName, "NumeralVariant", new string[]
							{
								num2.ToString(CultureInfo.InvariantCulture),
								cultureInfo.Name
							});
						}
					}
				}
			}
			return text2;
		}

		// Token: 0x06005AA2 RID: 23202 RVA: 0x00175018 File Offset: 0x00173218
		internal CultureInfo GetCulture(string language)
		{
			bool flag = false;
			int num = 0;
			CultureInfo cultureInfo = null;
			if (this.GetCulture(language, ref cultureInfo, ref flag, ref num))
			{
				return cultureInfo;
			}
			return Thread.CurrentThread.CurrentCulture;
		}

		// Token: 0x06005AA3 RID: 23203 RVA: 0x00175048 File Offset: 0x00173248
		private bool GetCulture(string language, ref CultureInfo formattingCulture, ref bool isThreadCulture, ref int languageState)
		{
			if (language != null)
			{
				try
				{
					formattingCulture = new CultureInfo(language, false);
					if (formattingCulture.IsNeutralCulture)
					{
						formattingCulture = CultureInfo.CreateSpecificCulture(language);
						formattingCulture = new CultureInfo(formattingCulture.Name, false);
					}
					return true;
				}
				catch (Exception)
				{
					this.m_context.ErrorContext.Register(ProcessingErrorCode.rsInvalidLanguage, Severity.Warning, this.m_objectType, this.m_objectName, "Language", new string[] { language });
					return false;
				}
			}
			languageState = 0;
			isThreadCulture = true;
			formattingCulture = Thread.CurrentThread.CurrentCulture;
			if (this.m_context.LanguageInstanceId != this.m_languageInstanceId)
			{
				this.m_calendarValidated = false;
				this.m_formattingCalendar = null;
				this.m_languageInstanceId = this.m_context.LanguageInstanceId;
			}
			return true;
		}

		// Token: 0x06005AA4 RID: 23204 RVA: 0x00175114 File Offset: 0x00173314
		private void CreateAndValidateCalendar(int languageState, CultureInfo formattingCulture)
		{
			AttributeInfo attributeInfo = null;
			Calendars calendars = Calendars.Default;
			bool flag = false;
			if (this.m_styleClass.GetAttributeInfo("Calendar", out attributeInfo))
			{
				if (attributeInfo.IsExpression)
				{
					flag = true;
					calendars = (Calendars)this.m_styleClass.EvaluateStyle(this.m_objectType, this.m_objectName, attributeInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.Calendar, this.m_context);
					this.m_sharedFormatSettings = false;
				}
				else
				{
					calendars = StyleTranslator.TranslateCalendar(attributeInfo.Value, this.m_context.ReportRuntime);
					if (languageState != 0)
					{
						if (languageState == 1)
						{
							flag = true;
						}
						else if (!this.m_calendarValidated)
						{
							this.m_calendarValidated = true;
							this.m_formattingCalendar = ProcessingValidator.CreateCalendar(calendars);
							return;
						}
					}
				}
			}
			if (flag || !this.m_calendarValidated)
			{
				if (calendars != Calendars.Default && ProcessingValidator.ValidateCalendar(formattingCulture, calendars, this.m_objectType, this.m_objectName, "Calendar", this.m_context.ErrorContext))
				{
					this.m_formattingCalendar = ProcessingValidator.CreateCalendar(calendars);
				}
				if (!flag)
				{
					this.m_calendarValidated = true;
				}
			}
		}

		// Token: 0x04002F1F RID: 12063
		private Microsoft.ReportingServices.ReportIntermediateFormat.Style m_styleClass;

		// Token: 0x04002F20 RID: 12064
		private OnDemandProcessingContext m_context;

		// Token: 0x04002F21 RID: 12065
		private bool m_sharedFormatSettings;

		// Token: 0x04002F22 RID: 12066
		private bool m_calendarValidated;

		// Token: 0x04002F23 RID: 12067
		private uint m_languageInstanceId;

		// Token: 0x04002F24 RID: 12068
		private ObjectType m_objectType;

		// Token: 0x04002F25 RID: 12069
		private string m_objectName;

		// Token: 0x04002F26 RID: 12070
		private Calendar m_formattingCalendar;
	}
}
