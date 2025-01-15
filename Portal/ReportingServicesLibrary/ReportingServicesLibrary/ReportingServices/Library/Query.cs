using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200002A RID: 42
	internal sealed class Query
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00005AF8 File Offset: 0x00003CF8
		public Query()
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005B0C File Offset: 0x00003D0C
		public Query(QueryDefinition soapQuery)
		{
			if (soapQuery == null)
			{
				throw new MissingElementException("Query");
			}
			this.CommandText = soapQuery.CommandText;
			this.Timeout = soapQuery.Timeout;
			if (soapQuery.CommandType != null && soapQuery.CommandType != "Text")
			{
				throw new InvalidElementException("CommandType");
			}
			if (this.CommandText == null)
			{
				throw new InvalidXmlException();
			}
			if (this.CommandText == string.Empty)
			{
				throw new InvalidElementException("CommandText");
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005BA0 File Offset: 0x00003DA0
		public QueryDefinition ToSoapQuery()
		{
			QueryDefinition queryDefinition = new QueryDefinition();
			queryDefinition.CommandText = this.CommandText;
			queryDefinition.CommandType = this.CommandType;
			queryDefinition.TimeoutSpecified = false;
			if (this.Timeout != -1)
			{
				queryDefinition.TimeoutSpecified = true;
				queryDefinition.Timeout = this.Timeout;
			}
			return queryDefinition;
		}

		// Token: 0x040000FF RID: 255
		internal const string _Query = "Query";

		// Token: 0x04000100 RID: 256
		private const string _CommandType = "CommandType";

		// Token: 0x04000101 RID: 257
		private const string _CommandText = "CommandText";

		// Token: 0x04000102 RID: 258
		private const string _Timeout = "Timeout";

		// Token: 0x04000103 RID: 259
		private const string _Text = "Text";

		// Token: 0x04000104 RID: 260
		public string CommandType = "Text";

		// Token: 0x04000105 RID: 261
		public string CommandText;

		// Token: 0x04000106 RID: 262
		public int Timeout;
	}
}
