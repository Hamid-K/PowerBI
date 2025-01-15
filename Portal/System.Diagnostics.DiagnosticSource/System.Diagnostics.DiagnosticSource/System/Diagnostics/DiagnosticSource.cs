using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	// Token: 0x02000014 RID: 20
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class DiagnosticSource
	{
		// Token: 0x06000053 RID: 83
		[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
		public abstract void Write(string name, [Nullable(2)] object value);

		// Token: 0x06000054 RID: 84
		public abstract bool IsEnabled(string name);

		// Token: 0x06000055 RID: 85 RVA: 0x00002B38 File Offset: 0x00000D38
		[NullableContext(2)]
		public virtual bool IsEnabled([Nullable(1)] string name, object arg1, object arg2 = null)
		{
			return this.IsEnabled(name);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B41 File Offset: 0x00000D41
		[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
		public Activity StartActivity(Activity activity, [Nullable(2)] object args)
		{
			activity.Start();
			this.Write(activity.OperationName + ".Start", args);
			return activity;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B62 File Offset: 0x00000D62
		[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
		public void StopActivity(Activity activity, [Nullable(2)] object args)
		{
			if (activity.Duration == TimeSpan.Zero)
			{
				activity.SetEndTime(Activity.GetUtcNow());
			}
			this.Write(activity.OperationName + ".Stop", args);
			activity.Stop();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B9F File Offset: 0x00000D9F
		public virtual void OnActivityImport(Activity activity, [Nullable(2)] object payload)
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002BA1 File Offset: 0x00000DA1
		public virtual void OnActivityExport(Activity activity, [Nullable(2)] object payload)
		{
		}

		// Token: 0x0400000E RID: 14
		internal const string WriteRequiresUnreferencedCode = "The type of object being written to DiagnosticSource cannot be discovered statically.";
	}
}
