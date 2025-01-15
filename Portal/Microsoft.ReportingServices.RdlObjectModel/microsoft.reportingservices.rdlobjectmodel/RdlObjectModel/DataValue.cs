using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000B3 RID: 179
	public class DataValue : ReportObject
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x0001B7D5 File Offset: 0x000199D5
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x0001B7E3 File Offset: 0x000199E3
		[ReportExpressionDefaultValue]
		public ReportExpression Name
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

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0001B7F7 File Offset: 0x000199F7
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x0001B805 File Offset: 0x00019A05
		public ReportExpression Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001B819 File Offset: 0x00019A19
		public DataValue()
		{
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001B821 File Offset: 0x00019A21
		internal DataValue(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000364 RID: 868
		internal class Definition : DefinitionStore<DataValue, DataValue.Definition.Properties>
		{
			// Token: 0x060017F7 RID: 6135 RVA: 0x0003AF3B File Offset: 0x0003913B
			private Definition()
			{
			}

			// Token: 0x02000481 RID: 1153
			internal enum Properties
			{
				// Token: 0x04000ADD RID: 2781
				Name,
				// Token: 0x04000ADE RID: 2782
				Value
			}
		}
	}
}
