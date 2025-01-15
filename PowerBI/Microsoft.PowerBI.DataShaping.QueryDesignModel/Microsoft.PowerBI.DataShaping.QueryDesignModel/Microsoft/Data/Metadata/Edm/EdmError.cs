using System;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000070 RID: 112
	public abstract class EdmError
	{
		// Token: 0x0600091B RID: 2331 RVA: 0x00014B7E File Offset: 0x00012D7E
		internal EdmError(string message)
		{
			this._message = message;
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00014B8D File Offset: 0x00012D8D
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x04000727 RID: 1831
		private string _message;
	}
}
