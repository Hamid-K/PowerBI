using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Json;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E05 RID: 3589
	internal class CdpaExtraParameters : IJsonSerializable
	{
		// Token: 0x060060BA RID: 24762 RVA: 0x0014A176 File Offset: 0x00148376
		public CdpaExtraParameters()
		{
			this.PropertyValues = CdpaExtraParameters.emptyDictionary;
		}

		// Token: 0x17001C90 RID: 7312
		// (get) Token: 0x060060BB RID: 24763 RVA: 0x0014A189 File Offset: 0x00148389
		// (set) Token: 0x060060BC RID: 24764 RVA: 0x0014A191 File Offset: 0x00148391
		public IDictionary<string, string> PropertyValues { get; set; }

		// Token: 0x060060BD RID: 24765 RVA: 0x0014A19C File Offset: 0x0014839C
		public string ToJson()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			bool flag = true;
			foreach (KeyValuePair<string, string> keyValuePair in this.PropertyValues)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append(JsonFormatter.FormatString(keyValuePair.Key));
				stringBuilder.Append(": ");
				stringBuilder.Append(JsonFormatter.FormatString(keyValuePair.Value));
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x040034CF RID: 13519
		private static readonly IDictionary<string, string> emptyDictionary = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
	}
}
