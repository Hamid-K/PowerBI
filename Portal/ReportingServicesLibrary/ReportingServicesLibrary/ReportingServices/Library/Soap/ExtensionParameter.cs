using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000343 RID: 835
	public class ExtensionParameter
	{
		// Token: 0x06001BF1 RID: 7153 RVA: 0x000712B0 File Offset: 0x0006F4B0
		public ExtensionParameter()
		{
			this.Name = null;
			this.DisplayName = null;
			this.Required = false;
			this.RequiredSpecified = false;
			this.Value = null;
			this.Error = null;
			this.ValidValues = null;
			this.Encrypted = false;
			this.IsPassword = false;
		}

		// Token: 0x04000B61 RID: 2913
		public string Name;

		// Token: 0x04000B62 RID: 2914
		public string DisplayName;

		// Token: 0x04000B63 RID: 2915
		public bool Required;

		// Token: 0x04000B64 RID: 2916
		[XmlIgnore]
		public bool RequiredSpecified;

		// Token: 0x04000B65 RID: 2917
		public bool ReadOnly;

		// Token: 0x04000B66 RID: 2918
		public string Value;

		// Token: 0x04000B67 RID: 2919
		public string Error;

		// Token: 0x04000B68 RID: 2920
		public bool Encrypted;

		// Token: 0x04000B69 RID: 2921
		public bool IsPassword;

		// Token: 0x04000B6A RID: 2922
		[XmlArrayItem("Value")]
		public ValidValue[] ValidValues;
	}
}
