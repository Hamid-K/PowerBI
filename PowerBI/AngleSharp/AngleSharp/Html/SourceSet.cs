using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AngleSharp.Css.Values;
using AngleSharp.Dom;
using AngleSharp.Extensions;

namespace AngleSharp.Html
{
	// Token: 0x020000C0 RID: 192
	public sealed class SourceSet
	{
		// Token: 0x060005AF RID: 1455 RVA: 0x0002CD78 File Offset: 0x0002AF78
		private static Regex CreateRegex()
		{
			string text = "(\\([^)]+\\))?\\s*(.+)";
			Regex regex;
			try
			{
				regex = new Regex(text, RegexOptions.ECMAScript | RegexOptions.CultureInvariant);
			}
			catch
			{
				regex = new Regex(text, RegexOptions.ECMAScript);
			}
			return regex;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0002CDBC File Offset: 0x0002AFBC
		public SourceSet(IDocument document)
		{
			this._document = document;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0002CDCB File Offset: 0x0002AFCB
		public static IEnumerable<SourceSet.ImageCandidate> Parse(string srcset)
		{
			string[] sources = srcset.Trim().SplitSpaces();
			int num;
			for (int i = 0; i < sources.Length; i = num + 1)
			{
				string text = sources[i];
				string text2 = null;
				if (text.Length != 0)
				{
					if (text[text.Length - 1] == ',')
					{
						text = text.Remove(text.Length - 1);
						text2 = string.Empty;
					}
					else
					{
						num = i + 1;
						i = num;
						if (num < sources.Length)
						{
							text2 = sources[i];
							int num2 = text2.IndexOf(',');
							if (num2 != -1)
							{
								sources[i] = text2.Substring(num2 + 1);
								text2 = text2.Substring(0, num2);
								num = i - 1;
								i = num;
							}
						}
					}
					yield return new SourceSet.ImageCandidate
					{
						Url = text,
						Descriptor = text2
					};
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0002CDDC File Offset: 0x0002AFDC
		private static SourceSet.MediaSize ParseSize(string sourceSizeStr)
		{
			Match match = SourceSet.SizeParser.Match(sourceSizeStr);
			return new SourceSet.MediaSize
			{
				Media = ((match.Success && match.Groups[1].Success) ? match.Groups[1].Value : string.Empty),
				Length = ((match.Success && match.Groups[2].Success) ? match.Groups[2].Value : string.Empty)
			};
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0002CE6C File Offset: 0x0002B06C
		private double ParseDescriptor(string descriptor, string sizesattr = null)
		{
			string text = sizesattr ?? SourceSet.FullWidth;
			string text2 = descriptor.Trim();
			double widthFromSourceSize = this.GetWidthFromSourceSize(text);
			double num = 1.0;
			string[] array = text2.Split(new char[] { ' ' });
			for (int i = array.Length - 1; i >= 0; i--)
			{
				string text3 = array[i];
				char c = ((text3.Length > 0) ? text3[text3.Length - 1] : '\0');
				if ((c == 'h' || c == 'w') && text3.Length > 2 && text3[text3.Length] == 'v')
				{
					num = (double)text3.Substring(0, text3.Length - 2).ToInteger(0) / widthFromSourceSize;
				}
				else if (c == 'x' && text3.Length > 0)
				{
					num = text3.Substring(0, text3.Length - 1).ToDouble(1.0);
				}
			}
			return num;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0002CF64 File Offset: 0x0002B164
		private double GetWidthFromLength(string length)
		{
			Length length2 = default(Length);
			Length.TryParse(length, out length2);
			return 0.0;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0002CF8C File Offset: 0x0002B18C
		private double GetWidthFromSourceSize(string sourceSizes)
		{
			string[] array = sourceSizes.Trim().Split(new char[] { ',' });
			for (int i = 0; i < array.Length; i++)
			{
				SourceSet.MediaSize mediaSize = SourceSet.ParseSize(array[i]);
				string length = mediaSize.Length;
				string media = mediaSize.Media;
				if (!string.IsNullOrEmpty(length) && (string.IsNullOrEmpty(media) || this._document.DefaultView.MatchMedia(media).IsMatched))
				{
					return this.GetWidthFromLength(length);
				}
			}
			return this.GetWidthFromLength(SourceSet.FullWidth);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0002D00E File Offset: 0x0002B20E
		public IEnumerable<string> GetCandidates(string srcset, string sizes)
		{
			if (!string.IsNullOrEmpty(srcset))
			{
				foreach (SourceSet.ImageCandidate imageCandidate in SourceSet.Parse(srcset))
				{
					yield return imageCandidate.Url;
				}
				IEnumerator<SourceSet.ImageCandidate> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0400052C RID: 1324
		private static readonly string FullWidth = "100vw";

		// Token: 0x0400052D RID: 1325
		private static readonly Regex SizeParser = SourceSet.CreateRegex();

		// Token: 0x0400052E RID: 1326
		private readonly IDocument _document;

		// Token: 0x0200046F RID: 1135
		private sealed class MediaSize
		{
			// Token: 0x17000A66 RID: 2662
			// (get) Token: 0x060023D0 RID: 9168 RVA: 0x0005D692 File Offset: 0x0005B892
			// (set) Token: 0x060023D1 RID: 9169 RVA: 0x0005D69A File Offset: 0x0005B89A
			public string Media { get; set; }

			// Token: 0x17000A67 RID: 2663
			// (get) Token: 0x060023D2 RID: 9170 RVA: 0x0005D6A3 File Offset: 0x0005B8A3
			// (set) Token: 0x060023D3 RID: 9171 RVA: 0x0005D6AB File Offset: 0x0005B8AB
			public string Length { get; set; }
		}

		// Token: 0x02000470 RID: 1136
		public sealed class ImageCandidate
		{
			// Token: 0x17000A68 RID: 2664
			// (get) Token: 0x060023D5 RID: 9173 RVA: 0x0005D6B4 File Offset: 0x0005B8B4
			// (set) Token: 0x060023D6 RID: 9174 RVA: 0x0005D6BC File Offset: 0x0005B8BC
			public string Url { get; set; }

			// Token: 0x17000A69 RID: 2665
			// (get) Token: 0x060023D7 RID: 9175 RVA: 0x0005D6C5 File Offset: 0x0005B8C5
			// (set) Token: 0x060023D8 RID: 9176 RVA: 0x0005D6CD File Offset: 0x0005B8CD
			public string Descriptor { get; set; }
		}
	}
}
