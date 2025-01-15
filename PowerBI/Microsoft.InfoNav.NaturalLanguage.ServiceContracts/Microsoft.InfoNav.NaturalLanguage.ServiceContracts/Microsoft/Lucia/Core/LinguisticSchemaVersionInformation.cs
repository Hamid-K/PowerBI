using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000063 RID: 99
	[ImmutableObject(true)]
	internal static class LinguisticSchemaVersionInformation
	{
		// Token: 0x06000177 RID: 375 RVA: 0x000043B4 File Offset: 0x000025B4
		internal static LinguisticSchemaVersion DetermineVersion(string xmlns)
		{
			LinguisticSchemaVersion linguisticSchemaVersion;
			if (!LinguisticSchemaVersionInformation.LinguisticSchemaInformation.TryGetValue(xmlns, out linguisticSchemaVersion))
			{
				return LinguisticSchemaVersion.Unknown;
			}
			return linguisticSchemaVersion;
		}

		// Token: 0x040001B2 RID: 434
		private const string V000 = "http://schemas.microsoft.com/sqlserver/2012/08/linguisticschema";

		// Token: 0x040001B3 RID: 435
		private const string V001 = "http://schemas.microsoft.com/sqlserver/2013/11/linguisticschema";

		// Token: 0x040001B4 RID: 436
		private const string V002 = "http://schemas.microsoft.com/sqlserver/2014/01/linguisticschema";

		// Token: 0x040001B5 RID: 437
		private const string V003 = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema";

		// Token: 0x040001B6 RID: 438
		internal const string DataContractNamespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema";

		// Token: 0x040001B7 RID: 439
		internal static readonly string LatestNamespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema";

		// Token: 0x040001B8 RID: 440
		internal static readonly ReadOnlyDictionary<string, LinguisticSchemaVersion> LinguisticSchemaInformation = new Dictionary<string, LinguisticSchemaVersion>(StringComparer.Ordinal)
		{
			{
				"http://schemas.microsoft.com/sqlserver/2012/08/linguisticschema",
				LinguisticSchemaVersion.V000
			},
			{
				"http://schemas.microsoft.com/sqlserver/2013/11/linguisticschema",
				LinguisticSchemaVersion.V001
			},
			{
				"http://schemas.microsoft.com/sqlserver/2014/01/linguisticschema",
				LinguisticSchemaVersion.V002
			},
			{
				"http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema",
				LinguisticSchemaVersion.V003
			}
		}.AsReadOnlyDictionary<string, LinguisticSchemaVersion>();
	}
}
