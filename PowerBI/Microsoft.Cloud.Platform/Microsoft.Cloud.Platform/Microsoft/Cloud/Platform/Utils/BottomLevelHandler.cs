using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200019D RID: 413
	public sealed class BottomLevelHandler<TInstanceContext, TOperationContext>
	{
		// Token: 0x06000A98 RID: 2712 RVA: 0x00024716 File Offset: 0x00022916
		public BottomLevelHandler(Dictionary<Type, Action<Exception, TInstanceContext, TOperationContext>> exceptionHandlers, TInstanceContext instanceContext)
		{
			this.m_exceptionHandlers = exceptionHandlers;
			this.m_instanceContext = instanceContext;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0002472C File Offset: 0x0002292C
		public void Run(TOperationContext operationContext, Action actionToInvoke)
		{
			ExceptionFilters.TryFilterCatch(actionToInvoke, delegate(Exception ex)
			{
				if (!this.HasHandlerForException(ex))
				{
					return ExceptionDisposition.ContinueSearch;
				}
				return ExceptionDisposition.ExecuteHandler;
			}, delegate(Exception ex)
			{
				this.InvokeHandlerForException(ex, operationContext);
			});
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00024774 File Offset: 0x00022974
		public TResult Run<TResult>(TOperationContext operationContext, Func<TResult> functionToInvoke)
		{
			TResult result = default(TResult);
			ExceptionFilters.TryFilterCatch(delegate
			{
				result = functionToInvoke();
			}, delegate(Exception ex)
			{
				if (!this.HasHandlerForException(ex))
				{
					return ExceptionDisposition.ContinueSearch;
				}
				return ExceptionDisposition.ExecuteHandler;
			}, delegate(Exception ex)
			{
				this.InvokeHandlerForException(ex, operationContext);
			});
			return result;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000247DD File Offset: 0x000229DD
		private bool HasHandlerForException(Exception ex)
		{
			return this.GetHandlerForException(ex) != null;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000247E9 File Offset: 0x000229E9
		private void InvokeHandlerForException(Exception ex, TOperationContext operationContext)
		{
			this.GetHandlerForException(ex)(ex, this.m_instanceContext, operationContext);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00024800 File Offset: 0x00022A00
		[CanBeNull]
		private Action<Exception, TInstanceContext, TOperationContext> GetHandlerForException(Exception ex)
		{
			Type type = ex.PeelTargetInvocationException().GetType();
			Action<Exception, TInstanceContext, TOperationContext> action;
			while (!this.m_exceptionHandlers.TryGetValue(type, out action))
			{
				type = type.BaseType;
				if (type == typeof(object))
				{
					return null;
				}
			}
			return action;
		}

		// Token: 0x0400042D RID: 1069
		private Dictionary<Type, Action<Exception, TInstanceContext, TOperationContext>> m_exceptionHandlers;

		// Token: 0x0400042E RID: 1070
		private TInstanceContext m_instanceContext;
	}
}
