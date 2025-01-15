using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019D4 RID: 6612
	public class EvaluationConstants : IEvaluationConstants
	{
		// Token: 0x0600A76B RID: 42859 RVA: 0x0022A486 File Offset: 0x00228686
		public EvaluationConstants(Guid activityId, string correlationId, IList<EvaluationConstant> tracedConstants = null)
		{
			this.activityId = activityId;
			this.correlationId = correlationId;
			this.tracedConstants = tracedConstants;
		}

		// Token: 0x17002AA1 RID: 10913
		// (get) Token: 0x0600A76C RID: 42860 RVA: 0x0022A4A3 File Offset: 0x002286A3
		public Guid ActivityId
		{
			get
			{
				return this.activityId;
			}
		}

		// Token: 0x17002AA2 RID: 10914
		// (get) Token: 0x0600A76D RID: 42861 RVA: 0x0022A4AB File Offset: 0x002286AB
		public string CorrelationId
		{
			get
			{
				return this.correlationId;
			}
		}

		// Token: 0x17002AA3 RID: 10915
		// (get) Token: 0x0600A76E RID: 42862 RVA: 0x0022A4B3 File Offset: 0x002286B3
		public IEnumerable<EvaluationConstant> TracedConstants
		{
			get
			{
				if (this.tracedConstants != null)
				{
					foreach (EvaluationConstant evaluationConstant in this.tracedConstants)
					{
						yield return evaluationConstant;
					}
					IEnumerator<EvaluationConstant> enumerator = null;
				}
				yield break;
				yield break;
			}
		}

		// Token: 0x0600A76F RID: 42863 RVA: 0x0022A4C3 File Offset: 0x002286C3
		public EvaluationConstants AddTraceConstant(string key, string value, bool isPii)
		{
			return this.AddTraceConstant(new EvaluationConstant(key, value, isPii));
		}

		// Token: 0x0600A770 RID: 42864 RVA: 0x0022A4D4 File Offset: 0x002286D4
		public EvaluationConstants AddTraceConstant(EvaluationConstant constant)
		{
			List<EvaluationConstant> list = new List<EvaluationConstant> { constant };
			foreach (EvaluationConstant evaluationConstant in this.TracedConstants)
			{
				list.Add(evaluationConstant);
			}
			return new EvaluationConstants(this.ActivityId, this.CorrelationId, list);
		}

		// Token: 0x04005729 RID: 22313
		public const string HostProcessId = "HostProcessId";

		// Token: 0x0400572A RID: 22314
		private readonly Guid activityId;

		// Token: 0x0400572B RID: 22315
		private readonly string correlationId;

		// Token: 0x0400572C RID: 22316
		private readonly IList<EvaluationConstant> tracedConstants;
	}
}
