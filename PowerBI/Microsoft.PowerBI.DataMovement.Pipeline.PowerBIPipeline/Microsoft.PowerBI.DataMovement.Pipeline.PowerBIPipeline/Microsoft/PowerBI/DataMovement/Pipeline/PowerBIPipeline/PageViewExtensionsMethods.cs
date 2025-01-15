using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x0200000D RID: 13
	internal static class PageViewExtensionsMethods
	{
		// Token: 0x06000054 RID: 84 RVA: 0x000037A8 File Offset: 0x000019A8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static Tuple<string, int> MapColumn(this Tuple<string, string> c, Dictionary<string, int> schemaTableLookupTable)
		{
			int num;
			if (schemaTableLookupTable.TryGetValue(c.Item1, out num))
			{
				return new Tuple<string, int>(c.Item2, num);
			}
			throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Column '{0}' not found in underlying page reader schema table", c.Item1));
		}
	}
}
