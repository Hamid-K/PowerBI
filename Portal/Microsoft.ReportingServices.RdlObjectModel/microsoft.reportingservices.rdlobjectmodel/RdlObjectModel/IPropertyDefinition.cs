using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001B3 RID: 435
	public interface IPropertyDefinition
	{
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000E68 RID: 3688
		string Name { get; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000E69 RID: 3689
		object Default { get; }

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000E6A RID: 3690
		object Maximum { get; }

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000E6B RID: 3691
		object Minimum { get; }

		// Token: 0x06000E6C RID: 3692
		void Validate(object component, object value);
	}
}
