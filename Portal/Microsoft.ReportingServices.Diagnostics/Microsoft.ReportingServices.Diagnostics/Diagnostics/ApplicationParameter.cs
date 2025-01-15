using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200001C RID: 28
	[DebuggerDisplay("{m_name} : Actual: '{m_baseValue}' Config: '{ConfigValue}'")]
	internal abstract class ApplicationParameter
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002EBA File Offset: 0x000010BA
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002EC2 File Offset: 0x000010C2
		public bool TraceSuccess
		{
			get
			{
				return this.m_traceSuccess;
			}
			set
			{
				this.m_traceSuccess = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002ECB File Offset: 0x000010CB
		protected RSTrace Tracer
		{
			get
			{
				return this.m_Tracer;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002ED3 File Offset: 0x000010D3
		protected string ConfigValue
		{
			get
			{
				return this.m_configValue;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002EDC File Offset: 0x000010DC
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002F4C File Offset: 0x0000114C
		protected virtual object BaseValue
		{
			get
			{
				if (this.m_ParameterSource.UseExternalStore)
				{
					this.m_configValue = this.m_ParameterSource.GetParameter(this.m_name);
					bool traceSuccess = this.TraceSuccess;
					this.m_traceUsingDefault = false;
					this.TraceSuccess = false;
					this.InterpretValue();
					this.TraceSuccess = traceSuccess;
					this.m_traceUsingDefault = true;
				}
				else if (this.m_BaseValue == null)
				{
					this.InterpretValue();
				}
				return this.m_BaseValue;
			}
			set
			{
				this.m_BaseValue = value;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002F58 File Offset: 0x00001158
		public ApplicationParameter(IParameterSource parameterSource, RSTrace tracer, string name, string configValue, object defaultValue, string units)
		{
			this.m_ParameterSource = parameterSource;
			this.m_Tracer = tracer;
			this.m_name = name;
			this.m_configValue = configValue;
			this.m_BaseDefaultValue = defaultValue;
			this.m_Units = units;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002FDD File Offset: 0x000011DD
		protected virtual bool Validate(string valueToValidate, out object validatedValue)
		{
			validatedValue = valueToValidate;
			return true;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002FE4 File Offset: 0x000011E4
		private void InterpretValue()
		{
			if (this.m_configValue == null)
			{
				this.m_BaseValue = this.m_BaseDefaultValue;
				if (this.m_traceUsingDefault && this.m_Tracer != null && this.m_Tracer.TraceInfo)
				{
					this.m_Tracer.Trace(TraceLevel.Info, this.m_TraceAbsent, new object[]
					{
						this.m_name,
						this.m_BaseValue,
						this.m_Units,
						this.m_ParameterSource.GetSourceNameForTrace()
					});
					return;
				}
			}
			else
			{
				try
				{
					object obj;
					if (this.Validate(this.m_configValue, out obj))
					{
						this.m_BaseValue = obj;
						if (this.m_Tracer != null && this.TraceSuccess && this.m_Tracer.TraceInfo)
						{
							this.m_Tracer.Trace(TraceLevel.Info, this.m_TraceRead, new object[]
							{
								this.m_name,
								this.m_BaseValue,
								this.m_Units,
								this.m_ParameterSource.GetSourceNameForTrace()
							});
						}
					}
					else
					{
						this.InvalidInputSetDefault();
					}
				}
				catch (Exception ex)
				{
					if (!(ex is ArgumentException) && !(ex is FormatException) && !(ex is OverflowException))
					{
						throw;
					}
					this.InvalidInputSetDefault();
				}
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003124 File Offset: 0x00001324
		private void InvalidInputSetDefault()
		{
			this.m_BaseValue = this.m_BaseDefaultValue;
			if (this.m_Tracer != null && this.m_Tracer.TraceWarning)
			{
				this.WriteInvalidConfigEntry();
				this.m_Tracer.Trace(TraceLevel.Warning, this.m_TraceWrong, new object[]
				{
					this.m_name,
					this.m_BaseValue,
					this.m_Units,
					this.m_ParameterSource.GetSourceNameForTrace(),
					this.m_configValue
				});
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000031A2 File Offset: 0x000013A2
		private void WriteInvalidConfigEntry()
		{
			RSEventLog.Current.WriteWarning(Event.InvalidConfigEntry, new object[] { this.m_name });
		}

		// Token: 0x04000063 RID: 99
		private bool m_traceSuccess = true;

		// Token: 0x04000064 RID: 100
		protected bool m_traceUsingDefault = true;

		// Token: 0x04000065 RID: 101
		private object m_BaseDefaultValue;

		// Token: 0x04000066 RID: 102
		private string m_Units = "";

		// Token: 0x04000067 RID: 103
		private string m_name = "";

		// Token: 0x04000068 RID: 104
		private string m_configValue;

		// Token: 0x04000069 RID: 105
		private RSTrace m_Tracer;

		// Token: 0x0400006A RID: 106
		private IParameterSource m_ParameterSource;

		// Token: 0x0400006B RID: 107
		private object m_BaseValue;

		// Token: 0x0400006C RID: 108
		private string m_TraceRead = "Initializing {0} to '{1}' {2} as specified in {3}.";

		// Token: 0x0400006D RID: 109
		private string m_TraceAbsent = "Initializing {0} to default value of '{1}' {2} because it was not specified in {3}.";

		// Token: 0x0400006E RID: 110
		private string m_TraceWrong = "Initializing {0} to default value of '{1}' {2} because it was incorrectly specified in {3} as '{4}'.";
	}
}
