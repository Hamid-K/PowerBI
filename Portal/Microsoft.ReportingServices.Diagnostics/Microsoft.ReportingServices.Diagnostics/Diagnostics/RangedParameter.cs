using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000033 RID: 51
	internal abstract class RangedParameter<T> : ApplicationParameter where T : IComparable<T>
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00003FC6 File Offset: 0x000021C6
		protected RangedParameter(IParameterSource parameterSource, RSTrace tracer, string name, string configValue, T defaultValue, string units, T minValue, T maxValue)
			: base(parameterSource, tracer, name, configValue, defaultValue, units)
		{
			this.m_minValue = minValue;
			this.m_maxValue = maxValue;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003FEC File Offset: 0x000021EC
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00003FF4 File Offset: 0x000021F4
		public T MaxValue
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_maxValue;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_maxValue = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003FFD File Offset: 0x000021FD
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00004005 File Offset: 0x00002205
		public T MinValue
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_minValue;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_minValue = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000400E File Offset: 0x0000220E
		public T Value
		{
			[DebuggerStepThrough]
			get
			{
				return (T)((object)this.BaseValue);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000401C File Offset: 0x0000221C
		protected override bool Validate(string valueToValidate, out object validatedValue)
		{
			T t = default(T);
			t = this.Parse(valueToValidate);
			if (t.CompareTo(this.MinValue) < 0 || t.CompareTo(this.MaxValue) > 0)
			{
				if (base.Tracer.TraceWarning)
				{
					base.Tracer.Trace("Value '{0}' of parameter {1} is out of the range [{2}, {3}].", new object[] { valueToValidate, base.ConfigValue, this.MinValue, this.MaxValue });
				}
				validatedValue = null;
				return false;
			}
			validatedValue = t;
			return true;
		}

		// Token: 0x060000F1 RID: 241
		protected abstract T Parse(string valueToValidate);

		// Token: 0x0400012B RID: 299
		private const string m_TraceOutOfRange = "Value '{0}' of parameter {1} is out of the range [{2}, {3}].";

		// Token: 0x0400012C RID: 300
		protected T m_minValue;

		// Token: 0x0400012D RID: 301
		protected T m_maxValue;
	}
}
