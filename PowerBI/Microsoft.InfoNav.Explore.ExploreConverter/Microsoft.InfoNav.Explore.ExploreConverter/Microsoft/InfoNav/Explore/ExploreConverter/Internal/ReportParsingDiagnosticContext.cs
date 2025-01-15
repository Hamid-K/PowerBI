using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000AC RID: 172
	internal sealed class ReportParsingDiagnosticContext
	{
		// Token: 0x0600039D RID: 925 RVA: 0x000136C2 File Offset: 0x000118C2
		internal ReportParsingDiagnosticContext()
		{
			this._messages = new List<DiagnosticMessage>();
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600039E RID: 926 RVA: 0x000136D5 File Offset: 0x000118D5
		public List<DiagnosticMessage> Messages
		{
			get
			{
				return this._messages;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600039F RID: 927 RVA: 0x000136DD File Offset: 0x000118DD
		public bool HasInfo
		{
			get
			{
				return this.Any(DiagnosticMessageSeverity.Info);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000136E6 File Offset: 0x000118E6
		public bool HasWarning
		{
			get
			{
				return this.Any(DiagnosticMessageSeverity.Warning);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x000136EF File Offset: 0x000118EF
		public bool HasError
		{
			get
			{
				return this.Any(DiagnosticMessageSeverity.Error);
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000136F8 File Offset: 0x000118F8
		public bool Any(DiagnosticMessageSeverity severity)
		{
			return this._messages.Any((DiagnosticMessage message) => message.Severity == severity);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001372C File Offset: 0x0001192C
		public bool Any(string errorCode)
		{
			return this._messages.Any((DiagnosticMessage message) => message.ErrorCode == errorCode);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001375D File Offset: 0x0001195D
		public void Add(DiagnosticMessage message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			this._messages.Add(message);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00013779 File Offset: 0x00011979
		public void AddInfo(string errorCode, params string[] parameters)
		{
			this.Add(new DiagnosticMessage(DiagnosticMessageSeverity.Info, errorCode, parameters));
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00013789 File Offset: 0x00011989
		public void AddWarning(string errorCode, params string[] parameters)
		{
			this.Add(new DiagnosticMessage(DiagnosticMessageSeverity.Warning, errorCode, parameters));
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001379C File Offset: 0x0001199C
		public DiagnosticMessage AddError(string errorCode, params string[] parameters)
		{
			DiagnosticMessage diagnosticMessage = new DiagnosticMessage(DiagnosticMessageSeverity.Error, errorCode, parameters);
			this.Add(diagnosticMessage);
			return diagnosticMessage;
		}

		// Token: 0x04000232 RID: 562
		internal const string Source = "ReportParsing";

		// Token: 0x04000233 RID: 563
		private readonly List<DiagnosticMessage> _messages;
	}
}
