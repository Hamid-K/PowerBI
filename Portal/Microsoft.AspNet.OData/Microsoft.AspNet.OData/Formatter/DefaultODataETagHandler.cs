using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNet.OData.Builder.Conventions;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x02000185 RID: 389
	internal class DefaultODataETagHandler : IETagHandler
	{
		// Token: 0x06000CE1 RID: 3297 RVA: 0x00032D10 File Offset: 0x00030F10
		public EntityTagHeaderValue CreateETag(IDictionary<string, object> properties)
		{
			if (properties == null)
			{
				throw Error.ArgumentNull("properties");
			}
			if (properties.Count == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('"');
			bool flag = true;
			foreach (object obj in properties.Values)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(',');
				}
				string text = ((obj == null) ? "null" : ConventionsHelpers.GetUriRepresentationForValue(obj));
				string text2 = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
				stringBuilder.Append(text2);
			}
			stringBuilder.Append('"');
			return new EntityTagHeaderValue(stringBuilder.ToString(), true);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00032DD4 File Offset: 0x00030FD4
		public IDictionary<string, object> ParseETag(EntityTagHeaderValue etagHeaderValue)
		{
			if (etagHeaderValue == null)
			{
				throw Error.ArgumentNull("etagHeaderValue");
			}
			string[] array = etagHeaderValue.Tag.Trim(new char[] { '"' }).Split(new char[] { ',' });
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			for (int i = 0; i < array.Length; i++)
			{
				byte[] array2 = Convert.FromBase64String(array[i]);
				object obj = ODataUriUtils.ConvertFromUriLiteral(Encoding.UTF8.GetString(array2), ODataVersion.V4);
				if (obj is ODataNullValue)
				{
					obj = null;
				}
				dictionary.Add(i.ToString(CultureInfo.InvariantCulture), obj);
			}
			return dictionary;
		}

		// Token: 0x040003B1 RID: 945
		private const string NullLiteralInETag = "null";

		// Token: 0x040003B2 RID: 946
		private const char Separator = ',';
	}
}
