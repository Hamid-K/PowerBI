using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x0200017D RID: 381
	internal sealed class LinguisticSchemaXmlToJsonUpgrader
	{
		// Token: 0x0600074C RID: 1868 RVA: 0x0000C8AC File Offset: 0x0000AAAC
		private LinguisticSchemaXmlToJsonUpgrader(XElement rootElement)
		{
			this._rootElement = rootElement;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000C8BC File Offset: 0x0000AABC
		public static bool TryUpgrade(XDocument xdoc, IDomainModelDiagnosticContext diagnosticContext, out JObject jsonDoc)
		{
			if (!xdoc.Root.GetDefaultNamespace().Equals(LinguisticSchemaUpgrader.SchemaConstants.Namespace))
			{
				diagnosticContext.Register(DomainModelDiagnosticMessageFactory.LinguisticSchemaUpgradeFromIncorrectXmlVersionError());
				jsonDoc = null;
				return false;
			}
			LinguisticSchemaXmlToJsonUpgrader linguisticSchemaXmlToJsonUpgrader = new LinguisticSchemaXmlToJsonUpgrader(xdoc.Root);
			jsonDoc = linguisticSchemaXmlToJsonUpgrader.Upgrade();
			return true;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0000C908 File Offset: 0x0000AB08
		public JObject Upgrade()
		{
			JObject jobject = new JObject(new JProperty("Version", "3.1.0"));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Language", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(this._rootElement, LinguisticSchemaUpgrader.SchemaConstants.LanguageAttr));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "DynamicImprovement", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(this._rootElement, LinguisticSchemaUpgrader.SchemaConstants.DynamicImprovementAttr));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "MinResultConfidence", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(this._rootElement, LinguisticSchemaUpgrader.SchemaConstants.MinResultConfidenceAttr));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Namespaces", this.ConvertNamespaces(), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Entities", this.ConvertEntities(), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Relationships", this.ConvertRelationships(), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "GlobalSubstitutions", this.ConvertGlobalSubstitutions(), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Examples", this.ConvertExamples(), false);
			return jobject;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		private JObject ConvertNamespaces()
		{
			XElement xelement = this._rootElement.Element(LinguisticSchemaUpgrader.SchemaConstants.SchemaReferencesElem);
			if (xelement == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			foreach (XElement xelement2 in xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.SchemaReferenceElem))
			{
				XAttribute xattribute = xelement2.Attribute(LinguisticSchemaUpgrader.SchemaConstants.SchemaReferenceNamespaceAttr);
				if (xattribute != null)
				{
					jobject.Add(xattribute.Value, new JObject());
				}
			}
			return jobject;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0000CA5C File Offset: 0x0000AC5C
		private JObject ConvertEntities()
		{
			XElement xelement = this._rootElement.Element(LinguisticSchemaUpgrader.SchemaConstants.EntitiesElem);
			if (xelement == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			foreach (XElement xelement2 in xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.EntityElem))
			{
				LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(xelement2, LinguisticSchemaUpgrader.SchemaConstants.NameAttr), LinguisticSchemaXmlToJsonUpgrader.ConvertEntityProperties(xelement2), false);
			}
			return jobject;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0000CADC File Offset: 0x0000ACDC
		private JObject ConvertRelationships()
		{
			XElement xelement = this._rootElement.Element(LinguisticSchemaUpgrader.SchemaConstants.RelationshipsElem);
			IEnumerable<XElement> enumerable = ((xelement != null) ? xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.RelationshipElem) : null);
			if (enumerable == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			foreach (XElement xelement2 in enumerable)
			{
				LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(xelement2, LinguisticSchemaUpgrader.SchemaConstants.NameAttr), LinguisticSchemaXmlToJsonUpgrader.ConvertRelationshipProperties(xelement2), false);
			}
			return jobject;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0000CB64 File Offset: 0x0000AD64
		private JArray ConvertGlobalSubstitutions()
		{
			XElement xelement = this._rootElement.Element(LinguisticSchemaUpgrader.SchemaConstants.GlobalSynonymsElem);
			IEnumerable<XElement> enumerable = ((xelement != null) ? xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.GlobalSynonymElem) : null);
			if (enumerable == null)
			{
				return null;
			}
			JArray jarray = new JArray();
			foreach (XElement xelement2 in enumerable)
			{
				jarray.Add(LinguisticSchemaXmlToJsonUpgrader.ConvertGlobalSubstitution(xelement2));
			}
			return jarray;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
		private JArray ConvertExamples()
		{
			XElement xelement = this._rootElement.Element(LinguisticSchemaUpgrader.SchemaConstants.ExamplesElem);
			IEnumerable<XElement> enumerable = ((xelement != null) ? xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.ExampleElem) : null);
			if (enumerable == null)
			{
				return null;
			}
			JArray jarray = new JArray();
			foreach (XElement xelement2 in enumerable)
			{
				jarray.Add(LinguisticSchemaXmlToJsonUpgrader.ConvertExample(xelement2));
			}
			return jarray;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0000CC5C File Offset: 0x0000AE5C
		private static JObject ConvertEntityProperties(XElement element)
		{
			JObject jobject = new JObject();
			JObject jobject2 = new JObject();
			string text;
			if (LinguisticSchemaXmlToJsonUpgrader.TryGetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.TextDefinitionAttr, out text))
			{
				jobject2.Add("Text", text);
			}
			else
			{
				JObject jobject3 = new JObject();
				LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject3, "ConceptualEntity", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.ConceptualEntityAttr));
				LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject3, "VariationSource", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.ConceptualVariationSourceAttr));
				LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject3, "VariationSet", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.ConceptualVariationSetAttr));
				LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject3, "Hierarchy", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.ConceptualHierarchyAttr));
				LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject3, "HierarchyLevel", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.ConceptualHierarchyLevelAttr));
				LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject3, "ConceptualProperty", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.ConceptualPropertyAttr));
				jobject2.Add("Binding", jobject3);
			}
			jobject.Add("Definition", jobject2);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "State", LinguisticSchemaXmlToJsonUpgrader.ConvertState(LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.SourceAttr)));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Visibility", LinguisticSchemaXmlToJsonUpgrader.ConvertVisibility(element.Element(LinguisticSchemaUpgrader.SchemaConstants.VisibilityElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Weight", LinguisticSchemaXmlToJsonUpgrader.ConvertWeight(element), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "TemplateSchema", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.TemplateSchemaAttr));
			string text2 = "Terms";
			XElement xelement = element.Element(LinguisticSchemaUpgrader.SchemaConstants.WordsElem);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, text2, LinguisticSchemaXmlToJsonUpgrader.ConvertTerms((xelement != null) ? xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.WordElem) : null), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "SemanticType", LinguisticSchemaXmlToJsonUpgrader.GetPropertyValue(element, LinguisticSchemaUpgrader.SchemaConstants.SemanticTypeElem));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "NameType", LinguisticSchemaXmlToJsonUpgrader.ConvertNameType(LinguisticSchemaXmlToJsonUpgrader.GetPropertyValue(element, LinguisticSchemaUpgrader.SchemaConstants.NameTypeElem)));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Instances", LinguisticSchemaXmlToJsonUpgrader.ConvertInstances(element, LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.ConceptualEntityAttr)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Units", LinguisticSchemaXmlToJsonUpgrader.ConvertUnits(element.Element(LinguisticSchemaUpgrader.SchemaConstants.UnitsElem)), false);
			return jobject;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0000CE31 File Offset: 0x0000B031
		private static string GetAttributeValue(XElement element, XName attribute)
		{
			XAttribute xattribute = element.Attribute(attribute);
			if (xattribute == null)
			{
				return null;
			}
			return xattribute.Value;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0000CE45 File Offset: 0x0000B045
		private static string GetPropertyValue(XElement element, XName attribute)
		{
			XElement xelement = element.Element(attribute);
			if (xelement == null)
			{
				return null;
			}
			return xelement.Value;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000CE59 File Offset: 0x0000B059
		private static bool TryGetAttributeValue(XElement element, XName attribute, out string value)
		{
			XAttribute xattribute = element.Attribute(attribute);
			value = ((xattribute != null) ? xattribute.Value : null);
			return value != null;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0000CE78 File Offset: 0x0000B078
		private static JValue ConvertWeight(XElement element)
		{
			XAttribute xattribute = element.Attribute(LinguisticSchemaUpgrader.SchemaConstants.WeightAttr);
			string text = ((xattribute != null) ? xattribute.Value : null);
			double num;
			if (text == null || !double.TryParse(text, out num))
			{
				return null;
			}
			return new JValue(num);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0000CEB2 File Offset: 0x0000B0B2
		private static void AddProperty(JObject jobject, string property, string value)
		{
			if (string.IsNullOrEmpty(property))
			{
				return;
			}
			if (value != null)
			{
				jobject.Add(property, value);
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0000CECD File Offset: 0x0000B0CD
		private static void AddProperty(JObject jobject, string property, JToken value, bool acceptsNull = false)
		{
			if (acceptsNull || value != null)
			{
				jobject.Add(property, value);
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0000CEE0 File Offset: 0x0000B0E0
		private static string ConvertState(string source)
		{
			if (source == null)
			{
				return null;
			}
			if (source == "User")
			{
				return "Authored";
			}
			if (!(source == "Generated") && !(source == "Suggested") && !(source == "Deleted"))
			{
				return null;
			}
			return source;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0000CF31 File Offset: 0x0000B131
		private static JObject ConvertVisibility(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Value", element.Value);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "State", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.StateAttr));
			return jobject;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0000CF64 File Offset: 0x0000B164
		private static string ConvertNameType(string value)
		{
			if (value == null)
			{
				return null;
			}
			if (value == "ID")
			{
				return "Identifier";
			}
			if (!(value == "Name"))
			{
				return null;
			}
			return "Name";
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0000CF94 File Offset: 0x0000B194
		private static JObject ConvertInstances(XElement element, string entity)
		{
			if (element == null)
			{
				return null;
			}
			XElement xelement = element.Element(LinguisticSchemaUpgrader.SchemaConstants.InstanceSynonymsElem);
			XElement xelement2 = element.Element(LinguisticSchemaUpgrader.SchemaConstants.InstanceWeightsElem);
			XElement xelement3 = element.Element(LinguisticSchemaUpgrader.SchemaConstants.InstanceIndexElem);
			string text = ((xelement3 != null) ? xelement3.Value : null);
			XElement xelement4 = element.Element(LinguisticSchemaUpgrader.SchemaConstants.InstancePluralNormalizationElem);
			string text2 = ((xelement4 != null) ? xelement4.Value : null);
			if (xelement == null && xelement2 == null && text == null && text2 == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Synonyms", LinguisticSchemaXmlToJsonUpgrader.ConvertInstanceSynonyms(xelement), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Weights", LinguisticSchemaXmlToJsonUpgrader.ConvertInstanceWeights(xelement2, entity), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Index", text);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "PluralNormalization", text2);
			return jobject;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0000D040 File Offset: 0x0000B240
		private static JObject ConvertInstanceSynonyms(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			string attributeValue = LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.ConceptualEntityAttr);
			string attributeValue2 = LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.WordConceptualPropertyAttr);
			string attributeValue3 = LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.ValueConceptualPropertyAttr);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "SynonymBinding", LinguisticSchemaXmlToJsonUpgrader.CreatePropertyBinding(attributeValue, attributeValue2), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "ValueBinding", LinguisticSchemaXmlToJsonUpgrader.CreatePropertyBinding(attributeValue, attributeValue3), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "State", LinguisticSchemaXmlToJsonUpgrader.ConvertState(LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.SourceAttr)));
			return jobject;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0000D0BC File Offset: 0x0000B2BC
		private static JObject ConvertInstanceWeights(XElement element, string entity)
		{
			if (element == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Binding", LinguisticSchemaXmlToJsonUpgrader.CreatePropertyBinding(entity, LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.ConceptualPropertyAttr)), false);
			return jobject;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0000D0E5 File Offset: 0x0000B2E5
		private static JObject CreatePropertyBinding(string entity, string property)
		{
			return new JObject(new object[]
			{
				new JProperty("ConceptualEntity", entity),
				new JProperty("ConceptualProperty", property)
			});
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0000D110 File Offset: 0x0000B310
		private static JArray ConvertTerms(IEnumerable<XElement> elements)
		{
			if (elements == null)
			{
				return null;
			}
			JArray jarray = new JArray();
			foreach (XElement xelement in elements)
			{
				string text = LinguisticSchemaXmlToJsonUpgrader.NormalizeNewlinesToSpace(xelement.Value);
				jarray.Add(new JObject(new JProperty(text, LinguisticSchemaXmlToJsonUpgrader.ConvertTermProperties(xelement))));
			}
			return jarray;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0000D180 File Offset: 0x0000B380
		private static JObject ConvertTermProperties(XElement element)
		{
			JObject jobject = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Type", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.TypeAttr));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "State", LinguisticSchemaXmlToJsonUpgrader.ConvertState(LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.SourceAttr)));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "TemplateSchema", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.TemplateSchemaAttr));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Source", LinguisticSchemaXmlToJsonUpgrader.ConvertSource(element), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Weight", LinguisticSchemaXmlToJsonUpgrader.ConvertWeight(element), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "LastModified", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.LastModifiedAttr));
			return jobject;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0000D214 File Offset: 0x0000B414
		private static JObject ConvertSource(XElement termElement)
		{
			string attributeValue = LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(termElement, LinguisticSchemaUpgrader.SchemaConstants.SourceTypeAttr);
			string attributeValue2 = LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(termElement, LinguisticSchemaUpgrader.SchemaConstants.SourceAgentAttr);
			if (attributeValue == null && attributeValue2 == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Type", attributeValue);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Agent", attributeValue2);
			return jobject;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0000D260 File Offset: 0x0000B460
		private static JArray ConvertUnits(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JArray jarray = new JArray();
			foreach (XElement xelement in element.Elements(LinguisticSchemaUpgrader.SchemaConstants.UnitElem))
			{
				string value = xelement.Value;
				jarray.Add(value);
			}
			return jarray;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0000D2C8 File Offset: 0x0000B4C8
		private static JObject ConvertRelationshipProperties(XElement element)
		{
			JObject jobject = new JObject();
			string attributeValue = LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.ConceptualEntityAttr);
			if (attributeValue != null)
			{
				JObject jobject2 = new JObject();
				LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "ConceptualEntity", attributeValue);
				LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Binding", jobject2, false);
			}
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "State", LinguisticSchemaXmlToJsonUpgrader.ConvertState(LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.SourceAttr)));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Weight", LinguisticSchemaXmlToJsonUpgrader.ConvertWeight(element), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "TemplateSchema", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.TemplateSchemaAttr));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Roles", LinguisticSchemaXmlToJsonUpgrader.ConvertRoles(element.Element(LinguisticSchemaUpgrader.SchemaConstants.RolesElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "SemanticSlots", LinguisticSchemaXmlToJsonUpgrader.ConvertSemanticSlots(element), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Conditions", LinguisticSchemaXmlToJsonUpgrader.ConvertConditions(element.Element(LinguisticSchemaUpgrader.SchemaConstants.ConditionElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Phrasings", LinguisticSchemaXmlToJsonUpgrader.ConvertPhrasings(element.Element(LinguisticSchemaUpgrader.SchemaConstants.PhrasingsElem)), false);
			return jobject;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000D3B4 File Offset: 0x0000B5B4
		private static JObject ConvertRoles(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			foreach (XElement xelement in element.Elements(LinguisticSchemaUpgrader.SchemaConstants.RoleElem))
			{
				LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(xelement, LinguisticSchemaUpgrader.SchemaConstants.NameAttr), LinguisticSchemaXmlToJsonUpgrader.ConvertRoleProperties(xelement), false);
			}
			return jobject;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000D424 File Offset: 0x0000B624
		private static JObject ConvertRoleProperties(XElement element)
		{
			JObject jobject = new JObject();
			JObject jobject2 = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Entity", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.EntityAttr));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Namespace", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.EntityNamespaceAttr));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Target", jobject2, false);
			string text = "Nouns";
			XElement xelement = element.Element(LinguisticSchemaUpgrader.SchemaConstants.NounsElem);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, text, LinguisticSchemaXmlToJsonUpgrader.ConvertTerms((xelement != null) ? xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.NounElem) : null), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Quantity", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.QuantityElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Amount", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.AmountElem)), false);
			return jobject;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0000D4DC File Offset: 0x0000B6DC
		private static JObject ConvertSemanticSlots(XElement element)
		{
			XElement xelement = element.Element(LinguisticSchemaUpgrader.SchemaConstants.WhereElem);
			XElement xelement2 = element.Element(LinguisticSchemaUpgrader.SchemaConstants.WhenElem);
			XElement xelement3 = element.Element(LinguisticSchemaUpgrader.SchemaConstants.DurationElem);
			XElement xelement4 = element.Element(LinguisticSchemaUpgrader.SchemaConstants.OccurrencesElem);
			if (xelement == null && xelement2 == null && xelement3 == null && xelement4 == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Where", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(xelement), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "When", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(xelement2), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Duration", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(xelement3), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Occurrences", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(xelement4), false);
			return jobject;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000D574 File Offset: 0x0000B774
		private static JObject ConvertRoleReference(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Role", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.RoleAttr));
			return jobject;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000D598 File Offset: 0x0000B798
		private static JArray ConvertConditions(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JArray jarray = new JArray();
			JObject jobject = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Target", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.TargetElement)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Aggregation", LinguisticSchemaXmlToJsonUpgrader.GetPropertyValue(element, LinguisticSchemaUpgrader.SchemaConstants.AggregationElem));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Operator", LinguisticSchemaXmlToJsonUpgrader.GetPropertyValue(element, LinguisticSchemaUpgrader.SchemaConstants.OperatorElem));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Value", LinguisticSchemaXmlToJsonUpgrader.ConvertValue(element), true);
			jarray.Add(jobject);
			return jarray;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0000D618 File Offset: 0x0000B818
		private static JObject ConvertValue(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			string text;
			if ((text = LinguisticSchemaXmlToJsonUpgrader.GetPropertyValue(element, LinguisticSchemaUpgrader.SchemaConstants.IntegerValueElem)) != null)
			{
				long num;
				if (!long.TryParse(text, out num))
				{
					return null;
				}
				LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Number", new JArray(new JValue(num)), false);
			}
			else if ((text = LinguisticSchemaXmlToJsonUpgrader.GetPropertyValue(element, LinguisticSchemaUpgrader.SchemaConstants.StringValueElem)) != null)
			{
				LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Text", new JArray(text), false);
			}
			else if ((text = LinguisticSchemaXmlToJsonUpgrader.GetPropertyValue(element, LinguisticSchemaUpgrader.SchemaConstants.NumberValueElem)) != null)
			{
				double num2;
				if (!double.TryParse(text, out num2))
				{
					return null;
				}
				LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Number", new JArray(new JValue(num2)), false);
			}
			else if ((text = LinguisticSchemaXmlToJsonUpgrader.GetPropertyValue(element, LinguisticSchemaUpgrader.SchemaConstants.BooleanValueElem)) != null)
			{
				bool flag;
				if (!bool.TryParse(text, out flag))
				{
					return null;
				}
				LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Boolean", new JArray(new JValue(flag)), false);
			}
			else if (LinguisticSchemaXmlToJsonUpgrader.GetPropertyValue(element, LinguisticSchemaUpgrader.SchemaConstants.NullValueElem) != null)
			{
				return null;
			}
			return jobject;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0000D704 File Offset: 0x0000B904
		private static JArray ConvertPhrasings(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JArray jarray = new JArray();
			foreach (XElement xelement in element.Elements())
			{
				if (xelement.Name.Equals(LinguisticSchemaUpgrader.SchemaConstants.AttributePhrasingElem))
				{
					jarray.Add(LinguisticSchemaXmlToJsonUpgrader.ConvertAttributePhrasing(xelement));
				}
				else if (xelement.Name.Equals(LinguisticSchemaUpgrader.SchemaConstants.AdjectivePhrasingElem))
				{
					jarray.Add(LinguisticSchemaXmlToJsonUpgrader.ConvertAdjectivePhrasing(xelement));
				}
				else if (xelement.Name.Equals(LinguisticSchemaUpgrader.SchemaConstants.DynamicAdjectivePhrasingElem))
				{
					jarray.Add(LinguisticSchemaXmlToJsonUpgrader.ConvertDynamicAdjectivePhrasing(xelement));
				}
				else if (xelement.Name.Equals(LinguisticSchemaUpgrader.SchemaConstants.NounPhrasingElem))
				{
					jarray.Add(LinguisticSchemaXmlToJsonUpgrader.ConvertNounPhrasing(xelement));
				}
				else if (xelement.Name.Equals(LinguisticSchemaUpgrader.SchemaConstants.DynamicNounPhrasingElem))
				{
					jarray.Add(LinguisticSchemaXmlToJsonUpgrader.ConvertDynamicNounPhrasing(xelement));
				}
				else if (xelement.Name.Equals(LinguisticSchemaUpgrader.SchemaConstants.NamePhrasingElem))
				{
					jarray.Add(LinguisticSchemaXmlToJsonUpgrader.ConvertNamePhrasing(xelement));
				}
				else if (xelement.Name.Equals(LinguisticSchemaUpgrader.SchemaConstants.PrepositionPhrasingElem))
				{
					jarray.Add(LinguisticSchemaXmlToJsonUpgrader.ConvertPrepositionPhrasing(xelement));
				}
				else if (xelement.Name.Equals(LinguisticSchemaUpgrader.SchemaConstants.VerbPhrasingElem))
				{
					jarray.Add(LinguisticSchemaXmlToJsonUpgrader.ConvertVerbPhrasing(xelement));
				}
			}
			return jarray;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0000D870 File Offset: 0x0000BA70
		private static JObject ConvertAttributePhrasing(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			JObject jobject2 = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Subject", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.SubjectElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Object", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.ObjectElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "PrepositionalPhrases", LinguisticSchemaXmlToJsonUpgrader.ConvertPrepositionalPhrases(element.Element(LinguisticSchemaUpgrader.SchemaConstants.PrepositionalPhrasesElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Attribute", jobject2, false);
			LinguisticSchemaXmlToJsonUpgrader.ConvertPhrasingCommonProperties(jobject, element);
			return jobject;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
		private static JObject ConvertAdjectivePhrasing(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			JObject jobject2 = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Subject", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.SubjectElem)), false);
			JObject jobject3 = jobject2;
			string text = "Adjectives";
			XElement xelement = element.Element(LinguisticSchemaUpgrader.SchemaConstants.AdjectivesElem);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject3, text, LinguisticSchemaXmlToJsonUpgrader.ConvertTerms((xelement != null) ? xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.AdjectiveElem) : null), false);
			JObject jobject4 = jobject2;
			string text2 = "Antonyms";
			XElement xelement2 = element.Element(LinguisticSchemaUpgrader.SchemaConstants.AntonymsElem);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject4, text2, LinguisticSchemaXmlToJsonUpgrader.ConvertTerms((xelement2 != null) ? xelement2.Elements(LinguisticSchemaUpgrader.SchemaConstants.AntonymElem) : null), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Measurement", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.MeasurementElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "PrepositionalPhrases", LinguisticSchemaXmlToJsonUpgrader.ConvertPrepositionalPhrases(element.Element(LinguisticSchemaUpgrader.SchemaConstants.PrepositionalPhrasesElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Adjective", jobject2, false);
			LinguisticSchemaXmlToJsonUpgrader.ConvertPhrasingCommonProperties(jobject, element);
			return jobject;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0000D9D8 File Offset: 0x0000BBD8
		private static JObject ConvertDynamicAdjectivePhrasing(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			JObject jobject2 = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Subject", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.SubjectElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Adjective", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.AdjectiveElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "PrepositionalPhrases", LinguisticSchemaXmlToJsonUpgrader.ConvertPrepositionalPhrases(element.Element(LinguisticSchemaUpgrader.SchemaConstants.PrepositionalPhrasesElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "DynamicAdjective", jobject2, false);
			LinguisticSchemaXmlToJsonUpgrader.ConvertPhrasingCommonProperties(jobject, element);
			return jobject;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0000DA60 File Offset: 0x0000BC60
		private static JObject ConvertNounPhrasing(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			JObject jobject2 = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Subject", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.SubjectElem)), false);
			JObject jobject3 = jobject2;
			string text = "Nouns";
			XElement xelement = element.Element(LinguisticSchemaUpgrader.SchemaConstants.NounsElem);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject3, text, LinguisticSchemaXmlToJsonUpgrader.ConvertTerms((xelement != null) ? xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.NounElem) : null), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "PrepositionalPhrases", LinguisticSchemaXmlToJsonUpgrader.ConvertPrepositionalPhrases(element.Element(LinguisticSchemaUpgrader.SchemaConstants.PrepositionalPhrasesElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Noun", jobject2, false);
			LinguisticSchemaXmlToJsonUpgrader.ConvertPhrasingCommonProperties(jobject, element);
			return jobject;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
		private static JObject ConvertDynamicNounPhrasing(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			JObject jobject2 = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Subject", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.SubjectElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Noun", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.NounElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "DynamicNoun", jobject2, false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "PrepositionalPhrases", LinguisticSchemaXmlToJsonUpgrader.ConvertPrepositionalPhrases(element.Element(LinguisticSchemaUpgrader.SchemaConstants.PrepositionalPhrasesElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.ConvertPhrasingCommonProperties(jobject, element);
			return jobject;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0000DB80 File Offset: 0x0000BD80
		private static JObject ConvertNamePhrasing(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			JObject jobject2 = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Subject", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.SubjectElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Name", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.NameElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "PrepositionalPhrases", LinguisticSchemaXmlToJsonUpgrader.ConvertPrepositionalPhrases(element.Element(LinguisticSchemaUpgrader.SchemaConstants.PrepositionalPhrasesElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Name", jobject2, false);
			LinguisticSchemaXmlToJsonUpgrader.ConvertPhrasingCommonProperties(jobject, element);
			return jobject;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0000DC08 File Offset: 0x0000BE08
		private static JObject ConvertPrepositionPhrasing(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			JObject jobject2 = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Subject", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.SubjectElem)), false);
			XElement xelement = element.Element(LinguisticSchemaUpgrader.SchemaConstants.PrepositionalPhraseElem);
			JObject jobject3 = jobject2;
			string text = "Prepositions";
			XElement xelement2 = xelement.Element(LinguisticSchemaUpgrader.SchemaConstants.PrepositionsElem);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject3, text, LinguisticSchemaXmlToJsonUpgrader.ConvertTerms((xelement2 != null) ? xelement2.Elements(LinguisticSchemaUpgrader.SchemaConstants.PrepositionElem) : null), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Object", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(xelement.Element(LinguisticSchemaUpgrader.SchemaConstants.ObjectElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "PrepositionalPhrases", LinguisticSchemaXmlToJsonUpgrader.ConvertPrepositionalPhrases(element.Element(LinguisticSchemaUpgrader.SchemaConstants.PrepositionalPhrasesElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Preposition", jobject2, false);
			LinguisticSchemaXmlToJsonUpgrader.ConvertPhrasingCommonProperties(jobject, element);
			return jobject;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
		private static JObject ConvertVerbPhrasing(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JObject jobject = new JObject();
			JObject jobject2 = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Subject", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.SubjectElem)), false);
			JObject jobject3 = jobject2;
			string text = "Verbs";
			XElement xelement = element.Element(LinguisticSchemaUpgrader.SchemaConstants.VerbsElem);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject3, text, LinguisticSchemaXmlToJsonUpgrader.ConvertTerms((xelement != null) ? xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.VerbElem) : null), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "IndirectObject", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.IndirectObjectElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Object", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.ObjectElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "PrepositionalPhrases", LinguisticSchemaXmlToJsonUpgrader.ConvertPrepositionalPhrases(element.Element(LinguisticSchemaUpgrader.SchemaConstants.PrepositionalPhrasesElem)), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Verb", jobject2, false);
			LinguisticSchemaXmlToJsonUpgrader.ConvertPhrasingCommonProperties(jobject, element);
			return jobject;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0000DD98 File Offset: 0x0000BF98
		private static JArray ConvertPrepositionalPhrases(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			JArray jarray = new JArray();
			foreach (XElement xelement in element.Elements(LinguisticSchemaUpgrader.SchemaConstants.PrepositionalPhraseElem))
			{
				jarray.Add(LinguisticSchemaXmlToJsonUpgrader.ConvertPrepositionPhrase(xelement));
			}
			return jarray;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0000DDFC File Offset: 0x0000BFFC
		private static JObject ConvertPrepositionPhrase(XElement element)
		{
			JObject jobject = new JObject();
			string text = "Prepositions";
			XElement xelement = element.Element(LinguisticSchemaUpgrader.SchemaConstants.PrepositionsElem);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, text, LinguisticSchemaXmlToJsonUpgrader.ConvertTerms((xelement != null) ? xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.PrepositionElem) : null), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "Object", LinguisticSchemaXmlToJsonUpgrader.ConvertRoleReference(element.Element(LinguisticSchemaUpgrader.SchemaConstants.ObjectElem)), false);
			return jobject;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0000DE58 File Offset: 0x0000C058
		private static void ConvertPhrasingCommonProperties(JObject result, XElement element)
		{
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(result, "State", LinguisticSchemaXmlToJsonUpgrader.ConvertState(LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.SourceAttr)));
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(result, "Weight", LinguisticSchemaXmlToJsonUpgrader.ConvertWeight(element), false);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(result, "TemplateSchema", LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.TemplateSchemaAttr));
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		private static JObject ConvertGlobalSubstitution(XElement element)
		{
			JObject jobject = new JObject();
			string text = LinguisticSchemaXmlToJsonUpgrader.GetPropertyValue(element, LinguisticSchemaUpgrader.SchemaConstants.OriginalTermElem);
			string text2 = LinguisticSchemaXmlToJsonUpgrader.GetPropertyValue(element, LinguisticSchemaUpgrader.SchemaConstants.TargetTermElem);
			string text3 = LinguisticSchemaXmlToJsonUpgrader.ConvertState(LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.SourceAttr));
			string attributeValue = LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.TemplateSchemaAttr);
			text = LinguisticSchemaXmlToJsonUpgrader.NormalizeNewlinesToSpace(text);
			text2 = LinguisticSchemaXmlToJsonUpgrader.NormalizeNewlinesToSpace(text2);
			JObject jobject2 = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "Substitute", text2);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "State", text3);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, "TemplateSchema", attributeValue);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, text, jobject2, false);
			return jobject;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0000DF38 File Offset: 0x0000C138
		private static JObject ConvertExample(XElement element)
		{
			JObject jobject = new JObject();
			string attributeValue = LinguisticSchemaXmlToJsonUpgrader.GetAttributeValue(element, LinguisticSchemaUpgrader.SchemaConstants.TemplateSchemaAttr);
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject, "TemplateSchema", attributeValue);
			string text = LinguisticSchemaXmlToJsonUpgrader.NormalizeNewlinesToSpace(element.Value);
			JObject jobject2 = new JObject();
			LinguisticSchemaXmlToJsonUpgrader.AddProperty(jobject2, text, jobject, false);
			return jobject2;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0000DF7D File Offset: 0x0000C17D
		private static string NormalizeNewlinesToSpace(string s)
		{
			return LinguisticSchemaXmlToJsonUpgrader._newlineSequenceRegex.Replace(s, " ");
		}

		// Token: 0x040006E5 RID: 1765
		private const string TargetVersion = "3.1.0";

		// Token: 0x040006E6 RID: 1766
		private static readonly Regex _newlineSequenceRegex = new Regex("[\\r\\n]+", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040006E7 RID: 1767
		private readonly XElement _rootElement;
	}
}
