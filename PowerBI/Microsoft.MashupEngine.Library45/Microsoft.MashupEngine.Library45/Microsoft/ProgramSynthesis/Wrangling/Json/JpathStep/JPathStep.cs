using System;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep
{
	// Token: 0x02000194 RID: 404
	public abstract class JPathStep
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060008DE RID: 2270
		public abstract JPathStepKind Kind { get; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060008DF RID: 2271
		public abstract double Score { get; }

		// Token: 0x060008E0 RID: 2272
		public abstract JToken[] Apply(JToken token);

		// Token: 0x060008E1 RID: 2273
		internal abstract string Serialize();

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001B09C File Offset: 0x0001929C
		public static JPathStep From(string step)
		{
			if (string.IsNullOrEmpty(step))
			{
				return null;
			}
			if (step[0] == '[')
			{
				int num;
				if (!int.TryParse(step.Substring(1, step.Length - 2), out num))
				{
					return null;
				}
				return new IndexStep(num);
			}
			else
			{
				if (step == "..")
				{
					return new ParentStep();
				}
				if (step == "0")
				{
					return new SinglePropertyStep();
				}
				if (step == "@")
				{
					return new PropertyKeyStep();
				}
				if (step == "#")
				{
					return new PropertyValueStep();
				}
				if (step == "*")
				{
					return new StarStep();
				}
				if (!(step == "."))
				{
					return AccessStep.Deserialize(step);
				}
				return new CurrentStep();
			}
		}
	}
}
