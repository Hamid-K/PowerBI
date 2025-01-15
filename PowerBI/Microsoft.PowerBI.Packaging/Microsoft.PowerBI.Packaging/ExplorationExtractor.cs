using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.PowerBI.Packaging.Project;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000014 RID: 20
	public class ExplorationExtractor : IExplorationExtractor
	{
		// Token: 0x0600005E RID: 94 RVA: 0x000028D8 File Offset: 0x00000AD8
		public ServiceExploration GetServiceDependentMetadata(string exploration)
		{
			if (string.IsNullOrEmpty(exploration))
			{
				throw new ExplorationFormatException("Exploration cannot be null or empty", ExplorationFormatErrorCode.InvalidContract, ExplorationErrorSource.System);
			}
			ExplorationContract explorationContract;
			try
			{
				explorationContract = JsonConvert.DeserializeObject<ExplorationContract>(exploration, V2ExplorationUtils.ExplorationSerializerSettings);
			}
			catch (JsonSerializationException ex)
			{
				throw new ExplorationFormatException("Error deserializing ExplorationContract", ex, ExplorationFormatErrorCode.MalformedExplorationJson, ExplorationErrorSource.User);
			}
			return this.GetServiceDependentMetadata(explorationContract);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002930 File Offset: 0x00000B30
		public ServiceExploration GetServiceDependentMetadata(ExplorationContract explorationContract)
		{
			if (explorationContract == null)
			{
				throw new ExplorationFormatException("Exploration Contract cannot be null", ExplorationFormatErrorCode.InvalidContract, ExplorationErrorSource.System);
			}
			V2ExplorationUtils.ValidateExplorationLimits(explorationContract);
			V2ExplorationUtils.ValidateUniqueExplorationObjectIdentities(explorationContract);
			List<SectionReferenceIdentifier> list = new List<SectionReferenceIdentifier>();
			List<Pod> list2 = new List<Pod>();
			foreach (Page page in explorationContract.Pages.PagesList)
			{
				SectionReferenceIdentifier sectionReferenceIdentifier = new SectionReferenceIdentifier
				{
					ObjectName = page.Content["name"].ToString(),
					VisualContainers = this.GetVisualObjectIdentifiers(page.VisualContainers)
				};
				list.Add(sectionReferenceIdentifier);
				JToken jtoken = page.Content["pageBinding"];
				if (!V2ExplorationUtils.IsNullJObject(jtoken))
				{
					list2.Add(this.MapPageBindingsToPods(jtoken, sectionReferenceIdentifier.ObjectName));
				}
			}
			return new ServiceExploration
			{
				Sections = list,
				Pods = list2,
				ResourcePackages = V2ExplorationUtils.GetExplorationResourcePackages(explorationContract),
				PublicCustomVisuals = this.GetPublicCustomVisuals(explorationContract)
			};
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002A3C File Offset: 0x00000C3C
		public JToken GetPublicCustomVisuals(ExplorationContract explorationContract)
		{
			return explorationContract.Report.Content["publicCustomVisuals"];
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002A54 File Offset: 0x00000C54
		private List<ArtifactIdentifier> GetVisualObjectIdentifiers(NonNulls<ExplorationArtifact> visualContainers)
		{
			List<ArtifactIdentifier> list = new List<ArtifactIdentifier>();
			if (visualContainers == null)
			{
				return list;
			}
			foreach (ExplorationArtifact explorationArtifact in visualContainers.Where((ExplorationArtifact visual) => !visual.FilePath.EndsWith(ExplorationSerializer.MobileVisualFileName, StringComparison.OrdinalIgnoreCase)))
			{
				list.Add(new ArtifactIdentifier
				{
					ObjectName = explorationArtifact.Content["name"].ToString()
				});
			}
			return list;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002AEC File Offset: 0x00000CEC
		private Pod MapPageBindingsToPods(JToken pageBinding, string sectionObjectName)
		{
			Pod pod = new Pod();
			pod.BoundSection = sectionObjectName;
			JToken jtoken = pageBinding["name"];
			pod.Name = ((jtoken != null) ? jtoken.ToString() : null);
			JToken jtoken2 = pageBinding["parameters"];
			pod.Parameters = ((jtoken2 != null) ? jtoken2.ToString() : null);
			pod.ReferenceScope = V2ExplorationUtils.ParseEnumOrDefault<PodReferenceScope>(pageBinding["referenceScope"]);
			pod.Type = V2ExplorationUtils.ParseEnumOrDefault<PodType>(pageBinding["type"]);
			Pod pod2 = pod;
			if (pod2.Type == 1 && pod2.Parameters == null)
			{
				throw new ExplorationFormatException("DrillThrough pods cannot contain null parameters.", ExplorationFormatErrorCode.MissingRequiredProperty, ExplorationErrorSource.User);
			}
			return pod2;
		}
	}
}
