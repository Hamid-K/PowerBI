using System;
using System.Collections.Generic;
using System.Data.Entity.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B3 RID: 1203
	internal sealed class EdmSerializationVisitor : EdmModelVisitor
	{
		// Token: 0x06003B23 RID: 15139 RVA: 0x000C35C3 File Offset: 0x000C17C3
		public EdmSerializationVisitor(XmlWriter xmlWriter, double edmVersion, bool serializeDefaultNullability = false)
			: this(new EdmXmlSchemaWriter(xmlWriter, edmVersion, serializeDefaultNullability, null))
		{
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x000C35D4 File Offset: 0x000C17D4
		public EdmSerializationVisitor(EdmXmlSchemaWriter schemaWriter)
		{
			this._schemaWriter = schemaWriter;
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x000C35E4 File Offset: 0x000C17E4
		public void Visit(EdmModel edmModel, string modelNamespace)
		{
			string text = modelNamespace ?? edmModel.NamespaceNames.DefaultIfEmpty("Empty").Single<string>();
			this._schemaWriter.WriteSchemaElementHeader(text);
			this.VisitEdmModel(edmModel);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x000C362A File Offset: 0x000C182A
		public void Visit(EdmModel edmModel, string provider, string providerManifestToken)
		{
			this.Visit(edmModel, edmModel.Containers.Single<EntityContainer>().Name + "Schema", provider, providerManifestToken);
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x000C3650 File Offset: 0x000C1850
		public void Visit(EdmModel edmModel, string namespaceName, string provider, string providerManifestToken)
		{
			bool flag = edmModel.Container.BaseEntitySets.Any((EntitySetBase e) => e.MetadataProperties.Any((MetadataProperty p) => p.Name.StartsWith("http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator", StringComparison.Ordinal)));
			this._schemaWriter.WriteSchemaElementHeader(namespaceName, provider, providerManifestToken, flag);
			this.VisitEdmModel(edmModel);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x000C36AF File Offset: 0x000C18AF
		protected override void VisitEdmEntityContainer(EntityContainer item)
		{
			this._schemaWriter.WriteEntityContainerElementHeader(item);
			base.VisitEdmEntityContainer(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x000C36CF File Offset: 0x000C18CF
		protected internal override void VisitEdmFunction(EdmFunction item)
		{
			this._schemaWriter.WriteFunctionElementHeader(item);
			base.VisitEdmFunction(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x000C36EF File Offset: 0x000C18EF
		protected internal override void VisitFunctionParameter(FunctionParameter functionParameter)
		{
			this._schemaWriter.WriteFunctionParameterHeader(functionParameter);
			base.VisitFunctionParameter(functionParameter);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x000C370F File Offset: 0x000C190F
		protected internal override void VisitFunctionReturnParameter(FunctionParameter returnParameter)
		{
			if (returnParameter.TypeUsage.EdmType.BuiltInTypeKind != BuiltInTypeKind.PrimitiveType)
			{
				this._schemaWriter.WriteFunctionReturnTypeElementHeader();
				base.VisitFunctionReturnParameter(returnParameter);
				this._schemaWriter.WriteEndElement();
				return;
			}
			base.VisitFunctionReturnParameter(returnParameter);
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x000C374A File Offset: 0x000C194A
		protected internal override void VisitCollectionType(CollectionType collectionType)
		{
			this._schemaWriter.WriteCollectionTypeElementHeader();
			base.VisitCollectionType(collectionType);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x000C376C File Offset: 0x000C196C
		protected override void VisitEdmAssociationSet(AssociationSet item)
		{
			this._schemaWriter.WriteAssociationSetElementHeader(item);
			base.VisitEdmAssociationSet(item);
			if (item.SourceSet != null)
			{
				this._schemaWriter.WriteAssociationSetEndElement(item.SourceSet, item.SourceEnd.Name);
			}
			if (item.TargetSet != null)
			{
				this._schemaWriter.WriteAssociationSetEndElement(item.TargetSet, item.TargetEnd.Name);
			}
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x000C37DF File Offset: 0x000C19DF
		protected internal override void VisitEdmEntitySet(EntitySet item)
		{
			this._schemaWriter.WriteEntitySetElementHeader(item);
			this._schemaWriter.WriteDefiningQuery(item);
			base.VisitEdmEntitySet(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000C380C File Offset: 0x000C1A0C
		protected internal override void VisitFunctionImport(EdmFunction functionImport)
		{
			this._schemaWriter.WriteFunctionImportElementHeader(functionImport);
			if (functionImport.ReturnParameters.Count == 1)
			{
				this._schemaWriter.WriteFunctionImportReturnTypeAttributes(functionImport.ReturnParameter, functionImport.EntitySet, true);
				this.VisitFunctionImportReturnParameter(functionImport.ReturnParameter);
			}
			base.VisitFunctionImport(functionImport);
			if (functionImport.ReturnParameters.Count > 1)
			{
				this.VisitFunctionImportReturnParameters(functionImport);
			}
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x000C387E File Offset: 0x000C1A7E
		protected internal override void VisitFunctionImportParameter(FunctionParameter parameter)
		{
			this._schemaWriter.WriteFunctionImportParameterElementHeader(parameter);
			base.VisitFunctionImportParameter(parameter);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x000C38A0 File Offset: 0x000C1AA0
		private void VisitFunctionImportReturnParameters(EdmFunction functionImport)
		{
			for (int i = 0; i < functionImport.ReturnParameters.Count; i++)
			{
				this._schemaWriter.WriteFunctionReturnTypeElementHeader();
				this._schemaWriter.WriteFunctionImportReturnTypeAttributes(functionImport.ReturnParameters[i], functionImport.EntitySets[i], false);
				this.VisitFunctionImportReturnParameter(functionImport.ReturnParameter);
				this._schemaWriter.WriteEndElement();
			}
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000C3909 File Offset: 0x000C1B09
		protected internal override void VisitRowType(RowType rowType)
		{
			this._schemaWriter.WriteRowTypeElementHeader();
			base.VisitRowType(rowType);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x000C3928 File Offset: 0x000C1B28
		protected internal override void VisitEdmEntityType(EntityType item)
		{
			StringBuilder stringBuilder = new StringBuilder();
			EdmSerializationVisitor.AppendSchemaErrors(stringBuilder, item);
			if (MetadataItemHelper.IsInvalid(item))
			{
				this.AppendMetadataItem<EntityType>(stringBuilder, item, delegate(EdmSerializationVisitor v, EntityType i)
				{
					v.InternalVisitEdmEntityType(i);
				});
				this.WriteComment(stringBuilder.ToString());
				return;
			}
			this.WriteComment(stringBuilder.ToString());
			this.InternalVisitEdmEntityType(item);
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x000C3991 File Offset: 0x000C1B91
		protected override void VisitEdmEnumType(EnumType item)
		{
			this._schemaWriter.WriteEnumTypeElementHeader(item);
			base.VisitEdmEnumType(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x000C39B1 File Offset: 0x000C1BB1
		protected override void VisitEdmEnumTypeMember(EnumMember item)
		{
			this._schemaWriter.WriteEnumTypeMemberElementHeader(item);
			base.VisitEdmEnumTypeMember(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x000C39D4 File Offset: 0x000C1BD4
		protected override void VisitKeyProperties(EntityType entityType, IList<EdmProperty> properties)
		{
			if (properties.Any<EdmProperty>())
			{
				this._schemaWriter.WriteDeclaredKeyPropertiesElementHeader();
				foreach (EdmProperty edmProperty in properties)
				{
					this._schemaWriter.WriteDeclaredKeyPropertyRefElement(edmProperty);
				}
				this._schemaWriter.WriteEndElement();
			}
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x000C3A40 File Offset: 0x000C1C40
		protected internal override void VisitEdmProperty(EdmProperty item)
		{
			this._schemaWriter.WritePropertyElementHeader(item);
			base.VisitEdmProperty(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x000C3A60 File Offset: 0x000C1C60
		protected override void VisitEdmNavigationProperty(NavigationProperty item)
		{
			this._schemaWriter.WriteNavigationPropertyElementHeader(item);
			base.VisitEdmNavigationProperty(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x000C3A80 File Offset: 0x000C1C80
		protected override void VisitComplexType(ComplexType item)
		{
			this._schemaWriter.WriteComplexTypeElementHeader(item);
			base.VisitComplexType(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x000C3AA0 File Offset: 0x000C1CA0
		protected internal override void VisitEdmAssociationType(AssociationType item)
		{
			StringBuilder stringBuilder = new StringBuilder();
			EdmSerializationVisitor.AppendSchemaErrors(stringBuilder, item);
			if (MetadataItemHelper.IsInvalid(item))
			{
				this.AppendMetadataItem<AssociationType>(stringBuilder, item, delegate(EdmSerializationVisitor v, AssociationType i)
				{
					v.InternalVisitEdmAssociationType(i);
				});
				this.WriteComment(stringBuilder.ToString());
				return;
			}
			this.WriteComment(stringBuilder.ToString());
			this.InternalVisitEdmAssociationType(item);
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x000C3B09 File Offset: 0x000C1D09
		protected override void VisitEdmAssociationEnd(RelationshipEndMember item)
		{
			this._schemaWriter.WriteAssociationEndElementHeader(item);
			if (item.DeleteBehavior != OperationAction.None)
			{
				this._schemaWriter.WriteOperationActionElement("OnDelete", item.DeleteBehavior);
			}
			this.VisitMetadataItem(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x000C3B48 File Offset: 0x000C1D48
		protected override void VisitEdmAssociationConstraint(ReferentialConstraint item)
		{
			this._schemaWriter.WriteReferentialConstraintElementHeader();
			this._schemaWriter.WriteReferentialConstraintRoleElement("Principal", item.FromRole, item.FromProperties);
			this._schemaWriter.WriteReferentialConstraintRoleElement("Dependent", item.ToRole, item.ToProperties);
			this.VisitMetadataItem(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x000C3BAA File Offset: 0x000C1DAA
		private void InternalVisitEdmEntityType(EntityType item)
		{
			this._schemaWriter.WriteEntityTypeElementHeader(item);
			base.VisitEdmEntityType(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x000C3BCA File Offset: 0x000C1DCA
		private void InternalVisitEdmAssociationType(AssociationType item)
		{
			this._schemaWriter.WriteAssociationTypeElementHeader(item);
			base.VisitEdmAssociationType(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x000C3BEC File Offset: 0x000C1DEC
		private static void AppendSchemaErrors(StringBuilder builder, MetadataItem item)
		{
			if (MetadataItemHelper.HasSchemaErrors(item))
			{
				builder.Append(Strings.MetadataItemErrorsFoundDuringGeneration);
				foreach (EdmSchemaError edmSchemaError in MetadataItemHelper.GetSchemaErrors(item))
				{
					builder.AppendLine();
					builder.Append(edmSchemaError.ToString());
				}
			}
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x000C3C5C File Offset: 0x000C1E5C
		private void AppendMetadataItem<T>(StringBuilder builder, T item, Action<EdmSerializationVisitor, T> visitAction) where T : MetadataItem
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
			{
				ConformanceLevel = ConformanceLevel.Fragment,
				Indent = true
			};
			XmlWriterSettings xmlWriterSettings2 = xmlWriterSettings;
			xmlWriterSettings2.NewLineChars += "        ";
			builder.Append(xmlWriterSettings.NewLineChars);
			using (XmlWriter xmlWriter = XmlWriter.Create(builder, xmlWriterSettings))
			{
				EdmSerializationVisitor edmSerializationVisitor = new EdmSerializationVisitor(this._schemaWriter.Replicate(xmlWriter));
				visitAction(edmSerializationVisitor, item);
			}
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x000C3CE0 File Offset: 0x000C1EE0
		private void WriteComment(string comment)
		{
			this._schemaWriter.WriteComment(comment.Replace("--", "- -"));
		}

		// Token: 0x04001478 RID: 5240
		private readonly EdmXmlSchemaWriter _schemaWriter;
	}
}
