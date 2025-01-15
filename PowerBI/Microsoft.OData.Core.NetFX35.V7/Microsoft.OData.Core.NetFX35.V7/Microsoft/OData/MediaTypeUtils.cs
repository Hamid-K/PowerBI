using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.OData
{
	// Token: 0x02000023 RID: 35
	internal static class MediaTypeUtils
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000475D File Offset: 0x0000295D
		internal static UTF8Encoding EncodingUtf8NoPreamble
		{
			get
			{
				return MediaTypeUtils.encodingUtf8NoPreamble;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004764 File Offset: 0x00002964
		internal static Encoding FallbackEncoding
		{
			get
			{
				return MediaTypeUtils.EncodingUtf8NoPreamble;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000476B File Offset: 0x0000296B
		internal static Encoding MissingEncoding
		{
			get
			{
				return Encoding.GetEncoding("ISO-8859-1", new EncoderExceptionFallback(), new DecoderExceptionFallback());
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004784 File Offset: 0x00002984
		internal static ODataFormat GetContentTypeFromSettings(ODataMessageWriterSettings settings, ODataPayloadKind payloadKind, ODataMediaTypeResolver mediaTypeResolver, out ODataMediaType mediaType, out Encoding encoding)
		{
			IList<ODataMediaTypeFormat> list = Enumerable.ToList<ODataMediaTypeFormat>(mediaTypeResolver.GetMediaTypeFormats(payloadKind));
			if (list == null || list.Count == 0)
			{
				throw new ODataContentTypeException(Strings.MediaTypeUtils_DidNotFindMatchingMediaType(null, settings.AcceptableMediaTypes));
			}
			ODataFormat format;
			if (settings.UseFormat == true)
			{
				mediaType = MediaTypeUtils.GetDefaultMediaType(list, settings.Format, out format);
				encoding = mediaType.SelectEncoding();
			}
			else
			{
				IList<KeyValuePair<ODataMediaType, string>> list2 = HttpUtils.MediaTypesFromString(settings.AcceptableMediaTypes);
				MediaTypeUtils.ConvertApplicationJsonInAcceptableMediaTypes(list2);
				string text = null;
				ODataMediaTypeFormat odataMediaTypeFormat;
				if (list2 == null || list2.Count == 0)
				{
					odataMediaTypeFormat = list[0];
				}
				else
				{
					MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo = MediaTypeUtils.MatchMediaTypes(Enumerable.Select<KeyValuePair<ODataMediaType, string>, ODataMediaType>(list2, (KeyValuePair<ODataMediaType, string> kvp) => kvp.Key), Enumerable.ToArray<ODataMediaType>(Enumerable.Select<ODataMediaTypeFormat, ODataMediaType>(list, (ODataMediaTypeFormat smt) => smt.MediaType)));
					if (mediaTypeMatchInfo == null)
					{
						string text2 = string.Join(", ", Enumerable.ToArray<string>(Enumerable.Select<ODataMediaTypeFormat, string>(list, (ODataMediaTypeFormat mt) => mt.MediaType.ToText())));
						throw new ODataContentTypeException(Strings.MediaTypeUtils_DidNotFindMatchingMediaType(text2, settings.AcceptableMediaTypes));
					}
					odataMediaTypeFormat = list[mediaTypeMatchInfo.TargetTypeIndex];
					text = list2[mediaTypeMatchInfo.SourceTypeIndex].Value;
				}
				format = odataMediaTypeFormat.Format;
				mediaType = odataMediaTypeFormat.MediaType;
				string text3 = settings.AcceptableCharsets;
				if (text != null)
				{
					text3 = ((text3 == null) ? text : (text + "," + text3));
				}
				encoding = MediaTypeUtils.GetEncoding(text3, payloadKind, mediaType, true);
			}
			return format;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000493C File Offset: 0x00002B3C
		internal static ODataFormat GetFormatFromContentType(string contentTypeHeader, ODataPayloadKind[] supportedPayloadKinds, ODataMediaTypeResolver mediaTypeResolver, out ODataMediaType mediaType, out Encoding encoding, out ODataPayloadKind selectedPayloadKind, out string batchBoundary)
		{
			ODataFormat formatFromContentType = MediaTypeUtils.GetFormatFromContentType(contentTypeHeader, supportedPayloadKinds, mediaTypeResolver, out mediaType, out encoding, out selectedPayloadKind);
			if (selectedPayloadKind == ODataPayloadKind.Batch)
			{
				KeyValuePair<string, string> keyValuePair = default(KeyValuePair<string, string>);
				IEnumerable<KeyValuePair<string, string>> parameters = mediaType.Parameters;
				if (parameters != null)
				{
					bool flag = false;
					foreach (KeyValuePair<string, string> keyValuePair2 in Enumerable.Where<KeyValuePair<string, string>>(parameters, (KeyValuePair<string, string> p) => HttpUtils.CompareMediaTypeParameterNames("boundary", p.Key)))
					{
						if (flag)
						{
							throw new ODataException(Strings.MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads(contentTypeHeader, "boundary"));
						}
						keyValuePair = keyValuePair2;
						flag = true;
					}
				}
				if (keyValuePair.Key == null)
				{
					throw new ODataException(Strings.MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads(contentTypeHeader, "boundary"));
				}
				batchBoundary = keyValuePair.Value;
				ValidationUtils.ValidateBoundaryString(batchBoundary);
			}
			else
			{
				batchBoundary = null;
			}
			return formatFromContentType;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004A20 File Offset: 0x00002C20
		internal static IList<ODataPayloadKindDetectionResult> GetPayloadKindsForContentType(string contentTypeHeader, ODataMediaTypeResolver mediaTypeResolver, out ODataMediaType contentType, out Encoding encoding)
		{
			encoding = null;
			string text;
			contentType = MediaTypeUtils.ParseContentType(contentTypeHeader, out text);
			ODataMediaType[] array = new ODataMediaType[] { contentType };
			List<ODataPayloadKindDetectionResult> list = new List<ODataPayloadKindDetectionResult>();
			for (int i = 0; i < MediaTypeUtils.allSupportedPayloadKinds.Length; i++)
			{
				ODataPayloadKind odataPayloadKind = MediaTypeUtils.allSupportedPayloadKinds[i];
				IList<ODataMediaTypeFormat> list2 = Enumerable.ToList<ODataMediaTypeFormat>(mediaTypeResolver.GetMediaTypeFormats(odataPayloadKind));
				MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo = MediaTypeUtils.MatchMediaTypes(Enumerable.Select<ODataMediaTypeFormat, ODataMediaType>(list2, (ODataMediaTypeFormat smt) => smt.MediaType), array);
				if (mediaTypeMatchInfo != null)
				{
					list.Add(new ODataPayloadKindDetectionResult(odataPayloadKind, list2[mediaTypeMatchInfo.SourceTypeIndex].Format));
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				encoding = HttpUtils.GetEncodingFromCharsetName(text);
			}
			return list;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004ADD File Offset: 0x00002CDD
		internal static bool MediaTypeAndSubtypeAreEqual(string firstTypeAndSubtype, string secondTypeAndSubtype)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(firstTypeAndSubtype, "firstTypeAndSubtype");
			ExceptionUtils.CheckArgumentNotNull<string>(secondTypeAndSubtype, "secondTypeAndSubtype");
			return HttpUtils.CompareMediaTypeNames(firstTypeAndSubtype, secondTypeAndSubtype);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004AFE File Offset: 0x00002CFE
		internal static bool MediaTypeStartsWithTypeAndSubtype(string mediaType, string typeAndSubtype)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(mediaType, "mediaType");
			ExceptionUtils.CheckArgumentNotNull<string>(typeAndSubtype, "typeAndSubtype");
			return mediaType.StartsWith(typeAndSubtype, 5);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004B20 File Offset: 0x00002D20
		internal static bool MediaTypeHasParameterWithValue(this ODataMediaType mediaType, string parameterName, string parameterValue)
		{
			return mediaType.Parameters != null && Enumerable.Any<KeyValuePair<string, string>>(mediaType.Parameters, (KeyValuePair<string, string> p) => HttpUtils.CompareMediaTypeParameterNames(p.Key, parameterName) && string.Compare(p.Value, parameterValue, 5) == 0);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004B62 File Offset: 0x00002D62
		internal static bool HasStreamingSetToTrue(this ODataMediaType mediaType)
		{
			return mediaType.MediaTypeHasParameterWithValue("odata.streaming", "true");
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004B74 File Offset: 0x00002D74
		internal static bool HasIeee754CompatibleSetToTrue(this ODataMediaType mediaType)
		{
			return mediaType.MediaTypeHasParameterWithValue("IEEE754Compatible", "true");
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004B86 File Offset: 0x00002D86
		internal static void CheckMediaTypeForWildCards(ODataMediaType mediaType)
		{
			if (HttpUtils.CompareMediaTypeNames("*", mediaType.Type) || HttpUtils.CompareMediaTypeNames("*", mediaType.SubType))
			{
				throw new ODataContentTypeException(Strings.ODataMessageReader_WildcardInContentType(mediaType.FullTypeName));
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004BC0 File Offset: 0x00002DC0
		internal static string AlterContentTypeForJsonPadding(string contentType)
		{
			if (contentType.StartsWith("application/json", 5))
			{
				return contentType.Remove(0, "application/json".Length).Insert(0, "text/javascript");
			}
			if (contentType.StartsWith("text/plain", 5))
			{
				return contentType.Remove(0, "text/plain".Length).Insert(0, "text/javascript");
			}
			throw new ODataException(Strings.ODataMessageWriter_JsonPaddingOnInvalidContentType(contentType));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004C30 File Offset: 0x00002E30
		private static ODataFormat GetFormatFromContentType(string contentTypeName, ODataPayloadKind[] supportedPayloadKinds, ODataMediaTypeResolver mediaTypeResolver, out ODataMediaType mediaType, out Encoding encoding, out ODataPayloadKind selectedPayloadKind)
		{
			string text;
			mediaType = MediaTypeUtils.ParseContentType(contentTypeName, out text);
			foreach (ODataPayloadKind odataPayloadKind in supportedPayloadKinds)
			{
				IList<ODataMediaTypeFormat> list = Enumerable.ToList<ODataMediaTypeFormat>(mediaTypeResolver.GetMediaTypeFormats(odataPayloadKind));
				MediaTypeUtils.MatchInfoCacheKey matchInfoCacheKey = new MediaTypeUtils.MatchInfoCacheKey(mediaTypeResolver, odataPayloadKind, contentTypeName);
				MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo;
				if (!MediaTypeUtils.MatchInfoCache.TryGetValue(matchInfoCacheKey, out mediaTypeMatchInfo))
				{
					mediaTypeMatchInfo = MediaTypeUtils.MatchMediaTypes(Enumerable.Select<ODataMediaTypeFormat, ODataMediaType>(list, (ODataMediaTypeFormat smt) => smt.MediaType), new ODataMediaType[] { mediaType });
					MediaTypeUtils.MatchInfoCache.Add(matchInfoCacheKey, mediaTypeMatchInfo);
				}
				if (mediaTypeMatchInfo != null)
				{
					selectedPayloadKind = odataPayloadKind;
					encoding = MediaTypeUtils.GetEncoding(text, selectedPayloadKind, mediaType, false);
					return list[mediaTypeMatchInfo.SourceTypeIndex].Format;
				}
			}
			string text2 = string.Join(", ", Enumerable.ToArray<string>(Enumerable.SelectMany<ODataPayloadKind, string>(supportedPayloadKinds, (ODataPayloadKind pk) => Enumerable.Select<ODataMediaTypeFormat, string>(mediaTypeResolver.GetMediaTypeFormats(pk), (ODataMediaTypeFormat mt) => mt.MediaType.ToText()))));
			throw new ODataContentTypeException(Strings.MediaTypeUtils_CannotDetermineFormatFromContentType(text2, contentTypeName));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004D48 File Offset: 0x00002F48
		private static ODataMediaType ParseContentType(string contentTypeHeader, out string charset)
		{
			IList<KeyValuePair<ODataMediaType, string>> list = HttpUtils.MediaTypesFromString(contentTypeHeader);
			if (list.Count != 1)
			{
				throw new ODataContentTypeException(Strings.MediaTypeUtils_NoOrMoreThanOneContentTypeSpecified(contentTypeHeader));
			}
			ODataMediaType key = list[0].Key;
			MediaTypeUtils.CheckMediaTypeForWildCards(key);
			charset = list[0].Value;
			return key;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004D9C File Offset: 0x00002F9C
		private static ODataMediaType GetDefaultMediaType(IList<ODataMediaTypeFormat> supportedMediaTypes, ODataFormat specifiedFormat, out ODataFormat actualFormat)
		{
			for (int i = 0; i < supportedMediaTypes.Count; i++)
			{
				ODataMediaTypeFormat odataMediaTypeFormat = supportedMediaTypes[i];
				if (specifiedFormat == null || odataMediaTypeFormat.Format == specifiedFormat)
				{
					actualFormat = odataMediaTypeFormat.Format;
					return odataMediaTypeFormat.MediaType;
				}
			}
			throw new ODataException(Strings.ODataUtils_DidNotFindDefaultMediaType(specifiedFormat));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004DE8 File Offset: 0x00002FE8
		private static Encoding GetEncoding(string acceptCharsetHeader, ODataPayloadKind payloadKind, ODataMediaType mediaType, bool useDefaultEncoding)
		{
			if (payloadKind == ODataPayloadKind.BinaryValue)
			{
				return null;
			}
			return HttpUtils.EncodingFromAcceptableCharsets(acceptCharsetHeader, mediaType, MediaTypeUtils.encodingUtf8NoPreamble, useDefaultEncoding ? MediaTypeUtils.encodingUtf8NoPreamble : null);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004E08 File Offset: 0x00003008
		private static MediaTypeUtils.MediaTypeMatchInfo MatchMediaTypes(IEnumerable<ODataMediaType> sourceTypes, ODataMediaType[] targetTypes)
		{
			MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo = null;
			int num = 0;
			if (sourceTypes != null)
			{
				foreach (ODataMediaType odataMediaType in sourceTypes)
				{
					int num2 = 0;
					foreach (ODataMediaType odataMediaType2 in targetTypes)
					{
						MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo2 = new MediaTypeUtils.MediaTypeMatchInfo(odataMediaType, odataMediaType2, num, num2);
						if (!mediaTypeMatchInfo2.IsMatch)
						{
							num2++;
						}
						else
						{
							if (mediaTypeMatchInfo == null)
							{
								mediaTypeMatchInfo = mediaTypeMatchInfo2;
							}
							else
							{
								int num3 = mediaTypeMatchInfo.CompareTo(mediaTypeMatchInfo2);
								if (num3 < 0)
								{
									mediaTypeMatchInfo = mediaTypeMatchInfo2;
								}
							}
							num2++;
						}
					}
					num++;
				}
			}
			if (mediaTypeMatchInfo == null)
			{
				return null;
			}
			return mediaTypeMatchInfo;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004EBC File Offset: 0x000030BC
		private static void ConvertApplicationJsonInAcceptableMediaTypes(IList<KeyValuePair<ODataMediaType, string>> specifiedTypes)
		{
			if (specifiedTypes == null)
			{
				return;
			}
			for (int i = 0; i < specifiedTypes.Count; i++)
			{
				ODataMediaType key = specifiedTypes[i].Key;
				if (HttpUtils.CompareMediaTypeNames(key.SubType, "json") && HttpUtils.CompareMediaTypeNames(key.Type, "application"))
				{
					if (key.Parameters != null)
					{
						if (Enumerable.Any<KeyValuePair<string, string>>(key.Parameters, (KeyValuePair<string, string> p) => HttpUtils.CompareMediaTypeParameterNames(p.Key, "odata.metadata")))
						{
							goto IL_00F8;
						}
					}
					IList<KeyValuePair<string, string>> list = ((key.Parameters != null) ? Enumerable.ToList<KeyValuePair<string, string>>(key.Parameters) : null);
					int num = ((list == null) ? 1 : (list.Count + 1));
					List<KeyValuePair<string, string>> list2 = new List<KeyValuePair<string, string>>(num);
					list2.Add(new KeyValuePair<string, string>("odata.metadata", "minimal"));
					if (list != null)
					{
						list2.AddRange(list);
					}
					specifiedTypes[i] = new KeyValuePair<ODataMediaType, string>(new ODataMediaType(key.Type, key.SubType, list2), specifiedTypes[i].Value);
				}
				IL_00F8:;
			}
		}

		// Token: 0x04000095 RID: 149
		private static readonly ODataPayloadKind[] allSupportedPayloadKinds = new ODataPayloadKind[]
		{
			ODataPayloadKind.ResourceSet,
			ODataPayloadKind.Resource,
			ODataPayloadKind.Property,
			ODataPayloadKind.MetadataDocument,
			ODataPayloadKind.ServiceDocument,
			ODataPayloadKind.Value,
			ODataPayloadKind.BinaryValue,
			ODataPayloadKind.Collection,
			ODataPayloadKind.EntityReferenceLinks,
			ODataPayloadKind.EntityReferenceLink,
			ODataPayloadKind.Batch,
			ODataPayloadKind.Error,
			ODataPayloadKind.Parameter,
			ODataPayloadKind.IndividualProperty,
			ODataPayloadKind.Delta,
			ODataPayloadKind.Asynchronous
		};

		// Token: 0x04000096 RID: 150
		private static readonly UTF8Encoding encodingUtf8NoPreamble = new UTF8Encoding(false, true);

		// Token: 0x04000097 RID: 151
		private const int MatchInfoCacheMaxSize = 1024;

		// Token: 0x04000098 RID: 152
		private static MediaTypeUtils.MatchInfoConcurrentCache MatchInfoCache = new MediaTypeUtils.MatchInfoConcurrentCache(1024);

		// Token: 0x02000245 RID: 581
		private sealed class MediaTypeMatchInfo : IComparable<MediaTypeUtils.MediaTypeMatchInfo>
		{
			// Token: 0x06001715 RID: 5909 RVA: 0x00046A9D File Offset: 0x00044C9D
			public MediaTypeMatchInfo(ODataMediaType sourceType, ODataMediaType targetType, int sourceIndex, int targetIndex)
			{
				this.sourceIndex = sourceIndex;
				this.targetIndex = targetIndex;
				this.MatchTypes(sourceType, targetType);
			}

			// Token: 0x1700052D RID: 1325
			// (get) Token: 0x06001716 RID: 5910 RVA: 0x00046ABC File Offset: 0x00044CBC
			public int SourceTypeIndex
			{
				get
				{
					return this.sourceIndex;
				}
			}

			// Token: 0x1700052E RID: 1326
			// (get) Token: 0x06001717 RID: 5911 RVA: 0x00046AC4 File Offset: 0x00044CC4
			public int TargetTypeIndex
			{
				get
				{
					return this.targetIndex;
				}
			}

			// Token: 0x1700052F RID: 1327
			// (get) Token: 0x06001718 RID: 5912 RVA: 0x00046ACC File Offset: 0x00044CCC
			// (set) Token: 0x06001719 RID: 5913 RVA: 0x00046AD4 File Offset: 0x00044CD4
			public int MatchingTypeNamePartCount { get; private set; }

			// Token: 0x17000530 RID: 1328
			// (get) Token: 0x0600171A RID: 5914 RVA: 0x00046ADD File Offset: 0x00044CDD
			// (set) Token: 0x0600171B RID: 5915 RVA: 0x00046AE5 File Offset: 0x00044CE5
			public int MatchingParameterCount { get; private set; }

			// Token: 0x17000531 RID: 1329
			// (get) Token: 0x0600171C RID: 5916 RVA: 0x00046AEE File Offset: 0x00044CEE
			// (set) Token: 0x0600171D RID: 5917 RVA: 0x00046AF6 File Offset: 0x00044CF6
			public int QualityValue { get; private set; }

			// Token: 0x17000532 RID: 1330
			// (get) Token: 0x0600171E RID: 5918 RVA: 0x00046AFF File Offset: 0x00044CFF
			// (set) Token: 0x0600171F RID: 5919 RVA: 0x00046B07 File Offset: 0x00044D07
			public int SourceTypeParameterCountForMatching { get; private set; }

			// Token: 0x17000533 RID: 1331
			// (get) Token: 0x06001720 RID: 5920 RVA: 0x00046B10 File Offset: 0x00044D10
			public bool IsMatch
			{
				get
				{
					return this.QualityValue != 0 && this.MatchingTypeNamePartCount >= 0 && (this.MatchingTypeNamePartCount <= 1 || this.MatchingParameterCount == -1 || this.MatchingParameterCount >= this.SourceTypeParameterCountForMatching);
				}
			}

			// Token: 0x06001721 RID: 5921 RVA: 0x00046B4C File Offset: 0x00044D4C
			public int CompareTo(MediaTypeUtils.MediaTypeMatchInfo other)
			{
				ExceptionUtils.CheckArgumentNotNull<MediaTypeUtils.MediaTypeMatchInfo>(other, "other");
				if (this.MatchingTypeNamePartCount > other.MatchingTypeNamePartCount)
				{
					return 1;
				}
				if (this.MatchingTypeNamePartCount == other.MatchingTypeNamePartCount)
				{
					if (this.MatchingParameterCount > other.MatchingParameterCount)
					{
						return 1;
					}
					if (this.MatchingParameterCount == other.MatchingParameterCount)
					{
						int num = this.QualityValue.CompareTo(other.QualityValue);
						if (num != 0)
						{
							return num;
						}
						if (other.TargetTypeIndex >= this.TargetTypeIndex)
						{
							return 1;
						}
						return -1;
					}
				}
				return -1;
			}

			// Token: 0x06001722 RID: 5922 RVA: 0x00046BD0 File Offset: 0x00044DD0
			private static int ParseQualityValue(string qualityValueText)
			{
				int num = 1000;
				if (qualityValueText.Length > 0)
				{
					int num2 = 0;
					HttpUtils.ReadQualityValue(qualityValueText, ref num2, out num);
				}
				return num;
			}

			// Token: 0x06001723 RID: 5923 RVA: 0x00046BFC File Offset: 0x00044DFC
			private static bool TryFindMediaTypeParameter(IList<KeyValuePair<string, string>> parameters, string parameterName, out string parameterValue)
			{
				parameterValue = null;
				if (parameters != null)
				{
					for (int i = 0; i < parameters.Count; i++)
					{
						string key = parameters[i].Key;
						if (HttpUtils.CompareMediaTypeParameterNames(parameterName, key))
						{
							parameterValue = parameters[i].Value;
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x06001724 RID: 5924 RVA: 0x00046C4D File Offset: 0x00044E4D
			private static bool IsQualityValueParameter(string parameterName)
			{
				return HttpUtils.CompareMediaTypeParameterNames("q", parameterName);
			}

			// Token: 0x06001725 RID: 5925 RVA: 0x00046C5C File Offset: 0x00044E5C
			private void MatchTypes(ODataMediaType sourceType, ODataMediaType targetType)
			{
				this.MatchingTypeNamePartCount = -1;
				if (sourceType.Type == "*")
				{
					this.MatchingTypeNamePartCount = 0;
				}
				else if (HttpUtils.CompareMediaTypeNames(sourceType.Type, targetType.Type))
				{
					if (sourceType.SubType == "*")
					{
						this.MatchingTypeNamePartCount = 1;
					}
					else if (HttpUtils.CompareMediaTypeNames(sourceType.SubType, targetType.SubType))
					{
						this.MatchingTypeNamePartCount = 2;
					}
				}
				this.QualityValue = 1000;
				this.SourceTypeParameterCountForMatching = 0;
				this.MatchingParameterCount = 0;
				IList<KeyValuePair<string, string>> list = ((sourceType.Parameters != null) ? Enumerable.ToList<KeyValuePair<string, string>>(sourceType.Parameters) : null);
				IList<KeyValuePair<string, string>> list2 = ((targetType.Parameters != null) ? Enumerable.ToList<KeyValuePair<string, string>>(targetType.Parameters) : null);
				bool flag = list2 != null && list2.Count > 0;
				bool flag2 = list != null && list.Count > 0;
				if (flag2)
				{
					for (int i = 0; i < list.Count; i++)
					{
						string key = list[i].Key;
						if (MediaTypeUtils.MediaTypeMatchInfo.IsQualityValueParameter(key))
						{
							this.QualityValue = MediaTypeUtils.MediaTypeMatchInfo.ParseQualityValue(list[i].Value.Trim());
							break;
						}
						this.SourceTypeParameterCountForMatching = i + 1;
						string text;
						if (flag && MediaTypeUtils.MediaTypeMatchInfo.TryFindMediaTypeParameter(list2, key, out text) && string.Compare(list[i].Value.Trim(), text.Trim(), 5) == 0)
						{
							int matchingParameterCount = this.MatchingParameterCount;
							this.MatchingParameterCount = matchingParameterCount + 1;
						}
					}
				}
				if (!flag2 || this.SourceTypeParameterCountForMatching == 0 || this.MatchingParameterCount == this.SourceTypeParameterCountForMatching)
				{
					this.MatchingParameterCount = -1;
				}
			}

			// Token: 0x04000AA8 RID: 2728
			private const int DefaultQualityValue = 1000;

			// Token: 0x04000AA9 RID: 2729
			private readonly int sourceIndex;

			// Token: 0x04000AAA RID: 2730
			private readonly int targetIndex;
		}

		// Token: 0x02000246 RID: 582
		private sealed class MatchInfoCacheKey
		{
			// Token: 0x06001726 RID: 5926 RVA: 0x00046E0A File Offset: 0x0004500A
			public MatchInfoCacheKey(ODataMediaTypeResolver resolver, ODataPayloadKind payloadKind, string contentTypeName)
			{
				this.MediaTypeResolver = resolver;
				this.PayloadKind = payloadKind;
				this.ContentTypeName = contentTypeName;
			}

			// Token: 0x17000534 RID: 1332
			// (get) Token: 0x06001727 RID: 5927 RVA: 0x00046E27 File Offset: 0x00045027
			// (set) Token: 0x06001728 RID: 5928 RVA: 0x00046E2F File Offset: 0x0004502F
			private ODataMediaTypeResolver MediaTypeResolver { get; set; }

			// Token: 0x17000535 RID: 1333
			// (get) Token: 0x06001729 RID: 5929 RVA: 0x00046E38 File Offset: 0x00045038
			// (set) Token: 0x0600172A RID: 5930 RVA: 0x00046E40 File Offset: 0x00045040
			private ODataPayloadKind PayloadKind { get; set; }

			// Token: 0x17000536 RID: 1334
			// (get) Token: 0x0600172B RID: 5931 RVA: 0x00046E49 File Offset: 0x00045049
			// (set) Token: 0x0600172C RID: 5932 RVA: 0x00046E51 File Offset: 0x00045051
			private string ContentTypeName { get; set; }

			// Token: 0x0600172D RID: 5933 RVA: 0x00046E5C File Offset: 0x0004505C
			public override bool Equals(object obj)
			{
				MediaTypeUtils.MatchInfoCacheKey matchInfoCacheKey = obj as MediaTypeUtils.MatchInfoCacheKey;
				return matchInfoCacheKey != null && this.MediaTypeResolver == matchInfoCacheKey.MediaTypeResolver && this.PayloadKind == matchInfoCacheKey.PayloadKind && this.ContentTypeName == matchInfoCacheKey.ContentTypeName;
			}

			// Token: 0x0600172E RID: 5934 RVA: 0x00046EA4 File Offset: 0x000450A4
			public override int GetHashCode()
			{
				int num = this.MediaTypeResolver.GetHashCode() ^ this.PayloadKind.GetHashCode();
				if (this.ContentTypeName == null)
				{
					return num;
				}
				return num ^ this.ContentTypeName.GetHashCode();
			}
		}

		// Token: 0x02000247 RID: 583
		private sealed class MatchInfoConcurrentCache
		{
			// Token: 0x0600172F RID: 5935 RVA: 0x00046EE9 File Offset: 0x000450E9
			public MatchInfoConcurrentCache(int maxSize)
			{
				this.maxSize = maxSize;
				this.dict = new Dictionary<MediaTypeUtils.MatchInfoCacheKey, MediaTypeUtils.MediaTypeMatchInfo>(maxSize + 1);
			}

			// Token: 0x06001730 RID: 5936 RVA: 0x00046F08 File Offset: 0x00045108
			public bool TryGetValue(MediaTypeUtils.MatchInfoCacheKey key, out MediaTypeUtils.MediaTypeMatchInfo value)
			{
				IDictionary<MediaTypeUtils.MatchInfoCacheKey, MediaTypeUtils.MediaTypeMatchInfo> dictionary = this.dict;
				bool flag;
				lock (dictionary)
				{
					flag = this.dict.TryGetValue(key, ref value);
				}
				return flag;
			}

			// Token: 0x06001731 RID: 5937 RVA: 0x00046F4C File Offset: 0x0004514C
			public void Add(MediaTypeUtils.MatchInfoCacheKey key, MediaTypeUtils.MediaTypeMatchInfo value)
			{
				IDictionary<MediaTypeUtils.MatchInfoCacheKey, MediaTypeUtils.MediaTypeMatchInfo> dictionary = this.dict;
				lock (dictionary)
				{
					if (!this.dict.ContainsKey(key))
					{
						if (this.dict.Count == this.maxSize)
						{
							this.dict.Clear();
						}
						this.dict.Add(key, value);
					}
				}
			}

			// Token: 0x04000AB2 RID: 2738
			private readonly int maxSize;

			// Token: 0x04000AB3 RID: 2739
			private IDictionary<MediaTypeUtils.MatchInfoCacheKey, MediaTypeUtils.MediaTypeMatchInfo> dict;
		}
	}
}
