using System;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200018E RID: 398
	internal sealed class SerializationActivityInfoScope : Disposable
	{
		// Token: 0x0600185E RID: 6238 RVA: 0x000A3B25 File Offset: 0x000A1D25
		public SerializationActivityInfoScope(SerializationActivityContext context, string activityInfoKey)
		{
			this.context = context;
			this.activityInfoKey = activityInfoKey;
			this.hasPreviousInfo = context.ActivityInfo.TryGetValue(activityInfoKey, out this.previousInfo);
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x000A3B53 File Offset: 0x000A1D53
		public SerializationActivityInfoScope(SerializationActivityContext context, string activityInfoKey, object info)
			: this(context, activityInfoKey)
		{
			this.Info = info;
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x000A3B64 File Offset: 0x000A1D64
		// (set) Token: 0x06001861 RID: 6241 RVA: 0x000A3B82 File Offset: 0x000A1D82
		public object Info
		{
			get
			{
				base.ThrowIfAlreadyDisposed();
				return this.context.ActivityInfo[this.activityInfoKey];
			}
			set
			{
				base.ThrowIfAlreadyDisposed();
				this.context.ActivityInfo[this.activityInfoKey] = value;
			}
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x000A3BA1 File Offset: 0x000A1DA1
		public TValue GetInfo<TValue>()
		{
			return (TValue)((object)this.Info);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x000A3BB0 File Offset: 0x000A1DB0
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this.hasPreviousInfo)
					{
						this.context.ActivityInfo[this.activityInfoKey] = this.previousInfo;
					}
					else
					{
						this.context.ActivityInfo.Remove(this.activityInfoKey);
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0400049D RID: 1181
		private readonly SerializationActivityContext context;

		// Token: 0x0400049E RID: 1182
		private readonly string activityInfoKey;

		// Token: 0x0400049F RID: 1183
		private readonly bool hasPreviousInfo;

		// Token: 0x040004A0 RID: 1184
		private readonly object previousInfo;

		// Token: 0x020003B1 RID: 945
		public static class ReservedScopeKey
		{
			// Token: 0x0400115E RID: 4446
			public const string ChildDeserialization = "SerializationActivity::ChildDeserialization";

			// Token: 0x0400115F RID: 4447
			public const string ModelUpdateByObject = "SerializationActivity::ModelUpdateByObject";
		}
	}
}
