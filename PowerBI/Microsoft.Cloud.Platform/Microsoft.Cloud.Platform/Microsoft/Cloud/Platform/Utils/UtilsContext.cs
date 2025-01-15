using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002DD RID: 733
	public class UtilsContext : Context
	{
		// Token: 0x0600139F RID: 5023 RVA: 0x00044852 File Offset: 0x00042A52
		protected UtilsContext()
		{
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0004485A File Offset: 0x00042A5A
		public static UtilsContext Current
		{
			get
			{
				return UtilsContext.sm_instance;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x00044864 File Offset: 0x00042A64
		public Activity Activity
		{
			get
			{
				Activity contextMember = base.GetContextMember<Activity>(0);
				if (contextMember != null)
				{
					return contextMember;
				}
				return Activity.Empty;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x00044883 File Offset: 0x00042A83
		public TraceVerbosity TracingSuppressedLevel
		{
			get
			{
				return base.GetContextMember<TraceVerbosity>(1);
			}
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0004488C File Offset: 0x00042A8C
		public IDisposable PushTracingSuppressedLevel(TraceVerbosity traceLevel)
		{
			return base.PushContextMember<TraceVerbosity>(1, traceLevel);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00044896 File Offset: 0x00042A96
		public AsyncContextMemberProxy<TraceVerbosity> CreateTracingSuppressedLevelProxy(TraceVerbosity level)
		{
			return new AsyncContextMemberProxy<TraceVerbosity>(level, 1);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0004489F File Offset: 0x00042A9F
		public IDisposable PushTracingForced(bool value)
		{
			return base.PushContextMember<bool>(2, value);
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x000448A9 File Offset: 0x00042AA9
		public bool TracingForced
		{
			get
			{
				return base.GetContextMember<bool>(2);
			}
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000448B2 File Offset: 0x00042AB2
		public AsyncContextMemberProxy<bool> CreateEnforcedTracingProxy()
		{
			return new AsyncContextMemberProxy<bool>(true, 2);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x000448BB File Offset: 0x00042ABB
		public IEnumerable<Activity> GetActivityStack()
		{
			return base.GetContextMemberStack<Activity>(0);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x000448C4 File Offset: 0x00042AC4
		public bool IsCurrentActivityEqualToCaptured(IContextStack capturedStack)
		{
			Activity contextMember = capturedStack.GetContextMember<Activity>(0);
			return (this.Activity.Equals(Activity.Empty) && contextMember == null) || this.Activity.Equals(capturedStack.GetContextMember<Activity>(0));
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x00044902 File Offset: 0x00042B02
		public new IContextStack CaptureStack()
		{
			return base.CaptureStack();
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0004490C File Offset: 0x00042B0C
		public void RunWithClearContext([NotNull] Action action)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action>(action, "action");
			IContextStack contextStack = UtilsContext.Current.CaptureStack();
			try
			{
				UtilsContext.Current.ClearStack();
				action();
			}
			finally
			{
				contextStack.Restore();
			}
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0004495C File Offset: 0x00042B5C
		internal IDisposable PushActivity([NotNull] Activity activity)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Activity>(activity, "activity");
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Push Activity {0} to {1}", new object[]
			{
				activity.ActivityId,
				this.GetActivityStackRepresentation()
			});
			return base.PushContextMember<Activity>(0, activity);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x000449AC File Offset: 0x00042BAC
		internal void PopActivity(Activity activity, UtilsContext.ContextOperationOptions options)
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Pop Activity: {0}", new object[] { this.GetActivityStackRepresentation() });
			base.GetContextMember<Activity>(0);
			options.HasFlag(UtilsContext.ContextOperationOptions.DisableValidations);
			base.PopContextMember<Activity>(0);
			options.HasFlag(UtilsContext.ContextOperationOptions.DisableValidations);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00044A0C File Offset: 0x00042C0C
		internal void PopActivity(Activity activity)
		{
			this.PopActivity(activity, UtilsContext.ContextOperationOptions.None);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00044A18 File Offset: 0x00042C18
		public string GetActivityStackRepresentation()
		{
			IEnumerable<Activity> activityStack = this.GetActivityStack();
			if (activityStack.Count<Activity>() == 0)
			{
				return "Activity stack: [empty]";
			}
			string text = activityStack.Select((Activity a) => a.ActivityType).StringJoin(" > ");
			string text2 = activityStack.Select((Activity a) => a.ActivityId).StringJoin(" > ");
			return "Activity stack: [{0}] [{1}]".FormatWithInvariantCulture(new object[] { text, text2 });
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00044AB4 File Offset: 0x00042CB4
		internal static IEnumerable<string> GetActivityStackAsStrings(IEnumerable<Activity> stack)
		{
			if (stack == null || !stack.Any<Activity>())
			{
				return UtilsContext.s_empty;
			}
			List<string> list = new List<string>();
			list.AddRange(stack.Select((Activity activity) => activity.ActivityType + "/" + activity.ActivityId));
			return list;
		}

		// Token: 0x04000761 RID: 1889
		private static UtilsContext sm_instance = new UtilsContext();

		// Token: 0x04000762 RID: 1890
		internal const int ActivityContextKey = 0;

		// Token: 0x04000763 RID: 1891
		internal const int TraceSuppressKey = 1;

		// Token: 0x04000764 RID: 1892
		internal const int TracingEnforcedContextMemberKey = 2;

		// Token: 0x04000765 RID: 1893
		internal const int ApplicationRootKey = 3;

		// Token: 0x04000766 RID: 1894
		internal const int SelfTestBitKey = 4;

		// Token: 0x04000767 RID: 1895
		internal static readonly string[] KeyNames = new string[] { "Microsoft.Cloud.Platform.Utils.Activity", "Microsoft.Cloud.Platform.Utils.TraceSuppressLevel", "Microsoft.Cloud.Platform.Utils.TracingEnforced", "Microsoft.Cloud.Platform.ModularizationFramework.ApplicationRoot", "Microsoft.Cloud.Platform.ModularizationFramework.TestBit" };

		// Token: 0x04000768 RID: 1896
		private static IEnumerable<string> s_empty = new List<string>();

		// Token: 0x02000787 RID: 1927
		[Flags]
		internal enum ContextOperationOptions
		{
			// Token: 0x04001640 RID: 5696
			None = 0,
			// Token: 0x04001641 RID: 5697
			DisableValidations = 1
		}
	}
}
