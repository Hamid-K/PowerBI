using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001FF RID: 511
	public class TablixBody : DataRegionBody
	{
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x00027C47 File Offset: 0x00025E47
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x00027C5A File Offset: 0x00025E5A
		[XmlElement(typeof(RdlCollection<TablixColumn>))]
		public IList<TablixColumn> TablixColumns
		{
			get
			{
				return (IList<TablixColumn>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x00027C69 File Offset: 0x00025E69
		// (set) Token: 0x06001136 RID: 4406 RVA: 0x00027C7C File Offset: 0x00025E7C
		[XmlElement(typeof(RdlCollection<TablixRow>))]
		public IList<TablixRow> TablixRows
		{
			get
			{
				return (IList<TablixRow>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x00027C8B File Offset: 0x00025E8B
		// (set) Token: 0x06001138 RID: 4408 RVA: 0x00027C93 File Offset: 0x00025E93
		[XmlElement(typeof(RdlCollection<FieldSelection>), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		[XmlArrayItem("FieldSelection", typeof(FieldSelection), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		public IList<FieldSelection> FieldSelections
		{
			get
			{
				return this.m_FieldSelections;
			}
			set
			{
				this.m_FieldSelections = value;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x00027C9C File Offset: 0x00025E9C
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x00027CA4 File Offset: 0x00025EA4
		[XmlElement(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		[DefaultValue(false)]
		public bool ShowGrandTotals { get; set; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x00027CAD File Offset: 0x00025EAD
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x00027CB5 File Offset: 0x00025EB5
		[XmlElement(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		[DefaultValue("Summarized")]
		public string PreviewDataType { get; set; }

		// Token: 0x0600113D RID: 4413 RVA: 0x00027CBE File Offset: 0x00025EBE
		public TablixBody()
		{
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00027CC6 File Offset: 0x00025EC6
		internal TablixBody(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00027CCF File Offset: 0x00025ECF
		public override void Initialize()
		{
			base.Initialize();
			this.TablixColumns = new RdlCollection<TablixColumn>();
			this.TablixRows = new RdlCollection<TablixRow>();
			this.FieldSelections = new RdlCollection<FieldSelection>();
		}

		// Token: 0x04000584 RID: 1412
		private IList<FieldSelection> m_FieldSelections;

		// Token: 0x02000405 RID: 1029
		internal class Definition : DefinitionStore<TablixBody, TablixBody.Definition.Properties>
		{
			// Token: 0x060018DE RID: 6366 RVA: 0x0003BF63 File Offset: 0x0003A163
			private Definition()
			{
			}

			// Token: 0x02000514 RID: 1300
			internal enum Properties
			{
				// Token: 0x0400111C RID: 4380
				TablixColumns,
				// Token: 0x0400111D RID: 4381
				TablixRows
			}
		}
	}
}
