using System;
using System.ComponentModel;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200004A RID: 74
	internal class TableColumn2005 : ReportObject
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000479C File Offset: 0x0000299C
		// (set) Token: 0x06000283 RID: 643 RVA: 0x000047AA File Offset: 0x000029AA
		public ReportSize Width
		{
			get
			{
				return base.PropertyStore.GetSize(0);
			}
			set
			{
				base.PropertyStore.SetSize(0, value);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000284 RID: 644 RVA: 0x000047B9 File Offset: 0x000029B9
		// (set) Token: 0x06000285 RID: 645 RVA: 0x000047CC File Offset: 0x000029CC
		public Visibility Visibility
		{
			get
			{
				return (Visibility)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000286 RID: 646 RVA: 0x000047DB File Offset: 0x000029DB
		// (set) Token: 0x06000287 RID: 647 RVA: 0x000047E9 File Offset: 0x000029E9
		[DefaultValue(false)]
		public bool FixedHeader
		{
			get
			{
				return base.PropertyStore.GetBoolean(2);
			}
			set
			{
				base.PropertyStore.SetBoolean(2, value);
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000047F8 File Offset: 0x000029F8
		public TableColumn2005()
		{
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00004800 File Offset: 0x00002A00
		public TableColumn2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00004809 File Offset: 0x00002A09
		public override void Initialize()
		{
			base.Initialize();
			this.Width = Constants.DefaultZeroSize;
		}

		// Token: 0x02000318 RID: 792
		internal class Definition : DefinitionStore<TableColumn2005, TableColumn2005.Definition.Properties>
		{
			// Token: 0x06001714 RID: 5908 RVA: 0x0003653A File Offset: 0x0003473A
			private Definition()
			{
			}

			// Token: 0x0200044C RID: 1100
			public enum Properties
			{
				// Token: 0x040008E8 RID: 2280
				Width,
				// Token: 0x040008E9 RID: 2281
				Visibility,
				// Token: 0x040008EA RID: 2282
				FixedHeader
			}
		}
	}
}
