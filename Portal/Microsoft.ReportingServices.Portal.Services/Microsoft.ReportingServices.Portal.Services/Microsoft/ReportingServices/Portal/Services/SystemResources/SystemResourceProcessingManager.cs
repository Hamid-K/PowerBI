using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.ReportingServices.Library;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.SystemResources
{
	// Token: 0x02000032 RID: 50
	internal sealed class SystemResourceProcessingManager
	{
		// Token: 0x06000223 RID: 547 RVA: 0x0000EBB0 File Offset: 0x0000CDB0
		internal static SystemResourceProcessingManager GetInstance()
		{
			if (SystemResourceProcessingManager._instance == null)
			{
				object createLock = SystemResourceProcessingManager._createLock;
				lock (createLock)
				{
					if (SystemResourceProcessingManager._instance == null)
					{
						SystemResourceProcessingManager systemResourceProcessingManager = new SystemResourceProcessingManager();
						foreach (Type type in from x in typeof(SystemResourceProcessingManager).Assembly.GetTypes()
							where typeof(ISystemResourceProcessor).IsAssignableFrom(x) && x != typeof(ISystemResourceProcessor)
							select x)
						{
							SystemResourceType type2 = type.GetCustomAttribute<SystemResourceTypeAttribute>().Type;
							ISystemResourceProcessor systemResourceProcessor = (ISystemResourceProcessor)Activator.CreateInstance(type);
							SystemResourceProcessingManager._processors.Add(type2.ToString(), systemResourceProcessor);
						}
						SystemResourceProcessingManager._instance = systemResourceProcessingManager;
					}
				}
			}
			return SystemResourceProcessingManager._instance;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00002C7C File Offset: 0x00000E7C
		private SystemResourceProcessingManager()
		{
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000ECA8 File Offset: 0x0000CEA8
		public IEnumerable<ISystemResourceProcessor> Processors
		{
			get
			{
				return SystemResourceProcessingManager._processors.Values;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		internal void ProcessResource(Microsoft.ReportingServices.Library.SystemResource resource, IEnumerable<ISystemResourceManager> resourceManagers)
		{
			object processLock = SystemResourceProcessingManager._processLock;
			lock (processLock)
			{
				ISystemResourceProcessor systemResourceProcessor;
				if (SystemResourceProcessingManager._processors.TryGetValue(resource.TypeName, out systemResourceProcessor))
				{
					systemResourceProcessor.Process(resource, resourceManagers);
				}
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		internal bool TryLoadItem(Microsoft.ReportingServices.Library.SystemResource resource, string itemName, out byte[] bytes, out string contentType, out string fileName)
		{
			bytes = null;
			contentType = null;
			fileName = null;
			ISystemResourceProcessor systemResourceProcessor;
			return SystemResourceProcessingManager._processors.TryGetValue(resource.TypeName, out systemResourceProcessor) && systemResourceProcessor.TryLoadItem(resource, itemName, out bytes, out contentType, out fileName);
		}

		// Token: 0x040000A4 RID: 164
		private static IDictionary<string, ISystemResourceProcessor> _processors = new Dictionary<string, ISystemResourceProcessor>();

		// Token: 0x040000A5 RID: 165
		private static SystemResourceProcessingManager _instance;

		// Token: 0x040000A6 RID: 166
		private static readonly object _createLock = new object();

		// Token: 0x040000A7 RID: 167
		private static readonly object _processLock = new object();
	}
}
