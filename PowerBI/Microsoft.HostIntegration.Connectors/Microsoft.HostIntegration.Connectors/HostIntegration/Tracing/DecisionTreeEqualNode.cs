using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.HostIntegration.StrictResources.TracingGlobals;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000651 RID: 1617
	public class DecisionTreeEqualNode : DecisionTreeNode
	{
		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06003611 RID: 13841 RVA: 0x000B6E7C File Offset: 0x000B507C
		public int PropertyIdentifier
		{
			get
			{
				return this.propertyIdentifier;
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06003612 RID: 13842 RVA: 0x000B6E84 File Offset: 0x000B5084
		// (set) Token: 0x06003613 RID: 13843 RVA: 0x000B6E8C File Offset: 0x000B508C
		public bool IsInteger
		{
			get
			{
				return this.isInteger;
			}
			protected set
			{
				this.isInteger = value;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06003614 RID: 13844 RVA: 0x000B6E95 File Offset: 0x000B5095
		public object PropertyValue
		{
			get
			{
				if (!this.isInteger)
				{
					return this.propertyStringValue;
				}
				return this.propertyIntegerValue;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06003615 RID: 13845 RVA: 0x000B6EB4 File Offset: 0x000B50B4
		public override string Name
		{
			get
			{
				ITracePointPropertyInformation tracePointPropertyInformation = this.traceContainer.PropertyInformationFromIdentifier(this.propertyIdentifier);
				string name = tracePointPropertyInformation.Name;
				string text = null;
				if (!this.isInteger)
				{
					text = this.propertyStringValue;
				}
				else
				{
					if (tracePointPropertyInformation.ValueType == PropertyType.Enumeration)
					{
						using (List<ITracePointPropertyEnumerationValue>.Enumerator enumerator = tracePointPropertyInformation.EnumerationValues.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ITracePointPropertyEnumerationValue tracePointPropertyEnumerationValue = enumerator.Current;
								if (tracePointPropertyEnumerationValue.Identifier == this.propertyIntegerValue)
								{
									text = tracePointPropertyEnumerationValue.Name;
									break;
								}
							}
							goto IL_008A;
						}
					}
					text = this.propertyIntegerValue.ToString();
				}
				IL_008A:
				if (this.notted)
				{
					return name + " != " + text;
				}
				return name + " == " + text;
			}
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x000B6F7C File Offset: 0x000B517C
		public DecisionTreeEqualNode(DecisionTree tree, XmlNode node)
			: base(tree, node, true)
		{
			XmlAttribute xmlAttribute = node.Attributes["property"];
			if (xmlAttribute == null)
			{
				throw new TraceException(SR.EqualNodePropertyAttribute);
			}
			XmlAttribute xmlAttribute2 = node.Attributes["value"];
			if (xmlAttribute2 == null)
			{
				throw new TraceException(SR.EqualNodeValueAttribute);
			}
			int num = (this.notted ? 3 : 2);
			if (node.Attributes.Count != num)
			{
				throw new TraceException(SR.EqualNodeAllAttributes);
			}
			if (node.HasChildNodes)
			{
				throw new TraceException(SR.EqualNodeNoChildren);
			}
			string value = xmlAttribute.Value;
			ITracePointPropertyInformation tracePointPropertyInformation = this.traceContainer.PropertyInformationFromName(value);
			this.propertyIdentifier = tracePointPropertyInformation.Identifier;
			string value2 = xmlAttribute2.Value;
			switch (tracePointPropertyInformation.ValueType)
			{
			case PropertyType.Integer:
				if (!int.TryParse(value2, out this.propertyIntegerValue))
				{
					throw new TraceException(SR.EnumerationValuePropertyInteger(value2, tracePointPropertyInformation.Name));
				}
				this.isInteger = true;
				break;
			case PropertyType.String:
				this.propertyStringValue = value2;
				this.isInteger = false;
				break;
			case PropertyType.Enumeration:
			{
				bool flag = false;
				foreach (ITracePointPropertyEnumerationValue tracePointPropertyEnumerationValue in tracePointPropertyInformation.EnumerationValues)
				{
					if (string.Compare(tracePointPropertyEnumerationValue.Name, value2, StringComparison.InvariantCulture) == 0)
					{
						flag = true;
						this.propertyIntegerValue = tracePointPropertyEnumerationValue.Identifier;
						this.isInteger = true;
						break;
					}
				}
				if (!flag)
				{
					throw new TraceException(SR.EnumerationValueProperty(value2, tracePointPropertyInformation.Name));
				}
				break;
			}
			}
			tree.AddPropertyEqualNode(this);
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x000B7118 File Offset: 0x000B5318
		protected override EvaluationResult InternalEvaluate()
		{
			if (this.isInteger)
			{
				if (this.notted)
				{
					if (this.integerValue == this.propertyIntegerValue)
					{
						return EvaluationResult.False;
					}
					return EvaluationResult.True;
				}
				else
				{
					if (this.integerValue != this.propertyIntegerValue)
					{
						return EvaluationResult.False;
					}
					return EvaluationResult.True;
				}
			}
			else if (this.notted)
			{
				if (string.Compare(this.stringValue, this.propertyStringValue, StringComparison.InvariantCulture) == 0)
				{
					return EvaluationResult.False;
				}
				return EvaluationResult.True;
			}
			else
			{
				if (string.Compare(this.stringValue, this.propertyStringValue, StringComparison.InvariantCulture) != 0)
				{
					return EvaluationResult.False;
				}
				return EvaluationResult.True;
			}
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x000B7190 File Offset: 0x000B5390
		protected override void InternalSetValue(object value)
		{
			if (this.isInteger)
			{
				if (!(value is int))
				{
					throw new TraceException(SR.PropertyStringIncorrectlySet(this.traceContainer.PropertyInformationFromIdentifier(this.propertyIdentifier).Name));
				}
				this.integerValue = (int)value;
				return;
			}
			else
			{
				if (!(value is string))
				{
					throw new TraceException(SR.PropertyNonStringIncorrectlySet(this.traceContainer.PropertyInformationFromIdentifier(this.propertyIdentifier).Name));
				}
				this.stringValue = (string)value;
				return;
			}
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x000189CC File Offset: 0x00016BCC
		public override DecisionTreeNode AddEqualNode(int identifier, int integerValue, string stringValue, bool isInteger)
		{
			return null;
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x000036A9 File Offset: 0x000018A9
		public override void AddChild(DecisionTreeNode newNode)
		{
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000B7210 File Offset: 0x000B5410
		public DecisionTreeEqualNode(DecisionTree tree, int identifier, bool useNot, int integerValue, string stringValue, bool isIntegerValue)
			: base(tree, useNot, true)
		{
			this.propertyIntegerValue = integerValue;
			this.propertyStringValue = stringValue;
			this.isInteger = isIntegerValue;
			this.propertyIdentifier = identifier;
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x000B723A File Offset: 0x000B543A
		public void ChangeValue(int identifier, int integerValue, string stringValue, bool isIntegerValue)
		{
			this.propertyIntegerValue = integerValue;
			this.propertyStringValue = stringValue;
			this.isInteger = isIntegerValue;
			this.propertyIdentifier = identifier;
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000B7259 File Offset: 0x000B5459
		public DecisionTreeEqualNode(DecisionTreeNode parentNode, int identifier, bool useNot, int integerValue, string stringValue, bool isIntegerValue)
			: base(parentNode.DecisionTree, useNot, true)
		{
			this.propertyIntegerValue = integerValue;
			this.propertyStringValue = stringValue;
			this.isInteger = isIntegerValue;
			this.propertyIdentifier = identifier;
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x000B7288 File Offset: 0x000B5488
		public override DecisionTreeNode InternalClone(DecisionTree decisionTree)
		{
			DecisionTreeEqualNode decisionTreeEqualNode = new DecisionTreeEqualNode(decisionTree, this.propertyIdentifier, this.notted, this.propertyIntegerValue, this.propertyStringValue, this.isInteger);
			decisionTree.AddPropertyEqualNode(decisionTreeEqualNode);
			return decisionTreeEqualNode;
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x000B72C4 File Offset: 0x000B54C4
		public override string GenerateXml()
		{
			ITracePointPropertyInformation tracePointPropertyInformation = this.traceContainer.PropertyInformationFromIdentifier(this.propertyIdentifier);
			string name = tracePointPropertyInformation.Name;
			string text = null;
			if (tracePointPropertyInformation.ValueType == PropertyType.String)
			{
				text = this.propertyStringValue;
			}
			if (tracePointPropertyInformation.ValueType == PropertyType.Integer)
			{
				text = this.propertyIntegerValue.ToString();
			}
			if (tracePointPropertyInformation.ValueType == PropertyType.Enumeration)
			{
				foreach (ITracePointPropertyEnumerationValue tracePointPropertyEnumerationValue in tracePointPropertyInformation.EnumerationValues)
				{
					if (tracePointPropertyEnumerationValue.Identifier == this.propertyIntegerValue)
					{
						text = tracePointPropertyEnumerationValue.Name;
						break;
					}
				}
			}
			if (this.notted)
			{
				return string.Format("<equal property=\"{0}\" value=\"{1}\" not=\"true\" />", name, text);
			}
			return string.Format("<equal property=\"{0}\" value=\"{1}\" />", name, text);
		}

		// Token: 0x04001F31 RID: 7985
		private int propertyIdentifier;

		// Token: 0x04001F32 RID: 7986
		protected int propertyIntegerValue;

		// Token: 0x04001F33 RID: 7987
		protected string propertyStringValue;

		// Token: 0x04001F34 RID: 7988
		private bool isInteger;

		// Token: 0x04001F35 RID: 7989
		private int integerValue;

		// Token: 0x04001F36 RID: 7990
		private string stringValue;
	}
}
