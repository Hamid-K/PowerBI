using System;
using System.Diagnostics;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000EC RID: 236
	[Serializable]
	public sealed class NormalizationOptions : IEquatable<NormalizationOptions>
	{
		// Token: 0x06000958 RID: 2392 RVA: 0x0002B429 File Offset: 0x00029629
		public NormalizationOptions()
		{
			this.IgnoreCase = true;
			this.IgnoreNonSpacing = true;
			this.IgnoreSymbols = true;
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0002B446 File Offset: 0x00029646
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x0002B457 File Offset: 0x00029657
		public bool IgnoreCase
		{
			get
			{
				return (this.m_mapStringFlags & StringNormalization.LCMapStringFlags.LCMAP_LOWERCASE) > StringNormalization.LCMapStringFlags.NONE;
			}
			set
			{
				if (value)
				{
					this.m_mapStringFlags |= StringNormalization.LCMapStringFlags.LCMAP_LOWERCASE;
					return;
				}
				this.m_mapStringFlags &= ~StringNormalization.LCMapStringFlags.LCMAP_LOWERCASE;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0002B481 File Offset: 0x00029681
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x0002B492 File Offset: 0x00029692
		public bool IgnoreKana
		{
			get
			{
				return (this.m_mapStringFlags & StringNormalization.LCMapStringFlags.LCMAP_HIRAGANA) > StringNormalization.LCMapStringFlags.NONE;
			}
			set
			{
				if (value)
				{
					this.m_mapStringFlags |= StringNormalization.LCMapStringFlags.LCMAP_HIRAGANA;
					return;
				}
				this.m_mapStringFlags &= ~StringNormalization.LCMapStringFlags.LCMAP_HIRAGANA;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0002B4BC File Offset: 0x000296BC
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x0002B4CD File Offset: 0x000296CD
		public bool IgnoreWidth
		{
			get
			{
				return (this.m_mapStringFlags & StringNormalization.LCMapStringFlags.LCMAP_HALFWIDTH) > StringNormalization.LCMapStringFlags.NONE;
			}
			set
			{
				if (value)
				{
					this.m_mapStringFlags |= StringNormalization.LCMapStringFlags.LCMAP_HALFWIDTH;
					return;
				}
				this.m_mapStringFlags &= ~StringNormalization.LCMapStringFlags.LCMAP_HALFWIDTH;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0002B4F7 File Offset: 0x000296F7
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x0002B504 File Offset: 0x00029704
		public bool IgnoreSymbols
		{
			get
			{
				return (this.m_mapStringFlags & StringNormalization.LCMapStringFlags.NORM_IGNORESYMBOLS) > StringNormalization.LCMapStringFlags.NONE;
			}
			set
			{
				if (value)
				{
					this.m_mapStringFlags |= StringNormalization.LCMapStringFlags.NORM_IGNORESYMBOLS;
					return;
				}
				this.m_mapStringFlags &= ~StringNormalization.LCMapStringFlags.NORM_IGNORESYMBOLS;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0002B527 File Offset: 0x00029727
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x0002B534 File Offset: 0x00029734
		public bool IgnoreNonSpacing
		{
			get
			{
				return (this.m_mapStringFlags & StringNormalization.LCMapStringFlags.NORM_IGNORENONSPACE) > StringNormalization.LCMapStringFlags.NONE;
			}
			set
			{
				if (value)
				{
					this.m_mapStringFlags |= StringNormalization.LCMapStringFlags.NORM_IGNORENONSPACE;
					return;
				}
				this.m_mapStringFlags &= ~StringNormalization.LCMapStringFlags.NORM_IGNORENONSPACE;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0002B557 File Offset: 0x00029757
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x0002B568 File Offset: 0x00029768
		public bool ExpandLigatures
		{
			get
			{
				return (this.m_FoldStringFlags & StringNormalization.FoldStringFlags.MAP_EXPAND_LIGATURES) > StringNormalization.FoldStringFlags.NONE;
			}
			set
			{
				if (value)
				{
					this.m_FoldStringFlags |= StringNormalization.FoldStringFlags.MAP_EXPAND_LIGATURES;
					return;
				}
				this.m_FoldStringFlags &= ~StringNormalization.FoldStringFlags.MAP_EXPAND_LIGATURES;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0002B592 File Offset: 0x00029792
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x0002B5A0 File Offset: 0x000297A0
		public bool MapPrecomposed
		{
			get
			{
				return (this.m_FoldStringFlags & StringNormalization.FoldStringFlags.MAP_PRECOMPOSED) > StringNormalization.FoldStringFlags.NONE;
			}
			set
			{
				if (value)
				{
					this.m_FoldStringFlags |= StringNormalization.FoldStringFlags.MAP_PRECOMPOSED;
					return;
				}
				this.m_FoldStringFlags &= ~StringNormalization.FoldStringFlags.MAP_PRECOMPOSED;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0002B5C4 File Offset: 0x000297C4
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x0002B5D5 File Offset: 0x000297D5
		public bool FoldDigits
		{
			get
			{
				return (this.m_FoldStringFlags & StringNormalization.FoldStringFlags.MAP_FOLDDIGITS) > StringNormalization.FoldStringFlags.NONE;
			}
			set
			{
				if (value)
				{
					this.m_FoldStringFlags |= StringNormalization.FoldStringFlags.MAP_FOLDDIGITS;
					return;
				}
				this.m_FoldStringFlags &= ~StringNormalization.FoldStringFlags.MAP_FOLDDIGITS;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0002B5FF File Offset: 0x000297FF
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x0002B60D File Offset: 0x0002980D
		public bool FoldCompatibilityZone
		{
			get
			{
				return (this.m_FoldStringFlags & StringNormalization.FoldStringFlags.MAP_FOLDCZONE) > StringNormalization.FoldStringFlags.NONE;
			}
			set
			{
				if (value)
				{
					this.m_FoldStringFlags |= StringNormalization.FoldStringFlags.MAP_FOLDCZONE;
					return;
				}
				this.m_FoldStringFlags &= ~StringNormalization.FoldStringFlags.MAP_FOLDCZONE;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0002B631 File Offset: 0x00029831
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x0002B639 File Offset: 0x00029839
		public StringNormalization.FoldStringFlags FoldStringFlags
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_FoldStringFlags;
			}
			internal set
			{
				this.m_FoldStringFlags = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0002B642 File Offset: 0x00029842
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x0002B64A File Offset: 0x0002984A
		public StringNormalization.LCMapStringFlags MapStringFlags
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_mapStringFlags;
			}
			internal set
			{
				this.m_mapStringFlags = value;
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0002B653 File Offset: 0x00029853
		public bool Equals(NormalizationOptions ti)
		{
			return this.FoldStringFlags == ti.FoldStringFlags && this.MapStringFlags == ti.MapStringFlags;
		}

		// Token: 0x040003A7 RID: 935
		private StringNormalization.FoldStringFlags m_FoldStringFlags;

		// Token: 0x040003A8 RID: 936
		private StringNormalization.LCMapStringFlags m_mapStringFlags;
	}
}
