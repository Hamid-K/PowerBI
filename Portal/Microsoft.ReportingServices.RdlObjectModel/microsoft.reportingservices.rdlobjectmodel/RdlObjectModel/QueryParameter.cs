using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000C0 RID: 192
	public class QueryParameter : ReportObject, INamedObject
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0001C0BB File Offset: 0x0001A2BB
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x0001C0CE File Offset: 0x0001A2CE
		[XmlAttribute(typeof(string))]
		public string Name
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

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0001C0DD File Offset: 0x0001A2DD
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x0001C0EB File Offset: 0x0001A2EB
		public ReportExpression Value
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

		// Token: 0x06000817 RID: 2071 RVA: 0x0001C0FF File Offset: 0x0001A2FF
		public QueryParameter()
		{
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001C107 File Offset: 0x0001A307
		internal QueryParameter(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0001C110 File Offset: 0x0001A310
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x0001C118 File Offset: 0x0001A318
		[DefaultValue(false)]
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public bool UserDefined
		{
			get
			{
				return this.m_userDefined;
			}
			set
			{
				if (this.m_userDefined != value)
				{
					this.m_userDefined = value;
				}
			}
		}

		// Token: 0x04000160 RID: 352
		private bool m_userDefined;

		// Token: 0x0200036C RID: 876
		internal class Definition : DefinitionStore<QueryParameter, QueryParameter.Definition.Properties>
		{
			// Token: 0x02000489 RID: 1161
			internal enum Properties
			{
				// Token: 0x04000B20 RID: 2848
				Name,
				// Token: 0x04000B21 RID: 2849
				Value
			}
		}
	}
}
