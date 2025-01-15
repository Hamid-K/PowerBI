using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003D4 RID: 980
	internal class FailPoint
	{
		// Token: 0x06002279 RID: 8825 RVA: 0x0006A6A9 File Offset: 0x000688A9
		public FailPoint(IFailPointCriteria criteria, IFailPointAction action, int priority)
		{
			if (criteria == null)
			{
				throw new ArgumentNullException("criteria");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			this.m_criteria = criteria;
			this.m_action = action;
			this.m_priority = priority;
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x0006A6E2 File Offset: 0x000688E2
		public FailPoint(IFailPointCriteria criteria, IFailPointAction action)
			: this(criteria, action, 0)
		{
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x0006A6F0 File Offset: 0x000688F0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "criteria: {0} priority: {1} action: {2}", new object[] { this.m_criteria, this.m_priority, this.m_action });
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x0006A734 File Offset: 0x00068934
		public static void Set(FailPoint failpoint)
		{
			if (failpoint == null)
			{
				throw new ArgumentNullException("failpoint");
			}
			EventLogWriter.WriteInfo("FailPoint.Set", "Setting failpoint {0}", new object[] { failpoint });
			lock (FailPoint.s_failpoints)
			{
				FailPoint.Remove(failpoint.m_criteria);
				FailPoint.s_failpoints.Add(failpoint);
			}
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x0006A7A8 File Offset: 0x000689A8
		public static void Set(IFailPointCriteria criteria, IFailPointAction action, int priority)
		{
			FailPoint.Set(new FailPoint(criteria, action, priority));
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x0006A7B7 File Offset: 0x000689B7
		public static void Set(IFailPointCriteria criteria, IFailPointAction action)
		{
			FailPoint.Set(new FailPoint(criteria, action));
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x0006A7C5 File Offset: 0x000689C5
		public static void Remove(FailPoint failpoint)
		{
			if (failpoint == null)
			{
				throw new ArgumentNullException("failpoint");
			}
			FailPoint.Remove(failpoint.m_criteria);
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x0006A7E0 File Offset: 0x000689E0
		public static void Remove(IFailPointCriteria criteria)
		{
			if (criteria == null)
			{
				throw new ArgumentNullException("criteria");
			}
			EventLogWriter.WriteInfo("FailPoint.Remove", "Remove failpoint with criteria {0}", new object[] { criteria });
			lock (FailPoint.s_failpoints)
			{
				for (int i = 0; i < FailPoint.s_failpoints.Count; i++)
				{
					if (FailPoint.s_failpoints[i].m_criteria.Equals(criteria))
					{
						FailPoint.s_failpoints.RemoveAt(i);
						break;
					}
				}
			}
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x0006A878 File Offset: 0x00068A78
		public static bool Check(FailPointContext context)
		{
			FailPoint failPoint = null;
			bool flag = false;
			lock (FailPoint.s_failpoints)
			{
				foreach (FailPoint failPoint2 in FailPoint.s_failpoints)
				{
					if (failPoint2.m_criteria.Match(context))
					{
						if (failPoint == null || failPoint2.m_priority > failPoint.m_priority)
						{
							failPoint = failPoint2;
							flag = false;
						}
						else if (failPoint != null && failPoint2.m_priority == failPoint.m_priority)
						{
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				throw new FailPointException("Multiple failpoints matched!");
			}
			if (failPoint == null)
			{
				return false;
			}
			EventLogWriter.WriteInfo("FailPoint.Activate", "Fail point {0} activated", new object[] { failPoint });
			failPoint.m_action.Invoke(context);
			if (context["removefp"] != null && (bool)context["removefp"])
			{
				FailPoint.Remove(failPoint.m_criteria);
			}
			return true;
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x0006A98C File Offset: 0x00068B8C
		public static string List()
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			lock (FailPoint.s_failpoints)
			{
				foreach (FailPoint failPoint in FailPoint.s_failpoints)
				{
					stringBuilder.AppendFormat("{0}\n", failPoint);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x0006AA18 File Offset: 0x00068C18
		public static IFailPointAction CreateAction(string actionClassName)
		{
			return (IFailPointAction)Utility.CreateInstanceByReflection(actionClassName);
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x0006AA28 File Offset: 0x00068C28
		public static void RemoveAll()
		{
			EventLogWriter.WriteInfo("FailPoint.RemoveAll", "Removing all failpoints.", new object[0]);
			lock (FailPoint.s_failpoints)
			{
				FailPoint.s_failpoints.Clear();
			}
		}

		// Token: 0x040015B3 RID: 5555
		private IFailPointCriteria m_criteria;

		// Token: 0x040015B4 RID: 5556
		private IFailPointAction m_action;

		// Token: 0x040015B5 RID: 5557
		private int m_priority;

		// Token: 0x040015B6 RID: 5558
		private static List<FailPoint> s_failpoints = new List<FailPoint>();
	}
}
