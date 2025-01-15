using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000031 RID: 49
	internal class MatrixCell2005 : ReportObject
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000038B1 File Offset: 0x00001AB1
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x000038C4 File Offset: 0x00001AC4
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

		// Token: 0x060001A4 RID: 420 RVA: 0x000038D3 File Offset: 0x00001AD3
		public MatrixCell2005()
		{
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000038DB File Offset: 0x00001ADB
		public MatrixCell2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000038E4 File Offset: 0x00001AE4
		public override void Initialize()
		{
			base.Initialize();
			this.ReportItems = new RdlCollection<ReportItem>();
		}

		// Token: 0x0200030B RID: 779
		internal class Definition : DefinitionStore<MatrixCell2005, MatrixCell2005.Definition.Properties>
		{
			// Token: 0x06001707 RID: 5895 RVA: 0x000364D2 File Offset: 0x000346D2
			private Definition()
			{
			}

			// Token: 0x0200043F RID: 1087
			public enum Properties
			{
				// Token: 0x040008A1 RID: 2209
				ReportItems
			}
		}
	}
}
