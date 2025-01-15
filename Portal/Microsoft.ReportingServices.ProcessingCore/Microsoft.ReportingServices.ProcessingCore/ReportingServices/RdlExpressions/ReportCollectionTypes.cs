using System;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x0200056C RID: 1388
	internal static class ReportCollectionTypes
	{
		// Token: 0x060050A3 RID: 20643 RVA: 0x00152C90 File Offset: 0x00150E90
		public static Type GetCollectionItemType(string collectionName)
		{
			string text = collectionName.ToUpperInvariant();
			if (text != null)
			{
				switch (text.Length)
				{
				case 5:
					if (text == "USERS")
					{
						return typeof(User);
					}
					break;
				case 6:
					if (text == "FIELDS")
					{
						return typeof(Field);
					}
					break;
				case 7:
				{
					char c = text[0];
					if (c != 'G')
					{
						if (c == 'L')
						{
							if (text == "LOOKUPS")
							{
								return typeof(Lookup);
							}
						}
					}
					else if (text == "GLOBALS")
					{
						return typeof(Globals);
					}
					break;
				}
				case 9:
					if (text == "VARIABLES")
					{
						return typeof(Variable);
					}
					break;
				case 10:
				{
					char c = text[0];
					if (c != 'A')
					{
						if (c == 'P')
						{
							if (text == "PARAMETERS")
							{
								return typeof(Parameter);
							}
						}
					}
					else if (text == "AGGREGATES")
					{
						return typeof(object);
					}
					break;
				}
				}
			}
			return null;
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x00152DC4 File Offset: 0x00150FC4
		public static bool IsCollection(string collectionName)
		{
			string text = collectionName.ToUpperInvariant();
			return text == "AGGREGATES" || text == "FIELDS" || text == "LOOKUPS" || text == "PARAMETERS" || text == "VARIABLES";
		}
	}
}
