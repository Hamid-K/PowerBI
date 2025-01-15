using System;
using System.Diagnostics;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001611 RID: 5649
	public class TimedEvent : IDisposable
	{
		// Token: 0x17002066 RID: 8294
		// (get) Token: 0x0600BC3D RID: 48189 RVA: 0x0028805B File Offset: 0x0028625B
		public bool IsRunning
		{
			get
			{
				return this._watch.IsRunning;
			}
		}

		// Token: 0x17002067 RID: 8295
		// (get) Token: 0x0600BC3E RID: 48190 RVA: 0x00288068 File Offset: 0x00286268
		// (set) Token: 0x0600BC3F RID: 48191 RVA: 0x00288070 File Offset: 0x00286270
		public string Category { get; set; }

		// Token: 0x17002068 RID: 8296
		// (get) Token: 0x0600BC40 RID: 48192 RVA: 0x00288079 File Offset: 0x00286279
		// (set) Token: 0x0600BC41 RID: 48193 RVA: 0x00288081 File Offset: 0x00286281
		public double? Elapsed { get; set; }

		// Token: 0x17002069 RID: 8297
		// (get) Token: 0x0600BC42 RID: 48194 RVA: 0x0028808A File Offset: 0x0028628A
		// (set) Token: 0x0600BC43 RID: 48195 RVA: 0x00288092 File Offset: 0x00286292
		public string Name { get; set; }

		// Token: 0x1700206A RID: 8298
		// (get) Token: 0x0600BC44 RID: 48196 RVA: 0x0028809B File Offset: 0x0028629B
		// (set) Token: 0x0600BC45 RID: 48197 RVA: 0x002880A3 File Offset: 0x002862A3
		public bool AllowGroup { get; set; }

		// Token: 0x1700206B RID: 8299
		// (get) Token: 0x0600BC46 RID: 48198 RVA: 0x002880AC File Offset: 0x002862AC
		// (set) Token: 0x0600BC47 RID: 48199 RVA: 0x002880B4 File Offset: 0x002862B4
		public bool Focus { get; set; }

		// Token: 0x0600BC48 RID: 48200 RVA: 0x002880BD File Offset: 0x002862BD
		public void Start()
		{
			if (!this._watch.IsRunning)
			{
				this._watch.Start();
			}
		}

		// Token: 0x0600BC49 RID: 48201 RVA: 0x002880D7 File Offset: 0x002862D7
		public void Stop()
		{
			if (!this._watch.IsRunning)
			{
				return;
			}
			this._watch.Stop();
			this.Elapsed = new double?(this._watch.ElapsedMillisecondsAsDouble());
		}

		// Token: 0x0600BC4A RID: 48202 RVA: 0x00288108 File Offset: 0x00286308
		public void Dispose()
		{
			this.Stop();
		}

		// Token: 0x04004761 RID: 18273
		private readonly Stopwatch _watch = new Stopwatch();
	}
}
