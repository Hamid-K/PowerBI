using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003FA RID: 1018
	public sealed class ReportParameter
	{
		// Token: 0x06002041 RID: 8257 RVA: 0x000025F4 File Offset: 0x000007F4
		public ReportParameter()
		{
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x0007F38C File Offset: 0x0007D58C
		internal ReportParameter(string name, ReportParameters.DataType dataType, bool nullable, string defaultValue, string prompt, ArrayList values)
		{
			this.Name = name;
			this.DataType = dataType;
			this.Nullable = nullable;
			this.Prompt = prompt;
			if (defaultValue != null && defaultValue != "")
			{
				this.DefaultValue = new ReportParameters.DefaultValue();
				this.DefaultValue.Values = new ArrayList();
				this.DefaultValue.Values.Add(new Expression(defaultValue));
			}
			if (values != null && values.Count > 0)
			{
				this.ValidValues = new ReportParameters.ValidValues();
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < values.Count; i++)
				{
					ReportParameters.ParameterValue parameterValue = new ReportParameters.ParameterValue();
					arrayList.Add(parameterValue);
					parameterValue.Value = (string)values[i];
				}
				this.ValidValues.ParameterValues = arrayList;
			}
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x0007F45F File Offset: 0x0007D65F
		internal void AddRecentValue(string value)
		{
			if (this.RecentValues == null)
			{
				this.RecentValues = new ArrayList();
			}
			if (!this.RecentValues.Contains(value))
			{
				this.RecentValues.Insert(0, value);
			}
		}

		// Token: 0x04000E0F RID: 3599
		public string Name;

		// Token: 0x04000E10 RID: 3600
		public ReportParameters.DataType DataType;

		// Token: 0x04000E11 RID: 3601
		[DefaultValue(false)]
		public bool Nullable;

		// Token: 0x04000E12 RID: 3602
		[DefaultValue("")]
		public ReportParameters.DefaultValue DefaultValue;

		// Token: 0x04000E13 RID: 3603
		[DefaultValue(false)]
		public bool AllowBlank;

		// Token: 0x04000E14 RID: 3604
		[DefaultValue("")]
		public string Prompt;

		// Token: 0x04000E15 RID: 3605
		[DefaultValue(false)]
		public bool Hidden;

		// Token: 0x04000E16 RID: 3606
		public ReportParameters.ValidValues ValidValues;

		// Token: 0x04000E17 RID: 3607
		[DefaultValue(false)]
		public bool MultiValue;

		// Token: 0x04000E18 RID: 3608
		[DefaultValue(ReportParameter.UsedInQueryEnum.Auto)]
		public ReportParameter.UsedInQueryEnum UsedInQuery;

		// Token: 0x04000E19 RID: 3609
		internal ArrayList RecentValues;

		// Token: 0x02000520 RID: 1312
		public enum UsedInQueryEnum
		{
			// Token: 0x0400129A RID: 4762
			Auto,
			// Token: 0x0400129B RID: 4763
			True,
			// Token: 0x0400129C RID: 4764
			False
		}
	}
}
