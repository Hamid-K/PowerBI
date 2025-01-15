using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using Microsoft.AnalysisServices.Azure.Gateway;
using Microsoft.Cloud.ModelCommon.Model;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.ExploreToDataRouter
{
	// Token: 0x0200014B RID: 331
	internal sealed class ExploreToDataExternalRouter : Router
	{
		// Token: 0x0600118A RID: 4490 RVA: 0x00047A84 File Offset: 0x00045C84
		public ExploreToDataExternalRouter(IEventsKitFactory eventsKitFactory, IClusterManagementService clusterManagementService, BIAzure.ServiceType serviceType, BIAzure.EndpointType resolvedEndoint, BIAzure.EndpointType targetEndpoint)
			: base(true, new RoundRobinRetryPolicy(new Type[]
			{
				typeof(EndpointNotFoundException),
				typeof(CommunicationException),
				typeof(CommunicationFrameworkException)
			}), eventsKitFactory)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IClusterManagementService>(clusterManagementService, "clusterManagementService");
			this.clusterManagementService = clusterManagementService;
			this.serviceType = serviceType;
			this.resolvedEndoint = resolvedEndoint;
			this.targetEndpoint = targetEndpoint;
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00047AF4 File Offset: 0x00045CF4
		public override IAsyncResult BeginGetEndpoints(object[] keys, AsyncCallback callback, object state)
		{
			string[] array;
			if (keys == null || keys.Length == 0)
			{
				array = new string[0];
			}
			else
			{
				array = new string[keys.Length];
				for (int i = 0; i < keys.Length; i++)
				{
					XmlObjectSerializer xmlObjectSerializer = new DataContractSerializer(keys[i].GetType());
					MemoryStream memoryStream = new MemoryStream();
					XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(memoryStream, Encoding.UTF8);
					xmlObjectSerializer.WriteObject(xmlDictionaryWriter, keys[i]);
					xmlDictionaryWriter.Flush();
					memoryStream.Flush();
					memoryStream.Seek(0L, SeekOrigin.Begin);
					array[i] = Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			return this.clusterManagementService.BeginResolvePowerBIService(this.serviceType.ToString(), this.resolvedEndoint.ToString(), this.targetEndpoint.ToString(), new string[0], array, callback, state);
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00047BCC File Offset: 0x00045DCC
		public override IEnumerable<Uri> EndGetEndpoints(IAsyncResult result)
		{
			return (from it in this.clusterManagementService.EndResolvePowerBIService(result)
				select it.Uri).Materialize<Uri>();
		}

		// Token: 0x0400040C RID: 1036
		private readonly IClusterManagementService clusterManagementService;

		// Token: 0x0400040D RID: 1037
		private readonly BIAzure.ServiceType serviceType;

		// Token: 0x0400040E RID: 1038
		private readonly BIAzure.EndpointType resolvedEndoint;

		// Token: 0x0400040F RID: 1039
		private readonly BIAzure.EndpointType targetEndpoint;
	}
}
