using System;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Eventing
{
	// Token: 0x0200044B RID: 1099
	[Serializable]
	public sealed class EnabledEventTypesConfiguration : ConfigurationClass
	{
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x0007DF4C File Offset: 0x0007C14C
		// (set) Token: 0x06002236 RID: 8758 RVA: 0x0007DF54 File Offset: 0x0007C154
		[ConfigurationProperty]
		public ConfigurationCollection<EventPurpose> EnabledEventTypesCollection { get; set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x0007DF60 File Offset: 0x0007C160
		[NonConfigurationProperty]
		public EventPurpose EnabledEventTypes
		{
			get
			{
				EventPurpose eventPurpose = EventPurpose.None;
				foreach (EventPurpose eventPurpose2 in this.EnabledEventTypesCollection)
				{
					eventPurpose |= eventPurpose2;
				}
				return eventPurpose;
			}
		}
	}
}
