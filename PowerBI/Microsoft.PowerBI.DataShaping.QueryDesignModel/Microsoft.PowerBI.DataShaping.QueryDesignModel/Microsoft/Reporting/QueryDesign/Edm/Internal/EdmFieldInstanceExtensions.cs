using System;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001FE RID: 510
	internal static class EdmFieldInstanceExtensions
	{
		// Token: 0x06001812 RID: 6162 RVA: 0x000425FA File Offset: 0x000407FA
		internal static TReturn InvokeTypeSpecificFunction<TArgument, TReturn>(this IEdmFieldInstance arg1, TArgument arg2, Func<EdmFieldInstance, TArgument, TReturn> func1, Func<EdmHierarchyLevelInstance, TArgument, TReturn> func2)
		{
			return Util.InvokeStructSpecificFunction<TArgument, TReturn, IEdmFieldInstance, EdmFieldInstance, EdmHierarchyLevelInstance>(arg1, arg2, func1, func2);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00042605 File Offset: 0x00040805
		internal static TReturn InvokeTypeSpecificFunction<TArgument, TReturn>(this IEdmFieldInstance arg1, TArgument arg2, Func<TArgument, EdmFieldInstance, TReturn> func1, Func<TArgument, EdmHierarchyLevelInstance, TReturn> func2)
		{
			return Util.InvokeStructSpecificFunction<TArgument, TReturn, IEdmFieldInstance, EdmFieldInstance, EdmHierarchyLevelInstance>(arg1, arg2, func1, func2);
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00042610 File Offset: 0x00040810
		internal static TReturn InvokeTypeSpecificFunction<TReturn>(this IEdmFieldInstance arg1, Func<EdmFieldInstance, TReturn> func1, Func<EdmHierarchyLevelInstance, TReturn> func2)
		{
			return Util.InvokeStructSpecificFunction<TReturn, IEdmFieldInstance, EdmFieldInstance, EdmHierarchyLevelInstance>(arg1, func1, func2);
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0004261A File Offset: 0x0004081A
		internal static void InvokeTypeSpecificAction(this IEdmFieldInstance arg, Action<EdmFieldInstance> action1, Action<EdmHierarchyLevelInstance> action2)
		{
			Util.InvokeStructSpecificAction<IEdmFieldInstance, EdmFieldInstance, EdmHierarchyLevelInstance>(arg, action1, action2);
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00042624 File Offset: 0x00040824
		internal static bool SupportsTreatAs(this IEdmFieldInstance field)
		{
			return field.Field.Grouping.Identity == GroupingIdentity.Value || field.Field.Grouping.IdentityFields.Contains(field.Field);
		}
	}
}
