using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Routing.Conventions;
using Microsoft.OData;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Template
{
	// Token: 0x02000089 RID: 137
	public class KeySegmentTemplate : ODataPathSegmentTemplate
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x0001004B File Offset: 0x0000E24B
		public KeySegmentTemplate(KeySegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			this.Segment = segment;
			this.ParameterMappings = KeySegmentTemplate.BuildKeyMappings(segment.Keys);
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00010079 File Offset: 0x0000E279
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x00010081 File Offset: 0x0000E281
		public KeySegment Segment { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001008A File Offset: 0x0000E28A
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x00010092 File Offset: 0x0000E292
		public IDictionary<string, string> ParameterMappings { get; private set; }

		// Token: 0x060004FA RID: 1274 RVA: 0x0001009C File Offset: 0x0000E29C
		public override bool TryMatch(ODataPathSegment pathSegment, IDictionary<string, object> values)
		{
			KeySegment keySegment = pathSegment as KeySegment;
			return keySegment != null && keySegment.TryMatch(this.ParameterMappings, values);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000100C4 File Offset: 0x0000E2C4
		internal static IDictionary<string, string> BuildKeyMappings(IEnumerable<KeyValuePair<string, object>> keys)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (KeyValuePair<string, object> keyValuePair in keys)
			{
				UriTemplateExpression uriTemplateExpression = keyValuePair.Value as UriTemplateExpression;
				string text;
				if (uriTemplateExpression != null)
				{
					text = uriTemplateExpression.LiteralText.Trim();
				}
				else
				{
					text = keyValuePair.Value as string;
				}
				if (text == null || !RoutingConventionHelpers.IsRouteParameter(text))
				{
					throw new ODataException(Error.Format(SRResources.KeyTemplateMustBeInCurlyBraces, new object[] { keyValuePair.Value, keyValuePair.Key }));
				}
				text = text.Substring(1, text.Length - 2);
				if (string.IsNullOrEmpty(text))
				{
					throw new ODataException(Error.Format(SRResources.EmptyKeyTemplate, new object[] { keyValuePair.Value, keyValuePair.Key }));
				}
				dictionary[keyValuePair.Key] = text;
			}
			return dictionary;
		}
	}
}
