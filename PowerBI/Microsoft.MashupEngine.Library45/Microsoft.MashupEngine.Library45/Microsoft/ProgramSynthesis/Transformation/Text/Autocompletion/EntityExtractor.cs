using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion
{
	// Token: 0x02001E05 RID: 7685
	internal static class EntityExtractor
	{
		// Token: 0x17002AAB RID: 10923
		// (get) Token: 0x0601013D RID: 65853 RVA: 0x00373897 File Offset: 0x00371A97
		public static IReadOnlyList<EntityBasedTokenizer> BuiltInTokenizers
		{
			get
			{
				return EntityExtractor.BuiltinTokenizersLazy.Value;
			}
		}

		// Token: 0x0601013E RID: 65854 RVA: 0x003738A4 File Offset: 0x00371AA4
		private static IReadOnlyList<EntityBasedTokenizer> GetBuiltInTokenizers()
		{
			return (from t in typeof(EntityBasedTokenizer).AllNonAbstractSubTypesInAssembly().AsType()
				where t != typeof(CharCategoryBasedTokenizer)
				select (EntityBasedTokenizer)Activator.CreateInstance(t)).ToList<EntityBasedTokenizer>();
		}

		// Token: 0x0601013F RID: 65855 RVA: 0x00373914 File Offset: 0x00371B14
		public static IEnumerable<EntityToken> Extract(string str, bool useBuiltInTokenizers = true, params EntityBasedTokenizer[] additionalTokenizers)
		{
			IEnumerable<EntityBasedTokenizer> enumerable;
			if (!useBuiltInTokenizers)
			{
				enumerable = Enumerable.Empty<EntityBasedTokenizer>();
			}
			else
			{
				IEnumerable<EntityBasedTokenizer> builtInTokenizers = EntityExtractor.BuiltInTokenizers;
				enumerable = builtInTokenizers;
			}
			List<EntityToken> list = enumerable.Concat(additionalTokenizers).SelectMany((EntityBasedTokenizer t) => t.Tokenize(str)).ToList<EntityToken>();
			IntervalSet intervalSet = new IntervalSet(0, str.Length);
			foreach (EntityToken entityToken in list)
			{
				intervalSet.CoverInterval(entityToken.Start, entityToken.End);
			}
			CharCategoryBasedTokenizer charCategoryBasedTokenizer = new CharCategoryBasedTokenizer();
			foreach (Interval interval in intervalSet.UncoveredIntervals)
			{
				list.AddRange(charCategoryBasedTokenizer.Tokenize(str, interval.Start, interval.Start + interval.Length));
			}
			return list;
		}

		// Token: 0x06010140 RID: 65856 RVA: 0x00373A2C File Offset: 0x00371C2C
		public static IEnumerable<EntityToken> Extract(IEnumerable<string> strs, bool useBuiltInTokenizers = true, params EntityBasedTokenizer[] additionalTokenizers)
		{
			return strs.SelectMany((string str) => EntityExtractor.Extract(str, useBuiltInTokenizers, additionalTokenizers));
		}

		// Token: 0x040060D7 RID: 24791
		private static readonly Lazy<IReadOnlyList<EntityBasedTokenizer>> BuiltinTokenizersLazy = new Lazy<IReadOnlyList<EntityBasedTokenizer>>(new Func<IReadOnlyList<EntityBasedTokenizer>>(EntityExtractor.GetBuiltInTokenizers));
	}
}
