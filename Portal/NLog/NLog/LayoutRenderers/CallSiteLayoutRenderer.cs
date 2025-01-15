using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000B8 RID: 184
	[LayoutRenderer("callsite")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class CallSiteLayoutRenderer : LayoutRenderer, IUsesStackTrace
	{
		// Token: 0x06000BB5 RID: 2997 RVA: 0x0001E7FC File Offset: 0x0001C9FC
		public CallSiteLayoutRenderer()
		{
			this.ClassName = true;
			this.MethodName = true;
			this.CleanNamesOfAnonymousDelegates = false;
			this.IncludeNamespace = true;
			this.FileName = false;
			this.IncludeSourcePath = true;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0001E82E File Offset: 0x0001CA2E
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x0001E836 File Offset: 0x0001CA36
		[DefaultValue(true)]
		public bool ClassName { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0001E83F File Offset: 0x0001CA3F
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x0001E847 File Offset: 0x0001CA47
		[DefaultValue(true)]
		public bool IncludeNamespace { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0001E850 File Offset: 0x0001CA50
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x0001E858 File Offset: 0x0001CA58
		[DefaultValue(true)]
		public bool MethodName { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0001E861 File Offset: 0x0001CA61
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x0001E869 File Offset: 0x0001CA69
		[DefaultValue(false)]
		public bool CleanNamesOfAnonymousDelegates { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0001E872 File Offset: 0x0001CA72
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x0001E87A File Offset: 0x0001CA7A
		[DefaultValue(false)]
		public bool CleanNamesOfAsyncContinuations { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0001E883 File Offset: 0x0001CA83
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x0001E88B File Offset: 0x0001CA8B
		[DefaultValue(0)]
		public int SkipFrames { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0001E894 File Offset: 0x0001CA94
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x0001E89C File Offset: 0x0001CA9C
		[DefaultValue(false)]
		public bool FileName { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0001E8A5 File Offset: 0x0001CAA5
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x0001E8AD File Offset: 0x0001CAAD
		[DefaultValue(true)]
		public bool IncludeSourcePath { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0001E8B6 File Offset: 0x0001CAB6
		StackTraceUsage IUsesStackTrace.StackTraceUsage
		{
			get
			{
				if (this.FileName)
				{
					return StackTraceUsage.WithSource;
				}
				return StackTraceUsage.WithoutSource;
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0001E8C4 File Offset: 0x0001CAC4
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (logEvent.CallSiteInformation != null)
			{
				if (this.ClassName || this.MethodName)
				{
					MethodBase callerStackFrameMethod = logEvent.CallSiteInformation.GetCallerStackFrameMethod(this.SkipFrames);
					if (this.ClassName)
					{
						string text = logEvent.CallSiteInformation.GetCallerClassName(callerStackFrameMethod, this.IncludeNamespace, this.CleanNamesOfAsyncContinuations, this.CleanNamesOfAnonymousDelegates);
						if (string.IsNullOrEmpty(text))
						{
							text = "<no type>";
						}
						builder.Append(text);
					}
					if (this.MethodName)
					{
						string text2 = logEvent.CallSiteInformation.GetCallerMemberName(callerStackFrameMethod, false, this.CleanNamesOfAsyncContinuations, this.CleanNamesOfAnonymousDelegates);
						if (string.IsNullOrEmpty(text2))
						{
							text2 = "<no method>";
						}
						if (this.ClassName)
						{
							builder.Append(".");
						}
						builder.Append(text2);
					}
				}
				if (this.FileName)
				{
					string callerFilePath = logEvent.CallSiteInformation.GetCallerFilePath(this.SkipFrames);
					if (!string.IsNullOrEmpty(callerFilePath))
					{
						int callerLineNumber = logEvent.CallSiteInformation.GetCallerLineNumber(this.SkipFrames);
						this.AppendFileName(builder, callerFilePath, callerLineNumber);
					}
				}
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0001E9CC File Offset: 0x0001CBCC
		private void AppendFileName(StringBuilder builder, string fileName, int lineNumber)
		{
			builder.Append("(");
			if (this.IncludeSourcePath)
			{
				builder.Append(fileName);
			}
			else
			{
				builder.Append(Path.GetFileName(fileName));
			}
			builder.Append(":");
			builder.AppendInvariant(lineNumber);
			builder.Append(")");
		}
	}
}
