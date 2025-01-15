using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001F9 RID: 505
	public class TablixCorner : ReportObject
	{
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x000278FA File Offset: 0x00025AFA
		// (set) Token: 0x060010FF RID: 4351 RVA: 0x0002790D File Offset: 0x00025B0D
		[XmlElement(typeof(RdlCollection<IList<TablixCornerCell>>))]
		[XmlArrayItem("TablixCornerRow", typeof(TablixCornerRow))]
		public IList<IList<TablixCornerCell>> TablixCornerRows
		{
			get
			{
				return (IList<IList<TablixCornerCell>>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0002791C File Offset: 0x00025B1C
		public TablixCorner()
		{
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00027924 File Offset: 0x00025B24
		internal TablixCorner(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0002792D File Offset: 0x00025B2D
		public override void Initialize()
		{
			base.Initialize();
			this.TablixCornerRows = new RdlCollection<IList<TablixCornerCell>>();
		}

		// Token: 0x020003FE RID: 1022
		internal class Definition : DefinitionStore<TablixCorner, TablixCorner.Definition.Properties>
		{
			// Token: 0x060018C7 RID: 6343 RVA: 0x0003BC4C File Offset: 0x00039E4C
			private Definition()
			{
			}

			// Token: 0x0200050F RID: 1295
			internal enum Properties
			{
				// Token: 0x04001105 RID: 4357
				TablixCornerRows
			}
		}
	}
}
