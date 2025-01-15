using System;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007B2 RID: 1970
	internal sealed class ParameterImpl : Parameter
	{
		// Token: 0x06006FE6 RID: 28646 RVA: 0x001D27E2 File Offset: 0x001D09E2
		internal ParameterImpl()
		{
		}

		// Token: 0x06006FE7 RID: 28647 RVA: 0x001D27EC File Offset: 0x001D09EC
		internal ParameterImpl(ParameterInfo parameterInfo)
		{
			this.m_value = parameterInfo.Values;
			this.m_labels = parameterInfo.Labels;
			this.m_isMultiValue = parameterInfo.MultiValue;
			this.m_prompt = parameterInfo.Prompt;
			this.m_isUserSupplied = parameterInfo.IsUserSupplied;
			if (parameterInfo.ParameterObjectType == ObjectType.QueryParameter)
			{
				this.m_isDataSetQueryParameter = true;
			}
		}

		// Token: 0x1700261E RID: 9758
		// (get) Token: 0x06006FE8 RID: 28648 RVA: 0x001D284C File Offset: 0x001D0A4C
		public override object Value
		{
			get
			{
				if (this.m_value == null)
				{
					return null;
				}
				if (!this.m_isMultiValue || (this.m_isDataSetQueryParameter && this.m_value.Length == 1))
				{
					return this.m_value[0];
				}
				return this.m_value;
			}
		}

		// Token: 0x1700261F RID: 9759
		// (get) Token: 0x06006FE9 RID: 28649 RVA: 0x001D2882 File Offset: 0x001D0A82
		public override object Label
		{
			get
			{
				if (this.m_labels == null || this.m_labels.Length == 0)
				{
					return null;
				}
				if (!this.m_isMultiValue || (this.m_isDataSetQueryParameter && this.m_labels.Length == 1))
				{
					return this.m_labels[0];
				}
				return this.m_labels;
			}
		}

		// Token: 0x17002620 RID: 9760
		// (get) Token: 0x06006FEA RID: 28650 RVA: 0x001D28C1 File Offset: 0x001D0AC1
		public override int Count
		{
			get
			{
				if (this.m_value == null)
				{
					return 0;
				}
				return this.m_value.Length;
			}
		}

		// Token: 0x17002621 RID: 9761
		// (get) Token: 0x06006FEB RID: 28651 RVA: 0x001D28D5 File Offset: 0x001D0AD5
		public override bool IsMultiValue
		{
			get
			{
				return this.m_isMultiValue;
			}
		}

		// Token: 0x17002622 RID: 9762
		// (get) Token: 0x06006FEC RID: 28652 RVA: 0x001D28DD File Offset: 0x001D0ADD
		internal bool IsUserSupplied
		{
			get
			{
				return this.m_isUserSupplied;
			}
		}

		// Token: 0x06006FED RID: 28653 RVA: 0x001D28E5 File Offset: 0x001D0AE5
		internal void SetIsMultiValue(bool isMultiValue)
		{
			this.m_isMultiValue = isMultiValue;
		}

		// Token: 0x06006FEE RID: 28654 RVA: 0x001D28EE File Offset: 0x001D0AEE
		internal void SetIsUserSupplied(bool isUserSupplied)
		{
			this.m_isUserSupplied = isUserSupplied;
		}

		// Token: 0x06006FEF RID: 28655 RVA: 0x001D28F7 File Offset: 0x001D0AF7
		internal void SetValues(object[] values)
		{
			this.m_value = values;
		}

		// Token: 0x06006FF0 RID: 28656 RVA: 0x001D2900 File Offset: 0x001D0B00
		internal object[] GetValues()
		{
			return this.m_value;
		}

		// Token: 0x06006FF1 RID: 28657 RVA: 0x001D2908 File Offset: 0x001D0B08
		internal void SetLabels(string[] labels)
		{
			this.m_labels = labels;
		}

		// Token: 0x06006FF2 RID: 28658 RVA: 0x001D2911 File Offset: 0x001D0B11
		internal string[] GetLabels()
		{
			return this.m_labels;
		}

		// Token: 0x17002623 RID: 9763
		// (get) Token: 0x06006FF3 RID: 28659 RVA: 0x001D2919 File Offset: 0x001D0B19
		internal string Prompt
		{
			get
			{
				return this.m_prompt;
			}
		}

		// Token: 0x06006FF4 RID: 28660 RVA: 0x001D2921 File Offset: 0x001D0B21
		internal void SetPrompt(string prompt)
		{
			this.m_prompt = prompt;
		}

		// Token: 0x06006FF5 RID: 28661 RVA: 0x001D292C File Offset: 0x001D0B2C
		internal bool ValuesAreEqual(ParameterImpl obj)
		{
			if (!this.m_isUserSupplied)
			{
				return true;
			}
			int count = this.Count;
			if (obj == null || count != obj.Count)
			{
				return false;
			}
			object[] values = obj.GetValues();
			for (int i = 0; i < count; i++)
			{
				if (!object.Equals(this.m_value[i], values[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006FF6 RID: 28662 RVA: 0x001D2980 File Offset: 0x001D0B80
		internal int GetValuesHashCode()
		{
			if (!this.m_isUserSupplied)
			{
				return 0;
			}
			if (this.m_hash == 0)
			{
				int count = this.Count;
				this.m_hash = 6659 | (count + 1 << 16);
				for (int i = 0; i < count; i++)
				{
					if (this.m_value[i] != null)
					{
						this.m_hash ^= this.m_value[i].GetHashCode();
					}
				}
			}
			return this.m_hash;
		}

		// Token: 0x040039CE RID: 14798
		private object[] m_value;

		// Token: 0x040039CF RID: 14799
		private string[] m_labels;

		// Token: 0x040039D0 RID: 14800
		private bool m_isMultiValue;

		// Token: 0x040039D1 RID: 14801
		private string m_prompt;

		// Token: 0x040039D2 RID: 14802
		private int m_hash;

		// Token: 0x040039D3 RID: 14803
		private bool m_isUserSupplied;

		// Token: 0x040039D4 RID: 14804
		private bool m_isDataSetQueryParameter;
	}
}
