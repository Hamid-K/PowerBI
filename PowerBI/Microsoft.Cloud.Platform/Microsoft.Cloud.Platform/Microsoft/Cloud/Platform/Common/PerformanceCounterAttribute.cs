using System;
using System.Diagnostics;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x0200053E RID: 1342
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public sealed class PerformanceCounterAttribute : Attribute
	{
		// Token: 0x060028DB RID: 10459 RVA: 0x000926DB File Offset: 0x000908DB
		public PerformanceCounterAttribute(int id, string counterName, CounterModifier counterModifier, CounterType counterType)
			: this(id, counterName, counterModifier, "1", counterType)
		{
			if (counterModifier == CounterModifier.Set)
			{
				throw new ArgumentException("'Set' action requires modifierExpression", "counterModifier");
			}
			if (counterType == CounterType.AverageDelta)
			{
				throw new ArgumentException("'AverageDelta' type requires modifierExpression", "counterType");
			}
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x0009271A File Offset: 0x0009091A
		public PerformanceCounterAttribute(int id, string counterName, CounterModifier counterModifier, string modifierExpression, CounterType counterType, string baseModifierExpression)
			: this(id, counterName, counterModifier, modifierExpression, counterType)
		{
			if (counterType != CounterType.AverageDelta)
			{
				throw new ArgumentException("'baseModifierExpression' requires 'AverageDelta' counter type", "counterType");
			}
			this.m_baseModifierExpression = baseModifierExpression;
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x0009274C File Offset: 0x0009094C
		public PerformanceCounterAttribute(int id, string counterName, CounterModifier counterModifier, string modifierExpression, CounterType counterType)
		{
			this.m_counterName = counterName;
			this.m_counterModifier = counterModifier;
			this.m_modifierExpression = modifierExpression;
			this.InstanceNameFormat = "{0}";
			this.m_counterType = counterType;
			this.m_id = id;
			if (counterType == CounterType.AverageDelta)
			{
				this.m_baseModifierExpression = "1";
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060028DE RID: 10462 RVA: 0x000927A3 File Offset: 0x000909A3
		public string CounterName
		{
			get
			{
				return this.m_counterName;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060028DF RID: 10463 RVA: 0x000927AB File Offset: 0x000909AB
		public CounterType CounterType
		{
			get
			{
				return this.m_counterType;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060028E0 RID: 10464 RVA: 0x000927AB File Offset: 0x000909AB
		public PerformanceCounterType WinPerformanceCounterType
		{
			get
			{
				return (PerformanceCounterType)this.m_counterType;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x000927B3 File Offset: 0x000909B3
		// (set) Token: 0x060028E2 RID: 10466 RVA: 0x000927BB File Offset: 0x000909BB
		public string InstanceNameFormat { get; set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x000927C4 File Offset: 0x000909C4
		public CounterModifier CounterModifier
		{
			get
			{
				return this.m_counterModifier;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060028E4 RID: 10468 RVA: 0x000927CC File Offset: 0x000909CC
		public string ModifierExpression
		{
			get
			{
				return this.m_modifierExpression;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x000927D4 File Offset: 0x000909D4
		public string BaseModifierExpression
		{
			get
			{
				return this.m_baseModifierExpression;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060028E6 RID: 10470 RVA: 0x000927DC File Offset: 0x000909DC
		public int Id
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x04000E94 RID: 3732
		private readonly string m_counterName;

		// Token: 0x04000E95 RID: 3733
		private readonly CounterType m_counterType;

		// Token: 0x04000E96 RID: 3734
		private readonly CounterModifier m_counterModifier;

		// Token: 0x04000E97 RID: 3735
		private readonly string m_modifierExpression;

		// Token: 0x04000E98 RID: 3736
		private readonly string m_baseModifierExpression;

		// Token: 0x04000E99 RID: 3737
		private readonly int m_id;

		// Token: 0x04000E9A RID: 3738
		private const string DEFAULT_INSTANCE_FORMAT = "{0}";
	}
}
