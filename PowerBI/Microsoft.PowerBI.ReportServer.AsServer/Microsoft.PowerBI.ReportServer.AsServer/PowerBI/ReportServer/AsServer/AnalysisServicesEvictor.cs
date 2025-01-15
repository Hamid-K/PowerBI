using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.Telemetry.Services;
using Microsoft.PowerBI.ReportServer.AsServer.DataAccessObject;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000004 RID: 4
	public sealed class AnalysisServicesEvictor
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public AnalysisServicesEvictor(AnalysisServicesServer asServer)
			: this(asServer.Settings.ModelCleanupCycle, asServer.Settings.ModelExpiration, asServer.Settings.EvictorInitialDelay, new ModelDataAccessor(asServer.Settings), asServer)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002090 File Offset: 0x00000290
		internal AnalysisServicesEvictor(TimeSpan cleanupInterval, TimeSpan maxModelAge, TimeSpan initialDelay, IModelDataAccessor modelDataAccessor, IAnalysisServicesServer testAnalysisServiceServer)
		{
			this._cleanupInterval = cleanupInterval;
			this._maxModelAge = maxModelAge;
			this._initialDelay = initialDelay;
			this._modelDataAccessor = modelDataAccessor;
			this._analysisServiceServer = testAnalysisServiceServer;
			this._timer = new Timer(new TimerCallback(this.RunEvictionPolicy), null, this._initialDelay, this._cleanupInterval);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EC File Offset: 0x000002EC
		internal void RunEvictionPolicy(object state)
		{
			if (Interlocked.CompareExchange(ref this._myLock, 1, 0) == 0)
			{
				try
				{
					Logger.Info("Started clean up of embedded models", Array.Empty<object>());
					int num = 0;
					foreach (long num2 in this.GetExpiredDatabases())
					{
						try
						{
							this._analysisServiceServer.DeleteDatabase(num2);
							num++;
						}
						catch (Exception ex)
						{
							Logger.Error(ex, "Deleting model {0}", new object[] { num2 });
						}
					}
					Logger.Info("Finished clean up of {0} embedded models", new object[] { num });
				}
				finally
				{
					Interlocked.CompareExchange(ref this._myLock, 0, 1);
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021CC File Offset: 0x000003CC
		private List<long> GetExpiredDatabases()
		{
			List<long> list = new List<long>();
			try
			{
				foreach (ModelInfoEntity modelInfoEntity in this._modelDataAccessor.GetModelInfo())
				{
					DateTime? lastUsed = this.GetLastUsed(modelInfoEntity);
					if (lastUsed != null && lastUsed.Value < DateTime.UtcNow.Subtract(this._maxModelAge))
					{
						this.TrackTelemetryEvent(modelInfoEntity);
						list.Add(modelInfoEntity.ModelId);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Getting list of expired models", Array.Empty<object>());
			}
			return list;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002284 File Offset: 0x00000484
		public DateTime? GetLastUsed(ModelInfoEntity model)
		{
			if (model.LastQueried == null || model.LastModified == null)
			{
				return model.LastQueried;
			}
			if (model.LastQueried.Value > model.LastModified)
			{
				return model.LastQueried;
			}
			return model.LastModified;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022F8 File Offset: 0x000004F8
		private void TrackTelemetryEvent(ModelInfoEntity model)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string> { 
			{
				"modelid",
				model.ModelId.ToString()
			} };
			if (model.LastModified != null)
			{
				dictionary.Add("createdTimestamp", model.LastModified.Value.ToUniversalTime().ToString(CultureInfo.InvariantCulture));
			}
			if (model.LastQueried != null)
			{
				dictionary.Add("lastQueriedTimestamp", model.LastQueried.Value.ToUniversalTime().ToString(CultureInfo.InvariantCulture));
			}
			if (model.LastModified != null && model.LastQueried != null)
			{
				TimeSpan? timeSpan = model.LastQueried - model.LastModified;
				TimeSpan? timeSpan2 = DateTime.UtcNow - model.LastModified;
				dictionary.Add("modelUsageInMinutes", timeSpan.Value.TotalMinutes.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("modelLifetimeInMinutes", timeSpan2.Value.TotalMinutes.ToString(CultureInfo.InvariantCulture));
			}
			TelemetryService.Current.TrackEvent("RS.PBI.EvictedModel", dictionary, null);
		}

		// Token: 0x04000029 RID: 41
		private int _myLock;

		// Token: 0x0400002A RID: 42
		private readonly TimeSpan _maxModelAge;

		// Token: 0x0400002B RID: 43
		private readonly TimeSpan _cleanupInterval;

		// Token: 0x0400002C RID: 44
		private readonly TimeSpan _initialDelay;

		// Token: 0x0400002D RID: 45
		private readonly IModelDataAccessor _modelDataAccessor;

		// Token: 0x0400002E RID: 46
		private readonly IAnalysisServicesServer _analysisServiceServer;

		// Token: 0x0400002F RID: 47
		private readonly Timer _timer;
	}
}
