using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000099 RID: 153
	public class ChartFormulaParameter : ReportObject, INamedObject
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0001A2BD File Offset: 0x000184BD
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x0001A2D0 File Offset: 0x000184D0
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

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001A2DF File Offset: 0x000184DF
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0001A2ED File Offset: 0x000184ED
		[ReportExpressionDefaultValue]
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

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001A301 File Offset: 0x00018501
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x0001A314 File Offset: 0x00018514
		[DefaultValue("")]
		public string Source
		{
			get
			{
				return (string)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001A323 File Offset: 0x00018523
		public ChartFormulaParameter()
		{
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001A32B File Offset: 0x0001852B
		internal ChartFormulaParameter(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0200034F RID: 847
		internal class Definition : DefinitionStore<ChartFormulaParameter, ChartFormulaParameter.Definition.Properties>
		{
			// Token: 0x060017D2 RID: 6098 RVA: 0x0003ABB3 File Offset: 0x00038DB3
			private Definition()
			{
			}

			// Token: 0x0200046E RID: 1134
			internal enum Properties
			{
				// Token: 0x04000A4E RID: 2638
				Name,
				// Token: 0x04000A4F RID: 2639
				Value,
				// Token: 0x04000A50 RID: 2640
				Source,
				// Token: 0x04000A51 RID: 2641
				PropertyCount
			}
		}
	}
}
