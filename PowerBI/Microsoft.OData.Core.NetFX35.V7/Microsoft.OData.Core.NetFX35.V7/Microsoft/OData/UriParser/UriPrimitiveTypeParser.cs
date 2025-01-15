using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;
using Microsoft.Spatial;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A6 RID: 422
	[Guid("A77303D9-3A04-4829-BDDE-3B4D43E21CFC")]
	internal sealed class UriPrimitiveTypeParser : IUriLiteralParser
	{
		// Token: 0x060010F8 RID: 4344 RVA: 0x00002CFE File Offset: 0x00000EFE
		private UriPrimitiveTypeParser()
		{
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0002F3EB File Offset: 0x0002D5EB
		public static UriPrimitiveTypeParser Instance
		{
			get
			{
				return UriPrimitiveTypeParser.singleInstance;
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0002F3F4 File Offset: 0x0002D5F4
		public object ParseUriStringToType(string text, IEdmTypeReference targetType, out UriLiteralParsingException parsingException)
		{
			object obj;
			if (this.TryUriStringToPrimitive(text, targetType, out obj, out parsingException))
			{
				return obj;
			}
			return null;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0002F411 File Offset: 0x0002D611
		internal bool TryParseUriStringToType(string text, IEdmTypeReference targetType, out object targetValue, out UriLiteralParsingException parsingException)
		{
			return this.TryUriStringToPrimitive(text, targetType, out targetValue, out parsingException);
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0002F420 File Offset: 0x0002D620
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
										goto IL_02DB;
									case EdmPrimitiveTypeKind.Byte:
										targetValue = XmlConvert.ToByte(text);
										goto IL_02DB;
									case EdmPrimitiveTypeKind.Decimal:
										UriParserHelper.TryRemoveLiteralSuffix("M", ref text);
										try
										{
											targetValue = XmlConvert.ToDecimal(text);
											goto IL_02DB;
										}
										catch (FormatException)
										{
											decimal num;
											if (decimal.TryParse(text, 167, NumberFormatInfo.InvariantInfo, ref num))
											{
												targetValue = num;
												goto IL_02DB;
											}
											targetValue = 0m;
											return false;
										}
										break;
									case EdmPrimitiveTypeKind.Double:
										UriParserHelper.TryRemoveLiteralSuffix("D", ref text);
										targetValue = XmlConvert.ToDouble(text);
										goto IL_02DB;
									case EdmPrimitiveTypeKind.Int16:
										targetValue = XmlConvert.ToInt16(text);
										goto IL_02DB;
									case EdmPrimitiveTypeKind.Int32:
										targetValue = XmlConvert.ToInt32(text);
										goto IL_02DB;
									case EdmPrimitiveTypeKind.Int64:
										UriParserHelper.TryRemoveLiteralSuffix("L", ref text);
										targetValue = XmlConvert.ToInt64(text);
										goto IL_02DB;
									case EdmPrimitiveTypeKind.SByte:
										targetValue = XmlConvert.ToSByte(text);
										goto IL_02DB;
									case EdmPrimitiveTypeKind.Single:
										UriParserHelper.TryRemoveLiteralSuffix("f", ref text);
										targetValue = XmlConvert.ToSingle(text);
										goto IL_02DB;
									case EdmPrimitiveTypeKind.String:
										targetValue = text;
										goto IL_02DB;
									}
									throw new ODataException(Strings.General_InternalError(InternalErrorCodes.UriPrimitiveTypeParser_TryUriStringToPrimitive));
									IL_02DB:
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

		// Token: 0x060010FD RID: 4349 RVA: 0x0002F7B4 File Offset: 0x0002D9B4
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Complexity is not too high; handling all the cases in one method is preferable to refactoring.")]
		private bool TryUriStringToPrimitive(string text, IEdmTypeReference targetType, out object targetValue)
		{
			UriLiteralParsingException ex;
			return this.TryUriStringToPrimitive(text, targetType, out targetValue, out ex);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0002F7CC File Offset: 0x0002D9CC
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

		// Token: 0x060010FF RID: 4351 RVA: 0x0002F820 File Offset: 0x0002DA20
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

		// Token: 0x06001100 RID: 4352 RVA: 0x0002F884 File Offset: 0x0002DA84
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

		// Token: 0x06001101 RID: 4353 RVA: 0x0002F8E8 File Offset: 0x0002DAE8
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

		// Token: 0x040008BD RID: 2237
		private static UriPrimitiveTypeParser singleInstance = new UriPrimitiveTypeParser();
	}
}
