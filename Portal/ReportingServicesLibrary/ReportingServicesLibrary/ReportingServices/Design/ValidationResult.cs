using System;

namespace Microsoft.ReportingServices.Design
{
	// Token: 0x0200038C RID: 908
	public abstract class ValidationResult
	{
		// Token: 0x06001E07 RID: 7687 RVA: 0x0007AE67 File Offset: 0x00079067
		protected ValidationResult(object obj, bool isError)
		{
			if (obj == null)
			{
				throw new ArgumentNullException();
			}
			this.Object = obj;
			this.IsError = isError;
		}

		// Token: 0x04000CBB RID: 3259
		public readonly object Object;

		// Token: 0x04000CBC RID: 3260
		public readonly bool IsError;
	}
}
