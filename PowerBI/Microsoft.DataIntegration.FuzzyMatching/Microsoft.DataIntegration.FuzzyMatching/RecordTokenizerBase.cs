using System;
using System.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000EB RID: 235
	[Serializable]
	public class RecordTokenizerBase
	{
		// Token: 0x06000936 RID: 2358 RVA: 0x0002B0A8 File Offset: 0x000292A8
		public RecordTokenizerBase()
		{
			this.NormalizationOptions = new NormalizationOptions();
			this.IsDelimiter = new Predicate<char>(this.IsDelimiterInternal);
			this.InitDelimiterClasses();
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0002B0F4 File Offset: 0x000292F4
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x0002B0FC File Offset: 0x000292FC
		public int DelimiterClasses
		{
			get
			{
				return this.m_delimiterClasses;
			}
			set
			{
				this.m_delimiterClasses = value;
				this.InitDelimiterBitArray();
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0002B10B File Offset: 0x0002930B
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x0002B113 File Offset: 0x00029313
		public Predicate<char> IsDelimiter
		{
			get
			{
				return this.m_isDelimiterDelegate;
			}
			set
			{
				if (value != null)
				{
					this.m_isDelimiterDelegate = value;
				}
				else
				{
					this.m_isDelimiterDelegate = new Predicate<char>(this.IsDelimiterInternal);
				}
				this.InitDelimiterBitArray();
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0002B139 File Offset: 0x00029339
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x0002B141 File Offset: 0x00029341
		public string CustomDelimiters
		{
			get
			{
				return this.m_customDelimiters;
			}
			set
			{
				this.m_customDelimiters = value;
				this.InitDelimiterBitArray();
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0002B150 File Offset: 0x00029350
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x0002B158 File Offset: 0x00029358
		public string CustomNonDelimiters
		{
			get
			{
				return this.m_customNonDelimiters;
			}
			set
			{
				this.m_customNonDelimiters = value;
				this.InitDelimiterBitArray();
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0002B167 File Offset: 0x00029367
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x0002B16F File Offset: 0x0002936F
		public NormalizationOptions NormalizationOptions { get; private set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0002B178 File Offset: 0x00029378
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x0002B185 File Offset: 0x00029385
		public bool IgnoreCase
		{
			get
			{
				return this.NormalizationOptions.IgnoreCase;
			}
			set
			{
				this.NormalizationOptions.IgnoreCase = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0002B193 File Offset: 0x00029393
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x0002B1A0 File Offset: 0x000293A0
		public bool IgnoreKana
		{
			get
			{
				return this.NormalizationOptions.IgnoreKana;
			}
			set
			{
				this.NormalizationOptions.IgnoreKana = value;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0002B1AE File Offset: 0x000293AE
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x0002B1BB File Offset: 0x000293BB
		public bool IgnoreWidth
		{
			get
			{
				return this.NormalizationOptions.IgnoreWidth;
			}
			set
			{
				this.NormalizationOptions.IgnoreWidth = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0002B1C9 File Offset: 0x000293C9
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x0002B1D6 File Offset: 0x000293D6
		public bool IgnoreSymbols
		{
			get
			{
				return this.NormalizationOptions.IgnoreSymbols;
			}
			set
			{
				this.NormalizationOptions.IgnoreSymbols = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0002B1E4 File Offset: 0x000293E4
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x0002B1F1 File Offset: 0x000293F1
		public bool IgnoreNonSpacing
		{
			get
			{
				return this.NormalizationOptions.IgnoreNonSpacing;
			}
			set
			{
				this.NormalizationOptions.IgnoreNonSpacing = value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0002B1FF File Offset: 0x000293FF
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x0002B20C File Offset: 0x0002940C
		public bool ExpandLigatures
		{
			get
			{
				return this.NormalizationOptions.ExpandLigatures;
			}
			set
			{
				this.NormalizationOptions.ExpandLigatures = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x0002B21A File Offset: 0x0002941A
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x0002B227 File Offset: 0x00029427
		public bool MapPrecomposed
		{
			get
			{
				return this.NormalizationOptions.MapPrecomposed;
			}
			set
			{
				this.NormalizationOptions.MapPrecomposed = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0002B235 File Offset: 0x00029435
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x0002B242 File Offset: 0x00029442
		public bool FoldDigits
		{
			get
			{
				return this.NormalizationOptions.FoldDigits;
			}
			set
			{
				this.NormalizationOptions.FoldDigits = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0002B250 File Offset: 0x00029450
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x0002B25D File Offset: 0x0002945D
		public bool FoldCompatibilityZone
		{
			get
			{
				return this.NormalizationOptions.FoldCompatibilityZone;
			}
			set
			{
				this.NormalizationOptions.FoldCompatibilityZone = value;
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0002B26C File Offset: 0x0002946C
		public bool IsDelimiterInternal(char c)
		{
			return this.m_customNonDelimiters.IndexOf(c) == -1 && ((this.IsDelimiterClass(RecordTokenizerBase.CharClass2.WhiteSpace) && char.IsWhiteSpace(c)) || (this.IsDelimiterClass(RecordTokenizerBase.CharClass2.Punctuation) && char.IsPunctuation(c)) || (this.IsDelimiterClass(RecordTokenizerBase.CharClass2.Symbol) && char.IsSymbol(c)) || (this.IsDelimiterClass(RecordTokenizerBase.CharClass2.Letter) && char.IsLetter(c)) || (this.IsDelimiterClass(RecordTokenizerBase.CharClass2.Control) && char.IsControl(c)) || (this.IsDelimiterClass(RecordTokenizerBase.CharClass2.Separator) && char.IsSeparator(c)) || (this.IsDelimiterClass(RecordTokenizerBase.CharClass2.Digit) && char.IsDigit(c)) || (this.IsDelimiterClass(RecordTokenizerBase.CharClass2.HighSurrogate) && char.IsHighSurrogate(c)) || (this.IsDelimiterClass(RecordTokenizerBase.CharClass2.LowSurrogate) && char.IsLowSurrogate(c)) || (this.IsDelimiterClass(RecordTokenizerBase.CharClass2.Surrogate) && char.IsSurrogate(c)) || (this.IsDelimiterClass(RecordTokenizerBase.CharClass2.Upper) && char.IsUpper(c)) || (this.IsDelimiterClass(RecordTokenizerBase.CharClass2.Lower) && char.IsLower(c)) || this.m_customDelimiters.IndexOf(c) >= 0);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0002B396 File Offset: 0x00029596
		private void InitDelimiterClasses()
		{
			this.m_delimiterClasses = 707;
			this.InitDelimiterBitArray();
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0002B3A9 File Offset: 0x000295A9
		public bool IsDelimiterClass(RecordTokenizerBase.CharClass2 charClass)
		{
			return (this.m_delimiterClasses & (int)charClass) != 0;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0002B3B6 File Offset: 0x000295B6
		public void SetDelimiterClass(RecordTokenizerBase.CharClass2 charClass, bool isDelimiter)
		{
			if (isDelimiter)
			{
				this.m_delimiterClasses |= (int)charClass;
			}
			else
			{
				this.m_delimiterClasses &= (int)(~(int)charClass);
			}
			this.InitDelimiterBitArray();
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0002B3E0 File Offset: 0x000295E0
		private void InitDelimiterBitArray()
		{
			this.m_isDelimiterBitArray = new BitArray(65536);
			for (int i = 0; i < 65535; i++)
			{
				if (this.m_isDelimiterDelegate.Invoke((char)i))
				{
					this.m_isDelimiterBitArray.Set(i, true);
				}
			}
		}

		// Token: 0x040003A1 RID: 929
		private int m_delimiterClasses;

		// Token: 0x040003A2 RID: 930
		private Predicate<char> m_isDelimiterDelegate;

		// Token: 0x040003A3 RID: 931
		private string m_customDelimiters = string.Empty;

		// Token: 0x040003A4 RID: 932
		private string m_customNonDelimiters = string.Empty;

		// Token: 0x040003A5 RID: 933
		internal BitArray m_isDelimiterBitArray;

		// Token: 0x02000186 RID: 390
		public enum CharClass2
		{
			// Token: 0x04000640 RID: 1600
			WhiteSpace = 1,
			// Token: 0x04000641 RID: 1601
			Punctuation,
			// Token: 0x04000642 RID: 1602
			Letter = 4,
			// Token: 0x04000643 RID: 1603
			Digit = 8,
			// Token: 0x04000644 RID: 1604
			HighSurrogate = 16,
			// Token: 0x04000645 RID: 1605
			LowSurrogate = 32,
			// Token: 0x04000646 RID: 1606
			Control = 64,
			// Token: 0x04000647 RID: 1607
			Separator = 128,
			// Token: 0x04000648 RID: 1608
			Surrogate = 256,
			// Token: 0x04000649 RID: 1609
			Symbol = 512,
			// Token: 0x0400064A RID: 1610
			Upper = 1024,
			// Token: 0x0400064B RID: 1611
			Lower = 2048
		}
	}
}
