using System;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Schema
{
	// Token: 0x020000B4 RID: 180
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class ValidationEventArgs : EventArgs
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x000279C7 File Offset: 0x00025BC7
		internal ValidationEventArgs(JsonSchemaException ex)
		{
			ValidationUtils.ArgumentNotNull(ex, "ex");
			this._ex = ex;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x000279E1 File Offset: 0x00025BE1
		public JsonSchemaException Exception
		{
			get
			{
				return this._ex;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x000279E9 File Offset: 0x00025BE9
		public string Path
		{
			get
			{
				return this._ex.Path;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x000279F6 File Offset: 0x00025BF6
		public string Message
		{
			get
			{
				return this._ex.Message;
			}
		}

		// Token: 0x04000357 RID: 855
		private readonly JsonSchemaException _ex;
	}
}
