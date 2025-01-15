using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200061D RID: 1565
	[Serializable]
	public sealed class ParameterInfo : ParameterBase, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060055F7 RID: 22007 RVA: 0x0016A082 File Offset: 0x00168282
		public ParameterInfo()
		{
		}

		// Token: 0x060055F8 RID: 22008 RVA: 0x0016A098 File Offset: 0x00168298
		internal ParameterInfo(ParameterInfo source)
			: base(source)
		{
			this.m_isUserSupplied = source.m_isUserSupplied;
			this.m_valuesChanged = source.m_valuesChanged;
			this.m_dynamicValidValues = source.m_dynamicValidValues;
			this.m_dynamicDefaultValue = source.m_dynamicDefaultValue;
			this.m_state = source.State;
			this.m_othersDependOnMe = source.m_othersDependOnMe;
			this.m_useExplicitDefaultValue = source.m_useExplicitDefaultValue;
			this.m_prompt = source.m_prompt;
			this.m_dynamicPrompt = source.m_dynamicPrompt;
			if (source.m_values != null)
			{
				int num = source.m_values.Length;
				this.m_values = new object[num];
				for (int i = 0; i < num; i++)
				{
					this.m_values[i] = source.m_values[i];
				}
			}
			if (source.m_labels != null)
			{
				int num2 = source.m_labels.Length;
				this.m_labels = new string[num2];
				for (int j = 0; j < num2; j++)
				{
					this.m_labels[j] = source.m_labels[j];
				}
			}
			if (source.m_dependencyList != null)
			{
				int count = source.m_dependencyList.Count;
				this.m_dependencyList = new ParameterInfoCollection(count);
				for (int k = 0; k < count; k++)
				{
					this.m_dependencyList.Add(source.m_dependencyList[k]);
				}
			}
			if (source.m_validValues != null)
			{
				int count2 = source.m_validValues.Count;
				this.m_validValues = new ValidValueList(count2);
				for (int l = 0; l < count2; l++)
				{
					this.m_validValues.Add(source.m_validValues[l]);
				}
			}
		}

		// Token: 0x060055F9 RID: 22009 RVA: 0x0016A22F File Offset: 0x0016842F
		internal ParameterInfo(ParameterBase source)
			: base(source)
		{
			this.m_prompt = source.Prompt;
		}

		// Token: 0x060055FA RID: 22010 RVA: 0x0016A252 File Offset: 0x00168452
		internal ParameterInfo(DataSetParameterValue source, bool usedInQuery)
			: base(source, usedInQuery)
		{
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x0016A26A File Offset: 0x0016846A
		internal ParameterInfo(ParameterValue source)
			: base(source)
		{
			this.m_isUserSupplied = true;
		}

		// Token: 0x060055FC RID: 22012 RVA: 0x0016A288 File Offset: 0x00168488
		internal new static Microsoft.ReportingServices.ReportProcessing.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportProcessing.Persistence.Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ParameterBase, new MemberInfoList
			{
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.IsUserSupplied, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Boolean),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.Value, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Array, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.Variant),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.DynamicValidValues, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Boolean),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.DynamicDefaultValue, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Boolean),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.DependencyList, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ParameterInfoCollection),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.ValidValues, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ValidValueList),
				new Microsoft.ReportingServices.ReportProcessing.Persistence.MemberInfo(Microsoft.ReportingServices.ReportProcessing.Persistence.MemberName.Label, Microsoft.ReportingServices.ReportProcessing.Persistence.Token.Array, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.String)
			});
		}

		// Token: 0x060055FD RID: 22013 RVA: 0x0016A338 File Offset: 0x00168538
		[SkipMemberStaticValidation(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DependencyList)]
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetNewDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterBase, new List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo>
			{
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.IsUserSupplied, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Boolean),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Object),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DynamicValidValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Boolean),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DynamicDefaultValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Boolean),
				new ReadOnlyMemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DependencyList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterInfo),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.ValidValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ValidValue),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Label, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.String),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Prompt, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.String),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DynamicPrompt, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Boolean),
				new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DependencyIndexList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Token.Int32)
			});
		}

		// Token: 0x060055FE RID: 22014 RVA: 0x0016A42C File Offset: 0x0016862C
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ParameterInfo.m_Declaration);
			while (writer.NextMember())
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.IsUserSupplied)
				{
					if (memberName <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Label)
					{
						if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Value)
						{
							writer.Write(this.m_values);
							continue;
						}
						if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Label)
						{
							writer.Write(this.m_labels);
							continue;
						}
					}
					else
					{
						if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Prompt)
						{
							writer.Write(this.m_prompt);
							continue;
						}
						if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.IsUserSupplied)
						{
							writer.Write(this.m_isUserSupplied);
							continue;
						}
					}
				}
				else if (memberName <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DynamicValidValues)
				{
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.ValidValues)
					{
						writer.Write(this.m_validValues);
						continue;
					}
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DynamicValidValues)
					{
						writer.Write(this.m_dynamicValidValues);
						continue;
					}
				}
				else
				{
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DynamicDefaultValue)
					{
						writer.Write(this.m_dynamicDefaultValue);
						continue;
					}
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DynamicPrompt)
					{
						writer.Write(this.m_dynamicPrompt);
						continue;
					}
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DependencyIndexList)
					{
						this.m_dependencyIndexList = null;
						if (this.m_dependencyList != null)
						{
							this.m_dependencyIndexList = new int[this.m_dependencyList.Count];
							for (int i = 0; i < this.m_dependencyList.Count; i++)
							{
								this.m_dependencyIndexList[i] = this.m_dependencyList[i].IndexInCollection;
							}
						}
						writer.Write(this.m_dependencyIndexList);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060055FF RID: 22015 RVA: 0x0016A5E0 File Offset: 0x001687E0
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ParameterInfo.m_Declaration);
			while (reader.NextMember())
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.IsUserSupplied)
				{
					if (memberName <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Label)
					{
						if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Value)
						{
							this.m_values = reader.ReadVariantArray();
							continue;
						}
						if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Label)
						{
							this.m_labels = reader.ReadStringArray();
							continue;
						}
					}
					else
					{
						if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Prompt)
						{
							this.m_prompt = reader.ReadString();
							continue;
						}
						if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.IsUserSupplied)
						{
							this.m_isUserSupplied = reader.ReadBoolean();
							continue;
						}
					}
				}
				else if (memberName <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DependencyList)
				{
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.ValidValues)
					{
						this.m_validValues = reader.ReadListOfRIFObjects<ValidValueList>();
						continue;
					}
					switch (memberName)
					{
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DynamicValidValues:
						this.m_dynamicValidValues = reader.ReadBoolean();
						continue;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DynamicDefaultValue:
						this.m_dynamicDefaultValue = reader.ReadBoolean();
						continue;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DependencyList:
						this.m_dependencyList = reader.ReadListOfRIFObjects<ParameterInfoCollection>();
						continue;
					}
				}
				else
				{
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DynamicPrompt)
					{
						this.m_dynamicPrompt = reader.ReadBoolean();
						continue;
					}
					if (memberName == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.DependencyIndexList)
					{
						this.m_dependencyIndexList = reader.ReadInt32Array();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06005600 RID: 22016 RVA: 0x0016A74A File Offset: 0x0016894A
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x0016A757 File Offset: 0x00168957
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterInfo;
		}

		// Token: 0x06005602 RID: 22018 RVA: 0x0016A760 File Offset: 0x00168960
		internal void ResolveDependencies(ParameterInfoCollection containingCollection)
		{
			if (this.m_dependencyIndexList != null)
			{
				this.m_dependencyList = new ParameterInfoCollection(this.m_dependencyIndexList.Length);
				for (int i = 0; i < this.m_dependencyIndexList.Length; i++)
				{
					this.m_dependencyList.Add(containingCollection[this.m_dependencyIndexList[i]]);
				}
			}
			this.m_dependencyIndexList = null;
		}

		// Token: 0x17001F77 RID: 8055
		// (get) Token: 0x06005603 RID: 22019 RVA: 0x0016A7BB File Offset: 0x001689BB
		// (set) Token: 0x06005604 RID: 22020 RVA: 0x0016A7C3 File Offset: 0x001689C3
		public object[] Values
		{
			get
			{
				return this.m_values;
			}
			set
			{
				this.m_values = value;
			}
		}

		// Token: 0x17001F78 RID: 8056
		// (get) Token: 0x06005605 RID: 22021 RVA: 0x0016A7CC File Offset: 0x001689CC
		// (set) Token: 0x06005606 RID: 22022 RVA: 0x0016A7D4 File Offset: 0x001689D4
		public string[] Labels
		{
			get
			{
				return this.m_labels;
			}
			set
			{
				this.m_labels = value;
			}
		}

		// Token: 0x17001F79 RID: 8057
		// (get) Token: 0x06005607 RID: 22023 RVA: 0x0016A7DD File Offset: 0x001689DD
		// (set) Token: 0x06005608 RID: 22024 RVA: 0x0016A7E5 File Offset: 0x001689E5
		public ValidValueList ValidValues
		{
			get
			{
				return this.m_validValues;
			}
			set
			{
				this.m_validValues = value;
			}
		}

		// Token: 0x17001F7A RID: 8058
		// (get) Token: 0x06005609 RID: 22025 RVA: 0x0016A7EE File Offset: 0x001689EE
		// (set) Token: 0x0600560A RID: 22026 RVA: 0x0016A7F6 File Offset: 0x001689F6
		public bool DynamicValidValues
		{
			get
			{
				return this.m_dynamicValidValues;
			}
			set
			{
				this.m_dynamicValidValues = value;
			}
		}

		// Token: 0x17001F7B RID: 8059
		// (get) Token: 0x0600560B RID: 22027 RVA: 0x0016A7FF File Offset: 0x001689FF
		// (set) Token: 0x0600560C RID: 22028 RVA: 0x0016A807 File Offset: 0x00168A07
		public bool DynamicDefaultValue
		{
			get
			{
				return this.m_dynamicDefaultValue;
			}
			set
			{
				this.m_dynamicDefaultValue = value;
			}
		}

		// Token: 0x17001F7C RID: 8060
		// (get) Token: 0x0600560D RID: 22029 RVA: 0x0016A810 File Offset: 0x00168A10
		// (set) Token: 0x0600560E RID: 22030 RVA: 0x0016A818 File Offset: 0x00168A18
		public bool UseExplicitDefaultValue
		{
			get
			{
				return this.m_useExplicitDefaultValue;
			}
			set
			{
				this.m_useExplicitDefaultValue = value;
			}
		}

		// Token: 0x17001F7D RID: 8061
		// (get) Token: 0x0600560F RID: 22031 RVA: 0x0016A821 File Offset: 0x00168A21
		// (set) Token: 0x06005610 RID: 22032 RVA: 0x0016A829 File Offset: 0x00168A29
		public ParameterInfoCollection DependencyList
		{
			get
			{
				return this.m_dependencyList;
			}
			set
			{
				this.m_dependencyList = value;
			}
		}

		// Token: 0x17001F7E RID: 8062
		// (get) Token: 0x06005611 RID: 22033 RVA: 0x0016A832 File Offset: 0x00168A32
		// (set) Token: 0x06005612 RID: 22034 RVA: 0x0016A83A File Offset: 0x00168A3A
		internal bool IsUserSupplied
		{
			get
			{
				return this.m_isUserSupplied;
			}
			set
			{
				this.m_isUserSupplied = value;
			}
		}

		// Token: 0x17001F7F RID: 8063
		// (get) Token: 0x06005613 RID: 22035 RVA: 0x0016A843 File Offset: 0x00168A43
		// (set) Token: 0x06005614 RID: 22036 RVA: 0x0016A84B File Offset: 0x00168A4B
		internal bool ValuesChanged
		{
			get
			{
				return this.m_valuesChanged;
			}
			set
			{
				this.m_valuesChanged = value;
			}
		}

		// Token: 0x17001F80 RID: 8064
		// (get) Token: 0x06005615 RID: 22037 RVA: 0x0016A854 File Offset: 0x00168A54
		// (set) Token: 0x06005616 RID: 22038 RVA: 0x0016A85C File Offset: 0x00168A5C
		public override string Prompt
		{
			get
			{
				return this.m_prompt;
			}
			set
			{
				this.m_prompt = value;
			}
		}

		// Token: 0x17001F81 RID: 8065
		// (get) Token: 0x06005617 RID: 22039 RVA: 0x0016A865 File Offset: 0x00168A65
		// (set) Token: 0x06005618 RID: 22040 RVA: 0x0016A86D File Offset: 0x00168A6D
		public bool DynamicPrompt
		{
			get
			{
				return this.m_dynamicPrompt;
			}
			set
			{
				this.m_dynamicPrompt = value;
			}
		}

		// Token: 0x17001F82 RID: 8066
		// (get) Token: 0x06005619 RID: 22041 RVA: 0x0016A876 File Offset: 0x00168A76
		// (set) Token: 0x0600561A RID: 22042 RVA: 0x0016A87E File Offset: 0x00168A7E
		public ReportParameterState State
		{
			get
			{
				return this.m_state;
			}
			set
			{
				this.m_state = value;
			}
		}

		// Token: 0x17001F83 RID: 8067
		// (get) Token: 0x0600561B RID: 22043 RVA: 0x0016A887 File Offset: 0x00168A87
		// (set) Token: 0x0600561C RID: 22044 RVA: 0x0016A88F File Offset: 0x00168A8F
		public bool OthersDependOnMe
		{
			get
			{
				return this.m_othersDependOnMe;
			}
			set
			{
				this.m_othersDependOnMe = value;
			}
		}

		// Token: 0x17001F84 RID: 8068
		// (get) Token: 0x0600561D RID: 22045 RVA: 0x0016A898 File Offset: 0x00168A98
		public bool IsVisible
		{
			get
			{
				return base.PromptUser && this.Prompt != null && this.Prompt.Length > 0;
			}
		}

		// Token: 0x17001F85 RID: 8069
		// (get) Token: 0x0600561E RID: 22046 RVA: 0x0016A8BA File Offset: 0x00168ABA
		// (set) Token: 0x0600561F RID: 22047 RVA: 0x0016A8C2 File Offset: 0x00168AC2
		internal int IndexInCollection
		{
			get
			{
				return this.m_indexInCollection;
			}
			set
			{
				this.m_indexInCollection = value;
			}
		}

		// Token: 0x17001F86 RID: 8070
		// (get) Token: 0x06005620 RID: 22048 RVA: 0x0016A8CB File Offset: 0x00168ACB
		// (set) Token: 0x06005621 RID: 22049 RVA: 0x0016A8D3 File Offset: 0x00168AD3
		internal bool MissingUpstreamDataSourcePrompt
		{
			get
			{
				return this.m_missingUpstreamDataSourcePrompt;
			}
			set
			{
				this.m_missingUpstreamDataSourcePrompt = value;
			}
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x0016A8DC File Offset: 0x00168ADC
		public void SetValuesFromQueryParameter(object value)
		{
			this.m_values = value as object[];
			if (this.m_values == null)
			{
				this.m_values = new object[1];
				this.m_values[0] = value;
			}
		}

		// Token: 0x06005623 RID: 22051 RVA: 0x0016A907 File Offset: 0x00168B07
		public bool AllDependenciesSpecified()
		{
			return this.CalculateDependencyStatus() == ReportParameterDependencyState.AllDependenciesSpecified;
		}

		// Token: 0x06005624 RID: 22052 RVA: 0x0016A914 File Offset: 0x00168B14
		internal ReportParameterDependencyState CalculateDependencyStatus()
		{
			ReportParameterDependencyState reportParameterDependencyState = ReportParameterDependencyState.AllDependenciesSpecified;
			if (this.DependencyList != null)
			{
				for (int i = 0; i < this.DependencyList.Count; i++)
				{
					ParameterInfo parameterInfo = this.DependencyList[i];
					if (parameterInfo.MissingUpstreamDataSourcePrompt)
					{
						reportParameterDependencyState = ReportParameterDependencyState.MissingUpstreamDataSourcePrompt;
						break;
					}
					if (parameterInfo.State != ReportParameterState.HasValidValue)
					{
						reportParameterDependencyState = ReportParameterDependencyState.HasOutstandingDependencies;
					}
				}
			}
			return reportParameterDependencyState;
		}

		// Token: 0x06005625 RID: 22053 RVA: 0x0016A968 File Offset: 0x00168B68
		public bool ValueIsValid()
		{
			if (this.Values == null || this.Values.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < this.Values.Length; i++)
			{
				object obj = this.Values[i];
				if (!base.Nullable && obj == null)
				{
					if (Global.Tracer.TraceVerbose)
					{
						Global.Tracer.Trace(TraceLevel.Verbose, "Value provided for parameter '{0}' is null and parameter is not nullable.", new object[] { base.Name });
					}
					return false;
				}
				if (base.DataType == DataType.String && !base.AllowBlank && obj != null && ((string)obj).Length == 0)
				{
					if (Global.Tracer.TraceVerbose)
					{
						Global.Tracer.Trace(TraceLevel.Verbose, "Value provided for string parameter '{0}' is either null or blank and parameter does not allow blanks.", new object[] { base.Name });
					}
					return false;
				}
				if (this.ValidValues != null)
				{
					bool flag = false;
					for (int j = 0; j < this.ValidValues.Count; j++)
					{
						if (ParameterBase.ParameterValuesEqual(obj, this.ValidValues[j].Value))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						if (Global.Tracer.TraceVerbose)
						{
							Global.Tracer.Trace(TraceLevel.Verbose, "The provided value '{0}' for parameter '{1}' is not a valid value.", new object[]
							{
								(obj == null) ? "null" : obj.ToString(),
								base.Name
							});
						}
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06005626 RID: 22054 RVA: 0x0016AAB4 File Offset: 0x00168CB4
		internal void StoreLabels()
		{
			this.EnsureLabelsAreGenerated();
			if (this.Values == null)
			{
				return;
			}
			this.m_labels = new string[this.Values.Length];
			for (int i = 0; i < this.Values.Length; i++)
			{
				string text = null;
				object obj = this.Values[i];
				bool flag = false;
				if (this.ValidValues != null)
				{
					for (int j = 0; j < this.ValidValues.Count; j++)
					{
						if (ParameterBase.ParameterValuesEqual(obj, this.ValidValues[j].Value))
						{
							flag = true;
							text = this.ValidValues[j].Label;
							break;
						}
					}
				}
				if (!flag && obj != null)
				{
					text = ParameterInfo.CastValueToLabelString(obj, Thread.CurrentThread.CurrentCulture);
				}
				this.m_labels[i] = text;
			}
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x0016AB7C File Offset: 0x00168D7C
		internal void EnsureLabelsAreGenerated()
		{
			if (this.ValidValues != null)
			{
				for (int i = 0; i < this.ValidValues.Count; i++)
				{
					this.ValidValues[i].EnsureLabelIsGenerated();
				}
			}
		}

		// Token: 0x06005628 RID: 22056 RVA: 0x0016ABB8 File Offset: 0x00168DB8
		internal void AddValidValue(object paramValue, string paramLabel)
		{
			if (paramLabel == null)
			{
				paramLabel = ParameterInfo.CastValueToLabelString(paramValue, Thread.CurrentThread.CurrentCulture);
			}
			this.AddValidValueExplicit(paramValue, paramLabel);
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x0016ABD8 File Offset: 0x00168DD8
		internal void AddValidValue(string paramValue, string paramLabel, ErrorContext errorContext, CultureInfo language)
		{
			object obj;
			if (ParameterBase.CastFromString(paramValue, out obj, base.DataType, language))
			{
				this.AddValidValueExplicit(obj, paramLabel);
				return;
			}
			if (errorContext != null)
			{
				errorContext.Register(ProcessingErrorCode.rsParameterPropertyTypeMismatch, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter, base.Name, "ValidValue", Array.Empty<string>());
				return;
			}
			throw new ReportParameterTypeMismatchException(base.Name);
		}

		// Token: 0x0600562A RID: 22058 RVA: 0x0016AC2E File Offset: 0x00168E2E
		internal void AddValidValueExplicit(object paramValue, string paramLabel)
		{
			if (this.ValidValues == null)
			{
				this.ValidValues = new ValidValueList();
			}
			this.ValidValues.Add(new ValidValue(paramValue, paramLabel));
		}

		// Token: 0x0600562B RID: 22059 RVA: 0x0016AC58 File Offset: 0x00168E58
		internal void Parse(string name, List<string> defaultValues, string type, string nullable, string prompt, bool promptIsExpr, string promptUser, string allowBlank, string multiValue, ValidValueList validValues, string usedInQuery, bool hidden, ErrorContext errorContext, CultureInfo language, string useAllValidValues)
		{
			base.Parse(name, defaultValues, type, nullable, prompt, promptUser, allowBlank, multiValue, usedInQuery, hidden, errorContext, language, useAllValidValues);
			if (hidden)
			{
				this.m_prompt = "";
			}
			else if (prompt == null)
			{
				this.m_prompt = name + ":";
			}
			else
			{
				this.m_prompt = prompt;
			}
			this.DynamicPrompt = promptIsExpr;
			if (validValues != null)
			{
				int count = validValues.Count;
				for (int i = 0; i < count; i++)
				{
					object obj;
					if (!ParameterBase.CastFromString(validValues[i].StringValue, out obj, base.DataType, language))
					{
						if (errorContext == null)
						{
							throw new ReportParameterTypeMismatchException(name);
						}
						errorContext.Register(ProcessingErrorCode.rsParameterPropertyTypeMismatch, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter, name, "ValidValue", Array.Empty<string>());
					}
					else
					{
						validValues[i].Value = obj;
						base.ValidateValue(obj, errorContext, base.ParameterObjectType, "ValidValue");
					}
				}
				this.m_validValues = validValues;
			}
		}

		// Token: 0x0600562C RID: 22060 RVA: 0x0016AD48 File Offset: 0x00168F48
		public void Parse(string name, string type, string nullable, string allowBlank, string multiValue, string usedInQuery, string state, string dynamicPrompt, string prompt, string promptUser, ParameterInfoCollection dependencies, string dynamicValidValues, ValidValueList validValues, string dynamicDefaultValue, List<string> defaultValues, List<string> values, string[] labels, CultureInfo language, string useAllValidValues)
		{
			bool flag = prompt != null && prompt.Length == 0;
			bool flag2 = false;
			if (dynamicPrompt != null)
			{
				flag2 = bool.Parse(dynamicPrompt);
			}
			this.Parse(name, defaultValues, type, nullable, prompt, flag2, promptUser, allowBlank, multiValue, validValues, usedInQuery, flag, null, language, useAllValidValues);
			if (state != null)
			{
				this.State = (ReportParameterState)Enum.Parse(typeof(ReportParameterState), state);
			}
			this.DependencyList = dependencies;
			if (dynamicValidValues != null)
			{
				this.DynamicValidValues = bool.Parse(dynamicValidValues);
			}
			if (dynamicDefaultValue != null)
			{
				this.DynamicDefaultValue = bool.Parse(dynamicDefaultValue);
			}
			if (values != null)
			{
				this.Values = new object[values.Count];
				for (int i = 0; i < values.Count; i++)
				{
					if (!ParameterBase.CastFromString(values[i], out this.Values[i], base.DataType, language))
					{
						throw new ReportParameterTypeMismatchException(name);
					}
				}
			}
			this.Labels = labels;
		}

		// Token: 0x0600562D RID: 22061 RVA: 0x0016AE38 File Offset: 0x00169038
		internal void WriteToXml(XmlTextWriter xml, bool writeTransientState)
		{
			xml.WriteStartElement("Parameter");
			xml.WriteElementString("Name", base.Name);
			xml.WriteElementString("Type", base.DataType.ToString());
			xml.WriteElementString("Nullable", base.Nullable.ToString(CultureInfo.InvariantCulture));
			xml.WriteElementString("AllowBlank", base.AllowBlank.ToString(CultureInfo.InvariantCulture));
			xml.WriteElementString("MultiValue", base.MultiValue.ToString(CultureInfo.InvariantCulture));
			xml.WriteElementString("UsedInQuery", base.UsedInQuery.ToString(CultureInfo.InvariantCulture));
			xml.WriteElementString("State", this.State.ToString());
			if (this.Prompt != null)
			{
				xml.WriteElementString("Prompt", this.Prompt);
			}
			if (this.Prompt != null)
			{
				xml.WriteElementString("DynamicPrompt", this.DynamicPrompt.ToString(CultureInfo.InvariantCulture));
			}
			xml.WriteElementString("PromptUser", base.PromptUser.ToString(CultureInfo.InvariantCulture));
			if (this.DependencyList != null)
			{
				xml.WriteStartElement("Dependencies");
				for (int i = 0; i < this.DependencyList.Count; i++)
				{
					if (this.DependencyList[i] != null)
					{
						xml.WriteElementString("Dependency", this.DependencyList[i].Name);
					}
				}
				xml.WriteEndElement();
			}
			if (this.DynamicValidValues)
			{
				xml.WriteElementString("DynamicValidValues", this.DynamicValidValues.ToString(CultureInfo.InvariantCulture));
			}
			if (this.ValidValues != null)
			{
				xml.WriteStartElement("ValidValues");
				for (int j = 0; j < this.ValidValues.Count; j++)
				{
					xml.WriteStartElement("ValidValue");
					if (this.ValidValues[j] != null)
					{
						if (this.ValidValues[j].Value != null)
						{
							this.WriteValueToXml(xml, base.DataType, this.ValidValues[j].Value);
						}
						if (this.ValidValues[j].LabelRaw != null)
						{
							xml.WriteElementString("Label", this.ValidValues[j].LabelRaw);
						}
					}
					xml.WriteEndElement();
				}
				xml.WriteEndElement();
			}
			if (this.DynamicDefaultValue)
			{
				xml.WriteElementString("DynamicDefaultValue", this.DynamicDefaultValue.ToString(CultureInfo.InvariantCulture));
			}
			if (base.DefaultValues != null)
			{
				xml.WriteStartElement("DefaultValues");
				for (int k = 0; k < base.DefaultValues.Length; k++)
				{
					this.WriteValueToXml(xml, base.DataType, base.DefaultValues[k]);
				}
				xml.WriteEndElement();
			}
			if (this.Values != null)
			{
				xml.WriteStartElement("Values");
				for (int l = 0; l < this.Values.Length; l++)
				{
					this.WriteValueToXml(xml, base.DataType, this.Values[l]);
				}
				xml.WriteEndElement();
			}
			if (writeTransientState)
			{
				xml.WriteElementString("IsUserSupplied", this.IsUserSupplied.ToString(CultureInfo.InvariantCulture));
				xml.WriteElementString("ValuesChanged", this.ValuesChanged.ToString(CultureInfo.InvariantCulture));
				xml.WriteElementString("UseExplicitDefaultValue", this.UseExplicitDefaultValue.ToString(CultureInfo.InvariantCulture));
			}
			xml.WriteEndElement();
		}

		// Token: 0x0600562E RID: 22062 RVA: 0x0016B1C8 File Offset: 0x001693C8
		internal static string EncodeObjectAsBase64String(object originalValue, bool convertValueToString)
		{
			if (originalValue == null)
			{
				return null;
			}
			string text;
			try
			{
				if (convertValueToString)
				{
					if (originalValue is bool)
					{
						originalValue = ((bool)originalValue).ToString(CultureInfo.InvariantCulture);
					}
					else if (originalValue is sbyte)
					{
						originalValue = ((sbyte)originalValue).ToString(CultureInfo.InvariantCulture);
					}
					else if (originalValue is short)
					{
						originalValue = ((short)originalValue).ToString(CultureInfo.InvariantCulture);
					}
					else if (originalValue is int)
					{
						originalValue = ((int)originalValue).ToString(CultureInfo.InvariantCulture);
					}
					else if (originalValue is long)
					{
						originalValue = ((long)originalValue).ToString(CultureInfo.InvariantCulture);
					}
					else if (originalValue is byte)
					{
						originalValue = ((byte)originalValue).ToString(CultureInfo.InvariantCulture);
					}
					else if (originalValue is ushort)
					{
						originalValue = ((ushort)originalValue).ToString(CultureInfo.InvariantCulture);
					}
					else if (originalValue is uint)
					{
						originalValue = ((uint)originalValue).ToString(CultureInfo.InvariantCulture);
					}
					else if (originalValue is ulong)
					{
						originalValue = ((ulong)originalValue).ToString(CultureInfo.InvariantCulture);
					}
					else if (originalValue is float)
					{
						originalValue = ((float)originalValue).ToString("r", CultureInfo.InvariantCulture);
					}
					else if (originalValue is double)
					{
						originalValue = ((double)originalValue).ToString("r", CultureInfo.InvariantCulture);
					}
					else if (originalValue is decimal)
					{
						originalValue = ((decimal)originalValue).ToString("f", CultureInfo.InvariantCulture);
					}
					else if (originalValue is DateTime)
					{
						originalValue = ((DateTime)originalValue).ToString("s", CultureInfo.InvariantCulture);
					}
				}
				using (MemoryStream memoryStream = new MemoryStream())
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter intermediateFormatWriter = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter(memoryStream, 0);
					intermediateFormatWriter.Write(new RIFVariantContainer(originalValue));
					memoryStream.Flush();
					byte[] array = memoryStream.ToArray();
					memoryStream.Close();
					text = Convert.ToBase64String(array);
				}
			}
			catch (Exception ex)
			{
				throw new InternalCatalogException(ex, string.Concat(new string[]
				{
					"Parameter value encoding failed for type='",
					originalValue.GetType().ToString(),
					"', value='",
					originalValue.ToString(),
					"'"
				}));
			}
			return text;
		}

		// Token: 0x0600562F RID: 22063 RVA: 0x0016B468 File Offset: 0x00169668
		private void WriteValueToXml(XmlTextWriter xml, DataType parameterType, object val)
		{
			this.WriteValueToXml(xml, parameterType, val, false);
		}

		// Token: 0x06005630 RID: 22064 RVA: 0x0016B474 File Offset: 0x00169674
		private void WriteValueToXml(XmlTextWriter xml, DataType parameterType, object val, bool convertValueToString)
		{
			if (parameterType == DataType.Object)
			{
				this.WriteValueToXml(xml, ParameterInfo.EncodeObjectAsBase64String(val, convertValueToString));
				return;
			}
			this.WriteValueToXml(xml, val);
		}

		// Token: 0x06005631 RID: 22065 RVA: 0x0016B494 File Offset: 0x00169694
		private void WriteValueToXml(XmlTextWriter xml, object val)
		{
			xml.WriteStartElement("Value");
			if (val == null)
			{
				xml.WriteAttributeString("nil", bool.TrueString);
			}
			else
			{
				string text = val as string;
				if (text == null)
				{
					xml.WriteString(this.CastToString(val, CultureInfo.InvariantCulture));
				}
				else
				{
					xml.WriteString(text);
				}
			}
			xml.WriteEndElement();
		}

		// Token: 0x06005632 RID: 22066 RVA: 0x0016B4EC File Offset: 0x001696EC
		internal void WriteNameValueToXml(XmlTextWriter xml, bool convertToString)
		{
			xml.WriteStartElement("Parameter");
			xml.WriteElementString("Name", base.Name);
			if (this.Values != null)
			{
				xml.WriteStartElement("Values");
				for (int i = 0; i < this.Values.Length; i++)
				{
					this.WriteValueToXml(xml, base.DataType, this.Values[i], convertToString);
				}
				xml.WriteEndElement();
			}
			xml.WriteEndElement();
		}

		// Token: 0x06005633 RID: 22067 RVA: 0x0016B560 File Offset: 0x00169760
		internal string[] ValuesToStringArray(CultureInfo cultureInfo)
		{
			string[] array = new string[this.Values.Length];
			for (int i = 0; i < this.Values.Length; i++)
			{
				string text = this.CastToString(this.Values[i], cultureInfo);
				array[i] = text;
			}
			return array;
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x0016B5A4 File Offset: 0x001697A4
		internal static ParameterInfo Cast(ParameterInfo oldValue, ParameterInfo newType, CultureInfo language)
		{
			bool flag = false;
			return ParameterInfo.Cast(oldValue, newType, language, ref flag);
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x0016B5C0 File Offset: 0x001697C0
		internal static ParameterInfo Cast(ParameterInfo oldValue, ParameterInfo newType, CultureInfo language, ref bool metaChanges)
		{
			object[] array = null;
			object[] array2 = null;
			if (oldValue.Values != null)
			{
				array = new object[oldValue.Values.Length];
				for (int i = 0; i < oldValue.Values.Length; i++)
				{
					if (!ParameterBase.Cast(oldValue.Values[i], oldValue.DataType, out array[i], newType.DataType, language))
					{
						return null;
					}
				}
			}
			if (oldValue.DefaultValues != null)
			{
				array2 = new object[oldValue.DefaultValues.Length];
				for (int j = 0; j < oldValue.DefaultValues.Length; j++)
				{
					if (!ParameterBase.Cast(oldValue.DefaultValues[j], oldValue.DataType, out array2[j], newType.DataType, language))
					{
						return null;
					}
				}
			}
			if (oldValue.DataType != newType.DataType)
			{
				metaChanges = true;
			}
			ParameterInfo parameterInfo = new ParameterInfo(newType);
			parameterInfo.Values = array;
			parameterInfo.DefaultValues = array2;
			parameterInfo.StoreLabels();
			return parameterInfo;
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x0016B698 File Offset: 0x00169898
		public static string CastToString(object val, DataType type, CultureInfo language)
		{
			object obj;
			if (!ParameterBase.Cast(val, type, out obj, DataType.String, language))
			{
				throw new InternalCatalogException("Can not cast value of report parameter to string.");
			}
			return ParameterInfo.CastValueToLabelString(val, language);
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x0016B6C5 File Offset: 0x001698C5
		public string CastToString(object val, CultureInfo language)
		{
			return ParameterInfo.CastToString(val, base.DataType, language);
		}

		// Token: 0x06005638 RID: 22072 RVA: 0x0016B6D4 File Offset: 0x001698D4
		internal static string CastValueToLabelString(object val, CultureInfo language)
		{
			if (val == null)
			{
				return null;
			}
			return Convert.ToString(val, language);
		}

		// Token: 0x04002D8B RID: 11659
		internal const string ParametersXmlElement = "Parameters";

		// Token: 0x04002D8C RID: 11660
		internal const string ParameterXmlElement = "Parameter";

		// Token: 0x04002D8D RID: 11661
		internal const string ValidValueXmlElement = "ValidValue";

		// Token: 0x04002D8E RID: 11662
		internal const string LabelXmlElement = "Label";

		// Token: 0x04002D8F RID: 11663
		internal const string ValuesXmlElement = "Values";

		// Token: 0x04002D90 RID: 11664
		internal const string ParametersLayoutXmlElement = "ParametersLayout";

		// Token: 0x04002D91 RID: 11665
		internal const string ParametersGridLayoutDefinitionXmlElement = "ParametersGridLayoutDefinition";

		// Token: 0x04002D92 RID: 11666
		internal const string ColumnsDefinitionXmlElement = "ColumnsDefinition";

		// Token: 0x04002D93 RID: 11667
		internal const string NumberOfColumnsXmlElement = "NumberOfColumns";

		// Token: 0x04002D94 RID: 11668
		internal const string NumberOfRowsXmlElement = "NumberOfRows";

		// Token: 0x04002D95 RID: 11669
		internal const string CellDefinitionsXmlElement = "CellDefinitions";

		// Token: 0x04002D96 RID: 11670
		internal const string CellDefinitionXmlElement = "CellDefinition";

		// Token: 0x04002D97 RID: 11671
		internal const string RowIndexXmlElement = "RowIndex";

		// Token: 0x04002D98 RID: 11672
		internal const string ColumnsIndexXmlElement = "ColumnIndex";

		// Token: 0x04002D99 RID: 11673
		internal const string ParameterNameXmlElement = "ParameterName";

		// Token: 0x04002D9A RID: 11674
		internal const string DynamicValidValuesXmlElement = "DynamicValidValues";

		// Token: 0x04002D9B RID: 11675
		internal const string DynamicDefaultValueXmlElement = "DynamicDefaultValue";

		// Token: 0x04002D9C RID: 11676
		internal const string DynamicPromptXmlElement = "DynamicPrompt";

		// Token: 0x04002D9D RID: 11677
		internal const string DependenciesXmlElement = "Dependencies";

		// Token: 0x04002D9E RID: 11678
		internal const string DependencyXmlElement = "Dependency";

		// Token: 0x04002D9F RID: 11679
		internal const string UserProfileStateElement = "UserProfileState";

		// Token: 0x04002DA0 RID: 11680
		internal const string UseExplicitDefaultValueXmlElement = "UseExplicitDefaultValue";

		// Token: 0x04002DA1 RID: 11681
		internal const string ValuesChangedXmlElement = "ValuesChanged";

		// Token: 0x04002DA2 RID: 11682
		internal const string IsUserSuppliedXmlElement = "IsUserSupplied";

		// Token: 0x04002DA3 RID: 11683
		internal const string NilXmlAttribute = "nil";

		// Token: 0x04002DA4 RID: 11684
		private object[] m_values;

		// Token: 0x04002DA5 RID: 11685
		private string[] m_labels;

		// Token: 0x04002DA6 RID: 11686
		private bool m_isUserSupplied;

		// Token: 0x04002DA7 RID: 11687
		private bool m_dynamicValidValues;

		// Token: 0x04002DA8 RID: 11688
		private bool m_dynamicDefaultValue;

		// Token: 0x04002DA9 RID: 11689
		private bool m_dynamicPrompt;

		// Token: 0x04002DAA RID: 11690
		private string m_prompt;

		// Token: 0x04002DAB RID: 11691
		private ParameterInfoCollection m_dependencyList;

		// Token: 0x04002DAC RID: 11692
		private ValidValueList m_validValues;

		// Token: 0x04002DAD RID: 11693
		private int[] m_dependencyIndexList;

		// Token: 0x04002DAE RID: 11694
		[NonSerialized]
		private bool m_valuesChanged;

		// Token: 0x04002DAF RID: 11695
		[NonSerialized]
		private ReportParameterState m_state = ReportParameterState.MissingValidValue;

		// Token: 0x04002DB0 RID: 11696
		[NonSerialized]
		private bool m_othersDependOnMe;

		// Token: 0x04002DB1 RID: 11697
		[NonSerialized]
		private bool m_useExplicitDefaultValue;

		// Token: 0x04002DB2 RID: 11698
		[NonSerialized]
		private int m_indexInCollection = -1;

		// Token: 0x04002DB3 RID: 11699
		[NonSerialized]
		private bool m_missingUpstreamDataSourcePrompt;

		// Token: 0x04002DB4 RID: 11700
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ParameterInfo.GetNewDeclaration();
	}
}
