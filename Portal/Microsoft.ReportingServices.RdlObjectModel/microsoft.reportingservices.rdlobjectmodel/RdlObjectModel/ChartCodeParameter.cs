using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000A1 RID: 161
	public class ChartCodeParameter : ReportObject, INamedObject
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001AB95 File Offset: 0x00018D95
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x0001ABA8 File Offset: 0x00018DA8
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

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001ABB7 File Offset: 0x00018DB7
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0001ABC5 File Offset: 0x00018DC5
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

		// Token: 0x06000711 RID: 1809 RVA: 0x0001ABD9 File Offset: 0x00018DD9
		public ChartCodeParameter()
		{
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001ABE1 File Offset: 0x00018DE1
		internal ChartCodeParameter(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000357 RID: 855
		internal class Definition : DefinitionStore<ChartCodeParameter, ChartCodeParameter.Definition.Properties>
		{
			// Token: 0x060017DA RID: 6106 RVA: 0x0003ABF3 File Offset: 0x00038DF3
			private Definition()
			{
			}

			// Token: 0x02000476 RID: 1142
			internal enum Properties
			{
				// Token: 0x04000A9A RID: 2714
				Name,
				// Token: 0x04000A9B RID: 2715
				Value,
				// Token: 0x04000A9C RID: 2716
				PropertyCount
			}
		}
	}
}
