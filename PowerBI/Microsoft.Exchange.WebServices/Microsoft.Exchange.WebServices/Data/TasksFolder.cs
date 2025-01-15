using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000193 RID: 403
	[ServiceObjectDefinition("TasksFolder")]
	public class TasksFolder : Folder
	{
		// Token: 0x060011E3 RID: 4579 RVA: 0x0003380E File Offset: 0x0003280E
		public TasksFolder(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00033817 File Offset: 0x00032817
		public new static TasksFolder Bind(ExchangeService service, FolderId id, PropertySet propertySet)
		{
			return service.BindToFolder<TasksFolder>(id, propertySet);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00033821 File Offset: 0x00032821
		public new static TasksFolder Bind(ExchangeService service, FolderId id)
		{
			return TasksFolder.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0003382F File Offset: 0x0003282F
		public new static TasksFolder Bind(ExchangeService service, WellKnownFolderName name, PropertySet propertySet)
		{
			return TasksFolder.Bind(service, new FolderId(name), propertySet);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0003383E File Offset: 0x0003283E
		public new static TasksFolder Bind(ExchangeService service, WellKnownFolderName name)
		{
			return TasksFolder.Bind(service, new FolderId(name), PropertySet.FirstClassProperties);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00033851 File Offset: 0x00032851
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}
	}
}
