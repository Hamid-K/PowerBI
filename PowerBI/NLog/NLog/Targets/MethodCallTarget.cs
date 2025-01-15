using System;
using System.Reflection;
using NLog.Common;
using NLog.Internal;

namespace NLog.Targets
{
	// Token: 0x02000048 RID: 72
	[Target("MethodCall")]
	public sealed class MethodCallTarget : MethodCallTargetBase
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00011D33 File Offset: 0x0000FF33
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x00011D3B File Offset: 0x0000FF3B
		public string ClassName { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x00011D44 File Offset: 0x0000FF44
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x00011D4C File Offset: 0x0000FF4C
		public string MethodName { get; set; }

		// Token: 0x06000726 RID: 1830 RVA: 0x00011D55 File Offset: 0x0000FF55
		public MethodCallTarget()
		{
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00011D5D File Offset: 0x0000FF5D
		public MethodCallTarget(string name)
			: this(name, null)
		{
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00011D67 File Offset: 0x0000FF67
		public MethodCallTarget(string name, Action<LogEventInfo, object[]> logEventAction)
			: this()
		{
			base.Name = name;
			this._logEventAction = logEventAction;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00011D80 File Offset: 0x0000FF80
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			if (this.ClassName != null && this.MethodName != null)
			{
				Type type = Type.GetType(this.ClassName);
				if (type != null)
				{
					MethodInfo method = type.GetMethod(this.MethodName);
					if (method == null)
					{
						InternalLogger.Warn<string, string>("Initialize MethodCallTarget, method '{0}' in class '{1}' not found - it should be static", this.MethodName, this.ClassName);
						return;
					}
					this._logEventAction = MethodCallTarget.BuildLogEventAction(method);
					return;
				}
				else
				{
					InternalLogger.Warn<string>("Initialize MethodCallTarget, class '{0}' not found", this.ClassName);
				}
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00011E02 File Offset: 0x00010002
		private static Action<LogEventInfo, object[]> BuildLogEventAction(MethodInfo methodInfo)
		{
			int neededParameters = methodInfo.GetParameters().Length;
			return delegate(LogEventInfo logEvent, object[] parameters)
			{
				if (neededParameters - parameters.Length > 0)
				{
					object[] array = new object[neededParameters];
					for (int i = 0; i < parameters.Length; i++)
					{
						array[i] = parameters[i];
					}
					for (int j = parameters.Length; j < neededParameters; j++)
					{
						array[j] = Type.Missing;
					}
					parameters = array;
				}
				methodInfo.Invoke(null, parameters);
			};
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00011E30 File Offset: 0x00010030
		protected override void DoInvoke(object[] parameters, AsyncLogEventInfo logEvent)
		{
			try
			{
				this.ExecuteLogMethod(parameters, logEvent.LogEvent);
				logEvent.Continuation(null);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				logEvent.Continuation(ex);
			}
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00011E84 File Offset: 0x00010084
		protected override void DoInvoke(object[] parameters)
		{
			this.ExecuteLogMethod(parameters, null);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00011E8E File Offset: 0x0001008E
		private void ExecuteLogMethod(object[] parameters, LogEventInfo logEvent)
		{
			if (this._logEventAction != null)
			{
				this._logEventAction(logEvent, parameters);
				return;
			}
			InternalLogger.Trace("No invoke because class/method was not found or set");
		}

		// Token: 0x04000151 RID: 337
		private Action<LogEventInfo, object[]> _logEventAction;
	}
}
