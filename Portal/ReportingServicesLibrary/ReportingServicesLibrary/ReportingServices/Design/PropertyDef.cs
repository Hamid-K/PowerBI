using System;

namespace Microsoft.ReportingServices.Design
{
	// Token: 0x02000389 RID: 905
	public abstract class PropertyDef : DefinitionBase
	{
		// Token: 0x06001DFF RID: 7679 RVA: 0x0007AD6F File Offset: 0x00078F6F
		public PropertyDef(string name)
		{
			this.Name = name;
		}

		// Token: 0x04000CB4 RID: 3252
		public readonly string Name;
	}
}
