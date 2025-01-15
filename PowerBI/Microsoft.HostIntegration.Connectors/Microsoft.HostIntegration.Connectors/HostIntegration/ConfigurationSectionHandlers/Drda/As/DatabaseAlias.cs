using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x0200053F RID: 1343
	public class DatabaseAlias : ConfigurationElement
	{
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x00098592 File Offset: 0x00096792
		// (set) Token: 0x06002D63 RID: 11619 RVA: 0x000985A4 File Offset: 0x000967A4
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("sourceLocation", IsRequired = true)]
		public string SourceLocation
		{
			get
			{
				return (string)base["sourceLocation"];
			}
			set
			{
				base["sourceLocation"] = value;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x000985B2 File Offset: 0x000967B2
		// (set) Token: 0x06002D65 RID: 11621 RVA: 0x000985C4 File Offset: 0x000967C4
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("sourceCollection", IsRequired = true)]
		public string SourceCollection
		{
			get
			{
				return (string)base["sourceCollection"];
			}
			set
			{
				base["sourceCollection"] = value;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002D66 RID: 11622 RVA: 0x000985D2 File Offset: 0x000967D2
		// (set) Token: 0x06002D67 RID: 11623 RVA: 0x000985E4 File Offset: 0x000967E4
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("targetDatabase", IsRequired = true)]
		public string TargetDatabase
		{
			get
			{
				return (string)base["targetDatabase"];
			}
			set
			{
				base["targetDatabase"] = value;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06002D68 RID: 11624 RVA: 0x000985F2 File Offset: 0x000967F2
		// (set) Token: 0x06002D69 RID: 11625 RVA: 0x00098604 File Offset: 0x00096804
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("targetSchema", IsRequired = true)]
		public string TargetSchema
		{
			get
			{
				return (string)base["targetSchema"];
			}
			set
			{
				base["targetSchema"] = value;
			}
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x00098612 File Offset: 0x00096812
		public object GetElementKey()
		{
			return this.SourceLocation + this.SourceCollection + this.TargetDatabase + this.TargetSchema;
		}
	}
}
