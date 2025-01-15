using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200008C RID: 140
	[EditorBrowsable(1)]
	public sealed class TaskSuggestionCollection : ComplexPropertyCollection<TaskSuggestion>
	{
		// Token: 0x0600063E RID: 1598 RVA: 0x0001537F File Offset: 0x0001437F
		internal TaskSuggestionCollection()
		{
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00015387 File Offset: 0x00014387
		internal TaskSuggestionCollection(IEnumerable<TaskSuggestion> collection)
		{
			if (collection != null)
			{
				collection.ForEach(new Action<TaskSuggestion>(base.InternalAdd));
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000153A4 File Offset: 0x000143A4
		internal override TaskSuggestion CreateComplexProperty(string xmlElementName)
		{
			return new TaskSuggestion();
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000153AB File Offset: 0x000143AB
		internal override TaskSuggestion CreateDefaultComplexProperty()
		{
			return new TaskSuggestion();
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000153B2 File Offset: 0x000143B2
		internal override string GetCollectionItemXmlElementName(TaskSuggestion complexProperty)
		{
			return "TaskSuggestion";
		}
	}
}
