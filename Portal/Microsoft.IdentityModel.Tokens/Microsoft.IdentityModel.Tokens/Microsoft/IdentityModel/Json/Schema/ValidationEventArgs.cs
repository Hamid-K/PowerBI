using System;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Schema
{
	// Token: 0x020000B5 RID: 181
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class ValidationEventArgs : EventArgs
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x0002803F File Offset: 0x0002623F
		internal ValidationEventArgs(JsonSchemaException ex)
		{
			ValidationUtils.ArgumentNotNull(ex, "ex");
			this._ex = ex;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00028059 File Offset: 0x00026259
		public JsonSchemaException Exception
		{
			get
			{
				return this._ex;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00028061 File Offset: 0x00026261
		public string Path
		{
			get
			{
				return this._ex.Path;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x0002806E File Offset: 0x0002626E
		public string Message
		{
			get
			{
				return this._ex.Message;
			}
		}

		// Token: 0x04000372 RID: 882
		private readonly JsonSchemaException _ex;
	}
}
