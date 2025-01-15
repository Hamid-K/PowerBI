using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200001D RID: 29
	[DebuggerDisplay("Name={Name}")]
	internal sealed class EntityContainer : SchemaType
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x00009F58 File Offset: 0x00008158
		public EntityContainer(Schema parentElement)
			: base(parentElement)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00009F84 File Offset: 0x00008184
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

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00009F9F File Offset: 0x0000819F
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

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00009FC0 File Offset: 0x000081C0
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

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00009FE1 File Offset: 0x000081E1
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

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000A002 File Offset: 0x00008202
		public EntityContainer ExtendingEntityContainer
		{
			get
			{
				return this._entityContainerGettingExtended;
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0000A00A File Offset: 0x0000820A
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

		// Token: 0x0600060A RID: 1546 RVA: 0x0000A030 File Offset: 0x00008230
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
			return false;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0000A090 File Offset: 0x00008290
		private void HandleEntitySetElement(XmlReader reader)
		{
			EntityContainerEntitySet entityContainerEntitySet = new EntityContainerEntitySet(this);
			entityContainerEntitySet.Parse(reader);
			SchemaElementLookUpTable<SchemaElement> members = this.Members;
			SchemaElement schemaElement = entityContainerEntitySet;
			bool flag = true;
			Func<object, string> func;
			if ((func = EntityContainer.<>O.<0>__DuplicateEntityContainerMemberName) == null)
			{
				func = (EntityContainer.<>O.<0>__DuplicateEntityContainerMemberName = new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
			}
			members.Add(schemaElement, flag, func);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0000A0D4 File Offset: 0x000082D4
		private void HandleAssociationSetElement(XmlReader reader)
		{
			EntityContainerAssociationSet entityContainerAssociationSet = new EntityContainerAssociationSet(this);
			entityContainerAssociationSet.Parse(reader);
			SchemaElementLookUpTable<SchemaElement> members = this.Members;
			SchemaElement schemaElement = entityContainerAssociationSet;
			bool flag = true;
			Func<object, string> func;
			if ((func = EntityContainer.<>O.<0>__DuplicateEntityContainerMemberName) == null)
			{
				func = (EntityContainer.<>O.<0>__DuplicateEntityContainerMemberName = new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
			}
			members.Add(schemaElement, flag, func);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0000A118 File Offset: 0x00008318
		private void HandleFunctionImport(XmlReader reader)
		{
			FunctionImportElement functionImportElement = new FunctionImportElement(this);
			functionImportElement.Parse(reader);
			SchemaElementLookUpTable<SchemaElement> members = this.Members;
			SchemaElement schemaElement = functionImportElement;
			bool flag = true;
			Func<object, string> func;
			if ((func = EntityContainer.<>O.<0>__DuplicateEntityContainerMemberName) == null)
			{
				func = (EntityContainer.<>O.<0>__DuplicateEntityContainerMemberName = new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
			}
			members.Add(schemaElement, flag, func);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0000A15B File Offset: 0x0000835B
		private void HandleExtendsAttribute(XmlReader reader)
		{
			this._unresolvedExtendedEntityContainerName = base.HandleUndottedNameAttribute(reader, this._unresolvedExtendedEntityContainerName);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0000A170 File Offset: 0x00008370
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

		// Token: 0x06000610 RID: 1552 RVA: 0x0000A258 File Offset: 0x00008458
		internal override void ResolveSecondLevelNames()
		{
			base.ResolveSecondLevelNames();
			foreach (SchemaElement schemaElement in this.Members)
			{
				schemaElement.ResolveSecondLevelNames();
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0000A2A8 File Offset: 0x000084A8
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

		// Token: 0x06000612 RID: 1554 RVA: 0x0000A3A8 File Offset: 0x000085A8
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

		// Token: 0x06000613 RID: 1555 RVA: 0x0000A414 File Offset: 0x00008614
		private void DuplicateOrEquivalentMemberNameWhileExtendingEntityContainer(SchemaElement schemaElement, AddErrorKind error)
		{
			if (error != AddErrorKind.Succeeded)
			{
				schemaElement.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.DuplicateMemberNameInExtendedEntityContainer(schemaElement.Name, this.ExtendingEntityContainer.Name, this.Name));
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0000A440 File Offset: 0x00008640
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

		// Token: 0x06000615 RID: 1557 RVA: 0x0000A53C File Offset: 0x0000873C
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
							base.AddError(ErrorCode.SimilarRelationshipEnd, EdmSchemaErrorSeverity.Error, Strings.SimilarRelationshipEnd(entityContainerRelationshipSetEnd2.Name, entityContainerRelationshipSetEnd2.ParentElement.FQName, entityContainerRelationshipSetEnd.ParentElement.FQName, entityContainerRelationshipSetEnd2.EntitySet.FQName, this.FQName));
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

		// Token: 0x06000616 RID: 1558 RVA: 0x0000A660 File Offset: 0x00008860
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

		// Token: 0x06000617 RID: 1559 RVA: 0x0000A6B0 File Offset: 0x000088B0
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

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0000A718 File Offset: 0x00008918
		public override string FQName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0000A720 File Offset: 0x00008920
		public override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0000A728 File Offset: 0x00008928
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
			string text3 = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", text, text2);
			if (entitySet.DefiningQuery != null)
			{
				text3 = entitySet.Name;
			}
			if (!tableKeys.Add(text3))
			{
				entitySet.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.DuplicateEntitySetTable(entitySet.Name, text, text2));
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0000A7B2 File Offset: 0x000089B2
		private static bool AreRelationshipEndsEqual(EntityContainerRelationshipSetEnd left, EntityContainerRelationshipSetEnd right)
		{
			return left.EntitySet == right.EntitySet && left.ParentElement.Relationship == right.ParentElement.Relationship && left.Name == right.Name;
		}

		// Token: 0x040005A5 RID: 1445
		private SchemaElementLookUpTable<SchemaElement> _members;

		// Token: 0x040005A6 RID: 1446
		private ISchemaElementLookUpTable<EntityContainerEntitySet> _entitySets;

		// Token: 0x040005A7 RID: 1447
		private ISchemaElementLookUpTable<EntityContainerRelationshipSet> _relationshipSets;

		// Token: 0x040005A8 RID: 1448
		private ISchemaElementLookUpTable<Function> _functionImports;

		// Token: 0x040005A9 RID: 1449
		private string _unresolvedExtendedEntityContainerName;

		// Token: 0x040005AA RID: 1450
		private EntityContainer _entityContainerGettingExtended;

		// Token: 0x040005AB RID: 1451
		private bool _isAlreadyValidated;

		// Token: 0x040005AC RID: 1452
		private bool _isAlreadyResolved;

		// Token: 0x02000296 RID: 662
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000F62 RID: 3938
			public static Func<object, string> <0>__DuplicateEntityContainerMemberName;
		}
	}
}
