using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005D4 RID: 1492
	internal abstract class UpdateCommand : IComparable<UpdateCommand>, IEquatable<UpdateCommand>
	{
		// Token: 0x060047D6 RID: 18390 RVA: 0x000FED97 File Offset: 0x000FCF97
		protected UpdateCommand(UpdateTranslator translator, PropagatorResult originalValues, PropagatorResult currentValues)
		{
			this.OriginalValues = originalValues;
			this.CurrentValues = currentValues;
			this.Translator = translator;
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x060047D7 RID: 18391
		internal abstract IEnumerable<int> OutputIdentifiers { get; }

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x060047D8 RID: 18392
		internal abstract IEnumerable<int> InputIdentifiers { get; }

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x060047D9 RID: 18393 RVA: 0x000FEDB4 File Offset: 0x000FCFB4
		internal virtual EntitySet Table
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x060047DA RID: 18394
		internal abstract UpdateCommandKind Kind { get; }

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x060047DB RID: 18395 RVA: 0x000FEDB7 File Offset: 0x000FCFB7
		// (set) Token: 0x060047DC RID: 18396 RVA: 0x000FEDBF File Offset: 0x000FCFBF
		internal PropagatorResult OriginalValues { get; private set; }

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x060047DD RID: 18397 RVA: 0x000FEDC8 File Offset: 0x000FCFC8
		// (set) Token: 0x060047DE RID: 18398 RVA: 0x000FEDD0 File Offset: 0x000FCFD0
		internal PropagatorResult CurrentValues { get; private set; }

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x060047DF RID: 18399 RVA: 0x000FEDD9 File Offset: 0x000FCFD9
		// (set) Token: 0x060047E0 RID: 18400 RVA: 0x000FEDE1 File Offset: 0x000FCFE1
		private protected UpdateTranslator Translator { protected get; private set; }

		// Token: 0x060047E1 RID: 18401
		internal abstract IList<IEntityStateEntry> GetStateEntries(UpdateTranslator translator);

		// Token: 0x060047E2 RID: 18402 RVA: 0x000FEDEC File Offset: 0x000FCFEC
		internal void GetRequiredAndProducedEntities(UpdateTranslator translator, KeyToListMap<EntityKey, UpdateCommand> addedEntities, KeyToListMap<EntityKey, UpdateCommand> deletedEntities, KeyToListMap<EntityKey, UpdateCommand> addedRelationships, KeyToListMap<EntityKey, UpdateCommand> deletedRelationships)
		{
			IList<IEntityStateEntry> stateEntries = this.GetStateEntries(translator);
			foreach (IEntityStateEntry entityStateEntry in stateEntries)
			{
				if (!entityStateEntry.IsRelationship)
				{
					if (entityStateEntry.State == EntityState.Added)
					{
						addedEntities.Add(entityStateEntry.EntityKey, this);
					}
					else if (entityStateEntry.State == EntityState.Deleted)
					{
						deletedEntities.Add(entityStateEntry.EntityKey, this);
					}
				}
			}
			if (this.OriginalValues != null)
			{
				this.AddReferencedEntities(translator, this.OriginalValues, deletedRelationships);
			}
			if (this.CurrentValues != null)
			{
				this.AddReferencedEntities(translator, this.CurrentValues, addedRelationships);
			}
			foreach (IEntityStateEntry entityStateEntry2 in stateEntries)
			{
				if (entityStateEntry2.IsRelationship)
				{
					bool flag = entityStateEntry2.State == EntityState.Added;
					if (flag || entityStateEntry2.State == EntityState.Deleted)
					{
						CurrentValueRecord currentValueRecord = (flag ? entityStateEntry2.CurrentValues : entityStateEntry2.OriginalValues);
						EntityKey entityKey = (EntityKey)currentValueRecord[0];
						EntityKey entityKey2 = (EntityKey)currentValueRecord[1];
						KeyToListMap<EntityKey, UpdateCommand> keyToListMap = (flag ? addedRelationships : deletedRelationships);
						keyToListMap.Add(entityKey, this);
						keyToListMap.Add(entityKey2, this);
					}
				}
			}
		}

		// Token: 0x060047E3 RID: 18403 RVA: 0x000FEF34 File Offset: 0x000FD134
		private void AddReferencedEntities(UpdateTranslator translator, PropagatorResult result, KeyToListMap<EntityKey, UpdateCommand> referencedEntities)
		{
			foreach (PropagatorResult propagatorResult in result.GetMemberValues())
			{
				if (propagatorResult.IsSimple && propagatorResult.Identifier != -1 && PropagatorFlags.ForeignKey == (propagatorResult.PropagatorFlags & PropagatorFlags.ForeignKey))
				{
					foreach (int num in translator.KeyManager.GetDirectReferences(propagatorResult.Identifier))
					{
						PropagatorResult propagatorResult2;
						if (translator.KeyManager.TryGetIdentifierOwner(num, out propagatorResult2) && propagatorResult2.StateEntry != null)
						{
							referencedEntities.Add(propagatorResult2.StateEntry.EntityKey, this);
						}
					}
				}
			}
		}

		// Token: 0x060047E4 RID: 18404
		internal abstract long Execute(Dictionary<int, object> identifierValues, List<KeyValuePair<PropagatorResult, object>> generatedValues);

		// Token: 0x060047E5 RID: 18405
		internal abstract Task<long> ExecuteAsync(Dictionary<int, object> identifierValues, List<KeyValuePair<PropagatorResult, object>> generatedValues, CancellationToken cancellationToken);

		// Token: 0x060047E6 RID: 18406
		internal abstract int CompareToType(UpdateCommand other);

		// Token: 0x060047E7 RID: 18407 RVA: 0x000FEFF0 File Offset: 0x000FD1F0
		public int CompareTo(UpdateCommand other)
		{
			if (this.Equals(other))
			{
				return 0;
			}
			int num = this.Kind - other.Kind;
			if (num != 0)
			{
				return num;
			}
			num = this.CompareToType(other);
			if (num != 0)
			{
				return num;
			}
			if (this._orderingIdentifier == 0)
			{
				this._orderingIdentifier = Interlocked.Increment(ref UpdateCommand.OrderingIdentifierCounter);
			}
			if (other._orderingIdentifier == 0)
			{
				other._orderingIdentifier = Interlocked.Increment(ref UpdateCommand.OrderingIdentifierCounter);
			}
			return this._orderingIdentifier - other._orderingIdentifier;
		}

		// Token: 0x060047E8 RID: 18408 RVA: 0x000FF065 File Offset: 0x000FD265
		public bool Equals(UpdateCommand other)
		{
			return base.Equals(other);
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x000FF06E File Offset: 0x000FD26E
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x000FF077 File Offset: 0x000FD277
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400198F RID: 6543
		private static int OrderingIdentifierCounter;

		// Token: 0x04001990 RID: 6544
		private int _orderingIdentifier;
	}
}
