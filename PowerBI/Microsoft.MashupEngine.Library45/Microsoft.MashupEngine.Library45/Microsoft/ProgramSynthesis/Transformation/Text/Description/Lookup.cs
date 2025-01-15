using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C8D RID: 7309
	public class Lookup : TransformationDescription
	{
		// Token: 0x17002946 RID: 10566
		// (get) Token: 0x0600F76B RID: 63339 RVA: 0x0034BA91 File Offset: 0x00349C91
		[JsonIgnore]
		public Lookup LookupProgramNode
		{
			get
			{
				return GrammarBuilders.Instance(base.ProgramNode.Grammar).Node.CastRule.Lookup(base.ProgramNode);
			}
		}

		// Token: 0x0600F76C RID: 63340 RVA: 0x0034BAB8 File Offset: 0x00349CB8
		internal Lookup(Lookup programNode)
			: base(programNode.Node, TransformationCategory.Mutation, TransformationKind.Lookup)
		{
		}

		// Token: 0x17002947 RID: 10567
		// (get) Token: 0x0600F76D RID: 63341 RVA: 0x0034BACC File Offset: 0x00349CCC
		public IReadOnlyDictionary<Optional<string>, string> LookupDictionary
		{
			get
			{
				return this.LookupProgramNode.lookupDictionary.Value;
			}
		}
	}
}
