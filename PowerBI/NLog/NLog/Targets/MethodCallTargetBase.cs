using System;
using System.Collections.Generic;
using System.Globalization;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.Targets
{
	// Token: 0x02000049 RID: 73
	public abstract class MethodCallTargetBase : Target
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x00011EB0 File Offset: 0x000100B0
		protected MethodCallTargetBase()
		{
			this.Parameters = new List<MethodCallParameter>();
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00011EC3 File Offset: 0x000100C3
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x00011ECB File Offset: 0x000100CB
		[ArrayParameter(typeof(MethodCallParameter), "parameter")]
		public IList<MethodCallParameter> Parameters { get; private set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x00011ED4 File Offset: 0x000100D4
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x00011EFE File Offset: 0x000100FE
		private IPropertyTypeConverter PropertyTypeConverter
		{
			get
			{
				IPropertyTypeConverter propertyTypeConverter;
				if ((propertyTypeConverter = this._propertyTypeConverter) == null)
				{
					propertyTypeConverter = (this._propertyTypeConverter = ConfigurationItemFactory.Default.PropertyTypeConverter);
				}
				return propertyTypeConverter;
			}
			set
			{
				this._propertyTypeConverter = value;
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00011F07 File Offset: 0x00010107
		protected override void CloseTarget()
		{
			this.PropertyTypeConverter = null;
			base.CloseTarget();
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00011F18 File Offset: 0x00010118
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			object[] array = ((this.Parameters.Count > 0) ? new object[this.Parameters.Count] : ArrayHelper.Empty<object>());
			for (int i = 0; i < array.Length; i++)
			{
				try
				{
					array[i] = this.GetParameterValue(logEvent.LogEvent, this.Parameters[i]);
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrownImmediately())
					{
						throw;
					}
					InternalLogger.Warn(ex, "{0}(Name={1}): Failed to get parameter value {2}", new object[]
					{
						base.GetType(),
						base.Name,
						this.Parameters[i].Name
					});
					throw;
				}
			}
			this.DoInvoke(array, logEvent);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00011FD4 File Offset: 0x000101D4
		private object GetParameterValue(LogEventInfo logEvent, MethodCallParameter param)
		{
			Type type = param.ParameterType ?? typeof(string);
			string text = base.RenderLogEvent(param.Layout, logEvent) ?? string.Empty;
			if (type == typeof(string) || type == typeof(object))
			{
				return text;
			}
			if (string.IsNullOrEmpty(text) && type.IsValueType())
			{
				return Activator.CreateInstance(param.ParameterType);
			}
			return this.PropertyTypeConverter.Convert(text, type, null, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00012062 File Offset: 0x00010262
		protected virtual void DoInvoke(object[] parameters, AsyncLogEventInfo logEvent)
		{
			this.DoInvoke(parameters, logEvent.Continuation);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00012074 File Offset: 0x00010274
		protected virtual void DoInvoke(object[] parameters, AsyncContinuation continuation)
		{
			try
			{
				this.DoInvoke(parameters);
				continuation(null);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				continuation(ex);
			}
		}

		// Token: 0x06000738 RID: 1848
		protected abstract void DoInvoke(object[] parameters);

		// Token: 0x04000153 RID: 339
		private IPropertyTypeConverter _propertyTypeConverter;
	}
}
