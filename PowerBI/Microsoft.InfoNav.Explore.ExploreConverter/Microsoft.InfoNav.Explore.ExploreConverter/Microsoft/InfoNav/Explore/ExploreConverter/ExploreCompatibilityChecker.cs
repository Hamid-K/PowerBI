using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Explore.ExploreConverter.Internal;
using Microsoft.PowerBI.ExplorationContracts;

namespace Microsoft.InfoNav.Explore.ExploreConverter
{
	// Token: 0x02000005 RID: 5
	internal static class ExploreCompatibilityChecker
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		internal static List<ExplorationCompatibilityInfo> VerifyPVDocumentCompatibilityForExploration(PVDocument document)
		{
			PVVisual rootVisual = document.RootVisual;
			bool flag = true;
			List<ExplorationCompatibilityInfo> list = new List<ExplorationCompatibilityInfo>();
			if (rootVisual != null && rootVisual.Visuals != null && rootVisual.Visuals.Count > 0)
			{
				PVDocumentContext context = document.Context;
				List<PVVisual> visuals = rootVisual.Visuals[0].Visuals;
				for (int i = 0; i < visuals.Count; i++)
				{
					List<PVVisual> list2 = visuals[i].Visuals[0].Visuals ?? new List<PVVisual>();
					for (int j = 0; j < list2.Count; j++)
					{
						PVVisual pvvisual = list2[j];
						flag &= ExploreCompatibilityChecker.IsPVDocumentVisualTypeSupported(pvvisual, list) & !ExploreCompatibilityChecker.DrillExists(pvvisual, list);
					}
				}
			}
			return list;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002130 File Offset: 0x00000330
		private static bool IsPVDocumentVisualTypeSupported(PVVisual visual, List<ExplorationCompatibilityInfo> compatibilityInfo)
		{
			string type = visual.Type;
			if (type == "Subview" || type == "SmallMultiple")
			{
				compatibilityInfo.Add(new ExplorationCompatibilityInfo
				{
					Category = ExplorationCompatibilityCategory.UnsupportedVisualType,
					Details = visual.Type
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002180 File Offset: 0x00000380
		private static bool DrillExists(PVVisual visual, List<ExplorationCompatibilityInfo> compatibilityInfo)
		{
			bool flag = false;
			if (visual.DataContext != null)
			{
				List<Bucket> buckets = visual.DataContext.Buckets;
				if (buckets != null)
				{
					for (int i = 0; i < buckets.Count; i++)
					{
						Bucket bucket = buckets[i];
						if (bucket != null && bucket.BucketItems != null && (string.Compare(bucket.Name, "RowHierarchy", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(bucket.Name, "ColumnHierarchy", StringComparison.OrdinalIgnoreCase) == 0) && bucket.Properties != null)
						{
							for (int j = 0; j < bucket.Properties.Count; j++)
							{
								BucketProperty bucketProperty = bucket.Properties[j];
								if (string.Compare(bucketProperty.Name, "EnableDrilling", StringComparison.OrdinalIgnoreCase) == 0 && bucketProperty.Value)
								{
									compatibilityInfo.Add(new ExplorationCompatibilityInfo
									{
										Category = ExplorationCompatibilityCategory.Drill,
										Details = "Matrix"
									});
									flag = true;
								}
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002270 File Offset: 0x00000470
		internal static IEnumerable<Formula> GetFormulas(PVVisual visual)
		{
			DataContext dataContext = visual.DataContext;
			if (dataContext != null)
			{
				if (dataContext.Buckets != null)
				{
					IEnumerable<Formula> enumerable = dataContext.Buckets.SelectMany(delegate(Bucket b)
					{
						IEnumerable<BucketItem> bucketItems = b.BucketItems;
						return (bucketItems ?? Enumerable.Empty<BucketItem>()).Select((BucketItem bi) => bi.Formula);
					});
					foreach (Formula formula in enumerable)
					{
						yield return formula;
					}
					IEnumerator<Formula> enumerator = null;
				}
				if (dataContext.Formula != null)
				{
					yield return dataContext.Formula;
				}
			}
			if (visual.Visuals != null)
			{
				foreach (PVVisual pvvisual in visual.Visuals)
				{
					IEnumerable<Formula> formulas = ExploreCompatibilityChecker.GetFormulas(pvvisual);
					foreach (Formula formula2 in formulas)
					{
						yield return formula2;
					}
					IEnumerator<Formula> enumerator = null;
				}
				List<PVVisual>.Enumerator enumerator2 = default(List<PVVisual>.Enumerator);
			}
			yield break;
			yield break;
		}
	}
}
