using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018BD RID: 6333
	internal class PowerQueryConstant : FormulaExpression
	{
		// Token: 0x0600CE60 RID: 52832 RVA: 0x002C0254 File Offset: 0x002BE454
		public PowerQueryConstant(string constant)
		{
			this.Constant = constant;
		}

		// Token: 0x170022B7 RID: 8887
		// (get) Token: 0x0600CE61 RID: 52833 RVA: 0x002C0263 File Offset: 0x002BE463
		public string Constant { get; }

		// Token: 0x170022B8 RID: 8888
		// (get) Token: 0x0600CE62 RID: 52834 RVA: 0x002C026B File Offset: 0x002BE46B
		public static PowerQueryConstant ListOfDigits { get; } = new PowerQueryConstant("{\"0\"..\"9\"}");

		// Token: 0x170022B9 RID: 8889
		// (get) Token: 0x0600CE63 RID: 52835 RVA: 0x002C0272 File Offset: 0x002BE472
		public static PowerQueryConstant ListOfLetters { get; } = new PowerQueryConstant("{\"A\"..\"Z\",\"a\"..\"z\"}");

		// Token: 0x170022BA RID: 8890
		// (get) Token: 0x0600CE64 RID: 52836 RVA: 0x002C0279 File Offset: 0x002BE479
		public static PowerQueryConstant Null { get; } = new PowerQueryConstant("null");

		// Token: 0x170022BB RID: 8891
		// (get) Token: 0x0600CE65 RID: 52837 RVA: 0x002C0280 File Offset: 0x002BE480
		public static PowerQueryConstant OccurrenceAll { get; } = new PowerQueryConstant("Occurrence.All");

		// Token: 0x170022BC RID: 8892
		// (get) Token: 0x0600CE66 RID: 52838 RVA: 0x002C0287 File Offset: 0x002BE487
		public static PowerQueryConstant OccurrenceLast { get; } = new PowerQueryConstant("Occurrence.Last");

		// Token: 0x170022BD RID: 8893
		// (get) Token: 0x0600CE67 RID: 52839 RVA: 0x002C028E File Offset: 0x002BE48E
		public static PowerQueryConstant RoundingModeAwayFromZero { get; } = new PowerQueryConstant("RoundingMode.AwayFromZero");

		// Token: 0x0600CE68 RID: 52840 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CE69 RID: 52841 RVA: 0x002C0295 File Offset: 0x002BE495
		protected override string ToCodeString()
		{
			return this.Constant;
		}
	}
}
