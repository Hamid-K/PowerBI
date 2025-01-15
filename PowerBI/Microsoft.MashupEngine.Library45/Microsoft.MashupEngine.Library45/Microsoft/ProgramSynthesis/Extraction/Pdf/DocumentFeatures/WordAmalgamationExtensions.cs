using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CD2 RID: 3282
	[NullableContext(1)]
	[Nullable(0)]
	internal static class WordAmalgamationExtensions
	{
		// Token: 0x06005462 RID: 21602 RVA: 0x001099AD File Offset: 0x00107BAD
		public static bool HasAlignmentDotRow<T>(this IWordAmalgamation<T> amalgamation) where T : class, IWordAmalgamation<T>
		{
			return amalgamation.AfterAlignmentDotRow != null || amalgamation.BeforeAlignmentDotRow != null;
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x001099C4 File Offset: 0x00107BC4
		[return: Nullable(new byte[] { 1, 2 })]
		public static IEnumerable<FontCharacteristics> GetCommonFonts<T>(this IEnumerable<IWordAmalgamation<T>> amalgamations, bool ignoreSymbols) where T : class, IWordAmalgamation<T>
		{
			IEnumerable<IWordAmalgamation<T>> enumerable = amalgamations;
			Func<IWordAmalgamation<T>, IEnumerable<FontCharacteristics>> func;
			if (ignoreSymbols)
			{
				enumerable = enumerable.Where((IWordAmalgamation<T> cell) => cell.Children.Any((IWord word) => !word.IsSymbol));
				func = (IWordAmalgamation<T> cell) => from word in cell.Children
					where !word.IsSymbol
					select word.Font;
			}
			else
			{
				func = (IWordAmalgamation<T> cell) => cell.Children.Select((IWord word) => word.Font);
			}
			return enumerable.Select(func).Intersect<FontCharacteristics>();
		}
	}
}
