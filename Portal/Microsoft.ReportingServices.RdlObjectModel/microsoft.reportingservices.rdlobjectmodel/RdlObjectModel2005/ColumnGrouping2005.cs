using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000029 RID: 41
	internal class ColumnGrouping2005 : ReportObject
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00003403 File Offset: 0x00001603
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00003411 File Offset: 0x00001611
		public ReportSize Height
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

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00003420 File Offset: 0x00001620
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00003433 File Offset: 0x00001633
		public DynamicColumns2005 DynamicColumns
		{
			get
			{
				return (DynamicColumns2005)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00003442 File Offset: 0x00001642
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00003455 File Offset: 0x00001655
		[XmlElement(typeof(RdlCollection<StaticColumn2005>))]
		[XmlArrayItem("StaticColumn", typeof(StaticColumn2005))]
		public IList<StaticColumn2005> StaticColumns
		{
			get
			{
				return (IList<StaticColumn2005>)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00003464 File Offset: 0x00001664
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00003472 File Offset: 0x00001672
		[DefaultValue(false)]
		public bool FixedHeader
		{
			get
			{
				return base.PropertyStore.GetBoolean(3);
			}
			set
			{
				base.PropertyStore.SetBoolean(3, value);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00003481 File Offset: 0x00001681
		public ColumnGrouping2005()
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00003489 File Offset: 0x00001689
		public ColumnGrouping2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00003492 File Offset: 0x00001692
		public override void Initialize()
		{
			base.Initialize();
			this.Height = Constants.DefaultZeroSize;
			this.StaticColumns = new RdlCollection<StaticColumn2005>();
		}

		// Token: 0x02000304 RID: 772
		internal class Definition : DefinitionStore<ColumnGrouping2005, ColumnGrouping2005.Definition.Properties>
		{
			// Token: 0x06001700 RID: 5888 RVA: 0x0003649A File Offset: 0x0003469A
			private Definition()
			{
			}

			// Token: 0x02000438 RID: 1080
			public enum Properties
			{
				// Token: 0x04000880 RID: 2176
				Height,
				// Token: 0x04000881 RID: 2177
				DynamicColumns,
				// Token: 0x04000882 RID: 2178
				StaticColumns,
				// Token: 0x04000883 RID: 2179
				FixedHeader
			}
		}
	}
}
