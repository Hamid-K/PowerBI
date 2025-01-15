using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200016F RID: 367
	public class SortExpression : ReportObject
	{
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x000206BA File Offset: 0x0001E8BA
		// (set) Token: 0x06000BB0 RID: 2992 RVA: 0x000206C8 File Offset: 0x0001E8C8
		public ReportExpression Value
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

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x000206DC File Offset: 0x0001E8DC
		// (set) Token: 0x06000BB2 RID: 2994 RVA: 0x000206EA File Offset: 0x0001E8EA
		[DefaultValue(SortDirections.Ascending)]
		public SortDirections Direction
		{
			get
			{
				return (SortDirections)base.PropertyStore.GetInteger(1);
			}
			set
			{
				base.PropertyStore.SetInteger(1, (int)value);
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000206F9 File Offset: 0x0001E8F9
		public SortExpression()
		{
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00020701 File Offset: 0x0001E901
		internal SortExpression(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020003A0 RID: 928
		internal class Definition : DefinitionStore<SortExpression, SortExpression.Definition.Properties>
		{
			// Token: 0x06001843 RID: 6211 RVA: 0x0003B543 File Offset: 0x00039743
			private Definition()
			{
			}

			// Token: 0x020004B9 RID: 1209
			internal enum Properties
			{
				// Token: 0x04000DF6 RID: 3574
				Value,
				// Token: 0x04000DF7 RID: 3575
				Direction
			}
		}
	}
}
