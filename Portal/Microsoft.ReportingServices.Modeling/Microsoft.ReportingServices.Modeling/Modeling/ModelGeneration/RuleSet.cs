using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000FC RID: 252
	public sealed class RuleSet : IXmlLoadable, IXmlObjectFactory
	{
		// Token: 0x06000C91 RID: 3217 RVA: 0x0002965A File Offset: 0x0002785A
		private void Reset()
		{
			this.m_name = string.Empty;
			this.m_description = string.Empty;
			this.m_rules.Clear();
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0002967D File Offset: 0x0002787D
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x00029685 File Offset: 0x00027885
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value ?? string.Empty;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00029697 File Offset: 0x00027897
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x0002969F File Offset: 0x0002789F
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value ?? string.Empty;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x000296B1 File Offset: 0x000278B1
		public RuleCollection Rules
		{
			get
			{
				return this.m_rules;
			}
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x000296B9 File Offset: 0x000278B9
		public IEnumerable<EvaluateDsvItemRule> GetDsvItemRules()
		{
			foreach (Rule rule in this.m_rules)
			{
				if (rule is EvaluateDsvItemRule)
				{
					yield return (EvaluateDsvItemRule)rule;
				}
			}
			List<Rule>.Enumerator enumerator = default(List<Rule>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000296C9 File Offset: 0x000278C9
		public IEnumerable<ProcessingRule> GetProcessingRules()
		{
			foreach (Rule rule in this.m_rules)
			{
				if (rule is ProcessingRule)
				{
					yield return (ProcessingRule)rule;
				}
			}
			List<Rule>.Enumerator enumerator = default(List<Rule>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x000296D9 File Offset: 0x000278D9
		internal IEnumerable<EvaluateDsvItemRule> GetEnabledDsvItemRules()
		{
			foreach (Rule rule in this.m_rules)
			{
				if (rule is EvaluateDsvItemRule && rule.Enabled)
				{
					yield return (EvaluateDsvItemRule)rule;
				}
			}
			List<Rule>.Enumerator enumerator = default(List<Rule>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x000296E9 File Offset: 0x000278E9
		internal IEnumerable<ProcessingRule> GetEnabledProcessingRules()
		{
			foreach (Rule rule in this.m_rules)
			{
				if (rule is ProcessingRule && rule.Enabled)
				{
					yield return (ProcessingRule)rule;
				}
			}
			List<Rule>.Enumerator enumerator = default(List<Rule>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000296F9 File Offset: 0x000278F9
		public void Load(XmlReader xr)
		{
			this.Load(xr, null, null);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00029704 File Offset: 0x00027904
		public void Load(XmlReader xr, CultureInfo displayCulture, CultureInfo modelCulture)
		{
			if (displayCulture != null || modelCulture != null)
			{
				string content = null;
				XmlUtil.WrapXmlExceptions(delegate
				{
					content = XmlFragmentUtil.ToXmlString(delegate(XmlWriter xw)
					{
						xw.WriteNode(xr, false);
					});
				}, ModelingErrorCode.InvalidModelGenerationRules, new XmlUtil.ErrorMessageWrap(SRErrors.InvalidModelGenerationRules));
				if (displayCulture != null)
				{
					content = RuleSet.m_displayCultureTokenRegex.Replace(content, this.ReplaceTokenEvaluator(displayCulture));
				}
				if (modelCulture != null)
				{
					content = RuleSet.m_modelCultureTokenRegex.Replace(content, this.ReplaceTokenEvaluator(modelCulture));
				}
				xr = XmlFragmentUtil.ReadXmlString(content);
			}
			DeserializationContext deserializationContext = new DeserializationContext(null, null);
			ModelingXmlReader mxr = new ModelingXmlReader(xr, ModelGenerationSchema.Instance, deserializationContext);
			this.Reset();
			XmlUtil.WrapXmlExceptions(delegate
			{
				XmlUtil.CheckElement(mxr.Reader, "rules", "http://schemas.microsoft.com/sqlserver/2004/10/modelgeneration");
				mxr.LoadObject(this);
			}, ModelingErrorCode.InvalidModelGenerationRules, new XmlUtil.ErrorMessageWrap(SRErrors.InvalidModelGenerationRules));
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x000297F9 File Offset: 0x000279F9
		private MatchEvaluator ReplaceTokenEvaluator(CultureInfo culture)
		{
			return delegate(Match m)
			{
				string value = m.Groups["resName"].Value;
				string @string = RuleSet.m_ruleTokenRes.GetString(value, culture);
				if (@string != null)
				{
					return @string;
				}
				return m.Value;
			};
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00029814 File Offset: 0x00027A14
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "name")
				{
					this.Name = xr.ReadValueAsString();
					return true;
				}
				if (localName == "description")
				{
					this.Description = xr.ReadValueAsString();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00029869 File Offset: 0x00027A69
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "rule")
			{
				this.m_rules.Add(this.ObjectFactory.CreateRule(xr));
				return true;
			}
			return false;
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0002989F File Offset: 0x00027A9F
		private IXmlObjectFactory ObjectFactory
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000298A4 File Offset: 0x00027AA4
		private T CreateObject<T>(ModelingXmlReader xr)
		{
			if (xr.SchemaInfo == null)
			{
				throw new InternalModelingException("No SchemaInfo for " + xr.LocalName + " element");
			}
			XmlQualifiedName qualifiedName = xr.SchemaInfo.SchemaType.QualifiedName;
			Type type;
			if (!RuleSet.m_typeMappings.TryGetValue(qualifiedName, out type))
			{
				string text = "Unrecognized type ";
				XmlQualifiedName xmlQualifiedName = qualifiedName;
				throw new RuleConfigurationException(text + ((xmlQualifiedName != null) ? xmlQualifiedName.ToString() : null));
			}
			if (!typeof(T).IsAssignableFrom(type))
			{
				string[] array = new string[5];
				array[0] = "Unexpected type '";
				int num = 1;
				Type type2 = type;
				array[num] = ((type2 != null) ? type2.ToString() : null);
				array[2] = "': must derive from '";
				int num2 = 3;
				Type typeFromHandle = typeof(T);
				array[num2] = ((typeFromHandle != null) ? typeFromHandle.ToString() : null);
				array[4] = "'";
				throw new RuleConfigurationException(string.Concat(array));
			}
			return (T)((object)Activator.CreateInstance(type));
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00029980 File Offset: 0x00027B80
		Rule IXmlObjectFactory.CreateRule(ModelingXmlReader xr)
		{
			Rule rule = this.CreateObject<Rule>(xr);
			rule.Load(xr, this);
			return rule;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00029991 File Offset: 0x00027B91
		Filter IXmlObjectFactory.CreateFilter(ModelingXmlReader xr)
		{
			Filter filter = this.CreateObject<Filter>(xr);
			filter.Load(xr, this);
			return filter;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x000299A4 File Offset: 0x00027BA4
		private static Dictionary<XmlQualifiedName, Type> GetTypeMappings()
		{
			Dictionary<XmlQualifiedName, Type> dictionary = new Dictionary<XmlQualifiedName, Type>();
			RuleSet.AddTypeMapping(dictionary, "EvaluateDsvItemRule", typeof(EvaluateDsvItemRule));
			RuleSet.AddTypeMapping(dictionary, "CreateEntityRule", typeof(CreateEntityRule));
			RuleSet.AddTypeMapping(dictionary, "CreateColumnEntityRule", typeof(CreateColumnEntityRule));
			RuleSet.AddTypeMapping(dictionary, "CreateCountAttributeRule", typeof(CreateCountAttributeRule));
			RuleSet.AddTypeMapping(dictionary, "CreateAttributeRule", typeof(CreateAttributeRule));
			RuleSet.AddTypeMapping(dictionary, "CreateVariationAttributeRule", typeof(CreateVariationAttributeRule));
			RuleSet.AddTypeMapping(dictionary, "CreateRoleRule", typeof(CreateRoleRule));
			RuleSet.AddTypeMapping(dictionary, "SetEntityPropertiesRule", typeof(SetEntityPropertiesRule));
			RuleSet.AddTypeMapping(dictionary, "SetEntityAttributesRule", typeof(SetEntityAttributesRule));
			RuleSet.AddTypeMapping(dictionary, "SetAttributePropertiesRule", typeof(SetAttributePropertiesRule));
			RuleSet.AddTypeMapping(dictionary, "FilterGroup", typeof(FilterGroup));
			RuleSet.AddTypeMapping(dictionary, "PropertyFilter", typeof(PropertyFilter));
			RuleSet.AddTypeMapping(dictionary, "NameFilter", typeof(NameFilter));
			RuleSet.AddTypeMapping(dictionary, "RowCountFilter", typeof(RowCountFilter));
			RuleSet.AddTypeMapping(dictionary, "FieldCountFilter", typeof(FieldCountFilter));
			return dictionary;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00029AF1 File Offset: 0x00027CF1
		private static void AddTypeMapping(Dictionary<XmlQualifiedName, Type> dict, string name, Type type)
		{
			dict.Add(new XmlQualifiedName(name, "http://schemas.microsoft.com/sqlserver/2004/10/modelgeneration"), type);
		}

		// Token: 0x04000535 RID: 1333
		private const string RulesElem = "rules";

		// Token: 0x04000536 RID: 1334
		private const string NameAttr = "name";

		// Token: 0x04000537 RID: 1335
		private const string DescriptionAttr = "description";

		// Token: 0x04000538 RID: 1336
		private static readonly Regex m_displayCultureTokenRegex = new Regex("\\(\\$(?<resName>\\w+)\\)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04000539 RID: 1337
		private static readonly Regex m_modelCultureTokenRegex = new Regex("\\{\\$(?<resName>\\w+)\\}", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x0400053A RID: 1338
		private const string ResNameGroup = "resName";

		// Token: 0x0400053B RID: 1339
		private static readonly ResourceManager m_ruleTokenRes = new ResourceManager(typeof(RuleSet).Namespace + ".SRRuleTokens", typeof(RuleSet).Assembly);

		// Token: 0x0400053C RID: 1340
		private static readonly Dictionary<XmlQualifiedName, Type> m_typeMappings = RuleSet.GetTypeMappings();

		// Token: 0x0400053D RID: 1341
		private string m_name = string.Empty;

		// Token: 0x0400053E RID: 1342
		private string m_description = string.Empty;

		// Token: 0x0400053F RID: 1343
		private readonly RuleCollection m_rules = new RuleCollection();
	}
}
