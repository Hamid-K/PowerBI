using System;
using System.Diagnostics;
using System.Reflection;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x02000483 RID: 1155
	[RDEvent(64256, TraceEventType.Verbose)]
	public abstract class ExceptionEventBase : RDEventBase
	{
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x00078F00 File Offset: 0x00077100
		// (set) Token: 0x06002809 RID: 10249 RVA: 0x00078F08 File Offset: 0x00077108
		public Exception Exception { get; private set; }

		// Token: 0x0600280A RID: 10250 RVA: 0x00078F11 File Offset: 0x00077111
		protected ExceptionEventBase()
		{
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x00078F1C File Offset: 0x0007711C
		protected ExceptionEventBase(Exception exception, int exceptionID, string context, bool includeFileInfo)
		{
			this.Exception = exception;
			this.Type = exception.GetType().Name;
			this.Message = exception.Message;
			this.ExceptionID = exceptionID;
			this.Context = ((context == null) ? string.Empty : context);
			this.TargetSite = ((exception.TargetSite == null) ? string.Empty : exception.TargetSite.ToString());
			this.Source = exception.Source;
			this.StackTrace = Environment.NewLine + exception.StackTrace;
			if (includeFileInfo)
			{
				string @namespace = base.GetType().Namespace;
				int num = 0;
				StackFrame stackFrame;
				MethodBase method;
				do
				{
					num++;
					stackFrame = new StackFrame(num, true);
					method = stackFrame.GetMethod();
				}
				while (method != null && method.DeclaringType.Namespace.Equals(@namespace));
				this.FileName = stackFrame.GetFileName();
				this.FileName = ((this.FileName == null) ? string.Empty : this.FileName);
				this.FileLineNumber = stackFrame.GetFileLineNumber();
				if (method == null)
				{
					this.DeclaringType = string.Empty;
					this.MethodSignature = string.Empty;
				}
				else
				{
					this.DeclaringType = method.DeclaringType.FullName;
					this.MethodSignature = method.ToString();
				}
			}
			else
			{
				this.FileName = string.Empty;
				this.FileLineNumber = 0;
				this.DeclaringType = string.Empty;
				this.MethodSignature = string.Empty;
			}
			this.StringRepresentation = Environment.NewLine + exception.ToString();
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x0600280C RID: 10252 RVA: 0x000790A8 File Offset: 0x000772A8
		// (set) Token: 0x0600280D RID: 10253 RVA: 0x000790B0 File Offset: 0x000772B0
		[RDEventProperty]
		public string Type { get; set; }

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x000790B9 File Offset: 0x000772B9
		// (set) Token: 0x0600280F RID: 10255 RVA: 0x000790C1 File Offset: 0x000772C1
		[RDEventProperty]
		public string Message { get; set; }

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x000790CA File Offset: 0x000772CA
		// (set) Token: 0x06002811 RID: 10257 RVA: 0x000790D2 File Offset: 0x000772D2
		[RDEventProperty]
		public int ExceptionID { get; set; }

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x000790DB File Offset: 0x000772DB
		// (set) Token: 0x06002813 RID: 10259 RVA: 0x000790E3 File Offset: 0x000772E3
		[RDEventProperty]
		public string Context { get; set; }

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x000790EC File Offset: 0x000772EC
		// (set) Token: 0x06002815 RID: 10261 RVA: 0x000790F4 File Offset: 0x000772F4
		[RDEventProperty(ExcludeFromConsole = true)]
		public string TargetSite { get; set; }

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x000790FD File Offset: 0x000772FD
		// (set) Token: 0x06002817 RID: 10263 RVA: 0x00079105 File Offset: 0x00077305
		[RDEventProperty(ExcludeFromConsole = true)]
		public string Source { get; set; }

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06002818 RID: 10264 RVA: 0x0007910E File Offset: 0x0007730E
		// (set) Token: 0x06002819 RID: 10265 RVA: 0x00079116 File Offset: 0x00077316
		[RDEventProperty]
		public string FileName { get; set; }

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x0600281A RID: 10266 RVA: 0x0007911F File Offset: 0x0007731F
		// (set) Token: 0x0600281B RID: 10267 RVA: 0x00079127 File Offset: 0x00077327
		[RDEventProperty]
		public int FileLineNumber { get; set; }

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x0600281C RID: 10268 RVA: 0x00079130 File Offset: 0x00077330
		// (set) Token: 0x0600281D RID: 10269 RVA: 0x00079138 File Offset: 0x00077338
		[RDEventProperty(ExcludeFromConsole = true)]
		public string DeclaringType { get; set; }

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x0600281E RID: 10270 RVA: 0x00079141 File Offset: 0x00077341
		// (set) Token: 0x0600281F RID: 10271 RVA: 0x00079149 File Offset: 0x00077349
		[RDEventProperty(ExcludeFromConsole = true)]
		public string MethodSignature { get; set; }

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002820 RID: 10272 RVA: 0x00079152 File Offset: 0x00077352
		// (set) Token: 0x06002821 RID: 10273 RVA: 0x0007915A File Offset: 0x0007735A
		[RDEventProperty]
		public virtual string StackTrace { get; set; }

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06002822 RID: 10274 RVA: 0x00079163 File Offset: 0x00077363
		// (set) Token: 0x06002823 RID: 10275 RVA: 0x0007916B File Offset: 0x0007736B
		[RDEventProperty(ExcludeFromConsole = true)]
		public string StringRepresentation { get; set; }
	}
}
