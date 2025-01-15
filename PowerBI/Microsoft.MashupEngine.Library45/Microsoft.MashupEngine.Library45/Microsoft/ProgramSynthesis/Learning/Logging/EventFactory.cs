using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Logging
{
	// Token: 0x0200075C RID: 1884
	internal class EventFactory
	{
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x00072124 File Offset: 0x00070324
		public LogInfo IncludedInfo { get; }

		// Token: 0x06002836 RID: 10294 RVA: 0x0007212C File Offset: 0x0007032C
		public EventFactory(LogInfo includedInfo)
		{
			this.IncludedInfo = includedInfo;
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x0007213C File Offset: 0x0007033C
		public EventNode StartedLearn(LearningTask task)
		{
			if ((this.IncludedInfo & LogInfo.Branch) == LogInfo.None)
			{
				return null;
			}
			EventNode eventNode = new EventNode("Learn", Array.Empty<EventNode>());
			eventNode["symbol"] = task.Symbol;
			eventNode["spec"] = task.Spec;
			eventNode["k"] = (task.IsOrdered ? task.K.Value.ToString(CultureInfo.InvariantCulture) : "all");
			eventNode["feature"] = (task.IsOrdered ? task.TopProgramsFeature.Value.Info.Name : null);
			eventNode["randomK"] = (task.RandomK.HasValue ? task.RandomK.Value.ToString(CultureInfo.InvariantCulture) : "null");
			eventNode["depth"] = task.RecursionDepths.Sum((KeyValuePair<Symbol, int> e) => e.Value);
			return eventNode;
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x00072260 File Offset: 0x00070460
		public EventNode StartedLearnAlternative(ILanguage alternative, LearningTask task)
		{
			if ((this.IncludedInfo & LogInfo.Branch) == LogInfo.None)
			{
				return null;
			}
			GrammarRule grammarRule = alternative as GrammarRule;
			if (grammarRule != null)
			{
				return EventFactory.StartedLearnRule(grammarRule, task);
			}
			ProgramSet programSet = alternative as ProgramSet;
			if (programSet == null)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Could not handle language of type \"{0}\" in call to {1}.", new object[]
				{
					alternative.GetType(),
					"StartedLearnRule"
				})));
			}
			return EventFactory.StartedLearnProgramSet(programSet, task);
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000722CA File Offset: 0x000704CA
		private static EventNode StartedLearnRule(GrammarRule rule, LearningTask task)
		{
			EventNode eventNode = new EventNode("LearnRule", Array.Empty<EventNode>());
			eventNode["rule"] = rule;
			return eventNode;
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x000722E7 File Offset: 0x000704E7
		private static EventNode StartedLearnProgramSet(ProgramSet programSet, LearningTask task)
		{
			EventNode eventNode = new EventNode("LearnProgramSet", Array.Empty<EventNode>());
			eventNode["programSet"] = programSet;
			eventNode["symbol"] = programSet.Symbol;
			return eventNode;
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x00072318 File Offset: 0x00070518
		public EventNode StartedUsePlan(WitnessingPlan plan, bool withDependencies, bool needsVerification)
		{
			if ((this.IncludedInfo & LogInfo.WitnessingPlan) == LogInfo.None)
			{
				return null;
			}
			EventNode eventNode = new EventNode("UsePlan", Array.Empty<EventNode>());
			eventNode["plan"] = plan;
			eventNode["hasDependencies"] = withDependencies;
			eventNode["verify"] = needsVerification;
			return eventNode;
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x00072370 File Offset: 0x00070570
		public EventNode Witness(GrammarRule rule, int paramIndex, WitnessFunction wf, params Spec[] prereqs)
		{
			if ((this.IncludedInfo & LogInfo.Witness) == LogInfo.None)
			{
				return null;
			}
			EventNode eventNode = new EventNode("Witness", Array.Empty<EventNode>());
			eventNode["param"] = rule.Body[paramIndex];
			eventNode["index"] = paramIndex;
			eventNode["function"] = wf.Origin;
			EventNode eventNode2 = eventNode;
			for (int i = 0; i < prereqs.Length; i++)
			{
				eventNode2["prereqs." + rule.Body[wf.Prerequisites[i].ParamIndex].Name] = prereqs[i];
			}
			return eventNode2;
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x00072414 File Offset: 0x00070614
		public EventNode Cluster(KeyValuePair<object[], ProgramSet> cluster, int index, GrammarRule rule, int paramIndex)
		{
			if ((this.IncludedInfo & LogInfo.Cluster) == LogInfo.None)
			{
				return null;
			}
			EventNode eventNode = new EventNode("Cluster", Array.Empty<EventNode>());
			eventNode["for"] = rule.Body[paramIndex];
			eventNode["i"] = index;
			eventNode["output"] = cluster.Key;
			EventNode eventNode2 = eventNode;
			if ((this.IncludedInfo & LogInfo.ProgramSets) != LogInfo.None)
			{
				eventNode2["programCluster"] = cluster.Value;
			}
			return eventNode2;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x00072496 File Offset: 0x00070696
		public static EventNode TopK(LogListener logListener)
		{
			if (logListener == null || (logListener.IncludedInfo & LogInfo.TopK) == LogInfo.None)
			{
				return null;
			}
			return new EventNode("TopK", Array.Empty<EventNode>());
		}
	}
}
