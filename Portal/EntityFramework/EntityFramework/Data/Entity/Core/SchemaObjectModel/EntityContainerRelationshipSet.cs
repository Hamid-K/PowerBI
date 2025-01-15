using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002EC RID: 748
	internal abstract class EntityContainerRelationshipSet : SchemaElement
	{
		// Token: 0x060023A7 RID: 9127 RVA: 0x00064D57 File Offset: 0x00062F57
		public EntityContainerRelationshipSet(EntityContainer parentElement)
			: base(parentElement, null)
		{
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x00064D61 File Offset: 0x00062F61
		public override string FQName
		{
			get
			{
				return this.ParentElement.Name + "." + this.Name;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060023A9 RID: 9129 RVA: 0x00064D7E File Offset: 0x00062F7E
		// (set) Token: 0x060023AA RID: 9130 RVA: 0x00064D86 File Offset: 0x00062F86
		internal IRelationship Relationship
		{
			get
			{
				return this._relationship;
			}
			set
			{
				this._relationship = value;
			}
		}

		// Token: 0x060023AB RID: 9131
		protected abstract bool HasEnd(string role);

		// Token: 0x060023AC RID: 9132
		protected abstract void AddEnd(IRelationshipEnd relationshipEnd, EntityContainerEntitySet entitySet);

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060023AD RID: 9133
		internal abstract IEnumerable<EntityContainerRelationshipSetEnd> Ends { get; }

		// Token: 0x060023AE RID: 9134 RVA: 0x00064D90 File Offset: 0x00062F90
		protected void HandleRelationshipTypeNameAttribute(XmlReader reader)
		{
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, this._unresolvedRelationshipTypeName);
			if (returnValue.Succeeded)
			{
				this._unresolvedRelationshipTypeName = returnValue.Value;
			}
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x00064DC0 File Offset: 0x00062FC0
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (this._relationship == null)
			{
				SchemaType schemaType;
				if (!base.Schema.ResolveTypeName(this, this._unresolvedRelationshipTypeName, out schemaType))
				{
					return;
				}
				this._relationship = schemaType as IRelationship;
				if (this._relationship == null)
				{
					base.AddError(ErrorCode.InvalidPropertyType, EdmSchemaErrorSeverity.Error, Strings.InvalidRelationshipSetType(schemaType.Name));
					return;
				}
			}
			foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in this.Ends)
			{
				entityContainerRelationshipSetEnd.ResolveTopLevelNames();
			}
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x00064E58 File Offset: 0x00063058
		internal override void ResolveSecondLevelNames()
		{
			base.ResolveSecondLevelNames();
			foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in this.Ends)
			{
				entityContainerRelationshipSetEnd.ResolveSecondLevelNames();
			}
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x00064EA8 File Offset: 0x000630A8
		internal override void Validate()
		{
			base.Validate();
			this.InferEnds();
			foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in this.Ends)
			{
				entityContainerRelationshipSetEnd.Validate();
			}
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x00064F00 File Offset: 0x00063100
		private void InferEnds()
		{
			foreach (IRelationshipEnd relationshipEnd in this.Relationship.Ends)
			{
				if (!this.HasEnd(relationshipEnd.Name))
				{
					EntityContainerEntitySet entityContainerEntitySet = this.InferEntitySet(relationshipEnd);
					if (entityContainerEntitySet != null)
					{
						this.AddEnd(relationshipEnd, entityContainerEntitySet);
					}
				}
			}
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x00064F6C File Offset: 0x0006316C
		private EntityContainerEntitySet InferEntitySet(IRelationshipEnd relationshipEnd)
		{
			List<EntityContainerEntitySet> list = new List<EntityContainerEntitySet>();
			foreach (EntityContainerEntitySet entityContainerEntitySet in this.ParentElement.EntitySets)
			{
				if (relationshipEnd.Type.IsOfType(entityContainerEntitySet.EntityType))
				{
					list.Add(entityContainerEntitySet);
				}
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			if (list.Count == 0)
			{
				base.AddError(ErrorCode.MissingExtentEntityContainerEnd, EdmSchemaErrorSeverity.Error, Strings.MissingEntityContainerEnd(relationshipEnd.Name, this.FQName));
			}
			else
			{
				base.AddError(ErrorCode.AmbiguousEntityContainerEnd, EdmSchemaErrorSeverity.Error, Strings.AmbiguousEntityContainerEnd(relationshipEnd.Name, this.FQName));
			}
			return null;
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x00065028 File Offset: 0x00063228
		internal new EntityContainer ParentElement
		{
			get
			{
				return (EntityContainer)base.ParentElement;
			}
		}

		// Token: 0x04000C29 RID: 3113
		private IRelationship _relationship;

		// Token: 0x04000C2A RID: 3114
		private string _unresolvedRelationshipTypeName;
	}
}
