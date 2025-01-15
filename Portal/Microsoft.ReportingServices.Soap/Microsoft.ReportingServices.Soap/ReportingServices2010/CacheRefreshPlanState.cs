using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000007 RID: 7
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class CacheRefreshPlanState
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000C5E9 File Offset: 0x0000A7E9
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0000C5F1 File Offset: 0x0000A7F1
		public bool MissingParameterValue
		{
			get
			{
				return this.missingParameterValueField;
			}
			set
			{
				this.missingParameterValueField = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000C5FA File Offset: 0x0000A7FA
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x0000C602 File Offset: 0x0000A802
		public bool InvalidParameterValue
		{
			get
			{
				return this.invalidParameterValueField;
			}
			set
			{
				this.invalidParameterValueField = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000C60B File Offset: 0x0000A80B
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0000C613 File Offset: 0x0000A813
		public bool UnknownItemParameter
		{
			get
			{
				return this.unknownItemParameterField;
			}
			set
			{
				this.unknownItemParameterField = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000C61C File Offset: 0x0000A81C
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x0000C624 File Offset: 0x0000A824
		public bool CachingNotEnabledOnItem
		{
			get
			{
				return this.cachingNotEnabledOnItemField;
			}
			set
			{
				this.cachingNotEnabledOnItemField = value;
			}
		}

		// Token: 0x04000115 RID: 277
		private bool missingParameterValueField;

		// Token: 0x04000116 RID: 278
		private bool invalidParameterValueField;

		// Token: 0x04000117 RID: 279
		private bool unknownItemParameterField;

		// Token: 0x04000118 RID: 280
		private bool cachingNotEnabledOnItemField;
	}
}
