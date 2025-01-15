using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000028 RID: 40
	internal class Corner2005 : ReportObject
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000033BD File Offset: 0x000015BD
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000033D0 File Offset: 0x000015D0
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

		// Token: 0x06000156 RID: 342 RVA: 0x000033DF File Offset: 0x000015DF
		public Corner2005()
		{
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000033E7 File Offset: 0x000015E7
		public Corner2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000033F0 File Offset: 0x000015F0
		public override void Initialize()
		{
			base.Initialize();
			this.ReportItems = new RdlCollection<ReportItem>();
		}

		// Token: 0x02000303 RID: 771
		internal class Definition : DefinitionStore<Corner2005, Corner2005.Definition.Properties>
		{
			// Token: 0x060016FF RID: 5887 RVA: 0x00036492 File Offset: 0x00034692
			private Definition()
			{
			}

			// Token: 0x02000437 RID: 1079
			public enum Properties
			{
				// Token: 0x0400087E RID: 2174
				ReportItems
			}
		}
	}
}
