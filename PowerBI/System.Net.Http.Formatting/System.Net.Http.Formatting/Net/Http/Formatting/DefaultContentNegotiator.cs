using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200003F RID: 63
	public class DefaultContentNegotiator : IContentNegotiator
	{
		// Token: 0x06000255 RID: 597 RVA: 0x0000812F File Offset: 0x0000632F
		public DefaultContentNegotiator()
			: this(false)
		{
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00008138 File Offset: 0x00006338
		public DefaultContentNegotiator(bool excludeMatchOnTypeOnly)
		{
			this.ExcludeMatchOnTypeOnly = excludeMatchOnTypeOnly;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00008147 File Offset: 0x00006347
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000814F File Offset: 0x0000634F
		public bool ExcludeMatchOnTypeOnly { get; private set; }

		// Token: 0x06000259 RID: 601 RVA: 0x00008158 File Offset: 0x00006358
		public virtual ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (formatters == null)
			{
				throw Error.ArgumentNull("formatters");
			}
			Collection<MediaTypeFormatterMatch> collection = this.ComputeFormatterMatches(type, request, formatters);
			MediaTypeFormatterMatch mediaTypeFormatterMatch = this.SelectResponseMediaTypeFormatter(collection);
			if (mediaTypeFormatterMatch != null)
			{
				Encoding encoding = this.SelectResponseCharacterEncoding(request, mediaTypeFormatterMatch.Formatter);
				if (encoding != null)
				{
					mediaTypeFormatterMatch.MediaType.CharSet = encoding.WebName;
				}
				MediaTypeHeaderValue mediaType = mediaTypeFormatterMatch.MediaType;
				return new ContentNegotiationResult(mediaTypeFormatterMatch.Formatter.GetPerRequestFormatterInstance(type, request, mediaType), mediaType);
			}
			return null;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000081EC File Offset: 0x000063EC
		protected virtual Collection<MediaTypeFormatterMatch> ComputeFormatterMatches(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (formatters == null)
			{
				throw Error.ArgumentNull("formatters");
			}
			IEnumerable<MediaTypeWithQualityHeaderValue> enumerable = null;
			ListWrapperCollection<MediaTypeFormatterMatch> listWrapperCollection = new ListWrapperCollection<MediaTypeFormatterMatch>();
			foreach (MediaTypeFormatter mediaTypeFormatter in DefaultContentNegotiator.GetWritingFormatters(formatters))
			{
				if (mediaTypeFormatter.CanWriteType(type))
				{
					MediaTypeFormatterMatch mediaTypeFormatterMatch;
					if ((mediaTypeFormatterMatch = this.MatchMediaTypeMapping(request, mediaTypeFormatter)) != null)
					{
						listWrapperCollection.Add(mediaTypeFormatterMatch);
					}
					else
					{
						if (enumerable == null)
						{
							enumerable = this.SortMediaTypeWithQualityHeaderValuesByQFactor(request.Headers.Accept);
						}
						if ((mediaTypeFormatterMatch = this.MatchAcceptHeader(enumerable, mediaTypeFormatter)) != null)
						{
							listWrapperCollection.Add(mediaTypeFormatterMatch);
						}
						else if ((mediaTypeFormatterMatch = this.MatchRequestMediaType(request, mediaTypeFormatter)) != null)
						{
							listWrapperCollection.Add(mediaTypeFormatterMatch);
						}
						else if (this.ShouldMatchOnType(enumerable) && (mediaTypeFormatterMatch = this.MatchType(type, mediaTypeFormatter)) != null)
						{
							listWrapperCollection.Add(mediaTypeFormatterMatch);
						}
					}
				}
			}
			return listWrapperCollection;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000082DC File Offset: 0x000064DC
		protected virtual MediaTypeFormatterMatch SelectResponseMediaTypeFormatter(ICollection<MediaTypeFormatterMatch> matches)
		{
			if (matches == null)
			{
				throw Error.ArgumentNull("matches");
			}
			List<MediaTypeFormatterMatch> list = matches.AsList<MediaTypeFormatterMatch>();
			MediaTypeFormatterMatch mediaTypeFormatterMatch = null;
			MediaTypeFormatterMatch mediaTypeFormatterMatch2 = null;
			MediaTypeFormatterMatch mediaTypeFormatterMatch3 = null;
			MediaTypeFormatterMatch mediaTypeFormatterMatch4 = null;
			MediaTypeFormatterMatch mediaTypeFormatterMatch5 = null;
			MediaTypeFormatterMatch mediaTypeFormatterMatch6 = null;
			for (int i = 0; i < list.Count; i++)
			{
				MediaTypeFormatterMatch mediaTypeFormatterMatch7 = list[i];
				switch (mediaTypeFormatterMatch7.Ranking)
				{
				case MediaTypeFormatterMatchRanking.MatchOnCanWriteType:
					if (mediaTypeFormatterMatch == null)
					{
						mediaTypeFormatterMatch = mediaTypeFormatterMatch7;
					}
					break;
				case MediaTypeFormatterMatchRanking.MatchOnRequestAcceptHeaderLiteral:
					mediaTypeFormatterMatch2 = this.UpdateBestMatch(mediaTypeFormatterMatch2, mediaTypeFormatterMatch7);
					break;
				case MediaTypeFormatterMatchRanking.MatchOnRequestAcceptHeaderSubtypeMediaRange:
					mediaTypeFormatterMatch3 = this.UpdateBestMatch(mediaTypeFormatterMatch3, mediaTypeFormatterMatch7);
					break;
				case MediaTypeFormatterMatchRanking.MatchOnRequestAcceptHeaderAllMediaRange:
					mediaTypeFormatterMatch4 = this.UpdateBestMatch(mediaTypeFormatterMatch4, mediaTypeFormatterMatch7);
					break;
				case MediaTypeFormatterMatchRanking.MatchOnRequestWithMediaTypeMapping:
					mediaTypeFormatterMatch5 = this.UpdateBestMatch(mediaTypeFormatterMatch5, mediaTypeFormatterMatch7);
					break;
				case MediaTypeFormatterMatchRanking.MatchOnRequestMediaType:
					if (mediaTypeFormatterMatch6 == null)
					{
						mediaTypeFormatterMatch6 = mediaTypeFormatterMatch7;
					}
					break;
				}
			}
			if (mediaTypeFormatterMatch5 != null)
			{
				MediaTypeFormatterMatch mediaTypeFormatterMatch8 = mediaTypeFormatterMatch5;
				mediaTypeFormatterMatch8 = this.UpdateBestMatch(mediaTypeFormatterMatch8, mediaTypeFormatterMatch2);
				mediaTypeFormatterMatch8 = this.UpdateBestMatch(mediaTypeFormatterMatch8, mediaTypeFormatterMatch3);
				mediaTypeFormatterMatch8 = this.UpdateBestMatch(mediaTypeFormatterMatch8, mediaTypeFormatterMatch4);
				if (mediaTypeFormatterMatch8 != mediaTypeFormatterMatch5)
				{
					mediaTypeFormatterMatch5 = null;
				}
			}
			MediaTypeFormatterMatch mediaTypeFormatterMatch9 = null;
			if (mediaTypeFormatterMatch5 != null)
			{
				mediaTypeFormatterMatch9 = mediaTypeFormatterMatch5;
			}
			else if (mediaTypeFormatterMatch2 != null || mediaTypeFormatterMatch3 != null || mediaTypeFormatterMatch4 != null)
			{
				mediaTypeFormatterMatch9 = this.UpdateBestMatch(mediaTypeFormatterMatch9, mediaTypeFormatterMatch2);
				mediaTypeFormatterMatch9 = this.UpdateBestMatch(mediaTypeFormatterMatch9, mediaTypeFormatterMatch3);
				mediaTypeFormatterMatch9 = this.UpdateBestMatch(mediaTypeFormatterMatch9, mediaTypeFormatterMatch4);
			}
			else if (mediaTypeFormatterMatch6 != null)
			{
				mediaTypeFormatterMatch9 = mediaTypeFormatterMatch6;
			}
			else if (mediaTypeFormatterMatch != null)
			{
				mediaTypeFormatterMatch9 = mediaTypeFormatterMatch;
			}
			return mediaTypeFormatterMatch9;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008424 File Offset: 0x00006624
		protected virtual Encoding SelectResponseCharacterEncoding(HttpRequestMessage request, MediaTypeFormatter formatter)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			List<Encoding> supportedEncodingsInternal = formatter.SupportedEncodingsInternal;
			if (supportedEncodingsInternal.Count > 0)
			{
				foreach (StringWithQualityHeaderValue stringWithQualityHeaderValue in this.SortStringWithQualityHeaderValuesByQFactor(request.Headers.AcceptCharset))
				{
					for (int i = 0; i < supportedEncodingsInternal.Count; i++)
					{
						Encoding encoding = supportedEncodingsInternal[i];
						if (encoding != null)
						{
							double? quality = stringWithQualityHeaderValue.Quality;
							double num = 0.0;
							if (!((quality.GetValueOrDefault() == num) & (quality != null)) && (stringWithQualityHeaderValue.Value.Equals(encoding.WebName, StringComparison.OrdinalIgnoreCase) || stringWithQualityHeaderValue.Value.Equals("*", StringComparison.OrdinalIgnoreCase)))
							{
								return encoding;
							}
						}
					}
				}
				return formatter.SelectCharacterEncoding((request.Content != null) ? request.Content.Headers : null);
			}
			return null;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000853C File Offset: 0x0000673C
		protected virtual MediaTypeFormatterMatch MatchMediaTypeMapping(HttpRequestMessage request, MediaTypeFormatter formatter)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			List<MediaTypeMapping> mediaTypeMappingsInternal = formatter.MediaTypeMappingsInternal;
			for (int i = 0; i < mediaTypeMappingsInternal.Count; i++)
			{
				MediaTypeMapping mediaTypeMapping = mediaTypeMappingsInternal[i];
				double num;
				if (mediaTypeMapping != null && (num = mediaTypeMapping.TryMatchMediaType(request)) > 0.0)
				{
					return new MediaTypeFormatterMatch(formatter, mediaTypeMapping.MediaType, new double?(num), MediaTypeFormatterMatchRanking.MatchOnRequestWithMediaTypeMapping);
				}
			}
			return null;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000085B4 File Offset: 0x000067B4
		protected virtual MediaTypeFormatterMatch MatchAcceptHeader(IEnumerable<MediaTypeWithQualityHeaderValue> sortedAcceptValues, MediaTypeFormatter formatter)
		{
			if (sortedAcceptValues == null)
			{
				throw Error.ArgumentNull("sortedAcceptValues");
			}
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			foreach (MediaTypeWithQualityHeaderValue mediaTypeWithQualityHeaderValue in sortedAcceptValues)
			{
				List<MediaTypeHeaderValue> supportedMediaTypesInternal = formatter.SupportedMediaTypesInternal;
				for (int i = 0; i < supportedMediaTypesInternal.Count; i++)
				{
					MediaTypeHeaderValue mediaTypeHeaderValue = supportedMediaTypesInternal[i];
					if (mediaTypeHeaderValue != null)
					{
						double? quality = mediaTypeWithQualityHeaderValue.Quality;
						double num = 0.0;
						MediaTypeHeaderValueRange mediaTypeHeaderValueRange;
						if (!((quality.GetValueOrDefault() == num) & (quality != null)) && mediaTypeHeaderValue.IsSubsetOf(mediaTypeWithQualityHeaderValue, out mediaTypeHeaderValueRange))
						{
							MediaTypeFormatterMatchRanking mediaTypeFormatterMatchRanking;
							if (mediaTypeHeaderValueRange != MediaTypeHeaderValueRange.SubtypeMediaRange)
							{
								if (mediaTypeHeaderValueRange == MediaTypeHeaderValueRange.AllMediaRange)
								{
									mediaTypeFormatterMatchRanking = MediaTypeFormatterMatchRanking.MatchOnRequestAcceptHeaderAllMediaRange;
								}
								else
								{
									mediaTypeFormatterMatchRanking = MediaTypeFormatterMatchRanking.MatchOnRequestAcceptHeaderLiteral;
								}
							}
							else
							{
								mediaTypeFormatterMatchRanking = MediaTypeFormatterMatchRanking.MatchOnRequestAcceptHeaderSubtypeMediaRange;
							}
							return new MediaTypeFormatterMatch(formatter, mediaTypeHeaderValue, mediaTypeWithQualityHeaderValue.Quality, mediaTypeFormatterMatchRanking);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000086A0 File Offset: 0x000068A0
		protected virtual MediaTypeFormatterMatch MatchRequestMediaType(HttpRequestMessage request, MediaTypeFormatter formatter)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			if (request.Content != null)
			{
				MediaTypeHeaderValue contentType = request.Content.Headers.ContentType;
				if (contentType != null)
				{
					List<MediaTypeHeaderValue> supportedMediaTypesInternal = formatter.SupportedMediaTypesInternal;
					for (int i = 0; i < supportedMediaTypesInternal.Count; i++)
					{
						MediaTypeHeaderValue mediaTypeHeaderValue = supportedMediaTypesInternal[i];
						if (mediaTypeHeaderValue != null && mediaTypeHeaderValue.IsSubsetOf(contentType))
						{
							return new MediaTypeFormatterMatch(formatter, mediaTypeHeaderValue, new double?(1.0), MediaTypeFormatterMatchRanking.MatchOnRequestMediaType);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00008729 File Offset: 0x00006929
		protected virtual bool ShouldMatchOnType(IEnumerable<MediaTypeWithQualityHeaderValue> sortedAcceptValues)
		{
			if (sortedAcceptValues == null)
			{
				throw Error.ArgumentNull("sortedAcceptValues");
			}
			return !this.ExcludeMatchOnTypeOnly || !sortedAcceptValues.Any<MediaTypeWithQualityHeaderValue>();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000874C File Offset: 0x0000694C
		protected virtual MediaTypeFormatterMatch MatchType(Type type, MediaTypeFormatter formatter)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			MediaTypeHeaderValue mediaTypeHeaderValue = null;
			List<MediaTypeHeaderValue> supportedMediaTypesInternal = formatter.SupportedMediaTypesInternal;
			if (supportedMediaTypesInternal.Count > 0)
			{
				mediaTypeHeaderValue = supportedMediaTypesInternal[0];
			}
			return new MediaTypeFormatterMatch(formatter, mediaTypeHeaderValue, new double?(1.0), MediaTypeFormatterMatchRanking.MatchOnCanWriteType);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000087AC File Offset: 0x000069AC
		protected virtual IEnumerable<MediaTypeWithQualityHeaderValue> SortMediaTypeWithQualityHeaderValuesByQFactor(ICollection<MediaTypeWithQualityHeaderValue> headerValues)
		{
			if (headerValues == null)
			{
				throw Error.ArgumentNull("headerValues");
			}
			if (headerValues.Count > 1)
			{
				return headerValues.OrderByDescending((MediaTypeWithQualityHeaderValue m) => m, MediaTypeWithQualityHeaderValueComparer.QualityComparer).ToArray<MediaTypeWithQualityHeaderValue>();
			}
			return headerValues;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00008804 File Offset: 0x00006A04
		protected virtual IEnumerable<StringWithQualityHeaderValue> SortStringWithQualityHeaderValuesByQFactor(ICollection<StringWithQualityHeaderValue> headerValues)
		{
			if (headerValues == null)
			{
				throw Error.ArgumentNull("headerValues");
			}
			if (headerValues.Count > 1)
			{
				return headerValues.OrderByDescending((StringWithQualityHeaderValue m) => m, StringWithQualityHeaderValueComparer.QualityComparer).ToArray<StringWithQualityHeaderValue>();
			}
			return headerValues;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00008859 File Offset: 0x00006A59
		protected virtual MediaTypeFormatterMatch UpdateBestMatch(MediaTypeFormatterMatch current, MediaTypeFormatterMatch potentialReplacement)
		{
			if (potentialReplacement == null)
			{
				return current;
			}
			if (current == null)
			{
				return potentialReplacement;
			}
			if (potentialReplacement.Quality <= current.Quality)
			{
				return current;
			}
			return potentialReplacement;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00008878 File Offset: 0x00006A78
		private static MediaTypeFormatter[] GetWritingFormatters(IEnumerable<MediaTypeFormatter> formatters)
		{
			MediaTypeFormatterCollection mediaTypeFormatterCollection = formatters as MediaTypeFormatterCollection;
			if (mediaTypeFormatterCollection != null)
			{
				return mediaTypeFormatterCollection.WritingFormatters;
			}
			return formatters.AsArray<MediaTypeFormatter>();
		}
	}
}
