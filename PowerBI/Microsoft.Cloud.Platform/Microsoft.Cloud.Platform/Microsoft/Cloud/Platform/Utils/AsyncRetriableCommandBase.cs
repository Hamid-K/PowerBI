using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000191 RID: 401
	public abstract class AsyncRetriableCommandBase
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x00023CA5 File Offset: 0x00021EA5
		public AsyncRetriableCommandBase(int numberOfAttempts, string commandDescription)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(numberOfAttempts, "numberOfAttempts");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(commandDescription, "commandDescription");
			this.m_numberOfAttempts = numberOfAttempts;
			this.m_commandDescription = commandDescription;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00023CDC File Offset: 0x00021EDC
		public AsyncRetriableCommandBase(int numberOfAttempts, string commandDescription, TimeSpan timeBetweenAttempts)
			: this(numberOfAttempts, commandDescription)
		{
			this.m_timeBetweenAttempts = timeBetweenAttempts;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00023CED File Offset: 0x00021EED
		public string Description
		{
			get
			{
				return this.m_commandDescription;
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0000E568 File Offset: 0x0000C768
		protected virtual bool IsPermanentException(Exception e)
		{
			return false;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000181D7 File Offset: 0x000163D7
		protected virtual Exception HandlePermanentException(Exception e)
		{
			return e;
		}

		// Token: 0x06000A58 RID: 2648
		protected abstract bool IsRetriableException(Exception e);

		// Token: 0x06000A59 RID: 2649
		protected abstract Task RunInternal();

		// Token: 0x06000A5A RID: 2650 RVA: 0x00023CF8 File Offset: 0x00021EF8
		public async Task Run()
		{
			ExceptionDispatchInfo dispatchInfo = null;
			while (this.m_numberOfAttempts > 0)
			{
				bool failedWithRetriableException = false;
				TraceSourceBase<UtilsTrace>.Tracer.TraceInformation("Executing AsyncRetriableCommand '{0}'", new object[] { this.m_commandDescription });
				try
				{
					await this.RunInternal();
					return;
				}
				catch (Exception ex)
				{
					dispatchInfo = ExceptionDispatchInfo.Capture(ex);
					if (ex.IsFatal())
					{
						TraceSourceBase<UtilsTrace>.Tracer.TraceError("AsyncRetriableCommand '{0}' encountered a fatal exception {1}.", new object[] { this.m_commandDescription, ex });
						dispatchInfo.Throw();
					}
					else if (this.IsPermanentException(ex))
					{
						TraceSourceBase<UtilsTrace>.Tracer.TraceError("AsyncRetriableCommand '{0}' encountered a permanent exception {1}.", new object[] { this.m_commandDescription, ex });
						Exception ex2 = this.HandlePermanentException(ex);
						if (ex2 == ex)
						{
							dispatchInfo.Throw();
						}
						if (ex2 != null)
						{
							throw ex2;
						}
						TraceSourceBase<UtilsTrace>.Tracer.TraceWarning("AsyncRetriableCommand swallowed permanent exception: {0}.", new object[] { ex });
						return;
					}
					else if (this.IsRetriableException(ex))
					{
						this.m_numberOfAttempts--;
						failedWithRetriableException = true;
						TraceSourceBase<UtilsTrace>.Tracer.TraceWarning("AsyncRetriableCommand '{0}' encountered a retriable exception {1}. {2} attempts remain.", new object[] { this.m_commandDescription, ex, this.m_numberOfAttempts });
						if (this.m_numberOfAttempts <= 0)
						{
							TraceSourceBase<UtilsTrace>.Tracer.TraceError("AsyncRetriableCommand '{0}' exhausted all retries. Treating exception as a permanent one.", new object[] { this.m_commandDescription });
							Exception ex3 = this.HandlePermanentException(ex);
							if (ex3 != null && ex3 != ex)
							{
								throw ex3;
							}
						}
					}
					else
					{
						TraceSourceBase<UtilsTrace>.Tracer.TraceError("AsyncRetriableCommand '{0}' encountered an unknown exception {1}.", new object[] { this.m_commandDescription, ex });
						dispatchInfo.Throw();
					}
				}
				if (this.m_numberOfAttempts > 0 && failedWithRetriableException && this.m_timeBetweenAttempts > TimeSpan.Zero)
				{
					await Task.Delay(this.m_timeBetweenAttempts);
					continue;
				}
				continue;
			}
			if (dispatchInfo != null)
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceError("AsyncRetriableCommand exhausted all retries.");
				dispatchInfo.Throw();
			}
		}

		// Token: 0x0400040E RID: 1038
		private readonly string m_commandDescription;

		// Token: 0x0400040F RID: 1039
		private readonly TimeSpan m_timeBetweenAttempts = TimeSpan.Zero;

		// Token: 0x04000410 RID: 1040
		private int m_numberOfAttempts;
	}
}
