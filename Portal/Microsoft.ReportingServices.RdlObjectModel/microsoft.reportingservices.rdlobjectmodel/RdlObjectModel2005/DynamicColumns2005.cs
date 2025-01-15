using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200002A RID: 42
	internal class DynamicColumns2005 : ReportObject
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000034B0 File Offset: 0x000016B0
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000034C3 File Offset: 0x000016C3
		public Grouping2005 Grouping
		{
			get
			{
				return (Grouping2005)base.PropertyStore.GetObject(0);
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

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000034E0 File Offset: 0x000016E0
		// (set) Token: 0x06000167 RID: 359 RVA: 0x000034F3 File Offset: 0x000016F3
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

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00003502 File Offset: 0x00001702
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00003515 File Offset: 0x00001715
		public Subtotal2005 Subtotal
		{
			get
			{
				return (Subtotal2005)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00003524 File Offset: 0x00001724
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00003537 File Offset: 0x00001737
		[XmlElement(typeof(RdlCollection<ReportItem>))]
		public IList<ReportItem> ReportItems
		{
			get
			{
				return (IList<ReportItem>)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00003546 File Offset: 0x00001746
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00003559 File Offset: 0x00001759
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

		// Token: 0x0600016E RID: 366 RVA: 0x00003568 File Offset: 0x00001768
		public DynamicColumns2005()
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00003570 File Offset: 0x00001770
		public DynamicColumns2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00003579 File Offset: 0x00001779
		public override void Initialize()
		{
			base.Initialize();
			this.Grouping = new Grouping2005();
			this.ReportItems = new RdlCollection<ReportItem>();
		}

		// Token: 0x02000305 RID: 773
		internal class Definition : DefinitionStore<DynamicColumns2005, DynamicColumns2005.Definition.Properties>
		{
			// Token: 0x06001701 RID: 5889 RVA: 0x000364A2 File Offset: 0x000346A2
			private Definition()
			{
			}

			// Token: 0x02000439 RID: 1081
			public enum Properties
			{
				// Token: 0x04000885 RID: 2181
				Grouping,
				// Token: 0x04000886 RID: 2182
				Sorting,
				// Token: 0x04000887 RID: 2183
				Subtotal,
				// Token: 0x04000888 RID: 2184
				ReportItems,
				// Token: 0x04000889 RID: 2185
				Visibility
			}
		}
	}
}
