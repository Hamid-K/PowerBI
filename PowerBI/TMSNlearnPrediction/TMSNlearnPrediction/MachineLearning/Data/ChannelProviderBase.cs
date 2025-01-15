using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000BF RID: 191
	public abstract class ChannelProviderBase : IChannelProvider, IExceptionContext
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00016D34 File Offset: 0x00014F34
		public string ShortName
		{
			get
			{
				return this._shortName;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00016D3C File Offset: 0x00014F3C
		public string ParentFullName
		{
			get
			{
				return this._parentFullName;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00016D44 File Offset: 0x00014F44
		public string FullName
		{
			get
			{
				return this._fullName;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00016D4C File Offset: 0x00014F4C
		public bool Verbose
		{
			get
			{
				return this._verbose;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060003E2 RID: 994
		public abstract int Depth { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00016D54 File Offset: 0x00014F54
		public virtual string ContextDescription
		{
			get
			{
				return this.FullName;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00016D5C File Offset: 0x00014F5C
		protected ChannelProviderBase(string shortName, string parentFullName, bool verbose)
		{
			this._parentFullName = (string.IsNullOrEmpty(parentFullName) ? null : parentFullName);
			this._shortName = (string.IsNullOrEmpty(shortName) ? null : shortName);
			this._fullName = this.GenerateFullName();
			this._verbose = verbose;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00016D9B File Offset: 0x00014F9B
		protected virtual string GenerateFullName()
		{
			if (string.IsNullOrEmpty(this._parentFullName))
			{
				return this._shortName;
			}
			return string.Format("{0}; {1}", this._parentFullName, this._shortName);
		}

		// Token: 0x060003E6 RID: 998
		public abstract IChannel Start(string name);

		// Token: 0x060003E7 RID: 999
		public abstract IPipe<TMessage> StartPipe<TMessage>(string name);

		// Token: 0x060003E8 RID: 1000 RVA: 0x00016DC7 File Offset: 0x00014FC7
		public virtual Exception Process(Exception ex)
		{
			if (ex != null)
			{
				ex.Data["Throwing component"] = this.ShortName;
				ex.Data["Parent component"] = this.ParentFullName;
				Contracts.Mark(ex);
				this.DecorateException(ex);
			}
			return ex;
		}

		// Token: 0x060003E9 RID: 1001
		protected abstract void DecorateException(Exception ex);

		// Token: 0x040001B1 RID: 433
		private readonly string _shortName;

		// Token: 0x040001B2 RID: 434
		private readonly string _parentFullName;

		// Token: 0x040001B3 RID: 435
		private readonly string _fullName;

		// Token: 0x040001B4 RID: 436
		private readonly bool _verbose;

		// Token: 0x020000C0 RID: 192
		public static class ExceptionContextKeys
		{
			// Token: 0x040001B5 RID: 437
			public const string ThrowingComponent = "Throwing component";

			// Token: 0x040001B6 RID: 438
			public const string ParentComponent = "Parent component";

			// Token: 0x040001B7 RID: 439
			public const string Phase = "Phase";
		}
	}
}
