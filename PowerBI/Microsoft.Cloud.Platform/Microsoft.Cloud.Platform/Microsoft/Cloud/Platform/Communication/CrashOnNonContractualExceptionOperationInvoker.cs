using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004C5 RID: 1221
	internal class CrashOnNonContractualExceptionOperationInvoker : IOperationInvoker
	{
		// Token: 0x0600253D RID: 9533 RVA: 0x00084556 File Offset: 0x00082756
		public CrashOnNonContractualExceptionOperationInvoker(OperationDescription operationDescription, DispatchOperation dispatchOperation, IEnumerable<Type> knownExceptions, NonContractualExceptionBehavior crashServerOnNonContractualExceptionBehavior, ICommunicationFrameworkEventsKit eventsKit)
		{
			this.m_operationDescription = operationDescription;
			this.m_innerOperationInvoker = dispatchOperation.Invoker;
			this.m_knownExceptions = knownExceptions;
			this.m_eventsKit = eventsKit;
			this.m_crashServerOnNonContractualExceptionBehavior = crashServerOnNonContractualExceptionBehavior;
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x00084588 File Offset: 0x00082788
		public object[] AllocateInputs()
		{
			return this.m_innerOperationInvoker.AllocateInputs();
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x00084598 File Offset: 0x00082798
		private void CrashHandler(Action action)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Warning, "ECF service (contract: {0}, operation: {1}) throws an exception: {2}", new object[]
				{
					this.m_operationDescription.DeclaringContract.Name,
					this.m_operationDescription.Name,
					ex
				});
				if (ExtendedEnvironment.CrashOnUnhandledCommunicationException)
				{
					this.FailServerOnNonCotractExceptions(ex);
				}
				throw;
			}
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x0008460C File Offset: 0x0008280C
		public object Invoke(object instance, object[] inputs, out object[] outputs)
		{
			object[] outs = null;
			object ret = null;
			Action <>9__1;
			TopLevelHandler.Run(this, TopLevelHandlerOption.PassNonfatal, delegate
			{
				CrashOnNonContractualExceptionOperationInvoker <>4__this = this;
				Action action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate
					{
						ret = this.m_innerOperationInvoker.Invoke(instance, inputs, out outs);
					});
				}
				<>4__this.CrashHandler(action);
			});
			outputs = outs;
			return ret;
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x00084664 File Offset: 0x00082864
		public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
		{
			IAsyncResult ret = null;
			Action <>9__1;
			TopLevelHandler.Run(this, TopLevelHandlerOption.PassNonfatal, delegate
			{
				CrashOnNonContractualExceptionOperationInvoker <>4__this = this;
				Action action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate
					{
						ret = this.m_innerOperationInvoker.InvokeBegin(instance, inputs, callback, state);
					});
				}
				<>4__this.CrashHandler(action);
			});
			return ret;
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000846BC File Offset: 0x000828BC
		public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
		{
			object[] outs = null;
			object ret = null;
			Action <>9__1;
			TopLevelHandler.Run(this, TopLevelHandlerOption.PassNonfatal, delegate
			{
				CrashOnNonContractualExceptionOperationInvoker <>4__this = this;
				Action action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate
					{
						ret = this.m_innerOperationInvoker.InvokeEnd(instance, out outs, result);
					});
				}
				<>4__this.CrashHandler(action);
			});
			outputs = outs;
			return ret;
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06002543 RID: 9539 RVA: 0x00084714 File Offset: 0x00082914
		public bool IsSynchronous
		{
			get
			{
				return this.m_innerOperationInvoker.IsSynchronous;
			}
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x00084724 File Offset: 0x00082924
		private Type GetInnerTypeFromWebFaultException(Type type)
		{
			if (!type.IsGenericType)
			{
				return type;
			}
			Type genericTypeDefinition = type.GetGenericTypeDefinition();
			Type typeFromHandle = typeof(WebFaultException<>);
			if (genericTypeDefinition != typeFromHandle)
			{
				return type;
			}
			Type[] genericArguments = type.GetGenericArguments();
			if (genericArguments.Length != 1)
			{
				return type;
			}
			if (genericArguments[0].IsGenericParameter)
			{
				return type;
			}
			return genericArguments[0];
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x00084774 File Offset: 0x00082974
		private bool ExceptionTypesMatch(Type operationExceptionType, Type faultExceptionType)
		{
			return faultExceptionType.Equals(operationExceptionType) || operationExceptionType.IsSubclassOf(faultExceptionType);
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x00084788 File Offset: 0x00082988
		private bool ExceptionTypesMatch(Type operationExceptionType, FaultDescription faultDescription)
		{
			Type detailType = faultDescription.DetailType;
			return this.ExceptionTypesMatch(operationExceptionType, detailType);
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x000847A4 File Offset: 0x000829A4
		private bool ExceptionIsInFaultCollection(Type operationExceptionType, FaultDescriptionCollection declaredFaults)
		{
			operationExceptionType = this.GetInnerTypeFromWebFaultException(operationExceptionType);
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Looking service fault of type {0} in list of declared faults (there are {1} of them)", new object[] { operationExceptionType.FullName, declaredFaults.Count });
			foreach (FaultDescription faultDescription in declaredFaults)
			{
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Comparing service exception {0} to well-known exception {1}", new object[]
				{
					operationExceptionType.FullName,
					faultDescription.DetailType.FullName
				});
				if (this.ExceptionTypesMatch(operationExceptionType, faultDescription))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x0008485C File Offset: 0x00082A5C
		private bool ExceptionIsInKnownExceptionList(Type operationExceptionType)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Looking service fault of type {0} in configuration well-known exceptions (there are {1} of them)", new object[]
			{
				operationExceptionType.FullName,
				this.m_knownExceptions.Count<Type>()
			});
			foreach (Type type in this.m_knownExceptions)
			{
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Comparing service exception {0} to well-known exception {1}", new object[] { operationExceptionType.FullName, type.FullName });
				if (this.ExceptionTypesMatch(operationExceptionType, type))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x00084910 File Offset: 0x00082B10
		private void FailServerOnNonCotractExceptions(Exception error)
		{
			ExtendedEnvironment.ApplyFailSlowOnFatalPolicy(this, error);
			Type type = error.GetType();
			if (!this.ExceptionIsInKnownExceptionList(type) && (this.ShouldCrashOnNonSpecifiedExceptions(type) || this.ShouldCrashOnNonMonitoredExceptions(type) || this.ShouldCrashOnNonMonitoredExceptionsButAllowSpecifiedExceptions(type)))
			{
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Fatal, "ECF service (contract: {0}, operation: {1}) throws a non-recognized exception of type: {2}", new object[]
				{
					this.m_operationDescription.DeclaringContract.Name,
					this.m_operationDescription.Name,
					error.GetType().FullName
				});
				MonitoredException ex = new MonitoredException("Service threw an unexpected exception. Server will shut down.", error);
				this.m_eventsKit.NotifyUnknownExceptionThrownByServer(this.m_operationDescription.DeclaringContract.Name, this.m_operationDescription.Name, ex);
				ExtendedEnvironment.FailSlow(this, error);
			}
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000849D1 File Offset: 0x00082BD1
		private bool ShouldCrashOnNonMonitoredExceptionsButAllowSpecifiedExceptions(Type exceptionType)
		{
			return this.m_crashServerOnNonContractualExceptionBehavior == NonContractualExceptionBehavior.CrashOnNonMonitoredExceptionsButAllowSpecifiedExceptions && !typeof(MonitoredException).IsAssignableFrom(exceptionType) && !this.ExceptionIsInFaultCollection(exceptionType, this.m_operationDescription.Faults);
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x00084A07 File Offset: 0x00082C07
		private bool ShouldCrashOnNonSpecifiedExceptions(Type exceptionType)
		{
			return this.m_crashServerOnNonContractualExceptionBehavior == NonContractualExceptionBehavior.CrashOnNonSpecifiedExceptions && !this.ExceptionIsInFaultCollection(exceptionType, this.m_operationDescription.Faults);
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x00084A29 File Offset: 0x00082C29
		private bool ShouldCrashOnNonMonitoredExceptions(Type exceptionType)
		{
			return this.m_crashServerOnNonContractualExceptionBehavior == NonContractualExceptionBehavior.CrashOnNonMonitoredExceptions && !typeof(MonitoredException).IsAssignableFrom(exceptionType);
		}

		// Token: 0x04000D1C RID: 3356
		private IEnumerable<Type> m_knownExceptions;

		// Token: 0x04000D1D RID: 3357
		private ICommunicationFrameworkEventsKit m_eventsKit;

		// Token: 0x04000D1E RID: 3358
		private IOperationInvoker m_innerOperationInvoker;

		// Token: 0x04000D1F RID: 3359
		private OperationDescription m_operationDescription;

		// Token: 0x04000D20 RID: 3360
		private NonContractualExceptionBehavior m_crashServerOnNonContractualExceptionBehavior;
	}
}
