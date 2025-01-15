using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000089 RID: 137
	internal sealed class EntitySetBaseCollection : MetadataCollection<EntitySetBase>
	{
		// Token: 0x06000A16 RID: 2582 RVA: 0x00017F40 File Offset: 0x00016140
		internal EntitySetBaseCollection(EntityContainer entityContainer)
			: this(entityContainer, null)
		{
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00017F4A File Offset: 0x0001614A
		internal EntitySetBaseCollection(EntityContainer entityContainer, IEnumerable<EntitySetBase> items)
			: base(items)
		{
			EntityUtil.GenericCheckArgumentNull<EntityContainer>(entityContainer, "entityContainer");
			this._entityContainer = entityContainer;
		}

		// Token: 0x170003A1 RID: 929
		public override EntitySetBase this[int index]
		{
			get
			{
				return base[index];
			}
			set
			{
				throw EntityUtil.OperationOnReadOnlyCollection();
			}
		}

		// Token: 0x170003A2 RID: 930
		public override EntitySetBase this[string identity]
		{
			get
			{
				return base[identity];
			}
			set
			{
				throw EntityUtil.OperationOnReadOnlyCollection();
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00017F86 File Offset: 0x00016186
		public override void Add(EntitySetBase item)
		{
			EntityUtil.GenericCheckArgumentNull<EntitySetBase>(item, "item");
			EntitySetBaseCollection.ThrowIfItHasEntityContainer(item, "item");
			base.Add(item);
			item.ChangeEntityContainerWithoutCollectionFixup(this._entityContainer);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00017FB2 File Offset: 0x000161B2
		private static void ThrowIfItHasEntityContainer(EntitySetBase entitySet, string argumentName)
		{
			EntityUtil.GenericCheckArgumentNull<EntitySetBase>(entitySet, argumentName);
			if (entitySet.EntityContainer != null)
			{
				throw EntityUtil.EntitySetInAnotherContainer(argumentName);
			}
		}

		// Token: 0x04000826 RID: 2086
		private readonly EntityContainer _entityContainer;
	}
}
