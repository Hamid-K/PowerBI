using System;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000355 RID: 853
	public class FlattenedPropertyMetadata : PropertyMetadata
	{
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x0005E0F1 File Offset: 0x0005C2F1
		// (set) Token: 0x06001947 RID: 6471 RVA: 0x0005E0F9 File Offset: 0x0005C2F9
		public string ConstructorInitializerName { get; private set; }

		// Token: 0x06001948 RID: 6472 RVA: 0x0005E102 File Offset: 0x0005C302
		public FlattenedPropertyMetadata(Type type, string propertyName, string flattenedPropertyName)
			: base(type, propertyName)
		{
			this.ConstructorInitializerName = flattenedPropertyName;
		}
	}
}
