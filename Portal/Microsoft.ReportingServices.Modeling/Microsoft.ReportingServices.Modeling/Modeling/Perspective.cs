using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000095 RID: 149
	public sealed class Perspective : ModelItem
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x000168D8 File Offset: 0x00014AD8
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Perspective.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.ModelItems)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				((IPersistable)this.ModelItems).Serialize(writer);
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001694C File Offset: 0x00014B4C
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			using (base.AllowWriteOperations())
			{
				reader.RegisterDeclaration(Perspective.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.ModelItems)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					((IPersistable)this.ModelItems).Deserialize(reader);
				}
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000169E4 File Offset: 0x00014BE4
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000169F0 File Offset: 0x00014BF0
		internal override ObjectType GetObjectType()
		{
			return ObjectType.Perspective;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x000169F4 File Offset: 0x00014BF4
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref Perspective.__declaration, Perspective.__declarationLock, () => new Declaration(ObjectType.Perspective, ObjectType.ModelItem, new List<MemberInfo>
				{
					new MemberInfo(MemberName.ModelItems, ObjectType.PerspectiveModelItemCollection)
				}));
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00016A24 File Offset: 0x00014C24
		public Perspective()
		{
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00016A2C File Offset: 0x00014C2C
		internal override void Reset()
		{
			base.Reset();
			this.__modelItems = new Perspective.PerspectiveModelItemCollection(this);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00016A40 File Offset: 0x00014C40
		internal Perspective(Perspective baseItem, bool markAsHidden)
			: base(baseItem, markAsHidden)
		{
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x00016A4A File Offset: 0x00014C4A
		public Perspective.PerspectiveModelItemCollection ModelItems
		{
			get
			{
				if (this.__modelItems == null)
				{
					this.__modelItems = this.BaseItem.ModelItems.CloneFor(this);
				}
				return this.__modelItems;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00016A71 File Offset: 0x00014C71
		internal override string ElementName
		{
			get
			{
				return "Perspective";
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x00016A78 File Offset: 0x00014C78
		private new Perspective BaseItem
		{
			get
			{
				return (Perspective)base.BaseItem;
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00016A85 File Offset: 0x00014C85
		public override bool IsValidParentItem(ModelItem newParentItem, out ValidationMessage message)
		{
			if (newParentItem is SemanticModel)
			{
				message = null;
				return true;
			}
			return base.IsValidParentItem(newParentItem, out message);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00016A9C File Offset: 0x00014C9C
		public override IEnumerable<ModelItem> GetNamespaceItems()
		{
			if (base.OwnerCollection != null)
			{
				return base.OwnerCollection;
			}
			return ModelItem.EmptyArray;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00016AB2 File Offset: 0x00014CB2
		internal override bool LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "ModelItems")
			{
				this.ModelItems.LoadRefs(xr, "ModelItemID");
				return true;
			}
			return base.LoadXmlElement(xr);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00016AE8 File Offset: 0x00014CE8
		internal override void WriteXmlElements(ModelingXmlWriter xw)
		{
			base.WriteXmlElements(xw);
			this.ModelItems.WriteRefsTo(xw, "ModelItems", "ModelItemID");
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00016B08 File Offset: 0x00014D08
		internal override void CompileCore(CompilationContext ctx)
		{
			base.CompileCore(ctx);
			if (this.ModelItems.Count > 0)
			{
				foreach (ModelItem modelItem in this.ModelItems)
				{
					if (modelItem is SemanticModel || modelItem is Perspective)
					{
						ctx.AddScopedError(ModelingErrorCode.InvalidModelItemInPerspective, SRErrors.InvalidModelItemInPerspective(ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(modelItem)));
					}
				}
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00016B94 File Offset: 0x00014D94
		internal override ModelItem CreateLazyClone(bool markAsHidden)
		{
			return new Perspective(this, markAsHidden);
		}

		// Token: 0x04000367 RID: 871
		private static Declaration __declaration;

		// Token: 0x04000368 RID: 872
		private static readonly object __declarationLock = new object();

		// Token: 0x04000369 RID: 873
		internal const string PerspectiveElem = "Perspective";

		// Token: 0x0400036A RID: 874
		private const string ModelItemsElem = "ModelItems";

		// Token: 0x0400036B RID: 875
		private const string ModelItemIdElem = "ModelItemID";

		// Token: 0x0400036C RID: 876
		private Perspective.PerspectiveModelItemCollection __modelItems;

		// Token: 0x0200017A RID: 378
		public sealed class PerspectiveModelItemCollection : ModelItemCollection<ModelItem, Perspective>, IPersistable, IOwnedModelItemCollection, IList<ModelItem>, ICollection<ModelItem>, IEnumerable<ModelItem>, IEnumerable, IDeserializationCallback
		{
			// Token: 0x06000FD1 RID: 4049 RVA: 0x00031B27 File Offset: 0x0002FD27
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				this.Serialize(writer);
			}

			// Token: 0x06000FD2 RID: 4050 RVA: 0x00031B30 File Offset: 0x0002FD30
			internal override void Serialize(IntermediateFormatWriter writer)
			{
				base.Serialize(writer);
				writer.RegisterDeclaration(Perspective.PerspectiveModelItemCollection.Declaration);
				while (writer.NextMember())
				{
					if (writer.CurrentMember.MemberName != MemberName.Items)
					{
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
					PersistenceHelper.WriteModelingObjectReferences<ModelItem>(ref writer, this);
				}
			}

			// Token: 0x06000FD3 RID: 4051 RVA: 0x00031B9F File Offset: 0x0002FD9F
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				this.Deserialize(reader);
			}

			// Token: 0x06000FD4 RID: 4052 RVA: 0x00031BA8 File Offset: 0x0002FDA8
			internal override void Deserialize(IntermediateFormatReader reader)
			{
				base.Deserialize(reader);
				reader.RegisterDeclaration(Perspective.PerspectiveModelItemCollection.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.Items)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					reader.ReadListOfReferencesNoResolution(this);
				}
			}

			// Token: 0x06000FD5 RID: 4053 RVA: 0x00031C18 File Offset: 0x0002FE18
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				using (this.AllowWriteOperations())
				{
					List<MemberReference> list;
					if (memberReferencesCollection.TryGetValue(Perspective.PerspectiveModelItemCollection.Declaration.ObjectType, out list))
					{
						foreach (MemberReference memberReference in list)
						{
							if (memberReference.MemberName != MemberName.Items)
							{
								throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
							}
							this.Add(PersistenceHelper.ResolveModelingObjectReference<ModelItem>(referenceableItems[memberReference.RefID]));
						}
					}
				}
			}

			// Token: 0x06000FD6 RID: 4054 RVA: 0x00031CDC File Offset: 0x0002FEDC
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.PerspectiveModelItemCollection;
			}

			// Token: 0x170003DE RID: 990
			// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00031CE0 File Offset: 0x0002FEE0
			private static Declaration Declaration
			{
				get
				{
					return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref Perspective.PerspectiveModelItemCollection.__declaration, Perspective.PerspectiveModelItemCollection.__declarationLock, () => new Declaration(ObjectType.PerspectiveModelItemCollection, ObjectType.ModelItemCollection, new List<MemberInfo>
					{
						new MemberInfo(MemberName.Items, ObjectType.RIFObjectList, Token.Reference, ObjectType.ModelItem)
					}));
				}
			}

			// Token: 0x06000FD8 RID: 4056 RVA: 0x00031D10 File Offset: 0x0002FF10
			internal PerspectiveModelItemCollection(Perspective parentItem)
				: base(parentItem)
			{
			}

			// Token: 0x06000FD9 RID: 4057 RVA: 0x00031D19 File Offset: 0x0002FF19
			public new void Add(ModelItem item)
			{
				if (!base.Contains(item))
				{
					base.Add(item);
				}
			}

			// Token: 0x170003DF RID: 991
			ModelItem IOwnedModelItemCollection.this[string name]
			{
				get
				{
					throw new InternalModelingException("this[string name] is not supported.");
				}
			}

			// Token: 0x170003E0 RID: 992
			// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00031D37 File Offset: 0x0002FF37
			ModelItem IOwnedModelItemCollection.ParentItem
			{
				get
				{
					throw new InternalModelingException("ParentItem is not supported.");
				}
			}

			// Token: 0x06000FDC RID: 4060 RVA: 0x00031D43 File Offset: 0x0002FF43
			bool IOwnedModelItemCollection.CanContain(ModelItem item)
			{
				return item != null;
			}

			// Token: 0x06000FDD RID: 4061 RVA: 0x00031D4C File Offset: 0x0002FF4C
			bool IDeserializationCallback.ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
			{
				ModelItem modelItem = ctx.CurrentModel.TryResolveModelItemReference(reference, ctx.Validation);
				if (modelItem != null)
				{
					base.Add(modelItem);
				}
				return true;
			}

			// Token: 0x06000FDE RID: 4062 RVA: 0x00031D77 File Offset: 0x0002FF77
			internal Perspective.PerspectiveModelItemCollection CloneFor(Perspective newParentItem)
			{
				Perspective.PerspectiveModelItemCollection perspectiveModelItemCollection = new Perspective.PerspectiveModelItemCollection(newParentItem);
				perspectiveModelItemCollection.CopyFromBase(this);
				return perspectiveModelItemCollection;
			}

			// Token: 0x0400068B RID: 1675
			private static Declaration __declaration;

			// Token: 0x0400068C RID: 1676
			private static readonly object __declarationLock = new object();
		}
	}
}
