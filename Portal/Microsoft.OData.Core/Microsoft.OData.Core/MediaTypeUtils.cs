using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.OData
{
	// Token: 0x0200004B RID: 75
	internal static class MediaTypeUtils
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000072B7 File Offset: 0x000054B7
		internal static UTF8Encoding EncodingUtf8NoPreamble
		{
			get
			{
				return MediaTypeUtils.encodingUtf8NoPreamble;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000254 RID: 596 RVA: 0x000072BE File Offset: 0x000054BE
		internal static Encoding FallbackEncoding
		{
			get
			{
				return MediaTypeUtils.EncodingUtf8NoPreamble;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000072C5 File Offset: 0x000054C5
		internal static Encoding MissingEncoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000072CC File Offset: 0x000054CC
		internal static ODataFormat GetContentTypeFromSettings(ODataMessageWriterSettings settings, ODataPayloadKind payloadKind, ODataMediaTypeResolver mediaTypeResolver, out ODataMediaType mediaType, out Encoding encoding)
		{
			IList<ODataMediaTypeFormat> list = mediaTypeResolver.GetMediaTypeFormats(payloadKind).ToList<ODataMediaTypeFormat>();
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
				MediaTypeUtils.ConvertApplicationJsonInAcceptableMediaTypes(list2, settings.Version ?? ODataVersion.V4);
				string text = null;
				ODataMediaTypeFormat odataMediaTypeFormat;
				if (list2 == null || list2.Count == 0)
				{
					odataMediaTypeFormat = list[0];
				}
				else
				{
					MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo = MediaTypeUtils.MatchMediaTypes(list2.Select((KeyValuePair<ODataMediaType, string> kvp) => kvp.Key), list.Select((ODataMediaTypeFormat smt) => smt.MediaType).ToArray<ODataMediaType>());
					if (mediaTypeMatchInfo == null)
					{
						string text2 = string.Join(", ", list.Select((ODataMediaTypeFormat mt) => mt.MediaType.ToText()).ToArray<string>());
						throw new ODataContentTypeException(Strings.MediaTypeUtils_DidNotFindMatchingMediaType(text2, settings.AcceptableMediaTypes));
					}
					odataMediaTypeFormat = list[mediaTypeMatchInfo.TargetTypeIndex];
					text = list2[mediaTypeMatchInfo.SourceTypeIndex].Value;
				}
				format = odataMediaTypeFormat.Format;
				mediaType = odataMediaTypeFormat.MediaType;
				if (list2 != null && mediaType.Parameters != null)
				{
					if (list2.Any(delegate(KeyValuePair<ODataMediaType, string> t)
					{
						if (t.Key.Parameters != null)
						{
							return t.Key.Parameters.Any((KeyValuePair<string, string> p) => string.Compare(p.Key, "metadata", StringComparison.OrdinalIgnoreCase) == 0);
						}
						return false;
					}))
					{
						mediaType = new ODataMediaType(mediaType.Type, mediaType.SubType, mediaType.Parameters.Select((KeyValuePair<string, string> p) => new KeyValuePair<string, string>((string.Compare(p.Key, "odata.metadata", StringComparison.OrdinalIgnoreCase) == 0) ? "metadata" : p.Key, p.Value)));
					}
					if (list2.Any(delegate(KeyValuePair<ODataMediaType, string> t)
					{
						if (t.Key.Parameters != null)
						{
							return t.Key.Parameters.Any((KeyValuePair<string, string> p) => string.Compare(p.Key, "streaming", StringComparison.OrdinalIgnoreCase) == 0);
						}
						return false;
					}))
					{
						mediaType = new ODataMediaType(mediaType.Type, mediaType.SubType, mediaType.Parameters.Select((KeyValuePair<string, string> p) => new KeyValuePair<string, string>((string.Compare(p.Key, "odata.streaming", StringComparison.OrdinalIgnoreCase) == 0) ? "streaming" : p.Key, p.Value)));
					}
				}
				string text3 = settings.AcceptableCharsets;
				if (text != null)
				{
					text3 = ((text3 == null) ? text : (text + "," + text3));
				}
				encoding = MediaTypeUtils.GetEncoding(text3, payloadKind, mediaType, true);
			}
			return format;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00007584 File Offset: 0x00005784
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
				IList<ODataMediaTypeFormat> list2 = mediaTypeResolver.GetMediaTypeFormats(odataPayloadKind).ToList<ODataMediaTypeFormat>();
				MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo = MediaTypeUtils.MatchMediaTypes(list2.Select((ODataMediaTypeFormat smt) => smt.MediaType), array);
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

		// Token: 0x06000258 RID: 600 RVA: 0x00007641 File Offset: 0x00005841
		internal static bool MediaTypeAndSubtypeAreEqual(string firstTypeAndSubtype, string secondTypeAndSubtype)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(firstTypeAndSubtype, "firstTypeAndSubtype");
			ExceptionUtils.CheckArgumentNotNull<string>(secondTypeAndSubtype, "secondTypeAndSubtype");
			return HttpUtils.CompareMediaTypeNames(firstTypeAndSubtype, secondTypeAndSubtype);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00007662 File Offset: 0x00005862
		internal static bool MediaTypeStartsWithTypeAndSubtype(string mediaType, string typeAndSubtype)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(mediaType, "mediaType");
			ExceptionUtils.CheckArgumentNotNull<string>(typeAndSubtype, "typeAndSubtype");
			return mediaType.StartsWith(typeAndSubtype, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00007684 File Offset: 0x00005884
		internal static bool MediaTypeHasParameterWithValue(this ODataMediaType mediaType, string parameterName, string parameterValue)
		{
			return mediaType.Parameters != null && mediaType.Parameters.Any((KeyValuePair<string, string> p) => HttpUtils.CompareMediaTypeParameterNames(p.Key, parameterName) && string.Compare(p.Value, parameterValue, StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000076C6 File Offset: 0x000058C6
		internal static bool HasStreamingSetToTrue(this ODataMediaType mediaType)
		{
			return mediaType.MediaTypeHasParameterWithValue("odata.streaming", "true");
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000076D8 File Offset: 0x000058D8
		internal static bool HasIeee754CompatibleSetToTrue(this ODataMediaType mediaType)
		{
			return mediaType.MediaTypeHasParameterWithValue("IEEE754Compatible", "true");
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000076EA File Offset: 0x000058EA
		internal static void CheckMediaTypeForWildCards(ODataMediaType mediaType)
		{
			if (HttpUtils.CompareMediaTypeNames("*", mediaType.Type) || HttpUtils.CompareMediaTypeNames("*", mediaType.SubType))
			{
				throw new ODataContentTypeException(Strings.ODataMessageReader_WildcardInContentType(mediaType.FullTypeName));
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00007724 File Offset: 0x00005924
		internal static string AlterContentTypeForJsonPadding(string contentType)
		{
			if (contentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
			{
				return contentType.Remove(0, "application/json".Length).Insert(0, "text/javascript");
			}
			if (contentType.StartsWith("text/plain", StringComparison.OrdinalIgnoreCase))
			{
				return contentType.Remove(0, "text/plain".Length).Insert(0, "text/javascript");
			}
			throw new ODataException(Strings.ODataMessageWriter_JsonPaddingOnInvalidContentType(contentType));
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00007794 File Offset: 0x00005994
		internal static ODataFormat GetFormatFromContentType(string contentTypeName, ODataPayloadKind[] supportedPayloadKinds, ODataMediaTypeResolver mediaTypeResolver, out ODataMediaType mediaType, out Encoding encoding, out ODataPayloadKind selectedPayloadKind)
		{
			string text;
			mediaType = MediaTypeUtils.ParseContentType(contentTypeName, out text);
			foreach (ODataPayloadKind odataPayloadKind in supportedPayloadKinds)
			{
				IList<ODataMediaTypeFormat> list = mediaTypeResolver.GetMediaTypeFormats(odataPayloadKind).ToList<ODataMediaTypeFormat>();
				MediaTypeUtils.MatchInfoCacheKey matchInfoCacheKey = new MediaTypeUtils.MatchInfoCacheKey(mediaTypeResolver, odataPayloadKind, contentTypeName);
				MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo;
				if (!MediaTypeUtils.MatchInfoCache.TryGetValue(matchInfoCacheKey, out mediaTypeMatchInfo))
				{
					mediaTypeMatchInfo = MediaTypeUtils.MatchMediaTypes(list.Select((ODataMediaTypeFormat smt) => smt.MediaType), new ODataMediaType[] { mediaType });
					MediaTypeUtils.MatchInfoCache.Add(matchInfoCacheKey, mediaTypeMatchInfo);
				}
				if (mediaTypeMatchInfo != null)
				{
					selectedPayloadKind = odataPayloadKind;
					encoding = MediaTypeUtils.GetEncoding(text, selectedPayloadKind, mediaType, false);
					return list[mediaTypeMatchInfo.SourceTypeIndex].Format;
				}
			}
			string text2 = string.Join(", ", supportedPayloadKinds.SelectMany((ODataPayloadKind pk) => from mt in mediaTypeResolver.GetMediaTypeFormats(pk)
				select mt.MediaType.ToText()).ToArray<string>());
			throw new ODataContentTypeException(Strings.MediaTypeUtils_CannotDetermineFormatFromContentType(text2, contentTypeName));
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000078AC File Offset: 0x00005AAC
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

		// Token: 0x06000261 RID: 609 RVA: 0x00007900 File Offset: 0x00005B00
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

		// Token: 0x06000262 RID: 610 RVA: 0x0000794C File Offset: 0x00005B4C
		private static Encoding GetEncoding(string acceptCharsetHeader, ODataPayloadKind payloadKind, ODataMediaType mediaType, bool useDefaultEncoding)
		{
			if (payloadKind == ODataPayloadKind.BinaryValue)
			{
				return null;
			}
			return HttpUtils.EncodingFromAcceptableCharsets(acceptCharsetHeader, mediaType, MediaTypeUtils.encodingUtf8NoPreamble, useDefaultEncoding ? MediaTypeUtils.encodingUtf8NoPreamble : null);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000796C File Offset: 0x00005B6C
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

		// Token: 0x06000264 RID: 612 RVA: 0x00007A20 File Offset: 0x00005C20
		private static void ConvertApplicationJsonInAcceptableMediaTypes(IList<KeyValuePair<ODataMediaType, string>> specifiedTypes, ODataVersion version)
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
						if (key.Parameters.Any((KeyValuePair<string, string> p) => HttpUtils.IsMetadataParameter(p.Key)))
						{
							goto IL_0106;
						}
					}
					IList<KeyValuePair<string, string>> list = ((key.Parameters != null) ? key.Parameters.ToList<KeyValuePair<string, string>>() : null);
					int num = ((list == null) ? 1 : (list.Count + 1));
					List<KeyValuePair<string, string>> list2 = new List<KeyValuePair<string, string>>(num);
					list2.Add(new KeyValuePair<string, string>((version < ODataVersion.V401) ? "odata.metadata" : "metadata", "minimal"));
					if (list != null)
					{
						list2.AddRange(list);
					}
					specifiedTypes[i] = new KeyValuePair<ODataMediaType, string>(new ODataMediaType(key.Type, key.SubType, list2), specifiedTypes[i].Value);
				}
				IL_0106:;
			}
		}

		// Token: 0x040000FD RID: 253
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

		// Token: 0x040000FE RID: 254
		private static readonly UTF8Encoding encodingUtf8NoPreamble = new UTF8Encoding(false, true);

		// Token: 0x040000FF RID: 255
		private const int MatchInfoCacheMaxSize = 256;

		// Token: 0x04000100 RID: 256
		private static MediaTypeUtils.MatchInfoConcurrentCache MatchInfoCache = new MediaTypeUtils.MatchInfoConcurrentCache(256);

		// Token: 0x02000292 RID: 658
		private sealed class MediaTypeMatchInfo : IComparable<MediaTypeUtils.MediaTypeMatchInfo>
		{
			// Token: 0x06001C67 RID: 7271 RVA: 0x00056557 File Offset: 0x00054757
			public MediaTypeMatchInfo(ODataMediaType sourceType, ODataMediaType targetType, int sourceIndex, int targetIndex)
			{
				this.sourceIndex = sourceIndex;
				this.targetIndex = targetIndex;
				this.MatchTypes(sourceType, targetType);
			}

			// Token: 0x170005CD RID: 1485
			// (get) Token: 0x06001C68 RID: 7272 RVA: 0x00056576 File Offset: 0x00054776
			public int SourceTypeIndex
			{
				get
				{
					return this.sourceIndex;
				}
			}

			// Token: 0x170005CE RID: 1486
			// (get) Token: 0x06001C69 RID: 7273 RVA: 0x0005657E File Offset: 0x0005477E
			public int TargetTypeIndex
			{
				get
				{
					return this.targetIndex;
				}
			}

			// Token: 0x170005CF RID: 1487
			// (get) Token: 0x06001C6A RID: 7274 RVA: 0x00056586 File Offset: 0x00054786
			// (set) Token: 0x06001C6B RID: 7275 RVA: 0x0005658E File Offset: 0x0005478E
			public int MatchingTypeNamePartCount { get; private set; }

			// Token: 0x170005D0 RID: 1488
			// (get) Token: 0x06001C6C RID: 7276 RVA: 0x00056597 File Offset: 0x00054797
			// (set) Token: 0x06001C6D RID: 7277 RVA: 0x0005659F File Offset: 0x0005479F
			public int MatchingParameterCount { get; private set; }

			// Token: 0x170005D1 RID: 1489
			// (get) Token: 0x06001C6E RID: 7278 RVA: 0x000565A8 File Offset: 0x000547A8
			// (set) Token: 0x06001C6F RID: 7279 RVA: 0x000565B0 File Offset: 0x000547B0
			public int QualityValue { get; private set; }

			// Token: 0x170005D2 RID: 1490
			// (get) Token: 0x06001C70 RID: 7280 RVA: 0x000565B9 File Offset: 0x000547B9
			// (set) Token: 0x06001C71 RID: 7281 RVA: 0x000565C1 File Offset: 0x000547C1
			public int SourceTypeParameterCountForMatching { get; private set; }

			// Token: 0x170005D3 RID: 1491
			// (get) Token: 0x06001C72 RID: 7282 RVA: 0x000565CA File Offset: 0x000547CA
			public bool IsMatch
			{
				get
				{
					return this.QualityValue != 0 && this.MatchingTypeNamePartCount >= 0 && (this.MatchingTypeNamePartCount <= 1 || this.MatchingParameterCount == -1 || this.MatchingParameterCount >= this.SourceTypeParameterCountForMatching);
				}
			}

			// Token: 0x06001C73 RID: 7283 RVA: 0x00056604 File Offset: 0x00054804
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

			// Token: 0x06001C74 RID: 7284 RVA: 0x00056688 File Offset: 0x00054888
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

			// Token: 0x06001C75 RID: 7285 RVA: 0x000566B4 File Offset: 0x000548B4
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

			// Token: 0x06001C76 RID: 7286 RVA: 0x00056705 File Offset: 0x00054905
			private static bool IsQualityValueParameter(string parameterName)
			{
				return HttpUtils.CompareMediaTypeParameterNames("q", parameterName);
			}

			// Token: 0x06001C77 RID: 7287 RVA: 0x00056714 File Offset: 0x00054914
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
				IList<KeyValuePair<string, string>> list = ((sourceType.Parameters != null) ? sourceType.Parameters.ToList<KeyValuePair<string, string>>() : null);
				IList<KeyValuePair<string, string>> list2 = ((targetType.Parameters != null) ? targetType.Parameters.ToList<KeyValuePair<string, string>>() : null);
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
						if (flag && MediaTypeUtils.MediaTypeMatchInfo.TryFindMediaTypeParameter(list2, key, out text) && string.Compare(list[i].Value.Trim(), text.Trim(), StringComparison.OrdinalIgnoreCase) == 0)
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

			// Token: 0x04000C09 RID: 3081
			private const int DefaultQualityValue = 1000;

			// Token: 0x04000C0A RID: 3082
			private readonly int sourceIndex;

			// Token: 0x04000C0B RID: 3083
			private readonly int targetIndex;
		}

		// Token: 0x02000293 RID: 659
		private sealed class MatchInfoCacheKey : IEquatable<MediaTypeUtils.MatchInfoCacheKey>
		{
			// Token: 0x06001C78 RID: 7288 RVA: 0x000568C2 File Offset: 0x00054AC2
			public MatchInfoCacheKey(ODataMediaTypeResolver resolver, ODataPayloadKind payloadKind, string contentTypeName)
			{
				this.MediaTypeResolver = resolver;
				this.PayloadKind = payloadKind;
				this.ContentTypeName = contentTypeName;
			}

			// Token: 0x170005D4 RID: 1492
			// (get) Token: 0x06001C79 RID: 7289 RVA: 0x000568DF File Offset: 0x00054ADF
			// (set) Token: 0x06001C7A RID: 7290 RVA: 0x000568E7 File Offset: 0x00054AE7
			private ODataMediaTypeResolver MediaTypeResolver { get; set; }

			// Token: 0x170005D5 RID: 1493
			// (get) Token: 0x06001C7B RID: 7291 RVA: 0x000568F0 File Offset: 0x00054AF0
			// (set) Token: 0x06001C7C RID: 7292 RVA: 0x000568F8 File Offset: 0x00054AF8
			private ODataPayloadKind PayloadKind { get; set; }

			// Token: 0x170005D6 RID: 1494
			// (get) Token: 0x06001C7D RID: 7293 RVA: 0x00056901 File Offset: 0x00054B01
			// (set) Token: 0x06001C7E RID: 7294 RVA: 0x00056909 File Offset: 0x00054B09
			private string ContentTypeName { get; set; }

			// Token: 0x06001C7F RID: 7295 RVA: 0x00056912 File Offset: 0x00054B12
			public override bool Equals(object obj)
			{
				return this.Equals(obj as MediaTypeUtils.MatchInfoCacheKey);
			}

			// Token: 0x06001C80 RID: 7296 RVA: 0x00056920 File Offset: 0x00054B20
			public bool Equals(MediaTypeUtils.MatchInfoCacheKey other)
			{
				return other != null && (this == other || (this.MediaTypeResolver == other.MediaTypeResolver && this.PayloadKind == other.PayloadKind && this.ContentTypeName == other.ContentTypeName));
			}

			// Token: 0x06001C81 RID: 7297 RVA: 0x0005695C File Offset: 0x00054B5C
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

		// Token: 0x02000294 RID: 660
		private sealed class MatchInfoConcurrentCache
		{
			// Token: 0x06001C82 RID: 7298 RVA: 0x000569A1 File Offset: 0x00054BA1
			public MatchInfoConcurrentCache(int maxSize)
			{
				this.dict = new ConcurrentDictionary<MediaTypeUtils.MatchInfoCacheKey, MediaTypeUtils.MediaTypeMatchInfo>(4, maxSize);
			}

			// Token: 0x06001C83 RID: 7299 RVA: 0x000569B6 File Offset: 0x00054BB6
			public bool TryGetValue(MediaTypeUtils.MatchInfoCacheKey key, out MediaTypeUtils.MediaTypeMatchInfo value)
			{
				return this.dict.TryGetValue(key, out value);
			}

			// Token: 0x06001C84 RID: 7300 RVA: 0x000569C8 File Offset: 0x00054BC8
			public void Add(MediaTypeUtils.MatchInfoCacheKey key, MediaTypeUtils.MediaTypeMatchInfo value)
			{
				try
				{
					this.dict.TryAdd(key, value);
				}
				catch (OverflowException)
				{
					this.dict.Clear();
					this.dict.TryAdd(key, value);
				}
			}

			// Token: 0x04000C13 RID: 3091
			private readonly ConcurrentDictionary<MediaTypeUtils.MatchInfoCacheKey, MediaTypeUtils.MediaTypeMatchInfo> dict;
		}
	}
}
