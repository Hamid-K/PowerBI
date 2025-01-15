using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000022 RID: 34
	internal abstract class EntityContainerRelationshipSet : SchemaElement
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x0000AFCA File Offset: 0x000091CA
		public EntityContainerRelationshipSet(EntityContainer parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0000AFD3 File Offset: 0x000091D3
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0000AFDB File Offset: 0x000091DB
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

		// Token: 0x06000646 RID: 1606
		protected abstract bool HasEnd(string role);

		// Token: 0x06000647 RID: 1607
		protected abstract void AddEnd(IRelationshipEnd relationshipEnd, EntityContainerEntitySet entitySet);

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000648 RID: 1608
		internal abstract IEnumerable<EntityContainerRelationshipSetEnd> Ends { get; }

		// Token: 0x06000649 RID: 1609 RVA: 0x0000AFE4 File Offset: 0x000091E4
		protected void HandleRelationshipTypeNameAttribute(XmlReader reader)
		{
			string unresolvedRelationshipTypeName = this._unresolvedRelationshipTypeName;
			Func<object, string> func;
			if ((func = EntityContainerRelationshipSet.<>O.<0>__PropertyTypeAlreadyDefined) == null)
			{
				func = (EntityContainerRelationshipSet.<>O.<0>__PropertyTypeAlreadyDefined = new Func<object, string>(Strings.PropertyTypeAlreadyDefined));
			}
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, unresolvedRelationshipTypeName, func);
			if (returnValue.Succeeded)
			{
				this._unresolvedRelationshipTypeName = returnValue.Value;
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0000B030 File Offset: 0x00009230
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

		// Token: 0x0600064B RID: 1611 RVA: 0x0000B0C8 File Offset: 0x000092C8
		internal override void ResolveSecondLevelNames()
		{
			base.ResolveSecondLevelNames();
			foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in this.Ends)
			{
				entityContainerRelationshipSetEnd.ResolveSecondLevelNames();
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000B118 File Offset: 0x00009318
		internal override void Validate()
		{
			base.Validate();
			this.InferEnds();
			foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in this.Ends)
			{
				entityContainerRelationshipSetEnd.Validate();
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000B170 File Offset: 0x00009370
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

		// Token: 0x0600064E RID: 1614 RVA: 0x0000B1DC File Offset: 0x000093DC
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

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0000B298 File Offset: 0x00009498
		internal new EntityContainer ParentElement
		{
			get
			{
				return (EntityContainer)base.ParentElement;
			}
		}

		// Token: 0x040005B6 RID: 1462
		private IRelationship _relationship;

		// Token: 0x040005B7 RID: 1463
		private string _unresolvedRelationshipTypeName;

		// Token: 0x02000299 RID: 665
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000F6A RID: 3946
			public static Func<object, string> <0>__PropertyTypeAlreadyDefined;
		}
	}
}
