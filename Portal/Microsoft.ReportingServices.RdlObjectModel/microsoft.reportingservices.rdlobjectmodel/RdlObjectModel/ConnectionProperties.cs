using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000C2 RID: 194
	public class ConnectionProperties : ReportObject
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0001C267 File Offset: 0x0001A467
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x0001C27A File Offset: 0x0001A47A
		public string DataProvider
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0001C289 File Offset: 0x0001A489
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x0001C297 File Offset: 0x0001A497
		public ReportExpression ConnectString
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0001C2AB File Offset: 0x0001A4AB
		// (set) Token: 0x0600082D RID: 2093 RVA: 0x0001C2B9 File Offset: 0x0001A4B9
		[DefaultValue(false)]
		public bool IntegratedSecurity
		{
			get
			{
				return base.PropertyStore.GetBoolean(2);
			}
			set
			{
				base.PropertyStore.SetBoolean(2, value);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0001C2C8 File Offset: 0x0001A4C8
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x0001C2DB File Offset: 0x0001A4DB
		[DefaultValue("")]
		public string Prompt
		{
			get
			{
				return (string)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001C2EA File Offset: 0x0001A4EA
		public ConnectionProperties()
		{
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001C2F2 File Offset: 0x0001A4F2
		internal ConnectionProperties(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0200036E RID: 878
		internal class Definition : DefinitionStore<ConnectionProperties, ConnectionProperties.Definition.Properties>
		{
			// Token: 0x0200048B RID: 1163
			internal enum Properties
			{
				// Token: 0x04000B28 RID: 2856
				DataProvider,
				// Token: 0x04000B29 RID: 2857
				ConnectString,
				// Token: 0x04000B2A RID: 2858
				IntegratedSecurity,
				// Token: 0x04000B2B RID: 2859
				Prompt,
				// Token: 0x04000B2C RID: 2860
				PromptLocID
			}
		}
	}
}
