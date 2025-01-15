using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B6 RID: 1206
	internal class EdmXmlSchemaWriter : XmlSchemaWriter
	{
		// Token: 0x06003B75 RID: 15221 RVA: 0x000C46C0 File Offset: 0x000C28C0
		private static string SyndicationItemPropertyToString(object value)
		{
			return EdmXmlSchemaWriter._syndicationItemToTargetPath[(int)value];
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x000C46CE File Offset: 0x000C28CE
		private static string SyndicationTextContentKindToString(object value)
		{
			return EdmXmlSchemaWriter._syndicationTextContentKindToString[(int)value];
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x000C46DC File Offset: 0x000C28DC
		public EdmXmlSchemaWriter()
		{
			this._resolver = DbConfiguration.DependencyResolver;
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x000C46EF File Offset: 0x000C28EF
		internal EdmXmlSchemaWriter(XmlWriter xmlWriter, double edmVersion, bool serializeDefaultNullability, IDbDependencyResolver resolver = null)
		{
			this._resolver = resolver ?? DbConfiguration.DependencyResolver;
			this._serializeDefaultNullability = serializeDefaultNullability;
			this._xmlWriter = xmlWriter;
			this._version = edmVersion;
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x000C4720 File Offset: 0x000C2920
		internal virtual void WriteSchemaElementHeader(string schemaNamespace)
		{
			string csdlNamespace = XmlConstants.GetCsdlNamespace(this._version);
			this._xmlWriter.WriteStartElement("Schema", csdlNamespace);
			this._xmlWriter.WriteAttributeString("Namespace", schemaNamespace);
			this._xmlWriter.WriteAttributeString("Alias", "Self");
			if (this._version == 3.0)
			{
				this._xmlWriter.WriteAttributeString("annotation", "UseStrongSpatialTypes", "http://schemas.microsoft.com/ado/2009/02/edm/annotation", "false");
			}
			this._xmlWriter.WriteAttributeString("xmlns", "annotation", null, "http://schemas.microsoft.com/ado/2009/02/edm/annotation");
			this._xmlWriter.WriteAttributeString("xmlns", "customannotation", null, "http://schemas.microsoft.com/ado/2013/11/edm/customannotation");
		}

		// Token: 0x06003B7A RID: 15226 RVA: 0x000C47D8 File Offset: 0x000C29D8
		internal virtual void WriteSchemaElementHeader(string schemaNamespace, string provider, string providerManifestToken, bool writeStoreSchemaGenNamespace)
		{
			string ssdlNamespace = XmlConstants.GetSsdlNamespace(this._version);
			this._xmlWriter.WriteStartElement("Schema", ssdlNamespace);
			this._xmlWriter.WriteAttributeString("Namespace", schemaNamespace);
			this._xmlWriter.WriteAttributeString("Provider", provider);
			this._xmlWriter.WriteAttributeString("ProviderManifestToken", providerManifestToken);
			this._xmlWriter.WriteAttributeString("Alias", "Self");
			if (writeStoreSchemaGenNamespace)
			{
				this._xmlWriter.WriteAttributeString("xmlns", "store", null, "http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator");
			}
			this._xmlWriter.WriteAttributeString("xmlns", "customannotation", null, "http://schemas.microsoft.com/ado/2013/11/edm/customannotation");
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x000C4884 File Offset: 0x000C2A84
		private void WritePolymorphicTypeAttributes(EdmType edmType)
		{
			if (edmType.BaseType != null)
			{
				this._xmlWriter.WriteAttributeString("BaseType", XmlSchemaWriter.GetQualifiedTypeName("Self", edmType.BaseType.Name));
			}
			if (edmType.Abstract)
			{
				this._xmlWriter.WriteAttributeString("Abstract", "true");
			}
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x000C48DC File Offset: 0x000C2ADC
		public virtual void WriteFunctionElementHeader(EdmFunction function)
		{
			this._xmlWriter.WriteStartElement("Function");
			this._xmlWriter.WriteAttributeString("Name", function.Name);
			this._xmlWriter.WriteAttributeString("Aggregate", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(function.AggregateAttribute));
			this._xmlWriter.WriteAttributeString("BuiltIn", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(function.BuiltInAttribute));
			this._xmlWriter.WriteAttributeString("NiladicFunction", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(function.NiladicFunctionAttribute));
			this._xmlWriter.WriteAttributeString("IsComposable", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(function.IsComposableAttribute));
			this._xmlWriter.WriteAttributeString("ParameterTypeSemantics", function.ParameterTypeSemanticsAttribute.ToString());
			this._xmlWriter.WriteAttributeString("Schema", function.Schema);
			if (function.StoreFunctionNameAttribute != null && function.StoreFunctionNameAttribute != function.Name)
			{
				this._xmlWriter.WriteAttributeString("StoreFunctionName", function.StoreFunctionNameAttribute);
			}
			if (function.ReturnParameters != null && function.ReturnParameters.Any<FunctionParameter>())
			{
				EdmType edmType = function.ReturnParameters.First<FunctionParameter>().TypeUsage.EdmType;
				if (edmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType)
				{
					this._xmlWriter.WriteAttributeString("ReturnType", EdmXmlSchemaWriter.GetTypeName(edmType));
				}
			}
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x000C4A34 File Offset: 0x000C2C34
		public virtual void WriteFunctionParameterHeader(FunctionParameter functionParameter)
		{
			this._xmlWriter.WriteStartElement("Parameter");
			this._xmlWriter.WriteAttributeString("Name", functionParameter.Name);
			this._xmlWriter.WriteAttributeString("Type", functionParameter.TypeName);
			this._xmlWriter.WriteAttributeString("Mode", functionParameter.Mode.ToString());
			if (functionParameter.IsMaxLength)
			{
				this._xmlWriter.WriteAttributeString("MaxLength", "Max");
			}
			else if (!functionParameter.IsMaxLengthConstant && functionParameter.MaxLength != null)
			{
				this._xmlWriter.WriteAttributeString("MaxLength", functionParameter.MaxLength.Value.ToString(CultureInfo.InvariantCulture));
			}
			if (!functionParameter.IsPrecisionConstant && functionParameter.Precision != null)
			{
				this._xmlWriter.WriteAttributeString("Precision", functionParameter.Precision.Value.ToString(CultureInfo.InvariantCulture));
			}
			if (!functionParameter.IsScaleConstant && functionParameter.Scale != null)
			{
				this._xmlWriter.WriteAttributeString("Scale", functionParameter.Scale.Value.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x000C4B8B File Offset: 0x000C2D8B
		internal virtual void WriteFunctionReturnTypeElementHeader()
		{
			this._xmlWriter.WriteStartElement("ReturnType");
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x000C4BA0 File Offset: 0x000C2DA0
		internal void WriteEntityTypeElementHeader(EntityType entityType)
		{
			this._xmlWriter.WriteStartElement("EntityType");
			this._xmlWriter.WriteAttributeString("Name", entityType.Name);
			this.WriteExtendedProperties(entityType);
			if (entityType.Annotations.GetClrAttributes() != null)
			{
				foreach (Attribute attribute in entityType.Annotations.GetClrAttributes())
				{
					if (attribute.GetType().FullName.Equals("System.Data.Services.Common.HasStreamAttribute", StringComparison.Ordinal))
					{
						this._xmlWriter.WriteAttributeString("m", "HasStream", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", "true");
					}
					else if (attribute.GetType().FullName.Equals("System.Data.Services.MimeTypeAttribute", StringComparison.Ordinal))
					{
						string propertyName2 = attribute.GetType().GetDeclaredProperty("MemberName").GetValue(attribute, null) as string;
						EdmXmlSchemaWriter.AddAttributeAnnotation(entityType.Properties.SingleOrDefault((EdmProperty p) => p.Name.Equals(propertyName2, StringComparison.Ordinal)), attribute);
					}
					else if (attribute.GetType().FullName.Equals("System.Data.Services.Common.EntityPropertyMappingAttribute", StringComparison.Ordinal))
					{
						string text = attribute.GetType().GetDeclaredProperty("SourcePath").GetValue(attribute, null) as string;
						int num = text.IndexOf("/", StringComparison.Ordinal);
						string propertyName;
						if (num == -1)
						{
							propertyName = text;
						}
						else
						{
							propertyName = text.Substring(0, num);
						}
						EdmXmlSchemaWriter.AddAttributeAnnotation(entityType.Properties.SingleOrDefault((EdmProperty p) => p.Name.Equals(propertyName, StringComparison.Ordinal)), attribute);
					}
				}
			}
			this.WritePolymorphicTypeAttributes(entityType);
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x000C4D68 File Offset: 0x000C2F68
		internal void WriteEnumTypeElementHeader(EnumType enumType)
		{
			this._xmlWriter.WriteStartElement("EnumType");
			this._xmlWriter.WriteAttributeString("Name", enumType.Name);
			this._xmlWriter.WriteAttributeString("IsFlags", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(enumType.IsFlags));
			this.WriteExtendedProperties(enumType);
			if (enumType.UnderlyingType != null)
			{
				this._xmlWriter.WriteAttributeString("UnderlyingType", enumType.UnderlyingType.PrimitiveTypeKind.ToString());
			}
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x000C4DF0 File Offset: 0x000C2FF0
		internal void WriteEnumTypeMemberElementHeader(EnumMember enumTypeMember)
		{
			this._xmlWriter.WriteStartElement("Member");
			this._xmlWriter.WriteAttributeString("Name", enumTypeMember.Name);
			this._xmlWriter.WriteAttributeString("Value", enumTypeMember.Value.ToString());
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x000C4E40 File Offset: 0x000C3040
		private static void AddAttributeAnnotation(EdmProperty property, Attribute a)
		{
			if (property != null)
			{
				IList<Attribute> clrAttributes = property.Annotations.GetClrAttributes();
				if (clrAttributes != null)
				{
					if (!clrAttributes.Contains(a))
					{
						clrAttributes.Add(a);
						return;
					}
				}
				else
				{
					property.GetMetadataProperties().SetClrAttributes(new List<Attribute> { a });
				}
			}
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x000C4E87 File Offset: 0x000C3087
		internal void WriteComplexTypeElementHeader(ComplexType complexType)
		{
			this._xmlWriter.WriteStartElement("ComplexType");
			this._xmlWriter.WriteAttributeString("Name", complexType.Name);
			this.WriteExtendedProperties(complexType);
			this.WritePolymorphicTypeAttributes(complexType);
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x000C4EBD File Offset: 0x000C30BD
		internal virtual void WriteCollectionTypeElementHeader()
		{
			this._xmlWriter.WriteStartElement("CollectionType");
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x000C4ECF File Offset: 0x000C30CF
		internal virtual void WriteRowTypeElementHeader()
		{
			this._xmlWriter.WriteStartElement("RowType");
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x000C4EE1 File Offset: 0x000C30E1
		internal void WriteAssociationTypeElementHeader(AssociationType associationType)
		{
			this._xmlWriter.WriteStartElement("Association");
			this._xmlWriter.WriteAttributeString("Name", associationType.Name);
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x000C4F0C File Offset: 0x000C310C
		internal void WriteAssociationEndElementHeader(RelationshipEndMember associationEnd)
		{
			this._xmlWriter.WriteStartElement("End");
			this._xmlWriter.WriteAttributeString("Role", associationEnd.Name);
			string name = associationEnd.GetEntityType().Name;
			this._xmlWriter.WriteAttributeString("Type", XmlSchemaWriter.GetQualifiedTypeName("Self", name));
			this._xmlWriter.WriteAttributeString("Multiplicity", RelationshipMultiplicityConverter.MultiplicityToString(associationEnd.RelationshipMultiplicity));
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x000C4F81 File Offset: 0x000C3181
		internal void WriteOperationActionElement(string elementName, OperationAction operationAction)
		{
			this._xmlWriter.WriteStartElement(elementName);
			this._xmlWriter.WriteAttributeString("Action", operationAction.ToString());
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x000C4FB7 File Offset: 0x000C31B7
		internal void WriteReferentialConstraintElementHeader()
		{
			this._xmlWriter.WriteStartElement("ReferentialConstraint");
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x000C4FC9 File Offset: 0x000C31C9
		internal void WriteDeclaredKeyPropertiesElementHeader()
		{
			this._xmlWriter.WriteStartElement("Key");
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x000C4FDB File Offset: 0x000C31DB
		internal void WriteDeclaredKeyPropertyRefElement(EdmProperty property)
		{
			this._xmlWriter.WriteStartElement("PropertyRef");
			this._xmlWriter.WriteAttributeString("Name", property.Name);
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000C5010 File Offset: 0x000C3210
		internal void WritePropertyElementHeader(EdmProperty property)
		{
			this._xmlWriter.WriteStartElement("Property");
			this._xmlWriter.WriteAttributeString("Name", property.Name);
			this._xmlWriter.WriteAttributeString("Type", EdmXmlSchemaWriter.GetTypeReferenceName(property));
			if (property.CollectionKind != CollectionKind.None)
			{
				this._xmlWriter.WriteAttributeString("CollectionKind", property.CollectionKind.ToString());
			}
			if (property.ConcurrencyMode == ConcurrencyMode.Fixed)
			{
				this._xmlWriter.WriteAttributeString("ConcurrencyMode", "Fixed");
			}
			this.WriteExtendedProperties(property);
			if (property.Annotations.GetClrAttributes() != null)
			{
				int num = 0;
				foreach (Attribute attribute in property.Annotations.GetClrAttributes())
				{
					if (attribute.GetType().FullName.Equals("System.Data.Services.MimeTypeAttribute", StringComparison.Ordinal))
					{
						string text = attribute.GetType().GetDeclaredProperty("MimeType").GetValue(attribute, null) as string;
						this._xmlWriter.WriteAttributeString("m", "MimeType", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", text);
					}
					else if (attribute.GetType().FullName.Equals("System.Data.Services.Common.EntityPropertyMappingAttribute", StringComparison.Ordinal))
					{
						string text2 = ((num == 0) ? string.Empty : string.Format(CultureInfo.InvariantCulture, "_{0}", new object[] { num }));
						string text3 = attribute.GetType().GetDeclaredProperty("SourcePath").GetValue(attribute, null) as string;
						int num2 = text3.IndexOf("/", StringComparison.Ordinal);
						if (num2 != -1 && num2 + 1 < text3.Length)
						{
							this._xmlWriter.WriteAttributeString("m", "FC_SourcePath" + text2, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", text3.Substring(num2 + 1));
						}
						object value = attribute.GetType().GetDeclaredProperty("TargetSyndicationItem").GetValue(attribute, null);
						string text4 = attribute.GetType().GetDeclaredProperty("KeepInContent").GetValue(attribute, null)
							.ToString();
						PropertyInfo declaredProperty = attribute.GetType().GetDeclaredProperty("CriteriaValue");
						string text5 = null;
						if (declaredProperty != null)
						{
							text5 = declaredProperty.GetValue(attribute, null) as string;
						}
						if (text5 != null)
						{
							this._xmlWriter.WriteAttributeString("m", "FC_TargetPath" + text2, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", EdmXmlSchemaWriter.SyndicationItemPropertyToString(value));
							this._xmlWriter.WriteAttributeString("m", "FC_KeepInContent" + text2, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", text4);
							this._xmlWriter.WriteAttributeString("m", "FC_CriteriaValue" + text2, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", text5);
						}
						else if (string.Equals(value.ToString(), "CustomProperty", StringComparison.Ordinal))
						{
							string text6 = attribute.GetType().GetDeclaredProperty("TargetPath").GetValue(attribute, null)
								.ToString();
							string text7 = attribute.GetType().GetDeclaredProperty("TargetNamespacePrefix").GetValue(attribute, null)
								.ToString();
							string text8 = attribute.GetType().GetDeclaredProperty("TargetNamespaceUri").GetValue(attribute, null)
								.ToString();
							this._xmlWriter.WriteAttributeString("m", "FC_TargetPath" + text2, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", text6);
							this._xmlWriter.WriteAttributeString("m", "FC_NsUri" + text2, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", text8);
							this._xmlWriter.WriteAttributeString("m", "FC_NsPrefix" + text2, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", text7);
							this._xmlWriter.WriteAttributeString("m", "FC_KeepInContent" + text2, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", text4);
						}
						else
						{
							object value2 = attribute.GetType().GetDeclaredProperty("TargetTextContentKind").GetValue(attribute, null);
							this._xmlWriter.WriteAttributeString("m", "FC_TargetPath" + text2, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", EdmXmlSchemaWriter.SyndicationItemPropertyToString(value));
							this._xmlWriter.WriteAttributeString("m", "FC_ContentKind" + text2, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", EdmXmlSchemaWriter.SyndicationTextContentKindToString(value2));
							this._xmlWriter.WriteAttributeString("m", "FC_KeepInContent" + text2, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", text4);
						}
						num++;
					}
				}
			}
			if (property.IsMaxLength)
			{
				this._xmlWriter.WriteAttributeString("MaxLength", "Max");
			}
			else if (!property.IsMaxLengthConstant && property.MaxLength != null)
			{
				this._xmlWriter.WriteAttributeString("MaxLength", property.MaxLength.Value.ToString(CultureInfo.InvariantCulture));
			}
			if (!property.IsFixedLengthConstant && property.IsFixedLength != null)
			{
				this._xmlWriter.WriteAttributeString("FixedLength", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(property.IsFixedLength.Value));
			}
			if (!property.IsUnicodeConstant && property.IsUnicode != null)
			{
				this._xmlWriter.WriteAttributeString("Unicode", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(property.IsUnicode.Value));
			}
			if (!property.IsPrecisionConstant && property.Precision != null)
			{
				this._xmlWriter.WriteAttributeString("Precision", property.Precision.Value.ToString(CultureInfo.InvariantCulture));
			}
			if (!property.IsScaleConstant && property.Scale != null)
			{
				this._xmlWriter.WriteAttributeString("Scale", property.Scale.Value.ToString(CultureInfo.InvariantCulture));
			}
			if (property.StoreGeneratedPattern != StoreGeneratedPattern.None)
			{
				this._xmlWriter.WriteAttributeString("StoreGeneratedPattern", (property.StoreGeneratedPattern == StoreGeneratedPattern.Computed) ? "Computed" : "Identity");
			}
			if (this._serializeDefaultNullability || !property.Nullable)
			{
				this._xmlWriter.WriteAttributeString("Nullable", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(property.Nullable));
			}
			MetadataProperty metadataProperty;
			if (property.MetadataProperties.TryGetValue("http://schemas.microsoft.com/ado/2009/02/edm/annotation:StoreGeneratedPattern", false, out metadataProperty))
			{
				this._xmlWriter.WriteAttributeString("StoreGeneratedPattern", "http://schemas.microsoft.com/ado/2009/02/edm/annotation", metadataProperty.Value.ToString());
			}
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x000C5690 File Offset: 0x000C3890
		private static string GetTypeReferenceName(EdmProperty property)
		{
			if (property.IsPrimitiveType)
			{
				return property.TypeName;
			}
			if (property.IsComplexType)
			{
				return XmlSchemaWriter.GetQualifiedTypeName("Self", property.ComplexType.Name);
			}
			return XmlSchemaWriter.GetQualifiedTypeName("Self", property.EnumType.Name);
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x000C56E0 File Offset: 0x000C38E0
		internal void WriteNavigationPropertyElementHeader(NavigationProperty member)
		{
			this._xmlWriter.WriteStartElement("NavigationProperty");
			this._xmlWriter.WriteAttributeString("Name", member.Name);
			this._xmlWriter.WriteAttributeString("Relationship", XmlSchemaWriter.GetQualifiedTypeName("Self", member.Association.Name));
			this._xmlWriter.WriteAttributeString("FromRole", member.GetFromEnd().Name);
			this._xmlWriter.WriteAttributeString("ToRole", member.ToEndMember.Name);
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x000C5770 File Offset: 0x000C3970
		internal void WriteReferentialConstraintRoleElement(string roleName, RelationshipEndMember edmAssociationEnd, IEnumerable<EdmProperty> properties)
		{
			this._xmlWriter.WriteStartElement(roleName);
			this._xmlWriter.WriteAttributeString("Role", edmAssociationEnd.Name);
			foreach (EdmProperty edmProperty in properties)
			{
				this._xmlWriter.WriteStartElement("PropertyRef");
				this._xmlWriter.WriteAttributeString("Name", edmProperty.Name);
				this._xmlWriter.WriteEndElement();
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x000C5810 File Offset: 0x000C3A10
		internal virtual void WriteEntityContainerElementHeader(EntityContainer container)
		{
			this._xmlWriter.WriteStartElement("EntityContainer");
			this._xmlWriter.WriteAttributeString("Name", container.Name);
			this.WriteExtendedProperties(container);
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x000C5840 File Offset: 0x000C3A40
		internal void WriteAssociationSetElementHeader(AssociationSet associationSet)
		{
			this._xmlWriter.WriteStartElement("AssociationSet");
			this._xmlWriter.WriteAttributeString("Name", associationSet.Name);
			this._xmlWriter.WriteAttributeString("Association", XmlSchemaWriter.GetQualifiedTypeName("Self", associationSet.ElementType.Name));
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x000C5898 File Offset: 0x000C3A98
		internal void WriteAssociationSetEndElement(EntitySet end, string roleName)
		{
			this._xmlWriter.WriteStartElement("End");
			this._xmlWriter.WriteAttributeString("Role", roleName);
			this._xmlWriter.WriteAttributeString("EntitySet", end.Name);
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x000C58E8 File Offset: 0x000C3AE8
		internal virtual void WriteEntitySetElementHeader(EntitySet entitySet)
		{
			this._xmlWriter.WriteStartElement("EntitySet");
			this._xmlWriter.WriteAttributeString("Name", entitySet.Name);
			this._xmlWriter.WriteAttributeString("EntityType", XmlSchemaWriter.GetQualifiedTypeName("Self", entitySet.ElementType.Name));
			if (!string.IsNullOrWhiteSpace(entitySet.Schema))
			{
				this._xmlWriter.WriteAttributeString("Schema", entitySet.Schema);
			}
			if (!string.IsNullOrWhiteSpace(entitySet.Table))
			{
				this._xmlWriter.WriteAttributeString("Table", entitySet.Table);
			}
			this.WriteExtendedProperties(entitySet);
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x000C5990 File Offset: 0x000C3B90
		internal virtual void WriteFunctionImportElementHeader(EdmFunction functionImport)
		{
			this._xmlWriter.WriteStartElement("FunctionImport");
			this._xmlWriter.WriteAttributeString("Name", functionImport.Name);
			if (functionImport.IsComposableAttribute)
			{
				this._xmlWriter.WriteAttributeString("IsComposable", "true");
			}
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x000C59E0 File Offset: 0x000C3BE0
		internal virtual void WriteFunctionImportReturnTypeAttributes(FunctionParameter returnParameter, EntitySet entitySet, bool inline)
		{
			this._xmlWriter.WriteAttributeString(inline ? "ReturnType" : "Type", EdmXmlSchemaWriter.GetTypeName(returnParameter.TypeUsage.EdmType));
			if (entitySet != null)
			{
				this._xmlWriter.WriteAttributeString("EntitySet", entitySet.Name);
			}
		}

		// Token: 0x06003B96 RID: 15254 RVA: 0x000C5A30 File Offset: 0x000C3C30
		internal virtual void WriteFunctionImportParameterElementHeader(FunctionParameter parameter)
		{
			this._xmlWriter.WriteStartElement("Parameter");
			this._xmlWriter.WriteAttributeString("Name", parameter.Name);
			this._xmlWriter.WriteAttributeString("Mode", parameter.Mode.ToString());
			this._xmlWriter.WriteAttributeString("Type", EdmXmlSchemaWriter.GetTypeName(parameter.TypeUsage.EdmType));
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x000C5AA7 File Offset: 0x000C3CA7
		internal void WriteDefiningQuery(EntitySet entitySet)
		{
			if (!string.IsNullOrWhiteSpace(entitySet.DefiningQuery))
			{
				this._xmlWriter.WriteElementString("DefiningQuery", entitySet.DefiningQuery);
			}
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x000C5ACC File Offset: 0x000C3CCC
		internal EdmXmlSchemaWriter Replicate(XmlWriter xmlWriter)
		{
			return new EdmXmlSchemaWriter(xmlWriter, this._version, this._serializeDefaultNullability, null);
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x000C5AE4 File Offset: 0x000C3CE4
		internal void WriteExtendedProperties(MetadataItem item)
		{
			foreach (MetadataProperty metadataProperty in item.MetadataProperties.Where((MetadataProperty p) => p.PropertyKind == PropertyKind.Extended))
			{
				string text;
				string text2;
				if (EdmXmlSchemaWriter.TrySplitExtendedMetadataPropertyName(metadataProperty.Name, out text, out text2) && metadataProperty.Name != "http://schemas.microsoft.com/ado/2009/02/edm/annotation:StoreGeneratedPattern")
				{
					Func<IMetadataAnnotationSerializer> service = this._resolver.GetService(text2);
					string text3 = ((service == null) ? metadataProperty.Value.ToString() : service().Serialize(text2, metadataProperty.Value));
					this._xmlWriter.WriteAttributeString(text2, text, text3);
				}
			}
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x000C5BB4 File Offset: 0x000C3DB4
		private static bool TrySplitExtendedMetadataPropertyName(string name, out string xmlNamespaceUri, out string attributeName)
		{
			int num = name.LastIndexOf(':');
			if (num < 1 || name.Length <= num + 1)
			{
				xmlNamespaceUri = null;
				attributeName = null;
				return false;
			}
			xmlNamespaceUri = name.Substring(0, num);
			attributeName = name.Substring(num + 1, name.Length - 1 - num);
			return true;
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x000C5C04 File Offset: 0x000C3E04
		private static string GetTypeName(EdmType type)
		{
			if (type.BuiltInTypeKind == BuiltInTypeKind.CollectionType)
			{
				return string.Format(CultureInfo.InvariantCulture, "Collection({0})", new object[] { EdmXmlSchemaWriter.GetTypeName(((CollectionType)type).TypeUsage.EdmType) });
			}
			if (type.BuiltInTypeKind != BuiltInTypeKind.PrimitiveType)
			{
				return type.FullName;
			}
			return type.Name;
		}

		// Token: 0x0400147F RID: 5247
		private readonly bool _serializeDefaultNullability;

		// Token: 0x04001480 RID: 5248
		private readonly IDbDependencyResolver _resolver;

		// Token: 0x04001481 RID: 5249
		private const string AnnotationNamespacePrefix = "annotation";

		// Token: 0x04001482 RID: 5250
		private const string CustomAnnotationNamespacePrefix = "customannotation";

		// Token: 0x04001483 RID: 5251
		private const string StoreSchemaGenNamespacePrefix = "store";

		// Token: 0x04001484 RID: 5252
		private const string DataServicesPrefix = "m";

		// Token: 0x04001485 RID: 5253
		private const string DataServicesNamespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";

		// Token: 0x04001486 RID: 5254
		private const string DataServicesMimeTypeAttribute = "System.Data.Services.MimeTypeAttribute";

		// Token: 0x04001487 RID: 5255
		private const string DataServicesHasStreamAttribute = "System.Data.Services.Common.HasStreamAttribute";

		// Token: 0x04001488 RID: 5256
		private const string DataServicesEntityPropertyMappingAttribute = "System.Data.Services.Common.EntityPropertyMappingAttribute";

		// Token: 0x04001489 RID: 5257
		private static readonly string[] _syndicationItemToTargetPath = new string[]
		{
			string.Empty,
			"SyndicationAuthorEmail",
			"SyndicationAuthorName",
			"SyndicationAuthorUri",
			"SyndicationContributorEmail",
			"SyndicationContributorName",
			"SyndicationContributorUri",
			"SyndicationUpdated",
			"SyndicationPublished",
			"SyndicationRights",
			"SyndicationSummary",
			"SyndicationTitle",
			"SyndicationCategoryLabel",
			"SyndicationCategoryScheme",
			"SyndicationCategoryTerm",
			"SyndicationLinkHref",
			"SyndicationLinkHrefLang",
			"SyndicationLinkLength",
			"SyndicationLinkRel",
			"SyndicationLinkTitle",
			"SyndicationLinkType"
		};

		// Token: 0x0400148A RID: 5258
		private static readonly string[] _syndicationTextContentKindToString = new string[] { "text", "html", "xhtml" };

		// Token: 0x02000ADF RID: 2783
		internal static class SyndicationXmlConstants
		{
			// Token: 0x04002BDB RID: 11227
			internal const string SyndAuthorEmail = "SyndicationAuthorEmail";

			// Token: 0x04002BDC RID: 11228
			internal const string SyndAuthorName = "SyndicationAuthorName";

			// Token: 0x04002BDD RID: 11229
			internal const string SyndAuthorUri = "SyndicationAuthorUri";

			// Token: 0x04002BDE RID: 11230
			internal const string SyndPublished = "SyndicationPublished";

			// Token: 0x04002BDF RID: 11231
			internal const string SyndRights = "SyndicationRights";

			// Token: 0x04002BE0 RID: 11232
			internal const string SyndSummary = "SyndicationSummary";

			// Token: 0x04002BE1 RID: 11233
			internal const string SyndTitle = "SyndicationTitle";

			// Token: 0x04002BE2 RID: 11234
			internal const string SyndContributorEmail = "SyndicationContributorEmail";

			// Token: 0x04002BE3 RID: 11235
			internal const string SyndContributorName = "SyndicationContributorName";

			// Token: 0x04002BE4 RID: 11236
			internal const string SyndContributorUri = "SyndicationContributorUri";

			// Token: 0x04002BE5 RID: 11237
			internal const string SyndCategoryLabel = "SyndicationCategoryLabel";

			// Token: 0x04002BE6 RID: 11238
			internal const string SyndContentKindPlaintext = "text";

			// Token: 0x04002BE7 RID: 11239
			internal const string SyndContentKindHtml = "html";

			// Token: 0x04002BE8 RID: 11240
			internal const string SyndContentKindXHtml = "xhtml";

			// Token: 0x04002BE9 RID: 11241
			internal const string SyndUpdated = "SyndicationUpdated";

			// Token: 0x04002BEA RID: 11242
			internal const string SyndLinkHref = "SyndicationLinkHref";

			// Token: 0x04002BEB RID: 11243
			internal const string SyndLinkRel = "SyndicationLinkRel";

			// Token: 0x04002BEC RID: 11244
			internal const string SyndLinkType = "SyndicationLinkType";

			// Token: 0x04002BED RID: 11245
			internal const string SyndLinkHrefLang = "SyndicationLinkHrefLang";

			// Token: 0x04002BEE RID: 11246
			internal const string SyndLinkTitle = "SyndicationLinkTitle";

			// Token: 0x04002BEF RID: 11247
			internal const string SyndLinkLength = "SyndicationLinkLength";

			// Token: 0x04002BF0 RID: 11248
			internal const string SyndCategoryTerm = "SyndicationCategoryTerm";

			// Token: 0x04002BF1 RID: 11249
			internal const string SyndCategoryScheme = "SyndicationCategoryScheme";
		}
	}
}
