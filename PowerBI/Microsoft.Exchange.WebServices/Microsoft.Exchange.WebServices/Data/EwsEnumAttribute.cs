using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000003 RID: 3
	[AttributeUsage(384, AllowMultiple = false, Inherited = false)]
	internal sealed class EwsEnumAttribute : Attribute
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000010D8
		internal EwsEnumAttribute(string schemaName)
		{
			this.schemaName = schemaName;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E7 File Offset: 0x000010E7
		internal string SchemaName
		{
			get
			{
				return this.schemaName;
			}
		}

		// Token: 0x04000001 RID: 1
		private string schemaName;
	}
}
