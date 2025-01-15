using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.HostIntegration.StrictResources.TracingGlobals;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000664 RID: 1636
	public class TraceTree : BaseTraceTreeElement, ICloneable
	{
		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06003676 RID: 13942 RVA: 0x000B76C0 File Offset: 0x000B58C0
		public bool PropertiesInTraceTree
		{
			get
			{
				return this.propertyToEqualNodes != null;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06003677 RID: 13943 RVA: 0x000B76CB File Offset: 0x000B58CB
		public TraceTreeNode RootNode
		{
			get
			{
				return this.rootNode;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06003678 RID: 13944 RVA: 0x000B76D3 File Offset: 0x000B58D3
		// (set) Token: 0x06003679 RID: 13945 RVA: 0x000B76DB File Offset: 0x000B58DB
		public bool LiveTracing
		{
			get
			{
				return this.liveTracing;
			}
			set
			{
				this.liveTracing = value;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x0600367A RID: 13946 RVA: 0x000B76E4 File Offset: 0x000B58E4
		// (set) Token: 0x0600367B RID: 13947 RVA: 0x000B76EC File Offset: 0x000B58EC
		public int MaximumDataBytesTraced
		{
			get
			{
				return this.maximumDataBytesTraced;
			}
			set
			{
				this.maximumDataBytesTraced = value;
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x0600367C RID: 13948 RVA: 0x000B76F5 File Offset: 0x000B58F5
		public bool HasSomeTracingSet
		{
			get
			{
				return this.rootNode.HasSomeTracingSet;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x000B7702 File Offset: 0x000B5902
		// (set) Token: 0x0600367E RID: 13950 RVA: 0x000B7714 File Offset: 0x000B5914
		public string InstanceName
		{
			get
			{
				return this.rootNode.Container.InstanceName;
			}
			set
			{
				this.rootNode.Container.InstanceName = value;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x000B7728 File Offset: 0x000B5928
		public string Name
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.rootNode.Container.SupportsInstances)
				{
					stringBuilder.AppendFormat("{0}/", this.rootNode.Container.DisplayName);
				}
				stringBuilder.Append(this.rootNode.Container.InstanceDisplayName);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000B7788 File Offset: 0x000B5988
		public TraceTree(ITraceContainer container, XmlNode xmlOfContainer)
		{
			this.isDefinitionTree = true;
			this.rootNode = new TraceTreeNode(container);
			this.maximumDataBytesTraced = 256;
			if (xmlOfContainer == null)
			{
				return;
			}
			XmlAttribute xmlAttribute = xmlOfContainer.Attributes["liveTracing"];
			if (xmlAttribute != null && string.Equals(xmlAttribute.Value, "true", StringComparison.InvariantCultureIgnoreCase))
			{
				this.liveTracing = true;
			}
			XmlAttribute xmlAttribute2 = xmlOfContainer.Attributes["maxBytesTraced"];
			int num = 256;
			if (xmlAttribute2 != null && !string.IsNullOrWhiteSpace(xmlAttribute2.Value))
			{
				if (!int.TryParse(xmlAttribute2.Value, out num))
				{
					throw new TraceException(SR.ContainerMaximumDataBytesAttribute);
				}
				if (num < 32)
				{
					num = 32;
				}
			}
			this.maximumDataBytesTraced = num;
			foreach (object obj in xmlOfContainer.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (string.Compare(xmlNode.Name, "tracePoint", StringComparison.InvariantCulture) != 0)
				{
					throw new TraceException(SR.TopLevelTracepoint(xmlNode.Name));
				}
				XmlAttribute xmlAttribute3 = xmlNode.Attributes["name"];
				if (xmlAttribute3 == null)
				{
					throw new TraceException(SR.TracepointNameAttribute);
				}
				TraceTreeNode tracePointNode = this.rootNode.GetTracePointNode(xmlAttribute3.Value);
				if (tracePointNode == null)
				{
					throw new TraceException(SR.UnknownTracepointInContainer(xmlAttribute3.Value, this.rootNode.Container.Name));
				}
				foreach (object obj2 in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					if (string.Compare(xmlNode2.Name, "traceLevel", StringComparison.InvariantCulture) != 0)
					{
						throw new TraceException(SR.TracepointTraceLevelChildren(xmlAttribute3.Value));
					}
					int num2 = TraceTree.ParseLevel(tracePointNode.UsesLevels, xmlNode2);
					tracePointNode.GetLevelNode(num2).AddDecisionTree(xmlNode2);
				}
			}
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000B79B4 File Offset: 0x000B5BB4
		public static int ParseLevel(bool usesLevels, XmlNode levelNode)
		{
			XmlAttribute xmlAttribute = levelNode.Attributes["level"];
			if (xmlAttribute == null)
			{
				throw new TraceException(SR.TraceLevelAttribute);
			}
			if (usesLevels)
			{
				return TraceTree.ParseLevelText(xmlAttribute.Value.Trim());
			}
			string[] array = xmlAttribute.Value.Split(new char[] { '|' });
			int num = 0;
			foreach (string text in array)
			{
				num += TraceTree.ParseFlagsText(text.Trim());
			}
			return num;
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x000B7A30 File Offset: 0x000B5C30
		public static int ParseLevelText(string text)
		{
			if (text != null)
			{
				uint num = <868055c8-a2e5-4039-b5eb-07da6295884c><PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1062369733U)
				{
					if (num <= 810547195U)
					{
						if (num != 711000888U)
						{
							if (num == 810547195U)
							{
								if (text == "None")
								{
									return 0;
								}
							}
						}
						else if (text == "Debug")
						{
							return 6;
						}
					}
					else if (num != 935656591U)
					{
						if (num == 1062369733U)
						{
							if (text == "Data")
							{
								return 7;
							}
						}
					}
					else if (text == "Warning")
					{
						return 3;
					}
				}
				else if (num <= 4086144241U)
				{
					if (num != 3408311065U)
					{
						if (num == 4086144241U)
						{
							if (text == "Error")
							{
								return 2;
							}
						}
					}
					else if (text == "Fatal")
					{
						return 1;
					}
				}
				else if (num != 4246103997U)
				{
					if (num == 4256323669U)
					{
						if (text == "Information")
						{
							return 4;
						}
					}
				}
				else if (text == "Verbose")
				{
					return 5;
				}
			}
			throw new TraceException(SR.TraceLevelString(text));
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000B7B50 File Offset: 0x000B5D50
		private static string GenerateLevelText(int level)
		{
			switch (level)
			{
			case 0:
				return "None";
			case 1:
				return "Fatal";
			case 2:
				return "Error";
			case 3:
				return "Warning";
			case 4:
				return "Information";
			case 5:
				return "Verbose";
			case 6:
				return "Debug";
			case 7:
				return "Data";
			default:
				return null;
			}
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x000B7BB8 File Offset: 0x000B5DB8
		public static int ParseFlagsText(string text)
		{
			if (text != null)
			{
				uint num = <868055c8-a2e5-4039-b5eb-07da6295884c><PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1062369733U)
				{
					if (num <= 810547195U)
					{
						if (num != 711000888U)
						{
							if (num == 810547195U)
							{
								if (text == "None")
								{
									return 0;
								}
							}
						}
						else if (text == "Debug")
						{
							return 32;
						}
					}
					else if (num != 935656591U)
					{
						if (num == 1062369733U)
						{
							if (text == "Data")
							{
								return 64;
							}
						}
					}
					else if (text == "Warning")
					{
						return 4;
					}
				}
				else if (num <= 3408311065U)
				{
					if (num != 1974461284U)
					{
						if (num == 3408311065U)
						{
							if (text == "Fatal")
							{
								return 1;
							}
						}
					}
					else if (text == "All")
					{
						return 127;
					}
				}
				else if (num != 4086144241U)
				{
					if (num != 4246103997U)
					{
						if (num == 4256323669U)
						{
							if (text == "Information")
							{
								return 8;
							}
						}
					}
					else if (text == "Verbose")
					{
						return 16;
					}
				}
				else if (text == "Error")
				{
					return 2;
				}
			}
			throw new TraceException(SR.TraceFlagsString(text));
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000B7CFC File Offset: 0x000B5EFC
		private static string GenerateFlagsText(int flags)
		{
			if (flags == 0)
			{
				return "None";
			}
			if (flags == 127)
			{
				return "All";
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			for (int i = 1; i < 65; i *= 2)
			{
				if ((flags & i) != 0)
				{
					string text = null;
					if (i <= 8)
					{
						switch (i)
						{
						case 1:
							text = "Fatal";
							break;
						case 2:
							text = "Error";
							break;
						case 3:
							break;
						case 4:
							text = "Warning";
							break;
						default:
							if (i == 8)
							{
								text = "Information";
							}
							break;
						}
					}
					else if (i != 16)
					{
						if (i != 32)
						{
							if (i == 64)
							{
								text = "Data";
							}
						}
						else
						{
							text = "Debug";
						}
					}
					else
					{
						text = "Verbose";
					}
					if (!flag)
					{
						stringBuilder.AppendFormat(" | {0}", text);
					}
					else
					{
						stringBuilder.Append(text);
					}
					flag = false;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x000B7DCD File Offset: 0x000B5FCD
		public static string LevelsOrFlagsToString(bool usesLevels, int levelsOrFlags)
		{
			if (usesLevels)
			{
				return TraceTree.GenerateLevelText(levelsOrFlags);
			}
			return TraceTree.GenerateFlagsText(levelsOrFlags);
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x000B7DDF File Offset: 0x000B5FDF
		public object Clone()
		{
			return new TraceTree(this.rootNode)
			{
				LiveTracing = this.liveTracing,
				MaximumDataBytesTraced = this.maximumDataBytesTraced
			};
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x000B7E04 File Offset: 0x000B6004
		public override string GenerateXml()
		{
			return this.rootNode.GenerateXml();
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x000B7E14 File Offset: 0x000B6014
		private TraceTree(TraceTreeNode rootNodeToClone)
		{
			this.isDefinitionTree = false;
			this.rootNode = rootNodeToClone.InternalClone(this, null);
			this.rootNode.FindEqualNodes();
			this.propertiesSet = new object[this.rootNode.Container.HighestPropertyIdentifier + 1];
			this.Evaluate();
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000B7E6C File Offset: 0x000B606C
		public void UpdateDefinitions(TraceTree newTree)
		{
			this.rootNode.UpdateDefinitions(newTree.RootNode);
			if (this.propertyToEqualNodes != null)
			{
				this.propertyToEqualNodes.Clear();
			}
			this.propertyToEqualNodes = null;
			this.rootNode.FindEqualNodes();
			if (this.propertyToEqualNodes != null)
			{
				foreach (KeyValuePair<int, TraceTree.DecisionTreeEqualNodeInformation> keyValuePair in this.propertyToEqualNodes)
				{
					foreach (DecisionTreeEqualNode decisionTreeEqualNode in keyValuePair.Value.DecisionTreeEqualnodes)
					{
						if (this.propertiesSet[keyValuePair.Key] != null)
						{
							decisionTreeEqualNode.SetValue(this.propertiesSet[keyValuePair.Key]);
						}
					}
				}
			}
			this.Evaluate();
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x000B7F64 File Offset: 0x000B6164
		public void Evaluate()
		{
			if (this.isDefinitionTree)
			{
				throw new TraceException("BUGBUG: Can't evaluate the definition tree");
			}
			this.rootNode.Evaluate(0);
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000B7F88 File Offset: 0x000B6188
		public void EvaluateDefinitionTree()
		{
			if (!this.isDefinitionTree)
			{
				throw new TraceException("BUGBUG: EvaluateDefinitionTree called on a non-definition tree");
			}
			this.tracePointIdentifierToNodes = new Dictionary<int, TraceTreeNode>();
			this.rootNode.FindTracePointIdentifiers(this);
			this.rootNode.FindEqualNodes();
			this.rootNode.Evaluate(0);
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x000B7FD8 File Offset: 0x000B61D8
		public void AddPropertyEqualNode(DecisionTreeEqualNode equalNode)
		{
			int propertyIdentifier = equalNode.PropertyIdentifier;
			if (this.propertyToEqualNodes == null)
			{
				this.propertyToEqualNodes = new Dictionary<int, TraceTree.DecisionTreeEqualNodeInformation>();
			}
			TraceTree.DecisionTreeEqualNodeInformation decisionTreeEqualNodeInformation;
			if (this.propertyToEqualNodes.ContainsKey(propertyIdentifier))
			{
				decisionTreeEqualNodeInformation = this.propertyToEqualNodes[propertyIdentifier];
			}
			else
			{
				decisionTreeEqualNodeInformation = new TraceTree.DecisionTreeEqualNodeInformation();
				this.propertyToEqualNodes[propertyIdentifier] = decisionTreeEqualNodeInformation;
			}
			decisionTreeEqualNodeInformation.DecisionTreeEqualnodes.Add(equalNode);
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x000B803C File Offset: 0x000B623C
		public void AddTracePointNode(TraceTreeNode traceTreeNode)
		{
			int tracePointIdentifier = traceTreeNode.TracePointIdentifier;
			if (this.tracePointIdentifierToNodes == null)
			{
				this.tracePointIdentifierToNodes = new Dictionary<int, TraceTreeNode>();
			}
			this.tracePointIdentifierToNodes[tracePointIdentifier] = traceTreeNode;
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000B8070 File Offset: 0x000B6270
		public TraceTreeNode GetTracePointNode(int tracePointIdentifier)
		{
			return this.tracePointIdentifierToNodes[tracePointIdentifier];
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000B8080 File Offset: 0x000B6280
		public void SetPropertyValue(bool tracePointAllowsPropertyUpdates, int propertyIdentifier, object value)
		{
			if (this.propertiesSet != null)
			{
				this.propertiesSet[propertyIdentifier] = value;
			}
			if (this.propertyToEqualNodes == null)
			{
				return;
			}
			if (!this.propertyToEqualNodes.ContainsKey(propertyIdentifier))
			{
				return;
			}
			TraceTree.DecisionTreeEqualNodeInformation decisionTreeEqualNodeInformation = this.propertyToEqualNodes[propertyIdentifier];
			decisionTreeEqualNodeInformation.HasBeenSet = true;
			foreach (DecisionTreeEqualNode decisionTreeEqualNode in decisionTreeEqualNodeInformation.DecisionTreeEqualnodes)
			{
				decisionTreeEqualNode.SetValue(value);
			}
			this.Evaluate();
		}

		// Token: 0x04001F69 RID: 8041
		private TraceTreeNode rootNode;

		// Token: 0x04001F6A RID: 8042
		private bool isDefinitionTree;

		// Token: 0x04001F6B RID: 8043
		private Dictionary<int, TraceTree.DecisionTreeEqualNodeInformation> propertyToEqualNodes;

		// Token: 0x04001F6C RID: 8044
		private Dictionary<int, TraceTreeNode> tracePointIdentifierToNodes;

		// Token: 0x04001F6D RID: 8045
		private object[] propertiesSet;

		// Token: 0x04001F6E RID: 8046
		private bool liveTracing;

		// Token: 0x04001F6F RID: 8047
		private int maximumDataBytesTraced;

		// Token: 0x02000665 RID: 1637
		public class DecisionTreeEqualNodeInformation
		{
			// Token: 0x17000BEE RID: 3054
			// (get) Token: 0x06003691 RID: 13969 RVA: 0x000B8114 File Offset: 0x000B6314
			// (set) Token: 0x06003692 RID: 13970 RVA: 0x000B811C File Offset: 0x000B631C
			public bool HasBeenSet
			{
				get
				{
					return this.hasBeenSet;
				}
				set
				{
					this.hasBeenSet = value;
				}
			}

			// Token: 0x17000BEF RID: 3055
			// (get) Token: 0x06003693 RID: 13971 RVA: 0x000B8125 File Offset: 0x000B6325
			// (set) Token: 0x06003694 RID: 13972 RVA: 0x000B812D File Offset: 0x000B632D
			public List<DecisionTreeEqualNode> DecisionTreeEqualnodes
			{
				get
				{
					return this.decisionTreeEqualNodes;
				}
				set
				{
					this.decisionTreeEqualNodes = value;
				}
			}

			// Token: 0x06003695 RID: 13973 RVA: 0x000B8136 File Offset: 0x000B6336
			public DecisionTreeEqualNodeInformation()
			{
				this.hasBeenSet = false;
				this.decisionTreeEqualNodes = new List<DecisionTreeEqualNode>();
			}

			// Token: 0x04001F70 RID: 8048
			private bool hasBeenSet;

			// Token: 0x04001F71 RID: 8049
			private List<DecisionTreeEqualNode> decisionTreeEqualNodes;
		}
	}
}
