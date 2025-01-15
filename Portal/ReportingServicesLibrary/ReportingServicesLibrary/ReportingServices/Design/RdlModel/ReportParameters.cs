using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003F9 RID: 1017
	[Editor("ReportParametersDialog", typeof(UITypeEditor))]
	public sealed class ReportParameters : ArrayList
	{
		// Token: 0x17000918 RID: 2328
		public ReportParameter this[int index]
		{
			get
			{
				return (ReportParameter)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x0007F350 File Offset: 0x0007D550
		internal ReportParameter Find(string name)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (string.Compare(this[i].Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return this[i];
				}
			}
			return null;
		}

		// Token: 0x0200051B RID: 1307
		public enum DataType
		{
			// Token: 0x0400128B RID: 4747
			Boolean,
			// Token: 0x0400128C RID: 4748
			DateTime,
			// Token: 0x0400128D RID: 4749
			Integer,
			// Token: 0x0400128E RID: 4750
			Float,
			// Token: 0x0400128F RID: 4751
			String
		}

		// Token: 0x0200051C RID: 1308
		public class DataSetReference
		{
			// Token: 0x04001290 RID: 4752
			public string DataSetName;

			// Token: 0x04001291 RID: 4753
			public string ValueField;

			// Token: 0x04001292 RID: 4754
			[DefaultValue("")]
			public string LabelField;
		}

		// Token: 0x0200051D RID: 1309
		public class ParameterValue
		{
			// Token: 0x0600251C RID: 9500 RVA: 0x000878FA File Offset: 0x00085AFA
			public override string ToString()
			{
				if (this.Label == null || this.Label.Length <= 0)
				{
					return this.Value;
				}
				return this.Label;
			}

			// Token: 0x04001293 RID: 4755
			public string Value;

			// Token: 0x04001294 RID: 4756
			[DefaultValue("")]
			public string Label;
		}

		// Token: 0x0200051E RID: 1310
		public class ValidValues
		{
			// Token: 0x04001295 RID: 4757
			public ReportParameters.DataSetReference DataSetReference;

			// Token: 0x04001296 RID: 4758
			[XmlArrayItem("ParameterValue", typeof(ReportParameters.ParameterValue))]
			public ArrayList ParameterValues;
		}

		// Token: 0x0200051F RID: 1311
		public class DefaultValue
		{
			// Token: 0x04001297 RID: 4759
			public ReportParameters.DataSetReference DataSetReference;

			// Token: 0x04001298 RID: 4760
			[XmlArrayItem("Value", typeof(Expression))]
			public ArrayList Values;
		}
	}
}
