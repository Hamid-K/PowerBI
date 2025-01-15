using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001EA RID: 490
	public class ValidValues : ReportObject
	{
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x000265C1 File Offset: 0x000247C1
		// (set) Token: 0x06001044 RID: 4164 RVA: 0x000265D4 File Offset: 0x000247D4
		public DataSetReference DataSetReference
		{
			get
			{
				return (DataSetReference)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x000265E3 File Offset: 0x000247E3
		// (set) Token: 0x06001046 RID: 4166 RVA: 0x000265F6 File Offset: 0x000247F6
		[XmlElement(typeof(RdlCollection<ParameterValue>))]
		public IList<ParameterValue> ParameterValues
		{
			get
			{
				return (IList<ParameterValue>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x00026605 File Offset: 0x00024805
		// (set) Token: 0x06001048 RID: 4168 RVA: 0x00026613 File Offset: 0x00024813
		[ReportExpressionDefaultValue]
		public ReportExpression ValidationExpression
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00026627 File Offset: 0x00024827
		public ValidValues()
		{
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0002662F File Offset: 0x0002482F
		internal ValidValues(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00026638 File Offset: 0x00024838
		public override void Initialize()
		{
			base.Initialize();
			this.ParameterValues = new RdlCollection<ParameterValue>();
		}

		// Token: 0x020003F5 RID: 1013
		internal class Definition : DefinitionStore<ValidValues, ValidValues.Definition.Properties>
		{
			// Token: 0x060018B7 RID: 6327 RVA: 0x0003BB77 File Offset: 0x00039D77
			private Definition()
			{
			}

			// Token: 0x02000507 RID: 1287
			internal enum Properties
			{
				// Token: 0x040010CA RID: 4298
				DataSetReference,
				// Token: 0x040010CB RID: 4299
				ParameterValues,
				// Token: 0x040010CC RID: 4300
				ValidationExpression
			}
		}
	}
}
