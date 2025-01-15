using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000035 RID: 53
	[NullableContext(2)]
	[Nullable(0)]
	public static class DefaultWriterProperties
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000057F0 File Offset: 0x000039F0
		// (set) Token: 0x06000153 RID: 339 RVA: 0x000057F8 File Offset: 0x000039F8
		public static bool? EnableDictionary { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005800 File Offset: 0x00003A00
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00005808 File Offset: 0x00003A08
		public static bool? EnableStatistics { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005810 File Offset: 0x00003A10
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00005818 File Offset: 0x00003A18
		public static Compression? Compression { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005820 File Offset: 0x00003A20
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00005828 File Offset: 0x00003A28
		public static int? CompressionLevel { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005830 File Offset: 0x00003A30
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00005838 File Offset: 0x00003A38
		public static string CreatedBy { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005840 File Offset: 0x00003A40
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00005848 File Offset: 0x00003A48
		public static long? DataPagesize { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00005850 File Offset: 0x00003A50
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00005858 File Offset: 0x00003A58
		public static long? DictionaryPagesizeLimit { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00005860 File Offset: 0x00003A60
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00005868 File Offset: 0x00003A68
		public static Encoding? Encoding { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005870 File Offset: 0x00003A70
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00005878 File Offset: 0x00003A78
		public static long? MaxRowGroupLength { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00005880 File Offset: 0x00003A80
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00005888 File Offset: 0x00003A88
		public static ParquetVersion? Version { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00005890 File Offset: 0x00003A90
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00005898 File Offset: 0x00003A98
		public static long? WriteBatchSize { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000058A0 File Offset: 0x00003AA0
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000058A8 File Offset: 0x00003AA8
		public static bool? WritePageIndex { get; set; }
	}
}
