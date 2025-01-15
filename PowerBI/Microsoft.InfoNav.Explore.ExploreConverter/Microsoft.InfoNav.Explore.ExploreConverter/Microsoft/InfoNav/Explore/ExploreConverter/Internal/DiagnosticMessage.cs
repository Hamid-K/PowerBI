using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000063 RID: 99
	internal sealed class DiagnosticMessage
	{
		// Token: 0x06000208 RID: 520 RVA: 0x0000B7B7 File Offset: 0x000099B7
		internal DiagnosticMessage(DiagnosticMessageSeverity severity, string errorCode, params string[] parameters)
		{
			this._severity = severity;
			this._errorCode = errorCode;
			this._parameters = new List<string>(parameters);
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000B7D9 File Offset: 0x000099D9
		public DiagnosticMessageSeverity Severity
		{
			get
			{
				return this._severity;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000B7E1 File Offset: 0x000099E1
		public string ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000B7E9 File Offset: 0x000099E9
		public List<string> Params
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x0400016B RID: 363
		private readonly DiagnosticMessageSeverity _severity;

		// Token: 0x0400016C RID: 364
		private readonly string _errorCode;

		// Token: 0x0400016D RID: 365
		private readonly List<string> _parameters;
	}
}
