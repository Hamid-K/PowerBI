using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000BD RID: 189
	public class SharedDataSet : ReportObject
	{
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0001BD39 File Offset: 0x00019F39
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x0001BD4C File Offset: 0x00019F4C
		public string SharedDataSetReference
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

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001BD5B File Offset: 0x00019F5B
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0001BD6E File Offset: 0x00019F6E
		[XmlElement(typeof(RdlCollection<QueryParameter>))]
		public IList<QueryParameter> QueryParameters
		{
			get
			{
				return (IList<QueryParameter>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0001BD7D File Offset: 0x00019F7D
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x0001BD85 File Offset: 0x00019F85
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		[DefaultValue("")]
		public string ReportServerUrl
		{
			get
			{
				return this.m_reportServerUrl;
			}
			set
			{
				this.m_reportServerUrl = value;
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001BD8E File Offset: 0x00019F8E
		public SharedDataSet()
		{
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001BD96 File Offset: 0x00019F96
		internal SharedDataSet(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001BD9F File Offset: 0x00019F9F
		public override void Initialize()
		{
			base.Initialize();
			this.QueryParameters = new RdlCollection<QueryParameter>();
		}

		// Token: 0x04000159 RID: 345
		private string m_reportServerUrl;

		// Token: 0x02000369 RID: 873
		internal class Definition : DefinitionStore<SharedDataSet, SharedDataSet.Definition.Properties>
		{
			// Token: 0x02000486 RID: 1158
			internal enum Properties
			{
				// Token: 0x04000B13 RID: 2835
				SharedDataSetReference,
				// Token: 0x04000B14 RID: 2836
				QueryParameters
			}
		}
	}
}
