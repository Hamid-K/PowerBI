using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D0D RID: 7437
	public class RateLimitedDocumentEvaluatorFactory : LimitedDocumentEvaluatorFactory
	{
		// Token: 0x0600B97F RID: 47487 RVA: 0x00259524 File Offset: 0x00257724
		public RateLimitedDocumentEvaluatorFactory(string identity, TimeSpan expectedDuration)
			: base(identity)
		{
			this.expectedDuration = expectedDuration;
			this.startTimes = new Dictionary<IEvaluation, DateTime>();
			this.lastChecked = DateTime.UtcNow;
			this.totalElapsed = TimeSpan.Zero;
			this.timer = new Timer(delegate(object o)
			{
				base.CheckEvaluations();
			}, null, -1, -1);
		}

		// Token: 0x0600B980 RID: 47488 RVA: 0x0025957C File Offset: 0x0025777C
		protected override bool ShouldEvaluateNextPending(IEvaluation evaluation)
		{
			DateTime utcNow = DateTime.UtcNow;
			if (base.RunningCount == 0)
			{
				this.totalElapsed = TimeSpan.Zero;
			}
			else
			{
				TimeSpan timeSpan = utcNow - this.lastChecked;
				this.totalElapsed += TimeSpan.FromSeconds((double)base.RunningCount * timeSpan.TotalSeconds);
			}
			this.lastChecked = utcNow;
			TimeSpan timeSpan2 = TimeSpan.FromSeconds((double)base.RunningCount * this.expectedDuration.TotalSeconds) - this.totalElapsed;
			if (timeSpan2 > TimeSpan.Zero)
			{
				this.timer.Change(timeSpan2, TimeSpan.Zero);
				return false;
			}
			return true;
		}

		// Token: 0x0600B981 RID: 47489 RVA: 0x00259628 File Offset: 0x00257828
		protected override void OnEvaluationStarted(IEvaluation evaluation)
		{
			DateTime utcNow = DateTime.UtcNow;
			this.totalElapsed += this.lastChecked - utcNow;
			this.startTimes.Add(evaluation, utcNow);
		}

		// Token: 0x0600B982 RID: 47490 RVA: 0x00259668 File Offset: 0x00257868
		protected override void OnEvaluationCompleted(IEvaluation evaluation)
		{
			DateTime dateTime = this.startTimes[evaluation];
			this.startTimes.Remove(evaluation);
			this.totalElapsed -= this.lastChecked - dateTime;
		}

		// Token: 0x0600B983 RID: 47491 RVA: 0x002596AC File Offset: 0x002578AC
		public override void Dispose()
		{
			if (this.timer != null)
			{
				this.timer.Dispose();
				this.timer = null;
			}
			base.Dispose();
		}

		// Token: 0x04005E6E RID: 24174
		private readonly TimeSpan expectedDuration;

		// Token: 0x04005E6F RID: 24175
		private readonly Dictionary<IEvaluation, DateTime> startTimes;

		// Token: 0x04005E70 RID: 24176
		private DateTime lastChecked;

		// Token: 0x04005E71 RID: 24177
		private TimeSpan totalElapsed;

		// Token: 0x04005E72 RID: 24178
		private Timer timer;
	}
}
