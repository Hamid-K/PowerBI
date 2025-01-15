using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics
{
	// Token: 0x020015FD RID: 5629
	public class TimeWatch : IDisposable
	{
		// Token: 0x0600BB2A RID: 47914 RVA: 0x00002130 File Offset: 0x00000330
		private TimeWatch()
		{
		}

		// Token: 0x17002047 RID: 8263
		// (get) Token: 0x0600BB2B RID: 47915 RVA: 0x002849B0 File Offset: 0x00282BB0
		// (set) Token: 0x0600BB2C RID: 47916 RVA: 0x002849B8 File Offset: 0x00282BB8
		public bool AllowGroup { get; set; }

		// Token: 0x17002048 RID: 8264
		// (get) Token: 0x0600BB2D RID: 47917 RVA: 0x002849C1 File Offset: 0x00282BC1
		// (set) Token: 0x0600BB2E RID: 47918 RVA: 0x002849C9 File Offset: 0x00282BC9
		public double? Elapsed { get; set; }

		// Token: 0x17002049 RID: 8265
		// (get) Token: 0x0600BB2F RID: 47919 RVA: 0x002849D2 File Offset: 0x00282BD2
		// (set) Token: 0x0600BB30 RID: 47920 RVA: 0x002849DA File Offset: 0x00282BDA
		public bool Focus { get; set; }

		// Token: 0x1700204A RID: 8266
		// (get) Token: 0x0600BB31 RID: 47921 RVA: 0x002849E3 File Offset: 0x00282BE3
		// (set) Token: 0x0600BB32 RID: 47922 RVA: 0x002849EB File Offset: 0x00282BEB
		public string Name { get; set; }

		// Token: 0x0600BB33 RID: 47923 RVA: 0x002849F4 File Offset: 0x00282BF4
		public void Dispose()
		{
			this.Stop();
		}

		// Token: 0x0600BB34 RID: 47924 RVA: 0x002849FC File Offset: 0x00282BFC
		public static TimeWatch Start(string name = null, [CallerMemberName] string callerName = null)
		{
			if (name == null)
			{
				name = callerName;
			}
			return new TimeWatch
			{
				Name = name
			}.StartInternal();
		}

		// Token: 0x0600BB35 RID: 47925 RVA: 0x00284A18 File Offset: 0x00282C18
		public void Stop()
		{
			Stopwatch watch = this._watch;
			if (watch != null && watch.IsRunning)
			{
				Stopwatch watch2 = this._watch;
				this.Elapsed = ((watch2 != null) ? new double?(watch2.ElapsedMillisecondsAsDouble()) : null);
			}
			if (this._output)
			{
				return;
			}
			string.Format("{0}: {1,15}: {2:N3}ms", "TimeWatch", this.Name, this.Elapsed).Print(null, null, 100);
			this._output = true;
		}

		// Token: 0x0600BB36 RID: 47926 RVA: 0x00284A9E File Offset: 0x00282C9E
		private TimeWatch StartInternal()
		{
			this._watch = Stopwatch.StartNew();
			return this;
		}

		// Token: 0x040046C4 RID: 18116
		private bool _output;

		// Token: 0x040046C5 RID: 18117
		private Stopwatch _watch;
	}
}
