using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000D6 RID: 214
	public abstract class ExceptionLogger : IExceptionLogger
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x0000E27D File Offset: 0x0000C47D
		Task IExceptionLogger.LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExceptionContext exceptionContext = context.ExceptionContext;
			if (!this.ShouldLog(context))
			{
				return TaskHelpers.Completed();
			}
			return this.LogAsync(context, cancellationToken);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000E2AB File Offset: 0x0000C4AB
		public virtual Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
		{
			this.Log(context);
			return TaskHelpers.Completed();
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00005744 File Offset: 0x00003944
		public virtual void Log(ExceptionLoggerContext context)
		{
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000E2BC File Offset: 0x0000C4BC
		public virtual bool ShouldLog(ExceptionLoggerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			IDictionary data = context.ExceptionContext.Exception.Data;
			if (data == null || data.IsReadOnly)
			{
				return true;
			}
			ICollection<object> collection;
			if (data.Contains("MS_LoggedBy"))
			{
				collection = data["MS_LoggedBy"] as ICollection<object>;
				if (collection == null)
				{
					return true;
				}
				if (collection.Contains(this))
				{
					return false;
				}
			}
			else
			{
				collection = new List<object>();
				data.Add("MS_LoggedBy", collection);
			}
			collection.Add(this);
			return true;
		}

		// Token: 0x04000141 RID: 321
		internal const string LoggedByKey = "MS_LoggedBy";
	}
}
