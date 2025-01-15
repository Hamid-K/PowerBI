using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.QueryExtensionSchema
{
	// Token: 0x020000C3 RID: 195
	public sealed class QueryExtensionSchemaBuilder : QueryExtensionSchemaBuilder<object>
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x0000BCDC File Offset: 0x00009EDC
		public QueryExtensionSchemaBuilder(string name, string extends = "", int version = 1)
			: base(new QueryExtensionSchema(), null)
		{
			base.ActiveObject.Name = name;
			base.ActiveObject.Extends = extends;
			base.ActiveObject.Version = new int?(version);
		}
	}
}
