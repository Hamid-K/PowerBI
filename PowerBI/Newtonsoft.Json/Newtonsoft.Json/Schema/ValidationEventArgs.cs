using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000B4 RID: 180
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class ValidationEventArgs : EventArgs
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x0002805F File Offset: 0x0002625F
		internal ValidationEventArgs(JsonSchemaException ex)
		{
			ValidationUtils.ArgumentNotNull(ex, "ex");
			this._ex = ex;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00028079 File Offset: 0x00026279
		public JsonSchemaException Exception
		{
			get
			{
				return this._ex;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00028081 File Offset: 0x00026281
		public string Path
		{
			get
			{
				return this._ex.Path;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0002808E File Offset: 0x0002628E
		public string Message
		{
			get
			{
				return this._ex.Message;
			}
		}

		// Token: 0x04000371 RID: 881
		private readonly JsonSchemaException _ex;
	}
}
