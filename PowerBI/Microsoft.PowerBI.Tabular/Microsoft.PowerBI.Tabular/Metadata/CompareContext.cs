using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001DE RID: 478
	internal sealed class CompareContext
	{
		// Token: 0x06001C31 RID: 7217 RVA: 0x000C46E1 File Offset: 0x000C28E1
		public CompareContext(ObjectChangelist changelist)
		{
			this.changelist = changelist;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x000C46F0 File Offset: 0x000C28F0
		public void RegisterPropertyChange(MetadataObject obj, string propertyName, Type type, PropertyFlags propertyFlags, object origValue, object newValue)
		{
			this.RegisterPropertyChange(obj, propertyName, type, propertyFlags, origValue, null, newValue, null);
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x000C4710 File Offset: 0x000C2910
		public void RegisterPropertyChange(MetadataObject obj, string propertyName, Type type, PropertyFlags propertyFlags, object origValue, ObjectPath origPath, object newValue, ObjectPath newPath)
		{
			this.changelist.RegisterPropertyChange(new PropertyChangeEntry
			{
				Object = obj,
				PropertyName = propertyName,
				PropertyType = type,
				Flags = propertyFlags,
				NewValue = newValue,
				OriginalValue = origValue,
				NewPath = newPath,
				OriginalPath = origPath
			});
		}

		// Token: 0x040005D8 RID: 1496
		private ObjectChangelist changelist;
	}
}
