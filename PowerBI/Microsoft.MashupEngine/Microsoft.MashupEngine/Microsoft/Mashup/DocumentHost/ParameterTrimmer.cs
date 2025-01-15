using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.DocumentHost
{
	// Token: 0x02001946 RID: 6470
	internal class ParameterTrimmer
	{
		// Token: 0x0600A433 RID: 42035 RVA: 0x0021FF48 File Offset: 0x0021E148
		public ParameterTrimmer(IFirewallPlanCreator creator, IFirewallPlan firewallPlan, IMemberLetPartitionedDocument document)
		{
			this.creator = creator;
			this.firewallPlan = firewallPlan;
			this.document = document;
		}

		// Token: 0x0600A434 RID: 42036 RVA: 0x0021FF68 File Offset: 0x0021E168
		public IFirewallPlan TrimParameters()
		{
			HashSet<IPartitionKey> trimlist = new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance);
			foreach (IFirewallPartitionPlan firewallPartitionPlan in this.firewallPlan.PartitionPlans)
			{
				if (!firewallPartitionPlan.IsCyclic && !firewallPartitionPlan.Inputs.Any<IPartitionKey>() && !firewallPartitionPlan.Resources.Any<IResource>())
				{
					IMemberLetPartitionKey key = (IMemberLetPartitionKey)firewallPartitionPlan.PartitionKey;
					ISectionMember sectionMember = this.document.GetSectionDocument(key.Section).Section.Members.FirstOrDefault((ISectionMember member) => member.Name == key.Member);
					if (sectionMember != null && !ParameterTrimmer.InvocationFindingVisitor.HasInvocations(sectionMember.Value))
					{
						trimlist.Add(key);
					}
				}
			}
			if (trimlist.Count == 0)
			{
				return this.firewallPlan;
			}
			List<IFirewallPartitionPlan> list = new List<IFirewallPartitionPlan>();
			Func<IPartitionKey, bool> <>9__1;
			foreach (IFirewallPartitionPlan firewallPartitionPlan2 in this.firewallPlan.PartitionPlans)
			{
				IFirewallPlanCreator firewallPlanCreator = this.creator;
				IPartitionKey partitionKey = firewallPartitionPlan2.PartitionKey;
				int evaluationOrder = firewallPartitionPlan2.EvaluationOrder;
				bool isCyclic = firewallPartitionPlan2.IsCyclic;
				IEnumerable<IPartitionKey> inputs = firewallPartitionPlan2.Inputs;
				Func<IPartitionKey, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (IPartitionKey input) => !trimlist.Contains(input));
				}
				IFirewallPartitionPlan firewallPartitionPlan3 = firewallPlanCreator.CreatePartitionPlan(partitionKey, evaluationOrder, isCyclic, inputs.Where(func));
				firewallPartitionPlan3.AddResources(firewallPartitionPlan2.Resources);
				list.Add(firewallPartitionPlan3);
			}
			return this.creator.CreatePlan(list);
		}

		// Token: 0x04005580 RID: 21888
		private readonly IFirewallPlanCreator creator;

		// Token: 0x04005581 RID: 21889
		private readonly IFirewallPlan firewallPlan;

		// Token: 0x04005582 RID: 21890
		private readonly IMemberLetPartitionedDocument document;

		// Token: 0x02001947 RID: 6471
		private class InvocationFindingVisitor : ReadOnlyAstVisitor
		{
			// Token: 0x0600A435 RID: 42037 RVA: 0x00220130 File Offset: 0x0021E330
			public static bool HasInvocations(IExpression expression)
			{
				ParameterTrimmer.InvocationFindingVisitor invocationFindingVisitor = new ParameterTrimmer.InvocationFindingVisitor();
				invocationFindingVisitor.VisitExpression(expression);
				return invocationFindingVisitor.foundInvocation;
			}

			// Token: 0x0600A436 RID: 42038 RVA: 0x00220143 File Offset: 0x0021E343
			protected override void VisitExpression(IExpression expression)
			{
				if (!this.foundInvocation)
				{
					base.VisitExpression(expression);
				}
			}

			// Token: 0x0600A437 RID: 42039 RVA: 0x00220154 File Offset: 0x0021E354
			protected override void VisitInvocation(IInvocationExpression invocation)
			{
				if (invocation.Function.Kind == ExpressionKind.Constant)
				{
					base.VisitInvocation(invocation);
					return;
				}
				this.foundInvocation = true;
			}

			// Token: 0x04005583 RID: 21891
			private bool foundInvocation;
		}
	}
}
