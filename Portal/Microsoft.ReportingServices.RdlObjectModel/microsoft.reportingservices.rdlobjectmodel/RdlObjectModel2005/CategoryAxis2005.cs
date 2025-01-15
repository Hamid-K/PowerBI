using System;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000004 RID: 4
	internal class CategoryAxis2005 : ReportObject
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002530 File Offset: 0x00000730
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002543 File Offset: 0x00000743
		public Axis2005 Axis
		{
			get
			{
				return (Axis2005)base.PropertyStore.GetObject(0);
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

		// Token: 0x06000050 RID: 80 RVA: 0x00002560 File Offset: 0x00000760
		public CategoryAxis2005()
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002568 File Offset: 0x00000768
		public CategoryAxis2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002571 File Offset: 0x00000771
		public override void Initialize()
		{
			this.Axis = new Axis2005();
		}

		// Token: 0x020002ED RID: 749
		public class Definition : DefinitionStore<CategoryAxis2005, CategoryAxis2005.Definition.Properties>
		{
			// Token: 0x060016E9 RID: 5865 RVA: 0x000363E2 File Offset: 0x000345E2
			private Definition()
			{
			}

			// Token: 0x02000421 RID: 1057
			public enum Properties
			{
				// Token: 0x04000816 RID: 2070
				Axis
			}
		}
	}
}
