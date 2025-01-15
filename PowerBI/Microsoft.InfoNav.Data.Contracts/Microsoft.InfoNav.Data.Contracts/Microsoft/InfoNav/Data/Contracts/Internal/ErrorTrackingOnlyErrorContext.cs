using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000270 RID: 624
	internal sealed class ErrorTrackingOnlyErrorContext : IErrorContext
	{
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x000222A3 File Offset: 0x000204A3
		public bool HasError
		{
			get
			{
				return this._hasError;
			}
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x000222AB File Offset: 0x000204AB
		public void RegisterError(string messageTemplate, params object[] args)
		{
			this._hasError = true;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x000222B4 File Offset: 0x000204B4
		public void RegisterWarning(string messageTemplate, params object[] args)
		{
		}

		// Token: 0x040007D6 RID: 2006
		private bool _hasError;
	}
}
