using System;
using System.Resources;
using FxResources.System.Text.Json;

namespace System
{
	// Token: 0x0200000E RID: 14
	internal static class SR
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000240E File Offset: 0x0000060E
		internal static bool UsingResourceKeys()
		{
			return global::System.SR.s_usingResourceKeys;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002418 File Offset: 0x00000618
		private static string GetResourceString(string resourceKey)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return resourceKey;
			}
			string text = null;
			try
			{
				text = global::System.SR.ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			return text;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002454 File Offset: 0x00000654
		private static string GetResourceString(string resourceKey, string defaultString)
		{
			string resourceString = global::System.SR.GetResourceString(resourceKey);
			if (!(resourceKey == resourceString) && resourceString != null)
			{
				return resourceString;
			}
			return defaultString;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002477 File Offset: 0x00000677
		internal static string Format(string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024A0 File Offset: 0x000006A0
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024CE File Offset: 0x000006CE
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002501 File Offset: 0x00000701
		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (global::System.SR.UsingResourceKeys())
			{
				return resourceFormat + ", " + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000252D File Offset: 0x0000072D
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(provider, resourceFormat, p1);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002557 File Offset: 0x00000757
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(provider, resourceFormat, p1, p2);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002586 File Offset: 0x00000786
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(provider, resourceFormat, p1, p2, p3);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025BC File Offset: 0x000007BC
		internal static string Format(IFormatProvider provider, string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (global::System.SR.UsingResourceKeys())
			{
				return resourceFormat + ", " + string.Join(", ", args);
			}
			return string.Format(provider, resourceFormat, args);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000025E9 File Offset: 0x000007E9
		internal static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager resourceManager;
				if ((resourceManager = global::System.SR.s_resourceManager) == null)
				{
					resourceManager = (global::System.SR.s_resourceManager = new ResourceManager(typeof(FxResources.System.Text.Json.SR)));
				}
				return resourceManager;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002609 File Offset: 0x00000809
		internal static string ArrayDepthTooLarge
		{
			get
			{
				return global::System.SR.GetResourceString("ArrayDepthTooLarge");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002615 File Offset: 0x00000815
		internal static string CallFlushToAvoidDataLoss
		{
			get
			{
				return global::System.SR.GetResourceString("CallFlushToAvoidDataLoss");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002621 File Offset: 0x00000821
		internal static string CannotReadIncompleteUTF16
		{
			get
			{
				return global::System.SR.GetResourceString("CannotReadIncompleteUTF16");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000262D File Offset: 0x0000082D
		internal static string CannotReadInvalidUTF16
		{
			get
			{
				return global::System.SR.GetResourceString("CannotReadInvalidUTF16");
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002639 File Offset: 0x00000839
		internal static string CannotStartObjectArrayAfterPrimitiveOrClose
		{
			get
			{
				return global::System.SR.GetResourceString("CannotStartObjectArrayAfterPrimitiveOrClose");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002645 File Offset: 0x00000845
		internal static string CannotStartObjectArrayWithoutProperty
		{
			get
			{
				return global::System.SR.GetResourceString("CannotStartObjectArrayWithoutProperty");
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002651 File Offset: 0x00000851
		internal static string CannotTranscodeInvalidUtf8
		{
			get
			{
				return global::System.SR.GetResourceString("CannotTranscodeInvalidUtf8");
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000265D File Offset: 0x0000085D
		internal static string CannotDecodeInvalidBase64
		{
			get
			{
				return global::System.SR.GetResourceString("CannotDecodeInvalidBase64");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002669 File Offset: 0x00000869
		internal static string CannotTranscodeInvalidUtf16
		{
			get
			{
				return global::System.SR.GetResourceString("CannotTranscodeInvalidUtf16");
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002675 File Offset: 0x00000875
		internal static string CannotEncodeInvalidUTF16
		{
			get
			{
				return global::System.SR.GetResourceString("CannotEncodeInvalidUTF16");
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002681 File Offset: 0x00000881
		internal static string CannotEncodeInvalidUTF8
		{
			get
			{
				return global::System.SR.GetResourceString("CannotEncodeInvalidUTF8");
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000268D File Offset: 0x0000088D
		internal static string CannotWritePropertyWithinArray
		{
			get
			{
				return global::System.SR.GetResourceString("CannotWritePropertyWithinArray");
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002699 File Offset: 0x00000899
		internal static string CannotWritePropertyAfterProperty
		{
			get
			{
				return global::System.SR.GetResourceString("CannotWritePropertyAfterProperty");
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000026A5 File Offset: 0x000008A5
		internal static string CannotWriteValueAfterPrimitiveOrClose
		{
			get
			{
				return global::System.SR.GetResourceString("CannotWriteValueAfterPrimitiveOrClose");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000026B1 File Offset: 0x000008B1
		internal static string CannotWriteValueWithinObject
		{
			get
			{
				return global::System.SR.GetResourceString("CannotWriteValueWithinObject");
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000026BD File Offset: 0x000008BD
		internal static string DepthTooLarge
		{
			get
			{
				return global::System.SR.GetResourceString("DepthTooLarge");
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000026C9 File Offset: 0x000008C9
		internal static string DestinationTooShort
		{
			get
			{
				return global::System.SR.GetResourceString("DestinationTooShort");
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000026D5 File Offset: 0x000008D5
		internal static string EmptyJsonIsInvalid
		{
			get
			{
				return global::System.SR.GetResourceString("EmptyJsonIsInvalid");
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000026E1 File Offset: 0x000008E1
		internal static string EndOfCommentNotFound
		{
			get
			{
				return global::System.SR.GetResourceString("EndOfCommentNotFound");
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000026ED File Offset: 0x000008ED
		internal static string EndOfStringNotFound
		{
			get
			{
				return global::System.SR.GetResourceString("EndOfStringNotFound");
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000026F9 File Offset: 0x000008F9
		internal static string ExpectedEndAfterSingleJson
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedEndAfterSingleJson");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002705 File Offset: 0x00000905
		internal static string ExpectedEndOfDigitNotFound
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedEndOfDigitNotFound");
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002711 File Offset: 0x00000911
		internal static string ExpectedFalse
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedFalse");
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000271D File Offset: 0x0000091D
		internal static string ExpectedJsonTokens
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedJsonTokens");
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002729 File Offset: 0x00000929
		internal static string ExpectedOneCompleteToken
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedOneCompleteToken");
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002735 File Offset: 0x00000935
		internal static string ExpectedNextDigitEValueNotFound
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedNextDigitEValueNotFound");
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002741 File Offset: 0x00000941
		internal static string ExpectedNull
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedNull");
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000274D File Offset: 0x0000094D
		internal static string ExpectedSeparatorAfterPropertyNameNotFound
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedSeparatorAfterPropertyNameNotFound");
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002759 File Offset: 0x00000959
		internal static string ExpectedStartOfPropertyNotFound
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedStartOfPropertyNotFound");
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002765 File Offset: 0x00000965
		internal static string ExpectedStartOfPropertyOrValueNotFound
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedStartOfPropertyOrValueNotFound");
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002771 File Offset: 0x00000971
		internal static string ExpectedStartOfValueNotFound
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedStartOfValueNotFound");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000277D File Offset: 0x0000097D
		internal static string ExpectedTrue
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedTrue");
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002789 File Offset: 0x00000989
		internal static string ExpectedValueAfterPropertyNameNotFound
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedValueAfterPropertyNameNotFound");
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002795 File Offset: 0x00000995
		internal static string FailedToGetLargerSpan
		{
			get
			{
				return global::System.SR.GetResourceString("FailedToGetLargerSpan");
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000027A1 File Offset: 0x000009A1
		internal static string FoundInvalidCharacter
		{
			get
			{
				return global::System.SR.GetResourceString("FoundInvalidCharacter");
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000027AD File Offset: 0x000009AD
		internal static string InvalidCast
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidCast");
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000027B9 File Offset: 0x000009B9
		internal static string InvalidCharacterAfterEscapeWithinString
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidCharacterAfterEscapeWithinString");
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000027C5 File Offset: 0x000009C5
		internal static string InvalidCharacterWithinString
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidCharacterWithinString");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000027D1 File Offset: 0x000009D1
		internal static string InvalidEnumTypeWithSpecialChar
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidEnumTypeWithSpecialChar");
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000027DD File Offset: 0x000009DD
		internal static string InvalidEndOfJsonNonPrimitive
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidEndOfJsonNonPrimitive");
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000027E9 File Offset: 0x000009E9
		internal static string InvalidHexCharacterWithinString
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidHexCharacterWithinString");
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000027F5 File Offset: 0x000009F5
		internal static string JsonDocumentDoesNotSupportComments
		{
			get
			{
				return global::System.SR.GetResourceString("JsonDocumentDoesNotSupportComments");
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002801 File Offset: 0x00000A01
		internal static string JsonElementHasWrongType
		{
			get
			{
				return global::System.SR.GetResourceString("JsonElementHasWrongType");
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000280D File Offset: 0x00000A0D
		internal static string DefaultTypeInfoResolverImmutable
		{
			get
			{
				return global::System.SR.GetResourceString("DefaultTypeInfoResolverImmutable");
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002819 File Offset: 0x00000A19
		internal static string TypeInfoResolverChainImmutable
		{
			get
			{
				return global::System.SR.GetResourceString("TypeInfoResolverChainImmutable");
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002825 File Offset: 0x00000A25
		internal static string TypeInfoImmutable
		{
			get
			{
				return global::System.SR.GetResourceString("TypeInfoImmutable");
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002831 File Offset: 0x00000A31
		internal static string MaxDepthMustBePositive
		{
			get
			{
				return global::System.SR.GetResourceString("MaxDepthMustBePositive");
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000283D File Offset: 0x00000A3D
		internal static string CommentHandlingMustBeValid
		{
			get
			{
				return global::System.SR.GetResourceString("CommentHandlingMustBeValid");
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002849 File Offset: 0x00000A49
		internal static string MismatchedObjectArray
		{
			get
			{
				return global::System.SR.GetResourceString("MismatchedObjectArray");
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002855 File Offset: 0x00000A55
		internal static string CannotWriteEndAfterProperty
		{
			get
			{
				return global::System.SR.GetResourceString("CannotWriteEndAfterProperty");
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002861 File Offset: 0x00000A61
		internal static string ObjectDepthTooLarge
		{
			get
			{
				return global::System.SR.GetResourceString("ObjectDepthTooLarge");
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000286D File Offset: 0x00000A6D
		internal static string PropertyNameTooLarge
		{
			get
			{
				return global::System.SR.GetResourceString("PropertyNameTooLarge");
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002879 File Offset: 0x00000A79
		internal static string FormatDecimal
		{
			get
			{
				return global::System.SR.GetResourceString("FormatDecimal");
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002885 File Offset: 0x00000A85
		internal static string FormatDouble
		{
			get
			{
				return global::System.SR.GetResourceString("FormatDouble");
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002891 File Offset: 0x00000A91
		internal static string FormatInt32
		{
			get
			{
				return global::System.SR.GetResourceString("FormatInt32");
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000289D File Offset: 0x00000A9D
		internal static string FormatInt64
		{
			get
			{
				return global::System.SR.GetResourceString("FormatInt64");
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000028A9 File Offset: 0x00000AA9
		internal static string FormatSingle
		{
			get
			{
				return global::System.SR.GetResourceString("FormatSingle");
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000028B5 File Offset: 0x00000AB5
		internal static string FormatUInt32
		{
			get
			{
				return global::System.SR.GetResourceString("FormatUInt32");
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000028C1 File Offset: 0x00000AC1
		internal static string FormatUInt64
		{
			get
			{
				return global::System.SR.GetResourceString("FormatUInt64");
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000028CD File Offset: 0x00000ACD
		internal static string RequiredDigitNotFoundAfterDecimal
		{
			get
			{
				return global::System.SR.GetResourceString("RequiredDigitNotFoundAfterDecimal");
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000028D9 File Offset: 0x00000AD9
		internal static string RequiredDigitNotFoundAfterSign
		{
			get
			{
				return global::System.SR.GetResourceString("RequiredDigitNotFoundAfterSign");
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000028E5 File Offset: 0x00000AE5
		internal static string RequiredDigitNotFoundEndOfData
		{
			get
			{
				return global::System.SR.GetResourceString("RequiredDigitNotFoundEndOfData");
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000028F1 File Offset: 0x00000AF1
		internal static string SpecialNumberValuesNotSupported
		{
			get
			{
				return global::System.SR.GetResourceString("SpecialNumberValuesNotSupported");
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000028FD File Offset: 0x00000AFD
		internal static string ValueTooLarge
		{
			get
			{
				return global::System.SR.GetResourceString("ValueTooLarge");
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002909 File Offset: 0x00000B09
		internal static string ZeroDepthAtEnd
		{
			get
			{
				return global::System.SR.GetResourceString("ZeroDepthAtEnd");
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002915 File Offset: 0x00000B15
		internal static string DeserializeUnableToConvertValue
		{
			get
			{
				return global::System.SR.GetResourceString("DeserializeUnableToConvertValue");
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002921 File Offset: 0x00000B21
		internal static string DeserializeWrongType
		{
			get
			{
				return global::System.SR.GetResourceString("DeserializeWrongType");
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000292D File Offset: 0x00000B2D
		internal static string SerializationInvalidBufferSize
		{
			get
			{
				return global::System.SR.GetResourceString("SerializationInvalidBufferSize");
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002939 File Offset: 0x00000B39
		internal static string BufferWriterAdvancedTooFar
		{
			get
			{
				return global::System.SR.GetResourceString("BufferWriterAdvancedTooFar");
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002945 File Offset: 0x00000B45
		internal static string InvalidComparison
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidComparison");
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002951 File Offset: 0x00000B51
		internal static string UnsupportedFormat
		{
			get
			{
				return global::System.SR.GetResourceString("UnsupportedFormat");
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000295D File Offset: 0x00000B5D
		internal static string ExpectedStartOfPropertyOrValueAfterComment
		{
			get
			{
				return global::System.SR.GetResourceString("ExpectedStartOfPropertyOrValueAfterComment");
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002969 File Offset: 0x00000B69
		internal static string TrailingCommaNotAllowedBeforeArrayEnd
		{
			get
			{
				return global::System.SR.GetResourceString("TrailingCommaNotAllowedBeforeArrayEnd");
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002975 File Offset: 0x00000B75
		internal static string TrailingCommaNotAllowedBeforeObjectEnd
		{
			get
			{
				return global::System.SR.GetResourceString("TrailingCommaNotAllowedBeforeObjectEnd");
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002981 File Offset: 0x00000B81
		internal static string SerializerOptionsReadOnly
		{
			get
			{
				return global::System.SR.GetResourceString("SerializerOptionsReadOnly");
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000298D File Offset: 0x00000B8D
		internal static string SerializerOptions_InvalidChainedResolver
		{
			get
			{
				return global::System.SR.GetResourceString("SerializerOptions_InvalidChainedResolver");
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002999 File Offset: 0x00000B99
		internal static string StreamNotWritable
		{
			get
			{
				return global::System.SR.GetResourceString("StreamNotWritable");
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000029A5 File Offset: 0x00000BA5
		internal static string CannotWriteCommentWithEmbeddedDelimiter
		{
			get
			{
				return global::System.SR.GetResourceString("CannotWriteCommentWithEmbeddedDelimiter");
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000029B1 File Offset: 0x00000BB1
		internal static string SerializerPropertyNameConflict
		{
			get
			{
				return global::System.SR.GetResourceString("SerializerPropertyNameConflict");
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000029BD File Offset: 0x00000BBD
		internal static string SerializerPropertyNameNull
		{
			get
			{
				return global::System.SR.GetResourceString("SerializerPropertyNameNull");
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000029C9 File Offset: 0x00000BC9
		internal static string SerializationDataExtensionPropertyInvalid
		{
			get
			{
				return global::System.SR.GetResourceString("SerializationDataExtensionPropertyInvalid");
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000029D5 File Offset: 0x00000BD5
		internal static string SerializationDuplicateTypeAttribute
		{
			get
			{
				return global::System.SR.GetResourceString("SerializationDuplicateTypeAttribute");
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000029E1 File Offset: 0x00000BE1
		internal static string ExtensionDataConflictsWithUnmappedMemberHandling
		{
			get
			{
				return global::System.SR.GetResourceString("ExtensionDataConflictsWithUnmappedMemberHandling");
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000029ED File Offset: 0x00000BED
		internal static string SerializationNotSupportedType
		{
			get
			{
				return global::System.SR.GetResourceString("SerializationNotSupportedType");
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000029F9 File Offset: 0x00000BF9
		internal static string TypeRequiresAsyncSerialization
		{
			get
			{
				return global::System.SR.GetResourceString("TypeRequiresAsyncSerialization");
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002A05 File Offset: 0x00000C05
		internal static string InvalidCharacterAtStartOfComment
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidCharacterAtStartOfComment");
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002A11 File Offset: 0x00000C11
		internal static string UnexpectedEndOfDataWhileReadingComment
		{
			get
			{
				return global::System.SR.GetResourceString("UnexpectedEndOfDataWhileReadingComment");
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002A1D File Offset: 0x00000C1D
		internal static string CannotSkip
		{
			get
			{
				return global::System.SR.GetResourceString("CannotSkip");
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002A29 File Offset: 0x00000C29
		internal static string NotEnoughData
		{
			get
			{
				return global::System.SR.GetResourceString("NotEnoughData");
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002A35 File Offset: 0x00000C35
		internal static string UnexpectedEndOfLineSeparator
		{
			get
			{
				return global::System.SR.GetResourceString("UnexpectedEndOfLineSeparator");
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002A41 File Offset: 0x00000C41
		internal static string JsonSerializerDoesNotSupportComments
		{
			get
			{
				return global::System.SR.GetResourceString("JsonSerializerDoesNotSupportComments");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002A4D File Offset: 0x00000C4D
		internal static string DeserializeNoConstructor
		{
			get
			{
				return global::System.SR.GetResourceString("DeserializeNoConstructor");
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002A59 File Offset: 0x00000C59
		internal static string DeserializePolymorphicInterface
		{
			get
			{
				return global::System.SR.GetResourceString("DeserializePolymorphicInterface");
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002A65 File Offset: 0x00000C65
		internal static string SerializationConverterOnAttributeNotCompatible
		{
			get
			{
				return global::System.SR.GetResourceString("SerializationConverterOnAttributeNotCompatible");
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002A71 File Offset: 0x00000C71
		internal static string SerializationConverterOnAttributeInvalid
		{
			get
			{
				return global::System.SR.GetResourceString("SerializationConverterOnAttributeInvalid");
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002A7D File Offset: 0x00000C7D
		internal static string SerializationConverterRead
		{
			get
			{
				return global::System.SR.GetResourceString("SerializationConverterRead");
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002A89 File Offset: 0x00000C89
		internal static string SerializationConverterNotCompatible
		{
			get
			{
				return global::System.SR.GetResourceString("SerializationConverterNotCompatible");
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002A95 File Offset: 0x00000C95
		internal static string ResolverTypeNotCompatible
		{
			get
			{
				return global::System.SR.GetResourceString("ResolverTypeNotCompatible");
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002AA1 File Offset: 0x00000CA1
		internal static string ResolverTypeInfoOptionsNotCompatible
		{
			get
			{
				return global::System.SR.GetResourceString("ResolverTypeInfoOptionsNotCompatible");
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002AAD File Offset: 0x00000CAD
		internal static string SerializationConverterWrite
		{
			get
			{
				return global::System.SR.GetResourceString("SerializationConverterWrite");
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002AB9 File Offset: 0x00000CB9
		internal static string NamingPolicyReturnNull
		{
			get
			{
				return global::System.SR.GetResourceString("NamingPolicyReturnNull");
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002AC5 File Offset: 0x00000CC5
		internal static string SerializationDuplicateAttribute
		{
			get
			{
				return global::System.SR.GetResourceString("SerializationDuplicateAttribute");
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002AD1 File Offset: 0x00000CD1
		internal static string SerializeUnableToSerialize
		{
			get
			{
				return global::System.SR.GetResourceString("SerializeUnableToSerialize");
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002ADD File Offset: 0x00000CDD
		internal static string FormatByte
		{
			get
			{
				return global::System.SR.GetResourceString("FormatByte");
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002AE9 File Offset: 0x00000CE9
		internal static string FormatInt16
		{
			get
			{
				return global::System.SR.GetResourceString("FormatInt16");
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002AF5 File Offset: 0x00000CF5
		internal static string FormatSByte
		{
			get
			{
				return global::System.SR.GetResourceString("FormatSByte");
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002B01 File Offset: 0x00000D01
		internal static string FormatUInt16
		{
			get
			{
				return global::System.SR.GetResourceString("FormatUInt16");
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002B0D File Offset: 0x00000D0D
		internal static string SerializerCycleDetected
		{
			get
			{
				return global::System.SR.GetResourceString("SerializerCycleDetected");
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002B19 File Offset: 0x00000D19
		internal static string InvalidLeadingZeroInNumber
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidLeadingZeroInNumber");
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002B25 File Offset: 0x00000D25
		internal static string MetadataCannotParsePreservedObjectToImmutable
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataCannotParsePreservedObjectToImmutable");
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002B31 File Offset: 0x00000D31
		internal static string MetadataDuplicateIdFound
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataDuplicateIdFound");
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002B3D File Offset: 0x00000D3D
		internal static string MetadataIdIsNotFirstProperty
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataIdIsNotFirstProperty");
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002B49 File Offset: 0x00000D49
		internal static string MetadataInvalidReferenceToValueType
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataInvalidReferenceToValueType");
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002B55 File Offset: 0x00000D55
		internal static string MetadataInvalidTokenAfterValues
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataInvalidTokenAfterValues");
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00002B61 File Offset: 0x00000D61
		internal static string MetadataPreservedArrayFailed
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataPreservedArrayFailed");
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002B6D File Offset: 0x00000D6D
		internal static string MetadataInvalidPropertyInArrayMetadata
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataInvalidPropertyInArrayMetadata");
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002B79 File Offset: 0x00000D79
		internal static string MetadataStandaloneValuesProperty
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataStandaloneValuesProperty");
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002B85 File Offset: 0x00000D85
		internal static string MetadataReferenceCannotContainOtherProperties
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataReferenceCannotContainOtherProperties");
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002B91 File Offset: 0x00000D91
		internal static string MetadataReferenceNotFound
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataReferenceNotFound");
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002B9D File Offset: 0x00000D9D
		internal static string MetadataValueWasNotString
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataValueWasNotString");
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002BA9 File Offset: 0x00000DA9
		internal static string MetadataInvalidPropertyWithLeadingDollarSign
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataInvalidPropertyWithLeadingDollarSign");
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002BB5 File Offset: 0x00000DB5
		internal static string MetadataUnexpectedProperty
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataUnexpectedProperty");
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002BC1 File Offset: 0x00000DC1
		internal static string UnmappedJsonProperty
		{
			get
			{
				return global::System.SR.GetResourceString("UnmappedJsonProperty");
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002BCD File Offset: 0x00000DCD
		internal static string MetadataDuplicateTypeProperty
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataDuplicateTypeProperty");
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002BD9 File Offset: 0x00000DD9
		internal static string MultipleMembersBindWithConstructorParameter
		{
			get
			{
				return global::System.SR.GetResourceString("MultipleMembersBindWithConstructorParameter");
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002BE5 File Offset: 0x00000DE5
		internal static string ConstructorParamIncompleteBinding
		{
			get
			{
				return global::System.SR.GetResourceString("ConstructorParamIncompleteBinding");
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00002BF1 File Offset: 0x00000DF1
		internal static string ObjectWithParameterizedCtorRefMetadataNotSupported
		{
			get
			{
				return global::System.SR.GetResourceString("ObjectWithParameterizedCtorRefMetadataNotSupported");
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002BFD File Offset: 0x00000DFD
		internal static string SerializerConverterFactoryReturnsNull
		{
			get
			{
				return global::System.SR.GetResourceString("SerializerConverterFactoryReturnsNull");
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00002C09 File Offset: 0x00000E09
		internal static string SerializationNotSupportedParentType
		{
			get
			{
				return global::System.SR.GetResourceString("SerializationNotSupportedParentType");
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002C15 File Offset: 0x00000E15
		internal static string ExtensionDataCannotBindToCtorParam
		{
			get
			{
				return global::System.SR.GetResourceString("ExtensionDataCannotBindToCtorParam");
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00002C21 File Offset: 0x00000E21
		internal static string BufferMaximumSizeExceeded
		{
			get
			{
				return global::System.SR.GetResourceString("BufferMaximumSizeExceeded");
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002C2D File Offset: 0x00000E2D
		internal static string CannotSerializeInvalidType
		{
			get
			{
				return global::System.SR.GetResourceString("CannotSerializeInvalidType");
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00002C39 File Offset: 0x00000E39
		internal static string SerializeTypeInstanceNotSupported
		{
			get
			{
				return global::System.SR.GetResourceString("SerializeTypeInstanceNotSupported");
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00002C45 File Offset: 0x00000E45
		internal static string JsonIncludeOnInaccessibleProperty
		{
			get
			{
				return global::System.SR.GetResourceString("JsonIncludeOnInaccessibleProperty");
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002C51 File Offset: 0x00000E51
		internal static string CannotSerializeInvalidMember
		{
			get
			{
				return global::System.SR.GetResourceString("CannotSerializeInvalidMember");
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00002C5D File Offset: 0x00000E5D
		internal static string CannotPopulateCollection
		{
			get
			{
				return global::System.SR.GetResourceString("CannotPopulateCollection");
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00002C69 File Offset: 0x00000E69
		internal static string ConstructorContainsNullParameterNames
		{
			get
			{
				return global::System.SR.GetResourceString("ConstructorContainsNullParameterNames");
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00002C75 File Offset: 0x00000E75
		internal static string DefaultIgnoreConditionAlreadySpecified
		{
			get
			{
				return global::System.SR.GetResourceString("DefaultIgnoreConditionAlreadySpecified");
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00002C81 File Offset: 0x00000E81
		internal static string DefaultIgnoreConditionInvalid
		{
			get
			{
				return global::System.SR.GetResourceString("DefaultIgnoreConditionInvalid");
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00002C8D File Offset: 0x00000E8D
		internal static string DictionaryKeyTypeNotSupported
		{
			get
			{
				return global::System.SR.GetResourceString("DictionaryKeyTypeNotSupported");
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002C99 File Offset: 0x00000E99
		internal static string IgnoreConditionOnValueTypeInvalid
		{
			get
			{
				return global::System.SR.GetResourceString("IgnoreConditionOnValueTypeInvalid");
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002CA5 File Offset: 0x00000EA5
		internal static string NumberHandlingOnPropertyInvalid
		{
			get
			{
				return global::System.SR.GetResourceString("NumberHandlingOnPropertyInvalid");
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002CB1 File Offset: 0x00000EB1
		internal static string ConverterCanConvertMultipleTypes
		{
			get
			{
				return global::System.SR.GetResourceString("ConverterCanConvertMultipleTypes");
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002CBD File Offset: 0x00000EBD
		internal static string MetadataReferenceOfTypeCannotBeAssignedToType
		{
			get
			{
				return global::System.SR.GetResourceString("MetadataReferenceOfTypeCannotBeAssignedToType");
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002CC9 File Offset: 0x00000EC9
		internal static string DeserializeUnableToAssignValue
		{
			get
			{
				return global::System.SR.GetResourceString("DeserializeUnableToAssignValue");
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002CD5 File Offset: 0x00000ED5
		internal static string DeserializeUnableToAssignNull
		{
			get
			{
				return global::System.SR.GetResourceString("DeserializeUnableToAssignNull");
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002CE1 File Offset: 0x00000EE1
		internal static string SerializerConverterFactoryReturnsJsonConverterFactory
		{
			get
			{
				return global::System.SR.GetResourceString("SerializerConverterFactoryReturnsJsonConverterFactory");
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002CED File Offset: 0x00000EED
		internal static string SerializerConverterFactoryInvalidArgument
		{
			get
			{
				return global::System.SR.GetResourceString("SerializerConverterFactoryInvalidArgument");
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00002CF9 File Offset: 0x00000EF9
		internal static string NodeElementWrongType
		{
			get
			{
				return global::System.SR.GetResourceString("NodeElementWrongType");
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002D05 File Offset: 0x00000F05
		internal static string NodeElementCannotBeObjectOrArray
		{
			get
			{
				return global::System.SR.GetResourceString("NodeElementCannotBeObjectOrArray");
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00002D11 File Offset: 0x00000F11
		internal static string NodeAlreadyHasParent
		{
			get
			{
				return global::System.SR.GetResourceString("NodeAlreadyHasParent");
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00002D1D File Offset: 0x00000F1D
		internal static string NodeCycleDetected
		{
			get
			{
				return global::System.SR.GetResourceString("NodeCycleDetected");
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00002D29 File Offset: 0x00000F29
		internal static string NodeUnableToConvert
		{
			get
			{
				return global::System.SR.GetResourceString("NodeUnableToConvert");
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00002D35 File Offset: 0x00000F35
		internal static string NodeUnableToConvertElement
		{
			get
			{
				return global::System.SR.GetResourceString("NodeUnableToConvertElement");
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002D41 File Offset: 0x00000F41
		internal static string NodeValueNotAllowed
		{
			get
			{
				return global::System.SR.GetResourceString("NodeValueNotAllowed");
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00002D4D File Offset: 0x00000F4D
		internal static string NodeWrongType
		{
			get
			{
				return global::System.SR.GetResourceString("NodeWrongType");
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002D59 File Offset: 0x00000F59
		internal static string NodeParentWrongType
		{
			get
			{
				return global::System.SR.GetResourceString("NodeParentWrongType");
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00002D65 File Offset: 0x00000F65
		internal static string NodeDuplicateKey
		{
			get
			{
				return global::System.SR.GetResourceString("NodeDuplicateKey");
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00002D71 File Offset: 0x00000F71
		internal static string SerializerContextOptionsReadOnly
		{
			get
			{
				return global::System.SR.GetResourceString("SerializerContextOptionsReadOnly");
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002D7D File Offset: 0x00000F7D
		internal static string ConverterForPropertyMustBeValid
		{
			get
			{
				return global::System.SR.GetResourceString("ConverterForPropertyMustBeValid");
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00002D89 File Offset: 0x00000F89
		internal static string NoMetadataForType
		{
			get
			{
				return global::System.SR.GetResourceString("NoMetadataForType");
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00002D95 File Offset: 0x00000F95
		internal static string AmbiguousMetadataForType
		{
			get
			{
				return global::System.SR.GetResourceString("AmbiguousMetadataForType");
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00002DA1 File Offset: 0x00000FA1
		internal static string CollectionIsReadOnly
		{
			get
			{
				return global::System.SR.GetResourceString("CollectionIsReadOnly");
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00002DAD File Offset: 0x00000FAD
		internal static string ArrayIndexNegative
		{
			get
			{
				return global::System.SR.GetResourceString("ArrayIndexNegative");
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00002DB9 File Offset: 0x00000FB9
		internal static string ArrayTooSmall
		{
			get
			{
				return global::System.SR.GetResourceString("ArrayTooSmall");
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00002DC5 File Offset: 0x00000FC5
		internal static string NodeJsonObjectCustomConverterNotAllowedOnExtensionProperty
		{
			get
			{
				return global::System.SR.GetResourceString("NodeJsonObjectCustomConverterNotAllowedOnExtensionProperty");
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00002DD1 File Offset: 0x00000FD1
		internal static string NoMetadataForTypeProperties
		{
			get
			{
				return global::System.SR.GetResourceString("NoMetadataForTypeProperties");
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00002DDD File Offset: 0x00000FDD
		internal static string FieldCannotBeVirtual
		{
			get
			{
				return global::System.SR.GetResourceString("FieldCannotBeVirtual");
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00002DE9 File Offset: 0x00000FE9
		internal static string MissingFSharpCoreMember
		{
			get
			{
				return global::System.SR.GetResourceString("MissingFSharpCoreMember");
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00002DF5 File Offset: 0x00000FF5
		internal static string FSharpDiscriminatedUnionsNotSupported
		{
			get
			{
				return global::System.SR.GetResourceString("FSharpDiscriminatedUnionsNotSupported");
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00002E01 File Offset: 0x00001001
		internal static string Polymorphism_BaseConverterDoesNotSupportMetadata
		{
			get
			{
				return global::System.SR.GetResourceString("Polymorphism_BaseConverterDoesNotSupportMetadata");
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002E0D File Offset: 0x0000100D
		internal static string Polymorphism_DerivedConverterDoesNotSupportMetadata
		{
			get
			{
				return global::System.SR.GetResourceString("Polymorphism_DerivedConverterDoesNotSupportMetadata");
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00002E19 File Offset: 0x00001019
		internal static string Polymorphism_TypeDoesNotSupportPolymorphism
		{
			get
			{
				return global::System.SR.GetResourceString("Polymorphism_TypeDoesNotSupportPolymorphism");
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002E25 File Offset: 0x00001025
		internal static string Polymorphism_DerivedTypeIsNotSupported
		{
			get
			{
				return global::System.SR.GetResourceString("Polymorphism_DerivedTypeIsNotSupported");
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00002E31 File Offset: 0x00001031
		internal static string Polymorphism_DerivedTypeIsAlreadySpecified
		{
			get
			{
				return global::System.SR.GetResourceString("Polymorphism_DerivedTypeIsAlreadySpecified");
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00002E3D File Offset: 0x0000103D
		internal static string Polymorphism_TypeDicriminatorIdIsAlreadySpecified
		{
			get
			{
				return global::System.SR.GetResourceString("Polymorphism_TypeDicriminatorIdIsAlreadySpecified");
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00002E49 File Offset: 0x00001049
		internal static string Polymorphism_InvalidCustomTypeDiscriminatorPropertyName
		{
			get
			{
				return global::System.SR.GetResourceString("Polymorphism_InvalidCustomTypeDiscriminatorPropertyName");
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00002E55 File Offset: 0x00001055
		internal static string Polymorphism_ConfigurationDoesNotSpecifyDerivedTypes
		{
			get
			{
				return global::System.SR.GetResourceString("Polymorphism_ConfigurationDoesNotSpecifyDerivedTypes");
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00002E61 File Offset: 0x00001061
		internal static string Polymorphism_UnrecognizedTypeDiscriminator
		{
			get
			{
				return global::System.SR.GetResourceString("Polymorphism_UnrecognizedTypeDiscriminator");
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00002E6D File Offset: 0x0000106D
		internal static string Polymorphism_RuntimeTypeNotSupported
		{
			get
			{
				return global::System.SR.GetResourceString("Polymorphism_RuntimeTypeNotSupported");
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00002E79 File Offset: 0x00001079
		internal static string Polymorphism_RuntimeTypeDiamondAmbiguity
		{
			get
			{
				return global::System.SR.GetResourceString("Polymorphism_RuntimeTypeDiamondAmbiguity");
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00002E85 File Offset: 0x00001085
		internal static string InvalidJsonTypeInfoOperationForKind
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidJsonTypeInfoOperationForKind");
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00002E91 File Offset: 0x00001091
		internal static string CreateObjectConverterNotCompatible
		{
			get
			{
				return global::System.SR.GetResourceString("CreateObjectConverterNotCompatible");
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00002E9D File Offset: 0x0000109D
		internal static string JsonPropertyInfoBoundToDifferentParent
		{
			get
			{
				return global::System.SR.GetResourceString("JsonPropertyInfoBoundToDifferentParent");
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00002EA9 File Offset: 0x000010A9
		internal static string JsonSerializerOptionsNoTypeInfoResolverSpecified
		{
			get
			{
				return global::System.SR.GetResourceString("JsonSerializerOptionsNoTypeInfoResolverSpecified");
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00002EB5 File Offset: 0x000010B5
		internal static string JsonSerializerIsReflectionDisabled
		{
			get
			{
				return global::System.SR.GetResourceString("JsonSerializerIsReflectionDisabled");
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002EC1 File Offset: 0x000010C1
		internal static string JsonPolymorphismOptionsAssociatedWithDifferentJsonTypeInfo
		{
			get
			{
				return global::System.SR.GetResourceString("JsonPolymorphismOptionsAssociatedWithDifferentJsonTypeInfo");
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00002ECD File Offset: 0x000010CD
		internal static string JsonPropertyRequiredAndNotDeserializable
		{
			get
			{
				return global::System.SR.GetResourceString("JsonPropertyRequiredAndNotDeserializable");
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00002ED9 File Offset: 0x000010D9
		internal static string JsonPropertyRequiredAndExtensionData
		{
			get
			{
				return global::System.SR.GetResourceString("JsonPropertyRequiredAndExtensionData");
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00002EE5 File Offset: 0x000010E5
		internal static string JsonRequiredPropertiesMissing
		{
			get
			{
				return global::System.SR.GetResourceString("JsonRequiredPropertiesMissing");
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002EF1 File Offset: 0x000010F1
		internal static string ObjectCreationHandlingPopulateNotSupportedByConverter
		{
			get
			{
				return global::System.SR.GetResourceString("ObjectCreationHandlingPopulateNotSupportedByConverter");
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00002EFD File Offset: 0x000010FD
		internal static string ObjectCreationHandlingPropertyMustHaveAGetter
		{
			get
			{
				return global::System.SR.GetResourceString("ObjectCreationHandlingPropertyMustHaveAGetter");
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00002F09 File Offset: 0x00001109
		internal static string ObjectCreationHandlingPropertyValueTypeMustHaveASetter
		{
			get
			{
				return global::System.SR.GetResourceString("ObjectCreationHandlingPropertyValueTypeMustHaveASetter");
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002F15 File Offset: 0x00001115
		internal static string ObjectCreationHandlingPropertyCannotAllowPolymorphicDeserialization
		{
			get
			{
				return global::System.SR.GetResourceString("ObjectCreationHandlingPropertyCannotAllowPolymorphicDeserialization");
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002F21 File Offset: 0x00001121
		internal static string ObjectCreationHandlingPropertyCannotAllowReadOnlyMember
		{
			get
			{
				return global::System.SR.GetResourceString("ObjectCreationHandlingPropertyCannotAllowReadOnlyMember");
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00002F2D File Offset: 0x0000112D
		internal static string ObjectCreationHandlingPropertyCannotAllowReferenceHandling
		{
			get
			{
				return global::System.SR.GetResourceString("ObjectCreationHandlingPropertyCannotAllowReferenceHandling");
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00002F39 File Offset: 0x00001139
		internal static string ObjectCreationHandlingPropertyDoesNotSupportParameterizedConstructors
		{
			get
			{
				return global::System.SR.GetResourceString("ObjectCreationHandlingPropertyDoesNotSupportParameterizedConstructors");
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00002F45 File Offset: 0x00001145
		internal static string FormatInt128
		{
			get
			{
				return global::System.SR.GetResourceString("FormatInt128");
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00002F51 File Offset: 0x00001151
		internal static string FormatUInt128
		{
			get
			{
				return global::System.SR.GetResourceString("FormatUInt128");
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00002F5D File Offset: 0x0000115D
		internal static string FormatHalf
		{
			get
			{
				return global::System.SR.GetResourceString("FormatHalf");
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002F6C File Offset: 0x0000116C
		// Note: this type is marked as 'beforefieldinit'.
		static SR()
		{
			bool flag;
			global::System.SR.s_usingResourceKeys = AppContext.TryGetSwitch("System.Resources.UseSystemResourceKeys", out flag) && flag;
		}

		// Token: 0x04000071 RID: 113
		private static readonly bool s_usingResourceKeys;

		// Token: 0x04000072 RID: 114
		private static ResourceManager s_resourceManager;
	}
}
