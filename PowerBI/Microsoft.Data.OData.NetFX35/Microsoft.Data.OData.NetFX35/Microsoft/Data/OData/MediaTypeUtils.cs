using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Data.OData
{
	// Token: 0x02000258 RID: 600
	internal static class MediaTypeUtils
	{
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x0004670D File Offset: 0x0004490D
		internal static UTF8Encoding EncodingUtf8NoPreamble
		{
			get
			{
				return MediaTypeUtils.encodingUtf8NoPreamble;
			}
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00046734 File Offset: 0x00044934
		internal static ODataFormat GetContentTypeFromSettings(ODataMessageWriterSettings settings, ODataPayloadKind payloadKind, MediaTypeResolver mediaTypeResolver, out MediaType mediaType, out Encoding encoding)
		{
			IList<MediaTypeWithFormat> mediaTypesForPayloadKind = mediaTypeResolver.GetMediaTypesForPayloadKind(payloadKind);
			if (mediaTypesForPayloadKind == null || mediaTypesForPayloadKind.Count == 0)
			{
				throw new ODataContentTypeException(Strings.MediaTypeUtils_DidNotFindMatchingMediaType(null, settings.AcceptableMediaTypes));
			}
			ODataFormat format;
			if (settings.UseFormat == true)
			{
				mediaType = MediaTypeUtils.GetDefaultMediaType(mediaTypesForPayloadKind, settings.Format, out format);
				encoding = mediaType.SelectEncoding();
			}
			else
			{
				IList<KeyValuePair<MediaType, string>> list = HttpUtils.MediaTypesFromString(settings.AcceptableMediaTypes);
				if (settings.Version >= ODataVersion.V3)
				{
					MediaTypeUtils.ConvertApplicationJsonInAcceptableMediaTypes(list);
				}
				string text = null;
				MediaTypeWithFormat mediaTypeWithFormat;
				if (list == null || list.Count == 0)
				{
					mediaTypeWithFormat = mediaTypesForPayloadKind[0];
				}
				else
				{
					MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo = MediaTypeUtils.MatchMediaTypes(Enumerable.Select<KeyValuePair<MediaType, string>, MediaType>(list, (KeyValuePair<MediaType, string> kvp) => kvp.Key), Enumerable.ToArray<MediaType>(Enumerable.Select<MediaTypeWithFormat, MediaType>(mediaTypesForPayloadKind, (MediaTypeWithFormat smt) => smt.MediaType)));
					if (mediaTypeMatchInfo == null)
					{
						string text2 = string.Join(", ", Enumerable.ToArray<string>(Enumerable.Select<MediaTypeWithFormat, string>(mediaTypesForPayloadKind, (MediaTypeWithFormat mt) => mt.MediaType.ToText())));
						throw new ODataContentTypeException(Strings.MediaTypeUtils_DidNotFindMatchingMediaType(text2, settings.AcceptableMediaTypes));
					}
					mediaTypeWithFormat = mediaTypesForPayloadKind[mediaTypeMatchInfo.TargetTypeIndex];
					text = list[mediaTypeMatchInfo.SourceTypeIndex].Value;
				}
				format = mediaTypeWithFormat.Format;
				mediaType = mediaTypeWithFormat.MediaType;
				string text3 = settings.AcceptableCharsets;
				if (text != null)
				{
					text3 = ((text3 == null) ? text : (text + "," + text3));
				}
				encoding = MediaTypeUtils.GetEncoding(text3, payloadKind, mediaType, true);
			}
			return format;
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00046908 File Offset: 0x00044B08
		internal static ODataFormat GetFormatFromContentType(string contentTypeHeader, ODataPayloadKind[] supportedPayloadKinds, MediaTypeResolver mediaTypeResolver, out MediaType mediaType, out Encoding encoding, out ODataPayloadKind selectedPayloadKind, out string batchBoundary)
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

		// Token: 0x060012A7 RID: 4775 RVA: 0x000469F4 File Offset: 0x00044BF4
		internal static IList<ODataPayloadKindDetectionResult> GetPayloadKindsForContentType(string contentTypeHeader, MediaTypeResolver mediaTypeResolver, out MediaType contentType, out Encoding encoding)
		{
			encoding = null;
			string text;
			contentType = MediaTypeUtils.ParseContentType(contentTypeHeader, out text);
			MediaType[] array = new MediaType[] { contentType };
			List<ODataPayloadKindDetectionResult> list = new List<ODataPayloadKindDetectionResult>();
			for (int i = 0; i < MediaTypeUtils.allSupportedPayloadKinds.Length; i++)
			{
				ODataPayloadKind odataPayloadKind = MediaTypeUtils.allSupportedPayloadKinds[i];
				IList<MediaTypeWithFormat> mediaTypesForPayloadKind = mediaTypeResolver.GetMediaTypesForPayloadKind(odataPayloadKind);
				MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo = MediaTypeUtils.MatchMediaTypes(Enumerable.Select<MediaTypeWithFormat, MediaType>(mediaTypesForPayloadKind, (MediaTypeWithFormat smt) => smt.MediaType), array);
				if (mediaTypeMatchInfo != null)
				{
					list.Add(new ODataPayloadKindDetectionResult(odataPayloadKind, mediaTypesForPayloadKind[mediaTypeMatchInfo.SourceTypeIndex].Format));
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				encoding = HttpUtils.GetEncodingFromCharsetName(text);
			}
			return list;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00046AAF File Offset: 0x00044CAF
		internal static bool MediaTypeAndSubtypeAreEqual(string firstTypeAndSubtype, string secondTypeAndSubtype)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(firstTypeAndSubtype, "firstTypeAndSubtype");
			ExceptionUtils.CheckArgumentNotNull<string>(secondTypeAndSubtype, "secondTypeAndSubtype");
			return HttpUtils.CompareMediaTypeNames(firstTypeAndSubtype, secondTypeAndSubtype);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00046ACE File Offset: 0x00044CCE
		internal static bool MediaTypeStartsWithTypeAndSubtype(string mediaType, string typeAndSubtype)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(mediaType, "mediaType");
			ExceptionUtils.CheckArgumentNotNull<string>(typeAndSubtype, "typeAndSubtype");
			return mediaType.StartsWith(typeAndSubtype, 5);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00046B24 File Offset: 0x00044D24
		internal static bool MediaTypeHasParameterWithValue(this MediaType mediaType, string parameterName, string parameterValue)
		{
			return mediaType.Parameters != null && Enumerable.Any<KeyValuePair<string, string>>(mediaType.Parameters, (KeyValuePair<string, string> p) => HttpUtils.CompareMediaTypeParameterNames(p.Key, parameterName) && string.Compare(p.Value, parameterValue, 5) == 0);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00046B66 File Offset: 0x00044D66
		internal static bool HasStreamingSetToTrue(this MediaType mediaType)
		{
			return mediaType.MediaTypeHasParameterWithValue("streaming", "true");
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00046B78 File Offset: 0x00044D78
		internal static void CheckMediaTypeForWildCards(MediaType mediaType)
		{
			if (HttpUtils.CompareMediaTypeNames("*", mediaType.TypeName) || HttpUtils.CompareMediaTypeNames("*", mediaType.SubTypeName))
			{
				throw new ODataContentTypeException(Strings.ODataMessageReader_WildcardInContentType(mediaType.FullTypeName));
			}
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00046BB0 File Offset: 0x00044DB0
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

		// Token: 0x060012AE RID: 4782 RVA: 0x00046C6C File Offset: 0x00044E6C
		private static ODataFormat GetFormatFromContentType(string contentTypeName, ODataPayloadKind[] supportedPayloadKinds, MediaTypeResolver mediaTypeResolver, out MediaType mediaType, out Encoding encoding, out ODataPayloadKind selectedPayloadKind)
		{
			string text;
			mediaType = MediaTypeUtils.ParseContentType(contentTypeName, out text);
			if (!mediaTypeResolver.IsIllegalMediaType(mediaType))
			{
				foreach (ODataPayloadKind odataPayloadKind in supportedPayloadKinds)
				{
					IList<MediaTypeWithFormat> mediaTypesForPayloadKind = mediaTypeResolver.GetMediaTypesForPayloadKind(odataPayloadKind);
					MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo = MediaTypeUtils.MatchMediaTypes(Enumerable.Select<MediaTypeWithFormat, MediaType>(mediaTypesForPayloadKind, (MediaTypeWithFormat smt) => smt.MediaType), new MediaType[] { mediaType });
					if (mediaTypeMatchInfo != null)
					{
						selectedPayloadKind = odataPayloadKind;
						encoding = MediaTypeUtils.GetEncoding(text, selectedPayloadKind, mediaType, false);
						return mediaTypesForPayloadKind[mediaTypeMatchInfo.SourceTypeIndex].Format;
					}
				}
			}
			string text2 = string.Join(", ", Enumerable.ToArray<string>(Enumerable.SelectMany<ODataPayloadKind, string>(supportedPayloadKinds, (ODataPayloadKind pk) => Enumerable.Select<MediaTypeWithFormat, string>(mediaTypeResolver.GetMediaTypesForPayloadKind(pk), (MediaTypeWithFormat mt) => mt.MediaType.ToText()))));
			throw new ODataContentTypeException(Strings.MediaTypeUtils_CannotDetermineFormatFromContentType(text2, contentTypeName));
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00046D5C File Offset: 0x00044F5C
		private static MediaType ParseContentType(string contentTypeHeader, out string charset)
		{
			IList<KeyValuePair<MediaType, string>> list = HttpUtils.MediaTypesFromString(contentTypeHeader);
			if (list.Count != 1)
			{
				throw new ODataContentTypeException(Strings.MediaTypeUtils_NoOrMoreThanOneContentTypeSpecified(contentTypeHeader));
			}
			MediaType key = list[0].Key;
			MediaTypeUtils.CheckMediaTypeForWildCards(key);
			charset = list[0].Value;
			return key;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00046DB0 File Offset: 0x00044FB0
		private static MediaType GetDefaultMediaType(IList<MediaTypeWithFormat> supportedMediaTypes, ODataFormat specifiedFormat, out ODataFormat actualFormat)
		{
			for (int i = 0; i < supportedMediaTypes.Count; i++)
			{
				MediaTypeWithFormat mediaTypeWithFormat = supportedMediaTypes[i];
				if (specifiedFormat == null || mediaTypeWithFormat.Format == specifiedFormat)
				{
					actualFormat = mediaTypeWithFormat.Format;
					return mediaTypeWithFormat.MediaType;
				}
			}
			throw new ODataException(Strings.ODataUtils_DidNotFindDefaultMediaType(specifiedFormat));
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00046DFC File Offset: 0x00044FFC
		private static Encoding GetEncoding(string acceptCharsetHeader, ODataPayloadKind payloadKind, MediaType mediaType, bool useDefaultEncoding)
		{
			if (payloadKind == ODataPayloadKind.BinaryValue)
			{
				return null;
			}
			return HttpUtils.EncodingFromAcceptableCharsets(acceptCharsetHeader, mediaType, MediaTypeUtils.encodingUtf8NoPreamble, useDefaultEncoding ? MediaTypeUtils.encodingUtf8NoPreamble : null);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00046E1C File Offset: 0x0004501C
		private static MediaTypeUtils.MediaTypeMatchInfo MatchMediaTypes(IEnumerable<MediaType> sourceTypes, MediaType[] targetTypes)
		{
			MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo = null;
			int num = 0;
			if (sourceTypes != null)
			{
				foreach (MediaType mediaType in sourceTypes)
				{
					int num2 = 0;
					foreach (MediaType mediaType2 in targetTypes)
					{
						MediaTypeUtils.MediaTypeMatchInfo mediaTypeMatchInfo2 = new MediaTypeUtils.MediaTypeMatchInfo(mediaType, mediaType2, num, num2);
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

		// Token: 0x060012B3 RID: 4787 RVA: 0x00046EE4 File Offset: 0x000450E4
		private static void ConvertApplicationJsonInAcceptableMediaTypes(IList<KeyValuePair<MediaType, string>> specifiedTypes)
		{
			if (specifiedTypes == null)
			{
				return;
			}
			for (int i = 0; i < specifiedTypes.Count; i++)
			{
				MediaType key = specifiedTypes[i].Key;
				if (HttpUtils.CompareMediaTypeNames(key.SubTypeName, "json") && HttpUtils.CompareMediaTypeNames(key.TypeName, "application"))
				{
					if (key.Parameters != null)
					{
						if (Enumerable.Any<KeyValuePair<string, string>>(key.Parameters, (KeyValuePair<string, string> p) => HttpUtils.CompareMediaTypeParameterNames(p.Key, "odata")))
						{
							goto IL_00E6;
						}
					}
					IList<KeyValuePair<string, string>> parameters = key.Parameters;
					int num = ((parameters == null) ? 1 : (parameters.Count + 1));
					List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>(num);
					list.Add(new KeyValuePair<string, string>("odata", "minimalmetadata"));
					if (parameters != null)
					{
						list.AddRange(parameters);
					}
					specifiedTypes[i] = new KeyValuePair<MediaType, string>(new MediaType(key.TypeName, key.SubTypeName, list), specifiedTypes[i].Value);
				}
				IL_00E6:;
			}
		}

		// Token: 0x040006FD RID: 1789
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
			ODataPayloadKind.Parameter
		};

		// Token: 0x040006FE RID: 1790
		private static readonly UTF8Encoding encodingUtf8NoPreamble = new UTF8Encoding(false, true);

		// Token: 0x02000259 RID: 601
		private sealed class MediaTypeMatchInfo : IComparable<MediaTypeUtils.MediaTypeMatchInfo>
		{
			// Token: 0x060012BC RID: 4796 RVA: 0x00047047 File Offset: 0x00045247
			public MediaTypeMatchInfo(MediaType sourceType, MediaType targetType, int sourceIndex, int targetIndex)
			{
				this.sourceIndex = sourceIndex;
				this.targetIndex = targetIndex;
				this.MatchTypes(sourceType, targetType);
			}

			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x060012BD RID: 4797 RVA: 0x00047066 File Offset: 0x00045266
			public int SourceTypeIndex
			{
				get
				{
					return this.sourceIndex;
				}
			}

			// Token: 0x170003FC RID: 1020
			// (get) Token: 0x060012BE RID: 4798 RVA: 0x0004706E File Offset: 0x0004526E
			public int TargetTypeIndex
			{
				get
				{
					return this.targetIndex;
				}
			}

			// Token: 0x170003FD RID: 1021
			// (get) Token: 0x060012BF RID: 4799 RVA: 0x00047076 File Offset: 0x00045276
			// (set) Token: 0x060012C0 RID: 4800 RVA: 0x0004707E File Offset: 0x0004527E
			public int MatchingTypeNamePartCount { get; private set; }

			// Token: 0x170003FE RID: 1022
			// (get) Token: 0x060012C1 RID: 4801 RVA: 0x00047087 File Offset: 0x00045287
			// (set) Token: 0x060012C2 RID: 4802 RVA: 0x0004708F File Offset: 0x0004528F
			public int MatchingParameterCount { get; private set; }

			// Token: 0x170003FF RID: 1023
			// (get) Token: 0x060012C3 RID: 4803 RVA: 0x00047098 File Offset: 0x00045298
			// (set) Token: 0x060012C4 RID: 4804 RVA: 0x000470A0 File Offset: 0x000452A0
			public int QualityValue { get; private set; }

			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x060012C5 RID: 4805 RVA: 0x000470A9 File Offset: 0x000452A9
			// (set) Token: 0x060012C6 RID: 4806 RVA: 0x000470B1 File Offset: 0x000452B1
			public int SourceTypeParameterCountForMatching { get; private set; }

			// Token: 0x17000401 RID: 1025
			// (get) Token: 0x060012C7 RID: 4807 RVA: 0x000470BA File Offset: 0x000452BA
			public bool IsMatch
			{
				get
				{
					return this.QualityValue != 0 && this.MatchingTypeNamePartCount >= 0 && (this.MatchingTypeNamePartCount <= 1 || this.MatchingParameterCount == -1 || this.MatchingParameterCount >= this.SourceTypeParameterCountForMatching);
				}
			}

			// Token: 0x060012C8 RID: 4808 RVA: 0x000470F4 File Offset: 0x000452F4
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

			// Token: 0x060012C9 RID: 4809 RVA: 0x00047178 File Offset: 0x00045378
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

			// Token: 0x060012CA RID: 4810 RVA: 0x000471A4 File Offset: 0x000453A4
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

			// Token: 0x060012CB RID: 4811 RVA: 0x000471F5 File Offset: 0x000453F5
			private static bool IsQualityValueParameter(string parameterName)
			{
				return HttpUtils.CompareMediaTypeParameterNames("q", parameterName);
			}

			// Token: 0x060012CC RID: 4812 RVA: 0x00047204 File Offset: 0x00045404
			private void MatchTypes(MediaType sourceType, MediaType targetType)
			{
				this.MatchingTypeNamePartCount = -1;
				if (sourceType.TypeName == "*")
				{
					this.MatchingTypeNamePartCount = 0;
				}
				else if (HttpUtils.CompareMediaTypeNames(sourceType.TypeName, targetType.TypeName))
				{
					if (sourceType.SubTypeName == "*")
					{
						this.MatchingTypeNamePartCount = 1;
					}
					else if (HttpUtils.CompareMediaTypeNames(sourceType.SubTypeName, targetType.SubTypeName))
					{
						this.MatchingTypeNamePartCount = 2;
					}
				}
				this.QualityValue = 1000;
				this.SourceTypeParameterCountForMatching = 0;
				this.MatchingParameterCount = 0;
				IList<KeyValuePair<string, string>> parameters = sourceType.Parameters;
				IList<KeyValuePair<string, string>> parameters2 = targetType.Parameters;
				bool flag = parameters2 != null && parameters2.Count > 0;
				bool flag2 = parameters != null && parameters.Count > 0;
				if (flag2)
				{
					for (int i = 0; i < parameters.Count; i++)
					{
						string key = parameters[i].Key;
						if (MediaTypeUtils.MediaTypeMatchInfo.IsQualityValueParameter(key))
						{
							this.QualityValue = MediaTypeUtils.MediaTypeMatchInfo.ParseQualityValue(parameters[i].Value.Trim());
							break;
						}
						this.SourceTypeParameterCountForMatching = i + 1;
						string text;
						if (flag && MediaTypeUtils.MediaTypeMatchInfo.TryFindMediaTypeParameter(parameters2, key, out text) && string.Compare(parameters[i].Value.Trim(), text.Trim(), 5) == 0)
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

			// Token: 0x04000706 RID: 1798
			private const int DefaultQualityValue = 1000;

			// Token: 0x04000707 RID: 1799
			private readonly int sourceIndex;

			// Token: 0x04000708 RID: 1800
			private readonly int targetIndex;
		}
	}
}
