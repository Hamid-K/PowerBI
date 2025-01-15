using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Parsers.Common;
using Microsoft.OData.Core.UriParser.Parsers.UriParsers;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;
using Microsoft.Spatial;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020002C0 RID: 704
	internal sealed class UriPrimitiveTypeParser : IUriLiteralParser
	{
		// Token: 0x0600183E RID: 6206 RVA: 0x000527F2 File Offset: 0x000509F2
		private UriPrimitiveTypeParser()
		{
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x000527FA File Offset: 0x000509FA
		public static UriPrimitiveTypeParser Instance
		{
			get
			{
				return UriPrimitiveTypeParser.singleInstance;
			}
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00052804 File Offset: 0x00050A04
		public object ParseUriStringToType(string text, IEdmTypeReference targetType, out UriLiteralParsingException parsingException)
		{
			object obj;
			if (this.TryUriStringToPrimitive(text, targetType, out obj, out parsingException))
			{
				return obj;
			}
			return null;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00052821 File Offset: 0x00050A21
		internal bool TryParseUriStringToType(string text, IEdmTypeReference targetType, out object targetValue, out UriLiteralParsingException parsingException)
		{
			return this.TryUriStringToPrimitive(text, targetType, out targetValue, out parsingException);
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00052830 File Offset: 0x00050A30
		[SuppressMessage("DataWeb.Usage", "AC0014:DoNotHandleProhibitedExceptionsRule", Justification = "We're calling this correctly")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Complexity is not too high; handling all the cases in one method is preferable to refactoring.")]
		private bool TryUriStringToPrimitive(string text, IEdmTypeReference targetType, out object targetValue, out UriLiteralParsingException exception)
		{
			exception = null;
			bool flag;
			try
			{
				if (targetType.IsNullable && text == "null")
				{
					targetValue = null;
					flag = true;
				}
				else
				{
					IEdmPrimitiveTypeReference edmPrimitiveTypeReference = targetType.AsPrimitiveOrNull();
					if (edmPrimitiveTypeReference == null)
					{
						targetValue = null;
						flag = false;
					}
					else
					{
						EdmPrimitiveTypeKind edmPrimitiveTypeKind = edmPrimitiveTypeReference.PrimitiveKind();
						byte[] array;
						bool flag2 = UriPrimitiveTypeParser.TryUriStringToByteArray(text, out array);
						if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Binary)
						{
							targetValue = array;
							flag = flag2;
						}
						else if (flag2)
						{
							string @string = Encoding.UTF8.GetString(array, 0, array.Length);
							flag = this.TryUriStringToPrimitive(@string, targetType, out targetValue);
						}
						else if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Guid)
						{
							Guid guid;
							bool flag3 = UriUtils.TryUriStringToGuid(text, out guid);
							targetValue = guid;
							flag = flag3;
						}
						else if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Date)
						{
							Date date;
							bool flag4 = UriUtils.TryUriStringToDate(text, out date);
							targetValue = date;
							flag = flag4;
						}
						else if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.DateTimeOffset)
						{
							DateTimeOffset dateTimeOffset;
							bool flag5 = UriUtils.ConvertUriStringToDateTimeOffset(text, out dateTimeOffset);
							targetValue = dateTimeOffset;
							flag = flag5;
						}
						else if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Duration)
						{
							TimeSpan timeSpan;
							bool flag6 = UriPrimitiveTypeParser.TryUriStringToDuration(text, out timeSpan);
							targetValue = timeSpan;
							flag = flag6;
						}
						else if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Geography)
						{
							Geography geography;
							bool flag7 = UriPrimitiveTypeParser.TryUriStringToGeography(text, out geography, out exception);
							targetValue = geography;
							flag = flag7;
						}
						else if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Geometry)
						{
							Geometry geometry;
							bool flag8 = UriPrimitiveTypeParser.TryUriStringToGeometry(text, out geometry, out exception);
							targetValue = geometry;
							flag = flag8;
						}
						else if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.TimeOfDay)
						{
							TimeOfDay timeOfDay;
							bool flag9 = UriUtils.TryUriStringToTimeOfDay(text, out timeOfDay);
							targetValue = timeOfDay;
							flag = flag9;
						}
						else
						{
							bool flag10 = edmPrimitiveTypeKind == EdmPrimitiveTypeKind.String;
							if (flag10 != UriParserHelper.IsUriValueQuoted(text))
							{
								targetValue = null;
								flag = false;
							}
							else
							{
								if (flag10)
								{
									text = UriParserHelper.RemoveQuotes(text);
								}
								try
								{
									switch (edmPrimitiveTypeKind)
									{
									case EdmPrimitiveTypeKind.Boolean:
										targetValue = XmlConvert.ToBoolean(text);
										goto IL_02DF;
									case EdmPrimitiveTypeKind.Byte:
										targetValue = XmlConvert.ToByte(text);
										goto IL_02DF;
									case EdmPrimitiveTypeKind.Decimal:
										UriParserHelper.TryRemoveLiteralSuffix("M", ref text);
										try
										{
											targetValue = XmlConvert.ToDecimal(text);
											goto IL_02DF;
										}
										catch (FormatException)
										{
											decimal num;
											if (decimal.TryParse(text, 167, NumberFormatInfo.InvariantInfo, ref num))
											{
												targetValue = num;
												goto IL_02DF;
											}
											targetValue = 0m;
											return false;
										}
										break;
									case EdmPrimitiveTypeKind.Double:
										UriParserHelper.TryRemoveLiteralSuffix("D", ref text);
										targetValue = XmlConvert.ToDouble(text);
										goto IL_02DF;
									case EdmPrimitiveTypeKind.Int16:
										targetValue = XmlConvert.ToInt16(text);
										goto IL_02DF;
									case EdmPrimitiveTypeKind.Int32:
										targetValue = XmlConvert.ToInt32(text);
										goto IL_02DF;
									case EdmPrimitiveTypeKind.Int64:
										UriParserHelper.TryRemoveLiteralSuffix("L", ref text);
										targetValue = XmlConvert.ToInt64(text);
										goto IL_02DF;
									case EdmPrimitiveTypeKind.SByte:
										targetValue = XmlConvert.ToSByte(text);
										goto IL_02DF;
									case EdmPrimitiveTypeKind.Single:
										UriParserHelper.TryRemoveLiteralSuffix("f", ref text);
										targetValue = XmlConvert.ToSingle(text);
										goto IL_02DF;
									case EdmPrimitiveTypeKind.String:
										targetValue = text;
										goto IL_02DF;
									}
									throw new ODataException(Strings.General_InternalError(InternalErrorCodes.UriPrimitiveTypeParser_TryUriStringToPrimitive));
									IL_02DF:
									flag = true;
								}
								catch (FormatException)
								{
									targetValue = null;
									flag = false;
								}
								catch (OverflowException)
								{
									targetValue = null;
									flag = false;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				exception = new UriLiteralParsingException(string.Format(CultureInfo.InvariantCulture, Strings.UriPrimitiveTypeParsers_FailedToParseTextToPrimitiveValue(text, targetType), new object[] { ex }));
				targetValue = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00052BCC File Offset: 0x00050DCC
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Complexity is not too high; handling all the cases in one method is preferable to refactoring.")]
		private bool TryUriStringToPrimitive(string text, IEdmTypeReference targetType, out object targetValue)
		{
			UriLiteralParsingException ex;
			return this.TryUriStringToPrimitive(text, targetType, out targetValue, out ex);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00052BE4 File Offset: 0x00050DE4
		private static bool TryUriStringToByteArray(string text, out byte[] targetValue)
		{
			if (!UriParserHelper.TryRemoveLiteralPrefix("binary", ref text))
			{
				targetValue = null;
				return false;
			}
			if (!UriParserHelper.TryRemoveQuotes(ref text))
			{
				targetValue = null;
				return false;
			}
			try
			{
				targetValue = Convert.FromBase64String(text);
			}
			catch (FormatException)
			{
				targetValue = null;
				return false;
			}
			return true;
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00052C38 File Offset: 0x00050E38
		private static bool TryUriStringToDuration(string text, out TimeSpan targetValue)
		{
			if (!UriParserHelper.TryRemoveLiteralPrefix("duration", ref text))
			{
				targetValue = default(TimeSpan);
				return false;
			}
			if (!UriParserHelper.TryRemoveQuotes(ref text))
			{
				targetValue = default(TimeSpan);
				return false;
			}
			bool flag;
			try
			{
				targetValue = EdmValueParser.ParseDuration(text);
				flag = true;
			}
			catch (FormatException)
			{
				targetValue = default(TimeSpan);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00052C9C File Offset: 0x00050E9C
		private static bool TryUriStringToGeography(string text, out Geography targetValue, out UriLiteralParsingException parsingException)
		{
			parsingException = null;
			if (!UriParserHelper.TryRemoveLiteralPrefix("geography", ref text))
			{
				targetValue = null;
				return false;
			}
			if (!UriParserHelper.TryRemoveQuotes(ref text))
			{
				targetValue = null;
				return false;
			}
			bool flag;
			try
			{
				targetValue = LiteralUtils.ParseGeography(text);
				flag = true;
			}
			catch (ParseErrorException ex)
			{
				targetValue = null;
				parsingException = new UriLiteralParsingException(ex.Message);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00052D00 File Offset: 0x00050F00
		private static bool TryUriStringToGeometry(string text, out Geometry targetValue, out UriLiteralParsingException parsingFailureReasonException)
		{
			parsingFailureReasonException = null;
			if (!UriParserHelper.TryRemoveLiteralPrefix("geometry", ref text))
			{
				targetValue = null;
				return false;
			}
			if (!UriParserHelper.TryRemoveQuotes(ref text))
			{
				targetValue = null;
				return false;
			}
			bool flag;
			try
			{
				targetValue = LiteralUtils.ParseGeometry(text);
				flag = true;
			}
			catch (ParseErrorException ex)
			{
				targetValue = null;
				parsingFailureReasonException = new UriLiteralParsingException(ex.Message);
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000A4C RID: 2636
		private static UriPrimitiveTypeParser singleInstance = new UriPrimitiveTypeParser();
	}
}
