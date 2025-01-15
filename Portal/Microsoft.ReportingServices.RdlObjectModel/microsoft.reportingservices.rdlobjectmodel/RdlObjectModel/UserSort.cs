using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000204 RID: 516
	public class UserSort : ReportObject
	{
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x00027E78 File Offset: 0x00026078
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x00027E86 File Offset: 0x00026086
		public ReportExpression SortExpression
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x00027E9A File Offset: 0x0002609A
		// (set) Token: 0x0600115E RID: 4446 RVA: 0x00027EAD File Offset: 0x000260AD
		[DefaultValue("")]
		public string SortExpressionScope
		{
			get
			{
				return (string)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x00027EBC File Offset: 0x000260BC
		// (set) Token: 0x06001160 RID: 4448 RVA: 0x00027ECF File Offset: 0x000260CF
		[DefaultValue("")]
		public string SortTarget
		{
			get
			{
				return (string)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00027EDE File Offset: 0x000260DE
		public UserSort()
		{
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00027EE6 File Offset: 0x000260E6
		internal UserSort(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0200040A RID: 1034
		internal class Definition : DefinitionStore<UserSort, UserSort.Definition.Properties>
		{
			// Token: 0x060018E3 RID: 6371 RVA: 0x0003BF8B File Offset: 0x0003A18B
			private Definition()
			{
			}

			// Token: 0x02000519 RID: 1305
			internal enum Properties
			{
				// Token: 0x0400112B RID: 4395
				SortExpression,
				// Token: 0x0400112C RID: 4396
				SortExpressionScope,
				// Token: 0x0400112D RID: 4397
				SortTarget
			}
		}
	}
}
