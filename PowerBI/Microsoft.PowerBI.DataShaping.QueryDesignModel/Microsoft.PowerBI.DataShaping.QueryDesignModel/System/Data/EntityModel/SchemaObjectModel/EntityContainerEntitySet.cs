using System;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000020 RID: 32
	internal sealed class EntityContainerEntitySet : SchemaElement
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x0000ACC5 File Offset: 0x00008EC5
		public EntityContainerEntitySet(EntityContainer parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0000ACCE File Offset: 0x00008ECE
		public SchemaEntityType EntityType
		{
			get
			{
				return this._entityType;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0000ACD6 File Offset: 0x00008ED6
		public string DbSchema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0000ACDE File Offset: 0x00008EDE
		public string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0000ACE6 File Offset: 0x00008EE6
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

		// Token: 0x06000635 RID: 1589 RVA: 0x0000ACFD File Offset: 0x00008EFD
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel && base.CanHandleElement(reader, "DefiningQuery"))
			{
				this.HandleDefiningQueryElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000AD30 File Offset: 0x00008F30
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

		// Token: 0x06000637 RID: 1591 RVA: 0x0000AD9C File Offset: 0x00008F9C
		private void HandleDefiningQueryElement(XmlReader reader)
		{
			EntityContainerEntitySetDefiningQuery entityContainerEntitySetDefiningQuery = new EntityContainerEntitySetDefiningQuery(this);
			entityContainerEntitySetDefiningQuery.Parse(reader);
			this._definingQueryElement = entityContainerEntitySetDefiningQuery;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0000ADBE File Offset: 0x00008FBE
		protected override void HandleNameAttribute(XmlReader reader)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				this.Name = reader.Value;
				return;
			}
			base.HandleNameAttribute(reader);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0000ADE4 File Offset: 0x00008FE4
		private void HandleEntityTypeAttribute(XmlReader reader)
		{
			string unresolvedEntityTypeName = this._unresolvedEntityTypeName;
			Func<object, string> func;
			if ((func = EntityContainerEntitySet.<>O.<0>__PropertyTypeAlreadyDefined) == null)
			{
				func = (EntityContainerEntitySet.<>O.<0>__PropertyTypeAlreadyDefined = new Func<object, string>(Strings.PropertyTypeAlreadyDefined));
			}
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, unresolvedEntityTypeName, func);
			if (returnValue.Succeeded)
			{
				this._unresolvedEntityTypeName = returnValue.Value;
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000AE2E File Offset: 0x0000902E
		private void HandleDbSchemaAttribute(XmlReader reader)
		{
			this._schema = reader.Value;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0000AE3C File Offset: 0x0000903C
		private void HandleTableAttribute(XmlReader reader)
		{
			this._table = reader.Value;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0000AE4C File Offset: 0x0000904C
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

		// Token: 0x0600063D RID: 1597 RVA: 0x0000AEAC File Offset: 0x000090AC
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

		// Token: 0x0600063E RID: 1598 RVA: 0x0000AF30 File Offset: 0x00009130
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

		// Token: 0x040005B0 RID: 1456
		private SchemaEntityType _entityType;

		// Token: 0x040005B1 RID: 1457
		private string _unresolvedEntityTypeName;

		// Token: 0x040005B2 RID: 1458
		private string _schema;

		// Token: 0x040005B3 RID: 1459
		private string _table;

		// Token: 0x040005B4 RID: 1460
		private EntityContainerEntitySetDefiningQuery _definingQueryElement;

		// Token: 0x02000298 RID: 664
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000F69 RID: 3945
			public static Func<object, string> <0>__PropertyTypeAlreadyDefined;
		}
	}
}
