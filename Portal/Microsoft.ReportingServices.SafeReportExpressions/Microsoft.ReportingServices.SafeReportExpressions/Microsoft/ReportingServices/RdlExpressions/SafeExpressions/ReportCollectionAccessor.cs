using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000023 RID: 35
	internal sealed class ReportCollectionAccessor
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00003023 File Offset: 0x00001223
		public ReportCollectionAccessor(ISafeExpressionsReportContext safeExpressionsReportContext)
		{
			this._safeExpressionsContext = safeExpressionsReportContext;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003034 File Offset: 0x00001234
		public bool IsValidCollectionName(string collectionName)
		{
			string text = collectionName.ToUpperInvariant();
			return ReportCollectionAccessor.SupportedCollections.Contains(text);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003054 File Offset: 0x00001254
		public object GetValue(string collectionName, string itemName)
		{
			string text = collectionName.ToUpperInvariant();
			if (text != null)
			{
				switch (text.Length)
				{
				case 4:
					if (text == "USER")
					{
						return this._safeExpressionsContext.GetUser(itemName);
					}
					break;
				case 6:
					if (text == "FIELDS")
					{
						return this._safeExpressionsContext.GetField(itemName);
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
								return this._safeExpressionsContext.GetLookup(itemName);
							}
						}
					}
					else if (text == "GLOBALS")
					{
						return this._safeExpressionsContext.GetGlobal(itemName);
					}
					break;
				}
				case 9:
					if (text == "VARIABLES")
					{
						return this._safeExpressionsContext.GetVariable(itemName);
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
								return this._safeExpressionsContext.GetParameter(itemName);
							}
						}
					}
					else if (text == "AGGREGATES")
					{
						return this._safeExpressionsContext.GetAggregate(itemName);
					}
					break;
				}
				}
			}
			throw new NotSupportedException("The specified name of collection is not supported: " + text);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000031AF File Offset: 0x000013AF
		public Type GetCollectionItemType(string collectionName)
		{
			return this._safeExpressionsContext.GetCollectionItemType(collectionName);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000031BD File Offset: 0x000013BD
		public bool IsCollection(string collectionName)
		{
			return this._safeExpressionsContext.IsCollection(collectionName);
		}

		// Token: 0x04000027 RID: 39
		private readonly ISafeExpressionsReportContext _safeExpressionsContext;

		// Token: 0x04000028 RID: 40
		private const string AggregatesToken = "AGGREGATES";

		// Token: 0x04000029 RID: 41
		private const string FieldsToken = "FIELDS";

		// Token: 0x0400002A RID: 42
		private const string GlobalsToken = "GLOBALS";

		// Token: 0x0400002B RID: 43
		private const string LookupsToken = "LOOKUPS";

		// Token: 0x0400002C RID: 44
		private const string ParametersToken = "PARAMETERS";

		// Token: 0x0400002D RID: 45
		private const string UserToken = "USER";

		// Token: 0x0400002E RID: 46
		private const string VariablesToken = "VARIABLES";

		// Token: 0x0400002F RID: 47
		private static readonly HashSet<string> SupportedCollections = new HashSet<string> { "AGGREGATES", "FIELDS", "GLOBALS", "LOOKUPS", "PARAMETERS", "USER", "VARIABLES" };
	}
}
