using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000023 RID: 35
	[ImmutableObject(true)]
	public sealed class ConceptualSchemaLoadError
	{
		// Token: 0x0600012C RID: 300 RVA: 0x000068D6 File Offset: 0x00004AD6
		internal ConceptualSchemaLoadError(string message, string location, string errorCode)
		{
			this.Message = message;
			this.Location = location;
			this.ErrorCode = errorCode;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000068F3 File Offset: 0x00004AF3
		internal string Message { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000068FB File Offset: 0x00004AFB
		internal string Location { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00006903 File Offset: 0x00004B03
		internal string ErrorCode { get; }

		// Token: 0x06000130 RID: 304 RVA: 0x0000690B File Offset: 0x00004B0B
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0} [{1}]: {2}", this.ErrorCode, this.Location, this.Message);
		}
	}
}
