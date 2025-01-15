using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000047 RID: 71
	internal class MediaTypeWithQualityHeaderValueComparer : IComparer<MediaTypeWithQualityHeaderValue>
	{
		// Token: 0x060002CC RID: 716 RVA: 0x00004BD2 File Offset: 0x00002DD2
		private MediaTypeWithQualityHeaderValueComparer()
		{
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00009B38 File Offset: 0x00007D38
		public static MediaTypeWithQualityHeaderValueComparer QualityComparer
		{
			get
			{
				return MediaTypeWithQualityHeaderValueComparer._mediaTypeComparer;
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00009B40 File Offset: 0x00007D40
		public int Compare(MediaTypeWithQualityHeaderValue mediaType1, MediaTypeWithQualityHeaderValue mediaType2)
		{
			if (mediaType1 == mediaType2)
			{
				return 0;
			}
			int num = MediaTypeWithQualityHeaderValueComparer.CompareBasedOnQualityFactor(mediaType1, mediaType2);
			if (num == 0)
			{
				ParsedMediaTypeHeaderValue parsedMediaTypeHeaderValue = new ParsedMediaTypeHeaderValue(mediaType1);
				ParsedMediaTypeHeaderValue parsedMediaTypeHeaderValue2 = new ParsedMediaTypeHeaderValue(mediaType2);
				if (!parsedMediaTypeHeaderValue.TypesEqual(ref parsedMediaTypeHeaderValue2))
				{
					if (parsedMediaTypeHeaderValue.IsAllMediaRange)
					{
						return -1;
					}
					if (parsedMediaTypeHeaderValue2.IsAllMediaRange)
					{
						return 1;
					}
					if (parsedMediaTypeHeaderValue.IsSubtypeMediaRange && !parsedMediaTypeHeaderValue2.IsSubtypeMediaRange)
					{
						return -1;
					}
					if (!parsedMediaTypeHeaderValue.IsSubtypeMediaRange && parsedMediaTypeHeaderValue2.IsSubtypeMediaRange)
					{
						return 1;
					}
				}
				else if (!parsedMediaTypeHeaderValue.SubTypesEqual(ref parsedMediaTypeHeaderValue2))
				{
					if (parsedMediaTypeHeaderValue.IsSubtypeMediaRange)
					{
						return -1;
					}
					if (parsedMediaTypeHeaderValue2.IsSubtypeMediaRange)
					{
						return 1;
					}
				}
			}
			return num;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00009BDC File Offset: 0x00007DDC
		private static int CompareBasedOnQualityFactor(MediaTypeWithQualityHeaderValue mediaType1, MediaTypeWithQualityHeaderValue mediaType2)
		{
			double num = mediaType1.Quality ?? 1.0;
			double num2 = mediaType2.Quality ?? 1.0;
			double num3 = num - num2;
			if (num3 < 0.0)
			{
				return -1;
			}
			if (num3 > 0.0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x040000CE RID: 206
		private static readonly MediaTypeWithQualityHeaderValueComparer _mediaTypeComparer = new MediaTypeWithQualityHeaderValueComparer();
	}
}
