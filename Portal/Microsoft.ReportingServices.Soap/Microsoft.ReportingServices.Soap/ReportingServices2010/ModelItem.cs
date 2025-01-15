using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000021 RID: 33
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ModelItem
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000CD77 File Offset: 0x0000AF77
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x0000CD7F File Offset: 0x0000AF7F
		public string ID
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0000CD88 File Offset: 0x0000AF88
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x0000CD90 File Offset: 0x0000AF90
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0000CD99 File Offset: 0x0000AF99
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x0000CDA1 File Offset: 0x0000AFA1
		public string ModelItemTypeName
		{
			get
			{
				return this.modelItemTypeNameField;
			}
			set
			{
				this.modelItemTypeNameField = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0000CDAA File Offset: 0x0000AFAA
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0000CDB2 File Offset: 0x0000AFB2
		public string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000CDBB File Offset: 0x0000AFBB
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x0000CDC3 File Offset: 0x0000AFC3
		public ModelItem[] ModelItems
		{
			get
			{
				return this.modelItemsField;
			}
			set
			{
				this.modelItemsField = value;
			}
		}

		// Token: 0x04000181 RID: 385
		private string idField;

		// Token: 0x04000182 RID: 386
		private string nameField;

		// Token: 0x04000183 RID: 387
		private string modelItemTypeNameField;

		// Token: 0x04000184 RID: 388
		private string descriptionField;

		// Token: 0x04000185 RID: 389
		private ModelItem[] modelItemsField;
	}
}
