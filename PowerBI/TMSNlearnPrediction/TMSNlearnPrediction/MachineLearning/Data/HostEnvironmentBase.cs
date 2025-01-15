using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000C1 RID: 193
	public abstract class HostEnvironmentBase : ChannelProviderBase, IHostEnvironment, IChannelProvider, IExceptionContext, IProgressChannelProvider, IDisposable
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00016E07 File Offset: 0x00015007
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x00016E0F File Offset: 0x0001500F
		public int ConcurrencyFactor
		{
			get
			{
				return this._conc;
			}
			set
			{
				this._conc = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00016E18 File Offset: 0x00015018
		public override int Depth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00016E1B File Offset: 0x0001501B
		protected bool IsDisposed
		{
			get
			{
				return this._tempFiles == null;
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00016E28 File Offset: 0x00015028
		protected HostEnvironmentBase(IRandom rand, bool verbose, int conc, string shortName = null, string parentFullName = null)
			: base(shortName, parentFullName, verbose)
		{
			this._rand = rand ?? RandomUtils.Create();
			this._conc = conc;
			this._listenerDict = new ConcurrentDictionary<Type, HostEnvironmentBase.Dispatcher>();
			this._progressTracker = new ProgressReporting.ProgressTracker(this);
			this._tempLock = new object();
			this._tempFiles = new List<IFileHandle>();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00016E88 File Offset: 0x00015088
		protected HostEnvironmentBase(HostEnvironmentBase source, IRandom rand, bool verbose, int conc, string shortName = null, string parentFullName = null)
			: base(shortName, parentFullName, verbose)
		{
			Contracts.CheckValue<HostEnvironmentBase>(source, "source");
			this._rand = rand ?? RandomUtils.Create();
			this._conc = conc;
			this._master = source;
			this._listenerDict = source._listenerDict;
			this._progressTracker = source._progressTracker;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00016EE4 File Offset: 0x000150E4
		public virtual void Dispose()
		{
			if (this._master != null)
			{
				return;
			}
			List<IFileHandle> tempFiles;
			lock (this._tempLock)
			{
				tempFiles = this._tempFiles;
				this._tempFiles = null;
			}
			if (tempFiles == null)
			{
				return;
			}
			foreach (IFileHandle fileHandle in tempFiles)
			{
				fileHandle.Dispose();
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00016F7C File Offset: 0x0001517C
		public IHost Register(string name)
		{
			Contracts.CheckNonEmpty(name, "name");
			return this.RegisterCore(this._rand, null, name, new bool?(base.Verbose), null);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00016FB8 File Offset: 0x000151B8
		public IHost Register(IHostedComponent component)
		{
			Contracts.CheckValue<IHostedComponent>(component, "component");
			Contracts.CheckNonEmpty(component.Name, "component");
			return this.RegisterCore(this._rand, null, component.Name, new bool?(base.Verbose), null);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00017008 File Offset: 0x00015208
		private IHost Register(HostEnvironmentBase.HostBase parent, IHostedComponent component)
		{
			return this.Register(parent, component.Name);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00017017 File Offset: 0x00015217
		private IHost Register(HostEnvironmentBase.HostBase parent, string name)
		{
			return this.RegisterCore(parent.Rand, parent.FullName, name, new bool?(parent.Verbose), parent.Conc);
		}

		// Token: 0x060003F5 RID: 1013
		protected abstract IHost RegisterCore(IRandom rand, string parentFullName, string shortName, bool? verbose, int? conc);

		// Token: 0x060003F6 RID: 1014 RVA: 0x00017040 File Offset: 0x00015240
		public virtual IHostEnvironment Fork(int? seed = null, bool? verbose = null, int? conc = null)
		{
			TauswortheHybrid tauswortheHybrid = ((seed != null) ? RandomUtils.Create(seed.Value) : RandomUtils.Create(this._rand));
			return this.ForkCore(tauswortheHybrid, verbose, conc);
		}

		// Token: 0x060003F7 RID: 1015
		protected abstract IHostEnvironment ForkCore(IRandom rand, bool? verbose = null, int? conc = null);

		// Token: 0x060003F8 RID: 1016 RVA: 0x00017079 File Offset: 0x00015279
		public IFileHandle OpenInputFile(string path)
		{
			if (this._master != null)
			{
				return this._master.OpenInputFileCore(this, path);
			}
			return this.OpenInputFileCore(this, path);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00017099 File Offset: 0x00015299
		public IProgressChannel StartProgressChannel(string name)
		{
			Contracts.CheckNonEmpty(name, "name");
			return this.StartProgressChannelCore(null, name);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000170AF File Offset: 0x000152AF
		protected virtual IProgressChannel StartProgressChannelCore(HostEnvironmentBase.HostBase host, string name)
		{
			return new ProgressReporting.ProgressChannel(this, this._progressTracker, name);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000170BE File Offset: 0x000152BE
		protected virtual IFileHandle OpenInputFileCore(IHostEnvironment env, string path)
		{
			Contracts.CheckNonWhiteSpace(this, path, "path");
			if (this._master != null)
			{
				return this._master.OpenInputFileCore(env, path);
			}
			return new SimpleFileHandle(env, path, false, false);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000170EC File Offset: 0x000152EC
		public IFileHandle CreateOutputFile(string path)
		{
			if (this._master != null)
			{
				return this._master.CreateOutputFileCore(this, path);
			}
			return this.CreateOutputFileCore(this, path);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001710C File Offset: 0x0001530C
		protected virtual IFileHandle CreateOutputFileCore(IHostEnvironment env, string path)
		{
			Contracts.CheckNonWhiteSpace(this, path, "path");
			if (this._master != null)
			{
				return this._master.CreateOutputFileCore(env, path);
			}
			return new SimpleFileHandle(env, path, true, false);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001713A File Offset: 0x0001533A
		public IFileHandle CreateTempFile(string suffix = null, string prefix = null)
		{
			if (this._master != null)
			{
				return this._master.CreateAndRegisterTempFile(this, null, null);
			}
			return this.CreateAndRegisterTempFile(this, null, null);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001715C File Offset: 0x0001535C
		protected IFileHandle CreateAndRegisterTempFile(IHostEnvironment env, string suffix = null, string prefix = null)
		{
			if (this._master != null)
			{
				return this._master.CreateAndRegisterTempFile(env, suffix, prefix);
			}
			IFileHandle fileHandle = this.CreateTempFileCore(env, suffix, prefix);
			lock (this._tempLock)
			{
				if (this._tempFiles == null)
				{
					fileHandle.Dispose();
					throw Contracts.Except(env, "This environment has been disposed, so can't allocate new temp files");
				}
				this._tempFiles.Add(fileHandle);
			}
			return fileHandle;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000171E4 File Offset: 0x000153E4
		protected virtual IFileHandle CreateTempFileCore(IHostEnvironment env, string suffix = null, string prefix = null)
		{
			Contracts.CheckParam(this, !this.HasBadFileCharacters(suffix), "suffix");
			Contracts.CheckParam(this, !this.HasBadFileCharacters(prefix), "prefix");
			Guid guid = Guid.NewGuid();
			string fullPath = Path.GetFullPath(Path.Combine(Path.GetTempPath(), prefix + guid.ToString() + suffix));
			return new SimpleFileHandle(env, fullPath, true, true);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00017250 File Offset: 0x00015450
		protected virtual bool HasBadFileCharacters(string str = null)
		{
			if (string.IsNullOrEmpty(str))
			{
				return false;
			}
			char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
			return str.IndexOfAny(invalidFileNameChars) >= 0;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001727A File Offset: 0x0001547A
		private void DispatchMessageCore<TMessage>(Action<HostEnvironmentBase.IMessageSource, TMessage> listenerAction, HostEnvironmentBase.IMessageSource channel, TMessage message)
		{
			if (listenerAction != null)
			{
				listenerAction(channel, message);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00017288 File Offset: 0x00015488
		protected Action<HostEnvironmentBase.IMessageSource, TMessage> GetDispatchDelegate<TMessage>()
		{
			HostEnvironmentBase.Dispatcher<TMessage> dispatcher = this.EnsureDispatcher<TMessage>();
			return dispatcher.Dispatch;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000172A4 File Offset: 0x000154A4
		protected HostEnvironmentBase.Dispatcher<TMessage> EnsureDispatcher<TMessage>()
		{
			HostEnvironmentBase.Dispatcher dispatcher;
			if (!this._listenerDict.TryGetValue(typeof(TMessage), out dispatcher) && !this._listenerDict.TryAdd(typeof(TMessage), dispatcher = new HostEnvironmentBase.Dispatcher<TMessage>()))
			{
				dispatcher = this._listenerDict[typeof(TMessage)];
			}
			return (HostEnvironmentBase.Dispatcher<TMessage>)dispatcher;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00017304 File Offset: 0x00015504
		public override IChannel Start(string name)
		{
			return this.CreateCommChannel(this, name);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001730E File Offset: 0x0001550E
		public override IPipe<TMessage> StartPipe<TMessage>(string name)
		{
			return this.CreatePipe<TMessage>(this, name);
		}

		// Token: 0x06000407 RID: 1031
		protected abstract IChannel CreateCommChannel(ChannelProviderBase parent, string name);

		// Token: 0x06000408 RID: 1032
		protected abstract IPipe<TMessage> CreatePipe<TMessage>(ChannelProviderBase parent, string name);

		// Token: 0x06000409 RID: 1033 RVA: 0x00017318 File Offset: 0x00015518
		public void AddListener<TMessage>(Action<HostEnvironmentBase.IMessageSource, TMessage> listenerFunc)
		{
			Contracts.CheckValue<Action<HostEnvironmentBase.IMessageSource, TMessage>>(listenerFunc, "listenerFunc");
			HostEnvironmentBase.Dispatcher<TMessage> dispatcher = this.EnsureDispatcher<TMessage>();
			dispatcher.AddListener(listenerFunc);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00017340 File Offset: 0x00015540
		public void RemoveListener<TMessage>(Action<HostEnvironmentBase.IMessageSource, TMessage> listenerFunc)
		{
			Contracts.CheckValue<Action<HostEnvironmentBase.IMessageSource, TMessage>>(listenerFunc, "listenerFunc");
			HostEnvironmentBase.Dispatcher dispatcher;
			if (!this._listenerDict.TryGetValue(typeof(TMessage), out dispatcher))
			{
				return;
			}
			HostEnvironmentBase.Dispatcher<TMessage> dispatcher2 = dispatcher as HostEnvironmentBase.Dispatcher<TMessage>;
			dispatcher2.RemoveListener(listenerFunc);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00017380 File Offset: 0x00015580
		public override Exception Process(Exception ex)
		{
			if (ex != null)
			{
				ex.Data["Throwing component"] = "Environment";
				Contracts.Mark(ex);
			}
			return ex;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000173A2 File Offset: 0x000155A2
		protected override void DecorateException(Exception ex)
		{
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x000173A4 File Offset: 0x000155A4
		public override string ContextDescription
		{
			get
			{
				return "HostEnvironment";
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000173AC File Offset: 0x000155AC
		public virtual void PrintMessageNormalized(TextWriter writer, string message, bool removeLastNewLine)
		{
			int num = 0;
			int num2;
			for (;;)
			{
				num2 = num;
				while (num2 < message.Length && message[num2] != '\r' && message[num2] != '\n')
				{
					num2++;
				}
				if (num2 == message.Length)
				{
					break;
				}
				if (num == num2)
				{
					writer.WriteLine();
				}
				else
				{
					writer.WriteLine(message.Substring(num, num2 - num));
				}
				num = num2 + 1;
				if (num < message.Length && message[num2] == '\r' && message[num] == '\n')
				{
					num++;
				}
			}
			if (num < num2)
			{
				writer.WriteLine(message.Substring(num, num2 - num));
				return;
			}
			if (!removeLastNewLine)
			{
				writer.WriteLine();
			}
		}

		// Token: 0x040001B8 RID: 440
		protected readonly HostEnvironmentBase _master;

		// Token: 0x040001B9 RID: 441
		private readonly object _tempLock;

		// Token: 0x040001BA RID: 442
		private volatile List<IFileHandle> _tempFiles;

		// Token: 0x040001BB RID: 443
		protected readonly IRandom _rand;

		// Token: 0x040001BC RID: 444
		protected readonly ConcurrentDictionary<Type, HostEnvironmentBase.Dispatcher> _listenerDict;

		// Token: 0x040001BD RID: 445
		protected readonly ProgressReporting.ProgressTracker _progressTracker;

		// Token: 0x040001BE RID: 446
		protected int _conc;

		// Token: 0x020000C2 RID: 194
		public interface IMessageSource
		{
			// Token: 0x1700003F RID: 63
			// (get) Token: 0x0600040F RID: 1039
			string ShortName { get; }

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x06000410 RID: 1040
			string FullName { get; }

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x06000411 RID: 1041
			bool Verbose { get; }
		}

		// Token: 0x020000C3 RID: 195
		protected abstract class ChildChannelProviderBase : ChannelProviderBase
		{
			// Token: 0x06000412 RID: 1042 RVA: 0x00017450 File Offset: 0x00015650
			protected ChildChannelProviderBase(HostEnvironmentBase master, string shortName, string parentFullName, bool? verbose = null)
				: base(shortName, parentFullName, verbose ?? master.Verbose)
			{
				this.Master = master;
			}

			// Token: 0x06000413 RID: 1043 RVA: 0x00017487 File Offset: 0x00015687
			public override IChannel Start(string name)
			{
				Contracts.CheckNonEmpty(name, "name");
				return this.Master.CreateCommChannel(this, name);
			}

			// Token: 0x06000414 RID: 1044 RVA: 0x000174A4 File Offset: 0x000156A4
			public override IPipe<TMessage> StartPipe<TMessage>(string name)
			{
				Contracts.CheckNonEmpty(name, "name");
				if (typeof(TMessage) == typeof(ChannelMessage))
				{
					return (IPipe<TMessage>)this.Start(name);
				}
				return this.Master.CreatePipe<TMessage>(this, name);
			}

			// Token: 0x06000415 RID: 1045 RVA: 0x000174F2 File Offset: 0x000156F2
			protected override void DecorateException(Exception ex)
			{
				this.Master.DecorateException(ex);
			}

			// Token: 0x040001BF RID: 447
			public readonly HostEnvironmentBase Master;
		}

		// Token: 0x020000C4 RID: 196
		protected abstract class HostBase : HostEnvironmentBase.ChildChannelProviderBase, IHost, IHostEnvironment, IChannelProvider, IExceptionContext, IProgressChannelProvider
		{
			// Token: 0x17000042 RID: 66
			// (get) Token: 0x06000416 RID: 1046 RVA: 0x00017500 File Offset: 0x00015700
			public int? Conc
			{
				get
				{
					return this._conc;
				}
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x06000417 RID: 1047 RVA: 0x00017508 File Offset: 0x00015708
			public int ConcurrencyFactor
			{
				get
				{
					int? conc = this._conc;
					if (conc == null)
					{
						return this.Master.ConcurrencyFactor;
					}
					return conc.GetValueOrDefault();
				}
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x06000418 RID: 1048 RVA: 0x00017538 File Offset: 0x00015738
			public override int Depth
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x06000419 RID: 1049 RVA: 0x0001753B File Offset: 0x0001573B
			public IRandom Rand
			{
				get
				{
					return this._rand;
				}
			}

			// Token: 0x0600041A RID: 1050 RVA: 0x00017543 File Offset: 0x00015743
			protected HostBase(HostEnvironmentBase master, string shortName, string parentFullName, IRandom rand, bool? verbose, int? conc)
				: base(master, shortName, parentFullName, verbose)
			{
				this._rand = rand;
				this._conc = conc;
			}

			// Token: 0x0600041B RID: 1051 RVA: 0x00017560 File Offset: 0x00015760
			public IHost Register(string name)
			{
				Contracts.CheckNonEmpty(name, "name");
				return this.Master.Register(this, name);
			}

			// Token: 0x0600041C RID: 1052 RVA: 0x0001757B File Offset: 0x0001577B
			public IHost Register(IHostedComponent component)
			{
				Contracts.CheckValue<IHostedComponent>(component, "component");
				return this.Master.Register(this, component);
			}

			// Token: 0x0600041D RID: 1053 RVA: 0x00017595 File Offset: 0x00015795
			public IProgressChannel StartProgressChannel(string name)
			{
				Contracts.CheckValue<string>(name, "name");
				return this.Master.StartProgressChannelCore(this, name);
			}

			// Token: 0x0600041E RID: 1054 RVA: 0x000175B0 File Offset: 0x000157B0
			IHostEnvironment IHostEnvironment.Fork(int? seed, bool? verbose, int? conc)
			{
				TauswortheHybrid tauswortheHybrid = ((seed != null) ? RandomUtils.Create(seed.Value) : RandomUtils.Create(this._rand));
				return this.ForkCore(tauswortheHybrid, verbose, conc);
			}

			// Token: 0x0600041F RID: 1055 RVA: 0x000175EC File Offset: 0x000157EC
			IHost IHost.Fork()
			{
				return this.ForkCore(RandomUtils.Create(this.Rand), null, null);
			}

			// Token: 0x06000420 RID: 1056
			protected abstract IHost ForkCore(IRandom rand, bool? verbose = null, int? conc = null);

			// Token: 0x06000421 RID: 1057 RVA: 0x0001761C File Offset: 0x0001581C
			public virtual void Attach(IHostedComponent component)
			{
				Contracts.CheckValue<IHostedComponent>(component, "component");
				Contracts.Check(component.Name == base.ShortName, "Host name mismatch");
			}

			// Token: 0x06000422 RID: 1058 RVA: 0x00017644 File Offset: 0x00015844
			public IFileHandle OpenInputFile(string path)
			{
				return this.Master.OpenInputFileCore(this, path);
			}

			// Token: 0x06000423 RID: 1059 RVA: 0x00017653 File Offset: 0x00015853
			public IFileHandle CreateOutputFile(string path)
			{
				return this.Master.CreateOutputFileCore(this, path);
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x00017662 File Offset: 0x00015862
			public IFileHandle CreateTempFile(string suffix = null, string prefix = null)
			{
				return this.Master.CreateAndRegisterTempFile(this, suffix, prefix);
			}

			// Token: 0x040001C0 RID: 448
			private readonly IRandom _rand;

			// Token: 0x040001C1 RID: 449
			private readonly int? _conc;
		}

		// Token: 0x020000C5 RID: 197
		protected sealed class Host : HostEnvironmentBase.HostBase
		{
			// Token: 0x06000425 RID: 1061 RVA: 0x00017672 File Offset: 0x00015872
			public Host(HostEnvironmentBase master, string shortName, string parentFullName, IRandom rand, bool? verbose, int? conc)
				: base(master, shortName, parentFullName, rand, verbose, conc)
			{
			}

			// Token: 0x06000426 RID: 1062 RVA: 0x00017684 File Offset: 0x00015884
			protected override IHost ForkCore(IRandom rand, bool? verbose = null, int? conc = null)
			{
				HostEnvironmentBase master = this.Master;
				string shortName = base.ShortName;
				string parentFullName = base.ParentFullName;
				bool? flag = new bool?(verbose ?? base.Verbose);
				int? num = conc;
				return new HostEnvironmentBase.Host(master, shortName, parentFullName, rand, flag, (num != null) ? new int?(num.GetValueOrDefault()) : base.Conc);
			}
		}

		// Token: 0x020000C6 RID: 198
		protected abstract class PipeBase<TMessage> : HostEnvironmentBase.ChildChannelProviderBase, IPipe<TMessage>, IChannelProvider, IExceptionContext, IDisposable, HostEnvironmentBase.IMessageSource
		{
			// Token: 0x06000427 RID: 1063 RVA: 0x000176E7 File Offset: 0x000158E7
			protected PipeBase(HostEnvironmentBase master, ChannelProviderBase parent, string shortName, Action<HostEnvironmentBase.IMessageSource, TMessage> dispatch)
				: base(master, shortName, parent.FullName, new bool?(parent.Verbose))
			{
				this.Parent = parent;
				this._depth = parent.Depth + 1;
				this._active = true;
				this._dispatch = dispatch;
			}

			// Token: 0x06000428 RID: 1064 RVA: 0x00017726 File Offset: 0x00015926
			public virtual void Done()
			{
				this._active = false;
			}

			// Token: 0x06000429 RID: 1065 RVA: 0x0001772F File Offset: 0x0001592F
			public void Dispose()
			{
				this.DisposeCore();
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x00017737 File Offset: 0x00015937
			protected virtual void DisposeCore()
			{
				this._active = false;
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x0600042B RID: 1067 RVA: 0x00017740 File Offset: 0x00015940
			public bool IsActive
			{
				get
				{
					return this._active;
				}
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x0600042C RID: 1068 RVA: 0x00017748 File Offset: 0x00015948
			public override int Depth
			{
				get
				{
					return this._depth;
				}
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x00017750 File Offset: 0x00015950
			public void Send(TMessage msg)
			{
				this._dispatch(this, msg);
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x00017760 File Offset: 0x00015960
			public override Exception Process(Exception ex)
			{
				if (ex != null)
				{
					ex.Data["Throwing component"] = this.Parent.ShortName;
					ex.Data["Parent component"] = this.Parent.ParentFullName;
					ex.Data["Phase"] = base.ShortName;
					Contracts.Mark(ex);
					this.Master.DecorateException(ex);
				}
				return ex;
			}

			// Token: 0x040001C2 RID: 450
			private readonly int _depth;

			// Token: 0x040001C3 RID: 451
			private bool _active;

			// Token: 0x040001C4 RID: 452
			protected readonly Action<HostEnvironmentBase.IMessageSource, TMessage> _dispatch;

			// Token: 0x040001C5 RID: 453
			public readonly ChannelProviderBase Parent;
		}

		// Token: 0x020000C7 RID: 199
		protected abstract class ChannelBase : HostEnvironmentBase.PipeBase<ChannelMessage>, IChannel, IPipe<ChannelMessage>, IChannelProvider, IExceptionContext, IDisposable
		{
			// Token: 0x0600042F RID: 1071 RVA: 0x000177D0 File Offset: 0x000159D0
			protected ChannelBase(HostEnvironmentBase master, ChannelProviderBase parent, string shortName, Action<HostEnvironmentBase.IMessageSource, ChannelMessage> dispatch)
				: base(master, parent, shortName, dispatch)
			{
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x000177DD File Offset: 0x000159DD
			public void Trace(string msg)
			{
				this._dispatch(this, new ChannelMessage(0, msg));
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x000177F2 File Offset: 0x000159F2
			public void Trace(string fmt, params object[] args)
			{
				this._dispatch(this, new ChannelMessage(0, fmt, args));
			}

			// Token: 0x06000432 RID: 1074 RVA: 0x00017808 File Offset: 0x00015A08
			public void Error(string msg)
			{
				this._dispatch(this, new ChannelMessage(3, msg));
			}

			// Token: 0x06000433 RID: 1075 RVA: 0x0001781D File Offset: 0x00015A1D
			public void Error(string fmt, params object[] args)
			{
				this._dispatch(this, new ChannelMessage(3, fmt, args));
			}

			// Token: 0x06000434 RID: 1076 RVA: 0x00017833 File Offset: 0x00015A33
			public void Warning(string msg)
			{
				this._dispatch(this, new ChannelMessage(2, msg));
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x00017848 File Offset: 0x00015A48
			public void Warning(string fmt, params object[] args)
			{
				this._dispatch(this, new ChannelMessage(2, fmt, args));
			}

			// Token: 0x06000436 RID: 1078 RVA: 0x0001785E File Offset: 0x00015A5E
			public void Info(string msg)
			{
				this._dispatch(this, new ChannelMessage(1, msg));
			}

			// Token: 0x06000437 RID: 1079 RVA: 0x00017873 File Offset: 0x00015A73
			public void Info(string fmt, params object[] args)
			{
				this._dispatch(this, new ChannelMessage(1, fmt, args));
			}

			// Token: 0x06000438 RID: 1080 RVA: 0x0001788C File Offset: 0x00015A8C
			public void Progress(double completed, double total)
			{
				Contracts.CheckParam(completed >= 0.0, "completed");
				Contracts.CheckParam(total >= 0.0, "total");
				if (total != 0.0)
				{
					this._dispatch(this, new ChannelMessage(0, "Completed {0} of {1}", new object[] { completed, total }));
					return;
				}
				this._dispatch(this, new ChannelMessage(0, "Completed {0}", new object[] { completed }));
			}
		}

		// Token: 0x020000C8 RID: 200
		protected sealed class Pipe<TMessage> : HostEnvironmentBase.PipeBase<TMessage>
		{
			// Token: 0x06000439 RID: 1081 RVA: 0x00017931 File Offset: 0x00015B31
			public Pipe(HostEnvironmentBase master, ChannelProviderBase parent, string shortName, Action<HostEnvironmentBase.IMessageSource, TMessage> dispatch)
				: base(master, parent, shortName, dispatch)
			{
			}
		}

		// Token: 0x020000C9 RID: 201
		protected abstract class Dispatcher
		{
		}

		// Token: 0x020000CA RID: 202
		protected sealed class Dispatcher<TMessage> : HostEnvironmentBase.Dispatcher
		{
			// Token: 0x0600043B RID: 1083 RVA: 0x00017946 File Offset: 0x00015B46
			public Dispatcher()
			{
				this._dispatch = new Action<HostEnvironmentBase.IMessageSource, TMessage>(this.DispatchCore);
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x0600043C RID: 1084 RVA: 0x00017960 File Offset: 0x00015B60
			public Action<HostEnvironmentBase.IMessageSource, TMessage> Dispatch
			{
				get
				{
					return this._dispatch;
				}
			}

			// Token: 0x0600043D RID: 1085 RVA: 0x00017968 File Offset: 0x00015B68
			private void DispatchCore(HostEnvironmentBase.IMessageSource sender, TMessage message)
			{
				Action<HostEnvironmentBase.IMessageSource, TMessage> listenerAction = this._listenerAction;
				if (listenerAction != null)
				{
					listenerAction(sender, message);
				}
			}

			// Token: 0x0600043E RID: 1086 RVA: 0x0001798C File Offset: 0x00015B8C
			public void AddListener(Action<HostEnvironmentBase.IMessageSource, TMessage> listenerFunc)
			{
				lock (this._dispatch)
				{
					this._listenerAction = (Action<HostEnvironmentBase.IMessageSource, TMessage>)Delegate.Combine(this._listenerAction, listenerFunc);
				}
			}

			// Token: 0x0600043F RID: 1087 RVA: 0x000179E4 File Offset: 0x00015BE4
			public void RemoveListener(Action<HostEnvironmentBase.IMessageSource, TMessage> listenerFunc)
			{
				lock (this._dispatch)
				{
					this._listenerAction = (Action<HostEnvironmentBase.IMessageSource, TMessage>)Delegate.Remove(this._listenerAction, listenerFunc);
				}
			}

			// Token: 0x040001C6 RID: 454
			private volatile Action<HostEnvironmentBase.IMessageSource, TMessage> _listenerAction;

			// Token: 0x040001C7 RID: 455
			private readonly Action<HostEnvironmentBase.IMessageSource, TMessage> _dispatch;
		}
	}
}
