using System;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000005 RID: 5
	internal class ValueAxis2005 : ReportObject
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000257E File Offset: 0x0000077E
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002591 File Offset: 0x00000791
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

		// Token: 0x06000055 RID: 85 RVA: 0x000025AE File Offset: 0x000007AE
		public ValueAxis2005()
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000025B6 File Offset: 0x000007B6
		public ValueAxis2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000025BF File Offset: 0x000007BF
		public override void Initialize()
		{
			this.Axis = new Axis2005();
		}

		// Token: 0x020002EE RID: 750
		public class Definition : DefinitionStore<ValueAxis2005, ValueAxis2005.Definition.Properties>
		{
			// Token: 0x060016EA RID: 5866 RVA: 0x000363EA File Offset: 0x000345EA
			private Definition()
			{
			}

			// Token: 0x02000422 RID: 1058
			public enum Properties
			{
				// Token: 0x04000818 RID: 2072
				Axis
			}
		}
	}
}
