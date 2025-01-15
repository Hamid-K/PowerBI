using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004A2 RID: 1186
	[Serializable]
	public abstract class EdmError
	{
		// Token: 0x06003A2E RID: 14894 RVA: 0x000C05B2 File Offset: 0x000BE7B2
		internal EdmError(string message)
		{
			Check.NotEmpty(message, "message");
			this._message = message;
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06003A2F RID: 14895 RVA: 0x000C05CD File Offset: 0x000BE7CD
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x04001405 RID: 5125
		private readonly string _message;
	}
}
