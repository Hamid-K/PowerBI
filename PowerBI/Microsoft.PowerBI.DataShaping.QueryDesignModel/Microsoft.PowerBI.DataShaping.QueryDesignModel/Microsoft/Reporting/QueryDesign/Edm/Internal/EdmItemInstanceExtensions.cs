using System;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000209 RID: 521
	internal static class EdmItemInstanceExtensions
	{
		// Token: 0x06001865 RID: 6245 RVA: 0x00043007 File Offset: 0x00041207
		internal static TReturn InvokeTypeSpecificFunction<TReturn>(this IEdmItemInstance arg, Func<EdmPropertyInstance, TReturn> func1, Func<EdmHierarchyInstance, TReturn> func2)
		{
			return Util.InvokeStructSpecificFunction<TReturn, IEdmItemInstance, EdmPropertyInstance, EdmHierarchyInstance>(arg, func1, func2);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00043011 File Offset: 0x00041211
		internal static TReturn InvokeTypeSpecificFunction<TArgument, TReturn>(this IEdmItemInstance arg1, TArgument arg2, Func<TArgument, EdmPropertyInstance, TReturn> func1, Func<TArgument, EdmHierarchyInstance, TReturn> func2)
		{
			return Util.InvokeStructSpecificFunction<TArgument, TReturn, IEdmItemInstance, EdmPropertyInstance, EdmHierarchyInstance>(arg1, arg2, func1, func2);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0004301C File Offset: 0x0004121C
		internal static void InvokeTypeSpecificAction<TArgument>(this IEdmItemInstance arg1, TArgument arg2, Action<EdmPropertyInstance, TArgument> action1, Action<EdmHierarchyInstance, TArgument> action2)
		{
			Util.InvokeStructSpecificAction<IEdmItemInstance, EdmPropertyInstance, EdmHierarchyInstance, TArgument>(arg1, arg2, action1, action2);
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x00043027 File Offset: 0x00041227
		internal static void InvokeTypeSpecificAction(this IEdmItemInstance arg, Action<EdmPropertyInstance> action1, Action<EdmHierarchyInstance> action2)
		{
			Util.InvokeStructSpecificAction<IEdmItemInstance, EdmPropertyInstance, EdmHierarchyInstance>(arg, action1, action2);
		}
	}
}
