using System;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	public class ModelError
	{
		// Token: 0x060002AC RID: 684 RVA: 0x00007E38 File Offset: 0x00006038
		public ModelError(Exception exception)
			: this(exception, null)
		{
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00007E42 File Offset: 0x00006042
		public ModelError(Exception exception, string errorMessage)
			: this(errorMessage)
		{
			if (exception == null)
			{
				throw Error.ArgumentNull("exception");
			}
			this.Exception = exception;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00007E60 File Offset: 0x00006060
		public ModelError(string errorMessage)
		{
			this.ErrorMessage = errorMessage ?? string.Empty;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00007E78 File Offset: 0x00006078
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00007E80 File Offset: 0x00006080
		public Exception Exception { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00007E89 File Offset: 0x00006089
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00007E91 File Offset: 0x00006091
		public string ErrorMessage { get; private set; }
	}
}
