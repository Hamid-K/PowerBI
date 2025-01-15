using System;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001E2 RID: 482
	public class PageBreak : ReportObject, IShouldSerialize
	{
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x0002623B File Offset: 0x0002443B
		// (set) Token: 0x06001002 RID: 4098 RVA: 0x00026249 File Offset: 0x00024449
		public BreakLocations BreakLocation
		{
			get
			{
				return (BreakLocations)base.PropertyStore.GetInteger(0);
			}
			set
			{
				base.PropertyStore.SetInteger(0, (int)value);
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x00026258 File Offset: 0x00024458
		// (set) Token: 0x06001004 RID: 4100 RVA: 0x00026266 File Offset: 0x00024466
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Disabled
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x0002627A File Offset: 0x0002447A
		// (set) Token: 0x06001006 RID: 4102 RVA: 0x00026288 File Offset: 0x00024488
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> ResetPageNumber
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0002629C File Offset: 0x0002449C
		public PageBreak()
		{
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x000262A4 File Offset: 0x000244A4
		internal PageBreak(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x000262AD File Offset: 0x000244AD
		bool IShouldSerialize.ShouldSerializeThis()
		{
			return this.BreakLocation > BreakLocations.None;
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x000262B8 File Offset: 0x000244B8
		SerializationMethod IShouldSerialize.ShouldSerializeProperty(string name)
		{
			return SerializationMethod.Auto;
		}

		// Token: 0x020003F0 RID: 1008
		internal class Definition : DefinitionStore<PageBreak, PageBreak.Definition.Properties>
		{
			// Token: 0x060018B2 RID: 6322 RVA: 0x0003BB4F File Offset: 0x00039D4F
			private Definition()
			{
			}

			// Token: 0x02000502 RID: 1282
			internal enum Properties
			{
				// Token: 0x040010B0 RID: 4272
				BreakLocation,
				// Token: 0x040010B1 RID: 4273
				Disabled,
				// Token: 0x040010B2 RID: 4274
				ResetPageNumber
			}
		}
	}
}
