using System;
using System.Collections.Generic;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x0200017A RID: 378
	internal static class LinguisticEntityNameUtil
	{
		// Token: 0x0600073B RID: 1851 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		internal static string CreateAndRegisterEntityName(string entitySetName, string firstWord, HashSet<string> namesInUse)
		{
			string text = StringUtil.MakeUniqueName(LinguisticEntityNameUtil.CreateClsCompliantEntityName(entitySetName, firstWord), namesInUse);
			namesInUse.Add(text);
			return text;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0000C510 File Offset: 0x0000A710
		internal static string CreateAndRegisterEntityName(string parentEntityName, string entityName, string firstWord, HashSet<string> namesInUse)
		{
			string text = StringUtil.MakeUniqueName(LinguisticEntityNameUtil.CreateClsCompliantEntityName(parentEntityName, entityName, firstWord), namesInUse);
			namesInUse.Add(text);
			return text;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0000C535 File Offset: 0x0000A735
		internal static string CreateEntityName(string parentEntityName, string entityName)
		{
			return parentEntityName + "." + entityName;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0000C543 File Offset: 0x0000A743
		internal static string CreateClsCompliantEntityName(string entitySetName, string firstWord)
		{
			entitySetName = LinguisticEntityNameUtil.RemoveEntityContainer(entitySetName);
			return StringUtil.DeriveClsCompliantName(firstWord ?? entitySetName, entitySetName);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0000C55C File Offset: 0x0000A75C
		internal static string CreateClsCompliantEntityName(string parentEntityName, string entityName, string firstWord)
		{
			string text = StringUtil.DeriveClsCompliantName(firstWord ?? entityName, entityName);
			return parentEntityName + "." + text;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0000C584 File Offset: 0x0000A784
		internal static string RemoveEntityContainer(string entitySetName)
		{
			int num = entitySetName.IndexOf('.');
			if (num >= 0)
			{
				entitySetName = entitySetName.Substring(num + 1);
			}
			return entitySetName;
		}
	}
}
