using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000091 RID: 145
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class OwnedModelItemCollection<T> : ModelItemCollection<T, ModelItem>, IOwnedModelItemCollection, IList<ModelItem>, ICollection<ModelItem>, IEnumerable<ModelItem>, IEnumerable, IXmlLoadable, IPersistable where T : ModelItem
	{
		// Token: 0x060006AD RID: 1709 RVA: 0x00015342 File Offset: 0x00013542
		internal OwnedModelItemCollection(ModelItem parentItem)
			: base(parentItem)
		{
		}

		// Token: 0x17000181 RID: 385
		private T this[string name]
		{
			get
			{
				return base.Items.Find(ModelItem.NameMatch<T>(name));
			}
		}

		// Token: 0x17000182 RID: 386
		ModelItem IOwnedModelItemCollection.this[string name]
		{
			get
			{
				return this[name];
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001536C File Offset: 0x0001356C
		bool IOwnedModelItemCollection.CanContain(ModelItem item)
		{
			return item is T;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00015377 File Offset: 0x00013577
		IEnumerator<ModelItem> IEnumerable<ModelItem>.GetEnumerator()
		{
			int num;
			for (int i = 0; i < base.Count; i = num + 1)
			{
				yield return base[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00015386 File Offset: 0x00013586
		void ICollection<ModelItem>.Add(ModelItem item)
		{
			base.Add((T)((object)item));
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00015394 File Offset: 0x00013594
		void ICollection<ModelItem>.Clear()
		{
			base.Clear();
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001539C File Offset: 0x0001359C
		bool ICollection<ModelItem>.Contains(ModelItem item)
		{
			return base.Contains((T)((object)item));
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000153AA File Offset: 0x000135AA
		void ICollection<ModelItem>.CopyTo(ModelItem[] array, int arrayIndex)
		{
			((ICollection)base.Items).CopyTo(array, arrayIndex);
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x000153B9 File Offset: 0x000135B9
		int ICollection<ModelItem>.Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000153C1 File Offset: 0x000135C1
		bool ICollection<ModelItem>.Remove(ModelItem item)
		{
			return base.Remove((T)((object)item));
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000153CF File Offset: 0x000135CF
		int IList<ModelItem>.IndexOf(ModelItem item)
		{
			return base.IndexOf((T)((object)item));
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x000153DD File Offset: 0x000135DD
		void IList<ModelItem>.Insert(int index, ModelItem item)
		{
			base.Insert(index, (T)((object)item));
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000153EC File Offset: 0x000135EC
		void IList<ModelItem>.RemoveAt(int index)
		{
			base.RemoveAt(index);
		}

		// Token: 0x17000184 RID: 388
		ModelItem IList<ModelItem>.this[int index]
		{
			get
			{
				return base[index];
			}
			set
			{
				base[index] = (T)((object)value);
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00015412 File Offset: 0x00013612
		protected override void InsertItem(int index, T item)
		{
			base.CheckWriteable();
			this.LinkItem(item);
			base.InsertItem(index, item);
			this.NotifyCollectionChanged();
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00015434 File Offset: 0x00013634
		protected override void SetItem(int index, T item)
		{
			base.CheckWriteable();
			if (base[index] != item)
			{
				this.UnlinkItem(base[index]);
				this.LinkItem(item);
				base.SetItem(index, item);
				this.NotifyCollectionChanged();
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00015487 File Offset: 0x00013687
		protected override void RemoveItem(int index)
		{
			base.CheckWriteable();
			this.UnlinkItem(base[index]);
			base.RemoveItem(index);
			this.NotifyCollectionChanged();
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000154B0 File Offset: 0x000136B0
		protected override void ClearItems()
		{
			base.CheckWriteable();
			foreach (T t in this)
			{
				ModelItem modelItem = t;
				this.UnlinkItem(modelItem);
			}
			base.ClearItems();
			this.NotifyCollectionChanged();
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00015518 File Offset: 0x00013718
		internal void Load(ModelingXmlReader xr)
		{
			base.CheckWriteable();
			xr.LoadObject(this);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00015527 File Offset: 0x00013727
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001552C File Offset: 0x0001372C
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				T t = ModelItem.CreateItem<T>(xr.LocalName);
				if (t != null)
				{
					t.Load(xr);
					try
					{
						base.Add(t);
					}
					catch (ValidationException ex)
					{
						xr.Validation.AddMessages(ex.Messages);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00015594 File Offset: 0x00013794
		internal void WriteTo(ModelingXmlWriter xw, string collectionElementName)
		{
			xw.WriteCollectionElement<T>(collectionElementName, this);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001559E File Offset: 0x0001379E
		internal override void InitItemFromBase(T newItem)
		{
			if (newItem.ParentItem == null)
			{
				throw new InternalModelingException("Item.ParentItem should already be set on lazy cloned item");
			}
			newItem.SetOwnerCollection(this);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x000155C4 File Offset: 0x000137C4
		private void LinkItem(ModelItem item)
		{
			if (item == null)
			{
				throw new ArgumentNullException();
			}
			if (item.OwnerCollection != null)
			{
				throw new InvalidOperationException(DevExceptionMessages.ExistingOwner);
			}
			item.SetParentItem(base.ParentItem, false);
			item.SetOwnerCollection(this);
			if (base.ParentItem.Model != null)
			{
				base.ParentItem.Model.LinkItem(item);
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00015620 File Offset: 0x00013820
		private void UnlinkItem(ModelItem item)
		{
			if (item == null)
			{
				throw new InternalModelingException("null item in collection");
			}
			if (item.ParentItem != base.ParentItem)
			{
				throw new InternalModelingException("Existing item has incorrect ParentItem.");
			}
			if (base.ParentItem.Model != null)
			{
				base.ParentItem.Model.UnlinkItem(item);
			}
			item.SetOwnerCollection(null);
			item.SetParentItem(null, false);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00015681 File Offset: 0x00013881
		private void NotifyCollectionChanged()
		{
			if (base.ParentItem.Model != null)
			{
				base.ParentItem.Model.NotifyCollectionChanged(this);
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000156A1 File Offset: 0x000138A1
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000156AC File Offset: 0x000138AC
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(OwnedModelItemCollection<T>.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.Items)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				PersistenceHelper.WriteListOfModelingObjects<ModelItem>(ref writer, this);
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001571B File Offset: 0x0001391B
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00015724 File Offset: 0x00013924
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			using (this.AllowWriteOperations())
			{
				reader.RegisterDeclaration(OwnedModelItemCollection<T>.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.Items)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					PersistenceHelper.ReadListOfModelingObjects<ModelItem>(ref reader, this);
				}
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000157B8 File Offset: 0x000139B8
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000157C4 File Offset: 0x000139C4
		ObjectType IPersistable.GetObjectType()
		{
			return ObjectType.OwnedModelItemCollection;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x000157C8 File Offset: 0x000139C8
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref OwnedModelItemCollection<T>.__declaration, OwnedModelItemCollection<T>.__declarationLock, () => new Declaration(ObjectType.OwnedModelItemCollection, ObjectType.ModelItemCollection, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Items, ObjectType.RIFObjectList, ObjectType.ModelItem)
				}));
			}
		}

		// Token: 0x04000342 RID: 834
		private static Declaration __declaration;

		// Token: 0x04000343 RID: 835
		private static readonly object __declarationLock = new object();
	}
}
