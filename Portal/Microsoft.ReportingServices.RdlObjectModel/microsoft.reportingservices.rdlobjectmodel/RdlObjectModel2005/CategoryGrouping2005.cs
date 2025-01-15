using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000009 RID: 9
	internal class CategoryGrouping2005 : ReportObject
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002765 File Offset: 0x00000965
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002778 File Offset: 0x00000978
		public DynamicSeries2005 DynamicCategories
		{
			get
			{
				return (DynamicSeries2005)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002787 File Offset: 0x00000987
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000279A File Offset: 0x0000099A
		[XmlElement(typeof(RdlCollection<StaticMember2005>))]
		[XmlArrayItem("StaticMember", typeof(StaticMember2005))]
		public IList<StaticMember2005> StaticCategories
		{
			get
			{
				return (IList<StaticMember2005>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000027A9 File Offset: 0x000009A9
		public CategoryGrouping2005()
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000027B1 File Offset: 0x000009B1
		public CategoryGrouping2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000027BA File Offset: 0x000009BA
		public override void Initialize()
		{
			base.Initialize();
			this.StaticCategories = new RdlCollection<StaticMember2005>();
		}

		// Token: 0x020002F2 RID: 754
		internal class Definition : DefinitionStore<CategoryGrouping2005, CategoryGrouping2005.Definition.Properties>
		{
			// Token: 0x060016EE RID: 5870 RVA: 0x0003640A File Offset: 0x0003460A
			private Definition()
			{
			}

			// Token: 0x02000426 RID: 1062
			public enum Properties
			{
				// Token: 0x04000826 RID: 2086
				DynamicCategories,
				// Token: 0x04000827 RID: 2087
				StaticCategories
			}
		}
	}
}
