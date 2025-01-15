using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x02000382 RID: 898
	internal class OpenApiHelper
	{
		// Token: 0x06001FA6 RID: 8102 RVA: 0x00052410 File Offset: 0x00050610
		public static IList<string> ToStringList(ListValue list, IEngineHost host)
		{
			return list.Select((IValueReference vr) => TypeValue.Text.Cast(vr.Value, host).AsString).ToList<string>();
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x00052444 File Offset: 0x00050644
		public static void AppendToSecurityList(IList<IDictionary<string, IEnumerable<string>>> securityList, ListValue securityListValue, IEngineHost host)
		{
			foreach (IValueReference valueReference in securityListValue)
			{
				Dictionary<string, IEnumerable<string>> dictionary = new Dictionary<string, IEnumerable<string>>();
				RecordValue asRecord = valueReference.Value.AsRecord;
				if (asRecord != null)
				{
					foreach (NamedValue namedValue in asRecord.GetFields())
					{
						ListValue listValue = namedValue.Value as ListValue;
						if (listValue != null)
						{
							dictionary.Add(namedValue.Key, OpenApiHelper.ToStringList(listValue, host));
						}
					}
					securityList.Add(dictionary);
				}
			}
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x0005250C File Offset: 0x0005070C
		public static void AppendToMimeList(IList<string> mimeList, Value mimeValues, IEngineHost host)
		{
			if (mimeValues != null)
			{
				ListValue listValue = mimeValues as ListValue;
				if (listValue != null)
				{
					foreach (string text in OpenApiHelper.ToStringList(listValue, host))
					{
						mimeList.Add(text);
					}
				}
			}
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x00052568 File Offset: 0x00050768
		public static void AppendToResolvedParameterList(IList<OpenApiParameterDefinition> resolvedParameterList, ListValue parameterListValue, OpenApiDocument document)
		{
			if (parameterListValue != null)
			{
				foreach (IValueReference valueReference in parameterListValue)
				{
					OpenApiParameterDefinition orCreateParameter = document.GetOrCreateParameter(valueReference.Value.AsRecord);
					resolvedParameterList.Add(orCreateParameter);
				}
			}
		}
	}
}
