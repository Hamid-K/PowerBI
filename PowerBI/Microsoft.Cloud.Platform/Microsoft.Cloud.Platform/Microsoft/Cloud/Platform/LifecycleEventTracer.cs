using System;
using System.Diagnostics;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform
{
	// Token: 0x02000010 RID: 16
	public sealed class LifecycleEventTracer : IDisposable
	{
		// Token: 0x0600005A RID: 90 RVA: 0x000030FF File Offset: 0x000012FF
		public LifecycleEventTracer(string elementName)
			: this(elementName, null)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000310C File Offset: 0x0000130C
		public LifecycleEventTracer(string elementName, Func<string> additionalCompletionTextGetter)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(elementName, "elementName");
			this.m_elementName = elementName;
			this.m_additionalCompletionTextGetter = additionalCompletionTextGetter;
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.TraceInformation("LifecycleActivityStarted element={0}", new object[] { this.m_elementName });
			this.m_stopwatch.Start();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000316C File Offset: 0x0000136C
		public void Dispose()
		{
			this.m_stopwatch.Stop();
			if (this.m_additionalCompletionTextGetter == null)
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.TraceInformation("LifecycleActivityCompleted element={0}, duration={1}", new object[]
				{
					this.m_elementName,
					this.m_stopwatch.ElapsedMilliseconds
				});
				return;
			}
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.TraceInformation("LifecycleActivityCompleted element={0}, duration={1}, {2}", new object[]
			{
				this.m_elementName,
				this.m_stopwatch.ElapsedMilliseconds,
				this.m_additionalCompletionTextGetter()
			});
		}

		// Token: 0x04000043 RID: 67
		private readonly Stopwatch m_stopwatch = new Stopwatch();

		// Token: 0x04000044 RID: 68
		private readonly string m_elementName;

		// Token: 0x04000045 RID: 69
		private readonly Func<string> m_additionalCompletionTextGetter;
	}
}
