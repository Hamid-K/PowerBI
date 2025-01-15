using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x02000050 RID: 80
	public class EntityTracker : EntityTrackerBase
	{
		// Token: 0x06000269 RID: 617 RVA: 0x0000998A File Offset: 0x00007B8A
		internal EntityTracker(ClientEdmModel maxProtocolVersion)
		{
			this.model = maxProtocolVersion;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000099A9 File Offset: 0x00007BA9
		public IEnumerable<LinkDescriptor> Links
		{
			get
			{
				this.EnsureLinkBindings();
				return this.bindings.Values;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000099BC File Offset: 0x00007BBC
		public IEnumerable<EntityDescriptor> Entities
		{
			get
			{
				return this.entityDescriptors.Values;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000099CC File Offset: 0x00007BCC
		public EntityDescriptor TryGetEntityDescriptor(object entity)
		{
			EntityDescriptor entityDescriptor = null;
			this.entityDescriptors.TryGetValue(entity, out entityDescriptor);
			return entityDescriptor;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000099EC File Offset: 0x00007BEC
		internal override EntityDescriptor GetEntityDescriptor(object resource)
		{
			EntityDescriptor entityDescriptor = this.TryGetEntityDescriptor(resource);
			if (entityDescriptor == null)
			{
				throw Error.InvalidOperation(Strings.Context_EntityNotContained);
			}
			return entityDescriptor;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009A10 File Offset: 0x00007C10
		internal EntityDescriptor TryGetEntityDescriptor(Uri identity)
		{
			EntityDescriptor entityDescriptor;
			if (this.identityToDescriptor != null && this.identityToDescriptor.TryGetValue(identity, out entityDescriptor))
			{
				return entityDescriptor;
			}
			return null;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00009A38 File Offset: 0x00007C38
		internal void AddEntityDescriptor(EntityDescriptor descriptor)
		{
			try
			{
				this.entityDescriptors.Add(descriptor.Entity, descriptor);
			}
			catch (ArgumentException)
			{
				throw Error.InvalidOperation(Strings.Context_EntityAlreadyContained);
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009A78 File Offset: 0x00007C78
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "flag", Justification = "Local variable used in debug assertion.")]
		internal bool DetachResource(EntityDescriptor resource)
		{
			this.EnsureLinkBindings();
			foreach (LinkDescriptor linkDescriptor in this.bindings.Values.Where(new Func<LinkDescriptor, bool>(resource.IsRelatedEntity)).ToList<LinkDescriptor>())
			{
				this.DetachExistingLink(linkDescriptor, linkDescriptor.Target == resource.Entity && resource.State == EntityStates.Added);
			}
			resource.ChangeOrder = uint.MaxValue;
			resource.State = EntityStates.Detached;
			bool flag = this.entityDescriptors.Remove(resource.Entity);
			this.DetachResourceIdentity(resource);
			return true;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009B30 File Offset: 0x00007D30
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "removed", Justification = "Local variable used in debug assertion.")]
		internal void DetachResourceIdentity(EntityDescriptor resource)
		{
			EntityDescriptor entityDescriptor = null;
			if (null != resource.Identity && this.identityToDescriptor.TryGetValue(resource.Identity, out entityDescriptor) && entityDescriptor == resource)
			{
				bool flag = this.identityToDescriptor.Remove(resource.Identity);
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009B78 File Offset: 0x00007D78
		internal LinkDescriptor TryGetLinkDescriptor(object source, string sourceProperty, object target)
		{
			this.EnsureLinkBindings();
			LinkDescriptor linkDescriptor;
			this.bindings.TryGetValue(new LinkDescriptor(source, sourceProperty, target, this.model), out linkDescriptor);
			return linkDescriptor;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009BA8 File Offset: 0x00007DA8
		internal override void AttachLink(object source, string sourceProperty, object target, MergeOption linkMerge)
		{
			LinkDescriptor linkDescriptor = new LinkDescriptor(source, sourceProperty, target, this.model);
			LinkDescriptor linkDescriptor2 = this.TryGetLinkDescriptor(source, sourceProperty, target);
			if (linkDescriptor2 != null)
			{
				switch (linkMerge)
				{
				case MergeOption.OverwriteChanges:
					linkDescriptor = linkDescriptor2;
					break;
				case MergeOption.PreserveChanges:
					if (EntityStates.Added == linkDescriptor2.State || EntityStates.Unchanged == linkDescriptor2.State || (EntityStates.Modified == linkDescriptor2.State && linkDescriptor2.Target != null))
					{
						linkDescriptor = linkDescriptor2;
					}
					break;
				case MergeOption.NoTracking:
					throw Error.InvalidOperation(Strings.Context_RelationAlreadyContained);
				}
			}
			else if (this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(source.GetType())).GetProperty(sourceProperty, UndeclaredPropertyBehavior.ThrowException).IsEntityCollection || (linkDescriptor2 = this.DetachReferenceLink(source, sourceProperty, target, linkMerge)) == null)
			{
				this.AddLink(linkDescriptor);
				this.IncrementChange(linkDescriptor);
			}
			else if (linkMerge != MergeOption.AppendOnly && (MergeOption.PreserveChanges != linkMerge || EntityStates.Modified != linkDescriptor2.State))
			{
				linkDescriptor = linkDescriptor2;
			}
			linkDescriptor.State = EntityStates.Unchanged;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00009C8C File Offset: 0x00007E8C
		internal LinkDescriptor DetachReferenceLink(object source, string sourceProperty, object target, MergeOption linkMerge)
		{
			LinkDescriptor linkDescriptor = this.GetLinks(source, sourceProperty).FirstOrDefault<LinkDescriptor>();
			if (linkDescriptor != null)
			{
				if (target == linkDescriptor.Target || linkMerge == MergeOption.AppendOnly || (MergeOption.PreserveChanges == linkMerge && EntityStates.Modified == linkDescriptor.State))
				{
					return linkDescriptor;
				}
				this.DetachExistingLink(linkDescriptor, false);
			}
			return null;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009CD4 File Offset: 0x00007ED4
		internal void AddLink(LinkDescriptor linkDescriptor)
		{
			try
			{
				this.EnsureLinkBindings();
				this.bindings.Add(linkDescriptor, linkDescriptor);
			}
			catch (ArgumentException)
			{
				throw Error.InvalidOperation(Strings.Context_RelationAlreadyContained);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009D14 File Offset: 0x00007F14
		internal bool TryRemoveLinkDescriptor(LinkDescriptor linkDescriptor)
		{
			this.EnsureLinkBindings();
			return this.bindings.Remove(linkDescriptor);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00009D28 File Offset: 0x00007F28
		internal override IEnumerable<LinkDescriptor> GetLinks(object source, string sourceProperty)
		{
			this.EnsureLinkBindings();
			return this.bindings.Values.Where((LinkDescriptor o) => o.Source == source && o.SourceProperty == sourceProperty);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00009D6C File Offset: 0x00007F6C
		internal override void DetachExistingLink(LinkDescriptor existingLink, bool targetDelete)
		{
			if (existingLink.Target != null)
			{
				EntityDescriptor entityDescriptor = this.GetEntityDescriptor(existingLink.Target);
				if (entityDescriptor.IsDeepInsert && !targetDelete)
				{
					EntityDescriptor parentForInsert = entityDescriptor.ParentForInsert;
					if (entityDescriptor.ParentEntity == existingLink.Source && (parentForInsert.State != EntityStates.Deleted || parentForInsert.State != EntityStates.Detached))
					{
						throw new InvalidOperationException(Strings.Context_ChildResourceExists);
					}
				}
			}
			if (this.TryRemoveLinkDescriptor(existingLink))
			{
				existingLink.State = EntityStates.Detached;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00009DDC File Offset: 0x00007FDC
		internal override void AttachIdentity(EntityDescriptor entityDescriptorFromMaterializer, MergeOption metadataMergeOption)
		{
			this.EnsureIdentityToResource();
			EntityDescriptor entityDescriptor = this.entityDescriptors[entityDescriptorFromMaterializer.Entity];
			this.ValidateDuplicateIdentity(entityDescriptorFromMaterializer.Identity, entityDescriptor);
			this.DetachResourceIdentity(entityDescriptor);
			if (entityDescriptor.IsDeepInsert)
			{
				LinkDescriptor linkDescriptor = this.bindings[entityDescriptor.GetRelatedEnd()];
				linkDescriptor.State = EntityStates.Unchanged;
			}
			entityDescriptor.Identity = entityDescriptorFromMaterializer.Identity;
			AtomMaterializerLog.MergeEntityDescriptorInfo(entityDescriptor, entityDescriptorFromMaterializer, true, metadataMergeOption);
			entityDescriptor.State = EntityStates.Unchanged;
			entityDescriptor.PropertiesToSerialize.Clear();
			this.identityToDescriptor[entityDescriptorFromMaterializer.Identity] = entityDescriptor;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00009E70 File Offset: 0x00008070
		internal void AttachLocation(object entity, Uri identity, Uri editLink)
		{
			this.EnsureIdentityToResource();
			EntityDescriptor entityDescriptor = this.entityDescriptors[entity];
			this.ValidateDuplicateIdentity(identity, entityDescriptor);
			this.DetachResourceIdentity(entityDescriptor);
			if (entityDescriptor.IsDeepInsert)
			{
				LinkDescriptor linkDescriptor = this.bindings[entityDescriptor.GetRelatedEnd()];
				linkDescriptor.State = EntityStates.Unchanged;
			}
			entityDescriptor.Identity = identity;
			entityDescriptor.EditLink = editLink;
			this.identityToDescriptor[identity] = entityDescriptor;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00009EDC File Offset: 0x000080DC
		internal override EntityDescriptor InternalAttachEntityDescriptor(EntityDescriptor entityDescriptorFromMaterializer, bool failIfDuplicated)
		{
			this.EnsureIdentityToResource();
			EntityDescriptor entityDescriptor;
			this.entityDescriptors.TryGetValue(entityDescriptorFromMaterializer.Entity, out entityDescriptor);
			EntityDescriptor entityDescriptor2;
			this.identityToDescriptor.TryGetValue(entityDescriptorFromMaterializer.Identity, out entityDescriptor2);
			if (failIfDuplicated && entityDescriptor != null)
			{
				throw Error.InvalidOperation(Strings.Context_EntityAlreadyContained);
			}
			if (entityDescriptor != entityDescriptor2)
			{
				throw Error.InvalidOperation(Strings.Context_DifferentEntityAlreadyContained);
			}
			if (entityDescriptor == null)
			{
				entityDescriptor = entityDescriptorFromMaterializer;
				this.IncrementChange(entityDescriptorFromMaterializer);
				this.entityDescriptors.Add(entityDescriptorFromMaterializer.Entity, entityDescriptorFromMaterializer);
				this.identityToDescriptor.Add(entityDescriptorFromMaterializer.Identity, entityDescriptorFromMaterializer);
			}
			return entityDescriptor;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00009F68 File Offset: 0x00008168
		internal override object TryGetEntity(Uri resourceUri, out EntityStates state)
		{
			state = EntityStates.Detached;
			EntityDescriptor entityDescriptor = null;
			if (this.identityToDescriptor != null && this.identityToDescriptor.TryGetValue(resourceUri, out entityDescriptor))
			{
				state = entityDescriptor.State;
				return entityDescriptor.Entity;
			}
			return null;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009FA4 File Offset: 0x000081A4
		internal void IncrementChange(Descriptor descriptor)
		{
			uint num = this.nextChange + 1U;
			this.nextChange = num;
			descriptor.ChangeOrder = num;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00009FC8 File Offset: 0x000081C8
		private void EnsureIdentityToResource()
		{
			if (this.identityToDescriptor == null)
			{
				Interlocked.CompareExchange<ConcurrentDictionary<Uri, EntityDescriptor>>(ref this.identityToDescriptor, new ConcurrentDictionary<Uri, EntityDescriptor>(EqualityComparer<Uri>.Default), null);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009FE9 File Offset: 0x000081E9
		private void EnsureLinkBindings()
		{
			if (this.bindings == null)
			{
				Interlocked.CompareExchange<ConcurrentDictionary<LinkDescriptor, LinkDescriptor>>(ref this.bindings, new ConcurrentDictionary<LinkDescriptor, LinkDescriptor>(LinkDescriptor.EquivalenceComparer), null);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A00C File Offset: 0x0000820C
		private void ValidateDuplicateIdentity(Uri identity, EntityDescriptor descriptor)
		{
			EntityDescriptor entityDescriptor;
			if (this.identityToDescriptor.TryGetValue(identity, out entityDescriptor) && descriptor != entityDescriptor && entityDescriptor.State != EntityStates.Deleted && entityDescriptor.State != EntityStates.Detached)
			{
				throw Error.InvalidOperation(Strings.Context_DifferentEntityAlreadyContained);
			}
		}

		// Token: 0x040000D7 RID: 215
		private readonly ClientEdmModel model;

		// Token: 0x040000D8 RID: 216
		private ConcurrentDictionary<object, EntityDescriptor> entityDescriptors = new ConcurrentDictionary<object, EntityDescriptor>(EqualityComparer<object>.Default);

		// Token: 0x040000D9 RID: 217
		private ConcurrentDictionary<Uri, EntityDescriptor> identityToDescriptor;

		// Token: 0x040000DA RID: 218
		private ConcurrentDictionary<LinkDescriptor, LinkDescriptor> bindings;

		// Token: 0x040000DB RID: 219
		private uint nextChange;
	}
}
