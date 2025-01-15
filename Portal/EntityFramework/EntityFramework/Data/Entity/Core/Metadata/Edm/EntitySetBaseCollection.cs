using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004BA RID: 1210
	internal sealed class EntitySetBaseCollection : MetadataCollection<EntitySetBase>
	{
		// Token: 0x06003BD2 RID: 15314 RVA: 0x000C663C File Offset: 0x000C483C
		internal EntitySetBaseCollection(EntityContainer entityContainer)
			: this(entityContainer, null)
		{
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x000C6646 File Offset: 0x000C4846
		internal EntitySetBaseCollection(EntityContainer entityContainer, IEnumerable<EntitySetBase> items)
			: base(items)
		{
			Check.NotNull<EntityContainer>(entityContainer, "entityContainer");
			this._entityContainer = entityContainer;
		}

		// Token: 0x17000BA8 RID: 2984
		public override EntitySetBase this[int index]
		{
			get
			{
				return base[index];
			}
			set
			{
				throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
			}
		}

		// Token: 0x17000BA9 RID: 2985
		public override EntitySetBase this[string identity]
		{
			get
			{
				return base[identity];
			}
			set
			{
				throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
			}
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x000C668C File Offset: 0x000C488C
		public override void Add(EntitySetBase item)
		{
			Check.NotNull<EntitySetBase>(item, "item");
			EntitySetBaseCollection.ThrowIfItHasEntityContainer(item, "item");
			base.Add(item);
			item.ChangeEntityContainerWithoutCollectionFixup(this._entityContainer);
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x000C66B8 File Offset: 0x000C48B8
		private static void ThrowIfItHasEntityContainer(EntitySetBase entitySet, string argumentName)
		{
			Check.NotNull<EntitySetBase>(entitySet, argumentName);
			if (entitySet.EntityContainer != null)
			{
				throw new ArgumentException(Strings.EntitySetInAnotherContainer, argumentName);
			}
		}

		// Token: 0x0400149C RID: 5276
		private readonly EntityContainer _entityContainer;
	}
}
