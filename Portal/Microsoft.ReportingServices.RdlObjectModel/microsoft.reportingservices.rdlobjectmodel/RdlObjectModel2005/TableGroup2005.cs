using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200004E RID: 78
	internal class TableGroup2005 : ReportObject
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000498F File Offset: 0x00002B8F
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x000049A2 File Offset: 0x00002BA2
		public Group Grouping
		{
			get
			{
				return (Group)base.PropertyStore.GetObject(0);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x000049BF File Offset: 0x00002BBF
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x000049D2 File Offset: 0x00002BD2
		[XmlElement(typeof(RdlCollection<SortExpression>))]
		public IList<SortExpression> Sorting
		{
			get
			{
				return (IList<SortExpression>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x000049E1 File Offset: 0x00002BE1
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x000049F4 File Offset: 0x00002BF4
		public Header2005 Header
		{
			get
			{
				return (Header2005)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00004A03 File Offset: 0x00002C03
		// (set) Token: 0x060002AB RID: 683 RVA: 0x00004A16 File Offset: 0x00002C16
		public Footer2005 Footer
		{
			get
			{
				return (Footer2005)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00004A25 File Offset: 0x00002C25
		// (set) Token: 0x060002AD RID: 685 RVA: 0x00004A38 File Offset: 0x00002C38
		public Visibility Visibility
		{
			get
			{
				return (Visibility)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00004A47 File Offset: 0x00002C47
		public TableGroup2005()
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00004A4F File Offset: 0x00002C4F
		public TableGroup2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00004A58 File Offset: 0x00002C58
		public override void Initialize()
		{
			base.Initialize();
			this.Grouping = new Group();
		}

		// Token: 0x0200031C RID: 796
		internal class Definition : DefinitionStore<TableGroup2005, TableGroup2005.Definition.Properties>
		{
			// Token: 0x06001718 RID: 5912 RVA: 0x0003655A File Offset: 0x0003475A
			private Definition()
			{
			}

			// Token: 0x02000450 RID: 1104
			public enum Properties
			{
				// Token: 0x040008F7 RID: 2295
				Grouping,
				// Token: 0x040008F8 RID: 2296
				Sorting,
				// Token: 0x040008F9 RID: 2297
				Header,
				// Token: 0x040008FA RID: 2298
				Footer,
				// Token: 0x040008FB RID: 2299
				Visibility
			}
		}
	}
}
