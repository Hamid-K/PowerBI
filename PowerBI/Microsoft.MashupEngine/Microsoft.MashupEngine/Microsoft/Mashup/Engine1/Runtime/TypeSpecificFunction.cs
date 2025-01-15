using System;
using Microsoft.Mashup.Engine.Host;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200131F RID: 4895
	internal static class TypeSpecificFunction
	{
		// Token: 0x04004680 RID: 18048
		public static FunctionValue ListRandom = new Library.List.RandomFunctionValue(EngineHost.Empty);

		// Token: 0x04004681 RID: 18049
		public static FunctionValue NumberRandom = new Library.Number.RandomFunctionValue(EngineHost.Empty);

		// Token: 0x04004682 RID: 18050
		public static FunctionValue NumberRandomBetween = new Library.Number.RandomBetweenFunctionValue(EngineHost.Empty);

		// Token: 0x04004683 RID: 18051
		public static FunctionValue TextNewGuid = new Library.Text.NewGuidFunctionValue(EngineHost.Empty);
	}
}
