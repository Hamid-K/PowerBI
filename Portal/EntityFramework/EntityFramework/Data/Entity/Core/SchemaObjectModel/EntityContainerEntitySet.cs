using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002EA RID: 746
	internal sealed class EntityContainerEntitySet : SchemaElement
	{
		// Token: 0x06002393 RID: 9107 RVA: 0x00064A05 File Offset: 0x00062C05
		public EntityContainerEntitySet(EntityContainer parentElement)
			: base(parentElement, null)
		{
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x00064A0F File Offset: 0x00062C0F
		public override string FQName
		{
			get
			{
				return base.ParentElement.Name + "." + this.Name;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x00064A2C File Offset: 0x00062C2C
		public SchemaEntityType EntityType
		{
			get
			{
				return this._entityType;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x00064A34 File Offset: 0x00062C34
		public string DbSchema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x00064A3C File Offset: 0x00062C3C
		public string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x00064A44 File Offset: 0x00062C44
		public string DefiningQuery
		{
			get
			{
				if (this._definingQueryElement != null)
				{
					return this._definingQueryElement.Query;
				}
				return null;
			}
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x00064A5C File Offset: 0x00062C5C
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				if (base.CanHandleElement(reader, "DefiningQuery"))
				{
					this.HandleDefiningQueryElement(reader);
					return true;
				}
			}
			else if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
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

		// Token: 0x0600239A RID: 9114 RVA: 0x00064AD8 File Offset: 0x00062CD8
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "EntityType"))
			{
				this.HandleEntityTypeAttribute(reader);
				return true;
			}
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				if (SchemaElement.CanHandleAttribute(reader, "Schema"))
				{
					this.HandleDbSchemaAttribute(reader);
					return true;
				}
				if (SchemaElement.CanHandleAttribute(reader, "Table"))
				{
					this.HandleTableAttribute(reader);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x00064B44 File Offset: 0x00062D44
		private void HandleDefiningQueryElement(XmlReader reader)
		{
			EntityContainerEntitySetDefiningQuery entityContainerEntitySetDefiningQuery = new EntityContainerEntitySetDefiningQuery(this);
			entityContainerEntitySetDefiningQuery.Parse(reader);
			this._definingQueryElement = entityContainerEntitySetDefiningQuery;
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x00064B66 File Offset: 0x00062D66
		protected override void HandleNameAttribute(XmlReader reader)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				this.Name = reader.Value;
				return;
			}
			base.HandleNameAttribute(reader);
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x00064B8C File Offset: 0x00062D8C
		private void HandleEntityTypeAttribute(XmlReader reader)
		{
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, this._unresolvedEntityTypeName);
			if (returnValue.Succeeded)
			{
				this._unresolvedEntityTypeName = returnValue.Value;
			}
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x00064BBB File Offset: 0x00062DBB
		private void HandleDbSchemaAttribute(XmlReader reader)
		{
			this._schema = reader.Value;
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x00064BC9 File Offset: 0x00062DC9
		private void HandleTableAttribute(XmlReader reader)
		{
			this._table = reader.Value;
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x00064BD8 File Offset: 0x00062DD8
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (this._entityType == null)
			{
				SchemaType schemaType = null;
				if (!base.Schema.ResolveTypeName(this, this._unresolvedEntityTypeName, out schemaType))
				{
					return;
				}
				this._entityType = schemaType as SchemaEntityType;
				if (this._entityType == null)
				{
					base.AddError(ErrorCode.InvalidPropertyType, EdmSchemaErrorSeverity.Error, Strings.InvalidEntitySetType(this._unresolvedEntityTypeName));
					return;
				}
			}
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x00064C38 File Offset: 0x00062E38
		internal override void Validate()
		{
			base.Validate();
			if (this._entityType.KeyProperties.Count == 0)
			{
				base.AddError(ErrorCode.EntitySetTypeHasNoKeys, EdmSchemaErrorSeverity.Error, Strings.EntitySetTypeHasNoKeys(this.Name, this._entityType.FQName));
			}
			if (this._definingQueryElement != null)
			{
				this._definingQueryElement.Validate();
				if (this.DbSchema != null || this.Table != null)
				{
					base.AddError(ErrorCode.TableAndSchemaAreMutuallyExclusiveWithDefiningQuery, EdmSchemaErrorSeverity.Error, Strings.TableAndSchemaAreMutuallyExclusiveWithDefiningQuery(this.FQName));
				}
			}
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x00064CBC File Offset: 0x00062EBC
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			return new EntityContainerEntitySet((EntityContainer)parentElement)
			{
				_definingQueryElement = this._definingQueryElement,
				_entityType = this._entityType,
				_schema = this._schema,
				_table = this._table,
				Name = this.Name
			};
		}

		// Token: 0x04000C23 RID: 3107
		private SchemaEntityType _entityType;

		// Token: 0x04000C24 RID: 3108
		private string _unresolvedEntityTypeName;

		// Token: 0x04000C25 RID: 3109
		private string _schema;

		// Token: 0x04000C26 RID: 3110
		private string _table;

		// Token: 0x04000C27 RID: 3111
		private EntityContainerEntitySetDefiningQuery _definingQueryElement;
	}
}
