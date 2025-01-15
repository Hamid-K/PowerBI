using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200001D RID: 29
	internal static class DistinctDataSourceDiscoveryVisitor
	{
		// Token: 0x06000121 RID: 289 RVA: 0x000065E8 File Offset: 0x000047E8
		public static HashSet<MashupDiscovery> FindDataSources(IEnumerable<IDocument> documents, MashupDiscoveryOptions options, MashupPartitionCoordinateType coordinateType, IEvaluationConstants evaluationConstants = null)
		{
			DataSourceDiscoveryVisitor dataSourceDiscoveryVisitor = new DataSourceDiscoveryVisitor(options, coordinateType, evaluationConstants);
			dataSourceDiscoveryVisitor.VisitDocuments(documents);
			HashSet<MashupDiscovery> hashSet = new HashSet<MashupDiscovery>(dataSourceDiscoveryVisitor.Discoveries.Values);
			if (dataSourceDiscoveryVisitor.HasUnboundNativeQuery && !dataSourceDiscoveryVisitor.IgnoreNativeQueries)
			{
				hashSet = new HashSet<MashupDiscovery>(hashSet.Select((MashupDiscovery d) => d.SetUnknownQuery()));
			}
			foreach (IDocument document in documents)
			{
				ISectionDocument sectionDocument = document as ISectionDocument;
				if (sectionDocument != null)
				{
					try
					{
						IPackageSectionConfig sectionMetadata = sectionDocument.Section.GetSectionMetadata(null);
						if (sectionMetadata.Dependencies != null)
						{
							foreach (KeyValuePair<string, VersionRange> keyValuePair in sectionMetadata.Dependencies)
							{
								if ((options & MashupDiscoveryOptions.ReportDependencies) == MashupDiscoveryOptions.ReportDependencies)
								{
									MashupDiscoveryKind mashupDiscoveryKind = (dataSourceDiscoveryVisitor.Modules.Contains(keyValuePair.Key) ? MashupDiscoveryKind.ResolvedDependency : MashupDiscoveryKind.UnresolvedDependency);
									hashSet.Add(MashupDiscovery.NewDependency(mashupDiscoveryKind, keyValuePair.Key, keyValuePair.Value.ToString()));
								}
								IModule module;
								if (MashupEngines.Version1.TryGetModule(keyValuePair.Key, out module) && module.DynamicModuleDataSource != null && module.DynamicModuleDataSource.IsSingleton)
								{
									hashSet.Add(new MashupDiscovery(MashupDiscoveryKind.DataSource, module.Name, new DataSourceReference(module.DynamicModuleDataSource.Kind, module.DynamicModuleDataSource.Kind), MashupPartitionCoordinate.Empty, null, true, null));
									hashSet.RemoveWhere((MashupDiscovery d) => d.Kind == MashupDiscoveryKind.UnknownFunction);
								}
							}
						}
					}
					catch (InvalidOperationException)
					{
					}
				}
			}
			return hashSet;
		}
	}
}
