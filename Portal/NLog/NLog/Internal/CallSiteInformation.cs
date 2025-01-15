using System;
using System.Diagnostics;
using System.Reflection;

namespace NLog.Internal
{
	// Token: 0x0200010E RID: 270
	internal class CallSiteInformation
	{
		// Token: 0x06000E70 RID: 3696 RVA: 0x00023F68 File Offset: 0x00022168
		public void SetStackTrace(StackTrace stackTrace, int userStackFrame, int? userStackFrameLegacy)
		{
			this.StackTrace = stackTrace;
			this.UserStackFrameNumber = userStackFrame;
			int? num = userStackFrameLegacy;
			this.UserStackFrameNumberLegacy = ((!((num.GetValueOrDefault() == userStackFrame) & (num != null))) ? userStackFrameLegacy : null);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00023FAD File Offset: 0x000221AD
		public void SetCallerInfo(string callerClassName, string callerMemberName, string callerFilePath, int callerLineNumber)
		{
			this.CallerClassName = callerClassName;
			this.CallerMemberName = callerMemberName;
			this.CallerFilePath = callerFilePath;
			this.CallerLineNumber = new int?(callerLineNumber);
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x00023FD4 File Offset: 0x000221D4
		public StackFrame UserStackFrame
		{
			get
			{
				StackTrace stackTrace = this.StackTrace;
				if (stackTrace == null)
				{
					return null;
				}
				return stackTrace.GetFrame(this.UserStackFrameNumberLegacy ?? this.UserStackFrameNumber);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00024011 File Offset: 0x00022211
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x00024019 File Offset: 0x00022219
		public int UserStackFrameNumber { get; private set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00024022 File Offset: 0x00022222
		// (set) Token: 0x06000E76 RID: 3702 RVA: 0x0002402A File Offset: 0x0002222A
		public int? UserStackFrameNumberLegacy { get; private set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x00024033 File Offset: 0x00022233
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x0002403B File Offset: 0x0002223B
		public StackTrace StackTrace { get; private set; }

		// Token: 0x06000E79 RID: 3705 RVA: 0x00024044 File Offset: 0x00022244
		public MethodBase GetCallerStackFrameMethod(int skipFrames)
		{
			StackTrace stackTrace = this.StackTrace;
			StackFrame stackFrame = ((stackTrace != null) ? stackTrace.GetFrame(this.UserStackFrameNumber + skipFrames) : null);
			if (stackFrame == null)
			{
				return null;
			}
			return stackFrame.GetMethod();
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0002406C File Offset: 0x0002226C
		public string GetCallerClassName(MethodBase method, bool includeNameSpace, bool cleanAsyncMoveNext, bool cleanAnonymousDelegates)
		{
			if (!string.IsNullOrEmpty(this.CallerClassName))
			{
				if (includeNameSpace)
				{
					return this.CallerClassName;
				}
				int num = this.CallerClassName.LastIndexOf('.');
				if (num < 0 || num >= this.CallerClassName.Length - 1)
				{
					return this.CallerClassName;
				}
				return this.CallerClassName.Substring(num + 1);
			}
			else
			{
				method = method ?? this.GetCallerStackFrameMethod(0);
				if (method == null)
				{
					return string.Empty;
				}
				cleanAsyncMoveNext = cleanAsyncMoveNext || this.UserStackFrameNumberLegacy != null;
				cleanAnonymousDelegates = cleanAnonymousDelegates || this.UserStackFrameNumberLegacy != null;
				return StackTraceUsageUtils.GetStackFrameMethodClassName(method, includeNameSpace, cleanAsyncMoveNext, cleanAnonymousDelegates) ?? string.Empty;
			}
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00024128 File Offset: 0x00022328
		public string GetCallerMemberName(MethodBase method, bool includeMethodInfo, bool cleanAsyncMoveNext, bool cleanAnonymousDelegates)
		{
			if (!string.IsNullOrEmpty(this.CallerMemberName))
			{
				return this.CallerMemberName;
			}
			method = method ?? this.GetCallerStackFrameMethod(0);
			if (method == null)
			{
				return string.Empty;
			}
			cleanAsyncMoveNext = cleanAsyncMoveNext || this.UserStackFrameNumberLegacy != null;
			cleanAnonymousDelegates = cleanAnonymousDelegates || this.UserStackFrameNumberLegacy != null;
			return StackTraceUsageUtils.GetStackFrameMethodName(method, includeMethodInfo, cleanAsyncMoveNext, cleanAnonymousDelegates) ?? string.Empty;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x000241A8 File Offset: 0x000223A8
		public string GetCallerFilePath(int skipFrames)
		{
			if (!string.IsNullOrEmpty(this.CallerFilePath))
			{
				return this.CallerFilePath;
			}
			StackTrace stackTrace = this.StackTrace;
			StackFrame stackFrame = ((stackTrace != null) ? stackTrace.GetFrame(this.UserStackFrameNumber + skipFrames) : null);
			return ((stackFrame != null) ? stackFrame.GetFileName() : null) ?? string.Empty;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x000241F8 File Offset: 0x000223F8
		public int GetCallerLineNumber(int skipFrames)
		{
			if (this.CallerLineNumber != null)
			{
				return this.CallerLineNumber.Value;
			}
			StackTrace stackTrace = this.StackTrace;
			StackFrame stackFrame = ((stackTrace != null) ? stackTrace.GetFrame(this.UserStackFrameNumber + skipFrames) : null);
			if (stackFrame == null)
			{
				return 0;
			}
			return stackFrame.GetFileLineNumber();
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x00024249 File Offset: 0x00022449
		// (set) Token: 0x06000E7F RID: 3711 RVA: 0x00024251 File Offset: 0x00022451
		public string CallerClassName { get; private set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0002425A File Offset: 0x0002245A
		// (set) Token: 0x06000E81 RID: 3713 RVA: 0x00024262 File Offset: 0x00022462
		public string CallerMemberName { get; private set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x0002426B File Offset: 0x0002246B
		// (set) Token: 0x06000E83 RID: 3715 RVA: 0x00024273 File Offset: 0x00022473
		public string CallerFilePath { get; private set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x0002427C File Offset: 0x0002247C
		// (set) Token: 0x06000E85 RID: 3717 RVA: 0x00024284 File Offset: 0x00022484
		public int? CallerLineNumber { get; private set; }
	}
}
