using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000FE RID: 254
	public sealed class SetAttributePropertiesRule : AttributeRuleBase, IColumnProcessingRule
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00029B81 File Offset: 0x00027D81
		public override int ProcessOnPass
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00029B84 File Offset: 0x00027D84
		RuleProcessResult IColumnProcessingRule.Process(DsvColumn column, ExistingColumnBindingInfo existingInfo)
		{
			ModelAttribute attribute = existingInfo.Attribute;
			if (attribute == null)
			{
				return base.ProcessSkipped(new string[] { SR.Rules_AttributeDoesNotExist });
			}
			if (!existingInfo.EvaluateDependentRules)
			{
				return base.ProcessDependentRulesSkipped(attribute);
			}
			if (base.AttributeFragment != null)
			{
				attribute.LoadFragment(this.ReplaceTokens(base.AttributeFragment, column));
			}
			if (base.FolderFragment != null)
			{
				base.MoveToFolder(attribute, base.FolderFragment);
			}
			return base.ProcessModifiedItem(attribute);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00029BF8 File Offset: 0x00027DF8
		private IXPathNavigable ReplaceTokens(IXPathNavigable fragment, DsvColumn column)
		{
			string text = ((column.AvgScale != null) ? ((int)Math.Ceiling((double)column.AvgScale.Value)).ToString(CultureInfo.InvariantCulture) : "2");
			string text2 = ((column.MaxScale != null) ? column.MaxScale.Value.ToString(CultureInfo.InvariantCulture) : "");
			XmlDocument xmlDocument = new XmlDocument();
			XmlReader xmlReader = fragment.CreateNavigator().ReadSubtree();
			xmlDocument.Load(xmlReader);
			base.ReplaceStringTokens(xmlDocument, new KeyValuePair<string, string>[]
			{
				new KeyValuePair<string, string>("{AvgScale}", text),
				new KeyValuePair<string, string>("{MaxScale}", text2)
			});
			return xmlDocument;
		}

		// Token: 0x04000540 RID: 1344
		private const string AvgScaleToken = "{AvgScale}";

		// Token: 0x04000541 RID: 1345
		private const string MaxScaleToken = "{MaxScale}";
	}
}
