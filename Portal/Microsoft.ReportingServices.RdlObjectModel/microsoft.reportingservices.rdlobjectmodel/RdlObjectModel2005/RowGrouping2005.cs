using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200002C RID: 44
	internal class RowGrouping2005 : ReportObject
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000035DD File Offset: 0x000017DD
		// (set) Token: 0x06000177 RID: 375 RVA: 0x000035EB File Offset: 0x000017EB
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

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000035FA File Offset: 0x000017FA
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000360D File Offset: 0x0000180D
		public DynamicRows2005 DynamicRows
		{
			get
			{
				return (DynamicRows2005)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000361C File Offset: 0x0000181C
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000362F File Offset: 0x0000182F
		[XmlElement(typeof(RdlCollection<StaticColumn2005>))]
		[XmlArrayItem("StaticRow", typeof(StaticColumn2005))]
		public IList<StaticColumn2005> StaticRows
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

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000363E File Offset: 0x0000183E
		// (set) Token: 0x0600017D RID: 381 RVA: 0x0000364C File Offset: 0x0000184C
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

		// Token: 0x0600017E RID: 382 RVA: 0x0000365B File Offset: 0x0000185B
		public RowGrouping2005()
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00003663 File Offset: 0x00001863
		public RowGrouping2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000366C File Offset: 0x0000186C
		public override void Initialize()
		{
			base.Initialize();
			this.Width = Constants.DefaultZeroSize;
		}

		// Token: 0x02000307 RID: 775
		internal class Definition : DefinitionStore<RowGrouping2005, RowGrouping2005.Definition.Properties>
		{
			// Token: 0x06001703 RID: 5891 RVA: 0x000364B2 File Offset: 0x000346B2
			private Definition()
			{
			}

			// Token: 0x0200043B RID: 1083
			public enum Properties
			{
				// Token: 0x0400088D RID: 2189
				Width,
				// Token: 0x0400088E RID: 2190
				DynamicRows,
				// Token: 0x0400088F RID: 2191
				StaticRows,
				// Token: 0x04000890 RID: 2192
				FixedHeader
			}
		}
	}
}
