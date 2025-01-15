using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x0200007A RID: 122
	public sealed class ConceptualEntityExtensionAwareEqualityComparer : IEqualityComparer<IConceptualEntity>
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0000780C File Offset: 0x00005A0C
		private ConceptualEntityExtensionAwareEqualityComparer()
		{
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00007814 File Offset: 0x00005A14
		public bool Equals(IConceptualEntity x, IConceptualEntity y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			IExtensionConceptualEntity extensionConceptualEntity = x as IExtensionConceptualEntity;
			IExtensionConceptualEntity extensionConceptualEntity2 = y as IExtensionConceptualEntity;
			if (extensionConceptualEntity != null && extensionConceptualEntity2 != null)
			{
				return this.Equals(extensionConceptualEntity.ExtendedEntity, extensionConceptualEntity2.ExtendedEntity);
			}
			if (extensionConceptualEntity != null)
			{
				return this.Equals(extensionConceptualEntity.ExtendedEntity, y);
			}
			if (extensionConceptualEntity2 != null)
			{
				return this.Equals(x, extensionConceptualEntity2.ExtendedEntity);
			}
			return x.Schema.SchemaId == y.Schema.SchemaId && x.Name == y.Name;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000078A8 File Offset: 0x00005AA8
		public int GetHashCode(IConceptualEntity obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			IExtensionConceptualEntity extensionConceptualEntity = obj as IExtensionConceptualEntity;
			if (extensionConceptualEntity != null && extensionConceptualEntity.ExtendedEntity != null)
			{
				return this.GetHashCode(extensionConceptualEntity.ExtendedEntity);
			}
			return Hashing.CombineHash(obj.Schema.SchemaId.GetHashCode(), obj.Name.GetHashCode());
		}

		// Token: 0x040001A5 RID: 421
		public static readonly IEqualityComparer<IConceptualEntity> Instance = new ConceptualEntityExtensionAwareEqualityComparer();
	}
}
