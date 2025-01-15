using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000028 RID: 40
	internal sealed class DataSet
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00005868 File Offset: 0x00003A68
		public void FromSoapDataSet(DataSetDefinition soapDataSet)
		{
			this.Fields = null;
			this.Query = null;
			this.Fields = new Fields(soapDataSet.Fields);
			this.Query = new Query(soapDataSet.Query);
			if (soapDataSet.CaseSensitivitySpecified)
			{
				this.CaseSensitivity = soapDataSet.CaseSensitivity;
			}
			this.Collation = soapDataSet.Collation;
			if (soapDataSet.AccentSensitivitySpecified)
			{
				this.AccentSensitivity = soapDataSet.AccentSensitivity;
			}
			if (soapDataSet.KanatypeSensitivitySpecified)
			{
				this.KanatypeSensitivity = soapDataSet.KanatypeSensitivity;
			}
			if (soapDataSet.WidthSensitivitySpecified)
			{
				this.WidthSensitivity = soapDataSet.WidthSensitivity;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005904 File Offset: 0x00003B04
		public DataSetDefinition ToSoapDataSet()
		{
			return new DataSetDefinition
			{
				Fields = this.Fields.ToSoapFieldsArray(),
				Query = this.Query.ToSoapQuery(),
				CaseSensitivitySpecified = true,
				CaseSensitivity = this.CaseSensitivity,
				Collation = this.Collation,
				AccentSensitivitySpecified = true,
				AccentSensitivity = this.AccentSensitivity,
				KanatypeSensitivitySpecified = true,
				KanatypeSensitivity = this.KanatypeSensitivity,
				WidthSensitivitySpecified = true,
				WidthSensitivity = this.WidthSensitivity
			};
		}

		// Token: 0x040000ED RID: 237
		private const string _DataSet = "DataSet";

		// Token: 0x040000EE RID: 238
		private const string _CaseSensitivity = "CaseSensitivity";

		// Token: 0x040000EF RID: 239
		private const string _Collation = "Collation";

		// Token: 0x040000F0 RID: 240
		private const string _AccentSensitivity = "AccentSensitivity";

		// Token: 0x040000F1 RID: 241
		private const string _KanatypeSensitivity = "KanatypeSensitivity";

		// Token: 0x040000F2 RID: 242
		private const string _WidthSensitivity = "WidthSensitivity";

		// Token: 0x040000F3 RID: 243
		public Fields Fields = new Fields();

		// Token: 0x040000F4 RID: 244
		public Query Query = new Query();

		// Token: 0x040000F5 RID: 245
		public SensitivityEnum CaseSensitivity = SensitivityEnum.False;

		// Token: 0x040000F6 RID: 246
		public string Collation = "";

		// Token: 0x040000F7 RID: 247
		public SensitivityEnum AccentSensitivity = SensitivityEnum.False;

		// Token: 0x040000F8 RID: 248
		public SensitivityEnum KanatypeSensitivity = SensitivityEnum.False;

		// Token: 0x040000F9 RID: 249
		public SensitivityEnum WidthSensitivity = SensitivityEnum.False;
	}
}
