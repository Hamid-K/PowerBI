using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001DF RID: 479
	public class Drillthrough : ReportObject
	{
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x000260EB File Offset: 0x000242EB
		// (set) Token: 0x06000FEC RID: 4076 RVA: 0x000260F9 File Offset: 0x000242F9
		public ReportExpression ReportName
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x0002610D File Offset: 0x0002430D
		// (set) Token: 0x06000FEE RID: 4078 RVA: 0x00026120 File Offset: 0x00024320
		[XmlElement(typeof(RdlCollection<Parameter>))]
		public IList<Parameter> Parameters
		{
			get
			{
				return (IList<Parameter>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0002612F File Offset: 0x0002432F
		public Drillthrough()
		{
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00026137 File Offset: 0x00024337
		internal Drillthrough(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00026140 File Offset: 0x00024340
		public override void Initialize()
		{
			base.Initialize();
			this.Parameters = new RdlCollection<Parameter>();
		}

		// Token: 0x020003ED RID: 1005
		internal class Definition : DefinitionStore<Drillthrough, Drillthrough.Definition.Properties>
		{
			// Token: 0x060018AF RID: 6319 RVA: 0x0003BB37 File Offset: 0x00039D37
			private Definition()
			{
			}

			// Token: 0x020004FF RID: 1279
			internal enum Properties
			{
				// Token: 0x040010A6 RID: 4262
				ReportName,
				// Token: 0x040010A7 RID: 4263
				Parameters
			}
		}
	}
}
