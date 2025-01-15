using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200008F RID: 143
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ModelItemCollection<T, P> : CheckedCollection<T>, IDeserializationCallback where T : ModelItem where P : ModelItem
	{
		// Token: 0x0600069B RID: 1691 RVA: 0x00014FBD File Offset: 0x000131BD
		internal ModelItemCollection(P parentItem)
		{
			if (parentItem == null)
			{
				throw new InternalModelingException("parentItem is null");
			}
			this.m_parentItem = parentItem;
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00014FDF File Offset: 0x000131DF
		public override bool IsReadOnly
		{
			get
			{
				return this.m_parentItem.IsCompiled;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00014FF1 File Offset: 0x000131F1
		public P ParentItem
		{
			get
			{
				return this.m_parentItem;
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00014FFC File Offset: 0x000131FC
		internal bool CheckInvalidRefs(CompilationContext ctx, string collectionElementName, string itemElementName)
		{
			bool flag = true;
			foreach (T t in this)
			{
				if (ctx.ShouldCheckInvalidRefsDuringCompilation && t.IsInvalidRefTarget)
				{
					ctx.AddScopedError(ModelingErrorCode.ItemNotFound, SRErrors.ItemNotFound_MultipleProperties(ModelItemCollection<T, P>.CreateReferenceName(collectionElementName, itemElementName), ctx.CurrentObjectDescriptor, t.ID.ToString()));
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00015090 File Offset: 0x00013290
		internal void LoadRefs(ModelingXmlReader xr, string itemElementName)
		{
			string localName = xr.LocalName;
			xr.LoadObject(new ModelItemCollection<T, P>.ItemRefsLoader(this, itemElementName, ModelItemCollection<T, P>.CreateReferenceName(localName, itemElementName)));
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000150B8 File Offset: 0x000132B8
		private static string CreateReferenceName(string collectionElementName, string itemElementName)
		{
			return collectionElementName + "." + itemElementName;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x000150C8 File Offset: 0x000132C8
		bool IDeserializationCallback.ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
		{
			T t = ctx.CurrentModel.TryGetModelItem<T>(reference, ctx.Validation);
			base.Add(t);
			return true;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x000150F0 File Offset: 0x000132F0
		internal void WriteRefsTo(ModelingXmlWriter xw, string collectionElementName, string itemElementName)
		{
			xw.WriteCollectionElement<T>(collectionElementName, this, delegate(T item)
			{
				xw.WriteReferenceElement(itemElementName, item);
			});
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001512C File Offset: 0x0001332C
		internal void CopyFromBase(ModelItemCollection<T, P> items)
		{
			if (items == null)
			{
				throw new InternalModelingException("items is null");
			}
			if (this.ParentItem.Model == null || items.ParentItem.Model == null || this.ParentItem.Model == items.ParentItem.Model)
			{
				throw new InternalModelingException("ParentItem has no Model, items' ParentItem has no Model, or Models match; copy only allowed between different models");
			}
			if (base.Count > 0)
			{
				throw new InternalModelingException("Collection is not empty");
			}
			foreach (T t in items)
			{
				T t2 = this.ParentItem.Model.LookupItemByID(t.ID) as T;
				if (t2 != null)
				{
					base.Items.Add(t2);
					this.InitItemFromBase(t2);
				}
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001522C File Offset: 0x0001342C
		internal virtual void InitItemFromBase(T newItem)
		{
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001522E File Offset: 0x0001342E
		internal override IDisposable AllowWriteOperations()
		{
			return this.ParentItem.AllowWriteOperations();
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00015240 File Offset: 0x00013440
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ModelItemCollection<T, P>.Declaration);
			if (!writer.NextMember())
			{
				return;
			}
			MemberName memberName = writer.CurrentMember.MemberName;
			throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000152A4 File Offset: 0x000134A4
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ModelItemCollection<T, P>.Declaration);
			if (!reader.NextMember())
			{
				return;
			}
			MemberName memberName = reader.CurrentMember.MemberName;
			throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00015306 File Offset: 0x00013506
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ModelItemCollection<T, P>.__declaration, ModelItemCollection<T, P>.__declarationLock, delegate
				{
					List<MemberInfo> list = new List<MemberInfo>();
					return new Declaration(ObjectType.ModelItemCollection, ObjectType.CheckedCollection, list);
				});
			}
		}

		// Token: 0x0400033F RID: 831
		private readonly P m_parentItem;

		// Token: 0x04000340 RID: 832
		private static Declaration __declaration;

		// Token: 0x04000341 RID: 833
		private static readonly object __declarationLock = new object();

		// Token: 0x02000172 RID: 370
		private class ItemRefsLoader : ModelingXmlLoaderBase<ModelItemCollection<T, P>>
		{
			// Token: 0x06000FB2 RID: 4018 RVA: 0x0003160F File Offset: 0x0002F80F
			internal ItemRefsLoader(ModelItemCollection<T, P> item, string itemElementName, string referenceName)
				: base(item)
			{
				this.m_itemElementName = itemElementName;
				this.m_referenceName = referenceName;
			}

			// Token: 0x06000FB3 RID: 4019 RVA: 0x00031628 File Offset: 0x0002F828
			public override bool LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == this.m_itemElementName)
				{
					xr.Context.AddReference(base.Item, xr.ReadReferenceByID(this.m_referenceName, true));
					return true;
				}
				return base.LoadXmlElement(xr);
			}

			// Token: 0x0400067B RID: 1659
			private readonly string m_itemElementName;

			// Token: 0x0400067C RID: 1660
			private readonly string m_referenceName;
		}
	}
}
