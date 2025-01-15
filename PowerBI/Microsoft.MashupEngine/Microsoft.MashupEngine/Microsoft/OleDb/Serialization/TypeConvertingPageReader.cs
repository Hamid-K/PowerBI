using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02000178 RID: 376
	internal static class TypeConvertingPageReader
	{
		// Token: 0x06000723 RID: 1827 RVA: 0x0000C52C File Offset: 0x0000A72C
		public static IPageReader New(IPageReader reader, TableSchema targetSchemaTable, IList<TypeConversion> typeConversions)
		{
			Dictionary<int, ColumnConversion> dictionary = TypeConversion.GetTypeConversions(reader.Schema, targetSchemaTable, typeConversions).ToDictionary((KeyValuePair<int, TypeConversion> kvp) => kvp.Key, (KeyValuePair<int, TypeConversion> kvp) => kvp.Value.ColumnConversion);
			return ColumnConvertingPageReader.New(reader, dictionary);
		}
	}
}
