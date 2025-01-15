using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.OData.Core
{
	// Token: 0x02000122 RID: 290
	internal static class MediaTypeUtils
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x000281C1 File Offset: 0x000263C1
		internal static UTF8Encoding EncodingUtf8NoPreamble
		{
			get
			{
				return MediaTypeUtils.encodingUtf8NoPreamble;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x000281C8 File Offset: 0x000263C8
		internal static Encoding FallbackEncoding
		{
			get
			{
				return MediaTypeUtils.EncodingUtf8NoPreamble;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x000281CF File Offset: 0x000263CF
		internal static Encoding MissingEncoding
		{
			get
			{
				return Encoding.GetEncoding("ISO-8859-1", new EncoderExceptionFallback(), new DecoderExceptionFallback());
			}
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00028204 File Offset: 0x00026404
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

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000283BC File Offset: 0x000265BC
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

		// Token: 0x06000AEA RID: 2794 RVA: 0x000284A8 File Offset: 0x000266A8
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

		// Token: 0x06000AEB RID: 2795 RVA: 0x00028568 File Offset: 0x00026768
		internal static bool MediaTypeAndSubtypeAreEqual(string firstTypeAndSubtype, string secondTypeAndSubtype)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(firstTypeAndSubtype, "firstTypeAndSubtype");
			ExceptionUtils.CheckArgumentNotNull<string>(secondTypeAndSubtype, "secondTypeAndSubtype");
			return HttpUtils.CompareMediaTypeNames(firstTypeAndSubtype, secondTypeAndSubtype);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00028587 File Offset: 0x00026787
		internal static bool MediaTypeStartsWithTypeAndSubtype(string mediaType, string typeAndSubtype)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(mediaType, "mediaType");
			ExceptionUtils.CheckArgumentNotNull<string>(typeAndSubtype, "typeAndSubtype");
			return mediaType.StartsWith(typeAndSubtype, 5);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000285E0 File Offset: 0x000267E0
		internal static bool MediaTypeHasParameterWithValue(this ODataMediaType mediaType, string parameterName, string parameterValue)
		{
			return mediaType.Parameters != null && Enumerable.Any<KeyValuePair<string, string>>(mediaType.Parameters, (KeyValuePair<string, string> p) => HttpUtils.CompareMediaTypeParameterNames(p.Key, parameterName) && string.Compare(p.Value, parameterValue, 5) == 0);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00028622 File Offset: 0x00026822
		internal static bool HasStreamingSetToTrue(this ODataMediaType mediaType)
		{
			return mediaType.MediaTypeHasParameterWithValue("odata.streaming", "true");
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00028634 File Offset: 0x00026834
		internal static bool HasIeee754CompatibleSetToTrue(this ODataMediaType mediaType)
		{
			return mediaType.MediaTypeHasParameterWithValue("IEEE754Compatible", "true");
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00028646 File Offset: 0x00026846
		internal static void CheckMediaTypeForWildCards(ODataMediaType mediaType)
		{
			if (HttpUtils.CompareMediaTypeNames("*", mediaType.Type) || HttpUtils.CompareMediaTypeNames("*", mediaType.SubType))
			{
				throw new ODataContentTypeException(Strings.ODataMessageReader_WildcardInContentType(mediaType.FullTypeName));
			}
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00028680 File Offset: 0x00026880
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

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002873C File Offset: 0x0002693C
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

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00028854 File Offset: 0x00026A54
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

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000288A8 File Offset: 0x00026AA8
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

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000288F4 File Offset: 0x00026AF4
		private static Encoding GetEncoding(string acceptCharsetHeader, ODataPayloadKind payloadKind, ODataMediaType mediaType, bool useDefaultEncoding)
		{
			if (payloadKind == ODataPayloadKind.BinaryValue)
			{
				return null;
			}
			return HttpUtils.EncodingFromAcceptableCharsets(acceptCharsetHeader, mediaType, MediaTypeUtils.encodingUtf8NoPreamble, useDefaultEncoding ? MediaTypeUtils.encodingUtf8NoPreamble : null);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00028914 File Offset: 0x00026B14
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

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000289DC File Offset: 0x00026BDC
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
							goto IL_00F6;
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
				IL_00F6:;
			}
		}

		// Token: 0x04000466 RID: 1126
		private const int MatchInfoCacheMaxSize = 1024;

		// Token: 0x04000467 RID: 1127
		private static readonly ODataPayloadKind[] allSupportedPayloadKinds = new ODataPayloadKind[]
		{
			ODataPayloadKind.Feed,
			ODataPayloadKind.Entry,
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

		// Token: 0x04000468 RID: 1128
		private static readonly UTF8Encoding encodingUtf8NoPreamble = new UTF8Encoding(false, true);

		// Token: 0x04000469 RID: 1129
		private static MediaTypeUtils.MatchInfoConcurrentCache MatchInfoCache = new MediaTypeUtils.MatchInfoConcurrentCache(1024);

		// Token: 0x02000123 RID: 291
		private sealed class MediaTypeMatchInfo : IComparable<MediaTypeUtils.MediaTypeMatchInfo>
		{
			// Token: 0x06000B00 RID: 2816 RVA: 0x00028B70 File Offset: 0x00026D70
			public MediaTypeMatchInfo(ODataMediaType sourceType, ODataMediaType targetType, int sourceIndex, int targetIndex)
			{
				this.sourceIndex = sourceIndex;
				this.targetIndex = targetIndex;
				this.MatchTypes(sourceType, targetType);
			}

			// Token: 0x17000243 RID: 579
			// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00028B8F File Offset: 0x00026D8F
			public int SourceTypeIndex
			{
				get
				{
					return this.sourceIndex;
				}
			}

			// Token: 0x17000244 RID: 580
			// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00028B97 File Offset: 0x00026D97
			public int TargetTypeIndex
			{
				get
				{
					return this.targetIndex;
				}
			}

			// Token: 0x17000245 RID: 581
			// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00028B9F File Offset: 0x00026D9F
			// (set) Token: 0x06000B04 RID: 2820 RVA: 0x00028BA7 File Offset: 0x00026DA7
			public int MatchingTypeNamePartCount { get; private set; }

			// Token: 0x17000246 RID: 582
			// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00028BB0 File Offset: 0x00026DB0
			// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00028BB8 File Offset: 0x00026DB8
			public int MatchingParameterCount { get; private set; }

			// Token: 0x17000247 RID: 583
			// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00028BC1 File Offset: 0x00026DC1
			// (set) Token: 0x06000B08 RID: 2824 RVA: 0x00028BC9 File Offset: 0x00026DC9
			public int QualityValue { get; private set; }

			// Token: 0x17000248 RID: 584
			// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00028BD2 File Offset: 0x00026DD2
			// (set) Token: 0x06000B0A RID: 2826 RVA: 0x00028BDA File Offset: 0x00026DDA
			public int SourceTypeParameterCountForMatching { get; private set; }

			// Token: 0x17000249 RID: 585
			// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00028BE3 File Offset: 0x00026DE3
			public bool IsMatch
			{
				get
				{
					return this.QualityValue != 0 && this.MatchingTypeNamePartCount >= 0 && (this.MatchingTypeNamePartCount <= 1 || this.MatchingParameterCount == -1 || this.MatchingParameterCount >= this.SourceTypeParameterCountForMatching);
				}
			}

			// Token: 0x06000B0C RID: 2828 RVA: 0x00028C20 File Offset: 0x00026E20
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

			// Token: 0x06000B0D RID: 2829 RVA: 0x00028CA4 File Offset: 0x00026EA4
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

			// Token: 0x06000B0E RID: 2830 RVA: 0x00028CD0 File Offset: 0x00026ED0
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

			// Token: 0x06000B0F RID: 2831 RVA: 0x00028D21 File Offset: 0x00026F21
			private static bool IsQualityValueParameter(string parameterName)
			{
				return HttpUtils.CompareMediaTypeParameterNames("q", parameterName);
			}

			// Token: 0x06000B10 RID: 2832 RVA: 0x00028D30 File Offset: 0x00026F30
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
							this.MatchingParameterCount++;
						}
					}
				}
				if (!flag2 || this.SourceTypeParameterCountForMatching == 0 || this.MatchingParameterCount == this.SourceTypeParameterCountForMatching)
				{
					this.MatchingParameterCount = -1;
				}
			}

			// Token: 0x04000471 RID: 1137
			private const int DefaultQualityValue = 1000;

			// Token: 0x04000472 RID: 1138
			private readonly int sourceIndex;

			// Token: 0x04000473 RID: 1139
			private readonly int targetIndex;
		}

		// Token: 0x02000124 RID: 292
		private sealed class MatchInfoCacheKey
		{
			// Token: 0x06000B11 RID: 2833 RVA: 0x00028EDA File Offset: 0x000270DA
			public MatchInfoCacheKey(ODataMediaTypeResolver resolver, ODataPayloadKind payloadKind, string contentTypeName)
			{
				this.MediaTypeResolver = resolver;
				this.PayloadKind = payloadKind;
				this.ContentTypeName = contentTypeName;
			}

			// Token: 0x1700024A RID: 586
			// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00028EF7 File Offset: 0x000270F7
			// (set) Token: 0x06000B13 RID: 2835 RVA: 0x00028EFF File Offset: 0x000270FF
			private ODataMediaTypeResolver MediaTypeResolver { get; set; }

			// Token: 0x1700024B RID: 587
			// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00028F08 File Offset: 0x00027108
			// (set) Token: 0x06000B15 RID: 2837 RVA: 0x00028F10 File Offset: 0x00027110
			private ODataPayloadKind PayloadKind { get; set; }

			// Token: 0x1700024C RID: 588
			// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00028F19 File Offset: 0x00027119
			// (set) Token: 0x06000B17 RID: 2839 RVA: 0x00028F21 File Offset: 0x00027121
			private string ContentTypeName { get; set; }

			// Token: 0x06000B18 RID: 2840 RVA: 0x00028F2C File Offset: 0x0002712C
			public override bool Equals(object obj)
			{
				MediaTypeUtils.MatchInfoCacheKey matchInfoCacheKey = obj as MediaTypeUtils.MatchInfoCacheKey;
				return matchInfoCacheKey != null && this.MediaTypeResolver == matchInfoCacheKey.MediaTypeResolver && this.PayloadKind == matchInfoCacheKey.PayloadKind && this.ContentTypeName == matchInfoCacheKey.ContentTypeName;
			}

			// Token: 0x06000B19 RID: 2841 RVA: 0x00028F74 File Offset: 0x00027174
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

		// Token: 0x02000125 RID: 293
		private sealed class MatchInfoConcurrentCache
		{
			// Token: 0x06000B1A RID: 2842 RVA: 0x00028FB5 File Offset: 0x000271B5
			public MatchInfoConcurrentCache(int maxSize)
			{
				this.maxSize = maxSize;
				this.dict = new Dictionary<MediaTypeUtils.MatchInfoCacheKey, MediaTypeUtils.MediaTypeMatchInfo>(maxSize + 1);
			}

			// Token: 0x06000B1B RID: 2843 RVA: 0x00028FD4 File Offset: 0x000271D4
			public bool TryGetValue(MediaTypeUtils.MatchInfoCacheKey key, out MediaTypeUtils.MediaTypeMatchInfo value)
			{
				bool flag;
				lock (this.dict)
				{
					flag = this.dict.TryGetValue(key, ref value);
				}
				return flag;
			}

			// Token: 0x06000B1C RID: 2844 RVA: 0x00029018 File Offset: 0x00027218
			public void Add(MediaTypeUtils.MatchInfoCacheKey key, MediaTypeUtils.MediaTypeMatchInfo value)
			{
				lock (this.dict)
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

			// Token: 0x0400047B RID: 1147
			private readonly int maxSize;

			// Token: 0x0400047C RID: 1148
			private IDictionary<MediaTypeUtils.MatchInfoCacheKey, MediaTypeUtils.MediaTypeMatchInfo> dict;
		}
	}
}
