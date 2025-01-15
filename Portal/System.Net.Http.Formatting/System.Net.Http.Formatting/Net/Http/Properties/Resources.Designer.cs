using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Properties
{
	// Token: 0x02000025 RID: 37
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00004BD2 File Offset: 0x00002DD2
		internal Resources()
		{
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00004BDC File Offset: 0x00002DDC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Assembly assembly = typeof(Resources).Assembly;
					Resources.resourceMan = new ResourceManager("System.Net.Http.Properties.Resources", assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004C15 File Offset: 0x00002E15
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00004C1C File Offset: 0x00002E1C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00004C24 File Offset: 0x00002E24
		internal static string AsyncResult_CallbackThrewException
		{
			get
			{
				return Resources.ResourceManager.GetString("AsyncResult_CallbackThrewException", Resources.resourceCulture);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004C3A File Offset: 0x00002E3A
		internal static string AsyncResult_MultipleCompletes
		{
			get
			{
				return Resources.ResourceManager.GetString("AsyncResult_MultipleCompletes", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004C50 File Offset: 0x00002E50
		internal static string AsyncResult_MultipleEnds
		{
			get
			{
				return Resources.ResourceManager.GetString("AsyncResult_MultipleEnds", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00004C66 File Offset: 0x00002E66
		internal static string AsyncResult_ResultMismatch
		{
			get
			{
				return Resources.ResourceManager.GetString("AsyncResult_ResultMismatch", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00004C7C File Offset: 0x00002E7C
		internal static string ByteRangeStreamContentNoRanges
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamContentNoRanges", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00004C92 File Offset: 0x00002E92
		internal static string ByteRangeStreamContentNotBytesRange
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamContentNotBytesRange", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00004CA8 File Offset: 0x00002EA8
		internal static string ByteRangeStreamEmpty
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamEmpty", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00004CBE File Offset: 0x00002EBE
		internal static string ByteRangeStreamInvalidFrom
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamInvalidFrom", Resources.resourceCulture);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00004CD4 File Offset: 0x00002ED4
		internal static string ByteRangeStreamInvalidOffset
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamInvalidOffset", Resources.resourceCulture);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00004CEA File Offset: 0x00002EEA
		internal static string ByteRangeStreamNoneOverlap
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamNoneOverlap", Resources.resourceCulture);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00004D00 File Offset: 0x00002F00
		internal static string ByteRangeStreamNoOverlap
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamNoOverlap", Resources.resourceCulture);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00004D16 File Offset: 0x00002F16
		internal static string ByteRangeStreamNotSeekable
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamNotSeekable", Resources.resourceCulture);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004D2C File Offset: 0x00002F2C
		internal static string ByteRangeStreamReadOnly
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamReadOnly", Resources.resourceCulture);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00004D42 File Offset: 0x00002F42
		internal static string CannotHaveNullInList
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotHaveNullInList", Resources.resourceCulture);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004D58 File Offset: 0x00002F58
		internal static string CannotUseMediaRangeForSupportedMediaType
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotUseMediaRangeForSupportedMediaType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004D6E File Offset: 0x00002F6E
		internal static string CannotUseNullValueType
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotUseNullValueType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004D84 File Offset: 0x00002F84
		internal static string CookieInvalidName
		{
			get
			{
				return Resources.ResourceManager.GetString("CookieInvalidName", Resources.resourceCulture);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004D9A File Offset: 0x00002F9A
		internal static string CookieNull
		{
			get
			{
				return Resources.ResourceManager.GetString("CookieNull", Resources.resourceCulture);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00004DB0 File Offset: 0x00002FB0
		internal static string DelegatingHandlerArrayContainsNullItem
		{
			get
			{
				return Resources.ResourceManager.GetString("DelegatingHandlerArrayContainsNullItem", Resources.resourceCulture);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00004DC6 File Offset: 0x00002FC6
		internal static string DelegatingHandlerArrayHasNonNullInnerHandler
		{
			get
			{
				return Resources.ResourceManager.GetString("DelegatingHandlerArrayHasNonNullInnerHandler", Resources.resourceCulture);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00004DDC File Offset: 0x00002FDC
		internal static string ErrorReadingFormUrlEncodedStream
		{
			get
			{
				return Resources.ResourceManager.GetString("ErrorReadingFormUrlEncodedStream", Resources.resourceCulture);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004DF2 File Offset: 0x00002FF2
		internal static string FormUrlEncodedMismatchingTypes
		{
			get
			{
				return Resources.ResourceManager.GetString("FormUrlEncodedMismatchingTypes", Resources.resourceCulture);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00004E08 File Offset: 0x00003008
		internal static string FormUrlEncodedParseError
		{
			get
			{
				return Resources.ResourceManager.GetString("FormUrlEncodedParseError", Resources.resourceCulture);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00004E1E File Offset: 0x0000301E
		internal static string HttpInvalidStatusCode
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpInvalidStatusCode", Resources.resourceCulture);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00004E34 File Offset: 0x00003034
		internal static string HttpInvalidVersion
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpInvalidVersion", Resources.resourceCulture);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00004E4A File Offset: 0x0000304A
		internal static string HttpMessageContentAlreadyRead
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageContentAlreadyRead", Resources.resourceCulture);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00004E60 File Offset: 0x00003060
		internal static string HttpMessageContentStreamMustBeSeekable
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageContentStreamMustBeSeekable", Resources.resourceCulture);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00004E76 File Offset: 0x00003076
		internal static string HttpMessageErrorReading
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageErrorReading", Resources.resourceCulture);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00004E8C File Offset: 0x0000308C
		internal static string HttpMessageInvalidMediaType
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageInvalidMediaType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00004EA2 File Offset: 0x000030A2
		internal static string HttpMessageParserEmptyUri
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageParserEmptyUri", Resources.resourceCulture);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00004EB8 File Offset: 0x000030B8
		internal static string HttpMessageParserError
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageParserError", Resources.resourceCulture);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00004ECE File Offset: 0x000030CE
		internal static string HttpMessageParserInvalidHostCount
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageParserInvalidHostCount", Resources.resourceCulture);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00004EE4 File Offset: 0x000030E4
		internal static string HttpMessageParserInvalidUriScheme
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageParserInvalidUriScheme", Resources.resourceCulture);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00004EFA File Offset: 0x000030FA
		internal static string InvalidArrayInsert
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidArrayInsert", Resources.resourceCulture);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004F10 File Offset: 0x00003110
		internal static string JQuery13CompatModeNotSupportNestedJson
		{
			get
			{
				return Resources.ResourceManager.GetString("JQuery13CompatModeNotSupportNestedJson", Resources.resourceCulture);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00004F26 File Offset: 0x00003126
		internal static string JsonSerializerFactoryReturnedNull
		{
			get
			{
				return Resources.ResourceManager.GetString("JsonSerializerFactoryReturnedNull", Resources.resourceCulture);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004F3C File Offset: 0x0000313C
		internal static string JsonSerializerFactoryThrew
		{
			get
			{
				return Resources.ResourceManager.GetString("JsonSerializerFactoryThrew", Resources.resourceCulture);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00004F52 File Offset: 0x00003152
		internal static string MaxDepthExceeded
		{
			get
			{
				return Resources.ResourceManager.GetString("MaxDepthExceeded", Resources.resourceCulture);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004F68 File Offset: 0x00003168
		internal static string MaxHttpCollectionKeyLimitReached
		{
			get
			{
				return Resources.ResourceManager.GetString("MaxHttpCollectionKeyLimitReached", Resources.resourceCulture);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004F7E File Offset: 0x0000317E
		internal static string MediaTypeFormatter_BsonParseError_MissingData
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatter_BsonParseError_MissingData", Resources.resourceCulture);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004F94 File Offset: 0x00003194
		internal static string MediaTypeFormatter_BsonParseError_UnexpectedData
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatter_BsonParseError_UnexpectedData", Resources.resourceCulture);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004FAA File Offset: 0x000031AA
		internal static string MediaTypeFormatter_JsonReaderFactoryReturnedNull
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatter_JsonReaderFactoryReturnedNull", Resources.resourceCulture);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004FC0 File Offset: 0x000031C0
		internal static string MediaTypeFormatter_JsonWriterFactoryReturnedNull
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatter_JsonWriterFactoryReturnedNull", Resources.resourceCulture);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00004FD6 File Offset: 0x000031D6
		internal static string MediaTypeFormatterCannotRead
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatterCannotRead", Resources.resourceCulture);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004FEC File Offset: 0x000031EC
		internal static string MediaTypeFormatterCannotReadSync
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatterCannotReadSync", Resources.resourceCulture);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00005002 File Offset: 0x00003202
		internal static string MediaTypeFormatterCannotWrite
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatterCannotWrite", Resources.resourceCulture);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005018 File Offset: 0x00003218
		internal static string MediaTypeFormatterCannotWriteSync
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatterCannotWriteSync", Resources.resourceCulture);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000502E File Offset: 0x0000322E
		internal static string MediaTypeFormatterNoEncoding
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatterNoEncoding", Resources.resourceCulture);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005044 File Offset: 0x00003244
		internal static string MimeMultipartParserBadBoundary
		{
			get
			{
				return Resources.ResourceManager.GetString("MimeMultipartParserBadBoundary", Resources.resourceCulture);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000505A File Offset: 0x0000325A
		internal static string MultipartFormDataStreamProviderNoContentDisposition
		{
			get
			{
				return Resources.ResourceManager.GetString("MultipartFormDataStreamProviderNoContentDisposition", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005070 File Offset: 0x00003270
		internal static string MultipartStreamProviderInvalidLocalFileName
		{
			get
			{
				return Resources.ResourceManager.GetString("MultipartStreamProviderInvalidLocalFileName", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005086 File Offset: 0x00003286
		internal static string NestedBracketNotValid
		{
			get
			{
				return Resources.ResourceManager.GetString("NestedBracketNotValid", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000509C File Offset: 0x0000329C
		internal static string NonNullUriRequiredForMediaTypeMapping
		{
			get
			{
				return Resources.ResourceManager.GetString("NonNullUriRequiredForMediaTypeMapping", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000050B2 File Offset: 0x000032B2
		internal static string NoReadSerializerAvailable
		{
			get
			{
				return Resources.ResourceManager.GetString("NoReadSerializerAvailable", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000050C8 File Offset: 0x000032C8
		internal static string ObjectAndTypeDisagree
		{
			get
			{
				return Resources.ResourceManager.GetString("ObjectAndTypeDisagree", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000050DE File Offset: 0x000032DE
		internal static string ObjectContent_FormatterCannotWriteType
		{
			get
			{
				return Resources.ResourceManager.GetString("ObjectContent_FormatterCannotWriteType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000050F4 File Offset: 0x000032F4
		internal static string QueryStringNameShouldNotNull
		{
			get
			{
				return Resources.ResourceManager.GetString("QueryStringNameShouldNotNull", Resources.resourceCulture);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000510A File Offset: 0x0000330A
		internal static string ReadAsHttpMessageUnexpectedTermination
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsHttpMessageUnexpectedTermination", Resources.resourceCulture);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005120 File Offset: 0x00003320
		internal static string ReadAsMimeMultipartArgumentNoBoundary
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartArgumentNoBoundary", Resources.resourceCulture);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00005136 File Offset: 0x00003336
		internal static string ReadAsMimeMultipartArgumentNoContentType
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartArgumentNoContentType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000514C File Offset: 0x0000334C
		internal static string ReadAsMimeMultipartArgumentNoMultipart
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartArgumentNoMultipart", Resources.resourceCulture);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00005162 File Offset: 0x00003362
		internal static string ReadAsMimeMultipartErrorReading
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartErrorReading", Resources.resourceCulture);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00005178 File Offset: 0x00003378
		internal static string ReadAsMimeMultipartErrorWriting
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartErrorWriting", Resources.resourceCulture);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000518E File Offset: 0x0000338E
		internal static string ReadAsMimeMultipartHeaderParseError
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartHeaderParseError", Resources.resourceCulture);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000051A4 File Offset: 0x000033A4
		internal static string ReadAsMimeMultipartParseError
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartParseError", Resources.resourceCulture);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000051BA File Offset: 0x000033BA
		internal static string ReadAsMimeMultipartStreamProviderException
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartStreamProviderException", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000051D0 File Offset: 0x000033D0
		internal static string ReadAsMimeMultipartStreamProviderNull
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartStreamProviderNull", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000051E6 File Offset: 0x000033E6
		internal static string ReadAsMimeMultipartStreamProviderReadOnly
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartStreamProviderReadOnly", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000051FC File Offset: 0x000033FC
		internal static string ReadAsMimeMultipartUnexpectedTermination
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartUnexpectedTermination", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00005212 File Offset: 0x00003412
		internal static string RemoteStreamInfoCannotBeNull
		{
			get
			{
				return Resources.ResourceManager.GetString("RemoteStreamInfoCannotBeNull", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005228 File Offset: 0x00003428
		internal static string SerializerCannotSerializeType
		{
			get
			{
				return Resources.ResourceManager.GetString("SerializerCannotSerializeType", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000523E File Offset: 0x0000343E
		internal static string UnMatchedBracketNotValid
		{
			get
			{
				return Resources.ResourceManager.GetString("UnMatchedBracketNotValid", Resources.resourceCulture);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005254 File Offset: 0x00003454
		internal static string UnsupportedIndent
		{
			get
			{
				return Resources.ResourceManager.GetString("UnsupportedIndent", Resources.resourceCulture);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000526A File Offset: 0x0000346A
		internal static string XmlMediaTypeFormatter_InvalidSerializerType
		{
			get
			{
				return Resources.ResourceManager.GetString("XmlMediaTypeFormatter_InvalidSerializerType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005280 File Offset: 0x00003480
		internal static string XmlMediaTypeFormatter_NullReturnedSerializer
		{
			get
			{
				return Resources.ResourceManager.GetString("XmlMediaTypeFormatter_NullReturnedSerializer", Resources.resourceCulture);
			}
		}

		// Token: 0x04000063 RID: 99
		private static ResourceManager resourceMan;

		// Token: 0x04000064 RID: 100
		private static CultureInfo resourceCulture;
	}
}
