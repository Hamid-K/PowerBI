using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000CF RID: 207
	public sealed class CreateVariationAttributeRule : ProcessingRule, IColumnProcessingRule
	{
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x00026219 File Offset: 0x00024419
		public override int ProcessOnPass
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0002621C File Offset: 0x0002441C
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x00026224 File Offset: 0x00024424
		public bool SetFirstAsDefaultAggregate
		{
			get
			{
				return this.m_setFirstAsDefaultAggregate;
			}
			set
			{
				this.m_setFirstAsDefaultAggregate = value;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0002622D File Offset: 0x0002442D
		public CheckedCollection<IXPathNavigable> AttributeFragments
		{
			get
			{
				return this.m_attributeFragments;
			}
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00026238 File Offset: 0x00024438
		RuleProcessResult IColumnProcessingRule.Process(DsvColumn column, ExistingColumnBindingInfo existingInfo)
		{
			if (existingInfo.Attribute == null)
			{
				return base.ProcessSkipped(new string[] { SR.Rules_AttributeDoesNotExist });
			}
			if (!existingInfo.EvaluateDependentRules)
			{
				return base.ProcessDependentRulesSkipped(existingInfo.Attribute);
			}
			ModelAttribute[] array = new ModelAttribute[this.m_attributeFragments.Count];
			for (int i = 0; i < this.m_attributeFragments.Count; i++)
			{
				ModelAttribute attribute = existingInfo.Attribute;
				array[i] = this.CreateVariationAttribute(this.m_attributeFragments[i], attribute);
				if (this.m_setFirstAsDefaultAggregate && i == 0)
				{
					attribute.DefaultAggregate = array[i];
				}
			}
			return base.ProcessCreatedModelItems(array);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x000262D8 File Offset: 0x000244D8
		private ModelAttribute CreateVariationAttribute(IXPathNavigable attributeFragment, ModelAttribute parentAttribute)
		{
			string text = base.ApplyRenamers(parentAttribute.Name);
			if (attributeFragment != null)
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlReader xmlReader = attributeFragment.CreateNavigator().ReadSubtree();
				xmlDocument.Load(xmlReader);
				base.ReplaceIdTokens(xmlDocument, new KeyValuePair<string, QName>[]
				{
					new KeyValuePair<string, QName>("{AttributeID}", parentAttribute.ID)
				});
				base.ReplaceStringTokens(xmlDocument, new KeyValuePair<string, string>[]
				{
					new KeyValuePair<string, string>("{AttributeName}", text)
				});
				attributeFragment = xmlDocument;
			}
			ModelAttribute modelAttribute = new ModelAttribute();
			modelAttribute.Name = text;
			parentAttribute.Variations.Add(modelAttribute);
			base.FinalizeModelItem(modelAttribute, attributeFragment, null);
			if (modelAttribute.Expression != null)
			{
				modelAttribute.UpdateFromExpression();
			}
			return modelAttribute;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00026385 File Offset: 0x00024585
		internal override bool LoadXmlAttribute(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "setFirstAsDefaultAggregate")
			{
				this.m_setFirstAsDefaultAggregate = xr.ReadValueAsBoolean();
				return true;
			}
			return base.LoadXmlAttribute(xr, objectFactory);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x000263B8 File Offset: 0x000245B8
		internal override bool LoadXmlElement(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.NamespaceURI == "http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling" && xr.LocalName == "Attribute")
			{
				this.m_attributeFragments.Add(xr.ReadFragment());
				return true;
			}
			return base.LoadXmlElement(xr, objectFactory);
		}

		// Token: 0x040004B1 RID: 1201
		private const string SetFirstAsDefaultAggregateAttr = "setFirstAsDefaultAggregate";

		// Token: 0x040004B2 RID: 1202
		private const string AttributeIdToken = "{AttributeID}";

		// Token: 0x040004B3 RID: 1203
		private const string AttributeNameToken = "{AttributeName}";

		// Token: 0x040004B4 RID: 1204
		private bool m_setFirstAsDefaultAggregate;

		// Token: 0x040004B5 RID: 1205
		private readonly CheckedCollection<IXPathNavigable> m_attributeFragments = new CheckedCollection<IXPathNavigable>();
	}
}
