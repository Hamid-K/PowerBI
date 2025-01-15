using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200002D RID: 45
	internal class DynamicRows2005 : ReportObject
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000367F File Offset: 0x0000187F
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00003692 File Offset: 0x00001892
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

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000036AF File Offset: 0x000018AF
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000036C2 File Offset: 0x000018C2
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

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000036D1 File Offset: 0x000018D1
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000036E4 File Offset: 0x000018E4
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000036F3 File Offset: 0x000018F3
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00003706 File Offset: 0x00001906
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

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00003715 File Offset: 0x00001915
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00003728 File Offset: 0x00001928
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

		// Token: 0x0600018B RID: 395 RVA: 0x00003737 File Offset: 0x00001937
		public DynamicRows2005()
		{
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000373F File Offset: 0x0000193F
		public DynamicRows2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00003748 File Offset: 0x00001948
		public override void Initialize()
		{
			base.Initialize();
			this.Grouping = new Grouping2005();
			this.ReportItems = new RdlCollection<ReportItem>();
		}

		// Token: 0x02000308 RID: 776
		internal class Definition : DefinitionStore<DynamicRows2005, DynamicRows2005.Definition.Properties>
		{
			// Token: 0x06001704 RID: 5892 RVA: 0x000364BA File Offset: 0x000346BA
			private Definition()
			{
			}

			// Token: 0x0200043C RID: 1084
			public enum Properties
			{
				// Token: 0x04000892 RID: 2194
				Grouping,
				// Token: 0x04000893 RID: 2195
				Sorting,
				// Token: 0x04000894 RID: 2196
				Subtotal,
				// Token: 0x04000895 RID: 2197
				ReportItems,
				// Token: 0x04000896 RID: 2198
				Visibility
			}
		}
	}
}
