using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005FF RID: 1535
	internal sealed class OdbcImplicitTypeConversions
	{
		// Token: 0x0600308E RID: 12430 RVA: 0x00092DAC File Offset: 0x00090FAC
		private OdbcImplicitTypeConversions(Dictionary<string, string> conversions)
		{
			this.conversions = conversions;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x00092DBC File Offset: 0x00090FBC
		public bool TryGetImplicitConversion(string type1, string type2, out string resultType)
		{
			string key = OdbcImplicitTypeConversions.GetKey(type1, type2);
			return this.conversions.TryGetValue(key, out resultType);
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x00092DE0 File Offset: 0x00090FE0
		public static OdbcImplicitTypeConversions New(Value value)
		{
			if (value.IsNull)
			{
				return OdbcImplicitTypeConversions.None;
			}
			TableValue asTable = value.AsTable;
			Dictionary<string, string> dictionary = new Dictionary<string, string>(asTable.Count);
			foreach (IValueReference valueReference in asTable)
			{
				RecordValue asRecord = valueReference.Value.AsRecord;
				string asString = asRecord["Type1"].AsString;
				string asString2 = asRecord["Type2"].AsString;
				string asString3 = asRecord["ResultType"].AsString;
				string key = OdbcImplicitTypeConversions.GetKey(asString, asString2);
				if (dictionary.ContainsKey(key))
				{
					throw ValueException.NewExpressionError<Message0>(Strings.OdbcImplicitTypeConversionDuplicate, asRecord, null);
				}
				dictionary.Add(key, asString3);
			}
			return new OdbcImplicitTypeConversions(dictionary);
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x00092EAC File Offset: 0x000910AC
		private static string GetKey(string type1, string type2)
		{
			if (string.Compare(type1, type2, StringComparison.Ordinal) < 0)
			{
				string text = type1;
				type1 = type2;
				type2 = text;
			}
			return type1.Replace("/", "//") + "/" + type2.Replace("/", "//");
		}

		// Token: 0x04001544 RID: 5444
		private static readonly OdbcImplicitTypeConversions None = new OdbcImplicitTypeConversions(new Dictionary<string, string>(0));

		// Token: 0x04001545 RID: 5445
		private readonly Dictionary<string, string> conversions;
	}
}
