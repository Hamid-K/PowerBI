using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Globalization;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002E7 RID: 743
	[DebuggerDisplay("Name={Name}")]
	internal sealed class EntityContainer : SchemaType
	{
		// Token: 0x06002366 RID: 9062 RVA: 0x00063C7C File Offset: 0x00061E7C
		public EntityContainer(Schema parentElement)
			: base(parentElement)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002367 RID: 9063 RVA: 0x00063CA8 File Offset: 0x00061EA8
		private SchemaElementLookUpTable<SchemaElement> Members
		{
			get
			{
				if (this._members == null)
				{
					this._members = new SchemaElementLookUpTable<SchemaElement>();
				}
				return this._members;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x00063CC3 File Offset: 0x00061EC3
		public ISchemaElementLookUpTable<EntityContainerEntitySet> EntitySets
		{
			get
			{
				if (this._entitySets == null)
				{
					this._entitySets = new FilteredSchemaElementLookUpTable<EntityContainerEntitySet, SchemaElement>(this.Members);
				}
				return this._entitySets;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x00063CE4 File Offset: 0x00061EE4
		public ISchemaElementLookUpTable<EntityContainerRelationshipSet> RelationshipSets
		{
			get
			{
				if (this._relationshipSets == null)
				{
					this._relationshipSets = new FilteredSchemaElementLookUpTable<EntityContainerRelationshipSet, SchemaElement>(this.Members);
				}
				return this._relationshipSets;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x00063D05 File Offset: 0x00061F05
		public ISchemaElementLookUpTable<Function> FunctionImports
		{
			get
			{
				if (this._functionImports == null)
				{
					this._functionImports = new FilteredSchemaElementLookUpTable<Function, SchemaElement>(this.Members);
				}
				return this._functionImports;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x00063D26 File Offset: 0x00061F26
		public EntityContainer ExtendingEntityContainer
		{
			get
			{
				return this._entityContainerGettingExtended;
			}
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x00063D2E File Offset: 0x00061F2E
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Extends"))
			{
				this.HandleExtendsAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x00063D54 File Offset: 0x00061F54
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "EntitySet"))
			{
				this.HandleEntitySetElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "AssociationSet"))
			{
				this.HandleAssociationSetElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "FunctionImport"))
			{
				this.HandleFunctionImport(reader);
				return true;
			}
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				if (base.CanHandleElement(reader, "ValueAnnotation"))
				{
					this.SkipElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "TypeAnnotation"))
				{
					this.SkipElement(reader);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x00063DF0 File Offset: 0x00061FF0
		private void HandleEntitySetElement(XmlReader reader)
		{
			EntityContainerEntitySet entityContainerEntitySet = new EntityContainerEntitySet(this);
			entityContainerEntitySet.Parse(reader);
			this.Members.Add(entityContainerEntitySet, true, new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x00063E24 File Offset: 0x00062024
		private void HandleAssociationSetElement(XmlReader reader)
		{
			EntityContainerAssociationSet entityContainerAssociationSet = new EntityContainerAssociationSet(this);
			entityContainerAssociationSet.Parse(reader);
			this.Members.Add(entityContainerAssociationSet, true, new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x00063E58 File Offset: 0x00062058
		private void HandleFunctionImport(XmlReader reader)
		{
			FunctionImportElement functionImportElement = new FunctionImportElement(this);
			functionImportElement.Parse(reader);
			this.Members.Add(functionImportElement, true, new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x00063E8C File Offset: 0x0006208C
		private void HandleExtendsAttribute(XmlReader reader)
		{
			this._unresolvedExtendedEntityContainerName = base.HandleUndottedNameAttribute(reader, this._unresolvedExtendedEntityContainerName);
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x00063EA4 File Offset: 0x000620A4
		internal override void ResolveTopLevelNames()
		{
			if (!this._isAlreadyResolved)
			{
				base.ResolveTopLevelNames();
				if (!string.IsNullOrEmpty(this._unresolvedExtendedEntityContainerName))
				{
					SchemaType schemaType;
					if (this._unresolvedExtendedEntityContainerName == this.Name)
					{
						base.AddError(ErrorCode.EntityContainerCannotExtendItself, EdmSchemaErrorSeverity.Error, Strings.EntityContainerCannotExtendItself(this.Name));
					}
					else if (!base.Schema.SchemaManager.TryResolveType(null, this._unresolvedExtendedEntityContainerName, out schemaType))
					{
						base.AddError(ErrorCode.InvalidEntityContainerNameInExtends, EdmSchemaErrorSeverity.Error, Strings.InvalidEntityContainerNameInExtends(this._unresolvedExtendedEntityContainerName));
					}
					else
					{
						this._entityContainerGettingExtended = (EntityContainer)schemaType;
						this._entityContainerGettingExtended.ResolveTopLevelNames();
					}
				}
				foreach (SchemaElement schemaElement in this.Members)
				{
					schemaElement.ResolveTopLevelNames();
				}
				this._isAlreadyResolved = true;
			}
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x00063F8C File Offset: 0x0006218C
		internal override void ResolveSecondLevelNames()
		{
			base.ResolveSecondLevelNames();
			foreach (SchemaElement schemaElement in this.Members)
			{
				schemaElement.ResolveSecondLevelNames();
			}
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x00063FDC File Offset: 0x000621DC
		internal override void Validate()
		{
			if (!this._isAlreadyValidated)
			{
				base.Validate();
				if (this.ExtendingEntityContainer != null)
				{
					this.ExtendingEntityContainer.Validate();
					foreach (SchemaElement schemaElement in this.ExtendingEntityContainer.Members)
					{
						AddErrorKind addErrorKind = this.Members.TryAdd(schemaElement.Clone(this));
						this.DuplicateOrEquivalentMemberNameWhileExtendingEntityContainer(schemaElement, addErrorKind);
					}
				}
				HashSet<string> hashSet = new HashSet<string>();
				foreach (SchemaElement schemaElement2 in this.Members)
				{
					EntityContainerEntitySet entityContainerEntitySet = schemaElement2 as EntityContainerEntitySet;
					if (entityContainerEntitySet != null && base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
					{
						this.CheckForDuplicateTableMapping(hashSet, entityContainerEntitySet);
					}
					schemaElement2.Validate();
				}
				this.ValidateRelationshipSetHaveUniqueEnds();
				this.ValidateOnlyBaseEntitySetTypeDefinesConcurrency();
				this._isAlreadyValidated = true;
			}
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000640DC File Offset: 0x000622DC
		internal EntityContainerEntitySet FindEntitySet(string name)
		{
			for (EntityContainer entityContainer = this; entityContainer != null; entityContainer = entityContainer.ExtendingEntityContainer)
			{
				foreach (EntityContainerEntitySet entityContainerEntitySet in entityContainer.EntitySets)
				{
					if (Utils.CompareNames(entityContainerEntitySet.Name, name) == 0)
					{
						return entityContainerEntitySet;
					}
				}
			}
			return null;
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x00064148 File Offset: 0x00062348
		private void DuplicateOrEquivalentMemberNameWhileExtendingEntityContainer(SchemaElement schemaElement, AddErrorKind error)
		{
			if (error != AddErrorKind.Succeeded)
			{
				schemaElement.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.DuplicateMemberNameInExtendedEntityContainer(schemaElement.Name, this.ExtendingEntityContainer.Name, this.Name));
			}
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x00064174 File Offset: 0x00062374
		private void ValidateOnlyBaseEntitySetTypeDefinesConcurrency()
		{
			Dictionary<SchemaEntityType, EntityContainerEntitySet> dictionary = new Dictionary<SchemaEntityType, EntityContainerEntitySet>();
			foreach (SchemaElement schemaElement in this.Members)
			{
				EntityContainerEntitySet entityContainerEntitySet = schemaElement as EntityContainerEntitySet;
				if (entityContainerEntitySet != null && !dictionary.ContainsKey(entityContainerEntitySet.EntityType))
				{
					dictionary.Add(entityContainerEntitySet.EntityType, entityContainerEntitySet);
				}
			}
			foreach (SchemaType schemaType in base.Schema.SchemaTypes)
			{
				SchemaEntityType schemaEntityType = schemaType as SchemaEntityType;
				EntityContainerEntitySet entityContainerEntitySet2;
				if (schemaEntityType != null && EntityContainer.TypeIsSubTypeOf(schemaEntityType, dictionary, out entityContainerEntitySet2) && EntityContainer.TypeDefinesNewConcurrencyProperties(schemaEntityType))
				{
					base.AddError(ErrorCode.ConcurrencyRedefinedOnSubTypeOfEntitySetType, EdmSchemaErrorSeverity.Error, Strings.ConcurrencyRedefinedOnSubTypeOfEntitySetType(schemaEntityType.FQName, entityContainerEntitySet2.EntityType.FQName, entityContainerEntitySet2.FQName));
				}
			}
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x00064270 File Offset: 0x00062470
		private void ValidateRelationshipSetHaveUniqueEnds()
		{
			List<EntityContainerRelationshipSetEnd> list = new List<EntityContainerRelationshipSetEnd>();
			bool flag = true;
			foreach (EntityContainerRelationshipSet entityContainerRelationshipSet in this.RelationshipSets)
			{
				foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in entityContainerRelationshipSet.Ends)
				{
					flag = false;
					foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd2 in list)
					{
						if (EntityContainer.AreRelationshipEndsEqual(entityContainerRelationshipSetEnd2, entityContainerRelationshipSetEnd))
						{
							base.AddError(ErrorCode.SimilarRelationshipEnd, EdmSchemaErrorSeverity.Error, Strings.SimilarRelationshipEnd(entityContainerRelationshipSetEnd2.Name, entityContainerRelationshipSetEnd2.ParentElement.Name, entityContainerRelationshipSetEnd.ParentElement.Name, entityContainerRelationshipSetEnd2.EntitySet.Name, this.FQName));
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						list.Add(entityContainerRelationshipSetEnd);
					}
				}
			}
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x00064394 File Offset: 0x00062594
		private static bool TypeIsSubTypeOf(SchemaEntityType itemType, Dictionary<SchemaEntityType, EntityContainerEntitySet> baseEntitySetTypes, out EntityContainerEntitySet set)
		{
			if (itemType.IsTypeHierarchyRoot)
			{
				set = null;
				return false;
			}
			for (SchemaEntityType schemaEntityType = itemType.BaseType as SchemaEntityType; schemaEntityType != null; schemaEntityType = schemaEntityType.BaseType as SchemaEntityType)
			{
				if (baseEntitySetTypes.ContainsKey(schemaEntityType))
				{
					set = baseEntitySetTypes[schemaEntityType];
					return true;
				}
			}
			set = null;
			return false;
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000643E4 File Offset: 0x000625E4
		private static bool TypeDefinesNewConcurrencyProperties(SchemaEntityType itemType)
		{
			foreach (StructuredProperty structuredProperty in itemType.Properties)
			{
				if (structuredProperty.Type is ScalarType && MetadataHelper.GetConcurrencyMode(structuredProperty.TypeUsage) != ConcurrencyMode.None)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x0006444C File Offset: 0x0006264C
		public override string FQName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x00064454 File Offset: 0x00062654
		public override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x0006445C File Offset: 0x0006265C
		private void CheckForDuplicateTableMapping(HashSet<string> tableKeys, EntityContainerEntitySet entitySet)
		{
			string text;
			if (string.IsNullOrEmpty(entitySet.DbSchema))
			{
				text = this.Name;
			}
			else
			{
				text = entitySet.DbSchema;
			}
			string text2;
			if (string.IsNullOrEmpty(entitySet.Table))
			{
				text2 = entitySet.Name;
			}
			else
			{
				text2 = entitySet.Table;
			}
			string text3 = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[] { text, text2 });
			if (entitySet.DefiningQuery != null)
			{
				text3 = entitySet.Name;
			}
			if (!tableKeys.Add(text3))
			{
				entitySet.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.DuplicateEntitySetTable(entitySet.Name, text, text2));
			}
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000644F2 File Offset: 0x000626F2
		private static bool AreRelationshipEndsEqual(EntityContainerRelationshipSetEnd left, EntityContainerRelationshipSetEnd right)
		{
			return left.EntitySet == right.EntitySet && left.ParentElement.Relationship == right.ParentElement.Relationship && left.Name == right.Name;
		}

		// Token: 0x04000C18 RID: 3096
		private SchemaElementLookUpTable<SchemaElement> _members;

		// Token: 0x04000C19 RID: 3097
		private ISchemaElementLookUpTable<EntityContainerEntitySet> _entitySets;

		// Token: 0x04000C1A RID: 3098
		private ISchemaElementLookUpTable<EntityContainerRelationshipSet> _relationshipSets;

		// Token: 0x04000C1B RID: 3099
		private ISchemaElementLookUpTable<Function> _functionImports;

		// Token: 0x04000C1C RID: 3100
		private string _unresolvedExtendedEntityContainerName;

		// Token: 0x04000C1D RID: 3101
		private EntityContainer _entityContainerGettingExtended;

		// Token: 0x04000C1E RID: 3102
		private bool _isAlreadyValidated;

		// Token: 0x04000C1F RID: 3103
		private bool _isAlreadyResolved;
	}
}
