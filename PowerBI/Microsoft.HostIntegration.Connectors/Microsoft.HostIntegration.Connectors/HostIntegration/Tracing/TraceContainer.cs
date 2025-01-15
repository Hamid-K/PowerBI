using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.HostIntegration.StrictResources.TracingRuntime;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000670 RID: 1648
	public abstract class TraceContainer : ITraceContainer
	{
		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x0600372B RID: 14123 RVA: 0x000B9F85 File Offset: 0x000B8185
		// (set) Token: 0x0600372C RID: 14124 RVA: 0x000B9F8D File Offset: 0x000B818D
		public int Identifier { get; private set; }

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x0600372D RID: 14125 RVA: 0x000B9F96 File Offset: 0x000B8196
		// (set) Token: 0x0600372E RID: 14126 RVA: 0x000B9F9E File Offset: 0x000B819E
		public string Name { get; private set; }

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x0600372F RID: 14127 RVA: 0x000B9FA7 File Offset: 0x000B81A7
		// (set) Token: 0x06003730 RID: 14128 RVA: 0x000B9FAF File Offset: 0x000B81AF
		public string DisplayName { get; private set; }

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x000B9FB8 File Offset: 0x000B81B8
		// (set) Token: 0x06003732 RID: 14130 RVA: 0x000B9FC0 File Offset: 0x000B81C0
		public bool SupportsInstances { get; private set; }

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06003733 RID: 14131 RVA: 0x000B9FC9 File Offset: 0x000B81C9
		// (set) Token: 0x06003734 RID: 14132 RVA: 0x000B9FD1 File Offset: 0x000B81D1
		public string InstanceName
		{
			get
			{
				return this.changeableInstanceName;
			}
			set
			{
				this.changeableInstanceName = value;
				this.InstanceDisplayName = value;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06003735 RID: 14133 RVA: 0x000B9FE1 File Offset: 0x000B81E1
		// (set) Token: 0x06003736 RID: 14134 RVA: 0x000B9FE9 File Offset: 0x000B81E9
		public string InstanceDisplayName { get; private set; }

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06003737 RID: 14135 RVA: 0x000B9FF2 File Offset: 0x000B81F2
		// (set) Token: 0x06003738 RID: 14136 RVA: 0x000B9FFA File Offset: 0x000B81FA
		public bool UsesLevels { get; private set; }

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06003739 RID: 14137 RVA: 0x000BA003 File Offset: 0x000B8203
		// (set) Token: 0x0600373A RID: 14138 RVA: 0x000BA00B File Offset: 0x000B820B
		public List<ITracePointInformation> TracePoints { get; private set; }

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x0600373B RID: 14139 RVA: 0x000BA014 File Offset: 0x000B8214
		public int ExtraColumns
		{
			get
			{
				return (int)this.extraColumns;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x0600373C RID: 14140 RVA: 0x000BA01C File Offset: 0x000B821C
		public bool[] FilterableColumns
		{
			get
			{
				return this.filterableColumns;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x0600373D RID: 14141 RVA: 0x000BA024 File Offset: 0x000B8224
		// (set) Token: 0x0600373E RID: 14142 RVA: 0x000BA02C File Offset: 0x000B822C
		public object[] ExtraValues { get; set; }

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x0600373F RID: 14143 RVA: 0x000BA035 File Offset: 0x000B8235
		internal string[] ColumnNames
		{
			get
			{
				return this.columnNames;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06003740 RID: 14144 RVA: 0x000BA03D File Offset: 0x000B823D
		internal string[] LongestTexts
		{
			get
			{
				return this.longestTexts;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06003741 RID: 14145 RVA: 0x000BA045 File Offset: 0x000B8245
		internal bool ColumnsHaveMeanings
		{
			get
			{
				return this.columnsHaveMeanings;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06003742 RID: 14146 RVA: 0x000BA04D File Offset: 0x000B824D
		internal int[] CommonTracePointIdentifierToSpecific
		{
			get
			{
				return this.commonTracePointIdentifierToSpecific;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06003743 RID: 14147 RVA: 0x000BA055 File Offset: 0x000B8255
		internal int[][] CommonTracePointPropertyIdentifierToSpecific
		{
			get
			{
				return this.commonTracePointPropertyIdentifierToSpecific;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06003744 RID: 14148 RVA: 0x000BA05D File Offset: 0x000B825D
		// (set) Token: 0x06003745 RID: 14149 RVA: 0x000BA065 File Offset: 0x000B8265
		internal Dictionary<string, ITracePointPropertyInformation> PropertyNameToInformation { get; private set; }

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06003746 RID: 14150 RVA: 0x000BA06E File Offset: 0x000B826E
		// (set) Token: 0x06003747 RID: 14151 RVA: 0x000BA076 File Offset: 0x000B8276
		public List<ITracePointPropertyInformation> AllProperties { get; private set; }

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06003748 RID: 14152 RVA: 0x000BA07F File Offset: 0x000B827F
		// (set) Token: 0x06003749 RID: 14153 RVA: 0x000BA087 File Offset: 0x000B8287
		internal List<ITracePointInformation> AllTracePoints { get; private set; }

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x0600374A RID: 14154 RVA: 0x000BA090 File Offset: 0x000B8290
		// (set) Token: 0x0600374B RID: 14155 RVA: 0x000BA098 File Offset: 0x000B8298
		public int ProcessId { get; private set; }

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x0600374C RID: 14156 RVA: 0x000BA0A1 File Offset: 0x000B82A1
		// (set) Token: 0x0600374D RID: 14157 RVA: 0x000BA0A9 File Offset: 0x000B82A9
		public int HighestPropertyIdentifier { get; private set; }

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x0600374E RID: 14158 RVA: 0x000BA0B2 File Offset: 0x000B82B2
		public int MaximumDataBytesTraced
		{
			get
			{
				return this.traceTree.MaximumDataBytesTraced;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x0600374F RID: 14159 RVA: 0x000BA0BF File Offset: 0x000B82BF
		// (set) Token: 0x06003750 RID: 14160 RVA: 0x000BA0C7 File Offset: 0x000B82C7
		public long Correlator { get; private set; }

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06003751 RID: 14161 RVA: 0x000BA0D0 File Offset: 0x000B82D0
		// (set) Token: 0x06003752 RID: 14162 RVA: 0x000BA0D8 File Offset: 0x000B82D8
		public bool LongRunning { get; set; }

		// Token: 0x06003753 RID: 14163 RVA: 0x000BA0E4 File Offset: 0x000B82E4
		public void Release()
		{
			Dictionary<long, TraceContainer> correlatorToInstances = this.TraceContainerInformation.CorrelatorToInstances;
			lock (correlatorToInstances)
			{
				this.TraceContainerInformation.CorrelatorToInstances.Remove(this.Correlator);
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06003754 RID: 14164 RVA: 0x000BA13C File Offset: 0x000B833C
		// (set) Token: 0x06003755 RID: 14165 RVA: 0x000BA144 File Offset: 0x000B8344
		internal TraceContainerInformation TraceContainerInformation { get; set; }

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06003756 RID: 14166 RVA: 0x000BA14D File Offset: 0x000B834D
		// (set) Token: 0x06003757 RID: 14167 RVA: 0x000BA155 File Offset: 0x000B8355
		private bool IsDefinitionContainer { get; set; }

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06003758 RID: 14168 RVA: 0x000BA15E File Offset: 0x000B835E
		public ILiveTraceWriter TraceWriter
		{
			get
			{
				if (this.doingLiveTracing)
				{
					return TraceContainer.traceWriter;
				}
				return null;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x000BA16F File Offset: 0x000B836F
		// (set) Token: 0x0600375A RID: 14170 RVA: 0x000BA177 File Offset: 0x000B8377
		public bool ShouldTrace { get; private set; }

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x0600375B RID: 14171 RVA: 0x000BA180 File Offset: 0x000B8380
		public bool HasSomeTracingSet
		{
			get
			{
				return this.traceTree.HasSomeTracingSet;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (set) Token: 0x0600375C RID: 14172 RVA: 0x000BA18D File Offset: 0x000B838D
		public bool HasHisListener
		{
			set
			{
				this.ShouldTrace = value || this.doingLiveTracing;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x0600375D RID: 14173 RVA: 0x000BA1A1 File Offset: 0x000B83A1
		// (set) Token: 0x0600375E RID: 14174 RVA: 0x000BA1BE File Offset: 0x000B83BE
		public int CodePageForData
		{
			get
			{
				if (this.codePageUsed != null)
				{
					return this.codePageUsed.Value;
				}
				return 37;
			}
			set
			{
				this.codePageUsed = new int?(value);
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x0600375F RID: 14175 RVA: 0x00002B16 File Offset: 0x00000D16
		public virtual bool CanBeUsed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x000BA1CC File Offset: 0x000B83CC
		// (set) Token: 0x06003761 RID: 14177 RVA: 0x000BA1D4 File Offset: 0x000B83D4
		public List<string> InstanceNamesInConfigurationFile { get; set; }

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06003762 RID: 14178 RVA: 0x000BA1DD File Offset: 0x000B83DD
		// (set) Token: 0x06003763 RID: 14179 RVA: 0x000BA1E5 File Offset: 0x000B83E5
		public bool IsInConfigurationFile { get; set; }

		// Token: 0x06003764 RID: 14180 RVA: 0x000BA1EE File Offset: 0x000B83EE
		public string GetColumnName(int columnNumber)
		{
			return this.columnNames[columnNumber];
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x000BA1F8 File Offset: 0x000B83F8
		public string GetLongestText(int columnNumber)
		{
			return this.longestTexts[columnNumber];
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x000BA204 File Offset: 0x000B8404
		public virtual string GetColumn(string extraData, int columnNumber)
		{
			int i = -1;
			int num = 0;
			int num2 = -1;
			string text;
			while (i < columnNumber)
			{
				num2 = extraData.IndexOf(':', num);
				text = extraData.Substring(num, num2 - num);
				num = num2 + 1;
				int num3 = int.Parse(text);
				num2 += num3 + 1;
				i++;
				if (i == columnNumber)
				{
					break;
				}
				num = num2 + 1;
			}
			text = extraData.Substring(num, num2 - num);
			if (this.columnsHaveMeanings)
			{
				text = this.GetMeaningOfColumn(text, columnNumber);
			}
			return text;
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000189CC File Offset: 0x00016BCC
		public virtual string GetMeaningOfColumn(string text, int columnNumber)
		{
			return null;
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000BA270 File Offset: 0x000B8470
		public virtual string PackExtraData()
		{
			if (this.extraColumns == 0U)
			{
				return null;
			}
			if ((ulong)this.extraColumns != (ulong)((long)this.ExtraValues.Length))
			{
				throw new TraceException("BUGBUG: PackExtraData called with the wrong number of values");
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			object[] extraValues = this.ExtraValues;
			for (int i = 0; i < extraValues.Length; i++)
			{
				string text = extraValues[i].ToString();
				if (!flag)
				{
					stringBuilder.AppendFormat(":{0}:{1}", text.Length.ToString(), text);
				}
				else
				{
					stringBuilder.AppendFormat("{0}:{1}", text.Length.ToString(), text);
					flag = false;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000BA314 File Offset: 0x000B8514
		public int MapSharedTracePointToSpecific(SharedTracePoints sharedTracePoint)
		{
			return this.commonTracePointIdentifierToSpecific[(int)sharedTracePoint];
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x000BA31E File Offset: 0x000B851E
		public int[] SharedPropertiesToSpecific(SharedTracePoints sharedTracePoint)
		{
			return this.commonTracePointPropertyIdentifierToSpecific[(int)sharedTracePoint];
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x000BA328 File Offset: 0x000B8528
		public void UpdateTraceTree(TraceTree newTree)
		{
			if (this.allTopLevelTracePoints != null)
			{
				foreach (BaseTracePoint baseTracePoint in this.allTopLevelTracePoints.Values)
				{
					baseTracePoint.UpdateTraceTree(newTree);
				}
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x0600376C RID: 14188 RVA: 0x000BA388 File Offset: 0x000B8588
		// (set) Token: 0x0600376D RID: 14189 RVA: 0x000BA390 File Offset: 0x000B8590
		public TraceTree TraceTree
		{
			get
			{
				return this.traceTree;
			}
			set
			{
				object obj = this.lockTraceTree;
				lock (obj)
				{
					value.EvaluateDefinitionTree();
					this.traceTree = value;
					if (this.LongRunning)
					{
						Dictionary<long, TraceContainer> correlatorToInstances = this.TraceContainerInformation.CorrelatorToInstances;
						lock (correlatorToInstances)
						{
							foreach (TraceContainer traceContainer in this.TraceContainerInformation.CorrelatorToInstances.Values)
							{
								traceContainer.UpdateTraceTree(this.traceTree);
							}
						}
					}
					if (!this.doingLiveTracing && value.LiveTracing)
					{
						this.doingLiveTracing = true;
						if (TraceContainer.numberOfTraceWriters == 0U)
						{
							TraceContainer.traceWriter = new LiveTraceWriter(TraceContainer.liveTracingBufferSize);
						}
						TraceContainer.numberOfTraceWriters += 1U;
					}
					else if (this.doingLiveTracing && !value.LiveTracing)
					{
						TraceContainer.numberOfTraceWriters -= 1U;
						if (TraceContainer.numberOfTraceWriters == 0U)
						{
							TraceContainer.traceWriter.Close();
							TraceContainer.traceWriter = null;
						}
					}
				}
			}
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x000BA4CC File Offset: 0x000B86CC
		protected TraceContainer(bool supportsTraceContainerInstances, bool allTracePointsUsesLevels, string name, string displayName, string instanceName, ContainerIdentifier containerIdentifier, List<ITracePointInformation> topLevelTracePoints, bool longRunning)
		{
			this.InternalConstructor(supportsTraceContainerInstances, allTracePointsUsesLevels, name, displayName, instanceName, containerIdentifier, topLevelTracePoints, 0, null, null, null, false, false, null, null, longRunning);
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x000BA508 File Offset: 0x000B8708
		protected TraceContainer(bool supportsTraceContainerInstances, bool allTracePointsUsesLevels, string name, string displayName, string instanceName, ContainerIdentifier containerIdentifier, List<ITracePointInformation> topLevelTracePoints, int[] commonTracePointIdToSpecific, int[][] commonTracePointPropertyIdToSpecific, bool longRunning)
		{
			this.InternalConstructor(supportsTraceContainerInstances, allTracePointsUsesLevels, name, displayName, instanceName, containerIdentifier, topLevelTracePoints, 0, null, null, null, false, true, commonTracePointIdToSpecific, commonTracePointPropertyIdToSpecific, longRunning);
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000BA544 File Offset: 0x000B8744
		protected TraceContainer(bool supportsTraceContainerInstances, bool allTracePointsUsesLevels, string name, string displayName, string instanceName, ContainerIdentifier containerIdentifier, List<ITracePointInformation> topLevelTracePoints, int extraColumns, bool[] filterableColumns, string[] columnNames, string[] longestTexts, bool columnsHaveMeanings, bool longRunning)
		{
			this.InternalConstructor(supportsTraceContainerInstances, allTracePointsUsesLevels, name, displayName, instanceName, containerIdentifier, topLevelTracePoints, extraColumns, filterableColumns, columnNames, longestTexts, columnsHaveMeanings, false, null, null, longRunning);
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x000BA584 File Offset: 0x000B8784
		protected TraceContainer(bool supportsTraceContainerInstances, bool allTracePointsUsesLevels, string name, string displayName, string instanceName, ContainerIdentifier containerIdentifier, List<ITracePointInformation> topLevelTracePoints, int extraColumns, bool[] filterableColumns, string[] columnNames, string[] longestTexts, bool columnsHaveMeanings, int[] commonTracePointIdToSpecific, int[][] commonTracePointPropertyIdToSpecific, bool longRunning)
		{
			this.InternalConstructor(supportsTraceContainerInstances, allTracePointsUsesLevels, name, displayName, instanceName, containerIdentifier, topLevelTracePoints, extraColumns, filterableColumns, columnNames, longestTexts, columnsHaveMeanings, true, commonTracePointIdToSpecific, commonTracePointPropertyIdToSpecific, longRunning);
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x000BA5C4 File Offset: 0x000B87C4
		private void InternalConstructor(bool supportsTraceContainerInstances, bool allTracePointsUsesLevels, string name, string displayName, string instanceName, ContainerIdentifier containerIdentifier, List<ITracePointInformation> topLevelTracePoints, int extraColumns, bool[] filterableColumns, string[] columnNames, string[] longestTexts, bool columnsHaveMeanings, bool useMappings, int[] commonTracePointIdToSpecific, int[][] commonTracePointPropertyIdToSpecific, bool longRunning)
		{
			string text;
			string text2;
			if (string.IsNullOrEmpty(instanceName))
			{
				text = name;
				text2 = displayName;
			}
			else
			{
				text = instanceName;
				text2 = instanceName;
			}
			if (useMappings)
			{
				this.commonTracePointIdentifierToSpecific = commonTracePointIdToSpecific;
				this.commonTracePointPropertyIdentifierToSpecific = commonTracePointPropertyIdToSpecific;
			}
			this.Identifier = (int)containerIdentifier;
			this.SupportsInstances = supportsTraceContainerInstances;
			this.UsesLevels = allTracePointsUsesLevels;
			this.Name = name;
			this.DisplayName = displayName;
			this.changeableInstanceName = text;
			this.InstanceDisplayName = text2;
			this.TracePoints = topLevelTracePoints;
			this.extraColumns = (uint)extraColumns;
			this.columnNames = columnNames;
			this.longestTexts = longestTexts;
			this.columnsHaveMeanings = columnsHaveMeanings;
			this.filterableColumns = filterableColumns;
			this.LongRunning = longRunning;
			this.Correlator = Interlocked.Increment(ref TraceContainer.staticCorrelator);
			this.IsDefinitionContainer = false;
			TraceContainer traceContainer;
			if (TraceContainer.IdToTraceContainerInformations[(int)containerIdentifier] == null)
			{
				TraceContainerInformation[] idToTraceContainerInformations = TraceContainer.IdToTraceContainerInformations;
				lock (idToTraceContainerInformations)
				{
					if (TraceContainer.IdToTraceContainerInformations[(int)containerIdentifier] == null)
					{
						traceContainer = this.GetTraceDefinition(this);
						traceContainer.CheckTracePointConsistency();
						TraceContainerInformation traceContainerInformation = new TraceContainerInformation(traceContainer);
						TraceRuntime.AddContainer(traceContainer);
						TraceContainer.IdToTraceContainerInformations[this.Identifier] = traceContainerInformation;
					}
				}
			}
			TraceContainerInformation traceContainerInformation2 = TraceContainer.IdToTraceContainerInformations[this.Identifier];
			if (this.LongRunning)
			{
				Dictionary<long, TraceContainer> correlatorToInstances = traceContainerInformation2.CorrelatorToInstances;
				lock (correlatorToInstances)
				{
					traceContainerInformation2.CorrelatorToInstances.Add(this.Correlator, this);
				}
			}
			if (longRunning)
			{
				this.allTopLevelTracePoints = new Dictionary<int, BaseTracePoint>();
			}
			this.TraceContainerInformation = traceContainerInformation2;
			traceContainer = traceContainerInformation2.DefinitionTraceContainer;
			this.ShouldTrace = traceContainer.ShouldTrace;
			this.doingLiveTracing = traceContainer.doingLiveTracing;
			this.IsInConfigurationFile = traceContainer.IsInConfigurationFile;
			if (this.LongRunning || traceContainer.TraceTree.PropertiesInTraceTree)
			{
				this.traceTree = (TraceTree)traceContainer.TraceTree.Clone();
			}
			else
			{
				this.traceTree = traceContainer.TraceTree;
			}
			this.PropertyNameToInformation = traceContainer.PropertyNameToInformation;
			this.AllTracePoints = traceContainer.AllTracePoints;
			this.ProcessId = traceContainer.ProcessId;
		}

		// Token: 0x06003773 RID: 14195
		public abstract TraceContainer GetTraceDefinition(TraceContainer traceContainer);

		// Token: 0x06003774 RID: 14196 RVA: 0x000BA7E0 File Offset: 0x000B89E0
		protected TraceContainer(bool supportsTraceContainerInstances, bool allTracePointsUsesLevels, string name, string displayName, string instanceName, ContainerIdentifier containerIdentifier, List<ITracePointInformation> topLevelTracePoints, string configurationFileName)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (topLevelTracePoints == null)
			{
				throw new ArgumentNullException("topLevelTracePoints");
			}
			if (topLevelTracePoints.Count == 0)
			{
				throw new ArgumentNullException("topLevelTracePoints");
			}
			if (containerIdentifier > (ContainerIdentifier)Globals.HighestContainerIdentifier)
			{
				throw new ArgumentOutOfRangeException("containerIdentifier");
			}
			this.Identifier = (int)containerIdentifier;
			this.ProcessId = Process.GetCurrentProcess().Id;
			this.SupportsInstances = supportsTraceContainerInstances;
			this.UsesLevels = allTracePointsUsesLevels;
			this.Name = name;
			this.DisplayName = displayName;
			if (!supportsTraceContainerInstances)
			{
				instanceName = name;
				this.InstanceDisplayName = displayName;
			}
			else
			{
				if (string.IsNullOrEmpty(configurationFileName) && string.IsNullOrEmpty(instanceName))
				{
					throw new ArgumentNullException("instanceName");
				}
				this.InstanceDisplayName = instanceName;
			}
			this.changeableInstanceName = instanceName;
			this.TracePoints = topLevelTracePoints;
			this.PropertyNameToInformation = new Dictionary<string, ITracePointPropertyInformation>();
			this.CheckTracePointConsistency();
			this.traceTree = TraceConfigurationRuntime.AddConfigurationFile(this, configurationFileName);
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x000BA8E4 File Offset: 0x000B8AE4
		public TraceContainer(TraceContainer traceContainer)
		{
			this.commonTracePointIdentifierToSpecific = traceContainer.CommonTracePointIdentifierToSpecific;
			this.commonTracePointPropertyIdentifierToSpecific = traceContainer.CommonTracePointPropertyIdentifierToSpecific;
			this.Identifier = traceContainer.Identifier;
			this.ProcessId = Process.GetCurrentProcess().Id;
			this.SupportsInstances = traceContainer.SupportsInstances;
			this.UsesLevels = traceContainer.UsesLevels;
			this.Name = traceContainer.Name;
			this.changeableInstanceName = traceContainer.InstanceName;
			this.TracePoints = traceContainer.TracePoints;
			this.extraColumns = (uint)traceContainer.ExtraColumns;
			this.columnNames = traceContainer.ColumnNames;
			this.longestTexts = traceContainer.LongestTexts;
			this.columnsHaveMeanings = traceContainer.ColumnsHaveMeanings;
			this.filterableColumns = traceContainer.FilterableColumns;
			this.LongRunning = traceContainer.LongRunning;
			this.PropertyNameToInformation = new Dictionary<string, ITracePointPropertyInformation>();
			this.Correlator = 0L;
			this.IsDefinitionContainer = true;
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000BA9D4 File Offset: 0x000B8BD4
		public ITracePointPropertyInformation PropertyInformationFromName(string name)
		{
			if (!this.PropertyNameToInformation.ContainsKey(name))
			{
				throw new TraceException(SR.UnknownTracepointPropertyInContainer(name, this.Name));
			}
			return this.PropertyNameToInformation[name];
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x000BAA04 File Offset: 0x000B8C04
		public ITracePointPropertyInformation PropertyInformationFromIdentifier(int identifier)
		{
			foreach (ITracePointPropertyInformation tracePointPropertyInformation in this.PropertyNameToInformation.Values)
			{
				if (tracePointPropertyInformation.Identifier == identifier)
				{
					return tracePointPropertyInformation;
				}
			}
			throw new TraceException(SR.UnknownTracepointPropertyIdentifierInContainer(identifier, this.Name));
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x000BAA7C File Offset: 0x000B8C7C
		public ITracePointInformation TracePointInformationFromIdentifier(int identifier)
		{
			foreach (ITracePointInformation tracePointInformation in this.AllTracePoints)
			{
				if (tracePointInformation.Identifier == identifier)
				{
					return tracePointInformation;
				}
			}
			throw new TraceException(SR.UnknownTracepointIdentifierInContainer(identifier, this.Name));
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x000BAAF0 File Offset: 0x000B8CF0
		public string GenerateXml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<container name=\"{0}\"", this.Name);
			if (this.SupportsInstances)
			{
				stringBuilder.AppendFormat(" instanceName=\"{0}\"", this.InstanceName);
			}
			if (this.traceTree.LiveTracing)
			{
				stringBuilder.Append(" liveTracing=\"true\"");
			}
			if (this.traceTree.MaximumDataBytesTraced != 256)
			{
				stringBuilder.AppendFormat(" maxBytesTraced=\"{0}\"", this.MaximumDataBytesTraced);
			}
			stringBuilder.Append(">");
			stringBuilder.Append(this.traceTree.GenerateXml());
			stringBuilder.Append("</container>");
			return stringBuilder.ToString();
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x000BABA4 File Offset: 0x000B8DA4
		private void CheckTracePointConsistency()
		{
			List<int> list = new List<int>();
			List<string> list2 = new List<string>();
			List<int> list3 = new List<int>();
			List<string> list4 = new List<string>();
			this.AllProperties = new List<ITracePointPropertyInformation>();
			this.AllTracePoints = new List<ITracePointInformation>();
			foreach (ITracePointInformation tracePointInformation in this.TracePoints)
			{
				this.CheckSingleTracePoint(tracePointInformation, list, list2, list3, list4);
			}
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x000BAC2C File Offset: 0x000B8E2C
		private void CheckSingleTracePoint(ITracePointInformation tracePointInformation, List<int> tracePointIdentifiers, List<string> tracePointNames, List<int> propertyIdentifiers, List<string> propertyNames)
		{
			tracePointNames.Add(tracePointInformation.Name);
			tracePointIdentifiers.Add(tracePointInformation.Identifier);
			if (tracePointInformation.Properties != null)
			{
				foreach (ITracePointPropertyInformation tracePointPropertyInformation in tracePointInformation.Properties)
				{
					if (tracePointPropertyInformation.Identifier > this.HighestPropertyIdentifier)
					{
						this.HighestPropertyIdentifier = tracePointPropertyInformation.Identifier;
					}
					propertyNames.Add(tracePointPropertyInformation.Name);
					propertyIdentifiers.Add(tracePointPropertyInformation.Identifier);
					this.AllProperties.Add(tracePointPropertyInformation);
					if (tracePointPropertyInformation.ValueType == PropertyType.Enumeration)
					{
						List<int> list = new List<int>();
						List<string> list2 = new List<string>();
						foreach (ITracePointPropertyEnumerationValue tracePointPropertyEnumerationValue in tracePointPropertyInformation.EnumerationValues)
						{
							list2.Add(tracePointPropertyEnumerationValue.Name);
							list.Add(tracePointPropertyEnumerationValue.Identifier);
						}
					}
					this.PropertyNameToInformation.Add(tracePointPropertyInformation.Name, tracePointPropertyInformation);
				}
			}
			if (tracePointInformation.TracePoints != null)
			{
				foreach (ITracePointInformation tracePointInformation2 in tracePointInformation.TracePoints)
				{
					this.CheckSingleTracePoint(tracePointInformation2, tracePointIdentifiers, tracePointNames, propertyIdentifiers, propertyNames);
				}
			}
			this.AllTracePoints.Add(tracePointInformation);
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000BADC0 File Offset: 0x000B8FC0
		public TraceTree CreateInstanceTracepoint(BaseTracePoint topLevelTracePoint)
		{
			if (this.LongRunning)
			{
				object obj = this.lockTraceTree;
				TraceTree traceTree;
				lock (obj)
				{
					traceTree = this.traceTree;
					this.allTopLevelTracePoints[topLevelTracePoint.Identifier] = topLevelTracePoint;
				}
				return traceTree;
			}
			return this.traceTree;
		}

		// Token: 0x04001F9E RID: 8094
		private string changeableInstanceName;

		// Token: 0x04001FA2 RID: 8098
		private uint extraColumns;

		// Token: 0x04001FA3 RID: 8099
		private bool[] filterableColumns;

		// Token: 0x04001FA5 RID: 8101
		private string[] columnNames;

		// Token: 0x04001FA6 RID: 8102
		private string[] longestTexts;

		// Token: 0x04001FA7 RID: 8103
		private bool columnsHaveMeanings;

		// Token: 0x04001FA8 RID: 8104
		private int[] commonTracePointIdentifierToSpecific;

		// Token: 0x04001FA9 RID: 8105
		private int[][] commonTracePointPropertyIdentifierToSpecific;

		// Token: 0x04001FAA RID: 8106
		private TraceTree traceTree;

		// Token: 0x04001FAE RID: 8110
		private Dictionary<int, BaseTracePoint> allTopLevelTracePoints;

		// Token: 0x04001FB1 RID: 8113
		private static long staticCorrelator = 0L;

		// Token: 0x04001FB3 RID: 8115
		private object lockTraceTree = new object();

		// Token: 0x04001FB5 RID: 8117
		private static TraceContainerInformation[] IdToTraceContainerInformations = new TraceContainerInformation[Globals.HighestContainerIdentifier + 1];

		// Token: 0x04001FB8 RID: 8120
		private static ILiveTraceWriter traceWriter;

		// Token: 0x04001FB9 RID: 8121
		private static uint numberOfTraceWriters;

		// Token: 0x04001FBA RID: 8122
		private bool doingLiveTracing;

		// Token: 0x04001FBB RID: 8123
		private static int liveTracingBufferSize = 10000;

		// Token: 0x04001FBD RID: 8125
		private int? codePageUsed;
	}
}
