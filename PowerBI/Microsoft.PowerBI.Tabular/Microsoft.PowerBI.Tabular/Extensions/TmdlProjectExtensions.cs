using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Tmdl;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001CE RID: 462
	internal static class TmdlProjectExtensions
	{
		// Token: 0x06001BF5 RID: 7157 RVA: 0x000C38E4 File Offset: 0x000C1AE4
		public static IEnumerable<TmdlObject> CollectObjects(this TmdlProject project, ObjectType type)
		{
			Func<TmdlObject, bool> <>9__0;
			foreach (TmdlDocument tmdlDocument in project.Documents)
			{
				IEnumerable<TmdlObject> objects = tmdlDocument.Objects;
				Func<TmdlObject, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (TmdlObject o) => o.ObjectType == type);
				}
				foreach (TmdlObject tmdlObject in objects.Where(func))
				{
					yield return tmdlObject;
				}
				IEnumerator<TmdlObject> enumerator2 = null;
			}
			IEnumerator<TmdlDocument> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x000C38FB File Offset: 0x000C1AFB
		public static IEnumerable<TmdlObject> CollectObjects(this TmdlProject project, Func<TmdlObject, bool> predicate)
		{
			foreach (TmdlDocument tmdlDocument in project.Documents)
			{
				foreach (TmdlObject tmdlObject in tmdlDocument.Objects.Where(predicate))
				{
					yield return tmdlObject;
				}
				IEnumerator<TmdlObject> enumerator2 = null;
			}
			IEnumerator<TmdlDocument> enumerator = null;
			yield break;
			yield break;
		}
	}
}
