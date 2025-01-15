using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200006B RID: 107
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class DsvItemCollection<T> : IndirectReadOnlyCollection<T>, IPersistable where T : DsvItem
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x0000ED85 File Offset: 0x0000CF85
		internal DsvItemCollection()
		{
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000ED90 File Offset: 0x0000CF90
		internal static T CheckNameMatch(T item, string name)
		{
			if (item == null || !(item.Name == name))
			{
				return default(T);
			}
			return item;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000475 RID: 1141
		internal abstract IPersistable DataStorage { get; }

		// Token: 0x06000476 RID: 1142 RVA: 0x0000EDC3 File Offset: 0x0000CFC3
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.DataStorage.Serialize(writer);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000EDD1 File Offset: 0x0000CFD1
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.DataStorage.Deserialize(reader);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000EDDF File Offset: 0x0000CFDF
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			this.DataStorage.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000EDEE File Offset: 0x0000CFEE
		ObjectType IPersistable.GetObjectType()
		{
			return this.DataStorage.GetObjectType();
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000EDFC File Offset: 0x0000CFFC
		internal static T FindDsvItemByName(T[] array, Dictionary<string, T> dictionary, string name)
		{
			if (dictionary != null)
			{
				return dictionary[name];
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (string.CompareOrdinal(name, array[i].Name) == 0)
				{
					return array[i];
				}
			}
			throw new KeyNotFoundException(name);
		}
	}
}
