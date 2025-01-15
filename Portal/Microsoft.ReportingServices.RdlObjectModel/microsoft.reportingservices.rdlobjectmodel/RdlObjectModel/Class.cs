using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001D2 RID: 466
	public class Class : ReportObject
	{
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x000249BA File Offset: 0x00022BBA
		// (set) Token: 0x06000F30 RID: 3888 RVA: 0x000249CD File Offset: 0x00022BCD
		public string ClassName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x000249DC File Offset: 0x00022BDC
		// (set) Token: 0x06000F32 RID: 3890 RVA: 0x000249EF File Offset: 0x00022BEF
		public string InstanceName
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

		// Token: 0x06000F33 RID: 3891 RVA: 0x000249FE File Offset: 0x00022BFE
		public Class()
		{
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00024A06 File Offset: 0x00022C06
		internal Class(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020003E9 RID: 1001
		internal class Definition : DefinitionStore<Class, Class.Definition.Properties>
		{
			// Token: 0x060018AB RID: 6315 RVA: 0x0003BB17 File Offset: 0x00039D17
			private Definition()
			{
			}

			// Token: 0x020004FB RID: 1275
			internal enum Properties
			{
				// Token: 0x04001087 RID: 4231
				ClassName,
				// Token: 0x04001088 RID: 4232
				InstanceName
			}
		}
	}
}
