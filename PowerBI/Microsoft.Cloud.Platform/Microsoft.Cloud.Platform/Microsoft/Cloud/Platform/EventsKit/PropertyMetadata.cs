using System;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200036D RID: 877
	public class PropertyMetadata : VariableMetadata
	{
		// Token: 0x06001A15 RID: 6677 RVA: 0x000606AC File Offset: 0x0005E8AC
		public PropertyMetadata(Type type, string name)
			: base(type, NameUtils.CapitalizeNameFirstLetter(name))
		{
		}
	}
}
