using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004BF RID: 1215
	internal class ExpensiveOSpaceLoader
	{
		// Token: 0x06003C11 RID: 15377 RVA: 0x000C7138 File Offset: 0x000C5338
		public virtual Dictionary<string, EdmType> LoadTypesExpensiveWay(Assembly assembly)
		{
			KnownAssembliesSet knownAssembliesSet = new KnownAssembliesSet();
			Dictionary<string, EdmType> dictionary;
			List<EdmItemError> list;
			AssemblyCache.LoadAssembly(assembly, false, knownAssembliesSet, out dictionary, out list);
			if (list.Count != 0)
			{
				throw EntityUtil.InvalidSchemaEncountered(Helper.CombineErrorMessage(list));
			}
			return dictionary;
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x000C716C File Offset: 0x000C536C
		public virtual AssociationType GetRelationshipTypeExpensiveWay(Type entityClrType, string relationshipName)
		{
			Dictionary<string, EdmType> dictionary = this.LoadTypesExpensiveWay(entityClrType.Assembly());
			EdmType edmType;
			if (dictionary != null && dictionary.TryGetValue(relationshipName, out edmType) && Helper.IsRelationshipType(edmType))
			{
				return (AssociationType)edmType;
			}
			return null;
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x000C71A4 File Offset: 0x000C53A4
		public virtual IEnumerable<AssociationType> GetAllRelationshipTypesExpensiveWay(Assembly assembly)
		{
			Dictionary<string, EdmType> dictionary = this.LoadTypesExpensiveWay(assembly);
			if (dictionary != null)
			{
				foreach (EdmType edmType in dictionary.Values)
				{
					if (Helper.IsAssociationType(edmType))
					{
						yield return (AssociationType)edmType;
					}
				}
				Dictionary<string, EdmType>.ValueCollection.Enumerator enumerator = default(Dictionary<string, EdmType>.ValueCollection.Enumerator);
			}
			yield break;
			yield break;
		}
	}
}
