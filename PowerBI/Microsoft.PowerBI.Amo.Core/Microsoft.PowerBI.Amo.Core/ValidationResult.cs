using System;
using System.Collections.Specialized;
using System.Xml;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000CF RID: 207
	public sealed class ValidationResult
	{
		// Token: 0x06000978 RID: 2424 RVA: 0x00029FD4 File Offset: 0x000281D4
		internal ValidationResult(ModelComponent source, ValidationRule rule, string[] parameters)
		{
			this.source = source;
			this.sourcePath = null;
			this.sourceType = null;
			this.rule = rule;
			this.parameters = parameters;
			this.description = null;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0002A006 File Offset: 0x00028206
		internal ValidationResult(ModelComponent source, string description, ValidationRule rule)
		{
			this.source = source;
			this.sourcePath = null;
			this.sourceType = null;
			this.rule = rule;
			this.parameters = null;
			this.description = description;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0002A038 File Offset: 0x00028238
		private ValidationResult(string sourcePath, string sourceType, ValidationRule rule, StringCollection parameters)
		{
			this.source = null;
			this.sourcePath = sourcePath;
			this.sourceType = sourceType;
			this.rule = rule;
			if (parameters == null || parameters.Count == 0)
			{
				this.parameters = null;
			}
			else
			{
				this.parameters = new string[parameters.Count];
				parameters.CopyTo(this.parameters, 0);
			}
			this.description = null;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0002A0A4 File Offset: 0x000282A4
		public ModelComponent Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0002A0AC File Offset: 0x000282AC
		public string SourcePath
		{
			get
			{
				if (this.sourcePath != null)
				{
					return this.sourcePath;
				}
				if (this.source != null)
				{
					return this.source.GetObjectPath();
				}
				return null;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0002A0D2 File Offset: 0x000282D2
		public string SourceType
		{
			get
			{
				if (this.sourceType != null)
				{
					return this.sourceType;
				}
				if (this.source != null)
				{
					return this.source.GetType().Name;
				}
				return null;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0002A0FD File Offset: 0x000282FD
		public ValidationRule Rule
		{
			get
			{
				return this.rule;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0002A105 File Offset: 0x00028305
		public string Description
		{
			get
			{
				if (this.description == null)
				{
					return this.rule.GetDescription(this.parameters);
				}
				return this.description;
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0002A127 File Offset: 0x00028327
		public override string ToString()
		{
			if (this.source != null)
			{
				return ValidationSR.FullErrorText(this.source.FriendlyPath, this.Description);
			}
			return this.Description;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0002A150 File Offset: 0x00028350
		internal XmlNode AppendTo(XmlNode node)
		{
			XmlDocument ownerDocument = node.OwnerDocument;
			XmlNode xmlNode = node.AppendChild(ownerDocument.CreateNode(XmlNodeType.Element, "Result", "B77886FF900E4C18A95F79A6BFA488A9"));
			XmlAttributeCollection attributes = xmlNode.Attributes;
			attributes.Append(ownerDocument.CreateAttribute("sourcePath")).Value = this.SourcePath;
			attributes.Append(ownerDocument.CreateAttribute("sourceType")).Value = this.SourceType;
			attributes.Append(ownerDocument.CreateAttribute("rule")).Value = XmlConvert.ToString(this.rule.ID);
			if (this.parameters != null)
			{
				int i = 0;
				int num = this.parameters.Length;
				while (i < num)
				{
					attributes.Append(ownerDocument.CreateAttribute("param" + i.ToString())).Value = this.parameters[i];
					i++;
				}
			}
			return xmlNode;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0002A22C File Offset: 0x0002842C
		internal static ValidationResult TryToLoadFrom(XmlNode node)
		{
			XmlAttribute xmlAttribute = node.Attributes["sourcePath"];
			if (xmlAttribute == null)
			{
				return null;
			}
			string text = xmlAttribute.Value;
			if (string.IsNullOrEmpty(text))
			{
				text = null;
			}
			xmlAttribute = node.Attributes["sourceType"];
			if (xmlAttribute == null)
			{
				return null;
			}
			string text2 = xmlAttribute.Value;
			if (string.IsNullOrEmpty(text2))
			{
				text2 = null;
			}
			xmlAttribute = node.Attributes["rule"];
			if (xmlAttribute == null)
			{
				return null;
			}
			int num;
			if (!int.TryParse(xmlAttribute.Value, out num))
			{
				return null;
			}
			ValidationRule validationRule = ValidationRule.Find(num);
			if (validationRule == null)
			{
				return null;
			}
			xmlAttribute = node.Attributes["param0"];
			if (xmlAttribute == null)
			{
				return new ValidationResult(text, text2, validationRule, null);
			}
			StringCollection stringCollection = new StringCollection();
			int num2 = 1;
			while (xmlAttribute != null)
			{
				stringCollection.Add(xmlAttribute.Value);
				xmlAttribute = node.Attributes["param" + num2.ToString()];
				num2++;
			}
			return new ValidationResult(text, text2, validationRule, stringCollection);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0002A328 File Offset: 0x00028528
		internal bool IsTheSame(ValidationResult obj)
		{
			if (this.rule != obj.rule)
			{
				return false;
			}
			if (this.SourceType != obj.SourceType)
			{
				return false;
			}
			if (this.SourcePath != obj.SourcePath)
			{
				return false;
			}
			if (this.parameters == null)
			{
				if (obj.parameters != null && obj.parameters.Length != 0)
				{
					return false;
				}
			}
			else
			{
				if (obj.parameters == null)
				{
					return this.parameters.Length == 0;
				}
				if (this.parameters.Length != obj.parameters.Length)
				{
					return false;
				}
				int i = 0;
				int num = this.parameters.Length;
				while (i < num)
				{
					if (this.parameters[i] != obj.parameters[i])
					{
						return false;
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x0400070F RID: 1807
		private ModelComponent source;

		// Token: 0x04000710 RID: 1808
		private string sourcePath;

		// Token: 0x04000711 RID: 1809
		private string sourceType;

		// Token: 0x04000712 RID: 1810
		private ValidationRule rule;

		// Token: 0x04000713 RID: 1811
		private string[] parameters;

		// Token: 0x04000714 RID: 1812
		private string description;
	}
}
