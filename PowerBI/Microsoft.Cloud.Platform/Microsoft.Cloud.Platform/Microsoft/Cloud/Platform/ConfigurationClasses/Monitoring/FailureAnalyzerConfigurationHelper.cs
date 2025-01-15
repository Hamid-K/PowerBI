using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Cloud.Platform.ConfigurationManagement;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Monitoring
{
	// Token: 0x02000447 RID: 1095
	internal static class FailureAnalyzerConfigurationHelper
	{
		// Token: 0x06002200 RID: 8704 RVA: 0x0007DCDC File Offset: 0x0007BEDC
		internal static void ValidateCollectionSize(ICollection collection, int minCount, int maxCount, string whatTheCollectionContains)
		{
			if (collection.Count < minCount || collection.Count > maxCount)
			{
				throw new CCSValidationException(string.Format(CultureInfo.InvariantCulture, "Illegal # of {0}. Received: {1}. Min: {2}. Max: {3}.", new object[] { whatTheCollectionContains, collection.Count, minCount, maxCount }));
			}
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x0007DD3C File Offset: 0x0007BF3C
		internal static void ValidateUniqueness<T>(IEnumerable<T> collection, string descriptionOfItemInCollection)
		{
			HashSet<T> hashSet = new HashSet<T>();
			foreach (T t in collection)
			{
				if (!hashSet.Add(t))
				{
					throw new CCSValidationException(string.Format(CultureInfo.InvariantCulture, "{0}: {1} appears more than once", new object[] { descriptionOfItemInCollection, t }));
				}
			}
		}
	}
}
