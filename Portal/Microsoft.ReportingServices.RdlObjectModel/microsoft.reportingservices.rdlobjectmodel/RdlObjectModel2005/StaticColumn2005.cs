using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200002B RID: 43
	internal class StaticColumn2005 : ReportObject
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00003597 File Offset: 0x00001797
		// (set) Token: 0x06000172 RID: 370 RVA: 0x000035AA File Offset: 0x000017AA
		[XmlElement(typeof(RdlCollection<ReportItem>))]
		public IList<ReportItem> ReportItems
		{
			get
			{
				return (IList<ReportItem>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000035B9 File Offset: 0x000017B9
		public StaticColumn2005()
		{
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000035C1 File Offset: 0x000017C1
		public StaticColumn2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000035CA File Offset: 0x000017CA
		public override void Initialize()
		{
			base.Initialize();
			this.ReportItems = new RdlCollection<ReportItem>();
		}

		// Token: 0x02000306 RID: 774
		internal class Definition : DefinitionStore<StaticColumn2005, StaticColumn2005.Definition.Properties>
		{
			// Token: 0x06001702 RID: 5890 RVA: 0x000364AA File Offset: 0x000346AA
			private Definition()
			{
			}

			// Token: 0x0200043A RID: 1082
			public enum Properties
			{
				// Token: 0x0400088B RID: 2187
				ReportItems
			}
		}
	}
}
